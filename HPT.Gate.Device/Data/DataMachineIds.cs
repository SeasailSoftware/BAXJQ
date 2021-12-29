using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class DataMachineIds
    {
        #region ctor

        public DataMachineIds()
        {
            Machine01 = new byte[] { 0x00, 0x00 };
            Machine02 = new byte[] { 0x00, 0x00 };
            Machine03 = new byte[] { 0x00, 0x00 };
            Machine04 = new byte[] { 0x00, 0x00 };
            Machine05 = new byte[] { 0x00, 0x00 };
            Machine06 = new byte[] { 0x00, 0x00 };
            Machine07 = new byte[] { 0x00, 0x00 };
            Machine08 = new byte[] { 0x00, 0x00 };
            Machine09 = new byte[] { 0x00, 0x00 };
            Machine10 = new byte[] { 0x00, 0x00 };
            Machine11 = new byte[] { 0x00, 0x00 };
            Machine12 = new byte[] { 0x00, 0x00 };
            Machine13 = new byte[] { 0x00, 0x00 };
            Machine14 = new byte[] { 0x00, 0x00 };
            Machine15 = new byte[] { 0x00, 0x00 };
            Machine16 = new byte[] { 0x00, 0x00 };
            Machine17 = new byte[] { 0x00, 0x00 };
            Machine18 = new byte[] { 0x00, 0x00 };
            Machine19 = new byte[] { 0x00, 0x00 };
            Machine20 = new byte[] { 0x00, 0x00 };
            Machine21 = new byte[] { 0x00, 0x00 };
            Machine22 = new byte[] { 0x00, 0x00 };
            Machine23 = new byte[] { 0x00, 0x00 };
            Machine24 = new byte[] { 0x00, 0x00 };
            Machine25 = new byte[] { 0x00, 0x00 };
            Machine26 = new byte[] { 0x00, 0x00 };
            Machine27 = new byte[] { 0x00, 0x00 };
            Machine28 = new byte[] { 0x00, 0x00 };
            Machine29 = new byte[] { 0x00, 0x00 };
            Machine30 = new byte[] { 0x00, 0x00 };
            Machine31 = new byte[] { 0x00, 0x00 };
            Machine32 = new byte[] { 0x00, 0x00 };
        }
        #endregion

        #region MachineIds

        public byte[] Machine01 { get; set; }
        public byte[] Machine02 { get; set; }
        public byte[] Machine03 { get; set; }
        public byte[] Machine04 { get; set; }
        public byte[] Machine05 { get; set; }
        public byte[] Machine06 { get; set; }
        public byte[] Machine07 { get; set; }
        public byte[] Machine08 { get; set; }
        public byte[] Machine09 { get; set; }
        public byte[] Machine10 { get; set; }
        public byte[] Machine11 { get; set; }
        public byte[] Machine12 { get; set; }
        public byte[] Machine13 { get; set; }
        public byte[] Machine14 { get; set; }
        public byte[] Machine15 { get; set; }
        public byte[] Machine16 { get; set; }
        public byte[] Machine17 { get; set; }
        public byte[] Machine18 { get; set; }
        public byte[] Machine19 { get; set; }
        public byte[] Machine20 { get; set; }

        public byte[] Machine21 { get; set; }
        public byte[] Machine22 { get; set; }
        public byte[] Machine23 { get; set; }
        public byte[] Machine24 { get; set; }
        public byte[] Machine25 { get; set; }
        public byte[] Machine26 { get; set; }
        public byte[] Machine27 { get; set; }
        public byte[] Machine28 { get; set; }
        public byte[] Machine29 { get; set; }
        public byte[] Machine30 { get; set; }

        public byte[] Machine31 { get; set; }
        public byte[] Machine32 { get; set; }


        #endregion

        #region 序列化

        public void Init(byte[] arr)
        {
            if (arr.Length != 64) return;
            int index = 0;
            Machine01 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine02 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine03 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine04 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine05 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine06 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine07 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine08 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine09 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine10 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine11 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine12 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine13 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine14 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine15 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine16 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine17 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine18 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine19 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine20 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine21 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine22 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine23 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine24 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine25 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine26 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine27 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine28 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine29 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine30 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine31 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
            Machine32 = ArrayHelper.SubByte(arr, index, 2);
            index += 2;
        }

        public byte[] ToArray()
        {
            List<byte> arr = new List<byte>();
            arr.AddRange(Machine01);
            arr.AddRange(Machine02);
            arr.AddRange(Machine03);
            arr.AddRange(Machine04);
            arr.AddRange(Machine05);
            arr.AddRange(Machine06);
            arr.AddRange(Machine07);
            arr.AddRange(Machine08);
            arr.AddRange(Machine09);
            arr.AddRange(Machine10);
            arr.AddRange(Machine11);
            arr.AddRange(Machine12);
            arr.AddRange(Machine13);
            arr.AddRange(Machine14);
            arr.AddRange(Machine15);
            arr.AddRange(Machine16);
            arr.AddRange(Machine17);
            arr.AddRange(Machine18);
            arr.AddRange(Machine19);
            arr.AddRange(Machine20);
            arr.AddRange(Machine21);
            arr.AddRange(Machine22);
            arr.AddRange(Machine23);
            arr.AddRange(Machine24);
            arr.AddRange(Machine25);
            arr.AddRange(Machine26);
            arr.AddRange(Machine27);
            arr.AddRange(Machine28);
            arr.AddRange(Machine29);
            arr.AddRange(Machine30);
            arr.AddRange(Machine31);
            arr.AddRange(Machine32);
            return arr.ToArray();
        }
        #endregion

        #region 获取设备列表
        public List<UInt16> GetMachineIds()
        {
            List<UInt16> list = new List<UInt16>();
            if (ArrayHelper.bytesToInt(Machine01) != 0) list.Add(BitConverter.ToUInt16(Machine01, 0));
            if (ArrayHelper.bytesToInt(Machine02) != 0) list.Add(BitConverter.ToUInt16(Machine02, 0));
            if (ArrayHelper.bytesToInt(Machine03) != 0) list.Add(BitConverter.ToUInt16(Machine03, 0));
            if (ArrayHelper.bytesToInt(Machine04) != 0) list.Add(BitConverter.ToUInt16(Machine04, 0));
            if (ArrayHelper.bytesToInt(Machine05) != 0) list.Add(BitConverter.ToUInt16(Machine05, 0));
            if (ArrayHelper.bytesToInt(Machine06) != 0) list.Add(BitConverter.ToUInt16(Machine06, 0));
            if (ArrayHelper.bytesToInt(Machine07) != 0) list.Add(BitConverter.ToUInt16(Machine07, 0));
            if (ArrayHelper.bytesToInt(Machine08) != 0) list.Add(BitConverter.ToUInt16(Machine08, 0));
            if (ArrayHelper.bytesToInt(Machine09) != 0) list.Add(BitConverter.ToUInt16(Machine09, 0));
            if (ArrayHelper.bytesToInt(Machine10) != 0) list.Add(BitConverter.ToUInt16(Machine10, 0));
            if (ArrayHelper.bytesToInt(Machine11) != 0) list.Add(BitConverter.ToUInt16(Machine11, 0));
            if (ArrayHelper.bytesToInt(Machine12) != 0) list.Add(BitConverter.ToUInt16(Machine12, 0));
            if (ArrayHelper.bytesToInt(Machine13) != 0) list.Add(BitConverter.ToUInt16(Machine13, 0));
            if (ArrayHelper.bytesToInt(Machine14) != 0) list.Add(BitConverter.ToUInt16(Machine14, 0));
            if (ArrayHelper.bytesToInt(Machine15) != 0) list.Add(BitConverter.ToUInt16(Machine15, 0));
            if (ArrayHelper.bytesToInt(Machine16) != 0) list.Add(BitConverter.ToUInt16(Machine16, 0));
            if (ArrayHelper.bytesToInt(Machine17) != 0) list.Add(BitConverter.ToUInt16(Machine17, 0));
            if (ArrayHelper.bytesToInt(Machine18) != 0) list.Add(BitConverter.ToUInt16(Machine18, 0));
            if (ArrayHelper.bytesToInt(Machine19) != 0) list.Add(BitConverter.ToUInt16(Machine19, 0));
            if (ArrayHelper.bytesToInt(Machine20) != 0) list.Add(BitConverter.ToUInt16(Machine20, 0));
            if (ArrayHelper.bytesToInt(Machine21) != 0) list.Add(BitConverter.ToUInt16(Machine21, 0));
            if (ArrayHelper.bytesToInt(Machine22) != 0) list.Add(BitConverter.ToUInt16(Machine22, 0));
            if (ArrayHelper.bytesToInt(Machine23) != 0) list.Add(BitConverter.ToUInt16(Machine23, 0));
            if (ArrayHelper.bytesToInt(Machine24) != 0) list.Add(BitConverter.ToUInt16(Machine24, 0));
            if (ArrayHelper.bytesToInt(Machine25) != 0) list.Add(BitConverter.ToUInt16(Machine25, 0));
            if (ArrayHelper.bytesToInt(Machine26) != 0) list.Add(BitConverter.ToUInt16(Machine26, 0));
            if (ArrayHelper.bytesToInt(Machine27) != 0) list.Add(BitConverter.ToUInt16(Machine27, 0));
            if (ArrayHelper.bytesToInt(Machine28) != 0) list.Add(BitConverter.ToUInt16(Machine28, 0));
            if (ArrayHelper.bytesToInt(Machine29) != 0) list.Add(BitConverter.ToUInt16(Machine29, 0));
            if (ArrayHelper.bytesToInt(Machine30) != 0) list.Add(BitConverter.ToUInt16(Machine30, 0));
            if (ArrayHelper.bytesToInt(Machine31) != 0) list.Add(BitConverter.ToUInt16(Machine31, 0));
            if (ArrayHelper.bytesToInt(Machine32) != 0) list.Add(BitConverter.ToUInt16(Machine32, 0));
            return list;
        }
        #endregion

    }
}
