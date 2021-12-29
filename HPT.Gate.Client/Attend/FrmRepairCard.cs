using HPT.Gate.Client.Base;
using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
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
    public partial class FrmRepairCard : FrmBase
    {
        public FrmRepairCard()
        {
            InitializeComponent();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmRepairCard_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillDeptComboBox(cbbDept);
        }

        private void cbbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmps();
        }

        #region 加载员工信息
        private void LoadEmps()
        {
            int deptId = 0;
            try
            {
                deptId = Convert.ToInt32(cbbDept.SelectedValue);
            }
            catch
            {

            }
            if (deptId == 0) return;
            dgvEmps.DataSource = null;
            dgvEmps.Rows.Clear();
            Application.DoEvents();
            List<EmpInfo> empList = new List<EmpInfo>();
            if (ckbDept.Checked)
                empList = EmpInfoService.GetAllByDept(deptId);
            else
                empList = EmpInfoService.GetByDeptId(deptId);
            foreach (EmpInfo emp in empList)
            {
                int rowIndex = dgvEmps.Rows.Add();
                dgvEmps.Rows[rowIndex].Cells[0].Value = emp.DeptId;
                dgvEmps.Rows[rowIndex].Cells[1].Value = emp.DeptName;
                dgvEmps.Rows[rowIndex].Cells[2].Value = emp.EmpId;
                dgvEmps.Rows[rowIndex].Cells[3].Value = emp.EmpCode;
                dgvEmps.Rows[rowIndex].Cells[4].Value = emp.EmpName;
                Application.DoEvents();
            }
        }
        #endregion

        private void ckbDept_CheckedChanged(object sender, EventArgs e)
        {
            LoadEmps();
        }

        private void dgvEmps_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadAttendDatas();
        }

        private void LoadAttendDatas()
        {
            int empId = 0;
            try
            {
                empId = Convert.ToInt32(dgvEmps.SelectedRows[0].Cells[2].Value);
            }
            catch
            {

            }
            if (empId == 0) return;
            string begin = dtpRecDate.Text;
            string end = dtpRecDate.Text + " 23:59";
            List<AttendData> dataList = AttendDataService.Find(empId, begin, end);
            dataList.OrderBy(p => p.RecDatetime);
            dgvAttendData.DataSource = null;
            dgvAttendData.Rows.Clear();
            foreach (AttendData data in dataList)
            {
                int rowIndex = dgvAttendData.Rows.Add();
                dgvAttendData.Rows[rowIndex].Cells[0].Value = data.RecId;
                dgvAttendData.Rows[rowIndex].Cells[1].Value = data.EmpId;
                dgvAttendData.Rows[rowIndex].Cells[2].Value = data.DeviceId;
                dgvAttendData.Rows[rowIndex].Cells[3].Value = data.RecDate;
                dgvAttendData.Rows[rowIndex].Cells[4].Value = data.RecTime;
                dgvAttendData.Rows[rowIndex].Cells[5].Value = data.RecordType;
                Application.DoEvents();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadAttendDatas();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            SaveAttendData();
        }

        #region 保存考勤数据
        private void SaveAttendData()
        {
            DialogResult = DialogResult.OK;
        }
        #endregion

        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
dgvAttendData.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvAttendData.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvAttendData.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                int empId = Convert.ToInt32(dgvEmps.SelectedRows[0].Cells[2].Value);
                string recDate = dtpRecDate.Text;
                FrmAddAttendData add = new FrmAddAttendData(empId, recDate);
                DialogResult dr = add.ShowDialog();
                if (dr != DialogResult.OK) return;
                LoadAttendDatas();
            }
            catch
            {

            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            try
            {
                int recId = Convert.ToInt32(dgvAttendData.SelectedRows[0].Cells[0].Value);
                string recDate = dgvAttendData.SelectedRows[0].Cells[3].Value.ToString();
                string recTime = dgvAttendData.SelectedRows[0].Cells[4].Value.ToString();
                FrmEditAttendData edit = new FrmEditAttendData(recId, recDate, recTime);
                DialogResult dr = edit.ShowDialog();
                if (dr != DialogResult.OK) return;
                LoadAttendDatas();
            }
            catch
            {

            }
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            try
            {
                int recId = Convert.ToInt32(dgvAttendData.SelectedRows[0].Cells[0].Value);
                DialogResult dr = MessageBoxHelper.Question($"确定要删除考勤数据吗?");
                if (dr != DialogResult.OK) return;
                AttendDataService.Del(recId);
                LoadAttendDatas();
            }
            catch
            {

            }
        }
    }
}
