using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DataTimeGroup
    {

        #region 构造函数
        public DataTimeGroup()
        {
            this.BeginTime = new byte[2] { 0xFF, 0xFF };
            this.EndTime = new byte[2] { 0xFF, 0xFF };
        }

        public DataTimeGroup(string beginTme, string endTime)
        {
            if (beginTme.Equals("00:00") || beginTme.Equals("00:00:00"))
            {
                if (endTime.Equals("00:00") || endTime.Equals("00:00:00"))
                {
                    this.BeginTime = new byte[2] { 0xFF, 0xFF };
                    this.EndTime = new byte[2] { 0xFF, 0xFF };
                    return;
                }
            }
            this.BeginTime = ArrayHelper.DateTimeToArray1(beginTme);
            this.EndTime = ArrayHelper.DateTimeToArray1(endTime);
        }

        public DataTimeGroup(string beginTme, string endTime, bool convertFlag)
        {
            if (beginTme.Equals("00:00") || beginTme.Equals("00:00:00"))
            {
                if (endTime.Equals("00:00") || endTime.Equals("00:00:00"))
                {
                    this.BeginTime = new byte[2] { 0xFF, 0xFF };
                    this.EndTime = new byte[2] { 0xFF, 0xFF };
                    return;
                }
            }
            this.BeginTime = ArrayHelper.DateTimeToArray(beginTme);
            this.EndTime = ArrayHelper.DateTimeToArray(endTime);
        }
        #endregion

        #region Var
        /// <summary>
        /// 时间段的开始时间
        /// </summary>
        private byte[] _BeginTime;

        public byte[] BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }
        /// <summary>
        /// 时间段的开始时间
        /// </summary>
        public string SBeginTime
        {
            get { return ArrayHelper.ArrayToTime(BeginTime); }
        }
        /// <summary>
        /// 时间段的结束时间
        /// </summary>
        private byte[] _EndTime;

        public byte[] EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        /// <summary>
        /// 时间段的结束时间
        /// </summary>
        public string SEndTime
        {
            get { return ArrayHelper.ArrayToTime(EndTime); }
        }

        #endregion
        #region 序列化

        public byte[] ToArray()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(BeginTime);
            bytes.AddRange(EndTime);
            return bytes.ToArray();
        }

        #endregion
    }
}
