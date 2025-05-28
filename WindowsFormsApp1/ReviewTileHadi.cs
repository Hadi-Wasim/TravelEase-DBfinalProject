using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ReviewTileHadi : UserControl
    {
        public ReviewTileHadi()
        {
            InitializeComponent();
        }

        // Property to set Review ID
        public int ReviewID
        {
            get { return int.Parse(lblReviewID.Text.Replace("Review ID: ", "")); }
            set { lblReviewID.Text = $"Review ID: {value}"; }
        }

        // Property to set Traveller ID
        public int TravellerID
        {
            get { return int.Parse(lblTravellerID.Text.Replace("Traveller ID: ", "")); }
            set { lblTravellerID.Text = $"Traveller ID: {value}"; }
        }

        // Property to set Rating (display as stars)
        public int Rating
        {
            get { return int.Parse(lblStars.Text); }
            set
            {
                lblStars.Text = new string('★', value); // Display stars based on rating (1-5)
                if (value < 1 || value > 5) lblStars.Text = "Invalid Rating";
            }
        }

        // Property to set Comment
        public string Comment
        {
            get { return txtComment.Text; }
            set { txtComment.Text = value; }
        }

        private void ReviewTile_Load(object sender, EventArgs e)
        {

        }
    }
}