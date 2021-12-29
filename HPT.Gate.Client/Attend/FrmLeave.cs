using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Client.Attend
{
    public partial class FrmLeave : FrmBase
    {
        public FrmLeave()
        {
            InitializeComponent();
        }

        private void dgvLeave_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
dgvLeave.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvLeave.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvLeave.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmLeaveAdd add = new FrmLeaveAdd();
            add.ShowDialog();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            int recId = 0;
            try
            {
                recId = Convert.ToInt32(dgvLeave.SelectedRows[0].Cells[0].Value);
            }
            catch
            {

            }
            if (recId == 0)
            {
                MessageBoxHelper.Info("没有可操作的数据!");
                return;
            }
            FrmLeaveEdit edit = new FrmLeaveEdit(recId);
            DialogResult dr = edit.ShowDialog();
            if (dr != DialogResult.OK) return;
            AttendLeave leave = AttendLeaveService.Find(recId);
            int rowIndex = dgvLeave.SelectedRows[0].Index;
            dgvLeave.Rows[rowIndex].Cells[0].Value = leave.RecId;
            dgvLeave.Rows[rowIndex].Cells[1].Value = leave.DeptId;
            dgvLeave.Rows[rowIndex].Cells[2].Value = leave.DeptName;
            dgvLeave.Rows[rowIndex].Cells[3].Value = leave.EmpId;
            dgvLeave.Rows[rowIndex].Cells[4].Value = leave.EmpCode;
            dgvLeave.Rows[rowIndex].Cells[5].Value = leave.EmpName;
            dgvLeave.Rows[rowIndex].Cells[6].Value = leave.BeginTime;
            dgvLeave.Rows[rowIndex].Cells[7].Value = leave.EndTime;
            dgvLeave.Rows[rowIndex].Cells[8].Value = leave.LeaveName;
            dgvLeave.Rows[rowIndex].Cells[9].Value = leave.Remark;
            dgvLeave.Rows[rowIndex].Cells[10].Value = leave.CreateTime;
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            int recId = 0;
            try
            {
                recId = Convert.ToInt32(dgvLeave.SelectedRows[0].Cells[0].Value);
            }
            catch
            {

            }
            if (recId == 0)
            {
                MessageBoxHelper.Info("没有可操作的数据!");
                return;
            }
            DialogResult dr = MessageBoxHelper.Question("确定要删除该请假信息吗?");
            if (dr != DialogResult.OK) return;
            try
            {
                AttendLeaveService.Del(recId);
                foreach (DataGridViewRow row in dgvLeave.Rows)
                {
                    if (Convert.ToInt32(row.Cells[0].Value) == recId)
                    {
                        dgvLeave.Rows.Remove(row);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"删除请假信息失败:{ex.Message}");
            }
        }

        private void FindAttendLeaves()
        {

        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            FrmLeaveFind find = new FrmLeaveFind();
            DialogResult dr = find.ShowDialog();
            if (dr != DialogResult.OK) return;
            dgvLeave.DataSource = null;
            dgvLeave.Rows.Clear();
            List<AttendLeave> leaves = AttendLeaveService.Find(find.DeptId, find.DeptType, find.EmpCode, find.EmpName);
            foreach (AttendLeave leave in leaves)
            {
                int rowIndex = dgvLeave.Rows.Add();
                dgvLeave.Rows[rowIndex].Cells[0].Value = leave.RecId;
                dgvLeave.Rows[rowIndex].Cells[1].Value = leave.DeptId;
                dgvLeave.Rows[rowIndex].Cells[2].Value = leave.DeptName;
                dgvLeave.Rows[rowIndex].Cells[3].Value = leave.EmpId;
                dgvLeave.Rows[rowIndex].Cells[4].Value = leave.EmpCode;
                dgvLeave.Rows[rowIndex].Cells[5].Value = leave.EmpName;
                dgvLeave.Rows[rowIndex].Cells[6].Value = leave.BeginTime;
                dgvLeave.Rows[rowIndex].Cells[7].Value = leave.EndTime;
                dgvLeave.Rows[rowIndex].Cells[8].Value = leave.LeaveName;
                dgvLeave.Rows[rowIndex].Cells[9].Value = leave.Remark;
                dgvLeave.Rows[rowIndex].Cells[10].Value = leave.CreateTime;
                Application.DoEvents();
            }
        }
    }
}
