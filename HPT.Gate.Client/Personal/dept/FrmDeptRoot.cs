using HPT.Gate.Client.Base;
using hpt.gate.DbTools.Service;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Joey.UserControls;

namespace HPT.Gate.Client.Personal.dept
{
    public partial class FrmDeptRoot : JForm
    {
        public FrmDeptRoot()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        private void FrmDeptRoot_Load(object sender, EventArgs e)
        {
            LoadDeptRootName();
        }

        #region 加载部门根节点名称
        private void LoadDeptRootName()
        {
            try
            {
                DeptInfo dept = DeptInfoService.GetByDeptId(1);
                if (dept != null)
                    tbDeptName.Text = dept.DeptName;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"获取部门根节点名称失败:{ex.Message}");
            }
        }
        #endregion

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string deptName = tbDeptName.Text;
            if (deptName.Trim().Equals(string.Empty))
            {
                MessageBoxHelper.Info("部门根节点名称不能为空!");
                return;
            }
            try
            {
                DeptInfoService.Update(0, 1, deptName, LocalCache.CurrentOper);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"修改部门根节点名称失败:{ex.Message}");
            }
        }
    }
}
