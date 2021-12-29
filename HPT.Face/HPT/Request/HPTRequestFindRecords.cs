using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Face.HPT.Request
{
    public class HPTRequestFindRecords
    {
        /// <summary>
        /// 设备密码
        /// </summary>
        public string Pass { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// 每页数据数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 查询页码
        /// </summary>
        public int PageIndex { get; set; }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }

        public string Serialize()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"pass={Pass}")
                .Append("&")
                .Append($"personId={PersonId}")
                .Append("&")
                .Append($"length={PageSize}")
                .Append("&")
                .Append($"index={PageIndex}")
                .Append("&")
                .Append($"startTime={BeginTime}")
                .Append("&")
                .Append($"endTime={EndTime}");
            return buffer.ToString();
        }
    }
}
