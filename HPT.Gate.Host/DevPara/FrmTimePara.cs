using HPT.Gate.Host.Base;
using HPT.Gate.Device.Data;
using HPT.Gate.Device.Dev;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HPT.Gate.Host.Util;

namespace HPT.Gate.Host.DevPara
{
    public partial class FrmTimePara : WinBase
    {
        public FrmTimePara()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonItem25_Click(object sender, EventArgs e)
        {
            AddVacation();
        }

        #region 添加节假日
        private void AddVacation()
        {
            if (dgvPVacation.Rows.Count >= 16)
            {
                MessageBox.Show("最多只能添加16个节假日时间段!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmVacationAdd vacationAdd = new FrmVacationAdd();
            DialogResult dr = vacationAdd.ShowDialog();
            if (dr == DialogResult.OK)
                LoadVacation();
        }

        #region 加载节假日列表
        private void LoadVacation()
        {
            List<Vacation> vacationList = VacationService.GetEffectList();
            dgvPVacation.DataSource = null;
            dgvPVacation.Rows.Clear();
            foreach (Vacation vacation in vacationList)
            {
                int rowIndex = dgvPVacation.Rows.Add();
                dgvPVacation.Rows[rowIndex].Cells[0].Value = vacation.Vid;
                dgvPVacation.Rows[rowIndex].Cells[1].Value = vacation.VName;
                dgvPVacation.Rows[rowIndex].Cells[2].Value = vacation.VBeginDate;
                dgvPVacation.Rows[rowIndex].Cells[3].Value = vacation.VEndDate;
                dgvPVacation.Rows[rowIndex].Cells[4].Value = vacation.VDesc;
            }
        }
        #endregion

        #endregion

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            EditVacation();
        }

        #region 修改节假日
        private void EditVacation()
        {
            if (dgvPVacation.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请先选择需要修改的节假日!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int id = Convert.ToInt32(dgvPVacation.SelectedRows[0].Cells["VID"].Value);
            FrmVacationEdit vacationEdit = new FrmVacationEdit(id);
            DialogResult dr = vacationEdit.ShowDialog();
            if (dr == DialogResult.OK)
                LoadVacation();
        }

        #endregion

        #region 删除节假日
        private void DeleteVacation()
        {
            if (dgvPVacation.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请先选择需要删除的节假日!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int id = Convert.ToInt32(dgvPVacation.SelectedRows[0].Cells["VID"].Value);
            var name = dgvPVacation.SelectedRows[0].Cells["VName"].Value.ToString();
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            var dr = MessageBox.Show("确定要删除该节假日吗?", "删除节假日", messButton, MessageBoxIcon.Question);
            if (dr != DialogResult.OK) return;
            try
            {
                VacationService.Del(id);
                LoadVacation();
            }
            catch (Exception ec)
            {
                MessageBox.Show("删除节假日失败:" + ec.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        #endregion

        #region 同步节假日
        private void SynVacation()
        {
            List<DeviceInfo> devices = DeviceInfoService.ToList();
            List<TcpDevice> devList = new List<TcpDevice>();
            foreach (DeviceInfo device in devices)
            {
                TcpDevice dev = DataConverter.GetTcpDevice(device);
                devList.Add(dev);
            }
            if (devList.Count == 0)
            {
                MessageBox.Show("没有可同步的设备!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataVacation DVacation = DataConverter.GetDataVacation();
            foreach (TcpDevice device in devList)
            {
                if (device.SetVacation(DVacation))
                {
                    ShowMsg(device._DeviceName + ":上传节假日成功!");
                }
                else
                {
                    ShowMsg(device._DeviceName + ":上传节假日失败!");
                }
            }
        }
        #endregion

        #region 添加节假日时间组
        private void AddTimeGroupOfVacation()
        {
            if (dgvPtimeGroupOfVacation.Rows.Count >= 3)
            {
                MessageBox.Show("节假日时间组最多只能添加三组!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmTimegroupOfVacationAdd timeGroupAdd = new FrmTimegroupOfVacationAdd();
            DialogResult dr = timeGroupAdd.ShowDialog();
            if (dr == DialogResult.OK)
                LoadTimeGroupOfVacation();
        }
        #endregion

        #region 修改节假日时间组
        private void EditTimeGroupOfVacation()
        {
            if (dgvPtimeGroupOfVacation.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请先选择需要编辑的节假日时间组!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int Gid = Convert.ToInt32(dgvPtimeGroupOfVacation.SelectedRows[0].Cells["GID"].Value);
            if (Gid == 0)
            {
                MessageBox.Show("默认时间段【24小时通行】不可修改!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmTimegroupOfVacationEdit timeGroupEdit = new FrmTimegroupOfVacationEdit(Gid);
            DialogResult dr = timeGroupEdit.ShowDialog();
            if (dr == DialogResult.OK)
                LoadTimeGroupOfVacation();
        }
        #endregion

        #region 删除节假日时间组
        private void DeleteTimeGroupOfVacation()
        {
            if (dgvPtimeGroupOfVacation.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请先选择需要删除的节假日时间组!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var id = dgvPtimeGroupOfVacation.SelectedRows[0].Cells["GID"].Value.ToString();
            var gName = dgvPtimeGroupOfVacation.SelectedRows[0].Cells["GName"].Value.ToString();
            if (Convert.ToInt32(id.Trim()) == 0)
            {
                MessageBox.Show("默认时间段【24小时通行】不可删除!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            var dr = MessageBox.Show("确定要删除该节假日时间组吗?", "删除节假日时间组", messButton, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {


                try
                {
                    //删除节假日时间组，对应的卡的节假日时间组必须改变！
                    DategroupOfVacationService.Del(id);
                    LoadTimeGroupOfVacation();
                }
                catch (Exception ec)
                {
                    MessageBox.Show("删除节假日时间组失败:" + ec.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region 同步节假日时间段

        private void SynTimeGroupOfVacation()
        {
            List<DeviceInfo> devices = DeviceInfoService.ToList();
            List<TcpDevice> devList = new List<TcpDevice>();
            foreach (DeviceInfo device in devices)
            {
                TcpDevice dev = DataConverter.GetTcpDevice(device);
                devList.Add(dev);
            }
            if (devList.Count == 0)
            {
                MessageBox.Show("没有可同步的设备!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTimeGroupOfVacation dtgv = DataConverter.GetDataTimegroupOfVacation();
            foreach (TcpDevice device in devList)
            {
                if (device.SetTimeGroupOfVacation(dtgv))
                {
                    ShowMsg(string.Format("[{0}]:上传节假日时间组成功!", device._DeviceName));
                }
                else
                {
                    ShowMsg(string.Format("[{0}]:上传节假日时间组失败!", device._DeviceName));
                }
                Application.DoEvents();
            }


        }
        #endregion

        #region 添加星期时间段
        private void AddTimeGroupOfDoor()
        {
            if (dgvPtimegroupOfDoor.Rows.Count >= 16)
            {
                MessageBox.Show("门禁时间组最多只能添加16组！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmTimegroupOfWeekAdd timeGroupAdd = new FrmTimegroupOfWeekAdd();
            DialogResult dr = timeGroupAdd.ShowDialog();
            if (dr == DialogResult.OK)
                LoadTimrGroupOfWeek();
        }
        #endregion

        #region 修改星期时间段
        private void EditTimeGroupOfWeek()
        {
            if (dgvPtimegroupOfDoor.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择需要修改的门禁时间组!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var sGroupId = dgvPtimegroupOfDoor.SelectedRows[0].Cells[0].Value.ToString().Trim();
            var sGroupName = dgvPtimegroupOfDoor.SelectedRows[0].Cells[1].Value.ToString().Trim();
            var sDoordesc = dgvPtimegroupOfDoor.SelectedRows[0].Cells[2].Value.ToString().Trim();

            var groupId = Convert.ToInt32(sGroupId.Trim());
            if (groupId == 0)
            {
                MessageBox.Show("默认时间组【24小时通行】不能修改!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmTimegroupOfWeekEdit weekEdit = new FrmTimegroupOfWeekEdit(groupId);
            DialogResult dr = weekEdit.ShowDialog();
            if (dr == DialogResult.OK)
                LoadTimrGroupOfWeek();
        }
        #endregion

        #region 删除星期时间段
        private void DeleteTimeGroupOfWeek()
        {
            if (dgvPtimegroupOfDoor.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择需要修改的门禁时间组!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var groupId = Convert.ToInt32(dgvPtimegroupOfDoor.SelectedRows[0].Cells[0].Value);
            string doorGroupName = dgvPtimegroupOfDoor.SelectedRows[0].Cells[1].Value.ToString().Trim();


            if (groupId == 0)
            {
                MessageBox.Show("时间组【24小时通行】不能删除!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                TimegroupOfDoorService.Del(groupId);
                LoadTimrGroupOfWeek();
            }
            catch (Exception ex)
            {
                MessageBox.Show("在删除门禁时间段过程中发生错误，错误信息:" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 同步星期时间组到设备
        private void SynTimeGroupOfWeek()
        {
            List<DeviceInfo> devices = DeviceInfoService.ToList();
            List<TcpDevice> devList = new List<TcpDevice>();
            foreach (DeviceInfo device in devices)
            {
                TcpDevice dev = DataConverter.GetTcpDevice(device);
                devList.Add(dev);
            }
            if (devList.Count == 0)
            {
                MessageBox.Show("没有可同步的设备!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<DataTimeGroupOfDoor> dtgdList = DataConverter.GetDataTimegroupOfWeek();
            foreach (TcpDevice device in devList)
            {
                foreach (DataTimeGroupOfDoor dtgd in dtgdList)
                {
                    if (device.SetTimeGroupOfDoor(dtgd))
                    {
                        ShowMsg(string.Format("[{0}]:上传星期时间组[{1}]成功!", device._DeviceName, dtgd.IGroupNo));
                    }
                    else
                    {
                        ShowMsg(string.Format("[{0}]:上传星期时间组[{1}]失败!", device._DeviceName, dtgd.IGroupNo));
                    }
                    Application.DoEvents();
                }
            }
        }
        #endregion


        private void FrmTimePara_Load(object sender, EventArgs e)
        {
            LoadTimeParas();
        }

        #region 加载时间参数
        private void LoadTimeParas()
        {
            LoadVacation();
            LoadTimeGroupOfVacation();
            LoadTimrGroupOfWeek();
        }
        #endregion

        #region 加载节假日时间段
        private void LoadTimeGroupOfVacation()
        {
            List<DategroupOfVacation> vacationList = DategroupOfVacationService.GetEffectList();
            dgvPtimeGroupOfVacation.DataSource = null;
            dgvPtimeGroupOfVacation.Rows.Clear();
            foreach (DategroupOfVacation vacation in vacationList)
            {
                int rowIndex = dgvPtimeGroupOfVacation.Rows.Add();
                dgvPtimeGroupOfVacation.Rows[rowIndex].Cells[0].Value = vacation.Gid;
                dgvPtimeGroupOfVacation.Rows[rowIndex].Cells[1].Value = vacation.GName;
                dgvPtimeGroupOfVacation.Rows[rowIndex].Cells[2].Value = vacation.GMark;
            }
        }

        #endregion

        #region 加载星期时间段
        private void LoadTimrGroupOfWeek()
        {

            List<TimegroupOfDoor> vacationList = TimegroupOfDoorService.GetEffectList();
            dgvPtimegroupOfDoor.DataSource = null;
            dgvPtimegroupOfDoor.Rows.Clear();
            foreach (TimegroupOfDoor vacation in vacationList)
            {
                int rowIndex = dgvPtimegroupOfDoor.Rows.Add();
                dgvPtimegroupOfDoor.Rows[rowIndex].Cells[0].Value = vacation.Id;
                dgvPtimegroupOfDoor.Rows[rowIndex].Cells[1].Value = vacation.Name;
                dgvPtimegroupOfDoor.Rows[rowIndex].Cells[2].Value = vacation.Desc;
            }

        }
        #endregion

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            DeleteVacation();
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            SynVacation();
        }




        #region 消息提示
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
                if (txtLog.Lines.Length != 0)
                    txtLog.AppendText("\r\n");
                txtLog.AppendText(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
            }
        }
        #endregion

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            SynTimeGroupOfVacation();
        }

        private void buttonItem30_Click(object sender, EventArgs e)
        {
            DeleteTimeGroupOfVacation();
        }


        private void buttonItem28_Click(object sender, EventArgs e)
        {
            AddTimeGroupOfVacation();
        }

        private void buttonItem29_Click(object sender, EventArgs e)
        {
            EditTimeGroupOfVacation();
        }

        private void buttonItem31_Click(object sender, EventArgs e)
        {
            AddTimeGroupOfDoor();
        }

        private void buttonItem32_Click(object sender, EventArgs e)
        {
            EditTimeGroupOfWeek();
        }

        private void buttonItem33_Click(object sender, EventArgs e)
        {
            DeleteTimeGroupOfWeek();
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            SynTimeGroupOfWeek();
        }


    }
}
