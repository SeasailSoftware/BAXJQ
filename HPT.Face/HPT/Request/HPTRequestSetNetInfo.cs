using System.Text;

namespace HPT.Face.HPT.Request
{
    public class  HPTRequestSetNetInfo
    {
        public string Pass { get; set; }
        public int IsDHCPMod { get; set; }

        public string IP { get; set; }
        public string GateWay { get; set; }

        public string SubnetMask { get; set; }

        public string DNS { get; set; }

        public string Serialize()
        {
            StringBuilder buffer = new StringBuilder();
            if (IsDHCPMod == 1)
                buffer.Append($"pass={Pass}")
                .Append("&")
                .Append($"isDHCPMod={IsDHCPMod}");
            else
                buffer.Append($"pass={Pass}")
                    .Append("&")
                    .Append($"isDHCPMod={IsDHCPMod}")
                    .Append("&")
                    .Append($"ip={IP}")
                    .Append("&")
                    .Append($"subnetMask={SubnetMask}")
                    .Append("&")
                    .Append($"gateway={GateWay}")
                    .Append("&")
                    .Append($"DNS={DNS}");
            return buffer.ToString();

        }
    }
}
