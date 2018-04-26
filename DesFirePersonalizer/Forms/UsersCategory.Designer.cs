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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersCategory1));
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
            this.catogryTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.oneCard_SolutionDataSet1 = new DesFirePersonalizer.OneCard_SolutionDataSet1();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.catogryTypeTableAdapter = new DesFirePersonalizer.OneCard_SolutionDataSet1TableAdapters.CatogryTypeTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSearch = new System.Windows.Forms.ComboBox();
            this.catNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catCodeDataGridViewImageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catDescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridViewCat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.catogryTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oneCard_SolutionDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchBtnCat
            // 
            resources.ApplyResources(this.SearchBtnCat, "SearchBtnCat");
            this.SearchBtnCat.Name = "SearchBtnCat";
            this.SearchBtnCat.UseVisualStyleBackColor = true;
            this.SearchBtnCat.Click += new System.EventHandler(this.SearchBtnCat_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.InsertCat_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.DeletebtnCat_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelbtnCat_Click);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.EditbtnCat_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.TxtCatDesc);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtCatCode);
            this.groupBox1.Controls.Add(this.TxtCatName);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // TxtCatDesc
            // 
            resources.ApplyResources(this.TxtCatDesc, "TxtCatDesc");
            this.TxtCatDesc.Name = "TxtCatDesc";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // TxtCatCode
            // 
            resources.ApplyResources(this.TxtCatCode, "TxtCatCode");
            this.TxtCatCode.Name = "TxtCatCode";
            // 
            // TxtCatName
            // 
            resources.ApplyResources(this.TxtCatName, "TxtCatName");
            this.TxtCatName.Name = "TxtCatName";
            // 
            // UserGridViewCat
            // 
            resources.ApplyResources(this.UserGridViewCat, "UserGridViewCat");
            this.UserGridViewCat.AllowUserToAddRows = false;
            this.UserGridViewCat.AllowUserToDeleteRows = false;
            this.UserGridViewCat.AutoGenerateColumns = false;
            this.UserGridViewCat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserGridViewCat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.catNameDataGridViewTextBoxColumn,
            this.catCodeDataGridViewImageColumn,
            this.catDescDataGridViewTextBoxColumn});
            this.UserGridViewCat.DataSource = this.catogryTypeBindingSource;
            this.UserGridViewCat.Name = "UserGridViewCat";
            this.UserGridViewCat.ReadOnly = true;
            this.UserGridViewCat.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.UserGridViewCat_CellMouseClick);
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
            resources.ApplyResources(this.TxtSearch, "TxtSearch");
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // catogryTypeTableAdapter
            // 
            this.catogryTypeTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbSearch
            // 
            resources.ApplyResources(this.cmbSearch, "cmbSearch");
            this.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearch.FormattingEnabled = true;
            this.cmbSearch.Items.AddRange(new object[] {
            resources.GetString("cmbSearch.Items"),
            resources.GetString("cmbSearch.Items1"),
            resources.GetString("cmbSearch.Items2")});
            this.cmbSearch.Name = "cmbSearch";
            this.cmbSearch.SelectedIndexChanged += new System.EventHandler(this.cmbSearch_SelectedIndexChanged);
            // 
            // catNameDataGridViewTextBoxColumn
            // 
            this.catNameDataGridViewTextBoxColumn.DataPropertyName = "CatName";
            resources.ApplyResources(this.catNameDataGridViewTextBoxColumn, "catNameDataGridViewTextBoxColumn");
            this.catNameDataGridViewTextBoxColumn.Name = "catNameDataGridViewTextBoxColumn";
            this.catNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // catCodeDataGridViewImageColumn
            // 
            this.catCodeDataGridViewImageColumn.DataPropertyName = "CatCode";
            resources.ApplyResources(this.catCodeDataGridViewImageColumn, "catCodeDataGridViewImageColumn");
            this.catCodeDataGridViewImageColumn.Name = "catCodeDataGridViewImageColumn";
            this.catCodeDataGridViewImageColumn.ReadOnly = true;
            this.catCodeDataGridViewImageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.catCodeDataGridViewImageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // catDescDataGridViewTextBoxColumn
            // 
            this.catDescDataGridViewTextBoxColumn.DataPropertyName = "CatDesc";
            resources.ApplyResources(this.catDescDataGridViewTextBoxColumn, "catDescDataGridViewTextBoxColumn");
            this.catDescDataGridViewTextBoxColumn.Name = "catDescDataGridViewTextBoxColumn";
            this.catDescDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // UsersCategory1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
        private System.Windows.Forms.ComboBox cmbSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn catNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn catCodeDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn catDescDataGridViewTextBoxColumn;
    }
}