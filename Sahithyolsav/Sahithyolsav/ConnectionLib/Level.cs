using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ConnectionLib
{
    public class Level
    {
        public static DataTable getAllLevel()
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT [intLevelID],[vchLevelName] FROM [tbl_Level]";
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
        public static DataTable getAllLevelforSerMapID(int userMapId)
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT [intLevelID],[vchLevelName] FROM [tbl_Level] Where intLevelID in("+userMapId  +","+ (userMapId-1).ToString() +")";
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
    }
}
