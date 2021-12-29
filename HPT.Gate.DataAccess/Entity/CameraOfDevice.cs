using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Entity
{
    public class CameraOfDevice
    {
        public int RecId { get; set; }

        public int InCamId { get; set; }

        public string InCamName { get; set; }

        public int DeviceId { get; set; }

        public int OutCamId { get; set; }

        public string OutCamName { get; set; }

        public string DeviceName { get; set; }

    }
}
