using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace HPT.Gate.Device.Data
{
    public class DataNetPara
    {
        /// <summary>
        /// 检查设备是否在线定时器
        /// </summary>
        public System.Timers.Timer _CheckIsOnlineTimer = null;

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DataNetPara()
        {
            this.MachineId = new byte[2];
            this.DevName = new byte[12];
            this.IPAddress = new byte[4];
            this.SubNet = new byte[4];
            this.GateWay = new byte[4];
            this.Port = new byte[2];
            this.HardVersion = new byte[12];
            this.SoftVersion = new byte[6];
            this.ServerAddress = new byte[4];
            this.ServerPort = new byte[2];
            this.StandBy = new byte[12];
        }

        public DataNetPara(int machineId, string devName, string mac, string ip, string subnet, string gateway, int port, string hardversion, string softVersion)
        {
            this.MachineId = ArrayHelper.IntToBytes(machineId, 2);
            this.DevName = ArrayHelper.GB2312ToArray(devName, 12);
            this.MAC = ArrayHelper.MacToHexArray(mac);
            this.IPAddress = ArrayHelper.IPToArray(ip);
            this.SubNet = ArrayHelper.IPToArray(subnet);
            this.GateWay = ArrayHelper.IPToArray(gateway);
            this.Port = BitConverter.GetBytes(port);
            this.HardVersion = ArrayHelper.GB2312ToArray(hardversion, 30);
            this.SoftVersion = ArrayHelper.GB2312ToArray(softVersion, 30);
            this.ServerAddress = new byte[4];
            this.ServerPort = new byte[2];
            this.StandBy = new byte[12];
        }

        public DataNetPara(int machineId, string devName, string mac, string ip, string subnet, string gateway, int port, string hardversion, string softVersion, string serverAddress, int serverPort)
        {
            this.MachineId = ArrayHelper.IntToBytes(machineId, 2);
            this.DevName = ArrayHelper.GB2312ToArray(devName, 12);
            this.MAC = ArrayHelper.MacToHexArray(mac);
            this.IPAddress = ArrayHelper.IPToArray(ip);
            this.SubNet = ArrayHelper.IPToArray(subnet);
            this.GateWay = ArrayHelper.IPToArray(gateway);
            this.Port = BitConverter.GetBytes(port);
            this.HardVersion = ArrayHelper.GB2312ToArray(hardversion, 30);
            this.SoftVersion = ArrayHelper.GB2312ToArray(softVersion, 30);
            this.ServerAddress = ArrayHelper.IPToArray(serverAddress);
            this.ServerPort = ArrayHelper.IntToBytes(serverPort, 2);
            this.StandBy = new byte[12];
        }

        /// <summary>
        /// 是否在线
        /// </summary>
        private bool _IsOnline;

        public bool IsOnline
        {
            get { return _IsOnline; }
            set { _IsOnline = value; }
        }

        /// <summary>
        /// 物理地址
        /// </summary>
        public byte[] MAC
        {
            get { return ArrayHelper.MacToHexArray(this.SMAC); }
            set { this.SMAC = ArrayHelper.ArrayToMAC(value); }
        }
        /// <summary>
        /// 物理地址
        /// </summary>
        public string SMAC { get; set; }

        /// <summary>
        /// 机器号
        /// </summary>
        public byte[] MachineId
        {
            get { return ArrayHelper.IntToBytes(this.IMachineId, 2); }
            set { this.IMachineId = ArrayHelper.bytesToInt(value); }
        }

        /// <summary>
        /// 机器号
        /// </summary>
        public int IMachineId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public byte[] DevName
        {
            get { return ArrayHelper.GB2312ToArray(this.SDevName, 12); }
            set { this.SDevName = ArrayHelper.ArrayToGB2312(value); }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string SDevName { get; set; }


        /// <summary>
        /// IP地址
        /// </summary>
        public byte[] IPAddress
        {
            get { return ArrayHelper.IPToArray(SIPAddress); }
            set { this.SIPAddress = ArrayHelper.ArrayToIPAdress(value); }
        }
        /// <summary>
        /// IP地址
        /// </summary>
        public string SIPAddress { get; set; }


        /// <summary>
        /// 网关
        /// </summary>
        public byte[] GateWay
        {
            get { return ArrayHelper.IPToArray(SGateWay); }
            set { this.SGateWay = ArrayHelper.ArrayToIPAdress(value); }
        }

        /// <summary>
        /// 网关
        /// </summary>
        public string SGateWay { get; set; }


        /// <summary>
        /// 子网掩码
        /// </summary>
        public byte[] SubNet
        {
            get { return ArrayHelper.IPToArray(SSubNet); }
            set { this.SSubNet = ArrayHelper.ArrayToIPAdress(value); }
        }
        /// <summary>
        /// 子网掩码
        /// </summary>
        public string SSubNet { get; set; }


        /// <summary>
        /// 端口号
        /// </summary>
        public byte[] Port
        {
            get { return ArrayHelper.IntToBytes(IPort, 2); }
            set { this.IPort = ArrayHelper.bytesToInt(value); }
        }
        /// <summary>
        /// 端口号
        /// </summary>
        public int IPort { get; set; }


        /// <summary>
        /// 硬件版本号
        /// </summary>
        public byte[] HardVersion
        {
            get { return ArrayHelper.GB2312ToArray(SHardVersion, 12); }
            set { this.SHardVersion = ArrayHelper.ArrayToGB2312(value); }
        }
        /// <summary>
        /// 硬件版本号
        /// </summary>
        public string SHardVersion { get; set; }
        /// <summary>
        /// 软件版本号
        /// </summary>
        public byte[] SoftVersion
        {
            get { return ArrayHelper.GB2312ToArray(SSoftVersion, 6); }
            set { this.SSoftVersion = ArrayHelper.ArrayToGB2312(value); }
        }
        /// <summary>
        /// 软件版本号
        /// </summary>
        public string SSoftVersion { get; set; }

        public byte[] ServerAddress
        {
            get { return ArrayHelper.IPToArray(SServerAddress); }
            set { this.SServerAddress = ArrayHelper.ArrayToIPAdress(value); }
        }

        public string SServerAddress { get; set; }

        public byte[] ServerPort
        {
            get { return ArrayHelper.IntToBytes(IServerPort, 2); }
            set { this.IServerPort = ArrayHelper.bytesToInt(value); }
        }

        public int IServerPort { get; set; }

        public byte[] StandBy { get; set; }


        /// <summary>
        /// 设置设备网络参数是否已经成功的标志
        /// </summary>
        public bool UpdateFlag { get; internal set; }

        #region 序列化与反序列化
        /// <summary>
        /// 直接赋值
        /// </summary>
        /// <param name="data"></param>
        public void Init(Packets packet)
        {
            this.MAC = packet.MAC;
            byte[] data = packet.Data;
            int length = this.MachineId.Length;
            length += this.DevName.Length;
            length += this.IPAddress.Length;
            length += this.GateWay.Length;
            length += this.SubNet.Length;
            length += this.Port.Length;
            length += this.HardVersion.Length;
            length += this.SoftVersion.Length;

            //if (data.Length != length) return;
            int curLen = 0;
            if (data.Length >= curLen + MachineId.Length)
                MachineId = ArrayHelper.SubByte(data, curLen, MachineId.Length);
            curLen += MachineId.Length;

            if (data.Length >= curLen + DevName.Length)
                DevName = ArrayHelper.SubByte(data, curLen, DevName.Length);
            curLen += DevName.Length;

            if (data.Length >= curLen + IPAddress.Length)
                IPAddress = ArrayHelper.SubByte(data, curLen, IPAddress.Length);
            curLen += IPAddress.Length;

            if (data.Length >= curLen + GateWay.Length)
                GateWay = ArrayHelper.SubByte(data, curLen, GateWay.Length);
            curLen += GateWay.Length;

            if (data.Length >= curLen + SubNet.Length)
                SubNet = ArrayHelper.SubByte(data, curLen, SubNet.Length);
            curLen += SubNet.Length;

            if (data.Length >= curLen + Port.Length)
                Port = ArrayHelper.SubByte(data, curLen, Port.Length);
            curLen += Port.Length;

            if (data.Length >= curLen + HardVersion.Length)
                HardVersion = ArrayHelper.SubByte(data, curLen, HardVersion.Length);
            curLen += HardVersion.Length;

            if (data.Length >= curLen + SoftVersion.Length)
                SoftVersion = ArrayHelper.SubByte(data, curLen, SoftVersion.Length);
            curLen += SoftVersion.Length;

            if (data.Length >= curLen + ServerAddress.Length)
                ServerAddress = ArrayHelper.SubByte(data, curLen, ServerAddress.Length);
            curLen += ServerAddress.Length;

            if (data.Length >= curLen + ServerPort.Length)
                ServerPort = ArrayHelper.SubByte(data, curLen, ServerPort.Length);
            curLen += ServerPort.Length;

            if (data.Length >= curLen + StandBy.Length)
                StandBy = ArrayHelper.SubByte(data, curLen, StandBy.Length);
            curLen += StandBy.Length;


        }

        #region 串口数据初始化
        public void SerialInit(Packets packet)
        {
            this.MAC = packet.MAC;
            byte[] data = packet.Data;
            int length = this.MachineId.Length;
            length += this.DevName.Length;
            length += this.IPAddress.Length;
            length += this.GateWay.Length;
            length += this.SubNet.Length;
            length += this.Port.Length;
            length += this.HardVersion.Length;
            length += this.SoftVersion.Length;

            if (data.Length != length) return;
            int curLen = 0;
            MachineId = ArrayHelper.SubByte(data, curLen, MachineId.Length);
            curLen += MachineId.Length;
            DevName = ArrayHelper.SubByte(data, curLen, DevName.Length);
            curLen += DevName.Length;
            IPAddress = ArrayHelper.SubByte(data, curLen, IPAddress.Length);
            curLen += IPAddress.Length;
            GateWay = ArrayHelper.SubByte(data, curLen, GateWay.Length);
            curLen += GateWay.Length;
            SubNet = ArrayHelper.SubByte(data, curLen, SubNet.Length);
            curLen += SubNet.Length;
            Port = ArrayHelper.SubByte(data, curLen, Port.Length);
            curLen += Port.Length;
            HardVersion = ArrayHelper.SubByte(data, curLen, HardVersion.Length);
            curLen += HardVersion.Length;
            SoftVersion = ArrayHelper.SubByte(data, curLen, SoftVersion.Length);
            curLen += SoftVersion.Length;
        }
        #endregion


        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            List<byte> arrayList = new List<byte>();
            arrayList.AddRange(MachineId);
            arrayList.AddRange(DevName);
            arrayList.AddRange(IPAddress);
            arrayList.AddRange(GateWay);
            arrayList.AddRange(SubNet);
            arrayList.AddRange(Port);
            arrayList.AddRange(HardVersion);
            arrayList.AddRange(SoftVersion);
            arrayList.AddRange(ServerAddress);
            arrayList.AddRange(ServerPort);
            arrayList.AddRange(StandBy);
            return arrayList.ToArray();
        }
        #endregion

    }
}
