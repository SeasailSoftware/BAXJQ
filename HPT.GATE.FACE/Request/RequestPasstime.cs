using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.Request
{
    public class RequestPasstime
    {
        public string Pass { get; set; }

        public PassTime Passtime { get; set; }

        public string Serialize()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"pass={Pass}")
                .Append("&")
                .Append($"passtime={JsonConvert.SerializeObject(Passtime)}");
            return buffer.ToString();
        }
    }
}
