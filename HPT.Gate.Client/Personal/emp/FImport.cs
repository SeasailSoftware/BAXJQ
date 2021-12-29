using hpt.gate.dataImport;
using HPT.Gate.Client.config;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.DataAccess.Service;
using HPT.Joey.Lib.Controls;
using HPT.Joey.Lib.Utils;
using Joey.UserControls;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HPT.Gate.Client.emp
{
    public partial class FImport : JWindow
    {
        private DataTable CurrentDateTable;
        /// <summary>
        /// 错误列表
        /// </summary>
        public List<string> errorList = new List<string>();
        public FImport()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }



        private void buttonItem34_Click(object sender, EventArgs e)
        {

        }



        #region 导入数据
        private string ImportEmpData(string deptName, string empCode, string empName, string icCardNo, string sex, string identityCard, string birthDay, string nation, string bornEarth, string marrige, string duty, string joinDate)
        {
            string result = string.Empty;
            if (!Utils.Common.StringHelper.isNumberic(empCode))
                return "人员编号格式有误:必须为纯数字";
            empCode = Convert.ToInt32(empCode).ToString("00000000");
            //处理IC卡
            if (!icCardNo.Trim().Equals(string.Empty))
            {
                switch (AppSettings.CardType)
                {
                    case 0:
                        icCardNo = Utils.Common.ArrayHelper.IntCardNoToHexCardNo(icCardNo);
                        break;
                    case 1:
                        break;
                }
                //检查卡号格式
                if (!Regex.IsMatch(icCardNo, "^[0-9A-Fa-f]+$"))
                    return "IC/ID卡号格式有误:必须为十进制数或者十六进制字符串!";

                if (icCardNo.Length != 8)
                    return "IC/ID卡长度有误!";
            }
            try
            {
                result = EmpInfoService.DataImport(deptName, empCode, empName, icCardNo, sex, identityCard, birthDay, nation, bornEarth, marrige, duty, joinDate, AppSettings.DeptImportType, AppSettings.EmpImportType,
                    AppSettings.CardImportType, AppSettings.TicketType, AppSettings.BeginDate, AppSettings.EndDate);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion




        /// <summary>
        /// 从xls文件得到dataset
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public DataTable GetXlsDataIEmp(string filepath)
        {

            FileInfo existingFile = new FileInfo(filepath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                int colStart = worksheet.Dimension.Start.Column;  //工作区开始列
                int colEnd = worksheet.Dimension.End.Column + 1;       //工作区结束列
                int rowStart = worksheet.Dimension.Start.Row;       //工作区开始行号
                int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

                DataTable dt = new DataTable();
                dt.Columns.Add("部门名称", System.Type.GetType("System.String"));
                dt.Columns.Add("人员编号", System.Type.GetType("System.String"));
                dt.Columns.Add("人员姓名", System.Type.GetType("System.String"));
                dt.Columns.Add("IC/ID卡", System.Type.GetType("System.String"));
                dt.Columns.Add("身份证序列号", System.Type.GetType("System.String"));
                dt.Columns.Add("身份证号码", System.Type.GetType("System.String"));
                dt.Columns.Add("性别", System.Type.GetType("System.String"));
                dt.Columns.Add("民族", System.Type.GetType("System.String"));
                dt.Columns.Add("电话", System.Type.GetType("System.String"));
                dt.Columns.Add("籍贯", System.Type.GetType("System.String"));
                dt.Columns.Add("出生年月", System.Type.GetType("System.String"));
                dt.Columns.Add("入职日期", System.Type.GetType("System.String"));
                dt.Columns.Add("职务", System.Type.GetType("System.String"));
                dt.Columns.Add("住址", System.Type.GetType("System.String"));
                dt.Columns.Add("导入状态", System.Type.GetType("System.String"));

                for (int row = rowStart + 1; row <= rowEnd; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (int column = colStart; column <= colEnd; column++)
                    {
                        ExcelRange cell = worksheet.Cells[row, column];
                        if (cell.Value == null) continue;
                        string columnName = string.Empty;
                        switch (column)
                        {
                            case 1:
                                columnName = "部门名称";
                                break;
                            case 2:
                                columnName = "人员编号";
                                break;
                            case 3:
                                columnName = "人员姓名";
                                break;
                            case 4:
                                columnName = "IC/ID卡";
                                break;
                            case 5:
                                columnName = "身份证序列号";
                                break;
                            case 6:
                                columnName = "身份证号码";
                                break;
                            case 7:
                                columnName = "性别";
                                break;
                            case 8:
                                columnName = "民族";
                                break;
                            case 9:
                                columnName = "电话";
                                break;
                            case 10:
                                columnName = "籍贯";
                                break;
                            case 11:
                                columnName = "出生年月";
                                break;
                            case 12:
                                columnName = "入职日期";
                                break;
                            case 13:
                                columnName = "职务";
                                break;
                            case 14:
                                columnName = "住址";
                                break;
                            case 15:
                                columnName = "导入状态";
                                break;
                        }
                        dr[columnName] = cell.Value;
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

        /// <summary>
        /// 从xls文件得到dataset
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public DataTable GetXlsDataIdentNo(string filepath)
        {

            FileInfo existingFile = new FileInfo(filepath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                int colStart = worksheet.Dimension.Start.Column;  //工作区开始列
                int colEnd = worksheet.Dimension.End.Column + 1;       //工作区结束列
                int rowStart = worksheet.Dimension.Start.Row;       //工作区开始行号
                int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

                DataTable dt = new DataTable();
                dt.Columns.Add("部门名称", System.Type.GetType("System.String"));
                dt.Columns.Add("人员编号", System.Type.GetType("System.String"));
                dt.Columns.Add("人员姓名", System.Type.GetType("System.String"));
                dt.Columns.Add("身份证号码", System.Type.GetType("System.String"));
                dt.Columns.Add("有效期开始日期", System.Type.GetType("System.String"));
                dt.Columns.Add("有效期结束日期", System.Type.GetType("System.String"));
                dt.Columns.Add("导入状态", System.Type.GetType("System.String"));
                for (int row = rowStart + 1; row <= rowEnd; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (int column = colStart; column <= colEnd; column++)
                    {
                        ExcelRange cell = worksheet.Cells[row, column];
                        if (cell.Value == null) continue;
                        // { cell.Value = ""; }
                        string columnName = string.Empty;
                        switch (column)
                        {
                            case 1:
                                columnName = "部门名称";
                                break;
                            case 2:
                                columnName = "人员编号";
                                break;
                            case 3:
                                columnName = "人员姓名";
                                break;
                            case 4:
                                columnName = "身份证号码";
                                break;
                            case 5:
                                columnName = "有效期开始日期";
                                cell.Value = Convert.ToDateTime(cell.Value).ToString("yyyy-MM-dd");
                                break;
                            case 6:
                                columnName = "有效期结束日期";
                                cell.Value = Convert.ToDateTime(cell.Value).ToString("yyyy-MM-dd");
                                break;
                            case 7:
                                columnName = "导入状态";
                                break;
                        }
                        dr[columnName] = cell.Value;
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }


        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string folderName = fbd.SelectedPath; //获得选择的文件夹路径
                string file = folderName + @"\批量导入身份证号码模板.xlsx";
                if (File.Exists(file)) File.Delete(file);
                using (var excel = new ExcelPackage(new FileInfo(file)))
                {
                    try
                    {
                        var ws = excel.Workbook.Worksheets.Add("Sheet1");
                        ws.Cells[1, 1].Value = "部门名称";
                        ws.Cells[1, 2].Value = "人员编号";
                        ws.Cells[1, 3].Value = "人员姓名";
                        ws.Cells[1, 4].Value = "身份证号码";
                        ws.Cells[1, 5].Value = "有效期开始日期";
                        ws.Cells[1, 6].Value = "有效期结束日期";
                        ws.Column(1).AutoFit();
                        ws.Column(2).AutoFit();
                        ws.Column(3).AutoFit();
                        ws.Column(4).AutoFit();
                        ws.Column(5).AutoFit();
                        ws.Column(6).AutoFit();
                        excel.Save();
                        MessageBoxHelper.Info("生成模板成功!模板位置为:" + file);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.Error("在生成人员信息模板时发生错误，错误信息为:" + ex.Message);
                        return;
                    }

                }
            }
        }




        private void buttonItem5_Click(object sender, EventArgs e)
        {
            FrmImportSettings importSettings = new FrmImportSettings();
            importSettings.ShowDialog();
        }

        private void btTemplate_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            //fbd.ShowDialog();
            string folderName = fbd.SelectedPath; //获得选择的文件夹路径
            string file = folderName + @"\通道闸数据导入模板.xlsx";
            //var file = @"Sample.xlsx";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            using (var excel = new ExcelPackage(new FileInfo(file)))
            {
                try
                {
                    var ws = excel.Workbook.Worksheets.Add("Sheet1");
                    ws.Cells[1, 1].Value = "部门名称";
                    ws.Cells[1, 2].Value = "人员编号";
                    ws.Cells[1, 3].Value = "人员姓名";
                    ws.Cells[1, 4].Value = "IC/ID卡";
                    ws.Cells[1, 5].Value = "身份证序列号";
                    ws.Cells[1, 6].Value = "身份证号码";
                    ws.Cells[1, 7].Value = "性别";
                    ws.Cells[1, 8].Value = "民族";
                    ws.Cells[1, 9].Value = "电话";
                    ws.Cells[1, 10].Value = "籍贯";
                    ws.Cells[1, 11].Value = "出生年月";
                    ws.Cells[1, 12].Value = "入职日期";
                    ws.Cells[1, 13].Value = "职务";
                    ws.Cells[1, 14].Value = "住址";
                    ws.Column(1).AutoFit();
                    ws.Column(2).AutoFit();
                    ws.Column(3).AutoFit();
                    ws.Column(4).AutoFit();
                    ws.Column(5).AutoFit();
                    ws.Column(6).AutoFit();
                    ws.Column(7).AutoFit();
                    ws.Column(8).AutoFit();
                    ws.Column(9).AutoFit();
                    ws.Column(10).AutoFit();
                    ws.Column(11).AutoFit();
                    ws.Column(12).AutoFit();
                    ws.Column(13).AutoFit();
                    ws.Column(14).AutoFit();
                    excel.Save();
                    MessageBoxHelper.Info($"生成模板成功!模板位置为:{file}");
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Info($"生成模板失败:{ex.Message}");
                    return;
                }
            }
        }



        private void btImport_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog() { InitialDirectory = AppSettings.LastImportPath, Filter = @"xls 文件 (.xls)|*.xls|xlsx 文件 (.xlsx)|*.xlsx", FilterIndex = 2, RestoreDirectory = true };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            AppSettings.LastPath = openFileDialog.FileName.Substring(0, openFileDialog.FileName.LastIndexOf("\\"));
            DataTable dt = null;
            try
            {
                Utils.Common.ExcelHelper.ImportFromExcel(openFileDialog.FileName, out dt);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Info($"读取Excel文件失败:{ex.Message}");
                return;
            }
            if (dt.Rows.Count == 0)
            {
                MessageBoxHelper.Info("没有可导入的人员信息!");
                return;
            }
            CurrentDateTable = dt;
            JProgressHelper process = new JProgressHelper();
            process.MessageInfo = "正在处理中,请稍后...";
            process.BackgroundWork = ImportData;
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }

        private void process_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            MessageBoxHelper.Info("人员导入完毕!");
        }

        #region 数据导入
        private void ImportData(Action<int> progress, Action<string> showMsg)
        {
            List<string> empCodes = EmpInfoService.GetAllEmpCodes();
            List<EmpInfo> newEmps = new List<EmpInfo>();
            List<EmpInfo> oldEmps = new List<EmpInfo>();
            DataTable dt = CurrentDateTable;
            List<DeptInfo> depts = DeptInfoService.ToList();
            foreach (DataRow row in dt.Rows)
            {

                string deptName = row[0].ToString();
                DeptInfo dept = depts.FirstOrDefault(p => p.DeptName.Equals(deptName));
                if (dept == null)
                {
                    if (AppSettings.DeptImportType == 0)
                    {
                        DataGridViewAddRow(row, "部门不存在不作导入!");
                        continue;
                    }
                    else
                        dept = depts.FirstOrDefault(p => p.DeptId == 1);
                }
                EmpInfo emp = new EmpInfo();
                emp.DeptId = dept.DeptId;
                string empCode = row[1].ToString();
                if (!StringValidate.IsEmpCode(empCode))
                {
                    this.Invoke(new Action(() =>
                    {
                        DataGridViewAddRow(row, "人员编号格式有误!");
                    }));
                    continue;
                }
                emp.EmpCode = empCode;
                string empName = row[2].ToString();
                if (!CheckEmpName(empName))
                {
                    DataGridViewAddRow(row, "人员姓名格式有误!");
                    continue;

                }
                emp.EmpName = empName;
                string icCardNo = row[3].ToString();
                if (!CheckICCardNo(icCardNo))
                {
                    DataGridViewAddRow(row, "IC/ID卡格式有误!");
                    continue;
                }
                if (string.IsNullOrEmpty(icCardNo))
                    emp.ICCardNo = "";
                else
                {
                    switch (AppSettings.CardType)
                    {
                        case 0:
                            byte[] array = BitConverter.GetBytes(Convert.ToUInt32(icCardNo));
                            Array.Reverse(array);
                            emp.ICCardNo = Utils.Common.ArrayHelper.ArrayToHex(array);
                            break;
                        case 1:
                            byte[] arr = BitConverter.GetBytes(Convert.ToUInt32(icCardNo));
                            emp.ICCardNo = Utils.Common.ArrayHelper.ArrayToHex(arr);
                            break;
                        case 2:
                            emp.ICCardNo = icCardNo;
                            break;
                        case 3:
                            emp.ICCardNo = icCardNo;
                            break;
                    }
                    if (AppSettings.CardType == 4)
                    {
                        byte[] arr = Utils.Common.ArrayHelper.HexToArray(icCardNo, 4);
                        Array.Reverse(arr);
                        emp.ICCardNo = Utils.Common.ArrayHelper.ArrayToHex(arr);
                    }
                }

                string idSerial = row[4].ToString();
                if (!CheckIDSerial(idSerial))
                {
                    DataGridViewAddRow(row, "身份证序列号格式有误");
                    continue;
                }
                emp.IDSerial = idSerial;
                string idCardNo = row[5].ToString();
                if (!CheckIDCardNo(idCardNo))
                {
                    DataGridViewAddRow(row, "身份证号码格式有误");
                    continue;
                }
                emp.IDCardNo = idCardNo;
                string sex = row[6].ToString();
                if (!sex.Equals("男") || !sex.Equals("女"))
                    sex = "男";
                emp.Sex = sex;
                string nation = row[7].ToString();
                emp.Nation = nation;
                string telephone = row[8].ToString();
                emp.Telephone = telephone;
                string bornEarth = row[9].ToString();

                string birthDay = row[10].ToString();
                if (!CheckBirthDay(birthDay))
                {
                    birthDay = DateTime.Now.ToString("yyyy-MM-dd");
                }
                emp.BirthDay = birthDay;
                string joinDate = row[11].ToString();
                if (!CheckJoinDate(joinDate))
                {
                    joinDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                emp.JoinDate = joinDate;
                string duty = row[12].ToString();
                emp.Duty = duty;
                string address = row[13].ToString();
                emp.BornEarth = address;
                emp.EnglishName = "";
                emp.IdentityCard = emp.IDCardNo;
                emp.Marrige = "";
                emp.BeginDate = AppSettings.BeginDate;
                emp.EndDate = AppSettings.EndDate;
                emp.LeaveDate = "";
                emp.TicketType = AppSettings.TicketType;
                if (empCodes.Exists(p => p.Equals(emp.EmpCode)))
                    oldEmps.Add(emp);
                else
                {
                    newEmps.Add(emp);
                    empCodes.Add(emp.EmpCode);
                }
            }
            try
            {
                int max = newEmps.Count + oldEmps.Count;
                if (max == 0)
                    progress(100);
                else
                {
                    int index = 0;
                    List<EmpInfo> currents = new List<EmpInfo>();
                    for (int i = 0; i < newEmps.Count; i++)
                    {
                        currents.Add(newEmps[i]);
                        index++;
                        if (((i + 1) % 100 == 0 || (i + 1) == newEmps.Count))
                        {
                            EmpInfoService.Insert(currents);
                            progress(index * 100 / max);
                            currents.Clear();
                        }
                    }
                    for (int i = 0; i < oldEmps.Count; i++)
                    {
                        currents.Add(oldEmps[i]);
                        index++;
                        if (((i + 1) % 100 == 0 || (i + 1) == oldEmps.Count))
                        {
                            EmpInfoService.Update(currents);
                            progress(index * 100 / max);
                            currents.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Info($"导入失败:{ex.Message}");
            }
        }

        private void DataGridViewAddRow(DataRow row, string msg)
        {
            this.Invoke(new Action(() =>
            {
                int rowIndex = dgvEmps.Rows.Add();
                dgvEmps.Rows[rowIndex].Cells[0].Value = row[0].ToString();
                dgvEmps.Rows[rowIndex].Cells[1].Value = row[1].ToString();
                dgvEmps.Rows[rowIndex].Cells[2].Value = row[2].ToString();
                dgvEmps.Rows[rowIndex].Cells[3].Value = row[3].ToString();
                dgvEmps.Rows[rowIndex].Cells[4].Value = row[4].ToString();
                dgvEmps.Rows[rowIndex].Cells[5].Value = row[5].ToString();
                dgvEmps.Rows[rowIndex].Cells[6].Value = row[6].ToString();
                dgvEmps.Rows[rowIndex].Cells[7].Value = row[7].ToString();
                dgvEmps.Rows[rowIndex].Cells[8].Value = row[8].ToString();
                dgvEmps.Rows[rowIndex].Cells[9].Value = row[9].ToString();
                dgvEmps.Rows[rowIndex].Cells[10].Value = row[10].ToString();
                dgvEmps.Rows[rowIndex].Cells[11].Value = row[11].ToString();
                dgvEmps.Rows[rowIndex].Cells[12].Value = row[12].ToString();
                dgvEmps.Rows[rowIndex].Cells[13].Value = row[13].ToString();
                dgvEmps.Rows[rowIndex].Cells[14].Value = msg;
            }));
        }



        #endregion

        #region 设置错误信息
        private void SetErrorMessage(string msg, int index)
        {
            this.Invoke(new Action(() =>
            {
                dgvEmps.Rows[index].Cells["ImportStatus"].Value = msg;
                dgvEmps.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                dgvEmps.Rows[index].Selected = true;        // 设置为选中.(index为选重的记录索引)
                dgvEmps.FirstDisplayedScrollingRowIndex = index;  // 设置在当前区域的第一行显示
            }));
        }
        #endregion


        #region 检查人员姓名
        private bool CheckEmpName(string empName)
        {
            if (Encoding.ASCII.GetBytes(empName).Length > 20)
                return false;
            return true;
        }
        #endregion

        #region 检查IC/ID卡
        private bool CheckICCardNo(string icCardNo)
        {
            bool flag = true;
            switch (AppSettings.CardType)
            {
                case 0:
                    if (!string.IsNullOrEmpty(icCardNo))
                        flag = icCardNo.Length == 8 && Regex.IsMatch(icCardNo, @"^[0-9]*[1-9][0-9]*$");

                    break;
                case 1:
                    if (!string.IsNullOrEmpty(icCardNo))
                        flag = icCardNo.Length == 10 && Regex.IsMatch(icCardNo, @"^[0-9]*[1-9][0-9]*$");
                    break;
                case 2:
                    if (!string.IsNullOrEmpty(icCardNo))
                        flag = icCardNo.Length == 8 && Regex.IsMatch(icCardNo, @"[A-Fa-f0-9]+$");
                    break;
                case 3:
                    if (!string.IsNullOrEmpty(icCardNo))
                        flag = icCardNo.Length == 8 && Regex.IsMatch(icCardNo, @"[A-Fa-f0-9]+$");
                    break;
            }
            return flag;
        }
        #endregion

        #region 检查生日
        private bool CheckBirthDay(string birthDay)
        {
            DateTime dt;
            if (DateTime.TryParse(birthDay, out dt))
                return true;
            return false;
        }
        #endregion

        #region 检查入职日期
        private bool CheckJoinDate(string joinDate)
        {
            DateTime dt;
            if (DateTime.TryParse(joinDate, out dt))
            {
                if (dt > DateTime.Now)
                    return false;
                return true;
            }
            return false;
        }
        #endregion

        #region 检查身份证序列号
        private bool CheckIDSerial(string idSerial)
        {
            bool flag = true;
            if (!string.IsNullOrEmpty(idSerial))
                flag = idSerial.Length == 16 && Regex.IsMatch(idSerial, @"[A-Fa-f0-9]+$");
            return flag;
        }
        #endregion

        #region 检查身份证号码
        private bool CheckIDCardNo(string idCardNo)
        {
            bool flag = true;
            if (!string.IsNullOrEmpty(idCardNo))
                flag = idCardNo.Length == 18 && Regex.IsMatch(idCardNo, @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$");
            return flag;
        }
        #endregion

        private void panel_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
