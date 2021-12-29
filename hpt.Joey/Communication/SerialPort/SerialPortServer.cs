using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPT.Joey.Lib.Communication.SerialPort
{
    public class SerialPortServer
    {
        #region Private
        private bool IsRunning = false;

        #endregion

        #region Event

        public event Action<string> Message;
        protected void OnMessage(string msg)
        {
            if (msg.Equals(string.Empty)) return;
            try
            {
                Task.Factory.StartNew(() =>
                {
                    if (Message == null) return;
                    Message(msg);
                });
            }
            catch
            {
            }
        }

        #endregion



        #region Public 

        #region 启动服务
        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            OnMessage("服务已经启动...");
        }
        #endregion

        #region 关闭服务
        public void Stop()
        {
            IsRunning = false;
            OnMessage("服务已经停止...");
        }
        #endregion


        #endregion

    }
}
