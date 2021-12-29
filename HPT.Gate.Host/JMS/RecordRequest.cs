#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：HPT.Gate.Host.JMS

* 项目描述 ：

* 类 名 称 ：RecordRequest

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：HPT.Gate.Host.JMS

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-09 18:06:03

* 更新时间 ：2019-07-09 18:06:03

* 版 本 号 ：v1.0.0.0

*******************************************************************

* Copyright @ Administrator 2019. All rights reserved.

*******************************************************************

//----------------------------------------------------------------
*/

#endregion

namespace HPT.Gate.Host.JMS
{
    /// <summary>
    /// 功能描述    ：RecordRequest  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-09 18:06:03 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-09 18:06:03 
    /// </summary>
    public class RecordRequest
    {
        public string termSn { get; set; }
        public string cardNo { get; set; }
        public string cardTime { get; set; }
        public int type { get; set; }
        public string capture { get; set; }

    }
}
