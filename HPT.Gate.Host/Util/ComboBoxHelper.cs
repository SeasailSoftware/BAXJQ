using HPT.Gate.DataAccess.Service;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hpt.gate.Util
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

        internal static void FillComboBox(string s3, ComboBox cbbVoive, string v1, string v2)
        {
            throw new NotImplementedException();
        }

        #region 填充安装区域下拉框
        internal static void FillPlaceComboBox(ComboBox combobox)
        {
            List<DevicePlace> placeList = DevicePlaceService.ToList();
            if (placeList.Count == 0) return;
            combobox.DataSource = placeList;
            combobox.DisplayMember = "PlaceName";
            combobox.ValueMember = "PlaceId";
        }

        internal static void FillLedDynParas(ComboBox combobox)
        {
            List<LedDynPara> paraList = LedDynParaService.ToList();
            if (paraList.Count == 0) return;
            combobox.DataSource = paraList;
            combobox.DisplayMember = "ParaName";
            combobox.ValueMember = "ParaId";
        }
        #endregion

        internal static void FillVoiceComboBox(ComboBox cbbVoice)
        {
            List<Voice> voiceList = VoiceService.ToList();
            if (voiceList.Count == 0) return;
            cbbVoice.DataSource = voiceList;
            cbbVoice.DisplayMember = "VName";
            cbbVoice.ValueMember = "Vid";
        }

        #region 加载摄像头下拉框
        internal static void FillCameraComBoBox(ComboBox comboBox)
        {
            List<CameraInfo> cameraList = CameraInfoService.ToList();
            if (cameraList.Count == 0) return;
            comboBox.DataSource = cameraList;
            comboBox.DisplayMember = "CamName";
            comboBox.ValueMember = "CamId";
        }
        #endregion

        internal static void FillDeviceComBoBox(ComboBox comboBox)
        {
            List<DeviceInfo> devList = DeviceInfoService.ToList();
            if (devList.Count == 0) return;
            comboBox.DataSource = devList;
            comboBox.DisplayMember = "DeviceName";
            comboBox.ValueMember = "DeviceId";
        }

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

        internal static void FillDeptComboBox(ComboBox cbbDept)
        {
            List<DeptInfo> deptList = DeptInfoService.ToList();
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
