using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class FingerPrintTask
    {
        public int RecId { get; set; }

        public int EmpId { get; set; }

        public int FingerId { get; set; }

        public byte[] FingerData { get; set; }

        public int UpdateFlag { get; set; }
    }
}
