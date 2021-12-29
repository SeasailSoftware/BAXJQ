using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace hpt.gate.attend.Frm
{
    public partial class RptAttdSummaryDept : Form
    {
        public RptAttdSummaryDept()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string beginDate = dtpBegin.Text;
            string endDate = dtpEnd.Text;
            int deptId = Convert.ToInt32(cbbDept.SelectedValue);
            int flag = checkBox1.Checked ? 1 : 0;
            btQuery.Enabled = false;
            LoadAttendSummaryOfDeptReport(beginDate, endDate, deptId, flag);
            btQuery.Enabled = true;
        }

        #region 加载汇总表报表
        private void LoadAttendSummaryOfDeptReport(string beginDate, string endDate, int deptId, int flag)
        {
            try
            {
                DataTable dt = AttdService.GetDeptAttendSummaryDept(beginDate, endDate, deptId, flag);
                var rds = new ReportDataSource("ReportOfAttendSummaryDept", dt);
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"/report/ReportOfAttendSummaryDept.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("查询考勤明细表失败:" + ex.Message);
                return;
            }
        }
        #endregion

        private void RptAttdSumary_Load(object sender, EventArgs e)
        {
            try
            {
                ComboBoxHelper.FillDeptComboBox(cbbDept);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载部门列表失败:" + ex.Message);
            }
            GetFirstDayOfCurrentMonth();
            //this.reportViewer1.RefreshReport();
        }

        #region 获取当月第一天
        private void GetFirstDayOfCurrentMonth()
        {
            string firstDay = string.Empty;
            DateTime dt = DateTime.Now;
            firstDay = dt.Year.ToString() + "-" + dt.Month.ToString() + "-01";
            dtpBegin.Text = firstDay;
        }
        #endregion
    }
}
