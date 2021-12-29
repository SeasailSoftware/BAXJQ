#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：hpt.gate.DataAccess.Service

* 项目描述 ：

* 类 名 称 ：HTVacationService

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：hpt.gate.DataAccess.Service

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-08 14:16:24

* 更新时间 ：2019-07-08 14:16:24

* 版 本 号 ：v1.0.0.0

*******************************************************************

* Copyright @ Administrator 2019. All rights reserved.

*******************************************************************

//----------------------------------------------------------------
*/

#endregion
using hpt.gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace hpt.gate.DataAccess.Service
{
    /// <summary>
    /// 功能描述    ：HTVacationService  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-08 14:16:24 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-08 14:16:24 
    /// </summary>
    public class HTVacationService
    {
        static HTVacationService()
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"If Not exists(select * from dbo.sysObjects where id = Object_Id(N'HTVacation') and ObjectProperty(id, N'IsUserTable') = 1) ");
                    buffer.AppendLine($"CREATE TABLE [dbo].[HTVacation](");
                    buffer.AppendLine($"[Id] [int] IDENTITY(1,1) NOT NULL,");
                    buffer.AppendLine($"[CardNo] [varchar](20) NOT NULL,");
                    buffer.AppendLine($"[BeginTime] [varchar](20) NOT NULL,");
                    buffer.AppendLine($"[EndTime] [varchar](20) NOT NULL");
                    buffer.AppendLine($") ON [PRIMARY]");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
            catch
            {

            }
        }
        public static bool Insert(HTVacation vacation, out string msg)
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"Insert Into HTVacation(CardNo,BeginTime,EndTime) Values('{vacation.CardNo}','{vacation.BeginTime}','{vacation.EndTime}')");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
                msg = "Success!";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public static HTVacation Get(string sCardNo)
        {
            HTVacation vacation = null; 
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"Select top 1 * From HTVacation Where CardNo='{sCardNo}' And BeginTime<='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' And EndTime>='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    vacation = new HTVacation();
                    vacation.Id = Convert.ToInt32(row["Id"]);
                    vacation.CardNo = sCardNo;
                    vacation.BeginTime = row["BeginTime"].ToString();
                    vacation.EndTime = row["EndTime"].ToString();
                    break;
                }
            }
            return vacation;
        }
    }
}
