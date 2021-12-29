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
        /// �����ȴ�ʱ��(��)
        /// </summary>
        private int seconds = 5;

        /// <summary>
        /// �����ȴ�ʱ��(��)
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
                label1.Text = string.Format("�ǳ��ź���ϵͳ�����˴�����Ϣ���£�\r\n{0}", msg);
            }
        }



        private void tmMain_Tick(object sender, EventArgs e)
        {
            lbRestartMsg.Text = string.Format("{0}���ϵͳ����������,���Ժ�......", seconds);
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