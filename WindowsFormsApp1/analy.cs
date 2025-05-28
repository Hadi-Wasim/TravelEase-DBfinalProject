using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class analy : Form
    {
        public analy()
        {
            InitializeComponent();
            LoadBookingChart();
            LoadRevenueChart();
            LoadReviewTiles();
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

        private void button10_Click(object sender, EventArgs e)
        {
            OperatorHome f3 = new OperatorHome();
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

        private void button2_Click(object sender, EventArgs e)
        {
            manageBookings f3 = new manageBookings();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void analytics_Load(object sender, EventArgs e)
        {

        }

        private void LoadBookingChart()
        {
            string connStr = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            string query = @"SELECT MONTH(BookingDate) AS Month, COUNT(*) AS BookingCount
                     FROM Booking
                     GROUP BY MONTH(BookingDate)
                     ORDER BY Month";

            chartBookings.Series.Clear();
            Series series = new Series("Monthly Bookings");
            series.ChartType = SeriesChartType.Column;
            chartBookings.Series.Add(series);

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int month = reader.GetInt32(0);
                    int count = reader.GetInt32(1);

                    string monthName = new DateTime(2024, month, 1).ToString("MMM");
                    series.Points.AddXY(monthName, count);
                }
            }

            chartBookings.ChartAreas[0].AxisX.Title = "Month";
            chartBookings.ChartAreas[0].AxisY.Title = "Bookings";
            chartBookings.Titles.Clear();
            chartBookings.Titles.Add("Monthly Booking Statistics");
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }


        private void LoadRevenueChart()
        {
            string connStr = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            string query = @"SELECT 
                        MONTH(B.BookingDate) AS Month,
                        SUM(B.Cost) AS TotalRevenue
                    FROM Booking B
                    GROUP BY MONTH(B.BookingDate)
                    ORDER BY Month;";

            revenueChart.Series.Clear();
            Series revenueSeries = new Series("Monthly Revenue");
            revenueSeries.ChartType = SeriesChartType.Column;  // Or use Line, Area, etc.
            revenueSeries.Color = Color.Green;
            revenueChart.Series.Add(revenueSeries);

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int month = reader.GetInt32(0);
                    decimal revenue = reader.GetDecimal(1);
                    string monthName = new DateTime(2024, month, 1).ToString("MMM");

                    revenueSeries.Points.AddXY(monthName, revenue);
                }
            }

            revenueChart.ChartAreas[0].AxisX.Title = "Month";
            revenueChart.ChartAreas[0].AxisY.Title = "Revenue (PKR)";
            revenueChart.Titles.Clear();
            revenueChart.Titles.Add("Monthly Revenue");
        }

        private void LoadReviewTiles()
        {
            string connStr = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            string query = "SELECT ReviewID, TravellerID, Rating, Comment FROM Review";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                flowLayoutPanelReviews.Controls.Clear();

                while (reader.Read())
                {
                    ReviewTileHadi tile = new ReviewTileHadi(); // Your custom UserControl

                    tile.ReviewID = Convert.ToInt32(reader["ReviewID"]);
                    tile.TravellerID = Convert.ToInt32(reader["TravellerID"]);
                    tile.Rating = Convert.ToInt32(reader["Rating"]);
                    tile.Comment = reader["Comment"].ToString();

                    flowLayoutPanelReviews.Controls.Add(tile);
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void revenueChart_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
