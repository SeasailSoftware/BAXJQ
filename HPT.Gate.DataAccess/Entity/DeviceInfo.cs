using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class DeviceInfo
    {

        public int DeviceType { get; set; }
        public int DeviceId { get; set; }

        public string DeviceName { get; set; }

        public int PlaceId { get; set; }

        public string Mac { get; set; }

        public string ServerIP { get; set; } = "255.255.255.255";

        public int ServerPort { get; set; } = 0;
        public string IPAddress { get; set; }

        public string SubNet { get; set; }

        public string GateWay { get; set; }

        public int Port { get; set; }

        public string HardVersion { get; set; }

        public string SoftVersion { get; set; }
    }
}
