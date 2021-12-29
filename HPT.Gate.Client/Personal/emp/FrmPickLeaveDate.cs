using HPT.Gate.Client.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Client.Personal.emp
{
    public partial class FrmPickLeaveDate : FrmBase
    {
        public string _LeaveDate;
        public FrmPickLeaveDate()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            _LeaveDate = dtpLeaveDate.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
