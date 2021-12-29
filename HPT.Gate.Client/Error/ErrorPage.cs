using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace HPT.Gate.Client.Error
{
    public partial class ErrorPage : DevComponents.DotNetBar.Office2007Form
    {
        public ErrorPage()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
                #region var

        /// <summary>
        /// 重启等待时间(秒)
        /// </summary>
        private int seconds = 5;

        /// <summary>
        /// 重启等待时间(秒)
        /// </summary>
        public int Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }

        #endregion

        #region form

        public ErrorPage(int seconds = 5, string msg = "")
        {
            InitializeComponent();
            this.Seconds = seconds;
            if (!string.IsNullOrEmpty(msg))
            {
                label1.Text = string.Format("非常遗憾，系统发生了错误，信息如下：\r\n{0}", msg);
            }
        }



        private void tmMain_Tick(object sender, EventArgs e)
        {
            lbRestartMsg.Text = string.Format("{0}秒后系统将重新启动,请稍候......", seconds);
            if (--seconds < 0)
            {
                LocalCache.RestartFlag = true;
                Application.Restart();
            }
        }

        #endregion

        private void ErrorPage_Load(object sender, EventArgs e)
        {
            this.Activate();
        }
    }
}