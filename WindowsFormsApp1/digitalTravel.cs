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
    public partial class digitalTravel : Form
    {
        private string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

        public digitalTravel()
        {
            InitializeComponent();

            // Configure DataGridView and load data
            ConfigureDataGridView();
            LoadDataGridView();

            // Add three standalone buttons for subtracting tickets
            AddCustomButtons();
        }

        private void AddCustomButtons()
        {
            // Button to use E-Ticket
            Button btnUseETicket = new Button
            {
                Text = "Use E-Ticket",
                Location = new System.Drawing.Point(100, 320),
                Width = 100
            };
            btnUseETicket.Click += BtnUseETicket_Click;
            this.Controls.Add(btnUseETicket);

            // Button to use Voucher
            Button btnUseVoucher = new Button
            {
                Text = "Use Voucher",
                Location = new System.Drawing.Point(200, 320),
                Width = 100
            };
            btnUseVoucher.Click += BtnUseVoucher_Click;
            this.Controls.Add(btnUseVoucher);

            // Button to use Activity Pass
            Button btnUseActivityPass = new Button
            {
                Text = "Use Activity Pass",
                Location = new System.Drawing.Point(300, 320),
                Width = 120
            };
            btnUseActivityPass.Click += BtnUseActivityPass_Click;
            this.Controls.Add(btnUseActivityPass);
        }

        private void ConfigureDataGridView()
        {
            // Ensure DataGridView is configured
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void LoadDataGridView()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT  
                    s.ServiceID,
                    s.StatusApproval,
                    s.IsTransport,
                    s.ArrivalTimeTransport,
                    s.EstimatedArrivalTimeTransport,
                    s.IsTourGuide,
                    s.IsHotel,
                    s.NoOfRooms,
                    s.ServiceProviderID,
                    ps.TravellerID,
                    COALESCE(sap.ActivityPasses, 0) AS ActivityPasses,
                    COALESCE(seti.ETickets, 0) AS ETickets,
                    COALESCE(sv.Vouchers, 0) AS Vouchers
                FROM Services s
                LEFT JOIN ServiceActivityPasses sap ON sap.ServiceID = s.ServiceID
                LEFT JOIN ServiceETickets seti ON seti.ServiceID = s.ServiceID
                LEFT JOIN ServiceVouchers sv ON sv.ServiceID = s.ServiceID
                JOIN ProvideServices ps ON ps.ServiceID = s.ServiceID
                WHERE ps.TravellerID = @TravellerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TravellerID", SessionData.id);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Add formatted columns
                        dt.Columns.Add("ArrivalFormatted", typeof(string));
                        dt.Columns.Add("EstimatedArrivalFormatted", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            string arrivalFormatted = "";
                            string estimatedArrivalFormatted = "";

                            if (row["ArrivalTimeTransport"] != DBNull.Value)
                            {
                                if (DateTime.TryParse(row["ArrivalTimeTransport"].ToString(), out DateTime arrival))
                                    arrivalFormatted = arrival.ToString("yyyy-MM-dd HH:mm:ss");
                                else
                                    arrivalFormatted = "Invalid Date";
                            }

                            if (row["EstimatedArrivalTimeTransport"] != DBNull.Value)
                            {
                                if (DateTime.TryParse(row["EstimatedArrivalTimeTransport"].ToString(), out DateTime estArrival))
                                    estimatedArrivalFormatted = estArrival.ToString("yyyy-MM-dd HH:mm:ss");
                                else
                                    estimatedArrivalFormatted = "Invalid Date";
                            }

                            row["ArrivalFormatted"] = arrivalFormatted;
                            row["EstimatedArrivalFormatted"] = estimatedArrivalFormatted;
                        }

                        dataGridView1.DataSource = dt;

                        // Rename columns if needed
                        dataGridView1.Columns["ArrivalFormatted"].HeaderText = "Arrival Time";
                        dataGridView1.Columns["EstimatedArrivalFormatted"].HeaderText = "Estimated Arrival";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }


        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int serviceId;
            if (!int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["ServiceID"].Value?.ToString(), out serviceId))
            {
                MessageBox.Show("Invalid Service ID.");
                return;
            }

            int etickets = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ETickets"].Value ?? 0);
            int vouchers = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Vouchers"].Value ?? 0);
            int activityPasses = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ActivityPasses"].Value ?? 0);

            string serviceDetails = $"Service ID: {serviceId}, " +
                                   $"Status: {dataGridView1.Rows[e.RowIndex].Cells["StatusApproval"].Value?.ToString() ?? "N/A"}, " +
                                   $"Transport: {dataGridView1.Rows[e.RowIndex].Cells["IsTransport"].Value?.ToString() ?? "N/A"}, " +
                                   $"Arrival: {dataGridView1.Rows[e.RowIndex].Cells["ArrivalTimeTransport"].Value?.ToString() ?? "N/A"}, " +
                                   $"Est. Arrival: {dataGridView1.Rows[e.RowIndex].Cells["EstimatedArrivalTimeTransport"].Value?.ToString() ?? "N/A"}, " +
                                   $"Tour Guide: {dataGridView1.Rows[e.RowIndex].Cells["IsTourGuide"].Value?.ToString() ?? "N/A"}, " +
                                   $"Hotel: {dataGridView1.Rows[e.RowIndex].Cells["IsHotel"].Value?.ToString() ?? "N/A"}, " +
                                   $"Rooms: {dataGridView1.Rows[e.RowIndex].Cells["NoOfRooms"].Value?.ToString() ?? "N/A"}, " +
                                   $"Traveller ID: {dataGridView1.Rows[e.RowIndex].Cells["TravellerID"].Value?.ToString() ?? "N/A"}";
        }
        private string GetQuantityColumn(string tableName)
        {
            switch (tableName)
            {
                case "ServiceETickets":
                    return "ETickets";
                case "ServiceVouchers":
                    return "Vouchers";
                case "ServiceActivityPasses":
                    return "ActivityPasses";
                default:
                    throw new Exception("Unknown table name");
            }
        }

        private void UpdateQuantity(string tableName, string idColumn, int id, int newQuantity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string columnName = GetQuantityColumn(tableName);
                    string query = $"UPDATE {tableName} SET {columnName} = @NewQuantity WHERE {idColumn} = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating quantity in {tableName}: {ex.Message}");
            }
        }


        private void menu_Click(object sender, EventArgs e)
        {
            TripSearch f3 = new TripSearch();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            digitalTravel f3 = new digitalTravel();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            review f3 = new review();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            preferences f3 = new preferences();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void digitalTravel_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void BtnUseETicket_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to use an E-Ticket.");
                return;
            }

            int serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ServiceID"].Value);
            int etickets = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ETickets"].Value);

            if (etickets <= 0)
            {
                MessageBox.Show("No E-Tickets available.");
                return;
            }

            UpdateQuantity("ServiceETickets", "ServiceID", serviceId, etickets - 1);
            MessageBox.Show("E-Ticket used successfully!");
            LoadDataGridView();
        }

        private void BtnUseVoucher_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to use a Voucher.");
                return;
            }

            int serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ServiceID"].Value);
            int vouchers = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Vouchers"].Value);

            if (vouchers <= 0)
            {
                MessageBox.Show("No Vouchers available.");
                return;
            }

            UpdateQuantity("ServiceVouchers", "ServiceID", serviceId, vouchers - 1);
            MessageBox.Show("Voucher used successfully!");
            LoadDataGridView();
        }

        private void BtnUseActivityPass_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to use an Activity Pass.");
                return;
            }

            int serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ServiceID"].Value);
            int activityPasses = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ActivityPasses"].Value);

            if (activityPasses <= 0)
            {
                MessageBox.Show("No Activity Passes available.");
                return;
            }

            UpdateQuantity("ServiceActivityPasses", "ServiceID", serviceId, activityPasses - 1);
            MessageBox.Show("Activity Pass used successfully!");
            LoadDataGridView();
        }
    }
}