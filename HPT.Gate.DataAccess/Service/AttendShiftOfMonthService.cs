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
    class AttendShiftOfMonthService
    {
        #region 插入数据
        public static void Insert(AttendShiftOfMonth shift)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int groupId1 = shift.TimeGroup1 == null ? 0 : shift.TimeGroup1.GroupId;
                int groupId2 = shift.TimeGroup2 == null ? 0 : shift.TimeGroup2.GroupId;
                int groupId3 = shift.TimeGroup3 == null ? 0 : shift.TimeGroup3.GroupId;
                string sql = $"Delete From AttendShiftOfMonth Where ShiftId={shift.ShiftId} And DayId ={shift.DayId}";
                sql += $"{Environment.NewLine}Insert Into AttendShiftOfMonth(ShiftId,DayId,TimeGroup1,TimeGroup2,TimeGroup3)";
                sql += $"{Environment.NewLine}Values({shift.ShiftId},{shift.DayId},{groupId1},{groupId2},{groupId3})";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取对应班次的时间段列表
        internal static List<AttendShiftOfMonth> GetByShiftId(int shiftId)
        {
            return ToList().Where(p => p.ShiftId == shiftId).ToList();
        }
        #endregion

        #region 获取全部列表
        internal static List<AttendShiftOfMonth> ToList()
        {
            List<AttendShiftOfMonth> monthList = new List<AttendShiftOfMonth>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From AttendShiftOfMonth";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<TimeGroupOfShift> shiftList = TimeGroupOfShiftService.ToList();
                foreach (DataRow row in dt.Rows)
                {
                    AttendShiftOfMonth month = new AttendShiftOfMonth();
                    month.ShiftId = Convert.ToInt32(row["ShiftId"]);
                    month.DayId = Convert.ToInt32(row["DayId"]);
                    int groupId1 = Convert.ToInt32(row["TimeGroup1"]);
                    if (groupId1 > 0)
                        month.TimeGroup1 = shiftList.FirstOrDefault(p => p.GroupId == groupId1);
                    int groupId2 = Convert.ToInt32(row["TimeGroup2"]);
                    if (groupId2 > 0)
                        month.TimeGroup2 = shiftList.FirstOrDefault(p => p.GroupId == groupId2);
                    int groupId3 = Convert.ToInt32(row["TimeGroup3"]);
                    if (groupId3 > 0)
                        month.TimeGroup3 = shiftList.FirstOrDefault(p => p.GroupId == groupId3);
                    monthList.Add(month);
                }
            }
            return monthList;
        }
        #endregion



    }
}
