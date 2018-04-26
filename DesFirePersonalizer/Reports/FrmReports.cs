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
using PCSC;
using DesFirePersonalizer.Apps_Cood;

namespace DesFirePersonalizer.Reports
{
    public partial class FrmReports : Form
    {

        public FrmReports()
        {
            InitializeComponent();
        }
        DesFireWrapper dsf;
        SCardContext scc = null;
        SCardReader scr = null;
        SCardState scs;
        SCardMonitor scm = null;
        SCardProtocol scp;
        //////////////////////////////////////////////////////////////////////////////
        delegate void SetTextCallbackWithStatus(string text, Boolean connStatus);
        delegate void SetTextCallback(string text);
        private static readonly IContextFactory _contextFactory = ContextFactory.Instance;
        //////////////////////////////////////////////////////////////////////////////
        int mode = 0;
        string atrStr = null;
        int index = 0;
        string[] CardReaderList = null;
        String[] readername = null;
        byte[] atr = null;
        //////////////////////////////////////////////////////////////////////////////
        Random randomInstance;
        Boolean isConnected = false;
        //////////////////////////////////////////////////////////////////////////////
        private DataTable dt;
        string GridQuery = "SELECT Record_ID, Transaction_Type, trans_datetime, trans_amount, Transaction_ID, StudentID, ComputerName, ComputerUserName,"
                         + "UserNID, RestMenuName FROM  transactionLog ";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region fill data 

        private DataTable FillGridData(string pWhere1)
        {
            return dt = DBFun.FetchData(GridQuery + " " + pWhere1);
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void parseAndDisplayLogBook1(String log)
        {
            BookLog bl = new BookLog(log);

            TxtStdInc.Text = bl.book.STDID;

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void doTransaction(object sender, EventArgs e, String logFileID)
        {
            Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
            aStudent.LoadXml(false);
            scancard();
           // textBoxLog.Clear();

            RecordFile rf = aStudent.readPurseLog(logFileID);

            dt = DBFun.FetchData(GridQuery + " WHERE StudentID = '" + TxtStdInc.Text + "'");
            DataRow dr = (DataRow)dt.Rows[0];
            ReportGridView.DataSource = FillGridData(" WHERE StudentID = '" + TxtStdInc.Text + "'");

            //if (Convert.ToInt32(rf.currentNoOfRecords, 16) > 0)
            //{
            //    int rec_size_int = Convert.ToInt32(rf.record_size, 16);

            //    List<StudentLogClass> log = new List<StudentLogClass>();
            //    int totalRec = (rf.content.Length / 2) / rec_size_int;

            //    int start = 0;
            //    for (int i = 0; i < totalRec; i++)
            //    {
                    
            //        textBoxLog.AppendText(Environment.NewLine + parseLogDataRaw(i, (rf.content.Substring(start, rec_size_int * 2))));
               
            //        start += rec_size_int * 2;
            //    }
            //}
            //else
            //{
            //    if (logFileID == Student.CREDIT_PURSE_LOG_FILE_ID)
            //        textBoxLog.AppendText("No TOP UP transactions had been made");
            //    else
            //        textBoxLog.AppendText("No purchase transactions had been made");
            //}
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //private String parseLogDataRaw(int LogNum, String raw)
        //{

        //    dt = DBFun.FetchData(GridQuery + " WHERE StudentID = '" + TxtStdInc.Text + "'");
        //    DataRow dr = (DataRow)dt.Rows[0];
        //    ReportGridView.DataSource = FillGridData(" WHERE StudentID = '" + TxtStdInc.Text + "'");
        //    //String studentID = dr["StudentID"].ToString(); 
        //    //String MealData = dr["RestMenuName"].ToString();
        //    //String Amount = dr["trans_amount"].ToString();
        //    //String TransactionID = dr["Transaction_ID"].ToString();
        //    //String TransType = dr["Transaction_Type"].ToString();
        //    //String Date = dr["trans_datetime"].ToString();
        //    //String TerminalID = dr["ComputerName"].ToString();
        //    //String userID = dr["UserNID"].ToString();
        //    //String computername = dr["ComputerUserName"].ToString();

        //    //studentID     = "Student ID: " + studentID+ Environment.NewLine ;
        //    //MealData      = "Customer Data: " + MealData+ Environment.NewLine ;
        //    //Amount        = "Amount: " + Amount+ Environment.NewLine ;
        //    //TransType     = "Transaction Type: " + TransType+ Environment.NewLine ;
        //    //Date          = "Date: " + Date+ Environment.NewLine ;
        //    //TerminalID    = "Terminal ID: " + TerminalID+ Environment.NewLine ;
        //    //TransactionID = "Transaction ID: " + TransactionID+ Environment.NewLine ;
        //    //userID        = "Cashier ID: " + userID+ Environment.NewLine ;
        //    //computername  = "Terminal Computer: " + computername+ Environment.NewLine + "|_________________________________________________|" + Environment.NewLine;

        //    //String recordHeader = "Record_ID " + LogNum.ToString("D2") + ":" + Environment.NewLine + "------------------------" + Environment.NewLine;
            

        //    //return recordHeader + studentID + MealData + Amount + TransactionID + TransType + Date + TerminalID + TransactionID  + userID + computername ;
        //}
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnReadLogPurchase_Click(object sender, EventArgs e)
        {
            try
            {
            doTransaction(sender, e, Student.DEBIT_PURSE_LOG_FILE_ID);
            }
            catch(Exception ex)
            {
                //MessageBox.Show ()
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnReadLogTopUp_Click(object sender, EventArgs e)
        {
            doTransaction(sender, e, Student.CREDIT_PURSE_LOG_FILE_ID);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FrmReports_Load(object sender, EventArgs e)
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

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Reader 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void loadFromSettings()
        {
            cardReaderToolStrip.Text = ConfigurationManager.AppSettings.Get("CardReader");
        }
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
