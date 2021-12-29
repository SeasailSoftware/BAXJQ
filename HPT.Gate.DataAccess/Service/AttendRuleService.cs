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
    public class AttendRuleService
    {
        #region 获取考勤制度
        public static AttendRule Get()
        {
            AttendRule rule = new AttendRule();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From AttendRule";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    rule.FullName = row["FullName"].ToString();
                    rule.ReferName = row["ReferName"].ToString();
                    rule.MonthStart = Convert.ToInt32(row["MonthStart"]);
                    rule.WeekStart = Convert.ToInt32(row["WeekStart"]);
                    rule.AcrossDay = Convert.ToInt32(row["AcrossDay"]);
                    rule.OutStatus = Convert.ToInt32(row["OutStatus"]);
                    rule.OTStatus = Convert.ToInt32(row["OTStatus"]);
                    rule.MaxShift = Convert.ToInt32(row["MaxShift"]);
                    rule.MinShift = Convert.ToInt32(row["MinShift"]);
                    rule.EffectAttendInterval = Convert.ToInt32(row["EffectAttendInterval"]);
                    rule.WorkDayMinute = Convert.ToInt32(row["WorkDayMinute"]);
                    rule.LateMinute = Convert.ToInt32(row["LateMinute"]);
                    rule.EarlyMinute = Convert.ToInt32(row["EarlyMinute"]);
                    rule.NoSignInEnabled = Convert.ToInt32(row["NoSignInEnabled"]);
                    rule.NoSignInType = Convert.ToInt32(row["NoSignInType"]);
                    rule.NoSignInMinute = Convert.ToInt32(row["NoSignInMinute"]);
                    rule.NoSignOutEnabled = Convert.ToInt32(row["NoSignOutEnabled"]);
                    rule.NoSignOutType = Convert.ToInt32(row["NoSignOutType"]);
                    rule.NoSignOutMinute = Convert.ToInt32(row["NoSignOutMinute"]);
                    rule.OverSignInEnabled = Convert.ToInt32(row["OverSignInEnabled"]);
                    rule.OverSignInMinute = Convert.ToInt32(row["OverSignInMinute"]);
                    rule.BeforeSignOutEnabled = Convert.ToInt32(row["BeforeSignOutEnabled"]);
                    rule.BeforeSignOutMinute = Convert.ToInt32(row["BeforeSignOutMinute"]);
                    rule.OverSignOutEnabled = Convert.ToInt32(row["OverSignOutEnabled"]);
                    rule.OverSignOutInterval = Convert.ToInt32(row["OverSignOutInterval"]);
                    rule.OverSignOutOTMinute = Convert.ToInt32(row["OverSignOutOTMinute"]);
                    rule.LimitMaxOTAfterSignOutEnabled = Convert.ToInt32(row["LimitMaxOTAfterSignOutEnabled"]);
                    rule.LimitMaxOTAfterSignOutMinute = Convert.ToInt32(row["LimitMaxOTAfterSignOutMinute"]);
                    rule.BeforeSignInEnabled = Convert.ToInt32(row["BeforeSignInEnabled"]);
                    rule.BeforeSignInInterval = Convert.ToInt32(row["BeforeSignInInterval"]);
                    rule.BeforeSignInOTMinute = Convert.ToInt32(row["BeforeSignInOTMinute"]);
                    rule.LimitMaxOTBeforeSignInEnabled = Convert.ToInt32(row["LimitMaxOTBeforeSignInEnabled"]);
                    rule.LimitMaxOTBeforeSignInMinute = Convert.ToInt32(row["LimitMaxOTBeforeSignInMinute"]);
                    rule.LimitTotalOTMaxEnabled = Convert.ToInt32(row["LimitTotalOTMaxEnabled"]);
                    rule.LimitTotalOTMaxMinute = Convert.ToInt32(row["LimitTotalOTMaxMinute"]);
                    rule.Week0 = Convert.ToInt32(row["Week0"]);
                    rule.Week1 = Convert.ToInt32(row["Week1"]);
                    rule.Week2 = Convert.ToInt32(row["Week2"]);
                    rule.Week3 = Convert.ToInt32(row["Week3"]);
                    rule.Week4 = Convert.ToInt32(row["Week4"]);
                    rule.Week5 = Convert.ToInt32(row["Week5"]);
                    rule.Week6 = Convert.ToInt32(row["Week6"]);
                    rule.WeekendAllDayOT = Convert.ToInt32(row["WeekendAllDayOT"]);
                }
            }
            return rule;
        }
        #endregion

        #region 设置考勤制度
        public static void Set(AttendRule rule)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update AttendRule Set FullName ='{rule.FullName}',ReferName='{rule.ReferName}',MonthStart={rule.MonthStart},WeekStart={rule.WeekStart},AcrossDay={rule.AcrossDay},";
                sql += $"{Environment.NewLine} OutStatus={rule.OutStatus},OTStatus={rule.OTStatus},MaxShift={rule.MaxShift},MinShift={rule.MinShift},EffectAttendInterval={rule.EffectAttendInterval},";
                sql += $"{Environment.NewLine} WorkDayMinute ={rule.WorkDayMinute},LateMinute={rule.LateMinute},EarlyMinute={rule.EarlyMinute},NoSignInEnabled={rule.NoSignInEnabled},";
                sql += $"{Environment.NewLine} NoSignInType={rule.NoSignInType},NoSignInMinute={rule.NoSignInMinute},NoSignOutEnabled={rule.NoSignOutEnabled},NoSignOutType={rule.NoSignOutType},";
                sql += $"{Environment.NewLine} NoSignOutMinute={rule.NoSignOutMinute},OverSignInEnabled={rule.OverSignInEnabled},OverSignInMinute={rule.OverSignInMinute},BeforeSignOutEnabled={rule.BeforeSignOutEnabled},";
                sql += $"{Environment.NewLine} BeforeSignOutMinute={rule.BeforeSignOutMinute},OverSignOutEnabled={rule.OverSignOutEnabled},OverSignOutInterval={rule.OverSignOutInterval},OverSignOutOTMinute={rule.OverSignOutOTMinute},";
                sql += $"{Environment.NewLine} LimitMaxOTAfterSignOutEnabled={rule.LimitMaxOTAfterSignOutEnabled},LimitMaxOTAfterSignOutMinute={rule.LimitMaxOTAfterSignOutMinute},BeforeSignInEnabled={rule.BeforeSignInEnabled},";
                sql += $"{Environment.NewLine} BeforeSignInInterval={rule.BeforeSignInInterval},BeforeSignInOTMinute={rule.BeforeSignInOTMinute},LimitMaxOTBeforeSignInEnabled={rule.LimitMaxOTBeforeSignInEnabled},LimitMaxOTBeforeSignInMinute={rule.LimitMaxOTBeforeSignInMinute},";
                sql += $"{Environment.NewLine} LimitTotalOTMaxEnabled={rule.LimitTotalOTMaxEnabled},LimitTotalOTMaxMinute={rule.LimitTotalOTMaxMinute},Week0={rule.Week0},Week1={rule.Week1},Week2={rule.Week2},Week3={rule.Week3},Week4={rule.Week4},Week5={rule.Week5},Week6={rule.Week6},WeekendAllDayOT={rule.WeekendAllDayOT}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
