namespace DesFirePersonalizer.Purchasing_Pages
{
    partial class FrmEItems
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
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.DisplayPriceTextBox = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label49 = new System.Windows.Forms.Label();
            this.DisplayStatusTextBox = new System.Windows.Forms.TextBox();
            this.DisplayMenuNameListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ApplicationDataEPurse = new System.Windows.Forms.TextBox();
            this.TransactionIDEPurse = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.Controls.Add(this.DisplayPriceTextBox, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.label47, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.label48, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.button1, 1, 3);
            this.tableLayoutPanel11.Controls.Add(this.label49, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.DisplayStatusTextBox, 1, 2);
            this.tableLayoutPanel11.Controls.Add(this.DisplayMenuNameListBox, 1, 1);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(23, 12);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 3;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(1208, 550);
            this.tableLayoutPanel11.TabIndex = 5;
            // 
            // DisplayPriceTextBox
            // 
            this.DisplayPriceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPriceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayPriceTextBox.Location = new System.Drawing.Point(107, 3);
            this.DisplayPriceTextBox.Name = "DisplayPriceTextBox";
            this.DisplayPriceTextBox.ReadOnly = true;
            this.DisplayPriceTextBox.Size = new System.Drawing.Size(1143, 49);
            this.DisplayPriceTextBox.TabIndex = 3;
            // 
            // label47
            // 
            this.label47.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(3, 153);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(85, 31);
            this.label47.TabIndex = 0;
            this.label47.Text = "Menu";
            // 
            // label48
            // 
            this.label48.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(3, 12);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(81, 31);
            this.label48.TabIndex = 1;
            this.label48.Text = "Price";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.button1.Location = new System.Drawing.Point(598, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 65);
            this.button1.TabIndex = 4;
            this.button1.Text = "BUY";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label49
            // 
            this.label49.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(3, 294);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(98, 31);
            this.label49.TabIndex = 0;
            this.label49.Text = "Status";
            // 
            // DisplayStatusTextBox
            // 
            this.DisplayStatusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayStatusTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F);
            this.DisplayStatusTextBox.Location = new System.Drawing.Point(107, 285);
            this.DisplayStatusTextBox.Name = "DisplayStatusTextBox";
            this.DisplayStatusTextBox.ReadOnly = true;
            this.DisplayStatusTextBox.Size = new System.Drawing.Size(1143, 49);
            this.DisplayStatusTextBox.TabIndex = 5;
            // 
            // DisplayMenuNameListBox
            // 
            this.DisplayMenuNameListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayMenuNameListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.DisplayMenuNameListBox.FormattingEnabled = true;
            this.DisplayMenuNameListBox.ItemHeight = 31;
            this.DisplayMenuNameListBox.Location = new System.Drawing.Point(107, 58);
            this.DisplayMenuNameListBox.Name = "DisplayMenuNameListBox";
            this.DisplayMenuNameListBox.Size = new System.Drawing.Size(1143, 221);
            this.DisplayMenuNameListBox.TabIndex = 6;
            this.DisplayMenuNameListBox.SelectedIndexChanged += new System.EventHandler(this.DisplayMenuNameListBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(601, 578);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Application Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(317, 574);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "TransAction ID";
            // 
            // ApplicationDataEPurse
            // 
            this.ApplicationDataEPurse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ApplicationDataEPurse.Location = new System.Drawing.Point(692, 574);
            this.ApplicationDataEPurse.MaxLength = 24;
            this.ApplicationDataEPurse.Name = "ApplicationDataEPurse";
            this.ApplicationDataEPurse.Size = new System.Drawing.Size(182, 20);
            this.ApplicationDataEPurse.TabIndex = 11;
            this.ApplicationDataEPurse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TransactionIDEPurse
            // 
            this.TransactionIDEPurse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TransactionIDEPurse.Location = new System.Drawing.Point(401, 575);
            this.TransactionIDEPurse.MaxLength = 8;
            this.TransactionIDEPurse.Name = "TransactionIDEPurse";
            this.TransactionIDEPurse.Size = new System.Drawing.Size(182, 20);
            this.TransactionIDEPurse.TabIndex = 10;
            this.TransactionIDEPurse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmEItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 607);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ApplicationDataEPurse);
            this.Controls.Add(this.TransactionIDEPurse);
            this.Controls.Add(this.tableLayoutPanel11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmEItems";
            this.Text = "E_Items";
            this.Load += new System.EventHandler(this.FrmEItems_Load);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TextBox DisplayPriceTextBox;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox DisplayStatusTextBox;
        private System.Windows.Forms.ListBox DisplayMenuNameListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ApplicationDataEPurse;
        private System.Windows.Forms.TextBox TransactionIDEPurse;
    }
}