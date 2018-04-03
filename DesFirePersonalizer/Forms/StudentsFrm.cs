using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Aspose.OCR; // for reading text
using WIA;// for scanner lists
using System.IO;
using DesFirePersonalizer.Apps_Cood;
using Tesseract;
using IronOcr;
using IronOcr.Languages;
using DesFireWrapperLib;
using System.Configuration;
using FastReport.Utils;
using FastReport;


namespace DesFirePersonalizer
{
    public partial class StudentsFrm : Form
    {
        public StudentsFrm()
        {
            InitializeComponent();
        }

        /* CardStatus In ( 0 = Editable , 1 = InProcess , 2 = Active , 3 = inActive , 4 = Cancelled )   */
        /* InActiveStatus In ( 0 = Not , 1 = Cancelled , 2 = Expiryed , 3 = returned , 4 = Missing , 5 = rejected)   */
        /* IsApproved In ( -2 = rejected2, -1 = rejected1, 0 = Wiat , 1 = Approved1 , 2 = Approved2 )   */
        /* isPrinted In  ( 0 = NotPrinted , 1 = Printed )   */

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable dt;
        private string DateFormat = "MM/dd/yyyy";
        private DataTable CatDt;
        private DataTable stdt;
        private DataTable ISdt;
        private DataTable Tempdt;
        private DataTable CMdt;
        private DataTable CMPrdt;
        DatabaseProvider Provider = new DatabaseProvider();
        string GridQuery = "  SELECT StudentID,STDNatID,STDStatus ,STDFirstName,STDSecondName,STDFamilyName,"
                         + " STDCollage,STDDescription,STDBloodGroup,STDBirthDate,STDEmailID,STDMobileNo, "
                         + " STDPassportID, STDPassportIssuePlace, STDPassportEndDate, STDGender,STDimage,STDNationality,"
                         + " STDTempid FROM StudentsTable";

        string GridstQuery = " SELECT StudentID,STDTempid FROM StuEmpCatRelations";
        string TempTypeQuery = " SELECT TmpName FROM CardTemplate WHERE TmpType = 'Student";
        string CardMasterQuery = " SELECT * From CardMaster";
        string CardMasterPrintQuery = " SELECT CardID ,TmpID,StudentID,TmpType ,IsID ,STDFirstName,STDSecondName,STDFamilyName,IsNameEn,TmpType From CardInfoView ";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void StudentsFrm_Load(object sender, EventArgs e)
        {
            #region OCR Engin Load
            /*****************************************************************************************************/

            ocr = new OcrEngine();

            var deviceManager = new DeviceManager();
            // Loop through the list of devices
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                // Skip the devices if it's not a scanner 
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }
                SCDevCombox.Items.Add(deviceManager.DeviceInfos[i].Properties["Name"].get_Value());
                SCDevCombox.Items.Add(deviceManager.DeviceInfos[i].Properties["Port"].get_Value());
            }
            /*******************************Scanners Loading*******************************************************/
            #endregion
            /////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////
            #region Camera Load
            /*******************************Cameras Loading*******************************************************/
            VideoCaptureDev = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (AForge.Video.DirectShow.FilterInfo VideoCaptureDevice in VideoCaptureDev)
            {
                CamerasInfocomb.Items.Add(VideoCaptureDevice.Name);
            }
            CamerasInfocomb.SelectedIndex = 0;
            finalvedio = new VideoCaptureDevice();

            #endregion
            ////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////
            #region Load Student ID 

            TxtStdIdedit.Text = DatabaseProvider.StedentID;
            FillStudentPic1(TxtStdIdedit.Text);
            ImageCaptured.Image = StdPictureboxEdit.Image;
            if (!string.IsNullOrEmpty(TxtStdIdedit.Text))
            {
                PopulateStudentData(TxtStdIdedit.Text);
                PopulateStudentTemplates(TxtStdIdedit.Text);

            }
            #endregion
            ///////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////
            #region Card Load
            TxtNameEdit.Text = DatabaseProvider.StedentName;
            TxtIsStudentID.Text = DatabaseProvider.StedentID;
            TxtIsStudentName.Text = DatabaseProvider.StedentName;
            txtStudentPrintID.Text = DatabaseProvider.StedentID;

            FillIssuestatus();
            //  FillIssueTempType();
            CardsGridData.DataSource = FillCardsData(" WHERE StudentID LIKE '%" + TxtIsStudentID.Text + "%'");
            dvgPrintCard.DataSource = FillGridPrintCardData(" WHERE StudentID LIKE '%" + txtStudentPrintID.Text + "%'");
            PopulateTemplatesType();

            #endregion


            //FillGridPrintCard();
            // FillCategory();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region fill data 

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
        private DataTable FillIssueStatusData()
        {

            string IssueStstusQuery = "SELECT 0 as IsID,'Please select Issue Status' as IsNameEn, null as IsDescription UNION ALL SELECT IsID , IsNameEn ,IsDescription FROM  IssueState  ";

            return ISdt = DBFun.FetchData(IssueStstusQuery);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillIssuestatus()
        {
            ISdt = FillIssueStatusData();
            CmbIsIssueType.ValueMember = "IsID";
            CmbIsIssueType.DisplayMember = "IsNameEn";
            CmbIsIssueType.DataSource = ISdt;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillTemplateTypeData()
        {

            string TemplatesTypeQuery = "SELECT  TmpID,TmpType FROM  CardTemplate WHERE TmpType = 'Student'";

            return Tempdt = DBFun.FetchData(TemplatesTypeQuery);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillIsTemplatesType()
        {
            Tempdt = FillTemplateTypeData();
            CmbIsTemptype.ValueMember = "TmpID";
            CmbIsTemptype.DisplayMember = "TmpType";
            CmbIsTemptype.DataSource = Tempdt;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillstData()
        {
            return stdt = DBFun.FetchData(GridstQuery);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillstData1(string StpWhere)
        {
            return stdt = DBFun.FetchData(GridstQuery + " " + StpWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillCardsData(string pWhere)
        {
            return CMdt = DBFun.FetchData(CardMasterQuery + " " + pWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridPrintCardData(string pWhere)
        {
            return CMPrdt = DBFun.FetchData(CardMasterPrintQuery + " " + pWhere);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillGridPrintCard(string pID)
        {

            CMPrdt = DBFun.FetchData(CardMasterPrintQuery + " " + pID);
            dvgPrintCard.DataSource = CMPrdt;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region incoding parm
        /*******************************Incoding Parm*******************************************************/
        private const string DIALOGKEY = "Dialogs";

        private static String PersonalDataTag = "3F00";
        private static String FirstNameTag = "3F01";
        private static String SecondNameTag = "3F02";
        private static String SurnameTag = "3F03";
        private static String GenderTag = "3F04";
        private static String PlaceOfBirthTag = "3F05";
        private static String DateOfBirthTag = "3F06";
        private static String CountryOfOriginTag = "3F07";

        private static String UniversityDataTag = "4F00";
        private static String StudentNoTag = "4F01";
        private static String EntranceDateTag = "4F02";
        private static String MajorTag = "4F03";
        private static String DegreeProgramTag = "4F04";
        private static String InitialCreditTag = "4F05";
        private static String MinimumCreditTag = "4F06";
        private static String MaximumCreditTag = "4F07";

        private static String ImageDataTag = "5F00";

        private static String FpDataTag = "6F00";

        private String lastAccessedFolder = "";

        String newFile;
        String PersonalData;
        String UniversityData;
        String ImageData;
        String FingerPrintData;
        String InitialCreditValue;
        String InitialBookValue;
        String InitialCounterValue;
        String xmlFileTemplate;
        bool isFormatFirst;

        DesFireWrapper dsf;

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Cameras Loading
        /*******************************Cameras Loading*******************************************************/
        OcrEngine ocr;
        private FilterInfoCollection VideoCaptureDev;
        private VideoCaptureDevice finalvedio;
        private Bitmap img1;
        private Bitmap img2;

        /*****************************************************************************************************/
        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (finalvedio.IsRunning == false)
            {
                MessageBox.Show("Please Run Camera First ", "Camera Error");
            }
            else
            {
                ImageCaptured.Enabled = true;
                ImageCaptured.Image = (Bitmap)StdPictureBox.Image.Clone();
                finalvedio.Stop();
                //PicturePaanel.BackgroundImage.Clone();
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgcamera = new OpenFileDialog();
            if (dlgcamera.ShowDialog() == DialogResult.OK)
            {

                string imglocation = dlgcamera.FileName.ToString();
                ImageCaptured.ImageLocation = imglocation;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Finalvedio_NewFrame(object sender, NewFrameEventArgs eventArgs) /**video frame inside the picture box**/
        {
            Bitmap Video = (Bitmap)eventArgs.Frame.Clone();
            StdPictureBox.Image = Video;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void StartBtn_Click(object sender, EventArgs e) /**Start Camera **/
        {
            try
            {
                if (finalvedio.IsRunning == true) finalvedio.Stop();
                finalvedio = new VideoCaptureDevice(VideoCaptureDev[CamerasInfocomb.SelectedIndex].MonikerString);
                finalvedio.NewFrame += new NewFrameEventHandler(Finalvedio_NewFrame);
                finalvedio.Start();
            }
            catch
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region wizered

        private void advancedWizard1_Cancel(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are Sure need to exit", "Attentions", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TxtStdID.Text = "";
                this.Close();
                StdInformations stdfrm = new StdInformations();
                stdfrm.MdiParent = MainPage.ActiveForm;
                stdfrm.Left = 0;
                stdfrm.Top = 0;
                stdfrm.Size = this.ClientRectangle.Size;
                stdfrm.Dock = DockStyle.Fill;
                stdfrm.Show();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Cancelbtn_Click(object sender, EventArgs e)
        {

            finalvedio.Start();
            FillCategory();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void StudentsFrm_FormClosed(object sender, FormClosedEventArgs e) /** Validation**/
        {
            if (finalvedio.IsRunning == true)
            {
                finalvedio.Stop();
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region OCR 

        private void ReadBtn_Click(object sender, EventArgs e) /** read Image **/
        {

            Scanner oScanner = new Scanner();
            oScanner.Scann();
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                OCRPicturebox.Image = Image.FromFile(dlg.FileName);
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void openfiledialogbtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var Ocr = new AdvancedOcr()
                    {
                        Language = IronOcr.Languages.MultiLanguage.OcrLanguagePack(IronOcr.Languages.Arabic.OcrLanguagePack),
                        ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                        EnhanceContrast = true,
                        CleanBackgroundNoise = true,
                        ColorDepth = 4,
                        RotateAndStraighten = false,
                        DetectWhiteTextOnDarkBackgrounds = false,
                        ReadBarCodes = false,
                        Strategy = AdvancedOcr.OcrStrategy.Fast,
                        EnhanceResolution = true,
                        InputImageType = AdvancedOcr.InputTypes.Snippet
                    };
                    var image = Image.FromFile(dlg.FileName);
                    var Result = Ocr.Read(image);
                    var txt = Result.Text;
                    Console.WriteLine(txt);
                    var Resultsen = Ocr.Read(dlg.FileName);
                    // var Resultsar = ocrar.Read(dlg.FileName);

                    OcrTextbox.Text = Resultsen.Text;
                    // ocrtxtar.Text = Resultsar.Text;
                    OCRPicturebox.Image = Image.FromFile(dlg.FileName);

                }
                //    OpenFileDialog dlg = new OpenFileDialog();
                //    if (dlg.ShowDialog() == DialogResult.OK)
                //    {
                //        var ocrar = new AdvancedOcr()
                //        {
                //            CleanBackgroundNoise = true,
                //            EnhanceContrast = true,
                //            EnhanceResolution = true,
                //            Language = Arabic.OcrLanguagePack,
                //            Strategy = AdvancedOcr.OcrStrategy.Advanced,
                //            ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                //            DetectWhiteTextOnDarkBackgrounds = true,
                //            InputImageType = AdvancedOcr.InputTypes.AutoDetect,
                //            RotateAndStraighten = true,
                //            ReadBarCodes = true,
                //            ColorDepth = 4
                //        };
                //        var Ocr = new AdvancedOcr()
                //        {
                //            CleanBackgroundNoise = true,
                //            EnhanceContrast = true,
                //            EnhanceResolution = true,
                //            Language = English.OcrLanguagePack,
                //            Strategy = AdvancedOcr.OcrStrategy.Advanced,
                //            ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                //            DetectWhiteTextOnDarkBackgrounds = true,
                //            InputImageType = AdvancedOcr.InputTypes.AutoDetect,
                //            RotateAndStraighten = true,
                //            ReadBarCodes = true,
                //            ColorDepth = 4
                //        };

                //        var Resultsen = Ocr.Read(dlg.FileName);
                //        var Resultsar = ocrar.Read(dlg.FileName);

                //        OcrTextbox.Text = Resultsen.Text;
                //        ocrtxtar.Text = Resultsar.Text;
                //        OCRPicturebox.Image = Image.FromFile(dlg.FileName);


                //    }
            }
            catch (Exception)
            {

            }

            //var Ocr = new AutoOcr();
            //var Result = Ocr.Read(@"C:\\Users\\a.mustafa\\Pictures\\2018-01-01\\005.jpg");
            ////OcrTextbox.Text = Result.Text;
            //Console.WriteLine(Result.Text);
            //OpenFileDialog dlg = new OpenFileDialog();
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{



            //var imgen = new Bitmap(dlg.FileName);
            //var imgar = new Bitmap(dlg.FileName);
            //var ocrEN = new TesseractEngine(@"tessdata", "eng", EngineMode.TesseractAndCube);
            //var ocrAR = new TesseractEngine(@"tessdata", "ara", EngineMode.Default);


            //var page = ocrEN.Process(imgen);
            //var pagear = ocrAR.Process(imgar);

            //OcrTextbox.Text = page.GetText();
            //ocrtxtar.Text = pagear.GetText();

            //OCRPicturebox.Image = Image.FromFile(dlg.FileName);
            //ocr.Image = ImageStream.FromFile(dlg.FileName);
            //}


        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region incoding Personalize Cards
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool checkAndShowWarningTextBox(TextBox tb)
        {
            if (String.IsNullOrWhiteSpace(tb.Text))
            {
                MessageBox.Show("Data must not be empty",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                tb.Focus();

                return false;
            }

            return true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool formsAreAllOkay()
        {
            if (!checkAndShowWarningTextBox(FirstNameTextBox))
                return false;
            if (!checkAndShowWarningTextBox(SecondNameTextBox))
                return false;
            if (!checkAndShowWarningTextBox(SurnameTextBox))
                return false;
            if (!checkAndShowWarningTextBox(PlaceOfBirthTextBox))
                return false;

            if (!checkAndShowWarningTextBox(studentNoTextBox))
                return false;
            if (!checkAndShowWarningTextBox(MinCreditTextBox))
                return false;
            if (!checkAndShowWarningTextBox(MaxCreditTextBox))
                return false;

            return true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DialogResult formatCard()
        {
            DialogResult res = DialogResult.Cancel;

            if (String.Equals(System.Configuration.ConfigurationManager.AppSettings.Get("FormatConfirmation"), "true", StringComparison.OrdinalIgnoreCase))
            {
                res = MessageBox.Show("Format Card? (Click NO to personalize without formatting, CANCEL to abort the process)!", "Warn",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            }
            else if (String.Equals(System.Configuration.ConfigurationManager.AppSettings.Get("FormatConfirmation"), "false", StringComparison.OrdinalIgnoreCase))
            {
                //if no need for confirmation means always format
                res = DialogResult.Yes;
            }
            else
                throw new RuntimeErrorException("Wrong option on FormatConfirmation, should be true or false!");

            return res;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        String getTlvString(String tag, String data)
        {
            return tag + (data.Length / 2).ToString("X2") + data;
        }

        private String composePersonalDataTlv()
        {
            String finalTlv = null;

            finalTlv = getTlvString(FirstNameTag, MyUtil.asciiToHexString(FirstNameTextBox.Text))
                + getTlvString(SecondNameTag, MyUtil.asciiToHexString(SecondNameTextBox.Text))
                + getTlvString(SurnameTag, MyUtil.asciiToHexString(SurnameTextBox.Text))
                + getTlvString(GenderTag, MyUtil.asciiToHexString(GenderComboBox.Text))
                + getTlvString(PlaceOfBirthTag, MyUtil.asciiToHexString(PlaceOfBirthTextBox.Text))
                + getTlvString(DateOfBirthTag, MyUtil.asciiToHexString(DateOfBirthDatePicker.Text))
                + getTlvString(CountryOfOriginTag, MyUtil.asciiToHexString(CountryComboBox.Text));

            finalTlv = PersonalDataTag + (finalTlv.Length / 2).ToString("X4") + finalTlv;

            return finalTlv;

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private String composeUniversityDataTlv()
        {

            String finalTlv = null;

            finalTlv = getTlvString(StudentNoTag, MyUtil.asciiToHexString(studentNoTextBox.Text))
                + getTlvString(EntranceDateTag, MyUtil.asciiToHexString(EntranceDateTimePicker.Text))
                + getTlvString(MajorTag, MyUtil.asciiToHexString(MajorComboBox.Text))
                + getTlvString(DegreeProgramTag, MyUtil.asciiToHexString(DegreeProgramComboBox.Text))
                + getTlvString(InitialCreditTag, MyUtil.asciiToHexString(initialCreditTextBox.Text))
                + getTlvString(MinimumCreditTag, MyUtil.asciiToHexString(MinCreditTextBox.Text))
                + getTlvString(MaximumCreditTag, MyUtil.asciiToHexString(MaxCreditTextBox.Text));

            finalTlv = UniversityDataTag + (finalTlv.Length / 2).ToString("X4") + finalTlv;

            return finalTlv;
        }
        private String getInitialiCreditInHex()
        {
            String credit = initialCreditTextBox.Text;

            int dec = Convert.ToInt32(credit, 10);
            return dec.ToString("X8");

        }

        private String getInitialBookCreditInHex()
        {
            String credit = MaxAllowedBook.Text;

            int dec = Convert.ToInt32(credit, 10);
            return dec.ToString("X8");

        }

        private String getInitialCounterInHex()
        {
            String counter = ConfigurationManager.AppSettings.Get("InitialCounterValue");

            int dec = Convert.ToInt32(counter, 10);
            return dec.ToString("X8");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void PersonalizeButton_Click(object sender, EventArgs e)
        {
            string newfilename = FirstNameTextBox.Text + DateTime.Now.ToString("_yyyyMMdd_hmmtt");

            if (!formsAreAllOkay())
                return;

            DialogResult confirmation = formatCard();

            bool doFormat = false;
            Student aStudent = null;

            if (confirmation == DialogResult.Yes || confirmation == DialogResult.No)
            {
                doFormat = confirmation == DialogResult.Yes ? true : false;

                aStudent = new Student(dsf, System.Configuration.ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), newfilename);
                aStudent.fillStudentData1(composePersonalDataTlv(), composeUniversityDataTlv(), getInitialiCreditInHex(), getInitialBookCreditInHex(),
                 getInitialCounterInHex(), doFormat);
            }

            aStudent.LoadXml(true);

            try
            {
                aStudent.WriteToCard();

                MessageBox.Show("Card Personalized Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region incoding Read Cards

        private void parsePersonalData(String data)
        {
            int index = 0;
            int len = 0;
            String content = null;

            if (data == null || String.IsNullOrWhiteSpace(data))
            {
                MessageBox.Show("Data is not containing Personal Data", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String header = data.Substring(index, 4);
            index += 4;
            String lenStr = data.Substring(index, 4);
            index += 4;

            if (String.Equals(PersonalDataTag, header, StringComparison.OrdinalIgnoreCase) == false)
            {
                MessageBox.Show("Data is not containing Personal Data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            while (index < data.Length)
            {
                header = data.Substring(index, 4);
                index += 4;
                lenStr = data.Substring(index, 2);
                index += 2;

                len = Convert.ToInt16(lenStr, 16);
                content = data.Substring(index, len * 2);
                index += len * 2;

                if (String.Equals(header, FirstNameTag, StringComparison.OrdinalIgnoreCase))
                    FirstNameTextBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, SecondNameTag, StringComparison.OrdinalIgnoreCase))
                    SecondNameTextBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, SurnameTag, StringComparison.OrdinalIgnoreCase))
                    SurnameTextBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, GenderTag, StringComparison.OrdinalIgnoreCase))
                    GenderComboBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, PlaceOfBirthTag, StringComparison.OrdinalIgnoreCase))
                    PlaceOfBirthTextBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, DateOfBirthTag, StringComparison.OrdinalIgnoreCase))
                {
                    String date = MyUtil.ConvertHextoAscii(content);
                    DateTime theDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                    DateOfBirthDatePicker.Value = theDate;
                }
                else if (String.Equals(header, CountryOfOriginTag, StringComparison.OrdinalIgnoreCase))
                    CountryComboBox.Text = MyUtil.ConvertHextoAscii(content);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        private void parseUniversityData(String data)
        {
            int index = 0;
            int len = 0;
            String content = null;

            if (data == null || String.IsNullOrWhiteSpace(data))
            {
                MessageBox.Show("Data is not containing University Data", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            String header = data.Substring(index, 4);
            index += 4;
            String lenStr = data.Substring(index, 4);
            index += 4;

            if (String.Equals(UniversityDataTag, header, StringComparison.OrdinalIgnoreCase) == false)
            {
                MessageBox.Show("Data is not containing University Data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            while (index < data.Length)
            {
                header = data.Substring(index, 4);
                index += 4;
                lenStr = data.Substring(index, 2);
                index += 2;

                len = Convert.ToInt16(lenStr, 16);
                content = data.Substring(index, len * 2);
                index += len * 2;

                if (String.Equals(header, StudentNoTag, StringComparison.OrdinalIgnoreCase))
                    studentNoTextBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, EntranceDateTag, StringComparison.OrdinalIgnoreCase))
                {
                    String date = MyUtil.ConvertHextoAscii(content);
                    DateTime theDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                    EntranceDateTimePicker.Value = theDate;
                }
                else if (String.Equals(header, MajorTag, StringComparison.OrdinalIgnoreCase))
                    MajorComboBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, DegreeProgramTag, StringComparison.OrdinalIgnoreCase))
                    DegreeProgramComboBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, InitialCreditTag, StringComparison.OrdinalIgnoreCase))
                    initialCreditTextBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, MinimumCreditTag, StringComparison.OrdinalIgnoreCase))
                    MinCreditTextBox.Text = MyUtil.ConvertHextoAscii(content);
                else if (String.Equals(header, MaximumCreditTag, StringComparison.OrdinalIgnoreCase))
                    MaxCreditTextBox.Text = MyUtil.ConvertHextoAscii(content);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        private void readCardButton_Click_1(object sender, EventArgs e)
        {
            String newfilename = FirstNameTextBox.Text + DateTime.Now.ToString("yyyyMMdd_hmmtt");

            try
            {
                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), newfilename);

                aStudent.LoadXml(false);
                aStudent.readAStudentCard();

                aStudent.fillStudentData(aStudent.getContentFromAFile(Student.PERSONAL_DATA),
                    aStudent.getContentFromAFile(Student.UNIVERSITY_DATA),
                    aStudent.getContentFromAFile(Student.PHOTO_DATA),
                    aStudent.getContentFromAFile(Student.FINGERPRINT_DATA),
                    getInitialiCreditInHex(),
                    getInitialBookCreditInHex(),
                    getInitialCounterInHex(),
                    false);

                aStudent.LoadXml(true);

                parsePersonalData(aStudent.getContentFromAFile(Student.PERSONAL_DATA));
                parseUniversityData(aStudent.getContentFromAFile(Student.UNIVERSITY_DATA));
                // parsePhotoData(aStudent.getContentFromAFile(Student.PHOTO_DATA));
                //parseFingerPrintData(aStudent.getContentFromAFile(Student.FINGERPRINT_DATA));

                MessageBox.Show("Card Read Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ResetButton_Click_1(object sender, EventArgs e)
        {
            FirstNameTextBox.Clear();
            SecondNameTextBox.Clear();
            SurnameTextBox.Clear();
            PlaceOfBirthTextBox.Clear();
            studentNoTextBox.Clear();
        }

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Print Card
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void CreateCheckBoxHeader()
        {
            CheckBox checkboxHeader = new CheckBox();
            Rectangle rect = dvgPrintCard.GetCellDisplayRectangle(0, -1, true);
            rect.X = rect.Location.X + (rect.Width / 4);
            //rect.X = rect.Location.X + (rect.Width4);
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(16, 16);
            checkboxHeader.BackColor = ColorTranslator.FromHtml("#F0F0F0");

            if (Setting.Lang == "En") { checkboxHeader.Location = new Point(50, 2); } else if (Setting.Lang == "Ar") { checkboxHeader.Location = new Point(909, 10); }
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dvgPrintCard.Controls.Add(checkboxHeader);

            dvgPrintCard.ReadOnly = false;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dvgPrintCard.RowCount; i++)
            {
                dvgPrintCard[0, i].Value = ((CheckBox)dvgPrintCard.Controls.Find("checkboxHeader", true)[0]).Checked;
            }
            dvgPrintCard.EndEdit();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //public string getValue()
        //{
        //string sCategory = string.Empty;
        //// ComboboxItem empItem = (ComboboxItem)cmbEmpCat.SelectedItem;
        ////string value = empItem.Value;

        ////if (!string.IsNullOrEmpty(txtEmpID.Text)) { if (value != "Emp") { sCategory = value; } }
        ////return sCategory;
        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSearchCard_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtStudentPrintID.Text))
            //{
            //    General.ShowMsg("Please enter Employee ID", "الرجاء إدخال رقم موظف"); return;
            //    //if (Setting.Type == "CardView") { General.ShowMsg("Please enter Card ID", "الرجاء إدخال رقم بطاقة"); return; }
            //    //else { General.ShowMsg("Please enter Employee ID", "الرجاء إدخال رقم موظف"); return; }
            //}
            //FillGridPrintCard(txtStudentPrintID.Text);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCancelCard_Click(object sender, EventArgs e)
        {
            txtStudentPrintID.Text = "";
            FillGridPrintCard("");
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnPreviewCard_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tcard.Text) || string.IsNullOrEmpty(tTemplate.Text)) { return; }
                DataRow dr = GetCurrentRow(dvgPrintCard);
                if (dr == null) { General.ShowMsg("please select card", "يجب اختيار بطاقة"); return; }
                object[] vData = dr.ItemArray;
                FastReport.Report rpt = new FastReport.Report();
                string TmpID = tTemplate.Text;
                DataTable VieDt;
                VieDt = DBFun.FetchData("SELECT TmpStringEdited FROM CardTemplate WHERE TmpID = " + TmpID + "");
                if (DBFun.IsNullOrEmpty(VieDt)) { General.ShowMsg("Template not available", "النموذج غير موجود"); return; }
                rpt.LoadFromString(VieDt.Rows[0]["TmpStringEdited"].ToString());
                rpt.SetParameterValue("cardid", tcard.Text);
                rpt.Dictionary.Connections[0].ConnectionString = SQlConnect.SQL;
                rpt.Show();
            }
            catch (Exception EX) { General.ShowErr("You can not update the Template, please contact your system administrator", "لا يمكن تحديث النموذج,الرجاء الاتصال بمدير النظام"); }
            //  catch (Exception EX) { General.ShowErr(EX.ToString(), "You can not preview the card, please contact your system administrator", "لا يمكن مشاهدة البطاقة,الرجاء الاتصال بمدير النظام"); }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnPrintCard_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GetCurrentRow(dvgPrintCard);
                if (dr == null) { General.ShowMsg("please select card", "يجب اختيار بطاقة"); return; }
                object[] vData = dr.ItemArray;


                // Thread.CurrentThread.CurrentCulture = CultureEn;
                string PrintDate = DateTime.Now.ToString(DateFormat);   //DateTime.Now.ToString("MM/dd/yyyy"); , CultureEn.DateTimeFormat


                if (string.IsNullOrEmpty(tcard.Text) || string.IsNullOrEmpty(tTemplate.Text)) { return; }
                string CardID = tcard.Text; //vData[1].ToString(); // (string)dvgPrintCard[1, g].EditedFormattedValue;
                string TmpID = tTemplate.Text; //(string)dvgPrintCard[2, g].EditedFormattedValue;
                DataTable Tdt = DBFun.FetchData("SELECT TmpStringEdited FROM CardTemplate WHERE TmpID = " + TmpID + "");
                if (DBFun.IsNullOrEmpty(Tdt)) { General.ShowMsg("Template not available", "النموذج غير موجود"); return; }

                FastReport.Report rpt = new FastReport.Report();
                rpt.LoadFromString(Tdt.Rows[0]["TmpStringEdited"].ToString());
                rpt.SetParameterValue("cardid", CardID);
                //rpt.PrintSettings.Printer = "HDP5000 Card Printer";
                rpt.PrintSettings.ShowDialog = false;
                rpt.Dictionary.Connections[0].ConnectionString = SQlConnect.SQL;

                string UQ = "";
                if (Setting.Type != "CardPrint")
                {
                    // Update all CardStatus for employee to InActive 
                    DBFun.ExecuteData("UPDATE CardMaster Set CardStatus = 3 WHERE CardStatus IN (0,1) AND StudentID = '" + tEmpID.Text + "' AND CardID != " + CardID + "");

                    UQ = "UPDATE CardMaster SET CardStatus = 2, isPrinted = 1,PrintedBy = '" + DatabaseProvider.gLoginName + "',PrintedDate = '" + PrintDate + "' WHERE CardID = " + CardID + ""; //
                }

                int res = DBFun.ExecuteData(UQ);
                rpt.Print();


                FillGridPrintCard(" WHERE StudentID LIKE '%" + tEmpID.Text + "%'");
            }
            catch (Exception EX) { General.ShowErr("You can not print the card, please contact your system administrator", "لا يمكن طباعة البطاقة,الرجاء الاتصال بمدير النظام"); }
            //catch (Exception EX) { General.ShowErr(EX.ToString(), "You can not print the card, please contact your system administrator", "لا يمكن طباعة البطاقة,الرجاء الاتصال بمدير النظام"); }
            finally { FillGridPrintCard(""); }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void dvgPrintCard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool isChecked = (bool)dvgPrintCard[0, e.RowIndex].EditedFormattedValue;
                dvgPrintCard.Rows[e.RowIndex].Cells[0].Value = !isChecked;
                dvgPrintCard.EndEdit();
            }
            catch (Exception EX) { }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void dvgPrintCard_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int iCrdID = dvgPrintCard.Columns.IndexOf(dvgPrintCard.Columns["CardID"]);
                int iTmpID = dvgPrintCard.Columns.IndexOf(dvgPrintCard.Columns["TmpID"]);
                int iEmpID = dvgPrintCard.Columns.IndexOf(dvgPrintCard.Columns["StudentID"]);

                if (e.RowIndex != -1) { tcard.Text = dvgPrintCard[iCrdID, e.RowIndex].Value.ToString(); }
                if (e.RowIndex != -1) { tTemplate.Text = dvgPrintCard[iTmpID, e.RowIndex].Value.ToString(); }
                if (e.RowIndex != -1) { tEmpID.Text = dvgPrintCard[iEmpID, e.RowIndex].Value.ToString(); }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnDeleteCrd_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = GetCurrentRow(dvgPrintCard);
                if (dr == null) { General.ShowMsg("please select card", "يجب اختيار بطاقة"); return; }
                object[] vData = dr.ItemArray;

                // Thread.CurrentThread.CurrentCulture = CultureEn;
                string DeleteDate = DateTime.Now.ToString(DateFormat);   //DateTime.Now.ToString("MM/dd/yyyy");, CultureEn.DateTimeFormat

                if (string.IsNullOrEmpty(tcard.Text)) { return; }
                string CardID = tcard.Text; //vData[1].ToString(); // (string)dvgPrintCard[1, g].EditedFormattedValue;

                string UQ = "";
                if (Setting.Type == "CardPrint")
                {
                    UQ = "UPDATE CardMaster SET CardStatus = 4, CancelBy = '" + Setting.UserID + "',CancelDate = '" + DeleteDate + "' WHERE CardID = " + CardID + ""; //
                }
                try
                {
                    int res = DBFun.ExecuteData(UQ);
                    General.ShowMsg("Card Deleted", "تم حذف البطاقة");
                }
                catch { }

                FillGridPrintCard("");
            }
            catch (Exception EX) { General.ShowErr("You can not print the card, please contact your system administrator", "لا يمكن طباعة البطاقة,الرجاء الاتصال بمدير النظام"); }
            // catch (Exception EX) { General.ShowErr(EX.ToString(), "You can not print the card, please contact your system administrator", "لا يمكن طباعة البطاقة,الرجاء الاتصال بمدير النظام"); }
            //finally { FillGridPrintCard(""); }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnUpdateExpireDT_Click(object sender, EventArgs e)
        {
            //DataRow dr = GetCurrentRow(dvgPrintCard);
            //if (dr == null) { General.ShowMsg("please select card", "يجب اختيار بطاقة"); return; }
            //object[] vData = dr.ItemArray;

            ////Thread.CurrentThread.CurrentCulture = CultureEn;
            ////string DeleteDate = DateTime.Now.ToString(DateFormat, CultureEn.DateTimeFormat);   //DateTime.Now.ToString("MM/dd/yyyy");

            //if (string.IsNullOrEmpty(tcard.Text)) { return; }
            //string CardID = tcard.Text; //vData[1].ToString(); // (string)dvgPrintCard[1, g].EditedFormattedValue;

            ////ExpireDateFrm EDFrom = new ExpireDateFrm(CardID);
            ////EDFrom.ShowDialog();

        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Add Update Delete stedunts

        private void advancedWizard1_Finish(object sender, EventArgs e)
        {
            // this.advancedWizered1.CurrentPage.Equals(FinalPage);
            if (this.advancedWizered1.CurrentPage.Equals(FinalPage))
            {
                MessageBox.Show("User Addedd Succesfully", "Done");
                TxtStdID.Text = "";
                this.Close();
                StdInformations stdfrm = new StdInformations();
                stdfrm.MdiParent = MainPage.ActiveForm;
                stdfrm.Left = 0;
                stdfrm.Top = 0;
                stdfrm.Size = this.ClientRectangle.Size;
                stdfrm.Dock = DockStyle.Fill;
                stdfrm.Show();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void advancedWizard1_Back(object sender, EventArgs e)
        {
            if (finalvedio.IsRunning == true)
            {
                finalvedio.Stop();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void OnActiveStepChanged(object sender, EventArgs e)
        {

            // If the ActiveStep is changing to Step2, check to see whether the 
            //// CheckBox1 CheckBox is selected.  If it is, skip to the Step2 step.
            //if (advancedWizardPage1.ActiveStepIndex == Wizard1.WizardSteps.IndexOf(this.WizardStep2))
            //{
            //    if (this.CheckBox1.Checked)
            //    {
            //        Wizard1.ActiveStepIndex = Wizard1.WizardSteps.IndexOf(this.WizardStep3);
            //    }
            //}
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void advancedWizard1_Next(object sender, AdvancedWizardControl.EventArguments.WizardEventArgs e)
        {

            if (finalvedio.IsRunning == true)
            {
                finalvedio.Stop();
            }
            //first clicl -- commad is add new user 
            if (TxtStdIdedit.Text == "")
            {
                FillCategoryData();
                try
                {
                    if (TxtStdID.Text != "")
                    {
                        dt = FillGridData("WHERE StudentID = '" + TxtStdID.Text + " ' "); //
                        stdt = FillstData1("WHERE StudentID = '" + TxtStdID.Text + " ' ");
                        if (DBFun.IsNullOrEmpty(dt) || DBFun.IsNullOrEmpty(stdt))
                        {
                            //Provider.InsertNewStudent(TxtStdID.Text, TxtNationalID.Text, TxtPassportID.Text, CombCollage.Items[0].ToString(), TxtPassportIssuePlace.Text,
                            //CombBloodType.Items[0].ToString(), PassportEndDate.Value, CombGender.Items[0].ToString(), ChecBoxActive.Checked.ToString(),
                            //cmbnationality.Items[0].ToString(), TxtStdFirstName.Text, TxtStdSecondName.Text, TxtStdLastName.Text,
                            //TxtMobile.Text, TxtEmail.Text, StdDateBirth.Value, cmbTempType.SelectedValue.ToString(),
                            //TxtStdDesc.Text, ImageCaptured.Image);
                            //Provider.Insertstdcat(TxtStdID.Text, cmbTempType.SelectedValue.ToString());
                            //MessageBox.Show("Student Added Succesfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TxtStdIdedit.Text = TxtStdID.Text;
                        }
                        else
                        {
                            MessageBox.Show("Student ID Already Exist Please find another id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //  StudentInformation.ActiveForm = StudentInformation.Focus; ///= StudentInformation
                        //StudentsFrm stdfrm = new StudentsFrm();
                        //this.Hide();
                        //stdfrm.MdiParent = MainPage.ActiveForm;
                        //stdfrm.Left = 0;
                        //stdfrm.Top = 0;
                        //stdfrm.Size = this.ClientRectangle.Size;
                        //stdfrm.Dock = DockStyle.Fill;
                        //stdfrm.Show();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //  MessageBox.Show("Please Fill All Fields to can go next step", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            else
            {

                try
                {
                    dt = FillGridData("WHERE StudentID = '" + TxtStdID.Text + " ' ");
                    stdt = FillstData1("WHERE StudentID = '" + TxtStdID.Text + " ' ");
                    if (
                         TxtNationalID.Text != dt.Rows[0]["STDNatID"].ToString() ||
                         TxtPassportID.Text != (dt.Rows[0]["STDPassportID"]).ToString() ||
                         CombCollage.Text.ToString() != Convert.ToString(dt.Rows[0]["STDCollage"]) ||
                         TxtPassportIssuePlace.Text != (dt.Rows[0]["STDPassportIssuePlace"]).ToString() ||
                         CombBloodType.Text.ToString() != Convert.ToString(dt.Rows[0]["STDBloodGroup"]) ||
                         PassportEndDate.Value.ToString() != (dt.Rows[0]["STDPassportEndDate"]).ToString() ||
                         CombGender.Text.ToString() != Convert.ToString(dt.Rows[0]["STDGender"]) ||
                         ChecBoxActive.Checked.ToString() != Convert.ToString(dt.Rows[0]["STDStatus"]) ||
                         cmbnationality.Text.ToString() != Convert.ToString(dt.Rows[0]["STDNationality"]) ||
                         TxtStdFirstName.Text != Convert.ToString(dt.Rows[0]["STDFirstName"]) ||
                         TxtStdSecondName.Text != Convert.ToString(dt.Rows[0]["STDSecondName"]) ||
                         TxtStdLastName.Text != Convert.ToString(dt.Rows[0]["STDFamilyName"]) ||
                         TxtMobile.Text != Convert.ToString(dt.Rows[0]["STDMobileNo"]) ||
                         TxtEmail.Text != Convert.ToString(dt.Rows[0]["STDEmailID"]) ||
                         StdDateBirth.Value.ToString() != (dt.Rows[0]["STDBirthDate"]).ToString() ||
                         cmbTempType.Text.ToString() != (dt.Rows[0]["STDTempid"]).ToString() ||
                         cmbTempType.Text.ToString() != (stdt.Rows[0]["STDTempid"]).ToString() ||
                         TxtStdDesc.Text != (dt.Rows[0]["STDDescription"].ToString()) ||
                         StdPictureboxEdit.Image.ToString() != ImageCaptured.Image.ToString()
                         )
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you Sure need update Informations ", "Attentions", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (!DBFun.IsNullOrEmpty(dt))

                            {
                                //Provider.UpdateStudentInfo(TxtStdID.Text, TxtNationalID.Text.ToString(), TxtPassportID.Text, CombCollage.Text.ToString(), TxtPassportIssuePlace.Text,
                                //CombBloodType.Text.ToString(), PassportEndDate.Value, CombGender.Text.ToString(), ChecBoxActive.Checked.ToString(),
                                //cmbnationality.Text.ToString(), TxtStdFirstName.Text, TxtStdSecondName.Text, TxtStdLastName.Text,
                                //TxtMobile.Text, TxtEmail.Text, StdDateBirth.Value, cmbTempType.SelectedValue.ToString(),
                                //TxtStdDesc.Text, ImageCaptured.Image);
                                //Provider.Updatestdcat(TxtStdID.Text, cmbTempType.SelectedValue.ToString());
                                //FillData();
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            if (!string.IsNullOrEmpty(TxtStdIdedit.Text))
                            {
                                PopulateStudentData(TxtStdIdedit.Text);
                                PopulateStudentTemplates(TxtStdIdedit.Text);

                            }
                        }
                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool ImageCompareArray(Bitmap firstImage, Bitmap secondImage)
        {
            bool flag = true;
            string firstPixel;
            string secondPixel;


            if (firstImage.Width == secondImage.Width
                && firstImage.Height == secondImage.Height)
            {
                for (int i = 0; i < firstImage.Width; i++)
                {
                    for (int j = 0; j < firstImage.Height; j++)
                    {
                        firstPixel = firstImage.GetPixel(i, j).ToString();
                        secondPixel = secondImage.GetPixel(i, j).ToString();
                        if (firstPixel != secondPixel)
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                if (flag == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void TxtStdID_TextChanged(object sender, EventArgs e)
        {
            //cmbTempType.SelectedValue
        }
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            // cmbTempType.Text = DRs["STDTempid"].ToString();
            TxtPassportID.Text = dr["STDPassportID"].ToString();
            TxtPassportIssuePlace.Text = dr["STDPassportIssuePlace"].ToString();
            ChecBoxActive.Checked = Convert.ToBoolean(dr["STDStatus"].ToString());
            TxtEmail.Text = dr["STDEmailID"].ToString();
            StdDateBirth.Text = (dr["STDBirthDate"].ToString());
            PassportEndDate.Text = (dr["STDPassportEndDate"].ToString());
            TxtStdDesc.Text = dr["STDDescription"].ToString();
            FillStudentPic(TxtStdIdedit.Text);
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
        public void PopulateTemplatesType()
        {
            FillIsTemplatesType();
            Tempdt = DBFun.FetchData(TempTypeQuery); // + " WHERE TmpType = 'Student' "
            DataRow DRs = (DataRow)Tempdt.Rows[0];
            string e = DRs["TmpType"].ToString();
            CmbIsTemptype.Text = DRs["TmpType"].ToString();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //public void PopulateIssueCard()
        //{
        //    FillIssuestatus();
        //     ISdt = DBFun.FetchData(TissueStatus );
        //    DataRow DRs = (DataRow)Tempdt.Rows[0];
        //    string e = DRs["TmpType"].ToString();
        //    CmbIsTemptype.Text = DRs["TempType"].ToString();
        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void FillStudentPic(string StdPic) // A-E-D-S-C
        {
            DataTable StdPicture = DBFun.FetchData(" SELECT StudentID,STDimage FROM StudentsTable WHERE StudentID = '" + TxtStdIdedit.Text + "'");
            if (!DBFun.IsNullOrEmpty(StdPicture))
            {
                if (StdPicture.Rows[0]["STDimage"] != DBNull.Value)
                {
                    ImageCaptured.Image = General.byteArrayToImage((byte[])StdPicture.Rows[0]["STDimage"]);
                }

            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void FillStudentPic1(string StdPic) // A-E-D-S-C
        {
            DataTable StdPicture = DBFun.FetchData(" SELECT StudentID,STDimage FROM StudentsTable WHERE StudentID = '" + TxtStdIdedit.Text + "'");
            if (!DBFun.IsNullOrEmpty(StdPicture))
            {
                if (StdPicture.Rows[0]["STDimage"] != DBNull.Value)
                {
                    StdPictureboxEdit.Image = General.byteArrayToImage((byte[])StdPicture.Rows[0]["STDimage"]);
                }

            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Issue Cards 

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (TxtIsStudentID.Text != "" && CmbIsIssueType.SelectedValue.ToString() != "0")
                {
                    DateTime StartDate = DateTime.Parse(Convert.ToDateTime(DateTimeISStart.Text).ToShortDateString());
                    DateTime EndDate = DateTime.Parse(Convert.ToDateTime(DateTimeISEnd.Text).ToShortDateString());
                    if (StartDate >= EndDate)
                    {
                        MessageBox.Show("End Date Less than DateStart ");
                        return;
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                    CMdt = DBFun.FetchData("select * from CardMaster where EmpID = '" + CmbIsStatus.SelectedValue + TxtIsStudentID.Text.Trim() + "'  AND isPrinted = 1 ");  //AND CardStatus = 2
                    if (!DBFun.IsNullOrEmpty(CMdt) && CmbIsStatus.SelectedValue.ToString() == "1")
                    {
                        MessageBox.Show("This employee has a previous card can not be issued a new card type", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    ////if (DBFun.IsNullOrEmpty(CMdt) && CmbIsStatus.SelectedValue.ToString() != "1")
                    ////{
                    ////    MessageBox.Show("This employee does not have a card can not be issued previous card of the specified type", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////    return;
                    ////}

                    //CMdt = DBFun.FetchData(" WHERE StudentID = '" + TxtIsStudentID.Text + " ' ");  
                    //if (DBFun.IsNullOrEmpty(CMdt) && CmbIsStatus.SelectedValue.ToString() == "Cancel")
                    //{
                    //    MessageBox.Show("You Cant Use this option in card status", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    CMdt = FillCardsData("WHERE StudentID = '" + TxtIsStudentID.Text + " ' "); //
                    if (DBFun.IsNullOrEmpty(CMdt))
                    {
                        Provider.IssueNewCards(TxtIsStudentID.Text, Int32.Parse(CmbIsIssueType.SelectedValue.ToString()),
                         DateTimeISStart.Value, DateTimeISEnd.Value, TxtIsDesc.Text, Int32.Parse(CmbIsStatus.SelectedIndex.ToString()), Int32.Parse(CmbIsTemptype.SelectedValue.ToString()));
                        MessageBox.Show("card issued Succesfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Student Already have Card please restore old card", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("You Cant Issue Card from the specified Card , Procces will Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

