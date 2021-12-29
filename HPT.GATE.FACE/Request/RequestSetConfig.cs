using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.Request
{
    public class RequestSetConfig
    {
        public string Pass { get; set; }

        public Config Data { get; set; }

        public string Serialize()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"pass={Pass}")
                .Append("&")
                //.Append($"config={System.Web.HttpUtility.UrlEncode(JsonConvert.SerializeObject(Data)).ToUpper()}");
                .Append($"config={JsonConvert.SerializeObject(Data)}");
            return buffer.ToString();
    
        }
    }
}
