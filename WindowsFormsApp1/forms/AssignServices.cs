using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.forms
{
    public partial class AssignServices : Form
    {
                string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
        private int currentTourOperatorId;

        // Declare class-level fields for dynamic controls
        private DateTimePicker dtpArrivalTimeTransport;
        private DateTimePicker dtpEstimatedArrivalTimeTransport;
        private NumericUpDown nudNoOfRooms;

        public AssignServices(int tourOperatorId = 2001)
        {
            InitializeComponent();
            this.currentTourOperatorId = tourOperatorId;
            AssignServices_Load(null, null); // Load data on form initialization
        }

        private void AssignServices_Load(object sender, EventArgs e)
        {
            // Populate TravellerID dropdown with confirmed bookings
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT DISTINCT b.TravellerID
                        FROM Booking b
                        WHERE b.Status = 'Confirmed'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbTraveller.Items.Add(reader["TravellerID"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading traveller IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Populate ServiceProviderID dropdown
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ServiceProviderID FROM ServiceProvider";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbHotel.Items.Add(reader["ServiceProviderID"].ToString()); // Repurpose cmbHotel as ServiceProviderID
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading service provider IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Rename cmbHotel to cmbServiceProvider for clarity (in code, not designer)
            cmbHotel.Name = "cmbServiceProvider";

            // Initialize dynamic controls
            dtpArrivalTimeTransport = new DateTimePicker
            {
                Location = new Point(430, 480),
                Size = new Size(200, 50),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd HH:mm",
                Visible = false
            };
            dtpEstimatedArrivalTimeTransport = new DateTimePicker
            {
                Location = new Point(430, 520),
                Size = new Size(200, 50),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd HH:mm",
                Visible = false
            };
            nudNoOfRooms = new NumericUpDown
            {
                Location = new Point(430, 550),
                Size = new Size(150, 25),
                Minimum = 1,
                Maximum = 20,
                Visible = false
            };

            // Add labels and controls to the form
            Label lblNoOfRooms = new Label
            {
                Text = "Number of Rooms:",
                Location = new Point(350, 550),
                Size = new Size(100, 20),
                Visible = false
            };
            Label lblArrivalTime = new Label
            {
                Text = "Arrival Time:",
                Location = new Point(350, 480),
                Size = new Size(100, 20),
                Visible = false
            };
            Label lblEstimatedArrivalTime = new Label
            {
                Text = "Est. Departure Time:",
                Location = new Point(320, 520),
                Size = new Size(100, 20),
                Visible = false
            };

            this.Controls.AddRange(new Control[] { lblNoOfRooms, nudNoOfRooms, lblArrivalTime, dtpArrivalTimeTransport, lblEstimatedArrivalTime, dtpEstimatedArrivalTimeTransport });

            // Event handlers for showing/hiding fields
            clbServices.ItemCheck += (s, args) =>
            {
                string item = clbServices.Items[args.Index].ToString();
                lblNoOfRooms.Visible = item == "Hotel" && args.NewValue == CheckState.Checked;
                nudNoOfRooms.Visible = item == "Hotel" && args.NewValue == CheckState.Checked;
                lblArrivalTime.Visible = item == "Transport" && args.NewValue == CheckState.Checked;
                dtpArrivalTimeTransport.Visible = item == "Transport" && args.NewValue == CheckState.Checked;
                lblEstimatedArrivalTime.Visible = item == "Transport" && args.NewValue == CheckState.Checked;
                dtpEstimatedArrivalTimeTransport.Visible = item == "Transport" && args.NewValue == CheckState.Checked;
            };
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (cmbTraveller.SelectedIndex == -1 || cmbHotel.SelectedIndex == -1)
            {
                MessageBox.Show("Please select both Traveller ID and Service Provider ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clbServices.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one service to assign.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if Transport, TourGuide, and Hotel are all selected
            bool isTransportChecked = clbServices.CheckedItems.Contains("Transport");
            bool isTourGuideChecked = clbServices.CheckedItems.Contains("TourGuide");
            bool isHotelChecked = clbServices.CheckedItems.Contains("Hotel");

            if (isTransportChecked && isTourGuideChecked && isHotelChecked)
            {
                // Ensure controls are visible and prompt for data
                nudNoOfRooms.Visible = true;
                dtpEstimatedArrivalTimeTransport.Visible = true;

                // Custom dialog for Estimated Departure Time
                using (Form departureForm = new Form())
                {
                    departureForm.Text = "Enter Details";
                    departureForm.Size = new Size(300, 200);
                    departureForm.StartPosition = FormStartPosition.CenterParent;

                    Label lblDepartureTime = new Label
                    {
                        Text = "Estimated Departure Time:",
                        Location = new Point(20, 20),
                        Size = new Size(150, 20)
                    };
                    DateTimePicker dtpDeparture = new DateTimePicker
                    {
                        Location = new Point(20, 50),
                        Size = new Size(200, 25),
                        Format = DateTimePickerFormat.Custom,
                        CustomFormat = "yyyy-MM-dd HH:mm",
                        Value = DateTime.Now // Default to current time
                    };
                    Button btnOk = new Button
                    {
                        Text = "OK",
                        Location = new Point(100, 130),
                        Size = new Size(75, 25)
                    };

                    btnOk.Click += (s, args) =>
                    {
                        dtpEstimatedArrivalTimeTransport.Value = dtpDeparture.Value;
                        departureForm.Close();
                    };

                    departureForm.Controls.AddRange(new Control[] { lblDepartureTime, dtpDeparture, btnOk });
                    departureForm.ShowDialog();

                    // Validate and ensure nudNoOfRooms has a value
                    if (nudNoOfRooms.Value == 0) nudNoOfRooms.Value = 1; // Default to 1 if not set
                }
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Generate the next ServiceID
                    string maxIdQuery = "SELECT ISNULL(MAX(ServiceID), 0) + 1 FROM Services";
                    int nextServiceId = Convert.ToInt32(new SqlCommand(maxIdQuery, conn).ExecuteScalar());

                    // Insert into Services table with non-null values for checked services
                    string insertServicesQuery = @"
                        INSERT INTO Services (ServiceID, StatusApproval, IsTransport, ArrivalTimeTransport, EstimatedArrivalTimeTransport, 
                                              IsTourGuide, IsHotel, NoOfRooms, ServiceProviderID)
                        VALUES (@ServiceID, @StatusApproval, @IsTransport, @ArrivalTimeTransport, @EstimatedArrivalTimeTransport, 
                                @IsTourGuide, @IsHotel, @NoOfRooms, @ServiceProviderID)";
                    using (SqlCommand cmd = new SqlCommand(insertServicesQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ServiceID", nextServiceId);
                        cmd.Parameters.AddWithValue("@StatusApproval", "False");
                        cmd.Parameters.AddWithValue("@IsTransport", isTransportChecked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@ArrivalTimeTransport", isTransportChecked ? (object)dtpArrivalTimeTransport.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@EstimatedArrivalTimeTransport", isTransportChecked ? (object)dtpEstimatedArrivalTimeTransport.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsTourGuide", isTourGuideChecked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@IsHotel", isHotelChecked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@NoOfRooms", isHotelChecked ? (object)nudNoOfRooms.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@ServiceProviderID", Convert.ToInt32(cmbHotel.SelectedItem));
                        cmd.ExecuteNonQuery();
                    }

                    // Insert into ProvideServices table
                    string insertProvideServicesQuery = @"
                        INSERT INTO ProvideServices (ServiceID, TourOperatorID, TravellerID)
                        VALUES (@ServiceID, @TourOperatorID, @TravellerID)";
                    using (SqlCommand cmd = new SqlCommand(insertProvideServicesQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ServiceID", nextServiceId);
                        cmd.Parameters.AddWithValue("@TourOperatorID", currentTourOperatorId);
                        cmd.Parameters.AddWithValue("@TravellerID", Convert.ToInt32(cmbTraveller.SelectedItem));
                        cmd.ExecuteNonQuery();
                    }

                    // Update listBox1 for user feedback
                    string servicesText = string.Join(", ", clbServices.CheckedItems.Cast<string>());
                    listBox1.Items.Add($"TravellerID: {cmbTraveller.SelectedItem} → ServiceProviderID: {cmbHotel.SelectedItem} | Services: {servicesText}");

                    MessageBox.Show("Services assigned successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning services: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            operatorSignUp f3 = new operatorSignUp();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OperatorHome f3 = new OperatorHome();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            manageUpdateTrips f3 = new manageUpdateTrips();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            viewEditTrips f3 = new viewEditTrips();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Already on AssignServices form, no action needed
        }

        private void button2_Click(object sender, EventArgs e)
        {
            manageBookings f3 = new manageBookings(currentTourOperatorId); // Pass TourOperatorID
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            analy f3 = new analy(); // Pass TourOperatorID
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // No action needed
        }

        private void clbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No action needed unless additional logic is required
        }
    }
}