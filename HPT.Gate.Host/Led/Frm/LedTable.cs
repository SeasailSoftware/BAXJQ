using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Xml;
using System.Threading;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Host.Base;
using hpt.gate.Util;

namespace hpt.gate.Led.Frm
{
    public partial class LedTable : WinBase
    {
        public int curRtbStart = 0;
        public int len = 0;
        public string _FilePath = string.Empty;
        public LedTable(string fileName)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _FilePath = Application.StartupPath + @"/led/" + fileName;
            if (!File.Exists(_FilePath))
            {
                File.Create(_FilePath);
                Thread.Sleep(1000);
            }
        }

        private void LedTemplate_Load(object sender, EventArgs e)
        {
            try
            {
                ComboBoxHelper.FillLedDynParas(comboBox5);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门人数统计列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadXml(this._FilePath);
        }

        private void LoadXml(string _fildPath)
        {
            List<ColumnWidth> cellWidthList = new List<ColumnWidth>();
            DataTable dt = new DataTable();
            ///<----------------开始读取配置文件------------------>
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(_fildPath);    //加载Xml文件  
                XmlElement root = doc.DocumentElement;   //获取根节点  
                XmlNodeList rows = root.SelectNodes("//Table");
                ///root.GetElementsByTagName("person"); //获取person子节点集合 
                foreach (XmlNode node in rows)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        foreach (XmlNode childNode in child)
                        {

                            string columnName = childNode.Name;
                            dt.Columns.Add(new DataColumn(columnName, typeof(string)));
                            int cellWidth = Convert.ToInt32(((XmlElement)childNode).GetAttribute("Width"));
                            ColumnWidth cell = new ColumnWidth();
                            cell.ColumnName = columnName;
                            cell.Width = cellWidth;
                            cellWidthList.Add(cell);
                            ///string value = ((XmlElement)childNode).GetAttribute("Value");
                        }
                        break;
                    }
                }

                foreach (XmlNode node in rows)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        DataRow row = dt.NewRow();
                        foreach (XmlNode childNode in child)
                        {

                            string columnName = childNode.Name;
                            string value = ((XmlElement)childNode).GetAttribute("Value").ToString();
                            row[columnName] = value;
                        }
                        dt.Rows.Add(row);
                    }
                }

                dataGridView1.DataSource = dt;
                ///<----------------------------------------------------->
                ///<--------------设置DataGridView列宽--------------->

                foreach (ColumnWidth column in cellWidthList)
                {
                    foreach (DataGridViewColumn dColumn in dataGridView1.Columns)
                    {
                        if (column.ColumnName.Equals(dColumn.Name))
                        {
                            dColumn.Width = column.Width;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }

            ///<------------------------------------------------------>
        }





        /// <summary>
        /// 改变字体
        /// </summary>
        /// <param name="fontName"></param>
        private void ChangeFont(String fontName)
        {
            Font font = dataGridView1.SelectedCells[0].Style.Font;
            dataGridView1.SelectedCells[0].Style.Font = new Font(fontName, font == null ? 12 : font.Size);
        }


        /// <summary>
        /// 设置字体大小
        /// </summary>
        /// <param name="fontSize"></param>
        private void ChangFontSize(float fontSize)
        {
            Font font = dataGridView1.SelectedCells[0].Style.Font;
            if (font == null)
            {
                font = new Font("宋体", fontSize);
            }
            else
            {
                font = new Font(font.FontFamily, fontSize);
            }
            dataGridView1.SelectedCells[0].Style.Font = font;
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                int i = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = " ";
                dataGridView1.EndEdit();
            }
            else
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                ///dataGridView1.DataSource = null;
                dt.Rows.Add();
                dataGridView1.DataSource = dt;
            }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("请先添加行!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int count = dataGridView1.Columns.Count;
            string header = dataGridView1.Columns[count - 1].HeaderText;
            string columnName = "Column" + (Convert.ToInt32(header.Replace("Column", "")) + 1);
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = columnName;
            dataGridView1.Columns.Insert(count, column);
            dataGridView1.Rows[0].Cells[columnName].Value = " ";
            dataGridView1.EndEdit();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Rows.Count;
            if (count > 1)
            {
                DataGridViewRow row = dataGridView1.Rows[count - 1];
                try
                {
                    dataGridView1.EndEdit();
                    dataGridView1.Rows.Remove(row);
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Columns.Count;
            if (count > 2)
            {
                DataGridViewColumn column = dataGridView1.Columns[count - 1];
                try
                {
                    dataGridView1.EndEdit();
                    dataGridView1.Columns.Remove(column);
                }
                catch (Exception ex)
                {

                }
            }
        }


        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            /*
            if (e.RowIndex != -1 && Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value) == "GP")
            {
                e.CellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                e.CellStyle.WrapMode = DataGridViewTriState.True;
                MerageRowSpan(dataGridView1, e, 0, 1);
            }
            if ((e.RowIndex == dataGridView1.Rows.Count - 1) && (e.ColumnIndex == 0))
            {
                Rectangle rect = new Rectangle();
                rect.X = e.CellBounds.X;
                rect.Y = e.CellBounds.Y;
                rect.Width = e.CellBounds.Width;
                rect.Height = e.CellBounds.Height;
                e.Paint(rect, DataGridViewPaintParts.ContentBackground);
                e.PaintBackground(rect, false);
                e.Handled = true;
            }
             * */
        }
        private static SortedList rowSpan = new SortedList();//取得需要重新绘制的单元格
        private static string rowValue = "";//重新绘制的文本框内容

        #region  单元格绘制
        /// <summary>
        /// 
        /// DataGridView合并单元格(横向)
        /// </summary>
        /// <param name="dgv">绘制的DataGridview </param>
        /// <param name="cellArgs">绘制单元格的参数（DataGridview的CellPainting事件中参数）</param>
        /// <param name="minColIndex">起始单元格在DataGridView中的索引号</param>
        /// <param name="maxColIndex">结束单元格在DataGridView中的索引号</param>
        public void MerageRowSpan(DataGridView dgv, DataGridViewCellPaintingEventArgs cellArgs, int minColIndex, int maxColIndex)
        {
            if (cellArgs.ColumnIndex < minColIndex || cellArgs.ColumnIndex > maxColIndex) return;

            Rectangle rect = new Rectangle();
            using (Brush gridBrush = new SolidBrush(dgv.GridColor),
                backColorBrush = new SolidBrush(cellArgs.CellStyle.BackColor))
            {
                //抹去原来的cell背景
                cellArgs.Graphics.FillRectangle(backColorBrush, cellArgs.CellBounds);
            }
            cellArgs.Handled = true;

            if (rowSpan[cellArgs.ColumnIndex] == null)
            {
                //首先判断当前单元格是不是需要重绘的单元格
                //保留此单元格的信息，并抹去此单元格的背景
                rect.X = cellArgs.CellBounds.X;
                rect.Y = cellArgs.CellBounds.Y;
                rect.Width = cellArgs.CellBounds.Width;
                rect.Height = cellArgs.CellBounds.Height;

                rowValue += cellArgs.Value.ToString();
                rowSpan.Add(cellArgs.ColumnIndex, rect);
                if (cellArgs.ColumnIndex != maxColIndex)
                    return;
                MeragePrint(dgv, cellArgs, minColIndex, maxColIndex);
            }
            else
            {
                IsPostMerage(dgv, cellArgs, minColIndex, maxColIndex);
            }
        }

        /// <summary>
        /// 不是初次单元格绘制
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="cellArgs"></param>
        /// <param name="minColIndex"></param>
        /// <param name="maxColIndex"></param>
        public void IsPostMerage(DataGridView dgv, DataGridViewCellPaintingEventArgs cellArgs, int minColIndex, int maxColIndex)
        {
            //比较单元是否有变化
            Rectangle rectArgs = (Rectangle)rowSpan[cellArgs.ColumnIndex];
            if (rectArgs.X != cellArgs.CellBounds.X || rectArgs.Y != cellArgs.CellBounds.Y
                || rectArgs.Width != cellArgs.CellBounds.Width || rectArgs.Height != cellArgs.CellBounds.Height)
            {
                rectArgs.X = cellArgs.CellBounds.X;
                rectArgs.Y = cellArgs.CellBounds.Y;
                rectArgs.Width = cellArgs.CellBounds.Width;
                rectArgs.Height = cellArgs.CellBounds.Height;
                rowSpan[cellArgs.ColumnIndex] = rectArgs;
            }
            MeragePrint(dgv, cellArgs, minColIndex, maxColIndex);

        }

        //画制单元格
        private void MeragePrint(DataGridView dgv, DataGridViewCellPaintingEventArgs cellArgs, int minColIndex, int maxColIndex)
        {

            int width = 0;//合并后单元格总宽度
            int height = cellArgs.CellBounds.Height;//合并后单元格总高度

            for (int i = minColIndex; i <= maxColIndex; i++)
            {
                width += ((Rectangle)rowSpan[i]).Width;
            }

            Rectangle rectBegin = (Rectangle)rowSpan[minColIndex];//合并第一个单元格的位置信息
            Rectangle rectEnd = (Rectangle)rowSpan[maxColIndex];//合并最后一个单元格的位置信息

            //合并单元格的位置信息
            Rectangle reBounds = new Rectangle();
            reBounds.X = rectBegin.X;
            reBounds.Y = rectBegin.Y;
            reBounds.Width = width - 1;
            reBounds.Height = height - 1;


            using (Brush gridBrush = new SolidBrush(dgv.GridColor),
                         backColorBrush = new SolidBrush(cellArgs.CellStyle.BackColor))
            {
                using (Pen gridLinePen = new Pen(gridBrush))
                {
                    // 画出上下两条边线，左右边线无
                    Point blPoint = new Point(rectBegin.Left, rectBegin.Bottom - 1);//底线左边位置
                    Point brPoint = new Point(rectEnd.Right - 1, rectEnd.Bottom - 1);//底线右边位置
                    cellArgs.Graphics.DrawLine(gridLinePen, blPoint, brPoint);//下边线

                    Point tlPoint = new Point(rectBegin.Left, rectBegin.Top);//上边线左边位置
                    Point trPoint = new Point(rectEnd.Right - 1, rectEnd.Top);//上边线右边位置
                    cellArgs.Graphics.DrawLine(gridLinePen, tlPoint, trPoint); //上边线

                    Point ltPoint = new Point(rectBegin.Left, rectBegin.Top);//左边线顶部位置
                    Point lbPoint = new Point(rectBegin.Left, rectBegin.Bottom - 1);//左边线底部位置
                    cellArgs.Graphics.DrawLine(gridLinePen, ltPoint, lbPoint); //左边线

                    Point rtPoint = new Point(rectEnd.Right - 1, rectEnd.Top);//右边线顶部位置
                    Point rbPoint = new Point(rectEnd.Right - 1, rectEnd.Bottom - 1);//右边线底部位置
                    cellArgs.Graphics.DrawLine(gridLinePen, rtPoint, rbPoint); //右边线

                    //计算绘制字符串的位置
                    SizeF sf = cellArgs.Graphics.MeasureString(rowValue, cellArgs.CellStyle.Font);
                    float lstr = (width - sf.Width) / 2;
                    float rstr = (height - sf.Height) / 2;

                    //画出文本框
                    if (rowValue != "")
                    {
                        cellArgs.Graphics.DrawString(rowValue, cellArgs.CellStyle.Font,
                                                   new SolidBrush(cellArgs.CellStyle.ForeColor),
                                                     rectBegin.Left + lstr,
                                                     rectBegin.Top + rstr,
                                                     StringFormat.GenericDefault);
                    }
                }
                cellArgs.Handled = true;
            }

        }
        #endregion

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (comboBox5.Items.Count == 0)
            {
                MessageBox.Show("没有可操作的部门人数统计!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string msg = @"【" + comboBox5.SelectedValue + @"】";
            if (dataGridView1.CurrentCell != null)
            {
                try
                {
                    var value = dataGridView1.CurrentCell.Value;
                    if (value == null)
                    {
                        dataGridView1.CurrentCell.Value = msg;
                    }
                    else
                    {
                        dataGridView1.CurrentCell.Value = value.ToString() + msg;
                    }
                    dataGridView1.EndEdit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("插入数据参数失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择需要插入的位置!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridView1.EndEdit();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ///DgvToXml(_FilePath);
            DgvToConfigxml(_FilePath);
        }

        private void DgvToConfigxml(string _ConfigPath)
        {
            ///_ConfigPath = Application.StartupPath + @"/led/XmlConfig0.XML";
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data available!", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!File.Exists(_ConfigPath))
            {
                File.Create(_ConfigPath);
            }
            string str = _ConfigPath;
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "GB2312", null);
            doc.AppendChild(docNode);

            // Create and insert a new element.  
            XmlNode tableNode = doc.CreateElement("Table");
            doc.AppendChild(tableNode);

            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    XmlNode rowNode = doc.CreateElement("Row" + i);
                    ///行高
                    XmlAttribute rowHeightAttribute = doc.CreateAttribute("Height");
                    int rowHeight = dataGridView1.Rows[i].Height;
                    rowHeightAttribute.Value = rowHeight.ToString();
                    rowNode.Attributes.Append(rowHeightAttribute);
                    ///行宽
                    XmlAttribute rowWidthAttribute = doc.CreateAttribute("Width");
                    int rowWidth = dataGridView1.Width;
                    rowWidthAttribute.Value = rowWidth.ToString();
                    rowNode.Attributes.Append(rowWidthAttribute);

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        string header = dataGridView1.Columns[j].HeaderText.Equals(string.Empty) ? "Column" + j : dataGridView1.Columns[j].HeaderText;
                        XmlNode cellNode = doc.CreateElement(header);
                        XmlAttribute cellWidthAttribute = doc.CreateAttribute("Width");
                        int width = dataGridView1.Columns[j].Width;
                        cellWidthAttribute.Value = width.ToString();
                        cellNode.Attributes.Append(cellWidthAttribute);

                        XmlAttribute cellValueAttribute = doc.CreateAttribute("Value");
                        string value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        cellValueAttribute.Value = value;
                        cellNode.Attributes.Append(cellValueAttribute);
                        rowNode.AppendChild(cellNode);
                    }
                    tableNode.AppendChild(rowNode);
                }
                doc.Save(str);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exporting Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void DgvToXml(string path)
        {
            /*
            Stream myStream = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            try
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"GB2312\" ?>");
                sw.WriteLine("<Table>");

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    sw.WriteLine("<Row" + i + ">");
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        sw.WriteLine("<" + dataGridView1.Columns[j].HeaderText + ">" + dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() + "</" + dataGridView1.Columns[j].HeaderText + ">");
                    sw.WriteLine("</Row" + i + ">");
                }
                sw.WriteLine("</NewXML>");
                sw.Close();
                myStream.Close();
                ///MessageBox.Show("Data has been exported to：" + saveFileDialog.FileName.ToString(), "Exporting Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exporting Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data available!", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            */

            string str = path;
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "GB2312", null);
            doc.AppendChild(docNode);

            // Create and insert a new element.  
            XmlNode tableNode = doc.CreateElement("Table");
            doc.AppendChild(tableNode);

            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    XmlNode rowNode = doc.CreateElement("Row" + i);

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        string header = dataGridView1.Columns[j].HeaderText.Equals(string.Empty) ? "Column" + j : dataGridView1.Columns[j].HeaderText;
                        XmlAttribute cellValueAttribute = doc.CreateAttribute(header);
                        string value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        cellValueAttribute.Value = value;
                        rowNode.Attributes.Append(cellValueAttribute);
                        tableNode.AppendChild(rowNode);
                    }
                }
                doc.Save(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exporting Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public DataTable AppendDataTable(DataTable hostDt, DataTable clientDt)
        {
            if (hostDt != null && hostDt.Rows.Count > 0)
            {
                DataRow dr;
                for (int i = 0; i < clientDt.Columns.Count; i++)
                {
                    hostDt.Columns.Add(new DataColumn(clientDt.Columns[i].ColumnName));

                    if (clientDt.Rows.Count > 0)
                        for (int j = 0; j < clientDt.Rows.Count; j++)
                        {
                            dr = hostDt.Rows[j];
                            dr[hostDt.Columns.Count - 1] = clientDt.Rows[j][i];
                            dr = null;
                        }
                }
            }
            return hostDt.Copy();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {

        }
    }
}