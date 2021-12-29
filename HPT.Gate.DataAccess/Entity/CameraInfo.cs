using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class CameraInfo
    {

        #region propertity
        /// <summary>
        /// 摄像头编号
        /// </summary>
        private int _CamId;

        public int CamId
        {
            get { return _CamId; }
            set { _CamId = value; }
        }

        /// <summary>
        /// 摄像头名称
        /// </summary>
        private string _CamName;

        public string CamName
        {
            get { return _CamName; }
            set { _CamName = value; }
        }



        /// <summary>
        /// IP地址
        /// </summary>
        private string _IPAddress;

        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        /// <summary>
        /// 端口号
        /// </summary>
        private int _Port;

        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        private string _Mark;

        public string Mark
        {
            get { return _Mark; }
            set { _Mark = value; }
        }

        /// <summary>
        /// 是否已经打开
        /// </summary>
        public bool IsOpen { get; set; }

        public int M_IUserId { get; set; } = -1;
        #endregion



    }
}
