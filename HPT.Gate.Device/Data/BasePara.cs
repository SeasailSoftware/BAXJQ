using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class BasePara
    {
        #region Var


        public byte WaitTime { get; set; }


        public byte DelayTime { get; set; }

        public UInt16 RepeatTime { get; set; }

        public byte Direction { get; set; }

        public string P1SerialNo { get; set; }

        public byte IOOFP1 { get; set; }

        public string SoftVersionOfP1 { get; set; }

        public string HardVersionOfP1 { get; set; }

        public string P2SerialNo { get; set; }

        public byte IOOFP2 { get; set; }

        public string SoftVersionOfP2 { get; set; }

        public string HardVersionOfP2 { get; set; }

        #endregion

        #region 构造函数

        public BasePara()
        {

        }
        #endregion

        #region deserialization
        public void Deserialize(Packets packet)
        {
            byte[] data = packet.Data;
            if (data == null) return;
            int length = data.Length;
            int curLen = 0;
            if (length - curLen < 1) return;
            WaitTime = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            DelayTime = data[curLen];
            curLen += 1;
            if (length - curLen < 2) return;
            RepeatTime = BitConverter.ToUInt16(ArrayHelper.SubByte(data, curLen, 2), 0);
            curLen += 2;
            if (length - curLen < 1) return;
            Direction = data[curLen];
            curLen += 1;
            if (length - curLen < 2) return;
            P1SerialNo = ArrayHelper.ArrayToHex(ArrayHelper.SubByte(data, curLen, 2));
            curLen += 2;
            if (length - curLen < 1) return;
            IOOFP1 = data[curLen];
            curLen += 1;
            if (length - curLen < 3) return;
            HardVersionOfP1 = ArrayHelper.ArrayToGB2312(ArrayHelper.SubByte(data, curLen, 3));
            curLen += 3;
            if (length - curLen < 6) return;
            SoftVersionOfP1 = ArrayHelper.ArrayToGB2312(ArrayHelper.SubByte(data, curLen, 6));
            curLen += 6;
            if (length - curLen < 2) return;
            P2SerialNo = ArrayHelper.ArrayToHex(ArrayHelper.SubByte(data, curLen, 2));
            curLen += 2;
            if (length - curLen < 1) return;
            IOOFP2 = data[curLen];
            curLen += 1;
            if (length - curLen < 3) return;
            HardVersionOfP2 = ArrayHelper.ArrayToGB2312(ArrayHelper.SubByte(data, curLen, 3));
            curLen += 3;
            if (length - curLen < 6) return;
            SoftVersionOfP2 = ArrayHelper.ArrayToGB2312(ArrayHelper.SubByte(data, curLen, 6));
            curLen += 6;

        }
        #endregion

        #region Serialize
        public byte[] Serialize()
        {
            List<byte> list = new List<byte>();
            list.Add(WaitTime);
            list.Add(DelayTime);
            list.AddRange(BitConverter.GetBytes(RepeatTime));
            list.Add(Direction);
            list.AddRange(ArrayHelper.HexToArray(P1SerialNo, 2));
            list.Add(IOOFP1);
            list.AddRange(ArrayHelper.GB2312ToArray(HardVersionOfP1, 3));
            list.AddRange(ArrayHelper.GB2312ToArray(SoftVersionOfP1, 6));
            list.AddRange(ArrayHelper.HexToArray(P2SerialNo, 2));
            list.Add(IOOFP2);
            list.AddRange(ArrayHelper.GB2312ToArray(HardVersionOfP2, 3));
            list.AddRange(ArrayHelper.GB2312ToArray(SoftVersionOfP2, 6));
            return list.ToArray();
        }
        #endregion


    }
}
