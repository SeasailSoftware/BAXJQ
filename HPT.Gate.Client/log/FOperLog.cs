using HPT.Gate.Client.Tools;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HPT.Gate.Client
{
    public partial class FOperLog : Form
    {
        public FOperLog()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public int pageSize = 0;     //每页显示行数
        public int nMax = 0;         //总记录数
        public int pageCount = 0;    //页数＝总记录数/每页显示行数
        public int pageCurrent = 0;   //当前页号
        public int nCurrent = 0;      //当前记录行
        public DataSet dsOperLog = new DataSet();
        public DataTable dtOperLog = new DataTable();

        private static FOperLog instance;

        //三.通过静态方法创建字窗体  
        public static FOperLog CreateForm()
        {
            //判断是否存在该窗体,或时候该字窗体是否被释放过,如果不存在该窗体,则 new 一个字窗体  
            if (instance == null || instance.IsDisposed)
            {
                instance = new FOperLog();
            }
            return instance;
        }




        /// <summary>
        /// 查找操作记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            string operName = comboBoxItem1.ComboBoxEx.Text.Trim();
            string beginDate = dateTimePicker3.Text.Trim();
            string endDate = dateTimePicker4.Text.Trim();
            DataTable dt = OperLogService.Find(operName, beginDate, endDate);
            dgvOperLogs.DataSource = dt;

        }

        /// <summary>
        /// 加载操作日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FOperLog_Load(object sender, EventArgs e)
        {
            try
            {
                ComboBoxHelper.FillOpers(comboBoxItem1.ComboBoxEx);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载用户列表失败:" + ex.Message);
                return;
            }

        }

        private void dgvOperLogs_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
dgvOperLogs.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvOperLogs.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvOperLogs.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
