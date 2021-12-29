using System;
using System.Windows.Forms;
using HPT.Gate.Client.config;
using HPT.Gate.DataAccess.Entity.Service;
using Joey.UserControls;

namespace HPT.Gate.Client.db
{
    public partial class FrmDBConfig : DevComponents.DotNetBar.Office2007Form
    {
        public FrmDBConfig()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveConfig();
            this.Close();
        }

        #region 保存配置文件
        private void SaveConfig()
        {
            AppSettings.ServerName = tbServer.Text;
            AppSettings.DBName = tbDbName.Text;
            AppSettings.UserName = tbUserName.Text;
            AppSettings.Password = tbPass.Text;
            AppSettings.LoginType = "1";
        }

        #endregion

        private void buttonX3_Click(object sender, EventArgs e)
        {
            TestConection();
        }

        #region 测试连接
        private void TestConection()
        {
            SaveConfig();
            if (SystemService.TestConnect(AppSettings.OLEConnectString))
            {
                MessageBoxHelper.Info("数据库测试连接成功!");
            }
            else
            {
                MessageBoxHelper.Info("数据库连接失败!");
            }
        }
        #endregion

        private void FrmDBConfig_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        #region 加载配置文件
        private void LoadConfig()
        {
            tbServer.Text = AppSettings.ServerName;
            tbDbName.Text = AppSettings.DBName;
            tbUserName.Text = AppSettings.UserName;
            tbPass.Text = AppSettings.Password;
        }

        #endregion

    }
}
