using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace HPT.Gate.DataAccess.Entity
{
    public class EmpInfo
    {

        public int EmpId { get; set; }

        public string EmpCode { get; set; }
        public string EmpName { get; set; }

        public int IOFlag { get; set; }
        public string EnglishName { get; set; }

        public string Sex { get; set; }

        public string IdentityCard { get; set; }

        public int DeptId { get; set; }
        public string DeptName { get; set; }




        public string Telephone { get; set; }
        public string BirthDay { get; set; }
        public string Nation { get; set; }

        public string BornEarth { get; set; }

        public string Marrige { get; set; }

        public string JoinDate { get; set; }

        public Bitmap Photo { get; set; }

        public byte[] PhotoStream
        {
            get
            {
                if (Photo == null) return new byte[] { 0x00 };
                using (MemoryStream ms = new MemoryStream())
                {
                    using (Bitmap bmp = new Bitmap(Photo))
                    {
                        bmp.Save(ms, ImageFormat.Jpeg);
                    }
                    return ms.ToArray();
                }
            }
            set {; }
        }

        public int TicketType { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { get; set; }
        public string ICCardNo { get; set; }

        public string IDSerial { get; set; }

        public string IDCardNo { get; set; }

        public string Duty { get; set; }

        public int Status { get; set; }

        public string LeaveDate { get; set; }

        public byte[] FingerData1 { get; set; }

        public byte[] FingerData2 { get; set; }



        public int Rehire { get; set; }

        public int HireTimes { get; set; }

        public int[] FaceData { get; set; }

        public string CreateTime { get; set; }

        public string FaceStatus { get; set; }
    }
}
