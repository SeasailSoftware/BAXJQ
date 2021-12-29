using System.Collections.Generic;

namespace HPT.Led.YBKJ
{
    public class Controller
    {
        public int Lid { get; set; }


        public int ControlType { get; set; }

        public int Protocol { get; set; }

        public int Width { get; set; }

        public int Heigth { get; set; }

        public string IPAddress { get; set; }

        public int Port { get; set; }

        public List<AreaInfo> DynAreas { get; set; }
    }
}
