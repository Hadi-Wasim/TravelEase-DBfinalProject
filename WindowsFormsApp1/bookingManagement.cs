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
    public partial class bookingManagement : Form
    {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

        public bookingManagement()
        {
            InitializeComponent();

            // Ensure DataGridViews are configured
            ConfigureDataGridView1();
            ConfigureDataGridView2();

            // Load data into both DataGridViews
            LoadDataGridView1();
            LoadDataGridView2();
        }

        private void ConfigureDataGridView1()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.CellClick += DataGridView1_CellClick;

            if (!dataGridView1.Columns.Contains("Confirm"))
            {
                DataGridViewButtonColumn btnConfirmColumn = new DataGridViewButtonColumn
                {
                    Name = "Confirm",
                    HeaderText = "Action",
                    Text = "Confirm",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(btnConfirmColumn);
            }
            if (!dataGridView1.Columns.Contains("Paid"))
            {
                DataGridViewButtonColumn btnPaidColumn = new DataGridViewButtonColumn
                {
                    Name = "Paid",
                    HeaderText = "Action",
                    Text = "Paid",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(btnPaidColumn);
            }
            if (!dataGridView1.Columns.Contains("UpdateRooms"))
            {
                DataGridViewButtonColumn btnUpdateRoomsColumn = new DataGridViewButtonColumn
                {
                    Name = "UpdateRooms",
                    HeaderText = "Action",
                    Text = "Update Rooms",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(btnUpdateRoomsColumn);
            }
        }

        private void ConfigureDataGridView2()
        {
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ReadOnly = true;
        }

        private void LoadDataGridView1()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                   select booking.BookingID, booking.NumberOfPeople, booking.Status, booking.BookingDate, Services.NoOfRooms, services.ServiceProviderID 
                   from Booking 
                   join ProvideServices on ProvideServices.TourOperatorID = Booking.TourOperatorID
                   and Booking.TravellerID = ProvideServices.TravellerID 
                   join Services on Services.ServiceID = ProvideServices.ServiceID
                   ";//WHERE services.ServiceProviderID = @ServiceProviderID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                   // cmd.Parameters.AddWithValue("@ServiceProviderID", SessionData.id);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Handle NULL values
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["NoOfRooms"] == DBNull.Value)
                            row["NoOfRooms"] = "";
                    }

                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void LoadDataGridView2()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    select payment.PaymentID, payment.date, payment.TransactionID, Booking.BookingID, booking.NumberOfPeople, Services.NoOfRooms, services.ServiceProviderID 
                    from Booking 
                    join ProvideServices on ProvideServices.TourOperatorID = Booking.TourOperatorID
                    and Booking.TravellerID = ProvideServices.TravellerID 
                    join Services on Services.ServiceID = ProvideServices.ServiceID 
                    join Payment on Payment.BookingID = Booking.BookingID
                    ";//WHERE services.ServiceProviderID = @ServiceProviderID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                   // cmd.Parameters.AddWithValue("@ServiceProviderID", SessionData.id);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Handle NULL values
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["NoOfRooms"] == DBNull.Value)
                            row["NoOfRooms"] = "";
                    }

                    dataGridView2.DataSource = dt;
                }
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int bookingId;
            if (!int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["BookingID"].Value?.ToString(), out bookingId))
            {
                MessageBox.Show("Invalid Booking ID.");
                return;
            }

            string status = dataGridView1.Rows[e.RowIndex].Cells["Status"].Value?.ToString();

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Confirm")
            {
                UpdateBookingStatus(bookingId, "Confirmed");
                MessageBox.Show($"Booking {bookingId} has been confirmed.");
                LoadDataGridView1();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Paid")
            {
                UpdateBookingStatus(bookingId, "Paid");
                MessageBox.Show($"Booking {bookingId} has been paid.");
                LoadDataGridView1();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "UpdateRooms")
            {
                // Check if the booking is confirmed
                if (status == "Confirmed" || status == "Paid")
                {
                    MessageBox.Show($"Cannot update rooms for Booking {bookingId} as it is already confirmed.");
                    return;
                }

                int numberOfPeople;
                if (!int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["NumberOfPeople"].Value?.ToString(), out numberOfPeople))
                {
                    MessageBox.Show("Invalid Number of People.");
                    return;
                }

                object noOfRoomsValue = dataGridView1.Rows[e.RowIndex].Cells["NoOfRooms"].Value;
                int noOfRooms;
                if (noOfRoomsValue == null || noOfRoomsValue == DBNull.Value || !int.TryParse(noOfRoomsValue.ToString(), out noOfRooms))
                {
                    MessageBox.Show("No. of Rooms is invalid or not set.");
                    return;
                }

                int updatedRooms = noOfRooms - numberOfPeople;
                if (updatedRooms < 0)
                {
                    MessageBox.Show("Insufficient number of rooms.");
                    UpdateBookingStatus(bookingId, "Rejected");
                    MessageBox.Show($"Booking {bookingId} has been rejected.");
                    return;
                }

                UpdateNoOfRooms(bookingId, updatedRooms);
                MessageBox.Show($"No. of Rooms updated to {updatedRooms} for Booking {bookingId}.");
                LoadDataGridView1();
            }
        }

        private void UpdateBookingStatus(int bookingId, string status)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Booking SET Status = @Status WHERE BookingID = @BookingID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@BookingID", bookingId);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        private void UpdateNoOfRooms(int bookingId, int updatedRooms)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE Services 
                    SET NoOfRooms = @UpdatedRooms 
                    FROM Services s
                    JOIN ProvideServices ps ON ps.ServiceID = s.ServiceID
                    JOIN Booking b ON b.TourOperatorID = ps.TourOperatorID AND b.TravellerID = ps.TravellerID
                    WHERE b.BookingID = @BookingID ";//AND s.ServiceProviderID = @ServiceProviderID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UpdatedRooms", updatedRooms);
                    cmd.Parameters.AddWithValue("@BookingID", bookingId);
                 //   cmd.Parameters.AddWithValue("@ServiceProviderID", SessionData.id);
                    cmd.ExecuteNonQuery();
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

        private void reservation_Load(object sender, EventArgs e)
        {
        }
    }
}