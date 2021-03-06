using DevComponents.DotNetBar;
using hpt.gate.attend.Frm;
using HPT.Gate.Client.Attend;
using HPT.Gate.Client.BarCode;
using HPT.Gate.Client.Base;
using HPT.Gate.Client.config;
using HPT.Gate.Client.device;
using HPT.Gate.Client.emp;
using HPT.Gate.Client.Emp;
using HPT.Gate.Client.messageBox;
using HPT.Gate.Client.oper;
using HPT.Gate.Client.Properties;
using HPT.Gate.DataAccess.Entity;
using Joey.Controls;
using Joey.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HPT.Gate.Client
{
    public partial class Main : FrmBase
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
            else if (e.Clicks == 2 &&e.Button == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Normal)
                    WindowState = FormWindowState.Maximized;
                else if (WindowState == FormWindowState.Maximized)
                    WindowState = FormWindowState.Normal;
            }
        }
        #endregion

        public Main()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LocalCache.RestartFlag = false;
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayForms(object sender, EventArgs e)
        {
            string buttonName = (sender as ButtonItem).Name;
            string buttonText = (sender as ButtonItem).Text;
            Form form = null;
            switch (buttonName)
            {
                #region 人事管理

                //部门设置
                case "BTI_Dept":
                    form = new FrmDept();
                    break;
                //人员与卡证管理
                case "BTI_EmpAndCard":
                    form = new FrmEmp();
                    break;
                case "BTI_DeviceRights":
                    form = new FrmRights();
                    break;
                case "BTI_BarCode":
                    form = new FrmBarCode();
                    break;
                case "BTI_CardType":
                    form = new FrmTicketType();
                    break;
                #endregion

                #region  报表中心
                case "BTI_ReportOfEmp":

                    break;
                case "BTI_ReportOfRight":

                    break;
                case "BTI_ReportOfRecord":
                    break;
                case "BTI_ReportOfExceptionRecords":

                    break;
                case "BTI_ReportOfExceptionRecord":

                    break;
                #endregion

                #region 系统参数设置
                case "BTI_Oper":
                    form = new FrmOper();
                    break;
                case "BTI_OperLog":
                    form = new FOperLog();
                    break;
                case "BTI_Password":
                    FrmPassword fPass = new FrmPassword();
                    DialogResult dr = fPass.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        MessageBoxHelper.Info("修改密码之后程序需要重新启动。");
                        LocalCache.RestartFlag = true;
                        Application.Restart();
                    }
                    break;
                case "BTI_SysPara":
                    break;
                #endregion

                #region 考勤系统
                case "BTI_AttendRules":
                    FrmAttendRule attdRule = new FrmAttendRule();
                    attdRule.ShowDialog();
                    break;
                case "BTI_AttendShifts":
                    form = new FrmShifts();
                    break;
                case "BTI_AttendData":
                    form = new FrmAttendData();
                    break;
                case "BTI_AttendReports":
                    form = new FrmAttendReports();
                    break;
                case "BTI_AttendProc":
                    FrmAttdAlyze alyze = new FrmAttdAlyze();
                    alyze.ShowDialog();
                    break;
                case "BTI_OriginalRecord":
                    form = new RptOriginalRecord();
                    break;
                case "BTI_AttendDetailPersonal":
                    form = new RptAttdDetail();
                    break;
                case "BTI_AttendSummaryPersonal":
                    form = new RptAttdSummaryPersonal();
                    break;
                case "BTI_AttendSummaryDept":
                    form = new RptAttdSummaryDept();
                    break;
                #endregion

                #region 关于我们
                case "BTI_About":
                    HPTAbout about = new HPTAbout();
                    about.ShowDialog();
                    break;
                    #endregion


            }
            if (form == null) return;
            ///检查是否已经打开同一个页面
            foreach (SuperTabItem sti in _MainTabControl.Tabs)
            {
                if (sti.Text.Equals(buttonText))
                {
                    ///_MainTabControl.SelectedTab = sti;
                    sti.Close();
                    break;
                }
            }
            ///添加页面
            SuperTabItem newTabItem = new SuperTabItem();
            newTabItem.Text = buttonText;
            SuperTabControlPanel panel = new SuperTabControlPanel();
            SetFormStyle(panel, form);
            int index = _MainTabControl.Tabs.Count;
            _MainTabControl.CreateTab(newTabItem, panel, index);
            newTabItem.CloseButtonVisible = true;


            ///显示在最前面
            _MainTabControl.SelectedTab = newTabItem;
            labelPosition.Text = $"当前位置:{(sender as ButtonItem).Parent.Text}=>{buttonText}";
        }

        /// <summary>
        /// 设置窗体的样式
        /// </summary>
        /// <param name="fdae"></param>
        private void SetFormStyle(SuperTabControlPanel panel, Form form)
        {
            form.BackColor = Color.AliceBlue;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            panel.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ///加载菜单
            LoadMenus();
            ///写入操作日志
            //todo
            //Logs.InsertOperLog(LocalCache.CurrentOper.OperName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss "), "智能门禁管理系统", "登录", "登录系统", 1);
            ///加载状态栏
            LoadStatusBar();
            CheckDll();
        }

        private void CheckDll()
        {
            try
            {
                string s = GetBits();
                string path1 = $@"{Environment.CurrentDirectory}\msvcp120.dll";
                string path2 = $@"{Environment.CurrentDirectory}\msvcr120.dll";
                string source1 = string.Empty;
                string source2 = string.Empty;
                if (s.Equals("64"))
                {
                    source1 = $@"{Environment.CurrentDirectory}\x64\msvcp120.dll";
                    source2 = $@"{Environment.CurrentDirectory}\x64\msvcr120.dll";
                }
                else
                {
                    source1 = $@"{Environment.CurrentDirectory}\x86\msvcp120.dll";
                    source2 = $@"{Environment.CurrentDirectory}\x86\msvcr120.dll";
                }
                File.Copy(source1, path1, true);
                File.Copy(source2, path2, true);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Info($"初始化动态库失败:{ex.Message}");
            }
        }

        public static string GetBits()
        {
            string addressWidth = String.Empty;
            ConnectionOptions mConnOption = new ConnectionOptions();
            ManagementScope mMs = new ManagementScope(@"\\localhost", mConnOption);
            ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
            ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
            ManagementObjectCollection mObjectCollection = mSearcher.Get();
            foreach (ManagementObject oReturn in mObjectCollection)
            {
                addressWidth = oReturn["AddressWidth"].ToString();
            }


            return addressWidth;
        }

        /// <summary>
        /// 加载状态栏
        /// </summary>
        public void LoadStatusBar()
        {
            ///显示系统时间
            new Thread(() =>
            {
                while (true)
                {
                    this.Invoke(new Action(() =>
                    {
                        labelCurrentTime.Text = DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToString("HH:mm:ss");
                    }));
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true }.Start();
            ///加载当前用户
            labelOper.Text = LocalCache.CurrentOper.OperName;
            ///加载数据库
            labelDBServer.Text = AppSettings.ServerName;
        }

        /// <summary>
        /// 加载菜单项
        /// </summary>
        private void LoadMenus()
        {
            List<Menus> menuList = LocalCache.MenuList;
            foreach (Menus menu in menuList)
            {
                if (menu.ParMenuId == 0)
                {
                    ExplorerBarGroupItem group = new ExplorerBarGroupItem(menu.MenuName, menu.MenuText);
                    group.ExpandBackColor = Color.White;
                    group.Expanded = true;
                    group.ExpandForeColor = Color.FromArgb(0, 60, 165);
                    group.ExpandBorderColor = Color.FromArgb(174, 182, 216);
                    group.StockStyle = eExplorerBarStockStyle.Blue;
                    _Menus.Groups.Add(group);
                    foreach (Menus subMenu in menuList)
                    {
                        if (subMenu.ParMenuId == menu.MenuId)
                        {
                            ButtonItem item = new ButtonItem(subMenu.MenuName, subMenu.MenuText);
                            item.ForeColor = Color.FromArgb(33, 93, 198);
                            group.SubItems.Add(item);
                            item.Click += DisplayForms;
                        }
                    }
                }
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            CancelSystem();
        }

        private void CancelSystem()
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            var dr = MessageBoxHelper.Question("确定要注销吗?");
            if (dr == DialogResult.OK)
            {
                LocalCache.RestartFlag = true;
                Application.Restart();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //todo
            //Logs.InsertOperLog(LocalCache.CurrentOper.OperName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss "), "智能门禁管理系统", "登出", "退出系统", 1);
            if (!LocalCache.RestartFlag)
                Environment.Exit(0);
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _MainTabControl_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            string text = _MainTabControl.SelectedTab.Text;
            int parent = LocalCache.MenuList.FirstOrDefault(p => p.MenuText.Equals(text)).ParMenuId;
            string msg = LocalCache.MenuList.FirstOrDefault(p => p.MenuId == parent).MenuText;
            labelPosition.Text = $"当前位置:{msg}=>{text}";
        }

        private void _MainTabControl_TabItemClose(object sender, SuperTabStripTabItemCloseEventArgs e)
        {
            if (_MainTabControl.Tabs.Count == 1)
                labelPosition.Text = "当前位置:首页";
        }



        private void bar11_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Hello");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Normal: { WindowState = FormWindowState.Maximized; } break;
                case FormWindowState.Maximized: { WindowState = FormWindowState.Normal; } break;
            }
        }

        private void label_Close_MouseEnter(object sender, EventArgs e)
        {
            label_Close.Image = Resources.close_press;
        }

        private void label_Close_MouseLeave(object sender, EventArgs e)
        {
            label_Close.Image = Resources.close_normal;
        }

        private void label_Max_MouseEnter(object sender, EventArgs e)
        {
            label_Max.Image = Resources.max_hover;
        }

        private void label_Max_MouseLeave(object sender, EventArgs e)
        {
            label_Max.Image = Resources.max_normal;
        }

        private void label_Min_MouseEnter(object sender, EventArgs e)
        {
            label_Min.Image = Resources.min_hover;
        }

        private void label_Min_MouseLeave(object sender, EventArgs e)
        {
            label_Min.Image = Resources.min_normal;

        }

        private void label_Min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel_ControlBox_MouseDown(object sender, MouseEventArgs e)
        {
            DragAndDropWindow(sender, e);
        }


        private void bar11_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {

        }

        private void panel_ControlBox_Resize(object sender, EventArgs e)
        {

        }

        private void SetReion()
        {
            base.OnCreateControl();
            using (GraphicsPath path =
                    GraphicsPathHelper.CreatePath(
                    new Rectangle(Point.Empty, base.Size), 6, RoundStyle.All, true))
            {
                Region region = new Region(path);
                path.Widen(Pens.White);
                region.Union(path);
                this.Region = region;
            }
        }

        private void bar11_DoubleClick(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Normal: { WindowState = FormWindowState.Maximized; } break;
                case FormWindowState.Maximized: { WindowState = FormWindowState.Normal; } break;
            }
        }

        private void label_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
