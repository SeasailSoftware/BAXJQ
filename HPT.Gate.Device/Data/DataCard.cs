using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;

namespace HPT.Gate.Device.Data
{
    public class DataCard
    {
        #region 卡属性

        public int Type { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        private byte[] _CardNo;

        public byte[] CardNo
        {
            get => _CardNo;
            set => _CardNo = value;
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public string SCardNo => ArrayHelper.ArrayToHex(this.CardNo);

        /// <summary>
        /// 卡序号
        /// </summary>
        private byte[] _CurCardSerial;

        public byte[] CurCardSerial
        {
            get => _CurCardSerial;
            set => _CurCardSerial = value;
        }

        /// <summary>
        /// 卡编号
        /// </summary>
        public int CardId => ArrayHelper.bytesToInt(CurCardSerial);
        /// <summary>
        /// 当前开卡总数
        /// </summary>
        private byte[] _TotalCardSerial;

        public byte[] TatolCardSerial
        {
            get => _TotalCardSerial;
            set => _TotalCardSerial = value;
        }

        /// <summary>
        /// 卡状态
        /// </summary>
        private byte[] _CardStatus;

        public byte[] CardStatus
        {
            get => _CardStatus;
            set => _CardStatus = value;
        }

        /// <summary>
        /// 卡类型
        /// </summary>
        private byte[] _CardType;

        public byte[] CardType
        {
            get => _CardType;
            set => _CardType = value;
        }

        /// <summary>
        /// 显示屏第一行显示内容
        /// </summary>
        private byte[] _Row1;

        public byte[] Row1
        {
            get => _Row1;
            set => _Row1 = value;
        }

        /// <summary>
        /// 第二行显示内容
        /// </summary>
        private byte[] _Row2;

        public byte[] Row2
        {
            get => _Row2;
            set => _Row2 = value;
        }
        /// <summary>
        /// 显示屏第三行显示内容
        /// </summary>
        private byte[] _Row3;

        public byte[] Row3
        {
            get => _Row3;
            set => _Row3 = value;
        }

        /// <summary>
        /// 进门权限
        /// </summary>
        private byte[] _RightOfDoorIn;

        public byte[] RightOfDoorIn
        {
            get => _RightOfDoorIn;
            set => _RightOfDoorIn = value;
        }

        /// <summary>
        /// 出门权限
        /// </summary>
        private byte[] _RightOfDoorOut;

        public byte[] RightOfDoorOut
        {
            get => _RightOfDoorOut;
            set => _RightOfDoorOut = value;
        }

        /// <summary>
        /// 语言段号码
        /// </summary>
        private byte[] _VoiceNo;

        public byte[] VoiceNo
        {
            get => _VoiceNo;
            set => _VoiceNo = value;
        }

        /// <summary>
        /// 是否显示照片
        /// </summary>
        private byte[] _PhotoName;

        public byte[] PhotoName
        {
            get => _PhotoName;
            set => _PhotoName = value;
        }

        /// <summary>
        /// 节假日时间组号
        /// </summary>
        private byte[] _VacationGrop;

        public byte[] VacationGrop
        {
            get => _VacationGrop;
            set => _VacationGrop = value;
        }

        /// <summary>
        /// 进门节假日时间组号
        /// </summary>
        private byte[] _TimeGroupOfNormalDoorIn;

        public byte[] TimeGroupOfNormalDoorIn
        {
            get => _TimeGroupOfNormalDoorIn;
            set => _TimeGroupOfNormalDoorIn = value;
        }

        /// <summary>
        /// 出门节假日时间组号
        /// </summary>
        private byte[] _TimeGroupOfNormalDoorOut;

        public byte[] TimeGroupOfNormalDoorOut
        {
            get => _TimeGroupOfNormalDoorOut;
            set => _TimeGroupOfNormalDoorOut = value;
        }

        /// <summary>
        /// 今日过闸总数
        /// </summary>
        private byte[] _TatolOfPassGate;

        public byte[] TatolOfPassGate
        {
            get => _TatolOfPassGate;
            set => _TatolOfPassGate = value;
        }

        /// <summary>
        /// 连续过闸时间
        /// </summary>
        private byte[] _IntervalMinute;

        public byte[] IntervalMinute
        {
            get => _IntervalMinute;
            set => _IntervalMinute = value;
        }

        /// <summary>
        /// 卡有效期开始时间
        /// </summary>
        private byte[] _BeginDate;

        public byte[] BeginDate
        {
            get => _BeginDate;
            set => _BeginDate = value;
        }

        /// <summary>
        /// 卡有效期结束时间
        /// </summary>
        private byte[] _EndDate;

        public byte[] EndDate
        {
            get => _EndDate;
            set => _EndDate = value;
        }

        /// <summary>
        /// 设备是否收到反馈信息的标志
        /// </summary>
        private bool _UpdateFlag;

        public bool UpdateFlag
        {
            get => _UpdateFlag;
            set => _UpdateFlag = value;
        }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// 指纹数据
        /// </summary>
        public byte[] FingerData { get; set; }

        #endregion

        #region 构造函数
        public DataCard()
        {
            this.CardNo = new byte[4];
            this.CurCardSerial = new byte[2];
            this.TatolCardSerial = new byte[2];
            this.CardStatus = new byte[1];
            this.CardType = new byte[1];
            this.Row1 = new byte[16];
            this.Row2 = new byte[16];
            this.Row3 = new byte[16];
            this.RightOfDoorIn = new byte[1];
            this.RightOfDoorOut = new byte[1];
            this.VoiceNo = new byte[1];
            this.PhotoName = new byte[2];
            this.VacationGrop = new byte[1];
            this.TimeGroupOfNormalDoorIn = new byte[1];
            this.TimeGroupOfNormalDoorOut = new byte[1];
            this.BeginDate = new byte[3];
            this.EndDate = new byte[3];
            this.FingerData = new byte[1024];
        }
        #endregion

        #region 序列化与反序列化

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data"></param>
        public void Init(byte[] data)
        {

            if (data == null) return;
            int dataLen = data.Length;
            int index = 0;
            //卡号
            if (dataLen < index + CardNo.Length) return;
            Array.Copy(data, index, CardNo, 0, CardNo.Length);
            index += CardNo.Length;
            //当前卡序号
            if (dataLen < index + CurCardSerial.Length) return;
            Array.Copy(data, index, CurCardSerial, 0, CurCardSerial.Length);
            index += CurCardSerial.Length;
            //总卡号
            if (dataLen < index + TatolCardSerial.Length) return;
            Array.Copy(data, index, TatolCardSerial, 0, TatolCardSerial.Length);
            index += TatolCardSerial.Length;
            //卡权限
            if (dataLen < index + CardStatus.Length) return;
            Array.Copy(data, index, CardStatus, 0, CardStatus.Length);
            index += CardStatus.Length;
            //卡类型
            if (dataLen < index + CardType.Length) return;
            Array.Copy(data, index, CardType, 0, CardType.Length);
            index += CardType.Length;
            //第一行显示内容
            if (dataLen < index + Row1.Length) return;
            Array.Copy(data, index, Row1, 0, Row1.Length);
            index += Row1.Length;
            //第二行显示内容
            if (dataLen < index + Row2.Length) return;
            Array.Copy(data, index, Row2, 0, Row2.Length);
            index += Row2.Length;
            //第二行显示内容
            if (dataLen < index + Row3.Length) return;
            Array.Copy(data, index, Row3, 0, Row3.Length);
            index += Row3.Length;
            //入口权限
            if (dataLen < index + RightOfDoorIn.Length) return;
            Array.Copy(data, index, RightOfDoorIn, 0, RightOfDoorIn.Length);
            index += RightOfDoorIn.Length;
            //出口权限
            if (dataLen < index + RightOfDoorOut.Length) return;
            Array.Copy(data, index, RightOfDoorOut, 0, RightOfDoorOut.Length);
            index += RightOfDoorOut.Length;
            //出口权限
            if (dataLen < index + VoiceNo.Length) return;
            Array.Copy(data, index, VoiceNo, 0, VoiceNo.Length);
            index += VoiceNo.Length;
            //照片名称
            if (dataLen < index + PhotoName.Length) return;
            Array.Copy(data, index, PhotoName, 0, PhotoName.Length);
            index += PhotoName.Length;
            //节假日时间组号
            if (dataLen < index + VacationGrop.Length) return;
            Array.Copy(data, index, VacationGrop, 0, VacationGrop.Length);
            index += VacationGrop.Length;
            //入口时间组号
            if (dataLen < index + TimeGroupOfNormalDoorIn.Length) return;
            Array.Copy(data, index, TimeGroupOfNormalDoorIn, 0, TimeGroupOfNormalDoorIn.Length);
            index += TimeGroupOfNormalDoorIn.Length;
            //出口时间组号
            if (dataLen < index + TimeGroupOfNormalDoorOut.Length) return;
            Array.Copy(data, index, TimeGroupOfNormalDoorOut, 0, TimeGroupOfNormalDoorOut.Length);
            index += TimeGroupOfNormalDoorOut.Length;
            //有效期开始时间
            if (dataLen < index + BeginDate.Length) return;
            Array.Copy(data, index, BeginDate, 0, BeginDate.Length);
            index += BeginDate.Length;
            //有效期结束时间
            if (dataLen < index + EndDate.Length) return;
            Array.Copy(data, index, EndDate, 0, EndDate.Length);
            index += EndDate.Length;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            List<byte> array = new List<byte>();
            array.AddRange(CardNo);
            array.AddRange(CurCardSerial);
            array.AddRange(TatolCardSerial);
            array.AddRange(CardStatus);
            array.AddRange(CardType);
            array.AddRange(Row1);
            array.AddRange(Row2);
            array.AddRange(Row3);
            array.AddRange(RightOfDoorIn);
            array.AddRange(RightOfDoorOut);
            array.AddRange(VoiceNo);
            array.AddRange(PhotoName);
            array.AddRange(VacationGrop);
            array.AddRange(TimeGroupOfNormalDoorIn);
            array.AddRange(TimeGroupOfNormalDoorOut);
            array.AddRange(BeginDate);
            array.AddRange(EndDate);
            if (CheckIsFingerPrint(Type))
                array.AddRange(FingerData);
            return array.ToArray();
        }
        #endregion

        private bool CheckIsFingerPrint(int type)
        {
            return type == 5;
        }

    }
}
