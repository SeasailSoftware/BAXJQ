using HPT.Face.HPT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Face.HPT.Response
{
    public class HPTResponseRecords
    {
        /// <summary>
        /// 
        /// </summary>
        public HPTPage pageInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<HPTRecord> records { get; set; }
    }
}
