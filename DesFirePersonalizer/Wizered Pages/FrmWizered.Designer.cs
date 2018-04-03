namespace DesFirePersonalizer.Wizered_Pages
{
    partial class FrmWizered
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlbuttons = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblSteps = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlLeft.SuspendLayout();
            this.pnlbuttons.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlLeft.Controls.Add(this.LblSteps);
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(174, 583);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlbuttons
            // 
            this.pnlbuttons.Controls.Add(this.button1);
            this.pnlbuttons.Controls.Add(this.btnBack);
            this.pnlbuttons.Controls.Add(this.btnNext);
            this.pnlbuttons.Controls.Add(this.btnFinish);
            this.pnlbuttons.Location = new System.Drawing.Point(173, 587);
            this.pnlbuttons.Name = "pnlbuttons";
            this.pnlbuttons.Size = new System.Drawing.Size(1193, 77);
            this.pnlbuttons.TabIndex = 1;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(380, 11);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(204, 52);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(590, 11);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(204, 52);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(800, 11);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(178, 52);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlContent.Location = new System.Drawing.Point(173, 59);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1193, 527);
            this.pnlContent.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(173, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1193, 57);
            this.panel4.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // LblSteps
            // 
            this.LblSteps.AutoSize = true;
            this.LblSteps.Location = new System.Drawing.Point(4, 13);
            this.LblSteps.Name = "LblSteps";
            this.LblSteps.Size = new System.Drawing.Size(35, 13);
            this.LblSteps.TabIndex = 0;
            this.LblSteps.Text = "label3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 52);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FrmWizered
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 676);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlbuttons);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmWizered";
            this.Text = "Add Students";
            this.Load += new System.EventHandler(this.FrmWizered_Load);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlbuttons.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlbuttons;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LblSteps;
        private System.Windows.Forms.Button button1;
    }
}