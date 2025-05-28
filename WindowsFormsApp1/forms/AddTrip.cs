using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.forms;

namespace WindowsFormsApp1.forms
{
    public partial class AddTrip : Form
    {
                string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
        private int currentTourOperatorId; // Placeholder for logged-in user's TourOperatorID
        public AddTrip()
        {
            this.currentTourOperatorId = 2003; // Set the logged-in user's 
            InitializeComponent();
        }

        private void AddTrip_Load(object sender, EventArgs e)
        {
            // Initialize default values
            startDatePicker.MinDate = DateTime.Today;
            endDatePicker.MinDate = DateTime.Today;
            capacityUpDown.Minimum = 1;
            capacityUpDown.Maximum = 1000;
            typeBox.AutoCompleteCustomSource.Clear();
            typeBox.AutoCompleteCustomSource.AddRange(new string[] {
                "Adventure", "Beach","Business", "City","Corportate", "Cultural", "Culinary", "Desert",
                "Hiking", "Historical", "Leisure", "Nature", "Religious",
                "Scenic", "Trekking", "Wildlife", "Official"
            });
            destinationBox.AutoCompleteCustomSource.Clear();
            destinationBox.AutoCompleteCustomSource.AddRange(new string[] {
                "Paris", "Tokyo", "New York", "Sydney", "Cairo", "Rio de Janeiro",
                "Cape Town", "Bangkok", "Rome", "Barcelona", "Dubai",
                "Machu Picchu", "Santorini", "Kyoto"
            });
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(tripNameBox.Text))
            {
                MessageBox.Show("Trip name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tripNameBox.Text.Length > 100)
            {
                MessageBox.Show("Trip name cannot exceed 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(priceBox.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price (non-negative number).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (endDatePicker.Value < startDatePicker.Value)
            {
                MessageBox.Show("End date cannot be earlier than start date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (capacityUpDown.Value <= 0)
            {
                MessageBox.Show("Capacity must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(typeBox.Text))
            {
                MessageBox.Show("Trip type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (typeBox.Text.Length > 50)
            {
                MessageBox.Show("Trip type cannot exceed 50 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(destinationBox.Text))
            {
                MessageBox.Show("Destination is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (destinationBox.Text.Length > 100)
            {
                MessageBox.Show("Destination cannot exceed 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(descriptionRTB.Text))
            {
                MessageBox.Show("Description is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (descriptionRTB.Text.Length > 500)
            {
                MessageBox.Show("Description cannot exceed 500 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private int GetNextTripId()
        {
            int nextTripId = 1; // Default value if table is empty
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ISNULL(MAX(TripID), 0) + 1 FROM Trip";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    nextTripId = (int)cmd.ExecuteScalar();
                }
            }
            return nextTripId;
        }

        private void addTripBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                int duration = (endDatePicker.Value - startDatePicker.Value).Days;
                decimal price = decimal.Parse(priceBox.Text);
                int newTripId = GetNextTripId();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Trip (TripID, Title, Description, Destination, Type, Capacity, StartDate, EndDate, Duration, Price, TourOperatorID, BookingID)
                                    VALUES (@TripID, @Title, @Description, @Destination, @Type, @Capacity, @StartDate, @EndDate, @Duration, @Price, @TourOperatorID, NULL)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TripID", newTripId);
                        cmd.Parameters.AddWithValue("@Title", tripNameBox.Text);
                        cmd.Parameters.AddWithValue("@Description", descriptionRTB.Text);
                        cmd.Parameters.AddWithValue("@Destination", destinationBox.Text);
                        cmd.Parameters.AddWithValue("@Type", typeBox.Text);
                        cmd.Parameters.AddWithValue("@Capacity", (int)capacityUpDown.Value);
                        cmd.Parameters.AddWithValue("@StartDate", startDatePicker.Value);
                        cmd.Parameters.AddWithValue("@EndDate", endDatePicker.Value);
                        cmd.Parameters.AddWithValue("@Duration", duration);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@TourOperatorID", currentTourOperatorId);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Trip added successfully! New TripID: {newTripId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                this.DialogResult = DialogResult.OK;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            tripNameBox.Text = "";
            priceBox.Text = "";
            startDatePicker.Value = DateTime.Today;
            endDatePicker.Value = DateTime.Today;
            capacityUpDown.Value = 1;
            typeBox.Text = "";
            destinationBox.Text = "";
            descriptionRTB.Text = "";
        }

        private void endDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (endDatePicker.Value < startDatePicker.Value)
            {
                MessageBox.Show("End date cannot be earlier than start date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                endDatePicker.Value = startDatePicker.Value;
            }
        }




        private void button10_Click(object sender, EventArgs e)
        {
            OperatorHome f3 = new OperatorHome();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            operatorSignUp f3 = new operatorSignUp();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            manageBookings f3 = new manageBookings();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form

        }

        private void button8_Click(object sender, EventArgs e)
        {
            viewEditTrips f3 = new viewEditTrips();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form

        }

        private void button11_Click(object sender, EventArgs e)
        {
            AssignServices f3 = new AssignServices();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form

        }

        private void button1_Click(object sender, EventArgs e)
        {
            analy f3 = new analy();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            operatorSignUp f3 = new operatorSignUp();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form

        }

        private void button9_Click(object sender, EventArgs e)
        {
            manageUpdateTrips f3 = new manageUpdateTrips();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form

        }


        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
