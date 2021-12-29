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
    public class AttendShiftService
    {

        #region 获取班次列表
        public static List<AttendShift> ToList()
        {
            List<AttendShift> shiftList = new List<AttendShift>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From AttendShift";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    AttendShift shift = new AttendShift();
                    shift.ShiftId = Convert.ToInt32(row["ShiftId"]);
                    shift.ShiftType = Convert.ToInt32(row["ShiftType"]);
                    shift.ShiftName = row["ShiftName"].ToString();
                    if (shift.ShiftType == 0)
                        shift.ShiftOfWeek = AttendShiftOfWeekService.GetByShiftId(shift.ShiftId);
                    else
                        shift.ShiftOfMonth = AttendShiftOfMonthService.GetByShiftId(shift.ShiftId);
                    shiftList.Add(shift);
                }
            }
            return shiftList;
        }
        #endregion

        #region 获取班次详细信息
        public static AttendShift GetByShiftId(int shiftId)
        {
            return ToList().FirstOrDefault(p => p.ShiftId == shiftId);
        }
        #endregion



        #region 添加班次
        public static void Insert(AttendShift attendShift)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From AttendShift Where ShiftId ={attendShift.ShiftId}";
                sql += $"{Environment.NewLine}Insert Into AttendShift(ShiftId,ShiftType,ShiftName)";
                sql += $"{Environment.NewLine}Values({attendShift.ShiftId},{attendShift.ShiftType},'{attendShift.ShiftName}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
            if (attendShift.ShiftType == 0)
            {
                foreach (AttendShiftOfWeek week in attendShift.ShiftOfWeek)
                {
                    AttendShiftOfWeekService.Insert(week);
                }
            }
            if (attendShift.ShiftType == 1)
            {
                foreach (AttendShiftOfMonth month in attendShift.ShiftOfMonth)
                {
                    AttendShiftOfMonthService.Insert(month);
                }
            }
        }
        #endregion

        #region 获取班次编号
        public static int GetShiftId()
        {
            int shiftId = 0;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select IsNull(Max(ShiftId),0)+1 From AttendShift";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    shiftId = Convert.ToInt32(row[0]);
                }
            }
            return shiftId;
        }

        #endregion

        #region 删除排班信息
        public static void Del(int shiftId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From AttendShift Where ShiftId={shiftId}";
                sql += $"{Environment.NewLine}Delete From AttendShiftOfWeek Where ShiftId ={shiftId}";
                sql += $"{Environment.NewLine}Delete From AttendShiftOfMonth Where ShiftId ={shiftId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


    }
}
