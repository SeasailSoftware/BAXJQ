using hpt.gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace hpt.gate.DataAccess.Service
{
    public class SystemConfigService
    {
        static SystemConfigService()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"If Not exists(select * from dbo.sysObjects where id = Object_Id(N'SystemConfig') and ObjectProperty(id, N'IsUserTable') = 1) ");
                buffer.AppendLine($"CREATE TABLE[dbo].[SystemConfig]");
                buffer.AppendLine($"(");
                buffer.AppendLine($"[FaceEnabled][int] NOT NULL,");
                buffer.AppendLine($"[FaceMachineType] [int] NOT NULL");
                buffer.AppendLine($") ON[PRIMARY]");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"If Not exists(select Top 1 *  from SystemConfig)");
                buffer.AppendLine($"Insert Into SystemConfig(FaceEnabled,FaceMachineType) Values(0,0)");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                try
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"If COL_LENGTH('SystemConfig','OutPutType') Is Null");
                    buffer.AppendLine($"Alter Table SystemConfig Add OutPutType int Not Null Default (0)");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                  var val=  ex.Message;
                }
            }

        }

        public static SystemConfig Get()
        {
            SystemConfig config = new SystemConfig();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From SystemConfig";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    config.FaceEnabled = Convert.ToInt32(row["FaceEnabled"]) == 1;
                    config.FaceMachineType = Convert.ToInt32(row["FaceMachineType"]);
                    config.OutPutType = Convert.ToInt32(row["OutPutType"]);
                    break;
                }
            }
            return config;
        }

        public static void Set(SystemConfig config)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"Update SystemConfig Set FaceEnabled ={(config.FaceEnabled?1:0)},FaceMachineType={config.FaceMachineType},OutPutType={config.OutPutType}");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
    }
}
