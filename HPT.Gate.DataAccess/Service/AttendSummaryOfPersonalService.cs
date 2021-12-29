using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.DataAccess.Entity;

namespace HPT.Gate.DataAccess.Service
{
    public class AttendSummaryOfPersonalService
    {
        public static List<AttendSummaryOfPersonal> Find(int deptId, int deptType, string empCode, string empName, string beginDate, string endDate)
        {
            List<AttendSummaryOfPersonal> summarys = new List<AttendSummaryOfPersonal>();
            List<AttendDetail> details = AttendDetailService.Find(deptId, deptType, empCode, empName, beginDate, endDate);
            foreach (var value in details.GroupBy(p => p.EmpId))
            {
                AttendSummaryOfPersonal summary = new AttendSummaryOfPersonal();
                AttendDetail first = value.First();
                summary.DeptId = first.DeptId;
                summary.DeptName = first.DeptName;
                summary.EmpId = first.EmpId;
                summary.EmpCode = first.EmpCode;
                summary.EmpName = first.EmpName;
                summary.BeginDate = beginDate;
                summary.EndDate = endDate;
                foreach (AttendDetail detail in value)
                {
                    summary.ShouldAttd += detail.ShouldAttd;
                    summary.Attded += detail.Attded;
                    if (detail.Absent == 1)
                        summary.Absent += detail.ShouldAttd;
                    summary.LateMinutes += detail.LateMinutes;
                    summary.EarlyMinutes += detail.EarlyMinutes;
                    summary.OTMinutes += detail.OTMinutes;
                    summary.WorkMinutes += detail.WorkMinutes;
                    summary.ShouldSignIn += detail.ShouldSignIn;
                    summary.ShouldSignOut += detail.ShouldSignOut;
                    summary.SignIned += string.IsNullOrEmpty(detail.SignIn) ? 0 : 1;
                    summary.UnSignIn += string.IsNullOrEmpty(detail.SignIn) ? 1 : 0;
                    summary.SignOuted += string.IsNullOrEmpty(detail.SignOut) ? 0 : 1;
                    summary.UnSignOut += string.IsNullOrEmpty(detail.SignOut) ? 1 : 0;
                    summary.Leave += detail.Leave;
                }
                summarys.Add(summary);
            }
            return summarys;
        }
    }
}
