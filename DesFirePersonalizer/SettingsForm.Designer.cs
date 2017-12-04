namespace DesFirePersonalizer
{
    partial class SettingsForm
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
            this.SettingsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.CardReaderComboBox = new System.Windows.Forms.ComboBox();
            this.SettingsTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsTableLayoutPanel
            // 
            this.SettingsTableLayoutPanel.ColumnCount = 2;
            this.SettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.SettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.SettingsTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.SettingsTableLayoutPanel.Controls.Add(this.CardReaderComboBox, 1, 0);
            this.SettingsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.SettingsTableLayoutPanel.Name = "SettingsTableLayoutPanel";
            this.SettingsTableLayoutPanel.RowCount = 2;
            this.SettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SettingsTableLayoutPanel.Size = new System.Drawing.Size(520, 46);
            this.SettingsTableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Card Reader";
            // 
            // CardReaderComboBox
            // 
            this.CardReaderComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardReaderComboBox.FormattingEnabled = true;
            this.CardReaderComboBox.Location = new System.Drawing.Point(76, 3);
            this.CardReaderComboBox.MinimumSize = new System.Drawing.Size(250, 0);
            this.CardReaderComboBox.Name = "CardReaderComboBox";
            this.CardReaderComboBox.Size = new System.Drawing.Size(441, 21);
            this.CardReaderComboBox.TabIndex = 1;
            this.CardReaderComboBox.SelectedIndexChanged += new System.EventHandler(this.CardReaderComboBox_SelectedIndexChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 46);
            this.Controls.Add(this.SettingsTableLayoutPanel);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.SettingsTableLayoutPanel.ResumeLayout(false);
            this.SettingsTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel SettingsTableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CardReaderComboBox;
    }
}