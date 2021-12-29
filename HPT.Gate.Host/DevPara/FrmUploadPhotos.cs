using HPT.Gate.Host.Base;
using hpt.gate.DbTools.Service;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HPT.Gate.Device.Dev;
using HPT.Gate.Host.Util;
using HPT.Gate.Device.Data;
using HPT.Gate.Utils.Common;

namespace HPT.Gate.Host.DevPara
{
    public partial class FrmUploadPhotos : WinBase
    {
        private List<TcpDevice> _DevList = new List<TcpDevice>();
        public FrmUploadPhotos()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FrmUploadPhotos_Load(object sender, EventArgs e)
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
                int index = dgvDevice.Rows.Add();
                device.RowIndex = index;
                dgvDevice.Rows[index].Cells[0].Value = false;
                dgvDevice.Rows[index].Cells[1].Value = device._MachineId;
                dgvDevice.Rows[index].Cells[2].Value = device._DeviceName;
                dgvDevice.Rows[index].Cells[3].Value = device._Mac;
                dgvDevice.Rows[index].Cells[4].Value = 0;
                _DevList.Add(device);
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

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmUploadPhotos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btUploadBGImage.Enabled)
            {
                MessageBox.Show("正在上传人员照片,请稍候!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
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
            FPhotoPickUp photoPickUp = new FPhotoPickUp();
            DialogResult dr = photoPickUp.ShowDialog();
            if (dr != DialogResult.OK) return;
            List<UInt64> empIdList = photoPickUp._TaskList;
            if (empIdList.Count == 0)
            {
                MessageBox.Show("所选人员照片列表为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (TcpDevice dev in devList)
            {
                ShowMsg(string.Format("设备[{0}]开始上传人员照片...", dev._DeviceName));
                int index = 0;
                foreach (UInt64 empId in empIdList)
                {
                    Bitmap bmp = ArrayToBitmap(CollectService.GetEmpPhotoByEmpId(empId));
                    if (bmp == null) continue;
                    ShowMsg(string.Format("开始上传照片[{0}]", empId));
                    SendPhoto(dev, bmp, ioFlag, empId);
                    ShowMsg(string.Format("照片[{0}]上传完毕!", empId));
                    Application.DoEvents();
                    index++;
                    UpdateProgress(dev.RowIndex, index * 100 / empIdList.Count);
                    Thread.Sleep(300);
                    Application.DoEvents();
                }
                ShowMsg(string.Format("设备[{0}]照片上传完毕!", dev._DeviceName));
                Application.DoEvents();
                Thread.Sleep(1000);
            }
        }


        #region 上传背景图片
        private void SendPhoto(TcpDevice dev, Bitmap bmp, int ioFlag, UInt64 imageId)
        {
            string io = ioFlag == 3 ? "入口" : "出口";
            ShowMsg(string.Format("开始上传主板[{0}]{1}背景照片,请稍候...", dev._DeviceName, io));
            Application.DoEvents();
            bmp = ImageHelper.KiResizeImage(bmp, 160, 160);
            List<byte[]> imageBytes = ImageHelper.ImageByteList(ImageHelper.GetByteImage(bmp), 1024);
            int value = 0;
            //UpdateProgress(dev.RowIndex, value);
            for (int index = 0; index < imageBytes.Count; index++)
            {
                DataBmp dbmp = new DataBmp();
                dbmp.Index = new byte[] { (byte)index };
                dbmp.BmpType = new byte[] { (byte)ioFlag };
                dbmp.BmpName = ArrayHelper.IntToBytes(imageId);
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
                //UpdateProgress(dev.RowIndex, value);
                ShowMsg(string.Format("[{0}]上传图片数组[{1}]成功!", dev._DeviceName, index));
                Application.DoEvents();
            }
            ShowMsg(string.Format("[{0}]上传[{1}]背景图片成功!", dev._DeviceName, io));
        }
        #endregion

        #region 上传背景图片
        private void SendImage(TcpDevice dev, Bitmap bmp, int ioFlag, UInt64 imageId)
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
                dbmp.BmpName = ArrayHelper.IntToBytes(imageId);
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

        #region 数组转bitmap
        private Bitmap ArrayToBitmap(byte[] arr)
        {
            if (arr == null) return null;
            return (Bitmap)ImageHelper.GetImageByBytes(arr);
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
                    devList.AddRange(_DevList.Where(e => e._Mac.Equals(mac)));
                    /*
                    foreach (DeviceInfo device in _DevList)
                    {
                        if (mac.Equals(device._Mac))
                        {
                            devList.Add(device);
                            break;
                        }
                    }
                    */
                }
            }
            return devList;
        }
        #endregion
    }
}
