#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：hpt.gate.DataAccess.Service

* 项目描述 ：

* 类 名 称 ：HTTimegroupService

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：hpt.gate.DataAccess.Service

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-08 11:46:13

* 更新时间 ：2019-07-08 11:46:13

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
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace hpt.gate.DataAccess.Service
{
    /// <summary>
    /// 功能描述    ：HTTimegroupService  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-08 11:46:13 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-08 11:46:13 
    /// </summary>
    public class HTTimegroupService
    {
        static HTTimegroupService()
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"If Not exists(select * from dbo.sysObjects where id = Object_Id(N'HTTimegroup') and ObjectProperty(id, N'IsUserTable') = 1) ");
                    buffer.AppendLine($"CREATE TABLE [dbo].[HTTimegroup](");
                    buffer.AppendLine($"[Id] [int] IDENTITY(1,1) NOT NULL,");
                    buffer.AppendLine($"[GroupId] [int] NOT NULL,");
                    buffer.AppendLine($"[GradeId] [int] NOT NULL,");
                    buffer.AppendLine($"[Type] [int] NOT NULL,");
                    buffer.AppendLine($"[TermSN] [varchar](20) NOT NULL,");
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
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"If COL_LENGTH('HTTimegroup','DormBanId') Is Null");
                    buffer.AppendLine($"Begin");
                    buffer.AppendLine($"    Truncate Table HTTimegroup");
                    buffer.AppendLine($"    Alter Table HTTimegroup Add DormBanId int Not Null Default (0)");
                    buffer.AppendLine($"End");
                    buffer.AppendLine($"If COL_LENGTH('HTTimegroup','gateBanId') Is Null");
                    buffer.AppendLine($"Begin");
                    buffer.AppendLine($"    Truncate Table HTTimegroup");
                    buffer.AppendLine($"    Alter Table HTTimegroup Add gateBanId int Not Null Default (0)");
                    buffer.AppendLine($"End");

                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                var val = ex.Message;
            }

        }

        public static bool Insert(HTTimegroup timegroup, out string msg)
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"If Not Exists(Select top 1 * From HTTimegroup Where DormBanId={timegroup.dormBanId} And gateBanId={timegroup.gateBanId}  And TermSN='{timegroup.termSn}' And Type={timegroup.type} And GradeId={timegroup.gradeId}) ");
                    buffer.AppendLine($"Insert Into HTTimegroup(GroupId,DormBanId,gateBanId,TermSN,BeginTime,EndTime,GradeId,Type) Values({timegroup.id},{timegroup.dormBanId},{timegroup.gateBanId},'{timegroup.termSn}','{timegroup.beginTime}','{timegroup.endTime}',{timegroup.gradeId},{timegroup.type})");
                    buffer.AppendLine("Else");
                    buffer.AppendLine($"Update HTTimegroup Set BeginTime='{timegroup.beginTime}',EndTime ='{timegroup.endTime}'  Where  DormBanId={timegroup.dormBanId} And gateBanId={timegroup.gateBanId}   And TermSN='{timegroup.termSn}' And Type={timegroup.type} And GradeId={timegroup.gradeId} ");
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

        public static bool Delete(HTTimegroup timegroup, out string msg)
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($" Delete From HTTimegroup Where  GroupId={timegroup.id}   And TermSN='{timegroup.termSn}' And Type={timegroup.type} And GradeId={timegroup.gradeId}");
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

        public static List<HTTimegroup> GetByCard(int gradeId, string mac, int type)
        {
            List<HTTimegroup> tgs = new List<HTTimegroup>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($" Select *  From HTTimegroup Where  GradeId ={gradeId}  And TermSN='{mac}' And Type={type}");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    HTTimegroup tg = new HTTimegroup();
                    tg.id = Convert.ToInt32(row["GroupId"]);
                    tg.type = type;
                    tg.gradeId = gradeId;
                    tg.termSn = mac;
                    tg.beginTime = row["BeginTime"].ToString();
                    tg.endTime = row["EndTime"].ToString();
                    tgs.Add(tg);
                }
            }
            return tgs;
        }

        public static void Clear()
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($" Truncate Table HTTimegroup");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
            catch
            {

            }
        }
    }
}
