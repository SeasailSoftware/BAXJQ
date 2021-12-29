using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DataBmp
    {
        #region 构造函数
        public DataBmp()
        {
            this.BmpType = new byte[1];
            this.Index = new byte[1];
            this.BmpName = new byte[4];
            this.BmpBytes = new byte[1024];
        }

        #endregion

        #region Var
        /// <summary>
        /// 图片的类型，0x03代表入口通道，0x04代表出口通道
        /// </summary>
        private byte[] _BmpType;

        public byte[] BmpType
        {
            get { return _BmpType; }
            set { _BmpType = value; }
        }

        /// <summary>
        /// 当前图片数组的下标（0~199）
        /// </summary>
        private byte[] _Index;

        public byte[] Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        /// <summary>
        /// 图片名字，即4字节卡序列号，0xFFFFFFFF为背景图片
        /// </summary>
        private byte[] _BmpName;

        public byte[] BmpName
        {
            get { return _BmpName; }
            set { _BmpName = value; }
        }

        /// <summary>
        /// 图片数组
        /// </summary>
        private byte[] _BmpBytes;

        public byte[] BmpBytes
        {
            get { return _BmpBytes; }
            set { _BmpBytes = value; }
        }
        #endregion
        #region 序列化
        public byte[] ToArray()
        {
            byte[] newByte = BmpType;
            newByte = ArrayHelper.AddBytes(newByte, Index);
            newByte = ArrayHelper.AddBytes(newByte, BmpName);
            newByte = ArrayHelper.AddBytes(newByte, BmpBytes);
            return newByte;
        }

        #endregion

    }
}
