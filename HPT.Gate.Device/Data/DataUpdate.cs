using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class DataUpdate
    {
        #region 构造函数
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DataUpdate()
        {
            this.Total = new byte[1];
            this.UpdateIndex = new byte[1];
            this.UpdateArray = new byte[1024];
        }

        public DataUpdate(int total, int index, byte[] arr, byte[] crc32)
        {
            this.Total = ArrayHelper.IntToBytes(total, 1);
            this.UpdateIndex = ArrayHelper.IntToBytes(index, 1);
            this.UpdateArray = arr;
            this.LastCRC = crc32;
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

        public byte[] LastCRC { get; set; }
        public byte[] CRC
        {
            get
            {
                byte[] thisCRC = GetCRC32(this.UpdateArray);
                if (LastCRC == null)
                    return thisCRC;
                byte[] arr = ArrayHelper.AddBytes(this.LastCRC, thisCRC);
                return GetCRC32(arr);
            }
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
            newByte = ArrayHelper.AddBytes(newByte, CRC);
            return newByte;
        }

        #endregion

        #region 获取CRC32校验码
        private byte[] GetCRC32(byte[] array)
        {
            ///转换成UINT32
            UInt32[] uint32 = CRC32Helper.ByteToUnit32(array);
            UInt32 crc32 = CRC32Helper.Cal_CRC(uint32);
            return BitConverter.GetBytes(crc32);
        }
        #endregion

    }
}
