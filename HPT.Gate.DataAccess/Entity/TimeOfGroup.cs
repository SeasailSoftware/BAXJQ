using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Entity
{
    public class TimeOfGroup
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public int WeekNo { get; set; }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }
    }
}
