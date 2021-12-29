using hpt.gate.Util;
using HPT.Gate.Host.Base;
using HPT.Gate.Host.Config;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Host.Lcd
{
    public partial class FrmLcdSettings : WinBase
    {
        public FrmLcdSettings()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmLcdSettings_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillCameraComBoBox(cbbCam1);
            ComboBoxHelper.FillCameraComBoBox(cbbCam2);
            LoadConfig();
        }

        #region 加载配置文件
        private void LoadConfig()
        {
            tbTitle.Text = AppSettings.LcdTitle;
            cbbCam1.SelectedValue = AppSettings.LcdCamOfIn;
            cbbCam2.SelectedValue = AppSettings.LcdCamOfOut;
        }
        #endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveConfig();
            DialogResult = DialogResult.OK;
        }

        #region 保存配置
        private void SaveConfig()
        {
            AppSettings.LcdTitle = tbTitle.Text;
            AppSettings.LcdCamOfIn = cbbCam1.SelectedIndex == -1 ? 0 : Convert.ToInt32(cbbCam1.SelectedValue);
            AppSettings.LcdCamOfOut = cbbCam2.SelectedIndex == -1 ? 0 : Convert.ToInt32(cbbCam2.SelectedValue);
        }
        #endregion

    }
}
