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

namespace WindowsFormsApp1
{

    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }




        private void button6_Click(object sender, EventArgs e)
        {

        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string email = textBox1.Text;
            string password = textBox2.Text;
            if (Traveller.Checked)
            {
                
                string query = "select count(TravellerId) from traveller where email=@email and password=@password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 0)

                {
                    MessageBox.Show("Email or Password is Invalid");
                }
                else
                {
                    string query1 = "select name from traveller where email=@email and password=@password";
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    cmd1.Parameters.AddWithValue("@email", email);
                    cmd1.Parameters.AddWithValue("@password", password);
                    SessionData.Username = Convert.ToString(cmd1.ExecuteScalar());
                    string query2 = "select travellerid from traveller where email=@email and password=@password";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@email", email);
                    cmd2.Parameters.AddWithValue("@password", password);
                    SessionData.id = Convert.ToString(cmd2.ExecuteScalar());
                    MessageBox.Show(SessionData.id.ToString());
                    Dashboard f3 = new Dashboard();
                    f3.Dock = DockStyle.Fill;
                    f3.TopLevel = false;
                    Form1.MainPanel.Controls.Clear();
                    Form1.MainPanel.Controls.Add(f3);


                    f3.Show();
                }// Add Show() to display the form


            }
            else if (Admin.Checked)
            {
                string query = "select count(AdminId) from admin where email=@email and password=@password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    MessageBox.Show("Email or Password is Invalid");
                }
                else
                {
                    string query2 = "select adminid from admin where email=@email and password=@password";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@email", email);
                    cmd2.Parameters.AddWithValue("@password", password);
                    SessionData.id = Convert.ToString(cmd2.ExecuteScalar());
                    Admin_main f3 = new Admin_main();
                    f3.Dock = DockStyle.Fill;
                    f3.TopLevel = false;
                    Form1.MainPanel.Controls.Clear();
                    Form1.MainPanel.Controls.Add(f3);


                    f3.Show();
                }// Add Show() to display the form
            }
            else if (ServiceProvider.Checked)
            {
               
                string query = "select count(serviceproviderid) from serviceprovider where email=@email and password=@password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    MessageBox.Show("Email or Password is Invalid");
                }
                else
                {
                    string query2 = "select serviceproviderid from serviceprovider where email=@email and password=@password";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@email", email);
                    cmd2.Parameters.AddWithValue("@password", password);
                    SessionData.id = Convert.ToString(cmd2.ExecuteScalar());
                    Dashboard_provider f3 = new Dashboard_provider();
                    f3.Dock = DockStyle.Fill;
                    f3.TopLevel = false;
                    Form1.MainPanel.Controls.Clear();
                    Form1.MainPanel.Controls.Add(f3);


                    f3.Show();
                }// Add Show() to display the form
            }
            else if (TourOperator.Checked)
            {
                string query = "select count(operatorid) from touroperator where email=@email and password=@password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    MessageBox.Show("Email or Password is Invalid");
                }
                else
                {
                    string query2 = "select operatorid from touroperator where email=@email and password=@password";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@email", email);
                    cmd2.Parameters.AddWithValue("@password", password);
                    SessionData.id = Convert.ToString(cmd2.ExecuteScalar());
                    OperatorHome f3 = new OperatorHome();
                    f3.Dock = DockStyle.Fill;
                    f3.TopLevel = false;
                    Form1.MainPanel.Controls.Clear();
                    Form1.MainPanel.Controls.Add(f3);


                    f3.Show();
                }// Add Show() to display the form
            }
            else
            {
                MessageBox.Show("Please select a role before logging in.");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Traveller.Checked)
            {
                travellerSignUp f3 = new travellerSignUp();
                f3.Dock = DockStyle.Fill;
                f3.TopLevel = false;
                Form1.MainPanel.Controls.Clear();
                Form1.MainPanel.Controls.Add(f3);


                f3.Show(); // Add Show() to display the form
            }
            else if (Admin.Checked)
            {
                adminSignUp f3 = new adminSignUp();
                f3.Dock = DockStyle.Fill;
                f3.TopLevel = false;
                Form1.MainPanel.Controls.Clear();
                Form1.MainPanel.Controls.Add(f3);


                f3.Show(); // Add Show() to display the form
            }
            else if (ServiceProvider.Checked)
            {
                providerSignUp f3 = new providerSignUp();
                f3.Dock = DockStyle.Fill;
                f3.TopLevel = false;
                Form1.MainPanel.Controls.Clear();
                Form1.MainPanel.Controls.Add(f3);


                f3.Show(); // Add Show() to display the form
            }
            else if (TourOperator.Checked)
            {
                operatorSignUp f3 = new operatorSignUp();
                f3.Dock = DockStyle.Fill;
                f3.TopLevel = false;
                Form1.MainPanel.Controls.Clear();
                Form1.MainPanel.Controls.Add(f3);


                f3.Show(); // Add Show() to display the form
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    public static class SessionData
    {
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string Username { get; set; }
        public static string id { get; set; }

    }
}
