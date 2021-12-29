using hpt.gate.CardReader;
using HPT.Gate.Client.config;
using HPT.Gate.Utils.Common;
using Joey.UserControls;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HPT.Gate.Client.emp
{
    public partial class FSetCardPass : DevComponents.DotNetBar.Office2007Form
    {
        public bool _NeeddSynFlag = false;
        public bool _NeedSaveFlag = false;

        public FSetCardPass()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FSetCardPass_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            checkBoxX1.Checked = AppSettings.EnableWriteCard;
            textBox2.Text = AppSettings.CardPass;
            comboBox4.Text = AppSettings.SectorNo.ToString();
            textBox3.Text = textBox2.Text;
            checkBox1.Checked = AppSettings.AntiSubmarineWarfare;
            checkBox2.Checked = AppSettings.EnableLimitedTimes;
            numericUpDown1.Value = AppSettings.LimitedTimes;
            CheckConfigChange();
        }

        private void CheckConfigChange()
        {
            checkBoxX1.CheckedChanged += ConfigChanged;
            textBox2.TextChanged += ConfigChanged;
            comboBox4.SelectedValueChanged += ConfigChanged;
            checkBox1.CheckedChanged += ConfigChanged;
            checkBox2.CheckedChanged += ConfigChanged;
            numericUpDown1.ValueChanged += ConfigChanged;
        }

        private void ConfigChanged(object sender, EventArgs e)
        {
            _NeeddSynFlag = true;
            _NeedSaveFlag = true;
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBoxX1.Checked;
            groupBox4.Enabled = checkBoxX1.Checked;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (!CheckPassIsValidated())
            {
                return;
            }
        }

        private void SaveConfig()
        {
            AppSettingsHelper.Set("EnableWriteCard", checkBoxX1.Checked ? "true" : "false");
            AppSettingsHelper.Set("CardPass", textBox2.Text);
            AppSettingsHelper.Set("SectorNo", comboBox4.Text);
            AppSettingsHelper.Set("AntiSubmarineWarfare", checkBox1.Checked ? "true" : "false");
            AppSettingsHelper.Set("EnableLimitedTimes", checkBox2.Checked ? "true" : "false");
            AppSettingsHelper.Set("LimitedTimes", numericUpDown1.Value.ToString());
            _NeedSaveFlag = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CheckPassIsValidated();
        }

        private bool CheckPassIsValidated()
        {
            if (!Regex.IsMatch(textBox2.Text, "^[0-9A-Fa-f]+$"))
            {
                MessageBoxHelper.Info("卡密码只支持数字与子母的组合!");
                return false;
            }
            textBox3.Text = textBox2.Text;
            return true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (_NeedSaveFlag)
            {
                SaveConfig();
            }
            DialogResult = DialogResult.OK;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (!ICCardReader.Active)
            {
                ICCardReader.InitCardReader();
            }
            if (ICCardReader.SetCardPassword(textBox1.Text, textBox3.Text))
            {
                MessageBoxHelper.Info("IC卡加密成功!");
            }
            else
            {
                MessageBoxHelper.Info("IC卡加密失败!");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBox2.Checked;
        }
    }
}
