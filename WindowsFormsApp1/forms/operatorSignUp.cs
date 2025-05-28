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

namespace WindowsFormsApp1.forms
{
    public partial class operatorSignUp : Form
    {
        public operatorSignUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void signup_Click(object sender, EventArgs e)
        {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string name = companyName.Text;
            string el = email.Text;
            string ps = password.Text;

            string query = @"
            DECLARE @nextId INT;
            SELECT @nextId = ISNULL(MAX(operatorID), 0) + 1 FROM touroperator;

            INSERT INTO touroperator(operatorID, CompanyName, email, password)
            VALUES (@nextId, @name, @el, @ps);";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@el", el);
            cmd.Parameters.AddWithValue("@ps", ps);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();

            OperatorHome f3 = new OperatorHome();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void operatorSignUp_Load(object sender, EventArgs e)
        {

        }
    }
}