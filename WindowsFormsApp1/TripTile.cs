using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TripTile : UserControl
    {
        public event EventHandler bookClicked;
        public event EventHandler payClicked;
        public TripTile(bool buttonShow = false)
        {
            InitializeComponent();
            if (buttonShow)
            {
                bookbtn.Enabled = false;
                paymentButton.Enabled = false;
            }
            bookbtn.Click += (s, e) => bookClicked?.Invoke(this, e);
            paymentButton.Click += (s, e) => payClicked?.Invoke(this, e);

        }

        int bookid = 0;
        public int BookingId
        {
            get => bookid;
            set => bookid = value;
        }
        decimal payment = 0;
        public decimal Payment
        {
            get => payment;
            set => payment = value;
        }
        public string TripTitle
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        public string TripType
        {
            get => lblType.Text;
            set => lblType.Text = "Activity Type: " + value;
        }

        public string Destination
        {
            get => lblDestination.Text;
            set => lblDestination.Text = "To: " + value;
        }

        public string StartDate
        {
            get => lblStartdate.Text;
            set => lblStartdate.Text = value;
        }
        public string EndDate
        {
            get => lblenddate.Text;
            set => lblenddate.Text = value;
        }
        public string Capacity
        {
            get => lblCapacity.Text;
            set => lblCapacity.Text = "Group Size: " + value;
        }

        public string Description
        {
            get => lblDescription.Text;
            set => lblDescription.Text = value;
        }

        public string TripPrice
        {
            get => lblPrice.Text;
            set => lblPrice.Text = value;
        }
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void paymentButton_Click(object sender, EventArgs e)
        {

        }

        private void bookbtn_Click(object sender, EventArgs e)
        {

        }

        private void lblType_Click(object sender, EventArgs e)
        {

        }

        private void TripTile2_Load(object sender, EventArgs e)
        {

        }
    }
}
