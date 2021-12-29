using HPT.Face.YF.Model;
using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.YF.Request
{
    public class YFRequestCreateEmp
    {
        public string pass { get; set; }
        public YFPerson person { get; set; }
        public YFPassTime passtime { get; set; }
        public string time { get; set; }

        public string imgBase64First { get; set; }

        public string imgBase64Second { get; set; }
        public string imgBase64Third { get; set; }

        public string Serialize()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"pass={pass}")
                .Append("&")
                .Append($"person={JsonConvert.SerializeObject(person)}")
                .Append("&")
                .Append($"passtime={JsonConvert.SerializeObject(passtime)}")
                .Append("&")
                .Append($"time={time}")
                .Append("&")
                .Append($"imgBase64First={imgBase64First}")
                .Append("&")
                .Append($"imgBase64Second={imgBase64Second}")
                .Append("&")
                .Append($"imgBase64Third={imgBase64Third}");
            return buffer.ToString();
        }
    }
}
