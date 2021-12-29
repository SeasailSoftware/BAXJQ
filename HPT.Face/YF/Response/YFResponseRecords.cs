using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Face.YF.Model;

namespace HPT.Face.YF.Response
{
    public class YFResponseRecords
    {
        /// <summary>
        /// 
        /// </summary>
        public YFPage pageInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<YFRecord> records { get; set; }
    }
}
