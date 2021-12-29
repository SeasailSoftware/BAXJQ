using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPT.Gate.Host.Base;
using hpt.gate.Util;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;

namespace HPT.Gate.Host.DevPara
{
    public partial class FPhotoPickUp : WinBase
    {
        public List<UInt64> _TaskList = new List<UInt64>();
        public FPhotoPickUp()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FPhotoPickUp_Load(object sender, EventArgs e)
        {
            try
            {
                ComboBoxHelper.FillDeptComboBox(comboBoxItem1.ComboBoxEx);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门树形结构失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (dgv2.Rows.Count > 0)
            {
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    dgv2.Rows[i].Cells[0].Value = checkBox1.Checked;
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dgv2.Rows.Count == 0)
            {
                MessageBox.Show("请选择需要上传照片的人员！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < dgv2.Rows.Count; i++)
            {
                if ((bool)dgv2.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    UInt64 empid = Convert.ToUInt64(dgv2.Rows[i].Cells[1].Value);
                    _TaskList.Add(empid);
                }
            }
            DialogResult = DialogResult.OK;

        }



        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxItem1_SelectedIndexChanged(object sender, EventArgs e)
        {

            ShowEmpInfo();

        }

        #region 展示人员信息
        private void ShowEmpInfo()
        {
            int deptId = 0;
            try
            {
                deptId = Convert.ToInt32(comboBoxItem1.ComboBoxEx.SelectedValue);
            }
            catch
            {

            }
            int type = checkBoxItem1.Checked ? 1 : 0;
            List<EmpInfo> empList = new List<EmpInfo>();
            if (type == 0)
                empList = EmpInfoService.GetByDeptId(deptId);
            else
                empList = EmpInfoService.GetAllByDept(deptId);
            dgv1.DataSource = null;
            dgv1.Rows.Clear();
            foreach (EmpInfo emp in empList)
            {
                int rowIndex = dgv1.Rows.Add();
                dgv1.Rows[rowIndex].Cells[1].Value = emp.EmpId;
                dgv1.Rows[rowIndex].Cells[2].Value = emp.EmpCode;
                dgv1.Rows[rowIndex].Cells[3].Value = emp.EmpName;
            }
            checkBox1.Checked = false;
        }

        #endregion

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count == 0)
            {
                MessageBox.Show("没有可添加的人员!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < dgv1.Rows.Count; i++)
            {
                if ((bool)dgv1.Rows[i].Cells[0].EditedFormattedValue)
                {
                    int empId = Convert.ToInt32(dgv1.Rows[i].Cells[1].Value);
                    string empCode = dgv1.Rows[i].Cells[2].Value.ToString();
                    string empName = dgv1.Rows[i].Cells[3].Value.ToString();
                    AddRow(dgv2, empId, empCode, empName);
                    dgv1.Rows.Remove(dgv1.Rows[i]);
                    i--;
                }
            }
            foreach (DataGridViewRow row in dgv2.Rows)
            {
                row.Cells[0].Value = true;
            }
        }

        /// <summary>
        /// 添加新行
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        private void AddRow(DataGridView dgv, int empId, string empCode, string empName)
        {
            bool flag = true;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                int _empId = Convert.ToInt32(dgv.Rows[i].Cells[1].Value);
                if (_empId == empId)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                try
                {
                    if (dgv.DataSource != null)
                    {
                        string[] strArray = { empId.ToString(), empCode, empName };
                        ((DataTable)dgv.DataSource).Rows.Add(strArray);
                    }
                    else
                    {
                        int index = dgv.Rows.Add();
                        dgv.Rows[index].Cells[1].Value = empId;
                        dgv.Rows[index].Cells[2].Value = empCode;
                        dgv.Rows[index].Cells[3].Value = empName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("往表格添加新行失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv1.Rows)
            {
                row.Cells[0].Value = checkBox2.Checked;
            }
        }

        private void checkBoxItem1_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            ShowEmpInfo();
        }
    }
}