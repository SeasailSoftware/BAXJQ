using HPT.Joey.Lib.Properties;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Joey.UserControls
{
    public partial class JWindow : BaseForm
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool ReleaseCapture();
        private const long WM_GETMINMAXINFO = 0x24;

        public JWindow()
        {
            InitializeComponent();
        }

        

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void BaseWindow_TextChanged(object sender, EventArgs e)
        {
            label_title.Text = this.Text;
        }

        private void panel_top_MouseDown(object sender, MouseEventArgs e)
        {
            DragAndDropWindow(sender,e);
            /*
            const int WM_NCLBUTTONDOWN = 0x00A1;
            const int HTCAPTION = 2;

            if (e.Button == MouseButtons.Left) // 按下的是鼠标左键 
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero); // 拖动窗体 
            }
            */
        }

        #region 拖拽窗口
        private void DragAndDropWindow(object sender, MouseEventArgs e)
        {
            const int WM_NCLBUTTONDOWN = 0x00A1;
            const int HTCAPTION = 2;

            if (e.Button == MouseButtons.Left) // 按下的是鼠标左键 
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero); // 拖动窗体 
            }
        }
        #endregion



        private void button_max_Click(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Normal: { WindowState = FormWindowState.Maximized; } break;
                case FormWindowState.Maximized: { WindowState = FormWindowState.Normal; } break;
            }
        }

        private void panel_top_Resize(object sender, EventArgs e)
        {
            this.Invalidate();//使当前控件无效，重绘控件
        }

        private void button_min_Click(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Normal: { WindowState = FormWindowState.Minimized; } break;
                case FormWindowState.Maximized: { WindowState = FormWindowState.Minimized; } break;
            }

        }

        private void label_windowstool_MouseEnter(object sender, EventArgs e)
        {
            Label button = sender as Label;
            switch (button.Name)
            {
                case "label_min": { button.Image = Resources.min_hover; } break;
                case "label_max": { if (this.WindowState == FormWindowState.Maximized) { button.Image = Resources.store_hover; } else { button.Image = Resources.max_hover; } } break;
                case "label_close": { button.Image = Resources.close_hover; } break;
            }
        }

        private void label_windowstool_MouseLeave(object sender, EventArgs e)
        {
            Label l = sender as Label;
            switch (l.Name)
            {
                case "label_min": { l.Image = Resources.min_normal; } break;
                case "label_max": { if (this.WindowState == FormWindowState.Maximized) { l.Image = Resources.store_normal; } else { l.Image = Resources.max_normal; } } break;
                case "label_close": { l.Image = Resources.close_normal; } break;
            }
        }

        private void BaseWindow_SizeChanged(object sender, EventArgs e)
        {

        }

        private void BaseWindow_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.app;
            label_close.Visible = ControlBox;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void image_title_MouseDown(object sender, MouseEventArgs e)
        {
            DragAndDropWindow(sender, e);
        }

        private void label_title_MouseDown(object sender, MouseEventArgs e)
        {
            DragAndDropWindow(sender, e);
        }

        private void button_min_MouseDown(object sender, MouseEventArgs e)
        {
            DragAndDropWindow(sender, e);
        }

        private void button_max_MouseDown(object sender, MouseEventArgs e)
        {
            DragAndDropWindow(sender, e);
        }

        private void button_close_MouseDown(object sender, MouseEventArgs e)
        {
            DragAndDropWindow(sender, e);
        }
    }
}
