using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Host.camera
{
    public partial class FrmCameraEdit : WinBase
    {
        private int _CamId;
        private string _IPAddress = string.Empty;
        public FrmCameraEdit(int camId)
        {
            InitializeComponent();
            _CamId = camId;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmCameraEdit_Load(object sender, EventArgs e)
        {
            LoadCamera();
        }

        #region 加载摄像头
        private void LoadCamera()
        {
            Task.Factory.StartNew(() =>
            {
                CameraInfo camera = null;
                try
                {
                    camera = CameraInfoService.GetByCamId(_CamId);
                }
                catch
                {

                }
                this.Invoke(new Action(() =>
                {

                    if (camera != null)
                    {
                        tbCamName.Text = camera.CamName;
                        tbIPAddress.Text = camera.IPAddress;
                        numPort.Value = camera.Port;
                        tbUserName.Text = camera.UserName;
                        tbPassword.Text = camera.Password;
                        tbMark.Text = camera.Mark;
                        _IPAddress = camera.IPAddress;
                    }
                }));
            });
        }
        #endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveCamera();
        }

        #region 保存摄像头信息
        private void SaveCamera()
        {
            CameraInfo camera = new CameraInfo();
            camera.CamId = _CamId;
            string camName = tbCamName.Text.Trim();
            camera.CamName = camName;
            if (camName.Equals(string.Empty))
            {
                MessageBox.Show("设备名称不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string mark = tbMark.Text.Trim();
            camera.Mark = mark;
            string ipAddress = tbIPAddress.Text.Trim();
            camera.IPAddress = ipAddress;
            if (ipAddress.Equals(string.Empty))
            {
                MessageBox.Show("设备名称不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int port = (int)numPort.Value;
            camera.Port = port;
            string userName = tbUserName.Text.Trim();
            camera.UserName = userName;
            string password = tbPassword.Text.Trim();
            camera.Password = password;
            if (!_IPAddress.Equals(ipAddress) && CameraInfoService.CheckCameraExists(ipAddress))
            {
                MessageBox.Show("IP地址为[" + ipAddress + "]的设备已经存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                CameraInfoService.Update(camera);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加摄像头失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

    }
}
