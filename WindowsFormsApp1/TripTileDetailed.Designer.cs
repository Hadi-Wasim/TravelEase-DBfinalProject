using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class TripTileDetailed
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTripID;
        private Label lblTitle;
        private Label lblDescription;
        private Label lblDestination;
        private Label lblType;
        private Label lblCapacity;
        private Label lblStartDate;
        private Label lblEndDate;
        private Label lblDuration;
        private Label lblPrice;
        private Label lblOperatorName;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTripID = new Label();
            this.lblTitle = new Label();
            this.lblDescription = new Label();
            this.lblDestination = new Label();
            this.lblType = new Label();
            this.lblCapacity = new Label();
            this.lblStartDate = new Label();
            this.lblEndDate = new Label();
            this.lblDuration = new Label();
            this.lblPrice = new Label();
            this.lblOperatorName = new Label();

            this.SuspendLayout();

            Label[] labels = new Label[] {
            lblTripID, lblTitle, lblDescription, lblDestination, lblType,
            lblCapacity, lblStartDate, lblEndDate, lblDuration, lblPrice, lblOperatorName
        };

            int y = 10;
            foreach (var lbl in labels)
            {
                lbl.AutoSize = true;
                lbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                lbl.Location = new Point(10, y);
                lbl.Size = new Size(300, 20);
                this.Controls.Add(lbl);
                y += 22;
            }

            // 
            // TripTileDetailed
            // 
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Name = "TripTileDetailed";
            this.Size = new Size(350, y + 10);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

}
