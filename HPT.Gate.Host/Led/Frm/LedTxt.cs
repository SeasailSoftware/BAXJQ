using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HPT.Gate.Host.Base;
using hpt.gate.Util;

namespace hpt.gate.Led.Frm
{
    public partial class LedTxt : WinBase
    {

        public int curRtbStart = 0;
        public int len = 0;
        public string _FilePath = string.Empty;
        public LedTxt(string fileName)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            string directory = Path.Combine(Environment.CurrentDirectory, "Led");
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            _FilePath = Path.Combine(directory, fileName);
            if (!File.Exists(_FilePath))
            {
                File.Create(_FilePath).Close();
            }
        }

        private void LedTxt_Load(object sender, EventArgs e)
        {
            try
            {
                ComboBoxHelper.FillLedDynParas(comboBox5);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门人数统计列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadTxt(this._FilePath);
        }

        /// <summary>
        /// 加载文本内容
        /// </summary>
        /// <param name="p"></param>
        private void LoadTxt(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    //读取指定的文本文件,并支持中文编码字符 
                    StreamReader objReader = new StreamReader(filePath, System.Text.Encoding.GetEncoding("gb2312"));
                    string aa = objReader.ReadToEnd();
                    //StreamReader.ReadToEnd这个方法确实很吊，一下子就读完了，太给力了!!
                    objReader.Close();//关闭流  
                    curRichTextBox.Text = aa;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载文本文件失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (comboBox5.Items.Count == 0)
            {
                MessageBox.Show("没有可操作的动态参数!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string msg = string.Empty;
            if (curRichTextBox.Text.Trim().Length != 0)
            {
                msg = "\r\n" + msg;
            }
            msg += $"{comboBox5.Text}:【{comboBox5.SelectedValue}】";
            curRichTextBox.SelectionLength = 0;
            curRichTextBox.SelectedText = msg;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 改变字体
        /// </summary>
        /// <param name="fontName"></param>
        private void ChangeFont(String fontName)
        {

            if (fontName == "")

                throw new System.InvalidProgramException("字体参数错误");

            curRtbStart = curRichTextBox.SelectionStart;

            len = curRichTextBox.SelectionLength;



            RichTextBox tempRichTextBox = new RichTextBox();

            int tempRtbStart = 0;

            System.Drawing.Font font = curRichTextBox.SelectionFont;

            if (len <= 1 && font != null)
            {

                curRichTextBox.SelectionFont = new System.Drawing.Font(fontName, font.Size, font.Style);

                return;

            }

            tempRichTextBox.Rtf = curRichTextBox.SelectedRtf;

            for (int i = 0; i < len; i++)
            {

                tempRichTextBox.Select(tempRtbStart + i, 1);

                tempRichTextBox.SelectionFont = new System.Drawing.Font(fontName, tempRichTextBox.SelectionFont.Size, tempRichTextBox.SelectionFont.Style);

            }

            tempRichTextBox.Select(tempRtbStart, len);

            curRichTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;

            curRichTextBox.Select(curRtbStart, len);

            curRichTextBox.Focus();

        }

        /// <summary>
        /// 设置字体大小
        /// </summary>
        /// <param name="fontSize"></param>
        private void ChangFontSize(float fontSize)
        {

            if (fontSize <= 0.0)

                throw new InvalidProgramException("字号参数错误");

            curRtbStart = curRichTextBox.SelectionStart;

            len = curRichTextBox.SelectionLength;

            RichTextBox tempRichTextBox = new RichTextBox();

            int tempRtbStart = 0;

            System.Drawing.Font font = curRichTextBox.SelectionFont;

            if (len <= 1 && font != null)
            {

                curRichTextBox.SelectionFont = new System.Drawing.Font(font.Name, fontSize, font.Style);

                return;

            }

            tempRichTextBox.Rtf = curRichTextBox.SelectedRtf;

            for (int i = 0; i < len; i++)
            {

                tempRichTextBox.Select(tempRtbStart + i, 1);

                tempRichTextBox.SelectionFont = new System.Drawing.Font(tempRichTextBox.SelectionFont.Name, fontSize, tempRichTextBox.SelectionFont.Style);

            }

            tempRichTextBox.Select(tempRtbStart, len);

            curRichTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;

            curRichTextBox.Select(curRtbStart, len);

            curRichTextBox.Focus();

        }

        /// <summary>
        /// 添加去除字体格式(加粗,下划线等)
        /// </summary>
        /// <param name="fontstyle"></param>
        private void RemoveFontStyle(FontStyle fontstyle)
        {

            if (fontstyle != FontStyle.Bold && fontstyle != FontStyle.Italic && fontstyle != FontStyle.Underline && fontstyle != FontStyle.Strikeout && fontstyle != FontStyle.Regular)

                throw new System.InvalidProgramException("字体格式错误");

            RichTextBox tempRichTextBox = new RichTextBox();

            int tempRtbStart = 0;

            System.Drawing.Font font = curRichTextBox.SelectionFont;



            if (len <= 1 && font != null)
            {

                curRichTextBox.SelectionFont = new System.Drawing.Font(font, font.Style ^ fontstyle);

                return;

            }

            tempRichTextBox.Rtf = curRichTextBox.SelectedRtf;

            for (int i = 0; i < len; i++)
            {

                tempRichTextBox.Select(tempRtbStart + i, 1);

                tempRichTextBox.SelectionFont = new System.Drawing.Font(tempRichTextBox.SelectionFont,

                     tempRichTextBox.SelectionFont.Style ^ fontstyle);

            }

            tempRichTextBox.Select(tempRtbStart, len);

            curRichTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;

            curRichTextBox.Select(curRtbStart, len);

            curRichTextBox.Focus();

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(_FilePath))
                {
                    File.Delete(_FilePath);
                }
                File.AppendAllText(_FilePath, curRichTextBox.Text, Encoding.Default);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存文本文件失败:" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}