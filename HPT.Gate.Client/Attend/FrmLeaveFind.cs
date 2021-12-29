using HPT.Gate.Client.Base;
using HPT.Gate.Client.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Client.Attend
{
    public partial class FrmLeaveFind : FrmBase
    {
        public int DeptId;
        public int DeptType;
        public string EmpCode;
        public string EmpName;
        public FrmLeaveFind()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmLeaveFind_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillDeptComboBox(cbbDept);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DeptId = Convert.ToInt32(cbbDept.SelectedValue);
            DeptType = ckbDept.Checked ? 1 : 0;
            EmpCode = tbEmpCode.Text;
            EmpName = tbEmpName.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
