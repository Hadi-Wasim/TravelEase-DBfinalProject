using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1.forms
{
    public partial class analytics : Form
    {
                string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

        public analytics()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Reviews f3 = new Reviews();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tour_management f3 = new tour_management();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            analytics f3 = new analytics();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            registration f3 = new registration();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void analytics_Load(object sender, EventArgs e)
        {
            LoadAnalyticsData();
        }

        private void LoadAnalyticsData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // User Traffic: Total bookings per month (line chart)
                    string userTrafficQuery = @"
                        SELECT DATEPART(YEAR, BookingDate) AS Year, DATEPART(MONTH, BookingDate) AS Month, COUNT(*) AS BookingCount
                        FROM Booking
                        GROUP BY DATEPART(YEAR, BookingDate), DATEPART(MONTH, BookingDate)
                        ORDER BY Year, Month";
                    using (SqlCommand cmd = new SqlCommand(userTrafficQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            chartUserTraffic.Series.Clear();
                            chartUserTraffic.ChartAreas[0].AxisX.Title = "Month";
                            chartUserTraffic.ChartAreas[0].AxisY.Title = "Number of Bookings";
                            Series series = new Series("User Traffic")
                            {
                                ChartType = SeriesChartType.Line,
                                BorderWidth = 2
                            };
                            while (reader.Read())
                            {
                                int year = reader.GetInt32(0);
                                int month = reader.GetInt32(1);
                                int count = reader.GetInt32(2);
                                string monthYear = $"{new DateTime(year, month, 1):yyyy-MM}";
                                series.Points.AddXY(monthYear, count);
                            }
                            chartUserTraffic.Series.Add(series);
                        }
                    }

                    // Booking Trends: Bookings per destination (column chart)
                    string bookingTrendsQuery = @"
                        SELECT t.Destination, COUNT(*) AS BookingCount
                        FROM Booking b
                        JOIN Trip t ON b.TripID = t.TripID
                        GROUP BY t.Destination
                        ORDER BY BookingCount DESC";
                    using (SqlCommand cmd = new SqlCommand(bookingTrendsQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            chartBookings.Series.Clear();
                            chartBookings.ChartAreas[0].AxisX.Title = "Destination";
                            chartBookings.ChartAreas[0].AxisY.Title = "Number of Bookings";
                            Series series = new Series("Booking Trends")
                            {
                                ChartType = SeriesChartType.Column
                            };
                            while (reader.Read())
                            {
                                string destination = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0);
                                int count = reader.GetInt32(1);
                                series.Points.AddXY(destination, count);
                            }
                            chartBookings.Series.Add(series);
                        }
                    }

                    // Revenue: Total revenue per month (column chart)
                    string revenueQuery = @"
                        SELECT DATEPART(YEAR, BookingDate) AS Year, DATEPART(MONTH, BookingDate) AS Month, SUM(Cost) AS TotalRevenue
                        FROM Booking
                        GROUP BY DATEPART(YEAR, BookingDate), DATEPART(MONTH, BookingDate)
                        ORDER BY Year, Month";
                    using (SqlCommand cmd = new SqlCommand(revenueQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            chartRevenue.Series.Clear();
                            chartRevenue.ChartAreas[0].AxisX.Title = "Month";
                            chartRevenue.ChartAreas[0].AxisY.Title = "Revenue";
                            Series series = new Series("Revenue")
                            {
                                ChartType = SeriesChartType.Column
                            };
                            while (reader.Read())
                            {
                                int year = reader.GetInt32(0);
                                int month = reader.GetInt32(1);
                                decimal revenue = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
                                string monthYear = $"{new DateTime(year, month, 1):yyyy-MM}";
                                series.Points.AddXY(monthYear, revenue);
                            }
                            chartRevenue.Series.Add(series);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading analytics data: {ex.Message} (Error Code: {ex.Number})", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading analytics data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}