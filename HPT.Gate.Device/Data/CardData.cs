using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;

namespace HPT.Gate.Device.Data
{
    public class CardData
    {

        #region ctor

        public CardData(bool antiSubmarineWarfare, bool limitedTimes_Enabled)
        {
            this.RecId = new byte[1];
            this.IOFlag = antiSubmarineWarfare ? new byte[] { 0x04 } : new byte[] { 0xFF };
            this.FirstRecDateTime = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            this.LastRecDateTime = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            if (limitedTimes_Enabled)
            {
                this.CountOfIn = new byte[] { 0x00 };
                this.CountOfOut = new byte[] { 0x00 };
            }
            else
            {
                this.CountOfIn = new byte[] { 0xFF };
                this.CountOfOut = new byte[] { 0xFF };
            }
            this.Cumulative = new byte[] { 0xFF };
            List<byte> list = new List<byte>();
            list.AddRange(this.RecId);
            list.AddRange(this.IOFlag);
            list.AddRange(FirstRecDateTime);
            list.AddRange(LastRecDateTime);
            list.AddRange(CountOfIn);
            list.AddRange(CountOfOut);
            list.AddRange(Cumulative);
            byte[] arr = list.ToArray();
            ///转换成UINT32
            UInt32[] uint32 = CRC32Helper.ByteToUnit32(arr);
            UInt32 crc32 = CRC32Helper.Cal_CRC(uint32);
            byte[] crc = BitConverter.GetBytes(crc32);
            this.CheckCode = new byte[] { crc[0] };
        }

        #endregion

        #region Var

        /// <summary>
        /// 流水记录
        /// </summary>
        private byte[] recId;

        public byte[] RecId
        {
            get
            {
                return recId;
            }

            set
            {
                recId = value;
            }
        }

        /// <summary>
        /// 进出标志
        /// </summary>
        private byte[] iOFlag;

        public byte[] IOFlag
        {
            get
            {
                return iOFlag;
            }

            set
            {
                iOFlag = value;
            }
        }

        /// <summary>
        /// 第一次刷卡时间
        /// </summary>
        private byte[] firstRecDateTime;

        public byte[] FirstRecDateTime
        {
            get
            {
                return firstRecDateTime;
            }

            set
            {
                firstRecDateTime = value;
            }
        }

        /// <summary>
        /// 上次刷卡时间
        /// </summary>
        private byte[] lastRecDateTime;

        public byte[] LastRecDateTime
        {
            get
            {
                return lastRecDateTime;
            }

            set
            {
                lastRecDateTime = value;
            }
        }

        /// <summary>
        /// 入口刷卡次数
        /// </summary>
        private byte[] countOfIn;

        public byte[] CountOfIn
        {
            get
            {
                return countOfIn;
            }

            set
            {
                countOfIn = value;
            }
        }

        /// <summary>
        /// 出口刷卡次数
        /// </summary>
        private byte[] countOfOut;

        public byte[] CountOfOut
        {
            get
            {
                return countOfOut;
            }

            set
            {
                countOfOut = value;
            }
        }

        /// <summary>
        /// 累计分钟数
        /// </summary>
        private byte[] cumulative;

        public byte[] Cumulative
        {
            get
            {
                return cumulative;
            }

            set
            {
                cumulative = value;
            }
        }


        /// <summary>
        /// 验证码
        /// </summary>
        private byte[] checkCode;

        public byte[] CheckCode
        {
            get
            {
                return checkCode;
            }

            set
            {
                checkCode = value;
            }
        }



        #endregion

        #region  转化为数组

        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            List<byte> list = new List<byte>();
            list.AddRange(RecId);
            list.AddRange(IOFlag);
            list.AddRange(FirstRecDateTime);
            list.AddRange(LastRecDateTime);
            list.AddRange(CountOfIn);
            list.AddRange(CountOfOut);
            list.AddRange(Cumulative);
            list.AddRange(CheckCode);
            return list.ToArray();
        }
        #endregion

        #region 数组转实例

        public void init(byte[] array)
        {
            if (array.Length != this.ToArray().Length) return;
            int index = 0;
            this.RecId = ArrayHelper.SubByte(array, index, RecId.Length);
            index += recId.Length;
            this.IOFlag = ArrayHelper.SubByte(array, index, IOFlag.Length);
            index += IOFlag.Length;
            this.FirstRecDateTime = ArrayHelper.SubByte(array, index, FirstRecDateTime.Length);
            index += FirstRecDateTime.Length;
            this.LastRecDateTime = ArrayHelper.SubByte(array, index, LastRecDateTime.Length);
            index += LastRecDateTime.Length;
            this.CountOfIn = ArrayHelper.SubByte(array, index, CountOfIn.Length);
            index += CountOfIn.Length;
            this.CountOfOut = ArrayHelper.SubByte(array, index, CountOfOut.Length);
            index += CountOfOut.Length;
            this.Cumulative = ArrayHelper.SubByte(array, index, Cumulative.Length);
            index += Cumulative.Length;
            this.CheckCode = ArrayHelper.SubByte(array, index, CheckCode.Length);
            index += CheckCode.Length;
        }

        #endregion


    }
}
