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
    public partial class FrmTimegroupOfVacationEdit : WinBase
    {
        private int _Gid;
        public FrmTimegroupOfVacationEdit(int gId)
        {
            InitializeComponent();
            _Gid = gId;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmTimegroupOfVacationEdit_Load(object sender, EventArgs e)
        {
            LoadTimegroupOfVacation();
        }

        #region 加载节假日信息
        private void LoadTimegroupOfVacation()
        {
            DategroupOfVacation dateGroup = DategroupOfVacationService.GetByGid(_Gid);
            if (dateGroup != null)
            {
                tbName.Text = dateGroup.GName;
                dtpBegin1.Text = dateGroup.BeginTime1;
                dtpEnd1.Text = dateGroup.EndTime1;
                dtpBegin2.Text = dateGroup.BeginTime2;
                dtpEnd2.Text = dateGroup.EndTime2;
                dtpBegin3.Text = dateGroup.BeginTime3;
                dtpEnd3.Text = dateGroup.EndTime3;
                dtpBegin4.Text = dateGroup.BeginTime4;
                dtpEnd4.Text = dateGroup.EndTime4;
                dtpBegin5.Text = dateGroup.BeginTime5;
                dtpEnd5.Text = dateGroup.EndTime5;
                tbMark.Text = dateGroup.GMark;
            }

        }
        #endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveTimegroupOfVacation();
        }

        #region 保存节假日时间组
        private void SaveTimegroupOfVacation()
        {
            DategroupOfVacation dateGroup = new DategroupOfVacation();
            dateGroup.Gid = _Gid;
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
                DategroupOfVacationService.Update(dateGroup);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加节假日时间组失败:{ex.Message}", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void buttonX2_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
