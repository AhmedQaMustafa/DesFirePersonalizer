using DesFirePersonalizer.Apps_Cood;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesFirePersonalizer.Configurations_Pages
{
    public partial class MenuSetup : Form
    {
        public MenuSetup()
        {
            InitializeComponent();
        }
        DataTable dt;
        string GridQuery = "Select RestMenuID,RestMenuName,RestPrice From ResturantMenu ";

        string PermissionScreen = DatabaseProvider.UsrPermission;
        DatabaseProvider Provider = new DatabaseProvider();
        string commandAction = "";
        IList<Model.Menu> MenuList = null;
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  Others (Search -  Fill - Check - Get)
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MenuSetup_Load(object sender, EventArgs e)
        {
            //listAvailableMenu();
            CommandStatus("1110000");
            MenuIDTextBox.Enabled = true;
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridData(string pWhere1)
        {
            return dt = DBFun.FetchData(GridQuery + " " + pWhere1);
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void searchMenuBtn_Click(object sender, EventArgs e)
        {
            if (MenuIDTextBox.Text.Length < 1)
            {
                MessageBox.Show("ID Number must be 1 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                MenuIDTextBox.Focus();

                return;
            }
            else
            {
                SearchData();
            }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool checkFormMenuIsOk()
        {
            if (MenuIDTextBox.Text.Length < 8)
            {
                MessageBox.Show("ID Number must be 8 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //SetupBookIDTextBox.Focus();
                return false;
            }

            if (MenuNameTextBox.Text.Length < 3)
            {
                MessageBox.Show("Menu Length minimum is 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //  SetupTitleTextBox.Focus();
                return false;
            }

            if (PriceTextBox.Text.Length < 1)
            {
                MessageBox.Show("Price must not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // SetupAuthorTextBox.Focus();
                return false;
            }

            return true;
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchData()
        {
            if (MenuIDTextBox.Text != "")
            {
                if (!String.IsNullOrEmpty(MenuIDTextBox.Text)) { MenuDataGridView.DataSource = FillGridData(" WHERE RestMenuID LIKE '%" + MenuIDTextBox.Text + "%'"); } else { MenuDataGridView.DataSource = FillGridData("Where RestMenuID = -1"); MenuDataGridView.ClearSelection(); }
            }
            else
            {
                MessageBox.Show("Please Fill Search Data", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        #endregion
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Add Update Delete BTN
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void InsertMenuBtn_Click(object sender, EventArgs e)
        {
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UpdateMenuBtn_Click(object sender, EventArgs e)
        {
            CommandStatus("0001001");
            commandAction = "Edit";
            ItemDataStatus('E', true, true);
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DeleteMenuBtn_Click(object sender, EventArgs e)
        {

            try
            {
                int ID = 0;
                if (ID != -1)
                {

                    Provider.DeleteResturantMenu(MenuIDTextBox.Text);
                    MessageBox.Show("Record Deleted Successfully!");

                    CommandStatus("1110000");
                }
                else
                {
                    MessageBox.Show("Please Select Record to Delete");
                }

            }
            catch
            {
                MessageBox.Show("Procces NoT compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            commandAction = "Add";
            CommandStatus("0101001");
            MenuDataGridView.ClearSelection();
            ItemDataStatus('B', true, true);
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (commandAction == "Add") { CommandStatus("1110001"); ItemDataStatus('B', false, false); }
            if (commandAction == "Edit") { CommandStatus("1110001"); ItemDataStatus('B', false, false); }
            //MenuIDTextBox.Enabled = true;
            //MenuDataGridView.Enabled = true;
            commandAction = "";
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void showAllMenuBtn_Click(object sender, EventArgs e)
        {
            MenuDataGridView.DataSource = FillGridData("");
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (checkFormMenuIsOk() == false)
                return;

            try
            {
                dt = FillGridData("WHERE RestMenuID = '" + MenuIDTextBox.Text + " ' ");
                if (commandAction == "Add")
                {

                    if (DBFun.IsNullOrEmpty(dt))
                    {
                        Provider.insertRestMenu(MenuIDTextBox.Text, MenuNameTextBox.Text, PriceTextBox.Text);
                        CommandStatus("1000001");
                        MenuIDTextBox.Enabled = true;
                        MenuDataGridView.Enabled = true;
                        ItemDataStatus('E', false, false);
                        commandAction = "";
                        MessageBox.Show("Meal Added Succesfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("This Meal Exsiting ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (commandAction == "Edit")
                {
                    if (MenuIDTextBox.Text != "")
                    {
                        Provider.UpdateRestMenu(MenuIDTextBox.Text, MenuNameTextBox.Text, PriceTextBox.Text);
                        CommandStatus("0110101");
                        ItemDataStatus('E', false, false);
                        commandAction = "";
                        MessageBox.Show("Updated Done Successfuly", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MenuDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    DataRow vDataRow = GetCurrentRow(MenuDataGridView);
                    object[] vData = vDataRow.ItemArray;
                    MenuIDTextBox.Text = MenuDataGridView.Rows[e.RowIndex].Cells["RestMenuID"].Value.ToString();
                    MenuNameTextBox.Text = MenuDataGridView.Rows[e.RowIndex].Cells["RestMenuName"].Value.ToString();
                    PriceTextBox.Text = MenuDataGridView.Rows[e.RowIndex].Cells["RestPrice"].Value.ToString();
                    CommandStatus("0100111");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Permitions
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
            BtnAdd.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            showAllMenuBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            searchMenuBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));
            btnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString()));
            UpdateMenuBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));
            DeleteMenuBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[5].ToString()));
            btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[6].ToString()));

            if (pBtn[0] != '0') { BtnAdd.Enabled = PermissionScreen.Contains("AddMenuSet"); }
            //if (pBtn[1] != '0') { showAllMenuBtn.Enabled = PermissionScreen.Contains("RefresMenuSet"); }
            // if (pBtn[2] != '0') { searchMenuBtn.Enabled = PermissionScreen.Contains("SearchMenuSet"); }
            if (pBtn[3] != '0') { btnSave.Enabled = PermissionScreen.Contains("InsertMenuSet"); }
            if (pBtn[4] != '0') { UpdateMenuBtn.Enabled = PermissionScreen.Contains("UpdateMenuSet"); }
            if (pBtn[5] != '0') { DeleteMenuBtn.Enabled = PermissionScreen.Contains("DeleteMenuSet"); }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void ItemDataStatus(char pType, bool pADD, bool pEdit)
        {
            if (pType == 'E' || pType == 'B') // E=Enabled , B = Enabled&Clear
            {
                MenuIDTextBox.Enabled = pEdit;
                MenuNameTextBox.Enabled = pEdit;
                PriceTextBox.Enabled = pEdit;
            }

            if (pType == 'C' || pType == 'B')  // C=Clear
            {
                MenuIDTextBox.Text = "";
                MenuNameTextBox.Text = "";
                PriceTextBox.Text = "";
            }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
