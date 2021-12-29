using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Host.face
{
    public partial class FrmFaceEdit : WinBase
    {
        private string _OldIPAddress;
        private FaceDevice CurrentFaceDevice;
        public FrmFaceEdit(FaceDevice device)
        {
            InitializeComponent();
            CurrentFaceDevice = device;
            _OldIPAddress = device.IPAddress;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            SaveFaceDevice();
        }

        private void SaveFaceDevice()
        {
            CurrentFaceDevice.IPAddress = tbIPAddress.Text;
            CurrentFaceDevice.Mac = tbMac.Text;
            CurrentFaceDevice.Port = (int)numPort.Value;
            CurrentFaceDevice.SN = tbSN.Text;
            CurrentFaceDevice.Password = tbPassword.Text;
            List<FaceDevice> devList = FaceDeviceService.ToList();
            if (devList.Exists(p => p.DeviceId != CurrentFaceDevice.DeviceId && (p.IPAddress.Equals(CurrentFaceDevice.IPAddress))))
            {
                MessageBox.Show($"设备已重复!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                FaceDeviceService.Update(CurrentFaceDevice);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更新人脸设备失败:{ex.Message}", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FrmFaceEdit_Load(object sender, EventArgs e)
        {
            LoadFaceDevice();
        }

        private void LoadFaceDevice()
        {
            numDevId.Value = CurrentFaceDevice.DeviceId;
            tbIPAddress.Text = CurrentFaceDevice.IPAddress;
            numPort.Value = CurrentFaceDevice.Port;
            tbSN.Text = CurrentFaceDevice.SN;
            tbMac.Text = CurrentFaceDevice.Mac;
            tbPassword.Text = CurrentFaceDevice.Password;
        }

        private void FrmFaceEdit_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
