using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HPT.Joey.Lib.Communication.Tcp
{
    public class TcpSocketServer
    {

        #region private

        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private List<TcpSocketState> _StateList = new List<TcpSocketState>();

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

        #region 建立连接时间
        public event EventHandler<SocketRsDataEventArgs> ConnectionAcceptEvent;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        protected void OnConnectionAcceptEvent(Socket client, string message = "")
        {
            try
            {
                if (DataReceivedEvent == null) return;
                var args = new SocketRsDataEventArgs();
                args.Client = client;
                args.Data = null;
                args.Message = message;
                ConnectionAcceptEvent(this, args);
            }
            catch
            {
            }
        }
        #endregion

        #region 断开连接事件
        public event EventHandler<SocketRsDataEventArgs> LoseConnectionEvent;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        protected void OnLoseConnectionEvent(Socket client, string message = "")
        {
            try
            {
                if (DataReceivedEvent == null) return;
                var args = new SocketRsDataEventArgs();
                args.Client = client;
                args.Data = null;
                args.Message = message;
                LoseConnectionEvent(this, args);
            }
            catch
            {
            }
        }
        #endregion


        #region 数据接收事件
        public event EventHandler<SocketRsDataEventArgs> DataReceivedEvent;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        protected void OnDataReceivedEvent(Socket client, byte[] data, string message = "")
        {
            try
            {
                if (DataReceivedEvent == null) return;
                var args = new SocketRsDataEventArgs();
                args.Client = client;
                args.Data = data;
                args.Message = message;
                DataReceivedEvent(this, args);
            }
            catch
            {
            }
        }
        #endregion

        #region 发送数据事件
        /// <summary>
        /// 数据发送后事件
        /// </summary>
        public event EventHandler<SocketRsDataEventArgs> DataSendedEvent;

        /// <summary>
        /// 触发数据发送后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        protected void OnDataSendedEvent(Socket client, byte[] data, string message = "")
        {
            try
            {
                if (DataSendedEvent == null) return;
                var args = new SocketRsDataEventArgs();
                args.Client = client;
                args.Data = data;
                args.Message = message;
                DataSendedEvent(this, args);
            }
            catch
            {
            }
        }
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
            catch (Exception ex)
            {
                IsStart = false;
                OnMessage($@"Socket Server 监听异常:{ex.Message}");
            }
        }

        #endregion

        #region 接受连接回调函数
        private void ServerAcceptCallback(IAsyncResult ar)
        {
            if (ar == null) return;
            allDone.Set();
            Socket listener = (Socket)ar.AsyncState;
            try
            {
                Socket handler = listener.EndAccept(ar);
                string[] address = handler.RemoteEndPoint.ToString().Split(':');
                TcpSocketState state = new TcpSocketState();
                state._TcpSocket = handler;
                _StateList.Add(state);
                OnMessage($@"{address}建立连接成功!");
                OnConnectionAcceptEvent(handler);
                handler.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
            }
            catch (Exception ex)
            {
                OnMessage($@"{listener.AddressFamily}建立连接失败:{ex.Message}");
            }

        }
        #endregion

        #region 接受数据回调函数
        public void ServerReadCallback(IAsyncResult ar)
        {
            TcpSocketState state = (TcpSocketState)ar.AsyncState;
            Socket handler = state._TcpSocket; ;
            try
            {
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {

                    byte[] revBytes = new byte[bytesRead];
                    Array.Copy(state.RecvDataBuffer, revBytes, bytesRead);
                    OnDataReceivedEvent(handler, revBytes);
                    handler.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, 0, new AsyncCallback(ServerReadCallback), state);
                }
                else
                {
                    if (handler.Poll(-1, SelectMode.SelectRead))
                    {
                        int nRead = handler.Receive(state.RecvDataBuffer, state.RecvDataBuffer.Length, SocketFlags.None);
                        if (nRead == 0)
                        {
                            OnMessage($@"{handler.RemoteEndPoint.ToString()} 连接已经断开");
                            handler.Close();
                            handler.Dispose();
                            OnLoseConnectionEvent(handler);
                            _StateList.Remove(state);
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
                OnMessage($"接收数据异常:{ex.Message}");
                handler.Close();
                handler.Dispose();
                _StateList.Remove(state);
                OnLoseConnectionEvent(handler);
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

        }

        /// <summary>
        /// 终止数据发送线程
        /// </summary>
        private void StopReceiveTask()
        {

        }



        #region 自动发送
        private void SeverAutoSend(TcpSocketState device, byte[] sendData)
        {
            Socket handler = device._TcpSocket;
            handler.BeginSend(sendData, 0, sendData.Length, 0, new AsyncCallback(ServerSendCallback), device);
        }
        #endregion



        private void ServerSendCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.     
            TcpSocketState state = (TcpSocketState)ar.AsyncState;
            Socket handler = state._TcpSocket;
            string remote = handler.RemoteEndPoint.ToString();
            try
            {
                int bytesSent = handler.EndSend(ar);
                //OnMessage("【本地服务器】:" + state._CameraBuffer.ToString());
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




        #endregion

        #region socket服务提供接口方法


        #region 启动服务
        public virtual void Start()
        {
            if (IsStart) return;
            IsStart = true;
            _ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Task.Factory.StartNew(() => { Listenning(); });
            OnMessage("Socket Server 已经启动。");
        }
        #endregion

        /// <summary>
        /// 停止服务
        /// </summary>
        public virtual void Stop()
        {
            try
            {
                IsStart = false;
                CloseSocket();
                foreach (TcpSocketState state in _StateList)
                {
                    state.Dispose();
                    OnLoseConnectionEvent(state._TcpSocket);
                }
                OnMessage("服务器已经停止。");
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
