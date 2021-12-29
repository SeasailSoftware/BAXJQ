using hpt.gate.DataAccess.Entity;
using hpt.gate.DataAccess.Service;
using HPT.Gate.Host.Base;
using HPT.Gate.Host.Config;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Host
{
    public partial class FrmSettings : WinBase
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveConfig();
            DialogResult = DialogResult.OK;
        }

        #region 保存配置
        private void SaveConfig()
        {
            AppSettings.LedEnabled = ckbLedModule.Checked;
            AppSettings.CameraEnabled = ckbCamModule.Checked;
            AppSettings.LocalPort = (int)numLocalPort.Value;
            AppSettings.AutoClearEnabled = ckbAutoClear.Checked;
            AppSettings.AutoClearTime = dtpAutoClear.Text;
            AppSettings.LimitedTotalEnabled = ckbLimitTotalEnabled.Checked;
            AppSettings.LimitedTotalOfInside = (int)numLimitTotal.Value;
            AppSettings.AutoBackupData = ckbAutoBak.Checked;
            SystemConfig config = new SystemConfig()
            {
                FaceEnabled = ckbFaceEnabled.Checked,
                FaceMachineType = cbbFaceMachine.SelectedIndex,
                OutPutType = cbbFaceOutPutType.SelectedIndex
            };
            SystemConfigService.Set(config);
            AppSettings.SynCardEnabled = ckbSynCardData.Checked;
            AppSettings.NetCamType = cbbNetcamType.SelectedIndex;
            AppSettings.FingerPrintEnabled = ckbFingerPrint.Checked;
            AppSettings.RightsType = cbbRightsType.SelectedIndex;
            AppSettings.FingerPrintType = cbbFPType.SelectedIndex;
            AppSettings.JMSServer = tbJMSServer.Text;
            AppSettings.ServerURL = tbServerURL.Text;
            AppSettings.JMSFilter = tbJMSFilter.Text;
            AppSettings.JMSClient = tbJMSClient.Text;

        }
        #endregion


        private void FrmSettings_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        #region 加载配置
        private void LoadConfig()
        {
            ckbLedModule.Checked = AppSettings.LedEnabled;
            ckbCamModule.Checked = AppSettings.CameraEnabled;
            numLocalPort.Value = AppSettings.LocalPort;
            ckbAutoClear.Checked = AppSettings.AutoClearEnabled;
            dtpAutoClear.Text = AppSettings.AutoClearTime;
            ckbLimitTotalEnabled.Checked = AppSettings.LimitedTotalEnabled;
            numLimitTotal.Value = AppSettings.LimitedTotalOfInside;
            ckbAutoBak.Checked = AppSettings.AutoBackupData;
            SystemConfig config = SystemConfigService.Get();
            ckbFaceEnabled.Checked = config.FaceEnabled;
            cbbFaceMachine.SelectedIndex = config.FaceMachineType;
            cbbFaceOutPutType.SelectedIndex = config.OutPutType;
            ckbSynCardData.Checked = AppSettings.SynCardEnabled;
            cbbNetcamType.SelectedIndex = AppSettings.NetCamType;
            ckbFingerPrint.Checked = AppSettings.FingerPrintEnabled;
            cbbRightsType.SelectedIndex = AppSettings.RightsType;
            cbbFPType.SelectedIndex = AppSettings.FingerPrintType;
            tbJMSServer.Text = AppSettings.JMSServer;
            tbServerURL.Text = AppSettings.ServerURL;
            tbJMSAccount.Text = AppSettings.JMSAccount;
            tbJMSPassword.Text = AppSettings.JMSPassword;
            tbJMSFilter.Text = AppSettings.JMSFilter;
            tbJMSClient.Text = AppSettings.JMSClient;
        }
        #endregion


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
