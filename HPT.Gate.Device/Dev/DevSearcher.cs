using HPT.Gate.Device.Data;
using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HPT.Gate.Device.Dev
{
    public class DevSearcher
    {
        #region const

        private Socket _ServerSocket = null;
        private IPEndPoint _ServerIPEndPoint = null;
        private int _Port = 9293;
        private UdpSocketClient _DefaultClient = null;
        #endregion

        #region properity

        /// <summary>
        /// 是否已经启动的标志
        /// </summary>
        public bool IsStart { get; set; }

        /// <summary>
        /// 设备参数列表
        /// </summary>
        public List<DataNetPara> NetParas { get; set; }

        /// <summary>
        /// 彩屏列表
        /// </summary>
        public List<DataMonitor> Monitors { get; set; }

        #endregion



        #region ctor

        public DevSearcher()
        {
            _ServerIPEndPoint = new IPEndPoint(IPAddress.Broadcast, _Port);
            _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _ServerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
        }

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
                    Message(messgae);
                }
            });
        }

        #endregion

        #region Instance

        private static DevSearcher instance;
        private static readonly object lockHelper = new object();

        public static DevSearcher Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new DevSearcher();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion


        #region private Methords


        #region 处理数据包

        /// <summary>
        /// 处理数据包
        /// </summary>
        /// <param name="data"></param>
        public void AnlyzeData(byte[] data)
        {
            List<Packets> packetList = new List<Packets>();
            try
            {
                packetList = DataManager.ArrayToPacketList(data);
            }
            catch (Exception ex)
            {
                OnMessage(string.Format("数据分包错误:{0}", ex.Message));
            }
            foreach (Packets packet in packetList)
            {
                try
                {
                    Analyze(packet);
                }
                catch (Exception ex)
                {
                    OnMessage(string.Format("解析数据包出错:{0}", ex.Message));
                }
            }
        }
        #endregion



        #region 组织发送命令
        /// <summary>
        /// 组织发送命令
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        private byte[] Organize(Command comm, object obj = null)
        {
            Packets packet = new Packets();
            packet.Header = new byte[5] { 0x5A, 0xA5, 0x0F, 0x55, 0xAA };
            packet.DeviceType = new byte[] { 0x03 };
            packet.MachineId = new byte[] { 0x00, 0x01 };
            packet.MAC = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            switch (comm)
            {
                case Command.GetAllDeviceInfo:
                    packet.CommandWord = new byte[] { 0x00, 0x11 };
                    break;
                case Command.GetNetWorkPara:
                    packet.CommandWord = new byte[] { 0x00, 0x15 };
                    break;
                case Command.SetDeviceTime:
                    packet.CommandWord = new byte[2] { 0x00, 0x13 };
                    packet.Data = ArrayHelper.DateTimeToArray(DateTime.Now);
                    break;
                case Command.GetSubDevices:
                    packet.CommandWord = new byte[2] { 0x00, 0x17 };
                    packet.Data = (byte[])obj;
                    break;
                case Command.Wireless433GetDeviceInfo:
                    packet.CommandWord = new byte[2] { 0x00, 0x74 };
                    packet.Data = null;
                    break;
            }
            packet.CRC32 = packet.GetCRC32();
            byte[] arr = packet.ToArray();
            return Encryption.EncryPacket(arr);
        }

        #endregion

        #region 组织发送命令
        /// <summary>
        /// 组织发送命令
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        private byte[] OrganizeSerial(Command comm, object obj = null)
        {
            Packets packet = new Packets();
            packet.Header = new byte[5] { 0x5A, 0xA5, 0x0F, 0x55, 0xAA };
            packet.DeviceType = new byte[] { 0x00 };
            packet.MachineId = ArrayHelper.IntToBytes((int)obj, 2); ;
            packet.MAC = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            switch (comm)
            {
                case Command.Wireless433GetDeviceInfo:
                    packet.CommandWord = new byte[2] { 0x00, 0x74 };
                    packet.Data = null;
                    break;
            }
            packet.ComPass = new byte[] { 0x00, 0x00, 0x00 };
            byte[] sendData = packet.ToArray1();
            return sendData;
        }

        #endregion

        #region 处理数据包
        /// <summary>
        /// 处理接收结果
        /// </summary>
        /// <param name="temp"></param>
        private void Analyze(Packets packet)
        {
            Command command = (Command)ArrayHelper.bytesToInt(packet.CommandWord);
            switch (command)
            {
                case Command.GetAllDeviceInfo:
                    break;
                case Command.GetNetWorkPara:
                    DataNetPara dnwp = new DataNetPara();
                    dnwp.Init(packet);
                    NetParas.Add(dnwp);
                    break;
                case Command.GetSubDevices:
                    DataMonitor dm = new DataMonitor();
                    dm.Deserialize(packet);
                    dm.Mac = ArrayHelper.ArrayToMAC(packet.MAC);
                    Monitors.Add(dm);
                    //ShowMessage(tb, StringManager.ToHexString(data));
                    break;
            }
        }

        #endregion


        #endregion


        #region public Methods

        /// <summary>
        /// 获取在线设备
        /// </summary>
        /// <returns></returns>
        public List<UdpDevice> GetOnLineDevices()
        {
            List<UdpDevice> devList = new List<UdpDevice>();
            if (_DefaultClient == null)
            {
                _DefaultClient = new UdpSocketClient(_Port);
            }
            byte[] sendData = Organize(Command.GetNetWorkPara);
            byte[] revData = _DefaultClient.SendDataAndWaitFixedTime(sendData);
            if (revData == null) return devList;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return devList;
            return (List<UdpDevice>)AnalyzeSetResult(Command.GetNetWorkPara, packets);
        }
        #endregion

        #region 处理数据包
        private object AnalyzeSetResult(Command command, List<Packets> packetsList)
        {
            object obj = null;
            switch (command)
            {
                case Command.GetNetWorkPara:
                    List<UdpDevice> devList = new List<UdpDevice>();
                    foreach (Packets packet in packetsList)
                    {
                        DataNetPara netPara = new DataNetPara();
                        netPara.Init(packet);
                        UdpDevice device = new UdpDevice(netPara);
                        devList.Add(device);
                    }
                    obj = (object)devList;
                    break;
            }
            return obj;
        }
        #endregion


        #region 搜索无线433设备

        public DataNetPara GetOnlineWirelessDevices(int commNo, int baudRate, int index)
        {
            DataNetPara device = null;
            SerialPortHelper.Instance._CommNo = commNo;
            SerialPortHelper.Instance._BaudRate = baudRate;
            byte[] sendData = OrganizeSerial(Command.Wireless433GetDeviceInfo, index);
            //byte[] sendData = ArrayHelper.HexToArray("5A A5 0F 55 AA 00 0C DA DA CA DA 17 DA DA DA 67 9A 05 7E".Replace(" ", ""), 19);
            Console.WriteLine(ArrayHelper.ArrayToHex(sendData));
            byte[] revDate = SerialPortHelper.Instance.SendDataSynchronous(sendData);
            if (revDate == null) return null;
            DataNetPara para = (DataNetPara)AnlyzeData(Command.Wireless433GetDeviceInfo, revDate);
            if (para != null)
                device = para;
            return device;
        }

        #endregion

        #region 处理无线433返回数据
        private object AnlyzeData(Command command, byte[] revDate)
        {
            object obj = null;
            List<Packets> packetList = DataManager.SerialArrayToPacketList(revDate);
            if (packetList.Count == 0) return null;
            Packets packet = packetList[0];
            if ((Command)(packet.CommandWord[0] * 256 + packet.CommandWord[1]) != command)
                return obj;
            if (packet.Data == null)
                return obj;
            switch (command)
            {
                case Command.Wireless433GetDeviceInfo:
                    DataNetPara para = new DataNetPara();
                    para.SerialInit(packet);
                    obj = para;
                    break;
            }
            return obj;
        }
        #endregion


    }
}
