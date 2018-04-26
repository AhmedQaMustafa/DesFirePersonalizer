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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StdPrintCard_Pag));
            this.tEmpID = new System.Windows.Forms.TextBox();
            this.tTemplate = new System.Windows.Forms.TextBox();
            this.tcard = new System.Windows.Forms.TextBox();
            this.pnlPrintCard = new System.Windows.Forms.Panel();
            this.btnDeleteCrd = new System.Windows.Forms.Button();
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.btnCancelCard = new System.Windows.Forms.Button();
            this.btnSearchCard = new System.Windows.Forms.Button();
            this.txtStudentPrintID = new System.Windows.Forms.TextBox();
            this.lblEmpID = new System.Windows.Forms.Label();
            this.dvgPrintCard = new System.Windows.Forms.DataGridView();
            this.btnPrintCard = new System.Windows.Forms.Button();
            this.btnPreviewCard = new System.Windows.Forms.Button();
            this.SelectCard = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StudentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STDFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STDSecondName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STDFamilyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TmpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlPrintCard.SuspendLayout();
            this.grbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgPrintCard)).BeginInit();
            this.SuspendLayout();
            // 
            // tEmpID
            // 
            resources.ApplyResources(this.tEmpID, "tEmpID");
            this.tEmpID.Name = "tEmpID";
            // 
            // tTemplate
            // 
            resources.ApplyResources(this.tTemplate, "tTemplate");
            this.tTemplate.Name = "tTemplate";
            // 
            // tcard
            // 
            resources.ApplyResources(this.tcard, "tcard");
            this.tcard.Name = "tcard";
            // 
            // pnlPrintCard
            // 
            resources.ApplyResources(this.pnlPrintCard, "pnlPrintCard");
            this.pnlPrintCard.Controls.Add(this.btnDeleteCrd);
            this.pnlPrintCard.Controls.Add(this.tEmpID);
            this.pnlPrintCard.Controls.Add(this.grbSearch);
            this.pnlPrintCard.Controls.Add(this.tTemplate);
            this.pnlPrintCard.Controls.Add(this.tcard);
            this.pnlPrintCard.Controls.Add(this.dvgPrintCard);
            this.pnlPrintCard.Controls.Add(this.btnPrintCard);
            this.pnlPrintCard.Controls.Add(this.btnPreviewCard);
            this.pnlPrintCard.Name = "pnlPrintCard";
            // 
            // btnDeleteCrd
            // 
            resources.ApplyResources(this.btnDeleteCrd, "btnDeleteCrd");
            this.btnDeleteCrd.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDeleteCrd.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteCrd.Name = "btnDeleteCrd";
            this.btnDeleteCrd.UseVisualStyleBackColor = false;
            // 
            // grbSearch
            // 
            resources.ApplyResources(this.grbSearch, "grbSearch");
            this.grbSearch.Controls.Add(this.btnCancelCard);
            this.grbSearch.Controls.Add(this.btnSearchCard);
            this.grbSearch.Controls.Add(this.txtStudentPrintID);
            this.grbSearch.Controls.Add(this.lblEmpID);
            this.grbSearch.Name = "grbSearch";
            this.grbSearch.TabStop = false;
            // 
            // btnCancelCard
            // 
            resources.ApplyResources(this.btnCancelCard, "btnCancelCard");
            this.btnCancelCard.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancelCard.Name = "btnCancelCard";
            this.btnCancelCard.UseVisualStyleBackColor = false;
            // 
            // btnSearchCard
            // 
            resources.ApplyResources(this.btnSearchCard, "btnSearchCard");
            this.btnSearchCard.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearchCard.Name = "btnSearchCard";
            this.btnSearchCard.UseVisualStyleBackColor = false;
            // 
            // txtStudentPrintID
            // 
            resources.ApplyResources(this.txtStudentPrintID, "txtStudentPrintID");
            this.txtStudentPrintID.Name = "txtStudentPrintID";
            // 
            // lblEmpID
            // 
            resources.ApplyResources(this.lblEmpID, "lblEmpID");
            this.lblEmpID.Name = "lblEmpID";
            // 
            // dvgPrintCard
            // 
            resources.ApplyResources(this.dvgPrintCard, "dvgPrintCard");
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
            this.StudentID,
            this.STDFirstName,
            this.STDSecondName,
            this.STDFamilyName,
            this.TmpID,
            this.IsID,
            this.EmpType,
            this.IsName});
            this.dvgPrintCard.MultiSelect = false;
            this.dvgPrintCard.Name = "dvgPrintCard";
            this.dvgPrintCard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btnPrintCard
            // 
            resources.ApplyResources(this.btnPrintCard, "btnPrintCard");
            this.btnPrintCard.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPrintCard.ForeColor = System.Drawing.Color.Black;
            this.btnPrintCard.Name = "btnPrintCard";
            this.btnPrintCard.UseVisualStyleBackColor = false;
            // 
            // btnPreviewCard
            // 
            resources.ApplyResources(this.btnPreviewCard, "btnPreviewCard");
            this.btnPreviewCard.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPreviewCard.ForeColor = System.Drawing.Color.Black;
            this.btnPreviewCard.Name = "btnPreviewCard";
            this.btnPreviewCard.UseVisualStyleBackColor = false;
            // 
            // SelectCard
            // 
            resources.ApplyResources(this.SelectCard, "SelectCard");
            this.SelectCard.Name = "SelectCard";
            // 
            // CardID
            // 
            this.CardID.DataPropertyName = "CardID";
            resources.ApplyResources(this.CardID, "CardID");
            this.CardID.Name = "CardID";
            this.CardID.ReadOnly = true;
            this.CardID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // StudentID
            // 
            this.StudentID.DataPropertyName = "StudentID";
            resources.ApplyResources(this.StudentID, "StudentID");
            this.StudentID.Name = "StudentID";
            this.StudentID.ReadOnly = true;
            this.StudentID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // STDFirstName
            // 
            this.STDFirstName.DataPropertyName = "STDFirstName";
            resources.ApplyResources(this.STDFirstName, "STDFirstName");
            this.STDFirstName.Name = "STDFirstName";
            this.STDFirstName.ReadOnly = true;
            this.STDFirstName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // STDSecondName
            // 
            this.STDSecondName.DataPropertyName = "STDSecondName";
            resources.ApplyResources(this.STDSecondName, "STDSecondName");
            this.STDSecondName.Name = "STDSecondName";
            // 
            // STDFamilyName
            // 
            this.STDFamilyName.DataPropertyName = "STDFamilyName";
            resources.ApplyResources(this.STDFamilyName, "STDFamilyName");
            this.STDFamilyName.Name = "STDFamilyName";
            // 
            // TmpID
            // 
            this.TmpID.DataPropertyName = "TmpID";
            resources.ApplyResources(this.TmpID, "TmpID");
            this.TmpID.Name = "TmpID";
            this.TmpID.ReadOnly = true;
            this.TmpID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // IsID
            // 
            this.IsID.DataPropertyName = "IsID";
            resources.ApplyResources(this.IsID, "IsID");
            this.IsID.Name = "IsID";
            this.IsID.ReadOnly = true;
            this.IsID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // EmpType
            // 
            this.EmpType.DataPropertyName = "TmpType";
            resources.ApplyResources(this.EmpType, "EmpType");
            this.EmpType.Name = "EmpType";
            // 
            // IsName
            // 
            this.IsName.DataPropertyName = "IsNameEn";
            resources.ApplyResources(this.IsName, "IsName");
            this.IsName.Name = "IsName";
            this.IsName.ReadOnly = true;
            this.IsName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // StdPrintCard_Pag
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPrintCard);
            this.Name = "StdPrintCard_Pag";
            this.pnlPrintCard.ResumeLayout(false);
            this.pnlPrintCard.PerformLayout();
            this.grbSearch.ResumeLayout(false);
            this.grbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgPrintCard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tEmpID;
        private System.Windows.Forms.TextBox tTemplate;
        private System.Windows.Forms.TextBox tcard;
        private System.Windows.Forms.Panel pnlPrintCard;
        private System.Windows.Forms.Button btnDeleteCrd;
        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.Button btnCancelCard;
        private System.Windows.Forms.Button btnSearchCard;
        private System.Windows.Forms.TextBox txtStudentPrintID;
        private System.Windows.Forms.Label lblEmpID;
        private System.Windows.Forms.DataGridView dvgPrintCard;
        private System.Windows.Forms.Button btnPrintCard;
        private System.Windows.Forms.Button btnPreviewCard;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StudentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn STDFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn STDSecondName;
        private System.Windows.Forms.DataGridViewTextBoxColumn STDFamilyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TmpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsName;
    }
}
