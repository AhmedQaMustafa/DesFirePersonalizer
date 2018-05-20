namespace DesFirePersonalizer.Wizered_Pages
{
    partial class OCRUserControl
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
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.ChooseScannerbtn = new System.Windows.Forms.Button();
            this.SCDevCombox = new System.Windows.Forms.ComboBox();
            this.OCRPicturebox = new System.Windows.Forms.PictureBox();
            this.ocrtxtar = new System.Windows.Forms.TextBox();
            this.OcrTextbox = new System.Windows.Forms.TextBox();
            this.openfiledialogbtn = new System.Windows.Forms.Button();
            this.ReadBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.OCRPicturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Location = new System.Drawing.Point(29, 367);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(147, 37);
            this.BtnUpdate.TabIndex = 27;
            this.BtnUpdate.Text = "Update";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(193, 368);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(197, 33);
            this.button4.TabIndex = 26;
            this.button4.Text = "Save";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // ChooseScannerbtn
            // 
            this.ChooseScannerbtn.Location = new System.Drawing.Point(223, 23);
            this.ChooseScannerbtn.Name = "ChooseScannerbtn";
            this.ChooseScannerbtn.Size = new System.Drawing.Size(108, 23);
            this.ChooseScannerbtn.TabIndex = 25;
            this.ChooseScannerbtn.Text = "Choose Scanner";
            this.ChooseScannerbtn.UseVisualStyleBackColor = true;
            // 
            // SCDevCombox
            // 
            this.SCDevCombox.FormattingEnabled = true;
            this.SCDevCombox.Location = new System.Drawing.Point(32, 26);
            this.SCDevCombox.Name = "SCDevCombox";
            this.SCDevCombox.Size = new System.Drawing.Size(170, 21);
            this.SCDevCombox.TabIndex = 24;
            // 
            // OCRPicturebox
            // 
            this.OCRPicturebox.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.OCRPicturebox.Location = new System.Drawing.Point(34, 54);
            this.OCRPicturebox.Name = "OCRPicturebox";
            this.OCRPicturebox.Size = new System.Drawing.Size(506, 250);
            this.OCRPicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OCRPicturebox.TabIndex = 23;
            this.OCRPicturebox.TabStop = false;
            // 
            // ocrtxtar
            // 
            this.ocrtxtar.Location = new System.Drawing.Point(575, 180);
            this.ocrtxtar.Multiline = true;
            this.ocrtxtar.Name = "ocrtxtar";
            this.ocrtxtar.Size = new System.Drawing.Size(343, 103);
            this.ocrtxtar.TabIndex = 21;
            // 
            // OcrTextbox
            // 
            this.OcrTextbox.Location = new System.Drawing.Point(572, 59);
            this.OcrTextbox.Multiline = true;
            this.OcrTextbox.Name = "OcrTextbox";
            this.OcrTextbox.Size = new System.Drawing.Size(343, 106);
            this.OcrTextbox.TabIndex = 22;
            // 
            // openfiledialogbtn
            // 
            this.openfiledialogbtn.Location = new System.Drawing.Point(39, 323);
            this.openfiledialogbtn.Name = "openfiledialogbtn";
            this.openfiledialogbtn.Size = new System.Drawing.Size(88, 23);
            this.openfiledialogbtn.TabIndex = 20;
            this.openfiledialogbtn.Text = "Choose Image";
            this.openfiledialogbtn.UseVisualStyleBackColor = true;
            // 
            // ReadBtn
            // 
            this.ReadBtn.Location = new System.Drawing.Point(142, 323);
            this.ReadBtn.Name = "ReadBtn";
            this.ReadBtn.Size = new System.Drawing.Size(85, 24);
            this.ReadBtn.TabIndex = 19;
            this.ReadBtn.Text = "Scan";
            this.ReadBtn.UseVisualStyleBackColor = true;
            // 
            // OCRUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ChooseScannerbtn);
            this.Controls.Add(this.SCDevCombox);
            this.Controls.Add(this.OCRPicturebox);
            this.Controls.Add(this.ocrtxtar);
            this.Controls.Add(this.OcrTextbox);
            this.Controls.Add(this.openfiledialogbtn);
            this.Controls.Add(this.ReadBtn);
            this.Name = "OCRUserControl";
            this.Size = new System.Drawing.Size(1096, 494);
            ((System.ComponentModel.ISupportInitialize)(this.OCRPicturebox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button ChooseScannerbtn;
        private System.Windows.Forms.ComboBox SCDevCombox;
        private System.Windows.Forms.PictureBox OCRPicturebox;
        private System.Windows.Forms.TextBox ocrtxtar;
        private System.Windows.Forms.TextBox OcrTextbox;
        private System.Windows.Forms.Button openfiledialogbtn;
        private System.Windows.Forms.Button ReadBtn;
    }
}
