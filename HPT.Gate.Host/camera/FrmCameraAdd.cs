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

namespace HPT.Gate.Host.camera
{
    public partial class FrmCameraAdd : WinBase
    {
        public FrmCameraAdd()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveCamera();
        }

        #region 保存摄像头信息
        private void SaveCamera()
        {
            CameraInfo camera = new CameraInfo();
            string camName = textBox1.Text.Trim();
            camera.CamName = camName;
            if (camName.Equals(string.Empty))
            {
                MessageBox.Show("设备名称不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string mark = textBox2.Text.Trim();
            camera.Mark = mark;
            string ipAddress = textBox3.Text.Trim();
            camera.IPAddress = ipAddress;
            if (ipAddress.Equals(string.Empty))
            {
                MessageBox.Show("设备名称不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int port = (int)numericUpDown1.Value;
            camera.Port = port;
            string userName = textBox4.Text.Trim();
            camera.UserName = userName;
            string password = textBox6.Text.Trim();
            camera.Password = password;
            if (CameraInfoService.CheckCameraExists(ipAddress))
            {
                MessageBox.Show("IP地址为[" + ipAddress + "]的设备已经存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                CameraInfoService.Insert(camera);
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
