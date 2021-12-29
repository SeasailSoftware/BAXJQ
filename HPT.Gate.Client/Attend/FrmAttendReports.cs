using HPT.Gate.Client.Base;
using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Client.Attend
{
    public partial class FrmAttendReports : FrmBase
    {
        public FrmAttendReports()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            btFind.Enabled = false;
            btExPort.Enabled = false;
            Cursor = Cursors.WaitCursor;
            int deptId = Convert.ToInt32(cbbDept.SelectedValue);
            int deptType = ckbDept.Checked ? 1 : 0;
            string empCode = tbEmpCode.Text;
            string empName = tbEmpName.Text;
            string beginDate = dtpBegin.Text;
            string endDate = dtpEnd.Text;
            int index = tabControl1.SelectedTabIndex;
            int rowIndex;
            switch (index)
            {
                case 0:
                    dgvRecord.DataSource = null;
                    dgvRecord.Rows.Clear();
                    Application.DoEvents();
                    endDate = endDate + " 23:59";
                    List<AttendData> dataList = AttendDataService.Find(deptId, deptType, empCode, empName, beginDate, endDate);
                    foreach (AttendData data in dataList)
                    {
                        rowIndex = dgvRecord.Rows.Add();
                        dgvRecord.Rows[rowIndex].Cells[0].Value = data.RecId;
                        dgvRecord.Rows[rowIndex].Cells[1].Value = data.DeptId;
                        dgvRecord.Rows[rowIndex].Cells[2].Value = data.DeptName;
                        dgvRecord.Rows[rowIndex].Cells[3].Value = data.EmpId;
                        dgvRecord.Rows[rowIndex].Cells[4].Value = data.EmpCode;
                        dgvRecord.Rows[rowIndex].Cells[5].Value = data.EmpName;
                        dgvRecord.Rows[rowIndex].Cells[6].Value = data.RecDate;
                        dgvRecord.Rows[rowIndex].Cells[7].Value = data.RecTime;
                        dgvRecord.Rows[rowIndex].Cells[8].Value = data.Week;
                        dgvRecord.Rows[rowIndex].Cells[9].Value = data.CardNo;
                        dgvRecord.Rows[rowIndex].Cells[10].Value = data.RecordType;
                        dgvRecord.Rows[rowIndex].Cells[11].Value = data.DeviceName;
                        dgvRecord.Rows[rowIndex].Cells[12].Value = data.IOFlag;
                        dgvRecord.Rows[rowIndex].Cells[13].Value = data.Passed;
                        Application.DoEvents();
                    }
                    break;
                case 1:
                    dgvAttendDetail.DataSource = null;
                    dgvAttendDetail.Rows.Clear();
                    Application.DoEvents();
                    List<AttendDetail> details = AttendDetailService.Find(deptId, deptType, empCode, empName, beginDate, endDate);
                    foreach (AttendDetail detail in details)
                    {
                        rowIndex = dgvAttendDetail.Rows.Add();
                        dgvAttendDetail.Rows[rowIndex].Cells[0].Value = detail.RecId;
                        dgvAttendDetail.Rows[rowIndex].Cells[1].Value = detail.DeptId;
                        dgvAttendDetail.Rows[rowIndex].Cells[2].Value = detail.DeptName;
                        dgvAttendDetail.Rows[rowIndex].Cells[3].Value = detail.EmpId;
                        dgvAttendDetail.Rows[rowIndex].Cells[4].Value = detail.EmpCode;
                        dgvAttendDetail.Rows[rowIndex].Cells[5].Value = detail.EmpName;
                        dgvAttendDetail.Rows[rowIndex].Cells[6].Value = detail.RecDate;
                        dgvAttendDetail.Rows[rowIndex].Cells[7].Value = detail.Week;
                        dgvAttendDetail.Rows[rowIndex].Cells[8].Value = detail.GroupName;
                        dgvAttendDetail.Rows[rowIndex].Cells[9].Value = detail.TimeOfSignIn;
                        dgvAttendDetail.Rows[rowIndex].Cells[10].Value = detail.TimeOfSignOut;
                        dgvAttendDetail.Rows[rowIndex].Cells[11].Value = detail.SignIn;
                        dgvAttendDetail.Rows[rowIndex].Cells[12].Value = detail.SignOut;
                        dgvAttendDetail.Rows[rowIndex].Cells[13].Value = detail.ShouldAttd;
                        dgvAttendDetail.Rows[rowIndex].Cells[14].Value = detail.Attded;
                        dgvAttendDetail.Rows[rowIndex].Cells[15].Value = detail.LateMinutes;
                        dgvAttendDetail.Rows[rowIndex].Cells[16].Value = detail.EarlyMinutes;
                        dgvAttendDetail.Rows[rowIndex].Cells[17].Value = detail.Absent;
                        dgvAttendDetail.Rows[rowIndex].Cells[18].Value = detail.OTMinutes;
                        dgvAttendDetail.Rows[rowIndex].Cells[19].Value = detail.WorkMinutes;
                        dgvAttendDetail.Rows[rowIndex].Cells[20].Value = detail.ShouldSignIn;
                        dgvAttendDetail.Rows[rowIndex].Cells[21].Value = detail.ShouldSignOut;
                        Application.DoEvents();
                    }
                    break;
                case 2:
                    dgvSumaryPersonal.DataSource = null;
                    dgvSumaryPersonal.Rows.Clear();
                    Application.DoEvents();
                    List<AttendSummaryOfPersonal> summarys = AttendSummaryOfPersonalService.Find(deptId, deptType, empCode, empName, beginDate, endDate);
                    foreach (AttendSummaryOfPersonal summary in summarys)
                    {
                        rowIndex = dgvSumaryPersonal.Rows.Add();
                        dgvSumaryPersonal.Rows[rowIndex].Cells[0].Value = summary.DeptId;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[1].Value = summary.DeptName;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[2].Value = summary.EmpId;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[3].Value = summary.EmpCode;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[4].Value = summary.EmpName;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[5].Value = summary.BeginDate;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[6].Value = summary.EndDate;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[7].Value = summary.ShouldAttd;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[8].Value = summary.Attded;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[9].Value = summary.LateMinutes;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[10].Value = summary.EarlyMinutes;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[11].Value = summary.Absent;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[12].Value = summary.OTMinutes;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[13].Value = summary.WorkMinutes;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[14].Value = summary.ShouldSignIn;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[15].Value = summary.SignIned;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[16].Value = summary.UnSignIn;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[17].Value = summary.ShouldSignOut;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[18].Value = summary.SignOuted;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[19].Value = summary.UnSignOut;
                        dgvSumaryPersonal.Rows[rowIndex].Cells[20].Value = summary.Leave;
                        Application.DoEvents();
                    }
                    break;
                case 3:
                    break;
            }
            Cursor = Cursors.Default;
            btFind.Enabled = true;
            btExPort.Enabled = true;
        }

        private void FrmAttendReports_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillDeptComboBox(cbbDept);
        }

        private void dgvAttendDetail_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
dgvAttendDetail.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvAttendDetail.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvAttendDetail.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dvgRecord_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
dgvRecord.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvRecord.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvRecord.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvSumaryPersonal_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
dgvSumaryPersonal.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvSumaryPersonal.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvSumaryPersonal.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            string folderName = fbd.SelectedPath; //获得选择的文件夹路径
            DataGridView dgv = null;
            string file = string.Empty;
            btFind.Enabled = false;
            btExPort.Enabled = false;
            int index = tabControl1.SelectedTabIndex;
            switch (index)
            {
                case 0:
                    file = $@"{folderName}\凯塞德员工原始刷卡记录报表({DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}).xlsx";
                    dgv = dgvRecord;
                    break;
                case 1:
                    file = $@"{folderName}\凯塞德员工考勤明细报表({DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}).xlsx";
                    dgv = dgvAttendDetail;
                    break;
                case 2:
                    file = $@"{folderName}\凯塞德员工个人考勤汇总报表({DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}).xlsx";
                    dgv = dgvSumaryPersonal;
                    break;
            }
            ExportToExcel(file, dgv);
            btFind.Enabled = true;
            btExPort.Enabled = true;
        }

        #region 导出到Excel
        private void ExportToExcel(string fileName, DataGridView dgv)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            try
            {
                using (var excel = new ExcelPackage(new FileInfo(fileName)))
                {
                    var ws = excel.Workbook.Worksheets.Add("Sheet1");
                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        int columnIndex = column.Index + 1;
                        ws.Cells[1, columnIndex].Value = column.HeaderText;
                    }
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        int rowIndex = row.Index + 2;
                        foreach (DataGridViewColumn column in dgv.Columns)
                        {
                            int index = column.Index;
                            ws.Cells[rowIndex, index + 1].Value = row.Cells[index].Value;
                        }
                    }
                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        int index = column.Index;
                        ws.Column(index + 1).AutoFit();
                    }
                    excel.Save();
                    MessageBoxHelper.Info($"数据导入成功!文件存放路径为[{fileName}]");
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"导出数据失败:{ex.Message}");
            }
        }

        #endregion

        private void dgvOT_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
            dgvOT.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvOT.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvOT.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
