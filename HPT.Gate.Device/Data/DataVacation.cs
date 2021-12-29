using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Device.Data;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DataVacation
    {

        #region 构造函数
        public DataVacation()
        {
            this.VacationList = new List<DateGroup>();
            for (int i = 0; i < 16; i++)
            {
                DateGroup dateGroup = new DateGroup();
                VacationList.Add(dateGroup);
            }

        }

        #endregion

        #region Var
        /// <summary>
        /// 节假日列表
        /// </summary>
        List<DateGroup> _VacationList;

        public List<DateGroup> VacationList
        {
            get { return _VacationList; }
            set { _VacationList = value; }
        }

        /// <summary>
        /// 同步节假日信息是否成功的标志
        /// </summary>
        public bool UpdateFlag { get; set; }
        #endregion

        #region 序列化
        public byte[] ToArray()
        {
            byte[] newByte = null;
            for (int i = 0; i < this.VacationList.Count; i++)
            {
                newByte = ArrayHelper.AddBytes(newByte, VacationList[i].ToArray());
            }
            return newByte;
        }

        #endregion

    }
}
