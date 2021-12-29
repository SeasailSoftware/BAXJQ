using HPT.Gate.Host.Base;
using HPT.Gate.Device.Data;
using HPT.Gate.Device.Dev;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HPT.Gate.Host.Util;
using HPT.Gate.Host.Config;

namespace HPT.Gate.Host.DevPara
{
    public partial class FrmUploadBGImage : WinBase
    {
        private List<TcpDevice> _DevList = new List<TcpDevice>();
        public FrmUploadBGImage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FrmUploadBGImage_Load(object sender, EventArgs e)
        {
            LoadDevList();
            comboBoxItem1.ComboBoxEx.SelectedIndex = 0;
        }

        #region 加载设备列表
        private void LoadDevList()
        {
            List<DeviceInfo> devList = DeviceInfoService.ToList();
            dgvDevice.DataSource = null;
            dgvDevice.Rows.Clear();
            foreach (DeviceInfo dev in devList)
            {
                TcpDevice device = DataConverter.GetTcpDevice(dev);
                _DevList.Add(device);
                int index = dgvDevice.Rows.Add();
                device.RowIndex = index;
                dgvDevice.Rows[index].Cells[0].Value = false;
                dgvDevice.Rows[index].Cells[1].Value = device._MachineId;
                dgvDevice.Rows[index].Cells[2].Value = device._DeviceName;
                dgvDevice.Rows[index].Cells[3].Value = device._Mac;
                dgvDevice.Rows[index].Cells[4].Value = 0;
            }
        }
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckedAllDevice(checkBox1.Checked);
        }

        private void CheckedAllDevice(bool flag)
        {
            foreach (DataGridViewRow row in dgvDevice.Rows)
            {
                row.Cells[0].Value = flag;
            }
        }

        private void dgvDevice_Sorted(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvDevice.Rows)
            {
                foreach (TcpDevice device in _DevList)
                {
                    if (row.Cells[3].Value.ToString().Equals(device._Mac))
                    {
                        device.RowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (!btUploadBGImage.Enabled)
            {
                MessageBox.Show("正在上传背景图片,请稍候...", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.Close();
        }

        private void btUploadBGImage_Click(object sender, EventArgs e)
        {
            List<TcpDevice> devList = GetDevsByChecked();
            if (devList.Count == 0)
            {
                MessageBox.Show("没有可操作的设备!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (comboBoxItem1.ComboBoxEx.SelectedIndex == -1)
            {
                MessageBox.Show("请选择出入口!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int ioFlag = comboBoxItem1.ComboBoxEx.SelectedIndex == 0 ? 3 : 4;
            Bitmap bmp = GetImageFromLocal();
            btUploadBGImage.Enabled = false;
            foreach (TcpDevice dev in devList)
            {
                SendBackgroupImage(dev, bmp, ioFlag);
                Application.DoEvents();
            }
            btUploadBGImage.Enabled = true;
        }

        #region 上传背景图片
        private void SendBackgroupImage(TcpDevice dev, Bitmap bmp, int ioFlag)
        {
            string io = ioFlag == 3 ? "入口" : "出口";
            ShowMsg(string.Format("开始上传主板[{0}]{1}背景照片,请稍候...", dev._DeviceName, io));
            Application.DoEvents();
            bmp = ImageHelper.KiResizeImage(bmp, 160, 160);
            List<byte[]> imageBytes = ImageHelper.ImageByteList(ImageHelper.GetByteImage(bmp), 1024);
            int value = 0;
            UpdateProgress(dev.RowIndex, value);
            for (int index = 0; index < imageBytes.Count; index++)
            {
                DataBmp dbmp = new DataBmp();
                dbmp.Index = new byte[] { (byte)index };
                dbmp.BmpType = new byte[] { (byte)ioFlag };
                dbmp.BmpName = ArrayHelper.IntToBytes(0xFFFFFFFF);
                dbmp.BmpBytes = imageBytes[index];
                ShowMsg(string.Format("[{0}]开始上传图片数组[{1}],请稍候...", dev._DeviceName, index));
                if (!dev.SendImagePacket(dbmp))
                {
                    ShowMsg(string.Format("[{0}]上传图片数组[{1}]失败，重新发送...", dev._DeviceName, index));
                    if (!dev.SendImagePacket(dbmp))
                    {
                        ShowMsg(string.Format("[{0}]再次上传图片数组[{1}]失败，上传图片终止", dev._DeviceName, index));
                        return;
                    }
                }
                value = (index + 1) * 100 / imageBytes.Count;
                UpdateProgress(dev.RowIndex, value);
                ShowMsg(string.Format("[{0}]上传图片数组[{1}]成功!", dev._DeviceName, index));
                Application.DoEvents();
            }
            ShowMsg(string.Format("[{0}]上传[{1}]背景图片成功!", dev._DeviceName, io));
            /*
            if (dev.SendImage(bmp, ioFlag, 0xFFFFFFFF))
            {
                ShowMsg(string.Format("[{0}]上传[{1}]背景图片成功!", dev._DeviceName, io));
                SetUpdateStatus(dev.RowIndex, 0);
            }
            else
            {
                ShowMsg(string.Format("[{0}]上传[{1}]背景图片失败!", dev._DeviceName, io));
                SetUpdateStatus(dev.RowIndex, 1);
            }
            */
        }
        #endregion


        #region 消息提示
        private delegate void dlgShowMsg(string msg);
        private void ShowMsg(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                dlgShowMsg dlg = new dlgShowMsg(ShowMsg);
                txtLog.Invoke(dlg, msg);

            }
            else
            {
                if (txtLog.Lines.Length != 0)
                    txtLog.AppendText("\r\n");
                txtLog.AppendText(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
            }
        }
        #endregion

        #region 获取选中列表
        private List<TcpDevice> GetDevsByChecked()
        {
            List<TcpDevice> devList = new List<TcpDevice>();
            for (int i = 0; i < dgvDevice.Rows.Count; i++)
            {
                DataGridViewRow row = dgvDevice.Rows[i];
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    string mac = row.Cells[3].Value.ToString();
                    foreach (TcpDevice device in _DevList)
                    {
                        if (mac.Equals(device._Mac))
                        {
                            devList.Add(device);
                            break;
                        }
                    }
                }
            }
            return devList;
        }
        #endregion

        #region 从本地获取照片
        private Bitmap GetImageFromLocal()
        {
            //获取图片
            Bitmap bmp = null;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppSettings.LastPath;
            openFileDialog.Filter = @"BMP 图像 (.bmp)|*.bmp|JPEG 图像 (.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 3;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog.FileName);
                string directory = openFileDialog.FileName.Substring(0, openFileDialog.FileName.LastIndexOf("\\"));
                AppSettings.LastPath = directory;
            }
            return bmp;
        }
        #endregion

        #region 更新进度
        private delegate void dlgUpdateProgress(int rowIndex, int value);
        private void UpdateProgress(int rowIndex, int value)
        {
            if (dgvDevice.InvokeRequired)
            {
                dlgUpdateProgress dlg = new dlgUpdateProgress(UpdateProgress);
                dgvDevice.Invoke(dlg, rowIndex, value);

            }
            else
            {
                dgvDevice.Rows[rowIndex].Cells[4].Value = value;
            }
        }
        #endregion
    }
}
