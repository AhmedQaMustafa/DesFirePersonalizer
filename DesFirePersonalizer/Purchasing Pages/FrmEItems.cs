using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using DesFireWrapperLib;
using NHibernate;
using System.IO;
using DesFirePersonalizer.Apps_Cood;
using PCSC;
using System.Data.SqlClient;

namespace DesFirePersonalizer.Purchasing_Pages
{
    public partial class FrmEItems : Form
    {
        delegate void SetTextCallbackWithStatus(string text, Boolean connStatus);
        delegate void SetTextCallback(string text);
        private static readonly IContextFactory _contextFactory = ContextFactory.Instance;
        //////////////////////////////////////////////////////////////////////////////
        SCardContext scc = null;
        SCardReader scr = null;
        SCardState scs;
        SCardMonitor scm = null;
        SCardProtocol scp;
        DesFireWrapper dsf;
        //////////////////////////////////////////////////////////////////////////////
        int mode = 0;
        string atrStr = null;
        int index = 0;
        string[] CardReaderList = null;
        String[] readername = null;
        byte[] atr = null;

        Random randomInstance;
        Boolean isConnected = false;
        //////////////////////////////////////////////////////////////////////////////
        DataTable dt;
        DataRow dr;
        string GridQuery = "Select RestMenuID,RestMenuName,RestPrice From ResturantMenu ";
        DatabaseProvider Provider = new DatabaseProvider();
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public FrmEItems()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void listAvailableMenu()
        {
            DataTable datatable = new DataTable();
            SqlCommand cm = new SqlCommand();
            DataSet ds = new DataSet();
            string Query = " Select RestMenuID,RestMenuName,RestPrice From ResturantMenu";
            cm = new SqlCommand(Query, DBFun.con);
            SqlDataAdapter ADP = new SqlDataAdapter(cm);
            //ds = new DataSet();
            ADP.Fill(datatable);
            foreach (DataRow dr in datatable.Rows)
            {
                DisplayMenuNameListBox.Items.Add(dr["RestMenuName"].ToString());
            }
            //datatable = DBFun.FetchData("Select RestMenuID,RestMenuName,RestPrice From ResturantMenu");

            //dt = FillGridData();
            //DisplayMenuNameListBox.DataSource = MenuList;
            //DisplayMenuNameListBox.DisplayMember = "RestMenuName";
            //DisplayMenuNameListBox.ValueMember = "RestMenuName";

            //DisplayPriceTextBox.Text = MenuList.ElementAt(DisplayMenuNameListBox.SelectedIndex).Price;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridData()
        {

            return dt = DBFun.FetchData(GridQuery);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DisplayMenuNameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable datatable1 = new DataTable();
            SqlCommand cm1 = new SqlCommand();
            DataSet ds1 = new DataSet();
            string Query1 = " Select RestMenuID,RestMenuName,RestPrice From ResturantMenu where RestMenuName = '" + DisplayMenuNameListBox.SelectedItem.ToString() + "'";
            cm1 = new SqlCommand(Query1, DBFun.con);
            SqlDataAdapter ADP1 = new SqlDataAdapter(cm1);
            //ds = new DataSet();
            ADP1.Fill(datatable1);
            foreach (DataRow dr1 in datatable1.Rows)
            {
                DisplayPriceTextBox.Text = dr1["RestPrice"].ToString();
            }
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DisplayMenuNameListBox.SelectedIndex != -1)
                {
                    Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                    aStudent.LoadXml(false);
                    scancard();
                    DisplayStatusTextBox.Clear();

                    if (aStudent.doDebitPurseValueFile00(DisplayPriceTextBox.Text, getLog(Student.PURCHASE_TRANSACTION, DisplayPriceTextBox.Text, DisplayMenuNameListBox.Text)))
                    {
                        Provider.Purchasing(TxtStdInc.Text, txtuserID.Text,DisplayMenuNameListBox.SelectedItem.ToString(), DisplayPriceTextBox.Text, txtcomputername.Text, dateTimePicker1.Text,  txtcompusername.Text);
                        Provider.insertlog(TxtStdInc.Text, txtuserID.Text, DisplayMenuNameListBox.SelectedItem.ToString(), DisplayPriceTextBox.Text, txtcomputername.Text, dateTimePicker1.Text, txtcompusername.Text,TransactionIDEPurse.Text);

                        DisplayStatusTextBox.Text = "Purchase Successful";
                    }

                    else
                        DisplayStatusTextBox.Text = "Purchase Failed - No balance Enough";
                }

                else
                {
                    MessageBox.Show("Please Select Meal", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Process Not Complete Please Contact administrator");
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FrmEItems_Load(object sender, EventArgs e)
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
            listAvailableMenu();
            //populateDataForm();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            txtuserID.Text = DatabaseProvider.gLoginUserID;
            string compname = System.Windows.Forms.SystemInformation.ComputerName;
            string username = System.Windows.Forms.SystemInformation.UserName.ToString();
            txtcomputername.Text = compname;
            txtcompusername.Text = username;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void loadFromSettings()
        {
            cardReaderToolStrip.Text = ConfigurationManager.AppSettings.Get("CardReader");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        private void button2_Click(object sender, EventArgs e)
        {
  


        }
    }
}
