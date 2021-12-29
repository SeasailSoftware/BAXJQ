using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class AttdRecord
    {
        #region propeties
        public int RecId { get; set; }

        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public int EmpId { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string EmpCode { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 记录日期
        /// </summary>
        public string RecDate { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public string RecTime { get; set; }

        /// <summary>
        /// 进出标志
        /// </summary>
        public string IOFlag { get; set; }
        #endregion

    }
}
