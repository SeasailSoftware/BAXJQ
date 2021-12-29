using System;
using System.Windows.Forms;
using System.Threading;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using HPT.Gate.Client.db;
using HPT.Gate.Client.config;
using HPT.Gate.Client.Tools;
using System.Runtime.InteropServices;

namespace HPT.Gate.Client
{
    public partial class Login : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetCapture(IntPtr h);
        private const long WM_GETMINMAXINFO = 0x24;

        #region 拖拽窗口
        private void DragAndDropWindow(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                const int WM_NCLBUTTONDOWN = 0x00A1;
                const int HTCAPTION = 2;
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero); // 拖动窗体 
            }
            else if (e.Clicks == 2 && e.Button == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Normal)
                    WindowState = FormWindowState.Maximized;
                else if (WindowState == FormWindowState.Maximized)
                    WindowState = FormWindowState.Normal;
            }
        }
        #endregion
        public Login()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }


        private void Login_Load(object sender, EventArgs e)
        {
            new Thread(() => { LoadOpers(); }) { IsBackground = true }.Start();
        }



        private void buttonX2_Click(object sender, EventArgs e)
        {
            UserLogin();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmDBConfig dbConfig = new FrmDBConfig();
            dbConfig.ShowDialog();
            LoadOpers();
        }

        #region 加载用户列表
        private void LoadOpers()
        {
            if (SystemService.TestConnect(AppSettings.OLEConnectString))
            {
                //初始化时间组
                try
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        ComboBoxHelper.FillOpers(cbbOpers);
                        tbPassword.Focus();
                    }));
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error("加载用户列表失败:" + ex.Message);
                    return;
                }
            }
        }
        #endregion

        #region 用户登录
        private void UserLogin()
        {
            int operId = Convert.ToInt32(cbbOpers.SelectedValue);
            var OpPass = tbPassword.Text.Trim();
            try
            {
                OperInfo oper = OperInfoService.GetOperDetail(operId);
                if (!oper.OperPass.Equals(tbPassword.Text))
                {
                    MessageBoxHelper.Info("密码错误，如果忘记密码请联系系统管理员!");
                    return;
                }
                //获取菜单权限，并写入全局变量
                LocalCache.CurrentOper = oper;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("检验登录信息失败:" + ex.Message);
                return;
            }
        }
        #endregion

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            DragAndDropWindow(sender,e);
        }
    }
}
