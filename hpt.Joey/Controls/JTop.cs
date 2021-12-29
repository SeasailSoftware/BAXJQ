using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HPT.Joey.Lib.Controls
{
    public partial class JTop : UserControl
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool ReleaseCapture();
        private const long WM_GETMINMAXINFO = 0x24;
        public JTop()
        {
            InitializeComponent();
        }

        #region 拖拽窗口
        private void DragAndDropWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    if (((Form)this.Parent).WindowState == FormWindowState.Maximized)
                        ((Form)this.Parent).WindowState = FormWindowState.Normal;
                    else if (((Form)this.Parent).WindowState == FormWindowState.Normal)
                        ((Form)this.Parent).WindowState = FormWindowState.Maximized;
                }
                else
                {
                    const int WM_NCLBUTTONDOWN = 0x00A1;
                    const int HTCAPTION = 2;
                    ReleaseCapture();
                    SendMessage(this.Parent.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero); // 拖动窗体 
                }
            }
        }
        #endregion

        private void JTop_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Top_MouseDown(object sender, MouseEventArgs e)
        {
            DragAndDropWindow(sender, e);
        }
    }
}
