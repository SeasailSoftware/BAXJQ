using System;
using System.Linq;

namespace HPT.Gate.DataAccess.Entity
{
    public class AttendShiftOfEmp
    {
        public int RecId { get; set; }

        public int EmpId { get; set; }

        public DateTime RecDate { get; set; }

        public AttendShift ShiftOfDate { get; set; }

        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public TimeGroupOfShift TimeGroup1
        {
            get
            {
                TimeGroupOfShift timeGroup = null;
                if (ShiftOfDate != null)
                {
                    switch (ShiftOfDate.ShiftType)
                    {
                        case 0:
                            AttendShiftOfWeek week = ShiftOfDate.ShiftOfWeek.FirstOrDefault(p => p.WeekId == (int)RecDate.DayOfWeek);
                            if (week != null)
                                timeGroup = week.TimeGroup1;
                            break;
                        case 1:
                            AttendShiftOfMonth month = ShiftOfDate.ShiftOfMonth.FirstOrDefault(p => p.DayId == RecDate.Day);
                            if (month != null)
                                timeGroup = month.TimeGroup1;
                            break;
                    }
                }

                return timeGroup;
            }
        }

        public TimeGroupOfShift TimeGroup2
        {
            get
            {
                TimeGroupOfShift timeGroup = null;
                if (ShiftOfDate != null)
                {
                    switch (ShiftOfDate.ShiftType)
                    {
                        case 0:
                            AttendShiftOfWeek week = ShiftOfDate.ShiftOfWeek.FirstOrDefault(p => p.WeekId == (int)RecDate.DayOfWeek);
                            if (week != null)
                                timeGroup = week.TimeGroup2;
                            break;
                        case 1:
                            AttendShiftOfMonth month = ShiftOfDate.ShiftOfMonth.FirstOrDefault(p => p.DayId == RecDate.Day);
                            if (month != null)
                                timeGroup = month.TimeGroup2;
                            break;
                    }
                }
                return timeGroup;
            }
        }

        public TimeGroupOfShift TimeGroup3
        {
            get
            {
                TimeGroupOfShift timeGroup = null;
                if (ShiftOfDate != null)
                {
                    switch (ShiftOfDate.ShiftType)
                    {
                        case 0:
                            AttendShiftOfWeek week = ShiftOfDate.ShiftOfWeek.FirstOrDefault(p => p.WeekId == (int)RecDate.DayOfWeek);
                            if (week != null)
                                timeGroup = week.TimeGroup3;
                            break;
                        case 1:
                            AttendShiftOfMonth month = ShiftOfDate.ShiftOfMonth.FirstOrDefault(p => p.DayId == RecDate.Day);
                            if (month != null)
                                timeGroup = month.TimeGroup3;
                            break;
                    }
                }
                return timeGroup;
            }
        }
        public DateTime BeginTime1
        {
            get
            {
                DateTime dt = DateTime.MinValue;
                if (TimeGroup1 != null)
                    dt = Convert.ToDateTime($"{RecDate.ToString("yyyy-MM-dd")} {TimeGroup1.Time1}");
                return dt;
            }
        }

        public DateTime EndTime1
        {
            get
            {
                DateTime dt = DateTime.MinValue;
                if (TimeGroup1 != null)
                    dt = Convert.ToDateTime($"{RecDate.ToString("yyyy-MM-dd")} {TimeGroup1.Time2}");
                return dt;
            }
        }

        public DateTime BeginTime2
        {
            get
            {
                DateTime dt = DateTime.MinValue;
                if (TimeGroup2 != null)
                    dt = Convert.ToDateTime($"{RecDate.ToString("yyyy-MM-dd")} {TimeGroup2.Time1}");
                return dt;
            }
        }

        public DateTime EndTime2
        {
            get
            {
                DateTime dt = DateTime.MinValue;
                if (TimeGroup2 != null)
                    dt = Convert.ToDateTime($"{RecDate.ToString("yyyy-MM-dd")} {TimeGroup2.Time2}");
                return dt;
            }
        }

        public DateTime BeginTime3
        {
            get
            {
                DateTime dt = DateTime.MinValue;
                if (TimeGroup3 != null)
                    dt = Convert.ToDateTime($"{RecDate.ToString("yyyy-MM-dd")} {TimeGroup3.Time1}");
                return dt;
            }
        }

        public DateTime EndTime3
        {
            get
            {
                DateTime dt = DateTime.MinValue;
                if (TimeGroup3 != null)
                    dt = Convert.ToDateTime($"{RecDate.ToString("yyyy-MM-dd")} {TimeGroup3.Time2}");
                return dt;
            }
        }

    }
}
