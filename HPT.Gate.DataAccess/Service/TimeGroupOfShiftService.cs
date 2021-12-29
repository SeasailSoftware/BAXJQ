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
    public class TimeGroupOfShiftService
    {
        #region 获取所有时间段
        public static List<TimeGroupOfShift> ToList()
        {
            List<TimeGroupOfShift> groupList = new List<TimeGroupOfShift>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From TimeGroupOfShift";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    TimeGroupOfShift timeGroup = new TimeGroupOfShift();
                    timeGroup.GroupId = Convert.ToInt32(row["GroupId"]);
                    timeGroup.GroupName = row["GroupName"].ToString();
                    timeGroup.BeginTime1 = Convert.ToDateTime(row["BeginTime1"]).ToString("HH:mm");
                    timeGroup.Time1 = Convert.ToDateTime(row["Time1"]).ToString("HH:mm");
                    timeGroup.EndTime1 = Convert.ToDateTime(row["EndTime1"]).ToString("HH:mm");
                    timeGroup.BeginTime2 = Convert.ToDateTime(row["BeginTime2"]).ToString("HH:mm");
                    timeGroup.Time2 = Convert.ToDateTime(row["Time2"]).ToString("HH:mm");
                    timeGroup.EndTime2 = Convert.ToDateTime(row["EndTime2"]).ToString("HH:mm");
                    timeGroup.LateMinute = Convert.ToInt32(row["LateMinute"]);
                    timeGroup.EarlyMinute = Convert.ToInt32(row["EarlyMinute"]);
                    timeGroup.Day = Convert.ToDouble(row["Day"]);
                    timeGroup.Minute = Convert.ToInt32(row["Minute"]);
                    timeGroup.MustSignIn = Convert.ToInt32(row["MustSignIn"]);
                    timeGroup.MustSignOut = Convert.ToInt32(row["MustSignOut"]);
                    timeGroup.OTBeforeSignIn = Convert.ToInt32(row["OTBeforeSignIn"]);
                    timeGroup.OTAfterSignOut = Convert.ToInt32(row["OTAfterSignOut"]);
                    groupList.Add(timeGroup);
                }
            }
            return groupList;
        }
        #endregion

        #region 根据时间组编号获取时间组信息
        public static TimeGroupOfShift GetById(int _GroupId)
        {
            return ToList().FirstOrDefault(p => p.GroupId == _GroupId);
        }
        #endregion


        #region 添加时间段
        public static void Insert(TimeGroupOfShift timeGroup)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append($"Insert Into TimeGroupOfShift(GroupName,BeginTime1,Time1,EndTime1,BeginTime2,Time2,EndTime2,LateMinute,EarlyMinute,Day,Minute,MustSignIn,MustSignOut,OTBeforeSignIn,OTAfterSignOut)");
                buffer.Append($"{Environment.NewLine} Values('{timeGroup.GroupName}','{timeGroup.BeginTime1}','{timeGroup.Time1}','{timeGroup.EndTime1}','{timeGroup.BeginTime2}','{timeGroup.Time2}','{timeGroup.EndTime2}',{timeGroup.LateMinute},{timeGroup.EarlyMinute},{timeGroup.Day},{timeGroup.Minute},{timeGroup.MustSignIn},{timeGroup.MustSignOut},{timeGroup.OTBeforeSignIn},{timeGroup.OTAfterSignOut})");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 更新时间段信息
        public static void Update(TimeGroupOfShift timeGroup)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append($"Update TimeGroupOfShift Set GroupName='{timeGroup.GroupName}',BeginTime1='{timeGroup.BeginTime1}',Time1='{timeGroup.Time1}',EndTime1='{timeGroup.EndTime1}',BeginTime2='{timeGroup.BeginTime2}',Time2='{timeGroup.Time2}',EndTime2='{timeGroup.EndTime2}',");
                buffer.Append($"{Environment.NewLine}LateMinute ={timeGroup.LateMinute},EarlyMinute={timeGroup.EarlyMinute},Day ={timeGroup.Day},Minute ={timeGroup.Minute},MustSignIn ={timeGroup.MustSignIn},MustSignOut={timeGroup.MustSignOut},OTBeforeSignIn={timeGroup.OTBeforeSignIn},OTAfterSignOut={timeGroup.OTAfterSignOut}");
                buffer.Append($"{Environment.NewLine}Where GroupId ={timeGroup.GroupId}");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 删除时间段
        public static void Del(int groupId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append($"Delete From TimeGroupOfShift Where GroupId ={groupId}");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
