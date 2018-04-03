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
using NHibernate;
using System.IO;
using DesFirePersonalizer.Apps_Cood;

namespace DesFirePersonalizer.Configurations_Pages
{
    public partial class LibrarySetup : Form
    {
        public LibrarySetup()
        {
            InitializeComponent();
        }
        string PermissionScreen = DatabaseProvider.UsrPermission;
        string commandAction = "";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void LibrarySetup_Load(object sender, EventArgs e)
        {
            CommandStatus("100000");
        }
        IList<Model.Book> BookList = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Declarations
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadBookDb();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (SetupBookIDTextBox.Text.Length < 8)
            {
                MessageBox.Show("ID Number must be 8 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupBookIDTextBox.Focus();

                return;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SetupInsertButton_Click_1(object sender, EventArgs e)
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Permitions
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void CommandStatus(String pBtn) // A-E-D-S-C
        {
            BtnAdd.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
            LoadButton.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
            SearchBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));
            SetupInsertButton.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString()));
            SetupUpdateButton.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));
            SetupDeleteButton.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[5].ToString()));

            if (pBtn[0] != '0') { BtnAdd.Enabled = PermissionScreen.Contains("AddLibSet"); }
            if (pBtn[0] != '0') { LoadButton.Enabled = PermissionScreen.Contains("RefreshLibSet"); }
            if (pBtn[1] != '0') { SearchBtn.Enabled = PermissionScreen.Contains("SearchLibSet"); }
            if (pBtn[2] != '0') { SetupInsertButton.Enabled = PermissionScreen.Contains("InsertLibSet"); }
            if (pBtn[3] != '0') { SetupUpdateButton.Enabled = PermissionScreen.Contains("UpdateLibSet"); }
            if (pBtn[4] != '0') { SetupDeleteButton.Enabled = PermissionScreen.Contains("DeleteLibSet"); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            commandAction = "Add";
            CommandStatus("000111");
            SetupDataGridView.ClearSelection();
            ItemDataStatus('B', true, true);
    
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void ItemDataStatus(char pType, bool pADD, bool pEdit)
        {
            if (pType == 'E' || pType == 'B') // E=Enabled , B = Enabled&Clear
            {
                SetupBookIDTextBox.Enabled = pEdit;
                SetupTitleTextBox.Enabled = pEdit;
                SetupAuthorTextBox.Enabled = pEdit;
                SetupYearTextBox.Enabled = pEdit;
                //TxtUserName.Enabled = pADD;
                //TxtPassword.Enabled = pADD;
                //CeckBoxStatus.Enabled = pADD;
                //ExpiryDate.Enabled = pADD;
                //TxtUserDescription.Enabled = pADD;

            }

            if (pType == 'C' || pType == 'B')  // C=Clear
            {
                SetupBookIDTextBox.Text = "";
                SetupTitleTextBox.Text = "";
                SetupAuthorTextBox.Text = "";
                SetupYearTextBox.Text = "";

            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
