using hpt.gate.client.Authorize;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.Host.Base;
using HPT.Joey.Lib.Controls;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Host.face
{
    public partial class FrmAddData : WinBase
    {
        private List<EmpInfo> CurrentEmps;
        private List<FaceDevice> CurrentDevices = new List<FaceDevice>();
        public FrmAddData()
        {
            InitializeComponent();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmChooseEmpHasCard choose = new FrmChooseEmpHasCard();
            DialogResult dr = choose.ShowDialog();
            if (dr != DialogResult.OK) return;
            if (choose._EmpList.Count == 0)
                return;
            CurrentEmps = choose._EmpList;
            JWaitingHelper helper = new JWaitingHelper();
            helper.MessageInfo = "正在加载中,请稍候...";
            helper.BackgroundWork = LoadEmps;
            helper.BackgroundWorkerCompleted += Helper_BackgroundWorkerCompleted;
            helper.Start();

        }

        private void Helper_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {

        }

        private void LoadEmps(Action<string> obj)
        {
            foreach (EmpInfo emp in CurrentEmps)
            {
                this.Invoke(new Action(() =>
                {
                    AddRow(dgvEmps, emp.DeptId, emp.DeptName, emp.EmpId, emp.EmpCode, emp.EmpName);
                }));
            }
        }

        #region 添加行
        private void AddRow(DataGridView dgv, int deptId, string deptName, int empId, string empCode, string empName)
        {
            bool flag = true;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                int _empId = Convert.ToInt32(dgv.Rows[i].Cells[2].Value);
                if (_empId == empId)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                try
                {
                    if (dgv.DataSource != null)
                    {
                        string[] strArray = { deptId.ToString(), deptName, empId.ToString(), empCode, empName };
                        ((DataTable)dgv.DataSource).Rows.Add(strArray);
                    }
                    else
                    {
                        int index = dgv.Rows.Add();
                        dgv.Rows[index].Cells[0].Value = deptId;
                        dgv.Rows[index].Cells[1].Value = deptName;
                        dgv.Rows[index].Cells[2].Value = empId;
                        dgv.Rows[index].Cells[3].Value = empCode;
                        dgv.Rows[index].Cells[4].Value = empName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("往表格添加新行失败:" + ex.Message);
                    return;
                }
            }
        }


        #endregion

        private void FrmAddData_Load(object sender, EventArgs e)
        {
            LoadDevices();
        }

        private void LoadDevices()
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
                            dgvFaceDevice.Rows[rowIndex].Cells[3].Value = device.Mac;
                        }
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载人脸识别机器失败:{ex.Message}", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
        }

        private void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvFaceDevice.Rows)
            {
                row.Cells[0].Value = ckbAll.Checked;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (CurrentEmps.Count == 0)
            {
                MessageBox.Show("没有可添加的人员数据!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CurrentDevices.Clear();
            foreach (DataGridViewRow row in dgvFaceDevice.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    FaceDevice device = new FaceDevice();
                    device.DeviceId = Convert.ToInt32(row.Cells[1].Value);
                    device.IPAddress = row.Cells[2].Value.ToString();
                    device.Mac = row.Cells[3].Value.ToString();
                    CurrentDevices.Add(device);
                }
            }
            JProgressHelper helper = new JProgressHelper();
            helper.MessageInfo = "正在处理中,请稍候...";
            helper.BackgroundWork = AddDataToDevice;
            helper.BackgroundWorkerCompleted += AddDataCompleted;
            helper.Start();
        }

        private void AddDataCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            MessageBoxHelper.Info("处理完毕!");
            DialogResult = DialogResult.OK;
        }

        private void AddDataToDevice(Action<int> ShowProgress, Action<string> ShowMsg)
        {
            List<FaceDevice> devices = new List<FaceDevice>();
            List<EmpInfo> emps = new List<EmpInfo>();
            this.Invoke(new Action(() =>
            {
                devices.AddRange(CurrentDevices);
                emps.AddRange(CurrentEmps);
            }));
            foreach (FaceDevice device in devices)
            {
                int index = 1;
                foreach (EmpInfo emp in emps)
                {
                    ShowMsg($"正在处理[编号:{emp.EmpCode}，姓名:{emp.EmpName}]...");
                    EmpInfoService.AddFaceDataTask(emp, device, 1);
                    ShowProgress(index++ * 100 / CurrentEmps.Count);
                }
            }
        }


        private void buttonItem2_Click(object sender, EventArgs e)
        {
            dgvEmps.Rows.Clear();
        }
    }
}
