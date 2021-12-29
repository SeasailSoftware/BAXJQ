using HPT.Joey.Lib.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HPT.Joey.Lib.Controls
{
    public partial class JProgressForm : Form
    {
        private Image m_imgImage = null;
        private EventHandler m_evthdlAnimator = null;

        public delegate void ProgressCallback(JProgressForm frm);
        private Thread _thread;
        public JProgressForm()
        {
            InitializeComponent();
            this.ControlBox = false;   // 设置不出现关闭按钮
            this.StartPosition = FormStartPosition.CenterParent;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            m_evthdlAnimator = new EventHandler(OnImageAnimate);
            Debug.Assert(m_evthdlAnimator != null);
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
            _thread = new Thread(new ThreadStart(delegate ()
            {
                try
                {
                    //处理
                    if (_progress != null)
                    {
                        _progress(this);
                    }

                    //关闭
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    if (ex is ThreadAbortException)
                    {
                        DialogResult = DialogResult.Abort;
                    }
                    else
                    {
                        _exception = ex;
                        DialogResult = DialogResult.Cancel;
                    }
                }
            }));
            _thread.Start();
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

        private void JProgressForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_imgImage != null)
            {
                StopAnimate();
                m_imgImage = null;
            }
        }

        public JProgressForm(ProgressCallback progress)
        : this()
        {
            InitializeComponent();
            _progress = progress;
        }

        /// <summary>
        /// 设置或获取显示信息
        /// </summary>
        public string Caption
        {
            get
            {
                string info = null;
                Invoke(new MethodInvoker(delegate ()
                {
                    info = lbl_caption.Text;
                }));
                return info;
            }
            set
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    lbl_caption.Text = value;
                }));
            }
        }

        public string CurrentMessage
        {
            get
            {
                string info = null;
                Invoke(new MethodInvoker(delegate ()
                {
                    info = lbl_description.Text;
                }));
                return info;
            }
            set
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    lbl_description.Text = value;
                }));
            }
        }


        /// <summary>
        /// 设置或获取进度条的最大值
        /// </summary>
        public int MaxValue
        {
            get
            {
                int value = 0;
                Invoke(new MethodInvoker(delegate ()
                {
                    value = progressBar.Maximum;
                }));
                return value;
            }
            set
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    progressBar.Maximum = value;
                }));
            }
        }

        /// <summary>
        /// 设置或获取进度条的最小值
        /// </summary>
        public int MinValue
        {
            get
            {
                int value = 0;
                Invoke(new MethodInvoker(delegate ()
                {
                    value = progressBar.Minimum;
                }));
                return value;
            }
            set
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    progressBar.Minimum = value;
                }));
            }
        }

        /// <summary>
        /// 设置或获取进度条的当前值
        /// </summary>
        public int CurrentValue
        {
            get
            {
                int value = 0;
                Invoke(new MethodInvoker(delegate ()
                {
                    value = progressBar.Value;
                }));
                return value;
            }
            set
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    progressBar.Value = value;
                }));
            }
        }

        private ProgressCallback _progress;
        /// <summary>
        /// 设置或获取要处理的进度
        /// </summary>
        public ProgressCallback Progress
        {
            get { return _progress; }
            set { _progress = value; }
        }

        private Exception _exception;
        /// <summary>
        /// 设置或获取异常信息
        /// </summary>
        public Exception Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }


        //终止
        private void Abort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
    }
}
