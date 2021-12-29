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
    public partial class FrmVacationEdit : WinBase
    {
        private int _Vid;
        public FrmVacationEdit(int vId)
        {
            InitializeComponent();
            _Vid = vId;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            UpdateVacation();
        }

        #region 更新节假日信息
        private void UpdateVacation()
        {
            Vacation vacation = new Vacation();
            vacation.Vid = _Vid;
            vacation.VName = tbName.Text.Trim();
            vacation.VBeginDate = dtpBeginDate.Text.Trim();
            vacation.VEndDate = dtpEndDate.Text.Trim();
            vacation.VDesc = tbDesc.Text.Trim();
            if (vacation.VName.Equals(string.Empty))
            {
                MessageBox.Show("节假日名字不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                VacationService.Update(vacation);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加节假日失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void FrmVacationEdit_Load(object sender, EventArgs e)
        {
            LoadVacation();
        }

        #region 加载节假日信息
        private void LoadVacation()
        {
            Vacation vacation = VacationService.GetByVid(_Vid);
            if (vacation != null)
            {
                tbName.Text = vacation.VName;
                dtpBeginDate.Text = vacation.VBeginDate;
                dtpEndDate.Text = vacation.VEndDate;
                tbDesc.Text = vacation.VDesc;
            }
        }
        #endregion

    }
}
