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
    class UdpSocketClient : IDisposable
    {
        #region Var

        public Socket _ClientSocket = null;
        private IPEndPoint _ServerIPEndPoint = null;
        #endregion


        #region Ctor
        public UdpSocketClient(string remoteIp, int _port)
        {
            _ServerIPEndPoint = new IPEndPoint(IPAddress.Parse(remoteIp), _port);
            _ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _ClientSocket.ReceiveTimeout = 2000;
        }

        public UdpSocketClient(int _port)
        {
            _ServerIPEndPoint = new IPEndPoint(IPAddress.Broadcast, _port);
            _ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _ClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
        }
        #endregion

        #region properity

        /// <summary>
        /// 远程计算机IP地址
        /// </summary>
        public IPAddress _RemoteAddress { get; set; }

        /// <summary>
        /// 远程计算机端口
        /// </summary>
        public int _RemotePort { get; set; }
        /// <summary>
        /// 是否已经连接服务器的标志
        /// </summary>
        public bool IsConnected { get; set; }
        #endregion


        #region 消息提示事件Ecent

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
            if (Message == null)
            {
                return;
            }
            if (msg.Equals(string.Empty)) return;
            try
            {
                Task.Factory.StartNew(() =>
                {
                    if (Message != null)
                    {
                        Message(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
                    }

                });
            }
            catch
            {
            }
        }

        #endregion

        #region private method

        #region 打开Socket
        protected bool OpenSocket()
        {
            if (_ClientSocket != null) return true;
            try
            {
                //_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _ClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                return true;
            }
            catch (Exception ex)
            {
                OnMessage(" 【本地客户端】:Socket连接异常:" + ex.Message);
                return false;
            }
        }

        #endregion

        #region 关闭Socket

        /// <summary>
        /// 关闭socket连接
        /// </summary>
        public void CloseSocket()
        {
            IsConnected = false;
            if (_ClientSocket != null)
            {
                try
                {
                    if (_ClientSocket.Connected)
                    {
                        _ClientSocket.Shutdown(SocketShutdown.Both);
                        OnMessage(string.Format("连接{0}关闭!", _ClientSocket.RemoteEndPoint.ToString()));
                    }
                    _ClientSocket.Close();
                }
                catch (Exception ex)
                {
                    OnMessage(string.Format("关闭连接{0}失败:{1}", _ClientSocket.RemoteEndPoint.ToString(), ex.Message));
                }
            }
            _ClientSocket = null;

        }

        #endregion

        #region 发送数据
        /// <summary>
        /// 直接发送数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool SendData(byte[] sendData)
        {
            if (_ClientSocket != null && _ServerIPEndPoint != null && sendData != null)
            {
                //ShowMessage(tb,StringManager.ToHexString(sendData));
                try
                {
                    _ClientSocket.SendTo(sendData, 0, sendData.Length, SocketFlags.None, _ServerIPEndPoint);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }
        #endregion

        #region 发送数据
        /// <summary>
        /// 直接发送数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool SendBroadCastData(byte[] sendData)
        {
            if (_ClientSocket != null && _ServerIPEndPoint != null && sendData != null)
            {
                //ShowMessage(tb,StringManager.ToHexString(sendData));
                try
                {
                    _ClientSocket.SendTo(sendData, 0, sendData.Length, SocketFlags.None, _ServerIPEndPoint);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }
        #endregion

        #region 同步接收数据

        private byte[] ReceiveMsg()
        {
            var localIp = IPManager.GetLocalIpv4();
            List<byte> arr = new List<byte>();
            var ClietIPEP = new IPEndPoint(IPAddress.Any, 0);
            var _Remote = (EndPoint)ClietIPEP;
            int index = 100;
            while (index > 0)
            {
                try
                {
                    if (_ClientSocket.Available > 0)
                    {
                        byte[] data = new byte[_ClientSocket.Available];
                        int recv = _ClientSocket.ReceiveFrom(data, ref _Remote);

                        if (recv > 0)
                        {
                            var remoteIp = _Remote.ToString().Split(':')[0];
                            if (remoteIp.Equals(localIp))
                            {
                                continue;
                            }
                            arr.AddRange(data);
                            break;
                        }
                        else
                        {
                            Thread.Sleep(5);
                        }
                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    index--;
                }
            }
            byte[] reBytes = arr.Count == 0 ? null : arr.ToArray();
            return reBytes;
        }
        #endregion

        #region 同步接收数据

        private byte[] ReceiveMsgInFixedTime()
        {
            var localIp = IPManager.GetLocalIpv4();
            List<byte> arr = new List<byte>();
            var ClietIPEP = new IPEndPoint(IPAddress.Any, 0);
            var _Remote = (EndPoint)ClietIPEP;
            int index = 200;
            while (index > 0)
            {
                try
                {
                    if (_ClientSocket.Available > 0)
                    {
                        byte[] data = new byte[_ClientSocket.Available];
                        int recv = _ClientSocket.ReceiveFrom(data, ref _Remote);

                        if (recv > 0)
                        {
                            var remoteIp = _Remote.ToString().Split(':')[0];
                            arr.AddRange(data);
                            continue;
                        }
                        else
                        {
                            Thread.Sleep(10);
                        }
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    index--;
                }
            }
            byte[] reBytes = arr.Count == 0 ? null : arr.ToArray();
            return reBytes;
        }
        #endregion

        #endregion

        #region public methods

        #region 发送数据并关闭当前连接
        /// <summary>
        /// 发送数据并关闭当前连接
        /// </summary>
        /// <param name="sendData"></param>
        /// <param name="ServerIPAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public byte[] SendDataAndShutDownSocket(byte[] sendData)
        {
            if (!OpenSocket()) return null;
            byte[] revData = null;
            if (SendData(sendData))
            {
                revData = ReceiveMsg();
            }
            CloseSocket();
            return revData;
        }

        #endregion

        #region 发送数据并保持当前连接
        /// <summary>
        /// 发送数据并保持当前连接
        /// </summary>
        /// <param name="sendData"></param>
        /// <param name="ServerIPAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public byte[] SendDataAndKeepConnected(byte[] sendData)
        {
            byte[] revData = null;
            if (SendData(sendData))
            {
                revData = ReceiveMsg();
            }
            return revData;
        }
        #endregion

        #region 等待固定时间接受数据
        public byte[] SendDataAndWaitFixedTime(byte[] sendData)
        {
            byte[] revData = null;
            if (SendData(sendData))
            {
                revData = ReceiveMsgInFixedTime();
            }
            return revData;
        }
        #endregion


        #region 发送数据并保持当前连接
        /// <summary>
        /// 发送数据并保持当前连接
        /// </summary>
        /// <param name="sendData"></param>
        /// <param name="ServerIPAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public byte[] SendBroadCastDataAndKeepConnected(byte[] sendData)
        {
            byte[] revData = null;
            if (SendBroadCastData(sendData))
            {
                revData = ReceiveMsg();
            }
            return revData;
        }
        #endregion

        #region 发送数据并保持当前连接
        /// <summary>
        /// 发送数据并保持当前连接
        /// </summary>
        /// <param name="sendData"></param>
        /// <param name="ServerIPAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public byte[] SendBroadCastDataAndCloseSocket(byte[] sendData)
        {
            byte[] revData = null;
            if (SendBroadCastData(sendData))
            {
                revData = ReceiveMsg();
            }
            CloseSocket();
            return revData;
        }
        #endregion

        #region 发送数据并保持当前连接
        /// <summary>
        /// 发送数据并保持当前连接
        /// </summary>
        /// <param name="sendData"></param>
        /// <param name="ServerIPAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public byte[] SendDataAndReceiveNoData(byte[] sendData)
        {
            //if (!OpenSocket()) return null;
            List<byte> list = new List<byte>();

            if (SendData(sendData))
            {
                while (true)
                {
                    byte[] revData = ReceiveMsg();
                    if (revData != null && revData.Length != 0)
                    {
                        list.AddRange(revData);
                        continue;
                    }
                    break;
                }
            }
            return list.ToArray();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~UdpSocketClient() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion

        #endregion
    }
}
