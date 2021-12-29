#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：hpt.gate.DataAccess.Entity

* 项目描述 ：

* 类 名 称 ：HTVacation

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：hpt.gate.DataAccess.Entity

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-08 13:51:35

* 更新时间 ：2019-07-08 13:51:35

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
    /// 功能描述    ：HTVacation  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-08 13:51:35 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-08 13:51:35 
    /// </summary>
    public class HTVacation
    {
        public int Id { get; set; }

        public string CardNo { get; set; }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }
    }
}
