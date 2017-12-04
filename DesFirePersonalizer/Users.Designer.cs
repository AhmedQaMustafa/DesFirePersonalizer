namespace DesFirePersonalizer
{
    partial class Savebtn
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
            this.components = new System.ComponentModel.Container();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UserGridView = new System.Windows.Forms.DataGridView();
            this.usrLoginIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usrPasswordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usrFullnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usrDescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usrStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UsrExpireDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appUsersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.oneCard_SolutionDataSet = new DesFirePersonalizer.OneCard_SolutionDataSet();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtUserDescription = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CeckBoxStatus = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtUserName = new System.Windows.Forms.TextBox();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtLoginID = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Deletebtn = new System.Windows.Forms.Button();
            this.Cancelbtn = new System.Windows.Forms.Button();
            this.Editbtn = new System.Windows.Forms.Button();
            this.Insertbtn = new System.Windows.Forms.Button();
            this.appUsersTableAdapter = new DesFirePersonalizer.OneCard_SolutionDataSetTableAdapters.AppUsersTableAdapter();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.cmbSearch = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appUsersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oneCard_SolutionDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(272, 6);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(369, 20);
            this.TxtSearch.TabIndex = 2;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search ";
            // 
            // UserGridView
            // 
            this.UserGridView.AllowUserToAddRows = false;
            this.UserGridView.AllowUserToDeleteRows = false;
            this.UserGridView.AutoGenerateColumns = false;
            this.UserGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usrLoginIDDataGridViewTextBoxColumn,
            this.usrPasswordDataGridViewTextBoxColumn,
            this.usrFullnameDataGridViewTextBoxColumn,
            this.usrDescDataGridViewTextBoxColumn,
            this.usrStatusDataGridViewTextBoxColumn,
            this.UsrExpireDate});
            this.UserGridView.DataSource = this.appUsersBindingSource;
            this.UserGridView.Location = new System.Drawing.Point(56, 35);
            this.UserGridView.Name = "UserGridView";
            this.UserGridView.ReadOnly = true;
            this.UserGridView.Size = new System.Drawing.Size(840, 150);
            this.UserGridView.TabIndex = 0;
            this.UserGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.UserGridView_CellMouseClick);
            // 
            // usrLoginIDDataGridViewTextBoxColumn
            // 
            this.usrLoginIDDataGridViewTextBoxColumn.DataPropertyName = "UsrLoginID";
            this.usrLoginIDDataGridViewTextBoxColumn.HeaderText = "Login ID";
            this.usrLoginIDDataGridViewTextBoxColumn.Name = "usrLoginIDDataGridViewTextBoxColumn";
            this.usrLoginIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.usrLoginIDDataGridViewTextBoxColumn.Width = 300;
            // 
            // usrPasswordDataGridViewTextBoxColumn
            // 
            this.usrPasswordDataGridViewTextBoxColumn.DataPropertyName = "UsrPassword";
            this.usrPasswordDataGridViewTextBoxColumn.HeaderText = "Password";
            this.usrPasswordDataGridViewTextBoxColumn.Name = "usrPasswordDataGridViewTextBoxColumn";
            this.usrPasswordDataGridViewTextBoxColumn.ReadOnly = true;
            this.usrPasswordDataGridViewTextBoxColumn.Visible = false;
            // 
            // usrFullnameDataGridViewTextBoxColumn
            // 
            this.usrFullnameDataGridViewTextBoxColumn.DataPropertyName = "UsrFullname";
            this.usrFullnameDataGridViewTextBoxColumn.HeaderText = "User Name";
            this.usrFullnameDataGridViewTextBoxColumn.Name = "usrFullnameDataGridViewTextBoxColumn";
            this.usrFullnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usrDescDataGridViewTextBoxColumn
            // 
            this.usrDescDataGridViewTextBoxColumn.DataPropertyName = "UsrDesc";
            this.usrDescDataGridViewTextBoxColumn.HeaderText = "Description";
            this.usrDescDataGridViewTextBoxColumn.Name = "usrDescDataGridViewTextBoxColumn";
            this.usrDescDataGridViewTextBoxColumn.ReadOnly = true;
            this.usrDescDataGridViewTextBoxColumn.Width = 300;
            // 
            // usrStatusDataGridViewTextBoxColumn
            // 
            this.usrStatusDataGridViewTextBoxColumn.DataPropertyName = "UsrStatus";
            this.usrStatusDataGridViewTextBoxColumn.HeaderText = "User Status";
            this.usrStatusDataGridViewTextBoxColumn.Name = "usrStatusDataGridViewTextBoxColumn";
            this.usrStatusDataGridViewTextBoxColumn.ReadOnly = true;
            this.usrStatusDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // UsrExpireDate
            // 
            this.UsrExpireDate.DataPropertyName = "UsrExpireDate";
            this.UsrExpireDate.HeaderText = "UsrExpireDate";
            this.UsrExpireDate.Name = "UsrExpireDate";
            this.UsrExpireDate.ReadOnly = true;
            // 
            // appUsersBindingSource
            // 
            this.appUsersBindingSource.DataMember = "AppUsers";
            this.appUsersBindingSource.DataSource = this.oneCard_SolutionDataSet;
            // 
            // oneCard_SolutionDataSet
            // 
            this.oneCard_SolutionDataSet.DataSetName = "OneCard_SolutionDataSet";
            this.oneCard_SolutionDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtUserDescription);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.CeckBoxStatus);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ExpiryDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtUserName);
            this.groupBox1.Controls.Add(this.TxtPassword);
            this.groupBox1.Controls.Add(this.TxtLoginID);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(56, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(561, 190);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // TxtUserDescription
            // 
            this.TxtUserDescription.Location = new System.Drawing.Point(81, 127);
            this.TxtUserDescription.Name = "TxtUserDescription";
            this.TxtUserDescription.Size = new System.Drawing.Size(443, 59);
            this.TxtUserDescription.TabIndex = 5;
            this.TxtUserDescription.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Description :";
            // 
            // CeckBoxStatus
            // 
            this.CeckBoxStatus.AutoSize = true;
            this.CeckBoxStatus.Location = new System.Drawing.Point(399, 48);
            this.CeckBoxStatus.Name = "CeckBoxStatus";
            this.CeckBoxStatus.Size = new System.Drawing.Size(56, 17);
            this.CeckBoxStatus.TabIndex = 6;
            this.CeckBoxStatus.Text = "Active";
            this.CeckBoxStatus.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(356, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Status";
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.Location = new System.Drawing.Point(81, 97);
            this.ExpiryDate.Name = "ExpiryDate";
            this.ExpiryDate.Size = new System.Drawing.Size(212, 20);
            this.ExpiryDate.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Expiry Date : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "User Name :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Password :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Login ID :";
            // 
            // TxtUserName
            // 
            this.TxtUserName.Location = new System.Drawing.Point(81, 72);
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(212, 20);
            this.TxtUserName.TabIndex = 3;
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(81, 46);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(212, 20);
            this.TxtPassword.TabIndex = 2;
            // 
            // TxtLoginID
            // 
            this.TxtLoginID.Location = new System.Drawing.Point(81, 20);
            this.TxtLoginID.Name = "TxtLoginID";
            this.TxtLoginID.Size = new System.Drawing.Size(212, 20);
            this.TxtLoginID.TabIndex = 1;
            this.TxtLoginID.TextChanged += new System.EventHandler(this.TxtLoginID_TextChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.Deletebtn);
            this.groupBox2.Controls.Add(this.Cancelbtn);
            this.groupBox2.Controls.Add(this.Editbtn);
            this.groupBox2.Controls.Add(this.Insertbtn);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(56, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(561, 42);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(256, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Deletebtn
            // 
            this.Deletebtn.Enabled = false;
            this.Deletebtn.Location = new System.Drawing.Point(449, 9);
            this.Deletebtn.Name = "Deletebtn";
            this.Deletebtn.Size = new System.Drawing.Size(75, 23);
            this.Deletebtn.TabIndex = 3;
            this.Deletebtn.Text = "Delete";
            this.Deletebtn.UseVisualStyleBackColor = true;
            this.Deletebtn.Click += new System.EventHandler(this.Deletebtn_Click);
            // 
            // Cancelbtn
            // 
            this.Cancelbtn.Enabled = false;
            this.Cancelbtn.Location = new System.Drawing.Point(175, 9);
            this.Cancelbtn.Name = "Cancelbtn";
            this.Cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.Cancelbtn.TabIndex = 3;
            this.Cancelbtn.Text = "Cancel";
            this.Cancelbtn.UseVisualStyleBackColor = true;
            this.Cancelbtn.Click += new System.EventHandler(this.Cancelbtn_Click);
            // 
            // Editbtn
            // 
            this.Editbtn.Enabled = false;
            this.Editbtn.Location = new System.Drawing.Point(94, 9);
            this.Editbtn.Name = "Editbtn";
            this.Editbtn.Size = new System.Drawing.Size(75, 23);
            this.Editbtn.TabIndex = 2;
            this.Editbtn.Text = "Update";
            this.Editbtn.UseVisualStyleBackColor = true;
            this.Editbtn.Click += new System.EventHandler(this.Editbtn_Click);
            // 
            // Insertbtn
            // 
            this.Insertbtn.Enabled = false;
            this.Insertbtn.Location = new System.Drawing.Point(7, 9);
            this.Insertbtn.Name = "Insertbtn";
            this.Insertbtn.Size = new System.Drawing.Size(75, 23);
            this.Insertbtn.TabIndex = 1;
            this.Insertbtn.Text = "Add";
            this.Insertbtn.UseVisualStyleBackColor = true;
            this.Insertbtn.Click += new System.EventHandler(this.Insertbtn_Click);
            // 
            // appUsersTableAdapter
            // 
            this.appUsersTableAdapter.ClearBeforeFill = true;
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(672, 3);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(207, 23);
            this.SearchBtn.TabIndex = 24;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // cmbSearch
            // 
            this.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearch.FormattingEnabled = true;
            this.cmbSearch.Items.AddRange(new object[] {
            "Login ID",
            "User Name"});
            this.cmbSearch.Location = new System.Drawing.Point(96, 6);
            this.cmbSearch.Name = "cmbSearch";
            this.cmbSearch.Size = new System.Drawing.Size(163, 21);
            this.cmbSearch.TabIndex = 25;
            this.cmbSearch.SelectedIndexChanged += new System.EventHandler(this.cmbSearch_SelectedIndexChanged);
            // 
            // Savebtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 446);
            this.Controls.Add(this.cmbSearch);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.UserGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Savebtn";
            this.Text = "0";
            this.Load += new System.EventHandler(this.Users_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UserGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appUsersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oneCard_SolutionDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView UserGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox TxtUserDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox CeckBoxStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker ExpiryDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtUserName;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.TextBox TxtLoginID;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Deletebtn;
        private System.Windows.Forms.Button Cancelbtn;
        private System.Windows.Forms.Button Editbtn;
        private System.Windows.Forms.Button Insertbtn;
        private OneCard_SolutionDataSet oneCard_SolutionDataSet;
        private System.Windows.Forms.BindingSource appUsersBindingSource;
        private OneCard_SolutionDataSetTableAdapters.AppUsersTableAdapter appUsersTableAdapter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn usrLoginIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usrPasswordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usrFullnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usrDescDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn usrStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsrExpireDate;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.ComboBox cmbSearch;
    }
}