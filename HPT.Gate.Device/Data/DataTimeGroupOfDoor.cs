using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DataTimeGroupOfDoor
    {
        #region 构造函数

        /// <summary>
        /// 初始化时间组
        /// </summary>
        public DataTimeGroupOfDoor()
        {
            this.GroupNo = new byte[1] { 0x00 };
            this.TimeGroupList = new List<List<DataTimeGroup>>();
            for (int i = 0; i < 7; i++)
            {
                List<DataTimeGroup> list = new List<DataTimeGroup>();
                /*for (int j = 0; j < 4;j++ )
                {
                    TimeGroup timeGroup = new TimeGroup();
                    list.Add(timeGroup);
                }*/
                TimeGroupList.Add(list);
            }
        }
        #endregion

        #region Var
        /// <summary>
        /// 门禁时间组号
        /// </summary>
        private byte[] _GroupNo;

        public byte[] GroupNo
        {
            get { return _GroupNo; }
            set { _GroupNo = value; }
        }
        /// <summary>
        /// 门禁时间组号
        /// </summary>
        public int IGroupNo
        {
            get { return ArrayHelper.bytesToInt(this.GroupNo); }
        }
        /// <summary>
        /// 时间段列表
        /// </summary>
        private List<List<DataTimeGroup>> _TimeGroupList;

        public List<List<DataTimeGroup>> TimeGroupList
        {
            get { return _TimeGroupList; }
            set { _TimeGroupList = value; }
        }

        /// <summary>
        /// 同步星期时间组是否成功的标志
        /// </summary>
        public bool UpdateFlag { get; internal set; }
        #endregion
        #region 序列化

        /// <summary>
        /// 将DataTimeGroupOfDoor转化为数据
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            List<byte> byteList = new List<byte>();
            byteList.AddRange(GroupNo);
            for (int i = 0; i < TimeGroupList.Count; i++)
            {
                List<DataTimeGroup> list = TimeGroupList[i];
                for (int j = 0; j < list.Count; j++)
                {
                    byteList.AddRange(list[j].ToArray());
                }
            }
            return byteList.ToArray();
            /*
            byte[] newByte = null;
            newByte = ArrayHelper.AddBytes(newByte, GroupNo.ToArray());
            for (int i = 0; i < TimeGroupList.Count; i++)
            {
                List<DataTimeGroup> list = TimeGroupList[i];
                for (int j = 0; j < list.Count; j++)
                {
                    newByte = ArrayHelper.AddBytes(newByte, list[j].ToArray());
                }
            }
            return newByte;
            */
        }

        #endregion
    }
}
