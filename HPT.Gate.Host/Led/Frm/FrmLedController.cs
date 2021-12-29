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
        /// ����LED���ƿ��б�
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
                MessageBox.Show("����LED���ƿ��б�ʧ�ܣ�" + ex.Message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("û�п��޸ĵĿ��ƿ�!", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("��ѡ����Ҫ�޸ĵĿ��ƿ�!", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("û�п�ɾ���Ŀ��ƿ���Ϣ!", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("��ѡ����Ҫɾ���Ŀ��ƿ���Ϣ!", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string ipAddress = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            //int lId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            MessageBoxButtons messageButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("ȷ��Ҫɾ���ÿ��ƿ���Ϣ��?", "", messageButton, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                try
                {
                    CollectService.DeleteLedController(ipAddress);
                    MessageBox.Show("���ƿ���Ϣɾ���ɹ�!", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLedController();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ɾ�����ƿ���Ϣʧ�ܣ�" + ex.Message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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