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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsFormsApp1
{
    public partial class travellerPayment : Form
    {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
        int bookingId = 0;

        decimal amount = 0;
        public travellerPayment(int id, decimal amt)
        {
            InitializeComponent();
            bookingId = id;
            amount = amt;
        }


        private void cancelbtn_Click(object sender, EventArgs e)
        {
            string method = GetSelectedPaymentMethod();
            if (string.IsNullOrEmpty(method))
            {
                MessageBox.Show("Please select a payment method.");
                return;
            }

            if (!decimal.TryParse(amounttext.Text, out decimal amt) || amt <= 0)
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }
            if (amt < amount)
            {
                MessageBox.Show("Amount Insuufficient.");
                return;
            }

            try
            {
                InsertPayment(method, amt,SessionData.id, bookingId);
                MessageBox.Show("Payment processed successfully!");
                amounttext.Text = ""; // Clear amount field
                this.Close(); // Close the payment form
                TripSearch tripSearchForm = new TripSearch();
                tripSearchForm.Dock = DockStyle.Fill;
                tripSearchForm.TopLevel = false;
                Form1.MainPanel.Controls.Clear();
                Form1.MainPanel.Controls.Add(tripSearchForm);
                tripSearchForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void InsertPayment(string method, decimal amt, object id, int bookingId)
        {
            throw new NotImplementedException();
        }

        private string GetSelectedPaymentMethod()
        {
            RadioButton[] radioButtons = { radioButton1, radioButton2, radioButton3 };
            foreach (RadioButton rb in radioButtons)
            {
                if (rb.Checked) return rb.Text;
            }
            return null;
        }

        private void InsertPayment(string method, decimal amount, string travellerId, int bookingId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Get the next PaymentID (auto-increment starting from 8001)
                int nextPaymentId = GetNextPaymentId(conn);

                // Generate the next TransactionID (e.g., TXN123456)
                string transactionId = GenerateTransactionId(conn);

                // SQL Insert Query
                string query = @"INSERT INTO Payment (PaymentID, Date, TransactionID, MethodOfPayment, Amount, TravellerID, BookingID) 
                               VALUES (@PaymentID, @Date, @TransactionID, @Method, @Amount, @TravellerID, @BookingID)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentID", nextPaymentId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd")); // Current date
                    cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                    cmd.Parameters.AddWithValue("@Method", method);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@TravellerID", travellerId);
                    cmd.Parameters.AddWithValue("@BookingID", bookingId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private int GetNextPaymentId(SqlConnection conn)
        {
            string query = "SELECT ISNULL(MAX(PaymentID), 8000) + 1 FROM Payment";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        private string GenerateTransactionId(SqlConnection conn)
        {
            string query = "SELECT ISNULL(MAX(TransactionID), 'TXN123455') FROM Payment";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                string lastTransactionId = (string)cmd.ExecuteScalar();
                int numericPart = int.Parse(lastTransactionId.Substring(3)) + 1; // Extract and increment numeric part
                return $"TXN{numericPart:D6}"; // Format as TXN123456
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void travellerPayment_Load(object sender, EventArgs e)
        {

        }
    }

}
