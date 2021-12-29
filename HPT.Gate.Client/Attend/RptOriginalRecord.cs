using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace hpt.gate.attend.Frm
{
    public partial class RptOriginalRecord : Form
    {
        public RptOriginalRecord()
        {
            InitializeComponent();
        }

        private void FrmOriginalRecord_Load(object sender, EventArgs e)
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string beginDate = dtpBegin.Text;
            string endDate = dtpEnd.Text;
            int deptId = Convert.ToInt32(cbbDept.SelectedValue);
            int flag = checkBox1.Checked ? 1 : 0;
            string empCode = tbEmpCode.Text;
            string empName = tbEmpName.Text;
            btQuery.Enabled = false;
            LoadOriginalReport(beginDate, endDate, deptId, flag, empCode, empName);
            btQuery.Enabled = true;

        }

        #region 加载原始刷卡记录

        private void LoadOriginalReport(string beginDate, string endDate, int deptId, int deptType, string empCode, string empName)
        {
            try
            {
                DataTable dt = AttdService.GetOriginalRecord(beginDate, endDate, deptId, deptType, empCode, empName);
                var rds = new ReportDataSource("ReportOfOriginalRecord", dt);
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"/report/ReportOfOriginalRecord.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("查询原始记录报表失败:" + ex.Message);
                return;
            }
        }

        #endregion

    }
}
