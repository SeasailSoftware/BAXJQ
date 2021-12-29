using System;
using System.Collections.Generic;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System.Threading.Tasks;
using System.Linq;
using HPT.Gate.DataAccess.Service;
using System.Diagnostics;

namespace HPT.Gate.Client
{
    public class LocalCache
    {

        private static object EmpLocker = new object();
        private static object DeptLocker = new object();

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static OperInfo CurrentOper { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public static List<Menus> MenuList
        {
            get
            {
                if (CurrentOper == null)
                    return null;
                return MenusService.GetMenusByOperId(CurrentOper.OperId);
            }
        }


        /// <summary>
        /// 软件是否重启的标志
        /// </summary>
        private static bool _RestartFlag = false;

        public static bool RestartFlag
        {
            get { return _RestartFlag; }
            set { _RestartFlag = value; }
        }
    }
}
