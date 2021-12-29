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
    public partial class FrmVacationAdd : WinBase
    {
        public FrmVacationAdd()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveVacation();
        }

        #region 添加节假日
        private void SaveVacation()
        {
            Vacation vacation = new Vacation();
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
                VacationService.Insert(vacation);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加节假日失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
