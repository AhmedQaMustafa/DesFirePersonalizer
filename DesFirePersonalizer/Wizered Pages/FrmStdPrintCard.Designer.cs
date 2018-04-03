namespace DesFirePersonalizer.Wizered_Pages
{
    partial class FrmStdPrintCard
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TxtIsDesc = new System.Windows.Forms.RichTextBox();
            this.CmbIsTemptype = new System.Windows.Forms.ComboBox();
            this.CmbIsStatus = new System.Windows.Forms.ComboBox();
            this.CmbIsIssueType = new System.Windows.Forms.ComboBox();
            this.DateTimeISEnd = new System.Windows.Forms.DateTimePicker();
            this.DateTimeISStart = new System.Windows.Forms.DateTimePicker();
            this.TxtIsStudentName = new System.Windows.Forms.TextBox();
            this.TxtIsStudentID = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CardsGridData = new System.Windows.Forms.DataGridView();
            this.TxtNameEdit = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CardsGridData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(613, 468);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 36);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "Skip";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(510, 468);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 36);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Issue Card";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TxtIsDesc);
            this.groupBox3.Controls.Add(this.CmbIsTemptype);
            this.groupBox3.Controls.Add(this.CmbIsStatus);
            this.groupBox3.Controls.Add(this.CmbIsIssueType);
            this.groupBox3.Controls.Add(this.DateTimeISEnd);
            this.groupBox3.Controls.Add(this.DateTimeISStart);
            this.groupBox3.Controls.Add(this.TxtIsStudentName);
            this.groupBox3.Controls.Add(this.TxtIsStudentID);
            this.groupBox3.Controls.Add(this.label53);
            this.groupBox3.Controls.Add(this.label54);
            this.groupBox3.Controls.Add(this.label55);
            this.groupBox3.Controls.Add(this.label56);
            this.groupBox3.Controls.Add(this.label57);
            this.groupBox3.Controls.Add(this.label58);
            this.groupBox3.Controls.Add(this.label59);
            this.groupBox3.Controls.Add(this.label60);
            this.groupBox3.Controls.Add(this.label61);
            this.groupBox3.Location = new System.Drawing.Point(196, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(832, 269);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Card Issue Informations";
            // 
            // TxtIsDesc
            // 
            this.TxtIsDesc.Location = new System.Drawing.Point(179, 168);
            this.TxtIsDesc.Name = "TxtIsDesc";
            this.TxtIsDesc.Size = new System.Drawing.Size(337, 76);
            this.TxtIsDesc.TabIndex = 40;
            this.TxtIsDesc.Text = "";
            // 
            // CmbIsTemptype
            // 
            this.CmbIsTemptype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbIsTemptype.Enabled = false;
            this.CmbIsTemptype.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmbIsTemptype.FormattingEnabled = true;
            this.CmbIsTemptype.Items.AddRange(new object[] {
            "Student"});
            this.CmbIsTemptype.Location = new System.Drawing.Point(538, 131);
            this.CmbIsTemptype.Name = "CmbIsTemptype";
            this.CmbIsTemptype.Size = new System.Drawing.Size(203, 21);
            this.CmbIsTemptype.TabIndex = 39;
            // 
            // CmbIsStatus
            // 
            this.CmbIsStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbIsStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmbIsStatus.FormattingEnabled = true;
            this.CmbIsStatus.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.CmbIsStatus.Location = new System.Drawing.Point(179, 131);
            this.CmbIsStatus.Name = "CmbIsStatus";
            this.CmbIsStatus.Size = new System.Drawing.Size(203, 21);
            this.CmbIsStatus.TabIndex = 38;
            // 
            // CmbIsIssueType
            // 
            this.CmbIsIssueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbIsIssueType.FormattingEnabled = true;
            this.CmbIsIssueType.Location = new System.Drawing.Point(179, 64);
            this.CmbIsIssueType.Name = "CmbIsIssueType";
            this.CmbIsIssueType.Size = new System.Drawing.Size(203, 21);
            this.CmbIsIssueType.TabIndex = 37;
            // 
            // DateTimeISEnd
            // 
            this.DateTimeISEnd.Location = new System.Drawing.Point(538, 99);
            this.DateTimeISEnd.Name = "DateTimeISEnd";
            this.DateTimeISEnd.Size = new System.Drawing.Size(203, 20);
            this.DateTimeISEnd.TabIndex = 36;
            // 
            // DateTimeISStart
            // 
            this.DateTimeISStart.Location = new System.Drawing.Point(179, 99);
            this.DateTimeISStart.Name = "DateTimeISStart";
            this.DateTimeISStart.Size = new System.Drawing.Size(203, 20);
            this.DateTimeISStart.TabIndex = 35;
            // 
            // TxtIsStudentName
            // 
            this.TxtIsStudentName.Enabled = false;
            this.TxtIsStudentName.Location = new System.Drawing.Point(538, 24);
            this.TxtIsStudentName.Name = "TxtIsStudentName";
            this.TxtIsStudentName.Size = new System.Drawing.Size(197, 20);
            this.TxtIsStudentName.TabIndex = 34;
            // 
            // TxtIsStudentID
            // 
            this.TxtIsStudentID.Enabled = false;
            this.TxtIsStudentID.Location = new System.Drawing.Point(179, 28);
            this.TxtIsStudentID.Name = "TxtIsStudentID";
            this.TxtIsStudentID.Size = new System.Drawing.Size(203, 20);
            this.TxtIsStudentID.TabIndex = 33;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(99, 168);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(69, 13);
            this.label53.TabIndex = 32;
            this.label53.Text = "Description : ";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(451, 134);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(57, 13);
            this.label54.TabIndex = 31;
            this.label54.Text = "Template :";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(93, 134);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(75, 13);
            this.label55.TabIndex = 30;
            this.label55.Text = "* Card Status :";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(451, 99);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(65, 13);
            this.label56.TabIndex = 29;
            this.label56.Text = "* End Date :";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(93, 99);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(68, 13);
            this.label57.TabIndex = 28;
            this.label57.Text = "* Start Date :";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(440, 64);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(92, 13);
            this.label58.TabIndex = 27;
            this.label58.Text = "* Issue Condition :";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(99, 64);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(72, 13);
            this.label59.TabIndex = 26;
            this.label59.Text = "* Issue Type :";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(439, 31);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(84, 13);
            this.label60.TabIndex = 25;
            this.label60.Text = "Student Name  :";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(92, 31);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(64, 13);
            this.label61.TabIndex = 24;
            this.label61.Text = "Student ID :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CardsGridData);
            this.groupBox2.Location = new System.Drawing.Point(196, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(828, 177);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "All Cards";
            // 
            // CardsGridData
            // 
            this.CardsGridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CardsGridData.Location = new System.Drawing.Point(102, 18);
            this.CardsGridData.Name = "CardsGridData";
            this.CardsGridData.Size = new System.Drawing.Size(639, 141);
            this.CardsGridData.TabIndex = 4;
            // 
            // TxtNameEdit
            // 
            this.TxtNameEdit.Location = new System.Drawing.Point(84, 8);
            this.TxtNameEdit.Name = "TxtNameEdit";
            this.TxtNameEdit.Size = new System.Drawing.Size(81, 20);
            this.TxtNameEdit.TabIndex = 79;
            this.TxtNameEdit.Visible = false;
            // 
            // FrmStdPrintCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1245, 529);
            this.Controls.Add(this.TxtNameEdit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmStdPrintCard";
            this.Text = "FrmStdPrintCard";
            this.Load += new System.EventHandler(this.FrmStdPrintCard_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CardsGridData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox TxtIsDesc;
        private System.Windows.Forms.ComboBox CmbIsTemptype;
        private System.Windows.Forms.ComboBox CmbIsStatus;
        private System.Windows.Forms.ComboBox CmbIsIssueType;
        private System.Windows.Forms.DateTimePicker DateTimeISEnd;
        private System.Windows.Forms.DateTimePicker DateTimeISStart;
        private System.Windows.Forms.TextBox TxtIsStudentName;
        private System.Windows.Forms.TextBox TxtIsStudentID;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView CardsGridData;
        private System.Windows.Forms.TextBox TxtNameEdit;
    }
}