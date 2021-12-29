using HPT.Gate.Client.Base;
using HPT.Gate.Client.Tools;
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

namespace HPT.Gate.Client.Attend
{
    public partial class FrmShiftAdd : JForm
    {
        private int _ShiftId;
        public FrmShiftAdd()
        {
            InitializeComponent();
        }

        private void cbbShiftType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTimeGroups();
        }

        #region
        private void LoadTimeGroups()
        {
            int type = cbbShiftType.SelectedIndex;
            int rowIndex;
            int total = 0;
            dgvShifts.Rows.Clear();
            switch (type)
            {
                case 0:
                    total = 7;
                    for (int i = 0; i < total; i++)
                    {
                        rowIndex = dgvShifts.Rows.Add();
                        dgvShifts.Rows[rowIndex].Cells[0].Value = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)i);
                    }
                    break;
                case 1:
                    total = 31;
                    for (int i = 0; i < total; i++)
                    {
                        rowIndex = dgvShifts.Rows.Add();
                        dgvShifts.Rows[rowIndex].Cells[0].Value = $"{i + 1}号";
                    }
                    break;
            }
        }

        #endregion

        #region 初始化
        private void InitDataGridView()
        {
            dgvShifts.DataSource = null;
            dgvShifts.Rows.Clear();
            List<TimeGroupOfShift> list = new List<TimeGroupOfShift>();
            TimeGroupOfShift timeGroup = new TimeGroupOfShift();
            list.AddRange(TimeGroupOfShiftService.ToList());

            //名称
            DataGridViewTextBoxColumn columnName = new DataGridViewTextBoxColumn();
            columnName.HeaderText = "日期/星期";
            this.dgvShifts.Columns.Add(columnName);
            //时段一
            DataGridViewComboBoxColumn group1 = new DataGridViewComboBoxColumn();
            group1.DataSource = list;
            group1.HeaderText = "时段一";
            group1.DisplayMember = "GroupName";
            group1.ValueMember = "GroupId";
            group1.DataPropertyName = "Index";
            this.dgvShifts.Columns.Add(group1);
            //名称
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "  ";
            this.dgvShifts.Columns.Add(column1);
            //时段二
            DataGridViewComboBoxColumn group2 = new DataGridViewComboBoxColumn();
            group2.DataSource = list;
            group2.HeaderText = "时段二";
            group2.DisplayMember = "GroupName";
            group2.ValueMember = "GroupId";
            group2.DataPropertyName = "Index";
            this.dgvShifts.Columns.Add(group2);
            //名称
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.HeaderText = "  ";
            this.dgvShifts.Columns.Add(column2);
            //时段三
            DataGridViewComboBoxColumn group3 = new DataGridViewComboBoxColumn();
            group3.DataSource = list;
            group3.HeaderText = "时段三";
            group3.DisplayMember = "GroupName";
            group3.ValueMember = "GroupId";
            group3.DataPropertyName = "Index";
            this.dgvShifts.Columns.Add(group3);
        }

        #endregion

        private void FrmShiftAdd_Load(object sender, EventArgs e)
        {
            _ShiftId = AttendShiftService.GetShiftId();
            InitDataGridView();
            ComboBoxHelper.FillTimeGroupOfShift(cbbTimeGroupOfShift);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            int index = cbbGroupNo.SelectedIndex;
            int cellIndex = -1;
            switch (index)
            {
                case 0:
                    cellIndex = 1;
                    break;
                case 1:
                    cellIndex = 3;
                    break;
                case 2:
                    cellIndex = 5;
                    break;
            }
            int groupId = Convert.ToInt32(cbbTimeGroupOfShift.SelectedValue);
            string groupName = cbbTimeGroupOfShift.Text;
            foreach (DataGridViewRow row in dgvShifts.Rows)
            {
                row.Cells[cellIndex].Value = groupId;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        #region 保存班次信息
        private void SaveShift()
        {
            if (string.IsNullOrWhiteSpace(tbShiftName.Text))
            {
                MessageBoxHelper.Info("班次名称不能为空!");
                return;
            }
            if (cbbShiftType.SelectedIndex == -1)
            {
                MessageBoxHelper.Info("请选择班次周期!");
                return;
            }
            AttendShift shift = new AttendShift();
            shift.ShiftId = _ShiftId;
            shift.ShiftName = tbShiftName.Text;
            shift.ShiftType = cbbShiftType.SelectedIndex;
            switch (shift.ShiftType)
            {
                case 0:
                    shift.ShiftOfWeek = new List<AttendShiftOfWeek>();
                    foreach (DataGridViewRow row in dgvShifts.Rows)
                    {
                        AttendShiftOfWeek week = new AttendShiftOfWeek();
                        week.ShiftId = _ShiftId;
                        week.WeekId = row.Index;
                        int groupId1 = Convert.ToInt32(row.Cells[1].Value == null ? 0 : row.Cells[1].Value);
                        if (groupId1 > 0)
                            week.TimeGroup1 = TimeGroupOfShiftService.GetById(groupId1);
                        int groupId2 = Convert.ToInt32(row.Cells[3].Value == null ? 0 : row.Cells[3].Value);
                        if (groupId2 > 0)
                            week.TimeGroup2 = TimeGroupOfShiftService.GetById(groupId2);
                        int groupId3 = Convert.ToInt32(row.Cells[5].Value == null ? 0 : row.Cells[5].Value);
                        if (groupId3 > 0)
                            week.TimeGroup3 = TimeGroupOfShiftService.GetById(groupId3);
                        shift.ShiftOfWeek.Add(week);
                    }
                    break;
                case 1:
                    shift.ShiftOfMonth = new List<AttendShiftOfMonth>();
                    foreach (DataGridViewRow row in dgvShifts.Rows)
                    {
                        AttendShiftOfMonth month = new AttendShiftOfMonth();
                        month.ShiftId = _ShiftId;
                        month.DayId = row.Index;
                        int groupId1 = Convert.ToInt32(row.Cells[1].Value == null ? 0 : row.Cells[1].Value);
                        if (groupId1 > 0)
                            month.TimeGroup1 = TimeGroupOfShiftService.GetById(groupId1);
                        int groupId2 = Convert.ToInt32(row.Cells[3].Value == null ? 0 : row.Cells[3].Value);
                        if (groupId2 > 0)
                            month.TimeGroup2 = TimeGroupOfShiftService.GetById(groupId2);
                        int groupId3 = Convert.ToInt32(row.Cells[5].Value == null ? 0 : row.Cells[5].Value);
                        if (groupId3 > 0)
                            month.TimeGroup3 = TimeGroupOfShiftService.GetById(groupId3);
                        shift.ShiftOfMonth.Add(month);
                    }
                    break;
            }
            try
            {
                AttendShiftService.Insert(shift);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"添加班次失败:{ex.Message}");
            }
        }
        #endregion


        private void buttonX4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvShifts.Rows)
            {
                row.Cells[1].Value = null;
                row.Cells[3].Value = null;
                row.Cells[5].Value = null;
            }
        }

        private void dgvShifts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvShifts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 3 || e.ColumnIndex == 5)
                dgvShifts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveShift();
        }
    }
}
