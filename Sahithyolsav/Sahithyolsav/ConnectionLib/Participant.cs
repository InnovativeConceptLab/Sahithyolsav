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
    public class Participant
    {
        public static bool SaveParticipant(ArrayList Param, out int ParticipantId)
        {
            SqlParameter[] p = new SqlParameter[9];

            p[0] = new SqlParameter("@intParticipantId", Param[0]);
            p[1] = new SqlParameter("@vchPartcipantName", Param[1]);
            p[2] = new SqlParameter("@intUnitId", Param[2]);
            p[3] = new SqlParameter("@isActive", Param[3]);
            p[4] = new SqlParameter("@vchImagePath", Param[4]);
            p[5] = new SqlParameter("@intAge", Param[5]);
            p[6] = new SqlParameter("@vchCampusName", Param[6]);
            p[7] = new SqlParameter("@vchCourse", Param[7]);
            p[8] = new SqlParameter("@ParticipantId", SqlDbType.Int, 4);
            p[8].Direction = ParameterDirection.Output;
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveParticipant", p) == 1)
                {
                    ParticipantId = Convert.ToInt32(p[8].Value);
                    return true;
                }
                else
                {
                    ParticipantId = 0;
                    return false;
                }
            }
            catch
            {
                ParticipantId = 0;
                return false;
            }
        }
        public static bool SaveParticipantList(ArrayList Param, out int ParticipantListId)
        {
            SqlParameter[] p = new SqlParameter[15];

            p[0] = new SqlParameter("@intParticipantListId", Param[0]);
            p[1] = new SqlParameter("@intParticipantId", Param[1]);
            p[2] = new SqlParameter("@vchChessNo", Param[2]);
            p[3] = new SqlParameter("@intSectionId", Param[3]);
            p[4] = new SqlParameter("@intItemListID", Param[4]);
            p[5] = new SqlParameter("@dtDateAdded", Param[5]);
            p[6] = new SqlParameter("@intadedUserId", Param[6]);
            p[7] = new SqlParameter("@intProgramLevelId", Param[7]);
            p[8] = new SqlParameter("@intParticipantLevelId", Param[8]);
            p[9] = new SqlParameter("@IsActive", Param[9]);
            p[10] = new SqlParameter("@intParticipantToLevelId", Param[10]);
            p[11] = new SqlParameter("@intParticipantLevelTypeID", Param[11]);
            p[12] = new SqlParameter("@isGroupPaticipant", Param[12]);
            p[13] = new SqlParameter("@intNumberOfPaticiapnt", Param[13]);
            p[14] = new SqlParameter("@OutParticipantListId", SqlDbType.Int, 4);
            p[14].Direction = ParameterDirection.Output;
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveParticipantList", p) == 1)
                {
                    ParticipantListId = Convert.ToInt32(p[14].Value);
                    return true;
                }
                else
                {
                    ParticipantListId = 0;
                    return false;
                }
            }
            catch
            {
                ParticipantListId = 0;
                return false;
            }
        }
        public static bool SaveParticipantGroupList(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter("@intGrpParticipant", Param[0]);
            p[1] = new SqlParameter("@intParticipantListId", Param[1]);
            p[2] = new SqlParameter("@vchGroupParticipantName", Param[2]);
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveGroupParticiapnt", p) == 1)
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
        public static bool SaveItemList(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[4];

            p[0] = new SqlParameter("@intItemListId", Param[0]);
            p[1] = new SqlParameter("@intParticipantListId", Param[1]);
            p[2] = new SqlParameter("@intItemId", Param[2]);
            p[3] = new SqlParameter("@vchStatus", Param[3]);

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSavveItemList", p) == 1)
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

        public static DataTable getParticipantByLevelId(int LevelId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@LevelId", LevelId);
            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "SpGetParticipantByLevelId", p);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

            finally
            {
                ds.Dispose();
            }
        }
        public static DataTable getParticipantByLevelId(int LevelId, int participantLeveleId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@LevelId", LevelId);
            p[1] = new SqlParameter("@intParticipantLevelId", participantLeveleId);

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "SpGetParticipantByLevelId1", p);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

            finally
            {
                ds.Dispose();
            }
        }
        public static DataTable getParticipantByLevelId(int LevelId, int participantLeveleId, int addedUserId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@LevelId", LevelId);
            p[1] = new SqlParameter("@intParticipantLevelId", participantLeveleId);
            p[2] = new SqlParameter("@addedUserId", addedUserId);

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "SpGetParticipantByLevelId2", p);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

            finally
            {
                ds.Dispose();
            }
        }
        public static DataTable getParticipantByHeigherLevelId(int LevelId, int participantLeveleId)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@LevelId", LevelId);
            p[1] = new SqlParameter("@intParticipantLevelId", participantLeveleId);

            Query = "SELECT intParticipantListId,tbl_Participant.vchPartcipantName,tbl_ParticipantList.intProgramLevelId,"
                    + " tbl_ParticipantList.intParticipantLevelId,tbl_ParticipantList.vchChessNo"
                    + " FROM tbl_ParticipantList"
                    + " INNER JOIN tbl_Participant ON tbl_ParticipantList.intParticipantId=tbl_Participant.intParticipantId"
                    + " INNER JOIN tbl_Section ON tbl_Section.intSectionID= tbl_ParticipantList.intSectionId"
                    + " WHERE intProgramLevelId=@LevelId AND  intParticipantToLevelId=@intParticipantLevelId";
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
        public static DataTable getParticipantListById(int participantListd)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@participantListd", participantListd);
            String Query;
            Query = "SELECT [intParticipantListId],tbl_ParticipantList.[intParticipantId],[vchChessNo],[intSectionId],[intItemListID],[dtDateAdded] "
                    + " ,[intadedUserId],[intProgramLevelId],[intParticipantLevelId],tbl_ParticipantList.[IsActive],[intParticipantToLevelId]"
                    + " ,[intParticipantLevelTypeID],vchPartcipantName,tbl_Participant.intParticipantId,ISNULL(tbl_ParticipantList.isGroupPaticipant,0) FROM [tbl_ParticipantList] "
                    + " INNER JOIN tbl_Participant on tbl_Participant.intParticipantId=tbl_ParticipantList.intParticipantId"
                    + " WHERE intParticipantListId=@participantListd";
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
            }
        }
        public static DataTable getItemListByParticipantId(int participantListd)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@participantListd", participantListd);
            String Query;
            Query = "SELECT [intItemListId],[intParticipantListId],[intItemId],ISNULL([vchStatus],'No')  FROM [tbl_ItemList] WHERE intParticipantListId=@participantListd";
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
            }
        }
        public static bool SaveCodeLetterMap(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[5];

            p[0] = new SqlParameter("@intCodeLeterMapId", Param[0]);
            p[1] = new SqlParameter("@intParticipatListId", Param[1]);
            p[2] = new SqlParameter("@vchCodeLetter", Param[2]);
            p[3] = new SqlParameter("@dtEventDate", Param[3]);
            p[4] = new SqlParameter("@intItemId", Param[4]);

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveCodeLeterMap", p) == 1)
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
        public static bool SaveTabulation(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[9];

            p[0] = new SqlParameter("@intTabulationId", Param[0]);
            p[1] = new SqlParameter("@intParticipatListId", Param[1]);
            p[2] = new SqlParameter("@intCodeLetterID", Param[2]);
            p[3] = new SqlParameter("@numMarks", Param[3]);
            p[4] = new SqlParameter("@dtEventDate", Param[4]);
            p[5] = new SqlParameter("@intItemId", Param[5]);
            p[6] = new SqlParameter("@intPointToParticipant", Param[6]);
            p[7] = new SqlParameter("@intPointToTeam", Param[7]);
            p[8] = new SqlParameter("@vchGrade", Param[8]);
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveTabulation", p) == 1)
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
        public static DataTable getParticipantItemBySectionandItemId(int SectionId, int intItemId, int toLvelId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter("@SectionId", SectionId);
            p[1] = new SqlParameter("@intItemId", intItemId);
            p[2] = new SqlParameter("@intParticipantToLevelId", toLvelId);

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spGetParticipantItemBySectionandItemId", p);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

            finally
            {
                ds.Dispose();
            }
        }
        public static DataTable GetProgramChartByItem(int SectionId, int intItemId, int toLvelId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter("@SectionId", SectionId);
            p[1] = new SqlParameter("@intItemId", intItemId);
            p[2] = new SqlParameter("@intParticipantToLevelId", toLvelId);

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spGetProgramChartByItem", p);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

            finally
            {
                ds.Dispose();
            }
        }
        public static DataTable getParticipantGroupByID(int intParticipantListId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[1];
            String Query;
            p[0] = new SqlParameter("@intParticipantListId", intParticipantListId);

            Query = "SELECT * FROM tbl_ParticipantGroupList WHERE intParticipantListId=@intParticipantListId";
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
            }
        }
        public static DataTable GetParticipantBySectionId(int SectionId, int toLvelId, int intParticipantLevelId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter("@SectionId", SectionId);
            p[1] = new SqlParameter("@intParticipantToLevelId", toLvelId);
            p[2] = new SqlParameter("@intParticipantLevelId", intParticipantLevelId);

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spGetParticipantBySectionId", p);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }

            finally
            {
                ds.Dispose();
            }
        }
        public static bool DeleteParticipant(int participantID)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@participantID", participantID);

            Query = "DELETE FROM tbl_ParticipantList WHERE intParticipantListId=@participantID"
                    + " DELETE FROM tbl_ItemList WHERE intParticipantListId=@participantID";

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p) > 0)
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
        public static bool DeleteParticipantGroup(int intParticipantListId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intParticipantListId", intParticipantListId);

            Query = "DELETE FROM [tbl_ParticipantGroupList] WHERE intParticipantListId = @intParticipantListId";

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p) > 0)
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
        public static bool ResetItemList(int intParticipantListId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intParticipantListId", intParticipantListId);

            Query = "DELETE FROM tbl_ItemList WHERE  [intParticipantListId]=@intParticipantListId";

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p) > 0)
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
        public static bool UpdateChessNumber(int intParticipantListId, String chessNumber)
        {
            SqlParameter[] p = new SqlParameter[2];
            String Query;

            p[0] = new SqlParameter("@intParticipantListId", intParticipantListId);
            p[1] = new SqlParameter("@chessNumber", chessNumber);

            Query = "UPDATE tbl_ParticipantList SET vchChessNo=@chessNumber WHERE intParticipantListId=@intParticipantListId";

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
        public static bool ValidateItem(int ParticipantLevelId, int ItemId, int participantID)
        {
            SqlParameter[] p = new SqlParameter[3];
            String Query;
            DataSet ds = new DataSet();
            p[0] = new SqlParameter("@ParticipantLevelId", ParticipantLevelId);
            p[1] = new SqlParameter("@ItemId", ItemId);
            p[2] = new SqlParameter("@participantID", participantID);
            if (participantID != 0)
            {
                Query = "SELECT * FROM tbl_ParticipantList "
                        + " INNER JOIN tbl_ItemList on tbl_ItemList.intParticipantListId=tbl_ParticipantList.intParticipantListId"
                        + "  WHERE intParticipantLevelId=@ParticipantLevelId AND tbl_ItemList.intItemId=@ItemId and vchStatus='Yes' AND intParticipantId <> @participantID";
            }
            else
            {
                Query = "SELECT * FROM tbl_ParticipantList "
                       + " INNER JOIN tbl_ItemList on tbl_ItemList.intParticipantListId=tbl_ParticipantList.intParticipantListId"
                       + "  WHERE intParticipantLevelId=@ParticipantLevelId AND tbl_ItemList.intItemId=@ItemId and vchStatus='Yes'";
            }

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p);
                if (ds.Tables[0].Rows.Count > 0)
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
        public static bool GenerateChessNumbers(int ParticipantToLevelId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query,updQuery;
            int chessNumber = 0, resetChessNumber, setLevelId = 0;
            DataSet ds = new DataSet();
            p[0] = new SqlParameter("@ParticipantToLevelId", ParticipantToLevelId);
            
            Query = "SELECT intParticipantListId,intParticipantLevelId,intParticipantToLevelId,vchChessNo FROM tbl_ParticipantList"
                     + " WHERE intParticipantToLevelId=@ParticipantToLevelId"
                     + " ORDER BY  intParticipantLevelId";


            try
            {
                resetChessNumber = 100;
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        chessNumber = 100;
                        resetChessNumber = 100;
                        setLevelId = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1].ToString());
                    }
                    else
                    {
                        if (setLevelId == Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1].ToString()))
                        {
                            chessNumber = chessNumber + 1;
                        }
                        else
                        {
                            resetChessNumber = resetChessNumber + 200;
                            chessNumber = resetChessNumber;
                            setLevelId = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1].ToString());
                        }
                        
                    }
                    updQuery = "UPDATE tbl_ParticipantList SET vchChessNo=" + chessNumber + " WHERE intParticipantListId="+ Convert.ToInt32( ds.Tables[0].Rows[i].ItemArray[0].ToString()) ;
                    DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, updQuery);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SaveGroupSection(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter("@intGrpSectID", Param[0]);
            p[1] = new SqlParameter("@intParticipantListId", Param[1]);
            p[2] = new SqlParameter("@intSectionID", Param[2]);
          

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveGroupSection", p) == 1)
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
        public static bool ResetGroupSection(int intParticipantListId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intParticipantListId", intParticipantListId);

            Query = "DELETE FROM [tbl_GroupSection] WHERE  [intParticipantListId]=@intParticipantListId";

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p) > 0)
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
