using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Timers;

namespace hpt.gate.CardReader
{
    public class IDSerialReader
    {
        public static SerialPort _Port = new SerialPort();
        public static StringBuilder builder = new StringBuilder();
        public static System.Timers.Timer timer = null;
        public static Thread _ClearThread = null;

        public static bool InitFlag { get; set; }
        public static bool InitIDSerialReader(string portName)
        {
            if (InitFlag) return true;
            //根据当前串口对象，来判断操作  
            if (_Port.IsOpen)
            {
                _Port.DataReceived -= _Port_DataReceived;
                //打开时点击，则关闭串口  
                _Port.Close();
            }
            //关闭时点击，则设置好端口，波特率后打开  
            _Port.PortName = portName;
            _Port.BaudRate = 9600;
            Console.WriteLine("两用读卡器初始化成功!");
            try
            {
                _Port.Open();
                _Port.DataReceived += _Port_DataReceived;
                return true;
            }
            catch (Exception ex)
            {
                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。  
                _Port = new SerialPort();
                _Port.DataReceived += _Port_DataReceived;
                Console.Write("初始化读卡器发生异常:" + ex.Message);
                //现实异常信息给客户。  
                return false;
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void _Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(30);
            if (!_Port.IsOpen) return;
            int n = _Port.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致  
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据  
            _Port.Read(buf, 0, n);//读取缓冲数据  
            if (buf.Length <= 4)
            {
                return;
            }
            builder = new StringBuilder();//清除字符串构造器的内容  
            //依次的拼接出16进制字符串  
            foreach (byte b in buf)
            {
                builder.Append(b.ToString("X2") + " ");
            }

            if (_ClearThread != null)
            {
                _ClearThread.Abort();
            }
            _ClearThread = new Thread(() => { ClearBuffer(); });
            _ClearThread.Start();

        }

        /// <summary>
        /// 清除buffer内容
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ev"></param>
        private static void ClearBuffer()
        {
            Thread.Sleep(4000);
            builder.Clear();
        }

        /// <summary>
        /// 读取身份证序列号
        /// </summary>
        /// <returns></returns>
        public static string ReadIDSerialNo()
        {
            return Reverse(builder.ToString().Replace(" ", ""));
        }

        public static string Reverse(string str)
        {
            string temp = string.Empty;
            if (str.Length == 10)
            {
                temp += str.Substring(6, 2);
                temp += str.Substring(4, 2);
                temp += str.Substring(2, 2);
                temp += str.Substring(0, 2);
                temp += str.Substring(8, 2);
            }
            if (str.Length == 18)
            {
                temp += str.Substring(14, 2);
                temp += str.Substring(12, 2);
                temp += str.Substring(10, 2);
                temp += str.Substring(8, 2);
                temp += str.Substring(6, 2);
                temp += str.Substring(4, 2);
                temp += str.Substring(2, 2);
                temp += str.Substring(0, 2);
            }
            return temp;
        }

        public static void Close()
        {
            try
            {
                _Port.Close();
                _Port.DataReceived -= _Port_DataReceived;
            }
            catch
            {

            }
        }
    }
}
