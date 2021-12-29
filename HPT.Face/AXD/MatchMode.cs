using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Face.AXD
{
    /// <summary>
    /// 比对模式
    /// </summary>
    public enum MatchMode
    {

        /// <summary>
        /// 未初始化
        /// </summary>
        MATCH_MODE_NULL = 0,

        /// <summary>
        /// 刷脸+白名单开闸
        /// </summary>
        MATCH_MODE_NORMAL = 1,

        /// <summary>
        /// 刷脸+刷身份证开闸
        /// </summary>
        MATCH_MODE_IDCARD_1TO1 = 2,

        /// <summary>
        /// 刷脸+刷身份证+白名单开闸
        /// </summary>
        MATCH_MODE_FACE_IDCARD = 3,

        /// <summary>
        /// 刷卡+白名单开闸
        /// </summary>
        MATCH_MODE_WGCARD = 4,

        /// <summary>
        /// 刷脸+刷卡+白名单开闸
        /// </summary>
        MATCH_MODE_FACE_WGCARD = 5,

        /// <summary>
        /// 过人开闸
        /// </summary>
        MATCH_MODE_ANY_FACE = 6,

        /// <summary>
        /// 刷脸+白名单开闸或者刷卡+白名单开闸
        /// </summary>
        MATCH_MODE_NORMAL_OR_WGCARD = 7
    }
}
