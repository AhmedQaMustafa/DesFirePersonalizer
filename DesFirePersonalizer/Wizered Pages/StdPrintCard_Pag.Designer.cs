namespace DesFirePersonalizer.Wizered_Pages
{
    partial class StdPrintCard_Pag
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
            this.pnlPrintCard = new System.Windows.Forms.Panel();
            this.tEmpID = new System.Windows.Forms.TextBox();
            this.tTemplate = new System.Windows.Forms.TextBox();
            this.tcard = new System.Windows.Forms.TextBox();
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.txtEmpID = new System.Windows.Forms.TextBox();
            this.lblEmpID = new System.Windows.Forms.Label();
            this.dvgPrintCard = new System.Windows.Forms.DataGridView();
            this.SelectCard = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TmpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrintCard = new System.Windows.Forms.Button();
            this.btnPreviewCard = new System.Windows.Forms.Button();
            this.pnlPrintCard.SuspendLayout();
            this.grbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgPrintCard)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlPrintCard
            // 
            this.pnlPrintCard.Controls.Add(this.grbSearch);
            this.pnlPrintCard.Controls.Add(this.dvgPrintCard);
            this.pnlPrintCard.Controls.Add(this.btnPrintCard);
            this.pnlPrintCard.Controls.Add(this.btnPreviewCard);
            this.pnlPrintCard.Location = new System.Drawing.Point(161, 42);
            this.pnlPrintCard.Name = "pnlPrintCard";
            this.pnlPrintCard.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pnlPrintCard.Size = new System.Drawing.Size(853, 406);
            this.pnlPrintCard.TabIndex = 37;
            this.pnlPrintCard.Visible = false;
            // 
            // tEmpID
            // 
            this.tEmpID.Location = new System.Drawing.Point(656, 475);
            this.tEmpID.Name = "tEmpID";
            this.tEmpID.Size = new System.Drawing.Size(107, 20);
            this.tEmpID.TabIndex = 15;
            this.tEmpID.Visible = false;
            // 
            // tTemplate
            // 
            this.tTemplate.Location = new System.Drawing.Point(540, 475);
            this.tTemplate.Name = "tTemplate";
            this.tTemplate.Size = new System.Drawing.Size(107, 20);
            this.tTemplate.TabIndex = 12;
            this.tTemplate.Visible = false;
            // 
            // tcard
            // 
            this.tcard.Location = new System.Drawing.Point(416, 475);
            this.tcard.Name = "tcard";
            this.tcard.Size = new System.Drawing.Size(118, 20);
            this.tcard.TabIndex = 11;
            this.tcard.Visible = false;
            // 
            // grbSearch
            // 
            this.grbSearch.Controls.Add(this.txtEmpID);
            this.grbSearch.Controls.Add(this.lblEmpID);
            this.grbSearch.Location = new System.Drawing.Point(12, 13);
            this.grbSearch.Name = "grbSearch";
            this.grbSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grbSearch.Size = new System.Drawing.Size(831, 47);
            this.grbSearch.TabIndex = 10;
            this.grbSearch.TabStop = false;
            this.grbSearch.Text = "Info ";
            // 
            // txtEmpID
            // 
            this.txtEmpID.Location = new System.Drawing.Point(110, 15);
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.Size = new System.Drawing.Size(168, 20);
            this.txtEmpID.TabIndex = 10;
            this.txtEmpID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEmpID
            // 
            this.lblEmpID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEmpID.Location = new System.Drawing.Point(0, 15);
            this.lblEmpID.Name = "lblEmpID";
            this.lblEmpID.Size = new System.Drawing.Size(113, 21);
            this.lblEmpID.TabIndex = 9;
            this.lblEmpID.Text = " :Student No";
            // 
            // dvgPrintCard
            // 
            this.dvgPrintCard.AllowUserToAddRows = false;
            this.dvgPrintCard.AllowUserToDeleteRows = false;
            this.dvgPrintCard.AllowUserToOrderColumns = true;
            this.dvgPrintCard.AllowUserToResizeColumns = false;
            this.dvgPrintCard.AllowUserToResizeRows = false;
            this.dvgPrintCard.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dvgPrintCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgPrintCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectCard,
            this.CardID,
            this.TmpID,
            this.IsID,
            this.LicNo,
            this.EmpID,
            this.EmpName,
            this.EmpType,
            this.IsName});
            this.dvgPrintCard.Location = new System.Drawing.Point(9, 95);
            this.dvgPrintCard.MultiSelect = false;
            this.dvgPrintCard.Name = "dvgPrintCard";
            this.dvgPrintCard.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dvgPrintCard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgPrintCard.Size = new System.Drawing.Size(809, 300);
            this.dvgPrintCard.TabIndex = 4;
            // 
            // SelectCard
            // 
            this.SelectCard.HeaderText = "";
            this.SelectCard.Name = "SelectCard";
            this.SelectCard.Visible = false;
            this.SelectCard.Width = 30;
            // 
            // CardID
            // 
            this.CardID.DataPropertyName = "CardID";
            this.CardID.HeaderText = "رقم البطاقة";
            this.CardID.Name = "CardID";
            this.CardID.ReadOnly = true;
            this.CardID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // TmpID
            // 
            this.TmpID.DataPropertyName = "TmpID";
            this.TmpID.HeaderText = "TmpID";
            this.TmpID.Name = "TmpID";
            this.TmpID.ReadOnly = true;
            this.TmpID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TmpID.Visible = false;
            // 
            // IsID
            // 
            this.IsID.DataPropertyName = "IsID";
            this.IsID.HeaderText = "IsID";
            this.IsID.Name = "IsID";
            this.IsID.ReadOnly = true;
            this.IsID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsID.Visible = false;
            // 
            // LicNo
            // 
            this.LicNo.DataPropertyName = "LicNo";
            this.LicNo.HeaderText = "License ID";
            this.LicNo.Name = "LicNo";
            this.LicNo.Visible = false;
            // 
            // EmpID
            // 
            this.EmpID.DataPropertyName = "EmpID";
            this.EmpID.HeaderText = "رقم الموظف";
            this.EmpID.Name = "EmpID";
            this.EmpID.ReadOnly = true;
            this.EmpID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // EmpName
            // 
            this.EmpName.DataPropertyName = "EmpName";
            this.EmpName.HeaderText = "اسم الموظف";
            this.EmpName.Name = "EmpName";
            this.EmpName.ReadOnly = true;
            this.EmpName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EmpName.Width = 270;
            // 
            // EmpType
            // 
            this.EmpType.DataPropertyName = "EmpType";
            this.EmpType.HeaderText = "النوع";
            this.EmpType.Name = "EmpType";
            // 
            // IsName
            // 
            this.IsName.DataPropertyName = "IsName";
            this.IsName.HeaderText = "الإصدار";
            this.IsName.Name = "IsName";
            this.IsName.ReadOnly = true;
            this.IsName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsName.Width = 150;
            // 
            // btnPrintCard
            // 
            this.btnPrintCard.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPrintCard.ForeColor = System.Drawing.Color.Black;
            this.btnPrintCard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrintCard.Location = new System.Drawing.Point(96, 66);
            this.btnPrintCard.Name = "btnPrintCard";
            this.btnPrintCard.Size = new System.Drawing.Size(75, 23);
            this.btnPrintCard.TabIndex = 3;
            this.btnPrintCard.Text = "Print";
            this.btnPrintCard.UseVisualStyleBackColor = false;
            this.btnPrintCard.Click += new System.EventHandler(this.btnPrintCard_Click);
            // 
            // btnPreviewCard
            // 
            this.btnPreviewCard.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPreviewCard.ForeColor = System.Drawing.Color.Black;
            this.btnPreviewCard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPreviewCard.Location = new System.Drawing.Point(15, 66);
            this.btnPreviewCard.Name = "btnPreviewCard";
            this.btnPreviewCard.Size = new System.Drawing.Size(75, 23);
            this.btnPreviewCard.TabIndex = 2;
            this.btnPreviewCard.Text = "View";
            this.btnPreviewCard.UseVisualStyleBackColor = false;
            this.btnPreviewCard.Click += new System.EventHandler(this.btnPreviewCard_Click);
            // 
            // StdPrintCard_Pag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tEmpID);
            this.Controls.Add(this.pnlPrintCard);
            this.Controls.Add(this.tcard);
            this.Controls.Add(this.tTemplate);
            this.Name = "StdPrintCard_Pag";
            this.Size = new System.Drawing.Size(1210, 547);
            this.pnlPrintCard.ResumeLayout(false);
            this.grbSearch.ResumeLayout(false);
            this.grbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgPrintCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPrintCard;
        private System.Windows.Forms.TextBox tEmpID;
        private System.Windows.Forms.TextBox tTemplate;
        private System.Windows.Forms.TextBox tcard;
        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.TextBox txtEmpID;
        private System.Windows.Forms.Label lblEmpID;
        private System.Windows.Forms.DataGridView dvgPrintCard;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TmpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsName;
        private System.Windows.Forms.Button btnPrintCard;
        private System.Windows.Forms.Button btnPreviewCard;
    }
}
