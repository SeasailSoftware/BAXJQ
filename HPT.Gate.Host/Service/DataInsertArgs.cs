using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HPT.Gate.Host.Service
{
    public class DataInsertArgs : System.EventArgs
    {
        public int CardType { get; set; }
        public int CamId { get; set; }

        public string CardNo { get; set; }

        public int DeviceId { get; set; }

        public int IOFlag { get; set; }

        public string RecDatetime { get; set; }

        public Bitmap Photo { get; set; }

        public string RecordType { get; set; }
    }
}
