using System.Drawing;
using System.IO;

namespace HPT.Gate.DataAccess.Entity
{
    public class LcdRecord
    {
        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public int EmpId { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public Bitmap Photo { get; set; }


        public byte[] PhotoStream { get; set; }

        public string CardNo { get; set; }

        public string IOFlag { get; set; }

        public string RecordType { get; set; }

        public string RecTime { get; set; }

        public override bool Equals(object obj)
        {
            LcdRecord record = (LcdRecord)obj;
            if (DeptId != record.DeptId) return false;
            if (!DeptName.Equals(record.DeptName)) return false;
            if (EmpId != record.EmpId) return false;
            if (!EmpCode.Equals(record.EmpCode)) return false;
            if (!EmpName.Equals(record.EmpName)) return false;
            if (!CardNo.Equals(record.CardNo)) return false;
            if (!IOFlag.Equals(record.IOFlag)) return false;
            if (!RecordType.Equals(record.RecordType)) return false;
            if (!RecTime.Equals(record.RecTime)) return false;
            return true;
        }

    }
}
