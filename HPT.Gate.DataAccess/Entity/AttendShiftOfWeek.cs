using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class AttendShiftOfWeek
    {

        public int ShiftId { get; set; }

        public int WeekId { get; set; }

        public TimeGroupOfShift TimeGroup1 { get; set; }

        public TimeGroupOfShift TimeGroup2 { get; set; }

        public TimeGroupOfShift TimeGroup3 { get; set; }



    }
}
