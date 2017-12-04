using DesFirePersonalizer.Apps_Cood;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace DesFirePersonalizer
{
    public partial class Savebtn : Form
    { 
        SqlConnection con = new SqlConnection("Data Source=QASEM-PC;Initial Catalog=OneCard_Solution;User ID=sa;Password=admin123");
        SqlCommand cmd;
        SqlDataAdapter ad;
        int ID = 0;  // using for delete & Update 
        private DataTable dt;

        //  public SQlConnect con;
        //public  DataTable dt;
        //public  SqlDataAdapter da;
        /// //////////////////////////////////////////////////
        public Savebtn()
        {  
            InitializeComponent();
            DisplayData(); 
        }
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////
        /// </summary>
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            ad = new SqlDataAdapter("SELECT * FROM AppUsers", con);
            ad.Fill(dt);
            UserGridView.DataSource = dt;
            con.Close();
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        //Clear Data  
        private void ClearData()
        {
            TxtLoginID.Text = "";
            TxtPassword.Text = "";
            TxtUserDescription.Text = "";
            TxtUserName.Text = "";
           // ID = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            con.Open();
            ad = new SqlDataAdapter("select * from AppUsers", con);
            dt = new DataTable();
            ad.Fill(dt);
            UserGridView.DataSource = dt;
            con.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Insertbtn_Click(object sender, EventArgs e)
        {
        }
           
        private void Users_Load(object sender, EventArgs e)
        {
           
            //con.Open();
            //ad = new SqlDataAdapter("select * ", con);
            //dt = new DataTable();
            //ad.Fill(dt);
            //UserGridView.DataSource = dt;
            //con.Close();

        }

        private void UserGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {                  
                TxtLoginID.Text = UserGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                TxtPassword.Text = UserGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                TxtUserDescription.Text = UserGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                TxtUserName.Text = UserGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                ExpiryDate.Text = UserGridView[UserGridView.Columns.IndexOf(UserGridView.Columns["UsrExpireDate"]), e.RowIndex].Value.ToString();
                Deletebtn.Enabled = true;
                Cancelbtn.Enabled = true;
                Editbtn.Enabled = true;
            }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (TxtLoginID.Text != "" && TxtPassword.Text != "")
            {
                cmd = new SqlCommand("INSERT INTO AppUsers(UsrLoginID,UsrFullName,UsrStatus,UsrPassword,UsrDesc,UsrExpireDate) values(@UsrLoginID,@UsrFullName,@UsrStatus,@UsrPassword,@UsrDesc,@UsrExpireDate)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@UsrLoginID", TxtLoginID.Text);
                cmd.Parameters.AddWithValue("@UsrFullName", TxtUserName.Text);
                cmd.Parameters.AddWithValue("@UsrStatus", CeckBoxStatus.Checked.ToString());
                //cmd.Parameters.AddWithValue("@UsrStatus", ComStatus.Text);
                cmd.Parameters.AddWithValue("@UsrPassword", TxtPassword.Text);
                cmd.Parameters.AddWithValue("@UsrDesc", TxtUserDescription.Text);
                cmd.Parameters.AddWithValue("@UsrExpireDate", ExpiryDate.Value);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (TxtLoginID.Text == "admin")
            {
                MessageBox.Show("لا يمكن حذف المستخدم admin", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Deletebtn.Enabled = false;
                Editbtn.Enabled = false;

            }
            else
            {
                if (ID != 0)
                {
                    cmd = new SqlCommand("delete AppUsers whereUsrLoginID =@UsrLoginID", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@UsrLoginID", TxtLoginID);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Deleted Successfully!");
                    DisplayData();
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Please Select Record to Delete");
                }
            }
        }
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editbtn_Click(object sender, EventArgs e)
        {

         
                if (TxtLoginID.Text != "" && TxtPassword.Text != "")
                {

                    cmd = new SqlCommand("UPDATE AppUsers set UsrLoginID=@UsrLoginID,UsrFullName=@UsrFullName,UsrStatus=@UsrStatus,UsrPassword=@UsrPassword,UsrDesc=@UsrDesc,UsrExpireDate=@UsrExpireDate WHERE UsrLoginID=@UsrLoginID", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@UsrLoginID", TxtLoginID.Text);
                    cmd.Parameters.AddWithValue("@UsrFullName", TxtUserName.Text);
                    cmd.Parameters.AddWithValue("@UsrStatus", CeckBoxStatus.Checked.ToString());
                    cmd.Parameters.AddWithValue("@UsrPassword", TxtPassword.Text);
                    cmd.Parameters.AddWithValue("@UsrDesc", TxtUserDescription.Text);
                    cmd.Parameters.AddWithValue("@UsrExpireDate", ExpiryDate.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated Successfully");
                    con.Close();
                    DisplayData();
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Please Select Record to Update");
                }
              
            
        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            TxtLoginID.Text = "";
            TxtPassword.Text = "";
            TxtUserDescription.Text = "";
            TxtUserName.Text = "";
            Deletebtn.Enabled = false;
            

        }

        private void TxtLoginID_TextChanged(object sender, EventArgs e)
        {
          
                        


            }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
           
            con.Open();
            ad = new SqlDataAdapter("select * from AppUsers where UsrLoginID like '" + TxtSearch.Text + "%'", con);
            dt = new DataTable();
            ad.Fill(dt);
            UserGridView.DataSource = dt;
            con.Close();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            ad = new SqlDataAdapter("select * from AppUsers where UsrLoginID like '" + TxtSearch.Text + "%'", con);
            dt = new DataTable();
            ad.Fill(dt);
            UserGridView.DataSource = dt;
            con.Close();
        }

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }
  

  


