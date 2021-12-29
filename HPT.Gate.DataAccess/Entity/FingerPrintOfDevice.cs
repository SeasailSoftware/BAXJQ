using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class FingerPrintOfDevice
    {
        public int DeviceId { get; set; }
        public List<FingerPrint> CurrrentFingerPrints { get; set; } = new List<FingerPrint>();
    }
}
