using HPT.Joey.Lib.Properties;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Joey.UserControls
{
    public enum MessageBoxStyle
    {
        info = 0,
        question = 1,
        error = 2
    };
    public partial class JMessageButton : BaseForm
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool ReleaseCapture();
        private const long WM_GETMINMAXINFO = 0x24;

        public JMessageButton(MessageBoxStyle messageBoxStyle, string msg)
        {
            InitializeComponent();
            if (messageBoxStyle == MessageBoxStyle.info)
            {
                panel_Image.BackgroundImage = global::HPT.Joey.Lib.Properties.Resources.info;
                this.Text = "温馨提示";
                button_cancel.Visible = false;
                button_ok.Visible = true;
                button_yes.Visible = false;
            }
            else if (messageBoxStyle == MessageBoxStyle.question)
            {
                panel_Image.BackgroundImage = global::HPT.Joey.Lib.Properties.Resources.question;
                this.Text = "询问消息";
                button_cancel.Visible = true;
                button_ok.Visible = false;
                button_yes.Visible = true;
            }
            else if (messageBoxStyle == MessageBoxStyle.error)
            {
                panel_Image.BackgroundImage = global::HPT.Joey.Lib.Properties.Resources.error;
                this.Text = "错误消息";
                button_cancel.Visible = false;
                button_ok.Visible = true;
                button_yes.Visible = false;
            }

            this.label_msg.Text = msg;
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

        private void JMessageButton_Load(object sender, EventArgs e)
        {
            label_title.Text = this.Text;
        }

        private void panel_top_MouseDown(object sender, MouseEventArgs e)
        {
            const int WM_NCLBUTTONDOWN = 0x00A1;
            const int HTCAPTION = 2;

            if (e.Button == MouseButtons.Left) // 按下的是鼠标左键 
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero); // 拖动窗体 
            }
        }

        private void label_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
