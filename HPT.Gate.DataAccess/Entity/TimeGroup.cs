using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Entity
{
    public class TimeGroup
    {
        public TimeGroup(string beginTime, string endTime)
        {
            BeginTime = beginTime;
            EndTime = endTime;
        }
        public string BeginTime { get; set; }

        public string EndTime { get; set; }
    }
}
