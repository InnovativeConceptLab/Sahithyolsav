using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ConnectionLib
{
    public class Division
    {
        public static DataTable getAllDivisions(int intDisID = 0)
        {
            DataSet ds = new DataSet();
            String Query;
            if (intDisID == 0)
            {
                Query = "SELECT [intDivisionId],dis.[intDistrictId],[vchDivisionName],[vchDivisionCode],dis.vchDistrictName FROM [tbl_Division] div inner join tbl_District dis on div.intDistrictId=dis.intDistrictID ";
            }
            else
            {
                Query = "SELECT [intDivisionId],dis.[intDistrictId],[vchDivisionName],[vchDivisionCode],dis.vchDistrictName FROM [tbl_Division] div inner join tbl_District dis on div.intDistrictId=dis.intDistrictID where div.intDistrictId=" + intDisID;
            }
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
        public static bool SaveDivision(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[4];

            p[0] = new SqlParameter("@intDivisionId", Param[0]);
            p[1] = new SqlParameter("@intDistrictId", Param[1]);
            p[2] = new SqlParameter("@vchDivisionName", Param[2]);
            p[3] = new SqlParameter("@vchDivisionCode", Param[3]);
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveDivision", p) == 1)
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
        public static DataTable GetDivision(int DistID = 0, string DivsnName = "", string DivsCode = "", int ID = 0)
        {
            DataSet ds = new DataSet();
            String Query;
            if (DistID == 0)
            {
                Query = "SELECT row_number() over (order by [intDivisionId]) as slno,[intDivisionId],dis.[intDistrictId],[vchDivisionName],[vchDivisionCode],dis.vchDistrictName FROM [tbl_Division] div inner join tbl_District dis on div.intDistrictId=dis.intDistrictID ";
            }
            else
            {
                Query = "SELECT row_number() over (order by [intDivisionId]) as slno,[intDivisionId],dis.[intDistrictId],[vchDivisionName],[vchDivisionCode],dis.vchDistrictName FROM [tbl_Division] div inner join tbl_District dis on div.intDistrictId=dis.intDistrictID where div.[intDistrictId]=" + DistID;
            }

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
        public static bool DeleteDivision(int intDivsnId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intUserId", intDivsnId);

            Query = "DELETE FROM tbl_Division WHERE intDivisionId=" + intDivsnId;

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
    }
}
