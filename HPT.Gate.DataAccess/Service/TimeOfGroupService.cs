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
    public class TimeOfGroupService
    {
        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From TimeOfGroup";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 生成列表
        public static List<TimeOfGroup> ToList()
        {
            List<TimeOfGroup> timeGroupList = new List<TimeOfGroup>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                TimeOfGroup timeGroup = new TimeOfGroup();
                timeGroup.Id = Convert.ToInt32(row["Id"]);
                timeGroup.GroupId = Convert.ToInt32(row["GroupId"]);
                timeGroup.WeekNo = Convert.ToInt32(row["WeekNo"]);
                timeGroup.BeginTime = row["BeginTime"].ToString();
                timeGroup.EndTime = row["EndTime"].ToString();
                timeGroupList.Add(timeGroup);
            }
            return timeGroupList;
        }
        #endregion

        #region 获取列表
        public static List<TimeOfGroup> GetById(int groupId)
        {
            return ToList().Where(p => p.GroupId == groupId).ToList();
        }
        #endregion

    }
}
