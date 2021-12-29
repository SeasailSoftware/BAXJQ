using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Client.Personal.emp
{
    public partial class FrmBatch : JWindow
    {
        public FrmBatch()
        {
            InitializeComponent();
        }

        private void FrmBatch_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillDeptComboBox(cbbDept.ComboBoxEx);
            cbbEmpStatus.ComboBoxEx.SelectedIndex = 2;

        }

        private void cbbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmps();
        }

        #region 加载人员
        private void LoadEmps()
        {
            int deptId = 0;
            try
            {
                deptId = Convert.ToInt32(cbbDept.ComboBoxEx.SelectedValue);
            }
            catch
            {
            }
            if (deptId == 0) return;
            dgvEmps1.DataSource = null;
            dgvEmps1.Rows.Clear();
            ckbAll1.Checked = false;

            int deptType = ckbDept.Checked ? 1 : 0;
            int status = cbbEmpStatus.ComboBoxEx.SelectedIndex;
            Task.Factory.StartNew(() =>
            {
                List<EmpInfo> empList = EmpInfoService.Find(deptId, deptType, status);
                this.Invoke(new Action(() =>
                {
                    foreach (EmpInfo emp in empList)
                    {
                        int rowIndex = dgvEmps1.Rows.Add();
                        dgvEmps1.Rows[rowIndex].Cells[0].Value = false;
                        dgvEmps1.Rows[rowIndex].Cells[1].Value = emp.DeptName;
                        dgvEmps1.Rows[rowIndex].Cells[2].Value = emp.EmpId;
                        dgvEmps1.Rows[rowIndex].Cells[3].Value = emp.EmpCode;
                        dgvEmps1.Rows[rowIndex].Cells[4].Value = emp.EmpName;
                        dgvEmps1.Rows[rowIndex].Cells[5].Value = emp.JoinDate;
                        dgvEmps1.Rows[rowIndex].Cells[6].Value = emp.Status == 0 ? "在职" : "离职";
                        if (emp.Status == 0)
                            dgvEmps1.Rows[rowIndex].Cells[7].Value = string.Empty;
                        else
                        {
                            dgvEmps1.Rows[rowIndex].Cells[7].Value = emp.LeaveDate;
                            dgvEmps1.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gray;
                        }
                    }

                }));
            });
        }
        #endregion

        private void checkBoxItem1_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            LoadEmps();
        }

        private void cbbEmpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmps();
        }

        private void ckbAll1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEmps1.Rows)
            {
                row.Cells[0].Value = ckbAll1.Checked;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvEmps1.Rows.Count; i++)
            {
                if ((bool)dgvEmps1.Rows[i].Cells[0].EditedFormattedValue)
                {
                    string deptName = dgvEmps1.Rows[i].Cells[1].Value.ToString();
                    int empId = Convert.ToInt32(dgvEmps1.Rows[i].Cells[2].Value);
                    string empCode = dgvEmps1.Rows[i].Cells[3].Value.ToString();
                    string empName = dgvEmps1.Rows[i].Cells[4].Value.ToString();
                    string joinDate = dgvEmps1.Rows[i].Cells[5].Value.ToString();
                    string status = dgvEmps1.Rows[i].Cells[6].Value.ToString();
                    string leaveDate = dgvEmps1.Rows[i].Cells[5].Value.ToString();
                    AddNewRow(dgvEmp2, deptName, empId, empCode, empName, joinDate, status, leaveDate);
                    dgvEmps1.Rows.Remove(dgvEmps1.Rows[i]);
                    i--;
                }
            }
            ckbAll1.Checked = false;
        }

        #region 添加新行
        private void AddNewRow(DataGridView dgv, string deptName, int empId, string empCode, string empName, string joinDate, string status, string leaveDate)
        {
            bool flag = true;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                int _empId = Convert.ToInt32(dgv.Rows[i].Cells[2].Value);
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
                        string[] strArray = { "0", deptName, empId.ToString(), empCode, empName, joinDate, status.ToString(), leaveDate };
                        ((DataTable)dgv.DataSource).Rows.Add(strArray);
                    }
                    else
                    {
                        int index = dgv.Rows.Add();
                        dgv.Rows[index].Cells[0].Value = false;
                        dgv.Rows[index].Cells[1].Value = deptName;
                        dgv.Rows[index].Cells[2].Value = empId;
                        dgv.Rows[index].Cells[3].Value = empCode;
                        dgv.Rows[index].Cells[4].Value = empName;
                        dgv.Rows[index].Cells[5].Value = joinDate;
                        dgv.Rows[index].Cells[6].Value = status;
                        dgv.Rows[index].Cells[7].Value = leaveDate;
                        if (status.Equals("离职"))
                            dgv.Rows[index].DefaultCellStyle.ForeColor = Color.Gray;
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error("往表格添加新行失败:" + ex.Message);
                    return;
                }
            }
        }
        #endregion

        private void ckbAll2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEmp2.Rows)
            {
                row.Cells[0].Value = ckbAll2.Checked;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvEmp2.Rows.Count; i++)
            {
                if ((bool)dgvEmp2.Rows[i].Cells[0].EditedFormattedValue)
                {
                    string deptName = dgvEmp2.Rows[i].Cells[1].Value.ToString();
                    int empId = Convert.ToInt32(dgvEmp2.Rows[i].Cells[2].Value);
                    string empCode = dgvEmp2.Rows[i].Cells[3].Value.ToString();
                    string empName = dgvEmp2.Rows[i].Cells[4].Value.ToString();
                    string joinDate = dgvEmp2.Rows[i].Cells[5].Value.ToString();
                    string status = dgvEmp2.Rows[i].Cells[6].Value.ToString();
                    string leaveDate = dgvEmp2.Rows[i].Cells[5].Value.ToString();
                    AddNewRow(dgvEmps1, deptName, empId, empCode, empName, joinDate, status, leaveDate);
                    dgvEmp2.Rows.Remove(dgvEmp2.Rows[i]);
                    i--;
                }
            }
            ckbAll2.Checked = false;
        }

        private void btSetLeave_Click(object sender, EventArgs e)
        {
            SetLeave();
        }

        #region 设置离职
        private void SetLeave()
        {
            FrmPickLeaveDate pick = new FrmPickLeaveDate();
            DialogResult dr = pick.ShowDialog();
            if (dr != DialogResult.OK) return;
            btSetLeave.Enabled = false;
            btCancelCard.Enabled = false;
            btClear.Enabled = false;
            foreach (DataGridViewRow row in dgvEmp2.Rows)
            {
                if (!(bool)row.Cells[0].EditedFormattedValue) continue;
                int empId = Convert.ToInt32(row.Cells[2].Value);
                string empCode = row.Cells[3].Value.ToString();
                string status = row.Cells[6].Value.ToString();
                if (status.Equals("离职")) continue;
                try
                {
                    EmpInfoService.SetEmpLeave(empId, pick._LeaveDate);
                    row.Cells[6].Value = "离职";
                    row.Cells[7].Value = pick._LeaveDate;
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error($"设置人员离职失败:{ex.Message},人员编号={empCode}");
                }
            }
            MessageBoxHelper.Info("处理完毕!");
            btSetLeave.Enabled = true;
            btCancelCard.Enabled = true;
            btClear.Enabled = true;
        }
        #endregion

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            dgvEmp2.Rows.Clear();
            ckbAll2.Checked = false;
        }

        private void btCancelCard_Click(object sender, EventArgs e)
        {
            BatchCancelCard();
        }

        #region 批量注销卡
        private void BatchCancelCard()
        {
            btSetLeave.Enabled = false;
            btCancelCard.Enabled = false;
            btClear.Enabled = false;
            foreach (DataGridViewRow row in dgvEmp2.Rows)
            {
                if (!(bool)row.Cells[0].EditedFormattedValue) continue;
                int empId = Convert.ToInt32(row.Cells[2].Value);
                string empCode = row.Cells[3].Value.ToString();
                string status = row.Cells[6].Value.ToString();
                try
                {
                    EmpInfoService.CancelICCard(empId);
                    EmpInfoService.CancelIDSerial(empId);
                    EmpInfoService.CancelIDCardNo(empId);
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error($"注销人员卡失败:{ex.Message},人员编号={empCode}");
                }
            }
            MessageBoxHelper.Info("处理完毕!");
            btSetLeave.Enabled = true;
            btCancelCard.Enabled = true;
            btClear.Enabled = true;
        }
        #endregion

        private void btCancelFingerPrint_Click(object sender, EventArgs e)
        {
            BatchCancelFingerPrints();
        }

        #region 批量注销指纹
        private void BatchCancelFingerPrints()
        {
            btSetLeave.Enabled = false;
            btCancelCard.Enabled = false;
            btClear.Enabled = false;
            foreach (DataGridViewRow row in dgvEmp2.Rows)
            {
                if (!(bool)row.Cells[0].EditedFormattedValue) continue;
                int empId = Convert.ToInt32(row.Cells[2].Value);
                string empCode = row.Cells[3].Value.ToString();
                string status = row.Cells[6].Value.ToString();
                try
                {
                    EmpInfoService.CancelFingerPrints(empId);
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error($"注销人员指纹数据失败:{ex.Message},人员编号={empCode}");
                }
            }
            MessageBoxHelper.Info("处理完毕!");
            btSetLeave.Enabled = true;
            btCancelCard.Enabled = true;
            btClear.Enabled = true;
        }
        #endregion

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            LoadEmps();
        }
    }
}
