using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPT.Gate.Utils.Common;
using hpt.gate.DbTools.Service;
using HPT.Gate.Host.Base;
using hpt.gate.led;

namespace hpt.gate.Led.Frm
{
    public partial class FrmLedController : WinBase
    {
        public FrmLedController()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            /*
            LedControllerSetting ledControllerSetting = new LedControllerSetting();
            ledControllerSetting.ShowDialog();
            */
            FrmLedAdd ledAdd = new FrmLedAdd();
            DialogResult dr = ledAdd.ShowDialog();
            if (dr != DialogResult.OK) return;
            LoadLedController();
        }

        private void LedController_Load(object sender, EventArgs e)
        {
            LoadLedController();
        }

        /// <summary>
        /// 加载LED控制卡列表
        /// </summary>
        public void LoadLedController()
        {
            try
            {
                DataTable dt = LedDbService.LoadLEDController();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载LED控制卡列表失败：" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("没有可修改的控制卡!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要修改的控制卡!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int lId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            /*
            LedControllerSetting ledControllerSetting = new LedControllerSetting(lId);
            ledControllerSetting.ShowDialog();
            */
            FrmLedEdit ledEdit = new FrmLedEdit(lId);
            DialogResult dr = ledEdit.ShowDialog();
            if (dr != DialogResult.OK) return;
            LoadLedController();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("没有可删除的控制卡信息!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要删除的控制卡信息!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string ipAddress = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            //int lId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            MessageBoxButtons messageButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除该控制卡信息吗?", "", messageButton, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                try
                {
                    CollectService.DeleteLedController(ipAddress);
                    MessageBox.Show("控制卡信息删除成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLedController();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除控制卡信息失败：" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}