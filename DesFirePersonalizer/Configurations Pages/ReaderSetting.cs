using PCSC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesFirePersonalizer.Apps_Cood;
using DesFireWrapperLib;
using System.Drawing.Imaging;
using System.IO;
using DesFirePersonalizer.Apps_Cod;

namespace DesFirePersonalizer.Configurations_Pages
{
    public partial class ReaderSetting : Form
    {
        SCardContext scc = null;
        SCardReader scr = null;
        SCardMonitor scm = null;
        Boolean isConnected = false;
        String[] readername = null;
        SCardState scs;
        SCardProtocol scp;
        byte[] atr = null;
        string atrStr = null;
        private static readonly IContextFactory _contextFactory = ContextFactory.Instance;
        delegate void SetTextCallbackWithStatus(string text, Boolean connStatus);
        delegate void SetTextCallback(string text);
        string[] CardReaderList = null;
        Random randomInstance;

        int index = 0;

        public ReaderSetting()
        {
            InitializeComponent();
        }

        DataTable dt;

        string GridQuery = "  SELECT StudentID,STDNatID,STDStatus ,STDFirstName,STDSecondName,STDFamilyName,"
              + " STDCollage,STDDescription,STDBloodGroup,STDBirthDate,STDEmailID,STDMobileNo, "
              + " STDPassportID, STDPassportIssuePlace, STDPassportEndDate, STDGender,STDimage,STDNationality,PlaceOfBirth,"
              + " STDTempid FROM StudentsTable";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region PopulateData

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void populateDataForm()
        {
            /*Populate the country combo box*/
            String countryFile = ConfigurationManager.AppSettings.Get("CountryFile");

            //Only load the file if it exists
            if (System.IO.File.Exists(countryFile))
            {
                //Read in the file line-by-line
                string[] Lines = System.IO.File.ReadAllLines(countryFile);

                //Add the items to the ComboBox
                foreach (string Line in Lines)
                    CountryComboBox.Items.Add(Line);
            }
            else
                throw new CountryFileNotFoundException("Cannot find file: " + countryFile);

            CountryComboBox.SelectedIndex = 0;
            GenderComboBox.SelectedIndex = 0;

            /*Populate the country combo box*/
            String majorFile = ConfigurationManager.AppSettings.Get("MajorFile");

            //Only load the file if it exists
            if (System.IO.File.Exists(majorFile))
            {
                //Read in the file line-by-line
                string[] Lines = System.IO.File.ReadAllLines(majorFile);

                //Add the items to the ComboBox
                foreach (string Line in Lines)
                    MajorComboBox.Items.Add(Line);
            }
            else
                throw new CountryFileNotFoundException("Cannot find file: " + majorFile);

            MajorComboBox.SelectedIndex = 0;
            DegreeProgramComboBox.SelectedIndex = 0;

            MaxAllowedBook.Text = ConfigurationManager.AppSettings.Get("MaxBookAllowed");

            MinCreditTextBox.Text = ConfigurationManager.AppSettings.Get("MinimumCredit");
            MaxCreditTextBox.Text = ConfigurationManager.AppSettings.Get("MaximumCredit");
            initialCreditTextBox.Text = ConfigurationManager.AppSettings.Get("InitialCredit");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void redoRegisterEvent()
        {
            if (scc != null)
            {
                deRegisterEvent();
                scm.Cancel();
                scm.Dispose();
                scm = null;

                scc.Cancel();
                scc.Dispose();
                scc = null;
            }

            scc = new SCardContext();
            scc.Establish(SCardScope.System);

            scm = new SCardMonitor(_contextFactory, SCardScope.System);

            try
            {
                registerEvent();
                scm.Start(cardReaderToolStrip.Text);
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("No Card Readers found\n" + ane.Message);
                SmartCardStatusLabel.Text = ane.Message;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DisplayEventAndDisconnect(string eventName, CardStatusEventArgs unknown)
        {
            Action setStatus = () =>
            {
                SmartCardStatusLabel.Text = eventName + "," + unknown.State;
                tryToDisconnect();
            };

            if (InvokeRequired)
                Invoke(setStatus);
            else
                setStatus();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void StateChangeMethod(object sender, StatusChangeEventArgs ea)
        {
            Action setStatus = () =>
            {
                SmartCardStatusLabel.Text = ea.NewState.ToString();

                if ((ea.NewState & SCRState.Present) == SCRState.Present)
                {
                    tryToConnect();
                }
            };

            if (InvokeRequired)
                Invoke(setStatus);
            else
                setStatus();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MonitorExceptionMethod(object sender, PCSCException ex)
        {
            MessageBox.Show(SCardHelper.StringifyError(ex.SCardError) + "\nPlease make sure the correct reader is selected in Options > Settings",
                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //MessageBox.Show("Monitor Exception Happened! Error Message = " + SCardHelper.StringifyError(ex.SCardError),
            //    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void deRegisterEvent()
        {
            scm.StatusChanged -= StateChangeMethod;
            scm.CardInserted -= (sender, args) => DisplayEventAndConnect("CardInserted", args);
            scm.CardRemoved -= (sender, args) => DisplayEventAndDisconnect("CardRemoved", args);
            scm.Initialized -= (sender, args) => DisplayEvent("Initialized", args);
            scm.MonitorException -= MonitorExceptionMethod;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DisplayEvent(string eventName, CardStatusEventArgs unknown)
        {
            Action setStatus = () =>
            {
                SmartCardStatusLabel.Text = eventName + "," + unknown.State;

                if ((unknown.State & SCRState.Present) == SCRState.Present)
                {
                    tryToConnect();
                }

            };

            try
            {
                if (InvokeRequired)
                    Invoke(setStatus);
                else
                    setStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please try again! Place the DesFire card correctly on the reader and make sure it has been personalized before!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DisplayEventAndConnect(string eventName, CardStatusEventArgs unknown)
        {
            Action setStatus = () =>
            {
                SmartCardStatusLabel.Text = eventName + "," + unknown.State;
                tryToConnect();

                executeCounter(isConnected);

            };

            try
            {
                if (InvokeRequired)
                    Invoke(setStatus);
                else
                    setStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please try again! Place the DesFire card correctly on the reader and make sure it has been personalized before!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void executeCounter(bool isConnected)
        {
            if (isConnected)
            {

                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                aStudent.LoadXml(false);

                ValueFile vf = aStudent.getCounterValueFromFile();

                CounterValueTextBox.Clear();
                CounterValueTextBox.Text = Convert.ToInt32(vf.value_hex, 16).ToString("D");


            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void tryToConnect()
        {
            try
            {
                isConnected = false;

                if (scc == null || scc.IsValid() == false)
                {
                    scc = new SCardContext();
                    scc.Establish(SCardScope.User);
                }

                if (scr != null)
                {
                    scr.Status(out readername, out scs, out scp, out atr);
                }

                scr = new SCardReader(scc);

                SCardError sce = scr.Connect(ConfigurationManager.AppSettings.Get("CardReader"), SCardShareMode.Shared, SCardProtocol.Any);

                dsf = new DesFireWrapper(scr);

                if (sce == SCardError.Success)
                {
                    scr.Status(out readername, out scs, out scp, out atr);
                    isConnected = true;
                    atrStr = String.Concat(atr.Select(b => b.ToString("X2")));

                    validateDesFireAndUpdateStatusBar();
                }
            }
            catch (Exception msge)
            {
                MessageBox.Show("tryToConnect: " + msge.Message);
            }
            finally
            {
                updateSmartStatus(isConnected);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void validateDesFireAndUpdateStatusBar()
        {
            if (isConnected)
            {
                string uid = "";
                string size = "";
                DesFireWrappeResponse dfw_resp = dsf.getCardInformation(ref uid, ref size);

                if (uid != null)
                    uidStatusLabel.Text = uid;

                if (size != null)
                    sizeStatusLabel.Text = size;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void registerEvent()
        {
            scm.StatusChanged += new StatusChangeEvent(StateChangeMethod);
            scm.CardInserted += (sender, args) => DisplayEventAndConnect("CardInserted", args);
            scm.CardRemoved += (sender, args) => DisplayEventAndDisconnect("CardRemoved", args);
            scm.Initialized += (sender, args) => DisplayEvent("Initialized", args);
            scm.MonitorException += MonitorExceptionMethod;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void tryToDisconnect()
        {
            try
            {
                if (scc.IsValid())
                {
                    SCardError sce = scr.Disconnect(SCardReaderDisposition.Eject);

                    if (sce == SCardError.Success)
                    {
                        scr.Status(out readername, out scs, out scp, out atr);
                        isConnected = false;

                        atr = null;
                        atrStr = null;

                        updateSmartStatus(isConnected);
                    }
                    scc = null;
                }
            }
            catch (Exception msge)
            {
                MessageBox.Show(msge.Message);
                if (scr != null)
                    scr.Status(out readername, out scs, out scp, out atr);

                isConnected = false;

                atr = null;
                atrStr = null;

                updateSmartStatus(isConnected);

            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void updateSmartStatus(Boolean connStatus)
        {
            if (connStatus)
            {
                SetAtrStatusLabel(atrStr, false);
            }
            else
            {

                SetAtrStatusLabel("Disconnected!", false);

                uidStatusLabel.Text = "N/A";

                sizeStatusLabel.Text = "N/A";
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SetAtrStatusLabel(String text, Boolean isLink)
        {
            if (mainStatus.IsDisposed == false)
            {
                if (this.mainStatus.InvokeRequired)
                {
                    SetTextCallbackWithStatus d = new SetTextCallbackWithStatus(SetAtrStatusLabel);
                    this.Invoke(d, new object[] { text, isLink });
                }
                else
                {
                    toolStripAtrLabel.IsLink = isLink;
                    toolStripAtrLabel.Text = text;
                }
            }

        }
        #endregion
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
            try
            {
                String newfilename = FirstNameTextBox.Text + DateTime.Now.ToString("_yyyyMMdd_hmmtt");

                if (!formsAreAllOkay())
                    return;

                DialogResult confirmation = formatCard();

                bool doFormat = false;
                Student aStudent = null;

                if (confirmation == DialogResult.Yes || confirmation == DialogResult.No)
                {
                    doFormat = confirmation == DialogResult.Yes ? true : false;

                    aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), newfilename);
                    aStudent.fillStudentData(composePersonalDataTlv(), composeUniversityDataTlv(), composeImageDataTlv(), composeFingerPrintDataTlv(),
                        getInitialiCreditInHex(), getInitialBookCreditInHex(), getInitialCounterInHex(), doFormat);//
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
            catch (Exception ex)
            {
               
            }
            
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string composeImageDataTlv()
        {

            String finalTlv = null;

            bool isNullOrEmpty = HolderPictureBox == null || HolderPictureBox.Image == null;

            if (isNullOrEmpty)
            {
                MessageBox.Show("Image must not be empty",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HolderPictureBox.Focus();

                return null;
            }

            ImageFormat format = ImageFormat.Jpeg;

            if (ImageFormat.Jpeg.Equals(HolderPictureBox.Image.RawFormat))
            {
                // JPEG
                format = ImageFormat.Jpeg;
            }
            else if (ImageFormat.Png.Equals(HolderPictureBox.Image.RawFormat))
            {
                // PNG
                format = ImageFormat.Png;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                HolderPictureBox.Image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                //// Convert byte[] to Base64 String
                //string base64String = Convert.ToBase64String(imageBytes);
                //string hexString = asciiToHexString(base64String);

                string hexString = BitConverter.ToString(imageBytes).Replace("-", string.Empty);

                finalTlv = ImageDataTag + (hexString.Length / 2).ToString("X4") + hexString;

            }

            return finalTlv;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string composeFingerPrintDataTlv()
        {
            String finalTlv = null;

            bool isNullOrEmpty = HolderFPpictureBox == null || HolderFPpictureBox.Image == null;

            if (isNullOrEmpty)
            {
                MessageBox.Show("Fingerprint must not be empty",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HolderFPpictureBox.Focus();

                return null;
            }

            ImageFormat format = ImageFormat.Jpeg;

            if (ImageFormat.Jpeg.Equals(HolderPictureBox.Image.RawFormat))
            {
                // JPEG
                format = ImageFormat.Jpeg;
            }
            else if (ImageFormat.Png.Equals(HolderPictureBox.Image.RawFormat))
            {
                // PNG
                format = ImageFormat.Png;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                HolderFPpictureBox.Image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                //// Convert byte[] to Base64 String
                //string base64String = Convert.ToBase64String(imageBytes);
                //string hexString = asciiToHexString(base64String);

                string hexString = BitConverter.ToString(imageBytes).Replace("-", string.Empty);

                finalTlv = FpDataTag + (hexString.Length / 2).ToString("X4") + hexString;
            }

            return finalTlv;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region incoding Read & Reset Cards
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                lenStr = data.Substring(index, 4);
                index += 4;

                len = Convert.ToInt16(lenStr, 16);
                content = data.Substring(index, len * 4);
                index += len * 4;

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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void parseStudentIDData(String data)
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
                studentNoTextBox.Text = MyUtil.ConvertHextoAscii(content);
                MessageBox.Show("Data is not containing University Data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            while (index < data.Length)
            {
                header = data.Substring(index, 4);
                index += 4;
                lenStr = data.Substring(index, 4);
                index += 4;

                len = Convert.ToInt16(lenStr, 16);
                content = data.Substring(index, len * 4);
                index += len * 4;

                if (String.Equals(header, StudentNoTag, StringComparison.OrdinalIgnoreCase))
                {
                    studentNoTextBox.Text = MyUtil.ConvertHextoAscii(content);
                }

                   
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void parsePhotoData(String data)
        {
            int index = 0;
            int len = 0;
            String content = null;

            if (data == null || String.IsNullOrWhiteSpace(data))
            {
                MessageBox.Show("Data is not containing Photo Data", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            String header = data.Substring(index, 4);
            index += 4;
            String lenStr = data.Substring(index, 4);
            index += 4;
            len = Convert.ToInt16(lenStr, 16);
            content = data.Substring(index, len * 2);
            index += len * 2;

            if (String.Equals(ImageDataTag, header, StringComparison.OrdinalIgnoreCase) == false)
            {
                MessageBox.Show("Data is not containing Photo Data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] imageBytes = MyUtil.StringToByteArray(content);
            Image image;
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
            }
            HolderPictureBox.Image = image;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void parseFingerPrintData(String data)
        {
            int index = 0;
            int len = 0;
            String content = null;

            if (data == null || String.IsNullOrWhiteSpace(data))
            {
                MessageBox.Show("Data is not containing Fingerprint Data", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String header = data.Substring(index, 4);
            index += 4;
            String lenStr = data.Substring(index, 4);
            index += 4;
            len = Convert.ToInt16(lenStr, 16);
            content = data.Substring(index, len * 2);
            index += len * 2;

            if (String.Equals(FpDataTag, header, StringComparison.OrdinalIgnoreCase) == false)
            {
                MessageBox.Show("Data is not containing Fingerprint Data", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] imageBytes = MyUtil.StringToByteArray(content);
            Image image;
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
            }
            HolderFPpictureBox.Image = image;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                parsePhotoData(aStudent.getContentFromAFile(Student.PHOTO_DATA));
                parseFingerPrintData(aStudent.getContentFromAFile(Student.FINGERPRINT_DATA));

                MessageBox.Show("Card Read Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ResetButton_Click_1(object sender, EventArgs e)
        {
            FirstNameTextBox.Clear();
            SecondNameTextBox.Clear();
            SurnameTextBox.Clear();
            PlaceOfBirthTextBox.Clear();
            studentNoTextBox.Clear();
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void PersonalDataTableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ReaderSetting_Load(object sender, EventArgs e)
        {

            scc = new SCardContext();
            scc.Establish(SCardScope.System);

            CardReaderList = scc.GetReaders();
            if (CardReaderList != null && CardReaderList.Length >= 1)
            {
                index = 0;
                CardReaderComboBox.Items.AddRange(CardReaderList);

                for (int i = 0; i < CardReaderList.Length; i++)
                {
                    if (CardReaderList[i].Contains(ConfigurationManager.AppSettings.Get("CardReader")))
                    {
                        index = i;
                        break;
                    }
                }

                CardReaderComboBox.SelectedIndex = index;
            }

            randomInstance = new Random();
            RdnBtnTransID_Click(null, null);
            RdmBtnAppData_Click(null, null);
            scc.Cancel();
            scc.Dispose();
            scc = null;
            loadFromSettings();
            redoRegisterEvent();

            //  populateDataForm();

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void RndBtnTermID_Click(object sender, EventArgs e)
        {
            Int64 number = randomInstance.Next();
            TerminalIDEPurse.Text = number.ToString("X8");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void RdnBtnTransID_Click(object sender, EventArgs e)
        {
            Int64 number = randomInstance.Next();
            TransactionIDEPurse.Text = number.ToString("X8");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void RdmBtnAppData_Click(object sender, EventArgs e)
        {
            Int64 number = randomInstance.Next();
            String a = number.ToString("X8");

            number = randomInstance.Next();
            String b = number.ToString("X8");

            number = randomInstance.Next();
            String c = number.ToString("X8");

            ApplicationDataEPurse.Text = a + b + c;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void CardReaderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CardReaderList != null && CardReaderList.Length >= 1)
            {
                index = CardReaderComboBox.SelectedIndex;

                UpdateSetting("CardReader", CardReaderComboBox.SelectedItem.ToString());
            }
            loadFromSettings();
            UpdateSetting("CardReader", cardReaderToolStrip.Text);

            //redoRegisterEvent();
            tryToConnect();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void loadFromSettings()
        {
            cardReaderToolStrip.Text = ConfigurationManager.AppSettings.Get("CardReader");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void parseAndDisplayLogBook1(String log)
        {
            ReadCards bl = new ReadCards(log);

            TxtStdInc.Text = bl.book.STDID;

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void scancard()
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
                    parseStudentIDData(aStudent.getContentFromAFile(Student.UNIVERSITY_DATA));


                    MessageBox.Show("Card Read Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            catch (Exception ex)
            {
                MessageBox.Show("Please pe sure the reader connected or card placed on the reader");

            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void PopulateStudentData(string STDpID)
        {
            dt = DBFun.FetchData(GridQuery + " WHERE StudentID = '" + STDpID + "'");
            DataRow dr = (DataRow)dt.Rows[0];

            FirstNameTextBox.Text = dr["STDFirstName"].ToString();
            SecondNameTextBox.Text = dr["STDSecondName"].ToString();
            SurnameTextBox.Text = dr["STDFamilyName"].ToString();
            GenderComboBox.Text = dr["STDGender"].ToString();
            PlaceOfBirthTextBox.Text = dr["PlaceOfBirth"].ToString();
            DateOfBirthDatePicker.Text = dr["STDBirthDate"].ToString();
            CountryComboBox.Text = dr["STDNationality"].ToString();
            studentNoTextBox.Text = dr["StudentID"].ToString();
            MajorComboBox.Text = dr["STDCollage"].ToString();
            DegreeProgramComboBox.Text = dr["STDCollage"].ToString();
            //EntranceDateTimePicker.Text = dr["PlaceOfBirth"].ToString();

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            if (btnSearchandScan.Text == "Scan Card")
            {
                scancard();
                //PopulateStudentData(TxtStdInc.Text);
            }
            else
            {
                PopulateStudentData(TxtStdInc.Text);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Scan Card")
            {
                TxtStdInc.Text = "";
                TxtStdInc.Enabled = false;
                btnSearchandScan.Text = "Scan Card";
                PersonalizeButton.Enabled = true;
                readCardButton.Enabled = true;
                ResetButton.Enabled = true;


            }
            else if (comboBox1.Text == "Student ID")
            {
                TxtStdInc.Text = "";
                TxtStdInc.Text = "";
                TxtStdInc.Enabled = true;
                btnSearchandScan.Text = "Search";
                PersonalizeButton.Enabled = true;
                readCardButton.Enabled = true;
                ResetButton.Enabled = true;
            }
        }

        private void CounterValueTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
