using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class DataMonitor
    {
        #region Var

        public string Mac { get; set; }
        public string P1_Welcome { get; set; }

        public string P2_Welcome { get; set; }

        public byte P1_Voice { get; set; }

        public byte P2_Voice { get; set; }

        public byte P1_CardReader { get; set; }

        public byte P2_CardReader { get; set; }

        public byte P1_Barcode { get; set; }

        public byte P2_Barcode { get; set; }

        public byte P1_Throughput { get; set; }

        public byte P2_Throughput { get; set; }

        public byte P1_IDCardEnable { get; set; }

        public byte P2_IDCardEnable { get; set; }


        #endregion

        #region deserialization
        public void Deserialize(Packets packet)
        {
            byte[] data = packet.Data;
            if (data == null) return;
            int length = data.Length;
            int curLen = 0;
            //欢迎词
            if (length - curLen < 24) return;
            P1_Welcome = ArrayHelper.ArrayToGB2312(ArrayHelper.SubByte(data, curLen, 24));
            curLen += 24;
            //语音段
            if (length - curLen < 1) return;
            P1_Voice = data[curLen];
            curLen += 1;
            //读卡器
            if (length - curLen < 1) return;
            P1_CardReader = data[curLen];
            curLen += 1;
            //条码
            if (length - curLen < 1) return;
            P1_Barcode = data[curLen];
            curLen += 1;
            //吞吐机
            if (length - curLen < 1) return;
            P1_Throughput = data[curLen];
            curLen += 1;
            //身份证
            if (length - curLen < 1) return;
            P1_IDCardEnable = data[curLen];
            curLen += 1;

            //欢迎词
            if (length - curLen < 24) return;
            P2_Welcome = ArrayHelper.ArrayToGB2312(ArrayHelper.SubByte(data, curLen, 24));
            curLen += 24;
            //语音段
            if (length - curLen < 1) return;
            P2_Voice = data[curLen];
            curLen += 1;
            //读卡器
            if (length - curLen < 1) return;
            P2_CardReader = data[curLen];
            curLen += 1;
            //条码
            if (length - curLen < 1) return;
            P2_Barcode = data[curLen];
            curLen += 1;
            //吞吐机
            if (length - curLen < 1) return;
            P2_Throughput = data[curLen];
            curLen += 1;
            //身份证
            if (length - curLen < 1) return;
            P2_IDCardEnable = data[curLen];
            curLen += 1;

        }
        #endregion

        #region Serialize
        public byte[] Serialize()
        {
            List<byte> list = new List<byte>();
            list.AddRange(ArrayHelper.GB2312ToArray(P1_Welcome, 24));
            list.Add(P1_Voice);
            list.Add(P1_CardReader);
            list.Add(P1_Barcode);
            list.Add(P1_Throughput);
            list.Add(P1_IDCardEnable);
            list.AddRange(ArrayHelper.GB2312ToArray(P2_Welcome, 24));
            list.Add(P2_Voice);
            list.Add(P2_CardReader);
            list.Add(P2_Barcode);
            list.Add(P2_Throughput);
            list.Add(P2_IDCardEnable);
            return list.ToArray();
        }
        #endregion
    }
}
