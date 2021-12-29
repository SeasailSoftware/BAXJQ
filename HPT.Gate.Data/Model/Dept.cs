using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPT.Gate.Data.Model
{
    public class Dept
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        [Key]
        public int DeptId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DeptCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 创建/修改时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        public int Modifyer { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
