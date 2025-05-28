namespace WindowsFormsApp1
{
    partial class UpcomingTripTile
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblenddate = new System.Windows.Forms.Label();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.lblBookingId = new System.Windows.Forms.Label();
            this.lblStartdate = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDateOfBooking = new System.Windows.Forms.Label();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.lblPaymentStatus = new System.Windows.Forms.Label();
            this.lblBookingStatus = new System.Windows.Forms.Label();
            this.refundbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblenddate
            // 
            this.lblenddate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblenddate.Location = new System.Drawing.Point(179, 86);
            this.lblenddate.Name = "lblenddate";
            this.lblenddate.Size = new System.Drawing.Size(103, 20);
            this.lblenddate.TabIndex = 17;
            this.lblenddate.Text = "\t$499 / person";
            // 
            // cancelbtn
            // 
            this.cancelbtn.BackColor = System.Drawing.SystemColors.HotTrack;
            this.cancelbtn.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelbtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelbtn.Location = new System.Drawing.Point(302, 98);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(176, 65);
            this.cancelbtn.TabIndex = 16;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = false;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // lblBookingId
            // 
            this.lblBookingId.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingId.Location = new System.Drawing.Point(27, 119);
            this.lblBookingId.Name = "lblBookingId";
            this.lblBookingId.Size = new System.Drawing.Size(269, 20);
            this.lblBookingId.TabIndex = 14;
            this.lblBookingId.Text = "\t$499 / person";
            // 
            // lblStartdate
            // 
            this.lblStartdate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartdate.Location = new System.Drawing.Point(27, 86);
            this.lblStartdate.Name = "lblStartdate";
            this.lblStartdate.Size = new System.Drawing.Size(108, 20);
            this.lblStartdate.TabIndex = 13;
            this.lblStartdate.Text = "\t$499 / person";
            // 
            // lblDestination
            // 
            this.lblDestination.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestination.Location = new System.Drawing.Point(27, 52);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(265, 20);
            this.lblDestination.TabIndex = 12;
            this.lblDestination.Text = "\t$499 / person";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(23, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(186, 20);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "\"Hunza Valley Getaway\"";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblDateOfBooking
            // 
            this.lblDateOfBooking.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfBooking.Location = new System.Drawing.Point(27, 155);
            this.lblDateOfBooking.Name = "lblDateOfBooking";
            this.lblDateOfBooking.Size = new System.Drawing.Size(270, 20);
            this.lblDateOfBooking.TabIndex = 18;
            this.lblDateOfBooking.Text = "\t$499 / person";
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPrice.Location = new System.Drawing.Point(27, 190);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(355, 20);
            this.lblTotalPrice.TabIndex = 19;
            this.lblTotalPrice.Text = "\t$499 / person";
            // 
            // lblPaymentStatus
            // 
            this.lblPaymentStatus.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentStatus.Location = new System.Drawing.Point(27, 221);
            this.lblPaymentStatus.Name = "lblPaymentStatus";
            this.lblPaymentStatus.Size = new System.Drawing.Size(283, 20);
            this.lblPaymentStatus.TabIndex = 20;
            this.lblPaymentStatus.Text = "\t$499 / person";
            // 
            // lblBookingStatus
            // 
            this.lblBookingStatus.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingStatus.Location = new System.Drawing.Point(298, 64);
            this.lblBookingStatus.Name = "lblBookingStatus";
            this.lblBookingStatus.Size = new System.Drawing.Size(230, 20);
            this.lblBookingStatus.TabIndex = 21;
            this.lblBookingStatus.Text = "\t$499 / person";
            // 
            // refundbtn
            // 
            this.refundbtn.BackColor = System.Drawing.SystemColors.HotTrack;
            this.refundbtn.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refundbtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.refundbtn.Location = new System.Drawing.Point(302, 169);
            this.refundbtn.Name = "refundbtn";
            this.refundbtn.Size = new System.Drawing.Size(176, 65);
            this.refundbtn.TabIndex = 22;
            this.refundbtn.Text = "Refund";
            this.refundbtn.UseVisualStyleBackColor = false;
            // 
            // UpcomingTripTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.refundbtn);
            this.Controls.Add(this.lblBookingStatus);
            this.Controls.Add(this.lblPaymentStatus);
            this.Controls.Add(this.lblTotalPrice);
            this.Controls.Add(this.lblDateOfBooking);
            this.Controls.Add(this.lblenddate);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.lblBookingId);
            this.Controls.Add(this.lblStartdate);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.lblTitle);
            this.Name = "UpcomingTripTile";
            this.Size = new System.Drawing.Size(540, 265);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblenddate;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Label lblBookingId;
        private System.Windows.Forms.Label lblStartdate;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDateOfBooking;
        private System.Windows.Forms.Label lblTotalPrice;
        private System.Windows.Forms.Label lblPaymentStatus;
        private System.Windows.Forms.Label lblBookingStatus;
        private System.Windows.Forms.Button refundbtn;
    }
}
