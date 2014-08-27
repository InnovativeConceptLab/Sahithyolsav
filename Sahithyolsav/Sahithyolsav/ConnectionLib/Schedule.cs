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
            SqlParameter[] p = new SqlParameter[8];

            p[0] = new SqlParameter("@intShceduleID", Param[0]);
            p[1] = new SqlParameter("@intSectionId", Param[1]);
            p[2] = new SqlParameter("@intItemId", Param[2]);
            p[3] = new SqlParameter("@intStageId", Param[3]);
            p[4] = new SqlParameter("@dtDate", Param[4]);
            p[5] = new SqlParameter("@vchTime", Param[5]);
            p[6] = new SqlParameter("@vchTime1", Param[6]);
            p[7] = new SqlParameter("@IsAMPM", Param[7]);
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
        public static DataTable getSchedules()
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT tbl_Schedule.intShceduleID,tbl_Section.vchSectionName,vchItemName,VchStageName,"
                    + " CONVERT(VARCHAR(10),dtDate,106)'Date',vchTime +':' + vchTime1 +' ' +IsAMPM 'Time'"
                    +" FROM tbl_Schedule"
                    +" INNER JOIN tbl_Item ON tbl_Item.intItemId=tbl_Schedule.intItemId"
                    +" INNER JOIN tbl_Section on tbl_Section.intSectionID=tbl_Schedule.intSectionID"
                    +" INNER JOIN tbl_Stage ON tbl_Stage.intStageId=tbl_Schedule.intStageId";
            
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
        public static DataTable getItemOfCurrrentSchedules(int stageId,String time1,String time2,String ampm)
        {
            DataSet ds = new DataSet();
            String Query;

            SqlParameter[] p = new SqlParameter[4];

            p[0] = new SqlParameter("@stageId", stageId);
            p[1] = new SqlParameter("@time1", time1);
            p[2] = new SqlParameter("@time2", time2);
            p[3] = new SqlParameter("@ampm", ampm);

            Query = "SELECT intItemId FROM tbl_Schedule "
                   + " WHERE intStageId=@stageId and vchTime=@time1 and vchTime1=@time2 AND IsAMPM=@ampm";

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
        public static bool CheckSceduleVallidation(int itemid1, int itemid2,int tolevelId)
        {
            DataSet ds = new DataSet();
            String Query;

            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter("@itemid1", itemid1);
            p[1] = new SqlParameter("@itemid2", itemid2);
            p[2] = new SqlParameter("@tolevelId", tolevelId);

            Query = "SELECT * FROM tbl_ParticipantList"
                + " INNER JOIN tbl_ItemList on tbl_ParticipantList.intParticipantListId=tbl_ItemList.intParticipantListId"
                + " WHERE intItemId in(@itemid1,@itemid2) and intParticipantToLevelId=@tolevelId AND vchStatus='Yes'";

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query,p);
                if (ds.Tables[0].Rows.Count > 1)
                    return false;
                else
                    return true; 
            }
            catch
            {
                return false ;
            }

            finally
            {
                ds.Dispose();
                Query = "";
            }
        }
    }
}
