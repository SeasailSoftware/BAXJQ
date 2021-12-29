using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Dev
{
    public class CameraCuptureArgs : System.EventArgs
    {
        #region property

        public string CardNo { get; set; }
        public byte IOFlag { get; set; }

        public UInt16 DeviceId { get; set; }

        public string RecDatetime { get; set; }
        public string Message { get; set; }

        #endregion

    }
}
