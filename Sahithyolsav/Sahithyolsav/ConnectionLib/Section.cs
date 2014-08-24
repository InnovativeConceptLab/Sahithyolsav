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
    public class Section
    {
        public static bool SaveSection(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[4];

            p[0] = new SqlParameter("@intSectionID", Param[0]);
            p[1] = new SqlParameter("@vchSectionName", Param[1]);
            p[2] = new SqlParameter("@intSectionLevel", Param[2]);
            p[3] = new SqlParameter("@vchSectionCode", Param[3]);
        
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveSection", p) == 1)
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
        public static DataTable getAllSections()
        {
            DataSet ds = new DataSet();
            String Query;

            Query = "SELECT [intSectionID],[vchSectionName],[intSectionLevel],[vchSectionCode] FROM [tbl_Section]";

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
        public static bool validateSectionName(string SectionName)
        {
            DataSet ds = new DataSet();
            String Query;
            SqlParameter[] p = new SqlParameter[1];

            p[0] = new SqlParameter("@vchSectionName", SectionName);
            Query = "SELECT vchSectionName FROM [tbl_Section] WHERE vchSectionName =@vchSectionName";

            try
            {
                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p);
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
        public static bool DeleteSection(int intSectionId)
        {
            SqlParameter[] p = new SqlParameter[1];
            String Query;

            p[0] = new SqlParameter("@intSectionId", intSectionId);

            Query = "DELETE FROM tbl_Section WHERE intSectionID=@intSectionId";

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
