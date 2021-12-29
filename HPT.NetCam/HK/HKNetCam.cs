using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace HPT.NetCam.HK
{
    public class HKNetCam : NetCam
    {
        private int M_IUserId = -1;
        private CHCNetSDK.NET_DVR_DEVICEINFO_V30 M_deviceInfo;
        int m_lRealHandle = -1;
        public string IPAddress { get; set; }

        public string Password { get; set; }

        public ushort Port { get; set; }

        public string UserName { get; set; }

        public bool Capture(out string msg, out Image img,string fileName)
        {
            if (M_IUserId < 0)
            {
                msg = $"摄像头[{IPAddress}]尚未登录,抓拍失败!";
                img = null;
                return false;
            }
            if (string.IsNullOrEmpty(fileName))
            {
                string folder = $@"{Environment.CurrentDirectory}\NetCam\Capture\{DateTime.Now.ToString("yyyy-MM-dd")}";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                fileName = $@"{folder}\{Guid.NewGuid()}.jpg";
            }
            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 2; //图像质量 Image quality0最好,1较好，2一般
                                        // lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档
            lpJpegPara.wPicSize = 0x04; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(M_IUserId, 1, ref lpJpegPara, fileName))
            {
                uint retult = CHCNetSDK.NET_DVR_GetLastError();
                msg = $"摄像头[{IPAddress}]抓拍失败,错误代码:{retult}";
                img = null;
                return false;
            }
            else
            {
                msg = $"摄像头[{IPAddress}]抓拍成功!图片存放路径为:{fileName}";
                img = Image.FromFile(fileName);
                return true;
            }
        }

        public bool Login(out string msg)
        {
            if (M_IUserId >= 0)
            {
                msg = $"摄像头[{IPAddress}]已登录!";
                return true;
            }
            M_deviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

            //登录设备 Login the device
            M_IUserId = CHCNetSDK.NET_DVR_Login_V30(IPAddress, Port, UserName, Password, ref M_deviceInfo);
            if (M_IUserId < 0)
            {
                uint result = CHCNetSDK.NET_DVR_GetLastError();
                msg = $"摄像头[{IPAddress}]登录失败,错误代码:{result}";
                return false;
            }
            msg = $"摄像头[{IPAddress}]登录成功!";
            return true;
        }

        public bool LogOut(out string msg)
        {
            if (M_IUserId >= 0)
            {
                if (!CHCNetSDK.NET_DVR_Logout(M_IUserId))
                {
                    uint result = CHCNetSDK.NET_DVR_GetLastError();
                    msg = $"摄像头[{IPAddress}]关闭失败，错误代码:{result}";
                    return false;
                }
            }
            M_IUserId = -1;
            msg = $"摄像头[{IPAddress}]关闭成功!";
            return true;
        }

        public bool StartPreview(IntPtr handler, out string msg)
        {
            if (M_IUserId < 0)
            {
                msg = $"摄像头[{IPAddress}]尚未登录,无法预览!";
                return false;
            }
            CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
            lpPreviewInfo.hPlayWnd = handler;//预览窗口
            lpPreviewInfo.lChannel = 1;//预te览的设备通道
            lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
            lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
            lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
            lpPreviewInfo.dwDisplayBufNum = 15; //播放库播放缓冲区最大缓冲帧数

            //CHCNetSDK.REALDATACALLBACK RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
            IntPtr pUser = new IntPtr();//用户数据

            //打开预览 Start live view 
            m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(M_IUserId, ref lpPreviewInfo, null/*RealData*/, pUser);
            if (m_lRealHandle < 0)
            {
                uint result = CHCNetSDK.NET_DVR_GetLastError();
                msg = $"摄像头[{IPAddress}]打开视频预览失败,错误代码:{result}";
                return false;
            }
            msg = $"摄像头[{IPAddress}]打开视频预览成功!预览通道001";
            return false;
        }

        public bool StopPreview(out string msg)
        {
            if (M_IUserId <= 0)
            {
                m_lRealHandle = -1;
                msg = $"摄像头[{IPAddress}]关闭预览成功!";
                return true;
            }
            if (m_lRealHandle >= 0)
            {
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    uint retult = CHCNetSDK.NET_DVR_GetLastError();
                    msg = $"摄像头[{IPAddress}]关闭预览失败,错误代码:{retult}";
                    return false;
                }
            }
            m_lRealHandle = -1;
            msg = $"摄像头[{IPAddress}]关闭预览成功!";
            return false;
        }
    }
}
