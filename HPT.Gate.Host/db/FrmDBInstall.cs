using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity.Service;
using Joey.UserControls;
using HPT.Gate.Host.Config;

namespace hpt.gate
{
    public partial class FrmDBInstall : WinBase
    {
        public FrmDBInstall()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tbUserName.Enabled = rbAccountOfSQL.Checked;
            tbPassword.Enabled = rbAccountOfSQL.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private bool TestConnect()
        {
            return SystemService.TestConnect(AppSettings.ConnectString);
        }

        #region 获取当前连接字符串
        private string GetCurrentConnectString()
        {
            if (rbAccountOfWindows.Checked)
                return $@"Data Source = {cbbServer.Text}; Initial Catalog = {AppSettings.DBName};Integrated Security=true;";
            if (rbAccountOfSQL.Checked)
                return $@"Data Source = {cbbServer.Text}; Initial Catalog = {AppSettings.DBName };Integrated Security=false;Persist Security Info=False;User Id ={tbUserName.Text}; Password ={tbPassword.Text};";
            return string.Empty;
        }
        #endregion

        private void SaveConfig()
        {
            try
            {
                AppSettings.ServerName = cbbServer.Text;
                AppSettings.LoginType = rbAccountOfWindows.Checked ? "0" : "1";
                AppSettings.DBPath = tbDbPath.Text;
                AppSettings.UserName = tbUserName.Text;
                AppSettings.Password = tbPassword.Text;
                AppSettings.DBPath = tbDbPath.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存配置文件失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InstallDatabase()
        {
            ///创建文件夹
            string dir = tbDbPath.Text;
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

            try
            {
                ///复制文件到新文件夹
                if (!File.Exists(mdfPath))
                {
                    File.Copy(_filePath, mdfPath, true);
                }
                if (!File.Exists(ldfPath))
                {
                    File.Copy(_logPath, ldfPath, true);
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Info($"拷贝数据库文件失败:{ex.Message}");
                return;
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

        private void cbbServer_DropDown(object sender, EventArgs e)
        {
            if (cbbServer.DataSource == null)
            {
                DataTable table = new DataTable();
                table.Columns.Add("ServerName", System.Type.GetType("System.String"));
                int i = 0;
                foreach (DataRow row in SystemService.GetServerList().Rows)
                {
                    table.Rows.Add(row["ServerName"].ToString() + (row["InstanceName"].ToString().Equals("") ? "" : @"\" + row["InstanceName"].ToString()));
                    i++;
                }
                cbbServer.DataSource = table;
                cbbServer.DisplayMember = "ServerName";
                cbbServer.Text = "";
            }

        }

        private void DBInstall_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void DBInstall_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }


        #region 加载配置文件
        private void LoadConfig()
        {
            cbbServer.Text = AppSettings.ServerName;
            rbAccountOfWindows.Checked = AppSettings.LoginType.Equals("0");
            rbAccountOfSQL.Checked = AppSettings.LoginType.Equals("1");
            tbUserName.Text = AppSettings.UserName;
            tbPassword.Text = AppSettings.Password;
            if (AppSettings.DBPath.Equals(string.Empty))
            {
                AppSettings.DBPath = $@"{Environment.CurrentDirectory}\UserData";
            }
            tbDbPath.Text = AppSettings.DBPath;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseDBPath();
        }

        #region 选择数据库文件
        private void ChooseDBPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string folderName = fbd.SelectedPath; //获得选择的文件夹路径

                tbDbPath.Text = folderName;
            }
        }

        #endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void panel_bottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
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

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (tbDbPath.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("数据库存放路径不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SaveConfig();
            InstallDatabase();
        }
    }
}
