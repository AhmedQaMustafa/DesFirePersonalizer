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

namespace DesFirePersonalizer.Library_Pages
{
    public partial class FrmLibrary : Form
    {
        public FrmLibrary()
        {
            InitializeComponent();
        }
        DesFireWrapper dsf;
        IList<Model.Book> BookList = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

            if (success)
            {
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BookNoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BookTitleTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Title;
            BookAuthorTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Author;
            BookAvailabilityTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Availability == true ? "IN" : "OUT";
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

                        //SetupDataGridView.DataSource = bookList;

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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FrmLibrary_Load(object sender, EventArgs e)
        {
            listAvailableBook();
        }

        private void BtnScanCard_Click_1(object sender, EventArgs e)
        {

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
