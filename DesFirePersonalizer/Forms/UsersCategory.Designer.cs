namespace DesFirePersonalizer
{
    partial class UsersCategory1
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
            this.SearchBtnCat = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtCatDesc = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCatCode = new System.Windows.Forms.TextBox();
            this.TxtCatName = new System.Windows.Forms.TextBox();
            this.UserGridViewCat = new System.Windows.Forms.DataGridView();
            this.catNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catCodeDataGridViewImageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catDescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catogryTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.oneCard_SolutionDataSet1 = new DesFirePersonalizer.OneCard_SolutionDataSet1();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.catogryTypeTableAdapter = new DesFirePersonalizer.OneCard_SolutionDataSet1TableAdapters.CatogryTypeTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSearch = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridViewCat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.catogryTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oneCard_SolutionDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchBtnCat
            // 
            this.SearchBtnCat.Location = new System.Drawing.Point(558, 4);
            this.SearchBtnCat.Name = "SearchBtnCat";
            this.SearchBtnCat.Size = new System.Drawing.Size(108, 23);
            this.SearchBtnCat.TabIndex = 31;
            this.SearchBtnCat.Text = "Search";
            this.SearchBtnCat.UseVisualStyleBackColor = true;
            this.SearchBtnCat.Click += new System.EventHandler(this.SearchBtnCat_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(30, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(636, 42);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(175, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.InsertCat_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(299, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.DeletebtnCat_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(380, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelbtnCat_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(94, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.EditbtnCat_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(7, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtCatDesc);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtCatCode);
            this.groupBox1.Controls.Add(this.TxtCatName);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(30, 228);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(636, 190);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // TxtCatDesc
            // 
            this.TxtCatDesc.Enabled = false;
            this.TxtCatDesc.Location = new System.Drawing.Point(105, 71);
            this.TxtCatDesc.Name = "TxtCatDesc";
            this.TxtCatDesc.Size = new System.Drawing.Size(443, 59);
            this.TxtCatDesc.TabIndex = 5;
            this.TxtCatDesc.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Description :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Catogry shortcut :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Catogry Name :";
            // 
            // TxtCatCode
            // 
            this.TxtCatCode.Enabled = false;
            this.TxtCatCode.Location = new System.Drawing.Point(105, 45);
            this.TxtCatCode.Name = "TxtCatCode";
            this.TxtCatCode.Size = new System.Drawing.Size(212, 20);
            this.TxtCatCode.TabIndex = 2;
            // 
            // TxtCatName
            // 
            this.TxtCatName.Enabled = false;
            this.TxtCatName.Location = new System.Drawing.Point(105, 19);
            this.TxtCatName.Name = "TxtCatName";
            this.TxtCatName.Size = new System.Drawing.Size(212, 20);
            this.TxtCatName.TabIndex = 1;
            // 
            // UserGridViewCat
            // 
            this.UserGridViewCat.AllowUserToAddRows = false;
            this.UserGridViewCat.AllowUserToDeleteRows = false;
            this.UserGridViewCat.AutoGenerateColumns = false;
            this.UserGridViewCat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserGridViewCat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.catNameDataGridViewTextBoxColumn,
            this.catCodeDataGridViewImageColumn,
            this.catDescDataGridViewTextBoxColumn});
            this.UserGridViewCat.DataSource = this.catogryTypeBindingSource;
            this.UserGridViewCat.Location = new System.Drawing.Point(30, 34);
            this.UserGridViewCat.Name = "UserGridViewCat";
            this.UserGridViewCat.ReadOnly = true;
            this.UserGridViewCat.Size = new System.Drawing.Size(636, 150);
            this.UserGridViewCat.TabIndex = 26;
            this.UserGridViewCat.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.UserGridViewCat_CellMouseClick);
            // 
            // catNameDataGridViewTextBoxColumn
            // 
            this.catNameDataGridViewTextBoxColumn.DataPropertyName = "CatName";
            this.catNameDataGridViewTextBoxColumn.HeaderText = "Catogry Name";
            this.catNameDataGridViewTextBoxColumn.Name = "catNameDataGridViewTextBoxColumn";
            this.catNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.catNameDataGridViewTextBoxColumn.Width = 300;
            // 
            // catCodeDataGridViewImageColumn
            // 
            this.catCodeDataGridViewImageColumn.DataPropertyName = "CatCode";
            this.catCodeDataGridViewImageColumn.HeaderText = "Catogry ShortCut";
            this.catCodeDataGridViewImageColumn.Name = "catCodeDataGridViewImageColumn";
            this.catCodeDataGridViewImageColumn.ReadOnly = true;
            this.catCodeDataGridViewImageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.catCodeDataGridViewImageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // catDescDataGridViewTextBoxColumn
            // 
            this.catDescDataGridViewTextBoxColumn.DataPropertyName = "CatDesc";
            this.catDescDataGridViewTextBoxColumn.HeaderText = "Description";
            this.catDescDataGridViewTextBoxColumn.Name = "catDescDataGridViewTextBoxColumn";
            this.catDescDataGridViewTextBoxColumn.ReadOnly = true;
            this.catDescDataGridViewTextBoxColumn.Width = 180;
            // 
            // catogryTypeBindingSource
            // 
            this.catogryTypeBindingSource.DataMember = "CatogryType";
            this.catogryTypeBindingSource.DataSource = this.oneCard_SolutionDataSet1;
            // 
            // oneCard_SolutionDataSet1
            // 
            this.oneCard_SolutionDataSet1.DataSetName = "OneCard_SolutionDataSet1";
            this.oneCard_SolutionDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Enabled = false;
            this.TxtSearch.Location = new System.Drawing.Point(288, 8);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(197, 20);
            this.TxtSearch.TabIndex = 28;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // catogryTypeTableAdapter
            // 
            this.catogryTypeTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Search :";
            // 
            // cmbSearch
            // 
            this.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearch.FormattingEnabled = true;
            this.cmbSearch.Items.AddRange(new object[] {
            "All",
            "Category Name",
            "Category ShortCut"});
            this.cmbSearch.Location = new System.Drawing.Point(83, 8);
            this.cmbSearch.Name = "cmbSearch";
            this.cmbSearch.Size = new System.Drawing.Size(162, 21);
            this.cmbSearch.TabIndex = 34;
            this.cmbSearch.SelectedIndexChanged += new System.EventHandler(this.cmbSearch_SelectedIndexChanged);
            // 
            // UsersCategory1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 440);
            this.ControlBox = false;
            this.Controls.Add(this.cmbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchBtnCat);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.UserGridViewCat);
            this.Controls.Add(this.TxtSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UsersCategory1";
            this.Text = "UsersCategory";
            this.Load += new System.EventHandler(this.UsersCategory_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridViewCat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.catogryTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oneCard_SolutionDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SearchBtnCat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox TxtCatDesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtCatCode;
        private System.Windows.Forms.TextBox TxtCatName;
        private System.Windows.Forms.DataGridView UserGridViewCat;
        private System.Windows.Forms.TextBox TxtSearch;
        private OneCard_SolutionDataSet1 oneCard_SolutionDataSet1;
        private System.Windows.Forms.BindingSource catogryTypeBindingSource;
        private OneCard_SolutionDataSet1TableAdapters.CatogryTypeTableAdapter catogryTypeTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn catNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn catCodeDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn catDescDataGridViewTextBoxColumn;
        private System.Windows.Forms.ComboBox cmbSearch;
    }
}