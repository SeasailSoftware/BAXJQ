using HPT.Gate.Host.Base;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Host.DevPara
{
    public partial class FrmSoftKey : WinBase
    {

        public FrmSoftKey()
        {
            InitializeComponent();

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void FrmSoftKey_Load(object sender, EventArgs e)
        {

        }
    }
}
