using HPT.Gate.Utils.Common;
using HPT.Gate.ZKFP;
using Joey.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HPT.Gate.Client.Personal.emp
{
    public partial class FrmAddFingerPrint : JForm
    {
        private int _CurFingerId;
        public int _FingerId { get; set; }
        public byte[] _FingerData { get; set; }
        public FrmAddFingerPrint(int fingerId)
        {
            InitializeComponent();
            _CurFingerId = fingerId;
        }

        private void FrmAddFingerPrint_Load(object sender, EventArgs e)
        {
            StartService();
        }

        #region 启动服务
        private void StartService()
        {
            ZKFPHelper.Instance.Message += ShowMsg;
            ZKFPHelper.Instance.FingerPrint += ShowFingerPrint;
            ZKFPHelper.Instance.CurrentFingerId = _CurFingerId;
            //if (ZKFPHelper.Instance.Init(null, out string msg))
            ZKFPHelper.Instance.Start();
        }
        #endregion

        #region 关闭服务
        private void StopService()
        {
            ZKFPHelper.Instance.Message -= ShowMsg;
            ZKFPHelper.Instance.FingerPrint -= ShowFingerPrint;
            ZKFPHelper.Instance.Stop();
        }
        #endregion


        #region 展示指纹图像
        private delegate void dlgShowFingerPrint(Bitmap bmp);
        private void ShowFingerPrint(Bitmap bmp)
        {
            if (pictureBox1.InvokeRequired)
            {
                dlgShowFingerPrint dlg = new dlgShowFingerPrint(ShowFingerPrint);
                pictureBox1.Invoke(dlg, bmp);
            }
            else
            {
                pictureBox1.Image = ImageHelper.KiResizeImage(bmp, pictureBox1.Width, pictureBox1.Height);
            }
        }
        #endregion

        #region 
        private delegate void dlgShowMsg(string msg);
        private void ShowMsg(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                dlgShowMsg dlg = new dlgShowMsg(ShowMsg);
                txtLog.Invoke(dlg, msg);
            }
            else
            {
                if (msg.Equals("该手指指纹已经被注册!") || msg.Equals("请连续按同一个手指三次!") || msg.Equals("录入失败!"))
                {
                    txtLog.SelectionStart = txtLog.Text.Length;//设置插入符位置为文本框末
                    txtLog.SelectionColor = Color.Red;//设置文本颜色
                    txtLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msg}\r\n");
                    txtLog.ScrollToCaret();//滚动条滚到到最新插入行
                }
                else
                {
                    txtLog.SelectionStart = txtLog.Text.Length;//设置插入符位置为文本框末
                    txtLog.SelectionColor = Color.Blue;//设置文本颜色
                    txtLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msg}\r\n");
                    txtLog.ScrollToCaret();//滚动条滚到到最新插入行
                }

            }
        }
        #endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {


        }

        private void FrmAddFingerPrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopService();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ZKFPHelper.Instance.IsRunning)
            {
                DialogResult dr = MessageBox.Show("指纹尚未录入成功,是否退出?", "退出询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    ZKFPHelper.Instance.Stop();
                    DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                _FingerId = ZKFPHelper.Instance.CurrentFingerId;
                _FingerData = ZKFPHelper.Instance.CurrentFingerData;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
