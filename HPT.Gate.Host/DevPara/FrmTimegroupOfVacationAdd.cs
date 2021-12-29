using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Host.DevPara
{
    public partial class FrmTimegroupOfVacationAdd : WinBase
    {
        public FrmTimegroupOfVacationAdd()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveTimegroupOfVacation();
        }

        private void SaveTimegroupOfVacation()
        {
            DategroupOfVacation dateGroup = new DategroupOfVacation();
            dateGroup.GName = tbName.Text;
            if (string.IsNullOrWhiteSpace(dateGroup.GName))
            {
                MessageBox.Show("时间组名称不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dateGroup.GMark = tbMark.Text;
            dateGroup.BeginTime1 = dtpBegin1.Text;
            dateGroup.EndTime1 = dtpEnd1.Text;
            dateGroup.BeginTime2 = dtpBegin2.Text;
            dateGroup.EndTime2 = dtpEnd2.Text;
            dateGroup.BeginTime3 = dtpBegin3.Text;
            dateGroup.EndTime3 = dtpEnd3.Text;
            dateGroup.BeginTime4 = dtpBegin4.Text;
            dateGroup.EndTime4 = dtpEnd4.Text;
            dateGroup.BeginTime5 = dtpBegin5.Text;
            dateGroup.EndTime5 = dtpEnd5.Text;
            try
            {
                DategroupOfVacationService.Insert(dateGroup);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加节假日时间组失败:{ex.Message}", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
