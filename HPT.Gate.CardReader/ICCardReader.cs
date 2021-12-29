using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace hpt.gate.CardReader
{
    public class ICCardReader
    {
        #region methods
        /// <summary>
        /// 读取卡序列号
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="port"></param>
        /// <param name="bautRate"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        [DllImport(@"MZ_Card.dll", EntryPoint = "_ReadCardSerNum@16", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private extern static bool F_ReadCardSerNum(bool flag, int port, int bautRate, ref byte temp);

        /// <summary>
        /// 响蜂鸣器
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="port"></param>
        /// <param name="bautRate"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        [DllImport(@"MZ_Card.dll", EntryPoint = "_SendBeep@16")]
        private extern static bool F_SendBeep(bool flag, int port, int bautRate, int times);

        /// <summary>
        /// 将卡原密码读入动态库
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="port"></param>
        /// <param name="bautRate"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [DllImport(@"MZ_Card.dll", EntryPoint = "_SetCardPassWord@24")]
        private extern static bool SetCardPassword(bool flag, int port, int bautRate, int startAreaNo, int AreaCount, string password);

        /// <summary>
        /// 将卡原密码读入动态库
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="port"></param>
        /// <param name="bautRate"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [DllImport(@"MZ_Card.dll", EntryPoint = "_SetDevPassWord@16")]
        private extern static bool LoadDevOldPassword(bool flag, int port, int bautRate, string password);

        /// <summary>
        /// 读取块信息
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="port"></param>
        /// <param name="bautRate"></param>
        /// <param name="startBlockNo"></param>
        /// <param name="blockCount"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [DllImport(@"MZ_Card.dll", EntryPoint = "_ReadCardBlock@24", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private extern static bool ReadCardBlock(bool flag, int port, int bautRate, int startBlockNo, int blockCount, ref byte content);

        [DllImport(@"MZ_Card.dll", EntryPoint = "_WriteCardBlock@24")]
        private extern static bool WriteCardBlock(bool flag, int port, int bautRate, int startBlockNo, int blockCount, ref byte content);

        #endregion

        #region var

        /// <summary>
        /// 串口号
        /// </summary>
        public static int _ComNo;
        /// <summary>
        /// 波特率
        /// </summary>
        public static int BaudRate = 38400;

        /// <summary>
        /// 活跃标志
        /// </summary>
        public static bool Active = false;

        #endregion


        #region 获取当前使用的串口号
        public static string GetCurrentPort()
        {
            string port = string.Empty;
            foreach (string s in SerialPort.GetPortNames())
            {
                int i = Convert.ToInt32(s.Replace("COM", "").Trim());
                if (F_SendBeep(true, i, 38400, 1))
                {
                    port = s;
                    break;
                }
            }
            return port;
        }
        #endregion


        #region Initialization

        public static void InitCardReader()
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                int i = Convert.ToInt32(s.Replace("COM", "").Trim());
                if (F_SendBeep(true, i, 38400, 1))
                {
                    _ComNo = i;
                    Active = true;
                    break;
                }
            }
        }

        public static void InitCardReader(string port)
        {
            int temp = Convert.ToInt32(port.Replace("COM", "").Trim());
            _ComNo = temp;
        }

        #endregion


        #region 读取序列号

        /// <summary>
        /// 自动读取序列号
        /// </summary>
        /// <returns></returns>
        public static string AutoReadCardNo()
        {
            if (!Active)
            {
                InitCardReader();
            }
            byte[] a = new byte[20];
            string cardNo = string.Empty;
            try
            {
                if (F_ReadCardSerNum(true, _ComNo, 38400, ref a[0]))
                {
                    cardNo = new UTF8Encoding().GetString(a);
                    cardNo = ConvertCardNo(cardNo);
                }
                else
                {
                    //MessageBox.Show("无卡!", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cardNo = string.Empty;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("在读取卡号过程中发生错误，错误信息:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cardNo = string.Empty;
            }

            return cardNo;
        }

        public static string ReadCardNo()
        {
            if (!Active)
            {
                InitCardReader();
            }
            byte[] a = new byte[20];
            string cardNo = string.Empty;
            try
            {
                if (F_ReadCardSerNum(true, _ComNo, 38400, ref a[0]))
                {
                    F_SendBeep(true, _ComNo, 38400, 1);
                    cardNo = new UTF8Encoding().GetString(a);
                    cardNo = ConvertCardNo(cardNo);
                }
                else
                {
                    MessageBox.Show("无卡!", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cardNo = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("在读取卡号过程中发生错误，错误信息:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cardNo = string.Empty;
            }

            return cardNo;
        }

        /// <summary>
        /// 读取序列号
        /// </summary>
        /// <param name="serialNo"></param>
        /// <returns></returns>
        public static string ReadCardNo(int serialNo)
        {
            byte[] a = new byte[20];
            string cardNo = string.Empty;
            try
            {
                if (F_SendBeep(true, serialNo, 38400, 0))
                {
                    Thread.Sleep(200);
                    if (F_ReadCardSerNum(true, serialNo, 38400, ref a[0]))
                    {
                        cardNo = new UTF8Encoding().GetString(a);
                        cardNo = ConvertCardNo(cardNo);
                    }
                    else
                    {
                        MessageBox.Show("无卡!", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cardNo = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("读卡失败!请检查串口是否正确，读卡器是否已经连接等", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cardNo = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("在读取卡号过程中发生错误，错误信息:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cardNo = string.Empty;
            }

            return cardNo;
        }

        /// <summary>
        /// 转换卡号
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static string ConvertCardNo(string cardNo)
        {
            string newCardNo = string.Empty;
            if (cardNo.Length < 8)
            {
                return string.Empty;
            }
            if (cardNo.Length > 8)
            {
                cardNo = cardNo.Substring(0, 8);
            }
            for (int i = 0; i < 4; i++)
            {
                newCardNo += cardNo.Substring((4 - i - 1) * 2, 2);
            }
            return newCardNo;
        }
        #endregion

        #region IC卡加密

        /// <summary>
        /// IC卡加密
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static bool SetCardPassword(string oldPassword, string newPassword)
        {
            if (Active)
            {
                if (LoadDevOldPassword(true, _ComNo, BaudRate, oldPassword))
                {
                    if (SetCardPassword(true, _ComNo, BaudRate, 1, 15, newPassword))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region 读取块信息
        /// <summary>
        /// 读取块信息
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public static string ReadCardBlock(int AreaId, string cardPass)
        {
            if (LoadDevOldPassword(true, _ComNo, BaudRate, cardPass))
            {
                byte[] arr = new byte[32];
                if (ReadCardBlock(true, _ComNo, BaudRate, AreaId * 4, 2, ref arr[0]))
                {
                    return ArrayToHex(arr);
                }
            }
            return string.Empty;
        }

        private static string ArrayToHex(byte[] arr)
        {
            StringBuilder buffer = new StringBuilder();
            foreach (byte b in arr)
            {
                buffer.Append(b.ToString("X2"));
            }
            return buffer.ToString();
        }

        #endregion

        #region 写入块信息

        /// <summary>
        /// 写入块信息
        /// </summary>
        /// <param name="AreaId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteCardBlock(int AreaId, string content, string cardPass)
        {
            if (LoadDevOldPassword(true, _ComNo, BaudRate, cardPass))
            {
                byte[] arr = HexToArray(content, 32);
                if (WriteCardBlock(true, _ComNo, BaudRate, AreaId * 4, 2, ref arr[0]))
                {
                    return true;
                }
            }
            return false;
        }

        private static byte[] HexToArray(string content, int v)
        {
            //todo
            return null;
        }

        #endregion
    }
}
