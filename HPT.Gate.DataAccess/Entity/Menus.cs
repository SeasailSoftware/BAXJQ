using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class Menus
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        private int _MenuId;

        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }

        /// <summary>
        /// 菜单名字
        /// </summary>
        private string _MenuName;

        public string MenuName
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }

        /// <summary>
        /// 菜单显示文字
        /// </summary>
        private string _MenuText;

        public string MenuText
        {
            get { return _MenuText; }
            set { _MenuText = value; }
        }
        private int _ParMenuId;

        /// <summary>
        /// 父级菜单编号
        /// </summary>
        public int ParMenuId
        {
            get { return _ParMenuId; }
            set { _ParMenuId = value; }
        }

        public int Enabled { get; set; }
    }
}
