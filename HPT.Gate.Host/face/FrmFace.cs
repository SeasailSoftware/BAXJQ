using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Host.face
{
    public partial class FrmFace : WinBase
    {
        public FrmFace()
        {
            InitializeComponent();
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmFace_Load(object sender, EventArgs e)
        {
            LoadFaceDevices();
        }

        private void LoadFaceDevices()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    List<FaceDevice> devList = FaceDeviceService.ToList();
                    this.Invoke(new Action(() =>
                    {
                        dgvFaceDevice.DataSource = null;
                        dgvFaceDevice.Rows.Clear();
                        foreach (FaceDevice device in devList)
                        {
                            int rowIndex = dgvFaceDevice.Rows.Add();
                            dgvFaceDevice.Rows[rowIndex].Cells[0].Value = false;
                            dgvFaceDevice.Rows[rowIndex].Cells[1].Value = device.DeviceId;
                            dgvFaceDevice.Rows[rowIndex].Cells[2].Value = device.IPAddress;
                            dgvFaceDevice.Rows[rowIndex].Cells[3].Value = device.Port;
                            dgvFaceDevice.Rows[rowIndex].Cells[4].Value = device.SN;
                            dgvFaceDevice.Rows[rowIndex].Cells[5].Value = device.Mac;
                            dgvFaceDevice.Rows[rowIndex].Cells[6].Value = device.Password;
                        }
                        dgvFaceDevice.ClearSelection();
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载人脸识别机器失败:{ex.Message}", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
        }

        private void buttonItem25_Click(object sender, EventArgs e)
        {
            FrmFaceAdd faceAdd = new FrmFaceAdd();
            DialogResult dr = faceAdd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                LoadFaceDevices();
            }
        }

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            try
            {
                FaceDevice device = new FaceDevice();
                device.DeviceId = Convert.ToInt32(dgvFaceDevice.SelectedRows[0].Cells[1].Value);
                device.IPAddress = dgvFaceDevice.SelectedRows[0].Cells[2].Value.ToString();
                device.Port = Convert.ToInt32(dgvFaceDevice.SelectedRows[0].Cells[3].Value);
                device.SN = dgvFaceDevice.SelectedRows[0].Cells[4].Value.ToString();
                device.Mac = dgvFaceDevice.SelectedRows[0].Cells[5].Value.ToString();
                device.Password = dgvFaceDevice.SelectedRows[0].Cells[6].Value.ToString();
                FrmFaceEdit edit = new FrmFaceEdit(device);
                DialogResult dr = edit.ShowDialog();
                if (dr == DialogResult.OK)
                    LoadFaceDevices();
            }
            catch
            {

            }
        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            try
            {
                int recId = Convert.ToInt32(dgvFaceDevice.SelectedRows[0].Cells[1].Value);
                string ipAddress = dgvFaceDevice.SelectedRows[0].Cells[2].Value.ToString();
                DialogResult dr = MessageBox.Show($"是否删除机器号为[{ipAddress}]的人脸设备?", "删除询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        FaceDeviceService.Delete(recId);
                        LoadFaceDevices();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.Error($"删除人脸设备失败:{ex.Message}");
                    }
                }
            }
            catch
            {

            }
        }

        private void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvFaceDevice.Rows)
            {
                row.Cells[0].Value = ckbAll.Checked;
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            List<FaceDevice> devList = GetFaceDevicesFromChecked();
            if (devList.Count == 0)
            {
                MessageBoxHelper.Info("请选择需要初始化的设备!");
                return;
            }
            foreach (FaceDevice device in devList)
            {
                try
                {
                    FaceDataTaskService.DeleteTask(device.DeviceId);
                    EmpInfoService.AddFaceDataTask(null, device,5);
                    ShowMsg($"人脸设备[{device.IPAddress}]发送初始化指令成功!");

                }
                catch (Exception ex)
                {
                    ShowMsg($"人脸识别设备[{device.IPAddress}]发送初始化指令失败:{ex.Message}");
                }
            }
        }

        #region 消息展示
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
                if (txtLog.Lines.Length >= 1000)
                    txtLog.Clear();
                txtLog.AppendText($"{Environment.NewLine}{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msg}");
            }
        }
        #endregion

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            FrmAddData addData = new FrmAddData();
            addData.ShowDialog();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            List<FaceDevice> devList = GetFaceDevicesFromChecked();
            if (devList.Count == 0)
            {
                MessageBoxHelper.Info("请选择需要初始化的设备!");
                return;
            }
            foreach (FaceDevice device in devList)
            {

                try
                {
                    EmpInfoService.AddFaceDataTask(null, device,4);
                    ShowMsg($"已给人脸设备[{device.IPAddress}]发送设置时间指令!");
                }
                catch (Exception ex)
                {
                    ShowMsg($"人脸识别设备[{device.IPAddress}]添加设置时间任务失败:{ex.Message}");
                }
            }
        }

        #region 获取选中设备
        private List<FaceDevice> GetFaceDevicesFromChecked()
        {
            List<FaceDevice> devices = new List<FaceDevice>();
            try
            {
                foreach (DataGridViewRow row in dgvFaceDevice.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue)
                    {
                        FaceDevice device = new FaceDevice();
                        device.DeviceId = Convert.ToInt32(row.Cells[1].Value);
                        device.IPAddress = row.Cells[2].Value.ToString();
                        device.Port = Convert.ToInt32(row.Cells[3].Value);
                        device.SN = row.Cells[4].Value.ToString();
                        device.Mac = row.Cells[5].Value.ToString();
                        device.Password = row.Cells[6].Value.ToString();
                        devices.Add(device);
                    }
                }
            }
            catch
            {

            }
            return devices;
        }
        #endregion

        private void buttonItem4_Click(object sender, EventArgs e)
        {

        }

        private void FrmFace_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void buttonItem4_Click_1(object sender, EventArgs e)
        {
            FrmFaceDetail faceDetail = new FrmFaceDetail();
            DialogResult dr = faceDetail.ShowDialog();
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            FrmFaceBindings bindings = new FrmFaceBindings();
            DialogResult dr = bindings.ShowDialog();
        }
    }
}
