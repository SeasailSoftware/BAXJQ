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

namespace HPT.Gate.Client.Attend
{
    public partial class FrmTimeGroupOfShiftEdit : JForm
    {
        private int _GroupId;
        public FrmTimeGroupOfShiftEdit(int groupId)
        {
            InitializeComponent();
            _GroupId = groupId;
        }

        private void FrmTimeGroupOfShiftEdit_Load(object sender, EventArgs e)
        {
            LoadTimeGroupOfShift();
        }

        #region 加载时间段参数
        private void LoadTimeGroupOfShift()
        {
            TimeGroupOfShift timeGroup = TimeGroupOfShiftService.GetById(_GroupId);
            if (timeGroup != null)
            {
                tbName.Text = timeGroup.GroupName;
                dtpBeginTime1.Text = timeGroup.BeginTime1;
                dtpTime1.Text = timeGroup.Time1;
                dtpEndTime1.Text = timeGroup.EndTime1;
                dtpBeginTime2.Text = timeGroup.BeginTime2;
                dtpTime2.Text = timeGroup.Time2;
                dtpEndTime2.Text = timeGroup.EndTime2;
                numLate.Value = timeGroup.LateMinute;
                numEarly.Value = timeGroup.EarlyMinute;
                tbDay.Text = timeGroup.Day.ToString();
                numMinute.Value = timeGroup.Minute;
                cbbMustSignIn.SelectedIndex = timeGroup.MustSignIn;
                cbbMustSignOut.SelectedIndex = timeGroup.MustSignOut;
                cbbOTSignIn.SelectedIndex = timeGroup.OTBeforeSignIn;
                cbbOTSignOut.SelectedIndex = timeGroup.OTAfterSignOut;
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

        #region 保存信息
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBoxHelper.Info("时间段名称不能为空!");
                return;
            }
            if (string.IsNullOrWhiteSpace(numMinute.Text))
            {
                MessageBoxHelper.Info("工作日不能为空!");
                return;
            }
            TimeGroupOfShift timeGroup = new TimeGroupOfShift();
            timeGroup.GroupId = _GroupId;
            timeGroup.GroupName = tbName.Text;
            timeGroup.BeginTime1 = dtpBeginTime1.Text;
            timeGroup.Time1 = dtpTime1.Text;
            timeGroup.EndTime1 = dtpEndTime1.Text;
            timeGroup.BeginTime2 = dtpBeginTime2.Text;
            timeGroup.Time2 = dtpTime2.Text;
            timeGroup.EndTime2 = dtpEndTime2.Text;
            timeGroup.LateMinute = (int)numLate.Value;
            timeGroup.EarlyMinute = (int)numEarly.Value;
            timeGroup.Day = Convert.ToDouble(tbDay.Text);
            timeGroup.Minute = (int)numMinute.Value;
            timeGroup.MustSignIn = cbbMustSignIn.SelectedIndex;
            timeGroup.MustSignOut = cbbMustSignOut.SelectedIndex;
            timeGroup.OTBeforeSignIn = cbbOTSignIn.SelectedIndex;
            timeGroup.OTAfterSignOut = cbbOTSignOut.SelectedIndex;
            try
            {
                TimeGroupOfShiftService.Update(timeGroup);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"添加时间段失败:{ex.Message}");
            }
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void panel_main_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
