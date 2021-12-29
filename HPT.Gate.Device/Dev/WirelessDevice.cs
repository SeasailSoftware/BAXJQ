using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.Device.Data;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Device.Dev
{
    public class WirelessDevice
    {
        #region const
        private readonly byte[] DeviceType = new byte[] { 0x03 };
        #endregion


        #region Var

        public int CommNo { get; set; }

        public int BaudRate { get; set; }
        public int MachineId { get; set; }

        public string DeviceName { get; set; }

        public string Mac { get; set; }

        public int RowIndex { get; set; }

        #endregion

        #region public methods
        public bool StartUpdateMainBoard()
        {
            SerialPortHelper.Instance._CommNo = this.CommNo;
            SerialPortHelper.Instance._BaudRate = this.BaudRate;
            byte[] sendData = Organize(Command.StartUpdate);
            byte[] revData = SerialPortHelper.Instance.SendDataSynchronous(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.StartUpdate, packets[0]);
        }

        public bool SendUpdateMainBoardPacket(DataUpdate packet)
        {
            SerialPortHelper.Instance._CommNo = this.CommNo;
            SerialPortHelper.Instance._BaudRate = this.BaudRate;
            byte[] sendData = Organize(Command.Update, packet);
            byte[] revData = SerialPortHelper.Instance.SendDataSynchronous(sendData);
            if (revData == null) return false;
            List<Packets> packets = DataManager.ArrayToPacketList(revData);
            if (packets.Count == 0) return false;
            return AnalyzeSetResult(Command.Update, packets[0]);
        }
        #endregion

        #region 组织发送数据
        public byte[] Organize(Command comm, object obj = null)
        {
            Packets packet = new Packets();
            switch (comm)
            {
                ///启动更新程序
                case Command.StartUpdate:
                    packet.CommandWord = new byte[2] { 0x00, 0x71 };
                    packet.Data = new byte[] { 0x00, 0x00 };
                    break;
                case Command.Update:
                    packet.CommandWord = new byte[2] { 0x00, 0x72 };
                    packet.Data = ((DataUpdatePacket)obj).ToArray();
                    ///ShowMessage(Tb, "【" + ArrayHelper.ArrayToMAC(MAC) + "】" + System.DateTime.Now.ToString("T") + ":发送更新包【" + UpdateIndex.ToString() + "】", Color.Black);
                    break;
            }
            packet.Header = new byte[5] { 0x5A, 0xA5, 0x0F, 0x55, 0xAA };
            packet.DeviceType = DeviceType;
            packet.MachineId = ArrayHelper.IntToBytes(MachineId, 2);
            packet.MAC = ArrayHelper.MacToHexArray(Mac);
            byte[] sendData = packet.ToArray();
            byte[] retByte = Encryption.EncryPacket(sendData);
            return retByte;
        }
        #endregion

        #region 解析相应类信息
        private bool AnalyzeSetResult(Command command, Packets packet)
        {
            Command com = (Command)(packet.CommandWord[0] * 16 + packet.CommandWord[1]);
            if (com != command) return false;
            bool flag = false;
            byte[] data = packet.Data;
            switch (command)
            {
                case Command.StartUpdate:
                    if (data.Length == 1 && data[0] == 0x55)
                        flag = true;
                    break;
                case Command.Update:
                    if (data.Length == 1 && data[0] != 0xFF)
                        flag = true;
                    break;
            }
            return flag;
        }


        #endregion
    }
}
