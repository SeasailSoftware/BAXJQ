using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HPT.Joey.Lib.Communication
{
    /// <summary>
    /// Socket数据收发事件参数
    /// </summary>
    public class SocketRsDataEventArgs : System.EventArgs
    {
        #region property

        private Socket client;

        /// <summary>
        /// Socket连接
        /// </summary>
        public Socket Client
        {
            get { return client; }
            set
            {
                client = value;
                ClientIp = GetIp(client);
                ClientPort = GetPort(client).ToString();
                ClientIpFull = GetIpFull(client);
            }
        }

        /// <summary>
        /// 通道Mac地址
        /// </summary>
        public string GateMac { get; set; }

        /// <summary>
        /// Socket连接的IP地址
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// Socket连接的端口号
        /// </summary>
        public string ClientPort { get; set; }

        /// <summary>
        /// Socket连接的IP地址(含端口号)
        /// </summary>
        public string ClientIpFull { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 连接状态(1接入2断开)
        /// </summary>
        public int ConnectStatusId { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 返回空格分割的十六进制字符串
        /// </summary>
        public string DateHexString
        {
            get { return ConvertToHexString(Data); }
        }

        #endregion

        #region ctor

        //public SocketRsDataEventArgs()
        //{
        //    //
        //}

        #endregion

        #region public method

        /// <summary>
        /// 将Byte数组转换成空格分割的十六进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string ConvertToHexString(IEnumerable<byte> data)
        {
            if (data == null)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            foreach (var b in data)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public string GetIp(Socket socket)
        {
            try
            {
                return ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取端口号
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public int GetPort(Socket socket)
        {
            try
            {
                return ((IPEndPoint)socket.RemoteEndPoint).Port;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取IP地址(含端口号)
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public string GetIpFull(Socket socket)
        {
            try
            {
                return socket.RemoteEndPoint.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}