using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesFirePersonalizer.Apps_Cood;
using System.IO;


namespace DesFirePersonalizer.Wizered_Pages
{
    public partial class FrmWizered : Form
    {
        Form[] frm = { new StdEdInfo_page(), new FrmStdtakPic(), new FrmStdIncoding(), new FrmIssueCard(),
                       new FrmStdPrintCard(), new FrmLastPageInfo()};

        DatabaseProvider Provider = new DatabaseProvider();
        int top = -1;
        int count = 6;
        string valueID = ""; string valueNat = ""; string valueFiNa = ""; string valuePassID = ""; string valueSeNa = ""; string valueFaNa = ""; string TxtStdIdedit = "";
        string valuePasIsPl = ""; string valueMobile = ""; string valueEmail = ""; string valueNationality = ""; string valueCollage = ""; string valueDesc = "";
        string valueBlTy = ""; string valueGender = ""; string valueTempTy = ""; ValueType valueDateBirth; ValueType valuePassEndDate; string valueActNotAct = "";
        string valueTempTyUpdate = ""; string valueNationalityUpdate = ""; string valueCollageUpdate = ""; string valueGenderUpdate = ""; string valueBlTyUpdate = "";
        string valueTempTynew = ""; Image valueImage; Image valueImageCaptured; Bitmap valueImage1 = null ; Bitmap valueImageCaptured1 = null;
        string valuePlaceOfBirthUpdate = ""; string valuePlaceOfBirth= "";
        DataTable dt;
        DataTable stdt;
        string GridQuery = "  SELECT StudentID,STDNatID,STDStatus ,STDFirstName,STDSecondName,STDFamilyName,"
                 + " STDCollage,STDDescription,STDBloodGroup,STDBirthDate,STDEmailID,STDMobileNo, "
                 + " STDPassportID, STDPassportIssuePlace, STDPassportEndDate, STDGender,STDimage,STDNationality,PlaceOfBirth,"
                 + " STDTempid FROM StudentsTable";
        string GridstQuery = " SELECT StudentID,STDTempid FROM StuEmpCatRelations";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public FrmWizered()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FrmWizered_Load(object sender, EventArgs e)
        {
            Next();
            label1.Text = DatabaseProvider.Command;
            callFormsControlsNew();
        }
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
        #region Wizered Load Forms && Calls Controls
        private void LoadForm()
        {
            frm[top].TopLevel = false;
            frm[top].AutoScroll = true;
            frm[top].Dock = DockStyle.Fill;
            this.pnlContent.Controls.Clear();
            this.pnlContent.Controls.Add(frm[top]);
            frm[top].Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Back()
        {
            top--;

            if (top <= -1)
            {
                return;
            }
            else
            {
                btnBack.Enabled = true;
                btnNext.Enabled = true;
                LoadForm();
                if (top - 1 <= -1)
                {
                    btnBack.Enabled = false;
                }
            }

            if (top >= count)
            {
                btnNext.Enabled = false;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Next()
        {

            top++;
            if (top >= count)
            {
                return;
            }
            else
            {
                btnBack.Enabled = true;
                btnNext.Enabled = true;
                LoadForm();
                if (top + 1 == count)
                {
                    btnNext.Enabled = false;
                }
            }

            if (top <= 0)
            {
                btnBack.Enabled = false;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void callFormsControlsNew()
        {

            Control[] TxtStdID = frm[top].Controls.Find("TxtStdID", true);
            if (TxtStdID[0] is TextBox) { valueID = ((TextBox)TxtStdID[0]).Text; }
            //////////////////////////////////////////////////////////////////////////////
            Control[] TxtNationalID = frm[top].Controls.Find("TxtNationalID", true);
            if (TxtNationalID[0] is TextBox) { valueNat = ((TextBox)TxtNationalID[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtPassportID = frm[top].Controls.Find("TxtPassportID", true);
            if (TxtPassportID[0] is TextBox) { valuePassID = ((TextBox)TxtPassportID[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdFirstName = frm[top].Controls.Find("TxtStdFirstName", true);
            if (TxtStdFirstName[0] is TextBox) { valueFiNa = ((TextBox)TxtStdFirstName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdSecondName = frm[top].Controls.Find("TxtStdSecondName", true);
            if (TxtStdSecondName[0] is TextBox) { valueSeNa = ((TextBox)TxtStdSecondName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdLastName = frm[top].Controls.Find("TxtStdLastName", true);
            if (TxtStdLastName[0] is TextBox) { valueFaNa = ((TextBox)TxtStdLastName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtPassportIssuePlace = frm[top].Controls.Find("TxtPassportIssuePlace", true);
            if (TxtPassportIssuePlace[0] is TextBox) { valuePasIsPl = ((TextBox)TxtPassportIssuePlace[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtMobile = frm[top].Controls.Find("TxtMobile", true);
            if (TxtMobile[0] is TextBox) { valueMobile = ((TextBox)TxtMobile[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdDesc = frm[top].Controls.Find("TxtStdDesc", true);
            if (TxtStdDesc[0] is TextBox) { valueDesc = ((TextBox)TxtStdDesc[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtEmail = frm[top].Controls.Find("TxtEmail", true);
            if (TxtEmail[0] is TextBox) { valueEmail = ((TextBox)TxtEmail[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] cmbnationality = frm[top].Controls.Find("cmbnationality", true);
            if (cmbnationality[0] is ComboBox) { valueNationality = ((ComboBox)cmbnationality[0]).Items[0].ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombBloodType = frm[top].Controls.Find("CombBloodType", true);
            if (CombBloodType[0] is ComboBox) { valueBlTy = ((ComboBox)CombBloodType[0]).Items[0].ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombCollage = frm[top].Controls.Find("CombCollage", true);
            if (CombCollage[0] is ComboBox) { valueCollage = ((ComboBox)CombCollage[0]).Items[0].ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombGender = frm[top].Controls.Find("CombGender", true);
            if (CombGender[0] is ComboBox) { valueGender = ((ComboBox)CombGender[0]).Items[0].ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] cmbTempType = frm[top].Controls.Find("cmbTempType", true);
            if (cmbTempType[0] is ComboBox) { valueTempTy = ((ComboBox)cmbTempType[0]).Items.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CmbPlaceOfBirth = frm[top].Controls.Find(" CmbPlaceOfBirth", true);
            if (CmbPlaceOfBirth[0] is ComboBox) { valuePlaceOfBirth = ((ComboBox)CmbPlaceOfBirth[0]).Items.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] StdDateBirth = frm[top].Controls.Find("StdDateBirth", true);
            if (StdDateBirth[0] is DateTimePicker) { valueDateBirth = ((DateTimePicker)StdDateBirth[0]).Value; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] PassportEndDate = frm[top].Controls.Find("PassportEndDate", true);
            if (PassportEndDate[0] is DateTimePicker) { valuePassEndDate = ((DateTimePicker)PassportEndDate[0]).Value; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] ChecBoxActive = frm[top].Controls.Find("ChecBoxActive", true);
            if (ChecBoxActive[0] is CheckBox) { valueActNotAct = ((CheckBox)ChecBoxActive[0]).Checked.ToString(); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void callFormsControlsUpdate()
        {
            Control[] TxtStdID = frm[top].Controls.Find("TxtStdID", true);
            if (TxtStdID[0] is TextBox) { valueID = ((TextBox)TxtStdID[0]).Text; }
            //////////////////////////////////////////////////////////////////////////////
            Control[] TxtNationalID = frm[top].Controls.Find("TxtNationalID", true);
            if (TxtNationalID[0] is TextBox) { valueNat = ((TextBox)TxtNationalID[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtPassportID = frm[top].Controls.Find("TxtPassportID", true);
            if (TxtPassportID[0] is TextBox) { valuePassID = ((TextBox)TxtPassportID[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdFirstName = frm[top].Controls.Find("TxtStdFirstName", true);
            if (TxtStdFirstName[0] is TextBox) { valueFiNa = ((TextBox)TxtStdFirstName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdSecondName = frm[top].Controls.Find("TxtStdSecondName", true);
            if (TxtStdSecondName[0] is TextBox) { valueSeNa = ((TextBox)TxtStdSecondName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdLastName = frm[top].Controls.Find("TxtStdLastName", true);
            if (TxtStdLastName[0] is TextBox) { valueFaNa = ((TextBox)TxtStdLastName[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtPassportIssuePlace = frm[top].Controls.Find("TxtPassportIssuePlace", true);
            if (TxtPassportIssuePlace[0] is TextBox) { valuePasIsPl = ((TextBox)TxtPassportIssuePlace[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtMobile = frm[top].Controls.Find("TxtMobile", true);
            if (TxtMobile[0] is TextBox) { valueMobile = ((TextBox)TxtMobile[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtStdDesc = frm[top].Controls.Find("TxtStdDesc", true);
            if (TxtStdDesc[0] is TextBox) { valueDesc = ((TextBox)TxtStdDesc[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] TxtEmail = frm[top].Controls.Find("TxtEmail", true);
            if (TxtEmail[0] is TextBox) { valueEmail = ((TextBox)TxtEmail[0]).Text; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] cmbnationalityUpdate = frm[top].Controls.Find("cmbnationality", true);  
            if (cmbnationalityUpdate[0] is ComboBox) { valueNationalityUpdate = ((ComboBox)cmbnationalityUpdate[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombBloodTypeUpdate = frm[top].Controls.Find("CombBloodType", true);
            if (CombBloodTypeUpdate[0] is ComboBox) { valueBlTyUpdate = ((ComboBox)CombBloodTypeUpdate[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombCollageUpdate = frm[top].Controls.Find("CombCollage", true);
            if (CombCollageUpdate[0] is ComboBox) { valueCollageUpdate = ((ComboBox)CombCollageUpdate[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CombGenderUpdate = frm[top].Controls.Find("CombGender", true);
            if (CombGenderUpdate[0] is ComboBox) { valueGenderUpdate = ((ComboBox)CombGenderUpdate[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] cmbTempTypeUpdate = frm[top].Controls.Find("cmbTempType", true);
            if (cmbTempTypeUpdate[0] is ComboBox) { valueTempTyUpdate = ((ComboBox)cmbTempTypeUpdate[0]).SelectedValue.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] StdDateBirth = frm[top].Controls.Find("StdDateBirth", true);
            if (StdDateBirth[0] is DateTimePicker) { valueDateBirth = ((DateTimePicker)StdDateBirth[0]).Value; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] PassportEndDate = frm[top].Controls.Find("PassportEndDate", true);
            if (PassportEndDate[0] is DateTimePicker) { valuePassEndDate = ((DateTimePicker)PassportEndDate[0]).Value; }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] CmbPlaceOfBirth = frm[top].Controls.Find("CmbPlaceOfBirth", true);
            if (CmbPlaceOfBirth[0] is ComboBox) { valuePlaceOfBirthUpdate = ((ComboBox)CmbPlaceOfBirth[0]).SelectedItem.ToString(); }
            ///////////////////////////////////////////////////////////////////////////////
            Control[] ChecBoxActive = frm[top].Controls.Find("ChecBoxActive", true);
            if (ChecBoxActive[0] is CheckBox) { valueActNotAct = ((CheckBox)ChecBoxActive[0]).Checked.ToString(); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void callFormsControlsImage()
        {
             Control [] StdPictureboxEdit = frm[top].Controls.Find("StdPictureboxEdit", true);
            if (StdPictureboxEdit[0] is PictureBox) { valueImage = ((PictureBox)StdPictureboxEdit[0]).Image; }
            //////////////////////////////////////////////////////////////////////////////
            Control[] ImageCaptured = frm[top].Controls.Find("ImageCaptured", true);
            //FrmStdtakPic.ImageCaptured.Image = pctImage.Image.Clone() as Image;
            if (ImageCaptured[0] is PictureBox) { valueImageCaptured = ((PictureBox)ImageCaptured[0]).Image; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                #region StdEditIformations 
                if (this.pnlContent.Controls.Contains(frm[0]))
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
                                Control[] cmbTempTypeNew = frm[top].Controls.Find("cmbTempType", true);
                                if (cmbTempTypeNew[0] is ComboBox) { valueTempTynew = ((ComboBox)cmbTempTypeNew[0]).SelectedValue.ToString(); }
                                Provider.InsertNewStudent(valueID, valueNat, valuePassID, valueCollage, valuePasIsPl, valueBlTy, valuePassEndDate, valueGender, valueActNotAct,
                                                          valueNationality, valueFiNa, valueSeNa, valueFaNa, valueMobile, valueEmail, valueDateBirth, valueTempTynew, valueDesc ,valuePlaceOfBirth);
                                // TxtStdDesc.Text, ImageCaptured.Image
                                Provider.Insertstdcat(valueID, valueTempTynew);
                                //TxtStdIdedit = valueID;
                                label1.Text = "Update";
                                Next();
                            }
                            else
                            {
                                MessageBox.Show("Student ID Already Exist Please find another id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                                //  Next();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
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
                                            valueDesc,valuePlaceOfBirthUpdate);//, ImageCaptured.Image
                                            Provider.Updatestdcat(valueID, valueTempTyUpdate);
                                            FillData();
                                            Next();
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
                                        Next();
                                    }
                                }
                                else
                                {
                                    Next();
                                    LblSteps.Text = "StdPicStep";
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
                }
                #endregion
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #region Studentd Picture

                   if (this.pnlContent.Controls.Contains(frm[1]) && LblSteps.Text == "StdPicStep")
                   {
                       LblSteps.Text = "Stop";
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
                                               LblSteps.Text = "StdIncStep";
                                               Next();
                                           }
                                       }
                                       else if (dialogResult == DialogResult.No)
                                       {
                                        LblSteps.Text = "StdIncStep";
                                        Next();
                                       }

                               }

                                   if (top == 2)
                                   {
                                       Next();
                                   }

                           }
                           else
                                   {
                                       Next();
                                       frm[1].TopLevel = false;
                                       frm[1].AutoScroll = true;
                                       frm[1].Dock = DockStyle.Fill;
                                       this.pnlContent.Controls.Clear();
                                       this.pnlContent.Controls.Add(frm[1]);
                                       frm[1].Show();
                                   }

                       }
                       catch (Exception ex)
                       {
                           MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }
                   }
                   #endregion
                   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                   #region Encoding 
                     if (this.pnlContent.Controls.Contains(frm[2]))
                   {
                       //if ()
                       //{
                       //}
                   }
                   #endregion
                   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnBack_Click(object sender, EventArgs e)
        {
            callFormsControlsImage();
            Back();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    }
}
