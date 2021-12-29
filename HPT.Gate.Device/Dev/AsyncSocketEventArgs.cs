namespace HPT.Gate.Device.Dev
{
    public class AsyncSocketEventArgs : System.EventArgs
    {
        /// <summary>  
        /// 提示信息  
        /// </summary>  
        public string _msg;

        /// <summary>  
        /// 客户端状态封装类  
        /// </summary>  
        public AsyncSocketState _state;

        /// <summary>  
        /// 是否已经处理过了  
        /// </summary>  
        public bool IsHandled { get; set; }

        public AsyncSocketEventArgs(string msg)
        {
            this._msg = msg;
            IsHandled = false;
        }
        public AsyncSocketEventArgs(AsyncSocketState state)
        {
            this._state = state;
            IsHandled = false;
        }
        public AsyncSocketEventArgs(string msg, AsyncSocketState state)
        {
            this._msg = msg;
            this._state = state;
            IsHandled = false;
        }
    }
}