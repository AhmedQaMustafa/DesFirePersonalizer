using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport;
using DesFirePersonalizer.Apps_Cood;

namespace DesFirePersonalizer
{
    public partial class CardTemplatesForm : Form
    {

        private DataTable CatDt;
        private DataTable dt;
        //string GridQueryCat = "Select CatId,CatName,CatCode,CatDesc From CatogryType ";

        public CardTemplatesForm()
        {
            InitializeComponent();
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void CardTemplatesForm_Load(object sender, EventArgs e)
        {
            FillCategory();
            FillGridTemplateCard();
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region print Cards

        private DataTable FillCategoryData()
        {

            string GridQuery1 = "SELECT 0 as CatId,'Please select any value'  as CatName, null as CatCode UNION ALL SELECT CatId , CatName , CatCode FROM  CatogryType";

            return CatDt = DBFun.FetchData(GridQuery1);
        }

        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillCategory()
        {
            CatDt = FillCategoryData();
            Catcombbox.ValueMember = "CatName";
            Catcombbox.DataSource = CatDt;
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //public string getEmpType()
    //{

    //    string sCategory = string.Empty;
    //    ComboboxItem empItem = (ComboboxItem)cmbEmpCat.SelectedItem;
    //    string value = empItem.Value;

    //    if (value == "Emp") { sCategory = "'Emp','Oth'"; }
    //    //if (value == "ST") { sCategory = "'ST','OST'"; }
    //    //if (value == "HS") { sCategory = "'HS','OHS'"; }
    //    return sCategory;
    //}
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillGridTemplateCard()
    {
        //FillType(pp);
        dt = DBFun.FetchData(" SELECT TmpID,TmpName,EmpType,TmpType FROM CardTemplate ");//WHERE TmpType = '" + Setting.CardType + "'
            dvgTemplateCard.DataSource = dt;
    }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //private void FillType(bool pp)
        //{
        //    string CatType = "";
        //    if (Setting.Type == "CardTemplate") { CatType = "'Emp','HS','ST'"; } else if (Setting.Type == "VCardTemplate") { CatType = "'VCat'"; }

        //    if (pp) { try { cmbTmpType.Items.Clear(); } catch { } }

        //    if (CatType != "")
        //    {
        //        DataTable Typedt = DBFun.FetchData("SELECT TCatNameEn,TCatNameAr,TCatValue FROM CategoryTypes WHERE TCatType IN( " + CatType + " ) ORDER BY TCatID");
        //        if (!DBFun.IsNullOrEmpty(Typedt)) { return; }
        //        cmbTmpType.DataSource = Typedt;
        //        cmbTmpType.DisplayMember = "TCatName" + Setting.Lang;
        //        cmbTmpType.ValueMember = "TCatValue";
        //        cmbTmpType.SelectedValue = -1;
        //    }
        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void bntAddTmp_Click(object sender, EventArgs e)
    {

        try
        {
            if (string.IsNullOrEmpty(txtTmpName.Text)) { MessageBox.Show("Please enter Template Name","Error"); return; }
            if (string.IsNullOrEmpty(txtTmpPath.Text)) { MessageBox.Show("Please Select Valid Path", "Error"); return; }
            if (string.IsNullOrEmpty(Catcombbox.Text) && Catcombbox.Text == "Please select any value") { MessageBox.Show("Please select Template Type", "Error"); return; }

            dt = DBFun.FetchData(" SELECT TmpID FROM CardTemplate WHERE TmpName = '" + txtTmpName.Text + "'AND TmpType = '" + Setting.CardType + "'");//
            if (!DBFun.IsNullOrEmpty(dt)) { MessageBox.Show("Template Name already Exist", "Error"); return; }
            FastReport.Report rpt = new FastReport.Report();
            rpt.Load(txtTmpPath.Text);
            string repstr = rpt.SaveToString();
            repstr = repstr.Replace("'", "''");
            string QI = "INSERT INTO CardTemplate(TmpName,TmpStringDef,TmpStringEdited,TmpType) VALUES ('" + txtTmpName.Text + "','" + repstr + "','" + repstr + "','" + Catcombbox.Text+ "')";
            //MessageBox.Show(QI.ToString());

            int res = DBFun.ExecuteData(QI);
            General.ShowMsg("Template added successfully", "تمت إضافة النموذج");
            FillGridTemplateCard();

            txtTmpName.Text = "";
            txtTmpPath.Text = "";
        }
        catch (Exception EX) { General.ShowErr("You can not Add the Template, please contact your system administrator", "لا يمكن إضافة النموذج,الرجاء الاتصال بمدير النظام"); }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void btnUpdTmp_Click(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(txtTmpName.Text) && !String.IsNullOrEmpty(txtTmpID.Text) && !String.IsNullOrEmpty(txtTmpPath.Text))
            {
                FastReport.Report rpt = new FastReport.Report();
                rpt.Load(txtTmpPath.Text);
                string repstr = rpt.SaveToString();
                repstr = repstr.Replace("'", "''");
                string QU = "UPDATE CardTemplate SET TmpName='" + txtTmpName.Text + "',TmpStringDef='" + repstr + "',TmpStringEdited ='" + repstr + "', TmpType='" + Catcombbox.Text + "' WHERE TmpID=" + txtTmpID.Text + "";
                int res = DBFun.ExecuteData(QU);
                General.ShowMsg("Template Updated successfully", "تم تحديث النموذج");
                FillGridTemplateCard();
            }
            else { General.ShowMsg("Please Select Template", "الرجاء إختيار نموذج"); }
        }
        catch (Exception EX) { General.ShowErr("You can not update the Template, please contact your system administrator", "لا يمكن تحديث النموذج,الرجاء الاتصال بمدير النظام"); }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void btnEditTmp_Click(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(txtTmpName.Text) && !String.IsNullOrEmpty(txtTmpID.Text))
            {
                FastReport.Report rpt = new FastReport.Report();
                dt = DBFun.FetchData(" SELECT TmpStringEdited FROM CardTemplate WHERE TmpID = " + txtTmpID.Text + "");
                rpt.LoadFromString(dt.Rows[0]["TmpStringEdited"].ToString());
                rpt.Design();
            }
            else { General.ShowMsg("Please Select Template", "الرجاء إختيار نموذج"); }
        }
        catch (Exception EX) { General.ShowErr("You can not Load the Template, please contact your system administrator", "لا يمكن تحميل النموذج,الرجاء الاتصال بمدير النظام"); }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void btnDefTmp_Click(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(txtTmpName.Text) && !String.IsNullOrEmpty(txtTmpID.Text))
            {
                dt = DBFun.FetchData(" SELECT TmpStringDef FROM CardTemplate WHERE TmpID = " + txtTmpID.Text + "");
                if (DBFun.IsNullOrEmpty(dt)) { return; }

                string repstr = dt.Rows[0]["TmpStringDef"].ToString();
                repstr = repstr.Replace("'", "''");
                // string QU = "UPDATE CardTemplate SET TmpStringEdited ='" + repstr + "' WHERE TmpID = " + TmpID + "";
                // int res = DBFun.ExecuteData(QU);
                General.ShowMsg("Template Updated successfully", "تم إعادة النموذج الإفتراضي");//Tr
                FillGridTemplateCard();
            }
            else { General.ShowMsg("Please Select Template", "الرجاء إختيار نموذج"); }
        }
        //Tr
        catch (Exception EX) { General.ShowErr("You can not update the Template, please contact your system administrator", "لا يمكن إعادة النموذج الإفتراضي,الرجاء الاتصال بمدير النظام"); }
    }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //private void catco_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    txtTmpID.Text = "";
        //    if (Setting.Type == "CardPrint") { /**/  FillGridPrintCard(""); /**/  } //CreateCheckBoxHeader();

        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnTmpPath_Click(object sender, EventArgs e)
    {
        ofdReport.AddExtension = true;
        ofdReport.Title = "Select ID Card Template";
        ofdReport.DefaultExt = "*.frx";
        ofdReport.InitialDirectory = "C:\\";
        ofdReport.Filter = "Files(*.frx)|*.frx";
        ofdReport.FileName = "";

        if (ofdReport.ShowDialog() == System.Windows.Forms.DialogResult.OK) { txtTmpPath.Text = ofdReport.FileName; } //else { return false; }
    }
       //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void dvgTemplateCard_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        int iTmpID = dvgTemplateCard.Columns.IndexOf(dvgTemplateCard.Columns["TmpID2"]);
        int iType = dvgTemplateCard.Columns.IndexOf(dvgTemplateCard.Columns["EmpType"]);
        int iTypeName = dvgTemplateCard.Columns.IndexOf(dvgTemplateCard.Columns["TypeName"]);
        int iTmpName = dvgTemplateCard.Columns.IndexOf(dvgTemplateCard.Columns["TmpName"]);

        if (e.RowIndex != -1)
        {
            txtTmpID.Text = dvgTemplateCard[iTmpID, e.RowIndex].Value.ToString();
            // cmbTmpType.Text = dvgTemplateCard[iTypeName, e.RowIndex].Value.ToString();
            txtTmpName.Text = dvgTemplateCard[iTmpName, e.RowIndex].Value.ToString();
            txtTmpPath.Text = "";
        }
    }
      //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
