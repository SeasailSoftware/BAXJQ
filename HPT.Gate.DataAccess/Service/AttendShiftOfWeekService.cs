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
    public class AttendShiftOfWeekService
    {
        #region 插入数据
        public static void Insert(AttendShiftOfWeek shift)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int groupId1 = shift.TimeGroup1 == null ? 0 : shift.TimeGroup1.GroupId;
                int groupId2 = shift.TimeGroup2 == null ? 0 : shift.TimeGroup2.GroupId;
                int groupId3 = shift.TimeGroup3 == null ? 0 : shift.TimeGroup3.GroupId;
                string sql = $"Delete From AttendShiftOfWeek Where ShiftId={shift.ShiftId} And WeekId ={shift.WeekId}";
                sql += $"{Environment.NewLine}Insert Into AttendShiftOfWeek(ShiftId,WeekId,TimeGroup1,TimeGroup2,TimeGroup3)";
                sql += $"{Environment.NewLine}Values({shift.ShiftId},{shift.WeekId},{groupId1},{groupId2},{groupId3})";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 根据班次编号获取时间段
        internal static List<AttendShiftOfWeek> GetByShiftId(int shiftId)
        {
            return ToList().Where(p => p.ShiftId == shiftId).ToList();
        }
        #endregion

        #region 获取全部列表
        private static List<AttendShiftOfWeek> ToList()
        {
            List<AttendShiftOfWeek> weekList = new List<AttendShiftOfWeek>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From AttendShiftOfWeek";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<TimeGroupOfShift> shiftList = TimeGroupOfShiftService.ToList();
                foreach (DataRow row in dt.Rows)
                {
                    AttendShiftOfWeek week = new AttendShiftOfWeek();
                    week.ShiftId = Convert.ToInt32(row["ShiftId"]);
                    week.WeekId = Convert.ToInt32(row["WeekId"]);
                    int groupId1 = Convert.ToInt32(row["TimeGroup1"]);
                    if (groupId1 > 0)
                        week.TimeGroup1 = shiftList.FirstOrDefault(p => p.GroupId == groupId1);
                    int groupId2 = Convert.ToInt32(row["TimeGroup2"]);
                    if (groupId2 > 0)
                        week.TimeGroup2 = shiftList.FirstOrDefault(p => p.GroupId == groupId2);
                    int groupId3 = Convert.ToInt32(row["TimeGroup3"]);
                    if (groupId3 > 0)
                        week.TimeGroup3 = shiftList.FirstOrDefault(p => p.GroupId == groupId3);
                    weekList.Add(week);
                }
            }
            return weekList;
        }
        #endregion

    }
}
