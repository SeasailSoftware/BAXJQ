using AForge.Video.DirectShow;
using HPT.Joey.Lib.Utils;
using Joey.UserControls;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace HPT.Face.Client.Personal.emp
{
    public partial class FrmCaptureFace : JWindow
    {
        public Image CurrentPhoto = null;
        private bool IsRunning = false;
        public FrmCaptureFace()
        {
            InitializeComponent();
        }

        private void FrmCaptureFace_Load(object sender, EventArgs e)
        {
            try
            {
                FilterInfoCollection videoDevices;
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                FilterInfo info = videoDevices[0];//选取第一个,此处可作灵活改动
                VideoCaptureDevice videoSource = new VideoCaptureDevice(info.MonikerString);
                picCamera.VideoSource = videoSource;
                picCamera.Start();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Info($"启动摄像头失败:{ex.Message}");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {


        }

        private void FrmCaptureFace_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                picCamera.SignalToStop();
                picCamera.WaitForStop();
            }
            catch
            {

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX6_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void picCamera_Click(object sender, EventArgs e)
        {

        }

        private Image DoCapture()
        {
            if (picCamera.IsRunning)
            {
                BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                picCamera.GetCurrentVideoFrame().GetHbitmap(),
                                IntPtr.Zero,
                                 Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());
                PngBitmapEncoder pE = new PngBitmapEncoder();
                pE.Frames.Add(BitmapFrame.Create(bitmapSource));
                string picName = $@"{Environment.CurrentDirectory}\Capture.jpg";
                if (File.Exists(picName))
                    File.Delete(picName);
                using (Stream stream = File.Create(picName))
                {
                    pE.Save(stream);
                }
                return Image.FromFile(picName);
            }
            return null;
        }

        private void FrmCaptureFace_Load_1(object sender, EventArgs e)
        {
            try
            {
                FilterInfoCollection videoDevices;
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    MessageBoxHelper.Info("尚未连接摄像头!");
                    return;
                }
                FilterInfo info = videoDevices[0];//选取第一个,此处可作灵活改动
                VideoCaptureDevice videoSource = new VideoCaptureDevice(info.MonikerString);
                picCamera.VideoSource = videoSource;
                picCamera.Start();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Info($"启动摄像头失败:{ex.Message}");
            }
        }

        private void FrmCaptureFace_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            try
            {
                picCamera.SignalToStop();
                picCamera.WaitForStop();
            }
            catch
            {

            }
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX6_Click_1(object sender, EventArgs e)
        {
            if (picCapture.Image == null)
            {
                MessageBoxHelper.Info("请先抓拍!");
                return;
            }
            CurrentPhoto = picCapture.Image;
            DialogResult = DialogResult.OK;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            try
            {
                using (Image image = DoCapture())
                {
                    picCapture.Image = ImageHelper.KiResizeImage((Bitmap)image, picCapture.Width, picCapture.Height);
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"抓拍失败:{ex.Message}");
            }
            finally
            {

            }
        }
    }
}
