using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Face.AXD
{
    public class SerialDataArrivedEventArgs : EventArgs
    {
        public byte[] Data { get; internal set; }
    }
}
