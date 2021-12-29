
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class PhotoTask
    {
        #region properity

        /// <summary>
        /// 任务编号
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public UInt64 EmpId { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string EmpCode { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public Bitmap Photo { get; set; }

        /// <summary>
        /// 出入口标志
        /// </summary>
        public int IOFlag { get; set; }
        #endregion

    }
}
