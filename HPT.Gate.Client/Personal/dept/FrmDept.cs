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
        /// ��Ӳ���
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
        /// �޸Ĳ�����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (tvDepts.SelectedNode == tvDepts.Nodes[0])
            {
                MessageBoxHelper.Info("��Ŀ¼�����޸�!");
               return;
            }
            if (tvDepts.SelectedNode == null)
            {
                MessageBoxHelper.Info("��ѡ��Ҫ�޸ĵĲ���!");
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
        /// ɾ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            DeleteDept();
        }

        #region ɾ������
        private void DeleteDept()
        {
            if (tvDepts.SelectedNode == tvDepts.Nodes[0])
            {
                MessageBoxHelper.Info("��Ŀ¼����ɾ��!");
                return;
            }

            if (tvDepts.SelectedNode == null)
            {
                MessageBoxHelper.Info("��ѡ��Ҫɾ���Ĳ���!");
                return;
            }
            if (tvDepts.SelectedNode.Nodes.Count > 0)
            {
                MessageBoxHelper.Info("����ɾ���¼�����!");
                return;
            }

            int deptId = Convert.ToInt32(tvDepts.SelectedNode.Tag);
            var deptName = tvDepts.SelectedNode.Text;
            try
            {
                if (DeptInfoService.HasEmp(deptId))
                {
                    MessageBoxHelper.Info($"����[{deptName}]����Ա����Ϣ������ɾ��Ա����Ϣ���ٲ���!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("���Ҳ����Ƿ����Ա��ʱ��������:" + ex.Message);
                return;
            }
            var dr = MessageBoxHelper.Question($"ȷ��Ҫɾ������[{deptName}]?");
            if (dr == DialogResult.OK)
            {
                try
                {
                    DeptInfoService.Del(deptId,LocalCache.CurrentOper);
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error("ɾ������ʧ��:" + ex.Message);
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
                case "����¼�����":
                    FrmDeptAdd deptAdd = new FrmDeptAdd(deptId);
                    deptAdd.ShowDialog();
                    break;
                case "�޸Ĳ���":
                    if (tvDepts.SelectedNode == tvDepts.Nodes[0])
                    {
                        MessageBoxHelper.Info("��Ŀ¼�����޸�!");
                        return;
                    }

                    int parDeptId = Convert.ToInt32(tvDepts.SelectedNode.Parent.Tag);
                    FrmDeptEdit deptEdit = new FrmDeptEdit(parDeptId, deptId, deptName);
                    deptEdit.ShowDialog();
                    break;
                case "ɾ������":
                    DeleteDept();
                    break;
                case "�޸ĸ��ڵ�����":
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

        #region ˢ�²�����
        private void RefreshDeptTree()
        {
            TreeHelper.DisplayDeptTree(tvDepts, imageList1);
        }
        #endregion

    }
}