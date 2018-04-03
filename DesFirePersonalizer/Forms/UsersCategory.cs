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
    public partial class UsersCategory1 : Form
    {
        int ID = 0;
        DatabaseProvider CatProvider = new DatabaseProvider();
        string PermissionScreen = DatabaseProvider.UsrPermission;
        string commandAction = "";
        string GridQuery = "SELECT  CatName , CatCode , CatDesc FROM CatogryType";
        private DataTable dt;
        DatabaseProvider Provider = new DatabaseProvider();
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UsersCategory1()
        {
            InitializeComponent();
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UsersCategory_Load(object sender, EventArgs e)
        {
            CommandStatus("10000");
            // TODO: This line of code loads data into the 'oneCard_SolutionDataSet1.CatogryType' table. You can move, or remove it, as needed.
            this.catogryTypeTableAdapter.Fill(this.oneCard_SolutionDataSet1.CatogryType);

        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
            btnAdd.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            btnUpdate.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            btnDelete.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));
            btnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString()));
            btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));
            //std.= Convert.ToBoolean(Convert.ToInt32(pBtn[5].ToString()));

            if (pBtn[0] != '0') { btnAdd.Enabled = PermissionScreen.Contains("AddUsr"); }
            if (pBtn[1] != '0') { btnUpdate.Enabled = PermissionScreen.Contains("UpdateUsr"); }
            if (pBtn[2] != '0') { btnDelete.Enabled = PermissionScreen.Contains("DeleteUsr"); }
        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ClearData()
        {
            TxtCatName.Text = "";
            TxtCatCode.Text = "";
            TxtCatDesc.Text = "";
            TxtSearch.Text = "";
            // ID = 0;
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridData(string pWhere)
        {
            return dt = DBFun.FetchData(GridQuery + " " + pWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SelectRowWithValue(DataGridView UserGridView, string pValue)
        {
            UserGridViewCat.ClearSelection();
            foreach (DataGridViewRow row in UserGridView.Rows)
            {
                if (row.Cells[1].Value == null) { continue; }
                if (row.Cells[1].Value.ToString().Equals(pValue))
                {
                    row.Selected = true; return;
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchData()
        {
            if (cmbSearch.Text == "All")
            {
                UserGridViewCat.DataSource = FillGridData("");
            }
            else if (cmbSearch.Text == "Category Name")
            {
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { UserGridViewCat.DataSource = FillGridData(" WHERE CatName LIKE '%" + TxtSearch.Text + "%'"); } else { UserGridViewCat.DataSource = FillGridData("Where CatId = -1"); UserGridViewCat.ClearSelection(); }
            }
            else if (cmbSearch.Text == "Category ShortCut")
            {
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { UserGridViewCat.DataSource = FillGridData(" WHERE CatCode LIKE '%" + TxtSearch.Text + "%'"); } else { UserGridViewCat.DataSource = FillGridData("Where CatId = -1"); UserGridViewCat.ClearSelection(); }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void ItemDataStatus(char pType, bool pADD, bool pEdit)
        {
            if (pType == 'E' || pType == 'B') // E=Enabled , B = Enabled&Clear
            {
                TxtCatName.Enabled = pEdit;
                TxtCatCode.Enabled = pADD;
                TxtCatDesc.Enabled = pADD;
            }
            if (pType == 'C' || pType == 'B')  // C=Clear
            {
                TxtCatDesc.Text = "";
                TxtCatCode.Text = "";
                TxtCatName.Text = "";
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void InsertCat_Click(object sender, EventArgs e)
        {
            //string strPermission = ReadPermissionItem();
            try
            {
                if (commandAction == "Add")
                {
                    if (TxtCatName.Text != "")
                    {
                        dt = FillGridData("WHERE CatName = '" + TxtCatName.Text + " ' ");
                        //string CatName = dt.Rows[0]["CatName"].ToString();
                        if (DBFun.IsNullOrEmpty(dt))
                        {
                            //if (TxtCatName.Text == CatName) { MessageBox.Show("User Name Already Exist Please select anothe user name", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                            Provider.InsertNewCategory(TxtCatName.Text, TxtCatCode.Text, TxtCatDesc.Text);
                            CommandStatus("11100");
                            TxtSearch.Enabled = true;
                            UserGridViewCat.Enabled = true;
                            ClearData();
                            SearchData();
                            ItemDataStatus('E', false, false);
                            commandAction = "";
                            MessageBox.Show("User Added Succesfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SelectRowWithValue(UserGridViewCat, TxtCatName.Text);
                            // UserGridView.DataSource = Provider.Louad_GridView(TxtSearch.Text);
                        }
                        else
                        {
                            MessageBox.Show("Student ID Already Exist Please find another id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                if (commandAction == "Edit")
                {
                    if (TxtCatName.Text != "")
                    {
                        Provider.UpdateCategory(TxtCatName.Text, TxtCatCode.Text, TxtCatDesc.Text);
                        CommandStatus("11100");
                        TxtSearch.Enabled = true;
                        UserGridViewCat.Enabled = true;
                        SearchData();
                        ItemDataStatus('E', false, false);
                        commandAction = "";
                        MessageBox.Show("Updated Done Successfuly", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SelectRowWithValue(UserGridViewCat, TxtCatName.Text);
                    }
                    else
                    {
                        MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void EditbtnCat_Click(object sender, EventArgs e)
        {
            commandAction = "Edit";
            CommandStatus("00011");
            TxtSearch.Enabled = false;
            UserGridViewCat.Enabled = false;
            ItemDataStatus('E', true, false);

        }
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void CancelbtnCat_Click(object sender, EventArgs e)
        {
            TxtCatName.Text = "";
            TxtCatCode.Text = "";
            TxtCatDesc.Text = "";
            //TxtUserName.Text = "";
            btnDelete.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;


            if (commandAction == "Add")
            {
                CommandStatus("10000"); ItemDataStatus('B', false, false);
            }
            if (commandAction == "Edit")
            {
                CommandStatus("11000"); ItemDataStatus('E', false, false); returnData();
            }
            TxtSearch.Enabled = true;
            UserGridViewCat.Enabled = true;
            commandAction = "";

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void returnData()
        {
            DataRow vDataRow = GetCurrentRow(UserGridViewCat);
            object[] vData = vDataRow.ItemArray;
            TxtCatName.Text = vData[1].ToString();
            TxtCatCode.Text = vData[2].ToString();
            TxtCatDesc.Text = vData[4].ToString();
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            UserGridViewCat.DataSource = CatProvider.LouadCat_GridView(TxtSearch.Text);
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserGridViewCat_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TxtCatName.Text = UserGridViewCat.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtCatCode.Text = UserGridViewCat.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtCatDesc.Text = UserGridViewCat.Rows[e.RowIndex].Cells[2].Value.ToString();
            btnDelete.Enabled = true;
            btnCancel.Enabled = true;
            btnUpdate.Enabled = true;
            btnAdd.Enabled = false;
            btnSave.Enabled = false;
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DeletebtnCat_Click(object sender, EventArgs e)
        {

            if (ID != -1)
            {

                CatProvider.DeleteCategoryQuery(TxtCatName.Text);

                MessageBox.Show("Record Deleted Successfully!");
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private void SearchBtnCat_Click(object sender, EventArgs e)
        {
            SearchData();
            ItemDataStatus('B', false, false);
            UserGridViewCat.DataSource = CatProvider.LouadCat_GridView(TxtSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            commandAction = "Add";
            CommandStatus("00011");
            TxtSearch.Enabled = false;
            UserGridViewCat.Enabled = false;
            UserGridViewCat.ClearSelection();
            ItemDataStatus('B', true, true);
        }

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.Text == "All")
            {
                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                SearchBtnCat.Enabled = true;
                UserGridViewCat.DataSource = FillGridData("");
                UserGridViewCat.ClearSelection();
            }
            else if (cmbSearch.Text == "Category Name" || cmbSearch.Text == "Category ShortCut")
            {
                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                SearchBtnCat.Enabled = true;
                UserGridViewCat.DataSource = FillGridData("Where CatId = -1");
                UserGridViewCat.ClearSelection();
            }
            CommandStatus("10000");
            ItemDataStatus('B', false, false);
        }
    }
}
