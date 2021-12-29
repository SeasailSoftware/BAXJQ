using hpt.gate.DbTools.Service;
using hpt.gate.Led.Frm;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Host.Base;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hpt.gate.led
{
    public partial class FrmLedAdd : WinBase
    {
        private int _Lid;
        public FrmLedAdd()
        {
            InitializeComponent();
        }

        private void FrmLedAdd_Load(object sender, EventArgs e)
        {
            LoadDevices();
            _Lid = LedDbService.GetSuitableLid();
            LoadDefaultPara();
        }

        private void LoadDevices()
        {
            Task.Factory.StartNew(() =>
            {
                List<DeviceInfo> devices = DeviceInfoService.ToList();
                this.Invoke(new Action(() =>
                {
                    foreach (DeviceInfo device in devices)
                    {
                        int rowIndex = dgvDevices.Rows.Add();
                        dgvDevices.Rows[rowIndex].Cells[1].Value = device.DeviceId;
                        dgvDevices.Rows[rowIndex].Cells[2].Value = device.DeviceName;
                        dgvDevices.Rows[rowIndex].Cells[3].Value = device.Mac;
                    }
                }));
            });
        }

        #region 加载默认参数
        private void LoadDefaultPara()
        {

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

        private void P1_ckbBorder_CheckedChanged(object sender, EventArgs e)
        {
            P1_gbBord.Enabled = P1_ckbBorder.Checked;
        }

        private void P1_Text_Click(object sender, EventArgs e)
        {
            P1_Text.Checked = true;
            P1_Table.Checked = false;
            P1_ListBox.Items.Clear();
            P1_ListBox.Items.Add($"TxtFile{_Lid}0.TXT");
        }

        private void P1_Table_Click(object sender, EventArgs e)
        {
            P1_Text.Checked = false;
            P1_Table.Checked = true;
            P1_ListBox.Items.Clear();
            P1_ListBox.Items.Add($"XmlFile{_Lid}0.XML");
        }

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

        private void P2_ckbBorder_CheckedChanged(object sender, EventArgs e)
        {
            P2_gbBorder.Enabled = P2_ckbBorder.Checked;
        }

        private void P1_Bold_Click(object sender, EventArgs e)
        {
            P1_Bold.Checked = !P1_Bold.Checked;
        }

        private void P2_Bold_Click(object sender, EventArgs e)
        {
            P2_Bold.Checked = !P2_Bold.Checked;
        }

        private void P3_Bold_Click(object sender, EventArgs e)
        {
            P3_Bold.Checked = !P3_Bold.Checked;
        }

        private void P3_Text_Click(object sender, EventArgs e)
        {
            P3_Text.Checked = true;
            P3_Table.Checked = false;
            P3_ListBox.Items.Clear();
            P3_ListBox.Items.Add($"TxtFile{_Lid}1.TXT");
        }

        private void P3_Table_Click(object sender, EventArgs e)
        {
            P3_Text.Checked = false;
            P3_Table.Checked = true;
            P3_ListBox.Items.Clear();
            P3_ListBox.Items.Add($"XmlFile{_Lid}1.XML");
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

        private void P3_ckbBorder_CheckedChanged(object sender, EventArgs e)
        {
            P3_gbBorder.Enabled = P3_ckbBorder.Checked;
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            SaveController();
        }

        #region 添加控制卡
        private void SaveController()
        {
            LedController controller = new LedController();
            controller.Lid = (int)numPNo.Value;
            controller.ControlType = cbbLedType.SelectedIndex;
            controller.Protocol = cbbProtocol.SelectedIndex;
            controller.Width = (int)numWidth.Value;
            controller.Heigth = (int)numHeight.Value;
            controller.IPaddress = tbIPAddress.Text.Trim();
            controller.Port = (int)numPort.Value;
            controller.Devices = new List<int>();
            foreach (DataGridViewRow row in dgvDevices.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    int devId = Convert.ToInt32(row.Cells[1].Value);
                    controller.Devices.Add(devId);
                }
            }
            controller.DynAreas = new List<AreaInfo>();
            AreaInfo area1 = GetArea1();
            if (area1 != null) controller.DynAreas.Add(area1);
            AreaInfo area2 = GetArea2();
            if (area2 != null) controller.DynAreas.Add(area2);
            AreaInfo area3 = GetArea3();
            if (area3 != null) controller.DynAreas.Add(area3);

            string msg;
            if (LedDbService.AddController(controller, out msg))
                DialogResult = DialogResult.OK;
            else
                MessageBoxHelper.Info($"添加Led控制卡失败:{msg}");
        }
        #endregion

        #region 删除控制卡
        private void DeleteController(int lid)
        {
            CollectService.DeleteLedController(lid);
        }
        #endregion

        #region 保存动态区域一
        private AreaInfo GetArea1()
        {
            if (!ckbArea1.Checked) return null;
            AreaInfo area = new AreaInfo();
            area.AreaId = 0;
            area.Interval = (int)P1_numInterval.Value;
            area.LID = (int)numPNo.Value;
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
            area.SingleLine = P1_SingleLine.Checked ? 1 : 0;
            area.DisplayEffect = P1_DisplayEffect.SelectedIndex;
            area.Speed = P1_cbbSpeed.SelectedIndex + 1;
            area.Stay = (int)P1_StayTime.Value;
            return area;
        }
        #endregion

        #region 删除动态区域
        private void DeleteArea(int lid, int areaId)
        {
            LedDbService.DeleteArea(lid, areaId);
        }
        #endregion

        #region 保存动态区域二
        private AreaInfo GetArea2()
        {
            if (!ckbArea2.Checked) return null;
            AreaInfo area = new AreaInfo();
            area.AreaId = 1;
            area.Interval = (int)P2_numInterval.Value;
            area.LID = (int)numPNo.Value;
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
            area.SingleLine = P2_SingleLine.Checked ? 1 : 0;
            area.DisplayEffect = P2_DisplayEffect.SelectedIndex;
            area.Speed = P2_cbbSpeed.SelectedIndex + 1;
            area.Stay = (int)P2_StayTime.Value;
            return area;
        }
        #endregion

        #region 保存动态区域三
        private AreaInfo GetArea3()
        {
            if (!ckbArea3.Checked) return null;
            AreaInfo area = new AreaInfo();
            area.AreaId = 2;
            area.Interval = (int)P3_numInterval.Value;
            area.LID = (int)numPNo.Value;
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
            area.SingleLine = P3_SingleLine.Checked ? 1 : 0;
            area.DisplayEffect = P3_DisplayEffect.SelectedIndex;
            area.Speed = P3_cbbSpeed.SelectedIndex + 1;
            area.Stay = (int)P3_StayTime.Value;
            return area;
        }
        #endregion

        private void ckbDeviceAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvDevices.Rows)
            {
                row.Cells[0].Value = ckbDeviceAll.Checked;
            }
        }
    }
}
