using HPT.Face.HPT.Model;
using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.HPT.Request
{
    public class HPTRequestPasstime
    {
        public string Pass { get; set; }

        public HPTPassTime Passtime { get; set; }

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
