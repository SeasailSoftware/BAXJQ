using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Host.Config;

namespace HPT.Gate.Host.db
{
    public partial class DatabaseBak : WinBase
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
                MessageBox.Show("保存的路径不能为空!", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("数据库备份成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库备份失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
