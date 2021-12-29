using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity
{
    public class DBDevice
    {
        #region -------var

        /// <summary>
        /// 区域编号
        /// </summary>
        private int _PlaceId;

        public int PlaceId
        {
            get { return _PlaceId; }
            set { _PlaceId = value; }
        }

        /// <summary>
        /// 设备编号
        /// </summary>
        private int _DevId;

        public int DevId
        {
            get { return _DevId; }
            set { _DevId = value; }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        private string _DevName;

        public string DevName
        {
            get { return _DevName; }
            set { _DevName = value; }
        }

        /// <summary>
        /// 物理地址
        /// </summary>
        private string _MAC;

        public string MAC
        {
            get { return _MAC; }
            set { _MAC = value; }
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
        /// 子网掩码
        /// </summary>
        private string _SubNet;

        public string SubNet
        {
            get { return _SubNet; }
            set { _SubNet = value; }
        }
        /// <summary>
        /// 网关
        /// </summary>
        private string _GateWay;

        public string GateWay
        {
            get { return _GateWay; }
            set { _GateWay = value; }
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
        /// 硬件版本
        /// </summary>
        private string _HardVersion;

        public string HardVersion
        {
            get { return _HardVersion; }
            set { _HardVersion = value; }
        }
        /// <summary>
        /// 软件版本
        /// </summary>
        private string _SoftVersion;

        public string SoftVersion
        {
            get { return _SoftVersion; }
            set { _SoftVersion = value; }
        }

        /// <summary>
        /// 屏幕1是出口还是入口
        /// </summary>
        private int _P1;

        public int P1
        {
            get { return _P1; }
            set { _P1 = value; }
        }

        /// <summary>
        /// 屏幕一对应的语音段
        /// </summary>
        private int _V1;

        public int V1
        {
            get { return _V1; }
            set { _V1 = value; }
        }
        /// <summary>
        /// 屏幕二
        /// </summary>
        private int _P2;

        public int P2
        {
            get { return _P2; }
            set { _P2 = value; }
        }
        /// <summary>
        /// 屏幕二对应的语音段
        /// </summary>
        private int _V2;

        public int V2
        {
            get { return _V2; }
            set { _V2 = value; }
        }
        /// <summary>
        /// 欢迎词
        /// </summary>
        private string _WelCome;

        public string WelCome
        {
            get { return _WelCome; }
            set { _WelCome = value; }
        }

        /// <summary>
        /// 过闸等待时间
        /// </summary>
        private int _WaitTime;

        public int WaitTime
        {
            get { return _WaitTime; }
            set { _WaitTime = value; }
        }
        /// <summary>
        /// 过闸延时时间
        /// </summary>
        private int _DelayTime;

        public int DelayTime
        {
            get { return _DelayTime; }
            set { _DelayTime = value; }
        }

        /// <summary>
        /// 重复刷卡间隔
        /// </summary>
        private int _RepeatTime;

        public int RepeatTime
        {
            get { return _RepeatTime; }
            set { _RepeatTime = value; }
        }
        #endregion
    }
}
