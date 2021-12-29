using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Device.Data
{
    public class DataServer
    {


        #region ctor

        public DataServer()
        {
            this.Server = new byte[4];
            this.Port = new byte[2];
            this.MachineId = new byte[2];
            this.CardReaderId = new byte[2];
        }

        #endregion

        #region Var

        /// <summary>
        /// 服务器地址
        /// </summary>
        private byte[] server;

        public byte[] Server
        {
            get
            {
                return server;
            }

            set
            {
                server = value;
            }
        }


        /// <summary>
        /// 端口号
        /// </summary>
        private byte[] port;

        public byte[] Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        /// <summary>
        /// 机器号
        /// </summary>
        private byte[] machineId;

        public byte[] MachineId
        {
            get
            {
                return machineId;
            }

            set
            {
                machineId = value;
            }
        }

        /// <summary>
        /// 读头号
        /// </summary>
        private byte[] cardReaderId;

        public byte[] CardReaderId
        {
            get
            {
                return cardReaderId;
            }

            set
            {
                cardReaderId = value;
            }
        }



        #region 序列化


        /// <summary>
        /// 转化为数据
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            List<byte> list = new List<byte>();
            list.AddRange(this.Server);
            list.AddRange(this.Port);
            list.AddRange(this.MachineId);
            list.AddRange(this.CardReaderId);
            return list.ToArray();
        }

        #endregion

        #region 反序列化

        public void Init(byte[] arr)
        {
            if (arr.Length != this.ToArray().Length)
            {
                return;
            }

            int index = 0;
            this.Server = ArrayHelper.SubByte(arr, index, server.Length);
            index += Server.Length;
            this.Port = ArrayHelper.SubByte(arr, index, Port.Length);
            index += Port.Length;
            this.MachineId = ArrayHelper.SubByte(arr, index, MachineId.Length);
            index += MachineId.Length;
            this.CardReaderId = ArrayHelper.SubByte(arr, index, CardReaderId.Length);
            index += CardReaderId.Length;

        }

        #endregion



        #endregion
    }
}
