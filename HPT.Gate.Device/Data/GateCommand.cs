using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public enum GateCommand
    {
        OpenOnceForIn = 0x00,
        OpenOnceForOut = 0x01,
        OpenAlways = 0x02,
        CloseAlways = 0x03
    }
}
