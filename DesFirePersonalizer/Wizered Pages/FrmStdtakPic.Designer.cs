namespace DesFirePersonalizer.Wizered_Pages
{
    partial class FrmStdtakPic
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
            this.StdPictureboxEdit = new System.Windows.Forms.PictureBox();
            this.ImageCaptured = new System.Windows.Forms.PictureBox();
            this.StdPictureBox = new System.Windows.Forms.PictureBox();
            this.BtnBrowseImage = new System.Windows.Forms.Button();
            this.Cancelbtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.CamerasInfocomb = new System.Windows.Forms.ComboBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtStdIdedit = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.StdPictureboxEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageCaptured)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StdPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // StdPictureboxEdit
            // 
            this.StdPictureboxEdit.Location = new System.Drawing.Point(3, -6);
            this.StdPictureboxEdit.Name = "StdPictureboxEdit";
            this.StdPictureboxEdit.Size = new System.Drawing.Size(461, 378);
            this.StdPictureboxEdit.TabIndex = 77;
            this.StdPictureboxEdit.TabStop = false;
            // 
            // ImageCaptured
            // 
            this.ImageCaptured.Enabled = false;
            this.ImageCaptured.Image = global::DesFirePersonalizer.Properties.Resources.images__2_;
            this.ImageCaptured.Location = new System.Drawing.Point(35, 117);
            this.ImageCaptured.Name = "ImageCaptured";
            this.ImageCaptured.Size = new System.Drawing.Size(461, 378);
            this.ImageCaptured.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageCaptured.TabIndex = 79;
            this.ImageCaptured.TabStop = false;
            // 
            // StdPictureBox
            // 
            this.StdPictureBox.Image = global::DesFirePersonalizer.Properties.Resources.images__2_;
            this.StdPictureBox.Location = new System.Drawing.Point(688, 117);
            this.StdPictureBox.Name = "StdPictureBox";
            this.StdPictureBox.Size = new System.Drawing.Size(461, 378);
            this.StdPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StdPictureBox.TabIndex = 78;
            this.StdPictureBox.TabStop = false;
            // 
            // BtnBrowseImage
            // 
            this.BtnBrowseImage.Location = new System.Drawing.Point(537, 321);
            this.BtnBrowseImage.Name = "BtnBrowseImage";
            this.BtnBrowseImage.Size = new System.Drawing.Size(116, 66);
            this.BtnBrowseImage.TabIndex = 84;
            this.BtnBrowseImage.Text = "Browse";
            this.BtnBrowseImage.UseVisualStyleBackColor = true;
            this.BtnBrowseImage.Click += new System.EventHandler(this.BtnBrowseImage_Click);
            // 
            // Cancelbtn
            // 
            this.Cancelbtn.Location = new System.Drawing.Point(537, 420);
            this.Cancelbtn.Name = "Cancelbtn";
            this.Cancelbtn.Size = new System.Drawing.Size(114, 58);
            this.Cancelbtn.TabIndex = 83;
            this.Cancelbtn.Text = "Cancel";
            this.Cancelbtn.UseVisualStyleBackColor = true;
            this.Cancelbtn.Click += new System.EventHandler(this.Cancelbtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(537, 138);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(116, 69);
            this.StartBtn.TabIndex = 82;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // CamerasInfocomb
            // 
            this.CamerasInfocomb.FormattingEnabled = true;
            this.CamerasInfocomb.Location = new System.Drawing.Point(512, 52);
            this.CamerasInfocomb.Name = "CamerasInfocomb";
            this.CamerasInfocomb.Size = new System.Drawing.Size(211, 21);
            this.CamerasInfocomb.TabIndex = 81;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(537, 228);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(116, 65);
            this.btnCapture.TabIndex = 80;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(432, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "Select Source";
            // 
            // TxtStdIdedit
            // 
            this.TxtStdIdedit.Location = new System.Drawing.Point(78, 3);
            this.TxtStdIdedit.Name = "TxtStdIdedit";
            this.TxtStdIdedit.Size = new System.Drawing.Size(78, 20);
            this.TxtStdIdedit.TabIndex = 86;
            // 
            // FrmStdtakPic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1329, 551);
            this.Controls.Add(this.TxtStdIdedit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnBrowseImage);
            this.Controls.Add(this.Cancelbtn);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.CamerasInfocomb);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.ImageCaptured);
            this.Controls.Add(this.StdPictureBox);
            this.Controls.Add(this.StdPictureboxEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmStdtakPic";
            this.Text = "FrmStdtakPic";
            this.Load += new System.EventHandler(this.FrmStdtakPic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StdPictureboxEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageCaptured)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StdPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox StdPictureboxEdit;
        private System.Windows.Forms.PictureBox ImageCaptured;
        private System.Windows.Forms.PictureBox StdPictureBox;
        private System.Windows.Forms.Button BtnBrowseImage;
        private System.Windows.Forms.Button Cancelbtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.ComboBox CamerasInfocomb;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtStdIdedit;
    }
}