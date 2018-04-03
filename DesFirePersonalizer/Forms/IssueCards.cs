using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesFirePersonalizer.Apps_Cood;

namespace DesFirePersonalizer
{
    public partial class IssueCards : Form
    {

        public IssueCards()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable Empdt;
        private DataTable Stdt;

        string PermissionScreen = DatabaseProvider.UsrPermission;
        string EmpGridQuery = "Select UsrLoginID,UsrPassword,UsrFullname,UsrDesc,UsrExpireDate,UsrStatus,UsrPermission,UserCategory,UserGroup From AppUsers ";
        string StGridQuery = " SELECT ID,StudentID From StudentsTable ";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region fill data 

        private DataTable FillGridData(string pWhere)
        {
            return Empdt = DBFun.FetchData(EmpGridQuery + " " + pWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillStudentData(string StpWhere)
        {
            return Stdt = DBFun.FetchData(StGridQuery + " " + StpWhere);
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
            btnSearch.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            btnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));

            if (pBtn[0] != '0') { btnSave.Enabled = PermissionScreen.Contains("AddUsr"); }
            if (pBtn[1] != '0') { btnCancel.Enabled = PermissionScreen.Contains("UpdateUsr"); }
            if (pBtn[2] != '0') { btnSearch.Enabled = PermissionScreen.Contains("DeleteUsr"); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //protected void ItemDataStatus(char pType, bool pADD, bool pEdit)
        //{
        //    if (pType == 'E' || pType == 'B') // E=Enabled , B = Enabled&Clear
        //    {
        //        TxtSearch.Enabled = pEdit;
        //        //  pnlPermission.Enabled = pEdit;
        //        CombPermitions.Enabled = pEdit;
        //        Catcombbox.Enabled = pEdit;
        //        TxtUserName.Enabled = pADD;
        //        TxtPassword.Enabled = pADD;
        //        CeckBoxStatus.Enabled = pADD;
        //        //pnlPermission.Enabled = pADD;
        //        ExpiryDate.Enabled = pADD;
        //        TxtUserDescription.Enabled = pADD;

        //    }

        //    if (pType == 'C' || pType == 'B')  // C=Clear
        //    {
        //        TxtLoginID.Text = "";
        //        TxtUserName.Text = "";
        //        TxtPassword.Text = "";
        //        CeckBoxStatus.Checked = false;
        //        ClearPermissionItem();
        //        ExpiryDate.Text = DateTime.Now.ToString();
        //        TxtUserDescription.Enabled = pADD;
        //    }
        //}

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {


            CommandStatus("10000");
            //ItemDataStatus('B', false, false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbSearch.Text == "Employee ID")
            {
                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                btnSearch.Enabled = true;
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { CardsGridData.DataSource = FillStudentData(" WHERE UserID LIKE '%" + TxtSearch.Text + "%'"); } else { CardsGridData.DataSource = FillGridData("Where UserID = -1"); CardsGridData.ClearSelection(); }

                //CardsGridData.DataSource = FillGridData("Where UserID = -1");
                //CardsGridData.ClearSelection();
            }
            else if (cmbSearch.Text == "Student ID")
            {

                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                btnSearch.Enabled = true;
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { CardsGridData.DataSource = FillStudentData(" WHERE StudentID LIKE '%" + TxtSearch.Text + "%'"); } else { CardsGridData.DataSource = FillStudentData("Where ID = -1"); CardsGridData.ClearSelection();}
                //CardsGridData.DataSource = FillGridData("Where UserID = -1");
                //CardsGridData.ClearSelection();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
