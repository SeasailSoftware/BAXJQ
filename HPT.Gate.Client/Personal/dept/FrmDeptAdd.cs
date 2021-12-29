using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Client.dept
{
    public partial class FrmDeptAdd : JForm
    {
        private int CurParDeptId;
        public FrmDeptAdd(int parDeptId = 1)
        {
            InitializeComponent();
            CurParDeptId = parDeptId;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        private void FrmDeptAdd_Load(object sender, EventArgs e)
        {
            LoadDepts();
            cbbParDept.SelectedValue = CurParDeptId;
        }

        #region 加载部门列表
        private void LoadDepts()
        {
            try
            {
                ComboBoxHelper.FillDeptComboBox(cbbParDept);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载部门列表失败:{ex.Message}");
            }
        }
        #endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        #region 保存部门信息
        private void SaveDeptInfo()
        {
            int parDeptId = (int)cbbParDept.SelectedValue;
            string deptName = tbDeptName.Text;
            if (deptName.Trim().Equals(string.Empty))
            {
                MessageBoxHelper.Info($"部门名称不能为空!");
                return;
            }
            if (DeptInfoService.CheckDeptExists(parDeptId, deptName))
            {
                MessageBoxHelper.Info($"在同级上级部门下存在相同的部门名称!");
                return;
            }
            try
            {
                DeptInfoService.Insert(parDeptId, deptName, LocalCache.CurrentOper);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"保存部门信息失败:{ex.Message}");
            }
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveDeptInfo();
        }
    }
}
