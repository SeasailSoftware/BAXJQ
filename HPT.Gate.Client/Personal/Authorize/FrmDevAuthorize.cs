using hpt.gate.client.Authorize;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Joey.Lib.Controls;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace HPT.Gate.Client.Authorize
{
    public partial class FrmDevAuthorize : JForm
    {
        private List<EmpInfo> CurrentEmps = new List<EmpInfo>();
        public List<int> CurrentDevList = new List<int>();
        public FrmDevAuthorize()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmChooseEmpHasCard choose = new FrmChooseEmpHasCard();
            DialogResult dr = choose.ShowDialog();
            if (dr != DialogResult.OK) return;
            if (choose._EmpList.Count == 0)
                return;
            CurrentEmps.Clear();
            CurrentEmps.AddRange(choose._EmpList);
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
            try
            {
                this.Invoke(new Action(() => { dgvEmps.Rows.Clear(); }));
                foreach (EmpInfo emp in CurrentEmps)
                {
                    this.Invoke(new Action(() =>
                    {
                        AddRow(dgvEmps, emp.DeptId, emp.DeptName, emp.EmpId, emp.EmpCode, emp.EmpName);
                    }));
                }
                Thread.Sleep(300);
            }
            catch
            {
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
                    MessageBoxHelper.Error("往表格添加新行失败:" + ex.Message);
                    return;
                }
            }
        }


        #endregion

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            TreeHelper.ClearDataGridView(dgvEmps);
        }

        private void FrmDevAuthorize_Load(object sender, EventArgs e)
        {
            LoadDeviceTree();
        }

        #region 加载设备
        private void LoadDeviceTree()
        {
            TreeHelper.DisplayDeviceTree(PlaceTree, imageList1);
        }
        #endregion

        private void btConform_Click(object sender, EventArgs e)
        {

        }

        #region 设备授权
        private void DeviceAuthorize()
        {

        }
        #endregion

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeHelper.CheckChildren(e.Node);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (CurrentEmps.Count == 0)
            {
                MessageBoxHelper.Info("没有可授权的人员!");
                return;
            }

            CurrentDevList.Clear();
            List<int> devIds = new List<int>();
            TreeHelper.GetCheckedValueList(PlaceTree.Nodes[0], devIds);
            if (devIds.Count == 0)
            {
                MessageBoxHelper.Info("请选择需要授权的设备!");
                return;
            }
            CurrentDevList.AddRange(devIds);
            JProgressHelper helper = new JProgressHelper();
            helper.MessageInfo = "正在处理中,请稍候...";
            helper.BackgroundWork = DoRights;
            helper.BackgroundWorkerCompleted += FinishDoRights;
            helper.Start();
        }

        private void DoRights(Action<int> progress, Action<string> message)
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            List<int> devIds = new List<int>();
            this.Invoke(new Action(() =>
            {
                emps.AddRange(CurrentEmps);
                devIds.AddRange(CurrentDevList);
            }));
            try
            {
                int devCount = devIds.Count;
                int empCount = emps.Count;
                for (int i = 0; i < devCount; i++)
                {
                    int deviceId = devIds[i];
                    for (int k = 0; k < empCount; k++)
                    {
                        EmpInfo emp = emps[k];
                        message($"正在处理闸机[{deviceId}]权限,[{emp.EmpCode},{emp.EmpName}]");
                        int empId = emp.EmpId;
                        try
                        {
                            DevRightOfEmpService.Insert(empId, deviceId);
                        }
                        catch
                        {
                        }
                        progress(((i + 1) * (k + 1)) * 100 / (empCount * devCount));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Info($"批量授权失败:{ex.Message}");
            }
        }

        private void FinishDoRights(object sender, BackgroundWorkerEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
