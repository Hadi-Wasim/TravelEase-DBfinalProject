using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsApp1.forms
{
    partial class edittripform
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.NumericUpDown numCapacity;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblTourOperatorID;
        private System.Windows.Forms.NumericUpDown numTourOperatorID;
        private System.Windows.Forms.Label lblBookingID;
        private System.Windows.Forms.NumericUpDown numBookingID;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDestination = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.numCapacity = new System.Windows.Forms.NumericUpDown();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblDuration = new System.Windows.Forms.Label();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lblTourOperatorID = new System.Windows.Forms.Label();
            this.numTourOperatorID = new System.Windows.Forms.NumericUpDown();
            this.lblBookingID = new System.Windows.Forms.Label();
            this.numBookingID = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";

            // txtTitle
            this.txtTitle.Location = new System.Drawing.Point(120, 20);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 20);
            this.txtTitle.TabIndex = 1;

            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 50);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(66, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";

            // txtDescription
            this.txtDescription.Location = new System.Drawing.Point(120, 50);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 60);
            this.txtDescription.TabIndex = 3;

            // lblDestination
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(20, 120);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(66, 13);
            this.lblDestination.TabIndex = 4;
            this.lblDestination.Text = "Destination:";

            // txtDestination
            this.txtDestination.Location = new System.Drawing.Point(120, 120);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(200, 20);
            this.txtDestination.TabIndex = 5;

            // lblType
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(20, 150);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(38, 13);
            this.lblType.TabIndex = 6;
            this.lblType.Text = "Type:";

            // cmbType
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Items.AddRange(new object[] { "Adventure", "Cultural", "Leisure", "Business" });
            this.cmbType.Location = new System.Drawing.Point(120, 150);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(200, 21);
            this.cmbType.TabIndex = 7;

            // lblCapacity
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Location = new System.Drawing.Point(20, 180);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(50, 13);
            this.lblCapacity.TabIndex = 8;
            this.lblCapacity.Text = "Capacity:";

            // numCapacity
            this.numCapacity.Location = new System.Drawing.Point(120, 180);
            this.numCapacity.Maximum = 1000;
            this.numCapacity.Minimum = 1;
            this.numCapacity.Name = "numCapacity";
            this.numCapacity.Size = new System.Drawing.Size(200, 20);
            this.numCapacity.TabIndex = 9;

            // lblStartDate
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(20, 210);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 10;
            this.lblStartDate.Text = "Start Date:";

            // dtpStartDate
            this.dtpStartDate.Location = new System.Drawing.Point(120, 210);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 11;

            // lblEndDate
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(20, 240);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 12;
            this.lblEndDate.Text = "End Date:";

            // dtpEndDate
            this.dtpEndDate.Location = new System.Drawing.Point(120, 240);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 13;

            // lblDuration
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(20, 270);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(50, 13);
            this.lblDuration.TabIndex = 14;
            this.lblDuration.Text = "Duration:";

            // numDuration
            this.numDuration.Location = new System.Drawing.Point(120, 270);
            this.numDuration.Maximum = 365;
            this.numDuration.Minimum = 1;
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(200, 20);
            this.numDuration.TabIndex = 15;

            // lblPrice
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(20, 300);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(37, 13);
            this.lblPrice.TabIndex = 16;
            this.lblPrice.Text = "Price:";

            // numPrice
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(120, 300);
            this.numPrice.Maximum = 100000;
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(200, 20);
            this.numPrice.TabIndex = 17;

            // lblTourOperatorID
            this.lblTourOperatorID.AutoSize = true;
            this.lblTourOperatorID.Location = new System.Drawing.Point(20, 330);
            this.lblTourOperatorID.Name = "lblTourOperatorID";
            this.lblTourOperatorID.Size = new System.Drawing.Size(87, 13);
            this.lblTourOperatorID.TabIndex = 18;
            this.lblTourOperatorID.Text = "Tour Operator ID:";

            // numTourOperatorID
            this.numTourOperatorID.Location = new System.Drawing.Point(120, 330);
            this.numTourOperatorID.Maximum = 10000;
            this.numTourOperatorID.Name = "numTourOperatorID";
            this.numTourOperatorID.Size = new System.Drawing.Size(200, 20);
            this.numTourOperatorID.TabIndex = 19;

            // lblBookingID
            this.lblBookingID.AutoSize = true;
            this.lblBookingID.Location = new System.Drawing.Point(20, 360);
            this.lblBookingID.Name = "lblBookingID";
            this.lblBookingID.Size = new System.Drawing.Size(62, 13);
            this.lblBookingID.TabIndex = 20;
            this.lblBookingID.Text = "Booking ID:";

            // numBookingID
            this.numBookingID.Location = new System.Drawing.Point(120, 360);
            this.numBookingID.Maximum = 10000;
            this.numBookingID.Name = "numBookingID";
            this.numBookingID.Size = new System.Drawing.Size(200, 20);
            this.numBookingID.TabIndex = 21;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(120, 390);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(220, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;

            // edittripform
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblCapacity);
            this.Controls.Add(this.numCapacity);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.numDuration);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.lblTourOperatorID);
            this.Controls.Add(this.numTourOperatorID);
            this.Controls.Add(this.lblBookingID);
            this.Controls.Add(this.numBookingID);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "edittripform";
            this.Text = "Edit Trip";
            this.Load += new System.EventHandler(this.edittripform_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.numCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTourOperatorID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBookingID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}