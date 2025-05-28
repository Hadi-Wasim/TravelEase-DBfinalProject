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
namespace WindowsFormsApp1.forms
{
    public partial class Form1 : Form
    {
        public static Panel MainPanel;
        public Form1()
        {
            InitializeComponent();
            MainPanel = panel1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            login f2 = new login();
            f2.Dock = DockStyle.Fill;   
            f2.TopLevel = false;
            panel1.Controls.Clear();
            panel1.Controls.Add(f2);
            f2.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
