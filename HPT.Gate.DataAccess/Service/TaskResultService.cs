using HPT.Gate.Utils.Helper;
using System.Data;
using System.Data.Common;

namespace hpt.gate.DataAccess.Service
{
    public class TaskResultService
    {
        public static void Insert(int taskId, int empId, int deviceId, bool result, string msg)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Insert Into TaskResult(TaskId,EmpId,DeviceId,Result,Msg) Values({taskId},{empId},{deviceId},{(result ? 1 : 0)},'{msg}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }

        }

        public static void Clear()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Truncate Table TaskResult";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From TaskResult Order By EmpId,DeviceId,Result";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
    }
}
