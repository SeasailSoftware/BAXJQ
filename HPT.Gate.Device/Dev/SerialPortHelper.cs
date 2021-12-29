using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HPT.Gate.Device.Dev
{
    public class SerialPortHelper
    {

        #region properites
        public int _CommNo { get; set; }

        public int _BaudRate { get; set; }

        public SerialPort _SPort { get; set; }

        public bool IsOpen { get; set; }

        #endregion

        #region Instance

        private static SerialPortHelper instance;
        private static readonly object lockHelper = new object();

        public static SerialPortHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new SerialPortHelper();
                        }
                    }
                }
                return instance;
            }
        }

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

        #region private Methods

        #region  打开串口

        public void OpenPort()
        {
            if (IsOpen) return;
            if (_SPort == null)
            {
                _SPort = new SerialPort(string.Format("COM{0}", _CommNo), _BaudRate);
            }
            if (_SPort.IsOpen)
            {
                IsOpen = true;
                return;
            }

            try
            {
                _SPort.Close();
                _SPort.Open();
                IsOpen = true;
            }
            catch (Exception ex)
            {
                OnMessage(string.Format("打开串口失败:{0}", ex.Message));
                _SPort = null;
                IsOpen = false;
            }

        }
        #endregion

        #region 关闭串口

        public void ClosePort()
        {
            if (!IsOpen) return;
            if (_SPort == null)
            {
                IsOpen = false;
                return;
            }
            if (!_SPort.IsOpen)
            {
                IsOpen = false;
                return;
            }
            try
            {
                _SPort.Close();
                IsOpen = false;
            }
            catch (Exception ex)
            {
                OnMessage(string.Format("关闭串口失败:{0}", ex.Message));
            }
        }
        #endregion




        #endregion

        #region Public Methods

        #region 发送数据

        public bool SendData(byte[] data)
        {
            if (!IsOpen || _SPort == null || data == null) return false;
            try
            {
                _SPort.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception ex)
            {
                OnMessage(string.Format("发送数据失败:{0}", ex.Message));
                return false;
            }

        }
        #endregion

        #region 接收数据


        /// <summary>/// 向串口发送数据，读取返回数据/// </summary>
        /// /// <param name="sendData">发送的数据</param>
        /// /// <returns>返回的数据
        ///</returns>
        private byte[] ReadPort(byte[] sendData)
        {
            SerialPort serialPort = null;
            if (serialPort == null)
            {
                serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
                serialPort.ReadBufferSize = 1024;
                serialPort.WriteBufferSize = 1024;
            }
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            //发送数据    
            serialPort.Write(sendData, 0, sendData.Length);
            //读取返回数据    
            while (serialPort.BytesToRead == 0)
            {
                Thread.Sleep(1);
            }
            Thread.Sleep(50);
            //50毫秒内数据接收完毕，可根据实际情况调整    
            byte[] recData = new byte[serialPort.BytesToRead];
            serialPort.Read(recData, 0, recData.Length); return recData;
        }

        public byte[] ReceiveData()
        {

            ///如果串口已经关闭，则不执行
            if (!_SPort.IsOpen)
                return null;
            try
            {
                int index = 1000;
                while (_SPort.BytesToRead == 0 && index > 0)
                {
                    Thread.Sleep(1);
                    index--;
                }
                Thread.Sleep(50);
                int n = _SPort.BytesToRead;
                if (n == 0)
                    return null;
                byte[] buffer = new byte[n];
                _SPort.Read(buffer, 0, buffer.Length);
                return buffer;

            }
            catch (Exception ex)
            {
                OnMessage(string.Format("接收数据失败:{0}", ex.Message));
                return null;
            }
        }
        #endregion

        #region 同步发送与接收

        public byte[] SendDataSynchronous(byte[] sendData)
        {
            byte[] revData = null;
            OpenPort();
            if (SendData(sendData))
            {
                revData = ReceiveData();
            }
            return revData;
        }


        #endregion


        #endregion


    }
}
