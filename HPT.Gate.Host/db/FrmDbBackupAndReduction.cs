using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Windows.Forms;
using HPT.Gate.Host.Config;

namespace HPT.Gate.Host.db
{
    public partial class FrmDbBackupAndReduction : WinBase
    {
        public FrmDbBackupAndReduction()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveDbConfig();
        }

        #region 保存数据库配置
        private void SaveDbConfig()
        {
            AppSettings.DbBackupPath = tbBackupPath.Text;
            this.Close();
        }
        #endregion

        private void FrmDbBackupAndReduction_Load(object sender, EventArgs e)
        {
            LoadDbConfig();
        }

        #region 加载配置文件
        private void LoadDbConfig()
        {
            tbBackupPath.Text = AppSettings.DbBackupPath;
        }
        #endregion

        private void buttonX7_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string folderName = fbd.SelectedPath; //获得选择的文件夹路径
                tbBackupPath.Text = folderName;
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            BackUpDataBase();
        }

        #region 备份数据库
        private void BackUpDataBase()
        {
            string path = tbBackupPath.Text.Trim();
            if (path.Equals(string.Empty))
            {
                MessageBox.Show("请选择数据库备份的路径!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DatabaseBak databaseBak = new DatabaseBak(path);
            databaseBak.ShowDialog();
        }
        #endregion

        private void buttonX5_Click(object sender, EventArgs e)
        {
            DbRestore();
        }

        #region 数据库还原 
        private void DbRestore()
        {
            var openFileDialog = new OpenFileDialog() { InitialDirectory = AppSettings.DbBackupPath, Filter = @"数据库备份文件 (.bak)|*.bak", FilterIndex = 1, RestoreDirectory = true };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (SystemService.RestoreDatabase(fileName, AppSettings.DBName, AppSettings.ConnectString))
                    {
                        MessageBox.Show("数据库还原成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("还原数据库失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("还原数据库失败,错误代码:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        #endregion

        private void buttonX6_Click(object sender, EventArgs e)
        {
            InitSystem();
        }

        #region 系统初始化
        private void InitSystem()
        {
            MessageBoxButtons messageButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("当前操作将会把系统恢复到初始状态，所有的数据将会被清除，确认操作吗?", "系统初始化询问", messageButton, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                try
                {
                    SystemService.InitSystem();
                    MessageBox.Show("系统初始化成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("系统初始化失败,错误代码:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        #endregion

    }
}
