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
    public class TcpDevice
    {
        #region Const
        /// <summary>
        /// 设备类型
        /// </summary>
        public static readonly byte[] _DeviceType = new byte[] { 0x03 };

        #endregion

        #region private


        /// <summary>
        /// 接收缓冲区
        /// </summary>
        public byte[] RecvDataBuffer = new byte[2048];

        #endregion


        #region properity

        #region Socket

        public bool IsOnline { get { return !(_SocketState == null); } }

        public TcpSocketState _SocketState
        {
            get
            {
                //服务器没启动,返回空
                return TcpServer.Instance._StateList.FirstOrDefault(s => s.Mac.Equals(_Mac)); ;
            }
        }

        #endregion


        public int Status { get; set; }
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
        public int _Port { get; set; }

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
            get { return new DataNetPara(_MachineId, _DeviceName, _Mac, _IPAddress, _SubnetMark, _Gateway, _Port, _HardVersion, _SoftVersion); }
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
            }
        }

        /// <summary>
        /// 基本参数
        /// </summary>
        public BasePara DDevPara { get; set; }


        /// <summary>
        /// 门禁时间段参数
        /// </summary>
        public DataTimeGroupOfDoor DTimeGroupOfDoor { get; set; }

        /// <summary>
        /// 节假日时间组
        /// </summary>
        public DataTimeGroupOfVacation DTimeGroupOfVacation { get; set; }

        /// <summary>
        /// 门禁节假日
        /// </summary>
        public DataVacation DVacation { get; set; }

        /// <summary>
        /// 设备读写卡密码
        /// </summary>
        public DataCardPass DCardPass { get; set; }

        /// <summary>
        /// 就餐时间设置
        /// </summary>
        public DataDuration DDuration { get; set; }

        #region 数据缓冲区

        public Packets Packet_SetDeviceTime { get; set; }
        public Packets Packet_ActiveMainBoard { get; set; }

        public Packets Packet_BatchDeleteCard { get; set; }

        #endregion


        #endregion

        #region Ctor
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="para"></param>
        public TcpDevice(DataNetPara para)
        {
            this.NetPara = para;
        }

        public TcpDevice()
        {

        }

        public TcpDevice(string mac)
        {
            _MachineId = 0; ;
            _DeviceName = string.Empty;
            _Mac = mac;
            _IPAddress = "255.255.255.255";
            _SubnetMark = "255.255.255.255";
            _Gateway = "255.255.255.255";
            _Port = 0;
            _HardVersion = string.Empty;
            _SoftVersion = string.Empty;
        }

        public TcpDevice(int _machineId, string _deviceName, string _mac, string _iPAddress, string _subnetMark, string _gateway, int _port, string _hardVersion, string _softVersion)
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
                case Command.GetUnCollected:
                    packet.CommandWord = new byte[2] { 0x00, 0x29 };
                    packet.Data = null;
                    break;
                case Command.RestoreRecords:
                    packet.CommandWord = new byte[2] { 0x00, 0x2A };
                    packet.Data = BitConverter.GetBytes(Convert.ToUInt16(obj));
                    break;
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
                    packet.Data = new byte[] { 0x0A };
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
                    packet.Data = this.DCardPass.ToArray();
                    break;
                case Command.SetDurationOfDoorTimeGroup:
                    packet.CommandWord = new byte[2] { 0x00, 0x1C };
                    packet.Data = this.DDuration.ToArray();
                    break;
                case Command.RealtimeData:
                    packet.CommandWord = new byte[2] { 0x00, 0x3B };
                    packet.Data = ((DataRealtimeRespon)obj).ToArray();
                    break;
                case Command.RemoteControl:
                    packet.CommandWord = new byte[2] { 0x00, 0x2B };
                    byte b = (byte)((int)obj);
                    packet.Data = new byte[] { b };
                    break;
            }
            packet.Header = new byte[5] { 0x5A, 0xA5, 0x0F, 0x55, 0xAA };
            packet.DeviceType = _DeviceType;
            packet.MachineId = ArrayHelper.IntToBytes(_MachineId, 2);
            packet.MAC = ArrayHelper.MacToHexArray(this._Mac);
            byte[] sendData = packet.ToArray();
            byte[] retByte = Encryption.EncryPacket(sendData);
            return retByte;
        }



        #endregion

        #region 解析收到的数据



        #region 解析请求类信息
        private object AnalyzeGetResult(Command command, Packets packet)
        {
            if (packet == null)
                return null;
            Command com = (Command)(packet.CommandWord[0] * 16 + packet.CommandWord[1]);
            if (com != command) return null;
            object obj = null;
            switch (command)
            {
                case Command.GetUnCollected:
                    obj = (object)BitConverter.ToUInt16(packet.Data,0);
                    break;
                case Command.GetDeviceInfo:
                    BasePara basePara = new BasePara();
                    basePara.Deserialize(packet);
                    obj = (object)basePara;
                    break;
                case Command.GetRecord:
                    if (packet.Data == null) return null;
                    if (packet.Data.Length == 2)
                    {
                        break;
                    }
                    List<DataRecord> recordList = new List<DataRecord>();
                    recordList = DividedIntoRecords(packet.Data);
                    /*
                    Record record = new Record();
                    record.Init(packet.Data);
                    record.DeviceId = ArrayHelper.IntToBytes(_MachineId, 2);
                    obj = (object)record;
                    this.CurrentPointer.BeginIndex++;
                    */
                    obj = (object)recordList;
                    break;
                case Command.GetNetWorkPara:
                    DataNetPara netPara = new DataNetPara();
                    netPara.Init(packet);
                    obj = (object)netPara;
                    break;
            }
            return obj;
        }

        #endregion

        #region 解析相应类信息
        private bool AnalyzeSetResult(Command command, Packets packet)
        {
            if (packet == null)
                return false;
            Command com = (Command)(packet.CommandWord[0] * 16 + packet.CommandWord[1]);
            if (com != command) return false;
            bool flag = false;
            byte[] data = packet.Data;
            switch (command)
            {
                case Command.RemoteControl:
                    if (packet.Data.Length == 1 && (Result)packet.Data[0] == Result.SUCCESS)
                    {
                        flag = true;
                    }
                    break;
                case Command.RestoreRecords:
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
                        flag = true;
                    }
                    break;
                case Command.SendImage:
                    if (data.Length == 1 && data[0] != 0xFF)
                    {
                        flag = true;
                        //Console.WriteLine(string.Format("发送图片数组[{0}]成功!", data[0]));
                    }
                    break;
                case Command.SetVacation:
                    if (data.Length == 1 && data[0] != 0xFF)
                    {
                        flag = true;
                    }
                    break;
                case Command.SetTimeGroupOfVacation:
                    if (data.Length == 1 && data[0] != 0xFF)
                    {
                        flag = true;
                    }
                    break;
                case Command.SetTimeGroupOfNormal:
                    if (data.Length == 1 && data[0] != 0xFF)
                    {
                        flag = true;
                    }
                    break;
            }
            return flag;
        }
        #endregion

        #endregion

        #region 将数组分割成若干个记录
        private List<DataRecord> DividedIntoRecords(byte[] arr)
        {
            List<DataRecord> recordList = new List<DataRecord>();
            int length = new DataRecord().Length;
            while (arr.Length >= length)
            {
                byte[] data = ArrayHelper.SubByte(arr, 0, length);
                DataRecord record = new DataRecord();
                record.Init(data);
                recordList.Add(record);
                arr = ArrayHelper.SubByte(arr, length, arr.Length - length);
            }
            return recordList;
        }
        #endregion


        #endregion

        #region public methords

        #region 初始化接收缓冲区
        internal void InitRecvBuffer()
        {
            RecvDataBuffer = new byte[2048];
        }
        #endregion

        #region 发送数据
        private Packets SendAndRecvData(Command command, byte[] sendData)
        {
            if (_SocketState == null)
                return null;
            return _SocketState.SendAndRecvData(command, sendData);
        }
        #endregion


        #region 获取设备状态
        public void GetStatus()
        {
            byte[] sendData = Organize(Command.GetDeviceInfo);
            Packets packet = SendAndRecvData(Command.SetDeviceTime, sendData);
            if (packet == null)
                this.Status = 2;
            else
                this.Status = 1;

        }
        #endregion

        #region 设置设备时间

        /// <summary>
        /// 设置设备时间
        /// </summary>
        /// <returns></returns>
        public bool SetTime()
        {
            byte[] sendData = Organize(Command.SetDeviceTime);
            Packets packet = SendAndRecvData(Command.SetDeviceTime, sendData);
            return AnalyzeSetResult(Command.SetDeviceTime, packet);
        }

        #endregion

        #region 获取设备基本参数
        /// <summary>
        /// 获取设备基本参数
        /// </summary>
        /// <returns></returns>
        public BasePara GetBasePara()
        {
            byte[] sendData = Organize(Command.GetDeviceInfo);
            Packets packet = SendAndRecvData(Command.GetDeviceInfo, sendData);
            return (BasePara)AnalyzeGetResult(Command.GetDeviceInfo, packet);
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
            byte[] sendData = Organize(Command.SetDeviceInfo, para);
            Packets packet = SendAndRecvData(Command.SetDeviceInfo, sendData);
            return AnalyzeSetResult(Command.SetDeviceInfo, packet);
        }
        #endregion

        #region 获取设备网络参数

        public DataNetPara GetNetPara()
        {
            byte[] sendData = Organize(Command.GetNetWorkPara);
            Packets packet = SendAndRecvData(Command.GetNetWorkPara, sendData);
            return (DataNetPara)AnalyzeGetResult(Command.GetNetWorkPara, packet);
        }
        #endregion

        #region 刷新设备
        public void Refresh()
        {

        }
        #endregion

        #region 设置设备网络参数
        /// <summary>
        /// 设置设备网络参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public bool SetNetPara(DataNetPara para)
        {
            byte[] sendData = Organize(Command.SetNetWorKPara, para);
            Packets packet = SendAndRecvData(Command.SetNetWorKPara, sendData);
            return AnalyzeSetResult(Command.SetNetWorKPara, packet);

        }
        #endregion

        #region 同步卡片信息

        public bool SynCardData(DataCard dCard)
        {
            byte[] sendData = Organize(Command.CardEdit, dCard);
            Packets packet = SendAndRecvData(Command.CardEdit, sendData);
            return AnalyzeSetResult(Command.CardEdit, packet);
        }
        #endregion


        #region 获取记录
        public DataRecord GetRecord()
        {
            byte[] sendData = Organize(Command.GetRecord);
            Packets packet = SendAndRecvData(Command.GetRecord, sendData);
            if (packet == null)
                return null;
            return (DataRecord)AnalyzeGetResult(Command.GetRecord, packet);

        }
        #endregion

        #region 获取记录
        public List<DataRecord> GetRecords()
        {
            List<DataRecord> records = new List<DataRecord>();
            byte[] sendData = Organize(Command.GetRecord);
            Packets packet = SendAndRecvData(Command.GetRecord, sendData);
            return (List<DataRecord>)AnalyzeGetResult(Command.GetRecord, packet);
        }
        #endregion

        #region 获取未采集记录数
        public int GetUnCollected()
        {
            byte[] sendData = Organize(Command.GetUnCollected);
            Packets packet = SendAndRecvData(Command.GetUnCollected, sendData);
            return (UInt16)AnalyzeGetResult(Command.GetUnCollected, packet);
        }
        #endregion

        #region 获取未采集记录数
        public bool RestoreRecords(int count)
        {
            byte[] sendData = Organize(Command.RestoreRecords,count);
            Packets packet = SendAndRecvData(Command.RestoreRecords, sendData);
            return AnalyzeSetResult(Command.RestoreRecords, packet);
        }
        #endregion



        /*
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
            Packets packet = SendAndRecvData(Command.GetRecord, sendData);
            return AnalyzeSetResult(Command.GetRecord, packet);
        }
        #endregion
    */

        #region 远程控制
        public bool RemoteControl(GateCommand command)
        {
            byte[] sendData = Organize(Command.RemoteControl,command);
            Packets packet = SendAndRecvData(Command.RemoteControl, sendData);
            return AnalyzeSetResult(Command.RemoteControl, packet);
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
            byte[] sendData = Organize(Command.SendImage, dbmp);
            Packets packet = SendAndRecvData(Command.SendImage, sendData);
            return AnalyzeSetResult(Command.SendImage, packet);
        }
        #endregion

        #region 更新主板程序
        public bool UpdateMainBoard(byte[] updateFile)
        {
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
            byte[] sendData = Organize(Command.StartUpdate);
            Packets packet = SendAndRecvData(Command.StartUpdate, sendData);
            return AnalyzeSetResult(Command.StartUpdate, packet);
        }

        #endregion

        #region 发送主板更新包
        public bool SendUpdateMainBoardPacket(DataUpdatePacket pag)
        {
            byte[] sendData = Organize(Command.Update, pag);
            Packets packets = SendAndRecvData(Command.Update, sendData);
            return AnalyzeSetResult(Command.Update, packets);
        }
        #endregion

        #region 设备初始化
        /// <summary>
        /// 设备初始化
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            byte[] sendData = Organize(Command.Initialize);
            Packets packet = SendAndRecvData(Command.Initialize, sendData);
            return AnalyzeSetResult(Command.Initialize, packet);
        }
        #endregion

        #region 激活主板
        public bool ActiveMainBoard()
        {
            byte[] sendData = Organize(Command.ActiveMainBoard);
            Packets packet = SendAndRecvData(Command.ActiveMainBoard, sendData);
            return AnalyzeSetResult(Command.ActiveMainBoard, packet);
        }
        #endregion

        #region 下载字库
        public bool DownLoadFont()
        {
            byte[] sendData = Organize(Command.DownLoadFont);
            Packets packet = SendAndRecvData(Command.DownLoadFont, sendData);
            return AnalyzeSetResult(Command.DownLoadFont, packet);
        }
        #endregion

        #region 获取子设备列表
        public DataMonitor GetSubDevices(UInt16 machineId)
        {
            byte[] sendData = Organize(Command.GetSubDevices, machineId);
            Packets packet = SendAndRecvData(Command.GetSubDevices, sendData);
            return (DataMonitor)AnalyzeGetResult(Command.GetSubDevices, packet);
        }
        #endregion

        #region 获取Can中转器存储机器号
        public DataMachineIds GetCanMachineIds()
        {
            byte[] sendData = Organize(Command.GetCanMachineIds);
            Packets packet = SendAndRecvData(Command.GetCanMachineIds, sendData);
            return (DataMachineIds)AnalyzeGetResult(Command.GetCanMachineIds, packet);
        }
        #endregion

        #region 设置Can中转器存储机器号
        public bool SetCanMachineIds(DataMachineIds machineIds)
        {
            byte[] sendData = Organize(Command.SetCanMachineIds, machineIds);
            Packets packet = SendAndRecvData(Command.SetCanMachineIds, sendData);
            return AnalyzeSetResult(Command.SetCanMachineIds, packet);
        }

        #endregion

        #region 更新显示屏程序
        public bool UpdateMonitor(byte[] updateFile, int machineId)
        {
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
            byte[] sendData = Organize(Command.StartUpdateMonitor, machineId);
            Packets packet = SendAndRecvData(Command.StartUpdateMonitor, sendData);
            return AnalyzeSetResult(Command.StartUpdateMonitor, packet);
        }

        #endregion

        #region 发送显示屏更新包
        public bool SendMonitorUpdatePacket(DataMonitorPacket pag)
        {
            byte[] sendData = Organize(Command.UpdateMonitor, pag);
            Packets packet = SendAndRecvData(Command.UpdateMonitor, sendData);
            return AnalyzeSetResult(Command.UpdateMonitor, packet);
        }
        #endregion

        #region 获取主板功能参数
        public DataSoftPara GetSoftPara()
        {
            byte[] sendData = Organize(Command.GetSoftPara);
            Packets packet = SendAndRecvData(Command.GetSoftPara, sendData);
            return (DataSoftPara)AnalyzeGetResult(Command.GetSoftPara, packet);
        }
        #endregion

        #region 设置主板功能参数
        public bool SetSoftPara(DataSoftPara para)
        {
            byte[] sendData = Organize(Command.SetSoftPara, para);
            Packets packet = SendAndRecvData(Command.SetSoftPara, sendData);
            return AnalyzeSetResult(Command.SetSoftPara, packet);
        }
        #endregion

        #region 获取服务器信息
        public DataServer GetServer()
        {
            byte[] sendData = Organize(Command.GetServer);
            Packets packet = SendAndRecvData(Command.GetServer, sendData);
            return (DataServer)AnalyzeGetResult(Command.GetServer, packet);
        }
        #endregion

        #region  设置服务器
        public bool SetServer(DataServer dServer)
        {
            byte[] sendData = Organize(Command.SetServer, dServer);
            Packets packet = SendAndRecvData(Command.SetServer, sendData);
            return AnalyzeSetResult(Command.SetServer, packet);
        }
        #endregion

        #region 设置节假日
        public bool SetVacation(DataVacation dVacation)
        {
            byte[] sendData = Organize(Command.SetVacation, dVacation);
            Packets packet = SendAndRecvData(Command.SetVacation, sendData);
            return AnalyzeSetResult(Command.SetVacation, packet);
        }
        #endregion

        #region 设置节假日时间组
        public bool SetTimeGroupOfVacation(DataTimeGroupOfVacation dtgv)
        {
            byte[] sendData = Organize(Command.SetTimeGroupOfVacation, dtgv);
            Packets packet = SendAndRecvData(Command.SetTimeGroupOfVacation, sendData);
            return AnalyzeSetResult(Command.SetTimeGroupOfVacation, packet);
        }
        #endregion

        #region 设置星期时间段
        public bool SetTimeGroupOfDoor(DataTimeGroupOfDoor dtgd)
        {
            byte[] sendData = Organize(Command.SetTimeGroupOfNormal, dtgd);
            Packets packet = SendAndRecvData(Command.SetTimeGroupOfNormal, sendData);
            return AnalyzeSetResult(Command.SetTimeGroupOfNormal, packet);
        }
        #endregion

        #region 设置读写卡参数
        public bool SetCardPass(DataCardPass dCardPass)
        {
            byte[] sendData = Organize(Command.SetCardPass, dCardPass);
            Packets packet = SendAndRecvData(Command.SetCardPass, sendData);
            return AnalyzeSetResult(Command.SetCardPass, packet);
        }
        #endregion

        #region 实时数据回复
        public void RealtimeRespon(DataRealtimeRespon respon)
        {
            byte[] sendData = Organize(Command.RealtimeData, respon);
            SendAndRecvData(Command.RealtimeData, sendData);
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
                disposed = true;
            }
        }

        /// <summary>
        /// Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~TcpDevice()
        {
            Dispose(false);
        }

        #endregion

        #endregion

    }
}
