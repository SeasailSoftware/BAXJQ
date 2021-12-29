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

namespace HPT.Gate.Client.Attend
{
    public partial class FrmTimegroupOfShift : JWindow
    {
        private int _CurrentRowIndex;
        public FrmTimegroupOfShift()
        {
            InitializeComponent();
        }

        private void buttonItem39_Click(object sender, EventArgs e)
        {
            FrmTimegroupOfShiftAdd add = new FrmTimegroupOfShiftAdd();
            DialogResult dr = add.ShowDialog();
            if (dr != DialogResult.OK) return;
            LoadTimeGroupOfShifts();
        }

        private void FrmTimegroupOfShift_Load(object sender, EventArgs e)
        {
            LoadTimeGroupOfShifts();
        }

        #region 
        private void LoadTimeGroupOfShifts()
        {
            List<TimeGroupOfShift> groupList = TimeGroupOfShiftService.ToList();
            dgvTimeGroup.DataSource = null;
            dgvTimeGroup.Rows.Clear();
            foreach (TimeGroupOfShift group in groupList)
            {
                int rowIndex = dgvTimeGroup.Rows.Add();
                dgvTimeGroup.Rows[rowIndex].Cells[0].Value = group.GroupId;
                dgvTimeGroup.Rows[rowIndex].Cells[1].Value = group.GroupName;
                dgvTimeGroup.Rows[rowIndex].Cells[2].Value = group.Time1;
                dgvTimeGroup.Rows[rowIndex].Cells[3].Value = group.Time2;
                dgvTimeGroup.Rows[rowIndex].Cells[4].Value = group.BeginTime1;
                dgvTimeGroup.Rows[rowIndex].Cells[5].Value = group.EndTime1;
                dgvTimeGroup.Rows[rowIndex].Cells[6].Value = group.BeginTime2;
                dgvTimeGroup.Rows[rowIndex].Cells[7].Value = group.EndTime2;
                dgvTimeGroup.Rows[rowIndex].Cells[8].Value = group.Day;
            }
        }
        #endregion

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            int groupId = 0;
            try
            {
                groupId = Convert.ToInt32(dgvTimeGroup.Rows[_CurrentRowIndex].Cells[0].Value);
            }
            catch
            {

            }
            if (groupId == 0)
            {
                MessageBoxHelper.Info("没有可修改的时间段!");
                return;
            }
            FrmTimeGroupOfShiftEdit edit = new FrmTimeGroupOfShiftEdit(groupId);
            DialogResult dr = edit.ShowDialog();
            if (dr != DialogResult.OK) return;
            LoadTimeGroupOfShifts();
        }

        private void dgvTimeGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _CurrentRowIndex = e.RowIndex;
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            int groupId = 0;
            try
            {
                groupId = Convert.ToInt32(dgvTimeGroup.Rows[_CurrentRowIndex].Cells[0].Value);
            }
            catch
            {

            }
            if (groupId == 0)
            {
                MessageBoxHelper.Info("没有可修改的时间段!");
                return;
            }
            string groupName = dgvTimeGroup.Rows[_CurrentRowIndex].Cells[1].Value.ToString();
            DialogResult dr = MessageBoxHelper.Question($"确定要删除时间段[{groupName}]吗?");
            if (dr != DialogResult.OK) return;
            try
            {
                TimeGroupOfShiftService.Del(groupId);
                LoadTimeGroupOfShifts();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"删除时间段失败:{ex.Message}");
            }

        }

        private void dgvTimeGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _CurrentRowIndex = e.RowIndex;
        }
    }
}
