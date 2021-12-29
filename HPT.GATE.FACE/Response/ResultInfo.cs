using System;

namespace HPT.Face.Response
{
    public class ResultInfo<T>
    {
        public int Result { get; set; }//仅表示接口调用状态，1 成功，0 失败，通常只要服务器能响应，该值均为 1 
        public bool Success { get; set; }//操作状态，成功为 true，以该字段为准标识操作状态
        public T Data { get; set; }//接口返回数据封装类或集合
        public string Msg { get; set; }//异常信息提示
    }
}
