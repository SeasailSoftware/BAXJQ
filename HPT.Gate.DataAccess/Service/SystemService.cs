using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class SystemService
    {
        #region 测试数据库连接
        public static bool TestConnect(string conString)
        {
            try
            {

                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper(conString))
                {
                    string sql = "Select 1000";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    dbHelper.ExecuteNonQuery(cmd);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 备份数据库
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="path"></param>
        public static void BackUpData(string path, string dbName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = " BACKUP DATABASE  " + dbName + "  to DISK = '" + path + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 附加数据库
        public static void AttachDataBase(string conString, string dbName, string dataPath, string logPath)
        {
            using (OleDbHelper dbHelper = new OleDbHelper(conString))
            {
                string sqlAttach = " EXEC  sp_attach_db  @dbname='" + dbName + "',@filename1='" + dataPath + "',@filename2 = '" + logPath + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sqlAttach);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取数据库服务器列表
        public static DataTable GetServerList()
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            System.Data.DataTable table = instance.GetDataSources();
            return table;
        }
        #endregion

        #region 还原数据库
        public static bool RestoreDatabase(string fileName, string dbName, string connectString)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper(connectString))
            {
                ///<---------先找出所有连接数----------->
                string sqlKillSid = "Use master SELECT spid FROM sysprocesses ,sysdatabases WHERE sysprocesses.dbid=sysdatabases.dbid AND sysdatabases.Name='" + dbName + "'";

                DbCommand cmdKillSid = dbHelper.GetSqlStringCommond(sqlKillSid);
                DataTable dtKillSid = dbHelper.ExecuteDataTable(cmdKillSid);
                ///<---------删除所有连接数----------->
                foreach (DataRow row in dtKillSid.Rows)
                {
                    string sqlKill = "Kill " + row[0].ToString();
                    DbCommand cmdKill = dbHelper.GetSqlStringCommond(sqlKill);
                    dbHelper.ExecuteNonQuery(cmdKill);
                }
                ///<---------数据库还原----------->
                // SqlConnection constring = new SqlConnection(Data Source=(local);Initial Catalog=master;User ID=sa;Password=123);
                string sql = "Use Master  RESTORE DATABASE   " + dbName + "   FROM DISK ='" + fileName + "'   WITH REPLACE";//数据库名称和路径 WITH REPLACE是去除日志文件
                ///string sql = string.Format("use master;exec p_killspid '{0}';restore database {0} From disk = '{1}' with replace;", GlobalVariables.SpecifiedDBName, fileName);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                int index = dbHelper.ExecuteNonQuery(cmd);
                return index >= 0 ? false : true
                    ;
            }
        }
        #endregion

        #region 初始化数据库
        public static void InitSystem()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InitSystem";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 执行数据库语句
        public static bool ExecuteSqlFile(DBHelper dbHelper, string sql)
        {
            try
            {
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
                return true;
            }
            catch
            {

            }
            return false;
        }
        #endregion

        #region 更改考勤模式
        public static void ChangeAttendModel(int attendModel)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Empty;
                switch (attendModel)
                {
                    case 0:
                        sql += $"{Environment.NewLine}Update Menus Set EnableFlag = 1 where MenuId >=61 and MenuId <=66";
                        sql += $"{Environment.NewLine}Update Menus Set EnableFlag = 0 where MenuId = 67 or MenuId = 68 or MenuId = 69 or MenuId = 610";
                        break;
                    case 1:
                        sql += $"{Environment.NewLine}Update Menus Set EnableFlag = 0 where MenuId >=61 and MenuId <=66";
                        sql += $"{Environment.NewLine}Update Menus Set EnableFlag = 1 where MenuId = 67 or MenuId = 68 or MenuId = 69 or MenuId = 610";
                        break;
                }
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
