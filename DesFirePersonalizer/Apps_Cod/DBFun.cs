﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace DesFirePersonalizer.Apps_Cood
{
    public class DBFun
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
         static  SqlDataAdapter da = new SqlDataAdapter();
        //static SqlConnection con;
         static DataTable dt;
        // public static string SQL = "Data Source=QASEM-PC;Initial Catalog=OneCard_Solution; User ID=sa;Password=admin123";
        public static string SQL = "Data Source=50.62.35.11;Initial Catalog=OneCard_Solution;Integrated Security=False; Persist Security Info=True; User ID=ams;Password=123123123;Connection Timeout=6000; Max pool size=3500;";
        public static SqlConnection con = new SqlConnection(SQL);

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static public void OpenCon()
        {
            try
            {
                con = new SqlConnection(SQlConnect.SQL);
                if (con.State != ConnectionState.Open) { con.Open(); }
            }
            catch (Exception ex) { throw ex; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static public void CloseCon()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
            catch (Exception ex) { throw ex; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static public DataTable FetchData(string pQuery)
        {
            try
            {
                OpenCon();
                da.SelectCommand = new SqlCommand(pQuery, con);
                da.SelectCommand.CommandType = CommandType.Text;
               // OpenCon();
                SqlDataReader dr = da.SelectCommand.ExecuteReader(); 
                dt = new DataTable();
                dt.Load(dr);
                dr.Close();
                CloseCon();
                return dt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("لا يمكن الاتصال بقاعدة البيانات,الرجاء مراجعة مدير النظام", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //dt = new DataTable();
                //return dt;
                throw ex;
            }
        }

   
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static public int ExecuteData(string pQuery)
        {
            if ((pQuery == string.Empty) || (pQuery == null)) { return -1; }
            OpenCon();
            da.InsertCommand = new SqlCommand(pQuery,con);
            da.InsertCommand.CommandType = CommandType.Text;
            int rowsAffected = da.InsertCommand.ExecuteNonQuery();
            CloseCon();
            return rowsAffected;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static public bool UpdateData(string pQuery)
        {
            try
            {
                if ((pQuery == string.Empty) || (pQuery == null)) { return false; }
                OpenCon();
                da.InsertCommand = new SqlCommand(pQuery, con);
                da.InsertCommand.CommandType = CommandType.Text;
                int rowsAffected = da.InsertCommand.ExecuteNonQuery();
                CloseCon();

                return true;
            }
            catch (Exception ex) { return false; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static public bool IsNullOrEmpty(DataTable dt)
        {
            if (dt == null) { return true; }
            if (dt.Rows.Count == 0) { return true; }
            return false;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     
    }
}



