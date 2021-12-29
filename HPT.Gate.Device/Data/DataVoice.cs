using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class DataVoice
    {

        #region properity

        public byte VoiceNo { get; set; }

        public byte[] StandBy { get; set; }
        #endregion

        #region Init
        public void Init(byte[] data)
        {
            if (data == null || data.Length != 11)
                return;
            VoiceNo = data[0];
            StandBy = ArrayHelper.SubByte(data, 1, 10);
        }
        #endregion

    }
}
