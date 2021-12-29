using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DataUpdatePacket
    {
        #region 构造函数
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DataUpdatePacket()
        {
            this.Total = new byte[1];
            this.UpdateIndex = new byte[1];
            this.UpdateArray = new byte[1024];
        }

        public DataUpdatePacket(int total, int index, byte[] arr)
        {
            this.Total = ArrayHelper.IntToBytes(total, 1);
            this.UpdateIndex = ArrayHelper.IntToBytes(index, 1);
            this.UpdateArray = arr;
        }

        #endregion
        #region Var
        /// <summary>
        /// 更新包总包数
        /// </summary>
        private byte[] _Total;

        public byte[] Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        /// <summary>
        /// 更新包数组序号
        /// </summary>
        private byte[] _UpdateIndex;

        public byte[] UpdateIndex
        {
            get { return _UpdateIndex; }
            set { _UpdateIndex = value; }
        }

        public int IUpdateIndex { get { return ArrayHelper.bytesToInt(UpdateIndex); } }
        /// <summary>
        /// 更新包数组
        /// </summary>
        private byte[] _UpdateArray;

        public byte[] UpdateArray
        {
            get { return _UpdateArray; }
            set { _UpdateArray = value; }
        }
        #endregion
        #region  序列化

        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            byte[] newByte = null;
            newByte = ArrayHelper.AddBytes(newByte, Total);
            newByte = ArrayHelper.AddBytes(newByte, UpdateIndex);
            newByte = ArrayHelper.AddBytes(newByte, UpdateArray);
            return newByte;
        }

        #endregion

    }
}
