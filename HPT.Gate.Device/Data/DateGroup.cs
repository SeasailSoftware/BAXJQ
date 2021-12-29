using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DateGroup
    {

        #region 构造函数
        /// <summary>
        /// 初始化为FF
        /// </summary>
        public DateGroup()
        {
            this.BeginDate = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            this.EndDate = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
        }
        /// <summary>
        /// 初始化为FF
        /// </summary>
        public DateGroup(string beginDate, string endDate)
        {
            if (beginDate.Equals("FFFFFFFF"))
            {
                this.BeginDate = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            }
            else
            {
                this.BeginDate = ArrayHelper.DateToArray(Convert.ToDateTime(beginDate));
            }

            if (endDate.Equals("FFFFFFFF"))
            {
                this.EndDate = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            }
            else
            {
                this.EndDate = ArrayHelper.DateToArray(Convert.ToDateTime(endDate));
            }


        }

        #endregion
        #region Var
        /// <summary>
        /// 日期段的开始日期
        /// </summary>
        private byte[] _BeginDate;

        public byte[] BeginDate
        {
            get { return _BeginDate; }
            set { _BeginDate = value; }
        }
        /// <summary>
        /// 日期段的结束日期
        /// </summary>
        private byte[] _EndDate;

        public byte[] EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        #endregion
        #region 序列化
        public byte[] ToArray()
        {
            return ArrayHelper.AddBytes(BeginDate, EndDate);

        }

        #endregion

    }
}
