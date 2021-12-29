using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class Shift
    {
        /// <summary>
        /// 班次编号
        /// </summary>
        private int _SID;

        public int SID
        {
            get { return _SID; }
            set { _SID = value; }
        }
        /// <summary>
        /// 班次类型，0为白班，1为夜班
        /// </summary>
        private int _SType;

        public int SType
        {
            get { return _SType; }
            set { _SType = value; }
        }

        /// <summary>
        /// 班次名称
        /// </summary>
        private string _SName;

        public string SName
        {
            get { return _SName; }
            set { _SName = value; }
        }

        /// <summary>
        /// 班次描述
        /// </summary>
        private string _SDesc;

        public string SDesc
        {
            get { return _SDesc; }
            set { _SDesc = value; }
        }

        /// <summary>
        /// 时段一上班时间
        /// </summary>
        private string _BeginTime1;

        public string BeginTime1
        {
            get { return _BeginTime1; }
            set { _BeginTime1 = value; }
        }

        /// <summary>
        /// 是否签到
        /// </summary>
        private int _Begin_SignIn1;

        public int Begin_SignIn1
        {
            get { return _Begin_SignIn1; }
            set { _Begin_SignIn1 = value; }
        }

        /// <summary>
        /// 前有效
        /// </summary>
        private int _Begin_PreEffect1;

        public int Begin_PreEffect1
        {
            get { return _Begin_PreEffect1; }
            set { _Begin_PreEffect1 = value; }
        }

        /// <summary>
        /// 后有效
        /// </summary>
        private int _Begin_BehindEffect1;

        public int Begin_BehindEffect1
        {
            get { return _Begin_BehindEffect1; }
            set { _Begin_BehindEffect1 = value; }
        }

        /// <summary>
        /// 时段一下班时间
        /// </summary>
        private string _EndTime1;

        public string EndTime1
        {
            get { return _EndTime1; }
            set { _EndTime1 = value; }
        }

        /// <summary>
        /// 是否签退
        /// </summary>
        private int _End_SignOut1;

        public int End_SignOut1
        {
            get { return _End_SignOut1; }
            set { _End_SignOut1 = value; }
        }
        /// <summary>
        /// 前有效
        /// </summary>
        private int _End_PreEffect1;

        public int End_PreEffect1
        {
            get { return _End_PreEffect1; }
            set { _End_PreEffect1 = value; }
        }
        /// <summary>
        /// 后有效
        /// </summary>
        private int _End_BehindEffect1;

        public int End_BehindEffect1
        {
            get { return _End_BehindEffect1; }
            set { _End_BehindEffect1 = value; }
        }
        /// <summary>
        /// 时间段类型0为正常考勤，1为加班
        /// </summary>
        private int _AttendType1;

        public int AttendType1
        {
            get { return _AttendType1; }
            set { _AttendType1 = value; }
        }

        /// <summary>
        /// 时间段一是否为跨天标志
        /// </summary>
        public bool Flag1
        {
            get
            {
                return GetTimeSpan(BeginTime1, EndTime1) <= 0;
            }
        }

        /// <summary>
        /// 时段二上班时间
        /// </summary>
        private string _BeginTime2;

        public string BeginTime2
        {
            get { return _BeginTime2; }
            set { _BeginTime2 = value; }
        }

        /// <summary>
        /// 是否签到
        /// </summary>
        private int _Begin_SignIn2;

        public int Begin_SignIn2
        {
            get { return _Begin_SignIn2; }
            set { _Begin_SignIn2 = value; }
        }

        /// <summary>
        /// 前有效
        /// </summary>
        private int _Begin_PreEffect2;

        public int Begin_PreEffect2
        {
            get { return _Begin_PreEffect2; }
            set { _Begin_PreEffect2 = value; }
        }

        /// <summary>
        /// 后有效
        /// </summary>
        private int _Begin_BehindEffect2;

        public int Begin_BehindEffect2
        {
            get { return _Begin_BehindEffect2; }
            set { _Begin_BehindEffect2 = value; }
        }

        /// <summary>
        /// 时段一下班时间
        /// </summary>
        private string _EndTime2;

        public string EndTime2
        {
            get { return _EndTime2; }
            set { _EndTime2 = value; }
        }

        /// <summary>
        /// 是否签退
        /// </summary>
        private int _End_SignOut2;

        public int End_SignOut2
        {
            get { return _End_SignOut2; }
            set { _End_SignOut2 = value; }
        }
        /// <summary>
        /// 前有效
        /// </summary>
        private int _End_PreEffect2;

        public int End_PreEffect2
        {
            get { return _End_PreEffect2; }
            set { _End_PreEffect2 = value; }
        }
        /// <summary>
        /// 后有效
        /// </summary>
        private int _End_BehindEffect2;

        public int End_BehindEffect2
        {
            get { return _End_BehindEffect2; }
            set { _End_BehindEffect2 = value; }
        }
        /// <summary>
        /// 时间段类型0为正常考勤，1为加班
        /// </summary>
        private int _AttendType2;

        public int AttendType2
        {
            get { return _AttendType2; }
            set { _AttendType2 = value; }
        }

        /// <summary>
        /// 判断时间段2是否为跨天标志
        /// </summary>
        public bool Flag2
        {
            get
            {
                return GetTimeSpan(BeginTime1, EndTime1) <= 0;
            }
        }
        /// <summary>
        /// 时段一上班时间
        /// </summary>
        private string _BeginTime3;

        public string BeginTime3
        {
            get { return _BeginTime3; }
            set { _BeginTime3 = value; }
        }

        /// <summary>
        /// 是否签到
        /// </summary>
        private int _Begin_SignIn3;

        public int Begin_SignIn3
        {
            get { return _Begin_SignIn3; }
            set { _Begin_SignIn3 = value; }
        }

        /// <summary>
        /// 前有效
        /// </summary>
        private int _Begin_PreEffect3;

        public int Begin_PreEffect3
        {
            get { return _Begin_PreEffect3; }
            set { _Begin_PreEffect3 = value; }
        }

        /// <summary>
        /// 后有效
        /// </summary>
        private int _Begin_BehindEffect3;

        public int Begin_BehindEffect3
        {
            get { return _Begin_BehindEffect3; }
            set { _Begin_BehindEffect3 = value; }
        }

        /// <summary>
        /// 时段一下班时间
        /// </summary>
        private string _EndTime3;

        public string EndTime3
        {
            get { return _EndTime3; }
            set { _EndTime3 = value; }
        }

        /// <summary>
        /// 是否签退
        /// </summary>
        private int _End_SignOut3;

        public int End_SignOut3
        {
            get { return _End_SignOut3; }
            set { _End_SignOut3 = value; }
        }
        /// <summary>
        /// 前有效
        /// </summary>
        private int _End_PreEffect3;

        public int End_PreEffect3
        {
            get { return _End_PreEffect3; }
            set { _End_PreEffect3 = value; }
        }
        /// <summary>
        /// 后有效
        /// </summary>
        private int _End_BehindEffect3;

        public int End_BehindEffect3
        {
            get { return _End_BehindEffect3; }
            set { _End_BehindEffect3 = value; }
        }

        /// <summary>
        /// 时间段类型0为正常考勤，1为加班
        /// </summary>
        private int _AttendType3;

        public int AttendType3
        {
            get { return _AttendType3; }
            set { _AttendType3 = value; }
        }

        /// <summary>
        /// 标准工时
        /// </summary>
        private int _WorkHour;

        public int WorkHour
        {
            get { return _WorkHour; }
            set { _WorkHour = value; }
        }
        /// <summary>
        /// 判断时间段3是否为跨天标志
        /// </summary>
        public bool Flag3
        {
            get
            {
                return GetTimeSpan(BeginTime1, EndTime1) <= 0;
            }
        }

        /// <summary>
        /// 返回两个时间相差的分钟数
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int GetTimeSpan(string begin, string end)
        {
            DateTime dtBegin = Convert.ToDateTime(begin);
            DateTime dtEnd = Convert.ToDateTime(end);
            TimeSpan tsBegin = new TimeSpan(dtBegin.Ticks);
            TimeSpan tsEnd = new TimeSpan(dtEnd.Ticks);
            TimeSpan tsBetween = tsEnd.Subtract(tsBegin).Duration();
            return tsBetween.Hours * 60 + tsBetween.Minutes;
        }
    }
}
