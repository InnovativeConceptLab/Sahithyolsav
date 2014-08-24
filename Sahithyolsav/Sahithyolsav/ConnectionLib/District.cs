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
    public class District
    {

        public static DataSet GetAllDistricts()
        {
            DataSet ds = new DataSet();
            String Query;
            Query = "Select * from [tbl_District] ";
            try
            {

                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query);
                return ds;
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
        public static DataSet GetDistricts(string Distname, string DistCode, int ID = 0)
        {
            DataSet ds = new DataSet();
            String Query;
            string criteria = "";
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@intDistrictID", ID);

            if (ID != 0)
            {
                p[0] = new SqlParameter("@intDistrictID", ID);
                Query = "Select * from [tbl_District] where [intDistrictID]=@intDistrictID";
            }
            else
            {
                if (Distname != "")
                {
                    criteria = criteria + " where vchDistrictName='" + Distname + "'";
                }
                if (DistCode != "")
                {
                    if (criteria != "")
                    {
                        criteria = criteria + " and vchDistrictCode='" + DistCode + "'";
                    }
                    else
                    {
                        criteria = criteria + " where vchDistrictCode='" + DistCode + "'";
                    }
                }
                Query = "Select * from tbl_District " + criteria;

            }


            try
            {

                ds = DataLayer.SqlHelper.ExecuteDataset(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query, p);
                return ds;
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
        public static bool SaveDistrict(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter("@intDistrictID", Param[0]);
            p[1] = new SqlParameter("@vchDistrictName", Param[1]);
            p[2] = new SqlParameter("@vchDistrictCode", Param[2]);

            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveDistrict", p) == 1)
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
        public static Boolean DeleteDistricts(string ID)
        {
            DataSet ds = new DataSet();
            String Query;

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@intDistrictID", ID);

            p[0] = new SqlParameter("@intDistrictID", ID);
            Query = "Delete  from [tbl_District] where [intDistrictID]=" + ID;


            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.Text, Query) == 1)
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
