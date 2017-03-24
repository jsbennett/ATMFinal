namespace ATMSimulator
{
    partial class Receipt
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
            this.atmNameLabel = new System.Windows.Forms.Label();
            this.receiptDisplay = new System.Windows.Forms.ListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // atmNameLabel
            // 
            this.atmNameLabel.AutoSize = true;
            this.atmNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.atmNameLabel.Location = new System.Drawing.Point(24, 9);
            this.atmNameLabel.Name = "atmNameLabel";
            this.atmNameLabel.Size = new System.Drawing.Size(509, 55);
            this.atmNameLabel.TabIndex = 0;
            this.atmNameLabel.Text = "ATM Siumulator Bank";
            // 
            // receiptDisplay
            // 
            this.receiptDisplay.BackColor = System.Drawing.Color.White;
            this.receiptDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.receiptDisplay.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiptDisplay.FormattingEnabled = true;
            this.receiptDisplay.ItemHeight = 21;
            this.receiptDisplay.Location = new System.Drawing.Point(23, 76);
            this.receiptDisplay.Name = "receiptDisplay";
            this.receiptDisplay.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.receiptDisplay.Size = new System.Drawing.Size(566, 336);
            this.receiptDisplay.TabIndex = 1;
            this.receiptDisplay.SelectedIndexChanged += new System.EventHandler(this.receiptDisplay_SelectedIndexChanged);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.White;
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(493, 599);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(97, 56);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "close";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ATMSimulator.Properties.Resources.IMG_27591;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(23, 433);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(565, 164);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // Receipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(602, 667);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.receiptDisplay);
            this.Controls.Add(this.atmNameLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Receipt";
            this.Text = "Receipt";
            this.Load += new System.EventHandler(this.Receipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label atmNameLabel;
        private System.Windows.Forms.ListBox receiptDisplay;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}