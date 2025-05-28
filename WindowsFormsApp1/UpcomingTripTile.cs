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
    public partial class UpcomingTripTile : UserControl
    {
        public event EventHandler cancelClicked;
        public event EventHandler refundClicked;

        public UpcomingTripTile(bool check = false)
        {
            InitializeComponent();
            if (check)
            {
                cancelbtn.Visible = false;
                refundbtn.Visible = false;
            }
            cancelbtn.Click += (s, e) => cancelClicked?.Invoke(this, e);
            refundbtn.Click += (s, e) => refundClicked?.Invoke(this, e);

        }
        public string TripTitle
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        public string TripBookingStatus
        {
            get => lblBookingStatus.Text;
            set => lblBookingStatus.Text = "Booking Status: " + value;
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
        public string BookingId
        {
            get => lblBookingId.Text;
            set => lblBookingId.Text = "Booking Reference: " + value;
        }

        public string DateofBooking
        {
            get => lblDateOfBooking.Text;
            set => lblDateOfBooking.Text = value;
        }

        public string TripPrice
        {
            get => lblTotalPrice.Text;
            set => lblTotalPrice.Text = value;
        }
        public string PaymentStatus
        {
            get => lblPaymentStatus.Text;
            set => lblPaymentStatus.Text = "Payment Status " + value;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {

        }

        private void UpcomingTripTile_Load(object sender, EventArgs e)
        {

        }
    }
}
