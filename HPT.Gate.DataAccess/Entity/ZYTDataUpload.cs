using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class ZYTDataUpload
    {
        public int RecId { get; set; }

        public string Oper { get; set; }

        public int TaskType { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public string CardNo { get; set; }

        public string IDCard { get; set; }

        public string Phone { get; set; }

        public string SchoolCode { get; set; }

        public string DeptName { get; set; }

        public string CreateTime { get; set; }

        public string UploadTime { get; set; }

        public int UpdateFlag { get; set; }
    }
}
