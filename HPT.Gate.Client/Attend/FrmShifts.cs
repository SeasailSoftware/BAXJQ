using HPT.Gate.Client.Base;
using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HPT.Gate.Client.Attend
{
    public partial class FrmShifts : FrmBase
    {
        private int _CurrentRowIndex = -1;
        public FrmShifts()
        {
            InitializeComponent();
        }

        private void buttonItem39_Click(object sender, EventArgs e)
        {
            FrmTimegroupOfShift timegroup = new FrmTimegroupOfShift();
            timegroup.ShowDialog();
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            FrmShiftManager shiftManager = new FrmShiftManager();
            shiftManager.ShowDialog();
        }

        private void FrmShifts_Load(object sender, EventArgs e)
        {
            dtpBegin.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
            ComboBoxHelper.FillDeptComboBox(cbbDept);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            int deptId = Convert.ToInt32(cbbDept.SelectedValue);
            int deptType = ckbDept.Checked ? 1 : 0;
            string empcode = tbEmpCode.Text;
            string empName = tbEmpName.Text;
            List<EmpInfo> empList = EmpInfoService.Find(deptId, deptType, empcode, empName, "");
            dgvEmp.DataSource = null;
            dgvEmp.Rows.Clear();
            foreach (EmpInfo emp in empList)
            {
                int rowIndex = dgvEmp.Rows.Add();
                dgvEmp.Rows[rowIndex].Cells[0].Value = emp.DeptName;
                dgvEmp.Rows[rowIndex].Cells[1].Value = emp.EmpId;
                dgvEmp.Rows[rowIndex].Cells[2].Value = emp.EmpCode;
                dgvEmp.Rows[rowIndex].Cells[3].Value = emp.EmpName;
            }
            dgvEmp.ClearSelection();
            _CurrentRowIndex = -1;
        }

        private void 人员排班_Click(object sender, EventArgs e)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            foreach (DataGridViewRow row in dgvEmp.SelectedRows)
            {
                EmpInfo emp = new EmpInfo();
                emp.DeptName = row.Cells[0].Value.ToString();
                emp.EmpId = Convert.ToInt32(row.Cells[1].Value);
                emp.EmpCode = row.Cells[2].Value.ToString();
                emp.EmpName = row.Cells[3].Value.ToString();
                empList.Add(emp);
            }
            if (empList.Count == 0)
            {
                MessageBoxHelper.Info($"请选择需要排班的人员!");
                return;
            }
            FrmShiftSchedule schedule = new FrmShiftSchedule(empList);
            schedule.ShowDialog();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            string beginDate = dtpBegin.Text;
            string endDate = dtpEnd.Text;
            List<AttendShiftOfEmp> shiftList = new List<AttendShiftOfEmp>();
            if (_CurrentRowIndex == -1)
                shiftList = AttendShiftOfEmpService.GetAll(beginDate, endDate);
            else
            {
                int empId = Convert.ToInt32(dgvEmp.Rows[_CurrentRowIndex].Cells[1].Value);
                shiftList = AttendShiftOfEmpService.GetByEmpId(empId, beginDate, endDate);
            }
            dgvShifts.DataSource = null;
            dgvShifts.Rows.Clear();
            foreach (AttendShiftOfEmp shift in shiftList)
            {
                int rowIndex = dgvShifts.Rows.Add();
                dgvShifts.Rows[rowIndex].Cells[0].Value = shift.DeptName;
                dgvShifts.Rows[rowIndex].Cells[1].Value = shift.EmpCode;
                dgvShifts.Rows[rowIndex].Cells[2].Value = shift.EmpName;
                dgvShifts.Rows[rowIndex].Cells[3].Value = shift.RecDate.ToString("yyy-MM-dd");
                dgvShifts.Rows[rowIndex].Cells[4].Value = shift.BeginTime1.ToString("HH:mm");
                dgvShifts.Rows[rowIndex].Cells[5].Value = shift.EndTime1.ToString("HH:mm");
                dgvShifts.Rows[rowIndex].Cells[6].Value = "";
                dgvShifts.Rows[rowIndex].Cells[7].Value = shift.BeginTime2.ToString("HH:mm");
                dgvShifts.Rows[rowIndex].Cells[8].Value = shift.EndTime2.ToString("HH:mm");
                dgvShifts.Rows[rowIndex].Cells[9].Value = "";
                dgvShifts.Rows[rowIndex].Cells[10].Value = shift.BeginTime3.ToString("HH:mm");
                dgvShifts.Rows[rowIndex].Cells[11].Value = shift.EndTime3.ToString("HH:mm");
            }
        }

        private void dgvEmp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _CurrentRowIndex = e.RowIndex;
        }
    }
}
