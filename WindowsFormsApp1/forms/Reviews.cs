using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.forms;

namespace WindowsFormsApp1
{
    public partial class Reviews : Form
    {
                string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
        private int moderatorID; // Will be set dynamically
        private string searchTerm = ""; // Store the search term

        public Reviews()
        {
            InitializeComponent();
            moderatorID = 1002; // Form1.CurrentModeratorID > 0 ? Form1.CurrentModeratorID : 1; // Fallback to 1 if not set
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

        private void button11_Click(object sender, EventArgs e)
        {
            Reviews f3 = new Reviews();
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

        private void label4_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);
            f3.Show();
        }

        private void Reviews_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadReviewData();
            cmbFilterReview.SelectedIndex = 0; // Default to "Not Approved"
        }

        private void ConfigureDataGridView()
        {
            reviewDGV.Columns.Clear();
            reviewDGV.AutoGenerateColumns = false;

            reviewDGV.Columns.Add("ReviewID", "Review ID");
            reviewDGV.Columns.Add("Reviewed", "Reviewed Date");
            reviewDGV.Columns.Add("Rating", "Rating");
            reviewDGV.Columns.Add("Comment", "Comment");
            reviewDGV.Columns.Add("ModeratorID", "Moderator ID");
            reviewDGV.Columns.Add("TravellerID", "Traveller ID");

            reviewDGV.Columns["ReviewID"].DataPropertyName = "ReviewID";
            reviewDGV.Columns["Reviewed"].DataPropertyName = "Reviewed";
            reviewDGV.Columns["Rating"].DataPropertyName = "Rating";
            reviewDGV.Columns["Comment"].DataPropertyName = "Comment";
            reviewDGV.Columns["ModeratorID"].DataPropertyName = "ModeratorID";
            reviewDGV.Columns["TravellerID"].DataPropertyName = "TravellerID";

            DataGridViewButtonColumn approveCol = new DataGridViewButtonColumn();
            approveCol.Name = "Approve";
            approveCol.HeaderText = "Approve";
            approveCol.Text = "Approve";
            approveCol.UseColumnTextForButtonValue = true;
            reviewDGV.Columns.Add(approveCol);

            DataGridViewButtonColumn deleteCol = new DataGridViewButtonColumn();
            deleteCol.Name = "Delete";
            deleteCol.HeaderText = "Delete";
            deleteCol.Text = "Delete";
            deleteCol.UseColumnTextForButtonValue = true;
            reviewDGV.Columns.Add(deleteCol);
        }

        private void LoadReviewData()
        {
            string query = @"
                SELECT ReviewID, Reviewed, Rating, Comment, ModeratorID, TravellerID
                FROM Review";
            string filter = cmbFilterReview.SelectedItem?.ToString();
            string whereClause = "";

            if (filter == "Not Approved")
                whereClause = " WHERE ModeratorID IS NULL";
            else if (filter == "Approved")
                whereClause = " WHERE ModeratorID IS NOT NULL";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (string.IsNullOrEmpty(whereClause))
                    whereClause = " WHERE";
                else
                    whereClause += " AND";
                whereClause += " Comment LIKE @SearchTerm";
            }

            query += whereClause;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            da.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                        }
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        reviewDGV.DataSource = dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading reviews: {ex.Message} (Error Code: {ex.Number})", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading review data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbFilterReview_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReviewData();
        }

        private void reviewDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewColumn col = reviewDGV.Columns[e.ColumnIndex];
            object reviewIdValue = reviewDGV.Rows[e.RowIndex].Cells["ReviewID"].Value;

            // Validate ReviewID
            if (reviewIdValue == null || reviewIdValue == DBNull.Value)
            {
                MessageBox.Show("Invalid Review ID. Cannot delete this review.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int id;
            try
            {
                id = Convert.ToInt32(reviewIdValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving Review ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (col.Name == "Approve")
            {
                if (reviewDGV.Rows[e.RowIndex].Cells["ModeratorID"].Value != DBNull.Value)
                {
                    MessageBox.Show("This review is already approved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE Review SET ModeratorID = @ModeratorID WHERE ReviewID = @ID AND ModeratorID IS NULL";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                            cmd.Parameters.Add("@ModeratorID", SqlDbType.Int).Value = moderatorID;
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                                MessageBox.Show("Cannot approve an already approved review or review not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                                MessageBox.Show("Review approved successfully by Moderator ID " + moderatorID + ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    LoadReviewData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error approving review: {ex.Message} (Error Code: {ex.Number})", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error approving review: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (col.Name == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this review? This will also delete related entries in ReviewGivenTo.",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Delete related entries in ReviewGivenTo
                        string deleteRelatedQuery = "DELETE FROM ReviewGivenTo WHERE ReviewID = @ID";
                        using (SqlCommand deleteRelatedCmd = new SqlCommand(deleteRelatedQuery, conn))
                        {
                            deleteRelatedCmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                            deleteRelatedCmd.ExecuteNonQuery();
                        }

                        // Now delete the review
                        string query = "DELETE FROM Review WHERE ReviewID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                                MessageBox.Show("Review not found or already deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                                MessageBox.Show("Review and related entries deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    LoadReviewData();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) // Foreign key violation error code
                    {
                        MessageBox.Show("Cannot delete this review because it is referenced by another record. Please ensure all related records are deleted.",
                            "Foreign Key Violation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Database error deleting review: {ex.Message} (Error Code: {ex.Number})",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting review: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void searchButton_Click(object sender, EventArgs e)
        {
         //   searchTerm = searchTextBox.Text.Trim();
            LoadReviewData();
        }
    }
}