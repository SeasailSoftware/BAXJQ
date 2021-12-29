using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using HPT.Gate.DataAccess.Entity;
using hpt.gate.DbTools.Service;
using HPT.Gate.Host.Base;

namespace hpt.gate.Led.Frm
{
    public partial class FDynamicArea : WinBase
    {

        /// <summary>
        /// 新增还是修改标志
        /// </summary>
        public int _Flag;

        /// <summary>
        /// 区域编号
        /// </summary>
        public int _AreaId;

        /// <summary>
        /// LED控制卡编号
        /// </summary>
        public int _LID;
        /// <summary>
        /// 新增构造函数
        /// </summary>
        public FDynamicArea(int lid)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _Flag = 0;
            this._LID = lid;
            LoadFontSize();
            CheckUpdate();
            try
            {
                this._AreaId = LedDbService.GetAreaId(lid);
                numericUpDown2.Value = this._AreaId;
                cbb_DYAreaFStunt.SelectedIndex = 1;
                LoadSystemFont();
                LoadFontSize();
                comboBox2.SelectedIndex = 2;
                comboBox3.SelectedIndex = 9;
                checkBox3.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取新增动态区域编号失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #region 检查更新
        private void CheckUpdate()
        {
            try
            {
                LedDbService.CheckLedUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("检查Led更新失败:" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        /// <summary>
        /// 修改动态区域构造函数
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="areaId"></param>
        public FDynamicArea(int lid, int areaId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _Flag = 1;
            this._AreaId = areaId;
            this._LID = lid;
            CheckUpdate();
            LoadSystemFont();
            LoadFontSize();
            try
            {
                AreaInfo area = LedDbService.GetAreaInfo(this._LID, this._AreaId);
                if (area != null)
                {
                    numericUpDown2.Value = this._AreaId;
                    numericUpDown4.Value = area.Point_X;
                    numericUpDown5.Value = area.Point_Y;
                    numericUpDown6.Value = area.Width;
                    numericUpDown7.Value = area.Height;
                    checkBox3.Checked = area.BordreType != 255;
                    radbtn_DYAreaFSingle.Checked = area.BordreType == 0;
                    spnedt_DYAreaFSingle.Value = radbtn_DYAreaFSingle.Checked ? area.BorderNo : 0;
                    radbtn_DYAreaFMuli.Checked = area.BordreType == 1;
                    spnedt_DYAreaFMuli.Value = radbtn_DYAreaFMuli.Checked ? area.BorderNo : 0;
                    spnedt_DYAreaFMoveStep.Value = area.BorderLength;
                    spnedt_DYAreaFRunSpeed.Value = area.BorderSpeed;
                    cbb_DYAreaFStunt.SelectedIndex = area.BorderEffect;
                    buttonItem1.Checked = area.TextBold == 1;
                    string fileType = area.Text.Substring(area.Text.LastIndexOf("."));
                    if (!fileType.Equals(string.Empty))
                    {
                        switch (fileType.ToUpper())
                        {
                            case ".TXT":
                                buttonItem4.Checked = true;
                                buttonItem5.Checked = false;
                                break;
                            case ".XML":
                                buttonItem4.Checked = false;
                                buttonItem5.Checked = true;
                                break;
                        }
                    }
                    listBox1.Items.Add(area.Text);
                    comboBoxItem1.ComboBoxEx.SelectedItem = area.TextFont;
                    comboBoxItem2.ComboBoxEx.SelectedItem = area.TextFontSize.ToString();
                    ///checkBox1.Checked = area.TextBold == 1;
                    checkBox2.Checked = area.SingleLine == 1;
                    comboBox2.SelectedIndex = area.DisplayEffect;
                    comboBox3.SelectedIndex = area.Speed - 1;
                    numericUpDown8.Value = area.Stay;
                    numInterval.Value = area.Interval;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载动态区域信息失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void FDynamicArea_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 加载字体大小
        /// </summary>
        public void LoadFontSize()
        {
            comboBoxItem2.ComboBoxEx.Items.Clear();
            comboBoxItem2.ComboBoxEx.Items.Add("8");
            comboBoxItem2.ComboBoxEx.Items.Add("9");
            comboBoxItem2.ComboBoxEx.Items.Add("10");
            comboBoxItem2.ComboBoxEx.Items.Add("11");
            comboBoxItem2.ComboBoxEx.Items.Add("12");
            comboBoxItem2.ComboBoxEx.Items.Add("14");
            comboBoxItem2.ComboBoxEx.Items.Add("16");
            comboBoxItem2.ComboBoxEx.Items.Add("18");
            comboBoxItem2.ComboBoxEx.Items.Add("20");
            comboBoxItem2.ComboBoxEx.Items.Add("22");
            comboBoxItem2.ComboBoxEx.Items.Add("24");
            comboBoxItem2.ComboBoxEx.Items.Add("26");
            comboBoxItem2.ComboBoxEx.Items.Add("28");
            comboBoxItem2.ComboBoxEx.Items.Add("36");
            comboBoxItem2.ComboBoxEx.Items.Add("48");
            comboBoxItem2.ComboBoxEx.Items.Add("72");
            comboBoxItem2.ComboBoxEx.Items.Add("78");
            comboBoxItem2.ComboBoxEx.Items.Add("100");
            comboBoxItem2.ComboBoxEx.Items.Add("110");
            comboBoxItem2.ComboBoxEx.Items.Add("120");
            comboBoxItem2.ComboBoxEx.Items.Add("128");
            comboBoxItem2.ComboBoxEx.Items.Add("200");
            comboBoxItem2.ComboBoxEx.SelectedItem = "10";
        }

        public void LoadSystemFont()
        {
            ///加载字体
            try
            {
                InstalledFontCollection MyFont = new InstalledFontCollection();
                FontFamily[] MyFontFamilies = MyFont.Families;
                comboBoxItem1.ComboBoxEx.Items.Clear();
                foreach (FontFamily s in MyFont.Families)
                {
                    comboBoxItem1.ComboBoxEx.Items.Add(s.Name);
                }
                comboBoxItem1.ComboBoxEx.SelectedItem = "宋体";
                /*
                ArrayList list = new ArrayList();
                int Count = MyFontFamilies.Length;
                for (int i = 0; i < Count; i++)
                {
                    string FontName = MyFontFamilies[i].Name;
                    list.add(FontName);
                } 
                 * */
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载系统字体列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            AreaInfo area = new AreaInfo();
            area.AreaId = this._AreaId;
            area.Interval = (int)numInterval.Value;
            area.LID = this._LID;
            area.Point_X = (int)numericUpDown4.Value;
            area.Point_Y = (int)numericUpDown5.Value;
            area.Width = (int)numericUpDown6.Value;
            area.Height = (int)numericUpDown7.Value;
            if (!checkBox3.Checked)
            {
                area.BordreType = 255;
                area.BorderNo = 0;
            }
            else
            {
                area.BordreType = radbtn_DYAreaFSingle.Checked ? 0 : 1;
                area.BorderNo = radbtn_DYAreaFSingle.Checked ? (int)spnedt_DYAreaFSingle.Value : (int)spnedt_DYAreaFMuli.Value;
            }
            area.BorderLength = (int)spnedt_DYAreaFMoveStep.Value;
            area.BorderSpeed = (int)spnedt_DYAreaFRunSpeed.Value;
            area.BorderEffect = cbb_DYAreaFStunt.SelectedIndex;
            area.TextFont = comboBoxItem1.ComboBoxEx.Text;
            area.TextFontSize = Convert.ToInt32(comboBoxItem2.ComboBoxEx.Text);
            area.TextBold = buttonItem1.Checked ? 1 : 0;
            area.Text = listBox1.Items[0].ToString();
            if (area.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("请输入要显示的内容!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            area.SingleLine = checkBox2.Checked ? 1 : 0;
            area.DisplayEffect = comboBox2.SelectedIndex;
            area.Speed = comboBox3.SelectedIndex + 1;
            area.Stay = (int)numericUpDown8.Value;
            switch (this._Flag)
            {
                case 0:
                    try
                    {
                        LedDbService.AddDynamicArea(area);
                        MessageBox.Show("动态区域安装成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("添加动态区域失败：" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case 1:
                    try
                    {
                        LedDbService.UpdateDynamicArea(area);
                        MessageBox.Show("更新动态区域信息成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("更新动态区域信息失败：" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
            }
        }



        private void buttonItem4_Click(object sender, EventArgs e)
        {
            buttonItem4.Checked = true;
            buttonItem5.Checked = false;
            listBox1.Items.Clear();
            listBox1.Items.Add("TxtFile" + this._LID + this._AreaId + ".TXT");
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            buttonItem4.Checked = false;
            buttonItem5.Checked = true;
            listBox1.Items.Clear();
            listBox1.Items.Add("XmlFile" + this._LID + this._AreaId + ".XML");
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string fileName = listBox1.Items[index].ToString();
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

        private void cmsContent_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int index = 0;
            string fileName = listBox1.Items[index].ToString();
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

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            buttonItem1.Checked = !buttonItem1.Checked;
        }
    }
}