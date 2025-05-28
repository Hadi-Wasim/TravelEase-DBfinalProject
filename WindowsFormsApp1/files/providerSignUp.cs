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
    public partial class providerSignUp : Form
    {
       
        public providerSignUp()
        {
            InitializeComponent();
            
        }

        private void signup_Click(object sender, EventArgs e)
        {
            Dashboard_provider f3 = new Dashboard_provider();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }
    }
}
