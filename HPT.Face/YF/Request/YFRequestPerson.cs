using HPT.Face.YF.Model;
using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.YF.Request
{
    public class YFRequestPerson
    {
        public string Pass { get; set; }

        public YFPerson Person { get; set; }

        public string Serialize()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"pass={Pass}")
                .Append("&")
                .Append($"person={JsonConvert.SerializeObject(Person)}");
            return buffer.ToString();
        }
    }
}
