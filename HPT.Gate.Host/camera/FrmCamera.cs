using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System.Threading.Tasks;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.Host.camera;

namespace HPT.Gate.Host
{
    public partial class FrmCamera : WinBase
    {
        public FrmCamera()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonItem22_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmCameraAdd cameraAdd = new FrmCameraAdd();
            DialogResult dr = cameraAdd.ShowDialog();
            if (dr == DialogResult.OK)
                LoadCameraList();
        }

        private void FCamera_Load(object sender, EventArgs e)
        {
            LoadCameraList();
            LoadBindings();
            tabControl1.SelectedTabIndex = 0;
        }

        /// <summary>
        /// 加载摄像头列表
        /// </summary>
        private void LoadCameraList()
        {
            Task.Factory.StartNew(() =>
            {
                List<CameraInfo> cameraList = new List<CameraInfo>();
                try
                {
                    cameraList = CameraInfoService.ToList();
                }
                catch
                {

                }
                this.Invoke(new Action(() =>
                {
                    dgvCamera.DataSource = null;
                    dgvCamera.Rows.Clear();
                    foreach (CameraInfo camera in cameraList)
                    {
                        int rowIndex = dgvCamera.Rows.Add();
                        dgvCamera.Rows[rowIndex].Cells[0].Value = camera.CamId;
                        dgvCamera.Rows[rowIndex].Cells[1].Value = camera.CamName;
                        dgvCamera.Rows[rowIndex].Cells[2].Value = camera.IPAddress;
                        dgvCamera.Rows[rowIndex].Cells[3].Value = camera.Port;
                        dgvCamera.Rows[rowIndex].Cells[4].Value = camera.UserName;
                        dgvCamera.Rows[rowIndex].Cells[5].Value = camera.Password;
                        dgvCamera.Rows[rowIndex].Cells[6].Value = camera.Mark;
                    }
                }));
            });

        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (dgvCamera.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要编辑的摄像头信息!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataGridViewRow row = dgvCamera.SelectedRows[0];
            int camId = Convert.ToInt32(row.Cells[0].Value);
            FrmCameraEdit cameraEdit = new FrmCameraEdit(camId);
            DialogResult dr = cameraEdit.ShowDialog();
            if (dr == DialogResult.OK)
                LoadCameraList();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (dgvCamera.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要删除的摄像头信息!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int rowIndex = dgvCamera.SelectedRows[0].Index;
            if (rowIndex < 0)
            {
                MessageBox.Show("请选择需要删除的摄像头信息!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int camId = Convert.ToInt32(dgvCamera.SelectedRows[0].Cells[0].Value);
            MessageBoxButtons messageButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除摄像头信息吗?", "删除摄像头询问", messageButton, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                try
                {
                    CameraInfoService.Del(camId);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除摄像头失败!错误代码:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            LoadCameraList();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            FrmCamBindingAdd devBinding = new FrmCamBindingAdd();
            DialogResult dr = devBinding.ShowDialog();
            if (dr == DialogResult.OK)
                LoadBindings();
        }

        #region 加载绑定关系
        private void LoadBindings()
        {
            Task.Factory.StartNew(() =>
            {
                List<CameraOfDevice> bindings = new List<CameraOfDevice>();
                try
                {
                    bindings = CameraOfDeviceService.ToList();
                }
                catch
                {

                }
                this.Invoke(new Action(() =>
                {
                    dgvBinding.DataSource = null;
                    dgvBinding.Rows.Clear();
                    foreach (CameraOfDevice binding in bindings)
                    {
                        int rowIndex = dgvBinding.Rows.Add();
                        dgvBinding.Rows[rowIndex].Cells[0].Value = binding.RecId;
                        dgvBinding.Rows[rowIndex].Cells[1].Value = binding.DeviceId;
                        dgvBinding.Rows[rowIndex].Cells[2].Value = binding.DeviceName;
                        dgvBinding.Rows[rowIndex].Cells[3].Value = binding.InCamId;
                        dgvBinding.Rows[rowIndex].Cells[4].Value = binding.InCamName;
                        dgvBinding.Rows[rowIndex].Cells[5].Value = binding.OutCamId;
                        dgvBinding.Rows[rowIndex].Cells[6].Value = binding.OutCamName;
                    }
                }));
            });
        }
        #endregion

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            if (dgvBinding.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要编辑的摄像头绑定信息!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataGridViewRow row = dgvBinding.SelectedRows[0];
            int recId = Convert.ToInt32(row.Cells[0].Value);
            FrmCamBindingEdit bindingEdit = new FrmCamBindingEdit(recId);
            DialogResult dr = bindingEdit.ShowDialog();
            if (dr == DialogResult.OK)
                LoadBindings();
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            if (dgvBinding.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需要删除的摄像头信息!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int devId = Convert.ToInt32(dgvBinding.SelectedRows[0].Cells["Column3"].Value);
            MessageBoxButtons messageButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除摄像头绑定信息吗?", "删除摄像头绑定询问", messageButton, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                try
                {
                    CameraOfDeviceService.Del(devId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除摄像头绑定失败!错误代码:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            LoadBindings();
        }
    }
}