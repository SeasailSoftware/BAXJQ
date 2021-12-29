using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Service
{
    public class OperLogService
    {
        #region 写操作日志
        public static void Insert(string operName, string recDatetime, string logObject, string logAction, string logMessage, int logType)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertOperLog";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@OperName", DbType.String, operName);
                dbHelper.AddInParameter(cmd, "@recDatetime", DbType.String, recDatetime);
                dbHelper.AddInParameter(cmd, "@Object", DbType.String, logObject);
                dbHelper.AddInParameter(cmd, "@LogAction", DbType.String, logAction);
                dbHelper.AddInParameter(cmd, "@LogMessage", DbType.String, logMessage);
                dbHelper.AddInParameter(cmd, "@LogType", DbType.String, logType);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 根据类型查找日志
        public static DataTable Find(int logType)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"select * from OperLog  Where  LogType={logType}  Order By Convert(DateTime,RecDateTime)  desc";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion


        #region 生成列表
        public static List<OperLog> ToList()
        {
            List<OperLog> list = new List<OperLog>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                OperLog obj = new OperLog();
                obj.LogId = Convert.ToInt32(row["LogId"]);
                obj.OperName = row["OperName"].ToString();
                obj.RecDatetime = row["RecDatetime"].ToString();
                obj.LogObj = row["Object"].ToString();
                obj.LogAction = row["LogAction"].ToString();
                obj.LogMessage = row["LogMessage"].ToString();
                obj.LogType = Convert.ToInt32(row["LogType"]);
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From OperLog";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 查找日志
        public static DataTable Find(string operName, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "FindOperLog";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@OperName", DbType.String, operName);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion


    }
}
