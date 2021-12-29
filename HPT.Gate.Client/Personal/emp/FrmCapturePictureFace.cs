using hpt.gate.DataAccess.Entity;
using hpt.gate.DataAccess.Service;
using HPT.Face;
using HPT.Face.AXD;
using HPT.Face.HPT;
using HPT.Face.SYD;
using HPT.Face.YF;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using HPT.Joey.Lib.Utils;
using Joey.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HPT.Gate.Client.Personal.emp
{
    public partial class FrmCapturePictureFace : JWindow
    {
        public Image FaceImage;
        public FrmCapturePictureFace()
        {
            InitializeComponent();
        }

        private void FrmCapturePictureFace_Load(object sender, EventArgs e)
        {

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBoxHelper.Info("请先选择照片!");
                return;
            }
            FaceImage = pictureBox1.Image;
            DialogResult = DialogResult.OK;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog() { Filter = @"所有文件(*.*)|*.*|PNG 图像 (.png)|*.png|BMP 图像 (.bmp)|*.bmp|JPEG 图像 (.jpg)|*.jpg", FilterIndex = 0, RestoreDirectory = true };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            string fileType = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf(".")).ToUpper();
            if (!(fileType.Equals(".PNG") || fileType.Equals(".BMP") || fileType.Equals(".JPG")))
            {
                MessageBoxHelper.Info("不是有效的图片格式!");
                return;
            }
            try
            {
                using (Bitmap bmp = (Bitmap)Bitmap.FromFile(openFileDialog.FileName))
                {
                    Image image = ImageHelper.KiResizeImage(bmp, pictureBox1.Width, pictureBox1.Height);
                    string msg;
                    FaceDevice device = FaceDeviceService.FirstOrDefaultOnline();
                    if (device == null)
                    {
                        MessageBoxHelper.Error("尚未添加人脸设备或者设备全部连线,无法检测图片是否符合规定!");
                        return;
                    }
                    SystemConfig config = SystemConfigService.Get();
                    if (config.FaceEnabled)
                    {
                        HFace service = null;
                        switch (config.FaceMachineType)
                        {
                            case 0:
                                service = new HPTFace();
                                break;
                            case 1:
                                service = new YFFace();
                                break;
                            case 2:
                                service = new SYDFace();
                                break;
                            case 3:
                                service = new AXDFace();
                                break;
                        }
                        if (service.CheckFace(device.IPAddress, device.Password, image, out msg))
                            pictureBox1.Image = image;
                        else
                            MessageBoxHelper.Info($"图片不符合格式:{msg}");
                    }
                    else
                        pictureBox1.Image = image;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"获取图片失败:{ex.Message}");
            }
        }
    }
}
