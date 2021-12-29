using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class WriteConfig
    {
        /// <summary>
        /// 是否启用写卡
        /// </summary>
        public bool WriteCardEnable { get; set; }

        /// <summary>
        /// 读写卡密码
        /// </summary>
        public string CardPassword { get; set; }

        /// <summary>
        /// 写卡扇区
        /// </summary>
        public int SectorNo { get; set; }

        /// <summary>
        /// 是否启用反潜
        /// </summary>
        public bool AntiSubmarineWarfare { get; set; }

        /// <summary>
        /// 是否启用天限次数
        /// </summary>
        public bool LimitedTimes_Enabled { get; set; }

        /// <summary>
        /// 入口限制的次数
        /// </summary>
        public int LimitedTimesOfIn { get; set; }

        /// <summary>
        /// 入口限制的次数
        /// </summary>
        public int LimitedTimesOfOut { get; set; }

        /// <summary>
        /// 是否开启时段内限时
        /// </summary>
        public bool LimitedMinutes_Enable { get; set; }
        /// <summary>
        /// 时间段内限制分钟数
        /// </summary>
        public int LimitedMinutes { get; set; }

        /// <summary>
        /// 允许重复刷卡时间间隔
        /// </summary>
        public int CardInterval { get; set; }
    }
}
