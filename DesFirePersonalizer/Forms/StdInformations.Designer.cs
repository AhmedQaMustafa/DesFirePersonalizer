namespace DesFirePersonalizer
{
    partial class StdInformations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StdInformations));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSearch = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.StdGridView = new System.Windows.Forms.DataGridView();
            this.PicBoxStd = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.LblStdName = new System.Windows.Forms.Label();
            this.LblStdNat = new System.Windows.Forms.Label();
            this.LblStdEmail = new System.Windows.Forms.Label();
            this.LblStdTemType = new System.Windows.Forms.Label();
            this.LblStdStatus = new System.Windows.Forms.Label();
            this.LblStdMobi = new System.Windows.Forms.Label();
            this.LblStdGen = new System.Windows.Forms.Label();
            this.LblStdNationality = new System.Windows.Forms.Label();
            this.LblStdExpiryDat = new System.Windows.Forms.Label();
            this.LblStdPassIssuePlace = new System.Windows.Forms.Label();
            this.LblStdPassportID = new System.Windows.Forms.Label();
            this.LblStdCol = new System.Windows.Forms.Label();
            this.LblStdBloTy = new System.Windows.Forms.Label();
            this.LblStdDesc = new System.Windows.Forms.Label();
            this.LblStdDaBth = new System.Windows.Forms.Label();
            this.LblStdID = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkboxStatus = new System.Windows.Forms.CheckBox();
            this.LblPlaceOfbirth = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StdGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxStd)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(9, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(946, 30);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(13, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Managments";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbSearch);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnExport);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.TxtSearch);
            this.panel2.Location = new System.Drawing.Point(9, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(944, 44);
            this.panel2.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(823, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 37);
            this.btnRefresh.TabIndex = 28;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Search By";
            // 
            // cmbSearch
            // 
            this.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearch.FormattingEnabled = true;
            this.cmbSearch.Items.AddRange(new object[] {
            "All",
            "Login ID",
            "Login Name"});
            this.cmbSearch.Location = new System.Drawing.Point(65, 11);
            this.cmbSearch.Name = "cmbSearch";
            this.cmbSearch.Size = new System.Drawing.Size(118, 21);
            this.cmbSearch.TabIndex = 27;
            this.cmbSearch.SelectedIndexChanged += new System.EventHandler(this.cmbSearch_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::DesFirePersonalizer.Properties.Resources.Document_Add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(499, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 37);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(742, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 37);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(403, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 36);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(661, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 37);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(580, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 37);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Edit";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // TxtSearch
            // 
            this.TxtSearch.Enabled = false;
            this.TxtSearch.Location = new System.Drawing.Point(189, 11);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(208, 20);
            this.TxtSearch.TabIndex = 4;
            // 
            // StdGridView
            // 
            this.StdGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StdGridView.Location = new System.Drawing.Point(9, 93);
            this.StdGridView.Name = "StdGridView";
            this.StdGridView.Size = new System.Drawing.Size(941, 199);
            this.StdGridView.TabIndex = 3;
            this.StdGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.StdGridView_CellMouseClick);
            // 
            // PicBoxStd
            // 
            this.PicBoxStd.Image = ((System.Drawing.Image)(resources.GetObject("PicBoxStd.Image")));
            this.PicBoxStd.Location = new System.Drawing.Point(9, 366);
            this.PicBoxStd.Name = "PicBoxStd";
            this.PicBoxStd.Size = new System.Drawing.Size(205, 227);
            this.PicBoxStd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBoxStd.TabIndex = 4;
            this.PicBoxStd.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(216, 365);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(739, 228);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.LblPlaceOfbirth);
            this.tabPage2.Controls.Add(this.LblStdName);
            this.tabPage2.Controls.Add(this.LblStdNat);
            this.tabPage2.Controls.Add(this.LblStdEmail);
            this.tabPage2.Controls.Add(this.LblStdTemType);
            this.tabPage2.Controls.Add(this.LblStdStatus);
            this.tabPage2.Controls.Add(this.LblStdMobi);
            this.tabPage2.Controls.Add(this.LblStdGen);
            this.tabPage2.Controls.Add(this.LblStdNationality);
            this.tabPage2.Controls.Add(this.LblStdExpiryDat);
            this.tabPage2.Controls.Add(this.LblStdPassIssuePlace);
            this.tabPage2.Controls.Add(this.LblStdPassportID);
            this.tabPage2.Controls.Add(this.LblStdCol);
            this.tabPage2.Controls.Add(this.LblStdBloTy);
            this.tabPage2.Controls.Add(this.LblStdDesc);
            this.tabPage2.Controls.Add(this.LblStdDaBth);
            this.tabPage2.Controls.Add(this.LblStdID);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(731, 202);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Student Information";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // LblStdName
            // 
            this.LblStdName.AutoSize = true;
            this.LblStdName.Enabled = false;
            this.LblStdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdName.Location = new System.Drawing.Point(8, 38);
            this.LblStdName.Name = "LblStdName";
            this.LblStdName.Size = new System.Drawing.Size(87, 13);
            this.LblStdName.TabIndex = 70;
            this.LblStdName.Text = "Student Name";
            // 
            // LblStdNat
            // 
            this.LblStdNat.AutoSize = true;
            this.LblStdNat.Enabled = false;
            this.LblStdNat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdNat.Location = new System.Drawing.Point(6, 62);
            this.LblStdNat.Name = "LblStdNat";
            this.LblStdNat.Size = new System.Drawing.Size(71, 13);
            this.LblStdNat.TabIndex = 69;
            this.LblStdNat.Text = "National ID";
            // 
            // LblStdEmail
            // 
            this.LblStdEmail.AutoSize = true;
            this.LblStdEmail.Enabled = false;
            this.LblStdEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdEmail.Location = new System.Drawing.Point(373, 62);
            this.LblStdEmail.Name = "LblStdEmail";
            this.LblStdEmail.Size = new System.Drawing.Size(41, 13);
            this.LblStdEmail.TabIndex = 68;
            this.LblStdEmail.Text = "E-mail";
            // 
            // LblStdTemType
            // 
            this.LblStdTemType.AutoSize = true;
            this.LblStdTemType.Enabled = false;
            this.LblStdTemType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdTemType.Location = new System.Drawing.Point(264, 182);
            this.LblStdTemType.Name = "LblStdTemType";
            this.LblStdTemType.Size = new System.Drawing.Size(91, 13);
            this.LblStdTemType.TabIndex = 67;
            this.LblStdTemType.Text = "Template Type";
            // 
            // LblStdStatus
            // 
            this.LblStdStatus.AutoSize = true;
            this.LblStdStatus.Enabled = false;
            this.LblStdStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdStatus.Location = new System.Drawing.Point(108, 182);
            this.LblStdStatus.Name = "LblStdStatus";
            this.LblStdStatus.Size = new System.Drawing.Size(91, 13);
            this.LblStdStatus.TabIndex = 66;
            this.LblStdStatus.Text = "Student Status";
            // 
            // LblStdMobi
            // 
            this.LblStdMobi.AutoSize = true;
            this.LblStdMobi.Enabled = false;
            this.LblStdMobi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdMobi.Location = new System.Drawing.Point(373, 15);
            this.LblStdMobi.Name = "LblStdMobi";
            this.LblStdMobi.Size = new System.Drawing.Size(68, 13);
            this.LblStdMobi.TabIndex = 65;
            this.LblStdMobi.Text = "Mobile No.";
            // 
            // LblStdGen
            // 
            this.LblStdGen.AutoSize = true;
            this.LblStdGen.Enabled = false;
            this.LblStdGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdGen.Location = new System.Drawing.Point(9, 150);
            this.LblStdGen.Name = "LblStdGen";
            this.LblStdGen.Size = new System.Drawing.Size(48, 13);
            this.LblStdGen.TabIndex = 64;
            this.LblStdGen.Text = "Gender";
            // 
            // LblStdNationality
            // 
            this.LblStdNationality.AutoSize = true;
            this.LblStdNationality.Enabled = false;
            this.LblStdNationality.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdNationality.Location = new System.Drawing.Point(8, 86);
            this.LblStdNationality.Name = "LblStdNationality";
            this.LblStdNationality.Size = new System.Drawing.Size(67, 13);
            this.LblStdNationality.TabIndex = 63;
            this.LblStdNationality.Text = "Nationality";
            // 
            // LblStdExpiryDat
            // 
            this.LblStdExpiryDat.AutoSize = true;
            this.LblStdExpiryDat.Enabled = false;
            this.LblStdExpiryDat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdExpiryDat.Location = new System.Drawing.Point(373, 144);
            this.LblStdExpiryDat.Name = "LblStdExpiryDat";
            this.LblStdExpiryDat.Size = new System.Drawing.Size(119, 13);
            this.LblStdExpiryDat.TabIndex = 62;
            this.LblStdExpiryDat.Text = "Passport Exire Date";
            // 
            // LblStdPassIssuePlace
            // 
            this.LblStdPassIssuePlace.AutoSize = true;
            this.LblStdPassIssuePlace.Enabled = false;
            this.LblStdPassIssuePlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdPassIssuePlace.Location = new System.Drawing.Point(373, 114);
            this.LblStdPassIssuePlace.Name = "LblStdPassIssuePlace";
            this.LblStdPassIssuePlace.Size = new System.Drawing.Size(126, 13);
            this.LblStdPassIssuePlace.TabIndex = 61;
            this.LblStdPassIssuePlace.Text = "Passport Issue Place";
            // 
            // LblStdPassportID
            // 
            this.LblStdPassportID.AutoSize = true;
            this.LblStdPassportID.Enabled = false;
            this.LblStdPassportID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdPassportID.Location = new System.Drawing.Point(373, 86);
            this.LblStdPassportID.Name = "LblStdPassportID";
            this.LblStdPassportID.Size = new System.Drawing.Size(73, 13);
            this.LblStdPassportID.TabIndex = 60;
            this.LblStdPassportID.Text = "Passport ID";
            // 
            // LblStdCol
            // 
            this.LblStdCol.AutoSize = true;
            this.LblStdCol.Enabled = false;
            this.LblStdCol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdCol.Location = new System.Drawing.Point(8, 182);
            this.LblStdCol.Name = "LblStdCol";
            this.LblStdCol.Size = new System.Drawing.Size(49, 13);
            this.LblStdCol.TabIndex = 59;
            this.LblStdCol.Text = "Collage";
            // 
            // LblStdBloTy
            // 
            this.LblStdBloTy.AutoSize = true;
            this.LblStdBloTy.Enabled = false;
            this.LblStdBloTy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdBloTy.Location = new System.Drawing.Point(373, 38);
            this.LblStdBloTy.Name = "LblStdBloTy";
            this.LblStdBloTy.Size = new System.Drawing.Size(64, 13);
            this.LblStdBloTy.TabIndex = 58;
            this.LblStdBloTy.Text = "Blod Type";
            // 
            // LblStdDesc
            // 
            this.LblStdDesc.AutoSize = true;
            this.LblStdDesc.Enabled = false;
            this.LblStdDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdDesc.Location = new System.Drawing.Point(425, 182);
            this.LblStdDesc.Name = "LblStdDesc";
            this.LblStdDesc.Size = new System.Drawing.Size(65, 13);
            this.LblStdDesc.TabIndex = 56;
            this.LblStdDesc.Text = "Decription";
            // 
            // LblStdDaBth
            // 
            this.LblStdDaBth.AutoSize = true;
            this.LblStdDaBth.Enabled = false;
            this.LblStdDaBth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdDaBth.Location = new System.Drawing.Point(6, 105);
            this.LblStdDaBth.Name = "LblStdDaBth";
            this.LblStdDaBth.Size = new System.Drawing.Size(80, 13);
            this.LblStdDaBth.TabIndex = 55;
            this.LblStdDaBth.Text = "Date Of birth";
            // 
            // LblStdID
            // 
            this.LblStdID.AutoSize = true;
            this.LblStdID.Enabled = false;
            this.LblStdID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStdID.Location = new System.Drawing.Point(6, 15);
            this.LblStdID.Name = "LblStdID";
            this.LblStdID.Size = new System.Drawing.Size(68, 13);
            this.LblStdID.TabIndex = 54;
            this.LblStdID.Text = "Student ID";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(731, 202);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Card Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(731, 202);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Incoding Information";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chkboxStatus
            // 
            this.chkboxStatus.AutoSize = true;
            this.chkboxStatus.Location = new System.Drawing.Point(15, 299);
            this.chkboxStatus.Name = "chkboxStatus";
            this.chkboxStatus.Size = new System.Drawing.Size(80, 17);
            this.chkboxStatus.TabIndex = 8;
            this.chkboxStatus.Text = "checkBox1";
            this.chkboxStatus.UseVisualStyleBackColor = true;
            this.chkboxStatus.Visible = false;
            // 
            // LblPlaceOfbirth
            // 
            this.LblPlaceOfbirth.AutoSize = true;
            this.LblPlaceOfbirth.Enabled = false;
            this.LblPlaceOfbirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlaceOfbirth.Location = new System.Drawing.Point(8, 128);
            this.LblPlaceOfbirth.Name = "LblPlaceOfbirth";
            this.LblPlaceOfbirth.Size = new System.Drawing.Size(85, 13);
            this.LblPlaceOfbirth.TabIndex = 71;
            this.LblPlaceOfbirth.Text = "Place Of birth";
            // 
            // StdInformations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 608);
            this.Controls.Add(this.chkboxStatus);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.PicBoxStd);
            this.Controls.Add(this.StdGridView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StdInformations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StdInformations";
            this.Load += new System.EventHandler(this.StdInformations_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StdGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxStd)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.DataGridView StdGridView;
        private System.Windows.Forms.PictureBox PicBoxStd;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label LblStdEmail;
        private System.Windows.Forms.Label LblStdTemType;
        private System.Windows.Forms.Label LblStdStatus;
        private System.Windows.Forms.Label LblStdMobi;
        private System.Windows.Forms.Label LblStdGen;
        private System.Windows.Forms.Label LblStdNationality;
        private System.Windows.Forms.Label LblStdExpiryDat;
        private System.Windows.Forms.Label LblStdPassIssuePlace;
        private System.Windows.Forms.Label LblStdPassportID;
        private System.Windows.Forms.Label LblStdCol;
        private System.Windows.Forms.Label LblStdBloTy;
        private System.Windows.Forms.Label LblStdDesc;
        private System.Windows.Forms.Label LblStdDaBth;
        private System.Windows.Forms.Label LblStdID;
        private System.Windows.Forms.Label LblStdNat;
        private System.Windows.Forms.CheckBox chkboxStatus;
        private System.Windows.Forms.Label LblStdName;
        private System.Windows.Forms.Label LblPlaceOfbirth;
    }
}