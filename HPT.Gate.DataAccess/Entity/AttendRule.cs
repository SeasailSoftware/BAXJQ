using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class AttendRule
    {
        public string FullName { get; set; }

        public string ReferName { get; set; }

        public int MonthStart { get; set; }

        public int WeekStart { get; set; }

        public int AcrossDay { get; set; }

        public int OutStatus { get; set; }

        public int OTStatus { get; set; }

        public int MaxShift { get; set; }

        public int MinShift { get; set; }

        public int EffectAttendInterval { get; set; }

        public int WorkDayMinute { get; set; }

        public int LateMinute { get; set; }

        public int EarlyMinute { get; set; }

        public int NoSignInEnabled { get; set; }

        public int NoSignInType { get; set; }

        public int NoSignInMinute { get; set; }

        public int NoSignOutEnabled { get; set; }

        public int NoSignOutType { get; set; }

        public int NoSignOutMinute { get; set; }

        public int OverSignInEnabled { get; set; }

        public int OverSignInMinute { get; set; }

        public int BeforeSignOutEnabled { get; set; }

        public int BeforeSignOutMinute { get; set; }

        public int OverSignOutEnabled { get; set; }

        public int OverSignOutInterval { get; set; }

        public int OverSignOutOTMinute { get; set; }

        public int LimitMaxOTAfterSignOutEnabled { get; set; }

        public int LimitMaxOTAfterSignOutMinute { get; set; }

        public int BeforeSignInEnabled { get; set; }

        public int BeforeSignInInterval { get; set; }
        public int BeforeSignInOTMinute { get; set; }

        public int LimitMaxOTBeforeSignInEnabled { get; set; }

        public int LimitMaxOTBeforeSignInMinute { get; set; }

        public int LimitTotalOTMaxEnabled { get; set; }

        public int LimitTotalOTMaxMinute { get; set; }

        public int Week0 { get; set; }

        public int Week1 { get; set; }

        public int Week2 { get; set; }
        public int Week3 { get; set; }

        public int Week4 { get; set; }

        public int Week5 { get; set; }

        public int Week6 { get; set; }


        public int WeekendAllDayOT { get; set; }



    }
}
