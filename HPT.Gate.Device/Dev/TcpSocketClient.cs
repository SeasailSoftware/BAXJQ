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
    public class TcpSocketClient
    {
        #region Var

        public Socket _ClientSocket = null;

        #endregion


        #region Ctor
        public TcpSocketClient(string remoteIp, int _port)
        {
            this._RemoteAddress = IPAddress.Parse(remoteIp);
            this._RemotePort = _port;
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
                _ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //设置三秒钟超时
                _ClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
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

        #region 连接服务器
        public bool ConnectToServer()
        {
            if (_ClientSocket.Connected) return true;
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(_RemoteAddress, _RemotePort);
                _ClientSocket.Connect(remoteEP);
                IsConnected = true;
                OnMessage("【本地客户端】:连接银行服务器[" + remoteEP.ToString() + "]成功!");
                return _ClientSocket.Connected;
            }
            catch (Exception ex)
            {
                IsConnected = false;
                OnMessage("【本地客户端】:连接银行服务器失败!" + ex.Message);
                return false;
            }

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
            try
            {
                _ClientSocket.Send(sendData, sendData.Length, 0); //发送信息
                OnMessage(string.Format("本机发送:{0}", ArrayHelper.ArrayToHex(sendData)));
                return true;
            }
            catch (Exception ex)
            {
                OnMessage("【本地客户端】:发送数据失败:" + ex.Message);
                return false;
            }
        }
        #endregion

        #region 同步接收数据

        private byte[] ReceiveMsg()
        {
            //接受从服务器返回的信息
            byte[] recvBytes = new byte[2048];
            int index;
            try
            {
                EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                string remote = ((EndPoint)_ClientSocket.RemoteEndPoint).AddressFamily.ToString();
                index = this._ClientSocket.ReceiveFrom(recvBytes, ref remoteEndPoint);
                //index = _ClientSocket.Receive(recvBytes, recvBytes.Length, 0);    //从服务器端接受返回信息
                recvBytes = ArrayHelper.SubByte(recvBytes, 0, index);
                OnMessage(string.Format("[{0}] {1}", remote, ArrayHelper.ArrayToHex(recvBytes)));
            }
            catch (Exception ex)
            {
                OnMessage(string.Format("接收数据失败:{0}", ex.Message));
                recvBytes = null;
            }
            return recvBytes;
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
            if (!ConnectToServer()) return null;
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
            if (!OpenSocket()) return null;
            if (!ConnectToServer()) return null;
            byte[] revData = null;
            if (SendData(sendData))
            {
                revData = ReceiveMsg();
            }
            return revData;
        }
        #endregion

        #region 断开连接
        public void DisConnected()
        {
            if (!IsConnected) return;
            try
            {
                _ClientSocket.Shutdown(SocketShutdown.Both);
                IsConnected = false;
            }
            catch
            {

            }
        }
        #endregion

        #endregion

    }
}
