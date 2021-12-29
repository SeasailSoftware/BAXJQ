using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class FaceDevice
    {
        public int DeviceId { get; set; }

        public string IPAddress { get; set; }

        public string Mac { get; set; }
        
        public int Port { get; set; }

        public string SN { get; set; }

        public string Password { get; set; }

    }
}
