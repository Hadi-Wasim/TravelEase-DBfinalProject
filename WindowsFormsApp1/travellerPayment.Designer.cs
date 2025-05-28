namespace WindowsFormsApp1
{
    partial class travellerPayment
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label8 = new System.Windows.Forms.Label();
            this.resultContainer4 = new System.Windows.Forms.FlowLayoutPanel();
            this.resultContainer3 = new System.Windows.Forms.FlowLayoutPanel();
            this.resultContainer2 = new System.Windows.Forms.FlowLayoutPanel();
            this.resultContainer1 = new System.Windows.Forms.FlowLayoutPanel();
            this.amounttext = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.paybtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(159, 182);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(251, 29);
            this.label8.TabIndex = 82;
            this.label8.Text = "Select Payment Method";
            // 
            // resultContainer4
            // 
            this.resultContainer4.Location = new System.Drawing.Point(842, 309);
            this.resultContainer4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.resultContainer4.Name = "resultContainer4";
            this.resultContainer4.Size = new System.Drawing.Size(180, 0);
            this.resultContainer4.TabIndex = 81;
            // 
            // resultContainer3
            // 
            this.resultContainer3.Location = new System.Drawing.Point(573, 308);
            this.resultContainer3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.resultContainer3.Name = "resultContainer3";
            this.resultContainer3.Size = new System.Drawing.Size(180, 0);
            this.resultContainer3.TabIndex = 80;
            // 
            // resultContainer2
            // 
            this.resultContainer2.Location = new System.Drawing.Point(346, 308);
            this.resultContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.resultContainer2.Name = "resultContainer2";
            this.resultContainer2.Size = new System.Drawing.Size(180, 0);
            this.resultContainer2.TabIndex = 79;
            // 
            // resultContainer1
            // 
            this.resultContainer1.Location = new System.Drawing.Point(140, 308);
            this.resultContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.resultContainer1.Name = "resultContainer1";
            this.resultContainer1.Size = new System.Drawing.Size(180, 0);
            this.resultContainer1.TabIndex = 78;
            // 
            // amounttext
            // 
            this.amounttext.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.amounttext.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.amounttext.Location = new System.Drawing.Point(194, 385);
            this.amounttext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.amounttext.Name = "amounttext";
            this.amounttext.Size = new System.Drawing.Size(178, 26);
            this.amounttext.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(123)))), ((int)(((byte)(213)))));
            this.label1.Location = new System.Drawing.Point(336, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(574, 58);
            this.label1.TabIndex = 88;
            this.label1.Text = "PAYMENT SECTION";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(202, 328);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 29);
            this.label2.TabIndex = 89;
            this.label2.Text = "Enter amount";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(21, 238);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(114, 24);
            this.radioButton1.TabIndex = 90;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Credit Card";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(232, 238);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(82, 24);
            this.radioButton2.TabIndex = 91;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "PayPal";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(472, 238);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(110, 24);
            this.radioButton3.TabIndex = 92;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Debit Card";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // paybtn
            // 
            this.paybtn.BackColor = System.Drawing.SystemColors.HotTrack;
            this.paybtn.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paybtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.paybtn.Location = new System.Drawing.Point(710, 257);
            this.paybtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.paybtn.Name = "paybtn";
            this.paybtn.Size = new System.Drawing.Size(264, 100);
            this.paybtn.TabIndex = 93;
            this.paybtn.Text = "Pay";
            this.paybtn.UseVisualStyleBackColor = false;
            this.paybtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // travellerPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.paybtn);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.resultContainer4);
            this.Controls.Add(this.resultContainer3);
            this.Controls.Add(this.resultContainer2);
            this.Controls.Add(this.resultContainer1);
            this.Controls.Add(this.amounttext);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "travellerPayment";
            this.Text = "travellerPayment";
            this.Load += new System.EventHandler(this.travellerPayment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel resultContainer4;
        private System.Windows.Forms.FlowLayoutPanel resultContainer3;
        private System.Windows.Forms.FlowLayoutPanel resultContainer2;
        private System.Windows.Forms.FlowLayoutPanel resultContainer1;
        private System.Windows.Forms.TextBox amounttext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button paybtn;
    }
}