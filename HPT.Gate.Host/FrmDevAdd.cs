using hpt.gate.Util;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Device;
using HPT.Gate.Device.Data;
using HPT.Gate.Device.Dev;
using HPT.Gate.Host.Base;
using Joey.UserControls;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Host
{
    public partial class FrmDevAdd : WinBase
    {
        private DataNetPara _CurPara = null;
        #region Var
        /// <summary>
        /// 用于检测在线设备的定时器
        /// </summary>
        public System.Timers.Timer timer = null;
        /// <summary>
        /// 设备编号
        /// </summary>
        public int _DevId;

        #endregion

        /// <summary>
        /// 新增构造函数
        /// </summary>
        /// <param name="placeId"></param>
        /// <param name="PlaceName"></param>
        public FrmDevAdd()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            try
            {
                _DevId = DeviceInfoService.GetAvarilableDevId();
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取可用设备编号失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }



        /// <summary>
        /// 增加控制器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 搜索控制器自动添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {

        }



        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDevice(e);
        }

        /// <summary>
        /// 校验IP是否正确
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool IsIPAddress(string ip)
        {
            if (string.IsNullOrEmpty(ip) || ip.Length < 7 || ip.Length > 15)
            {
                return false;
            }
            string regformat = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(ip);
        }

        /// <summary>
        /// 判断mac格式是否正确
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public bool IsMac(string mac)
        {
            return Regex.IsMatch(mac, @"^([0-9a-fA-F]{2})(([/\s:-][0-9a-fA-F]{2}){5})$");
        }

        private void DeviceForm_Load(object sender, EventArgs e)
        {
            cbbDevType.SelectedIndex = 0;
            cbbPlace.SelectedIndex = 0;
            LoadOnlineDevices();
        }
        public void LoadOnlineDevices()
        {
            Task.Factory.StartNew(() =>
            {
                this.Invoke(new Action(() =>
                {
                    dgvOnline.Rows.Clear();
                    foreach (TcpSocketState state in TcpServer.Instance._StateList)
                    {
                        int rowIndex = dgvOnline.Rows.Add();
                        dgvOnline.Rows[rowIndex].Cells[0].Value = state.IPAddress;
                        dgvOnline.Rows[rowIndex].Cells[1].Value = state.Port;
                        dgvOnline.Rows[rowIndex].Cells[2].Value = state.Mac;
                    }
                }));
            });

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            AddDevice();
        }

        #region 添加设备
        private void AddDevice()
        {
            if (_CurPara == null)
            {
                MessageBox.Show("请先获取设备信息", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DeviceInfo device = new DeviceInfo();
            device.PlaceId = Convert.ToInt32(cbbPlace.SelectedIndex);
            device.DeviceType = Convert.ToInt32(cbbDevType.SelectedIndex);
            device.DeviceId = (int)numDeviceId.Value;
            device.DeviceName = tbDeviceName.Text.Trim();
            device.ServerIP = tbServerIPAddress.Text;
            device.ServerPort = (int)numServerPort.Value;
            device.IPAddress = tbIPAddress.Text.Trim();
            device.SubNet = tbSubnet.Text.Trim();
            device.GateWay = tbGateway.Text.Trim();
            device.Mac = tbMac.Text.Trim();
            device.Port = this._DevId + 9293;
            device.HardVersion = tbHardVersion.Text;
            device.SoftVersion = tbSoftVersion.Text;
            device.ServerIP = tbServerIPAddress.Text;
            device.ServerPort = (int)numServerPort.Value;
            ///设备名称校验
            if (device.DeviceName.Equals(string.Empty))
            {
                MessageBox.Show("设备名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (device.Mac.Equals(string.Empty))
            {
                MessageBox.Show("物理地址不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ///IP地址校验
            if (!IsIPAddress(device.IPAddress))
            {
                MessageBox.Show("IP地址格式有误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ///子网掩码校验
            if (!IsIPAddress(device.SubNet))
            {
                MessageBox.Show("子网掩码格式有误", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ///网关校验
            if (!IsIPAddress(device.GateWay))
            {
                MessageBox.Show("网关地址格式有误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ///--------------------新增------------
            //先判断是否存在

            string msg;
            if (DeviceInfoService.Insert(device, out msg))
                DialogResult = DialogResult.OK;
            else
            {
                MessageBoxHelper.Error($"添加闸机设备失败:{msg}");
            }
        }
        #endregion


        private void dataGridViewX1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDevice(e);
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectDevice(e);
        }

        #region 选择设备
        private void SelectDevice(DataGridViewCellEventArgs e)
        {

        }
        #endregion

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            string mac = tbMac.Text;
            TcpDevice device = new TcpDevice(mac);
            DataNetPara netPara = device.GetNetPara();
            if (netPara == null)
            {
                MessageBox.Show("获取设备Mac失败!请检查Mac地址是否正确。");
                return;
            }
            _CurPara = netPara;
            numDeviceId.Value = netPara.IMachineId;
            tbMac.Text = netPara.SMAC;
            tbDeviceName.Text = netPara.SDevName;
            tbIPAddress.Text = netPara.SIPAddress;
            tbSubnet.Text = netPara.SSubNet;
            tbGateway.Text = netPara.SGateWay;
            numPort.Value = netPara.IPort;
            tbHardVersion.Text = netPara.SHardVersion;
            tbSoftVersion.Text = netPara.SSoftVersion;
            tbServerIPAddress.Text = netPara.SServerAddress;
            numServerPort.Value = netPara.IServerPort;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            LoadOnlineDevices();
        }

        private void dgvOnline_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (dgvOnline.Rows.Count == 0) return;
            string mac = dgvOnline.Rows[e.RowIndex].Cells[2].Value.ToString();
            if (string.IsNullOrEmpty(mac))
            {
                MessageBox.Show("无法获取Mac,请检查控制板程序以及功能是否匹配!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Task.Factory.StartNew(() =>
            {
                TcpDevice device = new TcpDevice(mac);
                DataNetPara netPara = device.GetNetPara();
                if (netPara == null) return;
                this.Invoke(new Action(() =>
                {
                    _CurPara = netPara;
                    numDeviceId.Value = netPara.IMachineId;
                    tbMac.Text = netPara.SMAC;
                    tbDeviceName.Text = netPara.SDevName;
                    tbIPAddress.Value = netPara.SIPAddress;
                    tbSubnet.Value = netPara.SSubNet;
                    tbGateway.Value = netPara.SGateWay;
                    numPort.Value = netPara.IPort;
                    tbHardVersion.Text = netPara.SHardVersion;
                    tbSoftVersion.Text = netPara.SSoftVersion;
                    tbServerIPAddress.Value = netPara.SServerAddress;
                    numServerPort.Value = netPara.IServerPort;
                }));
            });
        }
    }
}
