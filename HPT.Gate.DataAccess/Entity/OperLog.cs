using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class OperLog
    {
        public int LogId { get; set; }

        public string OperName { get; set; }

        public string RecDatetime { get; set; }

        public string LogObj { get; set; }

        public string LogAction { get; set; }

        public string LogMessage { get; set; }

        public int LogType { get; set; }

    }
}
