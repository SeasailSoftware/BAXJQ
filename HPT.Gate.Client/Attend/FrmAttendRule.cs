using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hpt.gate.attend.Frm
{
    public partial class FrmAttendRule : JForm
    {
        public FrmAttendRule()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FrmAttdRule_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        #region 加载配置文件
        private void LoadConfig()
        {
            AttendRule rule = AttendRuleService.Get();
            if (rule != null)
            {
                //基本设置
                tbFullName.Text = rule.FullName;
                tbReferName.Text = rule.ReferName;
                numMonthStart.Value = rule.MonthStart;
                cbbWeekStart.SelectedIndex = rule.WeekStart;
                cbbCrossDay.SelectedIndex = rule.AcrossDay;
                cbbOutStatus.SelectedIndex = rule.OutStatus;
                cbbOTStatus.SelectedIndex = rule.OTStatus;
                numMaxShift.Value = rule.MaxShift;
                numMinShift.Value = rule.MinShift;
                numEffectAttendInterval.Value = rule.EffectAttendInterval;
                //考勤计算
                numWorkDayMinute.Value = rule.WorkDayMinute;
                numLateMinute.Value = rule.LateMinute;
                numEarlyMinute.Value = rule.EarlyMinute;
                ckbNoSignIn.Checked = rule.NoSignInEnabled == 1;
                cbbNoSignInType.SelectedIndex = rule.NoSignInType;
                numNoSignInMinute.Value = rule.NoSignInMinute;
                ckbNoSignOut.Checked = rule.NoSignOutEnabled == 1;
                cbbNoSignOutType.SelectedIndex = rule.NoSignOutType;
                numNoSignOutMinute.Value = rule.NoSignOutMinute;
                ckbOverSignIn.Checked = rule.OverSignInEnabled == 1;
                numOverSignInMinute.Value = rule.OverSignInMinute;
                ckbBeforeSignOut.Checked = rule.BeforeSignOutEnabled == 1;
                numBeforeSignOutMinute.Value = rule.BeforeSignOutMinute;
                ckbOverSignOut.Checked = rule.OverSignOutEnabled == 1;
                numOverSignOutInterval.Value = rule.OverSignOutInterval;
                numOverSignOutMinute.Value = rule.OverSignOutOTMinute;
                ckbLimitMaxPTAfterSignOut.Checked = rule.LimitMaxOTAfterSignOutEnabled == 1;
                numLimitMaxOTAfterSignOutMinute.Value = rule.LimitMaxOTAfterSignOutMinute;
                ckbBeforeSignIn.Checked = rule.BeforeSignInEnabled == 1;
                numBeforeSignInInterval.Value = rule.BeforeSignInInterval;
                numBeforeSignInOTMinute.Value = rule.BeforeSignInOTMinute;
                ckbLimitMaxOTBeforeSignIn.Checked = rule.LimitMaxOTBeforeSignInEnabled == 1;
                numLimitMaxOTBeforeSignMinute.Value = rule.LimitMaxOTBeforeSignInMinute;
                ckbLimitTotalOTMax.Checked = rule.LimitTotalOTMaxEnabled == 1;
                numLimitTotalOTMaxMinute.Value = rule.LimitTotalOTMaxMinute;
                //周末设置
                ckbWeek0.Checked = rule.Week0 == 1;
                ckbWeek1.Checked = rule.Week1 == 1;
                ckbWeek2.Checked = rule.Week2 == 1;
                ckbWeek3.Checked = rule.Week3 == 1;
                ckbWeek4.Checked = rule.Week4 == 1;
                ckbWeek5.Checked = rule.Week5 == 1;
                ckbWeek6.Checked = rule.Week6 == 1;
                ckbAllDay.Checked = rule.WeekendAllDayOT == 1;

            }
        }

        #endregion

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        #region 保存配置文件
        private void SaveConfig()
        {
            AttendRule rule = new AttendRule();
            //基本设置
            rule.FullName = tbFullName.Text;
            rule.ReferName = tbReferName.Text;
            rule.MonthStart = (int)numMonthStart.Value;
            rule.WeekStart = cbbWeekStart.SelectedIndex;
            rule.AcrossDay = cbbCrossDay.SelectedIndex;
            rule.OutStatus = cbbOutStatus.SelectedIndex;
            rule.OTStatus = cbbOTStatus.SelectedIndex;
            rule.MaxShift = (int)numMaxShift.Value;
            rule.MinShift = (int)numMinShift.Value;
            rule.EffectAttendInterval = (int)numEffectAttendInterval.Value;
            //考勤计算
            rule.WorkDayMinute = (int)numWorkDayMinute.Value;
            rule.LateMinute = (int)numLateMinute.Value;
            rule.EarlyMinute = (int)numEarlyMinute.Value;
            rule.NoSignInEnabled = ckbNoSignIn.Checked ? 1 : 0;
            rule.NoSignInType = cbbNoSignInType.SelectedIndex;
            rule.NoSignInMinute = (int)numNoSignInMinute.Value;
            rule.NoSignOutEnabled = ckbNoSignOut.Checked ? 1 : 0;
            rule.NoSignOutType = cbbNoSignOutType.SelectedIndex;
            rule.NoSignOutMinute = (int)numNoSignOutMinute.Value;
            rule.OverSignInEnabled = ckbOverSignIn.Checked ? 1 : 0;
            rule.OverSignInMinute = (int)numOverSignInMinute.Value;
            rule.BeforeSignOutEnabled = ckbBeforeSignOut.Checked ? 1 : 0;
            rule.BeforeSignOutMinute = (int)numBeforeSignOutMinute.Value;
            rule.OverSignOutEnabled = ckbOverSignOut.Checked ? 1 : 0;
            rule.OverSignOutInterval = (int)numOverSignOutInterval.Value;
            rule.OverSignOutOTMinute = (int)numOverSignOutMinute.Value;
            rule.LimitMaxOTAfterSignOutEnabled = ckbLimitMaxPTAfterSignOut.Checked ? 1 : 0;
            rule.LimitMaxOTAfterSignOutMinute = (int)numLimitMaxOTAfterSignOutMinute.Value;
            rule.BeforeSignInEnabled = ckbBeforeSignIn.Checked ? 1 : 0;
            rule.BeforeSignInInterval = (int)numBeforeSignInInterval.Value;
            rule.BeforeSignInOTMinute = (int)numBeforeSignInOTMinute.Value;
            rule.LimitMaxOTBeforeSignInEnabled = ckbLimitMaxOTBeforeSignIn.Checked ? 1 : 0;
            rule.LimitMaxOTBeforeSignInMinute = (int)numLimitMaxOTBeforeSignMinute.Value;
            rule.LimitTotalOTMaxEnabled = ckbLimitTotalOTMax.Checked ? 1 : 0;
            rule.LimitTotalOTMaxMinute = (int)numLimitTotalOTMaxMinute.Value;
            //周末设置
            rule.Week0 = ckbWeek0.Checked ? 1 : 0;
            rule.Week1 = ckbWeek1.Checked ? 1 : 0;
            rule.Week2 = ckbWeek2.Checked ? 1 : 0;
            rule.Week3 = ckbWeek3.Checked ? 1 : 0;
            rule.Week4 = ckbWeek4.Checked ? 1 : 0;
            rule.Week5 = ckbWeek5.Checked ? 1 : 0;
            rule.Week6 = ckbWeek6.Checked ? 1 : 0;
            rule.WeekendAllDayOT = ckbAllDay.Checked ? 1 : 0;
            try
            {
                AttendRuleService.Set(rule);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"保存考勤规则失败:{ex.Message}");
            }
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }
    }
}
