namespace WindowsFormsApp1
{
    partial class ReviewTileHadi
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
            this.lblReviewID = new System.Windows.Forms.Label();
            this.lblTravellerID = new System.Windows.Forms.Label();
            this.lblStars = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.traveleaseDataSet1 = new WindowsFormsApp1.traveleaseDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.traveleaseDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReviewID
            // 
            this.lblReviewID.AutoSize = true;
            this.lblReviewID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReviewID.Location = new System.Drawing.Point(10, 10);
            this.lblReviewID.Name = "lblReviewID";
            this.lblReviewID.Size = new System.Drawing.Size(107, 25);
            this.lblReviewID.TabIndex = 0;
            this.lblReviewID.Text = "Review ID: ";
            // 
            // lblTravellerID
            // 
            this.lblTravellerID.AutoSize = true;
            this.lblTravellerID.Location = new System.Drawing.Point(10, 30);
            this.lblTravellerID.Name = "lblTravellerID";
            this.lblTravellerID.Size = new System.Drawing.Size(97, 20);
            this.lblTravellerID.TabIndex = 1;
            this.lblTravellerID.Text = "Traveller ID: ";
            // 
            // lblStars
            // 
            this.lblStars.AutoSize = true;
            this.lblStars.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStars.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblStars.Location = new System.Drawing.Point(10, 50);
            this.lblStars.Name = "lblStars";
            this.lblStars.Size = new System.Drawing.Size(69, 28);
            this.lblStars.TabIndex = 2;
            this.lblStars.Text = "Rating";
            // 
            // txtComment
            // 
            this.txtComment.BackColor = System.Drawing.Color.White;
            this.txtComment.Location = new System.Drawing.Point(10, 75);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(280, 50);
            this.txtComment.TabIndex = 3;
            // 
            // traveleaseDataSet1
            // 
            this.traveleaseDataSet1.DataSetName = "traveleaseDataSet";
            this.traveleaseDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReviewTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblReviewID);
            this.Controls.Add(this.lblTravellerID);
            this.Controls.Add(this.lblStars);
            this.Controls.Add(this.txtComment);
            this.Name = "ReviewTile";
            this.Size = new System.Drawing.Size(300, 140);
            this.Load += new System.EventHandler(this.ReviewTile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.traveleaseDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReviewID;
        private System.Windows.Forms.Label lblTravellerID;
        private System.Windows.Forms.Label lblStars;
        private System.Windows.Forms.TextBox txtComment;
        private traveleaseDataSet traveleaseDataSet1;
    }
}