namespace formatted_textbox
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonTestValue = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelBulk = new System.Windows.Forms.Label();
            this.textBoxFormatted = new formatted_textbox.TextBoxFP();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.textBoxNet = new formatted_textbox.TextBoxFP();
            this.textBoxDiscount = new formatted_textbox.TextBoxFP();
            this.textBoxBulk = new formatted_textbox.TextBoxFP();
            this.labelNet = new System.Windows.Forms.Label();
            this.labelDiscount = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonTestValue
            // 
            this.buttonTestValue.Location = new System.Drawing.Point(12, 198);
            this.buttonTestValue.Name = "buttonTestValue";
            this.buttonTestValue.Size = new System.Drawing.Size(158, 34);
            this.buttonTestValue.TabIndex = 2;
            this.buttonTestValue.Text = "Set Value Test (Pi)";
            this.buttonTestValue.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Price 1 ea.";
            // 
            // labelBulk
            // 
            this.labelBulk.AutoSize = true;
            this.labelBulk.Location = new System.Drawing.Point(6, 40);
            this.labelBulk.Name = "labelBulk";
            this.labelBulk.Size = new System.Drawing.Size(80, 25);
            this.labelBulk.TabIndex = 3;
            this.labelBulk.Text = "Bulk 100";
            // 
            // textBoxFormatted
            // 
            this.textBoxFormatted.Format = "N2";
            this.textBoxFormatted.Location = new System.Drawing.Point(116, 36);
            this.textBoxFormatted.Name = "textBoxFormatted";
            this.textBoxFormatted.Size = new System.Drawing.Size(93, 31);
            this.textBoxFormatted.TabIndex = 0;
            this.textBoxFormatted.Text = "0.00";
            this.textBoxFormatted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxFormatted.Value = 0D;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.textBoxNet);
            this.groupBox.Controls.Add(this.textBoxDiscount);
            this.groupBox.Controls.Add(this.textBoxBulk);
            this.groupBox.Controls.Add(this.labelNet);
            this.groupBox.Controls.Add(this.labelDiscount);
            this.groupBox.Controls.Add(this.labelBulk);
            this.groupBox.Location = new System.Drawing.Point(224, 31);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(239, 197);
            this.groupBox.TabIndex = 4;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Bindings";
            // 
            // textBoxNet
            // 
            this.textBoxNet.Format = "N2";
            this.textBoxNet.Location = new System.Drawing.Point(120, 119);
            this.textBoxNet.Name = "textBoxNet";
            this.textBoxNet.ReadOnly = true;
            this.textBoxNet.Size = new System.Drawing.Size(103, 31);
            this.textBoxNet.TabIndex = 0;
            this.textBoxNet.TabStop = false;
            this.textBoxNet.Text = "0.00";
            this.textBoxNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxNet.Value = 0D;
            // 
            // textBoxDiscount
            // 
            this.textBoxDiscount.Format = "N2";
            this.textBoxDiscount.Location = new System.Drawing.Point(120, 78);
            this.textBoxDiscount.Name = "textBoxDiscount";
            this.textBoxDiscount.ReadOnly = true;
            this.textBoxDiscount.Size = new System.Drawing.Size(103, 31);
            this.textBoxDiscount.TabIndex = 0;
            this.textBoxDiscount.TabStop = false;
            this.textBoxDiscount.Text = "0.00";
            this.textBoxDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDiscount.Value = 0D;
            // 
            // textBoxBulk
            // 
            this.textBoxBulk.Format = "N2";
            this.textBoxBulk.Location = new System.Drawing.Point(120, 37);
            this.textBoxBulk.Name = "textBoxBulk";
            this.textBoxBulk.ReadOnly = true;
            this.textBoxBulk.Size = new System.Drawing.Size(103, 31);
            this.textBoxBulk.TabIndex = 0;
            this.textBoxBulk.TabStop = false;
            this.textBoxBulk.Text = "0.00";
            this.textBoxBulk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxBulk.Value = 0D;
            // 
            // labelNet
            // 
            this.labelNet.AutoSize = true;
            this.labelNet.Location = new System.Drawing.Point(6, 122);
            this.labelNet.Name = "labelNet";
            this.labelNet.Size = new System.Drawing.Size(40, 25);
            this.labelNet.TabIndex = 3;
            this.labelNet.Text = "Net";
            // 
            // labelDiscount
            // 
            this.labelDiscount.AutoSize = true;
            this.labelDiscount.Location = new System.Drawing.Point(6, 81);
            this.labelDiscount.Name = "labelDiscount";
            this.labelDiscount.Size = new System.Drawing.Size(82, 25);
            this.labelDiscount.TabIndex = 3;
            this.labelDiscount.Text = "Discount";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 244);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonTestValue);
            this.Controls.Add(this.textBoxFormatted);
            this.Name = "MainForm";
            this.Text = "Test Form";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonTestValue;
        private Label label1;
        private Label labelBulk;
        private TextBoxFP textBoxFormatted;
        private GroupBox groupBox;
        private TextBoxFP textBoxNet;
        private TextBoxFP textBoxDiscount;
        private TextBoxFP textBoxBulk;
        private Label labelNet;
        private Label labelDiscount;
    }
}