using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesFirePersonalizer.Wizered_Pages;
using SimpleWizard;
using DesFirePersonalizer.Apps_Cood;
using FastReport.Utils;
using FastReport;

namespace DesFirePersonalizer.Wizered_Pages
{
    public partial class StdPrintCard_Pag : UserControl, IWizardPage
    {
        public StdPrintCard_Pag()
        {
            InitializeComponent();
        }
        DataTable dt;
        private DataTable CMPrdt;
        private string DateFormat = "MM/dd/yyyy";

        string CardMasterPrintQuery = " SELECT CardID ,TmpID,StudentID,TmpType ,IsID ,STDFirstName,STDSecondName,STDFamilyName,IsNameEn,TmpType From CardInfoView ";
        #region IWizardPage Members

        public UserControl Content
        {
            get { return this; }
        }

        public new void Load()
        {
            txtStudentPrintID.Text = DatabaseProvider.StedentID;
            dvgPrintCard.DataSource = FillGridPrintCardData(" WHERE StudentID LIKE '%" + txtStudentPrintID.Text + "%'");
        }

        public void Save()
        {
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public bool IsBusy
        {
            get { throw new NotImplementedException(); }
        }

        public bool PageValid
        {
            get { return true; }
        }

        public string ValidationMessage
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridPrintCardData(string pWhere)
        {
            return CMPrdt = DBFun.FetchData(CardMasterPrintQuery + " " + pWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillGridPrintCard(string pID)
        {

            CMPrdt = DBFun.FetchData(CardMasterPrintQuery + " " + pID);
            dvgPrintCard.DataSource = CMPrdt;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        #region Print Card
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void CreateCheckBoxHeader()
        {
            CheckBox checkboxHeader = new CheckBox();
            Rectangle rect = dvgPrintCard.GetCellDisplayRectangle(0, -1, true);
            rect.X = rect.Location.X + (rect.Width / 4);
            //rect.X = rect.Location.X + (rect.Width4);
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(16, 16);
            checkboxHeader.BackColor = ColorTranslator.FromHtml("#F0F0F0");

            if (Setting.Lang == "En") { checkboxHeader.Location = new Point(50, 2); } else if (Setting.Lang == "Ar") { checkboxHeader.Location = new Point(909, 10); }
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dvgPrintCard.Controls.Add(checkboxHeader);

            dvgPrintCard.ReadOnly = false;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dvgPrintCard.RowCount; i++)
            {
                dvgPrintCard[0, i].Value = ((CheckBox)dvgPrintCard.Controls.Find("checkboxHeader", true)[0]).Checked;
            }
            dvgPrintCard.EndEdit();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //public string getValue()
        //{
        //string sCategory = string.Empty;
        //// ComboboxItem empItem = (ComboboxItem)cmbEmpCat.SelectedItem;
        ////string value = empItem.Value;

        ////if (!string.IsNullOrEmpty(txtEmpID.Text)) { if (value != "Emp") { sCategory = value; } }
        ////return sCategory;
        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSearchCard_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtStudentPrintID.Text))
            //{
            //    General.ShowMsg("Please enter Employee ID", "الرجاء إدخال رقم موظف"); return;
            //    //if (Setting.Type == "CardView") { General.ShowMsg("Please enter Card ID", "الرجاء إدخال رقم بطاقة"); return; }
            //    //else { General.ShowMsg("Please enter Employee ID", "الرجاء إدخال رقم موظف"); return; }
            //}
            //FillGridPrintCard(txtStudentPrintID.Text);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCancelCard_Click(object sender, EventArgs e)
        {
            txtStudentPrintID.Text = "";
            FillGridPrintCard("");
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnPreviewCard_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tcard.Text) || string.IsNullOrEmpty(tTemplate.Text)) { return; }
                DataRow dr = GetCurrentRow(dvgPrintCard);
                if (dr == null) { General.ShowMsg("please select card", "يجب اختيار بطاقة"); return; }
                object[] vData = dr.ItemArray;
                FastReport.Report rpt = new FastReport.Report();
                string TmpID = tTemplate.Text;
                DataTable VieDt;
                VieDt = DBFun.FetchData("SELECT TmpStringEdited FROM CardTemplate WHERE TmpID = " + TmpID + "");
                if (DBFun.IsNullOrEmpty(VieDt)) { General.ShowMsg("Template not available", "النموذج غير موجود"); return; }
                rpt.LoadFromString(VieDt.Rows[0]["TmpStringEdited"].ToString());
                rpt.SetParameterValue("cardid", tcard.Text);
                rpt.Dictionary.Connections[0].ConnectionString = SQlConnect.SQL;
                rpt.Show();
            }
            catch (Exception EX) { General.ShowErr("You can not update the Template, please contact your system administrator", "لا يمكن تحديث النموذج,الرجاء الاتصال بمدير النظام"); }
            //  catch (Exception EX) { General.ShowErr(EX.ToString(), "You can not preview the card, please contact your system administrator", "لا يمكن مشاهدة البطاقة,الرجاء الاتصال بمدير النظام"); }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnPrintCard_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GetCurrentRow(dvgPrintCard);
                if (dr == null) { General.ShowMsg("please select card", "يجب اختيار بطاقة"); return; }
                object[] vData = dr.ItemArray;


                // Thread.CurrentThread.CurrentCulture = CultureEn;
                string PrintDate = DateTime.Now.ToString(DateFormat);   //DateTime.Now.ToString("MM/dd/yyyy"); , CultureEn.DateTimeFormat


                if (string.IsNullOrEmpty(tcard.Text) || string.IsNullOrEmpty(tTemplate.Text)) { return; }
                string CardID = tcard.Text; //vData[1].ToString(); // (string)dvgPrintCard[1, g].EditedFormattedValue;
                string TmpID = tTemplate.Text; //(string)dvgPrintCard[2, g].EditedFormattedValue;
                DataTable Tdt = DBFun.FetchData("SELECT TmpStringEdited FROM CardTemplate WHERE TmpID = " + TmpID + "");
                if (DBFun.IsNullOrEmpty(Tdt)) { General.ShowMsg("Template not available", "النموذج غير موجود"); return; }

                FastReport.Report rpt = new FastReport.Report();
                rpt.LoadFromString(Tdt.Rows[0]["TmpStringEdited"].ToString());
                rpt.SetParameterValue("cardid", CardID);
                //rpt.PrintSettings.Printer = "HDP5000 Card Printer";
                rpt.PrintSettings.ShowDialog = false;
                rpt.Dictionary.Connections[0].ConnectionString = SQlConnect.SQL;

                string UQ = "";
                if (Setting.Type != "CardPrint")
                {
                    // Update all CardStatus for employee to InActive 
                    DBFun.ExecuteData("UPDATE CardMaster Set CardStatus = 3 WHERE CardStatus IN (0,1) AND StudentID = '" + tEmpID.Text + "' AND CardID != " + CardID + "");

                    UQ = "UPDATE CardMaster SET CardStatus = 2, isPrinted = 1,PrintedBy = '" + DatabaseProvider.gLoginName + "',PrintedDate = '" + PrintDate + "' WHERE CardID = " + CardID + ""; //
                }

                int res = DBFun.ExecuteData(UQ);
                rpt.Print();


                FillGridPrintCard(" WHERE StudentID LIKE '%" + tEmpID.Text + "%'");
            }
            catch (Exception EX) { General.ShowErr("You can not print the card, please contact your system administrator", "لا يمكن طباعة البطاقة,الرجاء الاتصال بمدير النظام"); }
            //catch (Exception EX) { General.ShowErr(EX.ToString(), "You can not print the card, please contact your system administrator", "لا يمكن طباعة البطاقة,الرجاء الاتصال بمدير النظام"); }
            finally { FillGridPrintCard(""); }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DataRow GetCurrentRow(DataGridView dgv)
        {
            DataRowView drv = null;
            try
            {
                if (dgv.CurrentRow == null) { return null; }
                if (dgv.CurrentRow.DataBoundItem == null) { return null; }
                drv = (DataRowView)dgv.CurrentRow.DataBoundItem;
            }
            catch { return null; }
            return drv.Row;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void dvgPrintCard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool isChecked = (bool)dvgPrintCard[0, e.RowIndex].EditedFormattedValue;
                dvgPrintCard.Rows[e.RowIndex].Cells[0].Value = !isChecked;
                dvgPrintCard.EndEdit();
            }
            catch (Exception EX) { }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void dvgPrintCard_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int iCrdID = dvgPrintCard.Columns.IndexOf(dvgPrintCard.Columns["CardID"]);
                int iTmpID = dvgPrintCard.Columns.IndexOf(dvgPrintCard.Columns["TmpID"]);
                int iEmpID = dvgPrintCard.Columns.IndexOf(dvgPrintCard.Columns["StudentID"]);

                if (e.RowIndex != -1) { tcard.Text = dvgPrintCard[iCrdID, e.RowIndex].Value.ToString(); }
                if (e.RowIndex != -1) { tTemplate.Text = dvgPrintCard[iTmpID, e.RowIndex].Value.ToString(); }
                if (e.RowIndex != -1) { tEmpID.Text = dvgPrintCard[iEmpID, e.RowIndex].Value.ToString(); }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnDeleteCrd_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GetCurrentRow(dvgPrintCard);
                if (dr == null) { General.ShowMsg("please select card", "يجب اختيار بطاقة"); return; }
                object[] vData = dr.ItemArray;

                // Thread.CurrentThread.CurrentCulture = CultureEn;
                string DeleteDate = DateTime.Now.ToString(DateFormat);   //DateTime.Now.ToString("MM/dd/yyyy");, CultureEn.DateTimeFormat

                if (string.IsNullOrEmpty(tcard.Text)) { return; }
                string CardID = tcard.Text; //vData[1].ToString(); // (string)dvgPrintCard[1, g].EditedFormattedValue;

                string UQ = "";
                if (Setting.Type == "CardPrint")
                {
                    UQ = "UPDATE CardMaster SET CardStatus = 4, CancelBy = '" + Setting.UserID + "',CancelDate = '" + DeleteDate + "' WHERE CardID = " + CardID + ""; //
                }
                try
                {
                    int res = DBFun.ExecuteData(UQ);
                    General.ShowMsg("Card Deleted", "تم حذف البطاقة");
                }
                catch { }

                FillGridPrintCard("");
            }
            catch (Exception EX) { General.ShowErr("You can not print the card, please contact your system administrator", "لا يمكن طباعة البطاقة,الرجاء الاتصال بمدير النظام"); }
            // catch (Exception EX) { General.ShowErr(EX.ToString(), "You can not print the card, please contact your system administrator", "لا يمكن طباعة البطاقة,الرجاء الاتصال بمدير النظام"); }
            //finally { FillGridPrintCard(""); }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnUpdateExpireDT_Click(object sender, EventArgs e)
        {
            //DataRow dr = GetCurrentRow(dvgPrintCard);
            //if (dr == null) { General.ShowMsg("please select card", "يجب اختيار بطاقة"); return; }
            //object[] vData = dr.ItemArray;

            ////Thread.CurrentThread.CurrentCulture = CultureEn;
            ////string DeleteDate = DateTime.Now.ToString(DateFormat, CultureEn.DateTimeFormat);   //DateTime.Now.ToString("MM/dd/yyyy");

            //if (string.IsNullOrEmpty(tcard.Text)) { return; }
            //string CardID = tcard.Text; //vData[1].ToString(); // (string)dvgPrintCard[1, g].EditedFormattedValue;

            ////ExpireDateFrm EDFrom = new ExpireDateFrm(CardID);
            ////EDFrom.ShowDialog();

        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        private void StdPrintCard_Pag_Load(object sender, EventArgs e)
        {
     
        }

    }
}
