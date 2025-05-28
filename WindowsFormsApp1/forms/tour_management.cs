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

namespace WindowsFormsApp1.forms
{
    public partial class tour_management : Form
    {
        public tour_management()
        {
            InitializeComponent();
            LoadTripsIntoFlowLayoutPanel();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tour_management f3 = new tour_management();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button8_Click(object sender, EventArgs e)
        {
            analytics f3 = new analytics();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Reviews f3 = new Reviews();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            registration f3 = new registration();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void label4_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadTripsIntoFlowLayoutPanel()
        {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            string query = @"
        SELECT 
            Trip.TripID,
            Trip.Title,
            Trip.Description,
            Trip.Destination,
            Trip.Type,
            Trip.Capacity,
            Trip.StartDate,
            Trip.EndDate,
            Trip.Duration,
            Trip.Price,
            TourOperator.CompanyName AS OperatorName
        FROM Trip
        JOIN TourOperator ON Trip.TourOperatorID = TourOperator.OperatorID
        ORDER BY Trip.StartDate";

            flowLayoutPanel1.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TripTileDetailed tile = new TripTileDetailed();

                    tile.TripID = reader["TripID"].ToString();
                    tile.Title = reader["Title"].ToString();
                    tile.Description = reader["Description"].ToString();
                    tile.Destination = reader["Destination"].ToString();
                    tile.Type = reader["Type"].ToString();
                    tile.Capacity = reader["Capacity"].ToString();

                    tile.StartDate = Convert.ToDateTime(reader["StartDate"]).ToString("yyyy-MM-dd");
                    tile.EndDate = Convert.ToDateTime(reader["EndDate"]).ToString("yyyy-MM-dd");
                    tile.Duration = reader["Duration"].ToString();
                    tile.Price = reader["Price"].ToString();
                    tile.TourOperatorName = reader["OperatorName"].ToString();

                    flowLayoutPanel1.Controls.Add(tile);
                }

                reader.Close();
            }
        }

        private void tour_management_Load(object sender, EventArgs e)
        {

        }
    }
}
