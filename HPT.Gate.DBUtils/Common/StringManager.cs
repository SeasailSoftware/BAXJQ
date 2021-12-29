using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Utils.Common
{
    public class StringHelper
    {
        /// <summary>
        /// 时间段转16进制字符串
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static string TimeGroupToHexString(string beginTime, string endTime)
        {
            if (beginTime.Equals(endTime))
            {
                return "FFFFFFFF";
            }
            else
            {
                var c = ':';
                var begin = beginTime.Split(c);
                var s_begin = begin[1] + begin[0];
                var end = endTime.Split(c);
                var s_end = end[1] + end[0];
                return (s_begin + s_end).ToUpper();
            }
        }
        /// <summary>
        /// 十进制转ASCLL
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DecimalToASCLL(string s, int len)
        {
            var ascll = string.Empty;
            for (var i = 0; i < s.Length; i++)
            {
                ascll += (Convert.ToInt32(s[i]) + 30).ToString();
            }
            if (ascll.Length < len)
            {
                for (var i = 0; i < len - ascll.Length; i++)
                {
                    ascll = "20" + ascll;
                }
            }
            return ascll;
        }

        /// <summary>
        /// 将时间格式字符串转化为对应的日期格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string DateToHexString(string time)
        {
            if (time.Equals("FFFF-FF-FF"))
            {
                return "FFFFFF";
            }
            else
            {
                var sp = '-';
                var str = time.Split(sp);
                if (str.Length < 3)
                {
                    return time;
                }
                else
                {
                    var year = Convert.ToInt32(str[0].ToString());
                    var year1 = year / 100;
                    var year2 = year % 100;
                    var month = Convert.ToInt32(str[1].ToString());
                    var day = Convert.ToInt32(str[2].ToString());

                    var s_year1 = year1 >= 10 ? year1.ToString() : "0" + year1.ToString();
                    var s_year2 = year2 >= 10 ? year2.ToString() : "0" + year2.ToString();
                    var s_month = month >= 10 ? month.ToString() : "0" + month.ToString();
                    var s_day = day >= 10 ? day.ToString() : "0" + day.ToString();
                    return s_day + s_month + s_year2 + s_year1;
                }
            }
        }

        /// <summary>
        /// 将时间格式字符串转化为对应的时间格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string TimeToHexString(string time)
        {
            if (time.Equals("FF:FF"))
            {
                return "FFFF";
            }
            else
            {
                var sp = ':';
                var str = time.Split(sp);
                if (str.Length < 2)
                {
                    return time;
                }
                else
                {
                    var hour = Convert.ToInt32(str[0].ToString());
                    var minute = Convert.ToInt32(str[1].ToString());

                    var s_hour = hour >= 10 ? hour.ToString() : "0" + hour.ToString();
                    var s_minute = minute >= 10 ? minute.ToString() : "0" + minute.ToString();
                    return s_minute + s_hour;
                }
            }
        }
        /// <summary>
        /// 将日期转换为BCD格式的字符串
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string DatetimeToHexString(DateTime time)
        {
            var year1 = time.Year % 100;
            var s_year1 = year1 >= 10 ? year1.ToString() : "0" + year1.ToString();
            var year2 = time.Year / 100;
            var s_year2 = year2 >= 10 ? year2.ToString() : "0" + year2.ToString();
            var month = time.Month;
            var s_month = month >= 10 ? month.ToString() : "0" + month.ToString();
            var day = time.Day;
            var s_day = day >= 10 ? day.ToString() : "0" + day.ToString();
            var week = (int)time.DayOfWeek;
            var s_week = "0" + week.ToString();
            var hour = time.Hour;
            var s_hour = hour >= 10 ? hour.ToString() : "0" + hour.ToString();
            var minute = time.Minute;
            var s_minute = minute >= 10 ? minute.ToString() : "0" + minute.ToString();
            var second = time.Second;
            var s_second = second >= 10 ? second.ToString() : "0" + second.ToString();

            return s_week + s_second + s_minute + s_hour + s_day + s_month + s_year1 + s_year2;
        }
        /// <summary>
        /// 将十进制转化为BCD
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int IntToBCD(byte val)
        {
            var res = 0;
            var bit = 0;
            while (val >= 10)
            {
                res |= (val % 10 << bit);
                val /= 10;
                bit += 4;
            }
            res |= val << bit;
            return res;
        }
        private byte ConvertBCD(string str)
        {
            return Convert.ToByte(str);
        }
        /// <summary>
        /// 判断是否十进制字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDecimalString(string s)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(s, "^\\d+$");
        }
        /// <summary>
        /// IP地址校验
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public static Boolean CheckIPValid(String strIP)
        {
            var chrFullStop = '.';
            var arrOctets = strIP.Split(chrFullStop);
            if (arrOctets.Length != 4)
            {
                return false;
            }

            var MAXVALUE = 255;
            Int32 temp;
            foreach (String strOctet in arrOctets)
            {
                if (strOctet.Length > 3)
                {
                    return false;
                }

                temp = int.Parse(strOctet);
                if (temp > MAXVALUE)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断IP地址是否有效，正则表达式判断法
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public static Boolean IPIsValid(String strIP)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIP, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
        }

        /// <summary>
        /// MAC地址转换成16进制字符串
        /// </summary>
        /// <param name="MAC"></param>
        /// <returns></returns>
        public static string MACToHexString(string MAC)
        {
            return MAC.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(":", string.Empty);
        }
        /// <summary>
        /// 检验是否匹配的MAC地址
        /// </summary>
        /// <param name="MAC"></param>
        /// <returns></returns>
        public static bool MACIsMacth(string MAC)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(MAC, "^([0-9a-fA-F]{2})(([/\\s:-][0-9a-fA-F]{2}){5})$");
        }
        /// <summary>
        /// GB2312转换成指定长度的16进制字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GBKToHexString(string s, int length)
        {
            if (s.Length == 0)
            {
                throw new Exception("GBK转16进制字符串出错，错误信息:被转换的字符串长度为0.");
            }
            byte[] arrByte = System.Text.Encoding.GetEncoding("GB2312").GetBytes(s);
            byte[] arrHex = new byte[length];
            if (arrByte.Length >= length)
            {
                Array.Copy(arrByte, arrHex, length);
            }
            else
            {
                Array.Copy(arrByte, arrHex, arrByte.Length);
            }
            return ToHexString(arrHex);

            /*var temp = length / 4;

            if (s.Length > temp)
            {
                s = s.Substring(0, temp);
            }
            var result = string.Empty;

            var arrByte = System.Text.Encoding.GetEncoding("GB2312").GetBytes(s);
            for (var i = 0; i < arrByte.Length; i++)
            {
                result += System.Convert.ToString(arrByte[i], 16);
            }
            var p = result.Length;
            if (p < length)
            {
                for (var i = 0; i < length - p; i++)
                {
                    result += "0";
                }
            }
            return result.ToUpper();
             * */
        }


        /// <summary>
        /// 十进制字符串转换成16进制的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DecimalToHex(string s)
        {
            if (s.Length == 0)
            {
                throw new Exception("十进制字符串转16进制字符串出错，错误信息:被转换的字符串长度为0.");
            }
            s = s.Replace(" ", string.Empty);
            if (!System.Text.RegularExpressions.Regex.IsMatch(s, "^\\d+$"))
            {
                throw new Exception("十进制字符串转16进制字符串出错，错误信息:无效的十进制字符串！.");
            }
            return Convert.ToInt32(s).ToString("X");
        }
        /// <summary>
        /// 十进制字符串转换成指定长度的16进制字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string DecimalToHex(string s, int length)
        {
            if (s.Trim().Equals("FFFFFFFF"))
            {
                return s;
            }
            if (s.Length == 0)
            {
                throw new Exception("十进制字符串转16进制字符串出错，错误信息:被转换的字符串长度为0.");
            }
            s = s.Replace(" ", string.Empty);
            if (!System.Text.RegularExpressions.Regex.IsMatch(s, "^\\d+$"))
            {
                throw new Exception("十进制字符串转16进制字符串出错，错误信息:无效的十进制字符串！.");
            }
            return Convert.ToInt64(s).ToString("X" + length.ToString());
        }

        /// <summary>
        /// 16进制字符串转GB2312
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string HexStringToGBK(string hex)
        {
            if (hex == null)
            {
                throw new ArgumentNullException("无效的16进制字符串！");
            }
            if (hex.Length % 2 != 0)
            {
                hex += "20";
            }

            var bytes = new byte[hex.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                try
                {
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    throw new ArgumentException("16进制字符串转汉字出错，错误信息：不是有效的16进制字符串。", "hex");
                }
            }


            var chs = System.Text.Encoding.GetEncoding("gb2312");


            return chs.GetString(bytes).Replace(@"\0", "");
        }
        /// <summary>
        /// 将16进制字符串转byte数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string s)
        {
            if (s.Length == 0)
            {
                throw new Exception("将16进制字符串转换成字节数组时出错，错误信息：被转换的字符串长度为0。");
            }
            s = s.Replace(" ", string.Empty);
            if (!System.Text.RegularExpressions.Regex.IsMatch(s, "^[0-9A-Fa-f]+$"))
            {
                throw new Exception("将16进制字符串转换成字节数组时出错,错误信息:" + s + "不是有效的16进制字符串");
            }
            ;
            if (s.Length % 2 > 0)
            {
                s = "0" + s;
            }
            var buffer = new byte[s.Length / 2];
            for (var i = 0; i < s.Length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
            }
            return buffer;
        }

        /// <summary>
        /// byte[]数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes)
        {
            var hexString = string.Empty;
            if (bytes != null)
            {
                var strB = new StringBuilder();
                for (var i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        /// <summary>
        /// 将字符串转为ascii
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringToASCII(string s)
        {
            int length = s.Length;
            string returnString = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string str = s.Substring(i, 1);
                byte[] array = System.Text.Encoding.ASCII.GetBytes(str);
                int ASCII = (int)array[0];
                returnString += StringHelper.DecimalToHex(ASCII.ToString(), 2);
            }
            int l = returnString.Length;
            for (int j = 0; j < (16 - l) / 2; j++)
            {
                returnString = "20" + returnString;
            }
            return returnString;
        }

        /// <summary>
        ///数组转响应格式的时间字符串
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToDateTime(byte[] arr)
        {
            if (arr.Length == 0)
            {
                return string.Empty;
            }
            string s = string.Empty;
            s = "20" + ToHexString(arr);
            ///非法时间字符串
            if (!System.Text.RegularExpressions.Regex.IsMatch(s, "^\\d+$"))
            {
                return "1900-01-01 00:00:00";
            }
            string year = s.Substring(0, 4);
            string month = s.Substring(4, 2);
            string date = s.Substring(6, 2);
            string hour = s.Substring(8, 2);
            string minute = s.Substring(10, 2);
            string second = s.Substring(12, 2);

            return year + "-" + month + "-" + date + " " + hour + ":" + minute + ":" + second;
        }
        /// <summary>
        /// arry to gbk
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToGBK(byte[] arr)
        {
            var gbk = string.Empty;
            gbk = System.Text.Encoding.GetEncoding("gb2312").GetString(arr);
            gbk = gbk.Replace("\u0000", "");
            return gbk;
        }
        /// <summary>
        /// 将字符串s 转成长度为length的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Format(string value, int length)
        {
            value = value.Trim();
            int len = value.Length;
            if (len >= length)
                return value.Substring(0, length);
            for (int i = 0; i < length - len; i++)
            {
                value = value + " ";
            }
            /*
            if (value[length - 1] == ' ')
                value = value.Substring(0, length - 1) + "0";
                */
            return value;
        }

        public static string Format(int value, int length)
        {
            string format = string.Empty;
            for (int i = 0; i < length; i++)
            {
                format += "0";
            }
            return value.ToString(format);
        }

        #region 校验字符串是否数字字符串
        public static bool isNumberic(string message)
        {
            System.Text.RegularExpressions.Regex rex =
            new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(message))
            {
                return true;
            }
            else
                return false;
        }

        #endregion

    }
}
