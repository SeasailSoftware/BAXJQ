using System;
using System.Collections.Generic;
using System.Text;

namespace HPT.Gate.DataAccess.Entity //修改名字空间
{
    public class AttdDetail
    {
        #region 构造函数
        public AttdDetail(int empId, string recDate, int sid, string shiftName)
        {
            this.EmpId = empId;
            this.RecDate = recDate;
            this.Sid = sid;
            this.Sname = shiftName;
        }
        #endregion
        private int recId;
        public int RecId
        {
            get { return recId; }
            set { recId = value; }
        }

        private int empId;
        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        private string recDate;
        public string RecDate
        {
            get { return recDate; }
            set { recDate = value; }
        }

        private string beginTime1;
        public string BeginTime1
        {
            get { return beginTime1; }
            set { beginTime1 = value; }
        }

        private string endTime1;
        public string EndTime1
        {
            get { return endTime1; }
            set { endTime1 = value; }
        }

        private string beginTime2;
        public string BeginTime2
        {
            get { return beginTime2; }
            set { beginTime2 = value; }
        }

        private string endTime2;
        public string EndTime2
        {
            get { return endTime2; }
            set { endTime2 = value; }
        }

        private string beginTime3;
        public string BeginTime3
        {
            get { return beginTime3; }
            set { beginTime3 = value; }
        }

        private string endTime3;
        public string EndTime3
        {
            get { return endTime3; }
            set { endTime3 = value; }
        }

        private int sid;
        public int Sid
        {
            get { return sid; }
            set { sid = value; }
        }

        private string sname;
        public string Sname
        {
            get { return sname; }
            set { sname = value; }
        }

        private double late;
        public double Late
        {
            get { return late; }
            set { late = value; }
        }

        private double leaveEarly;
        public double LeaveEarly
        {
            get { return leaveEarly; }
            set { leaveEarly = value; }
        }

        private double absent;
        public double Absent
        {
            get { return absent; }
            set { absent = value; }
        }

        private double vacation;
        public double Vacation
        {
            get { return vacation; }
            set { vacation = value; }
        }

        private double ot;
        public double Ot
        {
            get { return ot; }
            set { ot = value; }
        }

        private double workHour;
        public double WorkHour
        {
            get { return workHour; }
            set { workHour = value; }
        }
    }
}