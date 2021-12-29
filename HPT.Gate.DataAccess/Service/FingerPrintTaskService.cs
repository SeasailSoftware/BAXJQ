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
    public class FingerPrintTaskService
    {
        #region 生成列表
        public static List<FingerPrintTask> ToList()
        {
            List<FingerPrintTask> list = new List<FingerPrintTask>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                FingerPrintTask obj = new FingerPrintTask();
                obj.RecId = Convert.ToInt32(row["RecId"]);
                obj.EmpId = Convert.ToInt32(row["EmpId"]);
                obj.FingerId = Convert.ToInt32(row["FingerId"]);
                obj.FingerData = (byte[])row["FingerData"];
                obj.UpdateFlag = Convert.ToInt32(row["UpdateFlag"]);
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
                string sql = "Select * From FingerPrintTask Where UpdateFlag = 0";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 更新指纹任务状态
        public static void Update(int recId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From FingerPrintTask Where RecId={recId} ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
