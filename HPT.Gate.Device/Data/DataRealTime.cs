using HPT.Gate.Utils.Common;
using System;

namespace HPT.Gate.Device.Data
{
    public class DataRealTime
    {
        #region property

        public byte CardType { get; set; }

        public UInt16 DeviceId { get; set; }

        public byte IOFlag { get; set; }

        public string RecDateTime { get; set; }

        public byte[] Data { get; set; }

        public string CardNo
        {
            get
            {
                string cardNo = string.Empty;
                if (Data == null) return cardNo;
                if (Data.Length >= 4)
                {
                    switch (CardType)
                    {
                        case 0x01:
                            cardNo = ArrayHelper.ArrayToHex(ArrayHelper.SubByte(Data, 0, 4));
                            break;
                        case 0x02:
                            cardNo = ArrayHelper.ArrayToHex(ArrayHelper.SubByte(Data, 0, 8));
                            break;
                        case 0x03:
                            cardNo = ArrayHelper.ArrayToHex(ArrayHelper.SubByte(Data, 0, 9));
                            break;
                        case 0x04:
                            cardNo = ArrayHelper.ArrayToGB2312(ArrayHelper.SubByte(Data, 0, 18));
                            break;
                        case 0x05:
                            cardNo = ArrayHelper.ArrayToHex(Data);
                            break;
                        case 0x06:
                            byte[] array = ArrayHelper.SubByte(Data, 0, 4);
                            Array.Reverse(array);
                            cardNo = ArrayHelper.ArrayToHex(array);
                            break;
                    }
                }
                return cardNo;
            }
        }

        public byte[] FingerData
        {
            get
            {
                if (CardType != 5) return null;
                return Data;

            }
        }

        public byte[] FaceData
        {
            get
            {
                if (CardType != 6) return null;
                return Data;
            }
        }
        #endregion

        #region Init

        public void Init(byte[] arr)
        {
            if (arr == null) return;
            int length = arr.Length;
            int index = 0;
            //类型
            if (length < index + 1) return;
            this.CardType = arr[0];
            index += 1;
            //机器号
            if (length < index + 2) return;
            byte[] devId = ArrayHelper.SubByte(arr, index, 2);
            Array.Reverse(devId);
            this.DeviceId = BitConverter.ToUInt16(devId, 0);
            index += 2;
            //出入口
            if (length < index + 1) return;
            this.IOFlag = arr[index];
            index += 1;
            //刷卡时间
            if (length < index + 6) return;
            this.RecDateTime = ArrayHelper.ArrayToDateTimeString(ArrayHelper.SubByte(arr, index, 6));
            index += 6;
            if (length == index)
            {
                Data = null;
                return;
            }
            Data = ArrayHelper.SubByte(arr, index, length - index);
        }
        #endregion

    }
}
