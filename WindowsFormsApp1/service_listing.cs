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

namespace WindowsFormsApp1
{
    public partial class service_listing : Form
    {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

        public service_listing()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            // Define columns
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ServiceID",
                HeaderText = "Service ID",
                DataPropertyName = "ServiceID"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StatusApproval",
                HeaderText = "Status",
                DataPropertyName = "StatusApproval"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IsTransport",
                HeaderText = "Is Transport",
                DataPropertyName = "IsTransport"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ArrivalTimeTransport",
                HeaderText = "Arrival Time",
                DataPropertyName = "ArrivalTimeTransport"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EstimatedArrivalTimeTransport",
                HeaderText = "Est. Arrival Time",
                DataPropertyName = "EstimatedArrivalTimeTransport"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IsTourGuide",
                HeaderText = "Is Tour Guide",
                DataPropertyName = "IsTourGuide"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IsHotel",
                HeaderText = "Is Hotel",
                DataPropertyName = "IsHotel"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NoOfRooms",
                HeaderText = "No. of Rooms",
                DataPropertyName = "NoOfRooms"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ServiceProviderID",
                HeaderText = "Provider ID",
                DataPropertyName = "ServiceProviderID"
            });
            DataGridViewButtonColumn btnDeleteColumn = new DataGridViewButtonColumn();
            btnDeleteColumn.Name = "Delete";
            btnDeleteColumn.HeaderText = "Action";
            btnDeleteColumn.Text = "Delete";
            btnDeleteColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnDeleteColumn);

            DataGridViewButtonColumn btnEditColumn = new DataGridViewButtonColumn();
            btnEditColumn.Name = "Edit";
            btnEditColumn.HeaderText = "Action";
            btnEditColumn.Text = "Edit";
            btnEditColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnEditColumn);

            dataGridView1.CellClick += DataGridView1_CellClick;
            this.Controls.Add(dataGridView1);
            addbtn.Click += (s, e) => addService();


            LoadServiceData();

        }

        private void addService()
        {
            string isTransport = Transporttext.Text.Trim();
            string isHotel = HotelText.Text.Trim();
            string isTourGuide = tourGuideText.Text.Trim();
            string arrivalTimeTransport = arrivalText.Text.Trim();
            string estimatedTimeTransport = estimatedText.Text.Trim();
            string noOfRooms = noOfRoomsText.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(isTransport) || (!isTransport.Equals("Yes", StringComparison.OrdinalIgnoreCase) && !isTransport.Equals("No", StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("IsTransport must be 'Yes' or 'No'.");
                return;
            }
            if (string.IsNullOrEmpty(isHotel) || (!isHotel.Equals("Yes", StringComparison.OrdinalIgnoreCase) && !isHotel.Equals("No", StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("IsHotel must be 'Yes' or 'No'.");
                return;
            }
            if (string.IsNullOrEmpty(isTourGuide) || (!isTourGuide.Equals("Yes", StringComparison.OrdinalIgnoreCase) && !isTourGuide.Equals("No", StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("IsTourGuide must be 'Yes' or 'No'.");
                return;
            }

            // Initialize nullable datetime variables
            DateTime? arrivalDateTime = null;
            DateTime? estimatedDateTime = null;

            if (isTransport.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(arrivalTimeTransport) || !DateTime.TryParse(arrivalTimeTransport, out DateTime parsedArrivalTime))
                {
                    MessageBox.Show("Arrival Time must be a valid datetime in the format 'YYYY-MM-DD HH:MM:SS' (e.g., 2024-01-02 08:00:00).");
                    return;
                }
                arrivalDateTime = parsedArrivalTime;

                if (string.IsNullOrEmpty(estimatedTimeTransport) || !DateTime.TryParse(estimatedTimeTransport, out DateTime parsedEstimatedTime))
                {
                    MessageBox.Show("Estimated Arrival Time must be a valid datetime in the format 'YYYY-MM-DD HH:MM:SS' (e.g., 2024-01-02 08:00:00).");
                    return;
                }
                estimatedDateTime = parsedEstimatedTime;
            }

            // Validate NoOfRooms if hotel is selected
            if (isHotel.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(noOfRooms) || !int.TryParse(noOfRooms, out _))
                {
                    MessageBox.Show("No. of Rooms must be a valid number when IsHotel is 'Yes'.");
                    return;
                }
            }
            else
            {
                noOfRooms = null;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            DECLARE @nextid INT; 
            SELECT @nextid = ISNULL(MAX(serviceID), 0) + 1 FROM Services; 
            INSERT INTO Services 
                (serviceID, StatusApproval, IsTransport, ArrivalTimeTransport, EstimatedArrivalTimeTransport, 
                 IsTourGuide, IsHotel, NoOfRooms, ServiceProviderID) 
            VALUES 
                (@nextid, @StatusApproval, @IsTransport, @ArrivalTimeTransport, @EstimatedArrivalTimeTransport, 
                 @IsTourGuide, @IsHotel, @NoOfRooms, @ServiceProviderID); 
            SELECT @nextid;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StatusApproval", "Pending");
                    cmd.Parameters.AddWithValue("@IsTransport", isTransport);
                    cmd.Parameters.AddWithValue("@IsTourGuide", isTourGuide);
                    cmd.Parameters.AddWithValue("@IsHotel", isHotel);
                    cmd.Parameters.AddWithValue("@ServiceProviderID", SessionData.id);

                    // ArrivalTimeTransport
                    cmd.Parameters.Add("@ArrivalTimeTransport", SqlDbType.DateTime).Value =
                        arrivalDateTime.HasValue ? (object)arrivalDateTime.Value : DBNull.Value;

                    // EstimatedArrivalTimeTransport
                    cmd.Parameters.Add("@EstimatedArrivalTimeTransport", SqlDbType.DateTime).Value =
                        estimatedDateTime.HasValue ? (object)estimatedDateTime.Value : DBNull.Value;

                    // NoOfRooms
                    if (!string.IsNullOrEmpty(noOfRooms))
                    {
                        cmd.Parameters.Add("@NoOfRooms", SqlDbType.Int).Value = int.Parse(noOfRooms);
                    }
                    else
                    {
                        cmd.Parameters.Add("@NoOfRooms", SqlDbType.Int).Value = DBNull.Value;
                    }

                    // Execute and retrieve inserted ID
                    int newServiceId = Convert.ToInt32(cmd.ExecuteScalar());

                    LoadServiceData();
                    MessageBox.Show($"Service {newServiceId} has been added successfully.");
                }
            }
        }

        private void LoadServiceData()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ServiceID, StatusApproval, IsTransport, ArrivalTimeTransport, EstimatedArrivalTimeTransport, IsTourGuide, IsHotel, NoOfRooms, ServiceProviderID FROM Services where serviceproviderid=@serviceproviderid";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@serviceproviderid", SessionData.id);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Handle NULL values for varchar columns
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["IsTransport"] == DBNull.Value)
                            row["IsTransport"] = "No";
                        if (row["IsTourGuide"] == DBNull.Value)
                            row["IsTourGuide"] = "No";
                        if (row["IsHotel"] == DBNull.Value)
                            row["IsHotel"] = "No";
                    }

                    dataGridView1.DataSource = dt;
                }
            }

        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int serviceId;
            if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["ServiceID"].Value?.ToString(), out serviceId))
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    UpdateServiceStatus(serviceId, "Delete");
                    MessageBox.Show($"Service {serviceId} has been deleted.");
                    LoadServiceData();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
                {
                    UpdateServiceStatus(serviceId, "Edit");
                    MessageBox.Show($"Service {serviceId} has been Edited.");
                    LoadServiceData();
                }
            }
            else
            {
                MessageBox.Show("Invalid Service ID.");
            }
        }

        private void UpdateServiceStatus(int serviceId, string status)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (status == "Delete")
                {
                    conn.Open();
                    string query = "delete from services WHERE ServiceID = @ServiceId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ServiceId", serviceId);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (status == "Edit")
                {
                    conn.Open();
                    string query = "delete from services WHERE ServiceID = @ServiceId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ServiceId", serviceId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }

        private void menu_Click(object sender, EventArgs e)
        {
            service_integration f3 = new service_integration();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            service_listing f3 = new service_listing();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bookingManagement f3 = new bookingManagement();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            service_listing2 f3 = new service_listing2();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void Home_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dashboard_provider f3 = new Dashboard_provider();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void service_listing_Load(object sender, EventArgs e)
        {

        }
    }
}
