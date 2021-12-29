using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Utils.Common
{

    public class Packets
    {
        /// <summary>
        /// 分配内存空间
        /// </summary>
        public Packets()
        {
            this.Header = new byte[5];
            this.DeviceType = new byte[1];
            this.DataLength = new byte[2];
            this.MachineId = new byte[2];
            this.MAC = new byte[6];
            this.CommandWord = new byte[2];
            this.ComPass = new byte[3];
            this.CRC32 = new byte[4];
        }
        /// <summary>
        /// 元素初始化
        /// </summary>
        /// <param name="data"></param>
        public void Init(byte[] data)
        {
            if (data == null) return;
            int dataLength = data.Length;
            int currentLen = 0;
            //包头
            if (dataLength < currentLen + Header.Length) return;
            Header = ArrayHelper.SubByte(data, currentLen, Header.Length);
            currentLen += Header.Length;
            //数据长度
            if (dataLength < currentLen + DataLength.Length) return;
            DataLength = ArrayHelper.SubByte(data, currentLen, DataLength.Length);
            currentLen += DataLength.Length;
            //设备类型
            if (dataLength < currentLen + DeviceType.Length) return;
            DeviceType = ArrayHelper.SubByte(data, currentLen, DeviceType.Length);
            currentLen += DeviceType.Length;
            //机器号
            if (dataLength < currentLen + MachineId.Length) return;
            MachineId = ArrayHelper.SubByte(data, currentLen, MachineId.Length);
            currentLen += MachineId.Length;
            //Mac
            if (dataLength < currentLen + MAC.Length) return;
            MAC = ArrayHelper.SubByte(data, currentLen, MAC.Length);
            currentLen += MAC.Length;
            //命令字
            if (dataLength < currentLen + CommandWord.Length) return;
            CommandWord = ArrayHelper.SubByte(data, currentLen, CommandWord.Length);
            currentLen += CommandWord.Length;
            //Data
            if (dataLength < currentLen + CRC32.Length) return;
            if (dataLength == currentLen + CommandWord.Length)
            {
                CRC32 = ArrayHelper.SubByte(data, currentLen, CRC32.Length);
                currentLen += CRC32.Length;
            }
            else
            {
                int len = dataLength - currentLen - CRC32.Length;
                Data = ArrayHelper.SubByte(data, currentLen, len);
                currentLen += len;
                CRC32 = ArrayHelper.SubByte(data, currentLen, CRC32.Length);
                currentLen += CRC32.Length;
            }
        }

        #region SerialInit
        public void SerialInit(byte[] data)
        {
            int length = 0;
            if (data.Length >= 21)
            {
                Header = ArrayHelper.SubByte(data, length, Header.Length);
                length += Header.Length;
                DataLength = ArrayHelper.SubByte(data, length, DataLength.Length);
                length += DataLength.Length;
                DeviceType = ArrayHelper.SubByte(data, length, DeviceType.Length);
                length += DeviceType.Length;
                MachineId = ArrayHelper.SubByte(data, length, MachineId.Length);
                length += MachineId.Length;
                CommandWord = ArrayHelper.SubByte(data, length, CommandWord.Length);
                length += CommandWord.Length;
                ComPass = ArrayHelper.SubByte(data, length, ComPass.Length);
                length += ComPass.Length;
                int len = data.Length - 4 - length;
                if (len > 0)
                {
                    Data = new byte[len];
                    Data = ArrayHelper.SubByte(data, length, len);
                    length += Data.Length;
                }
                CRC32 = ArrayHelper.SubByte(data, length, CRC32.Length);
                length += CRC32.Length;
            }
        }
        #endregion

        /// <summary>
        /// 将数据包转化为数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            List<byte> array = new List<byte>();
            array.AddRange(Header);
            DataLength = ArrayHelper.DecimalToHexArray((DeviceType.Length + MachineId.Length + MAC.Length + CommandWord.Length + (Data == null ? 0 : Data.Length) + CRC32.Length), 2);
            array.AddRange(DataLength);
            array.AddRange(DeviceType);
            array.AddRange(MachineId);
            array.AddRange(MAC);
            array.AddRange(CommandWord);
            if (Data != null)
                array.AddRange(Data);
            CRC32 = GetCRC32();
            array.AddRange(CRC32);
            return array.ToArray();
            /*
            if (Data != null)
            {
                DataLength = ArrayHelper.DecimalToHexArray((DeviceType.Length + MachineId.Length + MAC.Length + CommandWord.Length + Data.Length + CRC32.Length), 2);
                int length = Header.Length + DataLength.Length + DeviceType.Length + MachineId.Length + MAC.Length + CommandWord.Length + Data.Length + CRC32.Length;
                byte[] array = new byte[length];

                int CurLen = 0;
                Array.Copy(Header, 0, array, CurLen, Header.Length);
                CurLen += Header.Length;
                Array.Copy(DataLength, 0, array, CurLen, DataLength.Length);
                CurLen += DataLength.Length;

                Array.Copy(DeviceType, 0, array, CurLen, DeviceType.Length);
                CurLen += DeviceType.Length;
                Array.Copy(MachineId, 0, array, CurLen, MachineId.Length);
                CurLen += MachineId.Length;
                Array.Copy(MAC, 0, array, CurLen, MAC.Length);
                CurLen += MAC.Length;
                Array.Copy(CommandWord, 0, array, CurLen, CommandWord.Length);
                CurLen += CommandWord.Length;
                Array.Copy(Data, 0, array, CurLen, Data.Length);
                CurLen += Data.Length;
                CRC32 = GetCRC32();
                Array.Copy(CRC32, 0, array, CurLen, CRC32.Length);
                CurLen += CRC32.Length;

                return array;
            }
            else
            {
                DataLength = ArrayHelper.DecimalToHexArray((DeviceType.Length + MachineId.Length + MAC.Length + CommandWord.Length + CRC32.Length), 2);
                int length = Header.Length + DataLength.Length + DeviceType.Length + MachineId.Length + MAC.Length + CommandWord.Length + CRC32.Length;
                byte[] array = new byte[length];
                int CurLen = 0;

                Array.Copy(Header, 0, array, CurLen, Header.Length);
                CurLen += Header.Length;
                Array.Copy(DataLength, 0, array, CurLen, DataLength.Length);
                CurLen += DataLength.Length;

                Array.Copy(DeviceType, 0, array, CurLen, DeviceType.Length);
                CurLen += DeviceType.Length;
                Array.Copy(MachineId, 0, array, CurLen, MachineId.Length);
                CurLen += MachineId.Length;
                Array.Copy(MAC, 0, array, CurLen, MAC.Length);
                CurLen += MAC.Length;
                Array.Copy(CommandWord, 0, array, CurLen, CommandWord.Length);
                CurLen += CommandWord.Length;

                CRC32 = GetCRC32();
                Array.Copy(CRC32, 0, array, CurLen, CRC32.Length);
                CurLen += CRC32.Length;

                return array;
            }
            */
        }

        #region 转化为数组
        public byte[] ToArray1()
        {
            if (Data != null)
            {
                DataLength = ArrayHelper.DecimalToHexArray((DeviceType.Length + MachineId.Length /*+ MAC.Length */+ CommandWord.Length + Data.Length + ComPass.Length + CRC32.Length), 2);
                int length = Header.Length + DataLength.Length + DeviceType.Length + MachineId.Length + /*MAC.Length + */CommandWord.Length + Data.Length + ComPass.Length + CRC32.Length;
                byte[] array = new byte[length];
                int CurLen = 0;
                ///数据包头
                Array.Copy(Header, 0, array, CurLen, Header.Length);
                CurLen += Header.Length;

                ///数据长度
                Array.Copy(DataLength, 0, array, CurLen, DataLength.Length);
                CurLen += DataLength.Length;

                ///设备类型
                this.DeviceType = Encryption.Encry(DeviceType);
                Array.Copy(DeviceType, 0, array, CurLen, DeviceType.Length);
                CurLen += DeviceType.Length;

                ///机器号
                this.MachineId = Encryption.Encry(MachineId);
                Array.Copy(MachineId, 0, array, CurLen, MachineId.Length);
                CurLen += MachineId.Length;

                ///MAC
                /*
                this.MAC = Encryption.Encry(MAC);
                Array.Copy(MAC, 0, array, CurLen, MAC.Length);
                CurLen += MAC.Length;
                */
                ///命令字
                this.CommandWord = Encryption.Encry(CommandWord);
                Array.Copy(CommandWord, 0, array, CurLen, CommandWord.Length);
                CurLen += CommandWord.Length;

                ///数据内容
                this.Data = Encryption.Encry(Data);
                Array.Copy(Data, 0, array, CurLen, Data.Length);
                CurLen += Data.Length;

                ///通讯密码
                this.ComPass = Encryption.Encry(ComPass);
                Array.Copy(ComPass, 0, array, CurLen, ComPass.Length);
                CurLen += ComPass.Length;
                ///CRC32
                CRC32 = GetCRC321();
                Array.Copy(CRC32, 0, array, CurLen, CRC32.Length);
                CurLen += CRC32.Length;

                return array;
            }
            else
            {
                DataLength = ArrayHelper.DecimalToHexArray((DeviceType.Length + MachineId.Length +/* MAC.Length + */CommandWord.Length + ComPass.Length + CRC32.Length), 2);
                int length = Header.Length + DataLength.Length + DeviceType.Length + MachineId.Length /*+ MAC.Length */+ CommandWord.Length + ComPass.Length + CRC32.Length;
                byte[] array = new byte[length];
                int CurLen = 0;

                ///数据包头
                Array.Copy(Header, 0, array, CurLen, Header.Length);
                CurLen += Header.Length;

                ///数据长度
                Array.Copy(DataLength, 0, array, CurLen, DataLength.Length);
                CurLen += DataLength.Length;

                ///设备类型
                this.DeviceType = Encryption.Encry(DeviceType);
                Array.Copy(DeviceType, 0, array, CurLen, DeviceType.Length);
                CurLen += DeviceType.Length;

                ///机器号
                this.MachineId = Encryption.Encry(MachineId);
                Array.Copy(MachineId, 0, array, CurLen, MachineId.Length);
                CurLen += MachineId.Length;

                ///MAC
                /*
                this.MAC = Encryption.Encry(MAC);
                Array.Copy(MAC, 0, array, CurLen, MAC.Length);
                CurLen += MAC.Length;
                */
                ///命令字
                this.CommandWord = Encryption.Encry(CommandWord);
                Array.Copy(CommandWord, 0, array, CurLen, CommandWord.Length);
                CurLen += CommandWord.Length;

                ///通讯密码
                this.ComPass = Encryption.Encry(ComPass);
                Array.Copy(ComPass, 0, array, CurLen, ComPass.Length);
                CurLen += ComPass.Length;
                ///CRC32
                CRC32 = GetCRC321();
                Array.Copy(CRC32, 0, array, CurLen, CRC32.Length);
                CurLen += CRC32.Length;

                return array;
            }
        }
        #endregion

        /// <summary>
        /// 5个字节的数据包头
        /// </summary>
        private static byte[] _Header;

        public byte[] Header
        {
            get { return _Header; }
            set { _Header = value; }
        }

        /// <summary>
        /// 2个字节的数据长度
        /// </summary>
        private byte[] _DataLength;

        public byte[] DataLength
        {
            get { return _DataLength; }
            set { _DataLength = value; }
        }


        /// <summary>
        /// 2个字节的设备类型
        /// </summary>
        private byte[] _DeviceType;

        public byte[] DeviceType
        {
            get { return _DeviceType; }
            set { _DeviceType = value; }
        }
        /// <summary>
        /// 2个字节的机器号
        /// </summary>
        private byte[] _MachineId;

        public byte[] MachineId
        {
            get { return _MachineId; }
            set { _MachineId = value; }
        }

        /// <summary>
        /// 6个字节的物理地址
        /// </summary>
        private byte[] _MAC;

        public byte[] MAC
        {
            get { return _MAC; }
            set { _MAC = value; }
        }

        /// <summary>
        /// 2个字节的命令字
        /// </summary>
        private byte[] _CommandWord;

        public byte[] CommandWord
        {
            get { return _CommandWord; }
            set { _CommandWord = value; }
        }

        /// <summary>
        /// 200个字节的闸机数据
        /// </summary>
        private byte[] _Data;

        public byte[] Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        public byte[] ComPass { get; set; }
        /// <summary>
        /// CRC32 校验码
        /// </summary>
        private byte[] _CRC32;

        public byte[] CRC32
        {
            get { return _CRC32; }
            set { _CRC32 = value; }
        }
        /// <summary>
        /// 判断是否以包头开始
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public bool LocateWithHeader(byte[] header)
        {
            if (this.Header == null)
            {
                return false;
            }
            if (this.Header.Length != header.Length)
            {
                return false;
            }
            for (int i = 0; i < this.Header.Length; i++)
            {
                if (this.Header[i] != header[i])
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 校验数据是否有效
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public bool CheckValid(byte[] arr)
        {
            if (arr.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                if (CRC32[i] != arr[i])
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// 生成CRC32校验码
        /// </summary>
        /// <returns></returns>
        public byte[] GetCRC32()
        {

            byte[] CRC_32 = new byte[4];
            if (DataLength == null || DeviceType == null || MachineId == null || MAC == null || CommandWord == null)
            {
                CRC_32 = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            }
            else
            {

                ///计算校验数据长度
                int length = DataLength.Length + DeviceType.Length + MachineId.Length + MAC.Length + CommandWord.Length + (Data == null ? 0 : Data.Length);
                ///赋值
                ///
                byte[] by = new byte[length];
                int index = 0;
                //DataLength = ArrayHelper.DecimalToHexArray((DeviceType.Length + MachineId.Length + MAC.Length + CommandWord.Length + (Data == null ? 0 : Data.Length) + CRC32.Length), 2);
                Array.Copy(DataLength, 0, by, index, DataLength.Length);
                index += DataLength.Length;
                Array.Copy(DeviceType, 0, by, index, DeviceType.Length);
                index += DeviceType.Length;
                Array.Copy(MachineId, 0, by, index, MachineId.Length);
                index += MachineId.Length;
                Array.Copy(MAC, 0, by, index, MAC.Length);
                index += MAC.Length;
                Array.Copy(CommandWord, 0, by, index, CommandWord.Length);
                index += CommandWord.Length;
                if (Data != null)
                {
                    Array.Copy(Data, 0, by, index, Data.Length);
                    index += Data.Length;
                }

                ///转换成UINT32
                UInt32[] uint32 = CRC32Helper.ByteToUnit32(by);
                UInt32 crc32 = CRC32Helper.Cal_CRC(uint32);
                CRC_32 = BitConverter.GetBytes(crc32);
            }
            return CRC_32;
        }

        #region CRC校验
        public byte[] GetCRC321()
        {
            byte[] CRC_32 = new byte[4];
            if (DataLength == null || DeviceType == null || MachineId == null || CommandWord == null)
            ///if (DataLength == null || DeviceType == null || MachineId == null || MAC == null || CommandWord == null)
            {
                CRC_32 = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            }
            else
            {

                ///计算校验数据长度
                int length = DataLength.Length + DeviceType.Length + MachineId.Length /*+ MAC.Length*/ + CommandWord.Length + (Data == null ? 0 : Data.Length) + ComPass.Length;
                ///赋值
                ///
                byte[] by = new byte[length];
                int index = 0;
                //DataLength = ArrayHelper.DecimalToHexArray((DeviceType.Length + MachineId.Length + MAC.Length + CommandWord.Length + (Data == null ? 0 : Data.Length) + CRC32.Length), 2);
                Array.Copy(DataLength, 0, by, index, DataLength.Length);
                index += DataLength.Length;
                Array.Copy(DeviceType, 0, by, index, DeviceType.Length);
                index += DeviceType.Length;
                Array.Copy(MachineId, 0, by, index, MachineId.Length);
                index += MachineId.Length;
                /*
                Array.Copy(MAC, 0, by, index, MAC.Length);
                index += MAC.Length;
                */
                Array.Copy(CommandWord, 0, by, index, CommandWord.Length);
                index += CommandWord.Length;
                if (Data != null)
                {
                    Array.Copy(Data, 0, by, index, Data.Length);
                    index += Data.Length;
                }
                Array.Copy(ComPass, 0, by, index, ComPass.Length);
                index += ComPass.Length;
                ///转换成UINT32
                UInt32[] uint32 = CRC32Helper.ByteToUnit32(by);
                UInt32 crc32 = CRC32Helper.Cal_CRC(uint32);
                byte[] arr = BitConverter.GetBytes(crc32);
                for (int i = 0; i < 4; i++)
                {
                    CRC_32[i] = arr[3 - i];
                }
            }
            return CRC_32;
        }
        #endregion


        /// <summary>
        /// 检查数据CRC32包校验是否成功
        /// </summary>
        /// <returns></returns>
        public bool CheckCRC32()
        {
            byte[] crc32 = GetCRC32();
            for (int j = 0; j < 4; j++)
            {
                if (crc32[j] != CRC32[j])
                {
                    return false;
                }
            }
            return true;
        }

        public bool SerialCheckCRC32()
        {
            byte[] crc32 = GetCRC321();
            for (int j = 0; j < 4; j++)
            {
                if (crc32[j] != CRC32[j])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
