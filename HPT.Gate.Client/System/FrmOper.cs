using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Service;
using hpt.gate.DbTools.Service;
using HPT.Gate.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Joey.UserControls;

namespace HPT.Gate.Client.oper
{
    public partial class FrmOper : FrmBase
    {
        public FrmOper()
        {
            InitializeComponent();
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            AddOper();
            LoadOpers();
        }

        #region 添加用户
        private void AddOper()
        {
            FrmOperAdd addOper = new FrmOperAdd();
            addOper.ShowDialog();
        }
        #endregion

        #region 加载所有操作员
        private void LoadOpers()
        {
            try
            {
                List<OperInfo> operList = OperInfoService.ToList();
                dgvOper.DataSource = null;
                dgvOper.Rows.Clear();
                foreach (OperInfo oper in operList)
                {
                    int rowIndex = dgvOper.Rows.Add();
                    dgvOper.Rows[rowIndex].Cells[0].Value = oper.OperId;
                    dgvOper.Rows[rowIndex].Cells[1].Value = oper.OperName;
                    dgvOper.Rows[rowIndex].Cells[2].Value = oper.Descr;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Info("加载用户列表失败:" + ex.Message);
                return;
            }
        }
        #endregion

        private void FrmOper_Load(object sender, EventArgs e)
        {
            LoadOpers();
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            if (dgvOper.SelectedRows == null || dgvOper.SelectedRows[0].Index == -1)
            {
                MessageBoxHelper.Info("请选择需要编辑的用户！");
                return;
            }
            var operId = Convert.ToInt32(dgvOper.SelectedRows[0].Cells[0].Value);
            var oe = new FrmOperEdit(operId);
            oe.ShowDialog();
            LoadOpers();
        }

        private void buttonItem20_Click(object sender, EventArgs e)
        {
            DeleteOper();
        }

        #region 删除用户
        private void DeleteOper()
        {
            if (dgvOper.SelectedRows == null || dgvOper.SelectedRows[0].Index == -1)
            {
                MessageBoxHelper.Info("请选择需要删除的用户！");
                return;
            }
            int operId = Convert.ToInt32(dgvOper.SelectedRows[0].Cells[0].Value);
            string operName = dgvOper.SelectedRows[0].Cells[1].Value.ToString();
            if (operId == 1)
            {
                MessageBoxHelper.Info("超级管理员admin不能删除！");
                return;
            }
            DialogResult dr = MessageBoxHelper.Question($"确定要删除用户[{operName}]吗?");
            if (dr != DialogResult.OK) return;
            try
            {
                OperInfoService.Del(operId);
                LoadOpers();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"删除操作员失败:{ex.Message}");
            }
        }
        #endregion

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            if (dgvOper.SelectedRows == null || dgvOper.SelectedRows[0].Index == -1)
            {
                MessageBoxHelper.Info("请选择需要删除的用户！");
                return;
            }
            int operId = Convert.ToInt32(dgvOper.SelectedRows[0].Cells[0].Value);
            if (operId == 1)
            {
                MessageBoxHelper.Info("超级管理员的权限不可修改！");
                return;
            }
            FrmOperRights operRights = new FrmOperRights(operId);
            operRights.ShowDialog();
        }
    }
}
