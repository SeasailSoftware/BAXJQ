using System;
using System.Drawing;
using System.IO;

namespace HPT.Gate.DataAccess.Entity
{
    public class Photo
    {
        public UInt32 EmpId { get; set; }

        public byte[] ImageByte { get; set; }

        public Bitmap Image
        {
            get
            {
                return ArrayToImage(ImageByte);
            }
        }

        public Bitmap ArrayToImage(byte[] arr)
        {
            if (arr == null || arr.Length < 100)
            {
                return null;
            }
            MemoryStream ms = new MemoryStream(arr);
            //Image img = Image.FromStream(ms, true);//在这里出错  
            Image img = System.Drawing.Image.FromStream(ms);//在这里出错  
            //流用完要及时关闭  
            ms.Close();
            Bitmap bmp = new Bitmap(img);
            return bmp;
        }
    }
}
