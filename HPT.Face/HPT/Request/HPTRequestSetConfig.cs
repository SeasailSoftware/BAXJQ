using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.HPT.Request
{
    public class HPTRequestSetConfig
    {
        public string Pass { get; set; }

        public HPTConfig Data { get; set; }

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
