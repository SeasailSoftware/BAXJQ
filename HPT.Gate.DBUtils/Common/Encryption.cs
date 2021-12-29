using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Utils.Common
{
    public class Encryption
    {
        public static byte[] USART_PASSWORD_TABLE =
        {
            0X1D,0X1E,0X2B,0X2C,0X63,0X65,0X62,0X26,0X60,0X61,0X71,0X72,0X7A,0X7B,0X69,0X6C,
            0X7D,0X6B,0X25,0X6A,0X70,0X66,0X73,0X74,0X75,0X78,0X79,0X67,0X11,0X76,0X68,0X6D,
            0X6E,0X6F,0X54,0X56,0X53,0X2A,0X51,0X98,0X99,0X9A,0XB9,0XA4,0XAD,0XA3,0X64,0X52,
            0X5A,0X5B,0X5C,0X5D,0XAE,0XAF,0XA0,0X57,0XA5,0XA6,0X7C,0XA1,0XA7,0X21,0X22,0X23,
            0XAA,0X58,0XA9,0X77,0XA8,0X59,0X5E,0X5F,0X50,0XB5,0XB7,0XB4,0X55,0XB2,0XB3,0X92,
            0X93,0X9B,0X9C,0XBB,0XBC,0X9D,0X9E,0X9F,0XAC,0XBD,0X90,0X91,0X17,0X18,0X10,0X95,
            0X97,0X94,0XA2,0XB8,0X7E,0X7F,0XBF,0XB0,0XB1,0XAB,0X1F,0XDD,0XDE,0XCA,0XCB,0XC0,
            0XC1,0XC2,0X16,0X19,0X15,0X27,0X14,0X1A,0X1B,0X1C,0XC6,0XC8,0XC5,0XB6,0XC3,0XC4,
            0XD6,0XD7,0XDF,0XD0,0XCC,0XCD,0XCE,0XCF,0XD1,0XD2,0XD3,0XD4,0XD5,0XC9,0XDA,0XDC,
            0XD9,0XBE,0XD8,0X2D,0X2E,0X2F,0X28,0X20,0X12,0X13,0X4B,0X83,0X32,0XE8,0XEA,0XE7,
            0X96,0XE5,0XE6,0XF8,0XF9,0XF1,0XF2,0XEE,0XEF,0XE0,0XE1,0XF3,0XF4,0XF5,0XF6,0XFA,
            0XFC,0XEB,0XBA,0XF7,0XFD,0XFE,0XFF,0XF0,0XEC,0XED,0XE2,0XE3,0XE4,0X4A,0X45,0X47,
            0X44,0XC7,0X42,0X43,0X08,0X24,0X02,0X4C,0X01,0XDB,0X09,0X4D,0X4E,0X03,0X04,0X05,
            0X06,0X07,0X48,0X0A,0X0B,0X0C,0X0D,0X0E,0X49,0X4F,0X00,0XE9,0X0F,0X40,0X41,0X29,
            0X82,0X84,0X81,0XFB,0X8F,0X80,0X35,0X36,0X3E,0X3F,0X88,0X89,0X8A,0X31,0X33,0X30,
            0X46,0X8B,0X34,0X85,0X37,0X38,0X39,0X3A,0X3B,0X3C,0X3D,0X86,0X87,0X8C,0X8D,0X8E,
        };
        /// <summary>
        /// 数据加密,返回所在所在的位置
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte Hex_PassWord(byte hex)//数据加密
        {
            byte i = 0;
            byte password = 0;

            for (i = 0; i < 0xFF; i++)
            {
                if (hex == USART_PASSWORD_TABLE[i])
                {
                    password = i;
                    break;
                }
            }
            return password;

        }
        /// <summary>
        /// 对数组加密
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static byte[] Encry(byte[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            int length = arr.Length;
            byte[] newByte = new byte[length];
            for (int j = 0; j < length; j++)
            {
                byte b1 = 0x00;
                for (byte i = 0x00; i <= 0xFF; i++)
                {
                    if (arr[j] == USART_PASSWORD_TABLE[(int)i])
                    {
                        b1 = i;
                        break;
                    }
                }
                newByte[j] = b1;
            }
            return newByte;
        }
        /// <summary>
        /// 数据解密
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static byte[] EncryConverse(byte[] arr)
        {
            if (arr == null) return null;
            int length = arr.Length;
            byte[] newByte = new byte[length];
            for (int i = 0; i < length; i++)
            {
                newByte[i] = USART_PASSWORD_TABLE[(int)arr[i]];
            }
            return newByte;
        }
        /// <summary>
        /// 数据解密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static byte PassWord_Hex(byte password)//数据解密 
        {
            byte hex = 0;

            hex = USART_PASSWORD_TABLE[password];
            return hex;
        }

        /// <summary>
        /// 对数据包加密
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static byte[] EncryPacket(byte[] arr)
        {
            Packets packet = new Packets();
            packet.Init(arr);
            byte[] newByte = null;
            byte[] retByte = null;
            retByte = ArrayHelper.AddBytes(retByte, packet.Header);
            retByte = ArrayHelper.AddBytes(retByte, packet.DataLength);

            newByte = ArrayHelper.AddBytes(newByte, packet.DeviceType);
            newByte = ArrayHelper.AddBytes(newByte, packet.MachineId);
            newByte = ArrayHelper.AddBytes(newByte, packet.MAC);
            newByte = ArrayHelper.AddBytes(newByte, packet.CommandWord);
            newByte = ArrayHelper.AddBytes(newByte, packet.Data);

            retByte = ArrayHelper.AddBytes(retByte, Encry(newByte));
            retByte = ArrayHelper.AddBytes(retByte, packet.CRC32);

            return retByte;
        }

        /// <summary>
        /// 解密成数据包
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static Packets EncryConversePacket(byte[] arr)
        {
            Packets packet = new Packets();
            packet.Init(arr);

            byte[] newByte = null;
            newByte = ArrayHelper.AddBytes(newByte, packet.DeviceType);
            newByte = ArrayHelper.AddBytes(newByte, packet.MachineId);
            newByte = ArrayHelper.AddBytes(newByte, packet.MAC);
            newByte = ArrayHelper.AddBytes(newByte, packet.CommandWord);
            newByte = ArrayHelper.AddBytes(newByte, packet.Data);

            byte[] b = EncryConverse(newByte);

            byte[] packetByte = null;
            packetByte = ArrayHelper.AddBytes(packetByte, packet.Header);
            packetByte = ArrayHelper.AddBytes(packetByte, packet.DataLength);
            packetByte = ArrayHelper.AddBytes(packetByte, b);
            packetByte = ArrayHelper.AddBytes(packetByte, packet.CRC32);

            packet.Init(packetByte);
            return packet;
        }

        #region 串口数据解密
        public static Packets EncryConverseSerialPacket(byte[] arr)
        {
            Packets packet = new Packets();
            packet.SerialInit(arr);

            byte[] newByte = null;
            newByte = ArrayHelper.AddBytes(newByte, packet.DeviceType);
            newByte = ArrayHelper.AddBytes(newByte, packet.MachineId);
            //newByte = ArrayHelper.AddBytes(newByte, packet.MAC);
            newByte = ArrayHelper.AddBytes(newByte, packet.CommandWord);
            newByte = ArrayHelper.AddBytes(newByte, packet.Data);
            newByte = ArrayHelper.AddBytes(newByte, packet.ComPass);
            byte[] b = EncryConverse(newByte);

            byte[] packetByte = null;
            packetByte = ArrayHelper.AddBytes(packetByte, packet.Header);
            packetByte = ArrayHelper.AddBytes(packetByte, packet.DataLength);
            packetByte = ArrayHelper.AddBytes(packetByte, b);
            packetByte = ArrayHelper.AddBytes(packetByte, packet.CRC32);

            packet.Init(packetByte);
            return packet;
        }
        #endregion

    }
}
