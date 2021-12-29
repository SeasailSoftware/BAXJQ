using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Entity
{
    public class ImageOfRecord
    {
        public int RecId { get; set; }

        public int CamId { get; set; }

        public int DeviceId { get; set; }

        public string IOFlag { get; set; }

        public string RecDatetime { get; set; }

        public byte[] ImageString { get; set; }
    }

}
