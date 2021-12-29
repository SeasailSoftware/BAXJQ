using System;
using System.Collections.Generic;
using System.Linq;
using HPT.Gate.Client.config;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;

namespace hpt.gate
{
    public class AttendManager
    {
        /// <summary>
        /// 处理人员连续日期内考勤
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        public static void ProAttendDataOfEmp(EmpInfo emp, string beginDate, string endDate, string beginTime, int secondDay, string endTime)
        {
            DateTime dtBegin = Convert.ToDateTime(beginDate);
            DateTime dtEnd = Convert.ToDateTime(endDate);
            for (DateTime d = dtBegin; d <= dtEnd; d = d.AddDays(1))
            {
                ProcAttendDataOfEmpByDay(emp, d, beginTime, secondDay, endTime);
            }
        }
        /// <summary>
        /// 处理人员具体那一天的考勤数据
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="d"></param>
        public static void ProcAttendDataOfEmpByDay(EmpInfo emp, DateTime date, string beginTime, int secondDay, string endTime)
        {

        }



        #region 获取相反的进出标志
        private static string GetRevertIOFlag(string ioFlag)
        {
            string flag = "";
            switch (ioFlag)
            {
                case "进":
                    flag = "出";
                    break;
                case "出":
                    flag = "进";
                    break;
            }
            return flag;
        }
        #endregion


        #region 处理人员当天没有考勤记录
        /// <summary>
        /// 处理人员当天没有考勤记录
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="date"></param>
        private static void ProcAttendDataOfEmpByDayOnRecord(EmpInfo emp, DateTime date)
        {
            AttdService.InsertAttendDetailNoRecord(emp.EmpId, date.ToString("yyyy-MM-dd"));
        }
        #endregion


        /// <summary>
        /// 获取时间段内分析结果
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="shift"></param>
        /// <param name="attendRule"></param>
        /// <returns></returns>
        public static AttendResult GetAttendProcResult(string beginTime, string endTime, Shift shift, AttdRule attendRule, int index)
        {
            AttendResult attendResult = new AttendResult();
            bool procFlag = true;
            switch (index)
            {
                case 1:
                    procFlag = !shift.BeginTime1.Equals(string.Empty);
                    break;
                case 2:
                    procFlag = !shift.BeginTime2.Equals(string.Empty);
                    break;
                case 3:
                    procFlag = !shift.BeginTime3.Equals(string.Empty);
                    break;
            }


            ///如果上下班都没有记录
            if (beginTime.Equals(string.Empty) && endTime.Equals(string.Empty))
            {
                switch (index)
                {
                    case 1:
                        attendResult.Absent = Convert.ToDateTime(shift.EndTime1).Subtract(Convert.ToDateTime(shift.BeginTime1)).Duration().Minutes;
                        attendResult.Absent += Convert.ToDateTime(shift.EndTime1).Subtract(Convert.ToDateTime(shift.BeginTime1)).Duration().Hours * 60;
                        break;
                    case 2:
                        attendResult.Absent = Convert.ToDateTime(shift.EndTime2).Subtract(Convert.ToDateTime(shift.BeginTime2)).Duration().Minutes;
                        attendResult.Absent += Convert.ToDateTime(shift.EndTime2).Subtract(Convert.ToDateTime(shift.BeginTime2)).Duration().Hours * 60;
                        break;
                    case 3:
                        attendResult.Absent = Convert.ToDateTime(shift.EndTime3).Subtract(Convert.ToDateTime(shift.BeginTime3)).Duration().Minutes;
                        attendResult.Absent = Convert.ToDateTime(shift.EndTime3).Subtract(Convert.ToDateTime(shift.BeginTime3)).Duration().Hours * 60;
                        break;
                }
                return attendResult;
            }

            ///只有上班没有记录
            if (beginTime.Equals(string.Empty))
            {
                /*
                if (attendRule.NoSignIn == 1)
                {
                    switch (attendRule.NoSignInType)
                    {
                        case 0:
                            attendResult.Late = attendRule.NoSignInMInute;
                            break;
                        case 1:
                            attendResult.Absent = attendRule.NoSignInMInute;
                            break;
                    }
                }
                */
                return attendResult;
            }

            ///只有下班卡没有记录
            if (endTime.Equals(string.Empty))
            {
                /*
                if (attendRule.NoSignOut == 1)
                {
                    switch (attendRule.NoSignOutType)
                    {
                        case 0:
                            attendResult.LeaveEarly = attendRule.NoSignOutMinute;
                            break;
                        case 1:
                            attendResult.Absent = attendRule.NoSignOutMinute;
                            break;
                    }
                }
                */
                return attendResult;
            }

            ///上下班时间都不为空

            DateTime dtBegin = Convert.ToDateTime(beginTime);
            DateTime dtEnd = Convert.ToDateTime(endTime);
            DateTime sdtBegin = Convert.ToDateTime(shift.BeginTime1);
            DateTime sdtEnd = Convert.ToDateTime(shift.EndTime1);
            switch (index)
            {
                case 1:
                    sdtBegin = Convert.ToDateTime(shift.BeginTime1);
                    sdtEnd = Convert.ToDateTime(shift.EndTime1);
                    break;
                case 2:
                    sdtBegin = Convert.ToDateTime(shift.BeginTime2);
                    sdtEnd = Convert.ToDateTime(shift.EndTime2);
                    break;
                case 3:
                    sdtBegin = Convert.ToDateTime(shift.BeginTime2);
                    sdtEnd = Convert.ToDateTime(shift.EndTime2);
                    break;
            }
            ///计算迟到
            if (DateTime.Compare(dtBegin, sdtBegin) > 0)
            {

            }
            ///计算早退
            if (DateTime.Compare(dtEnd, sdtEnd) < 0)
            {

            }

            return attendResult;
        }

        /// <summary>
        /// 获取时间段内分析结果
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="shift"></param>
        /// <param name="attendRule"></param>
        /// <returns></returns>
        public static AttendResult GetAttendProcResult(string beginTime, string endTime, Shift shift, AttdRule attendRule, int index, DateTime date)
        {
            AttendResult attendResult = new AttendResult();
            ///先设置班次
            DateTime bTime = Convert.ToDateTime(shift.BeginTime1);
            shift.BeginTime1 = Convert.ToDateTime(date.ToString("yyyy-MM-dd" + " " + shift.BeginTime1)).ToString("yyyy-MM-dd HH:mm");
            DateTime eTime1 = Convert.ToDateTime(shift.EndTime1);
            if (DateTime.Compare(bTime, eTime1) < 0)
            {
                shift.EndTime1 = Convert.ToDateTime(date.ToString("yyyy-MM-dd" + " " + shift.EndTime1)).ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                shift.EndTime1 = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd" + " " + shift.EndTime1)).ToString("yyyy-MM-dd HH:mm");
            }

            if (!shift.BeginTime2.Equals(string.Empty))
            {
                DateTime bTime2 = Convert.ToDateTime(shift.BeginTime2);
                if (DateTime.Compare(bTime, bTime2) < 0)
                {
                    shift.BeginTime2 = Convert.ToDateTime(date.ToString("yyyy-MM-dd" + " " + shift.BeginTime2)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    shift.BeginTime2 = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd" + " " + shift.BeginTime2)).ToString("yyyy-MM-dd HH:mm");
                }
                DateTime eTime2 = Convert.ToDateTime(shift.EndTime2);
                if (DateTime.Compare(bTime, eTime2) < 0)
                {
                    shift.EndTime2 = Convert.ToDateTime(date.ToString("yyyy-MM-dd" + " " + shift.EndTime2)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    shift.EndTime2 = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd" + " " + shift.EndTime2)).ToString("yyyy-MM-dd HH:mm");
                }
            }
            if (!shift.BeginTime3.Equals(string.Empty))
            {
                DateTime bTime3 = Convert.ToDateTime(shift.BeginTime3);
                if (DateTime.Compare(bTime, bTime3) < 0)
                {
                    shift.BeginTime3 = Convert.ToDateTime(date.ToString("yyyy-MM-dd" + " " + shift.BeginTime3)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    shift.BeginTime3 = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd" + " " + shift.BeginTime3)).ToString("yyyy-MM-dd HH:mm");
                }
                DateTime eTime3 = Convert.ToDateTime(shift.EndTime3);
                if (DateTime.Compare(bTime, eTime3) < 0)
                {
                    shift.EndTime3 = Convert.ToDateTime(date.ToString("yyyy-MM-dd" + " " + shift.EndTime3)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    shift.EndTime3 = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd" + " " + shift.EndTime3)).ToString("yyyy-MM-dd HH:mm");
                }
            }

            bool procFlag = true;
            switch (index)
            {
                case 1:
                    procFlag = !shift.BeginTime1.Equals(string.Empty);
                    break;
                case 2:
                    procFlag = !shift.BeginTime2.Equals(string.Empty);
                    break;
                case 3:
                    procFlag = !shift.BeginTime3.Equals(string.Empty);
                    break;
            }
            if (!procFlag)
            {

                return attendResult;
            }
            ///班次时间不为空


            ///如果上下班都没有记录
            if (beginTime.Equals(string.Empty) && endTime.Equals(string.Empty))
            {
                switch (index)
                {
                    case 1:
                        attendResult.Absent = Convert.ToDateTime(shift.EndTime1).Subtract(Convert.ToDateTime(shift.BeginTime1)).Duration().Minutes;
                        attendResult.Absent += Convert.ToDateTime(shift.EndTime1).Subtract(Convert.ToDateTime(shift.BeginTime1)).Duration().Hours * 60;
                        break;
                    case 2:
                        attendResult.Absent = Convert.ToDateTime(shift.EndTime2).Subtract(Convert.ToDateTime(shift.BeginTime2)).Duration().Minutes;
                        attendResult.Absent += Convert.ToDateTime(shift.EndTime2).Subtract(Convert.ToDateTime(shift.BeginTime2)).Duration().Hours * 60;
                        break;
                    case 3:
                        attendResult.Absent = Convert.ToDateTime(shift.EndTime3).Subtract(Convert.ToDateTime(shift.BeginTime3)).Duration().Minutes;
                        attendResult.Absent = Convert.ToDateTime(shift.EndTime3).Subtract(Convert.ToDateTime(shift.BeginTime3)).Duration().Hours * 60;
                        break;
                }
                return attendResult;
            }

            ///只有上班没有记录
            if (beginTime.Equals(string.Empty))
            {
                /*
                if (attendRule.NoSignIn == 1)
                {
                    switch (attendRule.NoSignInType)
                    {
                        case 0:
                            attendResult.Late = attendRule.NoSignInMInute;
                            break;
                        case 1:
                            attendResult.Absent = attendRule.NoSignInMInute;
                            break;
                    }
                }
                */
                return attendResult;
            }

            ///只有下班卡没有记录
            if (endTime.Equals(string.Empty))
            {
                /*
                if (attendRule.NoSignOut == 1)
                {
                    switch (attendRule.NoSignOutType)
                    {
                        case 0:
                            attendResult.LeaveEarly = attendRule.NoSignOutMinute;
                            break;
                        case 1:
                            attendResult.Absent = attendRule.NoSignOutMinute;
                            break;
                    }
                }
                */
                return attendResult;
            }

            ///上下班时间都不为空

            DateTime dtBegin = Convert.ToDateTime(beginTime);
            DateTime dtEnd = Convert.ToDateTime(endTime);
            DateTime sdtBegin = Convert.ToDateTime(shift.BeginTime1);
            DateTime sdtEnd = Convert.ToDateTime(shift.EndTime1);
            switch (index)
            {
                case 1:
                    sdtBegin = Convert.ToDateTime(shift.BeginTime1);
                    sdtEnd = Convert.ToDateTime(shift.EndTime1);
                    break;
                case 2:
                    sdtBegin = Convert.ToDateTime(shift.BeginTime2);
                    sdtEnd = Convert.ToDateTime(shift.EndTime2);
                    break;
                case 3:
                    sdtBegin = Convert.ToDateTime(shift.BeginTime2);
                    sdtEnd = Convert.ToDateTime(shift.EndTime2);
                    break;
            }
            ///计算迟到
            if (DateTime.Compare(dtBegin, sdtBegin) > 0)
            {

            }
            ///计算早退
            if (DateTime.Compare(dtEnd, sdtEnd) < 0)
            {

            }

            return attendResult;
        }

        /// <summary>
        /// 获取下班时间
        /// </summary>
        /// <param name="timeList"></param>
        /// <param name="shift"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetEndTime(List<DateTime> timeList, Shift shift, int index, DateTime date)
        {
            DateTime bTime = Convert.ToDateTime(shift.BeginTime1);
            DateTime time;
            string endTime = string.Empty;
            int _BeforeEffect;
            int _BehindEffect;
            switch (index)
            {
                case 1:
                    if (shift.EndTime1.Equals(string.Empty))
                    {
                        return endTime;
                    }
                    time = Convert.ToDateTime(shift.EndTime1);
                    if (DateTime.Compare(time, bTime) >= 0)
                    {
                        time = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + shift.EndTime1);
                    }
                    else
                    {
                        time = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd") + " " + shift.EndTime1);
                    }
                    _BeforeEffect = shift.End_PreEffect1;
                    _BehindEffect = shift.End_BehindEffect1;
                    break;
                case 2:
                    if (shift.EndTime2.Equals(string.Empty))
                    {
                        return endTime;
                    }
                    time = Convert.ToDateTime(shift.EndTime2);
                    if (DateTime.Compare(time, bTime) >= 0)
                    {
                        time = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + shift.EndTime2);
                    }
                    else
                    {
                        time = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd") + " " + shift.EndTime2);
                    }
                    _BeforeEffect = shift.End_PreEffect2;
                    _BehindEffect = shift.End_BehindEffect2;
                    break;
                case 3:
                    if (shift.EndTime3.Equals(string.Empty))
                    {
                        return endTime;
                    }
                    time = Convert.ToDateTime(shift.EndTime3);
                    if (DateTime.Compare(time, bTime) >= 0)
                    {
                        time = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + shift.EndTime3);
                    }
                    else
                    {
                        time = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd") + " " + shift.EndTime3);
                    }
                    _BeforeEffect = shift.End_PreEffect3;
                    _BehindEffect = shift.End_BehindEffect3;
                    break;
                default:
                    time = Convert.ToDateTime(shift.EndTime1);
                    if (DateTime.Compare(time, bTime) >= 0)
                    {
                        time = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + shift.EndTime1);
                    }
                    else
                    {
                        time = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd") + " " + shift.EndTime1);
                    }
                    _BeforeEffect = shift.End_PreEffect1;
                    _BehindEffect = shift.End_BehindEffect1;
                    break;
            }

            foreach (DateTime dt in timeList)
            {
                if ((dt.Subtract(time).Duration().Hours * 60 + dt.Subtract(time).Duration().Minutes) <= _BehindEffect && DateTime.Compare(dt, time) >= 0)
                {
                    endTime = dt.ToString("yyyy-MM-dd HH:mm");
                    break;
                }
            }
            foreach (DateTime dt in timeList)
            {
                if ((time.Subtract(dt).Duration().Hours * 60 + time.Subtract(dt).Duration().Minutes) <= _BehindEffect && DateTime.Compare(dt, time) <= 0)
                {
                    endTime = dt.ToString("yyyy-MM-dd HH:mm");
                    break;
                }
            }

            return endTime;
        }

        /// <summary>
        /// 获取下班时间
        /// </summary>
        /// <param name="timeList"></param>
        /// <param name="shift"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetEndTime(List<DateTime> timeList, Shift shift, int index)
        {
            DateTime time;
            string endTime = string.Empty;
            int _BeforeEffect;
            int _BehindEffect;
            switch (index)
            {
                case 1:
                    if (shift.EndTime1.Equals(string.Empty))
                    {
                        return endTime;
                    }
                    time = Convert.ToDateTime(shift.EndTime1);
                    _BeforeEffect = shift.End_PreEffect1;
                    _BehindEffect = shift.End_BehindEffect1;
                    break;
                case 2:
                    if (shift.EndTime2.Equals(string.Empty))
                    {
                        return endTime;
                    }
                    time = Convert.ToDateTime(shift.EndTime2);
                    _BeforeEffect = shift.End_PreEffect2;
                    _BehindEffect = shift.End_BehindEffect2;
                    break;
                case 3:
                    if (shift.EndTime3.Equals(string.Empty))
                    {
                        return endTime;
                    }
                    time = Convert.ToDateTime(shift.EndTime3);
                    _BeforeEffect = shift.End_PreEffect3;
                    _BehindEffect = shift.End_BehindEffect3;
                    break;
                default:
                    time = Convert.ToDateTime(shift.EndTime1);
                    _BeforeEffect = shift.End_PreEffect1;
                    _BehindEffect = shift.End_BehindEffect1;
                    break;
            }

            foreach (DateTime dt in timeList)
            {
                if ((dt.Subtract(time).Duration().Hours * 60 + dt.Subtract(time).Duration().Minutes) <= _BehindEffect && DateTime.Compare(dt, time) >= 0)
                {
                    endTime = dt.ToString("HH:mm");
                    break;
                }
            }
            if (endTime.Equals(string.Empty))
            {
                foreach (DateTime dt in timeList)
                {
                    if ((time.Subtract(dt).Duration().Hours * 60 + time.Subtract(dt).Duration().Minutes) <= _BehindEffect && DateTime.Compare(dt, time) <= 0)
                    {
                        endTime = dt.ToString("HH:mm");
                        break;
                    }
                }
            }

            return endTime;
        }
        /// <summary>
        /// 获取上班时间
        /// </summary>
        /// <param name="timeList"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        private static string GetBeginTime(List<DateTime> timeList, Shift shift, int index, DateTime date)
        {
            DateTime bTime = Convert.ToDateTime(shift.BeginTime1);
            DateTime time;
            string beginTime = string.Empty;
            int _BeforeEffect;
            int _BehindEffect;

            switch (index)
            {
                case 1:
                    if (shift.BeginTime1.Equals(string.Empty))
                    {
                        return beginTime;
                    }
                    time = Convert.ToDateTime(shift.BeginTime1);
                    if (DateTime.Compare(time, bTime) >= 0)
                    {
                        time = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + shift.BeginTime1);
                    }
                    else
                    {
                        time = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd") + " " + shift.BeginTime1);
                    }
                    _BeforeEffect = shift.Begin_PreEffect1;
                    _BehindEffect = shift.Begin_BehindEffect1;
                    break;
                case 2:
                    if (shift.BeginTime2.Equals(string.Empty))
                    {
                        return beginTime;
                    }
                    time = Convert.ToDateTime(shift.BeginTime2);
                    if (DateTime.Compare(time, bTime) >= 0)
                    {
                        time = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + shift.BeginTime2);
                    }
                    else
                    {
                        time = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd") + " " + shift.BeginTime2);
                    }
                    _BeforeEffect = shift.Begin_PreEffect2;
                    _BehindEffect = shift.Begin_BehindEffect2;
                    break;
                case 3:
                    if (shift.BeginTime3.Equals(string.Empty))
                    {
                        return beginTime;
                    }
                    time = Convert.ToDateTime(shift.BeginTime3);
                    if (DateTime.Compare(time, bTime) >= 0)
                    {
                        time = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + shift.BeginTime3);
                    }
                    else
                    {
                        time = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd") + " " + shift.BeginTime3);
                    }
                    _BeforeEffect = shift.Begin_PreEffect3;
                    _BehindEffect = shift.Begin_BehindEffect3;
                    break;
                default:
                    time = Convert.ToDateTime(shift.BeginTime1);
                    if (DateTime.Compare(time, bTime) >= 0)
                    {
                        time = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + shift.BeginTime1);
                    }
                    else
                    {
                        time = Convert.ToDateTime(date.AddDays(1).ToString("yyyy-MM-dd") + " " + shift.BeginTime1);
                    }
                    _BeforeEffect = shift.Begin_PreEffect1;
                    _BehindEffect = shift.Begin_BehindEffect1;
                    break;
            }
            ///先从前有效时间段去取数据
            foreach (DateTime dt in timeList)
            {
                if (DateTime.Compare(dt, time) <= 0 && (time.Subtract(dt).Duration().Hours * 60 + time.Subtract(dt).Duration().Minutes) <= _BeforeEffect)
                {
                    beginTime = dt.ToString("yyyy-MM-dd HH:mm");
                    break;
                }
            }
            ///如果没有再从后有效时间段去取数据
            if (beginTime.Equals(string.Empty))
            {
                foreach (DateTime dt in timeList)
                {
                    if (DateTime.Compare(dt, time) >= 0 && (time.Subtract(dt).Duration().Hours * 60 + dt.Subtract(time).Duration().Minutes) <= _BehindEffect)
                    {
                        beginTime = dt.ToString("yyyy-MM-dd HH:mm");
                        break;
                    }
                }
            }

            return beginTime;
        }
        /// <summary>
        /// 获取上班时间
        /// </summary>
        /// <param name="timeList"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        private static string GetBeginTime(List<DateTime> timeList, Shift shift, int index)
        {
            DateTime time;
            string beginTime = string.Empty;
            int _BeforeEffect;
            int _BehindEffect;

            switch (shift.SType)
            {
                case 0:
                    switch (index)
                    {
                        case 1:
                            if (shift.BeginTime1.Equals(string.Empty))
                            {
                                return beginTime;
                            }
                            time = Convert.ToDateTime(shift.BeginTime1);
                            _BeforeEffect = shift.Begin_PreEffect1;
                            _BehindEffect = shift.Begin_BehindEffect1;
                            break;
                        case 2:
                            if (shift.BeginTime2.Equals(string.Empty))
                            {
                                return beginTime;
                            }
                            time = Convert.ToDateTime(shift.BeginTime2);
                            _BeforeEffect = shift.Begin_PreEffect2;
                            _BehindEffect = shift.Begin_BehindEffect2;
                            break;
                        case 3:
                            if (shift.BeginTime3.Equals(string.Empty))
                            {
                                return beginTime;
                            }
                            time = Convert.ToDateTime(shift.BeginTime3);
                            _BeforeEffect = shift.Begin_PreEffect3;
                            _BehindEffect = shift.Begin_BehindEffect3;
                            break;
                        default:
                            time = Convert.ToDateTime(shift.BeginTime1);
                            _BeforeEffect = shift.Begin_PreEffect1;
                            _BehindEffect = shift.Begin_BehindEffect1;
                            break;
                    }
                    ///先从前有效时间段去取数据
                    foreach (DateTime dt in timeList)
                    {
                        if (DateTime.Compare(dt, time) <= 0 && (time.Subtract(dt).Duration().Hours * 60 + time.Subtract(dt).Duration().Minutes) <= _BeforeEffect)
                        {
                            beginTime = dt.ToString("HH:mm");
                            break;
                        }
                    }
                    ///如果没有再从后有效时间段去取数据
                    if (beginTime.Equals(string.Empty))
                    {
                        foreach (DateTime dt in timeList)
                        {
                            if (DateTime.Compare(dt, time) >= 0 && (time.Subtract(dt).Duration().Hours * 60 + dt.Subtract(time).Duration().Minutes) <= _BehindEffect)
                            {
                                beginTime = dt.ToString("HH:mm");
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    switch (index)
                    {
                        case 1:
                            if (shift.BeginTime1.Equals(string.Empty))
                            {
                                return beginTime;
                            }
                            time = Convert.ToDateTime(shift.BeginTime1);
                            _BeforeEffect = shift.Begin_PreEffect1;
                            _BehindEffect = shift.Begin_BehindEffect1;
                            break;
                        case 2:
                            if (shift.BeginTime2.Equals(string.Empty))
                            {
                                return beginTime;
                            }
                            time = Convert.ToDateTime(shift.BeginTime2);
                            _BeforeEffect = shift.Begin_PreEffect2;
                            _BehindEffect = shift.Begin_BehindEffect2;
                            break;
                        case 3:
                            if (shift.BeginTime3.Equals(string.Empty))
                            {
                                return beginTime;
                            }
                            time = Convert.ToDateTime(shift.BeginTime3);
                            _BeforeEffect = shift.Begin_PreEffect3;
                            _BehindEffect = shift.Begin_BehindEffect3;
                            break;
                        default:
                            time = Convert.ToDateTime(shift.BeginTime1);
                            _BeforeEffect = shift.Begin_PreEffect1;
                            _BehindEffect = shift.Begin_BehindEffect1;
                            break;
                    }
                    break;
            }
            return beginTime;
        }


        #region 考勤分析处理
        internal static void Proc(EmpInfo emp, List<AttendData> dataList, List<AttendShiftOfEmp> shiftList, AttendRule rule, string beginDate, string endDate)
        {
            List<AttendDetail> detailList = new List<AttendDetail>();
            DateTime dtBegin = Convert.ToDateTime(beginDate);
            DateTime dtEnd = Convert.ToDateTime(endDate);
            for (DateTime dt = dtBegin; dt < dtEnd; dt = dt.AddDays(1))
            {
                List<AttendDetail> details = new List<AttendDetail>();
                string currentDate = dt.ToString("yyyy-MM-dd");
                AttendShiftOfEmp shift = shiftList.FirstOrDefault(p => currentDate.Equals(p.RecDate.ToString("yyyy-MM-dd")));
                if (shift != null)
                {
                    AttendDetail attendDetail1 = GetAttendDetailOfTimeGroup(emp, dataList, shift.TimeGroup1, rule, dt);
                    AttendDetail attendDetail2 = GetAttendDetailOfTimeGroup(emp, dataList, shift.TimeGroup2, rule, dt);
                    AttendDetail attendDetail3 = GetAttendDetailOfTimeGroup(emp, dataList, shift.TimeGroup3, rule, dt);
                    if (attendDetail1 != null)
                        detailList.Add(attendDetail1);
                    if (attendDetail2 != null)
                        detailList.Add(attendDetail2);
                    if (attendDetail3 != null)
                        detailList.Add(attendDetail3);
                }
            }
            AttendDetailService.Del(emp.EmpId, beginDate, endDate);
            try
            {
                AttendDetailService.Insert(detailList, $"Data Source = {AppSettings.ServerName}; Initial Catalog ={AppSettings.DBName};Integrated Security=false;Persist Security Info=False;User Id = {AppSettings.UserName}; Password = {AppSettings.Password};");
            }
            catch (Exception ex)
            {

            }
            //todo
        }
        #endregion

        #region 生成考勤明细数据
        private static AttendDetail GetAttendDetailOfTimeGroup(EmpInfo emp, List<AttendData> attendDatas, TimeGroupOfShift timeGroup, AttendRule rule, DateTime dt)
        {
            if (timeGroup == null) return null;
            AttendDetail detail = new AttendDetail();
            detail.DeptId = emp.DeptId;
            detail.DeptName = emp.DeptName;
            detail.EmpId = emp.EmpId;
            detail.EmpCode = emp.EmpCode;
            detail.EmpName = emp.EmpName;
            detail.RecDate = dt.ToString("yyyy-MM-dd");
            detail.GroupId = timeGroup.GroupId;
            detail.GroupName = timeGroup.GroupName;
            detail.TimeOfSignIn = timeGroup.Time1;
            detail.TimeOfSignOut = timeGroup.Time2;
            detail.SignIn = string.Empty;
            detail.SignOut = string.Empty;
            detail.ShouldAttd = timeGroup.Day;
            detail.Attded = 0.0;
            detail.LateMinutes = 0;
            detail.EarlyMinutes = 0;
            detail.Absent = 0;
            detail.OTMinutes = 0;
            detail.ShouldSignIn = timeGroup.MustSignIn;
            detail.ShouldSignOut = timeGroup.MustSignOut;
            //获取考勤计算结果
            AttendResult result = GetAttendResult(attendDatas, timeGroup, rule, dt);
            if (result != null)
            {
                detail.SignIn = result.SignIn;
                detail.SignOut = result.SignOut;
                detail.Attded = result.Attded;
                detail.LateMinutes = result.LateMinute;
                detail.EarlyMinutes = result.EarlyMinutes;
                detail.Absent = result.Absent;
                detail.OTMinutes = result.OTMinutes;
                detail.WorkMinutes = result.WorkMinutes;
            }
            return detail;
        }

        #endregion

        #region 获取签到时间
        private static AttendResult GetAttendResult(List<AttendData> attendDatas, TimeGroupOfShift timeGroup, AttendRule rule, DateTime dt)
        {

            AttendResult result = new AttendResult();
            result.SignIn = string.Empty;
            result.SignOut = string.Empty;
            result.Attded = 0.0;
            result.LateMinute = 0;
            result.EarlyMinutes = 0;
            result.Absent = 0;
            result.OTMinutes = 0;
            result.WorkMinutes = 0;
            string timeOfSignIn = string.Empty;
            string timeOfSignOut = string.Empty;

            #region 处理时间段
            DateTime dtBegin;
            DateTime dtSignIn;
            DateTime dtEnd;
            DateTime begin;
            DateTime dtSignOut;
            DateTime end;
            List<AttendData> dataList = new List<AttendData>();
            switch (timeGroup.Across)
            {
                case 0:
                    dtBegin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime1}");
                    dtSignIn = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time1}");
                    dtEnd = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.EndTime1}");
                    begin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime2}");
                    dtSignOut = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time2}");
                    end = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.EndTime2}");
                    attendDatas = attendDatas.Where(p => p.RecDate.Equals(dt.ToString("yyyy-MM-dd"))).ToList();
                    break;
                case 1:
                    dtBegin = Convert.ToDateTime($"{dt.AddDays(-1).ToString("yyyy-MM-dd")} {timeGroup.BeginTime1}");
                    dtSignIn = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time1}");
                    dtEnd = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.EndTime1}");
                    begin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime2}");
                    dtSignOut = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time2}");
                    end = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.EndTime2}");
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.ToString("yyyy-MM-dd"))).ToList());
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.AddDays(-1).ToString("yyyy-MM-dd"))).ToList());
                    attendDatas = dataList;
                    break;
                case 2:
                    dtBegin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime1}");
                    dtSignIn = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time1}");
                    dtEnd = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.EndTime1}");
                    begin = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.BeginTime2}");
                    dtSignOut = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.Time2}");
                    end = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.EndTime2}");
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.ToString("yyyy-MM-dd"))).ToList());
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.AddDays(1).ToString("yyyy-MM-dd"))).ToList());
                    attendDatas = dataList;
                    break;
                case 3:
                    dtBegin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime1}");
                    dtSignIn = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time1}");
                    dtEnd = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.EndTime1}");
                    begin = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.BeginTime2}");
                    dtSignOut = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.Time2}");
                    end = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.EndTime2}");
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.ToString("yyyy-MM-dd"))).ToList());
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.AddDays(1).ToString("yyyy-MM-dd"))).ToList());
                    attendDatas = dataList;
                    break;
                case 4:
                    dtBegin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime1}");
                    dtSignIn = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time1}");
                    dtEnd = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.EndTime1}");
                    begin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime2}");
                    dtSignOut = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.Time2}");
                    end = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.EndTime2}");
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.ToString("yyyy-MM-dd"))).ToList());
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.AddDays(1).ToString("yyyy-MM-dd"))).ToList());
                    attendDatas = dataList;
                    break;
                case 5:
                    dtBegin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime1}");
                    dtSignIn = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time1}");
                    dtEnd = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.EndTime1}");
                    begin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime2}");
                    dtSignOut = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time2}");
                    end = Convert.ToDateTime($"{dt.AddDays(1).ToString("yyyy-MM-dd")} {timeGroup.EndTime2}");
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.ToString("yyyy-MM-dd"))).ToList());
                    dataList.AddRange(attendDatas.Where(p => p.RecDate.Equals(dt.AddDays(1).ToString("yyyy-MM-dd"))).ToList());
                    attendDatas = dataList;
                    break;
                default:
                    dtBegin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime1}");
                    dtSignIn = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time1}");
                    dtEnd = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.EndTime1}");
                    begin = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.BeginTime2}");
                    dtSignOut = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.Time2}");
                    end = Convert.ToDateTime($"{dt.ToString("yyyy-MM-dd")} {timeGroup.EndTime2}");
                    attendDatas = attendDatas.Where(p => p.RecDate.Equals(dt.ToString("yyyy-MM-dd"))).ToList();
                    break;
            }
            #endregion


            #region 处理签到时间
            attendDatas.OrderBy(p => p.RecDatetime);

            //上班前签到
            if (attendDatas.Exists(p => p.RecDatetime >= dtBegin && p.RecDatetime <= dtSignIn))
            {
                DateTime signIn = attendDatas.Where(p => p.RecDatetime >= dtBegin && p.RecDatetime <= dtSignIn).OrderBy(p => p.RecDatetime).First().RecDatetime;
                timeOfSignIn = signIn.ToString("HH:mm");
                //计算上班前加班
                if (timeGroup.OTBeforeSignIn == 1)
                {
                    int minute = GetMinutesFromTwoDatetime(dtSignIn, signIn);
                    //考勤规则
                    if (rule.BeforeSignInEnabled == 1)
                    {
                        if (minute >= rule.BeforeSignInInterval)
                            result.OTMinutes = rule.BeforeSignOutMinute;
                    }
                    else
                    {
                        result.OTMinutes = minute;
                    }
                }
            }
            else
            //上班后签到
            if (attendDatas.Exists(p => p.RecDatetime >= dtSignIn && p.RecDatetime <= dtEnd))
            {
                DateTime signIn = attendDatas.Where(p => p.RecDatetime >= dtSignIn && p.RecDatetime <= dtEnd).OrderBy(p => p.RecDatetime).First().RecDatetime;
                timeOfSignIn = signIn.ToString("HH:mm");
                int minute = GetMinutesFromTwoDatetime(signIn, dtSignIn);
                //根据考勤规则判断是否旷工
                if (rule.OverSignInEnabled == 1)
                {
                    if (minute > rule.OverSignInMinute)
                        result.Absent = 1;
                    else
                    {
                        //根据时间段判断是否旷工
                        if (minute > timeGroup.LateMinute)
                            result.LateMinute = minute;
                    }
                }
                else
                {
                    //根据时间段判断是否旷工
                    if (minute > timeGroup.LateMinute)
                        result.LateMinute = minute;
                }
            }
            else
            //没有签到
            {
                if (rule.NoSignInEnabled == 1)
                {
                    if (rule.NoSignInType == 0)
                        result.LateMinute = rule.NoSignInMinute;
                    else
                        result.Absent = 1;
                }
            }
            #endregion

            #region 处理签退时间
            //下班前签退
            if (attendDatas.Exists(p => p.RecDatetime >= begin && p.RecDatetime <= dtSignOut))
            {
                DateTime signOut = attendDatas.Where(p => p.RecDatetime >= begin && p.RecDatetime <= dtSignOut).OrderBy(p => p.RecDatetime).First().RecDatetime;
                timeOfSignOut = signOut.ToString("HH:mm");
                int minute = GetMinutesFromTwoDatetime(dtSignOut, signOut);
                //根据规则计算早退
                if (rule.BeforeSignOutEnabled == 1)
                {
                    if (minute > rule.BeforeSignOutMinute)
                        result.Absent = 1;
                    else
                    {
                        if (minute > timeGroup.EarlyMinute)
                            result.EarlyMinutes = minute;
                    }

                }
                else
                {
                    if (minute > timeGroup.EarlyMinute)
                        result.EarlyMinutes = minute;
                }
            }
            else
            //下班后签退
            if (attendDatas.Exists(p => p.RecDatetime >= dtSignOut && p.RecDatetime <= end))
            {
                DateTime signOut = attendDatas.Where(p => p.RecDatetime >= dtSignOut && p.RecDatetime <= end).OrderBy(p => p.RecDatetime).First().RecDatetime;
                timeOfSignOut = signOut.ToString("HH:mm");
                int minute = GetMinutesFromTwoDatetime(signOut, dtSignOut);
                //时段是否计算下班后加班
                if (timeGroup.OTAfterSignOut == 1)
                {
                    //根据考勤规则判断是否满足加班条件
                    if (rule.OverSignOutEnabled == 1)
                    {
                        //有误区,请教考勤人员
                        if (minute > rule.OverSignOutInterval)
                            result.OTMinutes = rule.OverSignOutOTMinute;
                    }
                    else
                    {
                        result.OTMinutes = minute;
                    }
                }
            }
            else
            //没有签退
            {
                if (rule.NoSignOutEnabled == 1)
                {
                    if (rule.NoSignOutType == 0)
                        result.EarlyMinutes = rule.NoSignOutMinute;
                    else
                        result.Absent = 1;
                }
            }
            #endregion
            result.SignIn = timeOfSignIn;
            result.SignOut = timeOfSignOut;

            #region 计算周末加班
            if (CheckWeekend(dt, rule))
            {
                if (!string.IsNullOrEmpty(result.SignIn) || !string.IsNullOrEmpty(result.SignOut))
                {
                    int minute = GetMinutesFromTwoDatetime(dtSignOut, dtSignIn);
                    result.OTMinutes += minute;
                }
            }
            #endregion


            #region 计算工时

            result.WorkMinutes = 480;
            if (string.IsNullOrWhiteSpace(result.SignIn) || string.IsNullOrWhiteSpace(result.SignOut))
            {
                result.Attded = 0.0;
                result.WorkMinutes = 0;
            }
            else
            {
                //如果旷工则不计算工时
                if (result.Absent == 1)
                {
                    result.Attded = 0.0;
                    result.WorkMinutes = 0;
                }
                else
                {
                    int total = GetMinutesFromTwoDatetime(dtSignOut, dtSignIn);
                    result.WorkMinutes = total - result.LateMinute - result.EarlyMinutes;
                    if (total == 0)
                        result.Attded = 0.0;
                    else
                        result.Attded = (result.WorkMinutes / total) * timeGroup.Day;
                }
            }

            #endregion

            return result;
        }
        #endregion

        #region 计算两个时间之间分钟差
        private static int GetMinutesFromTwoDatetime(DateTime dtSignIn, DateTime signIn)
        {
            return (int)dtSignIn.Subtract(signIn).Duration().TotalMinutes;
        }
        #endregion

        #region 检查当天是否周末
        private static bool CheckWeekend(DateTime dt, AttendRule rule)
        {
            bool flag = false;
            int week = (int)dt.DayOfWeek;
            switch (week)
            {
                case 0:
                    flag = rule.Week0 == 1;
                    break;
                case 1:
                    flag = rule.Week1 == 1;
                    break;
                case 2:
                    flag = rule.Week2 == 1;
                    break;
                case 3:
                    flag = rule.Week3 == 1;
                    break;
                case 4:
                    flag = rule.Week4 == 1;
                    break;
                case 5:
                    flag = rule.Week5 == 1;
                    break;
                case 6:
                    flag = rule.Week6 == 1;
                    break;
            }
            return flag;
        }
        #endregion

    }
}
