using System.ComponentModel.DataAnnotations;

namespace HPT.Gate.Data.Model
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public string Sex { get; set; }

        public int DeptId { get; set; }
        public virtual Dept Dept { get; set; }
    }
}
