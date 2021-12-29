
using HPT.Gate.Client;
using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Joey.Lib.Controls;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace hpt.gate.client.Authorize
{
    public partial class FrmChooseEmpHasCard : JWindow
    {
        private int _DeptId;
        private bool _DeptType;
        public List<EmpInfo> _EmpList = new List<EmpInfo>();
        public FrmChooseEmpHasCard()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            _EmpList = GetEmpListByChecked();
            if (_EmpList.Count == 0)
            {
                MessageBoxHelper.Info("没有选中人员!");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        #region 获取选中的人员
        private List<EmpInfo> GetEmpListByChecked()
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            foreach (DataGridViewRow row in dgvEmp.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    int deptId = Convert.ToInt32(row.Cells[1].Value);
                    string deptName = row.Cells[2].Value.ToString();
                    int empId = Convert.ToInt32(row.Cells[3].Value);
                    string empCode = row.Cells[4].Value.ToString();
                    string empName = row.Cells[5].Value.ToString();
                    EmpInfo emp = new EmpInfo()
                    {
                        DeptId = deptId,
                        DeptName = deptName,
                        EmpId = empId,
                        EmpCode = empCode,
                        EmpName = empName
                    };
                    emps.Add(emp);
                }
            }
            return emps;
        }
        #endregion




        #region 查找人员
        private void LoadEmps(Action<string> action)
        {
            try
            {
                int deptType = _DeptType ? 1 : 0;
                DataTable dt = EmpInfoService.Find(_DeptId, deptType, tbEmpCode.Text, tbEmpName.Text, "", "", "", "", 0);
                this.Invoke(new Action(() =>
                {
                    dgvEmp.AutoGenerateColumns = false;
                    dgvEmp.DataSource = dt;
                }));
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error(ex.Message);
            }
        }
        #endregion

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            TreeHelper.ClearDataGridView(dgvEmp);
        }

        private void FrmChooseEmpHasCard_Load(object sender, EventArgs e)
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
            }
        }
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEmp.Rows)
            {
                row.Cells[0].Value = checkBox1.Checked;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (cbbDept.SelectedIndex == -1)
            {
                MessageBoxHelper.Info("请选择部门!");
                return;
            }
            _DeptId = Convert.ToInt32(cbbDept.SelectedValue);
            _DeptType = ckbDept.Checked;
            JWaitingHelper helper = new JWaitingHelper();
            helper.MessageInfo = "正在加载中,请稍候...";
            helper.BackgroundWork = LoadEmps;
            helper.BackgroundWorkerCompleted += Helper_BackgroundWorkerCompleted;
            helper.Start();
        }


        private void Helper_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {

        }
    }
}
