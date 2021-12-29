using HPT.Gate.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Service
{
    public class LcdService
    {
        #region 获取最后一条记录
        public static List<LcdRecord> GetLastRecord()
        {
            List<LcdRecord> recordList = new List<LcdRecord>();
            using (OleDbHelper dbHelper = new OleDbHelper())
            {
                try
                {
                    string sql = "GetRecentRecords";
                    DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        LcdRecord record = new LcdRecord();
                        string deptId = row["DeptId"].ToString();
                        record.DeptId = Convert.ToInt32(string.IsNullOrWhiteSpace(deptId) ? "0" : deptId);
                        record.DeptName = row["DeptName"].ToString();
                        string empId = row["EmpId"].ToString();
                        record.EmpId = Convert.ToInt32(string.IsNullOrWhiteSpace(empId) ? "0" : empId);
                        record.EmpName = row["EmpName"].ToString();
                        record.EmpCode = row["EmpCode"].ToString();
                        record.CardNo = row["CardNo"].ToString();
                        record.IOFlag = row["IOFlag"].ToString();
                        record.RecordType = row["RecordType"].ToString();
                        record.RecTime = row["RecDateTime"].ToString();
                        record.PhotoStream = (byte[])row["Photo"];
                        recordList.Add(record);
                    }
                }
                catch
                {

                }
            }
            return recordList;
        }

        internal static byte[] GetPhotoByEmpId(int empId)
        {
            byte[] arr = new byte[] { 0x00 };
            using (OleDbHelper dbHelper = new OleDbHelper())
            {
                string sql = $"Select Photo From EmpInfo where EmpId ={empId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    arr = (byte[])row[0];
                }
            }
            return arr;
        }

        public static DataTable GetCountOfDept()
        {
            using (OleDbHelper dbHelper = new OleDbHelper())
            {
                string sql = "GetIOCount";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion
    }
}
