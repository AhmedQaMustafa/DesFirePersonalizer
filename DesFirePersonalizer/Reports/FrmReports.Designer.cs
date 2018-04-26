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
            this.TxtStdInc = new System.Windows.Forms.TextBox();
            this.CardReaderComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.RdnBtnTransID = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.TerminalIDEPurse = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.RdmBtnAppData = new System.Windows.Forms.Button();
            this.mainStatus = new System.Windows.Forms.StatusStrip();
            this.cardReaderToolStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.SmartCardStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripAtrLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.uidStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sizeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TransactionIDEPurse = new System.Windows.Forms.TextBox();
            this.ApplicationDataEPurse = new System.Windows.Forms.TextBox();
            this.ReportGridView = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.mainStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportGridView)).BeginInit();
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
            // TxtStdInc
            // 
            this.TxtStdInc.Location = new System.Drawing.Point(1002, 18);
            this.TxtStdInc.Name = "TxtStdInc";
            this.TxtStdInc.Size = new System.Drawing.Size(231, 20);
            this.TxtStdInc.TabIndex = 24;
            this.TxtStdInc.Visible = false;
            // 
            // CardReaderComboBox
            // 
            this.CardReaderComboBox.FormattingEnabled = true;
            this.CardReaderComboBox.Location = new System.Drawing.Point(457, 247);
            this.CardReaderComboBox.MinimumSize = new System.Drawing.Size(250, 0);
            this.CardReaderComboBox.Name = "CardReaderComboBox";
            this.CardReaderComboBox.Size = new System.Drawing.Size(369, 21);
            this.CardReaderComboBox.TabIndex = 26;
            this.CardReaderComboBox.Visible = false;
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
            this.tableLayoutPanel6.Controls.Add(this.textBox1, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.textBox2, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.RdmBtnAppData, 2, 2);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(457, 274);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(369, 90);
            this.tableLayoutPanel6.TabIndex = 27;
            this.tableLayoutPanel6.Visible = false;
            // 
            // RdnBtnTransID
            // 
            this.RdnBtnTransID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RdnBtnTransID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RdnBtnTransID.Location = new System.Drawing.Point(286, 29);
            this.RdnBtnTransID.Name = "RdnBtnTransID";
            this.RdnBtnTransID.Size = new System.Drawing.Size(75, 23);
            this.RdnBtnTransID.TabIndex = 2;
            this.RdnBtnTransID.Text = "Randomize";
            this.RdnBtnTransID.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
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
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
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
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(3, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(85, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Application Data";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox1.Location = new System.Drawing.Point(98, 30);
            this.textBox1.MaxLength = 8;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox2.Location = new System.Drawing.Point(98, 62);
            this.textBox2.MaxLength = 24;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(182, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // RdmBtnAppData
            // 
            this.RdmBtnAppData.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RdmBtnAppData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RdmBtnAppData.Location = new System.Drawing.Point(286, 61);
            this.RdmBtnAppData.Name = "RdmBtnAppData";
            this.RdmBtnAppData.Size = new System.Drawing.Size(75, 23);
            this.RdmBtnAppData.TabIndex = 5;
            this.RdmBtnAppData.Text = "Randomize";
            this.RdmBtnAppData.UseVisualStyleBackColor = true;
            // 
            // mainStatus
            // 
            this.mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cardReaderToolStrip,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel2,
            this.SmartCardStatusLabel,
            this.toolStripStatusLabel1,
            this.toolStripAtrLabel,
            this.toolStripStatusLabel3,
            this.uidStatusLabel,
            this.toolStripStatusLabel5,
            this.sizeStatusLabel,
            this.toolStripStatusLabel4});
            this.mainStatus.Location = new System.Drawing.Point(0, 538);
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.Size = new System.Drawing.Size(1269, 22);
            this.mainStatus.TabIndex = 28;
            this.mainStatus.Text = "statusStrip1";
            this.mainStatus.Visible = false;
            // 
            // cardReaderToolStrip
            // 
            this.cardReaderToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.cardReaderToolStrip.Name = "cardReaderToolStrip";
            this.cardReaderToolStrip.Size = new System.Drawing.Size(70, 17);
            this.cardReaderToolStrip.Text = "Card Reader";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel6.Text = "|";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel2.Text = "Status:";
            // 
            // SmartCardStatusLabel
            // 
            this.SmartCardStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.SmartCardStatusLabel.Name = "SmartCardStatusLabel";
            this.SmartCardStatusLabel.Size = new System.Drawing.Size(26, 17);
            this.SmartCardStatusLabel.Text = "N/A";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel1.Text = "ATR:";
            // 
            // toolStripAtrLabel
            // 
            this.toolStripAtrLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripAtrLabel.Name = "toolStripAtrLabel";
            this.toolStripAtrLabel.Size = new System.Drawing.Size(26, 17);
            this.toolStripAtrLabel.Text = "N/A";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel3.Text = "UID:";
            // 
            // uidStatusLabel
            // 
            this.uidStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.uidStatusLabel.Name = "uidStatusLabel";
            this.uidStatusLabel.Size = new System.Drawing.Size(26, 17);
            this.uidStatusLabel.Text = "N/A";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(30, 17);
            this.toolStripStatusLabel5.Text = "Size:";
            // 
            // sizeStatusLabel
            // 
            this.sizeStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.sizeStatusLabel.Name = "sizeStatusLabel";
            this.sizeStatusLabel.Size = new System.Drawing.Size(26, 17);
            this.sizeStatusLabel.Text = "N/A";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabel4.Text = "Bytes";
            // 
            // TransactionIDEPurse
            // 
            this.TransactionIDEPurse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TransactionIDEPurse.Location = new System.Drawing.Point(457, 370);
            this.TransactionIDEPurse.MaxLength = 8;
            this.TransactionIDEPurse.Name = "TransactionIDEPurse";
            this.TransactionIDEPurse.Size = new System.Drawing.Size(182, 20);
            this.TransactionIDEPurse.TabIndex = 29;
            this.TransactionIDEPurse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TransactionIDEPurse.Visible = false;
            // 
            // ApplicationDataEPurse
            // 
            this.ApplicationDataEPurse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ApplicationDataEPurse.Location = new System.Drawing.Point(651, 370);
            this.ApplicationDataEPurse.MaxLength = 24;
            this.ApplicationDataEPurse.Name = "ApplicationDataEPurse";
            this.ApplicationDataEPurse.Size = new System.Drawing.Size(175, 20);
            this.ApplicationDataEPurse.TabIndex = 30;
            this.ApplicationDataEPurse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ApplicationDataEPurse.Visible = false;
            // 
            // ReportGridView
            // 
            this.ReportGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReportGridView.Location = new System.Drawing.Point(80, 53);
            this.ReportGridView.Name = "ReportGridView";
            this.ReportGridView.Size = new System.Drawing.Size(1128, 430);
            this.ReportGridView.TabIndex = 31;
            // 
            // FrmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 560);
            this.Controls.Add(this.ReportGridView);
            this.Controls.Add(this.mainStatus);
            this.Controls.Add(this.TxtStdInc);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.TransactionIDEPurse);
            this.Controls.Add(this.ApplicationDataEPurse);
            this.Controls.Add(this.CardReaderComboBox);
            this.Controls.Add(this.tableLayoutPanel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReports";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmReports_Load);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.mainStatus.ResumeLayout(false);
            this.mainStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button BtnReadLogPurchase;
        private System.Windows.Forms.Button BtnReadLogTopUp;
        private System.Windows.Forms.TextBox TxtStdInc;
        private System.Windows.Forms.ComboBox CardReaderComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button RdnBtnTransID;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TerminalIDEPurse;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button RdmBtnAppData;
        private System.Windows.Forms.StatusStrip mainStatus;
        private System.Windows.Forms.ToolStripStatusLabel cardReaderToolStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel SmartCardStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripAtrLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel uidStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel sizeStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.TextBox TransactionIDEPurse;
        private System.Windows.Forms.TextBox ApplicationDataEPurse;
        private System.Windows.Forms.DataGridView ReportGridView;
    }
}