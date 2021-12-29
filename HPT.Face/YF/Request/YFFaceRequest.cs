using System.Text;

namespace HPT.Face.YF.Request
{
    public class FaceRequest<T>
    {
        public string Password { get; set; }

        public T Data { get; set; }

        public string Serialization()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"password");
            return buffer.ToString();
        }
    }
}
