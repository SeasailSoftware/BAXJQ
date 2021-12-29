using Apache.NMS.ActiveMQ;
using hpt.gate.Led.Frm;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Device.Data;
using HPT.Gate.Device.Dev;
using HPT.Gate.Host.Config;
using HPT.Gate.Host.db;
using HPT.Gate.Host.DevPara;
using HPT.Gate.Host.face;
using HPT.Gate.Host.Lcd;
using HPT.Gate.Host.Properties;
using HPT.Gate.Host.Service;
using HPT.Gate.Host.Util;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Host
{
    public partial class FrmMain : DevComponents.DotNetBar.Office2007Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool ReleaseCapture();
        private const long WM_GETMINMAXINFO = 0x24;

        #region 拖拽窗口
        private void DragAndDropWindow(object sender, MouseEventArgs e)
        {
            const int WM_NCLBUTTONDOWN = 0x00A1;
            const int HTCAPTION = 2;
            if (e.Clicks == 2 && e.Button == MouseButtons.Left)
            {
                switch (WindowState)
                {
                    case FormWindowState.Normal: { WindowState = FormWindowState.Maximized; } break;
                    case FormWindowState.Maximized: { WindowState = FormWindowState.Normal; } break;
                }
            }
            else if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero); // 拖动窗体 
            }
        }
        #endregion

        private FrmLcd_1 _FrmLcdInstance;
        #region  托盘对象
        //创建NotifyIcon对象 
        private NotifyIcon notifyicon = new NotifyIcon();
        //创建托盘图标对象 
        private Icon ico = new Icon(Application.StartupPath + @"\ico\hpt.gate.ico");

        //创建托盘菜单对象 
        private ContextMenu notifyContextMenu = new ContextMenu();
        #endregion

        private readonly List<string> messageCache = new List<string>();
        private List<LedController> _LedControllers = new List<LedController>();
        private List<CameraInfo> _CameraList = new List<CameraInfo>();

        public FrmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void labelItem1_Click(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            StartBackgroupService();
            //设置鼠标放在托盘图标上面的文字 
            this.notifyicon.Text = "通道闸数据采集服务";
            notifyicon.DoubleClick += notifyIcon_DoubleClick;
            LoadDeviceList();
        }

        #region 启动后台服务
        private void StartBackgroupService()
        {
            StartRealTimeService();
            DBService.Instance.Start();
            DBService.Instance.Message += AddMsgToList;
            StartFaceService();
        }

        #endregion

        #region 人脸同步服务
        private void StartFaceService()
        {

        }

        #endregion


        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮 
            if (WindowState == FormWindowState.Minimized)
            {
                //托盘显示图标等于托盘图标对象 
                //注意notifyIcon1是控件的名字而不是对象的名字 
                notifyicon.Icon = ico;
                //隐藏任务栏区图标 
                this.ShowInTaskbar = false;
                //图标显示在托盘区 
                notifyicon.Visible = true;
            }
        }

        #region 托盘双击事件

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            //判断是否已经最小化于托盘 
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示 
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点 
                this.Activate();
                //任务栏区显示图标 
                this.ShowInTaskbar = true;
                //托盘区图标隐藏 
                notifyicon.Visible = false;
            }
        }
        #endregion

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            SetPara();
        }

        #region 参数设置
        private void SetPara()
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool led = AppSettings.LedEnabled;
            bool cam = AppSettings.CameraEnabled;
            bool face = AppSettings.FaceEnabled;
            int port = AppSettings.LocalPort;
            bool fingerPrint = AppSettings.FingerPrintEnabled;
            int fingerPrintType = AppSettings.FingerPrintType;
            FrmSettings settings = new FrmSettings();
            DialogResult dr = settings.ShowDialog();
            if (dr != DialogResult.OK) return;
            if (port != AppSettings.LocalPort || led != AppSettings.LedEnabled || cam != AppSettings.CameraEnabled
                || fingerPrint != AppSettings.FingerPrintEnabled || fingerPrintType != AppSettings.FingerPrintType)
                RealtimeService.Instance.Restart();
            if (AppSettings.FaceEnabled && face != AppSettings.FaceEnabled)
                StartFaceService();
        }
        #endregion

        #region 数据库备份与还原
        private void DbBackupAndReduction()
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmDbBackupAndReduction dbConfig = new FrmDbBackupAndReduction();
            dbConfig.ShowDialog();
            LoadDeviceList();
        }
        #endregion

        private void btAddDev_Click(object sender, EventArgs e)
        {
            AddDevice();
        }

        #region 添加设备
        private void AddDevice()
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmDevAdd addDev = new FrmDevAdd();
            DialogResult dr = addDev.ShowDialog();
            if (dr == DialogResult.OK)
                LoadDeviceList();
        }

        #endregion


        #region 加载设备列表

        private void LoadDeviceList()
        {
            Task.Factory.StartNew(() =>
            {
                List<DeviceInfo> devList = new List<DeviceInfo>();
                try
                {
                    devList = DeviceInfoService.ToList();
                }
                catch
                {

                }
                this.Invoke(new Action(() =>
                {
                    dgvDev.DataSource = null;
                    dgvDev.Rows.Clear();
                    foreach (DeviceInfo device in devList)
                    {
                        int index = dgvDev.Rows.Add();
                        dgvDev.Rows[index].Cells[0].Value = device.DeviceId;
                        dgvDev.Rows[index].Cells[1].Value = device.DeviceName;
                        dgvDev.Rows[index].Cells[2].Value = device.Mac;
                        dgvDev.Rows[index].Cells[3].Value = device.IPAddress;
                        dgvDev.Rows[index].Cells[4].Value = device.SubNet;
                        dgvDev.Rows[index].Cells[5].Value = device.GateWay;
                        dgvDev.Rows[index].Cells[6].Value = device.PlaceId == 0 ? "宿舍" : "大门";
                        dgvDev.Rows[index].Cells[7].Value = "离线";

                    }
                }));
            });
        }
        #endregion

        private void btEditDev_Click(object sender, EventArgs e)
        {
            EditDev();
        }
        #region 编辑设备
        private void EditDev()
        {
            if (dgvDev.Rows.Count == 0 || dgvDev.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要编辑的设备信息!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string mac = dgvDev.SelectedRows[0].Cells[2].Value.ToString();
            TcpDevice device = new TcpDevice(mac);
            if (!device.IsOnline)
            {
                MessageBox.Show("设备离线,无法设置参数", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int devId = Convert.ToInt32(dgvDev.SelectedRows[0].Cells[0].Value);
            FrmDevEdit frmDevice = new FrmDevEdit(1, devId);
            frmDevice.ShowDialog();
            new Thread(() => { LoadDeviceList(); }) { IsBackground = true }.Start();
        }
        #endregion
        private void btDeleteDev_Click(object sender, EventArgs e)
        {
            DeleteDevice();
        }

        #region 删除设备
        private void DeleteDevice()
        {
            if (dgvDev.Rows.Count == 0 || dgvDev.SelectedRows.Count == 0)
            {
                MessageBox.Show("没有设备或没有选中设备！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = dgvDev.SelectedRows[0].Index;
            if (index == -1)
            {
                MessageBox.Show("请选择需要删除的设备信息。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int devId = Convert.ToInt32(dgvDev.Rows[index].Cells[0].Value);
            try
            {
                DeviceInfoService.Del(devId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除设备信息失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            new Thread(() => { LoadDeviceList(); }) { IsBackground = true }.Start();
        }
        #endregion

        private void btiStartServer_Click(object sender, EventArgs e)
        {
            //JMS.JMSCreater creater = new JMS.JMSCreater();
            //creater.Start();
            StartServer();
        }



        private void btiStopServer_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        #region 启动服务
        private void StartServer()
        {
            AddMsgToList("数据同步服务开始启动...");
            btiStartServer.Enabled = false;
            btiStopServer.Enabled = true;
            StartBaseService();
        }
        #endregion

        #region 启动基本服务
        private void StartBaseService()
        {
            DataService.Instance.Message += AddMsgToList;
            DataService.Instance.Start();
        }

        #endregion

        #region 启动实时服务

        private void StartRealTimeService()
        {
            RealtimeService.Instance.Message += AddMsgToList;
            RealtimeService.Instance.Start();
        }

        #endregion




        #region 停止服务
        private void StopServer()
        {
            AddMsgToList("数据同步服务已经停止。");
            StopBaseService();
            btiStartServer.Enabled = true;
            btiStopServer.Enabled = false;
        }
        #endregion

        #region 停止实时服务
        private void StopRealTimeService()
        {
            RealtimeService.Instance.Stop();
            RealtimeService.Instance.Message -= AddMsgToList;

        }
        #endregion

        #region 停止基本服务
        private void StopBaseService()
        {
            DataService.Instance.Stop();
            DataService.Instance.Message -= AddMsgToList;
        }
        #endregion



        #region 添加到消息队列
        private void AddMsgToList(string obj)
        {
            messageCache.Add(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), obj));
        }
        #endregion

        private void tmLog_Tick(object sender, EventArgs e)
        {
            try
            {
                if (messageCache.Count > 0)
                {
                    int count = messageCache.Count;
                    var sb = new StringBuilder();
                    var i = 0;
                    while (messageCache.Count > 0 && i++ < 1000)
                    {
                        sb.Append(Environment.NewLine).Append(messageCache[0]);
                        messageCache.RemoveAt(0);

                    }
                    if (sb.Length > 0)
                    {
                        if (txtLog.Lines.Length > 1000) txtLog.Clear();
                        txtLog.AppendText(sb.ToString());
                    }
                }
                toolStripLabel3.Text = DBService.Instance.GetRecordCount().ToString("0000");
                label_face.Text = DBService.Instance.GetTasksCount().ToString("0000");
                //toolStripLabel8.Text = DBServer.Instance.GetCardCount().ToString("0000");
                tbTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");
            }
            catch
            {
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            LedSettings();
        }

        #region Led设置
        private void LedSettings()
        {
            bool flag = AppSettings.LedEnabled;
            FrmLedController ledController = new FrmLedController();
            ledController.ShowDialog();
            if (AppSettings.LedEnabled || AppSettings.LedEnabled != flag)
                Task.Factory.StartNew(() =>
                {
                    RealtimeService.Instance.Restart();
                });


        }
        #endregion

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            string action = buttonItem3.Text;
            switch (action)
            {
                case "打开Lcd":
                    FrmLcdSettings ledSettings = new FrmLcdSettings();
                    DialogResult dr = ledSettings.ShowDialog();
                    if (dr != DialogResult.OK) return;
                    _FrmLcdInstance = new FrmLcd_1();
                    _FrmLcdInstance.Show();
                    buttonItem3.Text = "关闭Lcd";
                    buttonItem3.ForeColor = Color.Red;
                    break;
                case "关闭Lcd":
                    _FrmLcdInstance.Close();
                    buttonItem3.Text = "打开Lcd";
                    buttonItem3.ForeColor = Color.Black;
                    break;
            }
            /*
            string text = buttonItem3.Text;
            MessageBox.Show("该功能需要根据外接Lcd显示器尺寸专门订制,如果需要使用该功能，请联系我司业务员!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
            */
        }



        private void tmSynTime_Tick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvDev.Rows)
            {
                string mac = row.Cells[2].Value.ToString();
                int rowIndex = row.Index;
                TcpDevice device = new TcpDevice(mac);
                if (device.IsOnline)
                    UpdateDevStatus(rowIndex, "在线");
                else
                    UpdateDevStatus(rowIndex, "离线");
            }

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            try
            {
                RealtimeService.Instance.Message -= AddMsgToList;
                RealtimeService.Instance.Stop();
            }
            catch
            {

            }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            CameraSettings();
        }

        #region 摄像头设置
        private void CameraSettings()
        {
            FrmCamera camera = new FrmCamera();
            DialogResult dr = camera.ShowDialog();
            if (AppSettings.CameraEnabled)
            {
                RealtimeService.Instance.StopNetCamService();
                Thread.Sleep(1000);
                RealtimeService.Instance.StartNetCamService();
            }
        }
        #endregion

        #region Lcd设置
        private void LcdSettings()
        {

        }
        #endregion

        #region 设置基本参数
        private void SetBasePara()
        {
            TcpDevice device = GetDeviceByDgvSelected();
            if (device == null)
            {
                MessageBox.Show("没有可操作的设备!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (device._SocketState == null)
            {
                MessageBox.Show("设备离线!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmBasePara basePara = new FrmBasePara(device);
            basePara.ShowDialog();
        }
        #endregion



        #region 获取选中的设备
        private TcpDevice GetDeviceByDgvSelected()
        {
            if (dgvDev.Rows.Count == 0 || dgvDev.SelectedRows.Count == 0)
                return null;
            int devId = Convert.ToInt32(dgvDev.SelectedRows[0].Cells[0].Value);
            string devName = dgvDev.SelectedRows[0].Cells[1].Value.ToString();
            string mac = dgvDev.SelectedRows[0].Cells[2].Value.ToString();
            TcpDevice device = new TcpDevice(mac);
            device._MachineId = devId;
            device._DeviceName = devName;
            return device;
        }
        #endregion

        #region 设置时间组参数
        private void SetTimePara()
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmTimePara timePara = new FrmTimePara();
            timePara.ShowDialog();

        }
        #endregion

        #region 时间校正
        private void SetDevTime()
        {
            List<DeviceInfo> devList = DeviceInfoService.ToList();
            foreach (DeviceInfo dev in devList)
            {
                TcpDevice device = DataConverter.GetTcpDevice(dev);
                if (!device.IsOnline)
                {
                    AddMsgToList($"设备{device._DeviceName}离线...");
                    continue;
                }
                if (device.SetTime())
                {
                    AddMsgToList(string.Format("设备[{0}]校正时间成功!", device._DeviceName));
                }
                else
                {
                    AddMsgToList(string.Format("设备[{0}]校正时间成功!", device._DeviceName));
                }
                Application.DoEvents();
            }
        }
        #endregion




        #region 上传背景图片
        private void UploadBackGroupImage()
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmUploadBGImage uploadBGImage = new FrmUploadBGImage();
            uploadBGImage.ShowDialog();
        }
        #endregion

        #region 上传人员照片
        private void FUploadPhotos()
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmUploadPhotos uploadPhotos = new FrmUploadPhotos();
            uploadPhotos.ShowDialog();
        }
        #endregion


        private void Device_CameraDataEvent(object sender, CameraCuptureArgs e)
        {
            int deviceId = e.DeviceId;
            int ioFlag = e.IOFlag;
            AddMsgToList(string.Format("机器号:{0},出入口:{1}", deviceId, ioFlag));
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            AddDevice();
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            SetPara();
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            EditDev();
        }

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            DeleteDevice();
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            LedSettings();
        }

        private void buttonItem15_Click(object sender, EventArgs e)
        {
            CameraSettings();
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            LcdSettings();
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            SetBasePara();
        }



        private void DevTimePara_Click(object sender, EventArgs e)
        {
            SetTimePara();
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            SetDevTime();
        }

        private void buttonItem19_Click(object sender, EventArgs e)
        {
            UploadBackGroupImage();
        }

        private void buttonItem20_Click(object sender, EventArgs e)
        {
            FUploadPhotos();
        }

        private void buttonItem21_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem22_Click(object sender, EventArgs e)
        {
            DbBackupAndReduction();

        }

        private void dgvDev_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvDev.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvDev.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvDev.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void 数据库更新_Click(object sender, EventArgs e)
        {

        }

        #region 数据库更新
        private void UpdateDatabase()
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string file = Path.Combine(Application.StartupPath, "Update\\update.sql");
            if (!File.Exists(file))
            {
                MessageBox.Show("没找到可更新的数据库文件!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmDbUpdate update = new FrmDbUpdate(file);
            DialogResult dr = update.ShowDialog();
            if (dr == DialogResult.OK)
                MessageBox.Show("数据库更新完毕!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion



        #region 更新设备状态
        private delegate void DlgUpdateDevStatus(int rowIndex, string status);
        private void UpdateDevStatus(int rowIndex, string status)
        {
            if (dgvDev.InvokeRequired)
            {
                Invoke(new DlgUpdateDevStatus(UpdateDevStatus), rowIndex, status);
            }
            else
            {
                dgvDev.Rows[rowIndex].Cells[7].Value = status;
                if (status.Equals("在线"))
                    dgvDev.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Black;
                else
                    dgvDev.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;

            }
        }

        #endregion

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            RecordParaSettings();
        }

        private void RecordParaSettings()
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            TcpDevice device = GetDeviceByDgvSelected();
            if (device == null)
            {
                MessageBox.Show("没有可操作的设备!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (device._SocketState == null)
            {
                MessageBox.Show("设备离线!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmRecordPara recordPara = new FrmRecordPara(device);
            recordPara.ShowDialog();
        }

        private void buttonItem23_Click(object sender, EventArgs e)
        {
            RemoteOpen(0);
        }

        private void RemoteOpen(int ioFlag)
        {
            TcpDevice device = GetDeviceByDgvSelected();
            if (device == null)
            {
                MessageBox.Show("没有可操作的设备!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (device._SocketState == null)
            {
                MessageBox.Show("设备离线!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            GateCommand command;
            if (ioFlag == 0)
                command = GateCommand.OpenOnceForIn;
            else
                command = GateCommand.OpenOnceForOut;
            if (device.RemoteControl(command))
                MessageBox.Show($"[{device._DeviceName}]远程开门成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"[{device._DeviceName}]远程开门失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonItem24_Click(object sender, EventArgs e)
        {
            RemoteOpenAlways();
        }

        private void RemoteOpenAlways()
        {
            TcpDevice device = GetDeviceByDgvSelected();
            if (device == null)
            {
                MessageBox.Show("没有可操作的设备!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (device._SocketState == null)
            {
                MessageBox.Show("设备离线!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (device.RemoteControl(GateCommand.OpenAlways))
                MessageBox.Show($"[{device._DeviceName}]设置成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"[{device._DeviceName}]设置失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonItem25_Click(object sender, EventArgs e)
        {
            RemoteCloseAlways();
        }

        private void RemoteCloseAlways()
        {
            TcpDevice device = GetDeviceByDgvSelected();
            if (device == null)
            {
                MessageBox.Show("没有可操作的设备!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (device._SocketState == null)
            {
                MessageBox.Show("设备离线!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (device.RemoteControl(GateCommand.CloseAlways))
                MessageBox.Show($"[{device._DeviceName}]设置成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"[{device._DeviceName}]设置失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            RemoteOpen(1);
        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            RemoteOpen(0);
        }

        private void buttonItem28_Click(object sender, EventArgs e)
        {
            RemoteOpen(1);
        }

        private void buttonItem29_Click(object sender, EventArgs e)
        {
            ManageFaceDevices();
        }

        private void ManageFaceDevices()
        {
            if (!btiStartServer.Enabled)
            {
                MessageBox.Show("请先停止服务!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmFace face = new FrmFace();
            face.ShowDialog();
        }



        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ControlBox_MouseEnter(object sender, EventArgs e)
        {
            var label = sender as Label;
            switch (label.Name)
            {
                case "label_max": { label.Image = Resources.max_hover; } break;
                case "label_min": { label.Image = Resources.min_hover; } break;
                case "label_close": { label.Image = Resources.close_hover; } break;
            }
        }

        private void ControlBox_MouseLeave(object sender, EventArgs e)
        {
            var label = sender as Label;
            switch (label.Name)
            {
                case "label_max": { label.Image = Resources.max_normal; } break;
                case "label_min": { label.Image = Resources.min_normal; } break;
                case "label_close": { label.Image = Resources.close_normal; } break;
            }
        }

        private void ControlBox_Click(object sender, EventArgs e)
        {
            var label = sender as Label;
            switch (label.Name)
            {
                case "label_max":
                    if (WindowState == FormWindowState.Maximized)
                        WindowState = FormWindowState.Normal;
                    else if (WindowState == FormWindowState.Normal)
                        WindowState = FormWindowState.Maximized;
                    break;
                case "label_min":
                    label.Image = Resources.min_normal;
                    WindowState = FormWindowState.Minimized;
                    break;
                case "label_close":
                    if (!btiStartServer.Enabled)
                    {
                        MessageBoxHelper.Info("请先停止服务!");
                        return;
                    }
                    Close();
                    break;
            }
        }

        private void buttonItem30_Click(object sender, EventArgs e)
        {
            ManageFaceDevices();
        }

        private void buttonItem31_Click(object sender, EventArgs e)
        {
            try
            {
                EmpInfoService.ClearCountOfInside();
                MessageBox.Show($"[场内人数自动清零]:清除所有人员场内状态成功,下次清零时间为:{DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm")}", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[场内人数自动清零]:清除所有人员场内状态失败:{ex.Message},下次清零时间为:{DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm")}", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
