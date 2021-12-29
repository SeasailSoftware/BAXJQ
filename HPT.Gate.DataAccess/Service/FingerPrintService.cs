using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class FingerPrintService
    {
        #region 获取全部指纹
        public static List<FingerPrint> ToList()
        {
            List<FingerPrint> fingerList = new List<FingerPrint>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select EmpId ,FingerData1 From EmpInfo Where Status = 0";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    var fingerPrint = new FingerPrint();
                    fingerPrint.EmpId = Convert.ToInt32(row["EmpId"]);
                    fingerPrint.FingerData = (byte[])row["FingerData1"];
                    if (fingerPrint.FingerData != null && fingerPrint.FingerData.Length > 100)
                        fingerList.Add(fingerPrint);
                }
            }
            return fingerList.OrderBy(p => p.FingerId).ToList();
        }

        #endregion

        #region 获取人员指纹信息
        public static List<FingerPrint> GetByEmpId(int empId)
        {
            return ToList().Where(s => s.EmpId == empId).ToList();
        }

        #endregion

        #region 检查指纹信息是否已经存在
        public static bool CheckExists(int fingerId)
        {
            return ToList().Exists(s => s.FingerId == fingerId);
        }
        #endregion

        #region 删除指纹
        public static void Del(int empId, int fId, int positionId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From FingerPrint Where EmpId ={empId} and FingerId ={fId} and PositionId ={positionId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static bool CheckTableExists()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"if  not exists (select * from sysobjects where objectproperty(object_id('FingerPrintErrorTask'),'istable') = 1)";
                sql += $"{Environment.NewLine}Begin";
                sql += $"{Environment.NewLine}CREATE TABLE[dbo].[FingerPrintErrorTask](";
                sql += $"{Environment.NewLine}[RecId][int] IDENTITY(1, 1) NOT NULL,";
                sql += $"{Environment.NewLine}[EmpId] [int] NOT NULL,";
                sql += $"{Environment.NewLine}[FingerId] [int] NOT NULL,";
                sql += $"{Environment.NewLine}[FingerData] [image]";
                sql += $"{Environment.NewLine}NOT NULL,";
                sql += $"{Environment.NewLine}[UpdateFlag] [int] NOT NULL";
                sql += $"{Environment.NewLine}) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]";
                sql += $"{Environment.NewLine}End";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
                return true;
            }
        }
        #endregion

    }
}
