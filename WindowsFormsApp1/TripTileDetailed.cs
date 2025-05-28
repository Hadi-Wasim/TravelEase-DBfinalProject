using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1 // ← replace this with your actual namespace
{
    public partial class TripTileDetailed : UserControl
    {
        public TripTileDetailed()
        {
            InitializeComponent();
        }

        public string TripID
        {
            get => lblTripID.Text;
            set => lblTripID.Text = "Trip ID: " + value;
        }

        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = "Title: " + value;
        }

        public string Description
        {
            get => lblDescription.Text;
            set => lblDescription.Text = "Description: " + value;
        }

        public string Destination
        {
            get => lblDestination.Text;
            set => lblDestination.Text = "Destination: " + value;
        }

        public string Type
        {
            get => lblType.Text;
            set => lblType.Text = "Type: " + value;
        }

        public string Capacity
        {
            get => lblCapacity.Text;
            set => lblCapacity.Text = "Capacity: " + value;
        }

        public string StartDate
        {
            get => lblStartDate.Text;
            set => lblStartDate.Text = "Start Date: " + value;
        }

        public string EndDate
        {
            get => lblEndDate.Text;
            set => lblEndDate.Text = "End Date: " + value;
        }

        public string Duration
        {
            get => lblDuration.Text;
            set => lblDuration.Text = "Duration: " + value + " days";
        }

        public string Price
        {
            get => lblPrice.Text;
            set => lblPrice.Text = "Price: Rs. " + value;
        }

        public string TourOperatorName
        {
            get => lblOperatorName.Text;
            set => lblOperatorName.Text = "Operator: " + value;
        }
    }
}
