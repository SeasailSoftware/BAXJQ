using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;

namespace HPT.Gate.DataAccess.Service
{
    public class AttendDBService
    {

        #region 更新数据库版本号
        /// <summary>
        /// 更新数据库版本号
        /// </summary>
        /// <param name="version"></param>
        internal static void UpdateVersion(int version)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = " If Not Exists(Select top 1 * From DBVersion) ";
                sql += string.Format(" Insert Into DBVersion(Version) values({0}) ", version);
                sql += " Else ";
                sql += string.Format(" Update DBVersion Set Version = {0} ", version);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 执行数据库更新
        /// <summary>
        /// 执行数据库更新
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="progress"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static bool ExecuteSqlFile(SQLiteHelper dbHelper, string sql)
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

        #region 检查数据库版本
        internal static int CheckVersion()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int version = 0;
                try
                {
                    string sql = "if Not exists(select 1 from sysobjects where name = 'DBVersion')";
                    sql += " Begin ";
                    sql += " CREATE TABLE[dbo].[DBVersion]([RecId][int] IDENTITY(1, 1) NOT NULL,[Version] [int] NOT NULL) ON[PRIMARY] ";
                    sql += " Insert DBVersion(Version) Values(0) ";
                    sql += " End ";
                    sql += " Select Top 1 Version From DBVersion ";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        version = Convert.ToInt32(row["Version"]);
                    }
                }
                catch
                {

                }
                return version;
            }
        }
        #endregion
        /// <summary>
        /// 获取班次列表
        /// </summary>
        public static DataTable GetShifts()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select SID,Case SType when 0 then '白班' else '夜班' end as SType,SName,BeginTime1,EndTime1,BeginTime2,EndTime2,BeginTime3,EndTime3, WorkHour from Shift";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 获取可用的班次编号
        /// </summary>
        /// <returns></returns>
        public static int GetUseAbleShiftId()
        {
            int shiftId = 1;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select isnull(max(SID)+1,1) As ShiftId from Shift ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    shiftId = Convert.ToInt32(row["ShiftId"]);
                }
            }
            return shiftId;
        }

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
                DbCommand cmd = dbHelper.GetStoredProcCommond("Attend_InsertAttendDetailNoRecord");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@RecDate", DbType.String, recDate);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        /// <summary>
        /// 添加班次信息
        /// </summary>
        /// <param name="shift"></param>
        public static void InsertShift(Shift shift)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertShift");

                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 根据编号查找班次信息
        /// </summary>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        public static Shift GetShiftById(int shiftId)
        {
            Shift shift = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from Shift where SID =" + shiftId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    shift = new Shift();

                }
            }
            return shift;
        }

        /// <summary>
        /// 修改班次信息
        /// </summary>
        /// <param name="shift"></param>
        public static void UpdateShift(Shift shift)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateShift");

                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 插入考勤周期
        /// </summary>
        /// <param name="adc"></param>
        public static void InsertAttendCycle(AttendCycle adc)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertAttendCycle");
                dbHelper.AddInParameter(cmd, "@CName", DbType.String, adc.CName);
                dbHelper.AddInParameter(cmd, "@CType", DbType.Int32, adc.CType);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, adc.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, adc.EndDate);
                dbHelper.AddInParameter(cmd, "@Week1", DbType.Int32, adc.Week1);
                dbHelper.AddInParameter(cmd, "@Week2", DbType.Int32, adc.Week2);
                dbHelper.AddInParameter(cmd, "@Week3", DbType.Int32, adc.Week3);
                dbHelper.AddInParameter(cmd, "@Week4", DbType.Int32, adc.Week4);
                dbHelper.AddInParameter(cmd, "@Week5", DbType.Int32, adc.Week5);
                dbHelper.AddInParameter(cmd, "@Week6", DbType.Int32, adc.Week6);
                dbHelper.AddInParameter(cmd, "@Week7", DbType.Int32, adc.Week7);
                dbHelper.AddInParameter(cmd, "@Day1", DbType.Int32, adc.Day1);
                dbHelper.AddInParameter(cmd, "@Day2", DbType.Int32, adc.Day2);
                dbHelper.AddInParameter(cmd, "@Day3", DbType.Int32, adc.Day3);
                dbHelper.AddInParameter(cmd, "@Day4", DbType.Int32, adc.Day4);
                dbHelper.AddInParameter(cmd, "@Day5", DbType.Int32, adc.Day5);
                dbHelper.AddInParameter(cmd, "@Day6", DbType.Int32, adc.Day6);
                dbHelper.AddInParameter(cmd, "@Day7", DbType.Int32, adc.Day7);
                dbHelper.AddInParameter(cmd, "@Day8", DbType.Int32, adc.Day8);
                dbHelper.AddInParameter(cmd, "@Day9", DbType.Int32, adc.Day9);
                dbHelper.AddInParameter(cmd, "@Day10", DbType.Int32, adc.Day10);
                dbHelper.AddInParameter(cmd, "@Day11", DbType.Int32, adc.Day11);
                dbHelper.AddInParameter(cmd, "@Day12", DbType.Int32, adc.Day12);
                dbHelper.AddInParameter(cmd, "@Day13", DbType.Int32, adc.Day13);
                dbHelper.AddInParameter(cmd, "@Day14", DbType.Int32, adc.Day14);
                dbHelper.AddInParameter(cmd, "@Day15", DbType.Int32, adc.Day15);
                dbHelper.AddInParameter(cmd, "@Day16", DbType.Int32, adc.Day16);
                dbHelper.AddInParameter(cmd, "@Day17", DbType.Int32, adc.Day17);
                dbHelper.AddInParameter(cmd, "@Day18", DbType.Int32, adc.Day18);
                dbHelper.AddInParameter(cmd, "@Day19", DbType.Int32, adc.Day19);
                dbHelper.AddInParameter(cmd, "@Day20", DbType.Int32, adc.Day20);
                dbHelper.AddInParameter(cmd, "@Day21", DbType.Int32, adc.Day21);
                dbHelper.AddInParameter(cmd, "@Day22", DbType.Int32, adc.Day22);
                dbHelper.AddInParameter(cmd, "@Day23", DbType.Int32, adc.Day23);
                dbHelper.AddInParameter(cmd, "@Day24", DbType.Int32, adc.Day24);
                dbHelper.AddInParameter(cmd, "@Day25", DbType.Int32, adc.Day25);
                dbHelper.AddInParameter(cmd, "@Day26", DbType.Int32, adc.Day26);
                dbHelper.AddInParameter(cmd, "@Day27", DbType.Int32, adc.Day27);
                dbHelper.AddInParameter(cmd, "@Day28", DbType.Int32, adc.Day28);
                dbHelper.AddInParameter(cmd, "@Day29", DbType.Int32, adc.Day29);
                dbHelper.AddInParameter(cmd, "@Day30", DbType.Int32, adc.Day30);
                dbHelper.AddInParameter(cmd, "@Day31", DbType.Int32, adc.Day31);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 修改考勤周期
        /// </summary>
        /// <param name="adc"></param>
        public static void UpdateAttendCycle(AttendCycle adc)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateAttendCycle");
                dbHelper.AddInParameter(cmd, "@CID", DbType.Int32, adc.CID);
                dbHelper.AddInParameter(cmd, "@CName", DbType.String, adc.CName);
                dbHelper.AddInParameter(cmd, "@CType", DbType.Int32, adc.CType);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, adc.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, adc.EndDate);
                dbHelper.AddInParameter(cmd, "@Week1", DbType.Int32, adc.Week1);
                dbHelper.AddInParameter(cmd, "@Week2", DbType.Int32, adc.Week2);
                dbHelper.AddInParameter(cmd, "@Week3", DbType.Int32, adc.Week3);
                dbHelper.AddInParameter(cmd, "@Week4", DbType.Int32, adc.Week4);
                dbHelper.AddInParameter(cmd, "@Week5", DbType.Int32, adc.Week5);
                dbHelper.AddInParameter(cmd, "@Week6", DbType.Int32, adc.Week6);
                dbHelper.AddInParameter(cmd, "@Week7", DbType.Int32, adc.Week7);
                dbHelper.AddInParameter(cmd, "@Day1", DbType.Int32, adc.Day1);
                dbHelper.AddInParameter(cmd, "@Day2", DbType.Int32, adc.Day2);
                dbHelper.AddInParameter(cmd, "@Day3", DbType.Int32, adc.Day3);
                dbHelper.AddInParameter(cmd, "@Day4", DbType.Int32, adc.Day4);
                dbHelper.AddInParameter(cmd, "@Day5", DbType.Int32, adc.Day5);
                dbHelper.AddInParameter(cmd, "@Day6", DbType.Int32, adc.Day6);
                dbHelper.AddInParameter(cmd, "@Day7", DbType.Int32, adc.Day7);
                dbHelper.AddInParameter(cmd, "@Day8", DbType.Int32, adc.Day8);
                dbHelper.AddInParameter(cmd, "@Day9", DbType.Int32, adc.Day9);
                dbHelper.AddInParameter(cmd, "@Day10", DbType.Int32, adc.Day10);
                dbHelper.AddInParameter(cmd, "@Day11", DbType.Int32, adc.Day11);
                dbHelper.AddInParameter(cmd, "@Day12", DbType.Int32, adc.Day12);
                dbHelper.AddInParameter(cmd, "@Day13", DbType.Int32, adc.Day13);
                dbHelper.AddInParameter(cmd, "@Day14", DbType.Int32, adc.Day14);
                dbHelper.AddInParameter(cmd, "@Day15", DbType.Int32, adc.Day15);
                dbHelper.AddInParameter(cmd, "@Day16", DbType.Int32, adc.Day16);
                dbHelper.AddInParameter(cmd, "@Day17", DbType.Int32, adc.Day17);
                dbHelper.AddInParameter(cmd, "@Day18", DbType.Int32, adc.Day18);
                dbHelper.AddInParameter(cmd, "@Day19", DbType.Int32, adc.Day19);
                dbHelper.AddInParameter(cmd, "@Day20", DbType.Int32, adc.Day20);
                dbHelper.AddInParameter(cmd, "@Day21", DbType.Int32, adc.Day21);
                dbHelper.AddInParameter(cmd, "@Day22", DbType.Int32, adc.Day22);
                dbHelper.AddInParameter(cmd, "@Day23", DbType.Int32, adc.Day23);
                dbHelper.AddInParameter(cmd, "@Day24", DbType.Int32, adc.Day24);
                dbHelper.AddInParameter(cmd, "@Day25", DbType.Int32, adc.Day25);
                dbHelper.AddInParameter(cmd, "@Day26", DbType.Int32, adc.Day26);
                dbHelper.AddInParameter(cmd, "@Day27", DbType.Int32, adc.Day27);
                dbHelper.AddInParameter(cmd, "@Day28", DbType.Int32, adc.Day28);
                dbHelper.AddInParameter(cmd, "@Day29", DbType.Int32, adc.Day29);
                dbHelper.AddInParameter(cmd, "@Day30", DbType.Int32, adc.Day30);
                dbHelper.AddInParameter(cmd, "@Day31", DbType.Int32, adc.Day31);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        /// 获取考勤周期列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAttendCycleList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select CID,CName,case CType when 0 then '星期排班' else '月排班' end as CType,BeginDate,EndDate from AttendCycle ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }


        /// <summary>
        /// 根据编号查找考勤周期
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static AttendCycle GetAttendCycleById(int cid)
        {
            AttendCycle attdCycle = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select *  from AttendCycle  where CID = " + cid.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    attdCycle = new AttendCycle();
                    attdCycle.CID = cid;
                    attdCycle.CName = row["CName"].ToString();
                    attdCycle.CType = Convert.ToInt32(row["CType"]);
                    attdCycle.BeginDate = row["BeginDate"].ToString();
                    attdCycle.EndDate = row["EndDate"].ToString();
                    attdCycle.Week1 = Convert.ToInt32(row["Week1"]);
                    attdCycle.Week2 = Convert.ToInt32(row["Week2"]);
                    attdCycle.Week3 = Convert.ToInt32(row["Week3"]);
                    attdCycle.Week4 = Convert.ToInt32(row["Week4"]);
                    attdCycle.Week5 = Convert.ToInt32(row["Week5"]);
                    attdCycle.Week6 = Convert.ToInt32(row["Week6"]);
                    attdCycle.Week7 = Convert.ToInt32(row["Week7"]);
                    attdCycle.Day1 = Convert.ToInt32(row["Day1"]);
                    attdCycle.Day2 = Convert.ToInt32(row["Day2"]);
                    attdCycle.Day3 = Convert.ToInt32(row["Day3"]);
                    attdCycle.Day4 = Convert.ToInt32(row["Day4"]);
                    attdCycle.Day5 = Convert.ToInt32(row["Day5"]);
                    attdCycle.Day6 = Convert.ToInt32(row["Day6"]);
                    attdCycle.Day7 = Convert.ToInt32(row["Day7"]);
                    attdCycle.Day8 = Convert.ToInt32(row["Day8"]);
                    attdCycle.Day9 = Convert.ToInt32(row["Day9"]);
                    attdCycle.Day10 = Convert.ToInt32(row["Day10"]);

                    attdCycle.Day11 = Convert.ToInt32(row["Day11"]);
                    attdCycle.Day12 = Convert.ToInt32(row["Day12"]);
                    attdCycle.Day13 = Convert.ToInt32(row["Day13"]);
                    attdCycle.Day14 = Convert.ToInt32(row["Day14"]);
                    attdCycle.Day15 = Convert.ToInt32(row["Day15"]);
                    attdCycle.Day16 = Convert.ToInt32(row["Day16"]);
                    attdCycle.Day17 = Convert.ToInt32(row["Day17"]);
                    attdCycle.Day18 = Convert.ToInt32(row["Day18"]);
                    attdCycle.Day19 = Convert.ToInt32(row["Day19"]);
                    attdCycle.Day20 = Convert.ToInt32(row["Day20"]);
                    attdCycle.Day21 = Convert.ToInt32(row["Day21"]);
                    attdCycle.Day22 = Convert.ToInt32(row["Day22"]);
                    attdCycle.Day23 = Convert.ToInt32(row["Day23"]);
                    attdCycle.Day24 = Convert.ToInt32(row["Day24"]);
                    attdCycle.Day25 = Convert.ToInt32(row["Day25"]);
                    attdCycle.Day26 = Convert.ToInt32(row["Day26"]);
                    attdCycle.Day27 = Convert.ToInt32(row["Day27"]);
                    attdCycle.Day28 = Convert.ToInt32(row["Day28"]);
                    attdCycle.Day29 = Convert.ToInt32(row["Day29"]);
                    attdCycle.Day30 = Convert.ToInt32(row["Day30"]);
                    attdCycle.Day31 = Convert.ToInt32(row["Day31"]);
                }
            }
            return attdCycle;
        }

        /// <summary>
        /// 删除考勤周期
        /// </summary>
        /// <param name="cid"></param>
        public static void DeleteAttendCycle(int cid)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("DeleteAttendCycle");
                dbHelper.AddInParameter(cmd, "@CID", DbType.Int32, cid);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 插入排班表
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        public static void InsertShiftOfEmp(int empId, string beginDate, string endDate, int type, int id, SQLiteHelper dbHelper)
        {
            DateTime dtBegin = Convert.ToDateTime(beginDate);
            DateTime dtEnd = Convert.ToDateTime(endDate);
            for (DateTime d = dtBegin; d <= dtEnd; d = d.AddDays(1))
            {
                switch (type)
                {
                    case 0:
                        AttendCycle attCycle = AttendDBService.GetAttendCycleById(id);
                        int sid = attCycle.GetShiftIdByDateTime(d);
                        InsertShiftOfEmp(empId, d, sid, dbHelper);
                        break;
                    case 1:
                        InsertShiftOfEmp(empId, d, id, dbHelper);
                        break;
                }

            }
        }
        /// <summary>
        /// 插入每天排班表
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="d"></param>
        /// <param name="id"></param>
        /// <param name="dbHelper"></param>
        private static void InsertShiftOfEmp(int empId, DateTime d, int id, SQLiteHelper dbHelper)
        {
            DbCommand cmd = dbHelper.GetStoredProcCommond("InsertShiftOfEmp");
            dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
            dbHelper.AddInParameter(cmd, "@RecDate", DbType.String, d.ToString("yyyy-MM-dd"));
            dbHelper.AddInParameter(cmd, "@Sid", DbType.Int32, id);
            dbHelper.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 查询排班信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="deptType"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetShiftOfEmp(int deptId, int deptType, string empCode, string empName, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Empty;
                switch (deptType)
                {
                    case 0:
                        sql = "GetShiftOfDept";
                        break;
                    case 1:
                        sql = "GetShiftOfDeptAll";
                        break;
                }
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 获取考勤制度信息
        /// </summary>
        /// <returns></returns>
        public static AttdRule GetAttendRules()
        {
            AttdRule attendRule = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from AttendRule where RID = 1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    attendRule = new AttdRule();

                }
            }
            return attendRule;
        }

        /// <summary>
        /// 修改考勤制度
        /// </summary>
        /// <param name="attendRule"></param>
        public static void UpdateAttendRule(AttdRule attendRule)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateAttendRule");

                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 得到考勤分析的人员列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="deptType"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public static List<EmpInfo> GetAttendProcEmpList(int deptId, int deptType, string empCode, string empName)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("Attend_GetProcEmpList");
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptType", DbType.Int32, deptType);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    EmpInfo emp = new EmpInfo();
                    emp.DeptId = deptId;
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    empList.Add(emp);
                }
            }
            return empList;
        }

        /// <summary>
        /// 考勤分析处理
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dbHelper"></param>
        public static void ProAttendDataOfEmp(EmpInfo emp, string beginDate, string endDate, SQLiteHelper dbHelper)
        {

            DateTime dtBegin = Convert.ToDateTime(beginDate);
            DateTime dtEnd = Convert.ToDateTime(endDate);
            for (DateTime d = dtBegin; d <= dtEnd; d = d.AddDays(1))
            {
                DateTime dt1 = DateTime.Now;
                DbCommand cmd = dbHelper.GetStoredProcCommond("AttendProc");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, emp.EmpId);
                dbHelper.AddInParameter(cmd, "@RecDate", DbType.String, d.ToString("yyyy-MM-dd"));
                dbHelper.ExecuteNonQuery(cmd);
                DateTime dt2 = DateTime.Now;
                TimeSpan tsBegin = new TimeSpan(dt1.Ticks);
                TimeSpan tsEnd = new TimeSpan(dt2.Ticks);
                TimeSpan tsBetween = tsEnd.Subtract(tsBegin).Duration();
                float value = tsBetween.Hours * 60 * 60 + tsBetween.Minutes * 60 + tsBetween.Seconds + (float)tsBetween.Milliseconds / 1000;
                Console.WriteLine("\r\n 用时:" + value.ToString() + "秒");
            }
        }

        /// <summary>
        /// 原始刷卡记录报表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptId"></param>
        /// <param name="deptType"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 考勤明细表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptId"></param>
        /// <param name="deptType"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除班次信息
        /// </summary>
        /// <param name="shiftId"></param>
        public static void DeleteShift(int shiftId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("DeleteShift");
                dbHelper.AddInParameter(cmd, "@ShiftId", DbType.Int32, shiftId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static void InsertRecord(string cardNo, string beginTime1)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertRecord");
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.AddInParameter(cmd, "@RecordType", DbType.Int32, 0);
                dbHelper.AddInParameter(cmd, "@RecDateTime", DbType.String, beginTime1);
                dbHelper.AddInParameter(cmd, "@IOFlag", DbType.Int32, 3);
                dbHelper.AddInParameter(cmd, "@PlaceId", DbType.Int32, 0);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, 100);
                dbHelper.AddInParameter(cmd, "@RecPointer", DbType.Int32, 100);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 生成考勤数据
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dbHelper"></param>
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

        /// <summary>
        /// 从门禁记录插入考勤数据
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dbHelper"></param>
        public static void InsertAttendData(EmpInfo emp, string beginDate, string endDate, SQLiteHelper dbHelper)
        {
            DateTime dtBegin = Convert.ToDateTime(beginDate);
            DateTime dtEnd = Convert.ToDateTime(endDate);
            for (DateTime d = dtBegin; d <= dtEnd; d = d.AddDays(1))
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertAttendData");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, emp.EmpId);
                dbHelper.AddInParameter(cmd, "@RecDate", DbType.String, d.ToString("yyyy-MM-dd"));
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 考勤汇总表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptId"></param>
        /// <param name="deptType"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
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

        #region 获取个人考勤汇总表
        /// <summary>
        /// 获取个人考勤汇总表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 得到人员某天的班次信息
        /// </summary>
        /// <param name="p"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetEmpShift(int empId, DateTime date)
        {
            int shiftId = -1;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetShiftOfEmp";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@Date", DbType.String, date.ToString("yyyy-MM-dd"));
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    shiftId = Convert.ToInt32(row["ShiftId"]);
                }
            }
            return shiftId;
        }

        /// <summary>
        /// 查询人员在白班日期范围内的考勤记录
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<DateTime> GetRecDayShiftDatetime(EmpInfo emp, DateTime beginDate, DateTime endDate)
        {
            List<DateTime> timeList = new List<DateTime>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select RecTime From AttendData where EmpId = " + emp.EmpId + "  And  RecDate >= '" + beginDate.ToString("yyyy-MM-dd") + "'   and RecDate <=  '" + endDate.ToString("yyyy-MM-dd") + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    DateTime time = Convert.ToDateTime(row["RecTime"]);
                    timeList.Add(time);
                }
            }
            return timeList;
        }

        /// <summary>
        /// 查询人员在白班日期范围内的考勤记录
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<DateTime> GetRecNightShiftDatetime(EmpInfo emp, DateTime beginDate, DateTime endDate, string time1, string time2)
        {
            List<DateTime> timeList = new List<DateTime>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select RecTime From AttendData where EmpId = " + emp.EmpId + "  And  RecDate = '" + beginDate.ToString("yyyy-MM-dd") + "' ";
                sql += "  and  RecTime >='" + time1 + "' ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    DateTime time = Convert.ToDateTime(beginDate.ToString("yyyy-MM-dd") + " " + row["RecTime"]);
                    timeList.Add(time);
                }
            }
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select RecTime From AttendData where EmpId = " + emp.EmpId + "  and RecDate =  '" + endDate.ToString("yyyy-MM-dd") + "'";
                sql += "  and RecTime <= '" + time2 + "' ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    DateTime time = Convert.ToDateTime(endDate.ToString("yyyy-MM-dd") + " " + row["RecTime"]);
                    timeList.Add(time);
                }
            }
            return timeList;
        }

        #region 插入明细表
        /// <summary>
        /// 插入人员当天的考勤明细
        /// </summary>
        /// <param name="attendDetail"></param>
        public static void InsertAttendDetail(AttdDetail attendDetail)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Delete From AttendDetail where EmpId = " + attendDetail.EmpId + " and  RecDate = '" + attendDetail.RecDate + "'    ";
                sql += "INSERT INTO [HPTGate_S_V_2_2].[dbo].[AttendDetail]([EmpId],[RecDate],[BeginTime1],[EndTime1] ,[BeginTime2] ,[EndTime2],[BeginTime3],[EndTime3],[Sid],[Sname],[Late],[LeaveEarly],[Absent],[Vacation],[Ot],[WorkHour])";
                sql += "  Values (" + attendDetail.EmpId + ",";
                sql += "'" + attendDetail.RecDate + "',";
                sql += "'" + attendDetail.BeginTime1 + "',";
                sql += "'" + attendDetail.EndTime1 + "',";
                sql += "'" + attendDetail.BeginTime2 + "',";
                sql += "'" + attendDetail.EndTime2 + "',";
                sql += "'" + attendDetail.BeginTime3 + "',";
                sql += "'" + attendDetail.EndTime3 + "',";
                sql += attendDetail.Sid + ",";
                sql += "'" + attendDetail.Sname + "',";
                sql += attendDetail.Late + ",";
                sql += attendDetail.LeaveEarly + ",";
                sql += attendDetail.Absent + ",";
                sql += attendDetail.Vacation + ",";
                sql += attendDetail.Ot + ",";
                sql += attendDetail.WorkHour + ")";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


    }
}
