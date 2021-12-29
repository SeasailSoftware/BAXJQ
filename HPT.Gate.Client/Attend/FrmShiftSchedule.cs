using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HPT.Gate.Client.Attend
{
    public partial class FrmShiftSchedule :JForm
    {
        private List<EmpInfo> _CurrentEmps = new List<EmpInfo>();
        public FrmShiftSchedule(List<EmpInfo> empList)
        {
            InitializeComponent();
            _CurrentEmps = empList;
        }

        private void FrmShiftSchedule_Load(object sender, EventArgs e)
        {
            dtpBegin.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
            ComboBoxHelper.FillShiftComboBox(cbbShifts);
            foreach (EmpInfo emp in _CurrentEmps)
            {
                int rowIndex = dgvEmps.Rows.Add();
                dgvEmps.Rows[rowIndex].Cells[0].Value = emp.DeptName;
                dgvEmps.Rows[rowIndex].Cells[1].Value = emp.EmpId;
                dgvEmps.Rows[rowIndex].Cells[2].Value = emp.EmpCode;
                dgvEmps.Rows[rowIndex].Cells[3].Value = emp.EmpName;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            buttonX1.Enabled = false;
            btExit.Enabled = false;
            Cursor = Cursors.WaitCursor;
            ShiftSchedule();
            Cursor = Cursors.Default;
            buttonX1.Enabled = true;
            btExit.Enabled = true;
        }

        #region 人员排班
        private void ShiftSchedule()
        {
            string beginDate = dtpBegin.Text;
            string endDate = dtpEnd.Text;
            int shiftId = Convert.ToInt32(cbbShifts.SelectedValue);
            DateTime begin = DateTime.Now;
            DateTime end = DateTime.Now;
            int index = 1;
            int count = _CurrentEmps.Count;
            foreach (EmpInfo emp in _CurrentEmps)
            {
                ShowMsg($"正在为[{emp.EmpCode},{emp.EmpName}]排班...");
                Application.DoEvents();
                try
                {
                    AttendShiftOfEmpService.Insert(emp.EmpId, beginDate, endDate, shiftId);
                }
                catch (Exception ex)
                {
                    ShowMsg($"[{emp.EmpCode},{emp.EmpName}]排班失败:{ex.Message}");
                }
                end = DateTime.Now;
                double used = new TimeSpan(end.Ticks).Subtract(new TimeSpan(begin.Ticks)).Milliseconds;
                ShowMsg($"排班结束,排班用时{used}毫秒");
                begin = DateTime.Now;
                ShowProgress(index * 100 / count);
                index++;
            }
        }

        #endregion

        #region 消息展示
        private delegate void dlgShowMsg(string msg);
        private void ShowMsg(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                dlgShowMsg dlg = new dlgShowMsg(ShowMsg);
                txtLog.Invoke(dlg, msg);
            }
            else
            {
                if (txtLog.Lines.Length >= 100)
                    txtLog.Clear();
                txtLog.AppendText($"{Environment.NewLine}{msg}");
            }
        }

        #endregion

        #region 展示进度
        private delegate void dlgShowProgress(int value);
        private void ShowProgress(int value)
        {
            if (progressBar1.InvokeRequired)
            {
                dlgShowProgress dlg = new dlgShowProgress(ShowProgress);
                progressBar1.Invoke(dlg, value);
            }
            else
            {
                progressBar1.Value = value;
            }
        }
        #endregion

        private void btExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
