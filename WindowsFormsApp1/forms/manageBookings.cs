using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.forms;
using System.Data.SqlClient;


namespace WindowsFormsApp1.forms
{
    public partial class manageBookings : Form
    {
                string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
        private int currentTourOperatorId;
        private DateTimePicker dtpEndDate; // New control for end date filter

        public manageBookings(int tourOperatorId = 2001)
        {
            InitializeComponent();
            this.currentTourOperatorId = tourOperatorId;
            // Initialize the new DateTimePicker
            dtpEndDate = new DateTimePicker
            {
                CalendarFont = new Font("Calibri", 12F),
                Font = new Font("Calibri", 12F),
                Location = new Point(753, 470), // Below dtpFilter
                Size = new Size(341, 37),
                TabIndex = 38
            };
            this.Controls.Add(dtpEndDate);
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

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            operatorSignUp f3 = new operatorSignUp();
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

        private void button1_Click(object sender, EventArgs e)
        {
            analy f3 = new analy();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form

        }


        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(70, 130, 180); // SteelBlue
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Height = 40;
            btn.Width = 160;
            btn.Cursor = Cursors.Hand;
        }


        private void btnSendReminder_MouseEnter(object sender, EventArgs e)
        {
            btnSendReminder.BackColor = Color.FromArgb(100, 149, 237); // CornflowerBlue
        }

        private void btnSendReminder_MouseLeave(object sender, EventArgs e)
        {
            btnSendReminder.BackColor = Color.FromArgb(70, 130, 180); // SteelBlue
        }


        private void manageBookings_Load(object sender, EventArgs e)
        {
            // Setup columns
            dgvBookings.Columns.Clear();
            dgvBookings.Columns.Add("BookingID", "Booking ID");
            dgvBookings.Columns.Add("Customer", "Customer");
            dgvBookings.Columns.Add("Tour", "Tour");
            dgvBookings.Columns.Add("Date", "Date");
            dgvBookings.Columns.Add("Status", "Status");

            DataGridViewCheckBoxColumn reminderCol = new DataGridViewCheckBoxColumn();
            reminderCol.HeaderText = "Reminder Sent";
            reminderCol.Name = "ReminderSent";
            dgvBookings.Columns.Add(reminderCol);

            DataGridViewButtonColumn cancelCol = new DataGridViewButtonColumn();
            cancelCol.Text = "Cancel";
            cancelCol.Name = "Cancel";
            cancelCol.UseColumnTextForButtonValue = true;
            dgvBookings.Columns.Add(cancelCol);

            DataGridViewButtonColumn refundCol = new DataGridViewButtonColumn();
            refundCol.Text = "Refund";
            refundCol.Name = "Refund";
            refundCol.UseColumnTextForButtonValue = true;
            dgvBookings.Columns.Add(refundCol);

            StyleButton(btnSendReminder);
            btnSendReminder.MouseEnter += btnSendReminder_MouseEnter;
            btnSendReminder.MouseLeave += btnSendReminder_MouseLeave;

            // DataGridView Styling
            dgvBookings.EnableHeadersVisualStyles = false;
            dgvBookings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 90);
            dgvBookings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBookings.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvBookings.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvBookings.DefaultCellStyle.BackColor = Color.White;
            dgvBookings.DefaultCellStyle.ForeColor = Color.Black;
            dgvBookings.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 250);
            dgvBookings.DefaultCellStyle.SelectionBackColor = Color.FromArgb(170, 190, 255);
            dgvBookings.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvBookings.RowTemplate.Height = 45;
            dgvBookings.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBookings.GridColor = Color.LightGray;
            dgvBookings.BorderStyle = BorderStyle.None;

            // Initialize filter controls
            cmbStatusFilter.SelectedIndex = 0; // Default to "all"
            
            // Load bookings
            LoadBookings();

            // Attach filter event handlers
            cmbStatusFilter.SelectedIndexChanged += (s, ev) => LoadBookings();
            
        }

        private void LoadBookings()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    b.BookingID,
                    t.Name AS Customer,
                    tr.Title AS Tour,
                    b.BookingDate,
                    b.Status,
                    b.ReminderSent
                FROM Booking b
                JOIN Traveller t ON b.TravellerID = t.TravellerID
                JOIN Trip tr ON b.TripID = tr.TripID
               ";
                    
                    // Apply status filter
                    string statusFilter = cmbStatusFilter.SelectedItem?.ToString();
                    if (statusFilter != "all")
                    {
                        query += " WHERE b.Status = @Status";
                    }

                    // Apply date range filter only if dates are meaningful
                   

                    // Log the query for debugging
                    Console.WriteLine("Executing Query: " + query);

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (statusFilter != "all")
                        {
                            cmd.Parameters.AddWithValue("@Status", statusFilter);

                        }


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dgvBookings.Rows.Clear();
                            int rowCount = 0;
                            while (reader.Read())
                            {
                                dgvBookings.Rows.Add(
                                    reader["BookingID"].ToString(),
                                    reader["Customer"].ToString(),
                                    reader["Tour"].ToString(),
                                    Convert.ToDateTime(reader["BookingDate"]).ToString("yyyy-MM-dd"),
                                    reader["Status"].ToString(),
                                    Convert.ToBoolean(reader["ReminderSent"])
                                );
                                rowCount++;
                            }
                            Console.WriteLine($"Rows loaded: {rowCount}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bookings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dgvBookings.Columns[e.ColumnIndex].Name;
            string bookingID = dgvBookings.Rows[e.RowIndex].Cells["BookingID"].Value.ToString();
            string currentStatus = dgvBookings.Rows[e.RowIndex].Cells["Status"].Value.ToString();

            if (colName == "Cancel")
            {
                if (currentStatus == "Cancelled" || currentStatus == "Refunded")
                {
                    MessageBox.Show("This booking cannot be cancelled as it is already " + currentStatus.ToLower() + ".", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult res = MessageBox.Show($"Cancel booking {bookingID}?", "Confirm", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            // Check if the booking is confirmed and payment was made
                            string checkQuery = @"
                        SELECT Status, TimeOfPayment, TravellerID, Cost
                        FROM Booking
                        WHERE BookingID = @BookingID";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                using (SqlDataReader reader = checkCmd.ExecuteReader())
                                {
                                    if (!reader.Read())
                                    {
                                        MessageBox.Show("Booking not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    string status = reader["Status"].ToString();
                                    bool paymentMade = !reader.IsDBNull(reader.GetOrdinal("TimeOfPayment"));
                                    int travellerID = Convert.ToInt32(reader["TravellerID"]);
                                    decimal cost = Convert.ToDecimal(reader["Cost"]);

                                    reader.Close();

                                    if (status != "Confirmed")
                                    {
                                        MessageBox.Show("Only confirmed bookings can be cancelled with a refund.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    // Update the booking status to Cancelled
                                    string updateQuery = "UPDATE Booking SET Status = 'Cancelled' WHERE BookingID = @BookingID";
                                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                        updateCmd.ExecuteNonQuery();
                                    }

                                    // If payment was made, process the refund
                                    if (paymentMade)
                                    {
                                        // Generate the next RefundID
                                        string maxIdQuery = "SELECT ISNULL(MAX(RefundID), 0) + 1 FROM Refund";
                                        int nextRefundId = Convert.ToInt32(new SqlCommand(maxIdQuery, conn).ExecuteScalar());

                                        string refundQuery = @"
                                    INSERT INTO Refund (RefundID, Method, Amount, Reason, Date, TravellerID, BookingID)
                                    VALUES (@RefundID, @Method, @Amount, @Reason, @Date, @TravellerID, @BookingID)";
                                        using (SqlCommand refundCmd = new SqlCommand(refundQuery, conn))
                                        {
                                            refundCmd.Parameters.AddWithValue("@RefundID", nextRefundId);
                                            refundCmd.Parameters.AddWithValue("@Method", "Bank Transfer");
                                            refundCmd.Parameters.AddWithValue("@Amount", cost);
                                            refundCmd.Parameters.AddWithValue("@Reason", "cancelled by the operator, refund done");
                                            refundCmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                                            refundCmd.Parameters.AddWithValue("@TravellerID", travellerID);
                                            refundCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                            refundCmd.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("Booking cancelled and refund processed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Booking cancelled. No refund was processed as payment was not made.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }

                            // Update the grid
                            dgvBookings.Rows[e.RowIndex].Cells["Status"].Value = "Cancelled";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error cancelling booking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (colName == "Refund")
            {
                if (currentStatus == "Refunded" || currentStatus == "Cancelled")
                {
                    MessageBox.Show("This booking cannot be refunded as it is already " + currentStatus.ToLower() + ".", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult res = MessageBox.Show($"Refund for booking {bookingID}?", "Confirm Refund", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            // Check if the booking is confirmed and payment was made
                            string checkQuery = @"
                        SELECT Status, TimeOfPayment, TravellerID, Cost
                        FROM Booking
                        WHERE BookingID = @BookingID";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                using (SqlDataReader reader = checkCmd.ExecuteReader())
                                {
                                    if (!reader.Read())
                                    {
                                        MessageBox.Show("Booking not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    string status = reader["Status"].ToString();
                                    bool paymentMade = !reader.IsDBNull(reader.GetOrdinal("TimeOfPayment"));
                                    int travellerID = Convert.ToInt32(reader["TravellerID"]);
                                    decimal cost = Convert.ToDecimal(reader["Cost"]);

                                    reader.Close();

                                    if (status != "Confirmed")
                                    {
                                        MessageBox.Show("Only confirmed bookings can be refunded.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    if (!paymentMade)
                                    {
                                        MessageBox.Show("No payment has been made for this booking, so it cannot be refunded.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    // Update the booking status to Refunded
                                    string updateQuery = "UPDATE Booking SET Status = 'Refunded' WHERE BookingID = @BookingID";
                                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                        updateCmd.ExecuteNonQuery();
                                    }

                                    // Generate the next RefundID
                                    string maxIdQuery = "SELECT ISNULL(MAX(RefundID), 0) + 1 FROM Refund";
                                    int nextRefundId = Convert.ToInt32(new SqlCommand(maxIdQuery, conn).ExecuteScalar());

                                    // Insert refund record
                                    string refundQuery = @"
                                INSERT INTO Refund (RefundID, Method, Amount, Reason, Date, TravellerID, BookingID)
                                VALUES (@RefundID, @Method, @Amount, @Reason, @Date, @TravellerID, @BookingID)";
                                    using (SqlCommand refundCmd = new SqlCommand(refundQuery, conn))
                                    {
                                        refundCmd.Parameters.AddWithValue("@RefundID", nextRefundId);
                                        refundCmd.Parameters.AddWithValue("@Method", "Bank Transfer");
                                        refundCmd.Parameters.AddWithValue("@Amount", cost);
                                        refundCmd.Parameters.AddWithValue("@Reason", "manual refund initiated by operator");
                                        refundCmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                                        refundCmd.Parameters.AddWithValue("@TravellerID", travellerID);
                                        refundCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                        refundCmd.ExecuteNonQuery();
                                    }

                                    // Update the grid
                                    dgvBookings.Rows[e.RowIndex].Cells["Status"].Value = "Refunded";
                                    MessageBox.Show("Refund processed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error processing refund: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnSendReminder_Click(object sender, EventArgs e)
        {
            bool remindersSent = false;
            Label statusLabel = new Label { Location = new Point(340, 570), Text = "Sending reminders...", ForeColor = Color.Blue };
            this.Controls.Add(statusLabel);

            foreach (DataGridViewRow row in dgvBookings.Rows)
            {
                if (row.Cells["ReminderSent"].Value is bool sent && !sent)
                {
                    string bookingID = row.Cells["BookingID"].Value.ToString();
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "UPDATE Booking SET ReminderSent = 1 WHERE BookingID = @BookingID";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@BookingID", bookingID);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        row.Cells["ReminderSent"].Value = true;
                        remindersSent = true;
                        statusLabel.Text = "Sending reminders... (Processing " + bookingID + ")";
                        Application.DoEvents(); // Update UI during loop
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error sending reminder for booking " + bookingID + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            this.Controls.Remove(statusLabel);
            if (remindersSent)
            {
                MessageBox.Show("Reminders sent to pending customers.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No reminders to send.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
