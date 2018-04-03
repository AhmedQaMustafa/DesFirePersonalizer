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
    public partial class IssueCard_Pag : UserControl, IWizardPage
    {
        public IssueCard_Pag()
        {
            InitializeComponent();
        }
        private DataTable Empdt;
        private DataTable Stdt;
        private DataTable dt;
        DataTable CatDt;
        DataTable IssueDt;
        DataTable Carddt;

        string PermissionScreen = DatabaseProvider.UsrPermission;
         string GridQuery = "Select StudentID,IsID,StartDate,ExpiryDate,CardStatus From CardMaster ";
        string StdGridQuery = "Select *  From CardInfoView ";
        string StGridQuery = " SELECT * From CardInfoView ";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region IWizardPage Members

        public UserControl Content
        {
            get { return this; }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public new void Load()
        {
            TxtNameEdit.Text = DatabaseProvider.StedentName;
            TxtIsStudentID.Text = DatabaseProvider.StedentID;
            TxtIsStudentName.Text = DatabaseProvider.StedentName;
            FillIssueState();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Save()
        {
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Cancel()
        {
            throw new NotImplementedException();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool IsBusy
        {
            get { throw new NotImplementedException(); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool PageValid
        {
            get { return true; }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string ValidationMessage
        {
            get { throw new NotImplementedException(); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region fill data 

        private DataTable FillGridData(string pWhere)
        {
            return Empdt = DBFun.FetchData(StdGridQuery + " " + pWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillIssueData()
        {

            string GridQuery1 = "SELECT 0 as IsID,'Please select Card Issue'  as IsNameEn, null as IsNameAr UNION ALL SELECT IsID , IsNameEn , IsNameAr FROM  IssueState";
            return IssueDt = DBFun.FetchData(GridQuery1);

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillCategoryData()
        {
            string GridQueryTemplates = "SELECT 0 as CatId,'Please select Template'  as CatName, null as CatCode UNION ALL SELECT CatId , CatName , CatCode FROM  CatogryType";
            return CatDt = DBFun.FetchData(GridQueryTemplates);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillIssueState()
        {
            IssueDt = FillIssueData();
            CmbIsIssueType.ValueMember = "IsID";
            CmbIsIssueType.DisplayMember = "IsNameEn";
            CmbIsIssueType.DataSource = IssueDt;
            CatDt = FillCategoryData();
            CmbIsTemptype.ValueMember = "CatId";
            CmbIsTemptype.DisplayMember = "CatName";
            CmbIsTemptype.DataSource = CatDt;
            if (!String.IsNullOrEmpty(TxtIsStudentID.Text)) { CardsGridData.DataSource = FillStudentData(" WHERE StudentID LIKE '%" + TxtIsStudentID.Text + "%'"); } else { CardsGridData.DataSource = FillGridData("Where IsID = -1"); CardsGridData.ClearSelection(); }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillStudentData(string StpWhere)
        {
            return Stdt = DBFun.FetchData(StGridQuery + " " + StpWhere);
        }

        private DataTable FillCardsGridData(string pWhere1)
        {
            return Carddt = DBFun.FetchData(GridQuery + " " + pWhere1);
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
           // btnSearch.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            btnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));

            if (pBtn[0] != '0') { btnSave.Enabled = PermissionScreen.Contains("AddUsr"); }
            if (pBtn[1] != '0') { btnCancel.Enabled = PermissionScreen.Contains("UpdateUsr"); }
           // if (pBtn[2] != '0') { btnSearch.Enabled = PermissionScreen.Contains("DeleteUsr"); }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Carddt = FillGridData("WHERE StudentID = '" + TxtIsStudentID.Text + " ' ");

                if (CmbIsIssueType.Text != "Please select Card Issue" && CmbIsStatus.Text != "" && CmbIsTemptype.Text != "Please select Template" )
                {

                    if (!DBFun.IsNullOrEmpty(Carddt))
                    {
                        DatabaseProvider provider = new DatabaseProvider();
                        provider.IssueNewCards(TxtIsStudentID.Text, Convert.ToInt32(CmbIsIssueType.SelectedValue), DateTimeISStart.Value, DateTimeISEnd.Value, TxtIsDesc.Text, Convert.ToInt32(CmbIsStatus.SelectedValue), Convert.ToInt32(CmbIsTemptype.SelectedValue));

                        MessageBox.Show("Card Issued Succesfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //public void PopulateStudentData(string STDpID)
        //{
        //    dt = DBFun.FetchData(StdGridQuery + " WHERE StudentID = '" + STDpID + "'");
        //    DataRow dr = (DataRow)dt.Rows[0];
        //TxtStdID.Text = dr["StudentID"].ToString();
        //// TxtStdID.Text = DRs["StudentID"].ToString();
        //TxtStdFirstName.Text = dr["STDFirstName"].ToString();
        //TxtStdSecondName.Text = dr["STDSecondName"].ToString();
        //TxtStdLastName.Text = dr["STDFamilyName"].ToString();
        //cmbnationality.Text = dr["STDNationality"].ToString();
        //TxtNationalID.Text = dr["STDNatID"].ToString();
        //CombCollage.Text = dr["STDCollage"].ToString();
        //CombBloodType.Text = dr["STDBloodGroup"].ToString();
        //CombGender.Text = dr["STDGender"].ToString();
        //TxtMobile.Text = dr["STDMobileNo"].ToString();
        //TxtPassportID.Text = dr["STDPassportID"].ToString();
        //TxtPassportIssuePlace.Text = dr["STDPassportIssuePlace"].ToString();
        //ChecBoxActive.Checked = Convert.ToBoolean(dr["STDStatus"].ToString());
        //TxtEmail.Text = dr["STDEmailID"].ToString();
        //StdDateBirth.Text = (dr["STDBirthDate"].ToString());
        //PassportEndDate.Text = (dr["STDPassportEndDate"].ToString());
        //TxtStdDesc.Text = dr["STDDescription"].ToString();
        //CmbPlaceOfBirth.Text = dr["PlaceOfBirth"].ToString();
        //}
    }
}
