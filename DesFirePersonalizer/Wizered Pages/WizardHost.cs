﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesFirePersonalizer.Apps_Cod;
using DesFirePersonalizer.Apps_Cood;
using DesFirePersonalizer.Wizered_Pages;
using DesFirePersonalizer.Forms;

namespace SimpleWizard
{
    public partial class WizardHost : Form
    {
        private const string VALIDATION_MESSAGE = "Current page is not valid. Please fill in required information";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        UserControl[] UsrCo = { new StdEdInfo_Pag(), new StdtakPic_Pag(), new StdIncoding_Pag(), new IssueCard_Pag(),
                       new StdPrintCard_Pag(), new LastPageInfo_Pag()};
        int top = -1;
        int count = 6;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string valueID = ""; string valueNat = ""; string valueFiNa = ""; string valuePassID = ""; string valueSeNa = ""; string valueFaNa = ""; string TxtStdIdedit = "";
        string valuePasIsPl = ""; string valueMobile = ""; string valueEmail = ""; string valueNationality = ""; string valueCollage = ""; string valueDesc = "";
        string valueBlTy = ""; string valueGender = ""; string valueTempTy = ""; ValueType valueDateBirth; ValueType valuePassEndDate; string valueActNotAct = "";
        string valueTempTyUpdate = ""; string valueNationalityUpdate = ""; string valueCollageUpdate = ""; string valueGenderUpdate = ""; string valueBlTyUpdate = "";
        string valueTempTynew = ""; Image valueImage; Image valueImageCaptured; Bitmap valueImage1 = null; Bitmap valueImageCaptured1 = null;
        string valuePlaceOfBirthUpdate = ""; string valuePlaceOfBirth = "";
        DatabaseProvider Provider = new DatabaseProvider();
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        DataTable dt;
        DataTable stdt;
        DatabaseProvider ProvComm = new DatabaseProvider();
        string GridQuery = "  SELECT StudentID,STDNatID,STDStatus ,STDFirstName,STDSecondName,STDFamilyName,"
                 + " STDCollage,STDDescription,STDBloodGroup,STDBirthDate,STDEmailID,STDMobileNo, "
                 + " STDPassportID, STDPassportIssuePlace, STDPassportEndDate, STDGender,STDimage,STDNationality,PlaceOfBirth,"
                 + " STDTempid FROM StudentsTable";
        string GridstQuery = " SELECT StudentID,STDTempid FROM StuEmpCatRelations";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Fill Data 
        private DataTable FillGridData(string pWhere)
        {
            return dt = DBFun.FetchData(GridQuery + " " + pWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillData()
        {
            return dt = DBFun.FetchData(GridQuery);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillstData1(string StpWhere)
        {
            return stdt = DBFun.FetchData(GridstQuery + " " + StpWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillstData()
        {
            return stdt = DBFun.FetchData(GridstQuery);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region call controls
        public void callFormsControlsNew()
        {

            Control[] TxtStdID = Controls.Find("TxtStdID", true);
            if (TxtStdID[0] is TextBox) { valueID = ((TextBox)TxtStdID[0]).Text; }
            //////////////////////////////////////////////////////////////////////////////
            Control[] TxtNationalID = Controls.Find("TxtNationalID", true);
            if (TxtNationalID[0] is TextBox) { valueNat = ((TextBox)TxtNationalID[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtPassportID = Controls.Find("TxtPassportID", true);
            if (TxtPassportID[0] is TextBox) { valuePassID = ((TextBox)TxtPassportID[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdFirstName = Controls.Find("TxtStdFirstName", true);
            if (TxtStdFirstName[0] is TextBox) { valueFiNa = ((TextBox)TxtStdFirstName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdSecondName = Controls.Find("TxtStdSecondName", true);
            if (TxtStdSecondName[0] is TextBox) { valueSeNa = ((TextBox)TxtStdSecondName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdLastName = Controls.Find("TxtStdLastName", true);
            if (TxtStdLastName[0] is TextBox) { valueFaNa = ((TextBox)TxtStdLastName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtPassportIssuePlace = Controls.Find("TxtPassportIssuePlace", true);
            if (TxtPassportIssuePlace[0] is TextBox) { valuePasIsPl = ((TextBox)TxtPassportIssuePlace[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtMobile = Controls.Find("TxtMobile", true);
            if (TxtMobile[0] is TextBox) { valueMobile = ((TextBox)TxtMobile[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdDesc = Controls.Find("TxtStdDesc", true);
            if (TxtStdDesc[0] is TextBox) { valueDesc = ((TextBox)TxtStdDesc[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtEmail = Controls.Find("TxtEmail", true);
            if (TxtEmail[0] is TextBox) { valueEmail = ((TextBox)TxtEmail[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] cmbnationality = Controls.Find("cmbnationality", true);
            if (cmbnationality[0] is ComboBox) { valueNationality = ((ComboBox)cmbnationality[0]).Items[0].ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombBloodType = Controls.Find("CombBloodType", true);
            if (CombBloodType[0] is ComboBox) { valueBlTy = ((ComboBox)CombBloodType[0]).Items[0].ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombCollage = Controls.Find("CombCollage", true);
            if (CombCollage[0] is ComboBox) { valueCollage = ((ComboBox)CombCollage[0]).Items[0].ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombGender = Controls.Find("CombGender", true);
            if (CombGender[0] is ComboBox) { valueGender = ((ComboBox)CombGender[0]).Items[0].ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] cmbTempType = Controls.Find("cmbTempType", true);
            if (cmbTempType[0] is ComboBox) { valueTempTy = ((ComboBox)cmbTempType[0]).Items.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CmbPlaceOfBirth = Controls.Find("CmbPlaceOfBirth", true);
            if (CmbPlaceOfBirth[0] is ComboBox) { valuePlaceOfBirth = ((ComboBox)CmbPlaceOfBirth[0]).Items[0].ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] StdDateBirth = Controls.Find("StdDateBirth", true);
            if (StdDateBirth[0] is DateTimePicker) { valueDateBirth = ((DateTimePicker)StdDateBirth[0]).Value; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] PassportEndDate = Controls.Find("PassportEndDate", true);
            if (PassportEndDate[0] is DateTimePicker) { valuePassEndDate = ((DateTimePicker)PassportEndDate[0]).Value; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] ChecBoxActive = Controls.Find("ChecBoxActive", true);
            if (ChecBoxActive[0] is CheckBox) { valueActNotAct = ((CheckBox)ChecBoxActive[0]).Checked.ToString(); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void callFormsControlsUpdate()
        {

            Control[] TxtStdID = Controls.Find("TxtStdID", true);
            if (TxtStdID[0] is TextBox) { valueID = ((TextBox)TxtStdID[0]).Text; }
            //////////////////////////////////////////////////////////////////////////////
            Control[] TxtNationalID = Controls.Find("TxtNationalID", true);
            if (TxtNationalID[0] is TextBox) { valueNat = ((TextBox)TxtNationalID[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtPassportID = Controls.Find("TxtPassportID", true);
            if (TxtPassportID[0] is TextBox) { valuePassID = ((TextBox)TxtPassportID[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdFirstName = Controls.Find("TxtStdFirstName", true);
            if (TxtStdFirstName[0] is TextBox) { valueFiNa = ((TextBox)TxtStdFirstName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdSecondName = Controls.Find("TxtStdSecondName", true);
            if (TxtStdSecondName[0] is TextBox) { valueSeNa = ((TextBox)TxtStdSecondName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdLastName = Controls.Find("TxtStdLastName", true);
            if (TxtStdLastName[0] is TextBox) { valueFaNa = ((TextBox)TxtStdLastName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtPassportIssuePlace = Controls.Find("TxtPassportIssuePlace", true);
            if (TxtPassportIssuePlace[0] is TextBox) { valuePasIsPl = ((TextBox)TxtPassportIssuePlace[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtMobile = Controls.Find("TxtMobile", true);
            if (TxtMobile[0] is TextBox) { valueMobile = ((TextBox)TxtMobile[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdDesc = Controls.Find("TxtStdDesc", true);
            if (TxtStdDesc[0] is TextBox) { valueDesc = ((TextBox)TxtStdDesc[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtEmail = Controls.Find("TxtEmail", true);
            if (TxtEmail[0] is TextBox) { valueEmail = ((TextBox)TxtEmail[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] cmbnationalityUpdate = Controls.Find("cmbnationality", true);
            if (cmbnationalityUpdate[0] is ComboBox) { valueNationalityUpdate = ((ComboBox)cmbnationalityUpdate[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombBloodTypeUpdate = Controls.Find("CombBloodType", true);
            if (CombBloodTypeUpdate[0] is ComboBox) { valueBlTyUpdate = ((ComboBox)CombBloodTypeUpdate[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombCollageUpdate = Controls.Find("CombCollage", true);
            if (CombCollageUpdate[0] is ComboBox) { valueCollageUpdate = ((ComboBox)CombCollageUpdate[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombGenderUpdate = Controls.Find("CombGender", true);
            if (CombGenderUpdate[0] is ComboBox) { valueGenderUpdate = ((ComboBox)CombGenderUpdate[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] cmbTempTypeUpdate = Controls.Find("cmbTempType", true);
            if (cmbTempTypeUpdate[0] is ComboBox) { valueTempTyUpdate = ((ComboBox)cmbTempTypeUpdate[0]).SelectedValue.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] StdDateBirth = Controls.Find("StdDateBirth", true);
            if (StdDateBirth[0] is DateTimePicker) { valueDateBirth = ((DateTimePicker)StdDateBirth[0]).Value; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] PassportEndDate = Controls.Find("PassportEndDate", true);
            if (PassportEndDate[0] is DateTimePicker) { valuePassEndDate = ((DateTimePicker)PassportEndDate[0]).Value; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CmbPlaceOfBirth = Controls.Find("CmbPlaceOfBirth", true);
            if (CmbPlaceOfBirth[0] is ComboBox) { valuePlaceOfBirthUpdate = ((ComboBox)CmbPlaceOfBirth[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] ChecBoxActive = Controls.Find("ChecBoxActive", true);
            if (ChecBoxActive[0] is CheckBox) { valueActNotAct = ((CheckBox)ChecBoxActive[0]).Checked.ToString(); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void callFormsControlsImage()
        {
            Control[] StdPictureboxEdit = Controls.Find("StdPictureboxEdit", true);
            if (StdPictureboxEdit[0] is PictureBox) { valueImage = ((PictureBox)StdPictureboxEdit[0]).Image; }
            //////////////////////////////////////////////////////////////////////////////
            Control[] ImageCaptured = Controls.Find("ImageCaptured", true);
            //FrmStdtakPic.ImageCaptured.Image = pctImage.Image.Clone() as Image;
            if (ImageCaptured[0] is PictureBox) { valueImageCaptured = ((PictureBox)ImageCaptured[0]).Image; }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region comparing Image other Data 

        private bool compare(Bitmap bmp1, Bitmap bmp2)
        {
            bool equals = true;
            bool flag = true;  //Inner loop isn't broken

            //Test to see if we have the same size of image
            if (bmp1.Size == bmp2.Size)
            {
                for (int x = 0; x < bmp1.Width; ++x)
                {
                    for (int y = 0; y < bmp1.Height; ++y)
                    {
                        if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                        {
                            equals = false;
                            flag = false;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
            }
            else
            {
                equals = false;
            }
            return equals;
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Properties

        public WizardPageCollection WizardPages { get; set; }
        public bool ShowFirstButton
        {
            get { return btnFirst.Visible; }
            set { btnFirst.Visible = value; }
        }
        public bool ShowLastButton
        {
            get { return btnLast.Visible; }
            set { btnLast.Visible = value; }
        }

        private bool navigationEnabled = true;
        public bool NavigationEnabled
        {
            get { return navigationEnabled; }
            set
            {
                btnFirst.Enabled = value;
                btnPrevious.Enabled = value;
                btnNext.Enabled = value;
                btnLast.Enabled = value;
                navigationEnabled = value;
            }
        }

        public bool UserControl1 { get; private set; }

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Delegates & Events

        public delegate void WizardCompletedEventHandler();
        public event WizardCompletedEventHandler WizardCompleted;

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Constructor & Window Event Handlers
        public WizardHost()
        {
            InitializeComponent();
            WizardPages = new WizardPageCollection();
            WizardPages.WizardPageLocationChanged += new WizardPageCollection.WizardPageLocationChangedEventHanlder(WizardPages_WizardPageLocationChanged);
        }

        void WizardPages_WizardPageLocationChanged(WizardPageLocationChangedEventArgs e)
        {
            LoadNextPage(e.PageIndex, e.PreviousPageIndex, true);
        }

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Private Methods

        private void NotifyWizardCompleted()
        {
            if (WizardCompleted != null)
            {
                OnWizardCompleted();
                WizardCompleted();
            }
        }
        private void OnWizardCompleted()
        {
            WizardPages.LastPage.Save();
            WizardPages.Reset();
            this.DialogResult = DialogResult.OK;
        }

        public void UpdateNavigation()
        {
            #region Reset

            btnNext.Enabled = true;
            btnNext.Visible = true;

            btnLast.Text = "Last >>";
            if (ShowLastButton)
            {
                btnLast.Enabled = true;
            }
            else
            {
                btnLast.Enabled = false;
            }

            #endregion

            bool canMoveNext = WizardPages.CanMoveNext;
            bool canMovePrevious = WizardPages.CanMovePrevious;

            btnPrevious.Enabled = canMovePrevious;
            btnFirst.Enabled = canMovePrevious;

            if (canMoveNext)
            {
                btnNext.Text = "Next >";
                btnNext.Enabled = true;

                if (ShowLastButton)
                {
                    btnLast.Enabled = true;
                }
            }
            else
            {
                if (ShowLastButton)
                {
                    btnLast.Text = "Finish";
                    btnNext.Visible = false;
                }
                else
                {
                    btnNext.Text = "Finish";
                    btnNext.Visible = true;
                }
            }
        }

        private bool CheckPageIsValid()
        {
     
            if (!WizardPages.CurrentPage.PageValid)
            {
                    MessageBox.Show(
                        string.Concat(VALIDATION_MESSAGE, Environment.NewLine, Environment.NewLine, WizardPages.CurrentPage.ValidationMessage),
                        "Details Required",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return false;
            }
            return true;
        }
            
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Public Methods

        public void LoadWizard()
        {
            WizardPages.MovePageFirst();
            // label1.Text = DatabaseProvider.Command;
            BtnStep1.Enabled = false;

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void LoadNextPage(int pageIndex, int previousPageIndex, bool savePreviousPage)
        {
            if (pageIndex != -1)
            {
                contentPanel.Controls.Clear();
                contentPanel.Controls.Add(WizardPages[pageIndex].Content);
                if (savePreviousPage && previousPageIndex != -1)
                {
                    WizardPages[previousPageIndex].Save();
                }
                WizardPages[pageIndex].Load();
                UpdateNavigation();
            }
        }

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Event Handlers
        private void btnFirst_Click(object sender, EventArgs e)
        {
            //if (!CheckPageIsValid()) //Maybe doesn't matter if move back; only matters if move forward
            //{ return; }

            WizardPages.MovePageFirst();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //if (!CheckPageIsValid()) //Maybe doesn't matter if move back; only matters if move forward
            //{ return; }

            WizardPages.MovePagePrevious();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!CheckPageIsValid())
            { return; }
            if (WizardPages.CanMoveNext)
            {
                #region StdEditIformations
                BtnStep1.Enabled = true;
                if (this.contentPanel.Controls["StdEdInfo_Pag"] != null)
                {
                   if (label1.Text == "Add")
                    {
                        callFormsControlsNew();
                        if (valueID != "")//&& valueNat == "" && valueCollage.ToString() == "" && valueTempTy == "" && valueActNotAct == "" && valueEmail == ""
                        {
                            callFormsControlsNew();
                            dt = FillGridData("WHERE StudentID = '" + valueID + " ' ");
                            stdt = FillstData1("WHERE StudentID = '" + valueID + " ' ");

                            if (DBFun.IsNullOrEmpty(dt) || DBFun.IsNullOrEmpty(stdt))
                            {
                                Control[] cmbTempTypeNew = UsrCo[top].Controls.Find("cmbTempType", true);
                                if (cmbTempTypeNew[0] is ComboBox) { valueTempTynew = ((ComboBox)cmbTempTypeNew[0]).SelectedValue.ToString(); }
                                Provider.InsertNewStudent(valueID, valueNat, valuePassID, valueCollage, valuePasIsPl, valueBlTy, valuePassEndDate, valueGender, valueActNotAct,
                                                          valueNationality, valueFiNa, valueSeNa, valueFaNa, valueMobile, valueEmail, valueDateBirth, valueTempTynew, valueDesc, valuePlaceOfBirth);
                                // TxtStdDesc.Text, ImageCaptured.Image
                                Provider.Insertstdcat(valueID, valueTempTynew);
                                //TxtStdIdedit = valueID;
                                label1.Text = "Update";
                                WizardPages.MovePageNext();
                            }
                            else
                            {
                                MessageBox.Show("Student ID Already Exist Please find another id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        BtnStep1.Enabled = true;
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    else
                    {
                        try
                        {
                            if (label1.Text == "Update")
                            {
                                callFormsControlsUpdate();
                                dt = FillGridData("WHERE StudentID = '" + valueID + " ' ");
                                stdt = FillstData1("WHERE StudentID = '" + valueID + " ' ");
                                if (
                                     valueNat != dt.Rows[0]["STDNatID"].ToString() ||
                                     valuePassID != (dt.Rows[0]["STDPassportID"]).ToString() ||
                                     valueCollageUpdate != (dt.Rows[0]["STDCollage"]).ToString() ||
                                     valuePasIsPl != (dt.Rows[0]["STDPassportIssuePlace"]).ToString() ||
                                     valueBlTyUpdate != (dt.Rows[0]["STDBloodGroup"]).ToString() ||
                                     valuePassEndDate.ToString() != (dt.Rows[0]["STDPassportEndDate"]).ToString() ||
                                     valueGenderUpdate != (dt.Rows[0]["STDGender"]).ToString() ||
                                     valueActNotAct != Convert.ToString(dt.Rows[0]["STDStatus"]) ||
                                     valueNationalityUpdate != (dt.Rows[0]["STDNationality"]).ToString() ||
                                     valuePlaceOfBirthUpdate != (dt.Rows[0]["PlaceOfBirth"]).ToString() ||
                                     valueFiNa != Convert.ToString(dt.Rows[0]["STDFirstName"]) ||
                                     valueSeNa != Convert.ToString(dt.Rows[0]["STDSecondName"]) ||
                                     valueFaNa != Convert.ToString(dt.Rows[0]["STDFamilyName"]) ||
                                     valueMobile != Convert.ToString(dt.Rows[0]["STDMobileNo"]) ||
                                     valueEmail != Convert.ToString(dt.Rows[0]["STDEmailID"]) ||
                                     valueDateBirth.ToString() != (dt.Rows[0]["STDBirthDate"]).ToString() ||
                                     valueTempTyUpdate.ToString() != (dt.Rows[0]["STDTempid"]).ToString() ||
                                     valueTempTyUpdate.ToString() != (stdt.Rows[0]["STDTempid"]).ToString() ||
                                     valueDesc != (dt.Rows[0]["STDDescription"].ToString())
                                     )
                                {
                                    DialogResult dialogResult = MessageBox.Show("Are you Sure need update Informations ", "Attentions", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        if (!DBFun.IsNullOrEmpty(dt))

                                        {
                                            Provider.UpdateStudentInfo(valueID, valueNat, valuePassID, valueCollageUpdate, valuePasIsPl,
                                            valueBlTyUpdate, valuePassEndDate, valueGenderUpdate, valueActNotAct,
                                            valueNationalityUpdate, valueFiNa, valueSeNa, valueFaNa,
                                            valueMobile, valueEmail, valueDateBirth, valueTempTyUpdate,
                                            valueDesc, valuePlaceOfBirthUpdate);//, ImageCaptured.Image
                                            Provider.Updatestdcat(valueID, valueTempTyUpdate);
                                            FillData();

                                        }
                                        else
                                        {
                                            MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        callFormsControlsUpdate();

                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please fill the fields empty", "Note");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    BtnStep1.Enabled = true;
                }
                #endregion
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                #region Studentd Picture
                if (this.contentPanel.Controls["StdtakPic_Pag"] != null)
                {
                    //BtnStep1.Enabled = true;
                    //BtnStep2.Enabled = false;
                    try
                    {
                        if (label1.Text == "Update" && valueID != "")
                        {
                            callFormsControlsImage();
                            valueImage1 = new Bitmap(valueImage);
                            valueImageCaptured1 = new Bitmap(valueImageCaptured);

                            if (!compare(valueImage1, valueImageCaptured1))
                            {
                                DialogResult dialogResult = MessageBox.Show("Are you Sure need update Informations ", "Attentions", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    dt = FillGridData("WHERE StudentID = '" + valueID + " ' ");
                                    if (!DBFun.IsNullOrEmpty(dt))
                                    {
                                        Provider.UpdateStudentInfoImage(valueID, valueImageCaptured);//, ImageCaptured.Image
                                        FillData();
                                        WizardPages.MovePageNext();
                                 
                                    }
                                }

                                else if (dialogResult == DialogResult.No)
                                {
                                    WizardPages.MovePageNext();
                         
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    BtnStep2.Enabled = true;
                }
                #endregion
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
             
                #region Incoding
                if (this.contentPanel.Controls["StdIncoding_Pag"] != null)
                {
                    //BtnStep1.Enabled = true;
                    //BtnStep2.Enabled = true;
                    //BtnStep3.Enabled = false;
                    BtnStep3.Enabled = true;
                }
                #endregion
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
               
                #region Issue Card
                if (this.contentPanel.Controls["IssueCard_Pag"] != null)
                {
                    //  BtnStep1.Enabled = true;
                    //  BtnStep2.Enabled = true;
                    //  BtnStep3.Enabled = true;
                    //  BtnStep4.Enabled = false;
                    ////  BtnStep6.Enabled = true;
                    BtnStep4.Enabled = true;
                }
                #endregion
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
              
                #region Print Card
                if (this.contentPanel.Controls["StdPrintCard_Pag"] != null)
                {
                    //BtnStep1.Enabled = true;
                    //BtnStep2.Enabled = true;
                    //BtnStep3.Enabled = true;
                    //BtnStep4.Enabled = true;
                    BtnStep6.Enabled = false;

                }
                #endregion
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                WizardPages.MovePageNext();
            }
            else
            {
                //This is the finish button and it has been clicked
                NotifyWizardCompleted();
            }

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CloseChildForms()
        {
            foreach (Form Frm in this.MdiChildren)
            {
                if (!Frm.Focused)
                {
                    Frm.Visible = false;
                    Frm.Dispose();
                }
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (!CheckPageIsValid())
            { return; }

            if (WizardPages.CanMoveNext)
            {
                WizardPages.MovePageLast();
                CloseChildForms();
            }
            else
            {
                //This is the finish button and it has been clicked
                NotifyWizardCompleted();
               // CloseChildForms();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void WizardHost_Load(object sender, EventArgs e)
        {
            label1.Text = DatabaseProvider.Command;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnStep2_Click(object sender, EventArgs e)
        {
            int pageIndex = 2;
            if (pageIndex != -1)
            {
                contentPanel.Controls.Clear();
                contentPanel.Controls.Add(WizardPages[pageIndex].Content);
                WizardPages[pageIndex].Load();
                UpdateNavigation();
            }
                BtnStep1.Enabled = true;
                BtnStep2.Enabled = false;
                BtnStep3.Enabled = false;
                BtnStep4.Enabled = false;
               // BtnStep5.Enabled = false;
                BtnStep6.Enabled = false;
            WizardPages.MovePagePrevious();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnStep1_Click(object sender, EventArgs e)
        {
            int pageIndex = 1;
            if (pageIndex != -1)
            {
                contentPanel.Controls.Clear();
                contentPanel.Controls.Add(WizardPages[pageIndex].Content);
                WizardPages[pageIndex].Load();
                UpdateNavigation();
               
            }

            BtnStep2.Enabled = false;
            BtnStep3.Enabled = false;
            BtnStep4.Enabled = false;
            BtnStep6.Enabled = false;
           // WizardPages.MovePagePrevious();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnStep3_Click(object sender, EventArgs e)
        {
            int pageIndex = 3;
            if (pageIndex != -1)
            {
                contentPanel.Controls.Clear();
                contentPanel.Controls.Add(WizardPages[pageIndex].Content);
                WizardPages[pageIndex].Load();
                UpdateNavigation();
            }
           // BtnStep2.Enabled = false;
            BtnStep3.Enabled = false;
            BtnStep4.Enabled = false;
            BtnStep6.Enabled = false;
            //WizardPages.MovePagePrevious();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnStep4_Click(object sender, EventArgs e)
        {
            int pageIndex = 3;
            if (pageIndex != -1)
            {
                contentPanel.Controls.Clear();
                contentPanel.Controls.Add(WizardPages[pageIndex].Content);
                WizardPages[pageIndex].Load();
                UpdateNavigation();
            }
          
            //BtnStep3.Enabled = false;
            //BtnStep4.Enabled = false;
            BtnStep6.Enabled = false;
            //WizardPages.MovePagePrevious();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnStep5_Click(object sender, EventArgs e)
        {
            int pageIndex = 4;
            if (pageIndex != -1)
            {
                contentPanel.Controls.Clear();
                contentPanel.Controls.Add(WizardPages[pageIndex].Content);
                WizardPages[pageIndex].Load();
                UpdateNavigation();

            }
            BtnStep6.Enabled = false;
         
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnStep6_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            var LastPageInfo_Pag = new LastPageInfo_Pag();
            contentPanel.Controls.Add(LastPageInfo_Pag);
            //WizardPages.MovePagePrevious();

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}