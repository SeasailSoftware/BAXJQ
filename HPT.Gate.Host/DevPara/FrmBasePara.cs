using HPT.Gate.Host.Base;
using HPT.Gate.Device.Data;
using HPT.Gate.Device.Dev;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Host.DevPara
{
    public partial class FrmBasePara : WinBase
    {
        private TcpDevice _CurDevice = null;
        public FrmBasePara(TcpDevice device)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _CurDevice = device;
        }

        private void FrmBasePara_Load(object sender, EventArgs e)
        {
            LoadDevBasePara();
        }

        #region 加载设备基本参数
        private void LoadDevBasePara()
        {
            tbDevName.Text = _CurDevice._DeviceName;
            tbMac.Text = _CurDevice._Mac;
            BasePara para = _CurDevice.GetBasePara();
            if (para == null)
            {
                MessageBox.Show("获取设备基本参数失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                numWaitTime.Value = para.WaitTime;
                numRepeatTime.Value = para.RepeatTime;
                numDelayTime.Value = para.DelayTime;
                cbbDirection.SelectedIndex = para.Direction;
                tbP1SerialNo.Text = para.P1SerialNo;
                cbbP1.SelectedIndex = para.IOOFP1;
                tbHardVersion1.Text = para.HardVersionOfP1;
                tbSoftVersion1.Text = para.SoftVersionOfP1;
                tbP2SerialNo.Text = para.P2SerialNo;
                cbbP2.SelectedIndex = para.IOOFP2;
                tbHardVersion2.Text = para.HardVersionOfP2;
                tbSoftVersion2.Text = para.SoftVersionOfP2;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载设备基本参数失败:{ex.Message}", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

        }
        #endregion

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SetBasePara();
        }

        #region 设置设备基本参数
        private void SetBasePara()
        {
            BasePara para = new BasePara();
            para.WaitTime = (byte)numWaitTime.Value;
            para.RepeatTime = (UInt16)numRepeatTime.Value;
            para.DelayTime = (byte)numDelayTime.Value;
            para.Direction = (byte)cbbDirection.SelectedIndex;
            para.P1SerialNo = tbP1SerialNo.Text;
            para.IOOFP1 = (byte)cbbP1.SelectedIndex;
            para.HardVersionOfP1 = tbHardVersion1.Text;
            para.SoftVersionOfP1 = tbSoftVersion1.Text;
            para.P2SerialNo = tbP2SerialNo.Text;
            para.IOOFP2 = (byte)cbbP2.SelectedIndex;
            para.HardVersionOfP2 = tbHardVersion2.Text;
            para.SoftVersionOfP2 = tbSoftVersion2.Text;
            if (_CurDevice.SetBasePara(para))
            {
                MessageBox.Show("设置设备基本参数成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("设置设备基本参数失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion



    }
}
