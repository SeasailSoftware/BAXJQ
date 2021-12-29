using HPT.Gate.Utils.Common;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class DataRecord
    {
        #region 构造函数

        public DataRecord()
        {
        }
        #endregion
        #region Var

        public int Length => 27;

        public UInt32 CurrentIndex { get; set; }

        public UInt16 MachineId { get; set; }

        public byte Type { get; set; }


        public string CardNo { get; set; }

        public byte RecordType { get; set; }

        public string SRecordType
        {
            get
            {
                try
                {
                    RecordEvent e = (RecordEvent)RecordType;
                    Type t = e.GetType();
                    FieldInfo info = t.GetField(Enum.GetName(t, e));
                    DescriptionAttribute description = (DescriptionAttribute)Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute));
                    return description.Description;
                }
                catch
                {
                    return "未知卡状态";
                }
            }
        }

        public byte IOFlag { get; set; }

        public string SIOFlag => IOFlag == 0x03 ? "进" : "出";
        public string RecDatetime { get; set; }

        /// <summary>
        /// 身份证姓名
        /// </summary>
        public string Name { get; set; }

        public string Sex { get; set; }

        /// <summary>
        /// 身份证民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 身份证住址
        /// </summary>
        public string Address { get; set; }

        #endregion

        #region private
        private byte[] GetValueFromDescription(string sRecordType)
        {
            byte arr = 0x01;
            switch (sRecordType)
            {
                case "有效票":
                    arr = 0x00;
                    break;
                case "未查到票信息":
                    arr = 0x01;
                    break;
                case "黑名单":
                    arr = 0x02;
                    break;
                case "未授权":
                    arr = 0x03;
                    break;
                case "入口无权限":
                    arr = 0x04;
                    break;
                case "出口无权限":
                    arr = 0x05;
                    break;
                case "该时段禁止通行":
                    arr = 0x06;
                    break;
                case "该卡已过有效期":
                    arr = 0x07;
                    break;
                case "该卡已存在":
                    arr = 0x08;
                    break;
                case "读卡失败":
                    arr = 0x09;
                    break;
                case "该卡未进场":
                    arr = 0x0A;
                    break;
                case "该卡未出场":
                    arr = 0x0B;
                    break;
                case "限次":
                    arr = 0x0C;
                    break;
                case "限时":
                    arr = 0x0D;
                    break;
                case "有效未过闸":
                    arr = 0x10;
                    break;
            }
            return new byte[] { arr };
        }

        /// <summary>
        /// 得到民族信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNationInfo(byte index)
        {
            string nation = string.Empty;
            switch (index)
            {
                case 0x31:
                    nation = "汉族";
                    break;
                case 0x32:
                    nation = "蒙古族";
                    break;
                case 0x33:
                    nation = "回族";
                    break;
                case 0x34:
                    nation = "藏族";
                    break;
                case 0x35:
                    nation = "维吾尔族";
                    break;
                case 0x36:
                    nation = "苗族";
                    break;
                case 0x37:
                    nation = "彝族";
                    break;
                case 0x38:
                    nation = "壮族";
                    break;
                case 0x39:
                    nation = "布依族";
                    break;
                case 0x40:
                    nation = "朝鲜族";
                    break;
            }
            return nation;
        }

        private byte[] GetNationIndex(string sNation)
        {
            byte nation = 0x31;
            switch (sNation)
            {
                case "汉族":
                    nation = 0x31;
                    break;
                case "蒙古族":
                    nation = 0x32;
                    break;
                case "回族":
                    nation = 0x33;
                    break;
                case "藏族":
                    nation = 0x34;
                    break;
                case "维吾尔族":
                    nation = 0x35;
                    break;
                case "苗族":
                    nation = 0x36;
                    break;
                case "彝族":
                    nation = 0x37;
                    break;
                case "壮族":
                    nation = 0x38;
                    break;
                case "布依族":
                    nation = 0x39;
                    break;
                case "朝鲜族":
                    nation = 0x40;
                    break;
            }
            return new byte[] { nation };
        }
        #endregion

        #region 序列化与反序列化

        /// <summary>
        /// 序列化
        /// </summary>
        public void Init(byte[] data)
        {
            if (data == null) return;
            int length = data.Length;
            int index = 0;
            //开始指针
            if (length < index + 4) return;
            byte[] arr1 = ArrayHelper.SubByte(data, index, 4);
            Array.Reverse(arr1);
            CurrentIndex = BitConverter.ToUInt32(arr1, 0);
            index += 4;
            //结束指针
            if (length < index + 2) return;
            byte[] arr2 = ArrayHelper.SubByte(data, index, 2);
            Array.Reverse(arr2);
            MachineId = BitConverter.ToUInt16(arr2, 0);
            index += 2;
            //入口过闸总数
            if (length < index + 1) return;
            Type = data[index];
            index += 1;
            //出口过闸总数
            if (length < index + 12) return;
            string cardNo = ArrayHelper.ArrayToHex(ArrayHelper.SubByte(data, index, 12));
            switch (Type)
            {
                case 0x01:
                    CardNo = cardNo.Substring(0, 8);
                    break;
                case 0x02:
                    CardNo = cardNo.Substring(0, 16);
                    break;
                case 0x03:
                    CardNo = cardNo.Substring(0, 18);
                    break;
                case 0x04:
                    CardNo = cardNo.Substring(0, 24);
                    break;
                case 0x05:
                    string fingerId = cardNo.Substring(0, 8).Replace("A0", "00").Replace("B0", "00");
                    byte[] arr = ArrayHelper.HexToArray(fingerId, 4);
                    var temp = ArrayHelper.bytesToInt(arr);
                    if (temp == 0)
                        CardNo = "00000000";
                    else
                    {
                        CardNo = ((temp - 4) / 5 + 1).ToString("00000000");
                    }
                    break;
                case 0x06:
                    byte[] array = ArrayHelper.HexToArray(cardNo, 4);
                    Array.Reverse(array);
                    UInt32 value = BitConverter.ToUInt32(array, 0);
                    if (value > 0)
                        value = (value - 4) / 5 + 1;
                    CardNo = value.ToString("00000000");
                    break;
            }
            index += 12;
            //记录类型
            if (length < index + 1) return;
            RecordType = data[index];
            index += 1;
            //记录卡号
            if (length < index + 1) return;
            IOFlag = data[index];
            index += 1;
            //EmpId
            if (length < index + 6) return;
            RecDatetime = ArrayHelper.ArrayToDateTimeString(ArrayHelper.SubByte(data, index, 6));
            index += 6;
            if (length < index + 1) return;

        }


        /// <summary>
        /// 重写ToString()方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"当前指针:{CurrentIndex},机器号:{MachineId},");
            switch (Type)
            {
                case 0x01:
                    buffer.Append("卡种类:IC/ID卡,");
                    break;
                case 0x02:
                    buffer.Append("卡种类:身份证序列号,");
                    break;
                case 0x03:
                    buffer.Append("卡种类:身份证号码,");
                    break;
                case 0x04:
                    buffer.Append("卡种类:条形码数据,");
                    break;
                case 0x05:
                    buffer.Append("卡种类:指纹数据,");
                    break;
                case 0x06:
                    buffer.Append("卡种类:人脸数据,");
                    break;
            }
            buffer.Append(string.Format("编号:{0},", CardNo));
            buffer.Append(string.Format("记录时间:{0},", RecDatetime));
            buffer.Append(string.Format("进出标志:{0},", SIOFlag));
            buffer.Append(string.Format("记录类型:{0}", SRecordType));
            return buffer.ToString();
        }
        #endregion


    }
}
