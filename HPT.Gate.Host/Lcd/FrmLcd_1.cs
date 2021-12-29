using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Device.Dev;
using HPT.Gate.Host.Config;
using HPT.Gate.Host.Service;
using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Host.Lcd
{
    public partial class FrmLcd_1 : Form
    {
        public FrmLcd_1()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            //DateTimeFormatInfo myDTFI = new CultureInfo("en-US", false).DateTimeFormat;
            string month = DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture);
            lbMonth.Text = month;
            lbDay.Text = time.Day.ToString();
            lbTime.Text = time.ToString("HH:mm:ss");
            lbFullTime.Text = time.ToLongDateString();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadOnSecondMonitor();
            RealtimeService.Instance.RealtimeDataReceived += CaptureEvent;
            lbTitle.Text = AppSettings.LcdTitle;
        }


        #region 尝试将窗口展示在第二个屏幕


        private void LoadOnSecondMonitor()
        {
            this.Left = Screen.AllScreens[0].Bounds.Width;
            this.Top = Screen.AllScreens[0].Bounds.Top;
            this.FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        #endregion

        private void CaptureEvent(object sender, RealtimeArgs e)
        {
            DutySummary();
            //显示刷卡信息
            Task.Factory.StartNew(() =>
            {
                this.Invoke(new Action(() =>
                {
                    try
                    {
                        #region 显示第四个信息
                        if (pic_4.Image != null)
                            pic_4.Image.Dispose();
                        pic_4.Image = pic_3.Image == null ? null : ImageHelper.KiResizeImage(new Bitmap(pic_3.Image), pic_4.Width, pic_4.Height);
                        lbDept_4.Text = StringFormatting(lbDept_3.Text, 15);
                        lbEmpCode_4.Text = StringFormatting(lbEmpCode_3.Text, 15);
                        lbEmpName_4.Text = StringFormatting(lbEmpName_3.Text, 15);
                        lbIOFlag_4.Text = StringFormatting(lbIOFlag_3.Text, 15);
                        lbRecTime_4.Text = StringFormatting(lbRecTime_3.Text, 15);
                        #endregion

                        #region 显示第三个信息
                        if (pic_3.Image != null)
                            pic_3.Image.Dispose();
                        pic_3.Image = pic_2.Image == null ? null : ImageHelper.KiResizeImage(new Bitmap(pic_2.Image), pic_3.Width, pic_3.Height);
                        lbDept_3.Text = StringFormatting(lbDept_2.Text, 15);
                        lbEmpCode_3.Text = StringFormatting(lbEmpCode_2.Text, 15);
                        lbEmpName_3.Text = StringFormatting(lbEmpName_2.Text, 15);
                        lbIOFlag_3.Text = StringFormatting(lbIOFlag_2.Text, 15);
                        lbRecTime_3.Text = StringFormatting(lbRecTime_2.Text, 15);
                        #endregion

                        #region 显示第二个信息
                        if (pic_2.Image != null)
                            pic_2.Image.Dispose();
                        pic_2.Image = pic_1.Image == null ? null : ImageHelper.KiResizeImage(new Bitmap(pic_1.Image), pic_2.Width, pic_2.Height);
                        lbDept_2.Text = StringFormatting(lbDept_1.Text, 15);
                        lbEmpCode_2.Text = StringFormatting(lbEmpCode_1.Text, 15);
                        lbEmpName_2.Text = StringFormatting(lbEmpName_1.Text, 15);
                        lbIOFlag_2.Text = StringFormatting(lbIOFlag_1.Text, 15);
                        lbRecTime_2.Text = StringFormatting(lbRecTime_1.Text, 15);
                        #endregion

                        #region 显示第一个信息
                        if (pic_1.Image != null)
                            pic_1.Image.Dispose();
                        if (e.Photo != null)
                            pic_1.Image = ImageHelper.KiResizeImage(new Bitmap(e.Photo), pic_1.Width, pic_1.Height);
                        else
                            pic_1.Image = null;
                        lbDept_1.Text = StringFormatting($"部门:{e.DeptName}", 30);
                        lbEmpCode_1.Text = StringFormatting($"编号:{ e.EmpCode}", 30);
                        lbEmpName_1.Text = StringFormatting($"姓名:{ e.EmpName}", 30);
                        lbIOFlag_1.Text = StringFormatting($"进出:{(e.IOFlag == 3 ? "进场" : "出场")}", 30);
                        lbRecTime_1.Text = StringFormatting($"时间:{e.RecDatetime}", 30);
                        #endregion
                    }
                    catch
                    {

                    }

                }));
            });
        }

        private string StringFormatting(string buffer, int len = 20)
        {
            byte[] arr = Encoding.GetEncoding("gb2312").GetBytes(buffer.Trim());
            int length = arr.Length;
            if (length >= len) return buffer;
            List<byte> bytes = arr.ToList();
            for (int i = 0; i < 30 - length; i++)
            {
                bytes.Add(0x20);
            }
            return Encoding.GetEncoding("gb2312").GetString(bytes.ToArray());
        }
        private void DutySummary()
        {
            //更新工种人数统计
            Task.Factory.StartNew(() =>
            {
                DataTable dt = EmpInfoService.SummaryByDuty();
                this.Invoke(new Action(() =>
                {
                    ListSummary.Items.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        this.ListSummary.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
                        int index = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            //string item = $"{index++},{row[0].ToString()},{row[1].ToString()}";
                            //ListSummary.Items.Add(item);
                            ListViewItem item = new ListViewItem();
                            item.Text = $"{index++}";

                            item.SubItems.Add(row[0].ToString());

                            item.SubItems.Add(row[1].ToString());

                            this.ListSummary.Items.Add(item);
                        }

                        this.ListSummary.EndUpdate();  //结束数据处理，UI界面一次性绘制。
                    }
                }));

            });
        }


        private void countTimer_Tick(object sender, EventArgs e)
        {
            UpdateCountOfDept();
        }

        #region 生成部门人数统计
        private void UpdateCountOfDept()
        {

        }

        #endregion

        private void recordTimer_Tick(object sender, EventArgs e)
        {

        }



        private void FrmLcd_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
