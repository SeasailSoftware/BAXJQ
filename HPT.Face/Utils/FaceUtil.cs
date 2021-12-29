using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace HPT.Face.Utils
{
    public class FaceUtil
    {
        public static string ImageToBase64String(Image image)
        {
            if (image == null) return "";
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

        public static byte[] ImageToBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(image))
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                }
                return ms.ToArray();
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

        public static Image Base64StringToImage(string image)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(image.Replace("%2B", "+"));
                MemoryStream ms = new MemoryStream(arr);
                Image bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string CaptureFromImage(string filePath, int x, int y, int width, int height)
        {
            try
            {
                using (Image img = Image.FromFile(filePath))
                {
                    if (img.Width <= 300 || img.Height <= 350) return ImageToBase64String(img);
                    int w = width / 4;
                    x = x - w;
                    width = width + 2 * w;
                    height = (int)((double)width / 300 * 350);
                    int h = (int)((double)w / 200 * 350);
                    y = y - h;

                    using (var bmp = new Bitmap(width, height))
                    {
                        var g = Graphics.FromImage(bmp);
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(x, y, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                        g.Dispose();
                        Image capture = KiResizeImage(bmp, 300, 350);
                        return ImageToBase64String(capture);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                var b = new Bitmap(newW, newH);
                var g = Graphics.FromImage(b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }

        public static Bitmap GetThumbnail(Bitmap b, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
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

