﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;

namespace DesFirePersonalizer.Apps_Cood
{
    class DatabaseProvider
    {
        //Call Class SqlConnect
        SQlConnect Dbconnection = new SQlConnect();

        #region  Parameters 
        public string UsrLoginID { get; private set; }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static protected string gUsrPermission = "";
        static public string UsrPermission { get { return gUsrPermission; } set { gUsrPermission = value; } }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static protected string STDID = "";
        static public string StedentID { get { return STDID; } set { STDID = value; } }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static protected string STDName = "";
        static public string StedentName { get { return STDName; } set { STDName = value; } }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static protected string _loginFullName = "";
        public static string gLoginFullName { get { return _loginFullName; } set { _loginFullName = value; } }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static string g_LoginName = "";
        public static string gLoginName { get { return g_LoginName; } set { g_LoginName = value; } }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static string g_command = "";
        public static string Command { get { return g_command; } set { g_command = value; } }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static string g_TransactionIDEPurse = "";
        public static string TransactionCommand { get { return g_TransactionIDEPurse; } set { g_TransactionIDEPurse = value; } }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static string g_ApplicationDataEPurse = "";
        public static string ApplicationDataCommand { get { return g_ApplicationDataEPurse; } set { g_ApplicationDataEPurse = value; } }
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static string g_LoginUserID = "";
        public static string gLoginUserID { get { return g_LoginUserID; } set { g_LoginUserID = value; } }

        
        #region Add Update Delete Users
        //insert new user into db
        public void InsertNewUser(String TxtLoginID, string TxtUserName, string CeckBoxStatus, string TxtPassword, string TxtUserDescription, ValueType ExpiryDate ,string strPermission,string Catcombbox,string CombPermitions,string UserNID)
        {
            SQlConnect.OpenDbCon();
            string Query = ("INSERT INTO AppUsers(UsrLoginID,UserNID,UsrFullName,UsrStatus,UsrPassword,UsrDesc,UsrExpireDate,UsrPermission,UserCategory,UserGroup) values(@UsrLoginID,@UserNID,@UsrFullName,@UsrStatus,@UsrPassword,@UsrDesc,@UsrExpireDate,@strPermission,@UserCategory,@UserGroup)");
            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@UsrLoginID", TxtLoginID);
            SQlCMD.Parameters.AddWithValue("@UsrFullName", TxtUserName);
            SQlCMD.Parameters.AddWithValue("@UsrStatus", CeckBoxStatus);
            SQlCMD.Parameters.AddWithValue("@UsrPassword", TxtPassword);
            SQlCMD.Parameters.AddWithValue("@UserNID", UserNID);
            SQlCMD.Parameters.AddWithValue("@UsrDesc", TxtUserDescription);
            SQlCMD.Parameters.AddWithValue("@UsrExpireDate", ExpiryDate);
            SQlCMD.Parameters.AddWithValue("@strPermission", strPermission);
            SQlCMD.Parameters.AddWithValue("@UserCategory", Catcombbox);
            SQlCMD.Parameters.AddWithValue("@UserGroup", CombPermitions);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Update Users Metod*/

        public void UpdateUsers(String TxtLoginID, string UserNID, string TxtUserName, string CeckBoxStatus, string TxtPassword, string TxtUserDescription, ValueType ExpiryDate,string strPermission, string Catcombbox,string CombPermitions )
        {
            SQlConnect.OpenDbCon();
            string UpdateQuery = ("UPDATE AppUsers set UsrLoginID=@UsrLoginID,UserNID=@UserNID,UsrFullName=@UsrFullName,UsrStatus=@UsrStatus,UsrPassword=@UsrPassword,UsrDesc=@UsrDesc,UsrExpireDate=@UsrExpireDate,UsrPermission=@strPermission,UserCategory=@UserCategory,UserGroup=@UserGroup WHERE UsrLoginID=@UsrLoginID");
            SqlCommand SQlCMD = new SqlCommand(UpdateQuery, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@UsrLoginID", TxtLoginID);
            SQlCMD.Parameters.AddWithValue("@UserNID", UserNID);
            SQlCMD.Parameters.AddWithValue("@UsrFullName", TxtUserName);
            SQlCMD.Parameters.AddWithValue("@UsrStatus", CeckBoxStatus);
            SQlCMD.Parameters.AddWithValue("@UsrPassword", TxtPassword);
            SQlCMD.Parameters.AddWithValue("@UsrDesc", TxtUserDescription);
            SQlCMD.Parameters.AddWithValue("@UsrExpireDate", ExpiryDate);
            SQlCMD.Parameters.AddWithValue("@strPermission", strPermission);
            SQlCMD.Parameters.AddWithValue("@UserCategory", Catcombbox);
            SQlCMD.Parameters.AddWithValue("@UserGroup", CombPermitions);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*Delete Current users*/
        public void DeleteUsersQuery(String TxtLoginID)
        {
            SQlConnect.OpenDbCon();
            string DeleteUserQuery = ("DELETE From AppUsers WHERE UsrLoginID='" + TxtLoginID + "'");
            SqlCommand SQlCMD = new SqlCommand(DeleteUserQuery, Dbconnection.GetDbConnection());
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*search */
        public DataTable Louad_GridView(String TxtSearch)
        {
            SQlConnect.OpenDbCon();
            String SearchQuery = ("select* from AppUsers where UsrLoginID like '" + TxtSearch + "%'");
            SqlCommand SQlCMD = new SqlCommand(SearchQuery, Dbconnection.GetDbConnection());
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SQlCMD);
            da.Fill(dt);
            da.Dispose();
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();
            return dt;

        }
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  Insert - Update - Delete Students

        public void InsertNewStudent(string TxtStdID, string TxtNationalID, string TxtPassportID, string CombCollage, string TxtPassportIssuePlace,
                                     string CombBloodType, ValueType PassportEndDate, string CombGender, string ChecBoxActive, string cmbnationality,
                                     string TxtStdFirstName, string TxtStdSecondName, string TxtStdLastName, string TxtMobile, string TxtEmail, ValueType StdDateBirth,
                                     string cmbTempType, string TxtStdDesc,string CmbPlaceOfBirth)
        {
            SQlConnect.OpenDbCon();
            string Query = ("INSERT INTO StudentsTable"
                            + " ( StudentID,STDNatID,STDPassportID,STDCollage,STDPassportIssuePlace,STDBloodGroup,STDPassportEndDate, "
                            + " STDGender, STDStatus,STDNationality,STDFirstName,STDSecondName,STDFamilyName, "
                            + " STDMobileNo,STDEmailID,STDBirthDate,STDTempid,STDDescription,PlaceOfBirth)"//, STDimage
                            + " values"
                            + " ( @StudentID, @STDNatID, @STDPassportID, @STDCollage, @STDPassportIssuePlace, @STDBloodGroup, @STDPassportEndDate, "
                            + " @STDGender, @STDStatus,@STDNationality,@STDFirstName,@STDSecondName,@STDFamilyName, "
                            + " @STDMobileNo,@STDEmailID,@STDBirthDate,@STDTempid,@STDDescription,@PlaceOfBirth)"); //,@STDimage 

            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@StudentID", TxtStdID);
            SQlCMD.Parameters.AddWithValue("@STDNatID", TxtNationalID);
            SQlCMD.Parameters.AddWithValue("@STDPassportID", TxtPassportID);
            SQlCMD.Parameters.AddWithValue("@STDCollage", CombCollage);
            SQlCMD.Parameters.AddWithValue("@STDPassportIssuePlace", TxtPassportIssuePlace);
            SQlCMD.Parameters.AddWithValue("@STDBloodGroup", CombBloodType);
            SQlCMD.Parameters.AddWithValue("@STDPassportEndDate", PassportEndDate);
            SQlCMD.Parameters.AddWithValue("@STDGender", CombGender);
            SQlCMD.Parameters.AddWithValue("@STDStatus", ChecBoxActive);
            SQlCMD.Parameters.AddWithValue("@STDNationality", cmbnationality);
            SQlCMD.Parameters.AddWithValue("@STDFirstName", TxtStdFirstName);
            SQlCMD.Parameters.AddWithValue("@STDSecondName", TxtStdSecondName);
            SQlCMD.Parameters.AddWithValue("@STDFamilyName", TxtStdLastName);
            SQlCMD.Parameters.AddWithValue("@STDMobileNo", TxtMobile);
            SQlCMD.Parameters.AddWithValue("@STDEmailID", TxtEmail);
            SQlCMD.Parameters.AddWithValue("@STDBirthDate", StdDateBirth);
            SQlCMD.Parameters.AddWithValue("@STDTempid", cmbTempType);
            SQlCMD.Parameters.AddWithValue("@STDDescription", TxtStdDesc);
            SQlCMD.Parameters.AddWithValue("@PlaceOfBirth", CmbPlaceOfBirth);
            //SQlCMD.Parameters.AddWithValue("@STDimage", General.imageToByteArray(stdimage));
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Insertstdcat(string TxtStdID, string cmbTempType)
        {
            SQlConnect.OpenDbCon();
            string Query = ("INSERT INTO StuEmpCatRelations (StudentID , STDTempid) Values (@StudentID,@STDTempid)");
            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@StudentID", TxtStdID);
            SQlCMD.Parameters.AddWithValue("@STDTempid", cmbTempType);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Updatestdcat(string TxtStdID, string cmbTempType)
        {
            SQlConnect.OpenDbCon();
            string UpdateQuery = ("UPDATE StuEmpCatRelations set "
                            + "  StudentID=@StudentID , STDTempid=@STDTempid WHERE StudentID=@StudentID");

            SqlCommand SQlCMD = new SqlCommand(UpdateQuery, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@StudentID", TxtStdID);
            SQlCMD.Parameters.AddWithValue("@STDTempid", cmbTempType);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UpdateStudentInfo(string TxtStdID, string TxtNationalID, string TxtPassportID, string CombCollage, String TxtPassportIssuePlace,
                                     string CombBloodType, ValueType PassportEndDate, string CombGender, string ChecBoxActive, string cmbnationality,
                                     string TxtStdFirstName, string TxtStdSecondName, string TxtStdLastName, string TxtMobile, string TxtEmail, ValueType StdDateBirth,
                                     string cmbTempType, string TxtStdDesc,string CmbPlaceOfBirth)//, Image stdimage
        {
            SQlConnect.OpenDbCon();
            string UpdateQuery = ("UPDATE StudentsTable set "
                            + "  StudentID=@StudentID,STDNatID=@STDNatID,STDPassportID=@STDPassportID,STDCollage=@STDCollage,STDPassportIssuePlace=@STDPassportIssuePlace ,"
                            + "  STDBloodGroup=@STDBloodGroup,STDPassportEndDate=@STDPassportEndDate,STDGender=@STDGender,STDStatus=@STDStatus,STDNationality=@STDNationality ,"
                            + "  STDFirstName=@STDFirstName,STDSecondName=@STDSecondName,STDFamilyName=@STDFamilyName,STDMobileNo=@STDMobileNo,STDEmailID=@STDEmailID , "
                            + "  STDBirthDate=@STDBirthDate,STDTempid=@STDTempid,STDDescription=@STDDescription,PlaceOfBirth=@PlaceOfBirth WHERE StudentID=@StudentID ");//,STDimage=@STDimage 

            SqlCommand SQlCMD = new SqlCommand(UpdateQuery, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@StudentID", TxtStdID);
            SQlCMD.Parameters.AddWithValue("@STDNatID", TxtNationalID);
            SQlCMD.Parameters.AddWithValue("@STDPassportID", TxtPassportID);
            SQlCMD.Parameters.AddWithValue("@STDCollage", CombCollage);
            SQlCMD.Parameters.AddWithValue("@STDPassportIssuePlace", TxtPassportIssuePlace);
            SQlCMD.Parameters.AddWithValue("@STDBloodGroup", CombBloodType);
            SQlCMD.Parameters.AddWithValue("@STDPassportEndDate", PassportEndDate);
            SQlCMD.Parameters.AddWithValue("@STDGender", CombGender);
            SQlCMD.Parameters.AddWithValue("@STDStatus", ChecBoxActive);
            SQlCMD.Parameters.AddWithValue("@STDNationality", cmbnationality);
            SQlCMD.Parameters.AddWithValue("@STDFirstName", TxtStdFirstName);
            SQlCMD.Parameters.AddWithValue("@STDSecondName", TxtStdSecondName);
            SQlCMD.Parameters.AddWithValue("@STDFamilyName", TxtStdLastName);
            SQlCMD.Parameters.AddWithValue("@STDMobileNo", TxtMobile);
            SQlCMD.Parameters.AddWithValue("@STDEmailID", TxtEmail);
            SQlCMD.Parameters.AddWithValue("@STDBirthDate", StdDateBirth);
            SQlCMD.Parameters.AddWithValue("@STDTempid", cmbTempType);
            SQlCMD.Parameters.AddWithValue("@STDDescription", TxtStdDesc);
            SQlCMD.Parameters.AddWithValue("@PlaceOfBirth", CmbPlaceOfBirth);
            //SQlCMD.Parameters.AddWithValue("@STDimage", General.imageToByteArray(stdimage));
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UpdateStudentInfoImage(string TxtStdID, Image stdimage)
        {
            SQlConnect.OpenDbCon();
            string UpdateQueryImage = ("UPDATE StudentsTable set StudentID=@StudentID, STDimage=@STDimage WHERE StudentID=@StudentID");
            SqlCommand SQlCMD = new SqlCommand(UpdateQueryImage, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@StudentID", TxtStdID);
            SQlCMD.Parameters.AddWithValue("@STDimage", General.imageToByteArray(stdimage));
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void ComparingDatabeforesaving(String TxtStdID, string TxtNationalID, string TxtPassportID, string CombCollage, string TxtPassportIssuePlace,
                                     string CombBloodType, string PassportEndDate, string CombGender, string ChecBoxActive, string cmbnationality,
                                     string TxtStdFirstName, string TxtStdSecondName, string TxtStdLastName, string TxtMobile, string TxtEmail, string StdDateBirth,
                                     string cmbTempType, string TxtStdDesc/*, Image stdimage*/)
        {
            SQlConnect.OpenDbCon();
            string Query = (" Select "
                            + " ( StudentID,STDNatID,STDPassportID,STDCollage,STDPassportIssuePlace,STDBloodGroup,STDPassportEndDate, "
                            + " STDGender, STDStatus,STDNationality,STDFirstName,STDSecondName,STDFamilyName, "
                            + " STDMobileNo,STDEmailID,STDBirthDate,STDTempid,STDDescription,STDimage from StudentsTable where STDNatID=@STDNatID)");
            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());
            TxtStdID = ("@StudentID");
            TxtNationalID = ("@STDNatID");
            TxtPassportID = ("@STDPassportID");
            CombCollage = ("@STDCollage");
            TxtPassportIssuePlace =( "@STDPassportIssuePlace");
            CombBloodType = ("@STDBloodGroup");
            PassportEndDate = ("@STDPassportEndDate");
            CombGender = ("@STDGender");
            ChecBoxActive =( "@STDStatus");
            cmbnationality = ("@STDNationality");
            TxtStdFirstName = ("@STDFirstName");
            TxtStdSecondName = ("@STDSecondName");
            TxtStdLastName = ("@STDFamilyName");
            TxtMobile = ("@STDMobileNo");
            TxtEmail = ("@STDEmailID");
            StdDateBirth = ("@STDBirthDate");
            cmbTempType = ("@STDTempid");
            TxtStdDesc = ("@STDDescription");
           // General.imageToByteArray(stdimage) = "@STDimage";
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void DeleteStudentsQuery(String TxtStudentID)
        {
            SQlConnect.OpenDbCon();
            string DeleteStudentQuery = ("DELETE From StudentsTable WHERE StudentID='" + TxtStudentID + "'");
            SqlCommand SQlCMD = new SqlCommand(DeleteStudentQuery, Dbconnection.GetDbConnection());
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*search */
        public DataTable LouadStudent_GridView(String TxtSearch)
        {
            SQlConnect.OpenDbCon();
            String SearchQuery = ("select* from StudentsTable where UsrLoginID like '" + TxtSearch + "%'");
            SqlCommand SQlCMD = new SqlCommand(SearchQuery, Dbconnection.GetDbConnection());
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SQlCMD);
            da.Fill(dt);
            da.Dispose();
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();
            return dt;

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn != null)
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
            else
            {
                byte[] array1 = new byte[1];
                return array1;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Add Update Delete Category
        /*Insert Category Metod*/
        public void InsertNewCategory(String TxtCatName, string TxtCatCode, string TxtCatDesc)
        {
            SQlConnect.OpenDbCon();
            string Query = ("INSERT INTO CatogryType(CatName,CatCode,CatDesc) values (@CatName,@CatCode,@CatDesc)");
            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@CatName", TxtCatName);
            SQlCMD.Parameters.AddWithValue("@CatCode", TxtCatCode);
            SQlCMD.Parameters.AddWithValue("@CatDesc", TxtCatDesc);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Update Category Metod*/
        public void UpdateCategory(String TxtCatName, string TxtCatCode, string TxtCatDesc)
        {
            SQlConnect.OpenDbCon();
            string UpdateQuery = ("UPDATE CatogryType set CatName=@CatName,CatCode=@CatCode,CatDesc=@CatDesc WHERE CatName = @CatName ");
            SqlCommand SQlCMD = new SqlCommand(UpdateQuery, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@CatName", TxtCatName);
            SQlCMD.Parameters.AddWithValue("@CatCode", TxtCatCode);
            SQlCMD.Parameters.AddWithValue("@CatDesc", TxtCatDesc);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Delete Category*/
        public void DeleteCategoryQuery(String TxtCatName)
        {
            SQlConnect.OpenDbCon();
            string DeleteUserQuery = ("DELETE From CatogryType WHERE CatName='" + TxtCatName + "'");
            SqlCommand SQlCMD = new SqlCommand(DeleteUserQuery, Dbconnection.GetDbConnection());
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Category Load
        public DataTable LouadCat_GridView(String TxtSearch)
        {
            SQlConnect.OpenDbCon();
            String SearchQuery = ("select * from CatogryType where CatName like '" + TxtSearch + "%'");
            SqlCommand SQlCMD = new SqlCommand(SearchQuery, Dbconnection.GetDbConnection());
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SQlCMD);
            da.Fill(dt);
            da.Dispose();
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();
            return dt;

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*using for fill combo box in the permition form*/
        public DataTable LouadCat()
        {
            SQlConnect.OpenDbCon();
            String SearchQuery = ("select * from CatogryType ");
            SqlCommand SQlCMD = new SqlCommand(SearchQuery, Dbconnection.GetDbConnection());
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SQlCMD);
            da.Fill(dt);
            da.Dispose();
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();
            return dt;

        }
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Permisions region

        public void InsertPermitions(String txtSearch, string strPermission)
        {
            SQlConnect.OpenDbCon();
            string Query = ("INSERT INTO RoleMaster(RoleName,RolePermissions) values(@RoleName,@RolePermissions)");
            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());   
            SQlCMD.Parameters.AddWithValue("@RoleName", txtSearch);
            SQlCMD.Parameters.AddWithValue("@RolePermissions", strPermission);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        public void UpdatePermitions(String txtSearch, string strPermission)
        {
            SQlConnect.OpenDbCon();
            string Query = ("UPDATE RoleMaster set RoleName = @RoleName ,RolePermissions=@RolePermissions where RoleName = @RoleName");
            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@RoleName", txtSearch);
            SQlCMD.Parameters.AddWithValue("@RolePermissions", strPermission);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void DeletePermitions(String combopermitions)
        {
            SQlConnect.OpenDbCon(); 
            string DeleteUserQuery = ("DELETE From RoleMaster WHERE  RoleName ='" + combopermitions + "'");
            SqlCommand SQlCMD = new SqlCommand(DeleteUserQuery, Dbconnection.GetDbConnection());
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void InsertMenuPermitions(String txtSearch, string strPermission)
        {
            SQlConnect.OpenDbCon();
            string Query = ("INSERT INTO UserMenuPermitions (userid,menuid) values (@userid,@menuid) ");
            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@userid", txtSearch);
            SQlCMD.Parameters.AddWithValue("@menuid", strPermission);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        public void UpdateMenuPermitions(String txtSearch, string strPermission)
        {
            SQlConnect.OpenDbCon();
            string Query = ("UPDATE UserMenuPermitions set menuid = @menuid ,userid=@userid where userid = @userid");
            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@userid", txtSearch);
            SQlCMD.Parameters.AddWithValue("@menuid", strPermission);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void DeleteMenuPermitions(String combopermitions)
        {
            SQlConnect.OpenDbCon();
            string DeleteUserQuery = ("UPDATE UserMenuPermitions set isdeleted = 'True' where userid = @userid");
            SqlCommand SQlCMD = new SqlCommand(DeleteUserQuery, Dbconnection.GetDbConnection());
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Add Update  Issue Cards

        public void IssueNewCards(string TxtIsStudentID, int CmbIsIssueType,ValueType DateTimeISStart ,ValueType DateTimeISEnd,string TxtIsDesc,int CmbIsStatus ,int CmbIsTemptype) /*,string isprinted*/
        {
            SQlConnect.OpenDbCon();
            string Query = ("INSERT INTO CardMaster(IsID,StartDate,ExpiryDate,CardStatus,isPrinted,TmpID,Description,StudentID)"
                                       + " values (@IsID,@StartDate,@ExpiryDate,@CardStatus,@isPrinted,@TmpID,@Description,@StudentID)");
            SqlCommand SQlCMD = new SqlCommand(Query, Dbconnection.GetDbConnection());
           // SQlCMD.Parameters.AddWithValue("@CardID", TxtCatName);
            SQlCMD.Parameters.AddWithValue("@IsID", CmbIsIssueType);
            SQlCMD.Parameters.AddWithValue("@StartDate", DateTimeISStart);
            SQlCMD.Parameters.AddWithValue("@ExpiryDate", DateTimeISEnd);
            SQlCMD.Parameters.AddWithValue("@CardStatus", CmbIsStatus);
            SQlCMD.Parameters.AddWithValue("@isPrinted", 0);
            SQlCMD.Parameters.AddWithValue("@TmpID", CmbIsTemptype);
            SQlCMD.Parameters.AddWithValue("@Description", TxtIsDesc);
            SQlCMD.Parameters.AddWithValue("@StudentID", TxtIsStudentID);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*Update Category Metod*/
        public void UpdateCards(String TxtCatName, string TxtCatCode, string TxtCatDesc)
        {
            SQlConnect.OpenDbCon();
            string UpdateQuery = ("UPDATE CatogryType set CatName=@CatName,CatCode=@CatCode,CatDesc=@CatDesc WHERE CatName = @CatName ");
            SqlCommand SQlCMD = new SqlCommand(UpdateQuery, Dbconnection.GetDbConnection());
            SQlCMD.Parameters.AddWithValue("@CatName", TxtCatName);
            SQlCMD.Parameters.AddWithValue("@CatCode", TxtCatCode);
            SQlCMD.Parameters.AddWithValue("@CatDesc", TxtCatDesc);
            SQlCMD.ExecuteNonQuery();
            SQlConnect.CloseDbcon();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}