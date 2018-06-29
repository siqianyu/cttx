using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StarTech.DBUtility;

namespace StarTech.ELife
{
    public class IdCreator
    {
        public static string CreateId(string tableName, string fieldName)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_System_IdCreator where tableName='" + tableName + "' and fieldName='" + fieldName + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["flag"].ToString().ToUpper() == "TIME_RANDOM_RESET")
                {
                    System.Threading.Thread.Sleep(50);
                    int idValue = Int32.Parse(dt.Rows[0]["currentIdValue"].ToString()) + 1;
                    if (idValue < 99)
                    {
                        adoHelper.ExecuteSqlNonQuery("update T_System_IdCreator set currentIdValue=" + idValue + " where tableName='" + tableName + "' and fieldName='" + fieldName + "' ");
                    }
                    else
                    {
                        adoHelper.ExecuteSqlNonQuery("update T_System_IdCreator set currentIdValue=10 where tableName='" + tableName + "' and fieldName='" + fieldName + "' ");

                    }
                    return DateTime.Now.ToString("yyMMddHHmmss") + new Random().Next(1000, 9999).ToString() + idValue;
                }
                else
                {
                    int idValue = Int32.Parse(dt.Rows[0]["currentIdValue"].ToString()) + 1;
                    adoHelper.ExecuteSqlNonQuery("update T_System_IdCreator set currentIdValue=" + idValue + " where tableName='" + tableName + "' and fieldName='" + fieldName + "' ");
                    return dt.Rows[0]["flag"].ToString() + idValue;
                }
            }
            else
            {
                return "";
            }
        }
    }
}
