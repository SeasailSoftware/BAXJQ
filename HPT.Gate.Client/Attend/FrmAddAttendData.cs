using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Windows.Forms;

namespace HPT.Gate.Client.Attend
{
    public partial class FrmAddAttendData : FrmBase
    {
        private int _CurrentEmpId;
        public FrmAddAttendData(int empId, string recDate)
        {
            InitializeComponent();
            dtpRecDate.Text = recDate;
            _CurrentEmpId = empId;
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
            data.EmpId = _CurrentEmpId;
            data.DeviceId = 0;
            data.RecDatetime = Convert.ToDateTime($"{dtpRecDate.Text} {dtpRecTime.Text}");
            try
            {
                AttendDataService.Insert(data);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"添加考勤记录失败:{ex.Message}");
            }
        }
    }
}
