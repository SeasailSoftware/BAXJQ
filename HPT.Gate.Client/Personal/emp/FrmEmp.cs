using HPT.Gate.Client.Base;
using HPT.Gate.Client.Personal.emp;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Utils.Common;
using HPT.Joey.Lib.Controls;
using Joey.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT.Gate.Client.emp
{
    public partial class FrmEmp : FrmBase
    {
        private int _CurrentSelectedIndex = -1;
        private int _CurrentRowIndex = -1;
        #region 查询条件

        private int _DeptId;
        private int _DeptType;
        private string _EmpCode;
        private string _EmpName;
        private string _IDCardNo;
        private string _Duty;
        private string _Telephone;
        private string _CardNo;
        private int _Status;
        #endregion

        /// <summary>
        /// 当前鼠标点击的树节点
        /// </summary>
        private TreeNode _CurNode = null;
        public FrmEmp()
        {
            InitializeComponent();
        }

        private void buttonItem39_Click(object sender, EventArgs e)
        {
            int deptId = 1;
            if (EmpTree.SelectedNode != null)
            {
                deptId = Convert.ToInt32(EmpTree.SelectedNode.Tag);
            }
            FrmEmpAdd ef = new FrmEmpAdd(deptId);
            DialogResult dr = ef.ShowDialog();
            if (dr == DialogResult.OK)
                LoadEmpsOfDept();
        }

        private void buttonItem40_Click(object sender, EventArgs e)
        {
            EditEmp();
        }

        #region 编辑人员信息
        private void EditEmp()
        {
            if (dgvPempOfEmp.SelectedRows.Count < 1)
            {
                MessageBoxHelper.Info("请选择需要编辑的员工信息!");
                return;
            }
            int deptId = -1;
            try
            {
                deptId = Convert.ToInt32(dgvPempOfEmp.SelectedRows[0].Cells[0].Value);
            }
            catch
            {
                return;
            }
            _CurrentSelectedIndex = dgvPempOfEmp.SelectedRows[0].Index;
            _CurrentRowIndex = _CurrentSelectedIndex;
            int empId = Convert.ToInt32(dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[2].Value);
            FrmEmpEdit ef = new FrmEmpEdit(deptId, empId);
            DialogResult dr = ef.ShowDialog();
            if (dr != DialogResult.OK) return;
            Task.Factory.StartNew(() =>
            {
                EmpInfo emp = EmpInfoService.GetByEmpId(empId);
                if (emp == null) return;
                this.Invoke(new Action(() =>
                {
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[0].Value = emp.DeptId;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[1].Value = emp.DeptName;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[2].Value = emp.EmpId;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[3].Value = emp.EmpCode;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[4].Value = emp.EmpName;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[5].Value = emp.Sex;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[6].Value = emp.Nation;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[7].Value = emp.Telephone;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[8].Value = emp.IdentityCard;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[9].Value = emp.BirthDay;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[10].Value = emp.ICCardNo;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[11].Value = emp.IDSerial;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[12].Value = emp.IDCardNo;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[13].Value = emp.JoinDate;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[14].Value = emp.Duty;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[15].Value = emp.Rehire;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[16].Value = emp.HireTimes;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[17].Value = emp.Status;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[18].Value = emp.LeaveDate;
                    dgvPempOfEmp.Rows[_CurrentRowIndex].Cells[19].Value = emp.FaceStatus;
                    if (emp.Status == 1)
                        dgvPempOfEmp.Rows[_CurrentRowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    else
                        dgvPempOfEmp.Rows[_CurrentRowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }));
            });
        }

        #endregion

        private void buttonItem41_Click(object sender, EventArgs e)
        {

        }

        private void EmpTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _CurNode = e.Node;
            lbDeptName.Text = e.Node.Text;
            _CurrentSelectedIndex = -1;
            LoadEmpsOfDept();
            //ShowEmpsOfDept();
        }

        private void LoadEmpsOfDept()
        {
            JProgressHelper process = new JProgressHelper();
            process.ShowProgress = false;
            process.MessageInfo = "正在处理中,请稍后...";
            process.BackgroundWork = ShowEmpsOfDept;
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }

        private void process_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {

        }

        #region 展示部门人员列表
        private void ShowEmpsOfDept(Action<int> progress, Action<string> showMsg)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    DataTable current = (DataTable)dgvPempOfEmp.DataSource;
                    if (current != null)
                    {
                        current.Rows.Clear();
                        dgvPempOfEmp.DataSource = current;
                    }
                }));
                if (_CurNode == null) return;
                int deptId = 0;
                string deptName = _CurNode.Text.Trim();
                int deptType = 0;
                int status = 0; ;
                this.Invoke(new Action(() =>
                {
                    deptId = Convert.ToInt32(_CurNode.Tag);
                    status = rbAll.Checked ? 2 : rbOn.Checked ? 0 : 1;
                    deptType = ckbChildDept.Checked ? 1 : 0;
                }));
                DataTable dt = EmpInfoService.QueryTable(deptId, deptType, status);
                this.Invoke(new Action(() =>
                {
                    label_total.Text = dt.Rows.Count.ToString("000000");
                    dgvPempOfEmp.AutoGenerateColumns = false;
                    dgvPempOfEmp.DataSource = dt;
                    foreach (DataGridViewRow row in dgvPempOfEmp.Rows)
                    {
                        if ((bool)row.Cells["Col_Status"].EditedFormattedValue)
                            row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    dgvPempOfEmp.ClearSelection();
                    if (dgvPempOfEmp.DataSource != null && dgvPempOfEmp.Rows.Count != 0)
                    {
                        if (_CurrentSelectedIndex != -1 && _CurrentSelectedIndex < dgvPempOfEmp.Rows.Count)
                        {
                            dgvPempOfEmp.ClearSelection();
                            dgvPempOfEmp.Rows[_CurrentSelectedIndex].Selected = true;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载部门人员列表失败:" + ex.Message);
                return;
            }
        }
        #endregion


        private void dgvPempOfEmp_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPempOfEmp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _CurrentRowIndex = e.RowIndex;
            EditEmp();
        }

        /// <summary>
        /// 加载部门树形菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FEmp_Load(object sender, EventArgs e)
        {
            TreeHelper.DisplayDeptTree(EmpTree, imageList1);
        }

        private void buttonItem59_Click(object sender, EventArgs e)
        {
            FImport fImport = new FImport();
            fImport.ShowDialog();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            FrmEmpQuery search = new FrmEmpQuery();
            DialogResult dr = search.ShowDialog();
            if (dr != DialogResult.OK) return;
            _DeptId = search._DeptId;
            _DeptType = search._DeptType;
            _EmpCode = search._EmpCode;
            _EmpName = search._EmpName;
            _IDCardNo = search._IDCardNo;
            _Duty = search._Duty;
            _Telephone = search._Telephone;
            _CardNo = search.CardNo;
            _Status = search._Status;
            JWaitingHelper process = new JWaitingHelper();
            process.MessageInfo = "正在处理中,请稍后...";
            process.BackgroundWork = LoadEmpsQuery;
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }

        private void LoadEmpsQuery(Action<string> arg1)
        {
            try
            {
                DataTable dt = EmpInfoService.Find(_DeptId, _DeptType, _EmpCode,_EmpName, _IDCardNo, _Duty, _Telephone, _CardNo, _Status);
                this.Invoke(new Action(() =>
                {
                    label_total.Text = dt.Rows.Count.ToString("000000");
                    dgvPempOfEmp.AutoGenerateColumns = false;
                    dgvPempOfEmp.DataSource = dt;
                    foreach (DataGridViewRow row in dgvPempOfEmp.Rows)
                    {
                        if ((bool)row.Cells["Col_Status"].EditedFormattedValue)
                            row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    dgvPempOfEmp.ClearSelection();
                    if (dgvPempOfEmp.DataSource == null || dgvPempOfEmp.Rows.Count == 0)
                        return;
                    if (_CurrentSelectedIndex != -1 && _CurrentSelectedIndex < dgvPempOfEmp.Rows.Count)
                    {
                        dgvPempOfEmp.ClearSelection();
                        dgvPempOfEmp.Rows[_CurrentSelectedIndex].Selected = true;
                    }
                    lbDeptName.Text = string.Empty;
                    dgvPempOfEmp.ClearSelection();
                    _CurrentRowIndex = -1;
                    _CurNode = null;
                }));
            }
            catch
            {

            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            FrmBatch batch = new FrmBatch();
            batch.ShowDialog();
        }

        private void dgvPempOfEmp_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
            dgvPempOfEmp.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dgvPempOfEmp.RowHeadersDefaultCellStyle.Font, rectangle,
            dgvPempOfEmp.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {

        }

        private void cmsEmp_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            int empId = 0;
            if (dgvPempOfEmp.SelectedRows.Count > 0 && dgvPempOfEmp.SelectedRows[0].Index != -1)
            {
                empId = Convert.ToInt32(dgvPempOfEmp.SelectedRows[0].Cells[2].Value);
            }
            else
            {
                cmsEmp.Close();
                MessageBoxHelper.Info("请选择需要操作的人员!");
                return;
            }
            switch (itemText)
            {
                case "添加人员信息":
                    cmsEmp.Close();
                    buttonItem39_Click(sender, e);
                    break;
                case "修改人员信息":
                    cmsEmp.Close();
                    EditEmp();
                    break;
                case "删除人员信息":
                    cmsEmp.Close();
                    buttonItem41_Click(sender, e);
                    break;
            }
            LoadEmpsOfDept();
        }

        private void EmpTree_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            e.Node.ToolTipText = "点击显示【" + e.Node.Text + "】人员列表";
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            SerialSettingForm serialSettingForm = new SerialSettingForm();
            serialSettingForm.ShowDialog();
        }

        private void labelItem7_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            FrmFaceManager faceManager = new FrmFaceManager();
            faceManager.ShowDialog();
        }

        private void ckbChildDept_CheckedChanged(object sender, EventArgs e)
        {
            LoadEmpsOfDept();
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAll.Checked)
                LoadEmpsOfDept();
        }

        private void rbOn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOn.Checked)
                LoadEmpsOfDept();
        }

        private void rbOff_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOff.Checked)
                LoadEmpsOfDept();
        }

        private void buttonItem3_Click_1(object sender, EventArgs e)
        {
            if (dgvPempOfEmp.Rows.Count == 0)
            {
                MessageBoxHelper.Info("没有可导入的数据!");
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            string folderName = fbd.SelectedPath; //获得选择的文件夹路径
            string file = $@"{folderName}\人员信息表({DateTime.Now.ToString("yyyy-MM-dd")}).xlsx";
            Cursor = Cursors.WaitCursor;
            ExcelHelper.Export(file, dgvPempOfEmp);
            Cursor = Cursors.Default;
        }

        #region List 转datatable
        private DataTable ToDataTable(List<EmpInfo> empList)
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("DeptId", typeof(int)));
            dt.Columns.Add(new DataColumn("DeptName", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpId", typeof(int)));
            dt.Columns.Add(new DataColumn("EmpCode", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpName", typeof(string)));
            dt.Columns.Add(new DataColumn("Sex", typeof(string)));
            dt.Columns.Add(new DataColumn("Nation", typeof(string)));
            dt.Columns.Add(new DataColumn("Telephone", typeof(string)));
            dt.Columns.Add(new DataColumn("IdentityCard", typeof(string)));
            dt.Columns.Add(new DataColumn("Birthday", typeof(string)));
            dt.Columns.Add(new DataColumn("ICCardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("IDSerial", typeof(string)));
            dt.Columns.Add(new DataColumn("IDCardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("HireDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Duty", typeof(string)));
            dt.Columns.Add(new DataColumn("Rehire", typeof(int)));
            dt.Columns.Add(new DataColumn("HireTimes", typeof(int)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            dt.Columns.Add(new DataColumn("LeaveDate", typeof(string)));
            if (empList.Count() > 0)
            {
                foreach (EmpInfo emp in empList)
                {
                    ArrayList tempList = new ArrayList();

                    tempList.Add(emp.DeptId);
                    tempList.Add(emp.DeptName);
                    tempList.Add(emp.EmpId);
                    tempList.Add(emp.EmpCode);
                    tempList.Add(emp.EmpName);
                    tempList.Add(emp.Sex);
                    tempList.Add(emp.Nation);
                    tempList.Add(emp.Telephone);
                    tempList.Add(emp.IdentityCard);
                    tempList.Add(emp.BirthDay);
                    tempList.Add(emp.Status == 1 ? "" : emp.ICCardNo);
                    tempList.Add(emp.Status == 1 ? "" : emp.IDSerial);
                    tempList.Add(emp.Status == 1 ? "" : emp.IDCardNo);
                    tempList.Add(emp.JoinDate);
                    tempList.Add(emp.Duty);
                    tempList.Add(emp.Rehire);
                    tempList.Add(emp.HireTimes);
                    tempList.Add(emp.Status == 0 ? "在职" : "离职");
                    tempList.Add(emp.Status == 0 ? "" : emp.LeaveDate);
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }
        #endregion

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            DeleteEmp();
        }

        private void DeleteEmp()
        {
            if (dgvPempOfEmp.SelectedRows.Count == 0)
            {
                MessageBoxHelper.Info("请选择需要操作的人员!");
                return;
            }
            int rowIndex = dgvPempOfEmp.SelectedRows[0].Index;
            int empId = Convert.ToInt32(dgvPempOfEmp.Rows[rowIndex].Cells[2].Value);
            string empCode = dgvPempOfEmp.Rows[rowIndex].Cells[3].Value.ToString();
            string empName =dgvPempOfEmp.Rows[rowIndex].Cells[4].Value.ToString();
            DialogResult dr = MessageBoxHelper.Question($"确定要删除人员[{empCode},{empName}]吗?");
            if (dr == DialogResult.OK)
            {
                EmpInfoService.Del(empId);
                dgvPempOfEmp.Rows.RemoveAt(rowIndex);
            }
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            FrmRecords records = new FrmRecords();
            DialogResult dr = records.ShowDialog();
        }
    }
}
