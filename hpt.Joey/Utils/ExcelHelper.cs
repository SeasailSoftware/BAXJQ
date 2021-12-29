using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace HPT.Joey.Lib.Utils
{
    public class ExcelHelper : IDisposable
    {
        private readonly string _fileName; //文件名
        private IWorkbook _workbook;
        private FileStream _fs;
        private bool _disposed;

        public ExcelHelper(string fileName)
        {
            _fileName = fileName;
            _disposed = false;
        }


        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="titles"></param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="lastRowIsFooter"></param>
        /// <param name="summaryList"></param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, Tuple<string, string> titles, bool isColumnWritten = true, bool lastRowIsFooter = false, List<string> summaryList = null)
        {
            _fs = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (_fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0) // 2007版本
                _workbook = new XSSFWorkbook();
            else if (_fileName.IndexOf(".xls", StringComparison.Ordinal) > 0) // 2003版本
                _workbook = new HSSFWorkbook();

            try
            {
                ISheet sheet;
                if (_workbook != null)
                {
                    sheet = _workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }
                //取得列宽
                int[] arrColWidth = new int[data.Columns.Count];
                var headerStyle = _workbook.CreateCellStyle();
                headerStyle.Alignment = HorizontalAlignment.Center;
                headerStyle.FillForegroundColor = HSSFColor.Grey50Percent.Index;
                headerStyle.FillPattern = FillPattern.SolidForeground;
                headerStyle.BorderBottom = BorderStyle.Medium;
                headerStyle.BorderLeft = BorderStyle.Medium;
                headerStyle.BorderRight = BorderStyle.Medium;
                headerStyle.BorderRight = BorderStyle.Medium;
                headerStyle.LeftBorderColor = HSSFColor.Black.Index;
                headerStyle.RightBorderColor = HSSFColor.Black.Index;
                headerStyle.BottomBorderColor = HSSFColor.Black.Index;
                headerStyle.TopBorderColor = HSSFColor.Black.Index;

                var headerFont = _workbook.CreateFont();
                headerFont.IsBold = true;
                //headerFont.FontHeightInPoints = 16;
                headerStyle.SetFont(headerFont);

                var cellStyle = _workbook.CreateCellStyle();
                cellStyle.Alignment = HorizontalAlignment.Center;
                cellStyle.BorderBottom = BorderStyle.Thin;
                cellStyle.BorderLeft = BorderStyle.Thin;
                cellStyle.BorderRight = BorderStyle.Thin;
                cellStyle.BorderRight = BorderStyle.Thin;
                cellStyle.LeftBorderColor = HSSFColor.Black.Index;
                cellStyle.RightBorderColor = HSSFColor.Black.Index;
                cellStyle.BottomBorderColor = HSSFColor.Black.Index;
                cellStyle.TopBorderColor = HSSFColor.Black.Index;

                var titleStyle = _workbook.CreateCellStyle();
                titleStyle.Alignment = HorizontalAlignment.Center;
                var titleFont = _workbook.CreateFont();
                titleFont.IsBold = true;
                titleFont.Boldweight = short.MaxValue;
                titleFont.FontHeight = 14;
                titleStyle.SetFont(titleFont);

                var titleStyle1 = _workbook.CreateCellStyle();
                titleStyle1.Alignment = HorizontalAlignment.Right;
                var titleFont1 = _workbook.CreateFont();
                titleFont1.IsBold = true;
                titleStyle1.SetFont(titleFont1);

                int dataRowIndex = 0;
                if (titles != null)
                {
                    if (!string.IsNullOrWhiteSpace(titles.Item1))
                    {
                        var titleRow = sheet.CreateRow(0);
                        titleRow.CreateCell(0).SetCellValue(titles.Item1);
                        titleRow.GetCell(0).CellStyle = titleStyle;
                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, data.Columns.Count - 1));
                        dataRowIndex++;
                    }
                    if (!string.IsNullOrWhiteSpace(titles.Item2))

                    {
                        var titleRow = sheet.CreateRow(1);
                        titleRow.CreateCell(0).SetCellValue(titles.Item2);
                        titleRow.GetCell(0).CellStyle = titleStyle1;
                        sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, data.Columns.Count - 1));
                        dataRowIndex++;
                    }
                }

                //int count;
                int j;
                if (isColumnWritten) //写入DataTable的列名
                {
                    var row = sheet.CreateRow(dataRowIndex);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        var columnName = data.Columns[j].ColumnName;
                        var cell = row.CreateCell(j);
                        cell.SetCellValue(columnName);
                        cell.CellStyle = headerStyle;
                        arrColWidth[j] = Encoding.GetEncoding(936).GetBytes(columnName).Length;
                    }
                    dataRowIndex++;
                }

                int i;
                for (i = 0; i < data.Rows.Count; ++i)
                {
                    var row = sheet.CreateRow(dataRowIndex);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        var cellValue = data.Rows[i][j].ToString();
                        int intTemp = Encoding.GetEncoding(936).GetBytes(cellValue).Length;
                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                            sheet.SetColumnWidth(j, (arrColWidth[j] + 1) * 256);
                        }
                        row.CreateCell(j).SetCellValue(cellValue);
                        if (i + 1 == data.Rows.Count && lastRowIsFooter)
                        {
                            row.GetCell(j).CellStyle = headerStyle;
                        }
                        else
                        {
                            row.GetCell(j).CellStyle = cellStyle;
                        }
                    }
                    dataRowIndex++;
                }
                if (summaryList != null && summaryList.Any())
                {
                    var row = sheet.CreateRow(dataRowIndex);

                    for (j = 0; j < summaryList.Count; ++j)
                    {
                        var cellValue = summaryList[j];
                        int intTemp = Encoding.GetEncoding(936).GetBytes(cellValue).Length;
                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                            sheet.SetColumnWidth(j, (arrColWidth[j] + 1) * 256);
                        }
                        row.CreateCell(j).SetCellValue(cellValue);
                        row.GetCell(j).CellStyle = cellStyle;
                    }

                    dataRowIndex++;
                }
                _workbook.Write(_fs); //写入到excel
                return dataRowIndex;
            }
            catch (Exception ex)
            {
                //
                throw;
            }
            finally
            {
                if (_fs != null)
                    _fs.Dispose();
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn = true)
        {
            var data = new DataTable();
            try
            {
                _fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
                if (_fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0) // 2007版本
                    _workbook = new XSSFWorkbook(_fs);
                else if (_fileName.IndexOf(".xls", StringComparison.Ordinal) > 0) // 2003版本
                    _workbook = new HSSFWorkbook(_fs);

                ISheet sheet;
                if (sheetName != null)
                {
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    sheet = _workbook.GetSheet(sheetName) ?? _workbook.GetSheetAt(0);
                }
                else
                {
                    sheet = _workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    int startRow;
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Exception: " + ex.Message);
                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_fs != null)
                        _fs.Close();
                }

                _fs = null;
                _disposed = true;
            }
        }
    }
}