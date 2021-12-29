using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace HPT.NetCam.DH
{
    public class DHNetCam : NetCam
    {
        #region private 



        /// <summary>
        /// 设备信息
        /// </summary>
        private NET_DEVICEINFO_Ex m_DeviceInfo;

        /// <summary>
        /// 实时播放句柄
        /// </summary>
        private IntPtr m_RealPlayID = IntPtr.Zero;
        private uint m_SnapSerialNum = 1;

        public bool IsLogined => LoginId != IntPtr.Zero;
        #endregion



        #region Var

        private IntPtr LoginId = IntPtr.Zero;
        public string IPAddress { get; set; }

        public UInt16 Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        #endregion

        #region Ctor
        public DHNetCam()
        {

        }

        public DHNetCam(string ip, ushort port, string user, string password)
        {
            IPAddress = ip;
            Port = port;
            UserName = user;
            Password = password;
        }
        #endregion


        #region public 

        #region Login
        public bool Login(out string msg)
        {
            m_DeviceInfo = new NET_DEVICEINFO_Ex();
            LoginId = NETClient.Login(IPAddress, Port, UserName, Password, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref m_DeviceInfo);
            if (IntPtr.Zero == LoginId)
            {
                msg = NETClient.GetLastError();
                return false;
            }
            msg = $"摄像头[{IPAddress}]登录成功!";
            return true;
        }
        #endregion

        #region Logout

        public bool LogOut(out string msg)
        {
            bool result = NETClient.Logout(LoginId);
            if (!result)
            {
                msg = $"退出失败:{NETClient.GetLastError()}";
                return false;
            }
            LoginId = IntPtr.Zero;
            NETClient.Cleanup();
            msg = "成功退出!";
            return true;

        }
        #endregion

        #region Start RealPlay

        public bool StartPreview(IntPtr handler, out string msg)
        {
            if (m_RealPlayID != IntPtr.Zero)
            {
                msg = ($"摄像头[{IPAddress}]实时预览已启动!");
                return true;
            }
            EM_RealPlayType type = EM_RealPlayType.Realplay;
            m_RealPlayID = NETClient.RealPlay(LoginId, 0, handler, type);
            if (IntPtr.Zero == m_RealPlayID)
            {
                msg = ($"摄像头[{IPAddress}]启动实时预览失败:{NETClient.GetLastError()}");
                return false;
            }
            //NETClient.SetRealDataCallBack(m_RealPlayID, m_RealDataCallBackEx2, IntPtr.Zero, EM_REALDATA_FLAG.DATA_WITH_FRAME_INFO | EM_REALDATA_FLAG.PCM_AUDIO_DATA | EM_REALDATA_FLAG.RAW_DATA | EM_REALDATA_FLAG.YUV_DATA);
            msg = ($"摄像头[{IPAddress}]启动实时预览成功!通道_1");
            return true;
        }
        #endregion


        #region Stop RealPlay

        public bool StopPreview(out string msg)
        {
            bool ret = NETClient.StopRealPlay(m_RealPlayID);
            if (!ret)
            {
                msg = ($"停止实时预览失败:{NETClient.GetLastError()}");
                return false;
            }
            msg = ($"停止实时预览成功!");
            m_RealPlayID = IntPtr.Zero;
            return true;
        }
        #endregion


        #region 抓拍
        public Image LocalCapture(out string msg, string filePath = "")
        {
            if (IntPtr.Zero == m_RealPlayID)
            {
                msg = ("尚未打开实时预览,无法抓拍");
                return null;
            }
            if (string.IsNullOrEmpty(filePath))
                filePath = $@"{Environment.CurrentDirectory}\NetCam\Capture\{DateTime.Now.ToString("yyyy-MM-dd")}\{Guid.NewGuid().ToString()}.jpg";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            bool result = NETClient.CapturePicture(m_RealPlayID, filePath, EM_NET_CAPTURE_FORMATS.JPEG);
            if (!result)
            {
                msg = ($"抓拍失败:{NETClient.GetLastError()}");
                return null;
            }
            msg = ($"抓拍成功!图像存放路径:{filePath}");
            return new Bitmap(filePath);
        }

        public bool Capture(out string msg, out Image img, string fileName)
        {
            if (IntPtr.Zero == LoginId)
            {
                msg = $"摄像头[{IPAddress}]抓拍失败:{NETClient.GetLastError()}";
                img = null;
                return false;
            }
            if (string.IsNullOrEmpty(fileName))
            {
                string folder = $@"{Environment.CurrentDirectory}\Capture\{DateTime.Now.ToString("yyyy-MM-dd")}";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                fileName = $@"{folder}\{Guid.NewGuid()}.jpg";
            }
            NET_IN_SNAP_PIC_TO_FILE_PARAM snapIn = new NET_IN_SNAP_PIC_TO_FILE_PARAM();
            snapIn.stuParam = new NET_SNAP_PARAMS()
            {
                mode = 0,
                Channel = 0,
                Quality = 6,
                ImageSize = 2,
                InterSnap = 0,
                CmdSerial = m_SnapSerialNum
            };
            snapIn.szFilePath = fileName;
            snapIn.dwSize = (uint)Marshal.SizeOf(snapIn);
            NET_OUT_SNAP_PIC_TO_FILE_PARAM snapOut = new NET_OUT_SNAP_PIC_TO_FILE_PARAM();
            snapOut.dwPicBufLen = 4 * 1024 * 1024;
            snapOut.szPicBuf = Marshal.AllocHGlobal((int)snapOut.dwPicBufLen);
            snapOut.dwSize = (uint)Marshal.SizeOf(snapOut);
            bool ret = NETClient.SnapPictureToFile(LoginId, ref snapIn, ref snapOut, 3000);
            if (!ret)
            {
                msg = NETClient.GetLastError();
                img = null;
                return false;
            }
            byte[] data = new byte[snapOut.dwPicBufRetLen];
            Marshal.Copy(snapOut.szPicBuf, data, 0, (int)snapOut.dwPicBufRetLen);
            using (MemoryStream ms = new MemoryStream(data))
            {
                Image image = System.Drawing.Image.FromStream(ms);
                msg = $"抓拍成功!图片存放路径为:{fileName}";
                img = image;
                return true;
            }

        }
        #endregion


        #endregion


    }
}
