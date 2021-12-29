using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class DataManager
    {

        #region 将数组转化为数据包
        /// <summary>
        /// 将字节数组分割成数据包添加到列表
        /// </summary>
        /// <param name="packetList"></param>
        /// <param name="data"></param>
        public static List<Packets> ArrayToPacketList(byte[] data)
        {
            List<Packets> packetList = new List<Packets>();
            while (data.Length > 0)
            {
                int index = 0;
                int firstIndex = ArrayHelper.FirstHeaderOf(data);
                if (firstIndex == -1) break;
                int length = data.Length;
                byte[] arr = ArrayHelper.SubByte(data, firstIndex + 5, length - firstIndex - 5);
                int dataLength = ArrayHelper.bytesToInt(new byte[] { arr[0], arr[1] });
                if (dataLength + 7 > length - firstIndex) break;
                index = firstIndex + dataLength + 7;
                byte[] pArr = ArrayHelper.SubByte(data, firstIndex, dataLength + 7);
                Packets packet = new Packets();
                packet = Encryption.EncryConversePacket(pArr);
                if (packet.CheckCRC32()) packetList.Add(packet);
                data = ArrayHelper.SubByte(data, index, length - index);
            }
            return packetList;
        }
        #endregion

        #region 将串口数组转化为数据包
        /// <summary>
        /// 将字节数组分割成数据包添加到列表
        /// </summary>
        /// <param name="packetList"></param>
        /// <param name="data"></param>
        public static List<Packets> SerialArrayToPacketList(byte[] data)
        {
            List<Packets> packetList = new List<Packets>();
            while (data.Length > 0)
            {
                int index = 0;
                int firstIndex = ArrayHelper.FirstHeaderOf(data);
                if (firstIndex == -1) break;
                int length = data.Length;
                byte[] arr = ArrayHelper.SubByte(data, firstIndex + 5, length - firstIndex - 5);
                int dataLength = ArrayHelper.bytesToInt(new byte[] { arr[0], arr[1] });
                if (dataLength + 7 > length - firstIndex) break;
                index = firstIndex + dataLength + 7;
                byte[] pArr = ArrayHelper.SubByte(data, firstIndex, dataLength + 7);
                Packets packet = new Packets();
                packet = Encryption.EncryConverseSerialPacket(pArr);
                ///CRC32校验
                if (packet.SerialCheckCRC32())
                {
                    packetList.Add(packet);
                }
                data = ArrayHelper.SubByte(data, index, length - index);
            }
            return packetList;
        }
        #endregion

    }
}
