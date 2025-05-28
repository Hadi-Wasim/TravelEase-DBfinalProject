using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.forms;

namespace WindowsFormsApp1
{
    public partial class preferences : Form
    {
        public preferences()
        {
            InitializeComponent();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            loadTravelHistory();
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

        private void button5_Click(object sender, EventArgs e)
        {

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
        private void loadTravelHistory()
        {
            string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                StringBuilder query = new StringBuilder(@"select trip.Title,trip.Destination,trip.StartDate,trip.EndDate,Booking.Status,Booking.BookingID,Booking.BookingDate,Booking.Cost
                from Booking join Trip on Booking.TripId=trip.TripID group by Booking.TravellerID,trip.Title,trip.Destination,trip.StartDate,trip.EndDate,Booking.Status,Booking.BookingID,Booking.BookingDate,Booking.Cost
                having Booking.TravellerID=3001 and trip.startdate<CAST(GETDATE() AS DATE)");

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = query.ToString();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
                flowLayoutPanel1.Controls.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    UpcomingTripTile tile = new UpcomingTripTile(true)
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


                    flowLayoutPanel1.Controls.Add(tile);
                }
            }
        }
        private void preferences_Load(object sender, EventArgs e)
        {

        }
    }
}
