using libzkfpcsharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace HPT.Gate.ZKFP
{
    public class ZKFPHelper
    {
        #region DLLIMPORT

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        #endregion

        #region properity
        public bool IsRunning => Running;
        public int CurrentFingerId { get; set; }
        public byte[] CurrentFingerData { get; set; }

        /// <summary>
        /// 服务类型:1为指纹录入,2为指纹比对
        /// </summary>
        public int Type { get; set; } = 1;

        #endregion


        #region Private
        private bool Running = false;
        private bool _inited = false;
        private IntPtr mDevHandle = IntPtr.Zero;
        private ZKFPDB zkfpdb;
        private int WorkStatus = 0;
        private int cbRegTmp = 0;
        private int DevId;
        private int RegisterCount = 0;

        private byte[][] RegTmps = new byte[3][];

        private byte[] FPBuffer;
        private int mfpWidth = 0;
        private int mfpHeight = 0;
        private const int MESSAGE_CAPTURED_OK = 0x0400 + 6;
        private bool RegSuccess = false;
        private ConcurrentQueue<FingerPrint> TaskQueue = new ConcurrentQueue<FingerPrint>();
        #endregion


        #region Instance

        private static ZKFPHelper instance;
        private static readonly object lockHelper = new object();

        public static ZKFPHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new ZKFPHelper();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region Events

        #region 消息提示
        /// <summary>
        /// 消息提示事件
        /// </summary>
        public event Action<string> Message;

        /// <summary>
        /// 触发消息提示事件
        /// </summary>
        /// <param name="messgae"></param>
        private void OnMessage(string messgae)
        {
            if (Message == null) return;
            if (Type == 1)
                Message(messgae);
            else
                Message($"[FingerPrint Server]{messgae}");
        }
        #endregion

        #region 指纹图像展示
        /// <summary>
        /// 指纹图像展示
        /// </summary>
        public event Action<Bitmap> FingerPrint;


        private void OnFingerPrint(Bitmap bmp)
        {
            if (FingerPrint == null) return;
            Task.Factory.StartNew(() =>
            {
                if (FingerPrint != null)
                {
                    FingerPrint(bmp);
                }
            });
        }
        #endregion


        #endregion

        #region private Methods



        #region 释放资源
        private void FinalizeDll()
        {
            zkfp2.Terminate();
            cbRegTmp = 0;
        }

        #endregion


        private void RegisterData(byte[] capTmp)
        {
            if (zkfpdb.DBIdentify(capTmp))
            {
                OnMessage($"该手指指纹已经被注册!");
                return;
            }
            if (RegisterCount > 0 && zkfpdb.DBMatch(capTmp, RegTmps[RegisterCount - 1]) <= 0)
            {
                OnMessage("请连续按同一个手指三次!");
                return;
            }
            Array.Copy(capTmp, RegTmps[RegisterCount], capTmp.Length);
            RegisterCount++;
            if (RegisterCount >= 3)
            {
                RegisterCount = 0;
                CurrentFingerData = zkfpdb.DBMerge(RegTmps[0], RegTmps[1], RegTmps[2]);
                if (CurrentFingerData != null && zkfpdb.DBAdd(CurrentFingerId, CurrentFingerData))
                {
                    OnMessage("录入成功!");
                    RegSuccess = true;
                    Stop();
                }
                else
                {
                    OnMessage($"录入失败!");
                }
                return;
            }
            else
            {
                OnMessage($"你还需要按{3 - RegisterCount}次手指才能成功注册!");
            }

        }



        #region 抓取图像
        private void Enroll()
        {
            RegisterCount = 0;
            cbRegTmp = 0;
            for (int i = 0; i < 3; i++)
            {
                RegTmps[i] = new byte[2048];
            }
            byte[] paramValue = new byte[4];
            int size = 4;
            zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

            size = 4;
            zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

            FPBuffer = new byte[mfpWidth * mfpHeight];
            Running = true;
            OnMessage("开始进行指纹录入!");
            OnMessage("请按照提示按您的手指三次!");
            while (Running)
            {
                int cbCapTmp = 0;
                byte[] capTmp = new byte[2048];
                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, capTmp, ref cbCapTmp);
                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    MemoryStream ms = new MemoryStream();
                    BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
                    Bitmap bmp = new Bitmap(ms);
                    OnFingerPrint(bmp);
                    if (cbCapTmp > 0)
                    {
                        byte[] array = new byte[cbCapTmp];
                        Array.Copy(capTmp, array, cbCapTmp);
                        RegisterData(array);
                    }
                }
                Thread.Sleep(200);
            }
        }

        #endregion

        #region 采集指纹模板
        private void Collect()
        {
            while (Running)
            {
                while (TaskQueue.Count > 0)
                {
                    if (TaskQueue.TryDequeue(out FingerPrint task))
                    {
                        if (task != null)
                        {
                            if (task.FPid > 0)
                            {
                                int ret = 0;
                                if (task.FPData == null || task.FPData.Length < 100)
                                {
                                    if (zkfpdb.Delete(task.FPid, out string msg))
                                        OnMessage($"删除指纹成功!指纹编号:{task.FPid}");
                                    else
                                        OnMessage($"删除指纹失败:{msg},指纹编号:{task.FPid}");
                                }
                                else
                                {
                                    if (zkfpdb.Delete(task.FPid, out string msg))
                                    {
                                        OnMessage($"删除指纹成功!指纹编号:{task.FPid}");
                                        if (zkfpdb.DBAdd(task.FPid, task.FPData, out msg))
                                            OnMessage($"添加指纹成功!指纹编号:{task.FPid}");
                                        else
                                            OnMessage($"添加指纹失败:{msg},指纹编号:{task.FPid}");
                                    }
                                    else
                                        OnMessage($"删除指纹失败:{msg},指纹编号:{task.FPid}");
                                }
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1000);
            }
        }
        #endregion


        #region 设置指纹采集状态
        private void SetWorkStatus(int status)
        {
            if (status >= 0 && status <= 2)
            {
                WorkStatus = status;
                switch (status)
                {
                    case 0:
                        OnMessage("开始识别指纹,请按您的手指!");
                        break;
                    case 1:
                        OnMessage("开始验证指纹,请按您的手指!");
                        break;
                    case 2:
                        OnMessage("请按照提示按您的手指三次!");
                        break;
                }
            }

        }
        #endregion

        #endregion

        #region  Public  Methods

        #region 开启服务

        public bool Init(List<FingerPrint> fps, out string msg, bool reInit = false)
        {
            if (reInit) _inited = false;
            if (!_inited)
            {
                if (zkfp2.Init() < zkfperrdef.ZKFP_ERR_OK)
                {
                    msg = "初始化动态库失败!";
                    return false;
                }
                int nCount = zkfp2.GetDeviceCount();
                if (nCount == 0)
                {
                    msg = "没有发现连接的设备!";
                    return false;
                }
                if (Type == 1)
                {
                    if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(DevId)))
                    {
                        msg = "打开设备失败!";
                        return false;
                    }
                }
                zkfpdb = new ZKFPDB();
                if (!zkfpdb.Init())
                {
                    zkfp2.CloseDevice(mDevHandle);
                    mDevHandle = IntPtr.Zero;
                    msg = "初始化设备数据库失败!";
                    return false;
                }
                zkfpdb.Clear();
                if (fps != null)
                {
                    foreach (var fp in fps)
                    {
                        if (fp.FPid > 0 && fp.FPData != null && fp.FPData.Length > 100)
                            zkfpdb.DBAdd(fp.FPid, fp.FPData);
                    }
                }
                _inited = true;
            }
            else
            {
                zkfpdb.Clear();
                if (fps != null)
                {
                    foreach (var fp in fps)
                    {
                        if (fp.FPid > 0 && fp.FPData != null && fp.FPData.Length > 100)
                            zkfpdb.DBAdd(fp.FPid, fp.FPData);
                    }
                }
            }
            msg = "success";
            return true;
        }

        public bool Start()
        {
            if (Running) return true;
            if (!_inited)
            {
                OnMessage("尚未初始化!");
                return false;
            }
            Running = true;
            if (Type == 1)
                Task.Factory.StartNew(() => { Enroll(); });
            else
                Task.Factory.StartNew(() => { Collect(); });
            OnMessage("服务启动成功...");
            return true;
        }

        #endregion

        #region 停止服务
        public void Stop()
        {
            if (!Running) return;
            FinalizeDll();
            RegisterCount = 0;
            Thread.Sleep(1000);
            zkfp2.CloseDevice(mDevHandle);
            Running = false;
        }

        #endregion

        #region 添加任务

        public void AddTask(FingerPrint fingerPrint)
        {
            TaskQueue.Enqueue(fingerPrint);
        }
        #endregion

        #region 指纹后台比对
        public bool Identify(byte[] fingerData, out int fingerId)
        {
            if (!_inited)
            {
                OnMessage("指纹数据库尚未初始化!比对失败.");
                fingerId = 0;
                return false;
            }
            if (zkfpdb.DBIdentify(fingerData, out fingerId, out int score))
            {
                OnMessage($"指纹比对通过![指纹编号:{fingerId},比对分数:{score},指纹总数:{zkfpdb.Count}]");
                return true;
            }
            else
            {
                OnMessage($"指纹比对失败![指纹编号:{fingerId},比对分数:{score},指纹总数:{zkfpdb.Count}]");
                return false;
            }
        }
        #endregion


        #endregion

    }

    public class FingerPrint
    {
        public int FPid { get; set; }

        public byte[] FPData { get; set; }
    }

}
