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
    public class item
    {
        public static bool SaveItem(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[9];

            p[0] = new SqlParameter("@intItemId", Param[0]);
            p[1] = new SqlParameter("@vchItemName", Param[1]);
            p[2] = new SqlParameter("@vchItemCode", Param[2]);
            p[3] = new SqlParameter("@intSectionId", Param[3]);
            p[4] = new SqlParameter("@isGroupItem", Param[4]);
            p[5] = new SqlParameter("@intMaxNumberOfParticpant", Param[5]);
            p[6] = new SqlParameter("@intMarkForFirstPlace", Param[6]);
            p[7] = new SqlParameter("@intMarkForSecondPlace", Param[7]);
            p[8] = new SqlParameter("@intMarkForThirdPlace", Param[8]);
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveIteem", p) == 1)
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
        public static DataTable getAllItems()
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT [intItemId],[vchItemName],[vchItemCode],tbl_Item.[intSectionId],tbl_Section.vchSectionName FROM [tbl_Item] "
                     + " INNER JOIN tbl_Section on tbl_Section.intSectionID=tbl_Item.[intSectionId]";

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
        public static DataTable getItemsById(int intItemId)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("@intItemId", intItemId);

            Query = "SELECT [intItemId],[vchItemName],[vchItemCode],tbl_Item.[intSectionId],tbl_Section.vchSectionName,[isGroupItem],[intMaxNumberOfParticpant],[intMarkForFirstPlace],[intMarkForSecondPlace],[intMarkForThirdPlace] FROM [tbl_Item] "
                     + " INNER JOIN tbl_Section on tbl_Section.intSectionID=tbl_Item.[intSectionId]"
                     + " WHERE tbl_Item.[intItemId]=@intItemId";

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
        public static DataTable getItemDetailsById(int intItemId)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("@intItemId", intItemId);

            Query = "SELECT * FROM tbl_Item  WHERE intItemId=@intItemId";

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
        public static DataTable getItemsBySectionId(int intSectionId)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("@intSectionId", intSectionId);

            Query = "SELECT [intItemId],[vchItemName],[vchItemCode],tbl_Item.[intSectionId],tbl_Section.vchSectionName FROM [tbl_Item] "
                     + " INNER JOIN tbl_Section on tbl_Section.intSectionID=tbl_Item.[intSectionId]"
                     + " WHERE tbl_Item.[intSectionId]=@intSectionId";

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
        public static DataTable getGroupItemsBySectionId(int intSectionId)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("@intSectionId", intSectionId);

            Query = "SELECT [intItemId],[vchItemName],[vchItemCode],tbl_Item.[intSectionId],tbl_Section.vchSectionName FROM [tbl_Item] "
                     + " INNER JOIN tbl_Section on tbl_Section.intSectionID=tbl_Item.[intSectionId]"
                     + " WHERE tbl_Item.[intSectionId]=@intSectionId AND isGroupItem=1";

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
        public static int getMaxNumofParticiapntForItem(int itemId)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("@itemId", itemId);

            Query = "SELECT ISNULL(intMaxNumberOfParticpant,0) FROM tbl_Item WHERE intItemId=@itemId";

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p);
                return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 0;
            }

            finally
            {
                ds.Dispose();
                Query = "";
            }
        }
        public static bool Deleteitem(int intItemId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intItemId", intItemId);

            Query = "DELETE FROM [tbl_Item] WHERE intItemId=@intItemId";

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
        public static DataTable GetPoints(string itemId, string Position)
        {
            String Query;
            DataSet ds = new DataSet();
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("@intItemId", itemId);
            if (Position == "First")
            {
                Query = "SELECT intMarkForFirstPlace FROM [tbl_Item] WHERE intItemId=@intItemId";
            }
            else if (Position == "Second")
            {
                Query = "SELECT intMarkForSecondPlace FROM [tbl_Item] WHERE intItemId=@intItemId";
            }
            else
            {
                Query = "SELECT intMarkForThirdPlace FROM [tbl_Item] WHERE intItemId=@intItemId";
            }
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
    }
}
