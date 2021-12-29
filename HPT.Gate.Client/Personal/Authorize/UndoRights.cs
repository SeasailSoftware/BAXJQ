using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Entity.Service;
using Joey.UserControls;

namespace HPT.Gate.Client.device
{
    public partial class UndoRights : FrmBase
    {
        public object obj = null;
        public TreeNodeMouseClickEventArgs nodeEvenArgs = null;
        public UndoRights()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void UndoRights_Load(object sender, EventArgs e)
        {
            try
            {
                TreeHelper.DisplayDeviceTree(treeView1, imageList1);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载设备树形结构失败:" + ex.Message);
                return;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            try
            {
                int deviceId = Convert.ToInt32(e.Node.Tag);
                if (deviceId >= 0)
                {
                    obj = sender;
                    nodeEvenArgs = e;
                    DataTable dt = DevRightOfEmpService.GetRightsOfDevice(deviceId);
                    dgvRight.DataSource = dt;
                    foreach (DataGridViewRow row in dgvRight.Rows)
                    {
                        row.Cells[0].Value = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载设备人员权限列表失败:" + ex.Message);
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvRight.Rows)
            {
                row.Cells[0].Value = checkBox1.Checked;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveDeviceRights();
        }

        #region 保存设备权限
        private void SaveDeviceRights()
        {

            List<DevRightOfEmp> rightList = new List<DevRightOfEmp>();
            foreach (DataGridViewRow row in dgvRight.Rows)
            {
                DevRightOfEmp right = new DevRightOfEmp();
                right.DeviceId = Convert.ToInt32(row.Cells[1].Value);
                right.EmpId = Convert.ToInt32(row.Cells[5].Value);
                if ((bool)row.Cells[0].EditedFormattedValue == true)
                    right.Right = 1;
                else
                    right.Right = 0;
                rightList.Add(right);
            }
            if (rightList.Count == 0)
            {
                MessageBoxHelper.Info("没有可操作的权限!");
                return;
            }
            buttonX1.Enabled = false;
            try
            {
                DevRightOfEmpService.UndoRights(rightList);
                buttonX1.Enabled = true;
                if (obj != null)
                {
                    treeView1_NodeMouseClick(obj, nodeEvenArgs);
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("设备撤权失败:" + ex.Message);
                buttonX1.Enabled = true;
                return;
            }
        }

        #endregion

    }
}