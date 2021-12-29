using HPT.Gate.Client.config;
using Joey.UserControls;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Client.BarCode
{
    public partial class FrmBarcodeTemplate : JForm
    {
        public FrmBarcodeTemplate()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        private void FrmBarcodeTemplate_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        #region 加载配置文件
        private void LoadConfig()
        {
            tbContent.Text = AppSettings.BarcodeContent;
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            AppSettings.BarcodeContent = tbContent.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
