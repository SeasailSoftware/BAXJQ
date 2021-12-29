using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using HPT.Gate.Utils.Common;
using HPT.Gate.DataAccess.Entity;
using hpt.gate.DbTools;
using hpt.gate.DbTools.Service;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;

namespace HPT.Gate.Client
{
    public partial class FrmOperEdit : JForm
    {


        /// <summary>
        /// 操作员ID
        /// </summary>
        private int _OperId = 0;

        /// <summary>
        /// 修改用户
        /// </summary>
        public FrmOperEdit(int operId)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this._OperId = operId;
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {

        }

        #region 保存用户信息
        private void SaveOperInfo()
        {
            string operName = tbOperName.Text;
            string operPass = tbOperPass.Text;
            string reMark = tbRemark.Text;
            if (operPass.Trim().Equals(string.Empty))
            {
                MessageBoxHelper.Info("密码不能为空!");
                return;
            }
            try
            {
                OperInfo oper = new OperInfo()
                {
                    OperId = _OperId,
                    OperName = operName,
                    OperPass = operPass,
                    Descr = reMark
                };
                OperInfoService.Update(oper);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"修改用户信息失败:{ex.Message}");
            }
        }
        #endregion

        private void FrmOperEdit_Load(object sender, EventArgs e)
        {
            LoadOperInfo();
        }

        #region 加载用户信息
        private void LoadOperInfo()
        {
            try
            {
                OperInfo oper = OperInfoService.GetById(_OperId);
                tbOperName.Text = oper.OperName;
                tbOperPass.Text = oper.OperPass;
                tbRemark.Text = oper.Descr;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载用户信息失败:{ex.Message}");
            }
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveOperInfo();
        }
    }
}
