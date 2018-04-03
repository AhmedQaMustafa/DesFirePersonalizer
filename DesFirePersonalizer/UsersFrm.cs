using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDIFormTest
{
    public partial class UsersFrm : Form
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        FunDB FDB = new FunDB();
        DataTable dt;
        string GridQuery = "Select UsrID,LoginName,UsrName,UsrStatus,UsrPass,UsrPermission From Users ";
        string commandAction = "";
        string PermissionScreen = General.gUsrPermission;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UsersFrm() { InitializeComponent(); }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UsersFrm_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            FillListSearch();
            if (PermissionScreen.Contains("ViewUsr")) { dgvData.DataSource = FillGridData(""); dgvData.ClearSelection(); }
            else
            {
                pnlSearch.Enabled = false;
                dgvData.Enabled = false;
            }
            CommandStatus("10000");
            //string listQuery = "Select UsrID,UsrName From Users ";
            //dt = (DataTable)FDB.DaFetchDataQuery(listQuery);
            //cmbSearch.DataSource = dt;
            //cmbSearch.DisplayMember = "UsrName";
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillListSearch()
        {
            string[] itemText = { "", "اسم الدخول", "اسم المستخدم" };
            cmbSearch.DataSource = itemText;
            if (itemText.Length > 0) { cmbSearch.SelectedIndex = 0; }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridData(string pWhere)
        {
            return dt = (DataTable)FDB.DaFetchDataQuery(GridQuery + " " + pWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
            ItemDataStatus('B', false, false);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchData()
        {
            if (cmbSearch.Text == "")
            {
                dgvData.DataSource = FillGridData("");
            }
            else if (cmbSearch.Text == "اسم الدخول")
            {
                if (!String.IsNullOrEmpty(txtSearch.Text)) { dgvData.DataSource = FillGridData(" WHERE LoginName LIKE '%" + txtSearch.Text + "%'"); } else { dgvData.DataSource = FillGridData("Where UsrID = -1"); dgvData.ClearSelection(); }
            }
            else if (cmbSearch.Text == "اسم المستخدم")
            {
                if (!String.IsNullOrEmpty(txtSearch.Text)) { dgvData.DataSource = FillGridData(" WHERE UsrName LIKE '%" + txtSearch.Text + "%'"); } else { dgvData.DataSource = FillGridData("Where UsrID = -1"); dgvData.ClearSelection(); }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void dgvData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtLoginName.Text = dgvData[dgvData.Columns.IndexOf(dgvData.Columns["LoginName"]) , e.RowIndex].Value.ToString();
                txtUsrPass.Text   = dgvData[dgvData.Columns.IndexOf(dgvData.Columns["UsrPass"]), e.RowIndex].Value.ToString();
                txtUsrName.Text   = dgvData[dgvData.Columns.IndexOf(dgvData.Columns["UsrName"]), e.RowIndex].Value.ToString();
                chbUsrStatus.Checked = Convert.ToBoolean( dgvData[dgvData.Columns.IndexOf(dgvData.Columns["UsrStatus"]), e.RowIndex].Value.ToString() );
                FillPermissionItem(dgvData[dgvData.Columns.IndexOf(dgvData.Columns["UsrPermission"]), e.RowIndex].Value.ToString());
                CommandStatus("11100");
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.Text == "")
            {
                txtSearch.Text = "";
                txtSearch.Enabled = false;
                btnSearch.Enabled = false;
                dgvData.DataSource = FillGridData("");
                dgvData.ClearSelection();
            }
            else if (cmbSearch.Text == "اسم الدخول" || cmbSearch.Text == "اسم المستخدم" )
            {
                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                dgvData.DataSource = FillGridData("Where UsrID = -1");
                dgvData.ClearSelection();
            }
            CommandStatus("10000");
            ItemDataStatus('B', false, false);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
            btnAdd.Enabled    = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            btnUpdate.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            btnDelete.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));
            btnSave.Enabled   = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString()));
            btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));

            if (pBtn[0] != '0') { btnAdd.Enabled = PermissionScreen.Contains("AddUsr"); }
            if (pBtn[1] != '0') { btnUpdate.Enabled = PermissionScreen.Contains("UpdateUsr"); }
            if (pBtn[2] != '0') { btnDelete.Enabled = PermissionScreen.Contains("DeleteUsr"); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void ClearItemSearch()
        {
            cmbSearch.SelectedIndex = 0;
            txtSearch.Text = "";
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            dgvData.DataSource = FillGridData("");
            dgvData.ClearSelection();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void ItemDataStatus(char pType, bool pADD, bool pEdit)
        {
            if (pType == 'E' || pType == 'B') // E=Enabled , B = Enabled&Clear
            {
                txtLoginName.Enabled  = pEdit;
                txtUsrName.Enabled    = pADD;
                txtUsrPass.Enabled    = pADD;
                chbUsrStatus.Enabled  = pADD;
                pnlPermission.Enabled = pADD;
            }

            if (pType == 'C' || pType == 'B')  // C=Clear
            {
                txtLoginName.Text = "";
                txtUsrName.Text = "";
                txtUsrPass.Text = "";
                chbUsrStatus.Checked = false;
                ClearPermissionItem();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnAdd_Click(object sender, EventArgs e)
        {
            commandAction = "Add";
            CommandStatus("00011");
            pnlSearch.Enabled = false;
            dgvData.Enabled = false;
            dgvData.ClearSelection();
            ItemDataStatus('B', true, true);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnEdit_Click(object sender, EventArgs e)
        {
            commandAction = "Edit";
            CommandStatus("00011");
            pnlSearch.Enabled = false;
            dgvData.Enabled = false;
            ItemDataStatus('E', true, false);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLoginName.Text == "admin")
                {
                    MessageBox.Show("لا يمكن حذف المستخدم admin", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!isDelete())
                    {
                        MessageBox.Show("لا يمكن الحذف الحساب بسبب ارتباطه بسجلات أخرى", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int iDelete = FDB.DaExecuteDataQuery("DELETE FROM Users WHERE LoginName= '" + txtLoginName.Text + "'");
                        CommandStatus("10000");
                        pnlSearch.Enabled = true;
                        dgvData.Enabled = true;
                        SearchData();
                        ItemDataStatus('B', false, false);
                        MessageBox.Show("تم الحذف بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("لم تتم العملية الرجاء مراجعة مدير النظام", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool isDelete()
        {
            string UsR = txtLoginName.Text;
            dt = (DataTable)FDB.DaFetchDataQuery("SELECT * FROM MainAccount WHERE AccountCreatedBy = ' " + UsR + "' OR AccountModifiedBy = ' " + UsR + "'");
            if (FDB.IsValidDT(dt)) { return false; }

            dt = (DataTable)FDB.DaFetchDataQuery("SELECT * FROM BranchAccount WHERE BrcCreatedBy = ' " + UsR + "' OR BrcModifiedBy = ' " + UsR + "'");
            if (FDB.IsValidDT(dt)) { return false; }

            dt = (DataTable)FDB.DaFetchDataQuery("SELECT * FROM AmountAccount WHERE AmountCreatedBy = ' " + UsR + "' OR AmountModifiedBy = ' " + UsR + "'");
            if (FDB.IsValidDT(dt)) { return false; }

            dt = (DataTable)FDB.DaFetchDataQuery("SELECT * FROM Currencies WHERE CurrencyCreatedBy = ' " + UsR + "' OR CurrencyModifiedBy = ' " + UsR + "'");
            if (FDB.IsValidDT(dt)) { return false; }

            dt = (DataTable)FDB.DaFetchDataQuery("SELECT * FROM WithdrawPolicy WHERE WPolicyCreatedBy = ' " + UsR + "' OR WPolicyModifiedBy = ' " + UsR + "'");
            if (FDB.IsValidDT(dt)) { return false; }

            dt = (DataTable)FDB.DaFetchDataQuery("SELECT * FROM ChequePolicy WHERE CPolicyCreatedBy = ' " + UsR + "' OR CPolicyModifiedBy = ' " + UsR + "' OR CPolicyCancelBy = ' " + UsR + "'");
            if (FDB.IsValidDT(dt)) { return false; }

            dt = (DataTable)FDB.DaFetchDataQuery("SELECT * FROM Beneficiarys WHERE bfyCreatedBy = ' " + UsR + "' OR bfyModifiedBy = ' " + UsR + "'");
            if (FDB.IsValidDT(dt)) { return false; }
            return true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strPermission = ReadPermissionItem(); 
            string sqlSelect = "Select * From Users WHERE LoginName='" + txtLoginName.Text + "'";
            string sqlInsert = " INSERT INTO Users ( LoginName,UsrName,UsrStatus,UsrPass,UsrPermission )"
                             + " VALUES ('" + txtLoginName.Text + "','" + txtUsrName.Text + "','" + chbUsrStatus.Checked.ToString() + "','" + txtUsrPass.Text + "','" + strPermission + "')";

            string sqlUpdate = " UPDATE Users SET UsrName='" + txtUsrName.Text + "'" + ",UsrPass='" + txtUsrPass.Text + "'"
                                              + ",UsrStatus='" + chbUsrStatus.Checked.ToString() + "'"  + ",UsrPermission='" + strPermission + "'"
                             + " WHERE LoginName='" + txtLoginName.Text + "'";
            try
            {

                if (commandAction == "Add")
                {
                    if (!String.IsNullOrEmpty(txtLoginName.Text) && !String.IsNullOrEmpty(txtUsrPass.Text))
                    {
                        dt = (DataTable)FDB.DaFetchDataQuery(sqlSelect);
                        if (!FDB.IsValidDT(dt))
                        {
                            int iAdd = FDB.DaExecuteDataQuery(sqlInsert);
                            CommandStatus("11100");
                            pnlSearch.Enabled = true;
                            dgvData.Enabled = true;
                            ClearItemSearch();
                            SearchData();
                            ItemDataStatus('E', false, false);
                            commandAction = "";
                            MessageBox.Show("تمت الإضافة بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SelectRowWithValue(dgvData, txtLoginName.Text);
                        }
                        else
                        {
                            MessageBox.Show("اسم الدخول موجود مسبقا, الرجاء اختيار اسم آخر", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("يجب إدخال جميع الحقول المطلوبة", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                    
                
                if (commandAction == "Edit") 
                {
                    if (!String.IsNullOrEmpty(txtLoginName.Text) && !String.IsNullOrEmpty(txtUsrPass.Text))
                    {
                        int iEdit = FDB.DaExecuteDataQuery(sqlUpdate);
                        CommandStatus("11100");
                        pnlSearch.Enabled = true;
                        dgvData.Enabled = true;
                        SearchData();
                        ItemDataStatus('E', false, false);
                        commandAction = "";
                        MessageBox.Show("تم التعديل بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SelectRowWithValue(dgvData, txtLoginName.Text);
                    }
                    else
                    {
                        MessageBox.Show("يجب إدخال جميع الحقول المطلوبة", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }  
            }
            catch ( Exception ex )
            {
                MessageBox.Show("لم تتم العملية الرجاء مراجعة مدير النظام", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (commandAction == "Add")  { CommandStatus("10000"); ItemDataStatus('B', false, false); }
            if (commandAction == "Edit") { CommandStatus("11000"); ItemDataStatus('E', false, false); returnData(); }
            pnlSearch.Enabled = true;
            dgvData.Enabled = true;
            commandAction = "";
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void returnData()
        {
            DataRow vDataRow = GetCurrentRow(dgvData);
            object[] vData = vDataRow.ItemArray;
            txtLoginName.Text = vData[1].ToString();
            txtUsrName.Text = vData[2].ToString();
            txtUsrPass.Text = vData[4].ToString();
            FillPermissionItem(vData[5].ToString());
            chbUsrStatus.Checked =  Convert.ToBoolean( vData[3].ToString() );
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SelectRowWithValue(DataGridView pDataGrid, string pValue) 
        {
            pDataGrid.ClearSelection();
            foreach (DataGridViewRow row in pDataGrid.Rows) 
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



        /*#################################################################################################################################*/
        /*#################################################################################################################################*/

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string[] ScreenText = { "Usr", "MAcc", "BAcc", "AAcc", "WPolicy", "CPolicy", "Bfy" ,"Cur" };
        string[] PermText =  { "View", "Add", "Update", "Delete","Print" };
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string ReadPermissionItem()
        {
            string PermStr = "";     
            for (int i = 0; i < ScreenText.Length; i++)
            {
                for (int j = 0; j < PermText.Length; j++)
                {
                    CheckBox CurrentCheckBox = (CheckBox)pnlPermission.Controls[ "chb" + PermText[j] + ScreenText[i] ];
                    if (CurrentCheckBox.Checked) { PermStr += "," + PermText[j] + ScreenText[i]; }
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
                    CheckBox CurrentCheckBox = (CheckBox)pnlPermission.Controls["chb" + PermText[j] + ScreenText[i]];
                    CurrentCheckBox.Checked = pPerm.Contains(PermText[j] + ScreenText[i]);
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
                    CheckBox CurrentCheckBox = (CheckBox)pnlPermission.Controls["chb" + PermText[j] + ScreenText[i]];
                    CurrentCheckBox.Checked = false;
                }
            }
        }

        private void pnlData_Paint(object sender, PaintEventArgs e)
        {

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*#################################################################################################################################*/
        /*#################################################################################################################################*/
    }
}
