using hpt.gate.DbTools.Service;
using hpt.gate.Led.Frm;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Host.Base;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace hpt.gate.led
{
    public partial class FrmLedEdit : WinBase
    {
        private int _Lid;
        public FrmLedEdit(int lid)
        {
            InitializeComponent();
            _Lid = lid;
        }

        private void FrmLedEdit_Load(object sender, EventArgs e)
        {
            LoadDefaultPara();
            LoadLedControllerDetail();
        }

        #region 加载默认参数
        private void LoadDefaultPara()
        {
            List<DeviceInfo> devices = DeviceInfoService.ToList();
            foreach (DeviceInfo device in devices)
            {
                int rowIndex = dgvDevices.Rows.Add();
                dgvDevices.Rows[rowIndex].Cells[1].Value = device.DeviceId;
                dgvDevices.Rows[rowIndex].Cells[2].Value = device.DeviceName;
                dgvDevices.Rows[rowIndex].Cells[3].Value = device.Mac;
            }
            cbbLedType.SelectedIndex = 0;
            cbbProtocol.SelectedIndex = 0;
            cbbColorType.SelectedIndex = 0;
            numPNo.Value = _Lid;
            cbbLattice.SelectedIndex = 0;
            P1_cbbFontSize.ComboBoxEx.SelectedIndex = 2;
            P2_cbbFontSize.ComboBoxEx.SelectedIndex = 2;
            P3_cbbFontSize.ComboBoxEx.SelectedIndex = 2;
            P1_Text.Checked = true;
            P1_ListBox.Items.Clear();
            P1_ListBox.Items.Add($"TxtFile{_Lid}0.TXT");
            P2_Text.Checked = true;
            P2_ListBox.Items.Clear();
            P2_ListBox.Items.Add($"TxtFile{_Lid}1.TXT");
            P3_Text.Checked = true;
            P3_ListBox.Items.Clear();
            P3_ListBox.Items.Add($"TxtFile{_Lid}2.TXT");
            P1_cbbSpeed.SelectedIndex = 15;
            P2_cbbSpeed.SelectedIndex = 15;
            P3_cbbSpeed.SelectedIndex = 15;
            P1_DisplayEffect.SelectedIndex = 1;
            P2_DisplayEffect.SelectedIndex = 1;
            P3_DisplayEffect.SelectedIndex = 1;
            P1_cbb_DYAreaFStunt.SelectedIndex = 7;
            P2_cbb_DYAreaFStunt.SelectedIndex = 7;
            P3_cbb_DYAreaFStunt.SelectedIndex = 7;
            InstalledFontCollection MyFont = new InstalledFontCollection();
            FontFamily[] MyFontFamilies = MyFont.Families;
            P1_cbbFont.ComboBoxEx.Items.Clear();
            P2_cbbFont.ComboBoxEx.Items.Clear();
            P2_cbbFont.ComboBoxEx.Items.Clear();
            foreach (FontFamily s in MyFont.Families)
            {
                P1_cbbFont.ComboBoxEx.Items.Add(s.Name);
                P2_cbbFont.ComboBoxEx.Items.Add(s.Name);
                P3_cbbFont.ComboBoxEx.Items.Add(s.Name);
            }
            P1_cbbFont.ComboBoxEx.SelectedItem = "宋体";
            P2_cbbFont.ComboBoxEx.SelectedItem = "宋体";
            P3_cbbFont.ComboBoxEx.SelectedItem = "宋体";
        }
        #endregion

        #region 加载Led控制卡相信信息
        private void LoadLedControllerDetail()
        {
            try
            {
                //加载控制卡信息
                LedController ledController = LedDbService.GetLedController(_Lid);
                numPNo.Value = ledController.Lid;
                numPNo.Enabled = false;
                cbbProtocol.SelectedIndex = ledController.Protocol;
                numWidth.Value = ledController.Width;
                numHeight.Value = ledController.Heigth;
                tbIPAddress.Text = ledController.IPaddress;
                numPort.Value = ledController.Port;
                cbbLedType.SelectedIndex = ledController.ControlType;
                foreach (int devId in ledController.Devices)
                {
                    foreach (DataGridViewRow row in dgvDevices.Rows)
                    {
                        string val = row.Cells[1].Value.ToString();
                        int deviceId;
                        if (int.TryParse(val, out deviceId))
                        {
                            if (deviceId == devId)
                                row.Cells[0].Value = true;
                        }
                    }
                }
                //加载动态区域一
                List<AreaInfo> areas = ledController.DynAreas;
                if (areas.Exists(s => s.AreaId == 0))
                {
                    AreaInfo area = areas.Where(s => s.AreaId == 0).ToList()[0];
                    ckbArea1.Checked = true;
                    P1_PointX.Value = area.Point_X;
                    P1_PointY.Value = area.Point_Y;
                    P1_numWidth.Value = area.Width;
                    P1_numHeight.Value = area.Height;
                    P1_ckbBorder.Checked = area.BordreType != 255;
                    P1_gbBord.Enabled = P1_ckbBorder.Checked;
                    P1_radbtn_DYAreaFSingle.Checked = area.BordreType == 0;
                    P1_spnedt_DYAreaFSingle.Value = P1_radbtn_DYAreaFSingle.Checked ? area.BorderNo : 0;
                    P1_radbtn_DYAreaFMuli.Checked = area.BordreType == 1;
                    P1_spnedt_DYAreaFMuli.Value = P1_radbtn_DYAreaFMuli.Checked ? area.BorderNo : 0;
                    P1_spnedt_DYAreaFMoveStep.Value = area.BorderLength;
                    P1_spnedt_DYAreaFRunSpeed.Value = area.BorderSpeed;
                    P1_cbb_DYAreaFStunt.SelectedIndex = area.BorderEffect;
                    P1_Bold.Checked = area.TextBold == 1;
                    string fileType = area.Text.Substring(area.Text.LastIndexOf("."));
                    P1_ListBox.Items.Clear();
                    if (!fileType.Equals(string.Empty))
                    {
                        switch (fileType.ToUpper())
                        {
                            case ".TXT":
                                P1_Text.Checked = true;
                                P1_Table.Checked = false;
                                break;
                            case ".XML":
                                P1_Text.Checked = false;
                                P1_Table.Checked = true;
                                break;
                        }
                    }
                    P1_ListBox.Items.Add(area.Text);
                    P1_cbbFont.ComboBoxEx.SelectedItem = area.TextFont;
                    P1_cbbFontSize.ComboBoxEx.Text = area.TextFontSize.ToString();
                    P1_SingleLine.Checked = area.SingleLine == 1;
                    P1_DisplayEffect.SelectedIndex = area.DisplayEffect;
                    P1_cbbSpeed.SelectedIndex = area.Speed - 1;
                    P1_StayTime.Value = area.Stay;
                    P1_numInterval.Value = area.Interval;
                }
                //加载动态区域二
                if (areas.Exists(s => s.AreaId == 1))
                {
                    AreaInfo area = areas.Where(s => s.AreaId == 1).ToList()[0];
                    ckbArea2.Checked = true;
                    P2_PointX.Value = area.Point_X;
                    P2_PointY.Value = area.Point_Y;
                    P2_numWidth.Value = area.Width;
                    P2_numHeight.Value = area.Height;
                    P2_ckbBorder.Checked = area.BordreType != 255;
                    P2_gbBorder.Enabled = P2_ckbBorder.Checked;
                    P2_radbtn_DYAreaFSingle.Checked = area.BordreType == 0;
                    P2_spnedt_DYAreaFSingle.Value = P2_radbtn_DYAreaFSingle.Checked ? area.BorderNo : 0;
                    P2_radbtn_DYAreaFMuli.Checked = area.BordreType == 1;
                    P2_spnedt_DYAreaFMuli.Value = P2_radbtn_DYAreaFMuli.Checked ? area.BorderNo : 0;
                    P2_spnedt_DYAreaFMoveStep.Value = area.BorderLength;
                    P2_spnedt_DYAreaFRunSpeed.Value = area.BorderSpeed;
                    P2_cbb_DYAreaFStunt.SelectedIndex = area.BorderEffect;
                    P2_Bold.Checked = area.TextBold == 1;
                    string fileType = area.Text.Substring(area.Text.LastIndexOf("."));
                    P2_ListBox.Items.Clear();
                    if (!fileType.Equals(string.Empty))
                    {
                        switch (fileType.ToUpper())
                        {
                            case ".TXT":
                                P2_Text.Checked = true;
                                P2_Table.Checked = false;
                                break;
                            case ".XML":
                                P2_Text.Checked = false;
                                P2_Table.Checked = true;
                                break;
                        }
                    }
                    P2_ListBox.Items.Add(area.Text);
                    P2_cbbFont.ComboBoxEx.SelectedItem = area.TextFont;
                    P2_cbbFontSize.ComboBoxEx.Text = area.TextFontSize.ToString();
                    P2_SingleLine.Checked = area.SingleLine == 1;
                    P2_DisplayEffect.SelectedIndex = area.DisplayEffect;
                    P2_cbbSpeed.SelectedIndex = area.Speed - 1;
                    P2_StayTime.Value = area.Stay;
                    P2_numInterval.Value = area.Interval;
                }
                //加载动态区域三
                if (areas.Exists(s => s.AreaId == 2))
                {
                    AreaInfo area = areas.Where(s => s.AreaId == 2).ToList()[0];
                    ckbArea3.Checked = true;
                    P3_PointX.Value = area.Point_X;
                    P3_PointY.Value = area.Point_Y;
                    P3_numWidth.Value = area.Width;
                    P3_numHeight.Value = area.Height;
                    P3_ckbBorder.Checked = area.BordreType != 255;
                    P3_gbBorder.Enabled = P3_ckbBorder.Checked;
                    P3_radbtn_DYAreaFSingle.Checked = area.BordreType == 0;
                    P3_spnedt_DYAreaFSingle.Value = P3_radbtn_DYAreaFSingle.Checked ? area.BorderNo : 0;
                    P3_radbtn_DYAreaFMuli.Checked = area.BordreType == 1;
                    P3_spnedt_DYAreaFMuli.Value = P3_radbtn_DYAreaFMuli.Checked ? area.BorderNo : 0;
                    P3_spnedt_DYAreaFMoveStep.Value = area.BorderLength;
                    P3_spnedt_DYAreaFRunSpeed.Value = area.BorderSpeed;
                    P3_cbb_DYAreaFStunt.SelectedIndex = area.BorderEffect;
                    P3_Bold.Checked = area.TextBold == 1;
                    string fileType = area.Text.Substring(area.Text.LastIndexOf("."));
                    P3_ListBox.Items.Clear();
                    if (!fileType.Equals(string.Empty))
                    {
                        switch (fileType.ToUpper())
                        {
                            case ".TXT":
                                P3_Text.Checked = true;
                                P3_Table.Checked = false;
                                break;
                            case ".XML":
                                P3_Text.Checked = false;
                                P3_Table.Checked = true;
                                break;
                        }
                    }
                    P3_ListBox.Items.Add(area.Text);
                    P3_cbbFont.ComboBoxEx.SelectedItem = area.TextFont;
                    P3_cbbFontSize.ComboBoxEx.Text = area.TextFontSize.ToString();
                    P3_SingleLine.Checked = area.SingleLine == 1;
                    P3_DisplayEffect.SelectedIndex = area.DisplayEffect;
                    P3_cbbSpeed.SelectedIndex = area.Speed - 1;
                    P3_StayTime.Value = area.Stay;
                    P3_numInterval.Value = area.Interval;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取控制卡信息失败：" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            SaveLedController();
        }

        #region 保存Led控制卡
        private void SaveLedController()
        {
            LedController led = new LedController();
            led.Lid = (int)numPNo.Value;
            led.ControlType = cbbLedType.SelectedIndex;
            led.Protocol = cbbProtocol.SelectedIndex;
            led.Width = (int)numWidth.Value;
            led.Heigth = (int)numHeight.Value;
            led.IPaddress = tbIPAddress.Text;
            led.Port = (int)numPort.Value;
            led.Devices = new List<int>();
            foreach (DataGridViewRow row in dgvDevices.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    int devId = Convert.ToInt32(row.Cells[1].Value);
                    led.Devices.Add(devId);
                }
            }
            led.DynAreas = new List<AreaInfo>();
            if (ckbArea1.Checked)
            {
                AreaInfo area = new AreaInfo();
                area.AreaId = 0;
                area.Interval = (int)P1_numInterval.Value;
                area.LID = _Lid;
                area.Point_X = (int)P1_PointX.Value;
                area.Point_Y = (int)P1_PointY.Value;
                area.Width = (int)P1_numWidth.Value;
                area.Height = (int)P1_numHeight.Value;
                if (!P1_ckbBorder.Checked)
                {
                    area.BordreType = 255;
                    area.BorderNo = 0;
                }
                else
                {
                    area.BordreType = P1_radbtn_DYAreaFSingle.Checked ? 0 : 1;
                    area.BorderNo = P1_radbtn_DYAreaFSingle.Checked ? (int)P1_spnedt_DYAreaFSingle.Value : (int)P1_spnedt_DYAreaFMuli.Value;
                }
                area.BorderLength = (int)P1_spnedt_DYAreaFMoveStep.Value;
                area.BorderSpeed = (int)P1_spnedt_DYAreaFRunSpeed.Value;
                area.BorderEffect = P1_cbb_DYAreaFStunt.SelectedIndex;
                area.TextFont = P1_cbbFont.Text;
                area.TextFontSize = Convert.ToInt32(P1_cbbFontSize.ComboBoxEx.SelectedItem.ToString());
                area.TextBold = P1_Bold.Checked ? 1 : 0;
                area.Text = P1_ListBox.Items[0].ToString();
                if (area.Text.Trim().Equals(string.Empty))
                {
                    MessageBox.Show("请输入要显示的内容!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                area.SingleLine = P1_SingleLine.Checked ? 1 : 0;
                area.DisplayEffect = P1_DisplayEffect.SelectedIndex;
                area.Speed = P1_cbbSpeed.SelectedIndex + 1;
                area.Stay = (int)P1_StayTime.Value;
                led.DynAreas.Add(area);
            }
            if (ckbArea2.Checked)
            {
                AreaInfo area = new AreaInfo();
                area.AreaId = 1;
                area.Interval = (int)P2_numInterval.Value;
                area.LID = _Lid;
                area.Point_X = (int)P2_PointX.Value;
                area.Point_Y = (int)P2_PointY.Value;
                area.Width = (int)P2_numWidth.Value;
                area.Height = (int)P2_numHeight.Value;
                if (!P2_ckbBorder.Checked)
                {
                    area.BordreType = 255;
                    area.BorderNo = 0;
                }
                else
                {
                    area.BordreType = P2_radbtn_DYAreaFSingle.Checked ? 0 : 1;
                    area.BorderNo = P2_radbtn_DYAreaFSingle.Checked ? (int)P2_spnedt_DYAreaFSingle.Value : (int)P2_spnedt_DYAreaFMuli.Value;
                }
                area.BorderLength = (int)P2_spnedt_DYAreaFMoveStep.Value;
                area.BorderSpeed = (int)P2_spnedt_DYAreaFRunSpeed.Value;
                area.BorderEffect = P2_cbb_DYAreaFStunt.SelectedIndex;
                area.TextFont = P2_cbbFont.Text;
                area.TextFontSize = Convert.ToInt32(P2_cbbFontSize.ComboBoxEx.SelectedItem.ToString());
                area.TextBold = P2_Bold.Checked ? 1 : 0;
                area.Text = P2_ListBox.Items[0].ToString();
                if (area.Text.Trim().Equals(string.Empty))
                {
                    MessageBox.Show("请输入要显示的内容!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                area.SingleLine = P2_SingleLine.Checked ? 1 : 0;
                area.DisplayEffect = P2_DisplayEffect.SelectedIndex;
                area.Speed = P2_cbbSpeed.SelectedIndex + 1;
                area.Stay = (int)P2_StayTime.Value;
                led.DynAreas.Add(area);
            }
            if (ckbArea3.Checked)
            {
                AreaInfo area = new AreaInfo();
                area.AreaId = 2;
                area.Interval = (int)P3_numInterval.Value;
                area.LID = _Lid;
                area.Point_X = (int)P3_PointX.Value;
                area.Point_Y = (int)P3_PointY.Value;
                area.Width = (int)P3_numWidth.Value;
                area.Height = (int)P3_numHeight.Value;
                if (!P3_ckbBorder.Checked)
                {
                    area.BordreType = 255;
                    area.BorderNo = 0;
                }
                else
                {
                    area.BordreType = P3_radbtn_DYAreaFSingle.Checked ? 0 : 1;
                    area.BorderNo = P3_radbtn_DYAreaFSingle.Checked ? (int)P3_spnedt_DYAreaFSingle.Value : (int)P3_spnedt_DYAreaFMuli.Value;
                }
                area.BorderLength = (int)P3_spnedt_DYAreaFMoveStep.Value;
                area.BorderSpeed = (int)P3_spnedt_DYAreaFRunSpeed.Value;
                area.BorderEffect = P3_cbb_DYAreaFStunt.SelectedIndex;
                area.TextFont = P3_cbbFont.Text;
                area.TextFontSize = Convert.ToInt32(P3_cbbFontSize.ComboBoxEx.SelectedItem.ToString());
                area.TextBold = P3_Bold.Checked ? 1 : 0;
                area.Text = P3_ListBox.Items[0].ToString();
                if (area.Text.Trim().Equals(string.Empty))
                {
                    MessageBox.Show("请输入要显示的内容!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                area.SingleLine = P3_SingleLine.Checked ? 1 : 0;
                area.DisplayEffect = P3_DisplayEffect.SelectedIndex;
                area.Speed = P3_cbbSpeed.SelectedIndex + 1;
                area.Stay = (int)P3_StayTime.Value;
                led.DynAreas.Add(area);
            }
            try
            {
                LedDbService.UpdateLEDController(led);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"更新Led控制器信息失败:{ex.Message}");
            }
        }
        #endregion

        private void P1_ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.P1_ListBox.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string fileName = P1_ListBox.Items[index].ToString();
                string type = fileName.Substring(fileName.LastIndexOf(".")).ToUpper();
                switch (type)
                {
                    case ".TXT":
                        LedTxt ledTxt = new LedTxt(fileName);
                        ledTxt.ShowDialog();
                        break;
                    case ".XML":
                        LedTable ledTable = new LedTable(fileName);
                        ledTable.ShowDialog();
                        break;
                }
            }
        }

        private void P2_ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.P2_ListBox.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string fileName = P2_ListBox.Items[index].ToString();
                string type = fileName.Substring(fileName.LastIndexOf(".")).ToUpper();
                switch (type)
                {
                    case ".TXT":
                        LedTxt ledTxt = new LedTxt(fileName);
                        ledTxt.ShowDialog();
                        break;
                    case ".XML":
                        LedTable ledTable = new LedTable(fileName);
                        ledTable.ShowDialog();
                        break;
                }
            }
        }

        private void P3_ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.P3_ListBox.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string fileName = P3_ListBox.Items[index].ToString();
                string type = fileName.Substring(fileName.LastIndexOf(".")).ToUpper();
                switch (type)
                {
                    case ".TXT":
                        LedTxt ledTxt = new LedTxt(fileName);
                        ledTxt.ShowDialog();
                        break;
                    case ".XML":
                        LedTable ledTable = new LedTable(fileName);
                        ledTable.ShowDialog();
                        break;
                }
            }
        }

        private void P1_Table_Click(object sender, EventArgs e)
        {
            P1_Text.Checked = false;
            P1_Table.Checked = true;
            P1_ListBox.Items.Clear();
            P1_ListBox.Items.Add($"XmlFile{_Lid}0.XML");
        }

        private void P1_Text_Click(object sender, EventArgs e)
        {
            P1_Text.Checked = true;
            P1_Table.Checked = false;
            P1_ListBox.Items.Clear();
            P1_ListBox.Items.Add($"TxtFile{_Lid}0.TXT");
        }

        private void P2_Text_Click(object sender, EventArgs e)
        {
            P2_Text.Checked = true;
            P2_Table.Checked = false;
            P2_ListBox.Items.Clear();
            P2_ListBox.Items.Add($"TxtFile{_Lid}1.TXT");
        }

        private void P2_Table_Click(object sender, EventArgs e)
        {
            P2_Text.Checked = false;
            P2_Table.Checked = true;
            P2_ListBox.Items.Clear();
            P2_ListBox.Items.Add($"XmlFile{_Lid}1.XML");
        }

        private void P3_Table_Click(object sender, EventArgs e)
        {
            P3_Text.Checked = false;
            P3_Table.Checked = true;
            P3_ListBox.Items.Clear();
            P3_ListBox.Items.Add($"XmlFile{_Lid}1.XML");
        }

        private void P3_Text_Click(object sender, EventArgs e)
        {
            P3_Text.Checked = true;
            P3_Table.Checked = false;
            P3_ListBox.Items.Clear();
            P3_ListBox.Items.Add($"TxtFile{_Lid}1.TXT");
        }
    }
}
