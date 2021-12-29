using System.Collections.Generic;
using System.Threading;

namespace HPT.Gate.DataAccess.Entity //修改名字空间
{
    public class LedController
    {
        public static Mutex myMutex = new Mutex();
        private List<Thread> _AreaThreads = new List<Thread>();
        #region properties

        /// <summary>
        /// 无参构造方法
        /// </summary>
        public LedController() { }


        private int lid;
        public int Lid
        {
            get => lid;
            set => lid = value;
        }

        private int _ControlType;

        public int ControlType
        {
            get => _ControlType;
            set => _ControlType = value;
        }

        private int protocol;
        public int Protocol
        {
            get => protocol;
            set => protocol = value;
        }

        private int interval;
        public int Interval
        {
            get => interval;
            set => interval = value;
        }

        private int width;
        public int Width
        {
            get => width;
            set => width = value;
        }

        private int heigth;
        public int Heigth
        {
            get => heigth;
            set => heigth = value;
        }

        private string iPaddress;
        public string IPaddress
        {
            get => iPaddress;
            set => iPaddress = value;
        }

        private int port;
        public int Port
        {
            get => port;
            set => port = value;
        }

        public List<int> Devices { get; set; }

        private int _TotalRecord;

        public int TotalRecord
        {
            get => _TotalRecord;
            set => _TotalRecord = value;
        }

        public List<AreaInfo> DynAreas { get; set; }

        #endregion


    }
}