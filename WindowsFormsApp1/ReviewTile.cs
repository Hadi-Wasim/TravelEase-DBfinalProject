using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ReviewTile : UserControl
    {
        private Label lblReviewId;
        private Label lblReviewed;
        private Label lblDate;
        private Label lblRating;
        private Label lblComment;

        public ReviewTile()
        {
            InitializeComponent();
            SetupControls();
        }

        private void SetupControls()
        {
            // Initialize labels
            lblReviewId = new Label();
            lblReviewId.Location = new Point(10, 10);
            lblReviewId.Size = new Size(200, 20);
            lblReviewId.Text = "Review ID: ";
            this.Controls.Add(lblReviewId);

            lblReviewed = new Label();
            lblReviewed.Location = new Point(10, 35);
            lblReviewed.Size = new Size(200, 20);
            lblReviewed.Text = "Reviewed: ";
            this.Controls.Add(lblReviewed);

            lblDate = new Label();
            lblDate.Location = new Point(10, 60);
            lblDate.Size = new Size(200, 20);
            lblDate.Text = "Date: ";
            this.Controls.Add(lblDate);

            lblRating = new Label();
            lblRating.Location = new Point(10, 85);
            lblRating.Size = new Size(200, 20);
            lblRating.Text = "Rating: ";
            this.Controls.Add(lblRating);

            lblComment = new Label();
            lblComment.Location = new Point(10, 110);
            lblComment.Size = new Size(200, 40);
            lblComment.Text = "Comment: ";
            lblComment.AutoSize = false; // Allow wrapping
            this.Controls.Add(lblComment);

            // Set the size of the tile
            this.Size = new Size(220, 160);
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        // Properties to set the label values
        public string ReviewId
        {
            get => lblReviewId.Text;
            set => lblReviewId.Text = "Review ID: " + value;
        }

        public string Reviewed
        {
            get => lblReviewed.Text;
            set => lblReviewed.Text = "Reviewed: " + value;
        }

        public string Date
        {
            get => lblDate.Text;
            set => lblDate.Text = "Date: " + value;
        }

        public string Rating
        {
            get => lblRating.Text;
            set => lblRating.Text = "Rating: " + value;
        }

        public string Comment
        {
            get => lblComment.Text;
            set => lblComment.Text = "Comment: " + value;
        }

        private void ReviewTile_Load(object sender, EventArgs e)
        {

        }
    }
}