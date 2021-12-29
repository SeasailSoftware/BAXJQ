using hpt.gate.DataAccess.Service;
using Joey.UserControls;
using System;
using System.Data;
using System.Threading.Tasks;

namespace HPT.Gate.Host.face
{
    public partial class FrmFaceDetail : JWindow
    {
        public FrmFaceDetail()
        {
            InitializeComponent();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {

            LoadDetails();

        }

        private void LoadDetails()
        {
            Task.Factory.StartNew(() =>
            {
                this.Invoke(new Action(() =>
                {
                    btnQuery.Enabled = false;
                    btnClear.Enabled = false;
                    btnClose.Enabled = false;
                }));
                DataTable dt = TaskResultService.GetAll();
                this.Invoke(new Action(() =>
                {
                    dgvTaskResult.DataSource = dt;
                    btnQuery.Enabled = true;
                    btnClear.Enabled = true;
                    btnClose.Enabled = true;
                }));
            });
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            try
            {
                TaskResultService.Clear();
                MessageBoxHelper.Info($"已清空!");
                LoadDetails();
            }

            catch (Exception ex)
            {
                MessageBoxHelper.Info($"清空失败:{ex.Message}");
            }
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
