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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.SettingsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.CardReaderComboBox = new System.Windows.Forms.ComboBox();
            this.SettingsTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsTableLayoutPanel
            // 
            resources.ApplyResources(this.SettingsTableLayoutPanel, "SettingsTableLayoutPanel");
            this.SettingsTableLayoutPanel.Controls.Add(this.CardReaderComboBox, 1, 0);
            this.SettingsTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.SettingsTableLayoutPanel.Name = "SettingsTableLayoutPanel";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // CardReaderComboBox
            // 
            resources.ApplyResources(this.CardReaderComboBox, "CardReaderComboBox");
            this.CardReaderComboBox.FormattingEnabled = true;
            this.CardReaderComboBox.Name = "CardReaderComboBox";
            this.CardReaderComboBox.SelectedIndexChanged += new System.EventHandler(this.CardReaderComboBox_SelectedIndexChanged);
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SettingsTableLayoutPanel);
            this.Name = "SettingsForm";
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