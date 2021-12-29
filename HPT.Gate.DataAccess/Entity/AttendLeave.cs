using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class AttendLeave
    {
        public int RecId { get; set; }

        public int DeptId { get; set; }

        public string DeptName { get; set; }
        public int EmpId { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }
        public string BeginTime { get; set; }

        public string EndTime { get; set; }

        public int LeaveType { get; set; }

        public string LeaveName { get; set; }

        public string Remark { get; set; }

        public string CreateTime { get; set; }
    }
}
