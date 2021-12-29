using System;
using System.Collections.Generic;
using System.Linq;

namespace HPT.Gate.Device.Data
{

    /// <summary>
    /// 闸机命令
    /// </summary>
    public enum Command
    {
        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        GetAllDeviceInfo = 0x0010,
        /// <summary>
        /// 批量删除用户信息
        /// </summary>
        BatchDeleteCard = 0x0032,
        /// <summary>
        /// 添加/修改/删除卡信息
        /// </summary>
        CardEdit = 0x0031,
        /// <summary>
        /// 获取设备信息
        /// </summary>
        GetDeviceInfo = 0x0011,
        /// <summary>
        /// 请求获取网络参数
        /// </summary>
        GetNetWorkPara = 0x0015,
        /// <summary>
        /// 请求设置网络参数
        /// </summary>
        SetNetWorKPara = 0x0016,
        /// <summary>
        /// 获取显示屏参数
        /// </summary>
        GetSubDevices = 0x0017,
        /// <summary>
        /// 设置显示屏参数
        /// </summary>
        SetSubDevices = 0x0018,
        /// <summary>
        /// 请求刷卡记录
        /// </summary>
        GetRecord = 0x0051,
        /// <summary>
        /// 批量请求刷卡记录
        /// </summary>
        RequestRecords = 0x0052,
        /// <summary>
        /// 发送图片包
        /// </summary>
        SendImage = 0x61,
        /// <summary>
        /// 请求设置设备信息
        /// </summary>
        SetDeviceInfo = 0x0012,
        /// <summary>
        /// 请求设置设备时间
        /// </summary>
        SetDeviceTime = 0x0013,

        /// <summary>
        /// 获取设备时间
        /// </summary>
        GetDeviceTime = 0x0014,

        /// <summary>
        /// 请求设置正常日期时间组
        /// </summary>
        SetTimeGroupOfNormal = 0x0043,
        /// <summary>
        /// 请求设置节假日时间组
        /// </summary>
        SetTimeGroupOfVacation = 0x0042,
        /// <summary>
        /// 请求设置节假日信息
        /// </summary>
        SetVacation = 0x0041,
        /// <summary>
        /// 启动更新程序
        /// </summary>
        StartUpdate = 0x0071,

        /// <summary>
        /// 发送更新包
        /// </summary>
        Update = 0x0072,

        /// <summary>
        /// 设置读写卡密码
        /// </summary>
        SetCardPass = 0x001B,

        /// <summary>
        /// 设置门禁时间段内可刷卡的持续时间
        /// </summary>
        SetDurationOfDoorTimeGroup = 0x001C,

        /// <summary>
        /// 初始化系统
        /// </summary>
        Initialize = 0x0023,

        /// <summary>
        /// 激活主板
        /// </summary>
        ActiveMainBoard = 0x0021,

        /// <summary>
        /// 下载字库
        /// </summary>
        DownLoadFont = 0x0022,

        /// <summary>
        /// 获取中转器下所有机器号
        /// </summary>
        GetCanMachineIds = 0x001F,

        /// <summary>
        /// 设置Can中转器存储机器号
        /// </summary>
        SetCanMachineIds = 0x0020,

        /// <summary>
        /// 启动显示屏更新
        /// </summary>
        StartUpdateMonitor = 0x0063,

        /// <summary>
        /// 发送显示屏更新包
        /// </summary>
        UpdateMonitor = 0x0062,

        /// <summary>
        /// 获取软件参数
        /// </summary>
        GetSoftPara = 0x0019,

        /// <summary>
        /// 设置软件参数
        /// </summary>
        SetSoftPara = 0x001A,

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        GetServer = 0x001E,

        /// <summary>
        /// 设置服务器信息
        /// </summary>
        SetServer = 0x001D,

        /// <summary>
        /// 摄像头抓拍
        /// </summary>
        CameraCapture = 0x0035,

        /// <summary>
        /// 语音播报
        /// </summary>
        PlayVoice = 0x0036,

        /// <summary>
        /// 检查是否请假
        /// </summary>
        CheckOnLeave = 0x0037,

        /// <summary>
        /// 实时数据
        /// </summary>
        RealtimeData = 0x003B,

        /// <summary>
        /// 无线433获取设备信息
        /// </summary>
        Wireless433GetDeviceInfo = 0x0074,

        /// <summary>
        /// 心跳包
        /// </summary>
        HeartBeat = 0x0028,

        /// <summary>
        /// 获取未采集记录数
        /// </summary>
        GetUnCollected = 0x0029,

        /// <summary>
        /// 恢复记录重新采集
        /// </summary>
        RestoreRecords = 0x002A,

        /// <summary>
        /// 远程控制
        /// </summary>
        RemoteControl = 0x002B,
    };
}
