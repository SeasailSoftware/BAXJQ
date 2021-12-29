using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Utils.Common
{
    public static class ArrayHelper
    {

        /// <summary>
        /// Unicode转gb2312
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string UnicodeToGB2312(byte[] b)
        {
            if (b == null)
            {
                return string.Empty;
            }
            string s = System.Text.Encoding.GetEncoding("Unicode").GetString(b).Trim();
            return s;
        }
        /// <summary>
        /// 去掉回车符号
        /// </summary>
        /// <param name="AreaText"></param>
        /// <returns></returns>
        public static byte[] RemoveReturn(byte[] AreaText)
        {
            byte[] newByte = null;
            for (int i = 0; i < AreaText.Length; i++)
            {
                if (AreaText[i] != 0x0a)
                {
                    newByte = AddBytes(newByte, new byte[] { AreaText[i] });
                }
            }
            return newByte;
        }
        /// <summary>
        /// 重写Equals
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Compare(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断发送包头是否正确
        /// </summary>
        /// <param name="sourseArray"></param>
        /// <returns></returns>
        public static int FirstHeaderSendOf(byte[] sourseArray)
        {
            byte[] header = new byte[5];
            header[0] = 0x5A;
            header[1] = 0xA5;
            header[2] = 0x0F;
            header[3] = 0x55;
            header[4] = 0xAA;
            if (sourseArray.Length < 5)
            {
                return -1;
            }
            for (int index = 0; index < sourseArray.Length - 5; index++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (sourseArray[index + i] != header[i])
                    {
                        break;
                    }
                    if (i == 4)
                    {
                        return index;
                    }
                }
            }
            return -1;
        }


        /// <summary>
        /// 获取字节数组中第一个数据包头的位置，没返回-1
        /// </summary>
        /// <param name="sourseArray"></param>
        /// <returns></returns>
        public static int FirstHeaderOf(byte[] sourseArray)
        {
            byte[] header = new byte[5];
            header[0] = 0xA5;
            header[1] = 0x5A;
            header[2] = 0xF0;
            header[3] = 0xAA;
            header[4] = 0x55;
            if (sourseArray.Length < 5)
            {
                return -1;
            }
            for (int index = 0; index < sourseArray.Length - 5; index++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (sourseArray[index + i] != header[i])
                    {
                        break;
                    }
                    if (i == 4)
                    {
                        return index;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 比较两个字节数组是否相等
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static bool ISEquals(byte[] b1, byte[] b2)
        {
            for (int i = 0; i < 6; i++)
            {
                if (b1[i] == b2[i])
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 将10进制数转化为指定位数的byte数组
        /// </summary>
        /// <param name="a"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] DecimalToHexArray(int a, int length)
        {
            byte[] newbytes = new byte[length];
            if (length != 0)
            {
                string s = a.ToString("x" + (length * 2).ToString());
                for (int i = 0; i < length; i++)
                {
                    string str = s.Substring(i * 2, 2);
                    if (str.Equals("0") || str.Equals("00"))
                    {
                        newbytes[i] = 0x00;
                    }
                    else
                    {
                        newbytes[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
                    }


                }

                return newbytes;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 物理地址转化为16进制byte数组
        /// </summary>
        /// <returns></returns>
        public static byte[] MacToHexArray(string mac)
        {
            byte[] bytes = new byte[6];
            mac = mac.Replace("-", "").Replace(" ", "").Trim();
            if (mac.Length == 12)
            {
                for (int i = 0; i < 6; i++)
                {
                    bytes[i] = Convert.ToByte(mac.Substring(i * 2, 2), 16);
                }
            }
            return bytes;
        }

        #region 十进制卡号转十六进制卡号
        public static string IntCardNoToHexCardNo(string cardNo)
        {
            string hexCardNo = string.Empty;
            try
            {
                UInt32 intCardNo = Convert.ToUInt32(cardNo);
                byte[] arr = BitConverter.GetBytes(intCardNo);
                hexCardNo = ArrayToHex(arr);
            }
            catch
            {

            }
            return hexCardNo;
        }
        #endregion

        /// <summary>
        /// 截取数组从第i个下标开始长度为j的数组
        /// </summary>
        /// <param name="b"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static byte[] SubByte(byte[] b, int i, int j)
        {
            var NewByte = new Byte[j];
            try
            {
                for (var p = 0; p < j; p++)
                {
                    NewByte[p] = b[i + p];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return NewByte;
        }
        /// <summary>
        /// 两个数组相加
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte[] AddBytes(byte[] a, byte[] b)
        {
            if (a == null)
            {
                return b;
            }
            if (b == null)
            {
                return a;
            }
            int lenA = a.Length;
            int lenB = b.Length;
            byte[] c = new byte[lenA + lenB];
            Array.Copy(a, 0, c, 0, lenA);
            Array.Copy(b, 0, c, lenA, lenB);
            return c;
        }

        /// <summary>
        /// 16进制字符数组转GB2312
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ArrayToGB2312(byte[] b)
        {
            if (b == null)
            {
                return string.Empty;
            }
            string s = System.Text.Encoding.GetEncoding("gb2312").GetString(b).Replace("\0", "").Trim();
            return s;
        }

        /// <summary>
        /// GB2312 转指定长度的数组
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GB2312ToArray(string s, int length)
        {
            if (length <= 0)
            {
                return null;
            }
            List<byte> list = new List<byte>();
            //byte[] reByte = new byte[length];
            byte[] byteArray = System.Text.Encoding.GetEncoding("gb2312").GetBytes(s);
            if (byteArray.Length <= length)
            {
                list.AddRange(byteArray);
                for (int i = 0; i < length - byteArray.Length; i++)
                {
                    list.Add(0x20);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    list.Add(byteArray[i]);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 时间转化为对应格式的数组
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static byte[] DateTimeToArray(DateTime dateTime)
        {
            byte[] reTime = new byte[8];
            reTime[0] = Convert.ToByte(((int)dateTime.DayOfWeek).ToString(), 16);
            reTime[1] = Convert.ToByte(((int)dateTime.Second).ToString(), 16);
            reTime[2] = Convert.ToByte(((int)dateTime.Minute).ToString(), 16);
            reTime[3] = Convert.ToByte(((int)dateTime.Hour).ToString(), 16);
            reTime[4] = Convert.ToByte(((int)dateTime.Day).ToString(), 16);
            reTime[5] = Convert.ToByte(((int)dateTime.Month).ToString(), 16);
            reTime[6] = Convert.ToByte((((int)dateTime.Year % 100)).ToString(), 16);
            reTime[7] = Convert.ToByte((((int)dateTime.Year / 100)).ToString(), 16);
            return reTime;
        }

        /// <summary>
        /// 时间转化为对应格式的数组
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static byte[] DateToArray(DateTime dateTime)
        {
            byte[] reTime = new byte[4];
            reTime[0] = Convert.ToByte(((int)dateTime.Day).ToString(), 16);
            reTime[1] = Convert.ToByte(((int)dateTime.Month).ToString(), 16);
            reTime[2] = Convert.ToByte((((int)dateTime.Year % 100)).ToString(), 16);
            reTime[3] = Convert.ToByte((((int)dateTime.Year / 100)).ToString(), 16);
            return reTime;
        }

        /// <summary>
        /// 时间转化为对应格式的数组
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static byte[] DateToArray1(DateTime dateTime)
        {
            byte[] reTime = new byte[3];
            reTime[0] = Convert.ToByte(((int)dateTime.Day).ToString(), 16);
            reTime[1] = Convert.ToByte(((int)dateTime.Month).ToString(), 16);
            reTime[2] = Convert.ToByte((((int)dateTime.Year % 100)).ToString(), 16);
            return reTime;
        }


        /// <summary>
        /// int64转字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] IntToBytes(UInt64 value)
        {
            byte[] newByte = new byte[4];
            byte[] cByte = System.BitConverter.GetBytes(value);
            for (int i = 0; i < 4; i++)
            {
                newByte[i] = cByte[3 - i];
            }
            return newByte;
        }
        /// <summary>
        /// Int 类型转byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] IntToBytes(Int32 value, int length)
        {
            if (length == 1)
            {
                return new byte[1] { (byte)value };
            }
            byte[] newByte = new byte[length];
            byte[] cByte = System.BitConverter.GetBytes(value);
            byte[] Bint = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                Bint[i] = cByte[3 - i];
            }
            if (length == 4)
            {
                newByte = Bint;
            }
            if (length < Bint.Length && length > 0)
            {
                for (int k = 0; k < length; k++)
                {
                    newByte[k] = Bint[length + k];
                }
                //Array.Copy(Bint, 0, newByte, Bint.Length-length, length);
            }
            else
            {
                for (int i = 0; i < length - Bint.Length; i++)
                {
                    newByte[i] = 0x00;
                }
                Array.Copy(Bint, 0, newByte, length - Bint.Length, Bint.Length);
            }
            return newByte;
        }
        /// <summary>
        /// 将IP地址转byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] IPToArray(string s)
        {
            byte[] arr = new byte[4];
            string[] str = s.Split('.');
            arr[0] = (byte)Convert.ToInt32(str[0]);
            arr[1] = (byte)Convert.ToInt32(str[1]);
            arr[2] = (byte)Convert.ToInt32(str[2]);
            arr[3] = (byte)Convert.ToInt32(str[3]);
            return arr;

        }
        /// <summary>
        /// byte数组中取int数值，本方法适用于(低位在后，高位在前)的顺序。和intToBytes2（）配套使用 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        /// <returns></returns>    
        public static int bytesToInt(byte src)
        {
            int value;
            value = (int)src;
            return value;
        }
        /// <summary>
        /// byte数组中取int数值，本方法适用于(低位在后，高位在前)的顺序。和intToBytes2（）配套使用 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        /// <returns></returns>    
        public static int bytesToInt(byte[] src)
        {
            int value = 0;
            if (src.Length == 4)
            {
                value = src[0] << 24 | src[1] << 16 | src[2] << 8 | src[3];
            }
            if (src.Length == 2)
            {
                value = src[0] << 8 | src[1];
            }
            if (src.Length == 1)
            {
                value = (int)src[0];
            }
            return value;
        }

        /// <summary>
        /// 数组转对应格式的时间字符串
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToDateTimeString(byte[] arr)
        {
            if (arr.Length == 0)
            {
                return string.Empty;
            }
            string s = string.Empty;
            s = "20" + StringHelper.ToHexString(arr);
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
        /// 时间转数组
        /// </summary>
        /// <param name="beginTme"></param>
        /// <returns></returns>
        public static byte[] DateTimeToArray(string time)
        {
            byte[] newByte = new byte[2];
            string[] strArr = time.Split(':');
            ///时间格式不对
            if (strArr.Length < 2 || strArr.Length > 3)
            {
                return new byte[] { 0xFF, 0xFF };
            }
            int minute = Convert.ToInt32(strArr[0]);
            int second = Convert.ToInt32(strArr[1]);
            ///时间错误
            if (minute > 60 || second > 60)
            {
                return new byte[] { 0xFF, 0xFF };
            }
            newByte[0] = Convert.ToByte(strArr[0], 16);
            newByte[1] = Convert.ToByte(strArr[1], 16);
            return newByte;
        }

        /// <summary>
        /// 时间转数组
        /// </summary>
        /// <param name="beginTme"></param>
        /// <returns></returns>
        public static byte[] DateTimeToArray1(string time)
        {
            byte[] newByte = new byte[2];
            string[] strArr = time.Split(':');
            ///时间格式不对
            if (strArr.Length < 2 || strArr.Length > 3)
            {
                return new byte[] { 0xFF, 0xFF };
            }
            int hout = Convert.ToInt32(strArr[0]);
            int minute = Convert.ToInt32(strArr[1]);
            ///时间错误
            if (hout >= 24 || minute >= 60)
            {
                return new byte[] { 0xFF, 0xFF };
            }
            newByte[0] = Convert.ToByte(strArr[1], 16);
            newByte[1] = Convert.ToByte(strArr[0], 16);
            return newByte;
        }

        /// <summary>
        /// Array 转MAC
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToMAC(byte[] arr)
        {
            string mac = string.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                mac += arr[i].ToString("X2") + "-";
            }
            mac = mac.Substring(0, mac.LastIndexOf("-"));
            return mac;
        }

        /// <summary>
        /// Array 转MAC
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToIPAdress(byte[] arr)
        {
            string s = string.Empty;
            s += arr[0].ToString() + ".";
            s += arr[1].ToString() + ".";
            s += arr[2].ToString() + ".";
            s += arr[3].ToString();
            return s;
        }

        /// <summary>
        /// 十六进制字符串转byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexToArray(string s, int length)
        {
            byte[] arr = new byte[length];
            s = s.Trim();
            if (s.Equals(string.Empty))
            {
                return null;
            }
            int len = s.Length;
            if (s.Length < (length * 2))
            {
                for (int i = 0; i < (length * 2 - len); i++)
                {
                    s = "0" + s;
                }
            }
            if (len > (length * 2))
            {
                s = s.Substring(0, length * 2);
            }

            for (int j = 0; j < length; j++)
            {
                arr[j] = Convert.ToByte(s.Substring((j * 2), 2), 16);
            }

            return arr;
        }
        /// <summary>
        /// 身份证转为byte[]
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static byte[] IdentityToArray(string cardNo)
        {
            byte[] arr = new byte[4];
            int length = cardNo.Trim().Length;
            string str = string.Empty;
            if (length > 8)
            {
                str = cardNo.Substring(length - 8, 8);
                str = str.ToUpper().Replace("X", "A");
                arr = HexToArray(str, 4);
            }
            return arr;
        }
        /// <summary>
        /// byte[] 转16进制字符串
        /// </summary>
        /// <returns></returns>
        public static string ArrayToHex(byte[] bytes)
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
        /// 将数组转化为时间字符串
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <returns></returns>
        public static string ArrayToTime(byte[] time)
        {
            byte[] arr1 = time[0] == 0xFF ? new byte[] { 0x00 } : new byte[] { time[0] };
            byte[] arr2 = time[1] == 0xFF ? new byte[] { 0x00 } : new byte[] { time[1] };
            return ArrayHelper.ArrayToHex(arr1) + ":" + ArrayHelper.ArrayToHex(arr2);
        }

        /// <summary>
        /// 字符串转维根26卡号
        /// </summary>
        /// <param name="_OriginalCardNo"></param>
        /// <returns></returns>
        public static string ToWG26CardNo(string _OriginalCardNo)
        {
            int temp1 = Convert.ToInt32(_OriginalCardNo.Substring(0, 3));
            int temp2 = Convert.ToInt32(_OriginalCardNo.Substring(3, 5));
            byte[] arr1 = IntToBytes(temp1, 2);
            byte[] arr2 = IntToBytes(temp2, 2);
            return ArrayToHex(AddBytes(arr1, arr2));
        }

        #region 十进制卡号转4个字节IC/ID卡号
        public static string ToWG34CardNo(string cardNo, int type)
        {
            string hexCardNo = string.Empty;
            UInt32 value = 0;
            try
            {
                value = Convert.ToUInt32(cardNo);
            }
            catch
            {
                return hexCardNo;
            }
            byte[] arr = BitConverter.GetBytes(value);
            if (type == 1)
                Array.Reverse(arr);
            return ArrayToHex(arr);
        }
        #endregion

    }
}
