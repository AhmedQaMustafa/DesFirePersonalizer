using DesFirePersonalizer.Apps_Cood;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesFirePersonalizer
{
    public partial class AssignPermitions : Form
    {
        private DataTable dt;
        DatabaseProvider Provider = new DatabaseProvider();
        string GridQuery = "Select UsrLoginID,UsrPassword,UsrFullname,UsrDesc,UsrExpireDate,UsrStatus,UsrPermission From AppUsers ";
        string PermissionScreen = DatabaseProvider.UsrPermission;
        DataTable dtLeft;
        DataTable dtRight;
        private object grdLeftGrid;

        public AssignPermitions()
        {
            InitializeComponent();
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridData(string pWhere)
        {
            return dt = DBFun.FetchData(GridQuery + " " + pWhere);
        }
        private DataTable FillData()
        {
            return dt = DBFun.FetchData(GridQuery);
        }
        private DataTable FillUserData()
        {
            string GridQuery1 = "select UsrLoginID From AppUsers";

            return dt = DBFun.FetchData(GridQuery1);
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
            btnAdd.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            btnEdit.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            btnDelete.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));
            btnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString()));
            btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));
            //std.= Convert.ToBoolean(Convert.ToInt32(pBtn[5].ToString()));

            if (pBtn[0] != '0') { btnAdd.Enabled = PermissionScreen.Contains("AddUsr"); }
            if (pBtn[1] != '0') { btnEdit.Enabled = PermissionScreen.Contains("UpdateUsr"); }
            if (pBtn[2] != '0') { btnDelete.Enabled = PermissionScreen.Contains("DeleteUsr"); }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchData()
        {
            if (cmbSearch.Text == "All")
            {
                FirstUsersCheckList.DataSource = FillGridData("");
            }
            else if (cmbSearch.Text == "Login ID")
            {
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { FirstUsersCheckList.DataSource = FillGridData(" WHERE UsrLoginID LIKE '%" + TxtSearch.Text + "%'"); } else { FirstUsersCheckList.DataSource = FillGridData("Where UserID = -1");  }
            }
            else if (cmbSearch.Text == "User Name")
            {
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { FirstUsersCheckList.DataSource = FillGridData(" WHERE UsrFullname LIKE '%" + TxtSearch.Text + "%'"); } else { FirstUsersCheckList.DataSource = FillGridData("Where UserID = -1"); }
            }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void ItemDataStatus(char pType, bool pADD, bool pEdit)
        {
            if (pType == 'E' || pType == 'B') // E=Enabled , B = Enabled&Clear
            {
                TxtSearch.Enabled = pEdit;
                FirstUsersCheckList.Enabled = pADD;
                btnSearch.Enabled = pADD;
                SecondUsersCheckList.Enabled = pADD;
                btnSend.Enabled = pADD;
                btnForwred.Enabled = pADD;
                AddPermisionsGroup.Enabled = pADD;

            }

            if (pType == 'C' || pType == 'B')  // C=Clear
            {
                TxtSearch.Text = "";
                FirstUsersCheckList.Text = "";
                SecondUsersCheckList.Text = "";
                //CeckBoxStatus.Checked = false;
                //ClearPermissionItem();
                //ExpiryDate.Text = DateTime.Now.ToString();
                //TxtUserDescription.Enabled = pADD;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.Text == "All")
            {
                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                btnSearch.Enabled = true;
                dt = FillUserData();
                FirstUsersCheckList.DataSource = dt;
                FirstUsersCheckList.ValueMember = "UsrLoginID";
                FirstUsersCheckList.SelectedItem = "UsrLoginID";
                FirstUsersCheckList.Enabled = true;
            }
            else if (cmbSearch.Text == "Login ID" || cmbSearch.Text == "Login Name")
            {
                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                btnSearch.Enabled = true;
               // FirstUsersCheckList.DataSource = FillGridData("Where UserID = -1");
                //UserGridView.ClearSelection();
            }
            CommandStatus("10000");
           // ItemDataStatus('E', false, false);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //protected DataTable EmptyDataTable()
        //{
        //    DataTable Emptydt = new DataTable();
        //    Emptydt.Columns.Add(new DataColumn("UsrLoginID", typeof(string)));
        //    Emptydt.Columns.Add(new DataColumn("EmpID", typeof(string)));
        //    return Emptydt;
        //}
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
            ItemDataStatus('B', false, false);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSend_Click(object sender, EventArgs e)
        {

            SecondUsersCheckList.Items.Add(FirstUsersCheckList.SelectedValue);
           // DataRow dr = 
            
           //FirstUsersCheckList.Items.Remove(FirstUsersCheckList.SelectedItem(dt));

            //for (int i = 0; i < FirstUsersCheckList.Items.Count; i++)
            //{
            //    if (FirstUsersCheckList.Items.Contains(i) )
            //    {
            //        FirstUsersCheckList.Items.RemoveAt(i);
            //    }
            //}
            //
        }

        private void FirstUsersCheckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SecondUsersCheckList.Items.Add(FirstUsersCheckList.Items[index]);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < FirstUsersCheckList.Items.Count; i++)
                {
                    FirstUsersCheckList.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < FirstUsersCheckList.Items.Count; i++)
                {
                    FirstUsersCheckList.SetItemChecked(i, false);
                }
            }
        }

        private void chbSelectAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void PermitionsGroups_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
