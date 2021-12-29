using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.Request
{
    public class RequestPerson
    {
        public string Pass { get; set; }

        public Person Person { get; set; }

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
