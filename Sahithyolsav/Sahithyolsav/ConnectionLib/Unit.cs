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
    public class Unit
    {
        public static bool SaveUnit(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[6];

            p[0] = new SqlParameter("@intUnitId", Param[0]);
            p[1] = new SqlParameter("@intSectorId", Param[1]);
            p[2] = new SqlParameter("@intDivisionId", Param[2]);
            p[3] = new SqlParameter("@intDistrictId", Param[3]);
            p[4] = new SqlParameter("@vchUnitName", Param[4]);
            p[5] = new SqlParameter("@vchUnitcode", Param[5]);

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveUnit", p) == 1)
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
        public static DataTable getAllUnit()
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT [intUnitId],tbl_Sector.[intSectorId],tbl_Division.[intDivisionId],tbl_District.[intDistrictId],[vchUnitName],[vchUnitcode],"
                + " tbl_District.vchDistrictName,tbl_Division.vchDivisionName,tbl_Sector.vchSectorName FROM [tbl_Unit]"
                + " INNER JOIN tbl_District ON tbl_District.intDistrictID=tbl_Unit.intDistrictId"
                + " INNER JOIN tbl_Division ON tbl_Division.intDivisionId=tbl_Unit.intDivisionId"
                + " INNER JOIN tbl_Sector ON tbl_Sector.intSectorId=tbl_Unit.intSectorId";
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
        public static DataTable getAllUnitById(int intUnitId)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@intUnitId", intUnitId);

            Query = "SELECT [intUnitId],tbl_Sector.[intSectorId],tbl_Division.[intDivisionId],tbl_District.[intDistrictId],[vchUnitName],[vchUnitcode]"
                         + " FROM [tbl_Unit]"
                         + " INNER JOIN tbl_District ON tbl_District.intDistrictID=tbl_Unit.intDistrictId"
                         + " INNER JOIN tbl_Division ON tbl_Division.intDivisionId=tbl_Unit.intDivisionId"
                         + " INNER JOIN tbl_Sector ON tbl_Sector.intSectorId=tbl_Unit.intSectorId"
                         + " WHERE tbl_Unit.intUnitId=@intUnitId";
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

        public static DataTable getAllUnitBySearchId(String unitName, int districtID, int divisionID, int sectorId)
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT [intUnitId],tbl_Sector.[intSectorId],tbl_Division.[intDivisionId],tbl_District.[intDistrictId],[vchUnitName],[vchUnitcode],"
                           + " tbl_District.vchDistrictName,tbl_Division.vchDivisionName,tbl_Sector.vchSectorName FROM [tbl_Unit]"
                           + " INNER JOIN tbl_District ON tbl_District.intDistrictID=tbl_Unit.intDistrictId"
                           + " INNER JOIN tbl_Division ON tbl_Division.intDivisionId=tbl_Unit.intDivisionId"
                           + " INNER JOIN tbl_Sector ON tbl_Sector.intSectorId=tbl_Unit.intSectorId"
                           + " WHERE tbl_Unit.vchUnitName LIKE'%%'";

            if (unitName != "")
            {
                Query = Query + " AND vchUnitName LIKE '%" + unitName + "%'";
            }
            if (districtID != 0)
            {
                Query = Query + " AND tbl_District.intDistrictID=" + districtID;
            }
            if (divisionID != 0)
            {
                Query = Query + " AND tbl_Division.intDivisionId=" + divisionID;
            }
            if (sectorId != 0)
            {
                Query = Query + " AND tbl_Sector.intSectorId=" + sectorId;
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
        public static bool Deleteitem(int intUnitId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intUnitId", intUnitId);

            Query = "DELETE FROM [tbl_Unit] WHERE tbl_Unit.intUnitId=@intUnitId";

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
