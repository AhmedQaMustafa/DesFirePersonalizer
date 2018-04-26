using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesFireWrapperLib;
using System.Configuration;
using DesFirePersonalizer.Apps_Cood;
using PCSC;

namespace DesFirePersonalizer.Purchasing_Pages
{
    public partial class FrmPurchaseTrAc : Form
    {
        public FrmPurchaseTrAc()
        {
            InitializeComponent();
        }
        bool FormWasReset = true;
        delegate void SetTextCallbackWithStatus(string text, Boolean connStatus);
        delegate void SetTextCallback(string text);

        SCardContext scc = null;
        SCardReader scr = null;
        String[] readername = null;
        SCardState scs;
        SCardMonitor scm = null;
        SCardProtocol scp;
        byte[] atr = null;
        string atrStr = null;
        int index = 0;
        string[] CardReaderList = null;
        Random randomInstance;



        Boolean isConnected = false;
        private static readonly IContextFactory _contextFactory = ContextFactory.Instance;

        int mode = 0;
        private static int NOTHING = 0;
        private static int KITCHEN_PURCHASE = 1;
        private static int KITCHEN_TOPUP = 2;
        private static int KITCHEN_BALANCE = 3;

        DesFireWrapper dsf;

        #region Numbers
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn1_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn2_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn3_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn4_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn5_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn6_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn7_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        private void Btn8_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn9_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn0_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);

        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region calculator
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnCredit_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = KITCHEN_TOPUP;
            setEdcTextBox("Enter Top Up Amount:" + Environment.NewLine);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnBalance_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = KITCHEN_BALANCE;
            setEdcTextBox("Put your card on terminal and press OK" + Environment.NewLine);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!isConnected)
                return;
            //randomize transaction ID
            RdnBtnTransID_Click(null, null);
            RdmBtnAppData_Click(null, null);
            scancard();
          
            if (mode == KITCHEN_BALANCE)
            {
                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                aStudent.LoadXml(false);

                ValueFile vf = aStudent.getCreditPurseAppValueFile();

                EdcEWalletTextBox.Clear();
                EdcEWalletTextBox.AppendText("Lower Limit: " + Convert.ToInt32(vf.lower_limit_hex, 16).ToString("D") + Environment.NewLine);
                txtlowerlimit.Text = ("Lower Limit: " + Convert.ToInt32(vf.lower_limit_hex, 16).ToString("D"));
                EdcEWalletTextBox.AppendText("Upper Limit: " + Convert.ToInt32(vf.upper_limit_hex, 16).ToString("D") + Environment.NewLine);
                txtupperlimit.Text = ("Upper Limit: " + Convert.ToInt32(vf.upper_limit_hex, 16).ToString("D"));
                EdcEWalletTextBox.AppendText("Balance: " + Convert.ToInt32(vf.value_hex, 16).ToString("D"));
                txtbalance.Text= ("Balance: " + Convert.ToInt32(vf.value_hex, 16).ToString("D"));
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

                if (aStudent.doCreditPurseValueFile00(numberEWalletTextBox.Text))//, getLog(Student.TOPUP_TRANSACTION, numberEWalletTextBox.Text))
                    setEdcTextBox("Top Up Successful");
                else
                    setEdcTextBox("Top Up Failed");

                numberEWalletTextBox.Clear();
            }

            mode = NOTHING;

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnReset_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = NOTHING;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Declaritions
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private String getLog(String TransactionType, String AmountInDecNotFormatted, String appData = null)
        {
            //< !--LOG = TerminalID(4) ||  Transaction Type(1) || Date(4) || Time(3) || Amount(4)-- >

            String TerminalID = ConfigurationManager.AppSettings.Get("TerminalID");
            //String StoreID = ConfigurationManager.AppSettings.Get("StoreID");

            String Date = DateTime.Now.ToString("ddMMyyyy");
            String Time = DateTime.Now.ToString("hhmmss");

            String AmountInDec = Convert.ToInt32(AmountInDecNotFormatted, 10).ToString("D8");
            String transactionID = TransactionIDEPurse.Text;
            DatabaseProvider.TransactionCommand = TransactionIDEPurse.Text;

            String applicationData = "";

            if (string.IsNullOrEmpty(appData) == false)
            {
                String _appData = MyUtil.asciiToHexString(appData);
                if (_appData.Length >= 24)
                    applicationData = _appData.Substring(0, 24);
                else
                {
                    applicationData = (_appData + "202020202020202020202020").Substring(0, 24);
                }
            }
            else
                applicationData = ApplicationDataEPurse.Text;
            DatabaseProvider.ApplicationDataCommand = ApplicationDataEPurse.Text;

            //return (TerminalID + StoreID + TransactionType + Date + Time + AmountInDec);
            return (TerminalID + TransactionType + Date + Time + AmountInDec + transactionID + applicationData);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnDebit_Click_1(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = KITCHEN_PURCHASE;
            setEdcTextBox("Enter Purchase Amount:" + Environment.NewLine);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FrmPurchaseTrAc_Load(object sender, EventArgs e)
        {
            txtuserID.Text = DatabaseProvider.gLoginUserID;
            string compname = System.Windows.Forms.SystemInformation.ComputerName;
            string username = System.Windows.Forms.SystemInformation.UserName.ToString();
            txtcomputername.Text = compname;
            txtcompusername.Text = username;
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
            redoRegisterEvent();
            scc.Cancel();
            scc.Dispose();
            scc = null;
            loadFromSettings();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void loadFromSettings()
        {
            cardReaderToolStrip.Text = ConfigurationManager.AppSettings.Get("CardReader");
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void parseAndDisplayLogBook1(String log)
        {
            BookLog bl = new BookLog(log);

            TxtStdInc.Text = bl.book.STDID;

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void scancard()
        {
            try
            {

                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                aStudent.LoadXml(false);

                ValueFile vf = aStudent.getCreditLibraryAppValueFile();

                //Check if cards are empty
                RecordFile book1 = (RecordFile)aStudent.getFileSettings(Student.BOOK1_LOG_FILE_ID);


                if (book1.getCurrentNoOfRecordsInInt() > 0)
                {
                    //read and write to form
                    BasicFile bf = (BasicFile)book1;

                    aStudent.readFile(ref bf);

                    RecordFile book1Rf = (RecordFile)bf;

                    parseAndDisplayLogBook1(book1Rf.content);
                    // success = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Please pe sure the reader connected or card placed on the reader");

            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Reader 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void RdnBtnTransID_Click(object sender, EventArgs e)
        {
            Int64 number = randomInstance.Next();
            TransactionIDEPurse.Text = number.ToString("X8");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void registerEvent()
        {
            scm.StatusChanged += new StatusChangeEvent(StateChangeMethod);
            scm.CardInserted += (sender, args) => DisplayEventAndConnect("CardInserted", args);
            scm.CardRemoved += (sender, args) => DisplayEventAndDisconnect("CardRemoved", args);
            scm.Initialized += (sender, args) => DisplayEvent("Initialized", args);
            scm.MonitorException += MonitorExceptionMethod;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MonitorExceptionMethod(object sender, PCSCException ex)
        {
            //MessageBox.Show(SCardHelper.StringifyError(ex.SCardError) + "\nPlease make sure the correct reader is selected in Options > Settings",
            //     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //MessageBox.Show("Monitor Exception Happened! Error Message = " + SCardHelper.StringifyError(ex.SCardError),
            //    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void deRegisterEvent()
        {
            scm.StatusChanged -= StateChangeMethod;
            scm.CardInserted -= (sender, args) => DisplayEventAndConnect("CardInserted", args);
            scm.CardRemoved -= (sender, args) => DisplayEventAndDisconnect("CardRemoved", args);
            scm.Initialized -= (sender, args) => DisplayEvent("Initialized", args);
            scm.MonitorException -= MonitorExceptionMethod;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DisplayEventAndConnect(string eventName, CardStatusEventArgs unknown)
        {
            Action setStatus = () =>
            {
                SmartCardStatusLabel.Text = eventName + "," + unknown.State;
                tryToConnect();

                //executeCounter(isConnected);

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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void executeCounter(bool isConnected)
        {
            //if (isConnected)
            //{
            //    if (MainTabControl.SelectedTab == MainTabControl.TabPages["counterTabPage"])
            //    {
            //        Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
            //        aStudent.LoadXml(false);

            //        if (incrementToolStripMenuItem.Checked)
            //        {
            //            aStudent.doIncreaseCounter();
            //        }
            //        else if (decrementToolStripMenuItem.Checked)
            //        {
            //            aStudent.doDecreaseCounter();
            //        }

            //        ValueFile vf = aStudent.getCounterValueFromFile();

            //        CounterValueTextBox.Clear();
            //        CounterValueTextBox.Text = Convert.ToInt32(vf.value_hex, 16).ToString("D");


            //    }
            //}
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

    }
}
