using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Joey.Lib.Controls
{
    public class BackgroundWorkerEventArgs : EventArgs
    {
        /// <summary>
        /// 后台程序运行时抛出的异常
        /// </summary>
        public Exception BackGroundException { get; set; }
    }
}
