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
    public class Reports
    {
        public static DataTable GetReportDetails()
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            Query = "SELECT [intReportId],[vchReportName],[vchSPName],[intReportType] FROM [tbl_Report]";

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

        public static DataTable  GetReportById(int ReportID)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@ReportID", ReportID);
            Query = "Select [intReportId],[vchReportName],[vchSPName],[intReportType] FROM [tbl_Report] Where intReportId=@ReportID";

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p);
                return  ds.Tables[0];
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
        public static string GetReportName(int ReportID)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@ReportID", ReportID);
            Query = "Select vchSPName  FROM [tbl_Report] Where intReportId=@ReportID";

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }

            finally
            {
                ds.Dispose();
                Query = "";
            }
        }
        public static DataTable ExecuteReportQuery(ArrayList Param)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[5];
            String SP;

            SP = GetReportName(int.Parse(Param[0].ToString()));

            p[0] = new SqlParameter("@param1", Param[1].ToString());//sectionId
            p[1] = new SqlParameter("@param2", Param[2].ToString());//ItemId
            p[2] = new SqlParameter("@param3", Param[3].ToString());//ParticipantToLevelId
            p[3] = new SqlParameter("@param4", Param[4].ToString());//ParticipantToLevelId
            p[4] = new SqlParameter("@param5", Param[5].ToString());//ParticipantToLevelId

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, SP, p);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

            finally
            {
                ds.Dispose();
                SP = "";
            }
        }
    }
}
