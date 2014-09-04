using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;

namespace ConnectionLib
{
    public class UserManagement
    {
        #region Login variable
        private const string SessionUserID = "sUserID";
        private const string SessionUserName = "sUserName";
        private const string SessionUserType = "sUserType";
        private const string SessionUserTypeMapID = "sUserTypeId";
        private const string SessionUserLogin = "sUserlogin";
        private const string SessionUserHMapID = "sUserHMapID";
        private static int _userFlag;
        #endregion
        #region get and set

        public static bool Userlogin
        {
            get
            {
                if (HttpContext.Current.Session[SessionUserLogin] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                HttpContext.Current.Session[SessionUserLogin] = value;
            }
        }
        public static int UserID
        {
            get
            {
                if (HttpContext.Current.Session[SessionUserID] != null)
                {
                    return (int)HttpContext.Current.Session[SessionUserID];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session[SessionUserID] = value;
            }
        }
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session[SessionUserName] != null)
                {
                    return (string)HttpContext.Current.Session[SessionUserName];
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session[SessionUserName] = value;
            }
        }
        public static int UserTypeId
        {
            get
            {
                if (HttpContext.Current.Session[SessionUserType] != null)
                {
                    return (int)HttpContext.Current.Session[SessionUserType];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session[SessionUserType] = value;
            }
        }
        public static int UserMapId
        {
            get
            {
                if (HttpContext.Current.Session[SessionUserTypeMapID] != null)
                {
                    return (int)HttpContext.Current.Session[SessionUserTypeMapID];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session[SessionUserTypeMapID] = value;
            }
        }
        public static int UserHigherMapId
        {
            get
            {
                if (HttpContext.Current.Session[SessionUserHMapID] != null)
                {
                    return (int)HttpContext.Current.Session[SessionUserHMapID];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session[SessionUserHMapID] = value;
            }
        }
        #endregion
        #region Methods

        public static bool ValidateUser(String UName, String Password)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[2];
            String Query;

            p[0] = new SqlParameter("@UserName", UName);
            p[1] = new SqlParameter("@Password", Password);

            Query = "SELECT [intUserId],[vchUserName],[vcbPassword],[vchFName],[vchLName],[intUserTypeId],[IsActive],ISNULL([intLevelMapId],0),ISNULL(intHeigherMapId,0) FROM [tbl_User] WHERE vchUserName=@UserName AND vcbPassword=@Password AND IsActive=1";
            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UserID = (int)ds.Tables[0].Rows[0].ItemArray[0];
                    UserName = (string)ds.Tables[0].Rows[0].ItemArray[3];
                    UserTypeId = (int)ds.Tables[0].Rows[0].ItemArray[5];
                    UserMapId = (int)ds.Tables[0].Rows[0].ItemArray[7];
                    UserHigherMapId = (int)ds.Tables[0].Rows[0].ItemArray[8];
                    Userlogin = true;
                    return true;
                }
                else
                {
                    Userlogin = false;
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                ds.Dispose();
            } 
        }
        public static DataTable getAllUsers()
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT [intUserId],[vchUserName],[vchFName],[vchLName],[intUserTypeId],case [IsActive] when 1 then 'Active' when 0 then 'InActive' end Status FROM [tbl_User]";
                    
            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

            finally
            {
                ds.Dispose();
                Query = "";
            }
        }
        public static DataTable getMyUsers(int userID)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("@userID", userID);
            Query = "SELECT [intUserId],[vchUserName],[vchFName],[vchLName],[intUserTypeId],case [IsActive] when 1 then 'Active' when 0 then 'InActive' end Status FROM [tbl_User]"
                    + " Where intCreatedByUserID=@userID";

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query,p);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

            finally
            {
                ds.Dispose();
                Query = "";
            }
        }
        public static bool UpdateUserStatus(int intUsrID,string Status)
        {
            if (Status == "Active")
            { 
                Status="0";
            }
            else if (Status == "InActive")
            {
                Status = "1";
            }
            string Query = "update tbl_User set IsActive=" + Status + "where intUserId=" + intUsrID;
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query) == 1)
                {
                   
                    return true;
                }
                else
                {
                 
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool SaveUser(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[10];

            p[0] = new SqlParameter("@intUserId", Param[0]);
            p[1] = new SqlParameter("@vchUserName", Param[1]);
            p[2] = new SqlParameter("@vcbPassword", Param[2]);
            p[3] = new SqlParameter("@vchFName", Param[3]);
            p[4] = new SqlParameter("@vchLName", Param[4]);
            p[5] = new SqlParameter("@intUserTypeId", Param[5]);
            p[6] = new SqlParameter("@IsActive", Param[6]);
            p[7] = new SqlParameter("@intLevelMapId", Param[7]);
            p[8] = new SqlParameter("@intHeigherMapId", Param[8]);
            p[9] = new SqlParameter("@intCreatedByUserID", Param[9]);
            
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveUsers", p) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool MapUser (ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[5];

            p[0] = new SqlParameter("@intUserMapId", Param[0]);
            p[1] = new SqlParameter("@intUserID", Param[1]);
            p[2] = new SqlParameter("@intLevelId", Param[2]);
            p[3] = new SqlParameter("@intLevelMapId", Param[3]);
            p[4] = new SqlParameter("@isActive", Param[4]);
           

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spMapUsers", p) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool validateUserName(string userName)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("@userName", userName);
            Query = "SELECT vchUserName FROM  [tbl_User] WHERE vchUserName =@userName";

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query,p);
                if (ds.Tables[0].Rows.Count > 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }

            finally
            {
                ds.Dispose();
                Query = "";
            }
        }
        public static bool DeleteUser(int intUserId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intUserId", intUserId);

            Query = "DELETE FROM tbl_User WHERE intUserId=@intUserId";

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdatePassword(int intUsrID, string NewPwd)
        {

            string Query = "update tbl_User set vcbPassword='" + NewPwd + "' where intUserId=" + intUsrID;
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query) == 1)
                {

                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
