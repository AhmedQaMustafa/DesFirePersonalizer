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

namespace DesFirePersonalizer.Wizered_Pages
{
    public partial class StdPrintCard_Pag : UserControl, IWizardPage
    {
        public StdPrintCard_Pag()
        {
            InitializeComponent();
        }
        DataTable dt;
        #region IWizardPage Members

        public UserControl Content
        {
            get { return this; }
        }

        public new void Load()
        {

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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                dt = DBFun.FetchData("SELECT TmpStringEdited FROM CardTemplate WHERE TmpID = " + TmpID + "");
                if (!DBFun.IsNullOrEmpty(dt)) { General.ShowMsg("Template not available", "النموذج غير موجود"); return; }
                rpt.LoadFromString(dt.Rows[0]["TmpStringEdited"].ToString());
                rpt.SetParameterValue("cardid", tcard.Text);
                rpt.Dictionary.Connections[0].ConnectionString = General.ConnStringDB();
                rpt.Show();
            }
            catch (Exception EX) { MessageBox.Show( "You can not preview the card, please contact your system administrator", "لا يمكن مشاهدة البطاقة,الرجاء الاتصال بمدير النظام"); }
        }

        private void btnPrintCard_Click(object sender, EventArgs e)
        {
            try
            {
                //if (dvgPrintCard.Rows.Count < 1) { return; }
                DataRow dr = GetCurrentRow(dvgPrintCard);
                if (dr == null) { General.ShowMsg("please select card", "يجب اختيار بطاقة"); return; }
                object[] vData = dr.ItemArray;
                if (string.IsNullOrEmpty(tcard.Text) || string.IsNullOrEmpty(tTemplate.Text)) { return; }
                string CardID = tcard.Text; //vData[1].ToString(); // (string)dvgPrintCard[1, g].EditedFormattedValue;
                string TmpID = tTemplate.Text; //(string)dvgPrintCard[2, g].EditedFormattedValue;
                DataTable Tdt = DBFun.FetchData("SELECT TmpStringEdited FROM CardTemplate WHERE TmpID = " + TmpID + "");
                if (!DBFun.IsNullOrEmpty(Tdt)) { General.ShowMsg("Template not available", "النموذج غير موجود"); return; }

                FastReport.Report rpt = new FastReport.Report();
                rpt.LoadFromString(Tdt.Rows[0]["TmpStringEdited"].ToString());
                rpt.SetParameterValue("cardid", CardID);
                //rpt.PrintSettings.Printer = "HDP5000 Card Printer";
                rpt.PrintSettings.ShowDialog = false;
                rpt.Dictionary.Connections[0].ConnectionString = General.ConnStringDB();

                string UQ = "";
                if (Setting.Type == "CardPrint")
                {
                    // Update all CardStatus for employee to InActive 
                    DBFun.ExecuteData("UPDATE CardMaster Set CardStatus = 3 WHERE CardStatus IN (1,2) AND EmpID = '" + tEmpID.Text + "' AND CardID != " + CardID + "");

                   // UQ = "UPDATE CardMaster SET CardStatus = 2, isPrinted = 1,PrintedBy = '" + Setting.UserID + "',PrintedDate = '" + PrintDate + "' WHERE CardID = " + CardID + ""; //
                }

                int res = DBFun.ExecuteData(UQ);
                rpt.Print();


                //FillGridPrintCard("");
            }
            catch (Exception EX)
            {
            }
            //{ General.ShowErr(EX.ToString(), "You can not print the card, please contact your system administrator", "لا يمكن طباعة البطاقة,الرجاء الاتصال بمدير النظام"); }
            //finally { FillGridPrintCard(""); }
        }

    }
}
