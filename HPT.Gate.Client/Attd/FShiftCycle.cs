using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using hpt.gate.Entity;
using hpt.gate.DbTools.Service;

namespace hptGate.Attend
{
    public partial class FShiftCycle : DevComponents.DotNetBar.Office2007Form
    {
        public FShiftCycle()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmAttdCycleEdit ace = new FrmAttdCycleEdit();
            ace.ShowDialog();
            LoadAttendCycle();
        }

        private void FAttendCycle_Load(object sender, EventArgs e)
        {
            LoadAttendCycle();
        }

        /// <summary>
        /// 加载考勤周期列表
        /// </summary>
        private void LoadAttendCycle()
        {
            try
            {
                DataTable dt = AttendService.GetAttendCycleList();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载考勤周期失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }





        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要修改的考勤周期！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int cid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            FrmAttdCycleEdit ace = new FrmAttdCycleEdit(cid);
            ace.ShowDialog();
            LoadAttendCycle();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要删除的考勤周期！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int cid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            string cName = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            MessageBoxButtons messageButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除考勤周期【" + cName + "】吗", "删除考勤周期", messageButton, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                try
                {
                    AttendService.DeleteAttendCycle(cid);
                    LoadAttendCycle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除考勤周期失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[index].Selected = true;
            buttonItem2_Click(sender, e);
        }
    }
}