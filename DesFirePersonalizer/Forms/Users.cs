using DesFirePersonalizer.Apps_Cood;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace DesFirePersonalizer
{
    public partial class Users : Form
    {
        StdInformations std = new StdInformations();
        DatabaseProvider Provider = new DatabaseProvider();
        SqlConnection con = new SqlConnection();
        PermitionClass UsrPerm = new PermitionClass();

        private DataTable dt;
        private DataTable dtMnu;
        private DataTable ROleDt;
        private DataTable CatDt;
    
        int ID = 0;
        string PermissionScreen = DatabaseProvider.UsrPermission;
        string commandAction = "";
        string GridQuery = "Select UserNID,UsrLoginID,UsrPassword,UsrFullname,UsrDesc,UsrExpireDate,UsrStatus,UsrPermission,UserCategory,UserGroup From AppUsers ";
        string GridQueryMnu = "Select userid,menuid From UserMenuPermitions ";
        string GridQueryRole = "Select RoleID,RoleName,RolePermissions,EmpID From RoleMaster ";
        string GridQueryCat = "Select CatId,CatName,CatCode,CatDesc From CatogryType ";

        /// //////////////////////////////////////////////////
        public Users()
        {
            InitializeComponent();

        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region fill  data table

        private DataTable FillGridData(string pWhere1)
        {
            return dt = DBFun.FetchData(GridQuery + " " + pWhere1);
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridDataMnu(string PWhere2)
        {

            return dtMnu = DBFun.FetchData(GridQueryMnu + " " + PWhere2);

        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridDataRole(string pWhere)
        {
            return ROleDt = DBFun.FetchData(GridQueryRole + " " + pWhere);
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridDataRole()
        {
            string GridQuery1 = "SELECT 0 as RoleID,'Please select any value'  as RoleName, null as RolePermissions UNION ALL SELECT RoleID, RoleName , RolePermissions FROM  RoleMaster";

            return ROleDt = DBFun.FetchData(GridQuery1);
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        private void FillPermitions()
        {
            ROleDt = FillGridDataRole();
            CombPermitions.ValueMember = "RoleName";
            CombPermitions.DataSource = ROleDt;
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        # endregion

        private void SearchData()
        {
            if (cmbSearch.Text == "All")
            {
                UserGridView.DataSource = FillGridData("");
            }
            else if (cmbSearch.Text == "Login ID")
            {
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { UserGridView.DataSource = FillGridData(" WHERE UsrLoginID LIKE '%" + TxtSearch.Text + "%'"); } else { UserGridView.DataSource = FillGridData("Where UserID = -1"); UserGridView.ClearSelection(); }
            }
            else if (cmbSearch.Text == "User Name")
            {
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { UserGridView.DataSource = FillGridData(" WHERE UsrFullname LIKE '%" + TxtSearch.Text + "%'"); } else { UserGridView.DataSource = FillGridData("Where UserID = -1"); UserGridView.ClearSelection(); }
            }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ClearData()
        {
            TxtLoginID.Text = "";
            TxtPassword.Text = "";
            TxtUserDescription.Text = "";
            TxtUserName.Text = "";
            // ID = 0;
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Users_Load(object sender, EventArgs e)
        {
            FillPermitions();
            FillCategory();
            AddPermisionsGroup.Enabled = false;
            CommandStatus("10000");
            //TxtUserID=
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillListSearch()
        {
            string[] itemText = { "All", "Login ID ", "Login Name" };
            cmbSearch.DataSource = itemText;
            if (itemText.Length > 0) { cmbSearch.SelectedIndex = 0; }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected bool FindLoginID()
        {
            if (!string.IsNullOrEmpty(TxtLoginID.Text))
            {
                dt = DBFun.FetchData("SELECT * FROM AppUsers WHERE UsrLoginID = '" + TxtLoginID.Text + "'");
                if (!DBFun.IsNullOrEmpty(dt)) { return true; } else { return false; }
            }

            return false;
        }
        /*                                                  User Permitions Code                                              */
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string[] ScreenText = { "Usr", "Std", "Cat", "Per", "Config", "Per", "Cat", "PeGr", "WalSet", "LibSet", "MenuSet", "Coun", "RedSet", "Purch", "PurchEtran", "PurchEItem", "Library", "LibraryInOu", "LibSet", "MenuSet", "Cards", "IssuCards", "IssCardsSet", "CardsSet", "TermSet", "LibraryBorrow", "Libraryreturn", "RestCards", "IssueCards" , "ReaderSettings", "WaRep", "Rep" };
        string[] PermText = { "View", "Add", "Update", "Delete", "Refresh", "Print", "Edit", "Insert", "Search", "Restore" };
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string ReadPermissionItem()
        {
            string PermStr = "";
            for (int i = 0; i < ScreenText.Length; i++)
            {
                for (int j = 0; j < PermText.Length; j++)
                {
                    string a = PermText[j];
                    string b = ScreenText[i];

                    CheckBox CurrentCheckBox = (CheckBox)AddPermisionsGroup.Controls["chb" + PermText[j] + ScreenText[i]];

                    if (CurrentCheckBox != null)
                    {
                        if (CurrentCheckBox.Checked) { PermStr += "," + PermText[j] + ScreenText[i]; }
                    }

                }
            }
            return PermStr;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillPermissionItem(string pPerm)
        {
            for (int i = 0; i < ScreenText.Length; i++)
            {
                for (int j = 0; j < PermText.Length; j++)
                {
                    CheckBox CurrentCheckBox = (CheckBox)AddPermisionsGroup.Controls["chb" + PermText[j] + ScreenText[i]];
                    if (CurrentCheckBox != null)
                    {
                        CurrentCheckBox.Checked = pPerm.Contains(PermText[j] + ScreenText[i]);
                    }

                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string[] ScreenTextMnu = { "Usr", "Std", "Cat", "Per", "Config", "Per", "Cat", "PeGr", "WalSet", "LibSet", "MenuSet", "Coun", "RedSet", "Purch", "PurchEtran", "PurchEItem", "Library", "LibraryInOu", "Cards", "IssuCards", "IssCardsSet", "CardsSet", "TermSet", "ReaderSettings", "WaRep", "Rep" };
        string[] PermTextMnu = { "View" };
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string ReadPermissionMnu()
        {
            string PermStrMnu = "";
            for (int i = 0; i < ScreenTextMnu.Length; i++)
            {
                for (int j = 0; j < PermTextMnu.Length; j++)
                {
                    string a = PermTextMnu[j];
                    string b = ScreenTextMnu[i];

                    CheckBox CurrentCheckBox = (CheckBox)AddPermisionsGroup.Controls["chb" + PermTextMnu[j] + ScreenTextMnu[i]];

                    if (CurrentCheckBox != null)
                    {
                        if (CurrentCheckBox.Checked) { PermStrMnu += "," + PermTextMnu[j] + ScreenTextMnu[i]; }
                    }
                }
            }
            return PermStrMnu;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillPermissionMnu(string pPerm)
        {
            for (int i = 0; i < ScreenTextMnu.Length; i++)
            {
                for (int j = 0; j < PermTextMnu.Length; j++)
                {
                    CheckBox CurrentCheckBox = (CheckBox)AddPermisionsGroup.Controls["chb" + PermTextMnu[j] + ScreenTextMnu[i]];
                    if (CurrentCheckBox != null)
                    {
                        CurrentCheckBox.Checked = pPerm.Contains(PermTextMnu[j] + ScreenTextMnu[i]);
                    }

                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ClearPermissionItem()
        {
            for (int i = 0; i < ScreenText.Length; i++)
            {
                for (int j = 0; j < PermText.Length; j++)
                {
                    CheckBox CurrentCheckBox = (CheckBox)AddPermisionsGroup.Controls["chb" + PermText[j] + ScreenText[i]];
          
                    if (CurrentCheckBox != null)
                    {
                        CurrentCheckBox.Checked = false;
                    }
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SelectRowWithValue(DataGridView UserGridView, string pValue)
        {
            UserGridView.ClearSelection();
            foreach (DataGridViewRow row in UserGridView.Rows)
            {
                if (row.Cells[1].Value == null) { continue; }
                if (row.Cells[1].Value.ToString().Equals(pValue))
                {
                    row.Selected = true; return;
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DataRow GetCurrentRow(DataGridView UserGrid)
        {
            DataRowView drv = null;
            try
            {
                if (UserGrid.CurrentRow == null) { return null; }
                if (UserGrid.CurrentRow.DataBoundItem == null) { return null; }
                drv = (DataRowView)UserGrid.CurrentRow.DataBoundItem;
            }
            catch { return null; }
            return drv.Row;
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    DataRow vDataRow = GetCurrentRow(UserGridView);
                    object[] vData = vDataRow.ItemArray;
                    TxtLoginID.Text = UserGridView.Rows[e.RowIndex].Cells["UsrLoginID"].Value.ToString();
                    TxtUserID.Text = UserGridView.Rows[e.RowIndex].Cells["UserNID"].Value.ToString();
                    TxtPassword.Text = UserGridView.Rows[e.RowIndex].Cells["UsrPassword"].Value.ToString();
                    TxtUserDescription.Text = UserGridView.Rows[e.RowIndex].Cells["UsrDesc"].Value.ToString();
                    TxtUserName.Text = UserGridView.Rows[e.RowIndex].Cells["UsrFullname"].Value.ToString();
                    ExpiryDate.Text = UserGridView[UserGridView.Columns.IndexOf(UserGridView.Columns["UsrExpireDate"]), e.RowIndex].Value.ToString();
                    CeckBoxStatus.Checked = Convert.ToBoolean(vData[6].ToString());
                    Catcombbox.Text = UserGridView.Rows[e.RowIndex].Cells["UserCategory"].Value.ToString();
                    CombPermitions.Text = UserGridView.Rows[e.RowIndex].Cells["UserGroup"].Value.ToString();
                    TxtUserID.Text= UserGridView.Rows[e.RowIndex].Cells["UserNID"].Value.ToString();
                    //FillPermissionItem(UserGridView[UserGridView.Columns.IndexOf(UserGridView.Columns["UsrPermission"]), e.RowIndex].Value.ToString());
                    CommandStatus("11100");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }      
    }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void button1_Click_1(object sender, EventArgs e) //savebtn
        {
            string strPermission = ReadPermissionItem();
            string strPermissionMenu = ReadPermissionMnu();
            try
            {
                dt    = FillGridData("WHERE UsrLoginID = '" + TxtSearch.Text + " ' ");
                dtMnu = FillGridDataMnu("where userid ='" + TxtUserID.Text + "'");
                if (commandAction == "Add")
                {
                    if (Catcombbox.Text != "Please select any value" || CombPermitions.Text != "Please select any value")
                   {
                        
                    if (DBFun.IsNullOrEmpty(dt) && DBFun.IsNullOrEmpty(dtMnu))
                    {
                        
                        Provider.InsertNewUser(TxtLoginID.Text,TxtUserName.Text, CeckBoxStatus.Checked.ToString(), TxtPassword.Text, TxtUserDescription.Text, ExpiryDate.Value, strPermission ,Catcombbox.Text, CombPermitions.Text, TxtUserID.Text);
                        Provider.InsertMenuPermitions(TxtUserID.Text,strPermissionMenu);
                        CommandStatus("11100");
                        TxtSearch.Enabled = true;
                        UserGridView.Enabled = true;
                        ClearItemSearch();
                        SearchData();
                        ItemDataStatus('E', false, false);
                        commandAction = "";
                        MessageBox.Show("User Added Succesfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SelectRowWithValue(UserGridView, TxtLoginID.Text);
                     
                    }
                    }                  
                    else
                    {
                        MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (commandAction == "Edit")
                    {
                    if (Catcombbox.Text != "Please select any value" || CombPermitions.Text != "Please select any value")
                    {
                        if (TxtLoginID.Text != "")
                        {
                            Provider.UpdateUsers(TxtLoginID.Text, TxtUserID.Text, TxtUserName.Text, CeckBoxStatus.Checked.ToString(), TxtPassword.Text, TxtUserDescription.Text, ExpiryDate.Value, strPermission, Catcombbox.Text, CombPermitions.Text);
                            Provider.UpdateMenuPermitions(TxtUserID.Text,strPermissionMenu);
                            CommandStatus("11100");
                            TxtSearch.Enabled = true;
                            UserGridView.Enabled = true;
                            SearchData();
                            ItemDataStatus('E', false, false);
                            commandAction = "";
                            MessageBox.Show("Updated Done Successfuly", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SelectRowWithValue(UserGridView, TxtLoginID.Text);
                        }
                        else
                        {
                            MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
            btnAdd.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            btnUpdate.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            btnDelete.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));
            btnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString()));
            btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));
            //std.= Convert.ToBoolean(Convert.ToInt32(pBtn[5].ToString()));

            if (pBtn[0] != '0') { btnAdd.Enabled    = PermissionScreen.Contains("AddUsr"); }
            if (pBtn[1] != '0') { btnUpdate.Enabled = PermissionScreen.Contains("UpdateUsr"); }
            if (pBtn[2] != '0') { btnDelete.Enabled = PermissionScreen.Contains("DeleteUsr"); }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void ClearItemSearch()
        {
            cmbSearch.SelectedIndex = 0;
            TxtSearch.Text = "";
            TxtSearch.Enabled = false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void ItemDataStatus(char pType, bool pADD, bool pEdit)
        {
            if (pType == 'E' || pType == 'B') // E=Enabled , B = Enabled&Clear
            {
                TxtLoginID.Enabled = pEdit;
                TxtUserID.Enabled = pEdit;
                CombPermitions.Enabled = pEdit;
                Catcombbox.Enabled = pEdit;
                TxtUserName.Enabled = pADD;
                TxtPassword.Enabled = pADD;
                CeckBoxStatus.Enabled = pADD;
                ExpiryDate.Enabled = pADD;
                TxtUserDescription.Enabled = pADD;

            }

            if (pType == 'C' || pType == 'B')  // C=Clear
            {
                TxtLoginID.Text = "";
                TxtUserName.Text = "";
                TxtPassword.Text = "";
                CeckBoxStatus.Checked = false;
                ClearPermissionItem();
                ExpiryDate.Text = DateTime.Now.ToString();
                TxtUserDescription.Enabled = pADD;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Deletebtn_Click(object sender, EventArgs e)
        {
               try
            {
                if (TxtLoginID.Text == "admin")
                {
                    MessageBox.Show("You are not allowed to delete admininistrator", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;  
                }
                else
                {
                    if (ID != -1)
                    {
                       
                        Provider.DeleteUsersQuery(TxtLoginID.Text);
                        MessageBox.Show("Record Deleted Successfully!");
                        //DisplayData();
                        ClearData();
                        CommandStatus("10000");
                    }
                    else
                    {
                        MessageBox.Show("Please Select Record to Delete");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Procces NoT compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (TxtLoginID.Text == "Administrator")
            {
                commandAction = "Edit";
                CommandStatus("00011");
                TxtSearch.Enabled = false;
                UserGridView.Enabled = false;
                ItemDataStatus('E', true, false);
                AddPermisionsGroup.Enabled = false;
            }
            else
            {
                commandAction = "Edit";
                CommandStatus("00011");
                TxtSearch.Enabled = false;
                UserGridView.Enabled = false;
                ItemDataStatus('E', true, true);
            }


        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void returnData()
        {
            DataRow vDataRow = GetCurrentRow(UserGridView);
            object[] vData = vDataRow.ItemArray;
            TxtLoginID.Text = vData[1].ToString();
            TxtUserName.Text = vData[2].ToString();
            TxtPassword.Text = vData[4].ToString();
            FillPermissionItem(vData[5].ToString());
            //CeckBoxStatus.Checked = Convert.ToBoolean(vData[3].ToString());
            uncheck();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            if (commandAction == "Add") { CommandStatus("10000"); ItemDataStatus('B', false, false); }
            if (commandAction == "Edit") { CommandStatus("11000"); ItemDataStatus('E', false, false); returnData(); }
            TxtSearch.Enabled = true;
            UserGridView.Enabled = true;
            FillPermitions();
            FillCategory();
            AddPermisionsGroup.Enabled = false;
            commandAction = "";

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void TxtLoginID_TextChanged(object sender, EventArgs e)
        {
            
         //  TxtLoginID.Text = "";

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            //SearchData();
            //ItemDataStatus('B', false, false);
         }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            SearchData();
            ItemDataStatus('B', false, false);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Catcombbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DatabaseProvider provider = new DatabaseProvider();
            //provider.LouadCat();

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.Text == "All")
            {
                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                SearchBtn.Enabled = true;
                UserGridView.DataSource = FillGridData("");
                UserGridView.ClearSelection();
            }
            else if (cmbSearch.Text == "Login ID" || cmbSearch.Text == "Login Name" )
            {
                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                SearchBtn.Enabled = true;
                UserGridView.DataSource = FillGridData("Where UserID = -1");
                UserGridView.ClearSelection();
            }
            CommandStatus("10000");
            ItemDataStatus('B', false, false);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            commandAction = "Add";
            CommandStatus("00011");
            FillPermitions();
            FillCategory();
            TxtSearch.Enabled = false;
            UserGridView.Enabled = false;
            UserGridView.ClearSelection();
            ItemDataStatus('B', true, true);
            AddPermisionsGroup.Enabled = false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void uncheck()
        {
            if (chbSelectAll.Checked)
            {
                foreach (Control c in AddPermisionsGroup.Controls)
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Checked = true;
                    }
                }
            }
            else
            {
                foreach (Control c in AddPermisionsGroup.Controls)
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Checked = false;
                    }
                }
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void chbSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            uncheck();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void CombPermitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CombPermitions.Text == "Please select any value")
                {
                    uncheck();
                   // pnlPermission.Enabled = true;

                }
                else
                {
                   // pnlPermission.Enabled = false;
                    dt = FillGridDataRole("WHERE RoleName = '" + CombPermitions.Text + " ' ");
                  //  dtMnu = FillGridDataMnu("WHERE RoleName = '" + CombPermitions.Text + " ' ");
                    FillPermissionItem(dt.Rows[0]["RolePermissions"].ToString());
                   // FillPermissionMnu(dt.Rows[0]["RolePermissions"].ToString());


                }

            }
            catch
            {

            }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
}
  

  


