using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace HPT.Joey.Lib.Utils
{
    public static class ImageHelper
    {

        /// <summary>
        /// 将原始图像转换成格式为Bgr565的16位图像
        /// </summary>
        /// <param name="bmp">用于转换的原始图像</param>
        /// <returns>转换后格式为Bgr565的16位图像</returns>
        public static Bitmap ToBgr565(this Bitmap bmp)
        {
            var PixelHeight = bmp.Height;
            var PixelWidth = bmp.Width;
            var Stride = ((PixelWidth * 3 + 3) >> 2) << 2;
            var Pixels = new Byte[PixelHeight * Stride];


            var bmpData = bmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(bmpData.Scan0, Pixels, 0, Pixels.Length);
            bmp.UnlockBits(bmpData);


            var TargetStride = ((PixelWidth + 1) >> 1) << 2;
            var TargetPixels = new Byte[PixelHeight * TargetStride];
            for (var i = 0; i < PixelHeight; i++)
            {
                var Index = i * Stride;
                var Loc = i * TargetStride;
                for (var j = 0; j < PixelWidth; j++)
                {
                    var B = Pixels[Index++];
                    var G = Pixels[Index++];
                    var R = Pixels[Index++];

                    TargetPixels[Loc++] = (Byte)(((G << 3) & 0xe0) | ((B >> 3) & 0x1f));
                    TargetPixels[Loc++] = (Byte)((R & 0xf8) | ((G >> 5) & 7));
                }
            }


            var TargetBmp = new Bitmap(PixelWidth, PixelHeight, PixelFormat.Format16bppRgb565);


            var TargetBmpData = TargetBmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.WriteOnly, PixelFormat.Format16bppRgb565);
            Marshal.Copy(TargetPixels, 0, TargetBmpData.Scan0, TargetPixels.Length);
            TargetBmp.UnlockBits(TargetBmpData);

            return TargetBmp;
        }

        /// <summary>
        /// 将原始图像转换成格式为Bgr555的16位图像
        /// </summary>
        /// <param name="bmp">用于转换的原始图像</param>
        /// <returns>转换后格式为Bgr555的16位图像</returns>
        public static Bitmap ToBgr555(this Bitmap bmp)
        {
            var PixelHeight = bmp.Height;
            var PixelWidth = bmp.Width;
            var Stride = ((PixelWidth * 3 + 3) >> 2) << 2;
            var Pixels = new Byte[PixelHeight * Stride];


            var bmpData = bmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(bmpData.Scan0, Pixels, 0, Pixels.Length);
            bmp.UnlockBits(bmpData);


            var TargetStride = ((PixelWidth + 1) >> 1) << 2;
            var TargetPixels = new Byte[PixelHeight * TargetStride];
            for (var i = 0; i < PixelHeight; i++)
            {
                var Index = i * Stride;
                var Loc = i * TargetStride;
                for (var j = 0; j < PixelWidth; j++)
                {
                    var B = Pixels[Index++];
                    var G = Pixels[Index++];
                    var R = Pixels[Index++];

                    TargetPixels[Loc++] = (Byte)(((G << 2) & 0xe0) | ((B >> 3) & 0x1f));
                    TargetPixels[Loc++] = (Byte)(((R >> 1) & 0x7c) | ((G >> 6) & 3));
                }
            }


            var TargetBmp = new Bitmap(PixelWidth, PixelHeight, PixelFormat.Format16bppRgb555);


            var TargetBmpData = TargetBmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.WriteOnly, PixelFormat.Format16bppRgb555);
            Marshal.Copy(TargetPixels, 0, TargetBmpData.Scan0, TargetPixels.Length);
            TargetBmp.UnlockBits(TargetBmpData);

            return TargetBmp;
        }

        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <returns>处理以后的Bitmap</returns>
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
        /// <summary>
        /// 将图片转化为字符数组
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromImage(Image img)
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


        public static Image GetImageByBytes(byte[] bytes)
        {
            Image photo = null;
            using (var ms = new MemoryStream(bytes))
            {
                ms.Write(bytes, 0, bytes.Length);
                photo = Image.FromStream(ms, true);
            }
            return photo;

        }

        /// <summary>
        /// 分包处理，将图片分割为1K大小的list数组
        /// </summary>
        /// <param name="image"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public static List<byte[]> ImageByteList(Byte[] image, int chunk)
        {
            var list = new List<byte[]>();
            var a = image.Length / chunk;
            var b = image.Length % chunk;
            for (var i = 0; i < a; i++)
            {
                var temp = new byte[1024];
                Array.Copy(image, 0 + 1024 * i, temp, 0, 1024);
                list.Add(temp);
            }
            if (b > 0)
            {
                var t = new byte[b];
                Array.Copy(image, 0 + 1024 * a, t, 0, b);
                list.Add(t);
            }
            return list;
        }


        public static bool ChangeImageSize(string imgPath, int width, long maxFileSize)
        {
            var bmp = (Bitmap)Image.FromFile(imgPath);
            var s = new Size(width, width * bmp.Height / bmp.Width);
            var newBmp = new Bitmap(bmp, s);
            var ms = TrySaveJpeg(newBmp, maxFileSize);
            newBmp.Dispose();
            bmp.Dispose();

            if (ms == null)
            {
                return false;
            }
            var fs = new FileStream(imgPath, FileMode.Create);
            ms.CopyTo(fs);
            fs.Close();
            return true;
        }

        public static Stream TrySaveJpeg(Bitmap bitmap, long maxByteSize)
        {
            var msOld = new MemoryStream();
            var msNew = new MemoryStream();
            var codec = ImageCodecInfo.GetImageEncoders()
                .FirstOrDefault(c => c.MimeType == "image/jpeg");
            var encParam = new EncoderParameters(1);

            encParam.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 0L);
            bitmap.Save(msOld, codec, encParam);
            if (msOld.Length > maxByteSize)
            {
                return null;
            }
            encParam.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            bitmap.Save(msNew, codec, encParam);

            if (msNew.Length < maxByteSize)
            {
                msNew.Seek(0, SeekOrigin.Begin);
                return msNew;
            }


            long start = 1, end = 99;
            while (start < end)
            {
                var qua = (start + end) / 2;
                encParam.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qua);
                msNew.SetLength(0);
                bitmap.Save(msNew, codec, encParam);
                if (msNew.Length == maxByteSize)
                {
                    msNew.Seek(0, SeekOrigin.Begin);
                    return msNew;
                }
                else
                {
                    if (msNew.Length > maxByteSize)
                    {
                        end = qua - 1;
                    }
                    else
                    {
                        if (msNew.Length < maxByteSize)
                        {
                            start = qua + 1;
                            var temp = msNew;
                            msNew = msOld;
                            msOld = temp;
                        }
                    }
                }
            }
            msOld.Seek(0, SeekOrigin.Begin);
            return msOld;
        }

        public static Bitmap DrawBmp()
        {
            int length = 128;
            int height = 64;
            Bitmap bmp = new Bitmap(length, height);//新建一个图片对象
            ///Bitmap bmp = new Bitmap(length, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            Graphics g = Graphics.FromImage(bmp);//利用该图片对象生成“画板”

            System.Drawing.Font font = new System.Drawing.Font("宋体", 10);//设置字体颜色
            SolidBrush brush = new SolidBrush(Color.Red);//新建一个画刷,到这里为止,我们已经准备好了画板、画刷、和数据

            Pen p = new Pen(Color.Red, 1);//定义了一个红色,宽度为的画笔
            g.Clear(Color.Black); //设置黑色背景
            //一排数据
            g.DrawRectangle(p, 0, 0, 32, 16);//在画板上画矩形,起始坐标为(10,10),宽为80,高为20
            g.DrawRectangle(p, 33, 0, 32, 16);//在画板上画矩形,起始坐标为(90,10),宽为80,高为20
            g.DrawRectangle(p, 65, 0, 32, 16);//
            g.DrawRectangle(p, 97, 0, 32, 16);//
            g.DrawString("目标", font, brush, 2, 2);//
            g.DrawString("完成", font, brush, 34, 2);
            g.DrawString("效率", font, brush, 66, 2);//进行绘制文字。起始坐标为(172, 12)
            g.DrawString("效率", font, brush, 98, 2);//关键的一步，进行绘制文字。


            g.DrawRectangle(p, 0, 17, 32, 16);
            g.DrawRectangle(p, 33, 17, 32, 16);
            g.DrawRectangle(p, 65, 17, 32, 16);
            g.DrawRectangle(p, 97, 17, 32, 16);
            g.DrawString("800", font, brush, 2, 18);
            g.DrawString("500", font, brush, 34, 18);//关键的一步，进行绘制文字。
            g.DrawString("60%", font, brush, 66, 18);//关键的一步，进行绘制文字。
            g.DrawString("50%", font, brush, 98, 18);//关键的一步，进行绘制文字。

            g.DrawRectangle(p, 0, 33, 32, 16);
            g.DrawRectangle(p, 33, 33, 32, 16);
            g.DrawRectangle(p, 65, 33, 32, 16);//在画板上画矩形,起始坐标为(170,10),宽为160,高为20
            g.DrawRectangle(p, 97, 33, 32, 16);//在画板上画矩形,起始坐标为(170,10),宽为160,高为20
            g.DrawString("总查", font, brush, 2, 34);
            g.DrawString("不良", font, brush, 34, 34);
            g.DrawString("合格", font, brush, 66, 34);
            g.DrawString("合格", font, brush, 98, 34);
            bmp.Save(@"C:\Users\Administrator\Desktop\test.bmp", ImageFormat.Bmp);
            ///bmp.Save(@"C:\Users\Administrator\Desktop\test.bmp");//保存为输出流，否则页面上显示不出来
            g.Dispose();//释放掉该资源

            return bmp;
        }

        #region 数组转图像

        public static Image BytesToImage(byte[] arr)
        {
            if (arr == null || arr.Length < 100)
            {
                return null;
            }
            try
            {
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    Image img = Image.FromStream(ms);//在这里出错  
                    return img;
                }
            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region Image转Base64
        //图片 转为    base64编码的文本
        public static byte[] ImageToArray(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return arr;
            }
            catch
            {
                return null;
            }
        }

        #endregion


        #region Image转Base64
        //图片 转为    base64编码的文本
        public static string ImageToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region BaseString 转 Bmp
        //base64编码的文本 转为    图片
        public static Image Base64StringToImage(string inputStr)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 截取人脸
        public static void clipFaceFromImage(ref Bitmap image, System.Drawing.Rectangle rect)
        {

            if (rect.Width != 0 && rect.Height != 0)
            {
                int offsetX = rect.X - rect.Width / 2;
                offsetX = offsetX > 0 ? offsetX : 0;
                int offsetY = rect.Y - (int)(rect.Height / 1.5);
                offsetY = offsetY > 0 ? offsetY : 0;
                int width = rect.Width * 2;
                width = Math.Min(width, image.Width - offsetX);
                int height = (int)(126.0 / 102.0 * width);
                height = Math.Min(height, image.Height - offsetY);
                Bitmap newImage = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(newImage);
                g.DrawImage(image, System.Drawing.Rectangle.FromLTRB(0, 0, width, height), System.Drawing.Rectangle.FromLTRB(offsetX, offsetY, offsetX + width, offsetY + height), GraphicsUnit.Pixel);
                image.Dispose();
                image = newImage;
            }
        }
        #endregion

        #region 检查一个文件是否一张图片
        public static bool IsImage(string path)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region bmp 转jpg
        public static byte[] BmpToJpegBuff(Bitmap bmpSrc)
        {
            MemoryStream ms = new MemoryStream();
            bmpSrc.Save(ms, ImageFormat.Jpeg);
            byte[] jpeg = ms.ToArray();
            return jpeg;
        }
        #endregion

    }
}
