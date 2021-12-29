using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Joey.Lib.Utils
{
    public class ArrayHelper
    {
        #region 数组转16进制字符串
        public static string ToHexString(byte[] sourseArray)
        {
            if (sourseArray == null) return string.Empty;
            string str = string.Empty;
            foreach (byte b in sourseArray)
            {
                str += b.ToString("X2");
            }
            return str;
        }
        #endregion

        #region 数组转16进制字符串
        public static string ToHexStringWithSpace(byte[] sourseArray)
        {
            if (sourseArray == null) return string.Empty;
            string str = string.Empty;
            foreach (byte b in sourseArray)
            {
                str += b.ToString("X2") + " ";
            }
            return str;
        }
        #endregion

        #region 比较两个数组是否相等

        public static bool Compare(byte[] a, byte[] b)
        {
            if (a == null && b == null) return true;
            if (a == null && b != null) return false;
            if (a != null && b == null) return false;
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 获取目标数组在源数组中的下标
        public static int FirstIndexOf(byte[] sourseArray, byte[] targetArray)
        {
            if (targetArray == null || sourseArray == null) return -1;
            if (sourseArray.Length < targetArray.Length) return -1;
            for (int index = 0; index < sourseArray.Length - 5; index++)
            {
                for (int i = 0; i < targetArray.Length; i++)
                {
                    if (sourseArray[index + i] != targetArray[i])
                    {
                        break;
                    }
                    if (i == targetArray.Length - 1)
                    {
                        return index;
                    }
                }
            }
            return -1;
        }
        #endregion


    }
}
