using HPT.Gate.Client.Authorize;
using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Utils.Common;
using HPT.Joey.Lib.Controls;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HPT.Gate.Client.device
{
    public partial class FrmRights : FrmBase
    {
        private TreeNode _CurrentNode = null;
        public FrmRights()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FRights_Load(object sender, EventArgs e)
        {
            ///显示设备树
            try
            {
                TreeHelper.DisplayDeviceTree(DeviceTree, imageList1);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载安装区域与设备失败:" + ex.Message);
                return;
            }

        }


        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmDevAuthorize devAuthorize = new FrmDevAuthorize();
            DialogResult dr = devAuthorize.ShowDialog();
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            FEmpAuthorize withdrawRights = new FEmpAuthorize();
            withdrawRights.ShowDialog();
            ShowRightsOfDevice();
        }

        private void DeviceTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _CurrentNode = e.Node;
            ShowRightsOfDevice();
        }

        private void ShowRightsOfDevice()
        {
            JProgressHelper process = new JProgressHelper();
            process.ShowProgress = false;
            process.MessageInfo = "正在处理中,请稍后...";
            process.BackgroundWork = ShowEmpsOfDevice;
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }

        private void process_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {

        }

        private void ShowEmpsOfDevice(Action<int> arg1, Action<string> arg2)
        {
            ckbAll.Checked = false;
            int deviceId = 0;
            try
            {
                deviceId = Convert.ToInt32(_CurrentNode.Tag);
            }
            catch
            {
                return;
            }
            try
            {

                this.Invoke(new Action(() =>
                {
                    dgvRightOfDoor.DataSource = null;
                    dgvRightOfDoor.Rows.Clear();
                }));
                List<EmpInfo> empList = EmpInfoService.GetByDeviceId(deviceId);
                this.Invoke(new Action(() =>
                {
                    foreach (EmpInfo emp in empList)
                    {
                        int rowIndex = dgvRightOfDoor.Rows.Add();
                        dgvRightOfDoor.Rows[rowIndex].Cells[0].Value = false;
                        dgvRightOfDoor.Rows[rowIndex].Cells[1].Value = emp.DeptId;
                        dgvRightOfDoor.Rows[rowIndex].Cells[2].Value = emp.DeptName;
                        dgvRightOfDoor.Rows[rowIndex].Cells[3].Value = emp.EmpId;
                        dgvRightOfDoor.Rows[rowIndex].Cells[4].Value = emp.EmpCode;
                        dgvRightOfDoor.Rows[rowIndex].Cells[5].Value = emp.EmpName;
                        dgvRightOfDoor.Rows[rowIndex].Cells[6].Value = "删除权限";
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载设备人员权限列表失败:" + ex.Message);
                return;
            }
        }

        private void LoadEmpsCompleted_Event(object sender, BackgroundWorkerEventArgs e)
        {

        }

        private void dgvRightOfDoor_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
            dgvRightOfDoor.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvRightOfDoor.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvRightOfDoor.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private List<DevRightOfEmp> _Rights = new List<DevRightOfEmp>();
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            JWaitingHelper helper = new JWaitingHelper();
            helper.MessageInfo = "正在删除,请稍候...";
            helper.BackgroundWork = DeleteRights;
            helper.BackgroundWorkerCompleted += Helper_BackgroundWorkerCompleted;
            helper.Start();
        }

        private void DeleteRights(Action<string> ShowMsg)
        {
            int count = dgvRightOfDoor.Rows.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if ((bool)dgvRightOfDoor.Rows[i].Cells[0].EditedFormattedValue)
                {
                    int empId = Convert.ToInt32(dgvRightOfDoor.Rows[i].Cells[3].Value);
                    int deviceId = Convert.ToInt32(_CurrentNode.Tag);
                    DevRightOfEmp right = new DevRightOfEmp()
                    {
                        EmpId = empId,
                        DeviceId = deviceId,
                        Right = 0
                    };
                    try
                    {
                        string empCode = dgvRightOfDoor.Rows[i].Cells[3].Value.ToString();
                        string empName = dgvRightOfDoor.Rows[i].Cells[4].Value.ToString();
                        ShowMsg($"正在删除[{empCode},{empName}]...");
                        List<DevRightOfEmp> list = new List<DevRightOfEmp>();
                        list.Add(right);
                        DevRightOfEmpService.UndoRights(list);
                        this.Invoke(new Action(() =>
                        {
                            dgvRightOfDoor.Rows.RemoveAt(i);
                        }));

                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.Error($"撤权失败:{ex.Message}");
                    }
                }
            }

        }

        private void Helper_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            MessageBoxHelper.Info("删除成功!");
        }

        private void dgvRightOfDoor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dgvRightOfDoor.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    int empId = Convert.ToInt32(dgvRightOfDoor.Rows[e.RowIndex].Cells[3].Value);
                    int deviceId = Convert.ToInt32(DeviceTree.SelectedNode.Tag);
                    try
                    {
                        List<DevRightOfEmp> rightList = new List<DevRightOfEmp>();
                        DevRightOfEmp right = new DevRightOfEmp()
                        {
                            EmpId = empId,
                            DeviceId = deviceId,
                            Right = 0
                        };
                        rightList.Add(right);
                        DevRightOfEmpService.UndoRights(rightList);
                        ShowRightsOfDevice();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.Error($"撤权失败:{ex.Message}");
                    }
                }
            }

        }

        private void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvRightOfDoor.Rows)
            {
                row.Cells[0].Value = ckbAll.Checked;
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (dgvRightOfDoor.Rows.Count == 0)
            {
                MessageBoxHelper.Info("没有可导入的数据!");
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            string folderName = fbd.SelectedPath; //获得选择的文件夹路径
            string file = $@"{folderName}\人员权限报表({DateTime.Now.ToString("yyyy-MM-dd")}).xlsx";
            Cursor = Cursors.WaitCursor;
            ExcelHelper.Export(file, dgvRightOfDoor);
            Cursor = Cursors.Default;
        }
    }
}
