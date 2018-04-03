using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DesFirePersonalizer.Apps_Cood;

namespace DesFirePersonalizer
{
    public partial class LoginFrm : Form
    {
        DataTable dt;
        public static SqlConnection con = new SqlConnection();
        public bool successLogin = false;
        public LoginFrm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
        try
            {
                if (string.IsNullOrEmpty(TxtLoginID.Text)) { MessageBox.Show("Please Inter User Name", "Message", MessageBoxButtons.OK, MessageBoxIcon.None); return; }
                if (string.IsNullOrEmpty(TxtPassword.Text)) { MessageBox.Show("Please Inter Password", "Message", MessageBoxButtons.OK, MessageBoxIcon.None); return; }
                 string Query = " SELECT UserNID,UsrPassword, UsrFullName,UsrPermission,UserID FROM AppUsers WHERE UsrStatus = 'True'  AND '" + DateTime.Now + "' <= UsrExpireDate AND UsrLoginID = '" + TxtLoginID.Text + "'AND UsrPassword = '" + TxtPassword.Text + "'";
                dt = DBFun.FetchData(Query);
                if (!DBFun.IsNullOrEmpty(dt))
                {
                    string UsrPass = dt.Rows[0]["UsrPassword"].ToString();
                    if (TxtPassword.Text == UsrPass )
                    {
                        successLogin = true;
                        DatabaseProvider.gLoginName =TxtLoginID.Text;
                        DatabaseProvider.gLoginFullName = dt.Rows[0]["UsrFullname"].ToString();
                        DatabaseProvider.gLoginUserID= dt.Rows[0]["UserNID"].ToString();
                        // General.gLoginType = General.LoginType.User;
                        DatabaseProvider.UsrPermission = dt.Rows[0]["UsrPermission"].ToString();
                        this.Close();
                        return;
                    }
                }
           
                else
                {
                    MessageBox.Show("Please Insert Correct User Name Or Password", "Error Login");
                    TxtLoginID.Focus();
                }
            }
            catch 
            {
                
                MessageBox.Show("Procces Not compleate please contact administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Loginfrm_FormClosed(object sender, FormClosedEventArgs e) { if (!successLogin) { CloseAllForms.CloseAllOpenForms(); } }

        private void button2_Click(object sender, EventArgs e)
        {
            TxtLoginID.Text = "";
            TxtPassword.Text = "";
            TxtLoginID.Focus();
        }

        private void LoginFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!successLogin)  { Environment.Exit(1);}
        
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            TxtLoginID.Focus();

            TxtLoginID.Text = "administrator";


        }

        private void TxtLoginID_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
            {
            if (e.KeyCode == Keys.Enter)
            {
                TxtPassword.Focus();
            }
        }

        private void TxtPassword_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

      
    }
}
