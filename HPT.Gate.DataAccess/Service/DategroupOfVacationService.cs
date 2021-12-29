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
    public class DategroupOfVacationService
    {
        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From DategroupOfVacation";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 生成列表
        public static List<DategroupOfVacation> ToList()
        {
            List<DategroupOfVacation> vacationList = new List<DategroupOfVacation>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                DategroupOfVacation vacation = new DategroupOfVacation();
                vacation.Gid = Convert.ToInt32(row["Gid"]);
                vacation.GName = row["GName"].ToString();
                vacation.GMark = row["GMark"].ToString();
                vacation.BeginTime1 = row["BeginTime1"].ToString();
                vacation.EndTime1 = row["EndTime1"].ToString();
                vacation.BeginTime2 = row["BeginTime2"].ToString();
                vacation.EndTime2 = row["EndTime2"].ToString();
                vacation.BeginTime3 = row["BeginTime3"].ToString();
                vacation.EndTime3 = row["EndTime3"].ToString();
                vacation.BeginTime4 = row["BeginTime4"].ToString();
                vacation.EndTime4 = row["EndTime4"].ToString();
                vacation.BeginTime5 = row["BeginTime5"].ToString();
                vacation.EndTime5 = row["EndTime5"].ToString();
                vacation.Status = Convert.ToInt32(row["Status"]);
                vacationList.Add(vacation);
            }
            return vacationList;
        }

        #endregion

        #region 获取有效列表
        public static List<DategroupOfVacation> GetEffectList()
        {
            return ToList().Where(s => s.Status == 1).ToList();
        }

        #endregion

        #region 获取节假日信息
        public static DategroupOfVacation GetByGid(int gId)
        {
            return GetEffectList().FirstOrDefault(s => s.Gid == gId);
        }
        #endregion


        #region 添加节假日时间组
        public static void Insert(DategroupOfVacation dateGroup)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertDateGroupOfVacation");
                dbHelper.AddInParameter(cmd, "@gName", DbType.String, dateGroup.GName);
                dbHelper.AddInParameter(cmd, "@gMark", DbType.String, dateGroup.GMark);
                dbHelper.AddInParameter(cmd, "@TimeBegin1", DbType.String, dateGroup.BeginTime1);
                dbHelper.AddInParameter(cmd, "@TimeEnd1", DbType.String, dateGroup.EndTime1);
                dbHelper.AddInParameter(cmd, "@TimeBegin2", DbType.String, dateGroup.BeginTime2);
                dbHelper.AddInParameter(cmd, "@TimeEnd2", DbType.String, dateGroup.EndTime2);
                dbHelper.AddInParameter(cmd, "@TimeBegin3", DbType.String, dateGroup.BeginTime3);
                dbHelper.AddInParameter(cmd, "@TimeEnd3", DbType.String, dateGroup.EndTime3);
                dbHelper.AddInParameter(cmd, "@TimeBegin4", DbType.String, dateGroup.BeginTime4);
                dbHelper.AddInParameter(cmd, "@TimeEnd4", DbType.String, dateGroup.EndTime4);
                dbHelper.AddInParameter(cmd, "@TimeBegin5", DbType.String, dateGroup.BeginTime5);
                dbHelper.AddInParameter(cmd, "@TimeEnd5", DbType.String, dateGroup.EndTime5);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 更新节假日时间段
        public static void Update(DategroupOfVacation dateGroup)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update dategroupofvacation";
                sql += $"{Environment.NewLine} Set GName='{dateGroup.GName}',GMark ='{dateGroup.GMark}',";
                sql += $"BeginTime1='{dateGroup.BeginTime1}',EndTime1='{dateGroup.EndTime1}',";
                sql += $"BeginTime2='{dateGroup.BeginTime2}',EndTime2='{dateGroup.EndTime2}',";
                sql += $"BeginTime3='{dateGroup.BeginTime3}',EndTime3='{dateGroup.EndTime3}',";
                sql += $"BeginTime4='{dateGroup.BeginTime4}',EndTime4='{dateGroup.EndTime4}',";
                sql += $"BeginTime5='{dateGroup.BeginTime5}',EndTime5='{dateGroup.EndTime5}' ";
                sql += $" Where Gid ={dateGroup.Gid} And Status =1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除节假日时间段
        public static void Del(string gId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update DateGroupOfVacation Set Status = 0 Where Gid ={gId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


    }
}
