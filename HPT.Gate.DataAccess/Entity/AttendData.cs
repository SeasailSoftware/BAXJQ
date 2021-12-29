using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class AttendData
    {
        public int RecId { get; set; }

        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public int EmpId { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public int DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string CardNo { get; set; }

        public string RecordType
        {
            get
            {
                if (DeviceId == 0) return "补卡";
                string type = "刷卡";
                if (CardNo.Equals(EmpCode))
                    type = "指纹";
                return type;
            }
        }

        public DateTime RecDatetime { get; set; }

        public string RecDate
        {
            get
            {
                if (RecDatetime == null)
                    return string.Empty;
                return RecDatetime.ToString("yyyy-MM-dd");
            }
        }

        public string RecTime
        {
            get
            {
                if (RecDatetime == null)
                    return string.Empty;
                return RecDatetime.ToString("HH:mm");
            }
        }

        public string IOFlag { get; set; }

        public int Passed { get; set; }

        public string Week
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(RecDatetime.DayOfWeek);
            }
        }

    }
}
