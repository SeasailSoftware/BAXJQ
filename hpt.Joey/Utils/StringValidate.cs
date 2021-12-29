using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HPT.Joey.Lib.Utils
{
    public static class StringValidate
    {
        public static string Pattern_Number = @"^[0-9]*$";
        public static string Pattern_HexString = "^[A-Fa-f0-9]+$";
        public static string Pattern_IDCardNo = @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$";
        public static string Pattern_IPAddress = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";


        #region 验证是否数字
        public static bool IsNumber(string buffer)
        {
            return Regex.IsMatch(buffer,Pattern_Number);
        }
        #endregion

        #region 验证是否十六进制字符串
        public static bool IsHexString(string buffer)
        {
            return Regex.IsMatch(buffer, Pattern_HexString);
        }
        #endregion

        #region 验证是否身份证号码
        public static bool IsIDCardNo(string buffer)
        {
            return Regex.IsMatch(buffer, Pattern_IDCardNo);
        }
        #endregion

        #region 检查是否人员编号
        public static bool IsEmpCode(string buffer)
        {
            if (Regex.IsMatch(buffer, Pattern_HexString))
                return Encoding.GetEncoding("gb2312").GetBytes(buffer).Length <= 8;
            return false;
        }
        #endregion

        #region 检查是否为IC/ID卡号
        public static bool IsICIDCardNo(string buffer)
        {
            if (Regex.IsMatch(buffer, Pattern_HexString))
                return Encoding.GetEncoding("gb2312").GetBytes(buffer).Length == 8;
            return false;
        }
        #endregion

        #region 检查是否为IC/ID卡号
        public static bool IsIDSerial(string buffer)
        {
            if (Regex.IsMatch(buffer, Pattern_HexString))
                return Encoding.GetEncoding("gb2312").GetBytes(buffer).Length == 16;
            return false;
        }
        #endregion

        #region 检查是否IP地址
        public static bool IsIPAddress(string buffer)
        {
            return Regex.IsMatch(buffer, Pattern_IPAddress);
        }
        #endregion



    }
}
