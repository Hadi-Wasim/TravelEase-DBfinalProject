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
            if (Traveller.Checked)
            {
                Dashboard f3 = new Dashboard();
                f3.Dock = DockStyle.Fill;
                f3.TopLevel = false;
                Form1.MainPanel.Controls.Clear();
                Form1.MainPanel.Controls.Add(f3);


                f3.Show(); // Add Show() to display the form
            }
            else if (Admin.Checked)
            {
                Admin_main f3 = new Admin_main();
                f3.Dock = DockStyle.Fill;
                f3.TopLevel = false;
                Form1.MainPanel.Controls.Clear();
                Form1.MainPanel.Controls.Add(f3);


                f3.Show(); // Add Show() to display the form
            }
            else if (ServiceProvider.Checked)
            {
                Dashboard_provider f3 = new Dashboard_provider();
                f3.Dock = DockStyle.Fill;
                f3.TopLevel = false;
                Form1.MainPanel.Controls.Clear();
                Form1.MainPanel.Controls.Add(f3);


                f3.Show(); // Add Show() to display the form
            }
            else if (TourOperator.Checked)
            {
                OperatorHome f3 = new OperatorHome();
                f3.Dock = DockStyle.Fill;
                f3.TopLevel = false;
                Form1.MainPanel.Controls.Clear();
                Form1.MainPanel.Controls.Add(f3);


                f3.Show(); // Add Show() to display the form
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
    }
}
