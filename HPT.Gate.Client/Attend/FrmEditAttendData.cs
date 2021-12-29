using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Client.Attend
{
    public partial class FrmEditAttendData : FrmBase
    {
        private int _RecId;
        public FrmEditAttendData(int recId, string recDate, string recTime)
        {
            InitializeComponent();
            _RecId = recId;
            dtpRecDate.Text = recDate;
            dtpRecTime.Text = recTime;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveAttendData();
        }

        private void SaveAttendData()
        {
            AttendData data = new AttendData();
            data.RecId = _RecId;
            data.RecDatetime = Convert.ToDateTime($"{dtpRecDate.Text} {dtpRecTime.Text}");
            try
            {
                AttendDataService.Update(data);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"修改考勤记录失败:{ex.Message}");
            }
        }
    }
}
