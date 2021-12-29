using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace HPT.Face.Utils
{
    public class ImageHelper
    {
        public static string ImageToBase64String(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(image))
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                }
                byte[] array = ms.ToArray();
                return Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks).Replace("+", "%2B");//;
            }
        }

        public static byte[] ImageToBytes(Image img)
        {
            try
            {
                if (img == null) return null;
                using (Bitmap bmp = new Bitmap(img))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] arr = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(arr, 0, (int)ms.Length);
                        return arr;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static Image BytesToImage(byte[] array)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(array))
                {
                    Bitmap bmp = new Bitmap(ms);
                    return bmp;
                }
            }
            catch
            {
                return null;
            }
        }

        public static Image GetImageFromFTP(string path)
        {

            FtpWebResponse response = null;
            try
            {
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
                reqFtp.UseBinary = true;
                reqFtp.Credentials = new NetworkCredential();
                response = (FtpWebResponse)reqFtp.GetResponse();
                using (Stream ftpStream = response.GetResponseStream())
                {
                    Image image = Image.FromStream(ftpStream);
                    return image;
                }
            }
            catch (Exception ee)
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return null;
        }

    }
    public class RectFace
    {
        public int x { set; get; }
        public int y { set; get; }
        public int w { set; get; }
        public int h { set; get; }
    }
}

