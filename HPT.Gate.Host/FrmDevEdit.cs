using hpt.gate.Util;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Host.Base;
using Joey.UserControls;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Host
{
    public partial class FrmDevEdit : WinBase
    {
        public FrmDevEdit(int placeId, int devId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            try
            {
                DeviceInfo device = DeviceInfoService.GetByDeviceId(devId);
                cbbPlace.SelectedIndex = device.PlaceId;
                cbbDevType.SelectedIndex = device.DeviceType;
                tbMachineId.Text = device.DeviceId.ToString();
                tbDevName.Text = device.DeviceName;
                tbMac.Text = device.Mac;
                tbServerIPAddress.Text = device.ServerIP;
                numServerPort.Value = device.ServerPort;
                tbIP.Text = device.IPAddress;
                tbSunNet.Text = device.SubNet;
                tbGateway.Text = device.GateWay;
                tbPort.Text = device.Port.ToString();
                tbHardVersion.Text = device.HardVersion;
                tbSoftVersion.Text = device.SoftVersion;
                tbServerIPAddress.Text = device.ServerIP;
                numServerPort.Value = (int)device.ServerPort;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取设备信息失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            UpdateDev();
        }

        #region 更新设备信息

        private void UpdateDev()
        {
            DeviceInfo device = new DeviceInfo();
            device.PlaceId = Convert.ToInt32(cbbPlace.SelectedIndex);
            device.DeviceType = Convert.ToInt32(cbbDevType.SelectedIndex);
            device.DeviceId = Convert.ToInt32(tbMachineId.Text);
            device.DeviceName = tbDevName.Text;
            device.Mac = tbMac.Text;
            device.IPAddress = tbIP.Text;
            device.SubNet = tbSunNet.Text;
            device.GateWay = tbGateway.Text;
            device.Port = Convert.ToInt32(tbPort.Text);
            device.HardVersion = tbHardVersion.Text;
            device.SoftVersion = tbSoftVersion.Text;
            device.ServerIP = tbServerIPAddress.Text;
            device.ServerPort = (int)numServerPort.Value;
            string msg;
            if (DeviceInfoService.Update(device, out msg))
                DialogResult = DialogResult.OK;
            else
                MessageBoxHelper.Error($"更新设备信息失败:{msg}");
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmDevEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
