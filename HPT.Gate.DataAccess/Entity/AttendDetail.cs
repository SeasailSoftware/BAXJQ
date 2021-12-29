using System;
using System.Collections.Generic;
using System.Text;

namespace HPT.Gate.DataAccess.Entity //修改名字空间
{
    public class AttendDetail
    {

        #region  propeties
        /// <summary>
        /// 编号
        /// </summary>
        public int RecId { get; set; }

        /// <summary>
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
        /// 日期
        /// </summary>
        public string RecDate { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string TimeOfSignIn { get; set; }

        public string TimeOfSignOut { get; set; }

        public string SignIn { get; set; }

        public string SignOut { get; set; }

        public double ShouldAttd { get; set; }

        public double Attded { get; set; }

        public int LateMinutes { get; set; }

        public int EarlyMinutes { get; set; }

        public int Absent { get; set; }

        public int Leave { get; set; }

        public int OTMinutes { get; set; }

        public int WorkMinutes { get; set; }

        public int ShouldSignIn { get; set; }

        public int ShouldSignOut { get; set; }
        public string Week
        {
            get
            {
                DateTime dt = Convert.ToDateTime(RecDate);
                return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dt.DayOfWeek);
            }
        }
        #endregion

    }
}