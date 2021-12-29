using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace HPT.Gate.DataAccess.Service
{
    public class DeptInfoService
    {
        

        #region 检查部门是否存在人员
        public static bool HasEmp(int deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from EmpInfo where DeptId =" + deptId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region 删除部门信息
        public static void Del(int deptId,OperInfo oper)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete from DeptInfo where DeptId ={deptId}";
                sql += $"{Environment.NewLine}Delete From DeptOfOper Where DeptId ={deptId}";
                sql += $"{Environment.NewLine}Delete From Led_DynPara Where ParaId ={deptId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
            OperLogService.Insert(oper.OperName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),"部门信息","删除",$"删除部门,DeptId ={deptId}",1);
        }
        #endregion

        #region 生成列表
        public static List<DeptInfo> ToList()
        {
            List<DeptInfo> list = new List<DeptInfo>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                DeptInfo obj = new DeptInfo();
                obj.DeptId = Convert.ToInt32(row["DeptId"]);
                obj.ParDeptId = Convert.ToInt32(row["ParDeptId"]);
                obj.DeptName = row["DeptName"].ToString();
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
                string sql = "Select * From DeptInfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

        #region 检查同一个上级部门下是否存在部门名称一样的部门
        public static bool CheckDeptExists(int parDeptId, string deptName)
        {
            return ToList().Exists(s => s.ParDeptId == parDeptId && deptName.Equals(s.DeptName));
        }

        public static bool CheckDeptExists(string deptName)
        {
            return ToList().Exists(s => deptName.Equals(s.DeptName));
        }

        #endregion

        #region 添加部门
        public static void Insert(int parDeptId, string deptName,OperInfo oper)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertDept";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@ParDeptId", DbType.Int32, parDeptId);
                dbHelper.AddInParameter(cmd, "@DeptName", DbType.String, deptName);
                dbHelper.AddInParameter(cmd, "@OperId", DbType.Int32, oper.OperId);
                dbHelper.ExecuteNonQuery(cmd);
            }
            OperLogService.Insert(oper.OperName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "部门信息", "新增", $"新增部门[{deptName}]", 1);
        }
        #endregion

        #region 更新部门信息
        public static void Update(int parDeptId, int deptId, string deptName,OperInfo oper)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "UpdateDept";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@ParDeptId", DbType.Int32, parDeptId);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptName", DbType.String, deptName);
                dbHelper.ExecuteNonQuery(cmd);
            }
            OperLogService.Insert(oper.OperName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "部门信息", "更新", $"更新部门信息为[{deptName}]", 1);
        }
        #endregion

        #region 部门树
        public static DataTable GetDeptTreeDable()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select deptid as id,deptName as name,pardeptid as parid,ImageIndex as ImageIndex  from deptinfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取部门信息
        public static DeptInfo GetByDeptId(int deptId)
        {
            return ToList().FirstOrDefault(s => s.DeptId == deptId);
        }
        #endregion

        #region 获取下级部门(包括当前部门)
        public static List<DeptInfo> GetChildDepts(int parDeptId)
        {
            List<DeptInfo> arrayList = new List<DeptInfo>();
            arrayList.Add(GetByDeptId(parDeptId));
            arrayList.AddRange(GetSubDepts(ToList(), parDeptId));
            return arrayList;
        }
        #endregion


        #region 获取下级部门(不包括当前部门)
        private static List<DeptInfo> GetSubDepts(List<DeptInfo> deptList, int parDeptId)
        {
            List<DeptInfo> arrayList = new List<DeptInfo>();
            List<DeptInfo> depts = deptList.Where(s => s.ParDeptId == parDeptId).ToList();
            arrayList.AddRange(depts);
            foreach (DeptInfo dept in depts)
            {
                arrayList.AddRange(GetSubDepts(deptList, dept.DeptId));
            }
            return arrayList;
        }
        #endregion

        #region 获取操作员对应的部门
        public static List<DeptInfo> GetByOperId(int currentOperId)
        {
            List<DeptOfOper> deptOfOpers = DeptOfOperService.GetByOperId(currentOperId);
            return ToList().Where(s => deptOfOpers.Exists(p => s.DeptId == p.DeptId)).ToList();
        }
        #endregion



    }
}
