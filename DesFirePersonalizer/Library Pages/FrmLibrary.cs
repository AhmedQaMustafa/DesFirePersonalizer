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
using PCSC;
using DesFirePersonalizer.Apps_Cood;
using PCSC.Iso7816;

namespace DesFirePersonalizer.Library_Pages
{
    public partial class FrmLibrary : Form
    {

        delegate void SetTextCallbackWithStatus(string text, Boolean connStatus);
        delegate void SetTextCallback(string text);

        SCardContext scc = null;
        SCardReader scr = null;
        SCardMonitor scm = null;
        SCardState scs;
        SCardProtocol scp;
        byte[] atr = null;
        string atrStr = null;
        string[] CardReaderList = null;
        int index = 0;
        String logString;
        DataTable BooLi;
        DataTable dtStd;
        DataTable dt;
        DataTable bookdt;
        Random randomInstance;
        Boolean isConnected = false;
        private static readonly IContextFactory _contextFactory = ContextFactory.Instance;
        DatabaseProvider Provider = new DatabaseProvider();
        String[] readername = null;
        public FrmLibrary()
        {
            InitializeComponent();
        }
        DesFireWrapper dsf;
        IList<Model.Book> BookList = null;
        string GridQuery = "Select * From  LibraryBooks  ";
        RecordFile book1;
        RecordFile book2;
        RecordFile book1Rf;
        RecordFile book2Rf;
        Student aStudent;
        ValueFile vf;

        bool success = false;


        #region fill data 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillCategoryData()
        {

            string GridQuery1 = " SELECT 0 as Record_ID,'Please select any value'  as Book_ID, null as BookTitle UNION ALL SELECT Record_ID, Book_ID ,BookTitle FROM LibraryBooks"; //

            return BooLi = DBFun.FetchData(GridQuery1);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FillBookList()
        {
            BooLi = FillCategoryData();
            BookNoComboBox.ValueMember = "Book_ID";
            BookNoComboBox.DataSource = BooLi;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string StdGrid = "SELECT StudentID FROM StudentsTable"; //

        private DataTable FillSTDData(string pWhere1)
        {
            return dtStd = DBFun.FetchData(StdGrid + " " + pWhere1);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string bookgrid = " SELECT StudentID ,Std_flag_borrow ,Book_ID,return_date ,borrow_date FROM BorrowReturntbl "; //

        private DataTable FillbookData(string pWhere1)
        {
            return bookdt = DBFun.FetchData(bookgrid + " " + pWhere1 );
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnReturn_Click(object sender, EventArgs e)
        {
            bool success = false;
            try
                {
                    if (TxtStdInc.Text.Trim() !="")
                    {
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

                        Provider.returnBooks(TxtStdInc.Text, Book1NoTextBox.Text, Book1ReturnedDateTimePicker.Text);
                        Provider.IncreaseQuantity(TxtbookQuantiy.Text, BookNoComboBox.SelectedValue.ToString());

                        String logString = book2Rf.content.Substring(0, Student.LIB_LOG_LENGTH - 20) + DateReturned;
                        logString = logString.Remove(48, 2).Insert(48, Student.RETURN_TRANSACTION);

                        aStudent.doWriteLibLog(logString, Student.BOOK2_LOG_FILE_ID);

                        success = true;
                    }
                    if (success)
                    {
                        aStudent.processLibraryCreditFileAndCommit((BasicFile)vf, Student.OPERATION_INCREMENT);

                      //  setBookAvailabilityStatus(true);
                        //string status = bookdt.Rows[0]["Std_flag_borrow"].ToString();
                        //if (status == "1")
                        //{
                        //    Book1StatusTextBox.Text = "Booked";
                        //}
                        //else
                        //{
                        //    Book1StatusTextBox.Text = "Return";
                        //}
                    }

                    if (!success)
                        MessageBox.Show("Fail returning the book", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        BtnScanCard_Click(null, null);

                        MessageBox.Show("Returning the book was successful", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    }
                    else
                    {
                        MessageBox.Show("Please Scan Card First" , "Notifications");
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("", "");
                }

           // listAvailableBook();

            return;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnBorrow_Click(object sender, EventArgs e)
        {
         
            //scan the card first
           // BtnScanCard_Click(null, null);
           // return;
            /*------------------------------------------------------------------------------------------------------------*/
            //Take the student library file configuration
            aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
            aStudent.LoadXml(false);

            vf = aStudent.getCreditLibraryAppValueFile();

            LibraryFreeSlotTextBox.Text = vf.value_hex;

            //Check if cards are empty
            //If record in cyclic file book1 file is > 0
            book1 = (RecordFile)aStudent.getFileSettings(Student.BOOK1_LOG_FILE_ID);
            book1Rf = null; ;
            /*------------------------------------------------------------------------------------------------------------*/
            if (book1.getCurrentNoOfRecordsInInt() > 0)
            {
                // read 
                BasicFile bf1 = (BasicFile)book1;
                aStudent.readFile(ref bf1);
                book1Rf = (RecordFile)bf1;
            }
            /*------------------------------------------------------------------------------------------------------------*/
            //If record in cyclic file book2 file is > 0
            book2 = (RecordFile)aStudent.getFileSettings(Student.BOOK2_LOG_FILE_ID);
            book2Rf = null; ;
            if (book2.getCurrentNoOfRecordsInInt() > 0)
            {
                BasicFile bf2 = (BasicFile)book2;
                aStudent.readFile(ref bf2);
                book2Rf = (RecordFile)bf2;
            }
            /*------------------------------------------------------------------------------------------------------------*/
            //if value file is 0, means that no slot available to borrow the book
            if (vf.getValueInt() == 0)
            {
                MessageBox.Show("No Slot Available, please return book(s) to start borrowing!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            /*------------------------------------------------------------------------------------------------------------*/
            try
            {
                dt = FillGridData("WHERE Book_ID = '" +BookNoComboBox.SelectedValue+ "' ");
                dtStd = FillSTDData("WHERE StudentID = '" +TxtStdID.Text+ " ' ");
                bookdt = FillbookData("WHERE StudentID = '" +TxtStdID.Text+ "' and Std_flag_borrow= 1 and Book_ID='" +BookNoComboBox.SelectedValue+ "'"); // for filling Quantity of books 

                if (TxtStdID.Text.Trim() != "")
                {
                    if (!DBFun.IsNullOrEmpty(dt) && BookAuthorTextBox .Text != "" && TerminalIDLibComboBox.SelectedItem.ToString() != null)
                    {
                        if (TxtbookQuantiy.Text == "0") { MessageBox.Show("No Book Available", "Notes"); return; }
                        if (!DBFun.IsNullOrEmpty(bookdt)) { MessageBox.Show("you already have same book you cant borrow another one ", "Notes"); return; }
                        Provider.borrowbooks(TxtStdID.Text, BookNoComboBox.SelectedValue.ToString(), TxtAllowdQuantity.Text, TerminalIDLibComboBox.SelectedItem.ToString(), BookDateTimePicker.Value.ToString(), LblIncode.Text);
                        Provider.DecreaseQuantity(TxtbookQuantiy.Text, BookNoComboBox.SelectedValue.ToString());
                           //start borrowing
                           String DateBorrowed = MyUtil.asciiToHexString(BookDateTimePicker.Text);

                        //write here
                        logString = TerminalIDLibComboBox.Text.Substring(0, 8) +
                        BookNoComboBox.Text.Substring(0, 8) +
                        getPadString(BookTitleTextBox.Text, 32) +
                        TxtStdID.Text.Substring(0, 8) +
                        Student.BORROW_TRANSACTION +
                        DateBorrowed +
                        "000000000000";

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

                           // setBookAvailabilityStatus(false);
                        }

                        if (!success)
                            MessageBox.Show("Fail borrowing the book", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            BtnScanCard_Click(null, null);

                            MessageBox.Show("Borrowing the book was successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }

                    else
                    {
                        MessageBox.Show("Student ID Not Exist Or  data missing ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (string.Equals(BookAvailabilityTextBox.Text, "OUT", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Book is not available, cannot be borrowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                }

                else
                {
                    MessageBox.Show("Please Scan card first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            listAvailableBook();

            return;

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable FillGridData(string pWhere1)
        {
            return dtStd = DBFun.FetchData(GridQuery + " " + pWhere1);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BookNoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtStd = FillGridData("WHERE Book_ID = '" + BookNoComboBox.SelectedValue + " ' ");
            if (!DBFun.IsNullOrEmpty(dtStd))
            {
                BookTitleTextBox.Text = dtStd.Rows[0]["BookTitle"].ToString();
                BookAuthorTextBox.Text = dtStd.Rows[0]["BookAuthor"].ToString();
                BookAvailabilityTextBox.Text = dtStd.Rows[0]["status"].ToString();
                TxtbookQuantiy.Text = dtStd.Rows[0]["Quantity"].ToString();
                TxtAllowdQuantity.Text = "1";
            }
            
            //TerminalIDLibComboBox.Text = dt.Rows[0][""].ToString();
            //BookAuthorTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Author;
            //BookAvailabilityTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Availability == true ? "IN" : "OUT";



            //BookTitleTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Title;
            //BookAuthorTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Author;
            //BookAvailabilityTextBox.Text = BookList.ElementAt(BookNoComboBox.SelectedIndex).Availability == true ? "IN" : "OUT";
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void parseAndDisplayLogBook1(String log)
        {
            BookLog bl = new BookLog(log);


            Book1NoTextBox.Text = bl.book.BookID;
            Book1TitleTextBox.Text = MyUtil.ConvertHextoAscii(bl.book.Title);
            Book1TerminalIDTextBox.Text = bl.TerminalID;
            TxtStdInc.Text = bl.book.STDID;
            TxtStdID.Text = bl.book.STDID;
        
            bookdt = FillbookData("WHERE StudentID = '" +TxtStdID.Text+ "' and Book_ID='" +Book1NoTextBox.Text+ "'"); // for filling Quantity of books 
           // string status = bookdt.Rows[0]["Std_flag_borrow"].ToString();

            if (bl.Type == Student.BORROW_TRANSACTION)
            {
                Book1StatusTextBox.Text = "Booked";
                Book1BorrowedDateTimePicker.Text = bookdt.Rows[0]["borrow_date"].ToString();
                // Book1ReturnedDateTimePicker.Text = "present";
            }
            else
            {
            Book1StatusTextBox.Text = "Return";
            Book1ReturnedDateTimePicker.Text = bookdt.Rows[0]["return_date"].ToString();

            Book1BorrowedDateTimePicker.Enabled = true;
            Book1ReturnedDateTimePicker.Enabled = true;
            }

            if (bl.Type == Student.BORROW_TRANSACTION)
            {
               // Book1BorrowedDateTimePicker.Value = bl.getDateBorrowed(); // date  saved on card 
                Book1ReturnedDateTimePicker.Enabled = false;
            }
            else if (bl.Type == Student.RETURN_TRANSACTION)
            {
                Book1BorrowedDateTimePicker.Text = bookdt.Rows[0]["borrow_date"].ToString();
                Book1ReturnedDateTimePicker.Text = bookdt.Rows[0]["return_date"].ToString();
                //Book1BorrowedDateTimePicker.Value = bl.getDateBorrowed();
                //Book1ReturnedDateTimePicker.Value = bl.getDateReturned();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string getPadStringStdID(String text, int totalLen)
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void listAvailableBook()
        {
            FillBookList();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void loadFromSettings()
        {
            cardReaderToolStrip.Text = ConfigurationManager.AppSettings.Get("CardReader");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void FrmLibrary_Load(object sender, EventArgs e)
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
            scc.Cancel();
            scc.Dispose();
            scc = null;

            redoRegisterEvent();
            loadFromSettings();

            randomInstance = new Random();
            RdnBtnTransID_Click(null, null);
            RdmBtnAppData_Click(null, null);

            //   listAvailableBook();
            FillBookList();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnScanCard_Click_1(object sender, EventArgs e)
        {
            try
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
                    success = true;
                }

                if (book2.getCurrentNoOfRecordsInInt() > 0)
                {
                    //read and write to form
                    //read and write to form
                    BasicFile bf = (BasicFile)book2;

                    aStudent.readFile(ref bf);

                    RecordFile book2Rf = (RecordFile)bf;
                    RecordFile book21Rf = (RecordFile)bf;

                    resetBook2Form();
                    parseAndDisplayLogBook2(book2Rf.content);
                    success = true;
                }
                //if (success)
                //{
                //    bookdt = FillbookData("WHERE StudentID = '" +TxtStdID.Text+ "' and Std_flag_borrow = 1 and Book_ID='" +Book1NoTextBox.Text+ "'"); // for filling Quantity of books 
                //   // string status = bookdt.Rows[0]["Std_flag_borrow"].ToString();
                //    //string datereturn = bookdt.Rows[0]["return_date"].ToString();
                //    if (!DBFun.IsNullOrEmpty(bookdt))
                //    {
                //        Book1StatusTextBox.Text = "Booked";
                //        Book1BorrowedDateTimePicker.Text = bookdt.Rows[0]["borrow_date"].ToString();
                //        //Book1ReturnedDateTimePicker.Text = "present";
                //    }
                //    else
                //    {
                //        Book1StatusTextBox.Text = "Return";
                //        Book1ReturnedDateTimePicker.Text = bookdt.Rows[0]["return_date"].ToString();
            //}
                //}
            }
            catch  (Exception ex)
            {
                MessageBox.Show("Please pe sure the reader connected or card placed on the reader");

            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

            catch (Exception msge) //
            {
                return;
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
