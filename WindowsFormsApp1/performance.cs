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
    public partial class performance : Form
    {
        public performance()
        {
            InitializeComponent();
            LoadReviewTiles();
            LoadRevenueChart();

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

        private void button2_Click(object sender, EventArgs e)
        {
            performance f3 = new performance();
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


        private void button5_Click(object sender, EventArgs e)
        {
            Dashboard_provider f3 = new Dashboard_provider();
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

        private void performance_Load(object sender, EventArgs e)
        {

        }

        private void LoadReviewTiles()
        {
            string connStr = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            string query = @"
        SELECT r.ReviewID, r.TravellerID, r.Rating, r.Comment 
        FROM Review r
        JOIN ReviewGivenTo rgt ON r.ReviewID = rgt.ReviewID";
            // WHERE rgt.ServiceProviderID = @ServiceProviderID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // cmd.Parameters.AddWithValue("@ServiceProviderID", SessionData.id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    flowLayoutPanelReviews.Controls.Clear();

                    while (reader.Read())
                    {
                        ReviewTileHadi tile = new ReviewTileHadi(); // Your custom UserControl

                        tile.ReviewID = Convert.ToInt32(reader["ReviewID"]);
                        tile.TravellerID = Convert.ToInt32(reader["TravellerID"]);
                        tile.Rating = Convert.ToInt32(reader["Rating"]); // Rating is DECIMAL(3,2) in schema
                        tile.Comment = reader["Comment"].ToString();

                        flowLayoutPanelReviews.Controls.Add(tile);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading reviews: {ex.Message} (Error Code: {ex.Number})",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reviews: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chartRevenue_Click(object sender, EventArgs e)
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

            chartRevenue.Series.Clear();
            Series revenueSeries = new Series("Monthly Revenue");
            revenueSeries.ChartType = SeriesChartType.Column;  // Or use Line, Area, etc.
            revenueSeries.Color = Color.Green;
            chartRevenue.Series.Add(revenueSeries);

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

            chartRevenue.ChartAreas[0].AxisX.Title = "Month";
            chartRevenue.ChartAreas[0].AxisY.Title = "Revenue (PKR)";
            chartRevenue.Titles.Clear();
            chartRevenue.Titles.Add("Monthly Revenue");
        }
    }
}