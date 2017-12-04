using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;


namespace DesFirePersonalizer.Apps_Cood
{
    public class SQlConnect
    {
        //private SqlConnection con;
        public SqlCommand com;
        private SqlDataAdapter da;
        private DataTable dt;
        static string SQL = "Data Source=QASEM-PC;Initial Catalog=OneCard_Solution; User ID=sa;Password=admin123;";
        //public SQlConnect()
        //{
        SqlConnection con = new SqlConnection(SQL);
        // con.Open();
        //Return Database Connection
        public SqlConnection GetDbConnection()
        {
            return con;
        }

        //}
   ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SqlQuery(string querytext)
        {
            com = new SqlCommand(querytext,con);    
        }

        public DataTable QueryEx()
        {
            da = new SqlDataAdapter(com);
            dt = new DataTable();
            da.Fill(dt);
            return (dt);
        }

        public void NonQueryEx()
        {
            com.ExecuteNonQuery();
        }

       
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   
    
    }
