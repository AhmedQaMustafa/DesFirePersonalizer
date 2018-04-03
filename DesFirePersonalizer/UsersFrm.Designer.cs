namespace MDIFormTest
{
    partial class UsersFrm
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
            this.cmbSearch = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.UsrID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoginName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsrStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UsrPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsrPermission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlData = new System.Windows.Forms.Panel();
            this.pnlPermission = new System.Windows.Forms.GroupBox();
            this.chbPrintCur = new System.Windows.Forms.CheckBox();
            this.chbDeleteCur = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chbUpdateCur = new System.Windows.Forms.CheckBox();
            this.chbViewCur = new System.Windows.Forms.CheckBox();
            this.chbAddCur = new System.Windows.Forms.CheckBox();
            this.chbPrintBfy = new System.Windows.Forms.CheckBox();
            this.chbDeleteBfy = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chbUpdateBfy = new System.Windows.Forms.CheckBox();
            this.chbViewBfy = new System.Windows.Forms.CheckBox();
            this.chbAddBfy = new System.Windows.Forms.CheckBox();
            this.chbPrintCPolicy = new System.Windows.Forms.CheckBox();
            this.chbPrintWPolicy = new System.Windows.Forms.CheckBox();
            this.chbPrintAAcc = new System.Windows.Forms.CheckBox();
            this.chbPrintBAcc = new System.Windows.Forms.CheckBox();
            this.chbPrintMAcc = new System.Windows.Forms.CheckBox();
            this.chbPrintUsr = new System.Windows.Forms.CheckBox();
            this.chbDeleteCPolicy = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chbUpdateCPolicy = new System.Windows.Forms.CheckBox();
            this.chbViewCPolicy = new System.Windows.Forms.CheckBox();
            this.chbAddCPolicy = new System.Windows.Forms.CheckBox();
            this.chbDeleteWPolicy = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chbUpdateWPolicy = new System.Windows.Forms.CheckBox();
            this.chbViewWPolicy = new System.Windows.Forms.CheckBox();
            this.chbAddWPolicy = new System.Windows.Forms.CheckBox();
            this.chbDeleteAAcc = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chbUpdateAAcc = new System.Windows.Forms.CheckBox();
            this.chbViewAAcc = new System.Windows.Forms.CheckBox();
            this.chbAddAAcc = new System.Windows.Forms.CheckBox();
            this.chbDeleteBAcc = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chbUpdateBAcc = new System.Windows.Forms.CheckBox();
            this.chbViewBAcc = new System.Windows.Forms.CheckBox();
            this.chbAddBAcc = new System.Windows.Forms.CheckBox();
            this.chbDeleteMAcc = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chbUpdateMAcc = new System.Windows.Forms.CheckBox();
            this.chbViewMAcc = new System.Windows.Forms.CheckBox();
            this.chbAddMAcc = new System.Windows.Forms.CheckBox();
            this.chbDeleteUsr = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbUpdateUsr = new System.Windows.Forms.CheckBox();
            this.chbViewUsr = new System.Windows.Forms.CheckBox();
            this.chbAddUsr = new System.Windows.Forms.CheckBox();
            this.chbUsrStatus = new System.Windows.Forms.CheckBox();
            this.txtUsrPass = new System.Windows.Forms.TextBox();
            this.lblUsrPassword = new System.Windows.Forms.Label();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.txtUsrName = new System.Windows.Forms.TextBox();
            this.lblUsrName = new System.Windows.Forms.Label();
            this.pnlCommand = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pnlData.SuspendLayout();
            this.pnlPermission.SuspendLayout();
            this.pnlCommand.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(383, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "إلغاء";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbSearch
            // 
            this.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearch.FormattingEnabled = true;
            this.cmbSearch.Location = new System.Drawing.Point(655, 9);
            this.cmbSearch.Name = "cmbSearch";
            this.cmbSearch.Size = new System.Drawing.Size(210, 21);
            this.cmbSearch.TabIndex = 0;
            this.cmbSearch.SelectedIndexChanged += new System.EventHandler(this.cmbSearch_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(241, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(395, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(160, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.CausesValidation = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UsrID,
            this.LoginName,
            this.UsrName,
            this.UsrStatus,
            this.UsrPass,
            this.UsrPermission});
            this.dgvData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvData.Location = new System.Drawing.Point(14, 58);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(955, 193);
            this.dgvData.TabIndex = 3;
            this.dgvData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseClick);
            // 
            // UsrID
            // 
            this.UsrID.DataPropertyName = "UsrID";
            this.UsrID.HeaderText = "رقم المستخدم";
            this.UsrID.Name = "UsrID";
            this.UsrID.ReadOnly = true;
            this.UsrID.Width = 150;
            // 
            // LoginName
            // 
            this.LoginName.DataPropertyName = "LoginName";
            this.LoginName.HeaderText = "اسم الدخول";
            this.LoginName.Name = "LoginName";
            this.LoginName.ReadOnly = true;
            this.LoginName.Width = 200;
            // 
            // UsrName
            // 
            this.UsrName.DataPropertyName = "UsrName";
            this.UsrName.HeaderText = "اسم المستخدم";
            this.UsrName.Name = "UsrName";
            this.UsrName.ReadOnly = true;
            this.UsrName.Width = 300;
            // 
            // UsrStatus
            // 
            this.UsrStatus.DataPropertyName = "UsrStatus";
            this.UsrStatus.HeaderText = "حالة المستخدم";
            this.UsrStatus.Name = "UsrStatus";
            this.UsrStatus.ReadOnly = true;
            // 
            // UsrPass
            // 
            this.UsrPass.DataPropertyName = "UsrPass";
            this.UsrPass.HeaderText = "UsrPass";
            this.UsrPass.Name = "UsrPass";
            this.UsrPass.ReadOnly = true;
            this.UsrPass.Visible = false;
            // 
            // UsrPermission
            // 
            this.UsrPermission.DataPropertyName = "UsrPermission";
            this.UsrPermission.HeaderText = "UsrPermission";
            this.UsrPermission.Name = "UsrPermission";
            this.UsrPermission.ReadOnly = true;
            this.UsrPermission.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(839, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "إضافة";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(725, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "تعديل";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(611, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(497, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlData
            // 
            this.pnlData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlData.Controls.Add(this.pnlPermission);
            this.pnlData.Controls.Add(this.chbUsrStatus);
            this.pnlData.Controls.Add(this.txtUsrPass);
            this.pnlData.Controls.Add(this.lblUsrPassword);
            this.pnlData.Controls.Add(this.txtLoginName);
            this.pnlData.Controls.Add(this.lblLoginName);
            this.pnlData.Controls.Add(this.txtUsrName);
            this.pnlData.Controls.Add(this.lblUsrName);
            this.pnlData.Location = new System.Drawing.Point(14, 308);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(955, 283);
            this.pnlData.TabIndex = 9;
            this.pnlData.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlData_Paint);
            // 
            // pnlPermission
            // 
            this.pnlPermission.Controls.Add(this.chbPrintCur);
            this.pnlPermission.Controls.Add(this.chbDeleteCur);
            this.pnlPermission.Controls.Add(this.label9);
            this.pnlPermission.Controls.Add(this.chbUpdateCur);
            this.pnlPermission.Controls.Add(this.chbViewCur);
            this.pnlPermission.Controls.Add(this.chbAddCur);
            this.pnlPermission.Controls.Add(this.chbPrintBfy);
            this.pnlPermission.Controls.Add(this.chbDeleteBfy);
            this.pnlPermission.Controls.Add(this.label8);
            this.pnlPermission.Controls.Add(this.chbUpdateBfy);
            this.pnlPermission.Controls.Add(this.chbViewBfy);
            this.pnlPermission.Controls.Add(this.chbAddBfy);
            this.pnlPermission.Controls.Add(this.chbPrintCPolicy);
            this.pnlPermission.Controls.Add(this.chbPrintWPolicy);
            this.pnlPermission.Controls.Add(this.chbPrintAAcc);
            this.pnlPermission.Controls.Add(this.chbPrintBAcc);
            this.pnlPermission.Controls.Add(this.chbPrintMAcc);
            this.pnlPermission.Controls.Add(this.chbPrintUsr);
            this.pnlPermission.Controls.Add(this.chbDeleteCPolicy);
            this.pnlPermission.Controls.Add(this.label7);
            this.pnlPermission.Controls.Add(this.chbUpdateCPolicy);
            this.pnlPermission.Controls.Add(this.chbViewCPolicy);
            this.pnlPermission.Controls.Add(this.chbAddCPolicy);
            this.pnlPermission.Controls.Add(this.chbDeleteWPolicy);
            this.pnlPermission.Controls.Add(this.label6);
            this.pnlPermission.Controls.Add(this.chbUpdateWPolicy);
            this.pnlPermission.Controls.Add(this.chbViewWPolicy);
            this.pnlPermission.Controls.Add(this.chbAddWPolicy);
            this.pnlPermission.Controls.Add(this.chbDeleteAAcc);
            this.pnlPermission.Controls.Add(this.label5);
            this.pnlPermission.Controls.Add(this.chbUpdateAAcc);
            this.pnlPermission.Controls.Add(this.chbViewAAcc);
            this.pnlPermission.Controls.Add(this.chbAddAAcc);
            this.pnlPermission.Controls.Add(this.chbDeleteBAcc);
            this.pnlPermission.Controls.Add(this.label4);
            this.pnlPermission.Controls.Add(this.chbUpdateBAcc);
            this.pnlPermission.Controls.Add(this.chbViewBAcc);
            this.pnlPermission.Controls.Add(this.chbAddBAcc);
            this.pnlPermission.Controls.Add(this.chbDeleteMAcc);
            this.pnlPermission.Controls.Add(this.label2);
            this.pnlPermission.Controls.Add(this.chbUpdateMAcc);
            this.pnlPermission.Controls.Add(this.chbViewMAcc);
            this.pnlPermission.Controls.Add(this.chbAddMAcc);
            this.pnlPermission.Controls.Add(this.chbDeleteUsr);
            this.pnlPermission.Controls.Add(this.label1);
            this.pnlPermission.Controls.Add(this.chbUpdateUsr);
            this.pnlPermission.Controls.Add(this.chbViewUsr);
            this.pnlPermission.Controls.Add(this.chbAddUsr);
            this.pnlPermission.Location = new System.Drawing.Point(18, 13);
            this.pnlPermission.Name = "pnlPermission";
            this.pnlPermission.Size = new System.Drawing.Size(479, 255);
            this.pnlPermission.TabIndex = 8;
            this.pnlPermission.TabStop = false;
            this.pnlPermission.Text = "الصلاحيات";
            // 
            // chbPrintCur
            // 
            this.chbPrintCur.AutoSize = true;
            this.chbPrintCur.Enabled = false;
            this.chbPrintCur.Location = new System.Drawing.Point(44, 130);
            this.chbPrintCur.Name = "chbPrintCur";
            this.chbPrintCur.Size = new System.Drawing.Size(53, 17);
            this.chbPrintCur.TabIndex = 47;
            this.chbPrintCur.Text = "طباعة";
            this.chbPrintCur.UseVisualStyleBackColor = true;
            // 
            // chbDeleteCur
            // 
            this.chbDeleteCur.AutoSize = true;
            this.chbDeleteCur.Location = new System.Drawing.Point(98, 130);
            this.chbDeleteCur.Name = "chbDeleteCur";
            this.chbDeleteCur.Size = new System.Drawing.Size(49, 17);
            this.chbDeleteCur.TabIndex = 46;
            this.chbDeleteCur.Text = "حذف";
            this.chbDeleteCur.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(392, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "العملات";
            // 
            // chbUpdateCur
            // 
            this.chbUpdateCur.AutoSize = true;
            this.chbUpdateCur.Location = new System.Drawing.Point(153, 130);
            this.chbUpdateCur.Name = "chbUpdateCur";
            this.chbUpdateCur.Size = new System.Drawing.Size(52, 17);
            this.chbUpdateCur.TabIndex = 45;
            this.chbUpdateCur.Text = "تعديل";
            this.chbUpdateCur.UseVisualStyleBackColor = true;
            // 
            // chbViewCur
            // 
            this.chbViewCur.AutoSize = true;
            this.chbViewCur.Location = new System.Drawing.Point(269, 130);
            this.chbViewCur.Name = "chbViewCur";
            this.chbViewCur.Size = new System.Drawing.Size(51, 17);
            this.chbViewCur.TabIndex = 43;
            this.chbViewCur.Text = "عرض";
            this.chbViewCur.UseVisualStyleBackColor = true;
            // 
            // chbAddCur
            // 
            this.chbAddCur.AutoSize = true;
            this.chbAddCur.Location = new System.Drawing.Point(211, 130);
            this.chbAddCur.Name = "chbAddCur";
            this.chbAddCur.Size = new System.Drawing.Size(52, 17);
            this.chbAddCur.TabIndex = 44;
            this.chbAddCur.Text = "إضافة";
            this.chbAddCur.UseVisualStyleBackColor = true;
            // 
            // chbPrintBfy
            // 
            this.chbPrintBfy.AutoSize = true;
            this.chbPrintBfy.Location = new System.Drawing.Point(44, 196);
            this.chbPrintBfy.Name = "chbPrintBfy";
            this.chbPrintBfy.Size = new System.Drawing.Size(53, 17);
            this.chbPrintBfy.TabIndex = 41;
            this.chbPrintBfy.Text = "طباعة";
            this.chbPrintBfy.UseVisualStyleBackColor = true;
            // 
            // chbDeleteBfy
            // 
            this.chbDeleteBfy.AutoSize = true;
            this.chbDeleteBfy.Location = new System.Drawing.Point(98, 196);
            this.chbDeleteBfy.Name = "chbDeleteBfy";
            this.chbDeleteBfy.Size = new System.Drawing.Size(49, 17);
            this.chbDeleteBfy.TabIndex = 40;
            this.chbDeleteBfy.Text = "حذف";
            this.chbDeleteBfy.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(370, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "المستفيدون";
            // 
            // chbUpdateBfy
            // 
            this.chbUpdateBfy.AutoSize = true;
            this.chbUpdateBfy.Location = new System.Drawing.Point(153, 196);
            this.chbUpdateBfy.Name = "chbUpdateBfy";
            this.chbUpdateBfy.Size = new System.Drawing.Size(52, 17);
            this.chbUpdateBfy.TabIndex = 39;
            this.chbUpdateBfy.Text = "تعديل";
            this.chbUpdateBfy.UseVisualStyleBackColor = true;
            // 
            // chbViewBfy
            // 
            this.chbViewBfy.AutoSize = true;
            this.chbViewBfy.Location = new System.Drawing.Point(269, 196);
            this.chbViewBfy.Name = "chbViewBfy";
            this.chbViewBfy.Size = new System.Drawing.Size(51, 17);
            this.chbViewBfy.TabIndex = 37;
            this.chbViewBfy.Text = "عرض";
            this.chbViewBfy.UseVisualStyleBackColor = true;
            // 
            // chbAddBfy
            // 
            this.chbAddBfy.AutoSize = true;
            this.chbAddBfy.Location = new System.Drawing.Point(211, 196);
            this.chbAddBfy.Name = "chbAddBfy";
            this.chbAddBfy.Size = new System.Drawing.Size(52, 17);
            this.chbAddBfy.TabIndex = 38;
            this.chbAddBfy.Text = "إضافة";
            this.chbAddBfy.UseVisualStyleBackColor = true;
            // 
            // chbPrintCPolicy
            // 
            this.chbPrintCPolicy.AutoSize = true;
            this.chbPrintCPolicy.Location = new System.Drawing.Point(44, 174);
            this.chbPrintCPolicy.Name = "chbPrintCPolicy";
            this.chbPrintCPolicy.Size = new System.Drawing.Size(53, 17);
            this.chbPrintCPolicy.TabIndex = 35;
            this.chbPrintCPolicy.Text = "طباعة";
            this.chbPrintCPolicy.UseVisualStyleBackColor = true;
            // 
            // chbPrintWPolicy
            // 
            this.chbPrintWPolicy.AutoSize = true;
            this.chbPrintWPolicy.Location = new System.Drawing.Point(44, 152);
            this.chbPrintWPolicy.Name = "chbPrintWPolicy";
            this.chbPrintWPolicy.Size = new System.Drawing.Size(53, 17);
            this.chbPrintWPolicy.TabIndex = 34;
            this.chbPrintWPolicy.Text = "طباعة";
            this.chbPrintWPolicy.UseVisualStyleBackColor = true;
            // 
            // chbPrintAAcc
            // 
            this.chbPrintAAcc.AutoSize = true;
            this.chbPrintAAcc.Enabled = false;
            this.chbPrintAAcc.Location = new System.Drawing.Point(44, 108);
            this.chbPrintAAcc.Name = "chbPrintAAcc";
            this.chbPrintAAcc.Size = new System.Drawing.Size(53, 17);
            this.chbPrintAAcc.TabIndex = 33;
            this.chbPrintAAcc.Text = "طباعة";
            this.chbPrintAAcc.UseVisualStyleBackColor = true;
            // 
            // chbPrintBAcc
            // 
            this.chbPrintBAcc.AutoSize = true;
            this.chbPrintBAcc.Enabled = false;
            this.chbPrintBAcc.Location = new System.Drawing.Point(44, 86);
            this.chbPrintBAcc.Name = "chbPrintBAcc";
            this.chbPrintBAcc.Size = new System.Drawing.Size(53, 17);
            this.chbPrintBAcc.TabIndex = 32;
            this.chbPrintBAcc.Text = "طباعة";
            this.chbPrintBAcc.UseVisualStyleBackColor = true;
            // 
            // chbPrintMAcc
            // 
            this.chbPrintMAcc.AutoSize = true;
            this.chbPrintMAcc.Enabled = false;
            this.chbPrintMAcc.Location = new System.Drawing.Point(44, 64);
            this.chbPrintMAcc.Name = "chbPrintMAcc";
            this.chbPrintMAcc.Size = new System.Drawing.Size(53, 17);
            this.chbPrintMAcc.TabIndex = 31;
            this.chbPrintMAcc.Text = "طباعة";
            this.chbPrintMAcc.UseVisualStyleBackColor = true;
            // 
            // chbPrintUsr
            // 
            this.chbPrintUsr.AutoSize = true;
            this.chbPrintUsr.Enabled = false;
            this.chbPrintUsr.Location = new System.Drawing.Point(44, 42);
            this.chbPrintUsr.Name = "chbPrintUsr";
            this.chbPrintUsr.Size = new System.Drawing.Size(53, 17);
            this.chbPrintUsr.TabIndex = 30;
            this.chbPrintUsr.Text = "طباعة";
            this.chbPrintUsr.UseVisualStyleBackColor = true;
            // 
            // chbDeleteCPolicy
            // 
            this.chbDeleteCPolicy.AutoSize = true;
            this.chbDeleteCPolicy.Location = new System.Drawing.Point(98, 174);
            this.chbDeleteCPolicy.Name = "chbDeleteCPolicy";
            this.chbDeleteCPolicy.Size = new System.Drawing.Size(49, 17);
            this.chbDeleteCPolicy.TabIndex = 29;
            this.chbDeleteCPolicy.Text = "حذف";
            this.chbDeleteCPolicy.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(387, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "الشيكات";
            // 
            // chbUpdateCPolicy
            // 
            this.chbUpdateCPolicy.AutoSize = true;
            this.chbUpdateCPolicy.Location = new System.Drawing.Point(153, 174);
            this.chbUpdateCPolicy.Name = "chbUpdateCPolicy";
            this.chbUpdateCPolicy.Size = new System.Drawing.Size(52, 17);
            this.chbUpdateCPolicy.TabIndex = 28;
            this.chbUpdateCPolicy.Text = "تعديل";
            this.chbUpdateCPolicy.UseVisualStyleBackColor = true;
            // 
            // chbViewCPolicy
            // 
            this.chbViewCPolicy.AutoSize = true;
            this.chbViewCPolicy.Location = new System.Drawing.Point(269, 174);
            this.chbViewCPolicy.Name = "chbViewCPolicy";
            this.chbViewCPolicy.Size = new System.Drawing.Size(51, 17);
            this.chbViewCPolicy.TabIndex = 26;
            this.chbViewCPolicy.Text = "عرض";
            this.chbViewCPolicy.UseVisualStyleBackColor = true;
            // 
            // chbAddCPolicy
            // 
            this.chbAddCPolicy.AutoSize = true;
            this.chbAddCPolicy.Location = new System.Drawing.Point(211, 174);
            this.chbAddCPolicy.Name = "chbAddCPolicy";
            this.chbAddCPolicy.Size = new System.Drawing.Size(52, 17);
            this.chbAddCPolicy.TabIndex = 27;
            this.chbAddCPolicy.Text = "إضافة";
            this.chbAddCPolicy.UseVisualStyleBackColor = true;
            // 
            // chbDeleteWPolicy
            // 
            this.chbDeleteWPolicy.AutoSize = true;
            this.chbDeleteWPolicy.Location = new System.Drawing.Point(98, 152);
            this.chbDeleteWPolicy.Name = "chbDeleteWPolicy";
            this.chbDeleteWPolicy.Size = new System.Drawing.Size(49, 17);
            this.chbDeleteWPolicy.TabIndex = 24;
            this.chbDeleteWPolicy.Text = "حذف";
            this.chbDeleteWPolicy.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(387, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "أمر صرف";
            // 
            // chbUpdateWPolicy
            // 
            this.chbUpdateWPolicy.AutoSize = true;
            this.chbUpdateWPolicy.Location = new System.Drawing.Point(153, 152);
            this.chbUpdateWPolicy.Name = "chbUpdateWPolicy";
            this.chbUpdateWPolicy.Size = new System.Drawing.Size(52, 17);
            this.chbUpdateWPolicy.TabIndex = 23;
            this.chbUpdateWPolicy.Text = "تعديل";
            this.chbUpdateWPolicy.UseVisualStyleBackColor = true;
            // 
            // chbViewWPolicy
            // 
            this.chbViewWPolicy.AutoSize = true;
            this.chbViewWPolicy.Location = new System.Drawing.Point(269, 152);
            this.chbViewWPolicy.Name = "chbViewWPolicy";
            this.chbViewWPolicy.Size = new System.Drawing.Size(51, 17);
            this.chbViewWPolicy.TabIndex = 21;
            this.chbViewWPolicy.Text = "عرض";
            this.chbViewWPolicy.UseVisualStyleBackColor = true;
            // 
            // chbAddWPolicy
            // 
            this.chbAddWPolicy.AutoSize = true;
            this.chbAddWPolicy.Location = new System.Drawing.Point(211, 152);
            this.chbAddWPolicy.Name = "chbAddWPolicy";
            this.chbAddWPolicy.Size = new System.Drawing.Size(52, 17);
            this.chbAddWPolicy.TabIndex = 22;
            this.chbAddWPolicy.Text = "إضافة";
            this.chbAddWPolicy.UseVisualStyleBackColor = true;
            // 
            // chbDeleteAAcc
            // 
            this.chbDeleteAAcc.AutoSize = true;
            this.chbDeleteAAcc.Location = new System.Drawing.Point(98, 108);
            this.chbDeleteAAcc.Name = "chbDeleteAAcc";
            this.chbDeleteAAcc.Size = new System.Drawing.Size(49, 17);
            this.chbDeleteAAcc.TabIndex = 19;
            this.chbDeleteAAcc.Text = "حذف";
            this.chbDeleteAAcc.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(376, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "إضافة رصيد";
            // 
            // chbUpdateAAcc
            // 
            this.chbUpdateAAcc.AutoSize = true;
            this.chbUpdateAAcc.Location = new System.Drawing.Point(153, 108);
            this.chbUpdateAAcc.Name = "chbUpdateAAcc";
            this.chbUpdateAAcc.Size = new System.Drawing.Size(52, 17);
            this.chbUpdateAAcc.TabIndex = 18;
            this.chbUpdateAAcc.Text = "تعديل";
            this.chbUpdateAAcc.UseVisualStyleBackColor = true;
            // 
            // chbViewAAcc
            // 
            this.chbViewAAcc.AutoSize = true;
            this.chbViewAAcc.Location = new System.Drawing.Point(269, 108);
            this.chbViewAAcc.Name = "chbViewAAcc";
            this.chbViewAAcc.Size = new System.Drawing.Size(51, 17);
            this.chbViewAAcc.TabIndex = 16;
            this.chbViewAAcc.Text = "عرض";
            this.chbViewAAcc.UseVisualStyleBackColor = true;
            // 
            // chbAddAAcc
            // 
            this.chbAddAAcc.AutoSize = true;
            this.chbAddAAcc.Location = new System.Drawing.Point(211, 108);
            this.chbAddAAcc.Name = "chbAddAAcc";
            this.chbAddAAcc.Size = new System.Drawing.Size(52, 17);
            this.chbAddAAcc.TabIndex = 17;
            this.chbAddAAcc.Text = "إضافة";
            this.chbAddAAcc.UseVisualStyleBackColor = true;
            // 
            // chbDeleteBAcc
            // 
            this.chbDeleteBAcc.AutoSize = true;
            this.chbDeleteBAcc.Location = new System.Drawing.Point(98, 86);
            this.chbDeleteBAcc.Name = "chbDeleteBAcc";
            this.chbDeleteBAcc.Size = new System.Drawing.Size(49, 17);
            this.chbDeleteBAcc.TabIndex = 14;
            this.chbDeleteBAcc.Text = "حذف";
            this.chbDeleteBAcc.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(348, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "الحسابات الفرعية";
            // 
            // chbUpdateBAcc
            // 
            this.chbUpdateBAcc.AutoSize = true;
            this.chbUpdateBAcc.Location = new System.Drawing.Point(153, 86);
            this.chbUpdateBAcc.Name = "chbUpdateBAcc";
            this.chbUpdateBAcc.Size = new System.Drawing.Size(52, 17);
            this.chbUpdateBAcc.TabIndex = 13;
            this.chbUpdateBAcc.Text = "تعديل";
            this.chbUpdateBAcc.UseVisualStyleBackColor = true;
            // 
            // chbViewBAcc
            // 
            this.chbViewBAcc.AutoSize = true;
            this.chbViewBAcc.Location = new System.Drawing.Point(269, 86);
            this.chbViewBAcc.Name = "chbViewBAcc";
            this.chbViewBAcc.Size = new System.Drawing.Size(51, 17);
            this.chbViewBAcc.TabIndex = 11;
            this.chbViewBAcc.Text = "عرض";
            this.chbViewBAcc.UseVisualStyleBackColor = true;
            // 
            // chbAddBAcc
            // 
            this.chbAddBAcc.AutoSize = true;
            this.chbAddBAcc.Location = new System.Drawing.Point(211, 86);
            this.chbAddBAcc.Name = "chbAddBAcc";
            this.chbAddBAcc.Size = new System.Drawing.Size(52, 17);
            this.chbAddBAcc.TabIndex = 12;
            this.chbAddBAcc.Text = "إضافة";
            this.chbAddBAcc.UseVisualStyleBackColor = true;
            // 
            // chbDeleteMAcc
            // 
            this.chbDeleteMAcc.AutoSize = true;
            this.chbDeleteMAcc.Location = new System.Drawing.Point(98, 64);
            this.chbDeleteMAcc.Name = "chbDeleteMAcc";
            this.chbDeleteMAcc.Size = new System.Drawing.Size(49, 17);
            this.chbDeleteMAcc.TabIndex = 9;
            this.chbDeleteMAcc.Text = "حذف";
            this.chbDeleteMAcc.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "الحسابات الرئيسية";
            // 
            // chbUpdateMAcc
            // 
            this.chbUpdateMAcc.AutoSize = true;
            this.chbUpdateMAcc.Location = new System.Drawing.Point(153, 64);
            this.chbUpdateMAcc.Name = "chbUpdateMAcc";
            this.chbUpdateMAcc.Size = new System.Drawing.Size(52, 17);
            this.chbUpdateMAcc.TabIndex = 8;
            this.chbUpdateMAcc.Text = "تعديل";
            this.chbUpdateMAcc.UseVisualStyleBackColor = true;
            // 
            // chbViewMAcc
            // 
            this.chbViewMAcc.AutoSize = true;
            this.chbViewMAcc.Location = new System.Drawing.Point(269, 64);
            this.chbViewMAcc.Name = "chbViewMAcc";
            this.chbViewMAcc.Size = new System.Drawing.Size(51, 17);
            this.chbViewMAcc.TabIndex = 6;
            this.chbViewMAcc.Text = "عرض";
            this.chbViewMAcc.UseVisualStyleBackColor = true;
            // 
            // chbAddMAcc
            // 
            this.chbAddMAcc.AutoSize = true;
            this.chbAddMAcc.Location = new System.Drawing.Point(211, 64);
            this.chbAddMAcc.Name = "chbAddMAcc";
            this.chbAddMAcc.Size = new System.Drawing.Size(52, 17);
            this.chbAddMAcc.TabIndex = 7;
            this.chbAddMAcc.Text = "إضافة";
            this.chbAddMAcc.UseVisualStyleBackColor = true;
            // 
            // chbDeleteUsr
            // 
            this.chbDeleteUsr.AutoSize = true;
            this.chbDeleteUsr.Location = new System.Drawing.Point(98, 42);
            this.chbDeleteUsr.Name = "chbDeleteUsr";
            this.chbDeleteUsr.Size = new System.Drawing.Size(49, 17);
            this.chbDeleteUsr.TabIndex = 4;
            this.chbDeleteUsr.Text = "حذف";
            this.chbDeleteUsr.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "المستخدمون";
            // 
            // chbUpdateUsr
            // 
            this.chbUpdateUsr.AutoSize = true;
            this.chbUpdateUsr.Location = new System.Drawing.Point(153, 42);
            this.chbUpdateUsr.Name = "chbUpdateUsr";
            this.chbUpdateUsr.Size = new System.Drawing.Size(52, 17);
            this.chbUpdateUsr.TabIndex = 3;
            this.chbUpdateUsr.Text = "تعديل";
            this.chbUpdateUsr.UseVisualStyleBackColor = true;
            // 
            // chbViewUsr
            // 
            this.chbViewUsr.AutoSize = true;
            this.chbViewUsr.Location = new System.Drawing.Point(269, 42);
            this.chbViewUsr.Name = "chbViewUsr";
            this.chbViewUsr.Size = new System.Drawing.Size(51, 17);
            this.chbViewUsr.TabIndex = 1;
            this.chbViewUsr.Text = "عرض";
            this.chbViewUsr.UseVisualStyleBackColor = true;
            // 
            // chbAddUsr
            // 
            this.chbAddUsr.AutoSize = true;
            this.chbAddUsr.Location = new System.Drawing.Point(211, 42);
            this.chbAddUsr.Name = "chbAddUsr";
            this.chbAddUsr.Size = new System.Drawing.Size(52, 17);
            this.chbAddUsr.TabIndex = 2;
            this.chbAddUsr.Text = "إضافة";
            this.chbAddUsr.UseVisualStyleBackColor = true;
            // 
            // chbUsrStatus
            // 
            this.chbUsrStatus.AutoSize = true;
            this.chbUsrStatus.Enabled = false;
            this.chbUsrStatus.Location = new System.Drawing.Point(793, 121);
            this.chbUsrStatus.Name = "chbUsrStatus";
            this.chbUsrStatus.Size = new System.Drawing.Size(135, 17);
            this.chbUsrStatus.TabIndex = 6;
            this.chbUsrStatus.Text = "حالة المستخدم ( فعال )";
            this.chbUsrStatus.UseVisualStyleBackColor = true;
            // 
            // txtUsrPass
            // 
            this.txtUsrPass.Enabled = false;
            this.txtUsrPass.Location = new System.Drawing.Point(522, 60);
            this.txtUsrPass.Name = "txtUsrPass";
            this.txtUsrPass.Size = new System.Drawing.Size(303, 20);
            this.txtUsrPass.TabIndex = 5;
            this.txtUsrPass.UseSystemPasswordChar = true;
            // 
            // lblUsrPassword
            // 
            this.lblUsrPassword.Location = new System.Drawing.Point(831, 59);
            this.lblUsrPassword.Name = "lblUsrPassword";
            this.lblUsrPassword.Size = new System.Drawing.Size(100, 20);
            this.lblUsrPassword.TabIndex = 4;
            this.lblUsrPassword.Text = "كلمة المرور :";
            this.lblUsrPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLoginName
            // 
            this.txtLoginName.Enabled = false;
            this.txtLoginName.Location = new System.Drawing.Point(522, 31);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(303, 20);
            this.txtLoginName.TabIndex = 3;
            // 
            // lblLoginName
            // 
            this.lblLoginName.Location = new System.Drawing.Point(831, 30);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(100, 20);
            this.lblLoginName.TabIndex = 2;
            this.lblLoginName.Text = "اسم الدخول :";
            this.lblLoginName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUsrName
            // 
            this.txtUsrName.Enabled = false;
            this.txtUsrName.Location = new System.Drawing.Point(522, 89);
            this.txtUsrName.Name = "txtUsrName";
            this.txtUsrName.Size = new System.Drawing.Size(303, 20);
            this.txtUsrName.TabIndex = 1;
            // 
            // lblUsrName
            // 
            this.lblUsrName.Location = new System.Drawing.Point(831, 88);
            this.lblUsrName.Name = "lblUsrName";
            this.lblUsrName.Size = new System.Drawing.Size(100, 20);
            this.lblUsrName.TabIndex = 0;
            this.lblUsrName.Text = "اسم المستخدم :";
            this.lblUsrName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCommand
            // 
            this.pnlCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCommand.Controls.Add(this.btnCancel);
            this.pnlCommand.Controls.Add(this.btnSave);
            this.pnlCommand.Controls.Add(this.btnDelete);
            this.pnlCommand.Controls.Add(this.btnUpdate);
            this.pnlCommand.Controls.Add(this.btnAdd);
            this.pnlCommand.Location = new System.Drawing.Point(14, 260);
            this.pnlCommand.Name = "pnlCommand";
            this.pnlCommand.Size = new System.Drawing.Size(955, 41);
            this.pnlCommand.TabIndex = 10;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.cmbSearch);
            this.pnlSearch.Location = new System.Drawing.Point(14, 11);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(955, 41);
            this.pnlSearch.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(871, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "بحث بواسطة";
            // 
            // UsersFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 603);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlCommand);
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.dgvData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UsersFrm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "UsersFrm";
            this.Load += new System.EventHandler(this.UsersFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            this.pnlPermission.ResumeLayout(false);
            this.pnlPermission.PerformLayout();
            this.pnlCommand.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.Panel pnlCommand;
        private System.Windows.Forms.CheckBox chbUsrStatus;
        private System.Windows.Forms.TextBox txtUsrPass;
        private System.Windows.Forms.Label lblUsrPassword;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.TextBox txtUsrName;
        private System.Windows.Forms.Label lblUsrName;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbDeleteUsr;
        private System.Windows.Forms.CheckBox chbViewUsr;
        private System.Windows.Forms.CheckBox chbUpdateUsr;
        private System.Windows.Forms.CheckBox chbAddUsr;
        private System.Windows.Forms.GroupBox pnlPermission;
        private System.Windows.Forms.CheckBox chbDeleteMAcc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbUpdateMAcc;
        private System.Windows.Forms.CheckBox chbViewMAcc;
        private System.Windows.Forms.CheckBox chbAddMAcc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsrID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsrName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UsrStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsrPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsrPermission;
        private System.Windows.Forms.CheckBox chbDeleteBAcc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chbUpdateBAcc;
        private System.Windows.Forms.CheckBox chbViewBAcc;
        private System.Windows.Forms.CheckBox chbAddBAcc;
        private System.Windows.Forms.CheckBox chbDeleteAAcc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chbUpdateAAcc;
        private System.Windows.Forms.CheckBox chbViewAAcc;
        private System.Windows.Forms.CheckBox chbAddAAcc;
        private System.Windows.Forms.CheckBox chbDeleteWPolicy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chbUpdateWPolicy;
        private System.Windows.Forms.CheckBox chbViewWPolicy;
        private System.Windows.Forms.CheckBox chbAddWPolicy;
        private System.Windows.Forms.CheckBox chbDeleteCPolicy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chbUpdateCPolicy;
        private System.Windows.Forms.CheckBox chbViewCPolicy;
        private System.Windows.Forms.CheckBox chbAddCPolicy;
        private System.Windows.Forms.CheckBox chbPrintUsr;
        private System.Windows.Forms.CheckBox chbPrintCPolicy;
        private System.Windows.Forms.CheckBox chbPrintWPolicy;
        private System.Windows.Forms.CheckBox chbPrintAAcc;
        private System.Windows.Forms.CheckBox chbPrintBAcc;
        private System.Windows.Forms.CheckBox chbPrintMAcc;
        private System.Windows.Forms.CheckBox chbPrintBfy;
        private System.Windows.Forms.CheckBox chbDeleteBfy;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chbUpdateBfy;
        private System.Windows.Forms.CheckBox chbViewBfy;
        private System.Windows.Forms.CheckBox chbAddBfy;
        private System.Windows.Forms.CheckBox chbPrintCur;
        private System.Windows.Forms.CheckBox chbDeleteCur;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chbUpdateCur;
        private System.Windows.Forms.CheckBox chbViewCur;
        private System.Windows.Forms.CheckBox chbAddCur;
    }
}