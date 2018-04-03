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
        static SqlDataAdapter da;
        static DataTable dt;
       // static SqlConnection con ;
        static public string SQL = "Data Source=QASEM-PC;Initial Catalog=OneCard_Solution; User ID=sa;Password=admin123";
       static SqlConnection con = new SqlConnection(SQL);
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //get Database Cinnection
        public SqlConnection GetDbConnection()
        {
            return con;
        }
        
        /// //open connections 
      static public  void OpenDbCon()
        {
            if (con .State != ConnectionState.Open)
            {
                con.Open();
            }
          
        }
        //close DB Connections
      static public void CloseDbcon()
        {
            con.Close();
       
        }
      
    
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

        static public bool IsNullOrEmpty(DataTable dt)
        {
            if (dt == null) { return true; }
            if (dt.Rows.Count == 0) { return true; }
            return false;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static public DataTable DaFetchDataQuery(string pQuery)
        {
            try
            {
                da = new SqlDataAdapter();
                con = new SqlConnection(SQL);
                OpenDbCon();
                da.SelectCommand = new SqlCommand(pQuery, con);
                da.SelectCommand.CommandType = CommandType.Text;

                SqlDataReader dr = da.SelectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dt = new DataTable();
                dt.Load(dr);
                dr.Close();
                CloseDbcon();
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static public int DaExecuteDataQuery(string pQuery)
        {
            if (string.IsNullOrEmpty(pQuery)) { return 0; }
            con = new SqlConnection(SQL);
            OpenDbCon();

            da.InsertCommand = new SqlCommand("ExecuteData", con);
            da.InsertCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@Query", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pQuery);
            da.InsertCommand.Parameters.Add(param);

            int rowsAffected = da.InsertCommand.ExecuteNonQuery();
            CloseDbcon();
            return rowsAffected;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }

}
