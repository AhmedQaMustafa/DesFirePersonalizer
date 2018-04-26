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

        DataTable dt;
        string PermissionScreen = DatabaseProvider.UsrPermission;
        DatabaseProvider Provider = new DatabaseProvider();
        string commandAction = "";
        string GridQuery = "Select Book_ID,BookTitle,BookAuthor,BookYear,status,Quantity From LibraryBooks ";
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridData(string pWhere1)
        {
            return dt = DBFun.FetchData(GridQuery + " " + pWhere1);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void LibrarySetup_Load(object sender, EventArgs e)
        {
            CommandStatus("111000");
            SetupBookIDTextBox.Enabled = true;
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
            if (SetupBookIDTextBox.Text.Length < 1)
            {
                MessageBox.Show("ID Number must be atleast 1 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            CommandStatus("0001001");
            commandAction = "Edit";
            ItemDataStatus('E', true, true);
            SetupBookIDTextBox.Enabled = false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SetupDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = 0;
                if (ID != -1)
                {

                    Provider.DeleteBooks(SetupBookIDTextBox.Text);
                    MessageBox.Show("Record Deleted Successfully!");

                    CommandStatus("1110000");
                }
                else
                {
                    MessageBox.Show("Please Select Record to Delete");
                }

            }
            catch
            {
                MessageBox.Show("Procces NoT compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            BtnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString()));
            SetupUpdateButton.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));
            SetupDeleteButton.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[5].ToString()));
            btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[6].ToString()));

            if (pBtn[0] != '0') { BtnAdd.Enabled = PermissionScreen.Contains("AddLibSet"); }
            //if (pBtn[0] != '0') { LoadButton.Enabled = PermissionScreen.Contains("RefreshLibSet"); }
            //if (pBtn[1] != '0') { SearchBtn.Enabled = PermissionScreen.Contains("SearchLibSet"); }
            if (pBtn[3] != '0') { BtnSave.Enabled = PermissionScreen.Contains("InsertLibSet"); }
            if (pBtn[4] != '0') { SetupUpdateButton.Enabled = PermissionScreen.Contains("UpdateLibSet"); }
            if (pBtn[5] != '0') { SetupDeleteButton.Enabled = PermissionScreen.Contains("DeleteLibSet"); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            commandAction = "Add";
            CommandStatus("0101001");
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
                SetupStatusAvailabilityComboBox.Enabled = pEdit;
                TxtQuantity.Enabled = pEdit;


            }

            if (pType == 'C' || pType == 'B')  // C=Clear
            {
                SetupBookIDTextBox.Text = "";
                SetupTitleTextBox.Text = "";
                SetupAuthorTextBox.Text = "";
                SetupYearTextBox.Text = "";
                TxtQuantity.Text = "";
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (checkFormLibraryIsOk() == false)
                return;
            try
            {
                dt = FillGridData("WHERE Book_ID= '" + SetupBookIDTextBox.Text + " ' ");
                if (commandAction == "Add")
                {

                    if (DBFun.IsNullOrEmpty(dt))
                    {
                        Provider.inserBooks(SetupBookIDTextBox.Text, SetupTitleTextBox.Text, SetupAuthorTextBox.Text, SetupYearTextBox.Text, SetupStatusAvailabilityComboBox.Text,TxtQuantity.Text);
                        CommandStatus("1110000");
                        SetupBookIDTextBox.Enabled = true;
                        SetupDataGridView.Enabled = true;
                        ItemDataStatus('E', false, false);
                        commandAction = "";
                        MessageBox.Show("Meal Added Succesfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("This Meal Exsiting ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (commandAction == "Edit")
                {
                    if (SetupBookIDTextBox.Text != "")
                    {
                        Provider.UpdateBooks(SetupBookIDTextBox.Text, SetupTitleTextBox.Text, SetupAuthorTextBox.Text, SetupYearTextBox.Text, SetupStatusAvailabilityComboBox.Text, TxtQuantity.Text);
                        CommandStatus("1110000");
                        ItemDataStatus('E', false, false);
                        commandAction = "";
                        MessageBox.Show("Updated Done Successfuly", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Please Fill All Fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (commandAction == "Add") { CommandStatus("1110001"); ItemDataStatus('B', false, false); }
            if (commandAction == "Edit") { CommandStatus("1110001"); ItemDataStatus('B', false, false); }
            SetupBookIDTextBox.Enabled = true;
            commandAction = "";
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchBtn_Click_1(object sender, EventArgs e)
        {
            if (SetupBookIDTextBox.Text.Length < 1)
            {
                MessageBox.Show("ID Number must be 1 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetupBookIDTextBox.Focus();

                return;
            }
            else
            {
                SearchData();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchData()
        {
            if (SetupBookIDTextBox.Text != "")
            {
                if (!String.IsNullOrEmpty(SetupBookIDTextBox.Text)) { SetupDataGridView.DataSource = FillGridData(" WHERE Book_ID LIKE '%" + SetupBookIDTextBox.Text + "%'"); } else { SetupDataGridView.DataSource = FillGridData("Where Book_ID = -1"); SetupDataGridView.ClearSelection(); }
            }
            else
            {
                MessageBox.Show("Please Fill Search Data", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void LoadButton_Click_1(object sender, EventArgs e)
        {
            SetupDataGridView.DataSource = FillGridData("");
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DataRow GetCurrentRow(DataGridView UserGrid)
        {
            DataRowView drv = null;
            try
            {
                if (UserGrid.CurrentRow == null) { return null; }
                if (UserGrid.CurrentRow.DataBoundItem == null) { return null; }
                drv = (DataRowView)UserGrid.CurrentRow.DataBoundItem;
            }
            catch { return null; }
            return drv.Row;
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SetupDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    DataRow vDataRow = GetCurrentRow(SetupDataGridView);
                    object[] vData = vDataRow.ItemArray;
                    SetupBookIDTextBox.Text = SetupDataGridView.Rows[e.RowIndex].Cells["Book_ID"].Value.ToString();
                    SetupTitleTextBox.Text = SetupDataGridView.Rows[e.RowIndex].Cells["BookTitle"].Value.ToString();
                    SetupAuthorTextBox.Text = SetupDataGridView.Rows[e.RowIndex].Cells["BookAuthor"].Value.ToString();
                    SetupYearTextBox.Text = SetupDataGridView.Rows[e.RowIndex].Cells["BookYear"].Value.ToString();
                    SetupStatusAvailabilityComboBox.Text = SetupDataGridView.Rows[e.RowIndex].Cells["status"].Value.ToString();
                    CommandStatus("0000111");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
