namespace DesFirePersonalizer.Configurations_Pages
{
    partial class TerminalSetup
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
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.RdnBtnTransID = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.TerminalIDEPurse = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.TransactionIDEPurse = new System.Windows.Forms.TextBox();
            this.ApplicationDataEPurse = new System.Windows.Forms.TextBox();
            this.RdmBtnAppData = new System.Windows.Forms.Button();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.RdnBtnTransID, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.label16, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.TerminalIDEPurse, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label17, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.TransactionIDEPurse, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.ApplicationDataEPurse, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.RdmBtnAppData, 2, 2);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(367, 84);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // RdnBtnTransID
            // 
            this.RdnBtnTransID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RdnBtnTransID.Location = new System.Drawing.Point(286, 29);
            this.RdnBtnTransID.Name = "RdnBtnTransID";
            this.RdnBtnTransID.Size = new System.Drawing.Size(75, 23);
            this.RdnBtnTransID.TabIndex = 2;
            this.RdnBtnTransID.Text = "Randomize";
            this.RdnBtnTransID.UseVisualStyleBackColor = true;
            this.RdnBtnTransID.Click += new System.EventHandler(this.RdnBtnTransID_Click);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 34);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Transaction ID";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "SAM/Terminal ID";
            // 
            // TerminalIDEPurse
            // 
            this.TerminalIDEPurse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TerminalIDEPurse.Location = new System.Drawing.Point(98, 3);
            this.TerminalIDEPurse.MaxLength = 16;
            this.TerminalIDEPurse.Name = "TerminalIDEPurse";
            this.TerminalIDEPurse.ReadOnly = true;
            this.TerminalIDEPurse.Size = new System.Drawing.Size(182, 20);
            this.TerminalIDEPurse.TabIndex = 1;
            this.TerminalIDEPurse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(85, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Application Data";
            // 
            // TransactionIDEPurse
            // 
            this.TransactionIDEPurse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TransactionIDEPurse.Location = new System.Drawing.Point(98, 30);
            this.TransactionIDEPurse.MaxLength = 8;
            this.TransactionIDEPurse.Name = "TransactionIDEPurse";
            this.TransactionIDEPurse.Size = new System.Drawing.Size(182, 20);
            this.TransactionIDEPurse.TabIndex = 1;
            this.TransactionIDEPurse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ApplicationDataEPurse
            // 
            this.ApplicationDataEPurse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ApplicationDataEPurse.Location = new System.Drawing.Point(98, 59);
            this.ApplicationDataEPurse.MaxLength = 24;
            this.ApplicationDataEPurse.Name = "ApplicationDataEPurse";
            this.ApplicationDataEPurse.Size = new System.Drawing.Size(182, 20);
            this.ApplicationDataEPurse.TabIndex = 1;
            this.ApplicationDataEPurse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // RdmBtnAppData
            // 
            this.RdmBtnAppData.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RdmBtnAppData.Location = new System.Drawing.Point(286, 58);
            this.RdmBtnAppData.Name = "RdmBtnAppData";
            this.RdmBtnAppData.Size = new System.Drawing.Size(75, 23);
            this.RdmBtnAppData.TabIndex = 5;
            this.RdmBtnAppData.Text = "Randomize";
            this.RdmBtnAppData.UseVisualStyleBackColor = true;
            this.RdmBtnAppData.Click += new System.EventHandler(this.RdmBtnAppData_Click);
            // 
            // TerminalSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 108);
            this.Controls.Add(this.tableLayoutPanel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TerminalSetup";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.TerminalSetup_Load);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button RdnBtnTransID;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TerminalIDEPurse;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox TransactionIDEPurse;
        private System.Windows.Forms.TextBox ApplicationDataEPurse;
        private System.Windows.Forms.Button RdmBtnAppData;
    }
}