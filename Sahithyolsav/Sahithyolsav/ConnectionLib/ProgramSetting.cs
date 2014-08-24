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
    public class ProgramSetting
    {
        public static bool SaveParticipant(ArrayList Param)
        {
            SqlParameter[] p = new SqlParameter[5];

            p[0] = new SqlParameter("@intProgramSettingId", Param[0]);
            p[1] = new SqlParameter("@intProgramLevelId", Param[1]);
            p[2] = new SqlParameter("@intProgramLevelMapId", Param[2]);
            p[3] = new SqlParameter("@dtProgramDate", Param[3]);
            p[4] = new SqlParameter("@dtProgramEntryLastDate", Param[3]);
            try
            {
                if (DataLayer.SqlHelper.ExecuteNonQuery(Utilities.GetConnectionString(Utilities.DataBase.Sahithyolsav), CommandType.StoredProcedure, "spSaveProgramSettings", p) == 1)
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
