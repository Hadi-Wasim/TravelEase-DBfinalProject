using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.forms;

namespace WindowsFormsApp1
{
    public partial class registration : Form
    {
                string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

        public registration()
        {
            InitializeComponent();
        }

        private void registration_Load(object sender, EventArgs e)
        {
            // Add button columns to OperatorDGV
            DataGridViewButtonColumn approveColTO = new DataGridViewButtonColumn();
            approveColTO.Name = "ApproveTO";
            approveColTO.HeaderText = "Approve";
            approveColTO.Text = "Approve";
            approveColTO.UseColumnTextForButtonValue = true;
            OperatorDGV.Columns.Add(approveColTO);

            DataGridViewButtonColumn deleteColTO = new DataGridViewButtonColumn();
            deleteColTO.Name = "DeleteTO";
            deleteColTO.HeaderText = "Delete";
            deleteColTO.Text = "Delete";
            deleteColTO.UseColumnTextForButtonValue = true;
            OperatorDGV.Columns.Add(deleteColTO);

            // Add button columns to TravellerDGV
            DataGridViewButtonColumn approveColTraveller = new DataGridViewButtonColumn();
            approveColTraveller.Name = "ApproveTraveller";
            approveColTraveller.HeaderText = "Approve";
            approveColTraveller.Text = "Approve";
            approveColTraveller.UseColumnTextForButtonValue = true;
            TravellerDGV.Columns.Add(approveColTraveller);

            DataGridViewButtonColumn deleteColTraveller = new DataGridViewButtonColumn();
            deleteColTraveller.Name = "DeleteTraveller";
            deleteColTraveller.HeaderText = "Delete";
            deleteColTraveller.Text = "Delete";
            deleteColTraveller.UseColumnTextForButtonValue = true;
            TravellerDGV.Columns.Add(deleteColTraveller);

            // Add button columns to ServiceProviderDGV
            DataGridViewButtonColumn approveColSP = new DataGridViewButtonColumn();
            approveColSP.Name = "ApproveSP";
            approveColSP.HeaderText = "Approve";
            approveColSP.Text = "Approve";
            approveColSP.UseColumnTextForButtonValue = true;
            ServiceProviderDGV.Columns.Add(approveColSP);

            DataGridViewButtonColumn deleteColSP = new DataGridViewButtonColumn();
            deleteColSP.Name = "DeleteSP";
            deleteColSP.HeaderText = "Delete";
            deleteColSP.Text = "Delete";
            deleteColSP.UseColumnTextForButtonValue = true;
            ServiceProviderDGV.Columns.Add(deleteColSP);

            LoadTourOperatorData();
            LoadTravellerData();
            LoadServiceProviderData();
            cmbFilterTO.SelectedIndex = 0; // Default to "Not Approved"
            cmbFilterTraveller.SelectedIndex = 0;
            cmbFilterSP.SelectedIndex = 0;
        }

        private void LoadTourOperatorData()
        {
            string query = @"
                SELECT OperatorID AS ID, CompanyName AS Name, Email, RegistrationStatus AS Status, 
                       DateOfJoin AS Date, Contact
                FROM TourOperator";
            string filter = cmbFilterTO.SelectedItem?.ToString();
            if (filter == "Not Approved")
                query += " WHERE DateOfJoin IS NULL";
            else if (filter == "Accepted")
                query += " WHERE DateOfJoin IS NOT NULL";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        OperatorDGV.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading TourOperator data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTravellerData()
        {
            string query = @"
                SELECT TravellerID AS ID, Name, Email, 'N/A' AS Status, 
                       DateOfRegistration AS Date, Contact
                FROM Traveller";
            string filter = cmbFilterTraveller.SelectedItem?.ToString();
            if (filter == "Not Approved")
                query += " WHERE DateOfRegistration IS NULL";
            else if (filter == "Accepted")
                query += " WHERE DateOfRegistration IS NOT NULL";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        TravellerDGV.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Traveller data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadServiceProviderData()
        {
            string query = @"
                SELECT ServiceProviderID AS ID, Name, Email, Availability AS Status, 
                       NULL AS Date, ContactInfo AS Contact
                FROM ServiceProvider";
            string filter = cmbFilterSP.SelectedItem?.ToString();
            if (filter == "Not Approved")
                query += " WHERE Availability != 'Available'";
            else if (filter == "Accepted")
                query += " WHERE Availability = 'Available'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ServiceProviderDGV.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ServiceProvider data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbFilterTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTourOperatorData();
        }

        private void cmbFilterTraveller_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTravellerData();
        }

        private void cmbFilterSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadServiceProviderData();
        }

        private void OperatorDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewColumn col = OperatorDGV.Columns[e.ColumnIndex];
            int id = Convert.ToInt32(OperatorDGV.Rows[e.RowIndex].Cells["ID"].Value);

            if (col.Name == "ApproveTO")
            {
                if (OperatorDGV.Rows[e.RowIndex].Cells["Date"].Value != DBNull.Value)
                {
                    MessageBox.Show("This TourOperator registration is already approved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE TourOperator SET DateOfJoin = @Date, RegistrationStatus = 'Approved' WHERE OperatorID = @ID AND DateOfJoin IS NULL";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                                MessageBox.Show("Cannot approve an already approved TourOperator registration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                                MessageBox.Show("TourOperator registration approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    LoadTourOperatorData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error approving TourOperator registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (col.Name == "DeleteTO")
            {
                if (MessageBox.Show("Are you sure you want to delete this TourOperator registration?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        // Check for related records in ProvideServices
                        string checkQuery = "SELECT COUNT(*) FROM dbo.ProvideServices WHERE TourOperatorID = @ID";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@ID", id);
                            int relatedCount = (int)checkCmd.ExecuteScalar();
                            if (relatedCount > 0)
                            {
                                DialogResult result = MessageBox.Show("This TourOperator has related services. Do you want to delete them too? (Yes to delete all, No to cancel)", "Related Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.Yes)
                                {
                                    // Delete related services first
                                    string deleteServicesQuery = "DELETE FROM dbo.ProvideServices WHERE TourOperatorID = @ID";
                                    using (SqlCommand deleteCmd = new SqlCommand(deleteServicesQuery, conn))
                                    {
                                        deleteCmd.Parameters.AddWithValue("@ID", id);
                                        deleteCmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    return; // Cancel the deletion
                                }
                            }
                        }
                        // Delete the TourOperator
                        string query = "DELETE FROM TourOperator WHERE OperatorID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("TourOperator registration deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTourOperatorData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting TourOperator registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TravellerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewColumn col = TravellerDGV.Columns[e.ColumnIndex];
            int id = Convert.ToInt32(TravellerDGV.Rows[e.RowIndex].Cells["ID"].Value);

            if (col.Name == "ApproveTraveller")
            {
                if (TravellerDGV.Rows[e.RowIndex].Cells["Date"].Value != DBNull.Value)
                {
                    MessageBox.Show("This Traveller registration is already approved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE Traveller SET DateOfRegistration = @Date WHERE TravellerID = @ID AND DateOfRegistration IS NULL";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                                MessageBox.Show("Cannot approve an already approved Traveller registration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                                MessageBox.Show("Traveller registration approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    LoadTravellerData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error approving Traveller registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (col.Name == "DeleteTraveller")
            {
                if (MessageBox.Show("Are you sure you want to delete this Traveller registration?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Traveller WHERE TravellerID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Traveller registration deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTravellerData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting Traveller registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ServiceProviderDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewColumn col = ServiceProviderDGV.Columns[e.ColumnIndex];
            int id = Convert.ToInt32(ServiceProviderDGV.Rows[e.RowIndex].Cells["ID"].Value);

            if (col.Name == "ApproveSP")
            {
                if (ServiceProviderDGV.Rows[e.RowIndex].Cells["Status"].Value?.ToString() == "Available")
                {
                    MessageBox.Show("This ServiceProvider registration is already approved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE ServiceProvider SET Availability = 'Available' WHERE ServiceProviderID = @ID AND Availability != 'Available'";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                                MessageBox.Show("Cannot approve an already approved ServiceProvider registration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                                MessageBox.Show("ServiceProvider registration approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    LoadServiceProviderData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error approving ServiceProvider registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (col.Name == "DeleteSP")
            {
                if (MessageBox.Show("Are you sure you want to delete this ServiceProvider registration?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM ServiceProvider WHERE ServiceProviderID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("ServiceProvider registration deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadServiceProviderData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting ServiceProvider registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
    }
}