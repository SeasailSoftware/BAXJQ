using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Dev
{
    public class CheckOnLeaveArgs : System.EventArgs
    {
        public string CardNo { get; set; }
        public int DeviceId { get; set; }

        public int IOFlag { get; set; }
    }
}
