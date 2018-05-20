using DesFirePersonalizer.Apps_Cood;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DesFirePersonalizer.Wizered_Pages;
using SimpleWizard;
using DesFirePersonalizer.Forms;

namespace DesFirePersonalizer
{
    public partial class StdInformations : Form
    {
        string PermissionScreen = DatabaseProvider.UsrPermission;
        string StedentIDEditNo = DatabaseProvider.StedentID;
        string StedentNameEdit = DatabaseProvider.StedentName;
        DatabaseProvider Provider = new DatabaseProvider();
        int ID = 0 ;
        private DataTable dt;
       
        public static string SetStudentIDValueForEdit = "";
        string GridQuery = "  SELECT StudentID,STDNatID,STDStatus ,STDFirstName,STDSecondName,STDFamilyName,"
                          + " STDCollage,STDDescription,STDBloodGroup,STDBirthDate,STDEmailID,STDMobileNo, "
                          + " STDPassportID, STDPassportIssuePlace, STDPassportEndDate, STDGender,STDimage,STDNationality,PlaceOfBirth,"
                          + " STDTempid FROM StudentsTable";
        



        private DataTable FillData()
        {
            return dt = DBFun.FetchData(GridQuery);
        }
        //private DataTable FillDatast()
        //{
        //    return stdt = DBFun.FetchData(GridStQuery);
        //}
        // private object Imageformat;

        public StdInformations()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DatabaseProvider.StedentID = "";
            DatabaseProvider.Command = "Add";
            //StudentsFrm stdfrm = new StudentsFrm();
            WizardHost host = new WizardHost();
            host.Text = "My Wizard";
            host.WizardCompleted += new WizardHost.WizardCompletedEventHandler(host_WizardCompleted);
            host.WizardPages.Add(1, new StdEdInfo_Pag());
            host.WizardPages.Add(2, new StdtakPic_Pag());
            host.WizardPages.Add(3, new StdIncoding_Pag());
            host.WizardPages.Add(4, new IssueCard_Pag());
            host.WizardPages.Add(5, new StdPrintCard_Pag());
            host.WizardPages.Add(6, new LastPageInfo_Pag());
          //  host.WizardPages.Add(6, new OCRUserControl());
            host.Left = 0;
            host.Top = 0;
            host.Size = this.ClientRectangle.Size;
            host.Dock = DockStyle.Fill;
            host.LoadWizard();
            host.ShowDialog();
            string SendCommand = "Add";
            DatabaseProvider.Command = SendCommand;


            CommandStatus("00011");
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region search student

        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
            btnAdd.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            btnUpdate.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            btnDelete.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));
            btnRefresh.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString()));
           
            // btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));
            //std.= Convert.ToBoolean(Convert.ToInt32(pBtn[5].ToString()));

            if (pBtn[0] != '0') { btnAdd.Enabled    = PermissionScreen.Contains("AddStd"); }
            if (pBtn[1] != '0') { btnUpdate.Enabled = PermissionScreen.Contains("UpdateStd"); }
            if (pBtn[2] != '0') { btnDelete.Enabled = PermissionScreen.Contains("DeleteStd"); }
            if (pBtn[2] != '0') { btnRefresh.Enabled = PermissionScreen.Contains("RefreshStd"); } 
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnEdit_Click(object sender, EventArgs e)
        {
            DatabaseProvider.Command = "Update";
            WizardHost host = new WizardHost();
            host.Text = "My Wizard";
            host.WizardCompleted += new WizardHost.WizardCompletedEventHandler(host_WizardCompleted);
            host.WizardPages.Add(1, new StdEdInfo_Pag());
            host.WizardPages.Add(2, new StdtakPic_Pag());
            host.WizardPages.Add(3, new StdIncoding_Pag());
            host.WizardPages.Add(4, new IssueCard_Pag());
            host.WizardPages.Add(5, new StdPrintCard_Pag());
           // host.WizardPages.Add(6, new OCRUserControl());
            host.Left = 0;
            host.Top = 0;
            host.Size = this.ClientRectangle.Size;
            host.Dock = DockStyle.Fill;
            host.LoadWizard();
            host.ShowDialog();
   
            CommandStatus("00011");
        }

        void host_WizardCompleted()
        {
            MessageBox.Show("Finshed Succssefuly!"); //obviously you'd do something else in a real app...
            //ho/*st_WizardCompleted.*/
            // textBox1.Text = "You finished. Whoopdeedoo!";
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void StdInformations_Load(object sender, EventArgs e)
        {
            CommandStatus("10000");
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSearch_Click(object sender, EventArgs e)
        {
                SearchData();
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridData(string pWhere)
        {
            return dt = DBFun.FetchData(GridQuery + " " + pWhere);
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchData()
        {
            if (cmbSearch.Text == "All")
            {
                StdGridView.DataSource = FillGridData("");
            }
            else if (cmbSearch.Text == "Login ID")
            {
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { StdGridView.DataSource = FillGridData(" WHERE StudentID LIKE '%" + TxtSearch.Text + "%'"); } else { StdGridView.DataSource = FillGridData("Where ID = -1"); StdGridView.ClearSelection(); }
            }
            else if (cmbSearch.Text == "User Name")
            {
                if (!String.IsNullOrEmpty(TxtSearch.Text)) { StdGridView.DataSource = FillGridData(" WHERE STDNameEn LIKE '%" + TxtSearch.Text + "%'"); } else { StdGridView.DataSource = FillGridData("Where ID = -1"); StdGridView.ClearSelection(); }
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
        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.Text == "All")
            {
                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                btnSearch.Enabled = true;
                StdGridView.DataSource = FillGridData("");
                StdGridView.ClearSelection();
            }
            else if (cmbSearch.Text == "Login ID" || cmbSearch.Text == "Login Name")
            {
                TxtSearch.Text = "";
                TxtSearch.Enabled = true;
                btnSearch.Enabled = true;
                StdGridView.DataSource = FillGridData("Where StudentID = -1");
                StdGridView.ClearSelection();
            }
            CommandStatus("10000");
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void FillStudentPic(string StdPic) // A-E-D-S-C
        {
            DataTable StdPicture = DBFun.FetchData(" SELECT StudentID,STDimage FROM StudentsTable WHERE StudentID = '" + LblStdID.Text + "'");
            if (!DBFun.IsNullOrEmpty(StdPicture))
            {
                if (StdPicture.Rows[0]["STDimage"] != DBNull.Value)
                {
                    PicBoxStd.Image = General.byteArrayToImage((byte[])StdPicture.Rows[0]["STDimage"]);
                }

            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void StdGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            string name = "";
            try
            {
                if (e.RowIndex != -1)
                {
                    DataRow vDataRow = GetCurrentRow(StdGridView);
                    object[] vData = vDataRow.ItemArray;
                    LblStdID.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["StudentID"]), e.RowIndex].Value.ToString();
                    LblStdNat.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDNatID"]), e.RowIndex].Value.ToString();
                    LblStdNationality.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDNationality"]), e.RowIndex].Value.ToString();
                    LblStdDaBth.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDBirthDate"]), e.RowIndex].Value.ToString();
                    LblStdCol.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDCollage"]), e.RowIndex].Value.ToString();
                    LblStdGen.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDGender"]), e.RowIndex].Value.ToString();
                    LblStdTemType.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDTempid"]), e.RowIndex].Value.ToString();
                    LblStdMobi.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDMobileNo"]), e.RowIndex].Value.ToString();
                    LblStdBloTy.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDBloodGroup"]), e.RowIndex].Value.ToString();
                    LblStdEmail.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDEmailID"]), e.RowIndex].Value.ToString();
                    LblStdPassportID.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDPassportID"]), e.RowIndex].Value.ToString();
                    LblStdPassIssuePlace.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDPassportIssuePlace"]), e.RowIndex].Value.ToString();
                    LblStdExpiryDat.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDPassportEndDate"]), e.RowIndex].Value.ToString();
                    LblPlaceOfbirth.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["PlaceOfBirth"]), e.RowIndex].Value.ToString();
                    LblStdDesc.Text = StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDDescription"]), e.RowIndex].Value.ToString();
                    // FillPermissionItem(UserGridView[UserGridView.Columns.IndexOf(UserGridView.Columns["UsrPermission"]), e.RowIndex].Value.ToString());
                    name= StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDFirstName"]), e.RowIndex].Value.ToString() +" "+ StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDSecondName"]), e.RowIndex].Value.ToString()+" "+ StdGridView[StdGridView.Columns.IndexOf(StdGridView.Columns["STDFamilyName"]), e.RowIndex].Value.ToString();
                    LblStdName.Text = name;

                    if (chkboxStatus.Checked = Convert.ToBoolean(vData[2].ToString()))
                        {
                       // STDStatus
                        LblStdStatus.Text = "Active";
                        }
                    else
                    {
                        LblStdStatus.Text = "Not Active";
                    }

                    //to send ID ANd Name to the Students Frm Page 
                    StedentIDEditNo = LblStdID.Text;
                    StedentNameEdit = LblStdName.Text;
                    DatabaseProvider.StedentID = StedentIDEditNo;
                    DatabaseProvider.StedentName = StedentNameEdit;
                    FillStudentPic(LblStdID.Text);
                    CommandStatus("11100");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                    if (ID != -1)
                    {
                        Provider.DeleteStudentsQuery(LblStdID.Text);
                        MessageBox.Show("Record Deleted Successfully!");
                        //DisplayData();
                       // ClearData();
                        CommandStatus("10000");
                   
                    }
                    else
                    {
                        MessageBox.Show("Please Select Record to Delete");
                    FillData();
                }
            }
            catch
            {
                MessageBox.Show("Procces NoT compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OCRUSERFRm  ocrfrm= new OCRUSERFRm();
            ocrfrm.Show();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
