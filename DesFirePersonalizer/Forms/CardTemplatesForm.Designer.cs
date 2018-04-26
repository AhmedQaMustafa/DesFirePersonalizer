namespace DesFirePersonalizer
{
    partial class CardTemplatesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardTemplatesForm));
            FastReport.Design.DesignerSettings designerSettings1 = new FastReport.Design.DesignerSettings();
            FastReport.Design.DesignerRestrictions designerRestrictions1 = new FastReport.Design.DesignerRestrictions();
            FastReport.Export.Email.EmailSettings emailSettings1 = new FastReport.Export.Email.EmailSettings();
            FastReport.PreviewSettings previewSettings1 = new FastReport.PreviewSettings();
            FastReport.ReportSettings reportSettings1 = new FastReport.ReportSettings();
            this.pnlTemplateCard = new System.Windows.Forms.Panel();
            this.grbInsRep = new System.Windows.Forms.GroupBox();
            this.pnlTmpType = new System.Windows.Forms.Panel();
            this.Catcombbox = new System.Windows.Forms.ComboBox();
            this.lblTmpType = new System.Windows.Forms.Label();
            this.btnDefTmp = new System.Windows.Forms.Button();
            this.txtTmpID = new System.Windows.Forms.TextBox();
            this.bntAddTmp = new System.Windows.Forms.Button();
            this.btnTmpPath = new System.Windows.Forms.Button();
            this.txtTmpPath = new System.Windows.Forms.TextBox();
            this.btnUpdTmp = new System.Windows.Forms.Button();
            this.txtTmpName = new System.Windows.Forms.TextBox();
            this.btnEditTmp = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.dvgTemplateCard = new System.Windows.Forms.DataGridView();
            this.envSettingsReportCard = new FastReport.EnvironmentSettings();
            this.ofdReport = new System.Windows.Forms.OpenFileDialog();
            this.TmpID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTemplateCard.SuspendLayout();
            this.grbInsRep.SuspendLayout();
            this.pnlTmpType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgTemplateCard)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTemplateCard
            // 
            resources.ApplyResources(this.pnlTemplateCard, "pnlTemplateCard");
            this.pnlTemplateCard.Controls.Add(this.grbInsRep);
            this.pnlTemplateCard.Controls.Add(this.dvgTemplateCard);
            this.pnlTemplateCard.Name = "pnlTemplateCard";
            // 
            // grbInsRep
            // 
            resources.ApplyResources(this.grbInsRep, "grbInsRep");
            this.grbInsRep.Controls.Add(this.pnlTmpType);
            this.grbInsRep.Controls.Add(this.btnDefTmp);
            this.grbInsRep.Controls.Add(this.txtTmpID);
            this.grbInsRep.Controls.Add(this.bntAddTmp);
            this.grbInsRep.Controls.Add(this.btnTmpPath);
            this.grbInsRep.Controls.Add(this.txtTmpPath);
            this.grbInsRep.Controls.Add(this.btnUpdTmp);
            this.grbInsRep.Controls.Add(this.txtTmpName);
            this.grbInsRep.Controls.Add(this.btnEditTmp);
            this.grbInsRep.Controls.Add(this.label16);
            this.grbInsRep.Controls.Add(this.label17);
            this.grbInsRep.Name = "grbInsRep";
            this.grbInsRep.TabStop = false;
            // 
            // pnlTmpType
            // 
            resources.ApplyResources(this.pnlTmpType, "pnlTmpType");
            this.pnlTmpType.Controls.Add(this.Catcombbox);
            this.pnlTmpType.Controls.Add(this.lblTmpType);
            this.pnlTmpType.Name = "pnlTmpType";
            // 
            // Catcombbox
            // 
            resources.ApplyResources(this.Catcombbox, "Catcombbox");
            this.Catcombbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Catcombbox.FormattingEnabled = true;
            this.Catcombbox.Name = "Catcombbox";
            // 
            // lblTmpType
            // 
            resources.ApplyResources(this.lblTmpType, "lblTmpType");
            this.lblTmpType.Name = "lblTmpType";
            // 
            // btnDefTmp
            // 
            resources.ApplyResources(this.btnDefTmp, "btnDefTmp");
            this.btnDefTmp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDefTmp.Name = "btnDefTmp";
            this.btnDefTmp.UseVisualStyleBackColor = false;
            this.btnDefTmp.Click += new System.EventHandler(this.btnDefTmp_Click);
            // 
            // txtTmpID
            // 
            resources.ApplyResources(this.txtTmpID, "txtTmpID");
            this.txtTmpID.Name = "txtTmpID";
            // 
            // bntAddTmp
            // 
            resources.ApplyResources(this.bntAddTmp, "bntAddTmp");
            this.bntAddTmp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bntAddTmp.ForeColor = System.Drawing.Color.Black;
            this.bntAddTmp.Name = "bntAddTmp";
            this.bntAddTmp.UseVisualStyleBackColor = false;
            this.bntAddTmp.Click += new System.EventHandler(this.bntAddTmp_Click);
            // 
            // btnTmpPath
            // 
            resources.ApplyResources(this.btnTmpPath, "btnTmpPath");
            this.btnTmpPath.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnTmpPath.ForeColor = System.Drawing.Color.Black;
            this.btnTmpPath.Name = "btnTmpPath";
            this.btnTmpPath.UseVisualStyleBackColor = false;
            this.btnTmpPath.Click += new System.EventHandler(this.btnTmpPath_Click);
            // 
            // txtTmpPath
            // 
            resources.ApplyResources(this.txtTmpPath, "txtTmpPath");
            this.txtTmpPath.Name = "txtTmpPath";
            // 
            // btnUpdTmp
            // 
            resources.ApplyResources(this.btnUpdTmp, "btnUpdTmp");
            this.btnUpdTmp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUpdTmp.ForeColor = System.Drawing.Color.Black;
            this.btnUpdTmp.Name = "btnUpdTmp";
            this.btnUpdTmp.UseVisualStyleBackColor = false;
            this.btnUpdTmp.Click += new System.EventHandler(this.btnUpdTmp_Click);
            // 
            // txtTmpName
            // 
            resources.ApplyResources(this.txtTmpName, "txtTmpName");
            this.txtTmpName.Name = "txtTmpName";
            // 
            // btnEditTmp
            // 
            resources.ApplyResources(this.btnEditTmp, "btnEditTmp");
            this.btnEditTmp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnEditTmp.ForeColor = System.Drawing.Color.Black;
            this.btnEditTmp.Name = "btnEditTmp";
            this.btnEditTmp.UseVisualStyleBackColor = false;
            this.btnEditTmp.Click += new System.EventHandler(this.btnEditTmp_Click);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // dvgTemplateCard
            // 
            resources.ApplyResources(this.dvgTemplateCard, "dvgTemplateCard");
            this.dvgTemplateCard.AllowUserToAddRows = false;
            this.dvgTemplateCard.AllowUserToOrderColumns = true;
            this.dvgTemplateCard.AllowUserToResizeColumns = false;
            this.dvgTemplateCard.AllowUserToResizeRows = false;
            this.dvgTemplateCard.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dvgTemplateCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dvgTemplateCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TmpID2,
            this.Type,
            this.TmpName,
            this.TypeName});
            this.dvgTemplateCard.Name = "dvgTemplateCard";
            this.dvgTemplateCard.ReadOnly = true;
            this.dvgTemplateCard.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dvgTemplateCard.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dvgTemplateCard_CellMouseClick);
            // 
            // envSettingsReportCard
            // 
            designerSettings1.ApplicationConnection = null;
            designerSettings1.DefaultFont = new System.Drawing.Font("Arial", 10F);
            designerSettings1.Icon = null;
            designerSettings1.Restrictions = designerRestrictions1;
            designerSettings1.Text = "";
            this.envSettingsReportCard.DesignerSettings = designerSettings1;
            emailSettings1.Address = "";
            emailSettings1.Host = "";
            emailSettings1.MessageTemplate = "";
            emailSettings1.Name = "";
            emailSettings1.Password = "";
            emailSettings1.UserName = "";
            this.envSettingsReportCard.EmailSettings = emailSettings1;
            previewSettings1.Buttons = FastReport.PreviewButtons.Close;
            previewSettings1.Icon = ((System.Drawing.Icon)(resources.GetObject("previewSettings1.Icon")));
            previewSettings1.Text = "";
            this.envSettingsReportCard.PreviewSettings = previewSettings1;
            reportSettings1.ShowProgress = false;
            this.envSettingsReportCard.ReportSettings = reportSettings1;
            this.envSettingsReportCard.UIStyle = FastReport.Utils.UIStyle.Office2007Blue;
            // 
            // ofdReport
            // 
            this.ofdReport.FileName = "openFileDialog1";
            resources.ApplyResources(this.ofdReport, "ofdReport");
            // 
            // TmpID2
            // 
            this.TmpID2.DataPropertyName = "TmpID";
            resources.ApplyResources(this.TmpID2, "TmpID2");
            this.TmpID2.Name = "TmpID2";
            this.TmpID2.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "EmpType";
            resources.ApplyResources(this.Type, "Type");
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // TmpName
            // 
            this.TmpName.DataPropertyName = "TmpName";
            resources.ApplyResources(this.TmpName, "TmpName");
            this.TmpName.Name = "TmpName";
            this.TmpName.ReadOnly = true;
            // 
            // TypeName
            // 
            this.TypeName.DataPropertyName = "TmpType";
            resources.ApplyResources(this.TypeName, "TypeName");
            this.TypeName.Name = "TypeName";
            this.TypeName.ReadOnly = true;
            // 
            // CardTemplatesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTemplateCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CardTemplatesForm";
            this.Load += new System.EventHandler(this.CardTemplatesForm_Load);
            this.pnlTemplateCard.ResumeLayout(false);
            this.grbInsRep.ResumeLayout(false);
            this.grbInsRep.PerformLayout();
            this.pnlTmpType.ResumeLayout(false);
            this.pnlTmpType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgTemplateCard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTemplateCard;
        private System.Windows.Forms.GroupBox grbInsRep;
        private System.Windows.Forms.Button btnDefTmp;
        private System.Windows.Forms.TextBox txtTmpID;
        private System.Windows.Forms.Button bntAddTmp;
        private System.Windows.Forms.Button btnTmpPath;
        private System.Windows.Forms.TextBox txtTmpPath;
        private System.Windows.Forms.Button btnUpdTmp;
        private System.Windows.Forms.TextBox txtTmpName;
        private System.Windows.Forms.Button btnEditTmp;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridView dvgTemplateCard;
        private FastReport.EnvironmentSettings envSettingsReportCard;
        private System.Windows.Forms.OpenFileDialog ofdReport;
        private System.Windows.Forms.Panel pnlTmpType;
        private System.Windows.Forms.ComboBox Catcombbox;
        private System.Windows.Forms.Label lblTmpType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TmpID2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn TmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeName;
    }
}