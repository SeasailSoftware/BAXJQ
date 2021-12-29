using HPT.Gate.Client.Base;
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
    public partial class FrmAttendData : FrmBase
    {
        public FrmAttendData()
        {
            InitializeComponent();
        }

        private void dgvShifts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonItem39_Click(object sender, EventArgs e)
        {
            FrmRepairCard repairCard = new FrmRepairCard();
            repairCard.ShowDialog();
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            FrmLeave leave = new FrmLeave();
            leave.ShowDialog();
        }
    }
}
