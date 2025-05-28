using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.forms;

namespace WindowsFormsApp1
{
    public partial class service_integration : Form
    {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

        public service_integration()
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
            DataGridViewButtonColumn btnApproveColumn = new DataGridViewButtonColumn();
            btnApproveColumn.Name = "Approve";
            btnApproveColumn.HeaderText = "Action";
            btnApproveColumn.Text = "Approve";
            btnApproveColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnApproveColumn);

            DataGridViewButtonColumn btnRejectColumn = new DataGridViewButtonColumn();
            btnRejectColumn.Name = "Reject";
            btnRejectColumn.HeaderText = "Action";
            btnRejectColumn.Text = "Reject";
            btnRejectColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnRejectColumn);

            dataGridView1.CellClick += DataGridView1_CellClick;


            LoadServiceData();
        }

        private void LoadServiceData()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ServiceID, StatusApproval, IsTransport, ArrivalTimeTransport, EstimatedArrivalTimeTransport, IsTourGuide, IsHotel, NoOfRooms, ServiceProviderID FROM Services WHERE StatusApproval = 'Pending' OR StatusApproval IS NULL";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
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
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Approve")
                {
                    UpdateServiceStatus(serviceId, "Approved");
                    MessageBox.Show($"Service {serviceId} has been approved.");
                    LoadServiceData();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Reject")
                {
                    UpdateServiceStatus(serviceId, "Rejected");
                    MessageBox.Show($"Service {serviceId} has been rejected.");
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
                conn.Open();
                string query = "UPDATE Services SET StatusApproval = @Status WHERE ServiceID = @ServiceId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@ServiceId", serviceId);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        private void service_integration_Load(object sender, EventArgs e)
        {
            // Optional: Add initialization logic here if needed
        }

        private void menu_Click(object sender, EventArgs e)
        {
            service_integration f3 = new service_integration();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            service_listing f3 = new service_listing();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
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

        private void Home_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dashboard_provider f3 = new Dashboard_provider();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }
    }
}