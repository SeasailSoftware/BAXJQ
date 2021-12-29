using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace HPT.Gate.DataAccess.Service
{
    public class AttendDetailService
    {

        #region 删除考勤明细
        public static void Del(int empId, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From AttendDetail Where EmpId ={empId} And RecDate>='{beginDate}' And RecDate <'{endDate}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 插入考勤明细
        public static void Insert(List<AttendDetail> detailList,string connectString)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeptId", typeof(int));
            dt.Columns.Add("DeptName", typeof(string));
            dt.Columns.Add("EmpId", typeof(int));
            dt.Columns.Add("EmpCode", typeof(string));
            dt.Columns.Add("EmpName", typeof(string));
            dt.Columns.Add("RecDate", typeof(string));
            dt.Columns.Add("GroupId", typeof(int));
            dt.Columns.Add("GroupName", typeof(string));
            dt.Columns.Add("TimeOfSignIn", typeof(string));
            dt.Columns.Add("TimeOfSignOut", typeof(string));
            dt.Columns.Add("SignIn", typeof(string));
            dt.Columns.Add("SignOut", typeof(string));
            dt.Columns.Add("ShouldAttd", typeof(double));
            dt.Columns.Add("Attded", typeof(double));
            dt.Columns.Add("LateMinutes", typeof(int));
            dt.Columns.Add("EarlyMinutes", typeof(int));
            dt.Columns.Add("Absent", typeof(int));
            dt.Columns.Add("OTMinutes", typeof(int));
            dt.Columns.Add("WorkMinutes", typeof(int));
            dt.Columns.Add("ShouldSignIn", typeof(int));
            dt.Columns.Add("ShouldSignOut", typeof(int));
            foreach (AttendDetail detail in detailList)
            {

                DataRow row = dt.NewRow();
                row["DeptId"] = detail.DeptId;
                row["DeptName"] = detail.DeptName;
                row["EmpId"] = detail.EmpId;
                row["EmpCode"] = detail.EmpCode;
                row["EmpName"] = detail.EmpName;
                row["RecDate"] = detail.RecDate;
                row["GroupId"] = detail.GroupId;
                row["GroupName"] = detail.GroupName;
                row["TimeOfSignIn"] = detail.TimeOfSignIn;
                row["TimeOfSignOut"] = detail.TimeOfSignOut;
                row["SignIn"] = detail.SignIn;
                row["SignOut"] = detail.SignOut;
                row["ShouldAttd"] = detail.ShouldAttd;
                row["Attded"] = detail.Attded;
                row["LateMinutes"] = detail.LateMinutes;
                row["EarlyMinutes"] = detail.EarlyMinutes;
                row["Absent"] = detail.Absent;
                row["OTMinutes"] = detail.OTMinutes;
                row["WorkMinutes"] = detail.WorkMinutes;
                row["ShouldSignIn"] = detail.ShouldSignIn;
                row["ShouldSignOut"] = detail.ShouldSignOut;
                dt.Rows.Add(row);
            }
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlBulkCopy bulk = new SqlBulkCopy(connectString))
                {
                    bulk.BatchSize = 1000;
                    bulk.DestinationTableName = "AttendDetail";
                    bulk.ColumnMappings.Add("DeptId", "DeptId");
                    bulk.ColumnMappings.Add("DeptName", "DeptName");
                    bulk.ColumnMappings.Add("EmpId", "EmpId");
                    bulk.ColumnMappings.Add("EmpCode", "EmpCode");
                    bulk.ColumnMappings.Add("EmpName", "EmpName");
                    bulk.ColumnMappings.Add("RecDate", "RecDate");
                    bulk.ColumnMappings.Add("GroupId", "GroupId");
                    bulk.ColumnMappings.Add("GroupName", "GroupName");
                    bulk.ColumnMappings.Add("TimeOfSignIn", "TimeOfSignIn");
                    bulk.ColumnMappings.Add("TimeOfSignOut", "TimeOfSignOut");
                    bulk.ColumnMappings.Add("SignIn", "SignIn");
                    bulk.ColumnMappings.Add("SignOut", "SignOut");
                    bulk.ColumnMappings.Add("ShouldAttd", "ShouldAttd");
                    bulk.ColumnMappings.Add("Attded", "Attded");
                    bulk.ColumnMappings.Add("LateMinutes", "LateMinutes");
                    bulk.ColumnMappings.Add("EarlyMinutes", "EarlyMinutes");
                    bulk.ColumnMappings.Add("Absent", "Absent");
                    bulk.ColumnMappings.Add("OTMinutes", "OTMinutes");
                    bulk.ColumnMappings.Add("WorkMinutes", "WorkMinutes");
                    bulk.ColumnMappings.Add("ShouldSignIn", "ShouldSignIn");
                    bulk.ColumnMappings.Add("ShouldSignOut", "ShouldSignOut");
                    bulk.WriteToServer(dt);
                }

            }
        }

        #endregion

        #region 查询考勤数据
        public static List<AttendDetail> Find(int deptId, int deptType, string empCode, string empName, string beginDate, string endDate)
        {
            List<AttendDetail> list = new List<AttendDetail>();
            List<AttendDetail> detailList = ToList(beginDate, endDate);
            List<EmpInfo> empList = EmpInfoService.Find(deptId, deptType, empCode, empName, "");
            foreach (EmpInfo emp in empList)
            {
                List<AttendDetail> details = detailList.Where(p => p.EmpId == emp.EmpId).ToList();
                list.AddRange(details);
            }
            return list;
        }
        #endregion

        #region 获取时间段内的考勤明细
        public static List<AttendDetail> ToList(string beginDate, string endDate)
        {
            List<AttendDetail> detailList = new List<AttendDetail>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From AttendDetail Where RecDate>='{beginDate}' And RecDate<='{endDate}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    AttendDetail detail = new AttendDetail();
                    detail.RecId = Convert.ToInt32(row["RecId"]);
                    detail.DeptId = Convert.ToInt32(row["DeptId"]);
                    detail.DeptName = row["DeptName"].ToString();
                    detail.EmpId = Convert.ToInt32(row["EmpId"]);
                    detail.EmpCode = row["EmpCode"].ToString();
                    detail.EmpName = row["EmpName"].ToString();
                    detail.RecDate = row["RecDate"].ToString();
                    detail.GroupId = Convert.ToInt32(row["GroupId"]);
                    detail.GroupName = row["GroupName"].ToString();
                    detail.TimeOfSignIn = row["TimeOfSignIn"].ToString();
                    detail.TimeOfSignOut = row["TimeOfSignOut"].ToString();
                    detail.SignIn = row["SignIn"].ToString();
                    detail.SignOut = row["SignOut"].ToString();
                    detail.ShouldAttd = Convert.ToDouble(row["ShouldAttd"]);
                    detail.Attded = Convert.ToDouble(row["Attded"]);
                    detail.LateMinutes = Convert.ToInt32(row["LateMinutes"]);
                    detail.EarlyMinutes = Convert.ToInt32(row["EarlyMinutes"]);
                    detail.Absent = Convert.ToInt32(row["Absent"]);
                    detail.OTMinutes = Convert.ToInt32(row["OTMinutes"]);
                    detail.WorkMinutes = Convert.ToInt32(row["WorkMinutes"]);
                    detail.ShouldSignIn = Convert.ToInt32(row["ShouldSignIn"]);
                    detail.ShouldSignOut = Convert.ToInt32(row["ShouldSignOut"]);
                    detailList.Add(detail);
                }
            }
            return detailList;
        }
        #endregion
    }
}

