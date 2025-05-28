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
    public partial class review : Form
    {

        private List<KeyValuePair<int, string>> operatorList = new List<KeyValuePair<int, string>>();
        private List<KeyValuePair<int, string>> serviceProviderList = new List<KeyValuePair<int, string>>();
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

        public review()
        {
            InitializeComponent();



            // Add FlowLayoutPanel for review tiles

            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.WrapContents = true;
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            InitializeForm();
            LoadReviewTiles();
        }

        private void InitializeForm()
        {
            LoadOperatorData();
            SetOperatorAutocomplete();

            rbOperator.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
            rbTrip.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
            btnSearch.Click += new EventHandler(btnSearch_Click);
        }

        int currentOperator = 0;

        private void LoadOperatorData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT OperatorId, CompanyName FROM TourOperator";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int operatorId = reader.GetInt32(reader.GetOrdinal("OperatorId"));
                                currentOperator = operatorId; // Note: This only sets the last operatorId
                                string companyName = reader["CompanyName"].ToString();
                                operatorList.Add(new KeyValuePair<int, string>(operatorId, companyName));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading operators: " + ex.Message);
            }
        }

        private void LoadServiceProviderData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ServiceProviderID, Name FROM ServiceProvider";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int serviceProviderId = reader.GetInt32(reader.GetOrdinal("ServiceProviderID"));
                                string name = reader["Name"].ToString();
                                serviceProviderList.Add(new KeyValuePair<int, string>(serviceProviderId, name));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading service providers: " + ex.Message);
            }
        }

        private void SetOperatorAutocomplete()
        {
            List<string> operatorNames = operatorList.Select(kvp => kvp.Value).ToList();
            SetAutoComplete(searchbar, operatorNames);
        }

        private void SetServiceProviderAutocomplete()
        {
            if (serviceProviderList.Count == 0)
            {
                LoadServiceProviderData();
            }
            List<string> serviceProviderNames = serviceProviderList.Select(kvp => kvp.Value).ToList();
            SetAutoComplete(searchbar, serviceProviderNames);
        }

        private void SetAutoComplete(TextBox textBox, List<string> sourceList)
        {
            AutoCompleteStringCollection autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(sourceList.ToArray());

            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox.AutoCompleteCustomSource = autoSource;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            searchbar.Text = ""; // Clear the text box when switching
            if (rbOperator.Checked)
            {
                SetOperatorAutocomplete();
            }
            else if (rbTrip.Checked)
            {
                SetServiceProviderAutocomplete();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchbar.Text))
            {
                MessageBox.Show("Please enter an operator or service provider.");
                return;
            }


            string rating = nudRating.Text;
            string comment = tbReview.Text.Trim();

            if (string.IsNullOrEmpty(comment))
            {
                MessageBox.Show("Please enter a review.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                int newReviewId = GetNextReviewId(conn);

                string reviewInsert = @"
                DECLARE @nextId INT;
                SELECT @nextId = ISNULL(MAX(ReviewID), 0) + 1 FROM Review;
                INSERT INTO Review (ReviewID, rating, comment, ModeratorID, TravellerID)
                VALUES (@nextId, @Rating, @Comment, NULL, @travellerid)";
                using (SqlCommand cmd = new SqlCommand(reviewInsert, conn))
                {
                    cmd.Parameters.AddWithValue("@Rating", rating);
                    cmd.Parameters.AddWithValue("@Comment", comment);
                    cmd.Parameters.AddWithValue("@travellerid", SessionData.id);
                    cmd.ExecuteNonQuery();
                }

                if (rbOperator.Checked)
                {
                    string operatorQuery = "SELECT OperatorId FROM TourOperator WHERE CompanyName = @CompanyName";
                    int operatorId = -1;
                    using (SqlCommand cmd = new SqlCommand(operatorQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CompanyName", searchbar.Text);
                        object result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            MessageBox.Show("Invalid operator name. Please choose a valid operator.");
                            return;
                        }
                        operatorId = (int)result;
                    }

                    string operatorInsert = "INSERT INTO ReviewOperator (OperatorId, ReviewID) VALUES (@OperatorId, @ReviewID)";
                    using (SqlCommand cmd = new SqlCommand(operatorInsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@OperatorId", operatorId);
                        cmd.Parameters.AddWithValue("@ReviewID", newReviewId);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (rbTrip.Checked)
                {
                    string serviceProviderQuery = "SELECT ServiceProviderID FROM ServiceProvider WHERE Name = @Name";
                    int serviceProviderId = -1;
                    using (SqlCommand cmd = new SqlCommand(serviceProviderQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", searchbar.Text);
                        object result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            MessageBox.Show("Invalid service provider name. Please choose a valid service provider.");
                            return;
                        }
                        serviceProviderId = (int)result;
                    }
                    string serviceProviderInsert = "INSERT INTO ReviewGivenTo (ServiceProviderID, ReviewID) VALUES (@ServiceProviderID, @ReviewID)";
                    using (SqlCommand cmd = new SqlCommand(serviceProviderInsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@ServiceProviderID", serviceProviderId);
                        cmd.Parameters.AddWithValue("@ReviewID", newReviewId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Review submitted successfully!");
                ClearForm();
            }
        }

        private int GetNextReviewId(SqlConnection conn)
        {
            string query = "SELECT MAX(ReviewID) FROM Review";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                object result = cmd.ExecuteScalar();
                return result == DBNull.Value ? 7001 : (int)result + 1;
            }
        }

        private void ClearForm()
        {
            nudRating.Text = "1";
            tbReview.Text = "";
        }

        // Helper class for storing ID and Name pairs
        public class KeyValuePair<TKey, TValue>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            public KeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        private void LoadReviewTiles()
        {
            flowLayoutPanel.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ReviewID, Reviewed, rating, comment FROM Review";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ReviewTile tile = new ReviewTile
                            {
                                ReviewId = reader["ReviewID"].ToString(),
                                Reviewed = reader["Reviewed"].ToString(),
                                Rating = reader["rating"].ToString(),
                                Comment = reader["comment"].ToString()
                            };
                            flowLayoutPanel.Controls.Add(tile);
                        }
                    }
                }
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

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void flowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void review_Load(object sender, EventArgs e)
        {

        }
    }
}