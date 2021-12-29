using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Entity
{
    public class Vacation
    {
        public int Vid { get; set; }

        public string VName { get; set; }

        public string VBeginDate { get; set; }

        public string VEndDate { get; set; }

        public string VDesc { get; set; }

        public int Status { get; set; }
    }
}
