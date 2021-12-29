using System;
using System.Drawing;
using System.IO;

namespace HPT.Gate.DataAccess.Entity
{
    public class DisplayRecord
    {
        public UInt64 RecId { get; set; }
        public int CurrentIndex { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }

        public int DeptId { get; set; }
        public string DeptName { get; set; }

        public int EmpId { get; set; }
        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public int CardId { get; set; }
        public string CardNo { get; set; }

        public string RecDatetime { get; set; }

        public string IOFlag { get; set; }

        public string RecordType { get; set; }

        public Bitmap Photo
        {
            get
            {
                if (PhotoArray == null)
                {
                    return new Bitmap(Environment.CurrentDirectory + @"/Image/DefaultPhoto.png");
                }
                MemoryStream ms = new MemoryStream(PhotoArray);
                //Image img = Image.FromStream(ms, true);//在这里出错  
                Image img = Image.FromStream(ms);//在这里出错  
                //流用完要及时关闭  
                ms.Close();
                Bitmap bmp = new Bitmap(img);
                return bmp;
            }
        }

        public byte[] PhotoArray { get; set; }

        public UInt64 CompareTo(DisplayRecord other)
        {
            return this.RecId > other.RecId ? RecId : other.RecId;
        }

        public Bitmap _Image { get; set; }
    }

}

