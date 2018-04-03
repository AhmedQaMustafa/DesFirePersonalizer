using DesFirePersonalizer.Apps_Cood;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesFirePersonalizer.Configurations_Pages
{
    public partial class MenuSetup : Form
    {
        public MenuSetup()
        {
            InitializeComponent();
        }

        string PermissionScreen = DatabaseProvider.UsrPermission;
        string commandAction = "";
        IList<Model.Menu> MenuList = null;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void searchMenuBtn_Click(object sender, EventArgs e)
        {
            if (MenuIDTextBox.Text.Length < 8)
            {
                MessageBox.Show("ID Number must be 8 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                MenuIDTextBox.Focus();

                return;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool checkFormMenuIsOk()
        {
            if (MenuIDTextBox.Text.Length < 8)
            {
                MessageBox.Show("ID Number must be 8 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

               //SetupBookIDTextBox.Focus();
                return false;
            }

            if (MenuNameTextBox.Text.Length < 3)
            {
                MessageBox.Show("Menu Length minimum is 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

              //  SetupTitleTextBox.Focus();
                return false;
            }

            if (PriceTextBox.Text.Length < 1)
            {
                MessageBox.Show("Price must not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

               // SetupAuthorTextBox.Focus();
                return false;
            }

            return true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                        aMenu.MenuId = MenuIDTextBox.Text;
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MenuSetup_Load(object sender, EventArgs e)
        {
            listAvailableMenu();
            CommandStatus("100000");
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Permitions
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
            BtnAdd.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            showAllMenuBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            searchMenuBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));
            InsertMenuBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString()));
            UpdateMenuBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));
            DeleteMenuBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[5].ToString()));

            if (pBtn[0] != '0') { BtnAdd.Enabled = PermissionScreen.Contains("AddMenuSet"); }
            if (pBtn[0] != '0') { showAllMenuBtn.Enabled = PermissionScreen.Contains("RefresMenuSet"); }
            if (pBtn[1] != '0') { searchMenuBtn.Enabled = PermissionScreen.Contains("SearchMenuSet"); }
            if (pBtn[2] != '0') { InsertMenuBtn.Enabled = PermissionScreen.Contains("InsertMenuSet"); }
            if (pBtn[3] != '0') { UpdateMenuBtn.Enabled = PermissionScreen.Contains("UpdateMenuSet"); }
            if (pBtn[4] != '0') { DeleteMenuBtn.Enabled = PermissionScreen.Contains("DeleteMenuSet"); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void ItemDataStatus(char pType, bool pADD, bool pEdit)
        {
            if (pType == 'E' || pType == 'B') // E=Enabled , B = Enabled&Clear
            {
                MenuIDTextBox.Enabled = pEdit;
                MenuNameTextBox.Enabled = pEdit;
                PriceTextBox.Enabled = pEdit;
            }

            if (pType == 'C' || pType == 'B')  // C=Clear
            {
                MenuIDTextBox.Text = "";
                MenuNameTextBox.Text = "";
                PriceTextBox.Text = "";
               

            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            commandAction = "Add";
            CommandStatus("000111");
            MenuDataGridView.ClearSelection();
            ItemDataStatus('B', true, true);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
