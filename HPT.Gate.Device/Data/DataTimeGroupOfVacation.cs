using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DataTimeGroupOfVacation
    {
        #region 构造函数
        public DataTimeGroupOfVacation()
        {
            TimeGroupList = new List<List<DataTimeGroup>>();
            for (int i = 0; i < 3; i++)
            {
                List<DataTimeGroup> list = new List<DataTimeGroup>();
                for (int j = 0; j < 5; j++)
                {
                    DataTimeGroup timeGroup = new DataTimeGroup();
                    list.Add(timeGroup);
                }
                TimeGroupList.Add(list);
            }
        }
        #endregion

        #region Var
        /// <summary>
        /// 节假日时间列表
        /// </summary>
        List<List<DataTimeGroup>> _TimeGroupList;

        public List<List<DataTimeGroup>> TimeGroupList
        {
            get { return _TimeGroupList; }
            set { _TimeGroupList = value; }
        }

        /// <summary>
        /// 同步节假日时间组是否成功的标志
        /// </summary>
        public bool UpdateFlag { get; set; }

        #endregion
        #region 序列化
        /// <summary>
        /// 将DataTimeGroupOfVacation对象转成byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            byte[] newByte = null;
            for (int i = 0; i < TimeGroupList.Count; i++)
            {
                List<DataTimeGroup> list = TimeGroupList[i];
                for (int j = 0; j < list.Count; j++)
                {
                    newByte = ArrayHelper.AddBytes(newByte, list[j].ToArray());
                }

            }
            return newByte;
        }

        #endregion     #endregion
    }
}
