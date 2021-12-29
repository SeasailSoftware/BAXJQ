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
    public class AttendDataService
    {
        #region 获取时间段内的所有考勤数据
        public static List<AttendData> ToList(string beginTime, string endTime)
        {
            List<AttendData> dataList = new List<AttendData>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From (Select c.DeptId, c.DeptName,b.EmpCode,b.EmpName,a.* From AttendData a,EmpInfo b,DeptInfo c";
                sql += $"{Environment.NewLine}Where  a.EmpId = b.EmpId and b.DeptId = c.DeptId And a.RecDatetime>='{beginTime}' And a.RecDatetime<='{endTime}') e Left JoIn DeviceInfo d";
                sql += $"{Environment.NewLine} On e.DeviceId = d.DeviceId  Order By e.EmpId,e.RecDatetime";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    AttendData data = new AttendData();
                    data.RecId = Convert.ToInt32(row["RecId"]);
                    data.DeptId = Convert.ToInt32(row["DeptId"]);
                    data.DeptName = row["DeptName"].ToString();
                    data.EmpId = Convert.ToInt32(row["EmpId"]);
                    data.EmpCode = row["EmpCode"].ToString();
                    data.EmpName = row["EmpName"].ToString();
                    data.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    data.DeviceName = row["DeviceName"].ToString();
                    data.CardNo = row["CardNo"].ToString();
                    data.RecDatetime = Convert.ToDateTime(row["RecDateTime"]);
                    data.IOFlag = row["IOFlag"].ToString();
                    data.Passed = Convert.ToInt32(row["Passed"]);
                    dataList.Add(data);
                }
            }
            return dataList;
        }

        #endregion

        #region 查询考勤数据
        public static List<AttendData> Find(int deptId, int deptType, string empCode, string empName, string beginDate, string endDate)
        {
            List<AttendData> list = new List<AttendData>();
            List<AttendData> detailList = ToList(beginDate, endDate);
            List<EmpInfo> empList = EmpInfoService.Find(deptId, deptType, empCode, empName, "");
            foreach (EmpInfo emp in empList)
            {
                List<AttendData> details = detailList.Where(p => p.EmpId == emp.EmpId).ToList();
                list.AddRange(details);
            }
            return list;
        }
        #endregion


        #region 获取某个人的考勤数据
        public static List<AttendData> Find(int empId, string beginDate, string endDate)
        {
            return ToList(beginDate, endDate).Where(p => p.EmpId == empId).ToList();
        }
        #endregion


        #region 补签卡
        public static void Insert(AttendData data)
        {
            EmpInfo emp = EmpInfoService.GetByEmpId(data.EmpId);
            data.CardNo = emp.ICCardNo;
            data.IOFlag = "进";
            data.Passed = 0;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Insert Into AttendData(EmpId,DeviceId,CardNo,RecDatetime,IOFlag,Passed)";
                sql += $"{Environment.NewLine} Values({data.EmpId},{data.DeviceId},'{data.CardNo}','{data.RecDatetime.ToString("yyyy-MM-dd HH:mm:ss")}','{data.IOFlag}',{data.Passed})";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 修改考勤记录
        public static void Update(AttendData data)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update AttendData Set RecDateTime ='{data.RecDatetime}' Where RecId ={data.RecId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除考勤数据
        public static void Del(int recId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From AttendData Where RecId ={recId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
