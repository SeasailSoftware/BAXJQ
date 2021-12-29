using HPT.Joey.Lib;
using HPT.Joey.Lib.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Joey.Lib.Controls
{
    public partial class JWaiting : Form
    {
        private Image m_imgImage = null;
        private EventHandler m_evthdlAnimator = null;
        public JWaiting()
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

        /// <summary>
        /// 设置提示信息
        /// </summary>
        public string MessageInfo
        {
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        lbl_caption.Text = value;
                    }));
                }
                else
                {
                    lbl_caption.Text = value;
                }

            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (m_imgImage != null)
            {
                UpdateImage();
                e.Graphics.DrawImage(m_imgImage, new Rectangle(1, 1, 92, 84));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            m_imgImage = Resources.wait; // 加载测试用的Gif图片  
            BeginAnimate();
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
    }
}
