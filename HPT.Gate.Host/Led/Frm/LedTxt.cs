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
                MessageBox.Show("���ز�������ͳ���б�ʧ��:" + ex.Message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadTxt(this._FilePath);
        }

        /// <summary>
        /// �����ı�����
        /// </summary>
        /// <param name="p"></param>
        private void LoadTxt(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    //��ȡָ�����ı��ļ�,��֧�����ı����ַ� 
                    StreamReader objReader = new StreamReader(filePath, System.Text.Encoding.GetEncoding("gb2312"));
                    string aa = objReader.ReadToEnd();
                    //StreamReader.ReadToEnd�������ȷʵ�ܵ���һ���ӾͶ����ˣ�̫������!!
                    objReader.Close();//�ر���  
                    curRichTextBox.Text = aa;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("�����ı��ļ�ʧ��:" + ex.Message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("û�пɲ����Ķ�̬����!", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string msg = string.Empty;
            if (curRichTextBox.Text.Trim().Length != 0)
            {
                msg = "\r\n" + msg;
            }
            msg += $"{comboBox5.Text}:��{comboBox5.SelectedValue}��";
            curRichTextBox.SelectionLength = 0;
            curRichTextBox.SelectedText = msg;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// �ı�����
        /// </summary>
        /// <param name="fontName"></param>
        private void ChangeFont(String fontName)
        {

            if (fontName == "")

                throw new System.InvalidProgramException("�����������");

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
        /// ���������С
        /// </summary>
        /// <param name="fontSize"></param>
        private void ChangFontSize(float fontSize)
        {

            if (fontSize <= 0.0)

                throw new InvalidProgramException("�ֺŲ�������");

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
        /// ���ȥ�������ʽ(�Ӵ�,�»��ߵ�)
        /// </summary>
        /// <param name="fontstyle"></param>
        private void RemoveFontStyle(FontStyle fontstyle)
        {

            if (fontstyle != FontStyle.Bold && fontstyle != FontStyle.Italic && fontstyle != FontStyle.Underline && fontstyle != FontStyle.Strikeout && fontstyle != FontStyle.Regular)

                throw new System.InvalidProgramException("�����ʽ����");

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
                MessageBox.Show("�����ı��ļ�ʧ��:" + ex.Message, "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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