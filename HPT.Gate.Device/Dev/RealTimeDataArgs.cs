using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Dev
{
    public class RealTimeDataArgs : System.EventArgs
    {
        #region properity
        public byte CardType { get; set; }

        public UInt16 DeviceId { get; set; }

        public byte IOFlag { get; set; }

        public string RecDateTime { get; set; }

        public string CardNo { get; set; }

        public byte[] FingerPrint { get; set; }

        public byte[] FaceDate { get; set; }

        #endregion

    }
}
