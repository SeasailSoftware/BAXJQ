using hpt.gate.Util;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Host.Base;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Host.camera
{
    public partial class FrmCamBindingAdd : WinBase
    {
        public FrmCamBindingAdd()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmCamBindingAdd_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillDeviceComBoBox(comboBox1);
            ComboBoxHelper.FillCameraComBoBox(comboBox2);
            ComboBoxHelper.FillCameraComBoBox(comboBox3);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveBingding();
        }

        #region 保存绑定
        private void SaveBingding()
        {
            CameraOfDevice binding = new CameraOfDevice();
            int devId = Convert.ToInt32(comboBox1.SelectedValue);
            if (devId == 0)
            {
                MessageBox.Show("请选择需要绑定的通道闸设备!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            binding.DeviceId = devId;
            string devName = comboBox1.Text;
            binding.DeviceName = devName;
            int inCamId = Convert.ToInt32(comboBox2.SelectedValue);
            binding.InCamId = inCamId;
            string inCamName = comboBox2.Text;
            binding.InCamName = inCamName;
            int outCamId = Convert.ToInt32(comboBox3.SelectedValue);
            binding.OutCamId = outCamId;
            string outCamName = comboBox3.Text;
            binding.OutCamName = outCamName;
            if (CameraOfDeviceService.CheckCamOfDevice(devId))
            {
                MessageBox.Show("该通道闸已存在绑定关系,不能重复添加!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                CameraOfDeviceService.Insert(binding);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加摄像头绑定关系失败:" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
    #endregion

}

