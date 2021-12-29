using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class DataDuration
    {

        #region 构造函数

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DataDuration()
        {
            this.Time1 = new byte[2];
            this.Time2 = new byte[2];
        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        public DataDuration(int groupId, int time1, int time2)
        {
            this.GroupId = ArrayHelper.IntToBytes(groupId, 1);
            this.Time1 = ArrayHelper.IntToBytes(time1, 2);
            this.Time2 = ArrayHelper.IntToBytes(time2, 2);
        }

        #endregion

        #region Var

        /// <summary>
        /// 组号
        /// </summary>
        private byte[] groupId;

        public byte[] GroupId
        {
            get
            {
                return groupId;
            }

            set
            {
                groupId = value;
            }
        }

        /// <summary>
        /// 上午就餐可持续时间
        /// </summary>
        private byte[] time1;

        public byte[] Time1
        {
            get
            {
                return time1;
            }

            set
            {
                time1 = value;
            }
        }

        /// <summary>
        /// 下午就餐可持续时间
        /// </summary>
        private byte[] time2;

        public
            byte[] Time2
        {
            get
            {
                return time2;
            }

            set
            {
                time2 = value;
            }
        }

        /// <summary>
        /// 接收标志
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

        #region  序列化

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            List<byte> list = new List<byte>();
            list.AddRange(this.GroupId);
            list.AddRange(this.Time1);
            list.AddRange(this.Time2);
            return list.ToArray();
        }

        #endregion
    }
}
