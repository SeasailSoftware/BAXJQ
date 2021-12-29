using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.Device.Dev;
using HPT.Gate.Host.Config;
using HPT.Gate.Host.Service;
using HPT.Gate.Utils.Common;
using HPT.NetCam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hpt.gate.lcd.standard
{
    public partial class FrmLcd : Form
    {
        private List<LcdRecord> _CurrentLcdRecords = new List<LcdRecord>();
        private List<CameraInfo> Cams = new List<CameraInfo>();
        public FrmLcd()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            label_date.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            label_time.Text = $"{DateTime.Now.ToString("HH:mm:ss")}  {DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-CN"))}";

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadOnSecondMonitor();
            label_title.Text = AppSettings.LcdTitle;
            Cams = CameraService.GetAllCameras();
            LoadCameras();
            RealtimeService.Instance.RealtimeDataReceived += CaptureEvent;
            DutySummary();
        }

        private void LoadCameras()
        {
            Task.Factory.StartNew(() =>
            {

                this.Invoke(new Action(() =>
                {
                    if (AppSettings.LcdCamOfIn != 0)
                    {
                        CameraInfo cam = Cams.FirstOrDefault(p => p.CamId == AppSettings.LcdCamOfIn);
                        if (cam != null)
                        {
                            NetCamHelper.Instance.StartPreview(cam.IPAddress, preView1.Handle);
                        }
                    }
                    if (AppSettings.LcdCamOfOut != -1)
                    {
                        CameraInfo cam = Cams.FirstOrDefault(p => p.CamId == AppSettings.LcdCamOfOut);
                        if (cam != null)
                        {
                            NetCamHelper.Instance.StartPreview(cam.IPAddress, preView2.Handle);
                        }
                    }
                }));
            });
        }

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
                        if (e.IOFlag == 3)
                        {
                            if (photo1.Image != null)
                                photo1.Image.Dispose();
                            photo1.Image = ImageHelper.KiResizeImage((Bitmap)e.Photo, photo1.Width, photo1.Height);
                            lbDept.Text = StringFormatting(e.DeptName);
                            lbEmpCode.Text = StringFormatting(e.EmpCode);
                            lbEmpName.Text = StringFormatting(e.EmpName);
                            lbIOFlag.Text = StringFormatting((e.IOFlag == 3 ? "进校" : "离校"));
                            lbRecDatetime.Text = StringFormatting(Convert.ToDateTime(e.RecDatetime).ToString("HH:mm:ss"));
                        }
                        else
                        {
                            if (photo2.Image != null)
                                photo2.Image.Dispose();
                            photo2.Image = ImageHelper.KiResizeImage((Bitmap)e.Photo, photo2.Width, photo2.Height);
                            label_dept.Text = StringFormatting(e.DeptName);
                            label1_empcode.Text = StringFormatting(e.EmpCode);
                            label1_empname.Text = StringFormatting(e.EmpName);
                            label1_ioflag.Text = StringFormatting((e.IOFlag == 3 ? "进校" : "离校"));
                            label1_recdatetime.Text = StringFormatting(Convert.ToDateTime(e.RecDatetime).ToString("HH:mm:ss"));
                        }
                    }
                    catch
                    {
                        if (e.Photo != null)
                            e.Photo.Dispose();
                    }
                }));
            });
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

        private string StringFormatting(string buffer)
        {
            byte[] arr = Encoding.GetEncoding("gb2312").GetBytes(buffer);
            int length = arr.Length;
            if (length >= 30) return buffer;
            List<byte> bytes = arr.ToList();
            for (int i = 0; i < 30 - length; i++)
            {
                bytes.Add(0x20);
            }
            return Encoding.GetEncoding("gb2312").GetString(bytes.ToArray());
        }

        #region 尝试将窗口展示在第二个屏幕

        #region 添加记录到列表
        private void AddRecordToList(LcdRecord record)
        {
            List<LcdRecord> list = new List<LcdRecord>();
            list.Add(record);
            int index = _CurrentLcdRecords.Count >= 3 ? 3 : _CurrentLcdRecords.Count;
            for (int i = 0; i < index; i++)
            {
                list.Add(_CurrentLcdRecords[i]);
            }
            _CurrentLcdRecords.Clear();
            _CurrentLcdRecords.AddRange(list.ToArray());
        }
        #endregion


        private void LoadOnSecondMonitor()
        {
            this.Left = Screen.AllScreens[0].Bounds.Width;
            this.Top = Screen.AllScreens[0].Bounds.Top;
            this.FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        #endregion


        #region 根据控件高度获取字体大小


        #endregion

        private void countTimer_Tick(object sender, EventArgs e)
        {
            UpdateCountOfDept();
        }

        #region 生成部门人数统计
        private void UpdateCountOfDept()
        {
            try
            {
                DataTable dt = LcdService.GetCountOfDept();

            }
            catch
            {
            }
        }

        #endregion

        private void recordTimer_Tick(object sender, EventArgs e)
        {

        }

        #region 显示记录
        private void DisplayRecords()
        {



        }


        #endregion



        private void FrmLcd_FormClosed(object sender, FormClosedEventArgs e)
        {
            RealtimeService.Instance.RealtimeDataReceived -= CaptureEvent;
        }
    }
}
