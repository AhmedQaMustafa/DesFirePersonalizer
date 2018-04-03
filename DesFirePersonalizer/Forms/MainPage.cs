using DesFirePersonalizer.Apps_Cood;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesFirePersonalizer;
using DesFirePersonalizer.Wizered_Pages;
using DesFirePersonalizer.Purchasing_Pages;
using DesFirePersonalizer.Library_Pages;
using DesFirePersonalizer.Reports;
using DesFirePersonalizer.Configurations_Pages;
using System.Configuration;
using PCSC;
using DesFireWrapperLib;
using System.Data.SqlClient;
using DesFirePersonalizer.Apps_Cod;

namespace DesFirePersonalizer
{
    public partial class MainPage : Form
    {
        PermitionClass Permition = new PermitionClass();
        DatabaseProvider DBprov = new DatabaseProvider();
        //lblLoginName.Text = DatabaseProvider.gLoginFullName;
        public MainPage()
        {
            InitializeComponent();
        }
        SCardContext scc = null;
        SCardReader scr = null;
        SCardMonitor scm = null;
        SCardState scs;
        SCardProtocol scp;
        byte[] atr = null;
        string atrStr = null;

        Boolean isConnected = false;
        private static readonly IContextFactory _contextFactory = ContextFactory.Instance;

        String[] readername = null;
        DesFireWrapper dsf;
        delegate void SetTextCallbackWithStatus(string text, Boolean connStatus);
        delegate void SetTextCallback(string text);

        DataTable dt;
        public static SqlConnection con = new SqlConnection();
        DataSet ds = new DataSet{};
        DataSet ds1 = new DataSet{};
        SqlCommand cm = new SqlCommand();
        SqlCommand cm1 = new SqlCommand();
        string menu_str;
        private object incrementToolStripMenuItem;
        private object decrementToolStripMenuItem;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void loadFromSettings()
        {
            cardReaderToolStrip.Text = ConfigurationManager.AppSettings.Get("CardReader");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MainPage_Load(object sender, EventArgs e)
        {
          lblLoginName.Text = DatabaseProvider.gLoginFullName;
            loadFromSettings();
            redoRegisterEvent();
            DatabaseProvider.gLoginUserID = "";
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Reader 
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void registerEvent()
        {
            scm.StatusChanged += new StatusChangeEvent(StateChangeMethod);
            scm.CardInserted += (sender, args) => DisplayEventAndConnect("CardInserted", args);
            scm.CardRemoved += (sender, args) => DisplayEventAndDisconnect("CardRemoved", args);
            scm.Initialized += (sender, args) => DisplayEvent("Initialized", args);
            scm.MonitorException += MonitorExceptionMethod;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MonitorExceptionMethod(object sender, PCSCException ex)
        {
            //MessageBox.Show(SCardHelper.StringifyError(ex.SCardError) + "\nPlease make sure the correct reader is selected in Options > Settings",
            //     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //MessageBox.Show("Monitor Exception Happened! Error Message = " + SCardHelper.StringifyError(ex.SCardError),
            //    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void deRegisterEvent()
        {
            scm.StatusChanged -= StateChangeMethod;
            scm.CardInserted -= (sender, args) => DisplayEventAndConnect("CardInserted", args);
            scm.CardRemoved -= (sender, args) => DisplayEventAndDisconnect("CardRemoved", args);
            scm.Initialized -= (sender, args) => DisplayEvent("Initialized", args);
            scm.MonitorException -= MonitorExceptionMethod;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            //if (isConnected)
            //{
            //    if (menuStrip1.S == MainTabControl.TabPages["counterTabPage"])
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

             }

        #endregion
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                #region louad Forms
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            MainForm Application = new MainForm();
            Application.MdiParent = this;
            Application.Left = 0;
            Application.Top = 0;
            Application.Size = this.ClientRectangle.Size;
            Application.Dock = DockStyle.Fill;
            Application.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            Users userpage = new Users();
            userpage.MdiParent = this;
            userpage.Left = 0;
            userpage.Top = 0;
            userpage.Size = this.ClientRectangle.Size;
            userpage.Dock = DockStyle.Fill;
            userpage.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
       ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void addUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            Users userpage = new Users();
            userpage.MdiParent = this;
            userpage.Left = 0;
            userpage.Top = 0;
            userpage.Size = this.ClientRectangle.Size;
            userpage.Dock = DockStyle.Fill;
            userpage.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void addCategorysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            UsersCategory1 Catpage = new UsersCategory1();
            Catpage.MdiParent = this;
            Catpage.Left = 0;
            Catpage.Top = 0;
            Catpage.Size = this.ClientRectangle.Size;
            Catpage.Dock = DockStyle.Fill;
            Catpage.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void permitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            Users_Permitions userper = new Users_Permitions();
            userper.MdiParent = this;
            userper.Left = 0;
            userper.Top = 0;
            userper.Size = this.ClientRectangle.Size;
            userper.Dock = DockStyle.Fill;
            userper.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MainPage_Shown(object sender, EventArgs e)
        {
            LoginFrm login = new LoginFrm();
            login.ShowDialog();
            lblLoginName.Text = DatabaseProvider.gLoginFullName;
            getdynamicmenu();


        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            StdInformations stdfrm = new StdInformations();
            stdfrm.MdiParent = this;
            stdfrm.Left = 0;
            stdfrm.Top = 0;
            stdfrm.Size = this.ClientRectangle.Size;
            stdfrm.Dock = DockStyle.Fill;
            stdfrm.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cardSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            CardTemplatesForm CRDTem = new CardTemplatesForm();
            CRDTem.MdiParent = this;
            CRDTem.Left = 0;
            CRDTem.Top = 0;
            CRDTem.Size = this.ClientRectangle.Size;
            CRDTem.Dock = DockStyle.Fill;
            CRDTem.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void companySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            IssueCards CRDTem = new IssueCards();
            CRDTem.MdiParent = this;
            CRDTem.Left = 0;
            CRDTem.Top = 0;
            CRDTem.Size = this.ClientRectangle.Size;
            CRDTem.Dock = DockStyle.Fill;
            CRDTem.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void lblLoginName_Click(object sender, EventArgs e)
        {
           
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
             lblLoginName.Text = "";
            //DatabaseProvider.gLoginUserID = "";
            LoginFrm logout = new LoginFrm();
            CloseAllChildForms();
            logout.ShowDialog();
            getdynamicmenu();
           // 
        }
        delegate void CloseMethod(Form form);
        static public void CloseForm(Form form)
        {
            if (!form.IsDisposed)
            {
                if (form.InvokeRequired)
                {
                    CloseMethod method = new CloseMethod(CloseForm);
                    form.Invoke(method, new object[] { form });
                }
                else { form.Close(); }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Form[] OpenForms
        {
            get
            {
                Form[] forms = null;
                int count = Application.OpenForms.Count;
                forms = new Form[count];
                if (count > 0)
                {
                    int index = 0;
                    foreach (Form form in Application.OpenForms) { forms[index++] = form; }
                }
                return forms;
            }
        }

        public object MainTabControl { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static public void CloseAllChildForms()
        {
            Form[] forms = OpenForms; // get array because collection changes as we close forms
            foreach (Form form in forms) { if (form.Name != "MainPage") { CloseForm(form); } } // close every open form
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmWizered Application = new FrmWizered();
            //Application.MdiParent = this;
            //Application.Left = 0;
            //Application.Top = 0;
            //Application.Size = this.ClientRectangle.Size;
            //Application.Dock = DockStyle.Fill;
            //Application.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ePurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            FrmPurchaseTrAc Application = new FrmPurchaseTrAc();
            Application.MdiParent = this;
            Application.Left = 0;
            Application.Top = 0;
            Application.Size = this.ClientRectangle.Size;
            Application.Dock = DockStyle.Fill;
            Application.Show();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void eItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            FrmEItems Application = new FrmEItems();
            Application.MdiParent = this;
            Application.Left = 0;
            Application.Top = 0;
            Application.Size = this.ClientRectangle.Size;
            Application.Dock = DockStyle.Fill;
            Application.Show();
        }

        private void eBookInOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            FrmLibrary Application = new FrmLibrary();
            Application.MdiParent = this;
            Application.Left = 0;
            Application.Top = 0;
            Application.Size = this.ClientRectangle.Size;
            Application.Dock = DockStyle.Fill;
            Application.Show();
        }

        private void waletReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            FrmReports Application = new FrmReports();
            Application.MdiParent = this;
            Application.Left = 0;
            Application.Top = 0;
            Application.Size = this.ClientRectangle.Size;
            Application.Dock = DockStyle.Fill;
            Application.Show();
        }

        private void eTerminalSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            TerminalSetup Application = new TerminalSetup();
            Application.MdiParent = this;
            Application.Left = 0;
            Application.Top = 0;
            Application.Size = this.ClientRectangle.Size;
            Application.Dock = DockStyle.Fill;
            Application.Show();
        }

        private void librarySetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            LibrarySetup Application = new LibrarySetup();
            Application.MdiParent = this;
            Application.Left = 0;
            Application.Top = 0;
            Application.Size = this.ClientRectangle.Size;
            Application.Dock = DockStyle.Fill;
            Application.Show();
        }

        private void menuSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            MenuSetup Application = new MenuSetup();
            Application.MdiParent = this;
            Application.Left = 0;
            Application.Top = 0;
            Application.Size = this.ClientRectangle.Size;
            Application.Dock = DockStyle.Fill;
            Application.Show();

        }

        private void readerSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            ReaderSetting Application = new ReaderSetting();
            Application.MdiParent = this;
            Application.Left = 0;
            Application.Top = 0;
            Application.Size = this.ClientRectangle.Size;
            Application.Dock = DockStyle.Fill;
            Application.Show();
           
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region DaynamicMenu
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void getdynamicmenu()
        {
            try
            {
                string Query = " SELECT menuid FROM UserMenuPermitions WHERE userid  = '" + DatabaseProvider.gLoginUserID + "'";
                cm = new SqlCommand(Query, DBFun.con);
                SqlDataAdapter ADP = new SqlDataAdapter(cm);
                ds = new DataSet();
                ADP.Fill(ds);

                menu_str = ds.Tables[0].Rows[0][0].ToString();
                string[] MenuSpilt = menu_str.Split(',');


                string MenuList = "";
                foreach (string word in MenuSpilt)
                {
                    if(string.IsNullOrEmpty(MenuList))
                    { MenuList += "'" + word + "'"; }
                    else
                    { MenuList += ",'" + word + "'"; }
                    
                }

                DataTable datatable = DBFun.FetchData("SELECT menushortcut, menuname, menuparent FROM MenuList WHERE menushortcut in (" + MenuList + ")");

                DataView view = new DataView(datatable);
                view.RowFilter = "menuparent=0";
                
                disableitems();

                foreach (DataRowView row in view)
                {
                    foreach (ToolStripMenuItem item in menuStrip1.Items)
                    {
                        if (row["menuname"].ToString() == item.Name)
                        {
                            item.Visible = true;
                        }
                        AddchildForms(datatable, row["menushortcut"].ToString(), item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يمكن الاتصال بقاعدة البيانات,الرجاء مراجعة مدير النظام", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void AddchildForms(DataTable datatable, string id, ToolStripMenuItem ToolSMI)
        {
            DataView viewchild = new DataView(datatable);
            //viewchild.RowFilter = "menuparent=" + id; //
            foreach (DataRowView ChildViewitem in viewchild)
            {

                foreach (ToolStripMenuItem item in ToolSMI.DropDownItems)
                {
                    if (ChildViewitem["menuname"].ToString() == item.Name)
                    {
                        item.Visible = true;
                        
                    }

                    AddchildForms(datatable,ChildViewitem["menushortcut"].ToString(), item);
                }

   
                //foreach (ToolStripMenuItem item in MenuIte)
                //{
                //    if (item.Name != "logOutToolStripMenuItem" && item.Name != "homeToolStripMenuItem")
                //    {
                //        item.Visible = false;
                //    }
                //}
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void disableitems ()
        {
            lblLoginName.Text = DatabaseProvider.gLoginFullName;
            List<ToolStripMenuItem> myItems = GetAllMenuStripItems.GetItems(this.menuStrip1);
            foreach (var item in myItems)
            {
                if (item.Name != "logOutToolStripMenuItem" && item.Name != "homeToolStripMenuItem")
                {
                    item.Visible = false;
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
