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
    public class Sector
    {
        public static bool SaveSector(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[5];

            p[0] = new SqlParameter("@intSectorId", Param[0]);
            p[1] = new SqlParameter("@intDivisionId", Param[1]);
            p[2] = new SqlParameter("@intDistrictId", Param[2]);
            p[3] = new SqlParameter("@vchSectionName", Param[3]);
            p[4] = new SqlParameter("@vchSectionCode", Param[4]);


            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveSector", p) == 1)
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
        public static DataTable getAllSector(int intDivID = 0)
        {
            DataSet ds = new DataSet();
            String Query;
            if (intDivID == 0)
            {
                Query = "SELECT [intSectorId],tbl_Division.[intDivisionId],tbl_District.[intDistrictId],[vchSectorName],[vchSectionCode],"
                        + " vchDistrictName,vchDivisionName FROM [dbo].[tbl_Sector]"
                       + " INNER JOIN tbl_District ON tbl_District.intDistrictID=tbl_Sector.intDistrictId"
                        + " INNER JOIN tbl_Division ON tbl_Division.intDivisionId=tbl_Sector.intDivisionId";
            }
            else
            {
                Query = "SELECT [intSectorId],tbl_Division.[intDivisionId],tbl_District.[intDistrictId],[vchSectorName],[vchSectionCode],"
                       + " vchDistrictName,vchDivisionName FROM [dbo].[tbl_Sector]"
                      + " INNER JOIN tbl_District ON tbl_District.intDistrictID=tbl_Sector.intDistrictId"
                       + " INNER JOIN tbl_Division ON tbl_Division.intDivisionId=tbl_Sector.intDivisionId where tbl_Sector.intDivisionId=" + intDivID;
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
        public static DataTable getAllSectorById(int intSectorID)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@intSectorID", intSectorID);

            Query = "SELECT [intSectorId],tbl_Division.[intDivisionId],tbl_District.[intDistrictId],[vchSectorName],[vchSectionCode],"
                    + " vchDistrictName,vchDivisionName FROM [dbo].[tbl_Sector]"
                    + " INNER JOIN tbl_District ON tbl_District.intDistrictID=tbl_Sector.intDistrictId"
                    + " INNER JOIN tbl_Division ON tbl_Division.intDivisionId=tbl_Sector.intDivisionId"
                    + " Where intSectorId=@intSectorID";
            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p);
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
        public static DataTable getAllSectorBySearchId(String sectorName, int districtID, int divisionID)
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT [intSectorId],tbl_Division.[intDivisionId],tbl_District.[intDistrictId],[vchSectorName],[vchSectionCode],"
                    + " vchDistrictName,vchDivisionName FROM [dbo].[tbl_Sector]"
                    + " INNER JOIN tbl_District ON tbl_District.intDistrictID=tbl_Sector.intDistrictId"
                    + " INNER JOIN tbl_Division ON tbl_Division.intDivisionId=tbl_Sector.intDivisionId"
                    + " WHERE vchSectorName LIKE '%%'";
            if (sectorName != "")
            {
                Query = Query + " AND vchSectorName LIKE '%" + sectorName + "%'";
            }
            if (districtID != 0)
            {
                Query = Query + " AND tbl_District.intDistrictID=" + districtID;
            }
            if (divisionID != 0)
            {
                Query = Query + " AND tbl_Division.intDivisionId=" + divisionID;
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
        public static bool Deletesector(int intSectorId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intSectorId", intSectorId);

            Query = "DELETE FROM [tbl_Sector] WHERE intSectorId=@intSectorId";

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
