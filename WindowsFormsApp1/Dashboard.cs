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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp1
{
    public partial class Dashboard : Form
    {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

        public Dashboard()
        {
            InitializeComponent();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;

            travellerName.Text = "WELCOME " + SessionData.Username;
            upcomingtripsShow();

        }
        private void upcomingtripsShow()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                StringBuilder query = new StringBuilder("select trip.Title,trip.Destination,trip.StartDate,trip.EndDate,Booking.Status,Booking.BookingID,Booking.BookingDate,Booking.Cost\r\nfrom Booking join Trip on Booking.TripId=trip.TripID group by Booking.TravellerID,trip.Title,trip.Destination,trip.StartDate,trip.EndDate,Booking.Status,Booking.BookingID,Booking.BookingDate,Booking.Cost\r\nhaving Booking.TravellerID=@id and trip.startdate>CAST(GETDATE() AS DATE)");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@id", SessionData.id);
                // Use parameters to prevent SQL Injection

                cmd.CommandText = query.ToString();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                upcomingtripdatagrid.DataSource = dt;
                flowLayoutPanel1.Controls.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    UpcomingTripTile tile = new UpcomingTripTile
                    {
                        TripTitle = row["Title"].ToString(),
                        TripBookingStatus = row["Status"].ToString(),
                        Destination = row["Destination"].ToString(),
                        PaymentStatus = row["Status"].ToString(),
                        DateofBooking = row["BookingDate"].ToString(),
                        StartDate = row["startdate"].ToString(),
                        EndDate = row["EndDate"].ToString(),
                        TripPrice = row["cost"].ToString(),
                        BookingId = row["BookingId"].ToString(),

                    };

                    tile.cancelClicked += (s, e2) => cancelTrip(row["bookingid"].ToString());
                    tile.cancelClicked += (s, e2) => refundTrip(row["bookingid"].ToString(), row["cost"].ToString());
                    flowLayoutPanel1.Controls.Add(tile);
                }
            }
        }

        private void refundTrip(string bookingid, string cost)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                DECLARE @nextId INT;

                SELECT @nextId = ISNULL(MAX(RefundID), 10000) + 1 FROM Refund;

                INSERT INTO Refund (
                    RefundID, Method, Amount, Reason, Date,TravellerID, BookingID
                )
                VALUES (
                    @nextId, 'Paypal', @cost, 'Trip Cancelled', CAST(GETDATE() AS TIME), @travellerId, @bookingid
                );";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bookingid", bookingid);
                cmd.Parameters.AddWithValue("@cost", cost);
                cmd.Parameters.AddWithValue("@travellerid", SessionData.id);
                cmd.Parameters.AddWithValue("@bookingid", bookingid);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Booking " + bookingid + " refunded");
                upcomingtripsShow();
            }
        }
        private void cancelTrip(string bookingid)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"update booking set status='Cancelled' where bookingid=@bookingid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bookingid", bookingid);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Booking " + bookingid + " cancelled");
                upcomingtripsShow();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            digitalTravel f3 = new digitalTravel();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button3_Click(object sender, EventArgs e)
        {
            review f3 = new review();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            preferences f3 = new preferences();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void menu_Click(object sender, EventArgs e)
        {
            TripSearch f3 = new TripSearch();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
