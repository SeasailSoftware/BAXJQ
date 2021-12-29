using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class FaceDataTask
    {
        public int RecId { get; set; }

        public int Type { get; set; }
        public int DeviceId { get; set; }

        public int EmpId { get; set; }
        public int UpdateFlag { get; set; }
    }
}
