using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using hpt.gate.Util;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT.Gate.Host.Base;
using HPT.Gate.Host.extenssion;

namespace HPT.Gate.Host.camera
{
    public partial class FrmCamBindingEdit : WinBase
    {
        private int _RecId;
        public FrmCamBindingEdit(int recId)
        {
            InitializeComponent();
            _RecId = recId;
        }

        private void FrmCamBindingEdit_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillDeviceComBoBox(comboBox1);
            ComboBoxHelper.FillCameraComBoBox(comboBox2);
            ComboBoxHelper.FillCameraComBoBox(comboBox3);
            Task.Factory.StartNew(() =>
            {
                CameraOfDevice binding = null;
                try
                {
                    binding = CameraOfDeviceService.GetByRecId(_RecId);
                }
                catch
                {

                }
                this.Invoke(new Action(() =>
                {
                    if (binding != null)
                    {
                        comboBox1.SelectedValue = binding.DeviceId;
                        comboBox2.SelectedValue = binding.InCamId;
                        comboBox3.SelectedValue = binding.OutCamId;
                    }
                }));
            }).LogExceptions();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveBinding();
        }

        #region 保存绑定
        private void SaveBinding()
        {
            CameraOfDevice binding = new CameraOfDevice();
            binding.RecId = _RecId;
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
            try
            {
                CameraOfDeviceService.Update(binding);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加摄像头绑定关系失败:" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
