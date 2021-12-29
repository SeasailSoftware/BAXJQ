using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class OperInfo
    {
        /// <summary>
        /// 操作员编号
        /// </summary>
        private int _OperId;

        public int OperId
        {
            get { return _OperId; }
            set { _OperId = value; }
        }

        /// <summary>
        /// 操作员名字
        /// </summary>
        private string _OperName;

        public string OperName
        {
            get { return _OperName; }
            set { _OperName = value; }
        }

        /// <summary>
        /// 操作员密码
        /// </summary>
        private string _OperPass;

        public string OperPass
        {
            get { return _OperPass; }
            set { _OperPass = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        private string _Descr;

        public string Descr
        {
            get { return _Descr; }
            set { _Descr = value; }
        }
    }
}
