using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using HPT.Gate.Utils.Common;
using hpt.gate.DbTools;
using hpt.gate.DbTools.Service;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.DataAccess.Entity;
using Joey.UserControls;

namespace HPT.Gate.Client
{
    public partial class FrmPassword : JForm
    {
        public FrmPassword()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            textBox1.Text = LocalCache.CurrentOper.OperName;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string oldPass = textBox2.Text.Trim();
            string newPass = textBox3.Text.Trim();
            string newPassConform = textBox4.Text.Trim();
            if (!newPass.Equals(newPassConform))
            {
                MessageBoxHelper.Info("两次输入的新密码不正确!");
                return;
            }
            try
            {
                OperInfo oper = new OperInfo();
                oper.OperId = LocalCache.CurrentOper.OperId;
                oper.OperName = LocalCache.CurrentOper.OperName;
                oper.OperPass = newPass;
                OperInfoService.Update(oper);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"修改用户密码失败:{ex.Message}");
                return;
            }
        }
    }
}
