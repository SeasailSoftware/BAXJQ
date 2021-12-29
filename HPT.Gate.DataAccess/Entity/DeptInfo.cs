using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class DeptInfo
    {
        public int DeptId { get; set; }

        public int ParDeptId { get; set; }

        public string DeptName { get; set; }

        public int DeptCode { get; set; }

        public int DeptCodeLength { get; set; }

        public int IsBindingEmpcode { get; set; }

        public int ImageIndex { get; set; }
    }
}
