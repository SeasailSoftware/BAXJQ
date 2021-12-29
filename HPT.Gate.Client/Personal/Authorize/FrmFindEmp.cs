using HPT.Gate.Client.Base;
using HPT.Gate.Client.Tools;
using Joey.UserControls;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Client.Authorize
{
    public partial class FrmFindEmp : FrmBase
    {
        public int DeptId;
        public int DeptType;
        public string EmpCode;
        public string EmpName;
        public FrmFindEmp()
        {
            InitializeComponent();
        }

        private void FrmFindEmp_Load(object sender, EventArgs e)
        {
            LoadDepts();
        }

        #region 加载部门列表
        private void LoadDepts()
        {
            try
            {
                ComboBoxHelper.FillDeptComboBox(cbbDept);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载部门列表失败:{ex.Message}");
                return;
            }
        }

        #endregion

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btSubmit_Click(object sender, EventArgs e)
        {
            DeptId = Convert.ToInt32(cbbDept.SelectedValue);
            DeptType = ckbDept.Checked ? 1 : 0;
            EmpCode = tbEmpCode.Text;
            EmpName = tbEmpName.Text;
            DialogResult = DialogResult.OK;
        }

        private void btReadCard_Click(object sender, EventArgs e)
        {

        }
    }
}
