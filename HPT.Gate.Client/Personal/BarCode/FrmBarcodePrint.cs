using com.google.zxing;
using com.google.zxing.common;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Joey.UserControls;
using HPT.Gate.Client.config;

namespace HPT.Gate.Client.BarCode
{
    public partial class FrmBarcodePrint : JForm
    {
        string _BarcodeContent
        {
            get { return CreateNewBarcode(); }
        }

        private string _CurrentBarcode;
        Font _DefaultFont = new Font("微软雅黑", 12, FontStyle.Bold);
        string _BarcodeTitle = AppSettings.BarcodeContent;
        Font _TitleFont = new Font("微软雅黑", 12, FontStyle.Bold);
        public FrmBarcodePrint()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FrmBarcodePrint_Load(object sender, EventArgs e)
        {
            CreateBarcode();
            LoadPrinters();
        }

        #region 生成二维码
        private void CreateBarcode()
        {
            Bitmap bmap = new Bitmap(450, 200, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmap);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 450, 200));
            #region 画出二维码
            _CurrentBarcode = _BarcodeContent;
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(_CurrentBarcode, BarcodeFormat.QR_CODE, 200, 200);
            int width = byteMatrix.Width;
            int height = byteMatrix.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, byteMatrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml("0xFF000000") : ColorTranslator.FromHtml("0xFFFFFFFF"));
                }
            }
            #endregion

            #region 画出欢迎词
            Rectangle rect = new Rectangle(200, 30, 250, 140);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            g.DrawString(_BarcodeTitle, _TitleFont, Brushes.Black, rect, format);
            #endregion

            g.Dispose();
            pbBarcode.Image = bmap;

        }
        #endregion




        #region 加载打印机列表
        private void LoadPrinters()
        {
            PrintDocument print = new PrintDocument();
            string sDefault = print.PrinterSettings.PrinterName;//默认打印机名
            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                cbbPrinters.Items.Add(sPrint);
                if (sPrint == sDefault)
                    cbbPrinters.SelectedIndex = cbbPrinters.Items.IndexOf(sPrint);
            }
        }
        #endregion

        private void cbbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        #region 二维码打印
        private void PrintBarcode(object sender, PrintPageEventArgs e)
        {
            Image image = new Bitmap(pbBarcode.Image);
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            e.Graphics.DrawImage(image, 0, 0, image.Width, image.Height);
        }
        #endregion

        #region 生成新二维码
        private string CreateNewBarcode()
        {
            string barcode = string.Empty;
            Random ran = new Random();
            for (int i = 0; i < 6; i++)
            {
                byte b = (byte)ran.Next(0, 9);
                barcode += b.ToString("X2");
            }
            if (!BarcodeService.CheckBarcodeExists(barcode))
                return barcode;
            return CreateNewBarcode();
        }
        #endregion

        #region 保存二维码
        private void SaveBarcode()
        {
            try
            {
                Barcode barcode = new Barcode();
                barcode.BarcodeNo = _CurrentBarcode;
                barcode.DevList = AppSettings.BarcodeDeviceList;
                barcode.EffectTime = AppSettings.BarcodeEffectTime;
                barcode.TimesOfIn = AppSettings.BarcodeTimesOfIn;
                barcode.TimesOfOut = AppSettings.BarcodeTimesOfOut;
                BarcodeService.Insert(barcode);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"保存条码信息失败:{ex.Message}");
            }
        }

        #endregion

        private void buttonX4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = cbbPrinters.Text;
                pd.PrinterSettings.Copies = 1;
                pd.PrintController = new StandardPrintController();
                pd.PrintPage += new PrintPageEventHandler(PrintBarcode);
                if (pd.PrinterSettings.IsValid)
                {
                    pd.Print();
                    SaveBarcode();
                    Application.DoEvents();
                    CreateBarcode();
                }
                else
                {
                    MessageBoxHelper.Error("打印机连接错误");
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"打印条形码失败:{ex.Message}");
            }
        }
    }
}
