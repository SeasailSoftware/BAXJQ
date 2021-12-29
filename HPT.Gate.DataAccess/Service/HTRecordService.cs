#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：hpt.gate.DataAccess.Service

* 项目描述 ：

* 类 名 称 ：HTRecordService

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：hpt.gate.DataAccess.Service

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-09 15:43:52

* 更新时间 ：2019-07-09 15:43:52

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
    /// 功能描述    ：HTRecordService  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-09 15:43:52 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-09 15:43:52 
    /// </summary>
    public class HTRecordService
    {
        static HTRecordService()
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"If Not exists(select * from dbo.sysObjects where id = Object_Id(N'HTRecord') and ObjectProperty(id, N'IsUserTable') = 1) ");
                    buffer.AppendLine($"CREATE TABLE [dbo].[HTRecord](");
                    buffer.AppendLine($"[Id] [int] IDENTITY(1,1) NOT NULL,");
                    buffer.AppendLine($"[TermSN] [varchar](20) NOT NULL,");
                    buffer.AppendLine($"[RecordType] [int] Not Null Default 0,");
                    buffer.AppendLine($"[CardNo] [varchar](20) NOT NULL,");
                    buffer.AppendLine($"[IOFlag] [int] NOT NULL,");
                    buffer.AppendLine($"[RecDatetime] [varchar](20) NOT NULL,");
                    buffer.AppendLine($"[Capture] [varchar](20) NOT NULL,");
                    buffer.AppendLine($"[Status] [int] NOT NULL Default 0,");
                    buffer.AppendLine($"[Result] [varchar](300)  NULL,");
                    buffer.AppendLine($") ON [PRIMARY]");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
            catch
            {

            }
        }

        public static bool Insert(HTRecord record, out string msg)
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"Insert Into HTRecord(TermSN,RecordType,CardNo,IOFlag,RecDatetime,Capture,Status,Result) Values('{record.TermSN}',{record.RecordType},'{record.CardNo}',{record.IOFlag},'{record.RecDatetime}','{record.Capture}',{record.Status},'{record.Result}')");
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

        public static List<HTRecord> GetUnuploadedRecords()
        {
            List<HTRecord> records = new List<HTRecord>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"Select * From HTRecord Where Status = 0");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    HTRecord record = new HTRecord();
                    record.Id = Convert.ToInt32(row["Id"]);
                    record.RecordType = Convert.ToInt32(row["RecordType"]);
                    record.Capture = row["Capture"].ToString();
                    record.CardNo = row["CardNo"].ToString();
                    record.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    record.RecDatetime = row["RecDatetime"].ToString();
                    record.Result = row["Result"].ToString();
                    record.Status = 0;
                    record.TermSN = row["TermSN"].ToString();
                    records.Add(record);
                }
            }
            return records;
        }

        public static void Update(HTRecord record)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"Update HTRecord Set Status ={record.Status},Result = '{record.Result}' Where Id ={record.Id}");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
    }
}
