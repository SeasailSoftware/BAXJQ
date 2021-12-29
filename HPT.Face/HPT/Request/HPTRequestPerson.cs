using HPT.Face.HPT.Model;
using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.HPT.Request
{
    public class HPTRequestPerson
    {
        public string Pass { get; set; }

        public HPTPerson Person { get; set; }

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
