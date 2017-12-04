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



namespace DesFirePersonalizer
{
    public partial class LoginFrm : Form
    {
        SqlConnection con = new SqlConnection ();
        public bool successLogin = false;
        public LoginFrm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(TxtUserName.Text)) { MessageBox.Show("Please Inter User Name", "Message", MessageBoxButtons.OK, MessageBoxIcon.None); return; }
            if (string.IsNullOrEmpty(TxtPassword.Text)) { MessageBox.Show("Please Inter Password", "Message", MessageBoxButtons.OK, MessageBoxIcon.None); return; }
            SqlCommand cmd = new SqlCommand("select * from AppUsers where UsrLoginID=@UsrLoginID and UsrPassword =@UsrPassword", con);
            cmd.Parameters.AddWithValue("@UsrLoginID", TxtUserName.Text);
            cmd.Parameters.AddWithValue("@UsrPassword", TxtPassword.Text);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (dt.Rows.Count > 0)
            {
                MainForm sloginform = new MainForm();
                sloginform.Show();
            }

            else
            {

                MessageBox.Show("Please enter Correct Username and Password");
            }

        }

        private void LoginFrm_FormClosed(object sender, FormClosedEventArgs e) { if (!successLogin) { CloseAllForms.CloseAllOpenForms(); } }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();


        }

        private void LoginFrm_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            {
                if (MessageBox.Show("Are you sure want to exit?",
                                   "Exit",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                    Environment.Exit(1);
                }
            }
        }
    }
}
