using HPT.Gate.Client.Base;
using HPT.Gate.Client.Tools;
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
    public partial class FrmLeaveEdit : FrmBase
    {
        private int _RecId;
        public FrmLeaveEdit(int recId)
        {
            InitializeComponent();
            _RecId = recId;
        }

        private void FrmLeaveEdit_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillLeaves(cbbLeaveType);
            LoadAttendLeave();
        }

        private void LoadAttendLeave()
        {
            AttendLeave leave = AttendLeaveService.Find(_RecId);
            if (leave != null)
            {
                tbEmpCode.Text = leave.EmpCode;
                tbEmpCode.Enabled = false;
                tbEmpName.Text = leave.EmpName;
                tbEmpName.Enabled = false;
                dtpBeginTime.Text = leave.BeginTime;
                dtpEndTime.Text = leave.EndTime;
                cbbLeaveType.SelectedValue = leave.LeaveType;
                tbRemark.Text = leave.Remark;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveAttendLeave();
        }

        private void SaveAttendLeave()
        {
            AttendLeave leave = new AttendLeave();
            leave.RecId = _RecId;
            leave.BeginTime = dtpBeginTime.Text;
            leave.EndTime = dtpEndTime.Text;
            leave.LeaveType = Convert.ToInt32(cbbLeaveType.SelectedValue);
            leave.Remark = tbRemark.Text;
            try
            {
                AttendLeaveService.Update(leave);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"修改请假信息失败:{ex.Message}");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
