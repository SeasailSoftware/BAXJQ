using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using HPT.Gate.DataAccess.Entity;

namespace HPT.Gate.DataAccess.Service
{
    public class AttendShiftOfEmpService
    {
        #region 插入排班信息
        public static void Insert(int empId, string beginDate, string endDate, int shiftId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "AttendShiftOfEmp_Insert";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                dbHelper.AddInParameter(cmd, "@ShiftId", DbType.Int32, shiftId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static List<AttendShiftOfEmp> GetAll(string beginDate, string endDate)
        {
            List<AttendShiftOfEmp> shiftList = new List<AttendShiftOfEmp>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $@"Select a.*,b.DeptId,b.DeptName,c.EmpCode,c.EmpName From AttendShiftOfEmp a,DeptInfo b,EmpInfo c Where a.RecDate >='{beginDate}' And a.RecDate <='{endDate}'";
                sql += $@"{Environment.NewLine} And a.EmpId = c.EmpId And b.DeptId = c.DeptId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<AttendShift> attendList = AttendShiftService.ToList();
                foreach (DataRow row in dt.Rows)
                {
                    AttendShiftOfEmp shift = new AttendShiftOfEmp();
                    shift.RecId = Convert.ToInt32(row["RecId"]);
                    shift.DeptId = Convert.ToInt32(row["DeptId"]);
                    shift.DeptName = row["DeptName"].ToString();
                    shift.EmpId = Convert.ToInt32(row["EmpId"]);
                    shift.EmpCode = row["EmpCode"].ToString();
                    shift.EmpName = row["EmpName"].ToString();
                    shift.RecDate = Convert.ToDateTime(row["RecDate"]);
                    int shiftId = Convert.ToInt32(row["ShiftId"]);
                    shift.ShiftOfDate = attendList.FirstOrDefault(p => p.ShiftId == shiftId);
                    shiftList.Add(shift);
                }
            }
            return shiftList;
        }

        public static List<AttendShiftOfEmp> GetByEmpId(int empId, string beginDate, string endDate)
        {
            List<AttendShiftOfEmp> shiftList = new List<AttendShiftOfEmp>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $@"Select a.*,b.DeptId,b.DeptName,c.EmpCode,c.EmpName From AttendShiftOfEmp a,DeptInfo b,EmpInfo c Where a.RecDate >='{beginDate}' And a.RecDate <='{endDate}' And a.EmpId ={empId}";
                sql += $@"{Environment.NewLine} And a.EmpId = c.EmpId And b.DeptId = c.DeptId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<AttendShift> attendList = AttendShiftService.ToList();
                foreach (DataRow row in dt.Rows)
                {
                    AttendShiftOfEmp shift = new AttendShiftOfEmp();
                    shift.RecId = Convert.ToInt32(row["RecId"]);
                    shift.DeptId = Convert.ToInt32(row["DeptId"]);
                    shift.DeptName = row["DeptName"].ToString();
                    shift.EmpId = Convert.ToInt32(row["EmpId"]);
                    shift.EmpCode = row["EmpCode"].ToString();
                    shift.EmpName = row["EmpName"].ToString();
                    shift.RecDate = Convert.ToDateTime(row["RecDate"]);
                    int shiftId = Convert.ToInt32(row["ShiftId"]);
                    shift.ShiftOfDate = attendList.FirstOrDefault(p => p.ShiftId == shiftId);
                    shiftList.Add(shift);
                }
            }
            return shiftList;
        }
        #endregion

    }
}
