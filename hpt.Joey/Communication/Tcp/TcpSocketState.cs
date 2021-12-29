using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace HPT.Joey.Lib.Communication.Tcp
{
    public class TcpSocketState
    {


        #region private


        /// <summary>
        /// 接收缓冲区
        /// </summary>
        public byte[] RecvDataBuffer = new byte[2048];

        public Socket _TcpSocket { get; set; }

        #endregion



        #region Ctor


        public TcpSocketState()
        {

        }
        #endregion




        #region 释放资源

        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (_TcpSocket != null)
                {
                    if (_TcpSocket.Connected)
                    {
                        _TcpSocket.Close();
                    }
                    _TcpSocket = null;
                    disposed = true;
                }
            }
        }

        /// <summary>
        /// Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~TcpSocketState()
        {
            Dispose(false);
        }

        #endregion


    }
}
