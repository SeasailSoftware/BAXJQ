using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Joey.UserControls;

namespace HPT.Gate.Client.oper
{
    public partial class FrmOperRights : JForm
    {
        private int CurrentOperId;
        public FrmOperRights(int operId)
        {
            InitializeComponent();
            CurrentOperId = operId;
            LoadMenusTree();
            LoadDeptTree();
        }

        private void MenusTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            TreeHelper.CheckChildren(node);
        }

        private void FrmOperRights_Load(object sender, EventArgs e)
        {
            LoadOperInfo();
            LoadOperMenuRights();
            LoadOperDeptRights();
        }

        #region 加载操作员部门权限
        private void LoadOperDeptRights()
        {
            try
            {
                List<DeptInfo> deptList = DeptInfoService.GetByOperId(CurrentOperId);
                foreach (DeptInfo dept in deptList)
                {
                    CheckTreeNodeIsChecked(tvDept, tvDept.Nodes[0], dept.DeptId);
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载操作员菜单权限:" + ex.Message);
                return;
            }
        }
        #endregion


        #region 加载操作员菜单权限
        private void LoadOperMenuRights()
        {
            try
            {
                List<Menus> menusList = MenusService.GetMenusByOperId(CurrentOperId);
                int count = menusList.Count;
                if (count > 0)
                {

                    foreach (Menus menu in menusList)
                    {
                        CheckTreeNodeIsChecked(menuTess, menuTess.Nodes[0], menu.MenuId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载操作员菜单权限:" + ex.Message);
                return;
            }
        }
        #endregion

        #region 选中菜单权限
        public void CheckTreeNodeIsChecked(TreeView tv, TreeNode node, int temp)
        {
            if (Convert.ToInt32(node.Tag) == temp)
            {
                node.Checked = true;
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode treeNode in node.Nodes)
                    {
                        treeNode.Checked = false;
                    }
                }
            }
            else
            {
                foreach (TreeNode treeNode in node.Nodes)
                {
                    CheckTreeNodeIsChecked(tv, treeNode, temp);
                }
            }
        }
        #endregion



        #region 加载操作员信息
        private void LoadOperInfo()
        {
            try
            {
                OperInfo oper = OperInfoService.GetById(CurrentOperId);
                if (oper != null)
                {
                    tbOperName.Text = oper.OperName;
                    tbRemark.Text = oper.Descr;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载用户信息失败:{ex.Message}");
            }
        }
        #endregion

        #region 加载部门树
        private void LoadDeptTree()
        {
            try
            {
                TreeHelper.DisplayDeptTree(tvDept, imageList1);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载部门树形结构失败:" + ex.Message);
                return;
            }
        }
        #endregion

        #region 加载菜单树
        private void LoadMenusTree()
        {
            try
            {
                TreeHelper.DisplayMenusTree(menuTess, imageList1);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载菜单树形结构失败：" + ex.Message);
                return;
            }
        }
        #endregion

        private void tvDept_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            TreeHelper.CheckChildren(node);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }


        #region 保存操作员权限
        private void SaveOperRights()
        {
            #region 保存用户菜单权限
            List<int> menuList = new List<int>();
            menuList.Add(0);
            GetMenuList(menuTess.Nodes[0], menuList);
            try
            {
                MenuOfOperService.Del(CurrentOperId);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("删除操作员所有权限失败:" + ex.Message);
                return;
            }
            foreach (int menuId in menuList)
            {
                try
                {
                    MenuOfOper menuOfOper = new MenuOfOper()
                    {
                        OperId = CurrentOperId,
                        MenuId = menuId
                    };
                    MenuOfOperService.Insert(menuOfOper);
                }
                catch (Exception ee)
                {
                    MessageBoxHelper.Error("添加人员菜单权限失败:" + ee.Message);
                    return;
                }
            }
            #endregion

            #region 保存用户部门权限
            List<int> deptList = new List<int>();
            deptList.Add(1);
            GetMenuList(tvDept.Nodes[0], deptList);
            try
            {
                DeptOfOperService.Del(CurrentOperId);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("删除用户部门权限失败:" + ex.Message);
                return;
            }
            foreach (int deptId in deptList)
            {
                try
                {
                    DeptOfOper deptOfOper = new DeptOfOper();
                    deptOfOper.OperId = CurrentOperId;
                    deptOfOper.DeptId = deptId;
                    DeptOfOperService.Insert(deptOfOper);
                }
                catch (Exception ee)
                {
                    MessageBoxHelper.Error("添加用户部门权限失败:" + ee.Message);
                    return;
                }
            }
            #endregion


            DialogResult = DialogResult.OK;
        }

        #endregion

        #region 获取选中的菜单列表
        private void GetMenuList(TreeNode treeNode, List<int> list)
        {
            if (treeNode.Checked)
            {
                list.Add(Convert.ToInt32(treeNode.Tag));
            }
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Checked)
                {
                    int temp = Convert.ToInt32(node.Tag);
                    if (node.Parent.Checked == false)
                    {
                        int tag = Convert.ToInt32(node.Parent.Tag);
                        list.Remove(tag);
                        list.Add(tag);
                    }
                    list.Add(temp);
                }
                GetMenuList(node, list);
            }
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveOperRights();
        }
    }
}
