using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Device.Data;
using HPT.Gate.Utils.Common;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;

namespace HPT.Gate.Device.Dev
{
    public class UdpDevice
    {
        #region Const
        /// <summary>
        /// 设备类型
        /// </summary>
        public static readonly byte[] _DeviceType = new byte[] { 0x03 };

        #endregion

        #region private

        /// <summary>
        /// 数据采集专用Socket
        /// </summary>
        private UdpSocketClient _DefaultClient = null;

        private UdpSocketClient _HeartBeatClient = null;
        /// <summary>
        /// 当前记录指针
        /// </summary>
        private RecordPointer CurrentPointer;

        #endregion


        #region properity

        public string _ServerIPAddress { get; set; }

        public int _ServerPort { get; set; }

        public int Status { get; set; }
        /// <summary>
        /// 摄像头连接socket
        /// </summary>
        public Socket _RealTimeSocket { get; set; }

        public byte[] _CameraBuffer = new byte[1024];

        public int RowIndex { get; set; }

        /// <summary>
        /// 区域编号
        /// </summary>
        public int PlaceId { get; set; }

        /// <summary>
        /// 机器号
        /// </summary>
        public int _MachineId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string _DeviceName { get; set; }

        /// <summary>
        /// 物理地址
        /// </summary>
        public string _Mac { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string _IPAddress { get; set; }

        /// <summary>
        /// 子网掩码
        /// </summary>
        public string _SubnetMark { get; set; }

        /// <summary>
        /// 网关
        /// </summary>
        public string _Gateway { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int _Port { get; set; } = 9293;

        /// <summary>
        /// 硬件版本
        /// </summary>
        public string _HardVersion { get; set; }

        /// <summary>
        /// 软件版本
        /// </summary>
        public string _SoftVersion { get; set; }

        /// <summary>
        /// 设备网络参数
        /// </summary>
        public DataNetPara NetPara
        {
            get { return new DataNetPara(_MachineId, _DeviceName, _Mac, _IPAddress, _SubnetMark, _Gateway, _Port, _HardVersion, _SoftVersion, _ServerIPAddress, _ServerPort); }
            set
            {
                _MachineId = value.IMachineId;
                _DeviceName = value.SDevName;
                _Mac = value.SMAC;
                _IPAddress = value.SIPAddress;
                _SubnetMark = value.SSubNet;
                _Gateway = value.SGateWay;
                _Port = value.IPort;
                _HardVersion = value.SHardVersion;
                _SoftVersion = value.SSoftVersion;
                _Mac = value.SMAC;
                _ServerIPAddress = value.SServerAddress;
                _ServerPort = value.IServerPort;
            }
        }



        /// <summary>
        /// 基本参数
        /// </summary>
        public BasePara DDevPara { get; set; }
        public bool IsOnline
        {
            get
            {
                return GetNetPara() != null;
            }
        }


        #endregion

        #region Ctor
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="para"></param>
        public UdpDevice(DataNetPara para)
        {
            this.NetPara = para;
        }

        public UdpDevice(string ipAddress)
        {
            _MachineId = 0; ;
            _DeviceName = string.Empty;
            _Mac = "FF-FF-FF-FF-FF-FF";
            _IPAddress = ipAddress;
            _SubnetMark = "255.255.255.255";
            _Gateway = "255.255.255.255";
            _Port = 9293;
            _HardVersion = string.Empty;
            _SoftVersion = string.Empty;
        }

        public UdpDevice()
        {

        }

        public UdpDevice(int _machineId, string _deviceName, string _mac, string _iPAddress, string _subnetMark, string _gateway, int _port, string _hardVersion, string _softVersion)
        {
            _MachineId = _machineId;
            _DeviceName = _deviceName;
            _Mac = _mac;
            _IPAddress = _iPAddress;
            _SubnetMark = _subnetMark;
            _Gateway = _gateway;
            _Port = _port;
            _HardVersion = _hardVersion;
            _SoftVersion = _softVersion;
        }

        public UdpDevice(int _machineId, string _deviceName, string _mac, string _iPAddress, string _subnetMark, string _gateway, int _port, string serverIPAddress, int serverPort, string _hardVersion, string _softVersion)
        {
            _MachineId = _machineId;
            _DeviceName = _deviceName;
            _Mac = _mac;
            _IPAddress = _iPAddress;
            _SubnetMark = _subnetMark;
            _Gateway = _gateway;
            _Port = _port;
            _ServerIPAddress = serverIPAddress.Trim().Equals(string.Empty) ? "255.255.255.255" : serverIPAddress;
            _ServerPort = serverPort;
            _HardVersion = _hardVersion;
            _SoftVersion = _softVersion;
        }

        #endregion

        #region Event

        #region Camera

        /// <summary>
        /// 接收到摄像头拍照请求后事件
        /// </summary>
        public event EventHandler<CameraCuptureArgs> CameraCaptureEvent;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        protected void OnCameraCaptureEvent(string cardNo, UInt16 deviceId, byte ioFlag, string recDatetime, string message = "")
        {
            try
            {
                if (CameraCaptureEvent == null) return;
                var args = new CameraCuptureArgs();
                args.CardNo = cardNo;
                args.DeviceId = deviceId;
                args.IOFlag = ioFlag;
                args.RecDatetime = recDatetime;
                args.Message = message;
                CameraCaptureEvent(this, args);
            }
            catch
            {
            }
        }

        #endregion

        #region Voice
        /// <summary>
        /// 接收语音播报事件
        /// </summary>
        public event EventHandler<PlayVoiceArgs> PlayVoiceEvent;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        protected void OnPlayVoiceEvent(byte voiceNo)
        {
            try
            {
                if (PlayVoiceEvent == null) return;
                var args = new PlayVoiceArgs();
                args.VoiceNo = voiceNo;
                PlayVoiceEvent(this, args);
            }
            catch
            {
            }
        }
        #endregion

        #region 检验是否请假事件

        public event EventHandler<CheckOnLeaveArgs> CheckeOnLeaveEvent;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        protected void OnCheckeOnLeaveEvent(int deviceId, string cardNo, int ioFlag)
        {
            try
            {
                if (CheckeOnLeaveEvent == null) return;
                var args = new CheckOnLeaveArgs();
                args.DeviceId = deviceId;
                args.CardNo = cardNo;
                args.IOFlag = ioFlag;
                CheckeOnLeaveEvent(this, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion




        #region 短信平台发送短信事件
        /// <summary>
        /// 接收到摄像头拍照请求后事件
        /// </summary>
        public event EventHandler<RealTimeDataArgs> SMSEvent;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        protected void OnSMSEvent(DataRealTime dRealTime, string message = "")
        {
            try
            {
                if (RealTimeDataEvent == null) return;
                var args = new RealTimeDataArgs();
                args.CardType = dRealTime.CardType;
                args.DeviceId = dRealTime.DeviceId;
                args.IOFlag = dRealTime.IOFlag;
                args.CardNo = dRealTime.CardNo;
                args.RecDateTime = dRealTime.RecDateTime;
                SMSEvent(this, args);
            }
            catch
            {
            }
        }
        #endregion


        #region RealTime
        public event EventHandler<RealTimeDataArgs> RealTimeDataEvent;


        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        protected void OnRealTimeDataEvent(DataRealTime dRealTime, string message = "")
        {
            try
            {
                if (RealTimeDataEvent == null) return;
                var args = new RealTimeDataArgs();
                args.CardType = dRealTime.CardType;
                args.DeviceId = dRealTime.DeviceId;
                args.IOFlag = dRealTime.IOFlag;
                args.RecDateTime = dRealTime.RecDateTime;
                args.CardNo = dRealTime.CardNo;
                args.FingerPrint = dRealTime.FingerData;
                args.FaceDate = dRealTime.FaceData;
                RealTimeDataEvent(this, args);
            }
            catch
            {
            }
        }
        #endregion

        #endregion


        #region Private methords

        #region 将二进制文件转化成更新包
        private List<DataUpdatePacket> GetUpdatePacketListFromArray(byte[] arr)
        {
            List<DataUpdatePacket> updateList = new List<DataUpdatePacket>();
            List<byte[]> list = new List<byte[]>();
            int index = 0;
            while (arr.Length > index)
            {
                byte[] b = new byte[1024];
                int length = arr.Length - index < 1024 ? arr.Length - index : 1024;
                Array.Copy(arr, index, b, 0, length);
                list.Add(b);
                index += length;
            }
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                byte[] bytes = list[i];
                DataUpdatePacket dUpdatePacket = new DataUpdatePacket(count, i, bytes);
                updateList.Add(dUpdatePacket);
            }
            return updateList;
        }

        private List<DataMonitorPacket> GetMonitorUpdatePacketListFromArray(int machineId, byte[] arr)
        {
            List<DataMonitorPacket> updateList = new List<DataMonitorPacket>();
            List<byte[]> list = new List<byte[]>();
            int index = 0;
            while (arr.Length > index)
            {
                byte[] b = new byte[1024];
                int length = arr.Length - index < 1024 ? arr.Length - index : 1024;
                Array.Copy(arr, index, b, 0, length);
                list.Add(b);
                index += length;
            }
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                byte[] bytes = list[i];
                DataMonitorPacket dUpdatePacket = new DataMonitorPacket(machineId, count, i, bytes);
                updateList.Add(dUpdatePacket);
            }
            return updateList;
        }
        #endregion

        #region 组织发送数据
        public byte[] Organize(Command comm, object obj = null)
        {
            Packets packet = new Packets();
            switch (comm)
            {
                ///获取设备信息
                case Command.GetDeviceInfo:
                    packet.CommandWord = new byte[2] { 0x00, 0x11 };
                    packet.Data = null;
                    break;
                ///设置设备信息
                case Command.SetDeviceInfo:
                    packet.CommandWord = new byte[2] { 0x00, 0x12 };
                    packet.Data = ((BasePara)obj).Serialize();
                    break;
                ///卡注册，编辑，注销
                case Command.CardEdit:
                    packet.CommandWord = new byte[2] { 0x00, 0x31 };
                    packet.Data = ((DataCard)obj).ToArray();
                    break;
                ///请求刷卡记录
                case Command.GetRecord:
                    packet.CommandWord = new byte[2] { 0x00, 0x51 };
                    packet.Data = CurrentPointer.ToArray();
                    break;
                case Command.GetDeviceTime:
                    packet.CommandWord = new byte[2] { 0x00, 0x14 };
                    packet.Data = null;
                    break;
                ///请求设置时间
                case Command.SetDeviceTime:
                    packet.CommandWord = new byte[2] { 0x00, 0x13 };
                    packet.Data = ArrayHelper.DateTimeToArray(DateTime.Now);
                    break;
                case Command.GetNetWorkPara:
                    packet.CommandWord = new byte[2] { 0x00, 0x15 };
                    packet.Data = null;
                    break;
                ///请求修改IP地址
                case Command.SetNetWorKPara:
                    packet.CommandWord = new byte[2] { 0x00, 0x16 };
                    packet.Data = ((DataNetPara)obj).ToArray();
                    break;
                ///设置门禁时间段
                case Command.SetTimeGroupOfNormal:
                    packet.CommandWord = new byte[2] { 0x00, 0x43 };
                    packet.Data = ((DataTimeGroupOfDoor)obj).ToArray();
                    break;
                ///设置节假日时间组
                case Command.SetTimeGroupOfVacation:
                    packet.CommandWord = new byte[2] { 0x00, 0x42 };
                    packet.Data = ((DataTimeGroupOfVacation)obj).ToArray();
                    break;
                ///设置节假日
                case Command.SetVacation:
                    packet.CommandWord = new byte[2] { 0x00, 0x41 };
                    packet.Data = ((DataVacation)obj).ToArray();
                    break;
                ///发送图片数组
                case Command.SendImage:
                    packet.CommandWord = new byte[2] { 0x00, 0x61 };
                    packet.Data = ((DataBmp)obj).ToArray();
                    break;
                ///启动更新程序
                case Command.StartUpdate:
                    packet.CommandWord = new byte[2] { 0x00, 0x71 };
                    packet.Data = new byte[] { 0x00, 0x00 };
                    break;
                case Command.Update:
                    packet.CommandWord = new byte[2] { 0x00, 0x72 };
                    packet.Data = ((DataUpdatePacket)obj).ToArray();
                    ///ShowMessage(Tb, "【" + ArrayHelper.ArrayToMAC(MAC) + "】" + System.DateTime.Now.ToString("T") + ":发送更新包【" + UpdateIndex.ToString() + "】", Color.Black);
                    break;
                case Command.SetCardPass:
                    packet.CommandWord = new byte[2] { 0x00, 0x1B };
                    packet.Data = ((DataCardPass)obj).ToArray();
                    break;
                case Command.SetDurationOfDoorTimeGroup:
                    packet.CommandWord = new byte[2] { 0x00, 0x1C };
                    packet.Data = ((DataDuration)obj).ToArray();
                    break;
                case Command.Initialize:
                    packet.CommandWord = new byte[2] { 0x00, 0x23 };
                    packet.Data = null;
                    break;
                case Command.ActiveMainBoard:
                    packet.CommandWord = new byte[2] { 0x00, 0x21 };
                    packet.Data = ArrayHelper.MacToHexArray(_Mac);
                    break;
                case Command.DownLoadFont:
                    packet.CommandWord = new byte[2] { 0x00, 0x22 };
                    packet.Data = null;
                    break;
                case Command.GetSubDevices:
                    packet.CommandWord = new byte[2] { 0x00, 0x17 };
                    packet.Data = null;
                    break;
                case Command.SetSubDevices:
                    packet.CommandWord = new byte[2] { 0x00, 0x18 };
                    packet.Data = ((DataMonitor)obj).Serialize();
                    break;
                case Command.GetCanMachineIds:
                    packet.CommandWord = new byte[2] { 0x00, 0x1F };
                    packet.Data = null;
                    break;
                case Command.SetCanMachineIds:
                    packet.CommandWord = new byte[2] { 0x00, 0x20 };
                    packet.Data = ((DataMachineIds)obj).ToArray();
                    break;
                case Command.StartUpdateMonitor:
                    packet.CommandWord = new byte[2] { 0x00, 0x63 };
                    packet.Data = ArrayHelper.IntToBytes((int)obj, 1);
                    break;
                case Command.UpdateMonitor:
                    packet.CommandWord = new byte[2] { 0x00, 0x62 };
                    packet.Data = ((DataMonitorPacket)obj).ToArray();
                    break;
                case Command.GetSoftPara:
                    packet.CommandWord = new byte[2] { 0x00, 0x19 };
                    packet.Data = null;
                    break;
                case Command.SetSoftPara:
                    packet.CommandWord = new byte[2] { 0x00, 0x1A };
                    packet.Data = ((DataSoftPara)obj).Serialize();
                    break;
                case Command.GetServer:
                    packet.CommandWord = new byte[2] { 0x00, 0x1E };
                    packet.Data = null;
                    break;
                case Command.SetServer:
                    packet.CommandWord = new byte[2] { 0x00, 0x1D };
                    packet.Data = ((DataServer)obj).ToArray();
                    break;
                case Command.CheckOnLeave:
                    packet.CommandWord = new byte[2] { 0x00, 0x37 };
                    packet.Data = ArrayHelper.IntToBytes((int)obj, 1);
                    break;
            }
            packet.Header = new byte[5] { 0x5A, 0xA5, 0x0F, 0x55, 0xAA };
            packet.DeviceType = _DeviceType;
            packet.MachineId = ArrayHelper.IntToBytes(_MachineId, 2);
            packet.MAC = ArrayHelper.MacToHexArray(_Mac);
            byte[] sendData = packet.ToArray();
            byte[] retByte = Encryption.EncryPacket(sendData);
            return retByte;
        }


        #endregion

        #region 解析收到的数据

        #region 解析请求类信息
        private object AnalyzeGetResult(Command command, Packets packet)
        {
            Command com = (Command)(packet.CommandWord[0] * 16 + packet.CommandWord[1]);
            if (com != command) return null;
            object obj = null;
            switch (command)
            {
                case Command.GetDeviceTime:
                    obj = (object)ArrayHelper.ArrayToDateTimeString(packet.Data);
                    break;
                case Command.GetDeviceInfo:
                    BasePara basePara = new BasePara();
                    basePara.Deserialize(packet);
                    obj = (object)basePara;
                    break;
                case Command.GetNetWorkPara:
                    DataNetPara netPara = new DataNetPara();
                    netPara.Init(packet);
                    obj = (object)netPara;
                    break;
                case Command.GetRecord:
                    List<DataRecord> records = new List<DataRecord>();
                    if (packet.Data.Length == 12)
                    {
                        ///记录总数
                        int endIndex = ArrayHelper.bytesToInt(ArrayHelper.SubByte(packet.Data, 4, 4));
                        ///当前记录序号
                        int beginIndex = ArrayHelper.bytesToInt(ArrayHelper.SubByte(packet.Data, 0, 4));
                        if (this.CurrentPointer == null)
                            this.CurrentPointer = new RecordPointer(1);
                        this.CurrentPointer.BeginIndex = beginIndex;
                        this.CurrentPointer.TotalIndex = endIndex;
                        obj = (object)records;
                        break;
                    }
                    records = PacketsToRecords(packet);
                    obj = (object)records;
                    this.CurrentPointer.BeginIndex += records.Count;
                    break;
                case Command.GetSubDevices:
                    DataMonitor monitor = new DataMonitor();
                    monitor.Deserialize(packet);
                    obj = (object)monitor;
                    break;
                case Command.GetCanMachineIds:
                    DataMachineIds machineIds = new DataMachineIds();
                    machineIds.Init(packet.Data);
                    obj = (object)machineIds;
                    break;
                case Command.GetSoftPara:
                    DataSoftPara softPara = new DataSoftPara();
                    softPara.Deserialize(packet.Data);
                    obj = (object)softPara;
                    break;
                case Command.GetServer:
                    DataServer server = new DataServer();
                    server.Init(packet.Data);
                    obj = (object)server;
                    break;
            }
            return obj;
        }

        #endregion

        #region 解析请求类信息
        private object AnalyzeGetResultNew(Command command, Packets packet)
        {
            Command com = (Command)(packet.CommandWord[0] * 16 + packet.CommandWord[1]);
            if (com != command) return null;
            object obj = null;
            switch (command)
            {
                case Command.GetDeviceInfo:
                    BasePara basePara = new BasePara();
                    basePara.Deserialize(packet);
                    obj = (object)basePara;
                    break;
            }
            return obj;
        }

        #endregion

        #region 将数据包转化成记录列表
        private List<DataRecord> PacketsToRecords(Packets packet)
        {
            List<DataRecord> records = new List<DataRecord>();
            byte[] arr = packet.Data;
            int length = new DataRecord().Length;
            if (packet.Data.Length < 40) return records;
            int beginIndex = ArrayHelper.bytesToInt(ArrayHelper.SubByte(arr, 0, 4));
            byte[] endIndex = ArrayHelper.SubByte(arr, 4, 4);
            int endPointer = ArrayHelper.bytesToInt(endIndex);
            byte[] totalOfIn = ArrayHelper.SubByte(arr, 8, 2);
            byte[] totalOfOut = ArrayHelper.SubByte(arr, 10, 2);
            arr = ArrayHelper.SubByte(arr, 12, arr.Length - 12);
            while (arr.Length >= length)
            {
                byte[] nb = ArrayHelper.SubByte(arr, 0, length);
                byte[] arrRecord = ArrayHelper.AddBytes(ArrayHelper.IntToBytes(beginIndex, 4), endIndex);
                arrRecord = ArrayHelper.AddBytes(arrRecord, totalOfIn);
                arrRecord = ArrayHelper.AddBytes(arrRecord, totalOfOut);
                arrRecord = ArrayHelper.AddBytes(arrRecord, nb);
                DataRecord record = new DataRecord();
                record.Init(arrRecord);
                if (!record.CardNo.Equals("00000000"))
                {
                    //record.MachineId = ArrayHelper.IntToBytes(_MachineId, 2);
                    records.Add(record);
                }
                arr = ArrayHelper.SubByte(arr, length, arr.Length - length);
                beginIndex++;
                if (beginIndex >= endPointer)
                    endIndex = ArrayHelper.IntToBytes(beginIndex, 4);
            }
            return records;
        }
        #endregion


        #region 解析相应类信息
        private bool AnalyzeSetResult(Command command, Packets packet)
        {
            Command com = (Command)(packet.CommandWord[0] * 16 + packet.CommandWord[1]);
            if (com != command) return false;
            bool flag = false;
            byte[] data = packet.Data;
            switch (command)
            {
                case Command.SetSubDevices:
                    if (packet.Data.Length == 1 && (Result)packet.Data[0] == Result.SUCCESS)
                    {
                        flag = true;
                    }
                    break;
                case Command.SetNetWorKPara:
                    if (packet.Data.Length == 1 && (Result)packet.Data[0] == Result.SUCCESS)
                    {
                        flag = true;
                    }
                    break;
                case Command.SetDeviceInfo:
                    if (packet.Data.Length == 1 && (Result)packet.Data[0] == Result.SUCCESS)
                    {
                        flag = true;
                    }
                    break;
                case Command.SetDeviceTime:
                    if (packet.Data.Length == 1 && (Result)packet.Data[0] == Result.SUCCESS)
                    {
                        flag = true;
                    }
                    break;
                case Command.CardEdit:
                    if (packet.Data.Length == 7 && (Result)packet.Data[0] == Result.SUCCESS)
                    {
                        flag = true;
                    }
                    break;
                case Command.GetRecord:
                    if (packet.Data.Length == 12)
                    {
                        ///记录总数
                        int endIndex = ArrayHelper.bytesToInt(ArrayHelper.SubByte(packet.Data, 4, 4));
                        ///当前记录序号
                        int beginIndex = ArrayHelper.bytesToInt(ArrayHelper.SubByte(packet.Data, 0, 4));
                        if (this.CurrentPointer == null)
                            this.CurrentPointer = new RecordPointer(1);
                        this.CurrentPointer.BeginIndex = beginIndex;
                        this.CurrentPointer.TotalIndex = endIndex;
                        flag = true;
                    }
                    break;
                case Command.SendImage:
                    if (data.Length == 1 && data[0] != 0xFF)
                    {
                        flag = true;
                        Console.WriteLine(string.Format("发送图片数组[{0}]成功!", data[0]));
                    }
                    break;
                case Command.StartUpdate:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.Update:
                    if (data.Length == 1 && data[0] != 0xFF)
                        flag = true;
                    break;
                case Command.Initialize:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.ActiveMainBoard:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.DownLoadFont:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.SetCanMachineIds:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.StartUpdateMonitor:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.UpdateMonitor:
                    if (data.Length == 1 && data[0] != 0xFF)
                        flag = true;
                    break;
                case Command.SetSoftPara:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.SetServer:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.SetVacation:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.SetTimeGroupOfVacation:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.SetTimeGroupOfNormal:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.SetCardPass:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
            }
            return flag;
        }


        #endregion

        #endregion


        #endregion

        #region 处理实时消息

        internal byte[] AlyzeRealTimeData(byte[] arr)
        {
            List<Packets> packets = DataManager.ArrayToPacketList(arr);
            if (packets.Count == 0) return null;
            Packets packet = packets[0];
            Command command = (Command)(packet.CommandWord[0] * 16 * 16 + packet.CommandWord[1]);
            switch (command)
            {
                case Command.CameraCapture:
                    DataRealTime dRealTime = new DataRealTime();
                    dRealTime.Init(packet.Data);
                    string message = "请求摄像头抓拍";
                    OnCameraCaptureEvent(dRealTime.CardNo, dRealTime.DeviceId, dRealTime.IOFlag, dRealTime.RecDateTime, message);
                    OnRealTimeDataEvent(dRealTime);
                    OnSMSEvent(dRealTime);
                    break;
                case Command.PlayVoice:
                    DataVoice voice = new DataVoice();
                    voice.Init(packet.Data);
                    OnPlayVoiceEvent(voice.VoiceNo);
                    break;
                case Command.CheckOnLeave:
                    DataLeave leave = new DataLeave();
                    leave.Init(packet.Data);
                    OnCheckeOnLeaveEvent(leave.DeviceId, leave.CardNo, leave.IOFlag);
                    break;
            }
            return null;
        }

        #endregion


        #region public methords

        #region 获取设备状态
        public void GetStatus()
        {
            if (_HeartBeatClient == null)
            {
                _HeartBeatClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.GetDeviceInfo);
            byte[] revData = _HeartBeatClient.SendDataAndKeepConnected(sendData);
            if (revData == null)
                this.Status = 2;
            else
                this.Status = 1;

        }
        #endregion


        #region 获取设备时间
        public string GetTime()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.GetDeviceTime);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return string.Empty;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return string.Empty;
            return (string)AnalyzeGetResult(Command.GetDeviceTime, packets[0]);
        }
        #endregion

        #region 设置设备时间

        /// <summary>
        /// 设置设备时间
        /// </summary>
        /// <returns></returns>
        public bool SetTime()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SetDeviceTime);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetDeviceTime, packets[0]);
        }
        #endregion

        #region 设置设备时间
        public bool SetTimeByBroadCast()
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.SetDeviceTime);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetDeviceTime, packets[0]);
        }
        #endregion

        #region 获取设备基本参数
        /// <summary>
        /// 获取设备基本参数
        /// </summary>
        /// <returns></returns>
        public BasePara GetBasePara()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.GetDeviceInfo);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return this.DDevPara;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return null;
            return (BasePara)AnalyzeGetResult(Command.GetDeviceInfo, packets[0]);
        }
        #endregion

        #region 获取设备基本参数
        public BasePara GetBaseParaByBroadCast()
        {
            using (var client = new UdpSocketClient(9293))
            {
                byte[] sendData = Organize(Command.GetDeviceInfo);
                byte[] revData = client.SendDataAndKeepConnected(sendData);
                if (revData == null) return this.DDevPara;
                List<Packets> packets = DataManager.ArrayToPacketList(revData);
                if (packets.Count == 0) return null;
                return (BasePara)AnalyzeGetResultNew(Command.GetDeviceInfo, packets[0]);
            }
        }
        #endregion

        #region 设置设备基本参数
        /// <summary>
        /// 设置设备基本参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public bool SetBasePara(BasePara para)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SetDeviceInfo, para);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetDeviceInfo, packets[0]);
        }
        #endregion

        #region 设置设备基本参数
        /// <summary>
        /// 设置设备基本参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public bool SetBaseParaByBroadCast(BasePara para)
        {
            using (var client = new UdpSocketClient(9293))
            {
                byte[] sendData = Organize(Command.SetDeviceInfo, para);
                byte[] revData = client.SendDataAndKeepConnected(sendData);
                if (revData == null) return false;
                List<Packets> packets = DataManager.ArrayToPacketList(revData);
                if (packets.Count == 0) return false;
                return AnalyzeSetResult(Command.SetDeviceInfo, packets[0]);
            }

        }
        #endregion

        #region 获取设备网络参数

        public DataNetPara GetNetPara()
        {
            using (UdpSocketClient client = new UdpSocketClient(9293))
            {
                byte[] sendData = Organize(Command.GetNetWorkPara);
                byte[] revData = client.SendDataAndShutDownSocket(sendData);
                if (revData == null) return null;
                List<Packets> packets = DataManager.ArrayToPacketList(revData);
                if (packets.Count == 0) return null;
                return (DataNetPara)AnalyzeGetResult(Command.GetNetWorkPara, packets[0]);
            }
        }
        #endregion

        #region 获取网络参数
        public DataNetPara GetNetParaByBroadCast()
        {
            using (UdpSocketClient client = new UdpSocketClient(9293))
            {
                byte[] sendData = Organize(Command.GetNetWorkPara);
                byte[] revData = client.SendDataAndShutDownSocket(sendData);
                if (revData == null) return null;
                List<Packets> packets = DataManager.ArrayToPacketList(revData);
                if (packets.Count == 0) return null;
                return (DataNetPara)AnalyzeGetResult(Command.GetNetWorkPara, packets[0]);
            }
        }
        #endregion

        #region 刷新设备
        public void Refresh()
        {
            if (_DefaultClient == null) return;
            _DefaultClient.CloseSocket();
            _DefaultClient = null;
        }
        #endregion

        #region 设置设备网络参数
        /// <summary>
        /// 设置设备网络参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public bool SetNetPara(DataNetPara para, CommType commtype)
        {
            byte[] sendData = Organize(Command.SetNetWorKPara, para);
            byte[] revData = null;
            switch (commtype)
            {
                case CommType.TCP:
                    if (_DefaultClient == null)
                    {
                        _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
                    }
                    revData = _DefaultClient.SendDataAndKeepConnected(sendData);
                    break;
                case CommType.UDP:
                    UdpSocketClient udpClient = new UdpSocketClient(this._IPAddress, this._Port);
                    revData = udpClient.SendDataAndShutDownSocket(sendData);
                    break;
            }
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            bool flag = AnalyzeSetResult(Command.SetNetWorKPara, packets[0]);
            if (!flag) return false;
            _DefaultClient.CloseSocket();
            _DefaultClient = null;
            this.NetPara = para;
            return true;
        }
        #endregion

        #region Udp设置网络参数
        public bool SetNetPara(DataNetPara para)
        {
            if (_DefaultClient == null)
            {

                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SetNetWorKPara, para);
            byte[] revData = _DefaultClient.SendDataAndShutDownSocket(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            bool flag = AnalyzeSetResult(Command.SetNetWorKPara, packets[0]);
            if (!flag) return false;
            _DefaultClient.CloseSocket();
            _DefaultClient = null;
            this.NetPara = para;
            return true;
        }
        #endregion

        #region Udp设置网络参数
        public bool SetNetParaBroadcast(DataNetPara para)
        {
            using (UdpSocketClient client = new UdpSocketClient(9293))
            {
                byte[] sendData = Organize(Command.SetNetWorKPara, para);
                byte[] revData = client.SendBroadCastDataAndKeepConnected(sendData);
                if (revData == null) return false;
                List<Packets> packets = DataManager.ArrayToPacketList(revData);
                if (packets.Count == 0) return false;
                bool flag = AnalyzeSetResult(Command.SetNetWorKPara, packets[0]);
                if (!flag) return false;
                this.NetPara = para;
                return true;
            }
        }
        #endregion

        #region Udp设置网络参数
        public bool SetNetParaByBroadCast(DataNetPara para)
        {
            if (_DefaultClient == null)
            {

                _DefaultClient = new UdpSocketClient("255.255.255.255", this._Port);
            }
            byte[] sendData = Organize(Command.SetNetWorKPara, para);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            bool flag = AnalyzeSetResult(Command.SetNetWorKPara, packets[0]);
            if (!flag) return false;
            _DefaultClient.CloseSocket();
            _DefaultClient = null;
            this.NetPara = para;
            return true;
        }
        #endregion

        #region 同步卡片信息

        public bool SynCardData(DataCard dCard)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.CardEdit, dCard);
            //UdpSocketClient client = new UdpSocketClient(this._IPAddress, this._Port);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.CardEdit, packets[0]);
        }
        #endregion

        #region 获取记录
        public DataRecord GetRecord()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            if (!GetRecordPointer()) return null;
            byte[] sendData = Organize(Command.GetRecord, CurrentPointer);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return null;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return null;
            List<DataRecord> recordList = (List<DataRecord>)AnalyzeGetResult(Command.GetRecord, packets[0]);
            if (recordList == null || recordList.Count == 0) return null;
            return recordList[0];
        }
        #endregion

        #region 获取记录
        public List<DataRecord> GetRecords()
        {
            List<DataRecord> records = new List<DataRecord>();
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            if (!GetRecordPointer()) return records;
            byte[] revData = null;
            byte[] sendData = Organize(Command.GetRecord, CurrentPointer);
            revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return records;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return records;
            return (List<DataRecord>)AnalyzeGetResult(Command.GetRecord, packets[0]);
        }
        #endregion

        #region 获取采集记录指针
        /// <summary>
        /// 获取采集记录指针
        /// </summary>
        /// <returns></returns>
        private bool GetRecordPointer()
        {
            if (this.CurrentPointer != null) return true;
            this.CurrentPointer = new RecordPointer(1);
            byte[] sendData = Organize(Command.GetRecord);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.GetRecord, packets[0]);
        }
        #endregion

        #region 上传图片
        /// <summary>
        ///  上传图片
        /// </summary>
        /// <param name="image">图像文件Image</param>
        /// <param name="ioFlag">出入口标志:3入口，4出口</param>
        /// <param name="bmpId">图片编号,65535为背景图片</param>
        /// <returns></returns>
        public bool SendImage(Image image, int ioFlag, UInt64 bmpId)
        {
            if (image == null) return false;
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            Bitmap bmp = new Bitmap(image);
            bmp = ImageHelper.KiResizeImage(bmp, 160, 160);
            List<byte[]> imageBytes = ImageHelper.ImageByteList(ImageHelper.GetByteImage(bmp), 1024);
            for (int index = 0; index < imageBytes.Count; index++)
            {
                DataBmp dbmp = new DataBmp();
                dbmp.Index = new byte[] { (byte)index };
                dbmp.BmpType = new byte[] { (byte)ioFlag };
                dbmp.BmpName = ArrayHelper.IntToBytes(bmpId);
                dbmp.BmpBytes = imageBytes[index];
                if (!SendImagePacket(dbmp))
                {
                    if (!SendImagePacket(dbmp))
                        return false;
                }
            }
            return true;
        }

        #endregion

        #region 发送背景图片数据包
        public bool SendImagePacket(DataBmp dbmp)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SendImage, dbmp);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SendImage, packets[0]);
        }
        #endregion

        #region 发送背景图片数据包
        public bool SendImagePacketByBroadCast(DataBmp dbmp)
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.SendImage, dbmp);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SendImage, packets[0]);
        }
        #endregion

        #region 更新主板程序
        public bool UpdateMainBoard(byte[] updateFile)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            List<DataUpdatePacket> updateList = GetUpdatePacketListFromArray(updateFile);
            if (updateList.Count == 0) return false;
            if (!StartUpdateMainBoard()) return false;
            Thread.Sleep(2000);
            for (int i = 0; i < updateList.Count; i++)
            {
                DataUpdatePacket packet = updateList[i];
                if (!SendUpdateMainBoardPacket(packet))
                {
                    if (!SendUpdateMainBoardPacket(packet))
                        return false;
                }
            }
            return true;
        }

        #endregion

        #region 启动主板更新
        public bool StartUpdateMainBoard()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.StartUpdate);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.StartUpdate, packets[0]);
        }

        #endregion

        #region 启动主板更新
        public bool StartUpdateMainBoardByBroadCast()
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.StartUpdate);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.StartUpdate, packets[0]);
        }

        #endregion

        #region 发送主板更新包
        public bool SendUpdateMainBoardPacket(DataUpdatePacket packet)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.Update, packet);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.Update, packets[0]);
        }
        #endregion

        #region 发送主板更新包
        public bool SendUpdateMainBoardPacketByBroadCast(DataUpdatePacket packet)
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.Update, packet);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.Update, packets[0]);
        }
        #endregion

        #region 设备初始化
        /// <summary>
        /// 设备初始化
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.Initialize);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.Initialize, packets[0]);
        }
        #endregion

        #region 设备初始化
        /// <summary>
        /// 设备初始化
        /// </summary>
        /// <returns></returns>
        public bool InitializeByBroadCast()
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.Initialize);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.Initialize, packets[0]);
        }
        #endregion

        #region 激活主板
        public bool ActiveMainBoard()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.ActiveMainBoard);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.ActiveMainBoard, packets[0]);
        }
        #endregion

        #region 激活主板
        public bool ActiveMainBoardByBroadCast()
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.ActiveMainBoard);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.ActiveMainBoard, packets[0]);
        }
        #endregion

        #region 下载字库
        public bool DownLoadFont()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.DownLoadFont);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.DownLoadFont, packets[0]);
        }
        #endregion

        #region 下载字库
        public bool DownLoadFontByBroadCast()
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.DownLoadFont);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.DownLoadFont, packets[0]);
        }
        #endregion

        #region 获取子设备列表
        public DataMonitor GetSubDevices(UInt16 machineId)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            //List<DataMonitor> monitorList = new List<DataMonitor>();
            byte[] sendData = Organize(Command.GetSubDevices, machineId);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null || revData.Length == 0) return null;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return null;
            return (DataMonitor)AnalyzeGetResult(Command.GetSubDevices, packets[0]);
        }
        #endregion

        #region 获取子设备列表
        public DataMonitor GetSubDevicesByBroadCast()
        {
            using (var client = new UdpSocketClient(9293))
            {
                byte[] sendData = Organize(Command.GetSubDevices, null);
                byte[] revData = client.SendDataAndKeepConnected(sendData);
                if (revData == null || revData.Length == 0) return null;
                List<Packets> packets = DataManager.ArrayToPacketList(revData);
                if (packets.Count == 0) return null;
                return (DataMonitor)AnalyzeGetResult(Command.GetSubDevices, packets[0]);
            }
        }
        #endregion

        #region 设置显示屏参数
        public bool SetSubDevicesByBroadCast(DataMonitor dMonitor)
        {
            using (var client = new UdpSocketClient(9293))
            {
                byte[] sendData = Organize(Command.SetSubDevices, dMonitor);
                byte[] revData = client.SendDataAndKeepConnected(sendData);
                if (revData == null) return false;
                List<Packets> packets = DataManager.ArrayToPacketList(revData);
                if (packets.Count == 0) return false;
                return AnalyzeSetResult(Command.SetSubDevices, packets[0]);
            }
        }
        #endregion

        #region 获取Can中转器存储机器号
        public DataMachineIds GetCanMachineIds()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.GetCanMachineIds);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return null;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return null;
            return (DataMachineIds)AnalyzeGetResult(Command.GetCanMachineIds, packets[0]);
        }
        #endregion

        #region 设置Can中转器存储机器号
        public bool SetCanMachineIds(DataMachineIds machineIds)
        {
            byte[] sendData = Organize(Command.SetCanMachineIds, machineIds);
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);

            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetCanMachineIds, packets[0]);
        }
        #endregion

        #region 更新显示屏程序
        public bool UpdateMonitor(byte[] updateFile, int machineId)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            List<DataMonitorPacket> updateList = GetMonitorUpdatePacketListFromArray(machineId, updateFile);
            if (updateList.Count == 0) return false;
            if (!StartUpdateMonitor(machineId)) return false;
            Thread.Sleep(2000);
            for (int i = 0; i < updateList.Count; i++)
            {
                DataMonitorPacket packet = updateList[i];
                if (!SendMonitorUpdatePacket(packet))
                {
                    if (!SendMonitorUpdatePacket(packet))
                        return false;
                }
            }
            return true;
        }

        #endregion

        #region 启动显示屏更新
        public bool StartUpdateMonitor(int machineId)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.StartUpdateMonitor, machineId);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.StartUpdateMonitor, packets[0]);
        }

        #endregion

        #region 启动显示屏更新
        public bool StartUpdateMonitorByBroadCast(int machineId)
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.StartUpdateMonitor, machineId);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.StartUpdateMonitor, packets[0]);
        }

        #endregion

        #region 发送显示屏更新包
        public bool SendMonitorUpdatePacket(DataMonitorPacket packet)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.UpdateMonitor, packet);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.UpdateMonitor, packets[0]);
        }
        #endregion

        #region 发送显示屏更新包
        public bool SendMonitorUpdatePacketByBroadCast(DataMonitorPacket packet)
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.UpdateMonitor, packet);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.UpdateMonitor, packets[0]);
        }
        #endregion

        #region 获取主板功能参数
        public DataSoftPara GetSoftPara()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.GetSoftPara);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return null;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return null;
            return (DataSoftPara)AnalyzeGetResult(Command.GetSoftPara, packets[0]);
        }
        #endregion

        #region 获取主板功能参数
        public DataSoftPara GetSoftParaByBroadCast()
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.GetSoftPara);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return null;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return null;
            return (DataSoftPara)AnalyzeGetResult(Command.GetSoftPara, packets[0]);
        }
        #endregion

        #region 设置主板功能参数
        public bool SetSoftPara(DataSoftPara para)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SetSoftPara, para);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetSoftPara, packets[0]);
        }
        #endregion

        #region 设置主板功能参数
        public bool SetSoftParaByBroadCast(DataSoftPara para)
        {
            var client = new UdpSocketClient(9293);
            byte[] sendData = Organize(Command.SetSoftPara, para);
            byte[] revData = client.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetSoftPara, packets[0]);
        }
        #endregion

        #region 获取服务器信息
        public DataServer GetServer()
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.GetServer);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return null;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return null;
            return (DataServer)AnalyzeGetResult(Command.GetServer, packets[0]);
        }
        #endregion

        #region  设置服务器
        public bool SetServer(DataServer dServer)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SetServer, dServer);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetServer, packets[0]);
        }
        #endregion

        #region 设置节假日
        public bool SetVacation(DataVacation dVacation)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SetVacation, dVacation);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetVacation, packets[0]);
        }
        #endregion

        #region 设置节假日时间组
        public bool SetTimeGroupOfVacation(DataTimeGroupOfVacation dtgv)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SetTimeGroupOfVacation, dtgv);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetTimeGroupOfVacation, packets[0]);
        }
        #endregion

        #region 设置星期时间段
        public bool SetTimeGroupOfDoor(DataTimeGroupOfDoor dtgd)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SetTimeGroupOfNormal, dtgd);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetTimeGroupOfNormal, packets[0]);
        }
        #endregion

        #region 设置读写卡参数
        public bool SetCardPass(DataCardPass dCardPass)
        {
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(this._IPAddress, this._Port);
            }
            byte[] sendData = Organize(Command.SetCardPass, dCardPass);
            byte[] revData = _DefaultClient.SendDataAndKeepConnected(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.SetCardPass, packets[0]);
        }
        #endregion

        #region 释放资源

        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (_DefaultClient != null)
                {
                    if (_DefaultClient.IsConnected)
                    {
                        _DefaultClient.CloseSocket();
                    }
                    _DefaultClient = null;
                    disposed = true;
                }
            }
        }

        /// <summary>
        /// Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~UdpDevice()
        {
            Dispose(false);
        }

        #endregion

        #endregion

    }
}
