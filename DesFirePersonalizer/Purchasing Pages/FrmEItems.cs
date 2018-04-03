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

namespace DesFirePersonalizer.Purchasing_Pages
{
    public partial class FrmEItems : Form
    {
        public FrmEItems()
        {
            InitializeComponent();
        }

        DesFireWrapper dsf;
        IList<Model.Menu> MenuList = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                      //  MenuDataGridView.DataSource = MenuList;

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
            DisplayPriceTextBox.Text = MenuList.ElementAt(DisplayMenuNameListBox.SelectedIndex).Price;
            DisplayStatusTextBox.Clear();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

            //numberEWalletTextBox.Clear();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

                      //  MenuDataGridView.DataSource = menuList;

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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            LoadMenuDb();
            listAvailableMenu();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FrmEItems_Load(object sender, EventArgs e)
        {
            listAvailableMenu();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
