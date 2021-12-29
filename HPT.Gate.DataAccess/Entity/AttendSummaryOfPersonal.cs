using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class AttendSummaryOfPersonal
    {
        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public int EmpId { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { get; set; }

        public double ShouldAttd { get; set; }

        public double Attded { get; set; }

        public int LateMinutes { get; set; }

        public int EarlyMinutes { get; set; }

        public double Absent { get; set; }

        public int OTMinutes { get; set; }

        public int ShouldSignIn { get; set; }

        public int SignIned { get; set; }

        public int UnSignIn { get; set; }

        public int ShouldSignOut { get; set; }

        public int SignOuted { get; set; }

        public int UnSignOut { get; set; }

        public int Leave { get; set; }


        public int WorkMinutes { get; set; }

    }
}
