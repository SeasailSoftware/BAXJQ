using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class AttendResult
    {
        public string SignIn { get; set; }

        public string SignOut { get; set; }

        public double Attded { get; set; }

        public int LateMinute { get; set; }

        public int EarlyMinutes { get; set; }

        public int Absent { get; set; }

        public int OTMinutes { get; set; }

        public int WorkMinutes { get; set; }

    }
}
