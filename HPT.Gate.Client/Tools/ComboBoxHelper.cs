using HPT.Gate.DataAccess.Service;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HPT.Gate.Client.Tools
{
    class ComboBoxHelper
    {
        internal static void FillCardParaCombobox(ComboBox combobox)
        {
            List<CardPara> cardParas = CardParaService.ToList();
            if (cardParas.Count == 0) return;
            combobox.DataSource = cardParas;
            combobox.DisplayMember = "CName";
            combobox.ValueMember = "Cid";
        }

        internal static void FillShiftComboBox(ComboBox cbbShifts)
        {
            List<AttendShift> shifts = AttendShiftService.ToList();
            if (shifts.Count == 0) return;
            cbbShifts.DataSource = shifts;
            cbbShifts.DisplayMember = "ShiftName";
            cbbShifts.ValueMember = "ShiftId";
        }

        #region 假期类型
        internal static void FillLeaves(ComboBox cbbLeaveType)
        {
            List<LeaveType> list = LeaveTypeService.ToList();
            if (list.Count == 0) return;
            cbbLeaveType.DataSource = list;
            cbbLeaveType.DisplayMember = "TypeName";
            cbbLeaveType.ValueMember = "TypeId";
        }
        #endregion

        internal static void FillComboBox(string s3, ComboBox cbbVoive, string v1, string v2)
        {
            throw new NotImplementedException();
        }

        internal static void FillVoiceComboBox(ComboBox cbbVoice)
        {
            List<Voice> voiceList = VoiceService.ToList();
            if (voiceList.Count == 0) return;
            cbbVoice.DataSource = voiceList;
            cbbVoice.DisplayMember = "VName";
            cbbVoice.ValueMember = "Vid";
        }

        #region 填充设备列表
        internal static void FillDeviceComBoBox(ComboBox comboBox, bool flag = false)
        {
            List<DeviceInfo> devList = new List<DeviceInfo>();
            if (flag)
            {
                DeviceInfo device = new DeviceInfo
                {
                    DeviceId = 0,
                    DeviceName = "-请选择-"
                };
                devList.Add(device);
            }
            if (devList.Count == 0) return;
            devList.AddRange(DeviceInfoService.ToList());
            comboBox.DataSource = devList;
            comboBox.DisplayMember = "DeviceName";
            comboBox.ValueMember = "DeviceId";
        }
        #endregion

        internal static void FillTimeGroupOfWeek(ComboBox cbbTimegroupOfIn)
        {
            List<TimegroupOfDoor> timeGroupList = TimegroupOfDoorService.GetEffectList();
            if (timeGroupList.Count == 0) return;
            cbbTimegroupOfIn.DataSource = timeGroupList;
            cbbTimegroupOfIn.DisplayMember = "Name";
            cbbTimegroupOfIn.ValueMember = "Id";
        }

        #region 填充节假日时间段
        internal static void FillTimeGroupOfVacation(ComboBox cbbVacation)
        {
            List<DategroupOfVacation> vacationList = DategroupOfVacationService.GetEffectList();
            if (vacationList.Count == 0) return;
            cbbVacation.DataSource = vacationList;
            cbbVacation.DisplayMember = "GName";
            cbbVacation.ValueMember = "Gid";
        }
        #endregion

        internal static void FillDeptComboBox(ComboBox cbbDept, bool flag = false)
        {
            List<DeptInfo> deptList = new List<DeptInfo>();
            if (flag)
            {
                DeptInfo dept = new DeptInfo()
                {
                    DeptId = 0,
                    DeptName = "--请选择--"
                };
                deptList.Add(dept);
            }
            deptList.AddRange(DeptInfoService.ToList());
            if (deptList.Count == 0) return;
            cbbDept.DataSource = deptList;
            cbbDept.DisplayMember = "DeptName";
            cbbDept.ValueMember = "DeptId";
        }

        #region 填充操作员下拉框
        internal static void FillOpers(ComboBox combobox)
        {
            List<OperInfo> operList = OperInfoService.ToList();
            if (operList.Count == 0) return;
            combobox.DataSource = operList;
            combobox.DisplayMember = "OperName";
            combobox.ValueMember = "OperId";
        }

        internal static void FillTimeGroupOfShift(ComboBox combobox)
        {
            List<TimeGroupOfShift> groupList = TimeGroupOfShiftService.ToList();
            if (groupList.Count == 0) return;
            combobox.DataSource = groupList;
            combobox.DisplayMember = "GroupName";
            combobox.ValueMember = "GroupId";
        }
        #endregion

        internal static void FillTicketTypeCombobox(ComboBox cbbTicketType)
        {
            List<TicketType> ticketTypes = TicketTypeService.ToList();
            if (ticketTypes.Count == 0) return;
            cbbTicketType.DataSource = ticketTypes;
            cbbTicketType.DisplayMember = "Name";
            cbbTicketType.ValueMember = "RecId";
        }
    }
}
