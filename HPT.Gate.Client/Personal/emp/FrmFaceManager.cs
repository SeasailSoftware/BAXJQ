using HPT.Gate.DataAccess.Entity.Service;
using Joey.UserControls;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Client.Personal.emp
{
    public partial class FrmFaceManager : JWindow
    {
        public FrmFaceManager()
        {
            InitializeComponent();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmImportFaces importFaces = new FrmImportFaces();
            importFaces.ShowDialog();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            LoadUnnormalFaces();
        }

        private void FrmFaceManager_Load(object sender, EventArgs e)
        {
            LoadUnnormalFaces();
        }

        private void LoadUnnormalFaces()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    DataTable table = EmpInfoService.GetFaileFaces();
                    this.Invoke(new Action(() =>
                    {
                        dgvEmps.DataSource = table;
                    }));
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error($"加载异常人脸信息失败:{ex.Message}");
                }
            });
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (dgvEmps.SelectedRows.Count == 0) return;
            int deptId = Convert.ToInt32(dgvEmps.SelectedRows[0].Cells[0].Value);
            int empId = Convert.ToInt32(dgvEmps.SelectedRows[0].Cells[2].Value);
            EditEmp(deptId, empId);
        }

        private void EditEmp(int deptId, int empId)
        {
            FrmEmpEdit empEdit = new FrmEmpEdit(deptId, empId);
            DialogResult dr = empEdit.ShowDialog();
            if (dr == DialogResult.OK)
                LoadUnnormalFaces();
        }

        private void dgvEmps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            int deptId = Convert.ToInt32(dgvEmps.Rows[e.RowIndex].Cells[0].Value);
            int empId = Convert.ToInt32(dgvEmps.Rows[e.RowIndex].Cells[2].Value);
            EditEmp(deptId, empId);
        }
    }
}
