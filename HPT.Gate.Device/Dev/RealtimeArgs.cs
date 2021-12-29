using System.Drawing;

namespace HPT.Gate.Device.Dev
{
    public class RealtimeArgs : System.EventArgs
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public int EmpId { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public string Duty { get; set; }
        public int CardType { get; set; }

        public string CardNo { get; set; }


        public int DeviceId { get; set; }

        public string DevName { get; set; }

        public string DevIPAddress { get; set; }

        public string DevMac { get; set; }


        public int CamId { get; set; }

        public string CamIPAddress { get; set; }

        public int IOFlag { get; set; }

        public string RecDatetime { get; set; }

        public Image Photo { get; set; }

        public string RecordType { get; set; }

        public Image Capture { get; set; }

    }
}
