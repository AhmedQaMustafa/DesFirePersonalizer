using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesFirePersonalizer.Apps_Cood;
using DesFireWrapperLib;
using System.Configuration;

namespace DesFirePersonalizer.Wizered_Pages
{
    public partial class FrmStdIncoding : Form
    {
        DataTable dt;

        string GridQuery = "  SELECT StudentID,STDNatID,STDStatus ,STDFirstName,STDSecondName,STDFamilyName,"
              + " STDCollage,STDDescription,STDBloodGroup,STDBirthDate,STDEmailID,STDMobileNo, "
              + " STDPassportID, STDPassportIssuePlace, STDPassportEndDate, STDGender,STDimage,STDNationality,PlaceOfBirth,"
              + " STDTempid FROM StudentsTable";

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public FrmStdIncoding()
        {
            InitializeComponent();
        }

        private void FrmStdIncoding_Load(object sender, EventArgs e)
        {
            studentNoTextBox.Text = DatabaseProvider.StedentID;
            if (!string.IsNullOrEmpty(studentNoTextBox.Text))
            {
                PopulateStudentData(studentNoTextBox.Text);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void PopulateStudentData(string STDpID)
        {
            dt = DBFun.FetchData(GridQuery + " WHERE StudentID = '" + STDpID + "'");
            DataRow dr = (DataRow)dt.Rows[0];
            studentNoTextBox.Text = dr["StudentID"].ToString();
            FirstNameTextBox.Text = dr["STDFirstName"].ToString();
            SecondNameTextBox.Text = dr["STDSecondName"].ToString();
            SurnameTextBox.Text = dr["STDFamilyName"].ToString();
            CountryComboBox.Text = dr["STDNationality"].ToString();
            MajorComboBox.Text = dr["STDCollage"].ToString();
            GenderComboBox.Text = dr["STDGender"].ToString();
            PlaceOfBirthTextBox.Text = dr["PlaceOfBirth"].ToString();
            DateOfBirthDatePicker.Text = dr["STDBirthDate"].ToString();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private String getInitialiCreditInHex()
        {
            String credit = initialCreditTextBox.Text;

            int dec = Convert.ToInt32(credit, 10);
            return dec.ToString("X8");

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private String getInitialBookCreditInHex()
        {
            String credit = MaxAllowedBook.Text;

            int dec = Convert.ToInt32(credit, 10);
            return dec.ToString("X8");

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region incoding Read & Reset Cards

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
    }
}
