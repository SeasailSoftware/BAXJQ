using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Windows.Forms;
using Joey.UserControls;
using HPT.Gate.Client.Tools;

namespace HPT.Gate.Client.cardType
{
    public partial class FrmEditTicketType :JForm
    { 
        private int _RecId;
        public FrmEditTicketType(int recId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _RecId = recId;
        }

        private void FrmAddCardType_Load(object sender, EventArgs e)
        {
            InitTimeGroup();
            LoadCardType();
        }

        #region 加载卡类型相信信息
        private void LoadCardType()
        {
            try
            {
                TicketType cardType = TicketTypeService.GetByRecId(_RecId);
                if (cardType != null)
                {
                    tbName.Text = cardType.Name;
                    cbbCardType.SelectedIndex = cardType.CardType;
                    ckbRightOfIn.Checked = cardType.InRight == 1;
                    ckbRightOfOut.Checked = cardType.OutRight == 1;
                    cbbShowPhoto.SelectedIndex = cardType.Photo;
                    cbbVacation.SelectedValue = cardType.VacationId;
                    cbbVoice.SelectedIndex = cardType.VoiceNo;
                    cbbTimegroupOfIn.SelectedValue = cardType.IntimeGroupNo;
                    cbbTimegroupOfOut.SelectedValue = cardType.OutTimeGroupNo;
                    cbbAnti.SelectedIndex = cardType.AntiSubmarine;
                    ckbTimegroupLimit.Checked = cardType.LimitEnabled == 1;
                    rbLimit1.Checked = cardType.TimegroupLimitEnabled == 1;
                    cbbLimit1.SelectedIndex = cardType.LimitTypeOfTimeGroupLimit;
                    numLimit1.Value = cardType.TimesOfTimeGroupLimit;
                    rbLimit2.Checked = cardType.EffectDateLimitEnabled == 1;
                    cbbLimit2.SelectedIndex = cardType.LimitTypeOfEffectDateLimit;
                    numLimit2.Value = cardType.TimesOfEffectDateLimit;
                    rbLimit3.Checked = cardType.LimitTimeEnabled == 1;
                    numLimit3.Value = cardType.MinutesOfLimitTime;
                    rbRow10.Checked = cardType.DisplayType1 == 0;
                    rbRow11.Checked = cardType.DisplayType1 == 1;
                    tbText1.Text = cardType.Text1;
                    cbColumn1.SelectedValue = cardType.Column1;
                    tbContent1.Text = cardType.Content1;
                    rbRow20.Checked = cardType.DisplayType2 == 0;
                    rbRow21.Checked = cardType.DisplayType2 == 1;
                    tbText2.Text = cardType.Text2;
                    cbColumn2.SelectedValue = cardType.Column2;
                    tbContent2.Text = cardType.Content2;
                    rbRow30.Checked = cardType.DisplayType3 == 0;
                    rbRow31.Checked = cardType.DisplayType3 == 1;
                    tbText3.Text = cardType.Text3;
                    cbColumn3.SelectedValue = cardType.Column3;
                    tbContent3.Text = cardType.Content3;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("获取卡类信息失败:" + ex.Message);
            }


        }

        #endregion

        #region 初始化时间组
        /// <summary>
        /// 初始化时间组
        /// </summary>
        private void InitTimeGroup()
        {
            ///节假日时间组
            try
            {
                ComboBoxHelper.FillTimeGroupOfVacation(cbbVacation);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载节假日时间组失败:" + ex.Message);
                return;
            }
            ///初始化星期时间段
            try
            {
                ComboBoxHelper.FillTimeGroupOfWeek(cbbTimegroupOfIn);
                ComboBoxHelper.FillTimeGroupOfWeek(cbbTimegroupOfOut);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载节假日时间组失败:" + ex.Message);
                return;
            }
            //初始化语音段
            try
            {
                ComboBoxHelper.FillVoiceComboBox(cbbVoice);
                cbbVoice.SelectedIndex = 7;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载语音段失败:" + ex.Message);
                return;
            }

            ///初始化显示屏
            try
            {
                ComboBoxHelper.FillCardParaCombobox(cbColumn1);
                ComboBoxHelper.FillCardParaCombobox(cbColumn2);
                ComboBoxHelper.FillCardParaCombobox(cbColumn3);
                cbColumn1.SelectedIndex = 2;
                cbColumn2.SelectedIndex = 1;
                cbColumn3.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载彩屏文字显示参数失败:" + ex.Message);
                return;
            }

            cbbShowPhoto.SelectedIndex = 0;
            cbbAnti.SelectedIndex = 0;
            cbbLimit1.SelectedIndex = 0;
            cbbLimit2.SelectedIndex = 0;

        }
        #endregion


        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        #region 保存卡类信息
        private void SaveCardTypep()
        {
            if (tbName.Text.Trim().Equals(string.Empty))
            {
                MessageBoxHelper.Info("卡类名称不能为空!");
                return;
            }
            TicketType cardType = new TicketType();
            cardType.RecId = _RecId;
            cardType.Name = tbName.Text;
            cardType.TypeId = 1;
            cardType.CardType = cbbCardType.SelectedIndex;
            cardType.InRight = ckbRightOfIn.Checked ? 1 : 0;
            cardType.OutRight = ckbRightOfOut.Checked ? 1 : 0;
            cardType.Photo = cbbShowPhoto.SelectedIndex;
            cardType.VacationId = Convert.ToInt32(cbbVacation.SelectedValue);
            cardType.VoiceNo = cbbVoice.SelectedIndex;
            cardType.IntimeGroupNo = Convert.ToInt32(cbbTimegroupOfIn.SelectedValue);
            cardType.OutTimeGroupNo = Convert.ToInt32(cbbTimegroupOfOut.SelectedValue);
            cardType.AntiSubmarine = cbbAnti.SelectedIndex;
            cardType.LimitEnabled = ckbTimegroupLimit.Checked ? 1 : 0;
            cardType.TimegroupLimitEnabled = rbLimit1.Checked ? 1 : 0;
            cardType.LimitTypeOfTimeGroupLimit = cbbLimit1.SelectedIndex;
            cardType.TimesOfTimeGroupLimit = (int)numLimit1.Value;
            cardType.EffectDateLimitEnabled = rbLimit2.Checked ? 1 : 0;
            cardType.LimitTypeOfEffectDateLimit = cbbLimit2.SelectedIndex;
            cardType.TimesOfEffectDateLimit = (int)numLimit2.Value;
            cardType.LimitTimeEnabled = rbLimit3.Checked ? 1 : 0;
            cardType.MinutesOfLimitTime = (int)numLimit3.Value;
            cardType.DisplayType1 = rbRow10.Checked ? 0 : 1;
            cardType.Text1 = tbText1.Text;
            cardType.Column1 = (int)cbColumn1.SelectedValue;
            cardType.Content1 = tbContent1.Text;
            cardType.DisplayType2 = rbRow20.Checked ? 0 : 1;
            cardType.Text2 = tbText2.Text;
            cardType.Column2 = (int)cbColumn2.SelectedValue;
            cardType.Content2 = tbContent2.Text;
            cardType.DisplayType3 = rbRow30.Checked ? 0 : 1;
            cardType.Text3 = tbText3.Text;
            cardType.Column3 = (int)cbColumn3.SelectedValue;
            cardType.Content3 = tbContent3.Text;
            try
            {
                TicketTypeService.Update(cardType);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error(string.Format("添加卡类信息失败:{0}", ex.Message));
            }
        }
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckbTimegroupLimit_CheckedChanged(object sender, EventArgs e)
        {
            pnTimegroupLimit.Enabled = ckbTimegroupLimit.Checked;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pnLimit1.Enabled = rbLimit1.Checked;
        }

        private void rbLimit2_CheckedChanged(object sender, EventArgs e)
        {
            pnLimit2.Enabled = rbLimit2.Checked;
        }

        private void rbLimit3_CheckedChanged(object sender, EventArgs e)
        {
            pnLimit3.Enabled = rbLimit3.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveCardTypep();
        }
    }
}
