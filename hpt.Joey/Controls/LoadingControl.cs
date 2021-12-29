using HPT.Joey.Lib.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Joey.Lib.Controls
{
    public partial class LoadingControl : Form
    {

        public delegate void mydelegate();
        public mydelegate eventMethod;
        private static LoadingControl pLoading = new LoadingControl();
        delegate void SetTextCallback(string title, string caption, string description);
        delegate void CloseFormCallback();
        private Image m_imgImage = null;
        private EventHandler m_evthdlAnimator = null;
        public LoadingControl()
        {
            InitializeComponent();
            initLoadintForm();
            Thread t = new Thread(new ThreadStart(delegateEventMethod));
            t.IsBackground = true;
            t.Start();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            m_evthdlAnimator = new EventHandler(OnImageAnimate);
            Debug.Assert(m_evthdlAnimator != null);
        }

        private void LoadingControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsDisposed)
            {
                this.Dispose(true);
            }
        }

        private void initLoadintForm()
        {
            this.ControlBox = false;   // 设置不出现关闭按钮
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void delegateEventMethod()
        {
            eventMethod();
        }

        public static LoadingControl getLoading()
        {
            if (pLoading.IsDisposed)
            {
                pLoading = new LoadingControl();
                return pLoading;
            }
            else
            {
                return pLoading;
            }
        }

        //这种方法演示如何在线程安全的模式下调用Windows窗体上的控件。 
        /// <summary>
        /// 设置Loading 窗体的 标题title,标签 caption 和描述 description
        /// </summary>
        /// <param name="title">窗口的标题[为空时，取默认值]</param>
        /// <param name="caption">标签（例如:please wait）[为空时，取默认值]</param>
        /// <param name="description">描述(例如：正在加载资源...)[为空时，取默认值]</param>
        public void SetCaptionAndDescription(string title, string caption, string description, int value)
        {
            Task.Factory.StartNew(() =>
            {
                this.Invoke(new Action(() =>
                {
                    if (!title.Equals(""))
                    {
                        this.Text = title;
                    }
                    if (!caption.Equals(""))
                    {
                        this.lbl_caption.Text = caption;
                    }
                    if (!description.Equals(""))
                    {
                        this.lbl_description.Text = description;
                    }
                    if (progressBar.Minimum <= value && value <= progressBar.Maximum)
                        progressBar.Value = value;
                }));
            });
            /*
            if (this.InvokeRequired && this.lbl_caption.InvokeRequired && this.lbl_description.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetCaptionAndDescription);
                this.Invoke(d, new object[] { title, caption, description });
            }
            else
            {
                if (!title.Equals(""))
                {
                    this.Text = title;
                }
                if (!caption.Equals(""))
                {
                    this.lbl_caption.Text = caption;
                }
                if (!description.Equals(""))
                {
                    this.lbl_description.Text = description;
                }
            }
            */
        }

        public void CloseLoadingForm()
        {
            if (this.InvokeRequired)
            {
                CloseFormCallback d = new CloseFormCallback(CloseLoadingForm);
                this.Invoke(d, new object[] { });
            }
            else
            {
                if (!this.IsDisposed)
                {
                    this.Dispose(true);
                }
            }
        }

        public void SetExecuteMethod(mydelegate method)
        {
            this.eventMethod += method;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (m_imgImage != null)
            {
                UpdateImage();
                e.Graphics.DrawImage(m_imgImage, new Rectangle(1, 1, 92, 85));
            }
        }
         
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            m_imgImage = Resources.wait; // 加载测试用的Gif图片  
            BeginAnimate();
        }

        private void LoadingControl_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (m_imgImage != null)
            {
                StopAnimate();
                m_imgImage = null;
            }
        }

        private void BeginAnimate()
        {
            if (m_imgImage == null)
                return;

            if (ImageAnimator.CanAnimate(m_imgImage))
            {
                ImageAnimator.Animate(m_imgImage, m_evthdlAnimator);
            }
        }

        private void StopAnimate()
        {
            if (m_imgImage == null)
                return;

            if (ImageAnimator.CanAnimate(m_imgImage))
            {
                ImageAnimator.StopAnimate(m_imgImage, m_evthdlAnimator);
            }
        }

        private void UpdateImage()
        {
            if (m_imgImage == null)
                return;

            if (ImageAnimator.CanAnimate(m_imgImage))
            {
                ImageAnimator.UpdateFrames(m_imgImage);
            }
        }

        private void OnImageAnimate(Object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
