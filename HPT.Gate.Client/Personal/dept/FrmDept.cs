using System;
using System.Windows.Forms;
using HPT.Gate.Client.Base;
using HPT.Gate.Client.dept;
using HPT.Gate.Client.Personal.dept;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;

namespace HPT.Gate.Client.Emp
{
    public partial class FrmDept : FrmBase
    {
        public FrmDept()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FDept_Load(object sender, EventArgs e)
        {
            TreeHelper.DisplayDeptTree(tvDepts, imageList1);
            tvDepts.ContextMenuStrip = cmsDept;
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            int parDeptId = Convert.ToInt32(tvDepts.Nodes[0].Tag);
            if (tvDepts.SelectedNode != null)
            {
                parDeptId = Convert.ToInt32(tvDepts.SelectedNode.Tag);
            }
            FrmDeptAdd df = new FrmDeptAdd(parDeptId);
            df.ShowDialog();
            TreeHelper.DisplayDeptTree(tvDepts, imageList1);
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (tvDepts.SelectedNode == tvDepts.Nodes[0])
            {
                MessageBoxHelper.Info("更目录不可修改!");
               return;
            }
            if (tvDepts.SelectedNode == null)
            {
                MessageBoxHelper.Info("请选择要修改的部门!");
                return;
            }
            int parDeptid = Convert.ToInt32(tvDepts.SelectedNode.Parent.Tag);
            int deptId = Convert.ToInt32(tvDepts.SelectedNode.Tag);
            string deptName = tvDepts.SelectedNode.Text;
            FrmDeptEdit df = new FrmDeptEdit(parDeptid, deptId, deptName);
            df.ShowDialog();
            TreeHelper.DisplayDeptTree(tvDepts, imageList1);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            DeleteDept();
        }

        #region 删除部门
        private void DeleteDept()
        {
            if (tvDepts.SelectedNode == tvDepts.Nodes[0])
            {
                MessageBoxHelper.Info("根目录不能删除!");
                return;
            }

            if (tvDepts.SelectedNode == null)
            {
                MessageBoxHelper.Info("请选择要删除的部门!");
                return;
            }
            if (tvDepts.SelectedNode.Nodes.Count > 0)
            {
                MessageBoxHelper.Info("请先删除下级部门!");
                return;
            }

            int deptId = Convert.ToInt32(tvDepts.SelectedNode.Tag);
            var deptName = tvDepts.SelectedNode.Text;
            try
            {
                if (DeptInfoService.HasEmp(deptId))
                {
                    MessageBoxHelper.Info($"部门[{deptName}]还有员工信息，请先删除员工信息后再操作!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("查找部门是否存在员工时发生错误:" + ex.Message);
                return;
            }
            var dr = MessageBoxHelper.Question($"确定要删除部门[{deptName}]?");
            if (dr == DialogResult.OK)
            {
                try
                {
                    DeptInfoService.Del(deptId,LocalCache.CurrentOper);
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error("删除部门失败:" + ex.Message);
                    return;
                }
            }
            else
            {
                return;
            }
            TreeHelper.DisplayDeptTree(tvDepts, imageList1);
        }
        #endregion

        private void cmsDept_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            int deptId = Convert.ToInt32(tvDepts.SelectedNode.Tag);
            string deptName = tvDepts.SelectedNode.Text;
            cmsDept.Close();
            switch (itemText)
            {
                case "添加下级部门":
                    FrmDeptAdd deptAdd = new FrmDeptAdd(deptId);
                    deptAdd.ShowDialog();
                    break;
                case "修改部门":
                    if (tvDepts.SelectedNode == tvDepts.Nodes[0])
                    {
                        MessageBoxHelper.Info("根目录不能修改!");
                        return;
                    }

                    int parDeptId = Convert.ToInt32(tvDepts.SelectedNode.Parent.Tag);
                    FrmDeptEdit deptEdit = new FrmDeptEdit(parDeptId, deptId, deptName);
                    deptEdit.ShowDialog();
                    break;
                case "删除部门":
                    DeleteDept();
                    break;
                case "修改根节点名称":
                    FrmDeptRoot deptRoot = new FrmDeptRoot();
                    deptRoot.ShowDialog();
                    break;
            }
            TreeHelper.DisplayDeptTree(tvDepts, imageList1);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvDepts.SelectedNode = e.Node;
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            FrmDeptRoot deptRoot = new FrmDeptRoot();
            deptRoot.ShowDialog();
            RefreshDeptTree();
        }

        #region 刷新部门树
        private void RefreshDeptTree()
        {
            TreeHelper.DisplayDeptTree(tvDepts, imageList1);
        }
        #endregion

    }
}