using HPT.Gate.Client.config;
using HPT.Gate.Client.Tools;
using Joey.UserControls;
using System;
using System.Windows.Forms;

namespace hpt.gate.dataImport
{
    public partial class FrmImportSettings : JForm
    {
        public FrmImportSettings()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        #region 保存配置文件
        private void SaveConfig()
        {
            #region 导入设置
            AppSettings.DeptImportType = cbbDept.SelectedIndex;
            AppSettings.EmpImportType = cbbEmp.SelectedIndex;
            AppSettings.CardImportType = cbbCardNo.SelectedIndex;
            AppSettings.CardType = cbbCardType.SelectedIndex;
            #endregion

            #region 卡属性

            AppSettings.TicketType = (int)cbbTicketType.SelectedValue;
            AppSettings.BeginDate = dtpBegin.Text;
            AppSettings.EndDate = dtpEnd.Text;
            #endregion

        }

        #endregion

        private void FrmImportSettings_Load(object sender, EventArgs e)
        {
            LoadDefaultPara();
            LoadConfig();
        }

        #region 加载默认参数
        private void LoadDefaultPara()
        {
            try
            {
                ComboBoxHelper.FillTicketTypeCombobox(cbbTicketType);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载票类失败:{ex.Message}");
                return;
            }
        }


        #endregion

        #region 加载配置文件
        private void LoadConfig()
        {
            #region 导入设置
            cbbDept.SelectedIndex = AppSettings.DeptImportType;
            cbbEmp.SelectedIndex = AppSettings.EmpImportType;
            cbbCardNo.SelectedIndex = AppSettings.CardImportType;
            cbbCardType.SelectedIndex = AppSettings.CardType;
            #endregion

            #region 卡属性设置
            cbbTicketType.SelectedValue = AppSettings.TicketType;
            dtpBegin.Text = AppSettings.BeginDate;
            dtpEnd.Text = AppSettings.EndDate;
            #endregion

        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
            DialogResult = DialogResult.OK;
        }
    }
}
