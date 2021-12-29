using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Microsoft.Reporting.WinForms;
using hpt.gate.DbTools;
using hpt.gate.DbTools.Service;
using hpt.gate.client.Tools;

namespace hptGate.Attend
{
    public partial class FAttendReport : DevComponents.DotNetBar.Office2007Form
    {
        public FAttendReport()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FAttendReport_Load(object sender, EventArgs e)
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
            string firstDay = string.Empty;
            DateTime dt = DateTime.Now;
            firstDay = dt.Year.ToString() + "-" + dt.Month.ToString() + "-01";
            dateTimePicker1.Text = firstDay;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ///-------------------------------------------------------
            string beginDate = dateTimePicker1.Text;
            string endDate = dateTimePicker2.Text;

            int deptId = Convert.ToInt32(comboBox1.SelectedValue);
            int deptType = checkBox1.Checked ? 1 : 0;
            string empCode = textBox1.Text.Trim();
            string empName = textBox2.Text.Trim();
            int index = tabControl1.SelectedTabIndex;
            switch (index)
            {
                case 0:
                    try
                    {
                        DataTable dt = AttendService.GetOriginalRecord(beginDate, endDate, deptId, deptType, empCode, empName);
                        var rds = new ReportDataSource("ReportOfOriginalRecord", dt);
                        reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"/report/ReportOfOriginalRecord.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rds);
                        reportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("查询原始记录报表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    break;
                case 1:
                    try
                    {
                        DataTable dt = AttendService.GetAttendDetail(beginDate, endDate, deptId, deptType, empCode, empName);
                        var rds = new ReportDataSource("ReportOfAttendDetail", dt);
                        reportViewer2.LocalReport.ReportPath = Application.StartupPath + @"/report/ReportOfAttendDetail.rdlc";
                        reportViewer2.LocalReport.DataSources.Clear();
                        reportViewer2.LocalReport.DataSources.Add(rds);
                        reportViewer2.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("查询考勤明细表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    break;
                case 2:
                    try
                    {
                        DataTable dt = AttendService.GetAttendSummary(beginDate, endDate, deptId, deptType, empCode, empName);
                        var rds = new ReportDataSource("ReportOfAttendSummary", dt);
                        reportViewer3.LocalReport.ReportPath = Application.StartupPath + @"/report/ReportOfAttendSummary.rdlc";
                        reportViewer3.LocalReport.DataSources.Clear();
                        reportViewer3.LocalReport.DataSources.Add(rds);
                        reportViewer3.RefreshReport();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("查询考勤明细表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    break;
            }
        }

        private void tabControl1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            int index = tabControl1.SelectedTabIndex;
            switch (index)
            {
                case 0:
                    try
                    {
                        DataTable dt = new DataTable();
                        var rds = new ReportDataSource("ReportOfOriginalRecord", dt);
                        reportViewer1.LocalReport.ReportPath = @"report\ReportOfOriginalRecord.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rds);
                        reportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("查询原始记录报表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    break;
                case 1:
                    try
                    {
                        DataTable dt = new DataTable();
                        var rds = new ReportDataSource("ReportOfAttendDetail", dt);
                        reportViewer2.LocalReport.ReportPath = @"report/ReportOfAttendDetail.rdlc";
                        reportViewer2.LocalReport.DataSources.Clear();
                        reportViewer2.LocalReport.DataSources.Add(rds);
                        reportViewer2.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("查询考勤明细表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    break;
            }
        }
    }
}