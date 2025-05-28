using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TripTileHadi : UserControl
    {
        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;
        public int TripId { get; set; }
        public TripTileHadi()
        {
            InitializeComponent();
        }

        // Public properties to allow setting data from outside
        public Image TripImage
        {
            get => pictureBox1.Image;
            set => pictureBox1.Image = value;
        }

        // Inside TripTile.cs
        public string TripTitle
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        public string TripType
        {
            get => lblType.Text;
            set => lblType.Text = value;
        }

        public string Destination
        {
            get => lblDestination.Text;
            set => lblDestination.Text = value;
        }

        public string Duration
        {
            get => lblDuration.Text;
            set => lblDuration.Text = value;
        }

        public string Capacity
        {
            get => lblCapacity.Text;
            set => lblCapacity.Text = value;
        }

        public string Description
        {
            get => lblDescription.Text;
            set => lblDescription.Text = value;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }

        private void TripTile_Load(object sender, EventArgs e)
        {

        }

        private void lblDestination_Click(object sender, EventArgs e)
        {

        }
    }
}
