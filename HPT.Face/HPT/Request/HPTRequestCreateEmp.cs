using HPT.Face.HPT.Model;
using Newtonsoft.Json;
using System.Text;

namespace HPT.Face.HPT.Request
{
    public class HPTRequestCreateEmp
    {
        public string pass { get; set; }
        public HPTPerson person { get; set; }
        public HPTPassTime passtime { get; set; }
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
