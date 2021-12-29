using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using HPT.Gate.DataAccess.Entity.Service;
using Joey.UserControls;
using HPT.Gate.Client.config;

namespace HPT.Gate.Client
{
    public partial class DatabaseBak : DevComponents.DotNetBar.Office2007Form
    {
        public DatabaseBak(string path)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.Text = path;
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string folderName = fbd.SelectedPath; //获得选择的文件夹路径

                textBox1.Text = folderName;
            }
            else
            {
                return;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text.Trim();
            if (path.Equals(""))
            {
                MessageBoxHelper.Error("保存的路径不能为空!");
                return;
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                progressBarX1.Value = 0;
                path = path + @"\Data_bak_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".bak";
                SystemService.BackUpData(path, AppSettings.DBName);
                progressBarX1.Value = 33;
                Thread.Sleep(100);
                progressBarX1.Value = 67;
                Thread.Sleep(200);
                progressBarX1.Value = 100;
                Thread.Sleep(200);
                MessageBoxHelper.Info("数据库备份成功!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("数据库备份失败:" + ex.Message);
                return;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
