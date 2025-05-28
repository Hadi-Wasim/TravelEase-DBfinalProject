using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1.forms
{
    public partial class edittripform : Form
    {
        private int tripId;
                string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

        public edittripform(int tripId)
        {
            InitializeComponent();
            this.tripId = tripId;
            LoadTripDetails();
            
        }

        private void LoadTripDetails()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Trip WHERE TripID = @TripID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TripID", tripId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        { 
                            if (reader.Read())
                            {

                               txtTitle.Text = reader["Title"].ToString();
                                txtDescription.Text = reader["Description"].ToString();
                                txtDestination.Text = reader["Destination"].ToString();
                                cmbType.SelectedItem = reader["Type"].ToString();
                                numCapacity.Value = Convert.ToInt32(reader["Capacity"]);
                                dtpStartDate.Value = Convert.ToDateTime(reader["StartDate"]);
                                dtpEndDate.Value = Convert.ToDateTime(reader["EndDate"]);
                                numDuration.Value = Convert.ToInt32(reader["Duration"]);
                                numPrice.Value = Convert.ToDecimal(reader["Price"]);
                                numTourOperatorID.Value = Convert.ToInt32(reader["TourOperatorID"]);
                                numBookingID.Value = Convert.ToInt32(reader["BookingID"]);
                                
                            }
                            else
                            {
                                MessageBox.Show($"No trip found with TripID {tripId}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtTitle.Text.Length > 100)
            {
                MessageBox.Show("Title cannot exceed 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtDescription.Text.Length > 500)
            {
                MessageBox.Show("Description cannot exceed 500 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtDestination.Text.Length > 100)
            {
                MessageBox.Show("Destination cannot exceed 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a trip type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpEndDate.Value < dtpStartDate.Value)
            {
                MessageBox.Show("End Date cannot be earlier than Start Date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidateForeignKeys(decimal tourOperatorId, decimal bookingId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM TourOperator WHERE OperatorID = @OperatorID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OperatorID", tourOperatorId);
                        int operatorCount = (int)cmd.ExecuteScalar();

                        query = "SELECT COUNT(*) FROM Booking WHERE BookingID = @BookingID";
                        cmd.CommandText = query;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@BookingID", bookingId);
                        int bookingCount = (int)cmd.ExecuteScalar();

                        if (operatorCount == 0)
                        {
                            MessageBox.Show("Invalid Tour Operator ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        if (bookingCount == 0)
                        {
                            MessageBox.Show("Invalid Booking ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validating foreign keys: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs() || !ValidateForeignKeys(numTourOperatorID.Value, numBookingID.Value))
                    return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Trip SET 
                                        Title = @Title,
                                        Description = @Description,
                                        Destination = @Destination,
                                        Type = @Type,
                                        Capacity = @Capacity,
                                        StartDate = @StartDate,
                                        EndDate = @EndDate,
                                        Duration = @Duration,
                                        Price = @Price,
                                        TourOperatorID = @TourOperatorID,
                                        BookingID = @BookingID
                                    WHERE TripID = @TripID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@Destination", txtDestination.Text);
                        cmd.Parameters.AddWithValue("@Type", cmbType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Capacity", numCapacity.Value);
                        cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                        cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
                        cmd.Parameters.AddWithValue("@Duration", numDuration.Value);
                        cmd.Parameters.AddWithValue("@Price", numPrice.Value);
                        cmd.Parameters.AddWithValue("@TourOperatorID", numTourOperatorID.Value);
                        cmd.Parameters.AddWithValue("@BookingID", numBookingID.Value);
                        cmd.Parameters.AddWithValue("@TripID", tripId);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Trip updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            numDuration.Value = (dtpEndDate.Value - dtpStartDate.Value).Days;
        }

        private void edittripform_Load(object sender, EventArgs e)
        {

        }

        private void edittripform_Load_1(object sender, EventArgs e)
        {

        }
    }
}