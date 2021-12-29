using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Face.SYD
{
    public class TResult<T>
    {
        public int Status { get; set; }//仅表示接口调用状态，1 成功，0 失败，通常只要服务器能响应，该值均为 1 
        public T Data { get; set; }//接口返回数据封装类或集合
        public string Msg { get; set; }//异常信息提示
    }
}
