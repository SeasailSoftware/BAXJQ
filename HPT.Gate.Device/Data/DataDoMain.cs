using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device
{
    public class DataDoMain
    {
        public DataDoMain()
        {
            this.DoMain = new byte[48];
            this.Port = new byte[6];
        }
        /// <summary>
        /// 域名
        /// </summary>
        private byte[] _DoMain;

        public byte[] DoMain
        {
            get { return _DoMain; }
            set { _DoMain = value; }
        }
        /// <summary>
        /// 域名
        /// </summary>
        public string SDoMain
        {
            get { return ArrayHelper.ArrayToGB2312(this.DoMain); }
        }
        /// <summary>
        /// 端口号
        /// </summary>
        private byte[] _Port;

        public byte[] Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        public int IPort
        {
            get { return Convert.ToInt32(ArrayHelper.ArrayToGB2312(Port)); }
        }

        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            byte[] arr = ArrayHelper.AddBytes(this.DoMain, this.Port);
            return arr;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data"></param>
        public void Init(byte[] data)
        {
            int dataLength = this.DoMain.Length + this.Port.Length;
            if (data.Length == dataLength)
            {
                int index = 0;
                Array.Copy(data, index, DoMain, 0, DoMain.Length);
                index += DoMain.Length;
                Array.Copy(data, index, Port, 0, Port.Length);
                index += Port.Length;
            }
        }
    }
}
