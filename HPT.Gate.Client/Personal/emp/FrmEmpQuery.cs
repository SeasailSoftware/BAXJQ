using System;
using System.Windows.Forms;
using hpt.gate.CardReader;
using HPT.Gate.Client.Tools;
using Joey.UserControls;

namespace HPT.Gate.Client.emp
{
    public partial class FrmEmpQuery :JForm
    { 
        public int _DeptId;
        public int _DeptType;
        public string _IDCardNo;
        public string _Duty;
        public string _Telephone;
        public int _Status;
        public string _EmpCode = string.Empty;
        public string _EmpName = string.Empty;
        public int _CardType = -1;
        public string CardNo = string.Empty;
        public FrmEmpQuery()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            tbCardNo.Clear();
            tbCardNo.Text = ICCardReader.ReadCardNo();
        }

        private void FrmEmpQuery_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillDeptComboBox(cbbDept);
            ckbDept.Checked = true;
            cbbEmpStatus.SelectedIndex = 2;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _DeptId = Convert.ToInt32(cbbDept.SelectedValue);
            _DeptType = ckbDept.Checked ? 1 : 0;
            _EmpCode = tbEmpCode.Text;
            _EmpName = tbEmpName.Text;
            _IDCardNo = tbIdCard.Text;
            _Duty = tbDuty.Text;
            _Telephone = tbTelephone.Text;
            CardNo = tbCardNo.Text;
            _Status = cbbEmpStatus.SelectedIndex;
            DialogResult = DialogResult.OK;
        }
    }
}
