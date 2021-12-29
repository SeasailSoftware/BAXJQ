#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：HPT.Gate.Host.JMS

* 项目描述 ：

* 类 名 称 ：DoorHeartbeatRequest

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：HPT.Gate.Host.JMS

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-10 13:42:23

* 更新时间 ：2019-07-10 13:42:23

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
    /// 功能描述    ：DoorHeartbeatRequest  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-10 13:42:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-10 13:42:23 
    /// </summary>
    public class DoorHeartbeatRequest
    {
        public string termSn { get; set; }
        public int type { get; set; }
    }
}
