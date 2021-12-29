using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public enum Result
    {
        /// <summary>
        /// 失败
        /// </summary>
        FAILURE = 0xAA,

        /// <summary>
        /// 成功
        /// </summary>
        SUCCESS = 0x55,
    }
}
