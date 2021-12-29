using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.IO;
using System.Windows.Forms;
using HPT.Gate.Host.Config;

namespace HPT.Gate.Host.db
{
    public partial class FrmDBSettings : WinBase
    {
        public FrmDBSettings()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveConfig();
            if (!SystemService.TestConnect(AppSettings.ConnectString))
            {
                MessageBox.Show("测试数据库连接失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AppSettings.IsInstall = true;
            DialogResult = DialogResult.OK;
        }

        #region 保存配置文件
        private void SaveConfig()
        {
            AppSettings.ServerName = tbServer.Text;
            AppSettings.DBName = tbDbName.Text;
            AppSettings.UserName = tbUserName.Text;
            AppSettings.Password = tbPass.Text;
        }
        #endregion

        private void buttonX3_Click(object sender, EventArgs e)
        {
            SaveConfig();
            InstallDatabase();
        }

        private void FrmDBSettings_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        #region 加载配置文件
        private void LoadConfig()
        {
            AppSettings.IsInstall = false;
            tbServer.Text = AppSettings.ServerName;
            tbDbName.Text = AppSettings.DBName;
            tbDBPath.Text = string.IsNullOrWhiteSpace(AppSettings.DBPath) ? $@"{Application.StartupPath}\UserData" : AppSettings.DBPath;
            tbUserName.Text = AppSettings.UserName;
            tbPass.Text = AppSettings.Password;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string folderName = fbd.SelectedPath; //获得选择的文件夹路径

                tbDBPath.Text = folderName;
            }
        }

        #region 数据库安装
        private void InstallDatabase()
        {
            ///创建文件夹
            string dir = tbDBPath.Text;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            ///源文件
            string _filePath = Path.Combine(Application.StartupPath, Path.Combine("Data", AppSettings.DBName + @"_Data.mdf"));
            string _logPath = Path.Combine(Application.StartupPath, Path.Combine("Data", AppSettings.DBName + @"_Log.ldf"));
            ///目标文件
            string mdfPath = Path.Combine(dir, AppSettings.DBName + @"_Data.mdf");
            string ldfPath = Path.Combine(dir, AppSettings.DBName + @"_Log.ldf");

            ///复制文件到新文件夹
            if (!File.Exists(mdfPath))
            {
                File.Copy(_filePath, mdfPath, true);
            }
            if (!File.Exists(ldfPath))
            {
                File.Copy(_logPath, ldfPath, true);
            }
            try
            {
                SystemService.AttachDataBase(AppSettings.MasterConnectString, AppSettings.DBName, mdfPath, ldfPath);
                MessageBox.Show("数据库附加成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库附加失败!{ex.Message}", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion


    }
}
