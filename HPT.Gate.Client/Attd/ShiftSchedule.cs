using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using hpt.gate.DbTools;
using hpt.gate.DbTools.Service;
using hpt.gate.client.Tools;

namespace hptGate.Attend
{
    public partial class ShiftSchedule : DevComponents.DotNetBar.Office2007Form
    {
        public ShiftSchedule()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }



        private void buttonItem1_Click(object sender, EventArgs e)
        {
            ShiftScheduleSetting sss = new ShiftScheduleSetting();
            sss.ShowDialog();
        }

        private void ShiftSchedule_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select * from DeptInfo";
                ComboBoxHelper.FillComboBox(sql, comboBox1, "DeptName", "DeptId");
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            int deptId = Convert.ToInt32(comboBox1.SelectedValue);
            int deptType = checkBox1.Checked ? 1 : 0;
            string empCode = textBox1.Text.Trim();
            string empName = textBox2.Text.Trim();
            string beginDate = dateTimePicker1.Text;
            string endDate = dateTimePicker2.Text;
            try
            {
                DataTable dt = AttendService.GetShiftOfEmp(deptId, deptType, empCode, empName, beginDate, endDate);
                var rds = new ReportDataSource("ReportOfShiftEmp", dt);
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"/report/ReportOfShiftEmp.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询排班信息失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}