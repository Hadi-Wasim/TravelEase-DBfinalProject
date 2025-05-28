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
using System.IO;


namespace WindowsFormsApp1.forms
{
    public partial class viewEditTrips : Form
    {
        public viewEditTrips()
        {
            InitializeComponent();
            LoadTripsFromDatabase();
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

        private void button9_Click(object sender, EventArgs e)
        {
            manageUpdateTrips f3 = new manageUpdateTrips();
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

        private void button2_Click(object sender, EventArgs e)
        {
            manageBookings f3 = new manageBookings();
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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void LoadTripsFromDatabase()
        {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            string query = "SELECT TripID, Title, Type, Destination, Duration, Capacity, Description FROM Trip";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                flowLayoutPanel1.Controls.Clear();

                while (reader.Read())
                {
                    TripTileHadi tile = new TripTileHadi();

                    // Store ID for editing/deleting
                    tile.TripId = Convert.ToInt32(reader["TripID"]);

                    // Set visible data
                    tile.TripTitle = reader["Title"].ToString();
                    tile.TripType = reader["Type"].ToString();
                    tile.Destination = reader["Destination"].ToString();
                    tile.Duration = reader["Duration"].ToString();
                    tile.Capacity = reader["Capacity"].ToString();
                    tile.Description = reader["Description"].ToString();

                    // Static image
                    string staticImagePath = @"C:\Users\SABRI LAPTOP\source\repos\m2\Resources\env2.jpg";
                    if (File.Exists(staticImagePath))
                    {
                        tile.TripImage = Image.FromFile(staticImagePath);
                    }

                    // --- Edit Event ---
                    tile.EditClicked += (s, e) =>
                    {
                        edittripform editForm = new edittripform(tile.TripId);
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadTripsFromDatabase(); // reload after edit
                        }
                    };

                    // --- Delete Event ---
                    tile.DeleteClicked += (s, e) =>
                    {
                        var confirm = MessageBox.Show($"Are you sure you want to delete '{tile.TripTitle}'?", "Confirm Delete", MessageBoxButtons.YesNo);
                        if (confirm == DialogResult.Yes)
                        {
                            DeleteTripFromDatabase(tile.TripId);
                            flowLayoutPanel1.Controls.Remove(tile); // remove from UI
                            LoadTripsFromDatabase();
                        }
                    };

                    flowLayoutPanel1.Controls.Add(tile);
                }

                reader.Close();
            }
        }

        private void DeleteTripFromDatabase(int tripId)
        {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            string deleteQuery = "DELETE FROM Trip WHERE TripID = @TripID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@TripID", tripId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void viewEditTrips_Load(object sender, EventArgs e)
        {
            
        }
    }
}
