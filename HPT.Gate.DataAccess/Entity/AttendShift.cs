using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class AttendShift
    {
        public int ShiftId { get; set; }
        public int ShiftType { get; set; }

        public string ShiftName { get; set; }

        public List<AttendShiftOfWeek> ShiftOfWeek { get; set; }

        public List<AttendShiftOfMonth> ShiftOfMonth { get; set; }
    }
}
