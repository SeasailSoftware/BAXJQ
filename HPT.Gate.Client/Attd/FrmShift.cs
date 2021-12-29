using System;
using System.Data;
using System.Windows.Forms;
using hpt.gate.DbTools;
using hpt.gate.DbTools.Service;
using hpt.gate.Entity;
using hpt.gate.client.Base;

namespace hptGate.Attend
{
    public partial class FrmShift : BaseWindow
    {
        public FrmShift()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            ShiftEdit se = new ShiftEdit();
            se.ShowDialog();
            LoadShiftList();
        }

        private void FShift_Load(object sender, EventArgs e)
        {
            LoadShiftList();
        }

        /// <summary>
        /// 加载班次列表
        /// </summary>
        private void LoadShiftList()
        {
            try
            {
                DataTable dt = AttendService.GetShifts();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载班次列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// 修改班次信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要修改的班次!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int shiftId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            ShiftEdit se = new ShiftEdit(shiftId);
            se.ShowDialog();
            LoadShiftList();
        }

        /// <summary>
        /// 删除班次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要删除的班次!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int shiftId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            string shiftName = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            MessageBoxButtons messageButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("删除班次会导致所有的该班次的排班信息都清空,确定删除?", "提示信息", messageButton, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                try
                {
                    AttendService.DeleteShift(shiftId);
                    LoadShiftList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除班次失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
