using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
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
    public partial class FrmFaceAdd : WinBase
    {
        public string IPAddress;
        public FrmFaceAdd()
        {
            InitializeComponent();
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
            FaceDevice device = new FaceDevice();
            device.IPAddress = tbIPAddress.Text;
            device.Port = (int)numPort.Value;
            device.Mac = tbMac.Text;
            device.SN = tbSN.Text;
            device.Password = tbPass.Text;
            if (string.IsNullOrEmpty(device.IPAddress))
            {
                MessageBox.Show("IP地址不能为空!","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(device.Password))
            {
                MessageBox.Show("密码不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FaceDeviceService.CheckIPAddressExists(device.IPAddress) || FaceDeviceService.CheckMachineIdExists(device.DeviceId))
            {
                MessageBox.Show("设备已存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                FaceDeviceService.Insert(device);
                //EmpInfoService.AddFaceDataTask(null,device,FingerPrintAndFaceCommand.AddDevice);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加人脸设备失败:{ex.Message}", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmFaceAdd_Load(object sender, EventArgs e)
        {

        }

        private void FrmFaceAdd_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
