using HPT.Gate.Device.Data;
using HPT.Gate.Device.Dev;
using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace HPT.Gate.Device
{
    public class TcpServer
    {

        #region private

        public List<TcpSocketState> _StateList = new List<TcpSocketState>();
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        #endregion

        #region property

        #region Instance

        private static TcpServer instance;
        private static readonly object lockHelper = new object();
        private static readonly object _StateLocker = new object();
        public static TcpServer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new TcpServer();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region socket



        #region 服务器参数

        /// <summary>
        /// 服务器socket
        /// </summary>
        private Socket _ServerSocket { get; set; }

        private CancellationTokenSource cts = null;

        #endregion

        /// <summary>
        /// 本地监听端口号
        /// </summary>
        public int LocalPort { get; set; }


        #endregion

        #region data


        /// <summary>
        /// 接收消息队列
        /// </summary>
        private readonly Queue<byte[]> receiveQueue = new Queue<byte[]>();


        #endregion

        #region event

        #region common

        /// <summary>
        /// 消息提示事件
        /// </summary>
        public event Action<string> Message;

        /// <summary>
        /// 触发消息事件
        /// </summary>
        /// <param name="msg">提示消息</param>
        protected void OnMessage(string msg)
        {
            if (msg.Equals(string.Empty)) return;
            try
            {
                Task.Factory.StartNew(() =>
                {
                    if (Message == null) return;
                    Message($"[TCP SERVER]{msg}");
                });
            }
            catch
            {
            }
        }

        #endregion

        #region 实时数据事件

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
        public void OnRealtimeEvent(string cardNo, byte cardType, UInt16 deviceId, byte ioFlag, string recDatetime)
        {
            try
            {
                if (RealtimeEvent == null) return;
                RealTimeDataArgs args = new RealTimeDataArgs();
                args.CardNo = cardNo;
                args.CardType = cardType;
                args.DeviceId = deviceId;
                args.IOFlag = ioFlag;
                args.RecDateTime = recDatetime;
                RealtimeEvent(this, args);
            }
            catch
            {
            }
        }

        #endregion

        #endregion

        #endregion

        #region ctor

        public TcpServer()
        {

        }

        #endregion

        #region 服务器异步发送与接收

        #region 开始监听服务器端口
        /// <summary>
        /// 开始监听
        /// </summary>
        protected void Listenning()
        {

            try
            {
                _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, LocalPort);
                _ServerSocket.Bind(localEndPoint);
                _ServerSocket.Listen(1024);
                while (!cts.IsCancellationRequested)
                {
                    allDone.Reset();
                    if (_ServerSocket != null)
                    {
                        _ServerSocket.BeginAccept(new AsyncCallback(ServerAcceptCallback), _ServerSocket);
                        allDone.WaitOne();
                    }
                }
            }
            catch (Exception ex)
            {
                cts.Cancel();
                OnMessage(string.Format("[本地服务器]监听端口[{1}]发生异常:{0},服务已经停止。", ex.Message, LocalPort));
            }
        }

        #endregion

        /// <summary>
        /// 终止数据发送线程
        /// </summary>
        private void StopReceiveTask()
        {
            cts.Cancel();
            OnMessage("服务已停止....");
        }

        #region 接收数据回调函数
        public void ServerReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            TcpSocketState state = (TcpSocketState)ar.AsyncState;
            Socket handler = state._TcpSocket;
            if (_ServerSocket == null)
            {
                handler.Close();
                handler.Dispose();
                _StateList.Remove(state);
                state.RealtimeEvent -= RealtimeEvent;
                return;
            }
            if (handler == null || !handler.Connected)
                return;

            try
            {
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {
                    byte[] revBytes = ArrayHelper.SubByte(state.RecvDataBuffer, 0, bytesRead);
                    /*
                    if (CheckIsHeartBeat(state, revBytes))
                    {
                        handler.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
                        return;
                    }
                    */
                    state.AlyzeData(revBytes);
                    state.InitRecvBuffer();
                    handler.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
                }
                else
                {
                    if (handler.Poll(-1, SelectMode.SelectRead))
                    {
                        int nRead = handler.Receive(state.RecvDataBuffer, state.RecvDataBuffer.Length, SocketFlags.None);
                        if (nRead == 0)
                        {
                            OnMessage(string.Format("[{0}]连接已断开。", handler.RemoteEndPoint.ToString()));
                            handler.Close();
                            handler.Dispose();
                            _StateList.Remove(state);
                            state.RealtimeEvent -= RealtimeEvent;
                            return;
                        }
                    }
                    else
                    {
                        handler.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
                    }
                }
            }
            catch (Exception ex)
            {
                OnMessage(string.Format("接收数据异常:{0}", ex.Message));
                _StateList.Remove(state);
                state.RealtimeEvent -= RealtimeEvent;
            }


        }

        #endregion

        #region 自动发送
        private void SeverAutoSend(UdpDevice device, byte[] sendData)
        {
            Socket handler = device._RealTimeSocket;
            handler.BeginSend(sendData, 0, sendData.Length, 0, new AsyncCallback(ServerSendCallback), device);
        }
        #endregion

        #region 自动发送
        public void RealTimeResponse(UdpDevice _curDev, Command command, object obj)
        {
            TcpSocketState device = null;
            foreach (TcpSocketState dev in _StateList)
            {
                if (dev.IPAddress == _curDev._IPAddress)
                {
                    device = dev;
                    break;
                }
            }
            if (device == null)
            {
                OnMessage(string.Format("发送数据失败:没找到连接上的设备[{0}]", _curDev._IPAddress));
                return;
            }
            Socket handler = device._TcpSocket;
            byte[] sendData = null;
            try
            {
                sendData = _curDev.Organize(Command.CheckOnLeave, obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (sendData != null)
                handler.Send(sendData);
            //handler.BeginSend(sendData, 0, sendData.Length, 0, new AsyncCallback(ServerSendCallback), device);
            //handler.BeginSend(sendData, 0, sendData.Length, 0, new AsyncCallback(ServerSendCallback), device);
        }
        #endregion

        private void ServerSendCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.     
            UdpDevice state = (UdpDevice)ar.AsyncState;
            Socket handler = state._RealTimeSocket;
            string remote = handler.RemoteEndPoint.ToString();
            try
            {
                int bytesSent = handler.EndSend(ar);
                OnMessage("【本地服务器】:" + state._CameraBuffer.ToString());
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                handler.Close();
                handler.Dispose();
                OnMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "【" + remote + "】交易结束,连接关闭。");
            }
        }
        public void ServerAcceptCallback(IAsyncResult ar)
        {
            try
            {
                allDone.Set();
                Socket listener = (Socket)ar.AsyncState;
                if (_ServerSocket == null)
                {
                    listener.Close();
                    listener.Dispose();
                    return;
                }
                Socket handler = listener.EndAccept(ar);
                string[] address = handler.RemoteEndPoint.ToString().Split(':');
                TcpSocketState state = new TcpSocketState();
                state._TcpSocket = handler;
                state.IPAddress = address[0];
                state.Port = Convert.ToInt32(address[1]);
                lock (_StateLocker)
                {
                    //注册事件***************************************
                    state.RealtimeEvent += RealtimeEvent;
                    //***********************************************
                    if (_StateList.Exists(s => s.IPAddress.Equals(state.IPAddress) && s.Port == state.Port))
                    {
                        List<TcpSocketState> stateList = _StateList.Where(s => s.IPAddress.Equals(state.IPAddress) && s.Port == state.Port).ToList();
                        foreach (TcpSocketState sta in stateList)
                        {
                            DisConnectState(sta);
                        }
                    }
                    _StateList.Add(state);
                    OnMessage($"[{state.IPAddress}:{state.Port}]建立连接成功!");
                    handler.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
                }

            }
            catch (Exception ex)
            {
                //Logs.WriteTxtExceptionLog(ex);
                OnMessage(string.Format("建立连接失败:{0}", ex.Message));
            }

        }



        #endregion

        #region socket服务提供接口方法

        #region 启动服务
        /// <summary>
        /// 启动服务
        /// </summary>
        public virtual void Start()
        {
            cts = new CancellationTokenSource();
            Task.Factory.StartNew(() => { Listenning(); });
            OnMessage(string.Format("服务器开始启动,监听端口为[{0}]", LocalPort));

        }
        #endregion

        #region 停止服务
        /// <summary>
        /// 停止服务
        /// </summary>
        public virtual void Stop()
        {
            try
            {
                StopReceiveTask();
                CloseSocket();

                foreach (TcpSocketState device in _StateList)
                {
                    device.RealtimeEvent -= RealtimeEvent;
                    if (device._TcpSocket != null)
                    {
                        try
                        {
                            if (device._TcpSocket.Connected)
                            {
                                device._TcpSocket.Shutdown(SocketShutdown.Both);
                            }
                            device._TcpSocket.Close();
                        }
                        catch
                        {

                        }

                    }

                }
                OnMessage("服务器已关闭!");
            }
            catch (Exception ex)
            {
                OnMessage(string.Format("服务器关闭失败:{0}", ex.Message));
            }
        }
        #endregion

        #region 重启服务
        /// <summary>
        /// 重启服务
        /// </summary>
        public virtual void Restart()
        {
            OnMessage("服务重启中");
            Stop();
            Start();
        }
        #endregion

        #endregion

        #region private method


        #region 设置连接状态为在线,终端自动断开连接线程
        private void SetStateOnline(TcpSocketState state)
        {
            lock (_StateLocker)
            {
                if (_StateList.Exists(s => s.IPAddress.Equals(state.IPAddress) && s.Port == state.Port))
                    _StateList.Where(s => s.IPAddress.Equals(state.IPAddress) && s.Port == state.Port).ToArray()[0].HeartBeatTime = DateTime.Now;
            }
        }
        #endregion

        #region 主动断开连接

        public void DisConnectState(TcpSocketState state)
        {
            lock (_StateLocker)
            {
                try
                {
                    state._TcpSocket.Close();
                    state._TcpSocket.Dispose();
                    _StateList.Remove(state);
                    state.RealtimeEvent -= RealtimeEvent;
                    OnMessage($"连接[{state.IPAddress}:{state.Port}]已断开!");
                }
                catch
                {

                }
            }
        }
        #endregion

        #region 检查是否心跳包
        private bool CheckIsHeartBeat(TcpSocketState state, byte[] revBytes)
        {
            List<Packets> packets = DataManager.ArrayToPacketList(revBytes);
            if (packets.Count == 0) return false;
            Packets packet = packets[0];
            Command command = (Command)(packet.CommandWord[0] * 256 + packet.CommandWord[1]);
            if (command == Command.HeartBeat)
            {
                try
                {

                    Packets packetSend = new Packets();
                    packetSend.CommandWord = new byte[2] { 0x00, 0x11 };
                    packetSend.Data = null;
                    packetSend.Header = new byte[5] { 0x5A, 0xA5, 0x0F, 0x55, 0xAA };
                    packetSend.DeviceType = packet.DeviceType;
                    packetSend.MachineId = packet.MachineId;
                    packetSend.MAC = packet.MAC;
                    byte[] sendData = packet.ToArray();
                    byte[] retByte = Encryption.EncryPacket(sendData);
                    state._TcpSocket.Send(retByte);
                    SetStateOnline(state);
                }
                catch
                {
                }
                return true;
            }
            return false;
        }
        #endregion


        #region 打开Socket

        protected void OpenSocket()
        {

        }

        #endregion

        #region 关闭Socket

        /// <summary>
        /// 关闭socket连接
        /// </summary>
        protected void CloseSocket()
        {
            #region 服务器Socket
            if (_ServerSocket != null)
            {
                try
                {
                    if (_ServerSocket.Connected)
                    {
                        _ServerSocket.Shutdown(SocketShutdown.Both);
                    }
                    _ServerSocket.Close();
                }
                catch
                {
                }
            }
            _ServerSocket = null;
            #endregion

            #region 连接的Socket

            try
            {
                foreach (TcpSocketState device in _StateList)
                {
                    device.RealtimeEvent -= RealtimeEvent;
                    Socket handler = device._TcpSocket;
                    handler.Close();
                    handler.Dispose();
                }
                _StateList.Clear();
            }
            catch
            {

            }
            #endregion


        }

        #endregion

        #endregion

    }
}
