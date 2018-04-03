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

namespace DesFirePersonalizer.Reports
{
    public partial class FrmReports : Form
    {
        public FrmReports()
        {
            InitializeComponent();
        }
        DesFireWrapper dsf;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void doTransaction(object sender, EventArgs e, String logFileID)
        {
            Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
            aStudent.LoadXml(false);

            textBoxLog.Clear();

            RecordFile rf = aStudent.readPurseLog(logFileID);

            if (Convert.ToInt32(rf.currentNoOfRecords, 16) > 0)
            {
                int rec_size_int = Convert.ToInt32(rf.record_size, 16);

                List<StudentLogClass> log = new List<StudentLogClass>();
                int totalRec = (rf.content.Length / 2) / rec_size_int;

                int start = 0;
                for (int i = 0; i < totalRec; i++)
                {
                    //textBoxLog.AppendText("Raw Log: " + (rf.content.Substring(start, rec_size_int * 2)) + Environment.NewLine);
                    textBoxLog.AppendText(Environment.NewLine + parseLogDataRaw(i, (rf.content.Substring(start, rec_size_int * 2))));
                    //log.Add(new StudentLogClass(rf.content.Substring(start, rec_size_int * 2)));
                    start += rec_size_int * 2;
                }
            }
            else
            {
                if (logFileID == Student.CREDIT_PURSE_LOG_FILE_ID)
                    textBoxLog.AppendText("No TOP UP transactions had been made");
                else
                    textBoxLog.AppendText("No purchase transactions had been made");
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private String parseLogDataRaw(int LogNum, String raw)
        {
            int index = 0;
            //< !--LOG = TerminalID(8) || StoreID(8) || Transaction Type(1) || Date(3) || Time(3) || Amount(4)-- >
            String TerminalID = raw.Substring(index, 8 * 2); index += 8 * 2;
            //String StoreID = raw.Substring(index, 8 * 2); index += 8 * 2;
            String TransType = raw.Substring(index, 1 * 2); index += 1 * 2;
            String Date = raw.Substring(index, 4 * 2); index += 4 * 2;
            String Time = raw.Substring(index, 3 * 2); index += 3 * 2;
            String Amount = raw.Substring(index, 4 * 2); index += 4 * 2;
            String TransactionID = raw.Substring(index, 4 * 2); index += 4 * 2;
            String CustomerData = raw.Substring(index);

            TerminalID = "Terminal ID: " + TerminalID + Environment.NewLine;
            //StoreID = "Store ID: " + StoreID + Environment.NewLine;
            TransType = "Transaction Type: " + TransType + Environment.NewLine;
            Date = "Date: " + Date + Environment.NewLine;
            Time = "Time: " + Time + Environment.NewLine;
            Amount = "Amount: " + Amount + Environment.NewLine;
            TransactionID = "Transaction ID: " + TransactionID + Environment.NewLine;
            CustomerData = "Customer Data: " + CustomerData + " (" + MyUtil.ConvertHextoAscii(CustomerData) + ")" + Environment.NewLine;

            String recordHeader = "Record " + LogNum.ToString("D2") + ":" + Environment.NewLine;

            //return recordHeader + TerminalID + StoreID + TransType + Date + Time + Amount + TransID;
            return recordHeader + TerminalID + TransType + Date + Time + Amount + TransactionID + CustomerData;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnReadLogPurchase_Click(object sender, EventArgs e)
        {
            doTransaction(sender, e, Student.DEBIT_PURSE_LOG_FILE_ID);
        }

        private void BtnReadLogTopUp_Click(object sender, EventArgs e)
        {
            doTransaction(sender, e, Student.CREDIT_PURSE_LOG_FILE_ID);
        }
    }
}
