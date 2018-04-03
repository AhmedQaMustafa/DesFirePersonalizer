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
            FastReport.Design.DesignerSettings designerSettings2 = new FastReport.Design.DesignerSettings();
            FastReport.Design.DesignerRestrictions designerRestrictions2 = new FastReport.Design.DesignerRestrictions();
            FastReport.Export.Email.EmailSettings emailSettings2 = new FastReport.Export.Email.EmailSettings();
            FastReport.PreviewSettings previewSettings2 = new FastReport.PreviewSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardTemplatesForm));
            FastReport.ReportSettings reportSettings2 = new FastReport.ReportSettings();
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
            this.pnlTemplateCard.Controls.Add(this.grbInsRep);
            this.pnlTemplateCard.Controls.Add(this.dvgTemplateCard);
            this.pnlTemplateCard.Location = new System.Drawing.Point(157, 34);
            this.pnlTemplateCard.Name = "pnlTemplateCard";
            this.pnlTemplateCard.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pnlTemplateCard.Size = new System.Drawing.Size(855, 409);
            this.pnlTemplateCard.TabIndex = 40;
            // 
            // grbInsRep
            // 
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
            this.grbInsRep.Location = new System.Drawing.Point(13, 11);
            this.grbInsRep.Name = "grbInsRep";
            this.grbInsRep.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grbInsRep.Size = new System.Drawing.Size(838, 109);
            this.grbInsRep.TabIndex = 8;
            this.grbInsRep.TabStop = false;
            this.grbInsRep.Text = "Templates";
            // 
            // pnlTmpType
            // 
            this.pnlTmpType.Controls.Add(this.Catcombbox);
            this.pnlTmpType.Controls.Add(this.lblTmpType);
            this.pnlTmpType.Location = new System.Drawing.Point(559, 51);
            this.pnlTmpType.Name = "pnlTmpType";
            this.pnlTmpType.Size = new System.Drawing.Size(273, 52);
            this.pnlTmpType.TabIndex = 41;
            // 
            // Catcombbox
            // 
            this.Catcombbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Catcombbox.FormattingEnabled = true;
            this.Catcombbox.Location = new System.Drawing.Point(48, 4);
            this.Catcombbox.Name = "Catcombbox";
            this.Catcombbox.Size = new System.Drawing.Size(221, 21);
            this.Catcombbox.TabIndex = 31;
//            this.Catcombbox.SelectedIndexChanged += new System.EventHandler(this.Catcombbox_SelectedIndexChanged);
            // 
            // lblTmpType
            // 
            this.lblTmpType.AutoSize = true;
            this.lblTmpType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTmpType.Location = new System.Drawing.Point(6, 9);
            this.lblTmpType.Name = "lblTmpType";
            this.lblTmpType.Size = new System.Drawing.Size(37, 13);
            this.lblTmpType.TabIndex = 32;
            this.lblTmpType.Text = "Type :";
            // 
            // btnDefTmp
            // 
            this.btnDefTmp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDefTmp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDefTmp.Location = new System.Drawing.Point(405, 22);
            this.btnDefTmp.Name = "btnDefTmp";
            this.btnDefTmp.Size = new System.Drawing.Size(159, 22);
            this.btnDefTmp.TabIndex = 25;
            this.btnDefTmp.Text = "Restore Defult";
            this.btnDefTmp.UseVisualStyleBackColor = false;
            this.btnDefTmp.Click += new System.EventHandler(this.btnDefTmp_Click);
            // 
            // txtTmpID
            // 
            this.txtTmpID.Enabled = false;
            this.txtTmpID.Location = new System.Drawing.Point(658, 22);
            this.txtTmpID.Name = "txtTmpID";
            this.txtTmpID.Size = new System.Drawing.Size(170, 20);
            this.txtTmpID.TabIndex = 5;
            this.txtTmpID.Visible = false;
            // 
            // bntAddTmp
            // 
            this.bntAddTmp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bntAddTmp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bntAddTmp.ForeColor = System.Drawing.Color.Black;
            this.bntAddTmp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bntAddTmp.Location = new System.Drawing.Point(8, 21);
            this.bntAddTmp.Name = "bntAddTmp";
            this.bntAddTmp.Size = new System.Drawing.Size(120, 23);
            this.bntAddTmp.TabIndex = 9;
            this.bntAddTmp.Text = "Add Template";
            this.bntAddTmp.UseVisualStyleBackColor = false;
            this.bntAddTmp.Click += new System.EventHandler(this.bntAddTmp_Click);
            // 
            // btnTmpPath
            // 
            this.btnTmpPath.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnTmpPath.ForeColor = System.Drawing.Color.Black;
            this.btnTmpPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTmpPath.Location = new System.Drawing.Point(520, 77);
            this.btnTmpPath.Name = "btnTmpPath";
            this.btnTmpPath.Size = new System.Drawing.Size(30, 23);
            this.btnTmpPath.TabIndex = 7;
            this.btnTmpPath.Text = "...";
            this.btnTmpPath.UseVisualStyleBackColor = false;
            this.btnTmpPath.Click += new System.EventHandler(this.btnTmpPath_Click);
            // 
            // txtTmpPath
            // 
            this.txtTmpPath.Enabled = false;
            this.txtTmpPath.Location = new System.Drawing.Point(118, 77);
            this.txtTmpPath.Name = "txtTmpPath";
            this.txtTmpPath.Size = new System.Drawing.Size(396, 20);
            this.txtTmpPath.TabIndex = 6;
            // 
            // btnUpdTmp
            // 
            this.btnUpdTmp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUpdTmp.ForeColor = System.Drawing.Color.Black;
            this.btnUpdTmp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUpdTmp.Location = new System.Drawing.Point(143, 22);
            this.btnUpdTmp.Name = "btnUpdTmp";
            this.btnUpdTmp.Size = new System.Drawing.Size(120, 23);
            this.btnUpdTmp.TabIndex = 8;
            this.btnUpdTmp.Text = "UpdateTemplate";
            this.btnUpdTmp.UseVisualStyleBackColor = false;
            this.btnUpdTmp.Click += new System.EventHandler(this.btnUpdTmp_Click);
            // 
            // txtTmpName
            // 
            this.txtTmpName.Location = new System.Drawing.Point(118, 51);
            this.txtTmpName.Name = "txtTmpName";
            this.txtTmpName.Size = new System.Drawing.Size(432, 20);
            this.txtTmpName.TabIndex = 4;
            // 
            // btnEditTmp
            // 
            this.btnEditTmp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnEditTmp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEditTmp.ForeColor = System.Drawing.Color.Black;
            this.btnEditTmp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditTmp.Location = new System.Drawing.Point(279, 22);
            this.btnEditTmp.Name = "btnEditTmp";
            this.btnEditTmp.Size = new System.Drawing.Size(120, 23);
            this.btnEditTmp.TabIndex = 7;
            this.btnEditTmp.Text = "Edit Templates";
            this.btnEditTmp.UseVisualStyleBackColor = false;
            this.btnEditTmp.Click += new System.EventHandler(this.btnEditTmp_Click);
            // 
            // label16
            // 
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(4, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Template Path  : ";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(0, 55);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(97, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Template Name :   ";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dvgTemplateCard
            // 
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
            this.dvgTemplateCard.Location = new System.Drawing.Point(14, 126);
            this.dvgTemplateCard.Name = "dvgTemplateCard";
            this.dvgTemplateCard.ReadOnly = true;
            this.dvgTemplateCard.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dvgTemplateCard.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dvgTemplateCard.Size = new System.Drawing.Size(837, 272);
            this.dvgTemplateCard.TabIndex = 2;
            this.dvgTemplateCard.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dvgTemplateCard_CellMouseClick);
            // 
            // envSettingsReportCard
            // 
            designerSettings2.ApplicationConnection = null;
            designerSettings2.DefaultFont = new System.Drawing.Font("Arial", 10F);
            designerSettings2.Icon = null;
            designerSettings2.Restrictions = designerRestrictions2;
            designerSettings2.Text = "";
            this.envSettingsReportCard.DesignerSettings = designerSettings2;
            emailSettings2.Address = "";
            emailSettings2.Host = "";
            emailSettings2.MessageTemplate = "";
            emailSettings2.Name = "";
            emailSettings2.Password = "";
            emailSettings2.UserName = "";
            this.envSettingsReportCard.EmailSettings = emailSettings2;
            previewSettings2.Buttons = FastReport.PreviewButtons.Close;
            previewSettings2.Icon = ((System.Drawing.Icon)(resources.GetObject("previewSettings2.Icon")));
            previewSettings2.Text = "";
            this.envSettingsReportCard.PreviewSettings = previewSettings2;
            reportSettings2.ShowProgress = false;
            this.envSettingsReportCard.ReportSettings = reportSettings2;
            this.envSettingsReportCard.UIStyle = FastReport.Utils.UIStyle.Office2007Blue;
            // 
            // ofdReport
            // 
            this.ofdReport.FileName = "openFileDialog1";
            // 
            // TmpID2
            // 
            this.TmpID2.DataPropertyName = "TmpID";
            this.TmpID2.HeaderText = "Template ID";
            this.TmpID2.Name = "TmpID2";
            this.TmpID2.ReadOnly = true;
            this.TmpID2.Width = 150;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "EmpType";
            this.Type.HeaderText = "EmpType";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Visible = false;
            // 
            // TmpName
            // 
            this.TmpName.DataPropertyName = "TmpName";
            this.TmpName.HeaderText = "Template Name";
            this.TmpName.Name = "TmpName";
            this.TmpName.ReadOnly = true;
            this.TmpName.Width = 300;
            // 
            // TypeName
            // 
            this.TypeName.DataPropertyName = "TmpType";
            this.TypeName.HeaderText = "Type";
            this.TypeName.Name = "TypeName";
            this.TypeName.ReadOnly = true;
            this.TypeName.Width = 200;
            // 
            // CardTemplatesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 537);
            this.Controls.Add(this.pnlTemplateCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CardTemplatesForm";
            this.Text = "CardTemplatesForm";
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