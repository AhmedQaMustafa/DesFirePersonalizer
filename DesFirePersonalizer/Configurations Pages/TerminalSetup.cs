using DesFirePersonalizer.Apps_Cood;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesFirePersonalizer.Configurations_Pages
{
    public partial class TerminalSetup : Form
    {
        public TerminalSetup()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Random randomInstance;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void TerminalSetup_Load(object sender, EventArgs e)
        {
            TerminalIDEPurse.Text = ConfigurationManager.AppSettings.Get("TerminalID");
            RdnBtnTransID_Click(null, null);
            RdmBtnAppData_Click(null, null);
            TransactionIDEPurse.Text = DatabaseProvider.TransactionCommand;
            ApplicationDataEPurse.Text = DatabaseProvider.ApplicationDataCommand;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void RdnBtnTransID_Click(object sender, EventArgs e)
        {
            Int64 number = randomInstance.Next();
            TransactionIDEPurse.Text = number.ToString("X8");
        }

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
    }
}
