using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace HPT.Gate.DataAccess.Service
{
    public class AttendLeaveService
    {
        public static void Insert(AttendLeave leave)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Insert Into AttendLeave(EmpId,BeginTime,EndTime,LeaveType,Remark,CreateTime)";
                sql += $"{Environment.NewLine} Values({leave.EmpId},'{leave.BeginTime}','{leave.EndTime}',{leave.LeaveType},'{leave.Remark}','{leave.CreateTime}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static void Update(AttendLeave leave)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update AttendLeave Set BeginTime='{leave.BeginTime}',EndTime ='{leave.EndTime}',LeaveType ={leave.LeaveType},Remark='{leave.Remark}'";
                sql += $"{Environment.NewLine} Where RecId ={leave.RecId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static void Del(int recId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From AttendLeave Where RecId = {recId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static AttendLeave Find(int _RecId)
        {
            return GetAll().FirstOrDefault(p => p.RecId == _RecId);
        }

        public static List<AttendLeave> GetAll()
        {
            List<AttendLeave> leaves = new List<AttendLeave>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From AttendLeave a,EmpInfo b,DeptInfo c,LeaveType d Where a.EmpId = b.EmpId and b.DeptId = c.DeptId And a.LeaveType =d.TypeId Order By a.EmpId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    AttendLeave leave = new AttendLeave();
                    leave.RecId = Convert.ToInt32(row["RecId"]);
                    leave.EmpId = Convert.ToInt32(row["EmpId"]);
                    leave.DeptId = Convert.ToInt32(row["DeptId"]);
                    leave.LeaveType = Convert.ToInt32(row["LeaveType"]);
                    leave.LeaveName = row["TypeName"].ToString();
                    leave.DeptName = row["DeptName"].ToString();
                    leave.EmpCode = row["EmpCode"].ToString();
                    leave.EmpName = row["EmpName"].ToString();
                    leave.BeginTime = row["BeginTime"].ToString();
                    leave.EndTime = row["EndTime"].ToString();
                    leave.Remark = row["Remark"].ToString();
                    leave.CreateTime = row["CreateTime"].ToString();
                    leaves.Add(leave);
                }
            }
            return leaves;
        }

        public static List<AttendLeave> Find(int deptId, int deptType, string empCode, string empName)
        {
            List<EmpInfo> empList = EmpInfoService.Find(deptId, deptType, empCode, empName, "");
            List<AttendLeave> list = new List<AttendLeave>();
            List<AttendLeave> leaveList = GetAll();
            foreach (EmpInfo emp in empList)
            {
                list.AddRange(leaveList.Where(p => p.EmpId == emp.EmpId));
            }
            return list;
        }
    }
}
