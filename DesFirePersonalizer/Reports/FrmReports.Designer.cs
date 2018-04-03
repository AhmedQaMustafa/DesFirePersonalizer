namespace DesFirePersonalizer.Reports
{
    partial class FrmReports
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnReadLogPurchase = new System.Windows.Forms.Button();
            this.BtnReadLogTopUp = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.BtnReadLogPurchase);
            this.flowLayoutPanel2.Controls.Add(this.BtnReadLogTopUp);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(234, 12);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(743, 35);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // BtnReadLogPurchase
            // 
            this.BtnReadLogPurchase.Location = new System.Drawing.Point(3, 3);
            this.BtnReadLogPurchase.Name = "BtnReadLogPurchase";
            this.BtnReadLogPurchase.Size = new System.Drawing.Size(356, 23);
            this.BtnReadLogPurchase.TabIndex = 0;
            this.BtnReadLogPurchase.Text = "Read Log Purchase";
            this.BtnReadLogPurchase.UseVisualStyleBackColor = true;
            this.BtnReadLogPurchase.Click += new System.EventHandler(this.BtnReadLogPurchase_Click);
            // 
            // BtnReadLogTopUp
            // 
            this.BtnReadLogTopUp.Location = new System.Drawing.Point(365, 3);
            this.BtnReadLogTopUp.Name = "BtnReadLogTopUp";
            this.BtnReadLogTopUp.Size = new System.Drawing.Size(356, 23);
            this.BtnReadLogTopUp.TabIndex = 1;
            this.BtnReadLogTopUp.Text = "Read Log Top Up";
            this.BtnReadLogTopUp.UseVisualStyleBackColor = true;
            this.BtnReadLogTopUp.Click += new System.EventHandler(this.BtnReadLogTopUp_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(80, 54);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(1059, 474);
            this.textBoxLog.TabIndex = 4;
            // 
            // FrmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 560);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.flowLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReports";
            this.Text = "Form1";
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button BtnReadLogPurchase;
        private System.Windows.Forms.Button BtnReadLogTopUp;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}