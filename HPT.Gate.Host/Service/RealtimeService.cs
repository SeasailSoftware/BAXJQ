using hpt.gate.DataAccess.Entity;
using hpt.gate.DataAccess.Service;
using hpt.gate.DbTools.Service;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.Device;
using HPT.Gate.Device.Data;
using HPT.Gate.Device.Dev;
using HPT.Gate.Host.Config;
using HPT.Gate.Host.JMS;
using HPT.Gate.Host.Util;
using HPT.Gate.Utils.Common;
using HPT.Gate.ZKFP;
using HPT.Led.YBKJ;
using HPT.NetCam;
using HPT.NetCam.DH;
using HPT.NetCam.HK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace HPT.Gate.Host.Service
{
    public class RealtimeService
    {

        #region properties

        public int LocalPort { get; set; }

        private static object PrintLocker = new object();
        private bool IsRunning = false;

        private int FingerPrintType = 0;

        private JMSReceiver _JMsReceiver;
        #endregion

        #region Events
        /// <summary>
        /// 消息提示事件
        /// </summary>
        public event Action<string> Message;

        /// <summary>
        /// 触发消息提示事件
        /// </summary>
        /// <param name="messgae"></param>
        private void OnMessage(string messgae)
        {
            if (Message == null) return;
            Task.Factory.StartNew(() =>
            {
                if (Message != null)
                {
                    Message($"[实时服务]{messgae}");
                }
            });
        }

        #endregion

        #region Instance

        private static RealtimeService instance;
        private static readonly object lockHelper = new object();

        public static RealtimeService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new RealtimeService();
                        }
                    }
                }
                return instance;
            }
        }


        #endregion

        #region Event
        public event EventHandler<RealtimeArgs> RealtimeDataReceived;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public void OnRealtimeDataReceived(int deptId, string deptName, int empId, string empCode, string empName, string duty,
               int devId, string devName, string devIPAddress, string devMac, int camId, string camIPAddress, int cardType, string cardNo,
               int ioFlag, string recTime, Bitmap image, string recordEvent, Image capture)
        {
            try
            {
                if (RealtimeDataReceived == null) return;
                RealtimeArgs args = new RealtimeArgs();
                args.DeptId = deptId;
                args.DeptName = deptName;
                args.EmpId = empId;
                args.EmpCode = empCode;
                args.EmpName = empName;
                args.Duty = duty;
                args.DeviceId = devId;
                args.DevName = devName;
                args.DevIPAddress = devIPAddress;
                args.DevMac = devMac;
                args.CamId = camId;
                args.CamIPAddress = camIPAddress;
                args.CardType = cardType;
                args.CardNo = cardNo;
                args.IOFlag = ioFlag;
                args.RecDatetime = recTime;
                args.Photo = image;
                args.RecordType = recordEvent;
                args.Capture = capture;
                RealtimeDataReceived(this, args);
            }
            catch
            {
            }
        }
        #endregion


        #region public method

        #region 开启服务

        public void Start()
        {
            if (IsRunning)
            {
                OnMessage("服务已启动...");
                return;
            }
            IsRunning = true;
            FingerPrintType = AppSettings.FingerPrintType;
            //场内人数自动清零线程
            if (AppSettings.AutoClearEnabled)
                Task.Factory.StartNew(() => { AutoClear(AppSettings.AutoClearTime); });
            //监听数据
            TcpServer.Instance.LocalPort = AppSettings.LocalPort;
            TcpServer.Instance.RealtimeEvent += RealtimeEvent;
            TcpServer.Instance.Message += Message;
            TcpServer.Instance.Start();

            //指纹同步任务
            if (AppSettings.FingerPrintEnabled && AppSettings.FingerPrintType == 0)
            {
                ZKFPHelper.Instance.Type = 2;
                if (!ZKFPHelper.Instance.Init(null, out string msg))
                    OnMessage(msg);
                else
                {
                    ZKFPHelper.Instance.Message += Message;
                    ZKFPHelper.Instance.Start();
                    Task.Factory.StartNew(() => { CheckFingerPrintUpdate(); });
                }
            }
            //启动摄像头服务
            StartNetCamService();
            //启动Led服务
            StartLedService();
            DBService.Instance.DataInsertEvent += DataInsertEvent;

            #region JMS 服务
            _JMsReceiver = new JMSReceiver(AppSettings.JMSServer, AppSettings.JMSAccount, AppSettings.JMSPassword, AppSettings.JMSClient, AppSettings.JMSFilter);
            _JMsReceiver.Message += Message;
            _JMsReceiver.Start();
            #endregion

            #region 设备心跳
            Task.Factory.StartNew(() =>
            {
                UploadDeviceStatus();
            });
            #endregion

            #region 上传记录

            Task.Factory.StartNew(() =>
            {
                UploadRecords();
            });
            #endregion

            OnMessage("成功启动!");
        }


        #endregion

        #region 开始摄像头服务
        public void StartNetCamService()
        {
            Task.Factory.StartNew(() =>
            {
                if (AppSettings.CameraEnabled)
                {
                    List<CameraInfo> cameras = CameraInfoService.ToList();
                    NetCamHelper.Instance.NetCams = new List<NetCam.NetCam>();
                    foreach (CameraInfo cam in cameras)
                    {
                        NetCam.NetCam netCam;
                        if (AppSettings.NetCamType == 0)
                            netCam = new HKNetCam() { IPAddress = cam.IPAddress, Port = (UInt16)cam.Port, UserName = cam.UserName, Password = cam.Password };
                        else
                            netCam = new DHNetCam() { IPAddress = cam.IPAddress, Port = (UInt16)cam.Port, UserName = cam.UserName, Password = cam.Password };
                        NetCamHelper.Instance.NetCams.Add(netCam);
                    }
                    NetCamHelper.Instance.Message += Message;
                    NetCamHelper.Instance.Start();
                }
            });
        }
        #endregion

        #region 关闭摄像头服务
        public void StopNetCamService()
        {
            NetCamHelper.Instance.Stop();
            NetCamHelper.Instance.Message -= Message;
        }
        #endregion

        #region 启动Led服务
        public void StartLedService()
        {
            Task.Factory.StartNew(() =>
            {
                if (AppSettings.LedEnabled)
                {
                    List<LedController> controllers = LedDbService.GetAllLedControllers();
                    LedManager.Instance.Controllers = new List<Controller>();
                    controllers.ForEach(controller =>
                    {
                        Controller led = new Controller();
                        led.ControlType = controller.ControlType;
                        led.Heigth = controller.Heigth;
                        led.IPAddress = controller.IPaddress;
                        led.Lid = controller.Lid;
                        led.Port = controller.Port;
                        led.Protocol = controller.Protocol;
                        led.Width = controller.Width;
                        led.DynAreas = new List<Led.YBKJ.AreaInfo>();
                        controller.DynAreas.ForEach(p =>
                        {
                            Led.YBKJ.AreaInfo area = new Led.YBKJ.AreaInfo()
                            {
                                AreaId = p.AreaId,
                                BorderEffect = p.BorderEffect,
                                BorderLength = p.BorderLength,
                                BorderNo = p.BorderNo,
                                BorderSpeed = p.BorderSpeed,
                                BordreType = p.BordreType,
                                Content = p.Content,
                                DisplayEffect = p.DisplayEffect,
                                Height = p.Height,
                                Lid = p.LID,
                                Point_X = p.Point_X,
                                Point_Y = p.Point_Y,
                                SingleLine = p.SingleLine,
                                Speed = p.Speed,
                                Stay = p.Stay,
                                Text = p.Text,
                                TextBold = p.TextBold,
                                TextFont = p.TextFont,
                                TextFontSize = p.TextFontSize,
                                Width = p.Width
                            };
                            led.DynAreas.Add(area);
                        });
                        LedManager.Instance.Controllers.Add(led);
                    });

                    LedManager.Instance.Message += Message;
                    LedManager.Instance.Start();
                }
            });
        }
        #endregion

        #region 关闭Led服务
        public void StopLedService()
        {
            LedManager.Instance.Stop();
            LedManager.Instance.Message -= Message;
        }
        #endregion

        #region 关闭服务
        public void Stop()
        {
            IsRunning = false;
            TcpServer.Instance.RealtimeEvent -= RealtimeEvent;
            TcpServer.Instance.Message -= Message;
            TcpServer.Instance.Stop();
            StopNetCamService();
            StopLedService();
            _JMsReceiver.Stop();
            _JMsReceiver.Message -= Message;
        }

        #endregion

        #region 重启服务
        public void Restart()
        {
            Stop();
            Thread.Sleep(1200);
            Start();
        }

        #endregion

        #region 检查指纹同步任务是否有更新
        private void CheckFingerPrintUpdate()
        {
            List<HPT.Gate.DataAccess.Entity.FingerPrint> fps = FingerPrintService.ToList();
            fps.ForEach(item =>
            {
                ZKFPHelper.Instance.AddTask(new ZKFP.FingerPrint() { FPid = item.FingerId, FPData = item.FingerData });
            });

            while (IsRunning)
            {
                var tasks = FingerPrintTaskService.ToList();
                tasks.ForEach(item =>
                {
                    ZKFPHelper.Instance.AddTask(new ZKFP.FingerPrint() { FPid = item.EmpId * 5 + 3, FPData = item.FingerData });
                    FingerPrintTaskService.Update(item.RecId);
                });
                int index = 15;
                while (index > 0 && IsRunning)
                {
                    Thread.Sleep(1000);
                    index--;
                }
            }
        }

        #endregion

        #region 场内人数自动清零
        private void AutoClear(string time)
        {
            while (IsRunning)
            {
                if (DateTime.Now.ToString("HH:mm").Equals(time))
                {
                    try
                    {
                        EmpInfoService.ClearCountOfInside();
                        OnMessage($"[场内人数自动清零]:清除所有人员场内状态成功,下次清零时间为:{DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm")}");
                    }
                    catch (Exception ex)
                    {
                        OnMessage($"[场内人数自动清零]:清除所有人员场内状态失败:{ex.Message},下次清零时间为:{DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm")}");
                    }
                }
                int index = 59;
                while (index > 0 & IsRunning)
                {
                    Thread.Sleep(1000);
                    index--;
                }
            }
        }
        #endregion


        #region 发送设备状态
        private void UploadDeviceStatus()
        {
            while (IsRunning)
            {
                List<DeviceInfo> devices = DeviceInfoService.ToList();
                devices.ForEach(o =>
                {
                    TcpDevice device = DataConverter.GetTcpDevice(o);
                    if (device.IsOnline)
                        Task.Factory.StartNew(() =>
                        {
                            string msg;
                            if (o.DeviceType == 0)
                            {
                                DTAPI.UploadDoorDeviceStatus(AppSettings.ServerURL, o.Mac, o.PlaceId, out msg);
                                OnMessage($"设备[{o.Mac}]发送心跳成功：{msg}");
                            }
                            else if (o.DeviceType == 1)
                            {
                                DTAPI.UploadAttendanceDeviceStatus(AppSettings.ServerURL, o.Mac, out msg);
                                OnMessage($"设备[{o.Mac}]发送心跳成功：{msg}");
                            }



                        });
                });
                int index = 30;
                while (IsRunning && index > 0)
                {
                    Thread.Sleep(1000);
                    index--;
                }
            }
        }
        #endregion

        #region 上传记录
        private void UploadRecords()
        {
            while (IsRunning)
            {
                List<HTRecord> records = HTRecordService.GetUnuploadedRecords();
                foreach (HTRecord record in records)
                {
                    string msg;
                    if (record.RecordType == 0)
                    {
                        if (DTAPI.UploadDoorRecord(AppSettings.ServerURL, record.TermSN, record.CardNo, record.RecDatetime, record.IOFlag, record.Capture, out msg))
                        {
                            OnMessage($"上传门禁记录[设备:{record.TermSN},卡号:{record.CardNo},时间:{record.RecDatetime},出入口:{record.IOFlag}]成功!");
                            record.Status = 1;
                        }
                        else
                            OnMessage($"上传门禁记录[设备:{record.TermSN},卡号:{record.CardNo},时间:{record.RecDatetime},出入口:{record.IOFlag}]失败:{msg}!");
                    }
                    else if (record.RecordType == 1)
                    {
                        if (DTAPI.UploadAttendanceRecord(AppSettings.ServerURL, record.TermSN, record.CardNo, record.RecDatetime, out msg))
                        {
                            OnMessage($"上传考勤记录[设备:{record.TermSN},卡号:{record.CardNo},时间:{record.RecDatetime},出入口:{record.IOFlag}]成功!");
                            record.Status = 1;
                        }
                        else
                            OnMessage($"上传考勤记录[设备:{record.TermSN},卡号:{record.CardNo},时间:{record.RecDatetime},出入口:{record.IOFlag}]失败:{msg}!");
                    }
                    else
                    {
                        if (DTAPI.UploadSchoolCardAttendanceRecord(AppSettings.ServerURL, record.TermSN, record.CardNo, record.RecDatetime, out msg))
                        {
                            OnMessage($"上传校车考勤记录[设备:{record.TermSN},卡号:{record.CardNo},时间:{record.RecDatetime},出入口:{record.IOFlag}]成功!");
                            record.Status = 1;
                        }
                        else
                            OnMessage($"上传校车考勤记录[设备:{record.TermSN},卡号:{record.CardNo},时间:{record.RecDatetime},出入口:{record.IOFlag}]失败:{msg}!");
                    }
                    record.Result = msg;
                    HTRecordService.Update(record);
                }
                int index = 10;
                while (IsRunning && index > 0)
                {
                    Thread.Sleep(1000);
                    index--;
                }
            }
        }
        #endregion

        #region 实时事件

        private void RealtimeEvent(object sender, RealTimeDataArgs e)
        {
            if (e == null || sender == null) return;
            int devId = e.DeviceId;
            int ioFlag = e.IOFlag;
            string recTime = e.RecDateTime;

            var state = (TcpSocketState)sender;
            var ipAddress = state.IPAddress;
            var port = state.Port;
            OnMessage($"接收到闸机[{ipAddress}:{port},机器号:{devId}],出入口[{ioFlag}]的数据请求...");

            DeviceInfo dev = null;
            try
            {
                dev = DeviceInfoService.GetByDeviceId(devId);
            }
            catch (Exception ex)
            {
                OnMessage($"获取设备信息失败:{ex.Message},DeviceId ={devId}");
                return;
            }
            TcpDevice device = DataConverter.GetTcpDevice(dev);

            #region 处理返回消息
            Stopwatch watch = new Stopwatch();
            watch.Start();
            DataRealtimeRespon respon = AnlyzeRealtimeMessage(e, dev);
            OnMessage($"处理时间为:{watch.ElapsedMilliseconds}毫秒");
            watch.Stop();
            if (respon != null)
            {
                if (device == null)
                    OnMessage($"机器号为[{devId}]的机器尚未登记,无法发送开闸请求!");
                else
                    device.RealtimeRespon(respon);
                if (respon.Action == 0x00)
                    Task.Factory.StartNew(() =>
                    {
                        UpdateEmpIOStatus(respon.EmpId, e.IOFlag);
                    });

            }
            #endregion

            #region 处理抓拍
            Task.Factory.StartNew(() =>
            {
                Image capture = null;
                CameraInfo camera = null;
                if (AppSettings.CameraEnabled)
                {
                    camera = CameraInfoService.GetByDevId(dev.DeviceId, ioFlag);
                    if (camera == null)
                        OnMessage($"闸机[{dev.IPAddress}]尚未绑定摄像头,无法抓拍图像!");
                    else
                        NetCamHelper.Instance.Capture(camera.IPAddress, out capture);
                }
                try
                {
                    //抓拍照片
                    string recordEvent = EnumHelper.ToEnumDescriptionString(respon.Action, typeof(RecordEvent));
                    OnRealtimeDataReceived(respon.DeptId, respon.DeptName, respon.EmpId, respon.EmpCode, respon.EmpName, respon.Duty,
                     dev.DeviceId, dev.DeviceName, dev.IPAddress, dev.Mac, camera == null ? 0 : camera.CamId, camera == null ? "" : camera.IPAddress, e.CardType, e.CardNo, ioFlag, recTime, respon.Photo, respon.ErrorMessage, capture);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (respon.Action == 0x00)
                    {
                        HTRecord record = new HTRecord()
                        {
                            TermSN = dev.Mac,
                            RecordType = dev.DeviceType,
                            CardNo = respon.CardNo,
                            RecDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            IOFlag = e.IOFlag == 0x03 ? 1 : 2,
                            Capture = ImageHelper.ImageToBase64String(capture),
                            Status = 0,
                            Result = "未上传"
                        };
                        if (HTRecordService.Insert(record, out string msg))
                            OnMessage($"保存记录[设备:{record.TermSN},卡号:{record.CardNo},时间:{record.RecDatetime},出入口:{record.IOFlag}]成功!");
                        else
                            OnMessage($"保存记录[设备:{record.TermSN},卡号:{record.CardNo},时间:{record.RecDatetime},出入口:{record.IOFlag}]失败:{msg}!");
                    }
                }
            });
            #endregion

        }

        #endregion

        #region 读取文本文件内容
        /// <summary>
        /// 读取txt文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ReadTxtFile(string path)
        {
            path = Environment.CurrentDirectory + @"\led\" + path;
            if (!File.Exists(path))
            {
                return string.Empty;
            }
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string str = string.Empty;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    str += line;
                    str += "\r\n";
                }
                return str;
            }
        }
        #endregion

        #region 替换Led参数内容
        private string GetLedTxtContext(string content, int lId)
        {
            string _Content = string.Empty;
            List<int> deptList = new List<int>();
            if (content.IndexOf("【") < 0 || content.IndexOf("】") < 0)
            {
                return content;
            }
            while (content.Length > 0)
            {
                if (content.IndexOf("【") < 0 || content.IndexOf("】") < 0)
                {
                    _Content += content;
                    break;
                }
                else
                {
                    int index1 = content.IndexOf("【");
                    int index2 = content.IndexOf("】");
                    if (index2 - index1 > 0)
                    {
                        string deptId = content.Substring(index1 + 1, index2 - index1 - 1);
                        //校验是否为数字
                        if (!System.Text.RegularExpressions.Regex.IsMatch(deptId, "^\\d+$"))
                        {
                            _Content = content.Substring(0, index2 + 1);
                            content = content.Substring(index2 + 1);
                            continue;
                        }
                        else
                        {
                            int curIndex = Convert.ToInt32(deptId);
                            string instead = string.Empty;
                            instead = string.Empty;
                            try
                            {
                                instead = LedDbService.GetValueOfDynPara(curIndex, AppSettings.LedNumberLength);
                            }
                            catch (Exception ex)
                            {
                                OnMessage($"替换Led文本内容失败:{ex.Message}");
                            }
                            _Content += content.Substring(0, index1) + instead;
                            content = content.Substring(index2 + 1);
                            continue;
                        }
                    }
                }
            }
            return _Content;
        }
        #endregion

        #region 生成Bmp图像
        private string CreateLedImage(DataAccess.Entity.AreaInfo area)
        {
            //ClearAllBmpSended(temp);
            //生成新的发送Bmp图像
            try
            {
                string fileName = $"{Environment.CurrentDirectory}\\led\\{area.LID}{area.AreaId}.bmp";
                if (!File.Exists(fileName))
                    File.Create(fileName).Close();
                int length = area.Width;
                int height = area.Height;
                Bitmap bmp = new Bitmap(length, height);//新建一个图片对象

                Graphics g = Graphics.FromImage(bmp);//利用该图片对象生成“画板”

                Font font = new Font(area.TextFont, area.TextFontSize);//设置字体颜色
                SolidBrush brush = new SolidBrush(Color.Red);//新建一个画刷,到这里为止,我们已经准备好了画板、画刷、和数据

                Pen pen = new Pen(Color.Red, 1);//定义了一个红色,宽度为的画笔
                g.Clear(Color.Black); //设置黑色背景
                                      //一排数据
                                      ///<----------------开始读取配置文件------------------>
                XmlDocument doc = new XmlDocument();
                string path = Environment.CurrentDirectory + @"/led/" + area.Text;
                doc.Load(path);    //加载Xml文件  
                XmlElement root = doc.DocumentElement;   //获取根节点  
                XmlNodeList rows = root.SelectNodes("//Table");
                ///root.GetElementsByTagName("person"); //获取person子节点集合 
                int rowIndex = 0;
                foreach (XmlNode node in rows)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        string rowHeights = ((XmlElement)child).GetAttribute("Height");
                        ///高度根据字体的大小自动设置
                        ///int rowHeitht = Convert.ToInt32(((XmlElement)child).GetAttribute("Height")) ;
                        ///SizeF rowSize = g.MeasureString("测试", font);
                        ///int rowHeitht = (int)rowSize.Height;
                        int rowHeitht = GetHeightByFontSize(font);
                        int totalWidth = Convert.ToInt32(((XmlElement)child).GetAttribute("Width"));
                        int cellIndex = 0;
                        foreach (XmlNode childNode in child)
                        {
                            int cellWidth = Convert.ToInt32(((XmlElement)childNode).GetAttribute("Width"));
                            cellWidth = length * cellWidth / totalWidth;
                            string value = ((XmlElement)childNode).GetAttribute("Value");
                            value = GetLedTxtContext(value, area.LID);
                            SizeF size = g.MeasureString(value, font);
                            g.DrawRectangle(pen, cellIndex, rowIndex, cellWidth, rowHeitht);
                            g.DrawString(value, font, brush, cellIndex + 1, rowIndex + 1);//
                            cellIndex += cellWidth;
                        }
                        rowIndex += rowHeitht;
                    }
                }
                bmp.Save(fileName, ImageFormat.Bmp);//保存为输出流，否则页面上显示不出来
                g.Dispose();//释放掉该资源
                return fileName;
            }
            catch (Exception ex)
            {
                OnMessage($"生成Led表格图片失败:{ex.Message}");
                return string.Empty;
            }
        }

        #endregion

        #region 计算行高
        /// <summary>
        /// 根据字体算出行高
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public int GetHeightByFontSize(Font font)
        {
            int height = 16;
            switch ((int)font.Size)
            {
                case 8:
                    height = 11;
                    break;
                case 9:
                    height = 12;
                    break;
                case 10:
                    height = 13;
                    break;
                case 11:
                    height = 15;
                    break;
                case 12:
                    height = 16;
                    break;
                case 13:
                    height = 17;
                    break;
                case 14:
                    height = 19;
                    break;
                case 16:
                    height = 22;
                    break;
                case 18:
                    height = 24;
                    break;
                case 20:
                    height = 26;
                    break;
                case 28:
                    height = 37;
                    break;
            }
            return height + 1;
        }
        #endregion


        #region 改变人员的场内外状态
        private void UpdateEmpIOStatus(int empId, int ioFlag)
        {
            try
            {
                int status = ioFlag == 3 ? 1 : 0;
                string temp = ioFlag == 3 ? "场内" : "场外";
                EmpInfoService.ChangeIOStatus(empId, status);
                OnMessage($"人员[{empId}]的场内外状态为:{temp}");
            }
            catch (Exception ex)
            {
                OnMessage($"改变人员[{empId}]场内外状态失败:{ex.Message}");
            }
        }

        #endregion


        #region 处理实时消息
        private DataRealtimeRespon AnlyzeRealtimeMessage(RealTimeDataArgs args, DeviceInfo device)
        {
            #region 初始化实时响应对象
            DataRealtimeRespon response = new DataRealtimeRespon
            {
                SpecialAction = 0x00,
                Action = (byte)RecordEvent.RESULT_NO_TICKET_NUM,
                VoiceNo = 0x07,
                EmpId = 0,
                CardType = args.CardType,
                CardNo = args.CardNo,
                PhotoName = 65535,
                Message_Row1 = " ",
                Message_Row2 = " ",
                Message_Row3 = " ",
                SingleTicket = 0x00
            };
            #endregion

            #region IC卡处理
            if (args.CardType == 1)
            {
                byte[] temp = ArrayHelper.HexToArray(args.CardNo, 4);
                Array.Reverse(temp);
                UInt32 cardNo = BitConverter.ToUInt32(temp, 0);
                string sCardNo = cardNo.ToString("0000000000");
                response.CardNo = sCardNo;
                OnMessage($"请求类型为:IC/ID卡,卡号:{args.CardNo}_{sCardNo}");
                ///处理考勤设备
                if (device.DeviceType != 0)
                {
                    OnMessage($"当前设备[{device.Mac}]为考勤设备,不做权限判断!");
                    response.Action = 0x00;
                    return response;
                }
                HTCard card = HTCardService.GetByMacAndCardNo(device.Mac, sCardNo);
                if (card == null)
                {
                    OnMessage($"找不到卡号[{sCardNo}]匹配的权限");
                    return response;
                }
                if (card.type == 3)
                {
                    OnMessage($"此卡[{sCardNo}]为教师,允许通行!");
                    response.Action = 0x00;
                    return response;
                }
                //如果当前门禁为宿舍门禁
                if (device.PlaceId == 0 && card.type == 2)
                {
                    OnMessage($"通校生无法访问宿舍门禁!");
                    return response;
                }
                if (device.PlaceId == 1 && args.IOFlag == 0x03)
                {
                    OnMessage($"学校大门入口不受时段限制!");
                    response.Action = 0x00;
                    return response;
                }
                HTVacation vacation = HTVacationService.Get(sCardNo);
                if (vacation != null)
                {
                    OnMessage($"当前卡请假[开始时间:{vacation.BeginTime},结束时间:{vacation.EndTime}],允许通行!");
                    response.Action = 0x00;
                    return response;
                }
                List<HTTimegroup> tgs = HTTimegroupService.GetByCard(card.gradeId, device.Mac, card.type);
                foreach (HTTimegroup tg in tgs)
                {
                    DateTime dtBegin = StringToDateTime(tg.beginTime);
                    DateTime dtEnd = StringToDateTime(tg.endTime);
                    if (dtBegin <= DateTime.Now && dtEnd >= DateTime.Now)
                    {
                        OnMessage($"有效卡并在通行时段范围内[{dtBegin.ToString("yyyy-MM-dd HH:mm:ss")}~{dtEnd.ToString("yyyy-MM-dd HH:mm:ss")}]");
                        response.Action = 0x00;
                        return response;
                    }
                }
                OnMessage($"有效卡但不在通行时段范围内,禁止通行!");
                return response;
            }
            #endregion

            try
            {
                #region 条码数据处理
                if (args.CardType == 4)
                {
                    OnMessage($"请求类型为条形码,CardType ={args.CardType}");
                    string barcodeNo = args.CardNo;
                    Barcode barcode = BarcodeService.GetByNumber(barcodeNo);
                    if (barcode == null)
                    {
                        OnMessage($"未登记的条码,Barcode={barcodeNo}");
                        return response;
                    }
                    DateTime dtBegin = Convert.ToDateTime(barcode.CreateTime);
                    DateTime dtEnd = Convert.ToDateTime(barcode.OutOfTime);
                    if (DateTime.Now < dtBegin || DateTime.Now > dtEnd)
                    {
                        OnMessage($"条码已过有效期,Barcode={barcodeNo}");
                        response.Action = (byte)RecordEvent.RESULT_NOT_PERIOD_TIME;
                        return response;
                    }
                    response.Message_Row1 = "  条码临时卡";
                    response.Message_Row2 = "    ";
                    response.Message_Row3 = $@"   {barcodeNo}";
                    int countOfLeft = 0;
                    int currentTimes = 0;
                    switch (args.IOFlag)
                    {
                        case 0x03:
                            if (barcode.TimesOfIn == 0)
                            {
                                response.Action = (byte)RecordEvent.RESULT_TICKET_IS_OK;
                                return response;
                            }
                            countOfLeft = barcode.TimesOfIn - barcode.TimesOfInLeft;
                            currentTimes = barcode.TimesOfInLeft;
                            break;
                        case 0x04:
                            if (barcode.TimesOfOut == 0)
                            {
                                response.Action = (byte)RecordEvent.RESULT_TICKET_IS_OK;
                                return response;
                            }
                            countOfLeft = barcode.TimesOfOut - barcode.TimesOfOutLeft;
                            currentTimes = barcode.TimesOfOutLeft;
                            break;
                    }
                    if (countOfLeft == 0)
                    {
                        response.Action = (byte)RecordEvent.RESULT_Limit_Nums_ERROR;
                        return response;
                    }
                    if (countOfLeft == 1)
                    {
                        response.Action = (byte)RecordEvent.RESULT_TICKET_IS_OK;
                        UpdateBarcode(barcode.RecId, args.IOFlag, currentTimes + 1);
                        return response;
                    }
                    else
                    {
                        response.Action = (byte)RecordEvent.RESULT_TICKET_IS_OK;
                        response.SingleTicket = 0x01;
                        UpdateBarcode(barcode.RecId, args.IOFlag, currentTimes + 1);
                        return response;
                    }
                }
                #endregion


                #region 卡类与指纹

                EmpInfo empInfo = null;
                try
                {
                    switch (args.CardType)
                    {
                        case 1:
                            OnMessage($"请求类型为:IC/ID卡");

                            break;
                        case 2:
                            OnMessage($"请求类型为:身份证序列号");
                            empInfo = EmpInfoService.GetByCardNo(args.CardType, args.CardNo);
                            break;
                        case 3:
                            OnMessage($"请求类型为:身份证号码,身份证号码为:{args.CardNo}");
                            empInfo = EmpInfoService.GetByCardNo(args.CardType, args.CardNo);

                            break;
                        case 5:
                            if (FingerPrintType == 0)
                            {
                                OnMessage($"请求类型为指纹后台比对,指纹数据长度[{args.FingerPrint.Length}]");
                                int fpId = 0;
                                if (ZKFPHelper.Instance.Identify(args.FingerPrint, out fpId))
                                {
                                    var id = (fpId - 1) / 5;
                                    args.CardNo = id.ToString("00000000");
                                    empInfo = EmpInfoService.GetByEmpId(id);
                                }
                            }
                            else
                            {
                                string fingerId = args.CardNo.Replace("A0", "00").Replace("B0", "00");
                                OnMessage($"请求类型为指纹设备比对,指纹编号为:{args.CardNo}");
                                byte[] array = ArrayHelper.HexToArray(fingerId, 4);
                                int index = ArrayHelper.bytesToInt(array);
                                if (index != 0)
                                {
                                    index = (index - 3) / 5 + 1;
                                    empInfo = EmpInfoService.GetByEmpId(index);
                                }
                            }
                            break;
                        case 6:
                            OnMessage($"请求类型为人脸,CardType ={args.CardType},人脸输出:{args.CardNo}");
                            if (UInt32.TryParse($"{args.CardNo}", System.Globalization.NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out UInt32 empId))
                            {
                                byte[] arr = ArrayHelper.HexToArray(args.CardNo, 4);
                                UInt32 temp = BitConverter.ToUInt32(arr, 0);
                                if (temp > 0)
                                {
                                    temp = (temp - 4) / 5 + 1;
                                    empInfo = EmpInfoService.GetByEmpId((Int32)temp);
                                }
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    OnMessage($"获取人员信息失败:{ex.Message}{ex.StackTrace}，CardNo ={args.CardNo}");
                    return response;
                }
                //找不到匹配的人员信息-无效卡
                if (empInfo == null)
                {
                    response.PhotoName = 65535;
                    OnMessage(string.Format("[{0}]查无此卡信息,禁止通行...", args.CardNo));
                    return response;
                }
                else
                {
                    response.DeptId = empInfo.DeptId;
                    response.DeptName = empInfo.DeptName;
                    response.EmpId = empInfo.EmpId;
                    response.EmpCode = empInfo.EmpCode;
                    response.EmpName = empInfo.EmpName;
                    response.Duty = empInfo.Duty;
                    response.Photo = empInfo.Photo;
                    OnMessage($"找到匹配的人员[{empInfo.EmpCode}{empInfo.EmpName}]");
                }
                if (AppSettings.RightsType == 1)
                {
                    //人脸+刷卡双重验证
                    if (args.CardType == 0x06)
                    {
                        if (string.IsNullOrEmpty(empInfo.ICCardNo))
                        {
                            OnMessage($"人脸验证通过,但无法获取IC/ID卡信息,禁止通行...");
                            return response;
                        }
                        else
                        {
                            response.Action = 0xFF;
                            response.EmpId = ArrayHelper.bytesToInt(ArrayHelper.HexToArray(empInfo.ICCardNo, 4));
                            OnMessage($"人脸验证通过,正在向闸机请求双重验证,卡号[{empInfo.ICCardNo}]..");
                            return response;
                        }
                    }
                }
                //检查权限
                if (!DevRightOfEmpService.CheckRight(empInfo.EmpId, args.DeviceId))
                {
                    response.Action = (byte)RecordEvent.RESULT_IS_BLACK_NAME;
                    OnMessage(string.Format("[{0}]该卡在当前设备没有权限,禁止通行!", args.CardNo));
                    return response;
                }

                //获取票类
                TicketType ticketType = null;
                try
                {
                    ticketType = TicketTypeService.GetByRecId(empInfo.TicketType);
                }
                catch (Exception ex)
                {
                    OnMessage($"获取票类失败:{ex.Message}");
                    return response;
                }

                if (ticketType == null)
                {
                    OnMessage($"没有匹配的票类信息,EmpId ={empInfo.EmpId}");
                    return response;
                }
                //设置自定义显示内容

                string[] content = null;
                try
                {
                    content = CollectService.GetDisplayContent(empInfo.EmpId, ticketType.RecId);
                }
                catch (Exception ex)
                {
                    OnMessage($"获取显示内容失败:{ex.Message}");
                    return response;
                }
                response.Message_Row1 = content[0];
                response.Message_Row2 = content[1];
                response.Message_Row3 = content[2];

                //设置是否显示照片
                if (ticketType.Photo == 1)
                    response.PhotoName = (UInt16)empInfo.EmpId;


                //处理特殊功能卡
                if (ticketType.CardType != 0)
                {
                    response.SpecialAction = (byte)ticketType.CardType;
                    response.Action = 0x00;
                    OnMessage(string.Format("[{0}]特殊功能卡,允许通过!", args.CardNo));
                    return response;
                }

                //检查有效期
                if (!CheckCardEffectDate(empInfo.BeginDate, empInfo.EndDate))
                {
                    response.Action = (byte)RecordEvent.RESULT_OUT_OF_TIME;
                    OnMessage(string.Format("[{0}]该卡已经过有效卡，禁止通行...", args.CardNo));
                    return response;
                }
                #region 处理出入口刷卡请求
                switch (args.IOFlag)
                {
                    case 3:
                        if (AppSettings.LimitedTotalEnabled)
                        {
                            int total = EmpInfoService.GetInsideTotal();
                            if (total >= AppSettings.LimitedTotalOfInside)
                            {
                                response.Action = (byte)RecordEvent.RESULT_Out_Of_LimitedTotal;
                                OnMessage($"当前场内人数已经超过限制的总数[{AppSettings.LimitedTotalOfInside}],禁止进入...");
                                return response;
                            }
                        }
                        //入口无权限
                        if (ticketType.InRight == 0)
                        {
                            response.Action = (byte)RecordEvent.RESULT_CAN_NOT_IN;
                            OnMessage(string.Format("[{0}]该卡入口无权限，禁止通行...", args.CardNo));
                            return response;
                        }
                        //检查时间段
                        if (!CheckTimeGroup(ticketType.IntimeGroupNo))
                        {
                            response.Action = (byte)RecordEvent.RESULT_NOT_PERIOD_TIME;
                            OnMessage(string.Format("有效卡但不在可通行时段范围内,禁止通行...", args.CardNo));
                            return response;
                        }
                        //检验反潜回
                        if (ticketType.AntiSubmarine == 1 && empInfo.IOFlag > 0)
                        {
                            response.Action = (byte)RecordEvent.RESULT_AntiSubmarine;
                            OnMessage(string.Format("已启用反潜回,禁止通行...", args.CardNo));
                            return response;
                        }

                        //检验时段限制
                        if (ticketType.LimitEnabled == 1)
                        {
                            //时间段内限次
                            if (ticketType.TimegroupLimitEnabled == 1)
                            {
                                OnMessage($"时段内限次卡...");
                                if (ticketType.LimitTypeOfTimeGroupLimit == 0 || ticketType.LimitTypeOfTimeGroupLimit == 2)
                                {
                                    int times = RecordService.GetTimesOfTimegroup("进", ticketType.IntimeGroupNo, args.CardNo);
                                    OnMessage($"入口次数:{times}");
                                    if (times >= ticketType.TimesOfTimeGroupLimit)
                                    {
                                        response.Action = (byte)RecordEvent.RESULT_Out_Of_LimitedTimes;
                                        OnMessage(string.Format("超过限制的次数,禁止通行", args.CardNo));
                                        return response;

                                    }
                                }

                            }
                            //有效期内限次
                            if (ticketType.LimitTypeOfEffectDateLimit == 1)
                            {
                                if (ticketType.LimitTypeOfEffectDateLimit == 0 || ticketType.LimitTypeOfEffectDateLimit == 2)
                                {
                                    int times = RecordService.GetTimesOfEffectDate(3, args.CardNo, empInfo.BeginDate, empInfo.EndDate);
                                    if (times >= ticketType.TimesOfEffectDateLimit)
                                    {
                                        response.Action = (byte)RecordEvent.RESULT_Out_Of_LimitedTimes;
                                        OnMessage(string.Format("超过限制的次数,禁止通行", args.CardNo));
                                        return response;
                                    }
                                }
                            }
                            //时段内限时
                            if (ticketType.LimitTimeEnabled == 1)
                            {
                                string recDatetime = RecordService.GetLastTimeOfTimegroup(ticketType.IntimeGroupNo, empInfo.EmpId);
                                if (!recDatetime.Equals(string.Empty))
                                {
                                    try
                                    {
                                        DateTime dt = DateTime.Parse(recDatetime);
                                        int minutes = (int)new TimeSpan(DateTime.Now.Ticks).Subtract(new TimeSpan(dt.Ticks)).TotalMinutes;
                                        if (minutes > ticketType.MinutesOfLimitTime)
                                        {
                                            response.Action = (byte)RecordEvent.RESULT_Limit_Time_ERROR;
                                            OnMessage($"超出指定的时间,禁止通行!");
                                            return response;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        response.Action = (byte)RecordEvent.RESULT_Limit_Time_ERROR;
                                        OnMessage($"时间格式有误:{ex.Message}");
                                        return response;
                                    }
                                }
                            }
                        }
                        //判断实时参数
                        response.Action = 0x00;
                        OnMessage(string.Format("[{0}]有效票并在通行时间范围内，已放行...", args.CardNo));
                        break;
                    case 4:
                        //出口无权限
                        if (ticketType.OutRight == 0)
                        {
                            response.Action = (byte)RecordEvent.RESULT_CAN_NOT_OUT;
                            OnMessage(string.Format("[{0}]该卡出口无权限，禁止通行...", args.CardNo));
                            return response;
                        }
                        //检查时间段
                        if (!CheckTimeGroup(ticketType.OutTimeGroupNo))
                        {
                            response.Action = 0x06;
                            OnMessage(string.Format("有效卡但不再可通行时段范围内,禁止通行...", args.CardNo));
                            return response;
                        }
                        //检验反潜回
                        if (ticketType.AntiSubmarine == 1 && empInfo.IOFlag == 0)
                        {
                            response.Action = (byte)RecordEvent.RESULT_AntiSubmarine;
                            OnMessage(string.Format("已启用反潜回,禁止通行...", args.CardNo));
                            return response;
                        }

                        //检验时段限制
                        if (ticketType.LimitEnabled == 1)
                        {
                            //时间段内限次
                            if (ticketType.TimegroupLimitEnabled == 1)
                            {
                                OnMessage($"时段内限次卡...");
                                if (ticketType.LimitTypeOfTimeGroupLimit == 0 || ticketType.LimitTypeOfTimeGroupLimit == 2)
                                {
                                    int times = RecordService.GetTimesOfTimegroup("出", ticketType.OutTimeGroupNo, args.CardNo);
                                    OnMessage($"出口次数:{times}");
                                    if (times >= ticketType.TimesOfTimeGroupLimit)
                                    {
                                        response.Action = (byte)RecordEvent.RESULT_Out_Of_LimitedTimes;
                                        OnMessage(string.Format("超过限制的次数,禁止通行", args.CardNo));
                                        return response;

                                    }
                                }
                            }
                            //有效期内限次
                            if (ticketType.LimitTypeOfEffectDateLimit == 1)
                            {
                                if (ticketType.LimitTypeOfEffectDateLimit == 0 || ticketType.LimitTypeOfEffectDateLimit == 2)
                                {
                                    int times = RecordService.GetTimesOfEffectDate(4, args.CardNo, empInfo.BeginDate, empInfo.EndDate);
                                    if (times >= ticketType.TimesOfEffectDateLimit)
                                    {
                                        response.Action = (byte)RecordEvent.RESULT_Out_Of_LimitedTimes;
                                        OnMessage(string.Format("超过限制的次数,禁止通行", args.CardNo));
                                        return response;
                                    }
                                }
                            }
                            //时段内限时
                            if (ticketType.LimitTimeEnabled == 1)
                            {
                                string recDatetime = RecordService.GetLastTimeOfTimegroup(ticketType.OutTimeGroupNo, empInfo.EmpId);
                                if (!recDatetime.Equals(string.Empty))
                                {
                                    try
                                    {
                                        DateTime dt = DateTime.Parse(recDatetime);
                                        int minutes = (int)new TimeSpan(DateTime.Now.Ticks).Subtract(new TimeSpan(dt.Ticks)).TotalMinutes;
                                        if (minutes > ticketType.MinutesOfLimitTime)
                                        {
                                            response.Action = (byte)RecordEvent.RESULT_Limit_Time_ERROR;
                                            OnMessage($"超出指定的时间,禁止通行!");
                                            return response;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        response.Action = (byte)RecordEvent.RESULT_Limit_Time_ERROR;
                                        OnMessage($"时间格式有误:{ex.Message}");
                                        return response;
                                    }
                                }
                            }
                        }
                        //检查实时参数
                        response.Action = 0x00;
                        OnMessage(string.Format("[{0}]有效票并在通行时间范围内，已放行...", args.CardNo));
                        break;
                }
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                OnMessage($"实时事件处理失败:{ex.Message}");
            }
            //最后返回
            return response;
        }
        #endregion

        #endregion

        #region 数据插入事件
        private void DataInsertEvent(object sender, DataInsertArgs e)
        {
            if (AppSettings.LedEnabled)
            {
                Task.Factory.StartNew(() =>
                {

                    List<LedController> leds = LedDbService.GetByDeviceId(e.DeviceId);
                    foreach (LedController led in leds)
                    {
                        foreach (DataAccess.Entity.AreaInfo area in led.DynAreas)
                        {
                            int type = 0;
                            string _Content = string.Empty;
                            string content = ReadTxtFile(area.Text);
                            string fileType = area.Text.Substring(area.Text.LastIndexOf('.')).ToUpper();
                            {
                                switch (fileType)
                                {
                                    case ".TXT":
                                        type = 0;
                                        _Content = GetLedTxtContext(content, led.Lid);
                                        break;
                                    case "":
                                        type = 1;
                                        _Content = CreateLedImage(area);
                                        break;
                                }
                            }
                            if (!string.IsNullOrEmpty(_Content))
                                LedManager.Instance.AddTask(type, led.Lid, area.AreaId, _Content);
                        }
                    }
                });
            }
        }
        #endregion

        #region 检查当前时间是否在时间段范围内
        private bool CheckTimeGroup(int groupNo)
        {
            if (groupNo == 0)
                return true;
            int week = (int)DateTime.Now.DayOfWeek + 1;
            List<DataTimeGroup> groupList = DataConverter.GetTimeGroupList(groupNo, week);
            foreach (DataTimeGroup group in groupList)
            {
                Array.Reverse(group.BeginTime);
                Array.Reverse(group.EndTime);
                UInt16 begin = BitConverter.ToUInt16(group.BeginTime, 0);
                UInt16 end = BitConverter.ToUInt16(group.EndTime, 0);
                if (begin == 0 && end == 0)
                    continue;
                byte[] array = ArrayHelper.HexToArray(DateTime.Now.ToString("HHmm"), 2);
                Array.Reverse(array);
                UInt16 now = BitConverter.ToUInt16(array, 0);
                if (begin <= now && now <= end)
                    return true;
            }
            return false;
        }
        #endregion

        #region 检查卡的有效期
        private bool CheckCardEffectDate(string beginDate, string endDate)
        {
            DateTime dtBegin = Convert.ToDateTime($"{beginDate} 00:00:00");
            DateTime dtEnd = Convert.ToDateTime($"{endDate} 00:00:00").AddDays(1);
            if (dtBegin <= DateTime.Now && DateTime.Now <= dtEnd)
                return true;
            return false;

        }

        #endregion


        #region 更新条码使用次数
        private void UpdateBarcode(int recId, int ioFlag, int currentNum)
        {
            try
            {
                BarcodeService.UpdateBarcodeNumofIO(recId, ioFlag, currentNum);
            }
            catch (Exception ex)
            {
                OnMessage($"更新条码出入次数失败:{ex.Message}");
            }
        }
        #endregion

        #region 发送短信，消息推送
        private void SendMessage()
        {

        }
        #endregion

        private DateTime StringToDateTime(string timeStamp)
        {
            /*
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
            */
            timeStamp = $"{DateTime.Now.ToString("yyyy-MM-dd")} {timeStamp}";
            return DateTime.ParseExact(timeStamp, "yyyy-MM-dd HH:mm", null);
        }
    }
}
