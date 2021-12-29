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
    public class DeptOfOperService
    {
        #region 生成列表
        public static List<DeptOfOper> ToList()
        {
            List<DeptOfOper> list = new List<DeptOfOper>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                DeptOfOper obj = new DeptOfOper();
                obj.RecId = Convert.ToInt32(row["RecId"]);
                obj.DeptId = Convert.ToInt32(row["DeptId"]);
                obj.DeptId = Convert.ToInt32(row["DeptId"]);
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
                string sql = "Select * From DeptOfOper";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

        #region 添加操作员对部门的权限
        public static void Insert(DeptOfOper deptOfOper)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine}If Not Exists(Select Top 1 * From DeptOfOper Where DeptId ={deptOfOper.DeptId} And OperId ={deptOfOper.OperId})";
                sql += $"{Environment.NewLine}Insert Into DeptOfOper(OperId,DeptId) values({deptOfOper.OperId},{deptOfOper.DeptId})";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除操作员对部门的权限
        public static void Del(int currentOperId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From DeptOfOper Where OperId ={currentOperId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 根据操作员获取部门权限列表
        public static List<DeptOfOper> GetByOperId(int operId)
        {
            return ToList().Where(p => p.OperId == operId).ToList();
        }
        #endregion

    }
}
