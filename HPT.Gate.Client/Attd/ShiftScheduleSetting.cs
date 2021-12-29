using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using hpt.gate.DbTools;
using hpt.gate.DbTools.Service;
using hpt.gate.client.Tools;
using hpt.gate.Entity;
using hpt.gate.Entity.Service;

namespace hptGate.Attend
{
    public partial class ShiftScheduleSetting : DevComponents.DotNetBar.Office2007Form
    {
        public ShiftScheduleSetting()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void ShiftScheduleSetting_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select * from DeptInfo";
                ComboBoxHelper.FillComboBox(sql, comboBoxItem1.ComboBoxEx, "DeptName", "DeptId");
                comboBoxItem1.ComboBoxEx.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                string sql = "Select * from AttendCycle";
                ComboBoxHelper.FillComboBox(sql, comboBox2, "CName", "CID");
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                string sql = "Select * from Shift";
                ComboBoxHelper.FillComboBox(sql, comboBox3, "SName", "SID");
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = checkBox1.Checked;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                {
                    int empId = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    string empCode = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string empName = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    AddRow(empId, empCode, empName);
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                    i--;
                }
            }

        }

        /// <summary>
        /// 添加新行
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        private void AddRow(int empId, string empCode, string empName)
        {
            bool flag = true;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                int _empId = Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
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
                    if (dataGridView2.DataSource != null)
                    {
                        string[] strArray = { empId.ToString(), empCode, empName };
                        ((DataTable)dataGridView2.DataSource).Rows.Add(strArray);
                    }
                    else
                    {
                        int index = dataGridView2.Rows.Add();
                        dataGridView2.Rows[index].Cells[1].Value = empId;
                        dataGridView2.Rows[index].Cells[2].Value = empCode;
                        dataGridView2.Rows[index].Cells[3].Value = empName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("往表格添加新行失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = checkBox2.Checked;
            }
        }

        /// <summary>
        /// 开始排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            buttonX1.Enabled = false;
            List<int> empIdList = new List<int>();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if ((bool)dataGridView2.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    int empId = Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
                    empIdList.Add(empId);
                }
            }
            if (empIdList.Count == 0)
            {
                MessageBox.Show("请勾选需要排班的人员!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonX1.Enabled = true;
                return;
            }
            string beginDate = dateTimePicker1.Text;
            string endDate = dateTimePicker2.Text;
            int type = radioButton1.Checked ? 0 : 1;
            int id = Convert.ToInt32(radioButton1.Checked ? comboBox2.SelectedValue : comboBox3.SelectedValue);
            ///开始排班
            using (OleDbHelper dbHelper = new OleDbHelper())
            {
                int index = 1;
                int count = empIdList.Count;
                toolStripProgressBar1.Value = 0;
                foreach (int empId in empIdList)
                {
                    try
                    {
                        AttendService.InsertShiftOfEmp(empId, beginDate, endDate, type, id, dbHelper);
                        toolStripProgressBar1.Value = index * 100 / count;
                        toolStripStatusLabel2.Text = (index * 100 / count).ToString() + @"%";
                        index++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("添加人员排班信息失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    Application.DoEvents();
                }
            }
            MessageBox.Show("人员排班完毕!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            buttonX1.Enabled = true;
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            string empCode = textBoxItem1.TextBox.Text.Trim();
            string empName = textBoxItem2.TextBox.Text.Trim();
            if (empCode.Equals(string.Empty) && empName.Equals(string.Empty))
            {
                MessageBox.Show("请输入查询条件!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                List<EmpInfo> empList = EmpInfoService.Find(empCode, empName);
                dataGridView2.DataSource = null;
                dataGridView2.Rows.Clear();
                foreach (EmpInfo emp in empList)
                {
                    int rowIndex = dataGridView2.Rows.Add();
                    dataGridView2.Rows[rowIndex].Cells[1].Value = emp.EmpId;
                    dataGridView2.Rows[rowIndex].Cells[2].Value = emp.EmpCode;
                    dataGridView2.Rows[rowIndex].Cells[3].Value = emp.EmpName;
                }
                textBoxItem1.TextBox.Text = string.Empty;
                textBoxItem2.TextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查找员工信息失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void comboBoxItem1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmps();
        }

        #region
        private void LoadEmps()
        {
            int deptId = 0;
            try
            {
                deptId = Convert.ToInt32(comboBoxItem1.ComboBoxEx.SelectedValue);
            }
            catch
            {
                return;
            }
            List<EmpInfo> empList = new List<EmpInfo>();
            if (checkBoxItem1.Checked)
                empList = EmpInfoService.GetAllByDept(deptId);
            else
                empList = EmpInfoService.GetByDeptId(deptId);
            foreach (EmpInfo emp in empList)
            {
                int rowIndex = dataGridView1.Rows.Add();
                dataGridView1.Rows[rowIndex].Cells[1].Value = emp.EmpId;
                dataGridView1.Rows[rowIndex].Cells[2].Value = emp.EmpCode;
                dataGridView1.Rows[rowIndex].Cells[3].Value = emp.EmpName;
            }
        }
        #endregion

        private void checkBoxItem1_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {

        }
    }
}