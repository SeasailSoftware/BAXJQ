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
    public partial class FrmTimegroupOfShiftAdd : JForm
    {
        public FrmTimegroupOfShiftAdd()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmTimegroupOfShiftAdd_Load(object sender, EventArgs e)
        {
            cbbMustSignIn.SelectedIndex = 1;
            cbbMustSignOut.SelectedIndex = 1;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        #region 保存时间段信息
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
                TimeGroupOfShiftService.Insert(timeGroup);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"添加时间段失败:{ex.Message}");
            }
        }
        #endregion

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (tbDay.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(tbDay.Text, out oldf);
                    b2 = float.TryParse(tbDay.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
