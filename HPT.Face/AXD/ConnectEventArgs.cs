using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Face.AXD
{
    public class ConnectEventArgs : EventArgs
    {
        public bool Connected { get; internal set; }
    }
}
