using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using Joey.UserControls;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Client.Personal.emp
{
    public partial class FrmRecords : JWindow
    {
        public FrmRecords()
        {
            InitializeComponent();
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            int deptId = Convert.ToInt32(cbbDept.SelectedValue);
            int deptType = ckbDept.Checked ? 1 : 0;
            string empCode = tbEmpCode.Text;
            string empName = tbEmpName.Text;
            string beginDate = dtpBegin.Text;
            string endDate = dtpEnd.Text + " 23:59";
            Task.Factory.StartNew(() =>
            {
                this.Invoke(new Action(() =>
                {
                    btFind.Enabled = false;
                    btExPort.Enabled = false;
                }));

                List<Record> records = RecordService.Find(0, deptId, deptType, empCode, empName, 0, "", 0, beginDate, endDate, 1);
                this.Invoke(new Action(() =>
                {
                    dgvRecords.Rows.Clear();
                    foreach (Record record in records)
                    {
                        int rowIndex = dgvRecords.Rows.Add();
                        dgvRecords.Rows[rowIndex].Cells[0].Value = record.DeptName;
                        dgvRecords.Rows[rowIndex].Cells[1].Value = record.EmpCode;
                        dgvRecords.Rows[rowIndex].Cells[2].Value = record.EmpName;
                        dgvRecords.Rows[rowIndex].Cells[3].Value = record.CardNo;
                        dgvRecords.Rows[rowIndex].Cells[4].Value = record.TypeString;
                        dgvRecords.Rows[rowIndex].Cells[5].Value = record.DeviceName;
                        dgvRecords.Rows[rowIndex].Cells[6].Value = record.RecDatetime;
                        dgvRecords.Rows[rowIndex].Cells[7].Value = record.IOFlag;
                    }
                    label_count.Text = $"共查询到{records.Count}条记录!";
                }));
                this.Invoke(new Action(() =>
                {
                    btFind.Enabled = true;
                    btExPort.Enabled = true;
                }));
            });

        }

        private void FrmRecords_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillDeptComboBox(cbbDept);
        }

        private void btExPort_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            string folderName = fbd.SelectedPath; //获得选择的文件夹路径
            DataGridView dgv = null;
            string file = string.Empty;
            btFind.Enabled = false;
            btExPort.Enabled = false;
            file = $@"{folderName}\出入记录报表({DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}).xlsx";
            dgv = dgvRecords;
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

    }
}
