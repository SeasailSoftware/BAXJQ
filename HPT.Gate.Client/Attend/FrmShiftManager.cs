using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HPT.Gate.Client.Attend
{
    public partial class FrmShiftManager : JWindow
    {
        private int _CurrentRowIndex;
        public FrmShiftManager()
        {
            InitializeComponent();
        }

        private void FrmShiftManager_Load(object sender, EventArgs e)
        {
            LoadShifts();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmShiftAdd shiftAdd = new FrmShiftAdd();
            DialogResult dr = shiftAdd.ShowDialog();
            if (dr != DialogResult.OK) return;
            LoadShifts();
        }

        #region 加载班次列表
        private void LoadShifts()
        {
            dgvShift.DataSource = null;
            dgvShift.Rows.Clear();
            List<AttendShift> shiftList = AttendShiftService.ToList();
            foreach (AttendShift shift in shiftList)
            {
                int rowIndex = dgvShift.Rows.Add();
                dgvShift.Rows[rowIndex].Cells[0].Value = shift.ShiftId;
                dgvShift.Rows[rowIndex].Cells[1].Value = shift.ShiftName;
                dgvShift.Rows[rowIndex].Cells[2].Value = shift.ShiftType == 0 ? "按星期排班" : "按月排班";
            }
        }
        #endregion

        private void dgvShift_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _CurrentRowIndex = e.RowIndex;
            LoadShiftDetail(_CurrentRowIndex);
        }

        #region 加载详细时间段
        private void LoadShiftDetail(int rowIndex)
        {
            int shiftId = 0;
            try
            {
                shiftId = Convert.ToInt32(dgvShift.Rows[rowIndex].Cells[0].Value);
            }
            catch
            {
            }
            AttendShift shift = AttendShiftService.GetByShiftId(shiftId);
            if (shift != null)
            {
                dgvDetailOfShift.DataSource = null;
                dgvDetailOfShift.Rows.Clear();
                if (shift.ShiftType == 0)
                {
                    foreach (AttendShiftOfWeek week in shift.ShiftOfWeek)
                    {
                        int index = dgvDetailOfShift.Rows.Add();
                        dgvDetailOfShift.Rows[index].Cells[0].Value = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)index);
                        dgvDetailOfShift.Rows[index].Cells[1].Value = week.TimeGroup1 == null ? "" : week.TimeGroup1.Time1;
                        dgvDetailOfShift.Rows[index].Cells[2].Value = week.TimeGroup1 == null ? "" : week.TimeGroup1.Time2;
                        dgvDetailOfShift.Rows[index].Cells[3].Value = "";
                        dgvDetailOfShift.Rows[index].Cells[4].Value = week.TimeGroup2 == null ? "" : week.TimeGroup2.Time1;
                        dgvDetailOfShift.Rows[index].Cells[5].Value = week.TimeGroup2 == null ? "" : week.TimeGroup2.Time2;
                        dgvDetailOfShift.Rows[index].Cells[6].Value = "";
                        dgvDetailOfShift.Rows[index].Cells[7].Value = week.TimeGroup3 == null ? "" : week.TimeGroup3.Time1;
                        dgvDetailOfShift.Rows[index].Cells[8].Value = week.TimeGroup3 == null ? "" : week.TimeGroup3.Time2;
                    }
                    dgvDetailOfShift.Rows[0].DefaultCellStyle.BackColor = Color.Red;
                    dgvDetailOfShift.Rows[6].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    foreach (AttendShiftOfMonth month in shift.ShiftOfMonth)
                    {
                        int index = dgvDetailOfShift.Rows.Add();
                        dgvDetailOfShift.Rows[index].Cells[0].Value = $"{month.DayId}日";
                        dgvDetailOfShift.Rows[index].Cells[1].Value = month.TimeGroup1 == null ? "" : month.TimeGroup1.Time1;
                        dgvDetailOfShift.Rows[index].Cells[2].Value = month.TimeGroup1 == null ? "" : month.TimeGroup1.Time2;
                        dgvDetailOfShift.Rows[index].Cells[3].Value = "";
                        dgvDetailOfShift.Rows[index].Cells[4].Value = month.TimeGroup2 == null ? "" : month.TimeGroup2.Time1;
                        dgvDetailOfShift.Rows[index].Cells[5].Value = month.TimeGroup2 == null ? "" : month.TimeGroup2.Time2;
                        dgvDetailOfShift.Rows[index].Cells[6].Value = "";
                        dgvDetailOfShift.Rows[index].Cells[7].Value = month.TimeGroup3 == null ? "" : month.TimeGroup3.Time1;
                        dgvDetailOfShift.Rows[index].Cells[8].Value = month.TimeGroup3 == null ? "" : month.TimeGroup3.Time2;
                    }
                }
            }
        }
        #endregion

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (_CurrentRowIndex >= 0)
            {
                int shiftId = 0;
                try
                {
                    shiftId = Convert.ToInt32(dgvShift.Rows[_CurrentRowIndex].Cells[0].Value);
                }
                catch
                {
                    return;
                }
                FrmShiftEdit edit = new FrmShiftEdit(shiftId);
                DialogResult dr = edit.ShowDialog();
                if (dr != DialogResult.OK) return;
                LoadShifts();
            }
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (_CurrentRowIndex >= 0)
            {
                int shiftId = Convert.ToInt32(dgvShift.Rows[_CurrentRowIndex].Cells[0].Value);
                string shiftName = dgvShift.Rows[_CurrentRowIndex].Cells[1].Value.ToString();
                DialogResult dr = MessageBoxHelper.Question($"确定删除班次[{shiftName}]吗?此操作将导致所有以次班次的所有排班将删除.");
                if (dr != DialogResult.OK) return;
                try
                {
                    AttendShiftService.Del(shiftId);
                    LoadShifts();
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error($"删除班次信息失败:{ex.Message}");
                }
            }
        }

        private void dgvShift_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _CurrentRowIndex = e.RowIndex;
            LoadShiftDetail(_CurrentRowIndex);
        }
    }
}
