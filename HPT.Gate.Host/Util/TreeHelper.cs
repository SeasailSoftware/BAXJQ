using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.DataAccess.Entity.Service;

namespace hpt.gate.Util
{
    public class TreeHelper
    {
        /// <summary>
        /// 取消节点一下所有子节点的选中状态
        /// </summary>
        /// <param name="treeNode"></param>
        public static void ChangeCheckedStatus(TreeNode treeNode, bool status)
        {
            treeNode.Checked = status;
            if (treeNode.Nodes.Count > 0)
            {
                foreach (TreeNode node in treeNode.Nodes)
                {
                    node.Checked = status;
                    ChangeCheckedStatus(node, status);
                }
            }
        }

        /// <summary>
        /// 检查子节点下面所有节点的值是否与传入值相等
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="tag"></param>
        public static void CheckTreeNode(TreeNode treeNode, int tag)
        {
            if (Convert.ToInt32(treeNode.Tag) == tag)
            {
                treeNode.Checked = true;
                return;
            }
            else
            {
                if (treeNode.Nodes.Count == 0)
                {
                    return;
                }
                else
                {
                    foreach (TreeNode node in treeNode.Nodes)
                    {
                        CheckTreeNode(node, tag);
                    }
                }
            }

        }

        /// <summary>
        /// 取消所有树节点的选中状态
        /// </summary>
        /// <param name="MenusTree"></param>
        /// <param name="treeNode"></param>
        public static void ClearNodeChecked(TreeView MenusTree, TreeNode treeNode)
        {
            treeNode.Checked = false;
            if (treeNode.Nodes.Count > 0)
            {
                foreach (TreeNode node in treeNode.Nodes)
                {
                    node.Checked = false;
                    ClearNodeChecked(MenusTree, node);
                }
            }
        }

        /// <summary>
        /// 实现点击父节点，选中全部子节点
        /// </summary>
        /// <param name="node"></param>
        public static void CheckChildren(TreeNode node)
        {
            bool check = node.Checked;
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    n.Checked = check;
                    CheckChildren(n);
                }
            }
        }


        /// <summary>
        /// 初始化显示树
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="pNode"></param>
        /// <param name="tv"></param>
        /// <param name="ds"></param>
        public static void Init_Tree(int ParentID, TreeNode pNode, TreeView tv, DataTable dt)
        {
            DataView dvTree = new DataView(dt);
            dvTree.RowFilter = "[PARID] = " + ParentID;

            foreach (DataRowView Row in dvTree)
            {
                var Node = new TreeNode();
                if (pNode == null)
                {
                    Node.Text = Row["Name"].ToString();
                    Node.Tag = Row["ID"].ToString();
                    Node.ImageIndex = Convert.ToInt32(Row["ImageIndex"].ToString());
                    Node.SelectedImageIndex = Convert.ToInt32(Row["ImageIndex"].ToString());
                    tv.Nodes.Add(Node);
                    Init_Tree(Int32.Parse(Row["ID"].ToString()), Node, tv, dt);
                }
                else
                {
                    Node.Text = Row["Name"].ToString();
                    Node.Tag = Row["ID"].ToString();
                    pNode.Nodes.Add(Node);

                    Node.ImageIndex = Convert.ToInt32(Row["ImageIndex"].ToString());
                    Node.SelectedImageIndex = Convert.ToInt32(Row["ImageIndex"].ToString());
                    Init_Tree(Int32.Parse(Row["ID"].ToString()), Node, tv, dt);
                }
            }
        }

        /// <summary>
        /// 获取勾选的设备
        /// </summary>
        /// <param name="node"></param>
        public static void GetCheckedList(TreeNode node, List<int> list)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    var temp = Convert.ToInt32(n.Tag);
                    if (n.Checked && temp >= 100)
                    {
                        list.Add(temp);
                    }
                    GetCheckedList(n, list);
                }
            }
        }
        /// <summary>
        /// 获取树形结构的所有设备
        /// </summary>
        /// <param name="node"></param>
        public static void GetAllDeviceIdList(TreeNode node, List<int> list)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    var temp = Convert.ToInt32(n.Tag);
                    if (temp >= 100)
                    {
                        list.Add(temp);
                    }
                    GetCheckedList(n, list);
                }
            }
        }

        /// <summary>
        /// 清空datagridview的数据
        /// </summary>
        /// <param name="gdv"></param>
        public static void ClearDataGridView(DataGridView gdv)
        {
            ///令dgv列头没有排序功能
            foreach (DataGridViewColumn column in gdv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            int count = gdv.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    gdv.Rows.RemoveAt(0);
                }
            }

        }

        /// <summary>
        /// 遍历树节点,在满足条件的节点添加右键菜单
        /// </summary>
        /// <param name="aNodes"></param>
        public static void AddContextMenuForTreeNode(TreeNodeCollection aNodes, ContextMenuStrip cms1)
        {
            foreach (TreeNode iNode in aNodes)
            {
                iNode.ContextMenuStrip = cms1;
                if (iNode.Nodes.Count > 0)
                {
                    AddContextMenuForTreeNode(iNode.Nodes, cms1);
                }
            }
        }

        /// <summary>
        /// 遍历树节点,在满足条件的节点添加右键菜单
        /// </summary>
        /// <param name="aNodes"></param>
        public static void AddContextMenuForTreeNode(TreeNodeCollection aNodes, ContextMenuStrip cms1, ContextMenuStrip cms2)
        {
            foreach (TreeNode iNode in aNodes)
            {
                int temp = Convert.ToInt32(iNode.Tag);

                if (temp > 0 && temp < 100)
                {
                    iNode.ContextMenuStrip = cms1;
                }
                else if (temp >= 100)
                {
                    iNode.ContextMenuStrip = cms2;
                }
                if (iNode.Nodes.Count > 0)
                {
                    AddContextMenuForTreeNode(iNode.Nodes, cms1, cms2);
                }
            }
        }


        #region 显示部门树
        public static void DisplayDeptTree(TreeView tv, ImageList imageList1)
        {
            DataTable dt = DeptInfoService.GetDeptTreeDable();
            tv.Nodes.Clear();
            TreeHelper.Init_Tree(0, (TreeNode)null, tv, dt);
            tv.ExpandAll();
            tv.ImageList = imageList1;
        }
        #endregion

        #region 显示树
        /// <summary>
        /// 显示树
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tv"></param>
        /// <param name="imageList1"></param>
        public static void DisplayTree(DataTable dt, TreeView tv, int father, ImageList imageList1)
        {
            tv.Nodes.Clear();
            tv.ImageList = imageList1;

            TreeHelper.Init_Tree(father, (TreeNode)null, tv, dt);
            tv.ExpandAll();
            dt.Clear();
        }

        #endregion

        #region 显示设备树
        /// <summary>
        /// 显示部门树形结构
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="imageList1"></param>
        public static void DisplayDeviceTree(TreeView tv, ImageList imageList1)
        {
            DataTable dt = DeviceInfoService.GetDeptTreeDable();
            tv.Nodes.Clear();
            TreeHelper.Init_Tree(-1, (TreeNode)null, tv, dt);
            tv.ExpandAll();
            tv.ImageList = imageList1;
        }

        #endregion

        public static void GetCheckedValueList(TreeNode node, List<int> list)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    var temp = Convert.ToInt32(n.Tag);
                    if (n.Checked && temp >= 1)
                    {
                        list.Add(temp);
                    }
                    GetCheckedValueList(n, list);
                }
            }
        }


    }
}
