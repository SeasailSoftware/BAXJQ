using hpt.gate.client.Authorize;
using HPT.Gate.Client.log;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Joey.Lib.Controls;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HPT.Gate.Client.device
{
    public partial class FEmpAuthorize : JForm
    {
        private List<EmpInfo> CurrentEmps = new List<EmpInfo>();
        public FEmpAuthorize()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }




        private void FWithdrawRights_Load(object sender, EventArgs e)
        {
            try
            {
                TreeHelper.DisplayDeviceTree(devTree, imageList1);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载安装区域与设备树形结构失败:" + ex.Message);
                return;
            }
        }


        private void comboBoxItem1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 展示员工权限
        /// </summary>
        /// <param name="dt"></param>
        private void DisplayEmpRights(List<DeviceInfo> devList, TreeNode treeNode)
        {
            if (treeNode.Nodes.Count == 0)
            {
                int deviceId = Convert.ToInt32(treeNode.Tag);
                if (devList.Exists(p => p.DeviceId == deviceId))
                    treeNode.Checked = true;
            }
            else
            {
                foreach (TreeNode tNode in treeNode.Nodes)
                {
                    DisplayEmpRights(devList, tNode);
                }
            }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex == -1)
            {
                return;
            }
            foreach (DataGridViewRow row in dgvEmps.Rows)
            {
                row.Cells[0].Value = false;
            }
            dgvEmps.Rows[rowIndex].Cells[0].Value = true;
            try
            {
                int empId = Convert.ToInt32(dgvEmps.Rows[rowIndex].Cells[2].Value);
                List<DeviceInfo> devList = DevRightOfEmpService.GetDeviceByEmpid(empId);
                ///先清除treeView2的显示
                TreeHelper.ClearNodeChecked(devTree, devTree.Nodes[0]);
                DisplayEmpRights(devList, devTree.Nodes[0]);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"读取人员[{dgvEmps.Rows[rowIndex].Cells[3].Value}]权限列表失败:" + ex.Message);
                return;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            int empId = 0;
            try
            {
                empId = Convert.ToInt32(dgvEmps.SelectedRows[0].Cells[2].Value);
            }
            catch
            {

            }
            if (empId == 0)
            {
                MessageBoxHelper.Info("请选择需要设置门禁权限的人员!");
                return;
            }

            try
            {
                DevRightOfEmpService.ClearAllRightsOfEmpId(empId);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"清除人员权限失败:{ex.Message}");
                return;
            }
            try
            {
                List<int> devList = new List<int>();
                TreeHelper.GetAllDeviceIdList(devTree.Nodes[0], devList);
                for (int i = 0; i < devList.Count; i++)
                {
                    int devId = devList[i];
                    try
                    {
                        DevRightOfEmpService.Insert(empId, devId);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.Error($"添加人员权限失败,EmpId={empId}:{ex.Message}");
                        return;
                    }
                    Logs.InsertOperLog(LocalCache.CurrentOper.OperName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "门禁权限", "授权", "修改人员【" + empId + "】对应控制器【" + devId + "】的权限", 1);
                }
            }
            catch
            {

            }

            MessageBoxHelper.Info("人员权限保存成功!请及时同步到设备!");
        }



        /// <summary>
        /// 获取勾选的设备
        /// </summary>
        /// <param name="node"></param>
        private void AddDeviceList(TreeNode node, List<DeviceInfo> list)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    var deviceId = Convert.ToInt32(n.Tag);
                    var deviceName = n.Text;
                    if (deviceId >= 100)
                    {
                        if (n.Checked)
                        {
                            ///获取门信息
                            if (n.Nodes.Count > 0)
                            {
                                DeviceInfo device = new DeviceInfo();
                                device.DeviceId = deviceId;
                                device.DeviceName = deviceName;
                                foreach (TreeNode doorNode in n.Nodes)
                                {

                                }
                                list.Add(device);
                            }

                        }
                        else
                        {
                            if (n.Nodes.Count > 0)
                            {
                                ///检查门是否选中
                                bool flag = false;
                                foreach (TreeNode dNode in n.Nodes)
                                {
                                    if (dNode.Checked)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                ///如果有门选中
                                if (flag)
                                {
                                    DeviceInfo device = new DeviceInfo();
                                    device.DeviceId = deviceId;
                                    device.DeviceName = deviceName;
                                    foreach (TreeNode doorNode in n.Nodes)
                                    {

                                    }
                                    list.Add(device);
                                }
                            }
                        }
                    }
                    AddDeviceList(n, list);
                }
            }
        }

        /// <summary>
        /// 获取勾选的设备
        /// </summary>
        /// <param name="node"></param>
        private void GetEmpRightsList(TreeNode node, List<DevRightOfEmp> list)
        {
            if (node.Nodes.Count == 0)
            {
                var deviceId = Convert.ToInt32(node.Tag);
                if (deviceId >= 1)
                {
                    DevRightOfEmp right = new DevRightOfEmp();
                    right.DeviceId = deviceId;
                    if (node.Checked)
                    {
                        right.Right = 1;
                    }
                    else
                    {
                        right.Right = 0;
                    }
                    list.Add(right);
                }
            }
            else
            {
                foreach (TreeNode n in node.Nodes)
                {
                    GetEmpRightsList(n, list);
                }
            }

        }

        private void treeView2_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeHelper.CheckChildren(e.Node);
        }

        private void checkBoxItem1_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            ChooseEmps();
        }

        #region 选择人员
        private void ChooseEmps()
        {
            FrmChooseEmpHasCard choose = new FrmChooseEmpHasCard();
            DialogResult dr = choose.ShowDialog();
            if (dr != DialogResult.OK) return;
            if (choose._EmpList.Count == 0) return;
            CurrentEmps.Clear();
            CurrentEmps.AddRange(choose._EmpList);
            JWaitingHelper helper = new JWaitingHelper();
            helper.MessageInfo = "正在加载中,请稍候...";
            helper.BackgroundWork = LoadEmps;
            helper.BackgroundWorkerCompleted += Helper_BackgroundWorkerCompleted;
            helper.Start();
        }

        private void LoadEmps(Action<string> obj)
        {
            this.Invoke(new Action(() => { dgvEmps.Rows.Clear(); }));
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

        private void Helper_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {

        }
        #endregion

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            TreeHelper.ClearDataGridView(dgvEmps);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int empId = 0;
            try
            {
                empId = Convert.ToInt32(dgvEmps.SelectedRows[0].Cells[2].Value);
            }
            catch
            {

            }
            if (empId == 0)
            {
                MessageBoxHelper.Info("请选择需要设置门禁权限的人员!");
                return;
            }

            try
            {
                DevRightOfEmpService.ClearAllRightsOfEmpId(empId);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"清除人员权限失败:{ex.Message}");
                return;
            }
            try
            {
                List<int> devList = new List<int>();
                TreeHelper.GetAllDeviceIdList(devTree.Nodes[0], devList);
                for (int i = 0; i < devList.Count; i++)
                {
                    int devId = devList[i];
                    try
                    {
                        DevRightOfEmpService.Insert(empId, devId);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.Error($"添加人员权限失败,EmpId={empId}:{ex.Message}");
                        return;
                    }
                    Logs.InsertOperLog(LocalCache.CurrentOper.OperName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "门禁权限", "授权", "修改人员【" + empId + "】对应控制器【" + devId + "】的权限", 1);
                }
            }
            catch
            {

            }

            MessageBoxHelper.Info("人员权限保存成功!请及时同步到设备!");
        }
    }
}
