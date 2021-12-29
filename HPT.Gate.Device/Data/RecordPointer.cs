using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class RecordPointer
    {

        #region ctor

        public RecordPointer(int interval)
        {
            this.BeginIndex = 0;
            this.TotalIndex = 0;
            this.Interval = interval;
        }
        #endregion

        #region properity

        /// <summary>
        /// 当前指针
        /// </summary>
        public int BeginIndex { get; set; }

        /// <summary>
        /// 下一条指针
        /// </summary>
        public int NextIndex { get { return BeginIndex + Interval; } }

        public int Interval { get; set; }
        /// <summary>
        /// 总指针
        /// </summary>
        public int TotalIndex { get; set; }


        #endregion

        #region 转化为数组
        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <returns></returns>
        internal byte[] ToArray()
        {
            List<byte> list = new List<byte>();
            byte[] arrBegin;
            byte[] arrEnd;
            if (BeginIndex == 0 && TotalIndex == 0)
            {
                arrBegin = ArrayHelper.IntToBytes(BeginIndex, 4);
                arrEnd = ArrayHelper.IntToBytes(TotalIndex, 4);
            }
            else
            {
                arrBegin = ArrayHelper.IntToBytes(BeginIndex, 4);
                arrEnd = ArrayHelper.IntToBytes(NextIndex, 4);
            }
            list.AddRange(arrBegin);
            list.AddRange(arrEnd);
            return list.ToArray();
        }

        #endregion

    }
}
