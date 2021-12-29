
using HPT.NetCam.DH;
using HPT.NetCam.HK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace HPT.NetCam
{
    public class NetCamHelper
    {
        #region Var
        private uint iLastErr = 0;
        private bool IsStart = false;
        private bool m_bInitSDK = false;
        private string _LogPath = $@"{Environment.CurrentDirectory}\NetCam\Log";
        public List<NetCam> NetCams { get; set; }
        private static fRealDataCallBackEx2 m_RealDataCallBackEx2;
        private static fDisConnectCallBack m_DisConnectCallBack;
        private static fHaveReConnectCallBack m_ReConnectCallBack;
        private static fSnapRevCallBack m_SnapRevCallBack;
        #endregion

        #region Instance

        private static NetCamHelper instance;
        private static readonly object lockHelper = new object();

        public static NetCamHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new NetCamHelper();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region Events
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
            if (Message != null)
            {
                Message($"[NetCam Service]{messgae}");
            }
        }

        #endregion

        #region public

        #region 初始化摄像头
        private bool Init()
        {
            if (NetCams == null || NetCams.Count == 0)
            {
                OnMessage("请先添加设备!");
                return false;
            }
            if (!InitDll()) return false;
            return true;
        }
        #endregion


        #region 开启服务

        public void Start()
        {
            if (IsStart) return;
            if (!InitDll()) return;
            IsStart = true;
            foreach (NetCam dhCam in NetCams)
            {
                string msg;
                dhCam.Login(out msg);
                OnMessage(msg);
            }
            OnMessage("服务已启动...");
        }

        #endregion

        #region 关闭服务
        public void Stop()
        {
            if (!IsStart) return;
            IsStart = false;
            foreach (DHNetCam dhCam in NetCams)
            {
                string msg;
                dhCam.LogOut(out msg);
                Message(msg);
            }
            OnMessage("服务已停止.");
        }

        #endregion

        #endregion


        #region private

        #region 初始化动态库
        private bool InitDll()
        {
            bool m_bInitSDK = false;
            NetCam netCam = NetCams.FirstOrDefault();
            try
            {
                if (netCam is DHNetCam)
                {
                    m_DisConnectCallBack = new fDisConnectCallBack(DisConnectCallBack);
                    m_ReConnectCallBack = new fHaveReConnectCallBack(ReConnectCallBack);
                    m_RealDataCallBackEx2 = new fRealDataCallBackEx2(RealDataCallBackEx);
                    m_SnapRevCallBack = new fSnapRevCallBack(SnapRevCallBack);
                    NETClient.Init(m_DisConnectCallBack, IntPtr.Zero, null);
                    NETClient.SetAutoReconnect(m_ReConnectCallBack, IntPtr.Zero);
                    NETClient.SetSnapRevCallBack(m_SnapRevCallBack, IntPtr.Zero);
                    m_bInitSDK = true;
                    OnMessage("初始化动态库成功!");
                }
                else
                {
                    m_bInitSDK = CHCNetSDK.NET_DVR_Init();
                    if (!m_bInitSDK)
                    {
                        OnMessage("初始化动态库失败!");
                    }
                    else
                    {
                        CHCNetSDK.NET_DVR_SetLogToFile(3, _LogPath, true);
                        OnMessage(string.Format("初始化动态库成功!日志保存路径为[{0}]", _LogPath));
                        m_bInitSDK = true;
                    }
                }

            }
            catch (Exception ex)
            {
                OnMessage($"初始化动态库失败:{ex.Message}");
            }
            return m_bInitSDK;
        }

        #endregion


        #region 抓拍
        public bool Capture(string ip, out Image image, string fileName = "")
        {
            NetCam netCam = NetCams.FirstOrDefault(p => p.IPAddress.Equals(ip));
            if (netCam == null)
            {
                OnMessage($"尚未加载摄像头[{ip}]，无法抓拍图像。");
                image = null;
                return false;
            }
            string msg;
            bool success = netCam.Capture(out msg, out image, fileName);
            OnMessage(msg);
            return success;
        }
        #endregion

        #region 开始预览
        public bool StartPreview(string ip, IntPtr handler)
        {
            NetCam netCam = NetCams.FirstOrDefault(p => p.IPAddress.Equals(ip));
            if (netCam == null)
            {
                OnMessage($"尚未加载摄像头[{ip}]，无法预览图像。");
                return false;
            }
            string msg;
            return netCam.StartPreview(handler, out msg);
        }
        #endregion

        #region 结束预览
        public bool StopPreview(string ip)
        {
            NetCam netCam = NetCams.FirstOrDefault(p => p.IPAddress.Equals(ip));
            if (netCam == null) return true;
            string msg;
            return netCam.StopPreview(out msg);
        }
        #endregion


        #endregion

        #region CallBack 回调
        private void DisConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            string ip = Marshal.PtrToStringAnsi(pchDVRIP);
            OnMessage($"摄像头[{ip}]已离线...");
        }

        private void ReConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            string ip = Marshal.PtrToStringAnsi(pchDVRIP);
            OnMessage($"摄像头[{ip}]重新连接成功...");
        }

        private void RealDataCallBackEx(IntPtr lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, IntPtr param, IntPtr dwUser)
        {
            //do something such as save data,send data,change to YUV. 比如保存数据，发送数据，转成YUV等.
        }

        private void SnapRevCallBack(IntPtr lLoginID, IntPtr pBuf, uint RevLen, uint EncodeType, uint CmdSerial, IntPtr dwUser)
        {
            /*
            string path = $@"{Environment.CurrentDirectory}\Capture\{DateTime.Now.ToString("yyyy-MM-dd")}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (EncodeType == 10) //.jpg
            {
                DateTime now = DateTime.Now;
                string fileName = $"{Guid.NewGuid().ToString()}.jpg";
                string filePath = $@"{path}\{fileName}";
                byte[] data = new byte[RevLen];
                Marshal.Copy(pBuf, data, 0, (int)RevLen);
                using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    stream.Write(data, 0, (int)RevLen);
                    stream.Flush();
                    stream.Dispose();
                }
            }
            */
        }
        #endregion
    }
}
