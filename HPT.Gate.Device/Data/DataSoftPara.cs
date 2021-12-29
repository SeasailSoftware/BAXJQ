using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DataSoftPara
    {
        #region -----构造函数--------

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataSoftPara()
        {

        }

        #endregion


        #region -----------Var-----------

        public byte SoftType { get; set; }

        public byte GateType { get; set; }

        public byte CommType { get; set; }

        public byte WorkType { get; set; }

        public byte ModelOfIn { get; set; }

        public byte ModelOfOut { get; set; }

        public byte WGReaderType1 { get; set; }

        public byte IOOFWGReader1 { get; set; }

        public byte WGReader1Bak1 { get; set; }

        public byte WGReader1Bak2 { get; set; }

        public byte WGReaderType2 { get; set; }

        public byte IOOFWGReader2 { get; set; }

        public byte WGReader2Bak1 { get; set; }

        public byte WGReader2Bak2 { get; set; }

        public byte SerialPortType1 { get; set; }

        public byte IOOFSerialPort1 { get; set; }

        public byte SerialPort1Bak1 { get; set; }

        public byte SerialPort1Bak2 { get; set; }

        public byte SerialPortType2 { get; set; }

        public byte IOOFSerialPort2 { get; set; }

        public byte SerialPort2Bak1 { get; set; }

        public byte SerialPort2Bak2 { get; set; }


        #endregion

        #region -----序列化与反序列化------
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize()
        {
            List<byte> list = new List<byte>();
            list.Add(SoftType);
            list.Add(GateType);
            list.Add(CommType);
            list.Add(WorkType);
            list.Add(ModelOfIn);
            list.Add(ModelOfOut);
            list.Add(WGReaderType1);
            list.Add(IOOFWGReader1);
            list.Add(WGReader1Bak1);
            list.Add(WGReader1Bak2);
            list.Add(WGReaderType2);
            list.Add(IOOFWGReader2);
            list.Add(WGReader2Bak1);
            list.Add(WGReader2Bak2);
            list.Add(SerialPortType1);
            list.Add(IOOFSerialPort1);
            list.Add(SerialPort1Bak1);
            list.Add(SerialPort1Bak2);
            list.Add(SerialPortType2);
            list.Add(IOOFSerialPort2);
            list.Add(SerialPort2Bak1);
            list.Add(SerialPort2Bak2);
            return list.ToArray();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data"></param>
        public void Deserialize(byte[] data)
        {
            if (data == null) return;
            int length = data.Length;
            int curLen = 0;
            if (length - curLen < 1) return;
            SoftType = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            GateType = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            CommType = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            WorkType = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            ModelOfIn = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            ModelOfOut = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            WGReaderType1 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            IOOFWGReader1 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            WGReader1Bak1 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            WGReader1Bak2 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            WGReaderType2 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            IOOFWGReader2 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            WGReader2Bak1 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            WGReader2Bak2 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            SerialPortType1 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            IOOFSerialPort1 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            SerialPort1Bak1 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            SerialPort1Bak2 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            SerialPortType2 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            IOOFSerialPort2 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            SerialPort2Bak1 = data[curLen];
            curLen += 1;
            if (length - curLen < 1) return;
            SerialPort2Bak2 = data[curLen];
            curLen += 1;
        }


        #endregion

    }
}
