using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace HPT.Face.Utils
{
    public class ConvertHelper
    {
        #region Utils
        /// <summary>
        /// 返回时间对应的时间戳,单位毫秒
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double DateTimeToTimeStamp(DateTime dt)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            double timeStamp = (dt - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }

        //base64编码的文本 转为    图片
        public static Image Base64StringToImage(string inputStr)
        {

            try
            {
                inputStr = HttpUtility.UrlDecode(inputStr);
                byte[] arr = Convert.FromBase64String(inputStr);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    Image bmp = Image.FromStream(ms);
                    return bmp;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string BytesToBase64String(byte[] array)
        {
            return Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks).Replace("+", "%2B");
            //return Convert.ToBase64String(array);
        }
        public static string ImageToBase64String(Image img)
        {
            try
            {

                using (Bitmap bmp = new Bitmap(img))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] arr = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(arr, 0, (int)ms.Length);
                        return HttpUtility.UrlEncode(Convert.ToBase64String(arr)).Replace("%2F","");
                        return Convert.ToBase64String(arr, Base64FormattingOptions.None).Replace("+", "%2B");
                        //return Convert.ToBase64String(arr, Base64FormattingOptions.None).Replace("+", "%2B").Replace(@"\n", "").Replace(" ","").Replace(@"\t","").Replace(@"\r","");
                        return Convert.ToBase64String(arr);
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region 从FTP获取图片
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
        #endregion

    }
}
