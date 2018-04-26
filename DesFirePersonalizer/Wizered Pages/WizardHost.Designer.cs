namespace SimpleWizard
{
    partial class WizardHost
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
            this.contentPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnStep6 = new System.Windows.Forms.Button();
            this.BtnStep4 = new System.Windows.Forms.Button();
            this.BtnStep3 = new System.Windows.Forms.Button();
            this.BtnStep2 = new System.Windows.Forms.Button();
            this.BtnStep1 = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentPanel
            // 
            this.tableLayoutPanel.SetColumnSpan(this.contentPanel, 4);
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1257, 516);
            this.contentPanel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.Controls.Add(this.btnFirst, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnPrevious, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.btnNext, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.btnLast, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.contentPanel, 0, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(2, 76);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1257, 578);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(5, 543);
            this.btnFirst.Margin = new System.Windows.Forms.Padding(5);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(304, 30);
            this.btnFirst.TabIndex = 0;
            this.btnFirst.Text = "<< First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(319, 543);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(5);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(304, 30);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.Text = "< Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(633, 543);
            this.btnNext.Margin = new System.Windows.Forms.Padding(5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(304, 30);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(947, 543);
            this.btnLast.Margin = new System.Windows.Forms.Padding(5);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(305, 30);
            this.btnLast.TabIndex = 3;
            this.btnLast.Text = "Last >>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnStep1);
            this.panel1.Controls.Add(this.BtnStep6);
            this.panel1.Controls.Add(this.BtnStep2);
            this.panel1.Controls.Add(this.BtnStep4);
            this.panel1.Controls.Add(this.BtnStep3);
            this.panel1.Location = new System.Drawing.Point(100, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 52);
            this.panel1.TabIndex = 1;
            // 
            // BtnStep6
            // 
            this.BtnStep6.Enabled = false;
            this.BtnStep6.Location = new System.Drawing.Point(871, 3);
            this.BtnStep6.Name = "BtnStep6";
            this.BtnStep6.Size = new System.Drawing.Size(162, 40);
            this.BtnStep6.TabIndex = 5;
            this.BtnStep6.Text = "Last Page";
            this.BtnStep6.UseVisualStyleBackColor = true;
            this.BtnStep6.Click += new System.EventHandler(this.BtnStep6_Click);
            // 
            // BtnStep4
            // 
            this.BtnStep4.Enabled = false;
            this.BtnStep4.Location = new System.Drawing.Point(651, 3);
            this.BtnStep4.Name = "BtnStep4";
            this.BtnStep4.Size = new System.Drawing.Size(162, 40);
            this.BtnStep4.TabIndex = 3;
            this.BtnStep4.Text = "Step  4  -->";
            this.BtnStep4.UseVisualStyleBackColor = true;
            this.BtnStep4.Click += new System.EventHandler(this.BtnStep4_Click);
            // 
            // BtnStep3
            // 
            this.BtnStep3.Enabled = false;
            this.BtnStep3.Location = new System.Drawing.Point(445, 3);
            this.BtnStep3.Name = "BtnStep3";
            this.BtnStep3.Size = new System.Drawing.Size(162, 40);
            this.BtnStep3.TabIndex = 2;
            this.BtnStep3.Text = "Step  3  -->";
            this.BtnStep3.UseVisualStyleBackColor = true;
            this.BtnStep3.Click += new System.EventHandler(this.BtnStep3_Click);
            // 
            // BtnStep2
            // 
            this.BtnStep2.Enabled = false;
            this.BtnStep2.Location = new System.Drawing.Point(221, 3);
            this.BtnStep2.Name = "BtnStep2";
            this.BtnStep2.Size = new System.Drawing.Size(162, 40);
            this.BtnStep2.TabIndex = 1;
            this.BtnStep2.Text = "Step  2  -->";
            this.BtnStep2.UseVisualStyleBackColor = true;
            this.BtnStep2.Click += new System.EventHandler(this.BtnStep2_Click);
            // 
            // BtnStep1
            // 
            this.BtnStep1.Enabled = false;
            this.BtnStep1.Location = new System.Drawing.Point(15, 3);
            this.BtnStep1.Name = "BtnStep1";
            this.BtnStep1.Size = new System.Drawing.Size(162, 40);
            this.BtnStep1.TabIndex = 0;
            this.BtnStep1.Text = "Step  1  --->";
            this.BtnStep1.UseVisualStyleBackColor = true;
            this.BtnStep1.Click += new System.EventHandler(this.BtnStep1_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnClose.Location = new System.Drawing.Point(1244, 12);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(46, 38);
            this.BtnClose.TabIndex = 7;
            this.BtnClose.Text = "Close X";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // WizardHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 675);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WizardHost";
            this.Text = "Wizard";
            this.Load += new System.EventHandler(this.WizardHost_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnStep6;
        private System.Windows.Forms.Button BtnStep4;
        private System.Windows.Forms.Button BtnStep3;
        private System.Windows.Forms.Button BtnStep2;
        private System.Windows.Forms.Button BtnStep1;
        private System.Windows.Forms.Button BtnClose;
    }
}