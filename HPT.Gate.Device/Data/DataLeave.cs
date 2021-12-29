using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Data
{
    public class DataLeave
    {
        #region properity

        public string CardNo { get; set; }

        public int DeviceId { get; set; }

        public byte IOFlag { get; set; }
        public byte[] StandBy { get; set; }

        #endregion

        #region Init
        public void Init(byte[] data)
        {
            if (data == null || data.Length != 17)
                return;
            int index = 0;
            CardNo = ArrayHelper.ArrayToHex(ArrayHelper.SubByte(data, index, 4));
            index += 4;
            DeviceId = ArrayHelper.bytesToInt(ArrayHelper.SubByte(data, index, 2));
            index += 2;
            IOFlag = data[index];
            index += 1;
            StandBy = ArrayHelper.SubByte(data, 1, 10);
        }
        #endregion

    }
}
