namespace DesFirePersonalizer.Configurations_Pages
{
    partial class MenuSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuSetup));
            this.MenuDataGridView = new System.Windows.Forms.DataGridView();
            this.RestMenuID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RestMenuName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RestPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.UpdateMenuBtn = new System.Windows.Forms.Button();
            this.showAllMenuBtn = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.DeleteMenuBtn = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Informations = new System.Windows.Forms.GroupBox();
            this.searchMenuBtn = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.PriceTextBox = new System.Windows.Forms.TextBox();
            this.MenuIDTextBox = new System.Windows.Forms.TextBox();
            this.MenuNameTextBox = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.Actions = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.MenuDataGridView)).BeginInit();
            this.Informations.SuspendLayout();
            this.Actions.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuDataGridView
            // 
            this.MenuDataGridView.AllowUserToAddRows = false;
            this.MenuDataGridView.AllowUserToDeleteRows = false;
            this.MenuDataGridView.AllowUserToOrderColumns = true;
            this.MenuDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MenuDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RestMenuID,
            this.RestMenuName,
            this.RestPrice});
            resources.ApplyResources(this.MenuDataGridView, "MenuDataGridView");
            this.MenuDataGridView.Name = "MenuDataGridView";
            this.MenuDataGridView.ReadOnly = true;
            this.MenuDataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MenuDataGridView_CellMouseClick);
            // 
            // RestMenuID
            // 
            this.RestMenuID.DataPropertyName = "RestMenuID";
            resources.ApplyResources(this.RestMenuID, "RestMenuID");
            this.RestMenuID.Name = "RestMenuID";
            this.RestMenuID.ReadOnly = true;
            // 
            // RestMenuName
            // 
            this.RestMenuName.DataPropertyName = "RestMenuName";
            resources.ApplyResources(this.RestMenuName, "RestMenuName");
            this.RestMenuName.Name = "RestMenuName";
            this.RestMenuName.ReadOnly = true;
            // 
            // RestPrice
            // 
            this.RestPrice.DataPropertyName = "RestPrice";
            resources.ApplyResources(this.RestPrice, "RestPrice");
            this.RestPrice.Name = "RestPrice";
            this.RestPrice.ReadOnly = true;
            // 
            // BtnAdd
            // 
            resources.ApplyResources(this.BtnAdd, "BtnAdd");
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // UpdateMenuBtn
            // 
            resources.ApplyResources(this.UpdateMenuBtn, "UpdateMenuBtn");
            this.UpdateMenuBtn.Name = "UpdateMenuBtn";
            this.UpdateMenuBtn.UseVisualStyleBackColor = true;
            this.UpdateMenuBtn.Click += new System.EventHandler(this.UpdateMenuBtn_Click);
            // 
            // showAllMenuBtn
            // 
            resources.ApplyResources(this.showAllMenuBtn, "showAllMenuBtn");
            this.showAllMenuBtn.Name = "showAllMenuBtn";
            this.showAllMenuBtn.UseVisualStyleBackColor = true;
            this.showAllMenuBtn.Click += new System.EventHandler(this.showAllMenuBtn_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // DeleteMenuBtn
            // 
            resources.ApplyResources(this.DeleteMenuBtn, "DeleteMenuBtn");
            this.DeleteMenuBtn.Name = "DeleteMenuBtn";
            this.DeleteMenuBtn.UseVisualStyleBackColor = true;
            this.DeleteMenuBtn.Click += new System.EventHandler(this.DeleteMenuBtn_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Informations
            // 
            this.Informations.Controls.Add(this.searchMenuBtn);
            this.Informations.Controls.Add(this.label45);
            this.Informations.Controls.Add(this.label46);
            this.Informations.Controls.Add(this.PriceTextBox);
            this.Informations.Controls.Add(this.MenuIDTextBox);
            this.Informations.Controls.Add(this.MenuNameTextBox);
            this.Informations.Controls.Add(this.label44);
            resources.ApplyResources(this.Informations, "Informations");
            this.Informations.Name = "Informations";
            this.Informations.TabStop = false;
            // 
            // searchMenuBtn
            // 
            resources.ApplyResources(this.searchMenuBtn, "searchMenuBtn");
            this.searchMenuBtn.Name = "searchMenuBtn";
            this.searchMenuBtn.UseVisualStyleBackColor = true;
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.Name = "label45";
            // 
            // label46
            // 
            resources.ApplyResources(this.label46, "label46");
            this.label46.Name = "label46";
            // 
            // PriceTextBox
            // 
            resources.ApplyResources(this.PriceTextBox, "PriceTextBox");
            this.PriceTextBox.Name = "PriceTextBox";
            // 
            // MenuIDTextBox
            // 
            resources.ApplyResources(this.MenuIDTextBox, "MenuIDTextBox");
            this.MenuIDTextBox.Name = "MenuIDTextBox";
            // 
            // MenuNameTextBox
            // 
            resources.ApplyResources(this.MenuNameTextBox, "MenuNameTextBox");
            this.MenuNameTextBox.Name = "MenuNameTextBox";
            // 
            // label44
            // 
            resources.ApplyResources(this.label44, "label44");
            this.label44.Name = "label44";
            // 
            // Actions
            // 
            this.Actions.Controls.Add(this.showAllMenuBtn);
            this.Actions.Controls.Add(this.btnCancel);
            this.Actions.Controls.Add(this.BtnAdd);
            this.Actions.Controls.Add(this.DeleteMenuBtn);
            this.Actions.Controls.Add(this.UpdateMenuBtn);
            this.Actions.Controls.Add(this.btnSave);
            resources.ApplyResources(this.Actions, "Actions");
            this.Actions.Name = "Actions";
            this.Actions.TabStop = false;
            // 
            // MenuSetup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Actions);
            this.Controls.Add(this.Informations);
            this.Controls.Add(this.MenuDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuSetup";
            this.Load += new System.EventHandler(this.MenuSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MenuDataGridView)).EndInit();
            this.Informations.ResumeLayout(false);
            this.Informations.PerformLayout();
            this.Actions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView MenuDataGridView;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button UpdateMenuBtn;
        private System.Windows.Forms.Button showAllMenuBtn;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button DeleteMenuBtn;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn RestMenuID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RestMenuName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RestPrice;
        private System.Windows.Forms.GroupBox Informations;
        private System.Windows.Forms.Button searchMenuBtn;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox PriceTextBox;
        private System.Windows.Forms.TextBox MenuIDTextBox;
        private System.Windows.Forms.TextBox MenuNameTextBox;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.GroupBox Actions;
    }
}