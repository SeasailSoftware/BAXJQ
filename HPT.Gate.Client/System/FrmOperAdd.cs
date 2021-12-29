using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Service;
using hpt.gate.DbTools.Service;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
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
    public partial class FrmOperAdd : JForm
    {
        public FrmOperAdd()
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

        #region 保存用户信息
        private void SaveOperInfo()
        {
            string operName = tbOperName.Text;
            string operPass = tbOperPass.Text;
            string reMark = tbRemark.Text;
            if (operName.Trim().Equals(string.Empty) || operPass.Trim().Equals(string.Empty))
            {
                MessageBoxHelper.Info("用户名或者密码不能为空!");
                return;
            }
            if (OperInfoService.CheckOperName(operName))
            {
                MessageBoxHelper.Info("用户名已存在!");
                return;
            }
            try
            {
                OperInfo oper = new OperInfo()
                {
                    OperName = operName,
                    OperPass = operPass
                };
                MenusService.Insert(oper);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"保存用户信息失败:{ex.Message}");
            }
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveOperInfo();
        }
    }
}
