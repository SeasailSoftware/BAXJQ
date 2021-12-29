using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Utils.Common
{
    public class ExcelHelper
    {


        public static bool ExportToExcel(string fileName, System.Data.DataTable dt)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (var excel = new ExcelPackage(new FileInfo(fileName)))
                {
                    var ws = excel.Workbook.Worksheets.Add("Sheet1");
                    //导入表头
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ws.Cells[1, i+1].Value = dt.Columns[i].ColumnName;
                    }
                    //导入内容
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ws.Cells[i + 1, j].Value = dt.Rows[i][j].ToString();
                        }
                    }
                    //自适应表头
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ws.Column(i + 1).AutoFit();
                    }
                    excel.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>  
        /// 读取Excel文件数据到DataTable  
        /// </summary>  
        /// <param name="filePath">Excel文件路径</param>  
        public static void ImportFromExcel(string filepath, out DataTable dt)
        {
            dt = new DataTable();
            FileInfo existingFile = new FileInfo(filepath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                int colStart = worksheet.Dimension.Start.Column;  //工作区开始列
                int colEnd = worksheet.Dimension.End.Column;       //工作区结束列
                int rowStart = worksheet.Dimension.Start.Row;       //工作区开始行号
                int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

                for (int column = colStart; column <= colEnd; column++)
                {
                    dt.Columns.Add(worksheet.Cells[1, column].Value.ToString(), System.Type.GetType("System.String"));
                }

                for (int row = rowStart + 1; row <= rowEnd; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (int column = colStart; column <= colEnd; column++)
                    {
                        ExcelRange cell = worksheet.Cells[row, column];
                        if (cell.Value == null) continue;
                        dr[column-1] = cell.Value;
                    }
                    dt.Rows.Add(dr);
                }
            }
        }

        #region 导出到Excel
        public static bool Export(string fileName, DataGridView dgv)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
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
                    MessageBox.Show($"数据导入成功!文件存放路径为[{fileName}]", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导出数据失败:{ex.Message}", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion
    }
}
