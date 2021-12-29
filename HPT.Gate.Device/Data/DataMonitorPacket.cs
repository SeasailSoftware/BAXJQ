using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DataMonitorPacket
    {

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataMonitorPacket()
        {
            this.MachintId = new byte[1];
            this.Total = new byte[1];
            this.UpdateIndex = new byte[1];
            this.UpdateArray = new byte[1024];

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataMonitorPacket(int machineId, int total, int index, byte[] arr)
        {
            this.MachintId = ArrayHelper.IntToBytes(machineId, 1);
            this.Total = ArrayHelper.IntToBytes(total, 1);
            this.UpdateIndex = ArrayHelper.IntToBytes(index, 1);
            this.UpdateArray = arr;

        }

        #endregion
        #region Var
        /// <summary>
        /// 出口还是入口
        /// </summary>
        private byte[] _MachineId;

        public byte[] MachintId
        {
            get { return _MachineId; }
            set { _MachineId = value; }
        }

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

        /// <summary>
        /// 更新包数组
        /// </summary>
        private byte[] _UpdateArray;

        public byte[] UpdateArray
        {
            get { return _UpdateArray; }
            set { _UpdateArray = value; }
        }

        #endregion
        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            byte[] newByte = null;
            newByte = ArrayHelper.AddBytes(newByte, MachintId);
            newByte = ArrayHelper.AddBytes(newByte, Total);
            newByte = ArrayHelper.AddBytes(newByte, UpdateIndex);
            newByte = ArrayHelper.AddBytes(newByte, UpdateArray);
            return newByte;
        }
    }
}
