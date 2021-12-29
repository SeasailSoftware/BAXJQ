using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Client.dept
{
    public partial class FrmDeptEdit : JForm
    {
        private int CurDeptId;
        private int CurPardeptId;
        private string CurDeptName;
        public FrmDeptEdit(int parDeptId, int deptId, string deptName)
        {
            InitializeComponent();
            CurPardeptId = parDeptId;
            CurDeptId = deptId;
            CurDeptName = deptName;
        }

        private void FrmDeptEdit_Load(object sender, EventArgs e)
        {
            LoadDepts();
            cbbParDept.SelectedValue = CurPardeptId;
            tbDeptName.Text = CurDeptName;
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
            if (parDeptId == CurDeptId)
            {
                MessageBoxHelper.Info($"不可把自身当作自身的上级部门!");
                return;
            }
            string deptName = tbDeptName.Text;
            if (deptName.Trim().Equals(string.Empty))
            {
                MessageBoxHelper.Info($"部门名称不能为空!");
                return;
            }
            try
            {
                DeptInfoService.Update(parDeptId, CurDeptId, deptName,LocalCache.CurrentOper);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"保存部门信息失败:{ex.Message}");
            }
        }
        #endregion

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveDeptInfo();
        }
    }
}
