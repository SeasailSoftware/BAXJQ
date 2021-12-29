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
    public class TcpSocketState
    {


        #region private


        /// <summary>
        /// 接收缓冲区
        /// </summary>
        public byte[] RecvDataBuffer = new byte[2048];

        #endregion


        #region properity

        #region Socket

        /// <summary>
        /// 连接的Socket
        /// </summary>
        /// 
        public string Mac { get; set; } = "FF-FF-FF-FF-FF-FF";

        public Socket _TcpSocket { get; set; }

        public string IPAddress { get; set; }

        public int Port { get; set; }

        public DateTime HeartBeatTime { get; set; }

        public bool CheckConnect
        {
            get
            {
                bool flag = true;
                TimeSpan tsBegin = new TimeSpan(HeartBeatTime.Ticks);
                TimeSpan tsEnd = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan total = tsEnd.Subtract(tsBegin).Duration();
                int second = total.Days * 24 * 60 * 60 + total.Hours * 60 * 60 + total.Minutes * 60 + total.Seconds;
                if (second > 15)
                    flag = false;
                return flag;
            }
        }


        #endregion

        #region Event

        /// <summary>
        /// 接收到实时数据事件
        /// </summary>
        public event EventHandler<RealTimeDataArgs> RealtimeEvent;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public void OnRealtimeEvent(byte cardType, UInt16 deviceId, byte ioFlag, string recDatetime, string cardNo, byte[] fingerPrint, byte[] faceData)
        {
            try
            {
                if (RealtimeEvent == null) return;
                RealTimeDataArgs args = new RealTimeDataArgs();
                args.CardType = cardType;
                args.DeviceId = deviceId;
                args.IOFlag = ioFlag;
                args.RecDateTime = recDatetime;
                args.CardNo = cardNo;
                args.FingerPrint = fingerPrint;
                args.FaceDate = faceData;
                RealtimeEvent(this, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion



        #region 数据缓冲区

        public Packets Packet_SetDeviceTime { get; set; }

        public Packets Packet_GetDeviceInfo { get; set; }

        public Packets Packet_SetDeviceInfo { get; set; }
        public Packets Packet_ActiveMainBoard { get; set; }

        public Packets Packet_GetUnCollected { get; set; }

        public Packets Packet_RestoreRecords { get; set; }
        public Packets Packet_BatchDeleteCard { get; set; }

        public Packets Packet_CameraCapture { get; set; }
        public Packets Packet_CardEdit { get; set; }

        public Packets Packet_CheckOnLeave { get; set; }

        public Packets Packet_DownLoadFont { get; set; }

        public Packets Packet_GetCanMachineIds { get; set; }

        public Packets Packet_GetNetWorkPara { get; set; }

        public Packets Packet_GetRecord { get; set; }

        public Packets Packet_GetServer { get; set; }

        public Packets Packet_GetSoftPara { get; set; }

        public Packets Packet_GetSubDevices { get; set; }

        public Packets Packet_Initialize { get; set; }

        public Packets Packet_PlayVoice { get; set; }

        public Packets Packet_RequestRecords { get; set; }

        public Packets Packet_SendImage { get; set; }

        public Packets Packet_SetCanMachineIds { get; set; }

        public Packets Packet_SetCardPass { get; set; }

        public Packets Packet_SetDurationOfDoorTimeGroup { get; set; }

        public Packets Packet_SetNetWorKPara { get; set; }

        public Packets Packet_SetServer { get; set; }

        public Packets Packet_SetSoftPara { get; set; }

        public Packets Packet_SetTimeGroupOfNormal { get; set; }

        public Packets Packet_SetTimeGroupOfVacation { get; set; }

        public Packets Packet_SetVacation { get; set; }

        public Packets Packet_StartUpdate { get; set; }

        public Packets Packet_StartUpdateMonitor { get; set; }

        public Packets Packet_Update { get; set; }

        public Packets Packet_UpdateMonitor { get; set; }

        public Packets Packet_Wireless433GetDeviceInfo { get; set; }

        public Packets Packet_RemoteControl { get; set; }

        #endregion


        #endregion

        #region Ctor


        public TcpSocketState()
        {

        }
        #endregion

        #region 解析基础数据
        internal void AlyzeData(byte[] revBytes)
        {
            List<Packets> packets = DataManager.ArrayToPacketList(revBytes);
            foreach (Packets packet in packets)
            {
                Command command = (Command)(packet.CommandWord[0] * 256 + packet.CommandWord[1]);
                switch (command)
                {
                    case Command.RemoteControl:
                        Packet_RemoteControl = packet;
                        break;
                    case Command.GetUnCollected:
                        Packet_GetUnCollected = packet;
                        break;
                    case Command.RestoreRecords:
                        Packet_RestoreRecords = packet;
                        break;
                    case Command.ActiveMainBoard:
                        Packet_ActiveMainBoard = packet;
                        break;
                    case Command.BatchDeleteCard:
                        Packet_BatchDeleteCard = packet;
                        break;
                    case Command.CameraCapture:
                        Packet_CameraCapture = packet;
                        break;
                    case Command.CardEdit:
                        Packet_CardEdit = packet;
                        break;
                    case Command.CheckOnLeave:
                        Packet_CheckOnLeave = packet;
                        break;
                    case Command.DownLoadFont:
                        Packet_DownLoadFont = packet;
                        break;
                    case Command.GetAllDeviceInfo:
                        Packet_GetDeviceInfo = packet;
                        break;
                    case Command.GetCanMachineIds:
                        Packet_GetCanMachineIds = packet;
                        break;
                    case Command.GetDeviceInfo:
                        Packet_GetDeviceInfo = packet;
                        break;
                    case Command.GetNetWorkPara:
                        Packet_GetNetWorkPara = packet;
                        this.Mac = ArrayHelper.ArrayToMAC(packet.MAC);
                        this.HeartBeatTime = DateTime.Now;
                        break;
                    case Command.GetRecord:
                        Packet_GetRecord = packet;
                        break;
                    case Command.GetServer:
                        Packet_GetServer = packet;
                        break;
                    case Command.GetSoftPara:
                        Packet_GetSoftPara = packet;
                        break;
                    case Command.GetSubDevices:
                        Packet_GetSubDevices = packet;
                        break;
                    case Command.Initialize:
                        Packet_Initialize = packet;
                        break;
                    case Command.PlayVoice:
                        Packet_PlayVoice = packet;
                        break;
                    case Command.RealtimeData:
                        DataRealTime dRealTime = new DataRealTime();
                        dRealTime.Init(packet.Data);
                        string cardNo = dRealTime.CardNo;
                        byte cardType = dRealTime.CardType;
                        UInt16 deviceId = dRealTime.DeviceId;
                        byte ioFlag = dRealTime.IOFlag;
                        string recDatetime = dRealTime.RecDateTime;
                        byte[] fingerPrint = dRealTime.FingerData;
                        byte[] faceData = dRealTime.FaceData;
                        OnRealtimeEvent(cardType, deviceId, ioFlag, recDatetime, cardNo, fingerPrint, faceData);
                        break;
                    case Command.RequestRecords:
                        Packet_RequestRecords = packet;
                        break;
                    case Command.SendImage:
                        Packet_SendImage = packet;
                        break;
                    case Command.SetCanMachineIds:
                        Packet_SetCanMachineIds = packet;
                        break;
                    case Command.SetCardPass:
                        Packet_SetCardPass = packet;
                        break;
                    case Command.SetDeviceInfo:
                        Packet_SetDeviceInfo = packet;
                        break;
                    case Command.SetDeviceTime:
                        Packet_SetDeviceTime = packet;
                        break;
                    case Command.SetDurationOfDoorTimeGroup:
                        Packet_SetDurationOfDoorTimeGroup = packet;
                        break;
                    case Command.SetNetWorKPara:
                        Packet_SetNetWorKPara = packet;
                        break;
                    case Command.SetServer:
                        Packet_SetServer = packet;
                        break;
                    case Command.SetSoftPara:
                        Packet_SetSoftPara = packet;
                        break;
                    case Command.SetTimeGroupOfNormal:
                        Packet_SetTimeGroupOfNormal = packet;
                        break;
                    case Command.SetTimeGroupOfVacation:
                        Packet_SetTimeGroupOfVacation = packet;
                        break;
                    case Command.SetVacation:
                        Packet_SetVacation = packet;
                        break;
                    case Command.StartUpdate:
                        Packet_StartUpdate = packet;
                        break;
                    case Command.StartUpdateMonitor:
                        Packet_StartUpdateMonitor = packet;
                        break;
                    case Command.Update:
                        Packet_Update = packet;
                        break;
                    case Command.UpdateMonitor:
                        Packet_UpdateMonitor = packet;
                        break;
                    case Command.Wireless433GetDeviceInfo:
                        Packet_Wireless433GetDeviceInfo = packet;
                        break;
                }
            }

        }
        #endregion

        #region 初始化接收缓冲区
        internal void InitRecvBuffer()
        {
            RecvDataBuffer = new byte[2048];
        }
        #endregion

        #region 发送数据和接收数据
        public Packets SendAndRecvData(Command command, byte[] sendData)
        {
            Packets packet = null;
            _TcpSocket.Send(sendData);
            int index = 100;
            switch (command)
            {
                case Command.RemoteControl:
                    Packet_RemoteControl = null;
                    while (index > 0)
                    {
                        if (Packet_RemoteControl != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_RemoteControl;
                    break;
                case Command.GetUnCollected:
                    Packet_GetUnCollected = null;
                    while (index > 0)
                    {
                        if (Packet_GetUnCollected != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_GetUnCollected;
                    break;
                case Command.RestoreRecords:
                    Packet_RestoreRecords = null;
                    while (index > 0)
                    {
                        if (Packet_RestoreRecords != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_RestoreRecords;
                    break;
                case Command.ActiveMainBoard:
                    Packet_ActiveMainBoard = null;
                    while (index > 0)
                    {
                        if (Packet_ActiveMainBoard != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_ActiveMainBoard;
                    break;
                case Command.BatchDeleteCard:
                    Packet_BatchDeleteCard = null;
                    while (index > 0)
                    {
                        if (Packet_BatchDeleteCard != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_BatchDeleteCard;
                    break;
                case Command.CameraCapture:
                    Packet_CameraCapture = null;
                    while (index > 0)
                    {
                        if (Packet_CameraCapture != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_CameraCapture;
                    break;
                case Command.CardEdit:
                    Packet_CardEdit = null;
                    while (index > 0)
                    {
                        if (Packet_CardEdit != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_CardEdit;
                    break;
                case Command.CheckOnLeave:
                    Packet_CheckOnLeave = null;
                    while (index > 0)
                    {
                        if (Packet_CheckOnLeave != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_CheckOnLeave;
                    break;
                case Command.DownLoadFont:
                    Packet_DownLoadFont = null;
                    while (index > 0)
                    {
                        if (Packet_DownLoadFont != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_DownLoadFont;
                    break;
                case Command.GetAllDeviceInfo:
                    Packet_GetDeviceInfo = null;
                    while (index > 0)
                    {
                        if (Packet_GetDeviceInfo != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_GetDeviceInfo;
                    break;
                case Command.GetDeviceInfo:
                    Packet_GetDeviceInfo = null;
                    while (index > 0)
                    {
                        if (Packet_GetDeviceInfo != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_GetDeviceInfo;
                    break;
                case Command.GetCanMachineIds:
                    Packet_GetCanMachineIds = null;
                    while (index > 0)
                    {
                        if (Packet_GetCanMachineIds != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_GetCanMachineIds;
                    break;
                case Command.GetNetWorkPara:
                    Packet_GetNetWorkPara = null;
                    while (index > 0)
                    {
                        if (Packet_GetNetWorkPara != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_GetNetWorkPara;
                    break;
                case Command.GetRecord:
                    Packet_GetRecord = null;
                    while (index > 0)
                    {
                        if (Packet_GetRecord != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_GetRecord;
                    break;
                case Command.GetServer:
                    Packet_GetServer = null;
                    while (index > 0)
                    {
                        if (Packet_GetServer != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_GetServer;
                    break;
                case Command.GetSoftPara:
                    Packet_GetSoftPara = null;
                    while (index > 0)
                    {
                        if (Packet_GetSoftPara != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_GetSoftPara;
                    break;
                case Command.GetSubDevices:
                    Packet_GetSubDevices = null;
                    while (index > 0)
                    {
                        if (Packet_GetSubDevices != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_GetSubDevices;
                    break;
                case Command.Initialize:
                    Packet_Initialize = null;
                    while (index > 0)
                    {
                        if (Packet_Initialize != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_Initialize;
                    break;
                case Command.PlayVoice:
                    Packet_PlayVoice = null;
                    while (index > 0)
                    {
                        if (Packet_PlayVoice != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_PlayVoice;
                    break;
                case Command.RequestRecords:
                    Packet_RequestRecords = null;
                    while (index > 0)
                    {
                        if (Packet_RequestRecords != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_RequestRecords;
                    break;
                case Command.SendImage:
                    Packet_SendImage = null;
                    while (index > 0)
                    {
                        if (Packet_SendImage != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SendImage;
                    break;
                case Command.SetCanMachineIds:
                    Packet_SetCanMachineIds = null;
                    while (index > 0)
                    {
                        if (Packet_SetCanMachineIds != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetCanMachineIds;
                    break;
                case Command.SetCardPass:
                    Packet_SetCardPass = null;
                    while (index > 0)
                    {
                        if (Packet_SetCardPass != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetCardPass;
                    break;
                case Command.SetDeviceInfo:
                    Packet_SetDeviceInfo = null;
                    while (index > 0)
                    {
                        if (Packet_SetDeviceInfo != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetDeviceInfo;
                    break;
                case Command.SetDeviceTime:
                    Packet_SetDeviceTime = null;
                    while (index > 0)
                    {
                        if (Packet_SetDeviceTime != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetDeviceTime;
                    break;
                case Command.SetDurationOfDoorTimeGroup:
                    Packet_SetDurationOfDoorTimeGroup = null;
                    while (index > 0)
                    {
                        if (Packet_SetDurationOfDoorTimeGroup != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetDurationOfDoorTimeGroup;
                    break;
                case Command.SetNetWorKPara:
                    Packet_SetNetWorKPara = null;
                    while (index > 0)
                    {
                        if (Packet_SetNetWorKPara != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetNetWorKPara;
                    break;
                case Command.SetServer:
                    Packet_SetServer = null;
                    while (index > 0)
                    {
                        if (Packet_SetServer != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetServer;
                    break;
                case Command.SetSoftPara:
                    Packet_SetSoftPara = null;
                    while (index > 0)
                    {
                        if (Packet_SetSoftPara != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetSoftPara;
                    break;
                case Command.SetTimeGroupOfNormal:
                    Packet_SetTimeGroupOfNormal = null;
                    while (index > 0)
                    {
                        if (Packet_SetTimeGroupOfNormal != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetTimeGroupOfNormal;
                    break;
                case Command.SetTimeGroupOfVacation:
                    Packet_SetTimeGroupOfVacation = null;
                    while (index > 0)
                    {
                        if (Packet_SetTimeGroupOfVacation != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetTimeGroupOfVacation;
                    break;
                case Command.SetVacation:
                    Packet_SetVacation = null;
                    while (index > 0)
                    {
                        if (Packet_SetVacation != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_SetVacation;
                    break;
                case Command.StartUpdate:
                    Packet_StartUpdate = null;
                    while (index > 0)
                    {
                        if (Packet_StartUpdate != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_StartUpdate;
                    break;
                case Command.StartUpdateMonitor:
                    Packet_StartUpdateMonitor = null;
                    while (index > 0)
                    {
                        if (Packet_StartUpdateMonitor != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_StartUpdateMonitor;
                    break;
                case Command.Update:
                    Packet_Update = null;
                    while (index > 0)
                    {
                        if (Packet_Update != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_Update;
                    break;
                case Command.UpdateMonitor:
                    Packet_UpdateMonitor = null;
                    while (index > 0)
                    {
                        if (Packet_UpdateMonitor != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_UpdateMonitor;
                    break;
                case Command.Wireless433GetDeviceInfo:
                    Packet_Wireless433GetDeviceInfo = null;
                    while (index > 0)
                    {
                        if (Packet_Wireless433GetDeviceInfo != null)
                            break;
                        Thread.Sleep(20);
                        index--;
                    }
                    packet = Packet_Wireless433GetDeviceInfo;
                    break;
            }
            return packet;
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
                if (_TcpSocket != null)
                {
                    if (_TcpSocket.Connected)
                    {
                        _TcpSocket.Close();
                    }
                    _TcpSocket = null;
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
        ~TcpSocketState()
        {
            Dispose(false);
        }

        #endregion


    }
}
