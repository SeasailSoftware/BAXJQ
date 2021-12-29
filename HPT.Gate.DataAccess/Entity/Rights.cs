using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class Rights
    {
        public int DeviceId { get; set; }

        public string DeviceName { get; set; }

        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public int EmpId { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public string RightOfIn { get; set; }

        public string RightOfOut { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { get; set; }

    }
}
