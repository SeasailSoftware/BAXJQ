using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace hpt.gate.attend.Frm
{
    public partial class RptAttdSummaryPersonal : Form
    {
        public RptAttdSummaryPersonal()
        {
            InitializeComponent();
        }

        private void RptAttdSummaryPersonal_Load(object sender, EventArgs e)
        {
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

        private void btQuery_Click(object sender, EventArgs e)
        {
            string beginDate = dtpBegin.Text;
            string endDate = dtpEnd.Text;
            string empCode = tbEmpCode.Text;
            string empName = tbEmpName.Text;
            btQuery.Enabled = false;
            LoadReportOfSummaryPersonal(beginDate, endDate, empCode, empName);
            btQuery.Enabled = true;
        }

        #region 加载个人考勤汇总表
        private void LoadReportOfSummaryPersonal(string beginDate, string endDate, string empCode, string empName)
        {
            try
            {
                DataTable dt = AttdService.GetDeptAttendSummaryPersonal(beginDate, endDate, empCode, empName);
                var rds = new ReportDataSource("ReportOfAttendSummaryPersonal", dt);
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"/report/ReportOfAttendSummaryPersonal.rdlc";
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

    }
}
