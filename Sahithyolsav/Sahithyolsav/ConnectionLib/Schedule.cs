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
    public class Schedules
    {
        public static bool SaveStage(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter("@intStageId", Param[0]);
            p[1] = new SqlParameter("@VchStageName", Param[1]);
            p[2] = new SqlParameter("@vchPlace", Param[2]);

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveStage", p) == 1)
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
        public static DataTable getStages()
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT * FROM tbl_Stage";
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
        public static bool DeleteStage(int intStageID)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intStageID", intStageID);

            Query = "DELETE FROM tbl_Stage WHERE intStageId=" + intStageID;

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
        public static bool SaveSchedule(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[7];

            p[0] = new SqlParameter("@intShceduleID", Param[0]);
            p[1] = new SqlParameter("@intSectionId", Param[1]);
            p[2] = new SqlParameter("@intItemId", Param[2]);
            p[3] = new SqlParameter("@intStageId", Param[3]);
            p[4] = new SqlParameter("@dtDate", Param[4]);
            p[5] = new SqlParameter("@vchTime", Param[5]);
            p[6] = new SqlParameter("@IsAMPM", Param[6]);
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveSchedule", p) == 1)
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
