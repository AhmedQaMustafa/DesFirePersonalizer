using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesFireWrapperLib;
using System.Configuration;
using DesFirePersonalizer.Apps_Cood;

namespace DesFirePersonalizer.Purchasing_Pages
{
    public partial class FrmPurchaseTrAc : Form
    {
        public FrmPurchaseTrAc()
        {
            InitializeComponent();
        }
        bool FormWasReset = true;
        delegate void SetTextCallbackWithStatus(string text, Boolean connStatus);
        delegate void SetTextCallback(string text);
        int mode = 0;
        private static int NOTHING = 0;
        private static int KITCHEN_PURCHASE = 1;
        private static int KITCHEN_TOPUP = 2;
        private static int KITCHEN_BALANCE = 3;
        Boolean isConnected = false;

        DesFireWrapper dsf;


        #region Numbers
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn1_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn2_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn3_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn4_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn5_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn6_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn7_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        private void Btn8_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn9_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Btn0_Click(object sender, EventArgs e)
        {
            updateNumberLcd(sender);

        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region calculator
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnCredit_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = KITCHEN_TOPUP;
            setEdcTextBox("Enter Top Up Amount:" + Environment.NewLine);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnBalance_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = KITCHEN_BALANCE;
            setEdcTextBox("Put your card on terminal and press OK" + Environment.NewLine);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!isConnected)
                return;
            //randomize transaction ID
            //RdnBtnTransID_Click(null, null);
            //RdmBtnAppData_Click(null, null);

            if (mode == KITCHEN_BALANCE)
            {
                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                aStudent.LoadXml(false);

                ValueFile vf = aStudent.getCreditPurseAppValueFile();

                EdcEWalletTextBox.Clear();
                EdcEWalletTextBox.AppendText("Lower Limit: " + Convert.ToInt32(vf.lower_limit_hex, 16).ToString("D") + Environment.NewLine);
                EdcEWalletTextBox.AppendText("Upper Limit: " + Convert.ToInt32(vf.upper_limit_hex, 16).ToString("D") + Environment.NewLine);
                EdcEWalletTextBox.AppendText("Balance: " + Convert.ToInt32(vf.value_hex, 16).ToString("D"));
                numberEWalletTextBox.Clear();
            }
            else if (mode == KITCHEN_PURCHASE)
            {
                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                aStudent.LoadXml(false);

                EdcEWalletTextBox.Clear();

                if (aStudent.doDebitPurseValueFile00(numberEWalletTextBox.Text,
                    getLog(Student.PURCHASE_TRANSACTION, numberEWalletTextBox.Text)))
                    setEdcTextBox("Purchase Successful");
                else
                    setEdcTextBox("Purchase Failed");

                numberEWalletTextBox.Clear();
            }
            else if (mode == KITCHEN_TOPUP)
            {
                Student aStudent = new Student(dsf, ConfigurationManager.AppSettings.Get("TemplateXmlFiles"), "tmp");
                aStudent.LoadXml(false);

                EdcEWalletTextBox.Clear();

                if (aStudent.doCreditPurseValueFile00(numberEWalletTextBox.Text, getLog(Student.TOPUP_TRANSACTION, numberEWalletTextBox.Text)))
                    setEdcTextBox("Top Up Successful");
                else
                    setEdcTextBox("Top Up Failed");

                numberEWalletTextBox.Clear();
            }

            mode = NOTHING;

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnReset_Click(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = NOTHING;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnClear_Click(object sender, EventArgs e)
        {
            String num = numberEWalletTextBox.Text;
            if (!String.IsNullOrWhiteSpace(num))
            {
                String lastChar = num.Substring(num.Length - 1);
                int n;
                bool isNumeric = int.TryParse(lastChar, out n);

                if (true)
                {
                    if (num.Length > 1 && n != 0)
                        setNumberKitchenTextBox(num.Substring(0, num.Length - 1));
                    else
                    {
                        setNumberKitchenTextBox("0");
                        FormWasReset = true;
                    }
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Declaritions
        private void setEdcTextBox(String text)
        {
            if (EdcEWalletTextBox.IsDisposed == false)
            {
                if (this.EdcEWalletTextBox.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(setEdcTextBox);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    EdcEWalletTextBox.Text = text;
                }
            }

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void setNumberKitchenTextBox(String text)
        {
            if (numberEWalletTextBox.IsDisposed == false)
            {
                if (this.numberEWalletTextBox.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(setNumberKitchenTextBox);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    numberEWalletTextBox.Text = text;
                }
            }

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void updateNumberLcd(object sender)
        {
            String text = ((Button)sender).Text;
            if (FormWasReset)
            {
                setNumberKitchenTextBox(text);
                FormWasReset = false;
            }
            else
                numberEWalletTextBox.AppendText(text);

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        private void BtnDebit_Click_1(object sender, EventArgs e)
        {
            FormWasReset = true;
            setEdcTextBox("");
            setNumberKitchenTextBox("0");

            mode = KITCHEN_PURCHASE;
            setEdcTextBox("Enter Purchase Amount:" + Environment.NewLine);
        }
    }
}
