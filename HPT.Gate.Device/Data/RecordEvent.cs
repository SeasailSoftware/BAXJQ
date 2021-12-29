using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{

    public enum RecordEvent
    {
        [Description("有效票")]
        RESULT_TICKET_IS_OK = 0x00,  //--有效票
        [Description("未查到票信息")]
        RESULT_NO_TICKET_NUM = 0x01,  //--未查到票信息
        [Description("黑名单")]
        RESULT_IS_BLACK_NAME = 0x02,  //--黑名单
        [Description("未授权")]
        RESULT_UNAUTHORIZED = 0x03,  //--未授权
        [Description("入口无权限")]
        RESULT_CAN_NOT_IN = 0x04,  //--入口无权限
        [Description("出口无权限")]
        RESULT_CAN_NOT_OUT = 0x05,  //--出口无权限
        [Description("该时段禁止通行")]
        RESULT_NOT_PERIOD_TIME = 0x06, //--该时段禁止通行
        [Description("该卡已过有效期")]
        RESULT_OUT_OF_TIME = 0x07,  //--该卡已过有效期
        [Description("该卡已存在")]
        RESULT_CARD_EXISTING = 0x08,  //--该卡已存在
        [Description("读卡失败")]
        RESULT_READ_IC_ERROR = 0x09,  //--读卡失败
        [Description("该卡未进场")]
        RESULT_NOT_IN_ERROR = 0x0A,  //--该卡未进场
        [Description("该卡未出场")]
        RESULT_NOT_OUT_ERROR = 0x0B,  //--该卡未出场
        [Description("次数已经使用完!")]
        RESULT_Limit_Nums_ERROR = 0x0C,  //--限次
        [Description("已超出有效时段!")]
        RESULT_Limit_Time_ERROR = 0x0D,  //--限时
        [Description("有效未过闸")]
        RESULT_TICKET_OK_NOT_GO = 0x10, //--有效未过闸
        [Description("实时请求无响应")]
        RESULT_Realtime_UnAccept = 0x0F, //--实时请求无响应
        [Description("未识别的指纹")]
        RESULT_UNKNOWN_FINGERPRINT = 0x11, //--未识别的指纹
        [Description("指纹验证通过")]
        RESULT_FINGERPRINT_IDENTITY_SUCCESS = 0x12,
        [Description("已启用反潜回")]
        RESULT_AntiSubmarine = 0x13,
        [Description("超出限制的总数")]
        RESULT_Out_Of_LimitedTimes = 0x14,
        [Description("人数达上限请等待")]
        RESULT_Out_Of_LimitedTotal = 0x15,
        [Description("违反反潜规则")]
        RESULT_Break_AntiSubmarine_Rule = 0x16,


    }
}
