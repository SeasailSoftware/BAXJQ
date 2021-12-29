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

namespace HPT.Gate.Device
{
    public class TcpSocketServer
    {

        #region private

        private CancellationTokenSource ctsReceiveData = new CancellationTokenSource();

        private CancellationTokenSource ctsHeartbeat = new CancellationTokenSource();

        public List<UdpDevice> _StateList = new List<UdpDevice>();



        #endregion

        #region const



        #endregion

        #region property

        #region Instance

        private static TcpSocketServer instance;
        private static readonly object lockHelper = new object();

        public static TcpSocketServer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new TcpSocketServer();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region socket



        #region 服务器参数

        public static ManualResetEvent allDone = new ManualResetEvent(false);
        /// <summary>
        /// 服务器socket
        /// </summary>
        private Socket _ServerSocket { get; set; }

        /// <summary>
        /// 启动标志
        /// </summary>
        public bool IsStart { get; set; }

        #endregion

        /// <summary>
        /// 本地监听端口号
        /// </summary>
        public int LocalPort { get; set; }


        /// <summary>
        /// 远程端口号
        /// </summary>
        public int RemotePort { get; set; }

        /// <summary>
        /// 接收延时(毫秒)
        /// </summary>
        public int ReceiveDelayTimeout { get; set; } = 1;

        /// <summary>
        /// socket异常时延时(毫秒)
        /// </summary>
        public int ErrorDelayTimeout { get; set; } = 1;

        /// <summary>
        /// 在判定为连接失效之前发生的最大异常次数
        /// </summary>
        public int MaxErrorCount { get; set; } = 10;


        /// <summary>
        /// 在判定为连接失效之前发生的最大空包次数
        /// </summary>
        public int MaxEmptyDataCount { get; set; } = 10;

        /// <summary>
        /// 数据接收缓冲区
        /// </summary>
        public static byte[] ReceiveBuffer { get; set; } = new byte[4096];

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
                    Message(msg);
                });
            }
            catch
            {
            }
        }

        #endregion

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
                args.Message = message;
                args.RecDatetime = recDatetime;
                CameraCaptureEvent(this, args);
            }
            catch
            {
            }
        }

        #endregion



        #region RealTime Event

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
        protected void OnCheckeOnLeaveEvent(int deviceId, string cardNo)
        {
            try
            {
                if (CheckeOnLeaveEvent == null) return;
                var args = new CheckOnLeaveArgs();
                args.DeviceId = deviceId;
                args.CardNo = cardNo;
                CheckeOnLeaveEvent(this, args);
            }
            catch
            {
            }
        }
        #endregion

        #endregion

        #endregion

        #region ctor

        public TcpSocketServer()
        {

        }

        #endregion

        #region private methods

        #region 开始监听服务器端口
        /// <summary>
        /// 开始监听
        /// </summary>
        protected void Listenning()
        {

            try
            {
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, LocalPort);
                _ServerSocket.Bind(localEndPoint);
                _ServerSocket.Listen(100);
                while (IsStart)
                {
                    allDone.Reset();
                    if (_ServerSocket != null)
                    {
                        _ServerSocket.BeginAccept(new AsyncCallback(ServerAcceptCallback), _ServerSocket);
                        allDone.WaitOne();
                    }
                }
            }
            catch (Exception e)
            {
                ctsReceiveData.Cancel();
                OnMessage(string.Format("[本地服务器]监听发生异常:{0},服务重新启动....", e.Message));
            }
        }

        #endregion


        #endregion


        #region 服务器异步发送与接收

        /// <summary>
        /// 启动数据接收线程
        /// </summary>
        private void StartReceiveTask()
        {
            ctsReceiveData = new CancellationTokenSource();
            new Task(Listenning).Start();
            OnMessage("【本地服务器】:服务器开始启动...");
        }

        /// <summary>
        /// 终止数据发送线程
        /// </summary>
        private void StopReceiveTask()
        {
            ctsReceiveData.Cancel();
            OnMessage("服务已停止....");
        }

        public void ServerReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            UdpDevice state = (UdpDevice)ar.AsyncState;
            Socket handler = state._RealTimeSocket; ;
            if (_ServerSocket == null)
            {
                handler.Close();
                handler.Dispose();
                _StateList.Remove(state);
                return;
            }
            //string remote = handler.RemoteEndPoint.ToString();
            try
            {
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {
                    byte[] revBytes = ArrayHelper.SubByte(state._CameraBuffer, 0, bytesRead);
                    //OnMessage(string.Format("[{0}]{1}", handler.RemoteEndPoint.ToString(), ArrayHelper.ArrayToHex(revBytes)));
                    byte[] sendData = state.AlyzeRealTimeData(revBytes);
                    if (sendData != null)
                    {
                        SeverAutoSend(state, sendData);
                        //OnMessage(ArrayHelper.ArrayToHex(sendData));
                    }
                    else
                    {
                        handler.BeginReceive(state._CameraBuffer, 0, state._CameraBuffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
                    }

                }
                else
                {
                    if (handler.Poll(-1, SelectMode.SelectRead))
                    {
                        int nRead = handler.Receive(state._CameraBuffer, state._CameraBuffer.Length, SocketFlags.None);
                        if (nRead == 0)
                        {
                            OnMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "【" + handler.RemoteEndPoint.ToString() + "】连接已经断开....");
                            state.CameraCaptureEvent -= CameraCaptureEvent;
                            state.RealTimeDataEvent -= RealTimeDataEvent;
                            state.PlayVoiceEvent -= PlayVoiceEvent;
                            state.CheckeOnLeaveEvent -= CheckeOnLeaveEvent;
                            handler.Close();
                            handler.Dispose();
                            _StateList.Remove(state);
                            return;
                        }
                    }
                    else
                    {
                        handler.BeginReceive(state._CameraBuffer, 0, state._CameraBuffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
                    }
                }
                //handler.BeginReceive(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
            }
            catch (Exception ex)
            {
                OnMessage(string.Format("接收数据异常:{0}", ex.Message));
                //注销时间
                state.CameraCaptureEvent -= CameraCaptureEvent;
                state.RealTimeDataEvent -= RealTimeDataEvent;
                state.PlayVoiceEvent -= PlayVoiceEvent;
                state.CheckeOnLeaveEvent -= CheckeOnLeaveEvent;
                handler.Close();
                handler.Dispose();
                _StateList.Remove(state);
            }


        }

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
            UdpDevice device = null;
            foreach (UdpDevice dev in _StateList)
            {
                if (dev._IPAddress == _curDev._IPAddress)
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
            Socket handler = device._RealTimeSocket;
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
                UdpDevice state = new UdpDevice();
                state._RealTimeSocket = handler;
                state._IPAddress = address[0];
                state._Port = Convert.ToInt32(address[1]);
                //注册事件
                state.CameraCaptureEvent += CameraCaptureEvent;
                state.RealTimeDataEvent += RealTimeDataEvent;
                state.PlayVoiceEvent += PlayVoiceEvent;
                state.CheckeOnLeaveEvent += CheckeOnLeaveEvent;
                _StateList.Add(state);

                OnMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 【" + address + "】建立连接成功!");
                handler.BeginReceive(state._CameraBuffer, 0, state._CameraBuffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
            }
            catch (Exception ex)
            {
                //Logs.WriteTxtExceptionLog(ex);
                OnMessage(string.Format("建立连接失败:{0}", ex.Message));
            }

        }



        #endregion

        #region socket服务提供接口方法

        /// <summary>
        /// 启动服务
        /// </summary>
        public virtual void Start()
        {
            if (IsStart)
            {
                return;
            }
            IsStart = true;
            OpenSocket();
            StartReceiveTask();

        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public virtual void Stop()
        {
            try
            {
                IsStart = false;
                StopReceiveTask();
                CloseSocket();

                foreach (UdpDevice device in _StateList)
                {
                    device.CameraCaptureEvent -= CameraCaptureEvent;
                    device.RealTimeDataEvent -= RealTimeDataEvent;
                    device.PlayVoiceEvent -= PlayVoiceEvent;
                    device.CheckeOnLeaveEvent -= CheckeOnLeaveEvent;
                    device.Dispose();
                }
            }
            catch (Exception ex)
            {
                OnMessage(string.Format("服务器关闭失败:{0}", ex.Message));
            }
        }

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


        #region private method

        #region 打开Socket

        protected void OpenSocket()
        {
            try
            {

                _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //socket.Bind(new IPEndPoint(IPAddress.Any, LocalPort));
                ///socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            }
            catch (Exception ex)
            {
                OnMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 【本地服务器】:Socket连接异常:" + ex.Message);
            }
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
                foreach (UdpDevice device in _StateList)
                {
                    Socket handler = device._RealTimeSocket;
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
