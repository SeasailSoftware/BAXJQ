
using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class DataCardPass
    {
        #region 构造函数

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DataCardPass()
        {

        }

        #endregion

        #region var

        public bool WriteCard_Enable { get; set; }

        public int SectorNo { get; set; }

        public string CardPassword { get; set; }

        public bool LimitedTimes_Enabled { get; set; }

        public int LimitedTimesOfIn { get; set; }

        public int LimitedTimesOfOut { get; set; }

        public int CardInterval { get; set; }

        public bool LimitedMinutes_Enable { get; set; }

        public int LimitedMinutes { get; set; }

        public bool AntiSubmarineWarfare { get; set; }

        #endregion


        #region 转化为数组

        public byte[] ToArray()
        {

            List<byte> list = new List<byte>();
            byte enable = WriteCard_Enable ? (byte)0x01 : (byte)0x00;
            list.Add(enable);
            list.Add((byte)SectorNo);
            list.AddRange(ArrayHelper.HexToArray(CardPassword, 6));
            if (LimitedTimes_Enabled)
            {
                list.Add((byte)LimitedTimesOfIn);
                list.Add((byte)LimitedTimesOfOut);
            }
            else
            {
                list.Add(0xFF);
                list.Add(0xFF);
            }
            list.AddRange(ArrayHelper.IntToBytes(CardInterval, 2));
            if (LimitedMinutes_Enable)
            {
                list.AddRange(ArrayHelper.IntToBytes(LimitedMinutes, 2));
            }
            else
            {
                list.AddRange(new byte[] { 0xFF, 0xFF });
            }
            return list.ToArray();
        }
        #endregion

        #region 接收结果

        /// <summary>
        /// 接收结果
        /// </summary>
        private bool updateFlag;

        public bool UpdateFlag
        {
            get
            {
                return updateFlag;
            }

            set
            {
                updateFlag = value;
            }
        }
        /// <summary>
        /// 返回结果
        /// </summary>
        private bool result;

        public bool Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }





        #endregion
    }
}
