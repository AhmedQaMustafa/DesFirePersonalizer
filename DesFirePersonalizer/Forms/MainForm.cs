using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PCSC;
using Microsoft.Win32;
using System.Linq;
using DesFireWrapperLib;
using System.Collections.Generic;
using NHibernate;
using System.Collections;

namespace DesFirePersonalizer
{
    public partial class MainForm : Form
    {
        delegate void SetTextCallbackWithStatus(string text, Boolean connStatus);
        delegate void SetTextCallback(string text);

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

        Boolean isConnected = false;
        String[] readername = null;
        SCardState scs;
        SCardProtocol scp;
        byte[] atr = null;
        string atrStr = null;
        SCardContext scc = null;
        SCardReader scr = null;
        SCardMonitor scm = null;

        DesFireWrapper dsf;

        bool FormWasReset = true;
        int mode = 0;
        private static int NOTHING = 0;
        private static int KITCHEN_PURCHASE = 1;
        private static int KITCHEN_TOPUP = 2;
        private static int KITCHEN_BALANCE = 3;

        private static readonly IContextFactory _contextFactory = ContextFactory.Instance;

        IList<Model.Book> BookList = null;
        IList<Model.Menu> MenuList = null;

        Random randomInstance;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public MainForm()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void BrowsePictureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog imagOpenDlg = new OpenFileDialog();

            imagOpenDlg.InitialDirectory = lastAccessedFolder;
            imagOpenDlg.Title = "Browse Image Files";

            imagOpenDlg.CheckFileExists = true;
            imagOpenDlg.CheckPathExists = true;

            imagOpenDlg.DefaultExt = "jpg";
            //imagOpenDlg.Filter = "image files (*.png)|*.jpg|All files (*.*)|*.*";
            //imagOpenDlg.Filter = "image files|*.png;*.jpg| All files (*.*)|*.*";
            imagOpenDlg.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            imagOpenDlg.FilterIndex = 0;
            imagOpenDlg.RestoreDirectory = true;

            imagOpenDlg.ReadOnlyChecked = true;
            imagOpenDlg.ShowReadOnly = true;

            if (imagOpenDlg.ShowDialog() == DialogResult.OK)
            {
                //save the last accessed folder
                UpdateSetting("LastAccessedFolder", Path.GetDirectoryName(imagOpenDlg.FileName));

                int max_image_size = Convert.ToInt16(ConfigurationManager.AppSettings.Get("MaxImageSize"), 10);

                FileInfo fi = new FileInfo(imagOpenDlg.FileName);
                if (fi.Length > max_image_size)
                {
                    MessageBox.Show("Image is oversized, maximum allowed is = " + ConfigurationManager.AppSettings.Get("MaxImageSize"),
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                //load image to picture holder
                HolderPictureBox.Load(imagOpenDlg.FileName);
            }
        }

        private void readerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            sf.Parent = Parent;
            sf.StartPosition = FormStartPosition.CenterParent;
            sf.ShowDialog();

            loadFromSettings();
            UpdateSetting("CardReader", cardReaderToolStrip.Text);
            redoRegisterEvent();
            tryToConnect();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            

            try
            {
                              
                loadFromSettings();
                redoRegisterEvent();
                populateDataForm();

                randomInstance = new Random();
                //RndBtnTermID_Click(null, null);
                TerminalIDEPurse.Text = ConfigurationManager.AppSettings.Get("TerminalID");
                RdnBtnTransID_Click(null, null);
                RdmBtnAppData_Click(null, null);

                listAvailableBook();
                listAvailableMenu();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void listAvailableBook()
        {
            DataFactory df = null;
            ISession session = null;
            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            if (File.Exists(dbFile) == false)
            {
                createDB();
            }

            df = new DataFactory(dbFile, false);
            session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = session.CreateSQLQuery("Select * from Book").AddEntity(typeof(Model.Book));

                        BookList = query.List<Model.Book>();

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }

            BookNoComboBox.DataSource = BookList;
            BookNoComboBox.DisplayMember = "BookID";
            BookNoComboBox.ValueMember = "BookID";

            BookTitleTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Title;
            BookAuthorTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Author;
            BookAvailabilityTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Availability == true ? "IN" : "OUT";

            TerminalIDLibComboBox.SelectedIndex = 0;
        }

        private void listAvailableMenu()
        {
            DataFactory df = null;
            ISession session = null;
            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            if (File.Exists(dbFile) == false)
            {
                createDB();
            }

            df = new DataFactory(dbFile, false);
            session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = session.CreateSQLQuery("Select * from Menu").AddEntity(typeof(Model.Menu));
                        MenuList = query.List<Model.Menu>();
                        MenuDataGridView.DataSource = MenuList;

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
            
            DisplayMenuNameListBox.DataSource = MenuList;
            DisplayMenuNameListBox.DisplayMember = "MenuName";
            DisplayMenuNameListBox.ValueMember = "MenuName";

            DisplayPriceTextBox.Text = MenuList.ElementAt(DisplayMenuNameListBox.SelectedIndex).Price;
        }


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

        private void loadFromSettings()
        {
            cardReaderToolStrip.Text = ConfigurationManager.AppSettings.Get("CardReader");
        }

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

        private void registerEvent()
        {
            scm.StatusChanged += new StatusChangeEvent(StateChangeMethod);
            scm.CardInserted += (sender, args) => DisplayEventAndConnect("CardInserted", args);
            scm.CardRemoved += (sender, args) => DisplayEventAndDisconnect("CardRemoved", args);
            scm.Initialized += (sender, args) => DisplayEvent("Initialized", args);
            scm.MonitorException += MonitorExceptionMethod;
        }

        private void deRegisterEvent()
        {
            scm.StatusChanged -= StateChangeMethod;
            scm.CardInserted -= (sender, args) => DisplayEventAndConnect("CardInserted", args);
            scm.CardRemoved -= (sender, args) => DisplayEventAndDisconnect("CardRemoved", args);
            scm.Initialized -= (sender, args) => DisplayEvent("Initialized", args);
            scm.MonitorException -= MonitorExceptionMethod;
        }

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

        private void executeCounter(bool isConnected)
        {
            if (isConnected)
            {
                if (MainTabControl.SelectedTab == MainTabControl.TabPages["counterTabPage"])
                {
                    Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                    aStudent.LoadXml(false);

                    if (incrementToolStripMenuItem.Checked)
                    {
                        aStudent.doIncreaseCounter();
                    }
                    else if (decrementToolStripMenuItem.Checked)
                    {
                        aStudent.doDecreaseCounter();
                    }

                    ValueFile vf = aStudent.getCounterValueFromFile();

                    CounterValueTextBox.Clear();
                    CounterValueTextBox.Text = Convert.ToInt32(vf.value_hex, 16).ToString("D");

                    
                }
            }
        }

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

        private void MonitorExceptionMethod(object sender, PCSCException ex)
        {
            MessageBox.Show(SCardHelper.StringifyError(ex.SCardError) + "\nPlease make sure the correct reader is selected in Options > Settings",
                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //MessageBox.Show("Monitor Exception Happened! Error Message = " + SCardHelper.StringifyError(ex.SCardError),
            //    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary></summary>
        protected override void OnCreateControl()
        {
            LoadSettings();
            base.OnCreateControl();
        }

        /// <summary></summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings();
            base.OnClosing(e);
        }

        /// <summary>Saves the form's settings.</summary>
        public void SaveSettings()
        {
            RegistryKey dialogKey = Application.UserAppDataRegistry.CreateSubKey(DIALOGKEY);
            if (dialogKey != null)
            {
                RegistryKey formKey = dialogKey.CreateSubKey(this.GetType().ToString());
                if (formKey != null)
                {
                    formKey.SetValue("Left", this.Left);
                    formKey.SetValue("Top", this.Top);
                    formKey.Close();
                }
                dialogKey.Close();
            }
        }

        /// <summary></summary>
        public void LoadSettings()
        {
            RegistryKey dialogKey = Application.UserAppDataRegistry.OpenSubKey(DIALOGKEY);
            if (dialogKey != null)
            {
                RegistryKey formKey = dialogKey.OpenSubKey(this.GetType().ToString());
                if (formKey != null)
                {
                    this.Left = (int)formKey.GetValue("Left");
                    this.Top = (int)formKey.GetValue("Top");
                    formKey.Close();
                }
                dialogKey.Close();
            }
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

    



        String getTlvString(String tag, String data)
        {
            return tag + (data.Length / 2).ToString("X2") + data;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            FirstNameTextBox.Clear();
            SecondNameTextBox.Clear();
            SurnameTextBox.Clear();
            PlaceOfBirthTextBox.Clear();
            studentNoTextBox.Clear();

            HolderPictureBox.Image = null;
            HolderPictureBox.Invalidate();
            HolderPictureBox.InitialImage = null;

            HolderFPpictureBox.Image = null;
            HolderFPpictureBox.Invalidate();
            HolderFPpictureBox.InitialImage = null;
        }

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

        private DialogResult formatCard()
        {
            DialogResult res = DialogResult.Cancel;

            if (String.Equals(ConfigurationManager.AppSettings.Get("FormatConfirmation"), "true", StringComparison.OrdinalIgnoreCase))
            {
                res = MessageBox.Show("Format Card? (Click NO to personalize without formatting, CANCEL to abort the process)!", "Warn", 
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            }
            else if (String.Equals(ConfigurationManager.AppSettings.Get("FormatConfirmation"), "false", StringComparison.OrdinalIgnoreCase))
            {
                //if no need for confirmation means always format
                res = DialogResult.Yes;
            }
            else
                throw new RuntimeErrorException("Wrong option on FormatConfirmation, should be true or false!");

            return res;
        }

        private void PersonalizeButton_Click(object sender, EventArgs e)
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
                    getInitialiCreditInHex(), getInitialBookCreditInHex(), getInitialCounterInHex(), doFormat);
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


        private void readCardButton_Click(object sender, EventArgs e)
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



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void ReadFpButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fpOpenDlg = new OpenFileDialog();

            fpOpenDlg.InitialDirectory = lastAccessedFolder;
            fpOpenDlg.Title = "Browse Image Files";

            fpOpenDlg.CheckFileExists = true;
            fpOpenDlg.CheckPathExists = true;

            fpOpenDlg.DefaultExt = "jpg";
            //imagOpenDlg.Filter = "image files (*.png)|*.jpg|All files (*.*)|*.*";
            //imagOpenDlg.Filter = "image files|*.png;*.jpg| All files (*.*)|*.*";
            fpOpenDlg.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            fpOpenDlg.FilterIndex = 0;
            fpOpenDlg.RestoreDirectory = true;

            fpOpenDlg.ReadOnlyChecked = true;
            fpOpenDlg.ShowReadOnly = true;

            if (fpOpenDlg.ShowDialog() == DialogResult.OK)
            {
                //save the last accessed folder
                UpdateSetting("LastAccessedFolder", Path.GetDirectoryName(fpOpenDlg.FileName));

                int max_image_size = Convert.ToInt16(ConfigurationManager.AppSettings.Get("MaxImageSize"), 10);

                FileInfo fi = new FileInfo(fpOpenDlg.FileName);
                if (fi.Length > max_image_size)
                {
                    MessageBox.Show("Image is oversized, maximum allowed is = " + ConfigurationManager.AppSettings.Get("MaxImageSize"),
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                //load image to picture holder
                HolderFPpictureBox.Load(fpOpenDlg.FileName);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(C) Bondhan Novandy\nVersion: 0.00006b",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void setEdcTextBox(String text)
        {
            if (EdcEWalletTextBox.IsDisposed == false)
            {
                if (this.EdcEWalletTextBox.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(setEdcTextBox);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    EdcEWalletTextBox.Text = text;
                }
            }

        }

        private void updateNumberLcd(object sender)
        {
            String text = ((Button)sender).Text;
            if (FormWasReset)
            {
                setNumberKitchenTextBox(text);
                FormWasReset = false;
            }
            else
                numberEWalletTextBox.AppendText(text);

        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }

        private void Btn3_Click(object sender, EventArgs e)
        {

            updateNumberLcd(sender);
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }

        private String getLog(String TransactionType, String AmountInDecNotFormatted, String appData = null)
        {
            //< !--LOG = TerminalID(4) ||  Transaction Type(1) || Date(4) || Time(3) || Amount(4)-- >

            String TerminalID = ConfigurationManager.AppSettings.Get("TerminalID");
            //String StoreID = ConfigurationManager.AppSettings.Get("StoreID");

            String Date = DateTime.Now.ToString("ddMMyyyy");
            String Time = DateTime.Now.ToString("hhmmss");

            String AmountInDec = Convert.ToInt32(AmountInDecNotFormatted, 10).ToString("D8");
            String transactionID = TransactionIDEPurse.Text;

            String applicationData = "";

            if (string.IsNullOrEmpty(appData) == false)
            {
                String _appData = MyUtil.asciiToHexString(appData);
                if (_appData.Length >= 24)
                    applicationData = _appData.Substring(0, 24);
                else
                { 
                    applicationData = (_appData + "202020202020202020202020").Substring(0,24);
                }
            }
            else
                applicationData = ApplicationDataEPurse.Text;

            //return (TerminalID + StoreID + TransactionType + Date + Time + AmountInDec);
            return (TerminalID + TransactionType + Date + Time + AmountInDec + transactionID + applicationData);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!isConnected)
                return;
            
            //randomize transaction ID
            RdnBtnTransID_Click(null, null);
            RdmBtnAppData_Click(null, null);

            if (mode == KITCHEN_BALANCE)
            {
                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                aStudent.LoadXml(false);

                ValueFile vf = aStudent.getCreditPurseAppValueFile();

                EdcEWalletTextBox.Clear();
                EdcEWalletTextBox.AppendText("Lower Limit: " + Convert.ToInt32(vf.lower_limit_hex, 16).ToString("D") + Environment.NewLine);
                EdcEWalletTextBox.AppendText("Upper Limit: " + Convert.ToInt32(vf.upper_limit_hex, 16).ToString("D") + Environment.NewLine);
                EdcEWalletTextBox.AppendText("Balance: " + Convert.ToInt32(vf.value_hex, 16).ToString("D"));
                numberEWalletTextBox.Clear();
            }
            else if (mode == KITCHEN_PURCHASE)
            {
                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                aStudent.LoadXml(false);

                EdcEWalletTextBox.Clear();

                if (aStudent.doDebitPurseValueFile00(numberEWalletTextBox.Text,
                    getLog(Student.PURCHASE_TRANSACTION, numberEWalletTextBox.Text)))
                    setEdcTextBox("Purchase Successful");
                else
                    setEdcTextBox("Purchase Failed");

                numberEWalletTextBox.Clear();
            }
            else if (mode == KITCHEN_TOPUP)
            {
                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                aStudent.LoadXml(false);

                EdcEWalletTextBox.Clear();

                if (aStudent.doCreditPurseValueFile00(numberEWalletTextBox.Text, getLog(Student.TOPUP_TRANSACTION, numberEWalletTextBox.Text)))
                    setEdcTextBox("Top Up Successful");
                else
                    setEdcTextBox("Top Up Failed");

                numberEWalletTextBox.Clear();
            }

            mode = NOTHING;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            String num = numberEWalletTextBox.Text;
            if (!String.IsNullOrWhiteSpace(num))
            {
                String lastChar = num.Substring(num.Length - 1);
                int n;
                bool isNumeric = int.TryParse(lastChar, out n);

                if (true)
                {
                    if (num.Length > 1 && n != 0)
                        setNumberKitchenTextBox(num.Substring(0, num.Length - 1));
                    else
                    {
                        setNumberKitchenTextBox("0");
                        FormWasReset = true;
                    }
                }
            }
        }

        private void setNumberKitchenTextBox(String text)
        {
            if (numberEWalletTextBox.IsDisposed == false)
            {
                if (this.numberEWalletTextBox.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(setNumberKitchenTextBox);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    numberEWalletTextBox.Text = text;
                }
            }

        }

        private void BtnBalance_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = KITCHEN_BALANCE;
            setEdcTextBox("Put your card on terminal and press OK" + Environment.NewLine);
        }

        private void BtnDebit_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = KITCHEN_PURCHASE;
            setEdcTextBox("Enter Purchase Amount:" + Environment.NewLine);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = NOTHING;
        }

        private void BtnCredit_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = KITCHEN_TOPUP;
            setEdcTextBox("Enter Top Up Amount:" + Environment.NewLine);
        }

        private void BtnReadLog_Click(object sender, EventArgs e)
        {
            doTransaction(sender, e, Student.DEBIT_PURSE_LOG_FILE_ID);
        }

        private void doTransaction(object sender, EventArgs e, String logFileID)
        {
            Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
            aStudent.LoadXml(false);

            textBoxLog.Clear();

            RecordFile rf = aStudent.readPurseLog(logFileID);

            if (Convert.ToInt32(rf.currentNoOfRecords, 16) > 0)
            {
                int rec_size_int = Convert.ToInt32(rf.record_size, 16);

                List<StudentLogClass> log = new List<StudentLogClass>();
                int totalRec = (rf.content.Length / 2) / rec_size_int;

                int start = 0;
                for (int i = 0; i < totalRec; i++)
                {
                    //textBoxLog.AppendText("Raw Log: " + (rf.content.Substring(start, rec_size_int * 2)) + Environment.NewLine);
                    textBoxLog.AppendText(Environment.NewLine + parseLogDataRaw(i, (rf.content.Substring(start, rec_size_int * 2))));
                    //log.Add(new StudentLogClass(rf.content.Substring(start, rec_size_int * 2)));
                    start += rec_size_int * 2;
                }
            }
            else
            {
                if (logFileID == Student.CREDIT_PURSE_LOG_FILE_ID)
                    textBoxLog.AppendText("No TOP UP transactions had been made");
                else
                    textBoxLog.AppendText("No purchase transactions had been made");
            }
        }

        private String parseLogDataRaw(int LogNum, String raw)
        {
            int index = 0;
            //< !--LOG = TerminalID(8) || StoreID(8) || Transaction Type(1) || Date(3) || Time(3) || Amount(4)-- >
            String TerminalID = raw.Substring(index, 8 * 2); index += 8 * 2;
            //String StoreID = raw.Substring(index, 8 * 2); index += 8 * 2;
            String TransType = raw.Substring(index, 1 * 2); index += 1 * 2;
            String Date = raw.Substring(index, 4 * 2); index += 4 * 2;
            String Time = raw.Substring(index, 3 * 2); index += 3 * 2;
            String Amount = raw.Substring(index, 4 * 2); index += 4 * 2;
            String TransactionID = raw.Substring(index, 4 * 2); index += 4 * 2;
            String CustomerData = raw.Substring(index);

            TerminalID = "Terminal ID: " + TerminalID + Environment.NewLine;
            //StoreID = "Store ID: " + StoreID + Environment.NewLine;
            TransType = "Transaction Type: " + TransType + Environment.NewLine;
            Date = "Date: " + Date + Environment.NewLine;
            Time = "Time: " + Time + Environment.NewLine;
            Amount = "Amount: " + Amount + Environment.NewLine;
            TransactionID = "Transaction ID: " + TransactionID + Environment.NewLine;
            CustomerData = "Customer Data: " + CustomerData + " (" + MyUtil.ConvertHextoAscii(CustomerData) + ")" + Environment.NewLine;

            String recordHeader = "Record " + LogNum.ToString("D2") + ":" + Environment.NewLine;

            //return recordHeader + TerminalID + StoreID + TransType + Date + Time + Amount + TransID;
            return recordHeader + TerminalID + TransType + Date + Time + Amount + TransactionID + CustomerData;
        }

        private void BtnReadLogTopUp_Click_1(object sender, EventArgs e)
        {
            doTransaction(sender, e, Student.CREDIT_PURSE_LOG_FILE_ID);
        }

        private void RndBtnTermID_Click(object sender, EventArgs e)
        {
            Int64 number = randomInstance.Next();
            TerminalIDEPurse.Text = number.ToString("X8");
        }

        private void RdnBtnTransID_Click(object sender, EventArgs e)
        {
            Int64 number = randomInstance.Next();
            TransactionIDEPurse.Text = number.ToString("X8");
        }

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

        private void parseAndDisplayLogBook1(String log)
        {
            BookLog bl = new BookLog(log);

            Book1NoTextBox.Text = bl.book.BookID;
            Book1TitleTextBox.Text = MyUtil.ConvertHextoAscii(bl.book.Title);
            Book1TerminalIDTextBox.Text = bl.TerminalID;
            if (bl.Type == Student.BORROW_TRANSACTION)
            {
                Book1StatusTextBox.Text = Student.BOOKED;
            }
            else
                Book1StatusTextBox.Text = Student.COMPLETED;

            Book1BorrowedDateTimePicker.Enabled = true;
            Book1ReturnedDateTimePicker.Enabled = true;

            if (bl.Type == Student.BORROW_TRANSACTION)
            {
                Book1BorrowedDateTimePicker.Value = bl.getDateBorrowed();
                Book1ReturnedDateTimePicker.Enabled = false;
            }
            else if (bl.Type == Student.RETURN_TRANSACTION)
            {
                Book1BorrowedDateTimePicker.Value = bl.getDateBorrowed();
                Book1ReturnedDateTimePicker.Value = bl.getDateReturned();
            }
        }

        private void parseAndDisplayLogBook2(String log)
        {
            BookLog b2 = new BookLog(log);

            Book2NoTextBox.Text = b2.book.BookID;
            Book2TitleTextBox.Text = MyUtil.ConvertHextoAscii(b2.book.Title);
            Book2TerminalIDTextBox.Text = b2.TerminalID;
            if (b2.Type == Student.BORROW_TRANSACTION)
            {
                Book2StatusTextBox.Text = Student.BOOKED;
            }
            else
                Book2StatusTextBox.Text = Student.COMPLETED;

            Book2BorrowedDateTimePicker.Enabled = true;
            Book2ReturnedDateTimePicker.Enabled = true;

            if (b2.Type == Student.BORROW_TRANSACTION)
            {
                Book2BorrowedDateTimePicker.Value = b2.getDateBorrowed();
                Book2ReturnedDateTimePicker.Enabled = false;
            }
            else if (b2.Type == Student.RETURN_TRANSACTION)
            {
                Book2BorrowedDateTimePicker.Value = b2.getDateBorrowed();
                Book2ReturnedDateTimePicker.Value = b2.getDateReturned();
            }
        }

        private void resetBook1Form()
        {
            Book1StatusTextBox.Clear();
            Book1NoTextBox.Clear();
            Book1TerminalIDTextBox.Clear();
            Book1TitleTextBox.Clear();
            Book1ReturnedDateTimePicker.ResetText();
            Book1BorrowedDateTimePicker.ResetText();
            Book1BorrowedDateTimePicker.Enabled = false;
            Book1ReturnedDateTimePicker.Enabled = false;

        }

        private void resetBook2Form()
        {
            Book2StatusTextBox.Clear();
            Book2NoTextBox.Clear();
            Book2TerminalIDTextBox.Clear();
            Book2TitleTextBox.Clear();
            Book2BorrowedDateTimePicker.Enabled = false;
            Book2ReturnedDateTimePicker.Enabled = false;
            Book2ReturnedDateTimePicker.ResetText();
            Book2BorrowedDateTimePicker.ResetText();
        }


        private void BtnScanCard_Click(object sender, EventArgs e)
        {
            Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
            aStudent.LoadXml(false);

            ValueFile vf = aStudent.getCreditLibraryAppValueFile();

            LibraryFreeSlotTextBox.Text = vf.value_hex;

            //Check if cards are empty
            RecordFile book1 = (RecordFile)aStudent.getFileSettings(Student.BOOK1_LOG_FILE_ID);
            RecordFile book2 = (RecordFile)aStudent.getFileSettings(Student.BOOK2_LOG_FILE_ID);

            resetBook1Form();
            resetBook2Form();

            if (book1.getCurrentNoOfRecordsInInt() > 0)
            {
                //read and write to form
                BasicFile bf = (BasicFile)book1;

                aStudent.readFile(ref bf);

                RecordFile book1Rf = (RecordFile)bf;


                parseAndDisplayLogBook1(book1Rf.content);
            }

            if (book2.getCurrentNoOfRecordsInInt() > 0)
            {
                //read and write to form
                //read and write to form
                BasicFile bf = (BasicFile)book2;

                aStudent.readFile(ref bf);

                RecordFile book2Rf = (RecordFile)bf;

                resetBook2Form();
                parseAndDisplayLogBook2(book2Rf.content);
            }

        }

        private void BookNoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BookTitleTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Title;
            BookAuthorTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Author;
            BookAvailabilityTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Availability == true ? "IN" : "OUT";
        }

        private string getPadString(String text, int totalLen)
        {
            String val = MyUtil.asciiToHexString(text);

            int remain = totalLen - val.Length;

            if (remain > 0)
            {
                for (int i = 0; i < remain; i++)
                {
                    val += "20";
                }
            }

            return val.Substring(0, totalLen);
        }

        private void BtnBorrow_Click(object sender, EventArgs e)
        {
            bool success = false;

            //scan the card first
            BtnScanCard_Click(null, null);

            if (string.Equals(BookAvailabilityTextBox.Text, "OUT", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Book is not available, cannot be borrowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            //Take the student library file configuration
            Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
            aStudent.LoadXml(false);

            ValueFile vf = aStudent.getCreditLibraryAppValueFile();

            LibraryFreeSlotTextBox.Text = vf.value_hex;

            //Check if cards are empty
            //If record in cyclic file book1 file is > 0
            RecordFile book1 = (RecordFile)aStudent.getFileSettings(Student.BOOK1_LOG_FILE_ID);
            RecordFile book1Rf = null; ;
            if (book1.getCurrentNoOfRecordsInInt() > 0)
            {
                // read 
                BasicFile bf1 = (BasicFile)book1;
                aStudent.readFile(ref bf1);
                book1Rf = (RecordFile)bf1;
            }

            //If record in cyclic file book2 file is > 0
            RecordFile book2 = (RecordFile)aStudent.getFileSettings(Student.BOOK2_LOG_FILE_ID);
            RecordFile book2Rf = null; ;
            if (book2.getCurrentNoOfRecordsInInt() > 0)
            {
                BasicFile bf2 = (BasicFile)book2;
                aStudent.readFile(ref bf2);
                book2Rf = (RecordFile)bf2;
            }

            //if value file is 0, means that no slot available to borrow the book
            if (vf.getValueInt() == 0)
            {
                MessageBox.Show("No Slot Available, please return book(s) to start borrowing!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            else
            {
                //start borrowing
                String DateBorrowed = MyUtil.asciiToHexString(BookDateTimePicker.Text);

                //write here
                String logString = TerminalIDLibComboBox.Text.Substring(0, 8) +
                       BookNoComboBox.Text.Substring(0, 8) +
                       getPadString(BookTitleTextBox.Text, 32) +
                       Student.BORROW_TRANSACTION +
                       DateBorrowed +
                       "00000000000000000000";

                if (book1.getCurrentNoOfRecordsInInt() == 0 || String.Equals(book1Rf.content.Substring(48, 2), Student.RETURN_TRANSACTION, StringComparison.OrdinalIgnoreCase))
                {
                    aStudent.doWriteLibLog(logString, Student.BOOK1_LOG_FILE_ID);

                    success = true;
                }
                else if (book2.getCurrentNoOfRecordsInInt() == 0 || String.Equals(book2Rf.content.Substring(48, 2), Student.RETURN_TRANSACTION, StringComparison.OrdinalIgnoreCase))
                {
                    aStudent.doWriteLibLog(logString, Student.BOOK2_LOG_FILE_ID);

                    success = true;
                }

                if (success)
                {
                    aStudent.processLibraryCreditFileAndCommit((BasicFile)vf, Student.OPERATION_DECREMENT);

                    setBookAvailabilityStatus(false);
                }

                if (!success)
                    MessageBox.Show("Fail borrowing the book", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    BtnScanCard_Click(null, null);

                    MessageBox.Show("Borrowing the book was successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                listAvailableBook();

                return;
            }

        }

        private void setBookAvailabilityStatus(bool isAvailable)
        {
            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var bookList = session.CreateSQLQuery("Select * from Book Where BookId = \"" +
                            BookNoComboBox.Text + "\"").AddEntity(typeof(Model.Book)).List<Model.Book>();

                        Model.Book _Book = bookList.ElementAt(0);
                        Model.Book aBook = session.Load<Model.Book>(_Book.Id);

                        aBook.Availability = isAvailable;

                        session.Update(aBook);

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            bool success = false;

            BtnScanCard_Click(null, null);

            //scan the card first
            BtnScanCard_Click(null, null);

            if (string.Equals(BookAvailabilityTextBox.Text, "IN", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Book is available, cannot be returned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
            aStudent.LoadXml(false);

            ValueFile vf = aStudent.getCreditLibraryAppValueFile();

            LibraryFreeSlotTextBox.Text = vf.value_hex;

            if (vf.getValueInt() == 2)
            {
                MessageBox.Show("No books are being borrowed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            else
            {
                //Check if the card has the right book
                if (((String.Equals(Book1NoTextBox.Text, BookNoComboBox.Text, StringComparison.OrdinalIgnoreCase) == false)
                    && (String.Equals(Book1StatusTextBox.Text, Student.COMPLETED, StringComparison.OrdinalIgnoreCase) == false))
                    && ((String.Equals(Book2NoTextBox.Text, BookNoComboBox.Text, StringComparison.OrdinalIgnoreCase) == false)
                    && (String.Equals(Book2StatusTextBox.Text, Student.COMPLETED, StringComparison.OrdinalIgnoreCase) == false)))
                {
                    MessageBox.Show("Book does not match, please select the right book!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                String DateReturned = MyUtil.asciiToHexString(BookDateTimePicker.Text);

                if (String.Equals(Book1NoTextBox.Text, BookNoComboBox.Text, StringComparison.OrdinalIgnoreCase))
                {
                    RecordFile book1 = (RecordFile)aStudent.getFileSettings(Student.BOOK1_LOG_FILE_ID);

                    // read and write to form
                    BasicFile bf = (BasicFile)book1;
                    aStudent.readFile(ref bf);
                    RecordFile book1Rf = (RecordFile)bf;

                    String logString = book1Rf.content.Substring(0, Student.LIB_LOG_LENGTH - 20) + DateReturned;

                    logString = logString.Remove(48, 2).Insert(48, Student.RETURN_TRANSACTION);

                    aStudent.doWriteLibLog(logString, Student.BOOK1_LOG_FILE_ID);

                    success = true;
                }
                else if (String.Equals(Book2NoTextBox.Text, BookNoComboBox.Text, StringComparison.OrdinalIgnoreCase))
                {
                    RecordFile book2 = (RecordFile)aStudent.getFileSettings(Student.BOOK2_LOG_FILE_ID);

                    // read and write to form
                    BasicFile bf = (BasicFile)book2;
                    aStudent.readFile(ref bf);
                    RecordFile book2Rf = (RecordFile)bf;

                    String logString = book2Rf.content.Substring(0, Student.LIB_LOG_LENGTH - 20) + DateReturned;
                    logString = logString.Remove(48, 2).Insert(48, Student.RETURN_TRANSACTION);

                    aStudent.doWriteLibLog(logString, Student.BOOK2_LOG_FILE_ID);

                    success = true;
                }
                //write log
            }

            if (success) { 
                //increase one value
                aStudent.processLibraryCreditFileAndCommit((BasicFile)vf, Student.OPERATION_INCREMENT);

                setBookAvailabilityStatus(true);
            }

            if (!success)
                MessageBox.Show("Fail returning the book", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                BtnScanCard_Click(null, null);

                MessageBox.Show("Returning the book was successful", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            listAvailableBook();

            return;
        }

        private void createDB()
        {
            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory dx = new DataFactory(dbFile, true);
            ISession sessionx = DataFactory.OpenSession;
            sessionx.Flush();
            sessionx.Close();

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //add math book
                        Model.Book mathBook = new Model.Book();
                        mathBook.BookID = "A0000001";
                        mathBook.Title = "Mathematics";
                        mathBook.Author = "Muhammad AlFarabi";
                        mathBook.Year = "2017";
                        mathBook.Availability = true;

                        Model.Book phyBook = new Model.Book();
                        phyBook.BookID = "A0000002";
                        phyBook.Title = "Physics";
                        phyBook.Author = "Muhammad Pisiki";
                        phyBook.Year = "2013";
                        phyBook.Availability = true;

                        Model.Book historyBook = new Model.Book();
                        historyBook.BookID = "A0000003";
                        historyBook.Title = "History";
                        historyBook.Author = "Ibnu Hisyam";
                        historyBook.Year = "2009";
                        historyBook.Availability = true;

                        Model.Book chemistryBook = new Model.Book();
                        chemistryBook.BookID = "A0000004";
                        chemistryBook.Title = "Chemistry";
                        chemistryBook.Author = "Muhammad Alfarabi";
                        chemistryBook.Year = "2001";
                        chemistryBook.Availability = true;


                        session.Save(mathBook);
                        session.Save(phyBook);
                        session.Save(historyBook);
                        session.Save(chemistryBook);



                        //add math book
                        Model.Menu chickBri = new Model.Menu();
                        chickBri.MenuId = "00000001";
                        chickBri.MenuName = "Chicken Briyani";
                        chickBri.Price = "10";

                        Model.Menu shawarmaSmall = new Model.Menu();
                        shawarmaSmall.MenuId = "00000002";
                        shawarmaSmall.MenuName = "Shawarma Small";
                        shawarmaSmall.Price = "4";

                        Model.Menu shawarmaBig = new Model.Menu();
                        shawarmaBig.MenuId = "00000003";
                        shawarmaBig.MenuName = "Shawarma Big";
                        shawarmaBig.Price = "8";

                        Model.Menu Shawaya = new Model.Menu();
                        Shawaya.MenuId = "00000004";
                        Shawaya.MenuName = "Shawaya With Rice";
                        Shawaya.Price = "15";

                        Model.Menu TurkishCoffee = new Model.Menu();
                        TurkishCoffee.MenuId = "00000005";
                        TurkishCoffee.MenuName = "Turkish Coffee";
                        TurkishCoffee.Price = "5";

                        session.Save(chickBri);
                        session.Save(shawarmaSmall);
                        session.Save(shawarmaBig);
                        session.Save(Shawaya);
                        session.Save(TurkishCoffee);

                        session.Flush();
                        transaction.Commit();

                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }

            LoadBookDb();
            listAvailableBook();

            LoadMenuDb();
            listAvailableMenu();
        }



        private bool checkFormMenuIsOk()
        {
            if (MenuIDTextBox.Text.Length < 8)
            {
                MessageBox.Show("ID Number must be 8 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupBookIDTextBox.Focus();
                return false;
            }

            if (MenuNameTextBox.Text.Length < 3)
            {
                MessageBox.Show("Menu Length minimum is 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupTitleTextBox.Focus();
                return false;
            }

            if (PriceTextBox.Text.Length < 1)
            {
                MessageBox.Show("Price must not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupAuthorTextBox.Focus();
                return false;
            }

            return true;
        }

        private bool checkFormLibraryIsOk()
        {
            if (SetupBookIDTextBox.Text.Length < 8)
            {
                MessageBox.Show("ID Number must be 8 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupBookIDTextBox.Focus();
                return false;
            }

            if (SetupTitleTextBox.Text.Length < 3)
            {
                MessageBox.Show("Tittle Length minimum is 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupTitleTextBox.Focus();
                return false;
            }

            if (SetupAuthorTextBox.Text.Length < 3)
            {
                MessageBox.Show("Author Length minimum is 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupAuthorTextBox.Focus();
                return false;
            }

            if (SetupYearTextBox.Text.Length != 4)
            {
                MessageBox.Show("Year length must be equal to 4", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupYearTextBox.Focus();
                return false;
            }

            return true;
        }


        private void SetupInsertButton_Click(object sender, EventArgs e)
        {
            if (checkFormLibraryIsOk() == false)
                return;

            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //add math book
                        Model.Book aBook = new Model.Book();
                        aBook.BookID = SetupBookIDTextBox.Text;
                        aBook.Title = SetupTitleTextBox.Text;
                        aBook.Author = SetupAuthorTextBox.Text;
                        aBook.Year = SetupYearTextBox.Text;
                        aBook.Availability = string.Equals(SetupStatusAvailabilityComboBox.Text, "IN", StringComparison.OrdinalIgnoreCase);

                        session.Save(aBook);

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }

                }
            }

            LoadBookDb();
            listAvailableBook();
        }

        private void SetupUpdateButton_Click(object sender, EventArgs e)
        {
            if (checkFormLibraryIsOk() == false)
                return;

            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var bookList = session.CreateSQLQuery("Select * from Book Where Id = \"" + RecordIDTextBox.Text + "\"").AddEntity(typeof(Model.Book)).List<Model.Book>();

                        Model.Book _Book = bookList.ElementAt(0);
                        Model.Book aBook = session.Load<Model.Book>(_Book.Id);

                        aBook.BookID = SetupBookIDTextBox.Text;
                        aBook.Title = SetupTitleTextBox.Text;
                        aBook.Author = SetupAuthorTextBox.Text;
                        aBook.Year = SetupYearTextBox.Text;
                        aBook.Availability = string.Equals(SetupStatusAvailabilityComboBox.Text, "IN", StringComparison.OrdinalIgnoreCase);

                        session.Update(aBook);

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }

            LoadBookDb();
            listAvailableBook();
        }

        private void LoadBookDb()
        {
            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = session.CreateSQLQuery("Select * from Book").AddEntity(typeof(Model.Book));

                        IList<Model.Book> bookList = null;
                        bookList = query.List<Model.Book>();

                        SetupDataGridView.DataSource = bookList;

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }


        private void LoadMenuDb()
        {
            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = session.CreateSQLQuery("Select * from Menu").AddEntity(typeof(Model.Menu));

                        IList<Model.Menu> menuList = null;
                        menuList = query.List<Model.Menu>();

                        MenuDataGridView.DataSource = menuList;

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadBookDb();
        }

        private void createDBToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Recreate Library DB? Old DB data will be erased?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (result == DialogResult.Yes)
                createDB();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (SetupBookIDTextBox.Text.Length < 8)
            {
                MessageBox.Show("ID Number must be 8 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupBookIDTextBox.Focus();

                return;
            }

            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var bookList = session.CreateSQLQuery("Select * from Book Where BookID = \"" + SetupBookIDTextBox.Text +
                            "\"").AddEntity(typeof(Model.Book)).List<Model.Book>();

                        if (bookList.Count > 0)
                        {
                            Model.Book aBook = bookList.ElementAt(0);

                            RecordIDTextBox.Text = aBook.Id.ToString();
                            SetupBookIDTextBox.Text = aBook.BookID;
                            SetupTitleTextBox.Text = aBook.Title;
                            SetupAuthorTextBox.Text = aBook.Author;
                            SetupYearTextBox.Text = aBook.Year;

                            if (aBook.Availability)
                                SetupStatusAvailabilityComboBox.SelectedIndex = 0;
                            else
                                SetupStatusAvailabilityComboBox.SelectedIndex = 1;

                            session.Flush();
                            transaction.Commit();
                            session.Close();
                        }
                        else
                        {
                            MessageBox.Show("Data not found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }

        private void SetupDeleteButton_Click(object sender, EventArgs e)
        {
            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = session.CreateSQLQuery("Delete from Book Where BookID = \"" + SetupBookIDTextBox.Text + "\"").AddEntity(typeof(Model.Book));

                        query.ExecuteUpdate();

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }

            LoadBookDb();
            listAvailableBook();
        }

        private void refreshBookListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listAvailableBook();
            listAvailableMenu();
        }

        private void showAllMenuBtn_Click(object sender, EventArgs e)
        {
            listAvailableMenu();
        }

        private void searchMenuBtn_Click(object sender, EventArgs e)
        {
            if (MenuIDTextBox.Text.Length < 8)
            {
                MessageBox.Show("ID Number must be 8 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                MenuIDTextBox.Focus();

                return;
            }

            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var menuList = session.CreateSQLQuery("Select * from Menu Where MenuID = \"" + MenuIDTextBox.Text +
                            "\"").AddEntity(typeof(Model.Menu)).List<Model.Menu>();

                        if (menuList.Count > 0)
                        {
                            Model.Menu aMenu = menuList.ElementAt(0);

                            MenuNameTextBox.Text = aMenu.MenuName;
                            PriceTextBox.Text = aMenu.Price;

                            session.Flush();
                            transaction.Commit();
                            session.Close();
                        }
                        else
                        {
                            MessageBox.Show("Data not found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }
        }


        private void InsertMenuBtn_Click(object sender, EventArgs e)
        {
            if (checkFormMenuIsOk() == false)
                return;

            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //add math book
                        Model.Menu aMenu = new Model.Menu();
                        aMenu.MenuId= MenuIDTextBox.Text;
                        aMenu.MenuName = MenuNameTextBox.Text;
                        aMenu.Price = PriceTextBox.Text;

                        session.Save(aMenu);

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }

                }
            }

            LoadMenuDb();
            listAvailableMenu();
        }

        private void UpdateMenuBtn_Click(object sender, EventArgs e)
        {
            if (checkFormMenuIsOk() == false)
                return;

            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var menuList = session.CreateSQLQuery("Select * from Menu Where Id = \"" + MenuIDTextBox.Text + "\"").AddEntity(typeof(Model.Menu)).List<Model.Menu>();

                        Model.Menu _Menu = menuList.ElementAt(0);
                        Model.Menu aMenu = session.Load<Model.Menu>(_Menu.Id);

                        aMenu.MenuId = MenuIDTextBox.Text;
                        aMenu.MenuName = MenuNameTextBox.Text;
                        aMenu.Price = PriceTextBox.Text;

                        session.Update(aMenu);

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }

            LoadMenuDb();
            listAvailableMenu();
        }

        private void DeleteMenuBtn_Click(object sender, EventArgs e)
        {
            string dbFile = ConfigurationManager.AppSettings.Get("db_file");

            DataFactory df = new DataFactory(dbFile, false);
            ISession session = DataFactory.OpenSession;

            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var query = session.CreateSQLQuery("Delete from Menu Where MenuID = \"" + MenuIDTextBox.Text + "\"").AddEntity(typeof(Model.Menu));

                        query.ExecuteUpdate();

                        session.Flush();
                        transaction.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
            }

            LoadMenuDb();
            listAvailableMenu();
        }

        private void DisplayMenuNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayPriceTextBox.Text = MenuList.ElementAt(DisplayMenuNameListBox.SelectedIndex).Price;
            DisplayStatusTextBox.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
            aStudent.LoadXml(false);

            DisplayStatusTextBox.Clear();

            if (aStudent.doDebitPurseValueFile00(DisplayPriceTextBox.Text,
                getLog(Student.PURCHASE_TRANSACTION, DisplayPriceTextBox.Text, DisplayMenuNameListBox.Text)))
                DisplayStatusTextBox.Text = "Purchase Successful";
            else
                DisplayStatusTextBox.Text = "Purchase Failed";

            numberEWalletTextBox.Clear();
        }

        private void DisplayMenuNameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayPriceTextBox.Text = MenuList.ElementAt(DisplayMenuNameListBox.SelectedIndex).Price;
            DisplayStatusTextBox.Clear();
        }

        private void incrementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (incrementToolStripMenuItem.Checked)
            {
                decrementToolStripMenuItem.Checked = false;
                readOnlyToolStripMenuItem.Checked = false;
            }

            selectDefaultMenu();
        }

        private void decrementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decrementToolStripMenuItem.Checked)
            {
                incrementToolStripMenuItem.Checked = false;
                readOnlyToolStripMenuItem.Checked = false;
            }

            selectDefaultMenu();
        }

        private void readOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (readOnlyToolStripMenuItem.Checked)
            {
                incrementToolStripMenuItem.Checked = false;
                decrementToolStripMenuItem.Checked = false;
            }

            selectDefaultMenu();
        }

        private void selectDefaultMenu()
        {
            if (readOnlyToolStripMenuItem.Checked == false && 
                incrementToolStripMenuItem.Checked == false && 
                decrementToolStripMenuItem.Checked == false)
            {
                readOnlyToolStripMenuItem.Checked = true;
            }
        }

        private void FirstNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void mainMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void StudentDataTab_Click(object sender, EventArgs e)
        {

        }
    }
}
