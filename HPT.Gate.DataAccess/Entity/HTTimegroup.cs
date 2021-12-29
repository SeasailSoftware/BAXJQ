#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：hpt.gate.DataAccess.Entity

* 项目描述 ：

* 类 名 称 ：HTTimegroup

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：hpt.gate.DataAccess.Entity

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-08 10:53:31

* 更新时间 ：2019-07-08 10:53:31

* 版 本 号 ：v1.0.0.0

*******************************************************************

* Copyright @ Administrator 2019. All rights reserved.

*******************************************************************

//----------------------------------------------------------------
*/

#endregion

namespace hpt.gate.DataAccess.Entity
{
    /// <summary>
    /// 功能描述    ：HTTimegroup  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-08 10:53:31 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-08 10:53:31 
    /// </summary>
    public class HTTimegroup
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public int id { get; set; }

        public int dormBanId { get; set; }
        /// <summary>
        /// 校门门禁时间段编号
        /// </summary>
        public int gateBanId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sign { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string termSn { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string beginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }

        public int gradeId { get; set; }

        public int type { get; set; } = 1;
    }
}
