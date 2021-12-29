using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbTools.Entity
{
    public class Versions
    {
        /// <summary>
        /// 版本编号
        /// </summary>
        public int Vid { get; set; }

        public int MainVersion { get; set; }

        public int LedVersion { get; set; }

        public int AttendVersion { get; set; }
    }
}
