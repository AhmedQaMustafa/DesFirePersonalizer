﻿using System;
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

namespace DesFirePersonalizer.Forms
{
    public partial class StdEdInfo_Pag : UserControl, IWizardPage
    {
        public StdEdInfo_Pag()
        {
            InitializeComponent();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        DataTable CatDt;
        DataTable dt;
        DataTable stdt;


        string GridQuery = "  SELECT StudentID,STDNatID,STDStatus ,STDFirstName,STDSecondName,STDFamilyName,"
                         + " STDCollage,STDDescription,STDBloodGroup,STDBirthDate,STDEmailID,STDMobileNo, "
                         + " STDPassportID, STDPassportIssuePlace, STDPassportEndDate, STDGender,STDimage,STDNationality,PlaceOfBirth,"
                         + " STDTempid FROM StudentsTable";
        string GridstQuery = " SELECT StudentID,STDTempid FROM StuEmpCatRelations";

   
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        public UserControl Content
        {
            get { return this; }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        public new void Load()
        {
            TxtStdIdedit.Text = DatabaseProvider.StedentID;
            if (!string.IsNullOrEmpty(TxtStdIdedit.Text))
            {
                PopulateStudentData(TxtStdIdedit.Text);
                PopulateStudentTemplates(TxtStdIdedit.Text);
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region IWizardPage Members 
        public void Save()
        {
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        public void Cancel()
        {
            throw new NotImplementedException();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        public bool IsBusy
        {
            get { throw new NotImplementedException(); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        public bool PageValid
        {
            get { return true; }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        public string ValidationMessage
        {
            get { throw new NotImplementedException(); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        #region Fill Data & Populate Data
        private DataTable FillCategoryData()
        {

            string GridQuery1 = "SELECT 0 as CatId,'Please select Template'  as CatName, null as CatCode UNION ALL SELECT CatId , CatName , CatCode FROM  CatogryType";


            return CatDt = DBFun.FetchData(GridQuery1);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        private void FillCategory()
        {
            CatDt = FillCategoryData();
            cmbTempType.ValueMember = "CatName";
            cmbTempType.DataSource = CatDt;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void PopulateStudentData(string STDpID)
        {
            dt = DBFun.FetchData(GridQuery + " WHERE StudentID = '" + STDpID + "'");
            DataRow dr = (DataRow)dt.Rows[0];
            TxtStdID.Text = dr["StudentID"].ToString();
            // TxtStdID.Text = DRs["StudentID"].ToString();
            TxtStdFirstName.Text = dr["STDFirstName"].ToString();
            TxtStdSecondName.Text = dr["STDSecondName"].ToString();
            TxtStdLastName.Text = dr["STDFamilyName"].ToString();
            cmbnationality.Text = dr["STDNationality"].ToString();
            TxtNationalID.Text = dr["STDNatID"].ToString();
            CombCollage.Text = dr["STDCollage"].ToString();
            CombBloodType.Text = dr["STDBloodGroup"].ToString();
            CombGender.Text = dr["STDGender"].ToString();
            TxtMobile.Text = dr["STDMobileNo"].ToString();
            TxtPassportID.Text = dr["STDPassportID"].ToString();
            TxtPassportIssuePlace.Text = dr["STDPassportIssuePlace"].ToString();
            ChecBoxActive.Checked = Convert.ToBoolean(dr["STDStatus"].ToString());
            TxtEmail.Text = dr["STDEmailID"].ToString();
            StdDateBirth.Text = (dr["STDBirthDate"].ToString());
            PassportEndDate.Text = (dr["STDPassportEndDate"].ToString());
            TxtStdDesc.Text = dr["STDDescription"].ToString();
            CmbPlaceOfBirth.Text = dr["PlaceOfBirth"].ToString();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void PopulateStudentTemplates(string StdcatID)
        {
            FillCategory();
            stdt = DBFun.FetchData(GridstQuery + " WHERE StudentID = '" + StdcatID + "'");
            DataRow DRs = (DataRow)stdt.Rows[0];
            string e = DRs["STDTempid"].ToString();
            cmbTempType.Text = DRs["STDTempid"].ToString();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ChbxManualAdd_CheckedChanged(object sender, EventArgs e)
        {

            if (TxtStdIdedit.Text == "")
            {
                if (ChbxManualAdd.Checked)
                {
                    StudentInformationGroup.Enabled = true;
                    FillCategory();

                }

                else if (!ChbxManualAdd.Checked)
                {
                    StudentInformationGroup.Enabled = false;
                    FillCategory();
                }
            }

            if (TxtStdIdedit.Text != "")
            {
                if (ChbxManualAdd.Checked)
                {
                    StudentInformationGroup.Enabled = true;
                    TxtStdID.Enabled = false;
                    // FillCategory();
                }

                else if (!ChbxManualAdd.Checked)
                {
                    StudentInformationGroup.Enabled = false;
                    // FillCategory();
                }
            }
        }

        private void StdEdInfo_Pag_Load(object sender, EventArgs e)
        {
            TxtStdIdedit.Text = DatabaseProvider.StedentID;
            if (!string.IsNullOrEmpty(TxtStdIdedit.Text))
            {
                PopulateStudentData(TxtStdIdedit.Text);
                PopulateStudentTemplates(TxtStdIdedit.Text);
            }
        }

        private void label50_Click(object sender, EventArgs e)
        {

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}