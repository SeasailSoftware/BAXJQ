using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class DataSynTask
    {
        public int RecId { get; set; }

        public int EmpId { get; set; }

        public int DeviceId { get; set; }

        public int Rights { get; set; }

        public int UpdateFlag { get; set; }
    }
}
