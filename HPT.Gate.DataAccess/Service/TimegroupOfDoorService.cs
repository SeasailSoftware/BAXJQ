using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class TimegroupOfDoorService
    {
        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From TimegroupOfDoor";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

        #region 生成列表
        public static List<TimegroupOfDoor> ToList()
        {
            List<TimegroupOfDoor> timeGroupList = new List<TimegroupOfDoor>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                TimegroupOfDoor timeGroup = new TimegroupOfDoor();
                timeGroup.Id = Convert.ToInt32(row["Id"]);
                timeGroup.Name = row["Name"].ToString();
                timeGroup.Desc = row["Ddesc"].ToString();
                timeGroup.Status = Convert.ToInt32(row["Status"]);
                timeGroupList.Add(timeGroup);
            }
            return timeGroupList;
        }
        #endregion

        #region 获取有效列表
        public static List<TimegroupOfDoor> GetEffectList()
        {
            return ToList().Where(s => s.Status == 1).ToList();
        }

        public static void Insert(string groupName, string gDesc, string time0begin0, string time0end0, string time0begin1, string time0end1, string time0begin2, string time0end2, string time0begin3, string time0end3, string time1begin0, string time1end0, string time1begin1, string time1end1, string time1begin2, string time1end2, string time1begin3, string time1end3, string time2begin0, string time2end0, string time2begin1, string time2end1, string time2begin2, string time2end2, string time2begin3, string time2end3, string time3begin0, string time3end0, string time3begin1, string time3end1, string time3begin2, string time3end2, string time3begin3, string time3end3, string time4begin0, string time4end0, string time4begin1, string time4end1, string time4begin2, string time4end2, string time4begin3, string time4end3, string time5begin0, string time5end0, string time5begin1, string time5end1, string time5begin2, string time5end2, string time5begin3, string time5end3, string time6begin0, string time6end0, string time6begin1, string time6end1, string time6begin2, string time6end2, string time6begin3, string time6end3)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertTimeOfGroup");
                dbHelper.AddInParameter(cmd, "@GroupName", DbType.String, groupName);
                dbHelper.AddInParameter(cmd, "@groupDesc", DbType.String, gDesc);

                dbHelper.AddInParameter(cmd, "@time0begin0", DbType.String, time0begin0);
                dbHelper.AddInParameter(cmd, "@time0end0", DbType.String, time0end0);
                dbHelper.AddInParameter(cmd, "@time0begin1", DbType.String, time0begin1);
                dbHelper.AddInParameter(cmd, "@time0end1", DbType.String, time0end1);
                dbHelper.AddInParameter(cmd, "@time0begin2", DbType.String, time0begin2);
                dbHelper.AddInParameter(cmd, "@time0end2", DbType.String, time0end2);
                dbHelper.AddInParameter(cmd, "@time0begin3", DbType.String, time0begin3);
                dbHelper.AddInParameter(cmd, "@time0end3", DbType.String, time0end3);

                dbHelper.AddInParameter(cmd, "@time1begin0", DbType.String, time1begin0);
                dbHelper.AddInParameter(cmd, "@time1end0", DbType.String, time1end0);
                dbHelper.AddInParameter(cmd, "@time1begin1", DbType.String, time1begin1);
                dbHelper.AddInParameter(cmd, "@time1end1", DbType.String, time1end1);
                dbHelper.AddInParameter(cmd, "@time1begin2", DbType.String, time1begin2);
                dbHelper.AddInParameter(cmd, "@time1end2", DbType.String, time1end2);
                dbHelper.AddInParameter(cmd, "@time1begin3", DbType.String, time1begin3);
                dbHelper.AddInParameter(cmd, "@time1end3", DbType.String, time1end3);

                dbHelper.AddInParameter(cmd, "@time2begin0", DbType.String, time2begin0);
                dbHelper.AddInParameter(cmd, "@time2end0", DbType.String, time2end0);
                dbHelper.AddInParameter(cmd, "@time2begin1", DbType.String, time2begin1);
                dbHelper.AddInParameter(cmd, "@time2end1", DbType.String, time2end1);
                dbHelper.AddInParameter(cmd, "@time2begin2", DbType.String, time2begin2);
                dbHelper.AddInParameter(cmd, "@time2end2", DbType.String, time2end2);
                dbHelper.AddInParameter(cmd, "@time2begin3", DbType.String, time2begin3);
                dbHelper.AddInParameter(cmd, "@time2end3", DbType.String, time2end3);

                dbHelper.AddInParameter(cmd, "@time3begin0", DbType.String, time3begin0);
                dbHelper.AddInParameter(cmd, "@time3end0", DbType.String, time3end0);
                dbHelper.AddInParameter(cmd, "@time3begin1", DbType.String, time3begin1);
                dbHelper.AddInParameter(cmd, "@time3end1", DbType.String, time3end1);
                dbHelper.AddInParameter(cmd, "@time3begin2", DbType.String, time3begin2);
                dbHelper.AddInParameter(cmd, "@time3end2", DbType.String, time3end2);
                dbHelper.AddInParameter(cmd, "@time3begin3", DbType.String, time3begin3);
                dbHelper.AddInParameter(cmd, "@time3end3", DbType.String, time3end3);

                dbHelper.AddInParameter(cmd, "@time4begin0", DbType.String, time4begin0);
                dbHelper.AddInParameter(cmd, "@time4end0", DbType.String, time4end0);
                dbHelper.AddInParameter(cmd, "@time4begin1", DbType.String, time4begin1);
                dbHelper.AddInParameter(cmd, "@time4end1", DbType.String, time4end1);
                dbHelper.AddInParameter(cmd, "@time4begin2", DbType.String, time4begin2);
                dbHelper.AddInParameter(cmd, "@time4end2", DbType.String, time4end2);
                dbHelper.AddInParameter(cmd, "@time4begin3", DbType.String, time4begin3);
                dbHelper.AddInParameter(cmd, "@time4end3", DbType.String, time4end3);

                dbHelper.AddInParameter(cmd, "@time5begin0", DbType.String, time5begin0);
                dbHelper.AddInParameter(cmd, "@time5end0", DbType.String, time5end0);
                dbHelper.AddInParameter(cmd, "@time5begin1", DbType.String, time5begin1);
                dbHelper.AddInParameter(cmd, "@time5end1", DbType.String, time5end1);
                dbHelper.AddInParameter(cmd, "@time5begin2", DbType.String, time5begin2);
                dbHelper.AddInParameter(cmd, "@time5end2", DbType.String, time5end2);
                dbHelper.AddInParameter(cmd, "@time5begin3", DbType.String, time5begin3);
                dbHelper.AddInParameter(cmd, "@time5end3", DbType.String, time5end3);

                dbHelper.AddInParameter(cmd, "@time6begin0", DbType.String, time6begin0);
                dbHelper.AddInParameter(cmd, "@time6end0", DbType.String, time6end0);
                dbHelper.AddInParameter(cmd, "@time6begin1", DbType.String, time6begin1);
                dbHelper.AddInParameter(cmd, "@time6end1", DbType.String, time6end1);
                dbHelper.AddInParameter(cmd, "@time6begin2", DbType.String, time6begin2);
                dbHelper.AddInParameter(cmd, "@time6end2", DbType.String, time6end2);
                dbHelper.AddInParameter(cmd, "@time6begin3", DbType.String, time6begin3);
                dbHelper.AddInParameter(cmd, "@time6end3", DbType.String, time6end3);

                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 更新星期时间组
        public static void Update(object groupId, string time0begin0, string time0end0, string time0begin1, string time0end1, string time0begin2, string time0end2, string time0begin3, string time0end3, string time1begin0, string time1end0, string time1begin1, string time1end1, string time1begin2, string time1end2, string time1begin3, string time1end3, string time2begin0, string time2end0, string time2begin1, string time2end1, string time2begin2, string time2end2, string time2begin3, string time2end3, string time3begin0, string time3end0, string time3begin1, string time3end1, string time3begin2, string time3end2, string time3begin3, string time3end3, string time4begin0, string time4end0, string time4begin1, string time4end1, string time4begin2, string time4end2, string time4begin3, string time4end3, string time5begin0, string time5end0, string time5begin1, string time5end1, string time5begin2, string time5end2, string time5begin3, string time5end3, string time6begin0, string time6end0, string time6begin1, string time6end1, string time6begin2, string time6end2, string time6begin3, string time6end3)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateTimeOfGroup");
                dbHelper.AddInParameter(cmd, "@ID", DbType.Int32, groupId);

                dbHelper.AddInParameter(cmd, "@time0begin0", DbType.String, time0begin0);
                dbHelper.AddInParameter(cmd, "@time0end0", DbType.String, time0end0);
                dbHelper.AddInParameter(cmd, "@time0begin1", DbType.String, time0begin1);
                dbHelper.AddInParameter(cmd, "@time0end1", DbType.String, time0end1);
                dbHelper.AddInParameter(cmd, "@time0begin2", DbType.String, time0begin2);
                dbHelper.AddInParameter(cmd, "@time0end2", DbType.String, time0end2);
                dbHelper.AddInParameter(cmd, "@time0begin3", DbType.String, time0begin3);
                dbHelper.AddInParameter(cmd, "@time0end3", DbType.String, time0end3);

                dbHelper.AddInParameter(cmd, "@time1begin0", DbType.String, time1begin0);
                dbHelper.AddInParameter(cmd, "@time1end0", DbType.String, time1end0);
                dbHelper.AddInParameter(cmd, "@time1begin1", DbType.String, time1begin1);
                dbHelper.AddInParameter(cmd, "@time1end1", DbType.String, time1end1);
                dbHelper.AddInParameter(cmd, "@time1begin2", DbType.String, time1begin2);
                dbHelper.AddInParameter(cmd, "@time1end2", DbType.String, time1end2);
                dbHelper.AddInParameter(cmd, "@time1begin3", DbType.String, time1begin3);
                dbHelper.AddInParameter(cmd, "@time1end3", DbType.String, time1end3);

                dbHelper.AddInParameter(cmd, "@time2begin0", DbType.String, time2begin0);
                dbHelper.AddInParameter(cmd, "@time2end0", DbType.String, time2end0);
                dbHelper.AddInParameter(cmd, "@time2begin1", DbType.String, time2begin1);
                dbHelper.AddInParameter(cmd, "@time2end1", DbType.String, time2end1);
                dbHelper.AddInParameter(cmd, "@time2begin2", DbType.String, time2begin2);
                dbHelper.AddInParameter(cmd, "@time2end2", DbType.String, time2end2);
                dbHelper.AddInParameter(cmd, "@time2begin3", DbType.String, time2begin3);
                dbHelper.AddInParameter(cmd, "@time2end3", DbType.String, time2end3);

                dbHelper.AddInParameter(cmd, "@time3begin0", DbType.String, time3begin0);
                dbHelper.AddInParameter(cmd, "@time3end0", DbType.String, time3end0);
                dbHelper.AddInParameter(cmd, "@time3begin1", DbType.String, time3begin1);
                dbHelper.AddInParameter(cmd, "@time3end1", DbType.String, time3end1);
                dbHelper.AddInParameter(cmd, "@time3begin2", DbType.String, time3begin2);
                dbHelper.AddInParameter(cmd, "@time3end2", DbType.String, time3end2);
                dbHelper.AddInParameter(cmd, "@time3begin3", DbType.String, time3begin3);
                dbHelper.AddInParameter(cmd, "@time3end3", DbType.String, time3end3);

                dbHelper.AddInParameter(cmd, "@time4begin0", DbType.String, time4begin0);
                dbHelper.AddInParameter(cmd, "@time4end0", DbType.String, time4end0);
                dbHelper.AddInParameter(cmd, "@time4begin1", DbType.String, time4begin1);
                dbHelper.AddInParameter(cmd, "@time4end1", DbType.String, time4end1);
                dbHelper.AddInParameter(cmd, "@time4begin2", DbType.String, time4begin2);
                dbHelper.AddInParameter(cmd, "@time4end2", DbType.String, time4end2);
                dbHelper.AddInParameter(cmd, "@time4begin3", DbType.String, time4begin3);
                dbHelper.AddInParameter(cmd, "@time4end3", DbType.String, time4end3);

                dbHelper.AddInParameter(cmd, "@time5begin0", DbType.String, time5begin0);
                dbHelper.AddInParameter(cmd, "@time5end0", DbType.String, time5end0);
                dbHelper.AddInParameter(cmd, "@time5begin1", DbType.String, time5begin1);
                dbHelper.AddInParameter(cmd, "@time5end1", DbType.String, time5end1);
                dbHelper.AddInParameter(cmd, "@time5begin2", DbType.String, time5begin2);
                dbHelper.AddInParameter(cmd, "@time5end2", DbType.String, time5end2);
                dbHelper.AddInParameter(cmd, "@time5begin3", DbType.String, time5begin3);
                dbHelper.AddInParameter(cmd, "@time5end3", DbType.String, time5end3);

                dbHelper.AddInParameter(cmd, "@time6begin0", DbType.String, time6begin0);
                dbHelper.AddInParameter(cmd, "@time6end0", DbType.String, time6end0);
                dbHelper.AddInParameter(cmd, "@time6begin1", DbType.String, time6begin1);
                dbHelper.AddInParameter(cmd, "@time6end1", DbType.String, time6end1);
                dbHelper.AddInParameter(cmd, "@time6begin2", DbType.String, time6begin2);
                dbHelper.AddInParameter(cmd, "@time6end2", DbType.String, time6end2);
                dbHelper.AddInParameter(cmd, "@time6begin3", DbType.String, time6begin3);
                dbHelper.AddInParameter(cmd, "@time6end3", DbType.String, time6end3);

                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 获取星期时间组信息
        public static TimegroupOfDoor GetById(int groupId)
        {
            return GetEffectList().FirstOrDefault(p => p.Id == groupId);
        }
        #endregion

        #region 删除星期时间组
        public static void Del(int groupId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"Begin Tran");
                buffer.AppendLine($"Update TimeGroupOfDoor set status = 0 where id = {groupId}");
                buffer.AppendLine($"delete from TimeOfGroup where groupId ={groupId}");
                buffer.AppendLine($" If exists(Select top 1 * from TicketType where IntimeGroupNo = {groupId})");
                buffer.AppendLine($"        Update TicketType Set IntimeGroupNo = 0 where IntimeGroupNo = {groupId}");
                buffer.AppendLine($"If exists(Select top 1 * from TicketType where OutTimeGroupNo = {groupId})");
                buffer.AppendLine($"        Update TicketType Set OutTimeGroupNo = 0 where OutTimeGroupNo ={groupId}");
                buffer.AppendLine($"COMMIT  Tran");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取当前时间在对应时间组内的时间段
        internal static TimeGroup GetTimegroupDuring(int groupNo)
        {
            int weekNo = (int)DateTime.Now.DayOfWeek + 1;
            List<TimeOfGroup> groupList = TimeOfGroupService.ToList().Where(p => p.GroupId == groupNo && p.WeekNo == weekNo).ToList();
            foreach (TimeOfGroup group in groupList)
            {
                DateTime dtBegin = Convert.ToDateTime($"{DateTime.Now.ToString("yyyy-MM-dd")} {group.BeginTime}");
                DateTime dtEnd = Convert.ToDateTime($"{DateTime.Now.ToString("yyyy-MM-dd")} {group.EndTime}");
                if (dtBegin <= DateTime.Now && DateTime.Now <= dtEnd)
                    return new TimeGroup(group.BeginTime, group.EndTime);
            }
            return null;
        }
        #endregion
    }
}
