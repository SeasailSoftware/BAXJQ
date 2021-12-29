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
    public partial class FrmLeaveAdd : FrmBase
    {
        public FrmLeaveAdd()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveLeave();
        }

        private void SaveLeave()
        {
            string empCode = tbEmpCode.Text;
            string empName = tbEmpName.Text;
            EmpInfo emp = null;
            try
            {
                emp = EmpInfoService.Find(empCode, empName);
            }
            catch
            {
            }
            if (emp == null)
            {
                MessageBoxHelper.Info("找不到匹配的人员!");
                return;
            }
            AttendLeave leave = new AttendLeave();
            leave.EmpId = emp.EmpId;
            leave.BeginTime = dtpBeginTime.Text;
            leave.EndTime = dtpEndTime.Text;
            leave.LeaveType = Convert.ToInt32(cbbLeaveType.SelectedValue);
            leave.Remark = tbRemark.Text;
            leave.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                AttendLeaveService.Insert(leave);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"请假录入失败:{ex.Message}");
            }

        }

        private void FrmLeaveAdd_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillLeaves(cbbLeaveType);
        }
    }
}
