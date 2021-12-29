using HPT.Face.YF.Model;
using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.YF.Request
{
    public class YFRequestPasstime
    {
        public string Pass { get; set; }

        public YFPassTime Passtime { get; set; }

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
