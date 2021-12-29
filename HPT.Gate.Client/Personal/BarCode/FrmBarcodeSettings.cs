using HPT.Gate.Client.config;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HPT.Gate.Client.BarCode
{
    public partial class FrmBarcodeSettings : JForm
    {
        public FrmBarcodeSettings()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        #region 保存配置文件
        private void SaveConfig()
        {
            AppSettings.BarcodeEffectTime = (int)numBarcodeEffecttime.Value;
            AppSettings.BarcodeTimesOfIn = (int)numTimesOfIn.Value;
            AppSettings.BarcodeTimesOfOut = (int)numTimesOfOut.Value;
            List<int> devList = new List<int>();
            TreeHelper.GetCheckedList(DevTree.Nodes[0], devList);
            devList.Add(0);
            AppSettings.BarcodeDeviceList = devList;
        }
        #endregion


        private void FrmBarcodeSettings_Load(object sender, EventArgs e)
        {
            LoadDeviceTree();
            LoadConfig();
        }

        #region 加载设备树形菜单
        private void LoadDeviceTree()
        {
            try
            {
                TreeHelper.DisplayDeviceTree(DevTree, imageList1);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载安装区域与设备失败:" + ex.Message);
                return;
            }
        }
        #endregion


        #region 加载配置文件
        private void LoadConfig()
        {
            numTimesOfIn.Value = AppSettings.BarcodeTimesOfIn;
            numTimesOfOut.Value = AppSettings.BarcodeTimesOfOut;
            numBarcodeEffecttime.Value = AppSettings.BarcodeEffectTime;
            foreach (int index in AppSettings.BarcodeDeviceList)
            {
                CheckDevice(index);
            }
        }

        #endregion


        #region 选中设备
        private void CheckDevice(int index)
        {
            TreeHelper.CheckTreeNode(DevTree.Nodes[0], index);
        }


        #endregion

        private void DevTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeHelper.CheckChildren(e.Node);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
            DialogResult = DialogResult.OK;
        }
    }
}
