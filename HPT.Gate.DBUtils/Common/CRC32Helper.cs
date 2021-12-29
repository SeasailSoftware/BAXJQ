using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Utils.Common
{
    public class CRC32Helper
    {
        public static uint dwPolynomial = 0x04c11db7;
        //--RCR32校验
        public static UInt32 Cal_CRC(UInt32[] ptr)
        {
            int len = ptr.Length;
            UInt32 xbit;
            UInt32 data;
            UInt32 _CRC = 0xFFFFFFFF; // init
            uint i = 0;
            while ((len > 0))
            {
                len--;
                xbit = ((UInt32)1) << 31;

                //data = *(ptr++);
                data = ptr[i++];
                for (int bits = 0; bits < 32; bits++)
                {
                    if ((_CRC & 0x80000000) > 0)
                    {
                        _CRC <<= 1;
                        _CRC ^= dwPolynomial;
                    }
                    else
                        _CRC <<= 1;
                    if ((data & xbit) > 0)
                        _CRC ^= dwPolynomial;

                    xbit >>= 1;
                }
            }
            return _CRC;
        }
        /// <summary>
        /// 将byte[] 转成 4个字节为一组的uint32数组
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static UInt32[] ByteToUnit32(byte[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            int length = arr.Length % 4 == 0 ? arr.Length / 4 : arr.Length / 4 + 1;
            UInt32[] uint32 = new UInt32[length];

            for (int i = 0; i < length; i++)
            {
                int index = 4 * i;
                byte[] curByte = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                int curLen = (int)arr.Length - index < 4 ? (int)arr.Length - index : 4;
                Array.Copy(arr, 4 * i, curByte, 0, curLen);
                uint32[i] = BitConverter.ToUInt32(curByte, 0);
            }
            return uint32;
        }

        public static byte[] GetCrc32(byte[] arr)
        {
            UInt32[] uint32 = CRC32Helper.ByteToUnit32(arr);
            UInt32 crc32 = CRC32Helper.Cal_CRC(uint32);
            return BitConverter.GetBytes(crc32);
        }
    }
}
