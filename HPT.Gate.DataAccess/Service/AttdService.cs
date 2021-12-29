using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace HPT.Gate.DataAccess.Service
{
    public class AttdService
    {
        #region 插入考勤明细表
        public static void InsertAttendDetail(AttendDetail attendDetail)
        {

        }
        #endregion

        #region 处理当天没有考勤记录的考勤数据
        public static void InsertAttendDetailNoRecord(int empId, string recDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"Declare @DeptId int, @DeptName varchar(30),@EmpCode varchar(10),@EmpName varchar(30),@Record varchar(100),@SignIn int, @IOCount int, @Minutes int");
                buffer.AppendLine($"Select @DeptId = b.DeptId,@DeptName = b.DeptName,@EmpCode = a.EmpCode,@EmpName = a.EmpName From EmpInfo a,DeptInfo b");
                buffer.AppendLine($"where a.DeptId = b.Deptid and a.EmpId = {empId}");
                buffer.AppendLine($"Select @Record = ''");
                buffer.AppendLine($"Select @SignIn = 0");
                buffer.AppendLine($"Select @IOCount = 0");
                buffer.AppendLine($"Select @Minutes = 0");
                buffer.AppendLine($"Insert Into Attend_Detail(DeptId, DeptName, EmpId, EmpCode, EmpName, RecDate, Record, SignIn, IOCount, Minutes)");
                buffer.AppendLine($"values(@DeptId, @DeptName, @EmpId, @EmpCode, @EmpName, '{recDate}', @Record, @SignIn, @IOCount, @Minutes)");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 生成考勤数据
        public static void CreateAttendData(EmpInfo emp, string beginDate, string endDate, string attendBeginTime, int attendEndDay, string attendEndTime)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("Attend_CreateAttendData");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, emp.EmpId);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                dbHelper.AddInParameter(cmd, "@BeginTime", DbType.String, attendBeginTime);
                dbHelper.AddInParameter(cmd, "@EndDay", DbType.Int32, attendEndDay);
                dbHelper.AddInParameter(cmd, "@EndTime", DbType.String, attendEndTime);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 考勤明细表
        public static DataTable GetAttendDetail(string beginDate, string endDate, int deptId, int deptType, string empCode, string empName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("Attend_ReportOfAttendDetail");
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptType", DbType.Int32, deptType);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 部门考勤汇总表
        public static DataTable GetDeptAttendSummaryDept(string beginDate, string endDate, int deptId, int deptType)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("Attend_ReportOfSummaryDept");
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptType", DbType.Int32, deptType);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取个人考勤汇总表
        public static DataTable GetDeptAttendSummaryPersonal(string beginDate, string endDate, string empCode, string empName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("Attend_ReportOfSummaryPersonal");
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region  原始刷卡表
        public static DataTable GetOriginalRecord(string beginDate, string endDate, int deptId, int deptType, string empCode, string empName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("Attend_GetOriginalRecord");
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptType", DbType.Int32, deptType);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取时间记录
        public static List<AttdRecord> GetAttdRecords(int empId, DateTime date, string beginTime, int secondDay, string endTime)
        {
            List<AttdRecord> recordList = new List<AttdRecord>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql;
                if (secondDay == 0)
                    sql = string.Format("Select * from Attend_Data Where EmpId = {0} and RecDate = '{1}' and RecTime >= '{2}' and RecTime < '{3}' Order By RecDate,RecTime ", empId, date.ToString("yyyy-MM-dd"), beginTime, endTime);
                else
                    sql = string.Format("Select * from Attend_Data Where EmpId = {0} and RecDate = '{1}' and RecTime >= '{2}' Or (RecDate ='{3}' and RecTime < '{4}') Order By RecDate,RecTime ", empId, date.ToString("yyyy-MM-dd"), beginTime, date.Year + "-" + date.Month + "-" + (date.Day + 1), endTime); ;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    AttdRecord record = new AttdRecord();
                    record.RecId = Convert.ToInt32(row["RecId"]);
                    record.DeptId = Convert.ToInt32(row["DeptId"]);
                    record.DeptName = row["DeptName"].ToString();
                    record.EmpId = Convert.ToInt32(row["EmpId"]);
                    record.EmpCode = row["EmpCode"].ToString();
                    record.EmpName = row["EmpName"].ToString();
                    record.CardNo = row["CardNo"].ToString();
                    record.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    record.DeviceName = row["DeviceName"].ToString();
                    record.RecDate = row["RecDate"].ToString();
                    record.IOFlag = row["IOFlag"].ToString();
                    record.RecTime = row["RecTime"].ToString();
                    recordList.Add(record);
                }
            }
            return recordList;
        }

        #endregion
    }
}
