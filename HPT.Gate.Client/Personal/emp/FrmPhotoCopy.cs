using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using HPT.Gate.Utils.Common;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Client.Base;
using HPT.Gate.DataAccess.Entity.Service;
using System.Linq;
using Joey.UserControls;
using HPT.Gate.Client.Tools;

namespace hpt.gate.collect
{
    public partial class FrmPhotoCopy : FrmBase
    {
        public const int WM_DEVICECHANGE = 0x219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_CONFIGCHANGECANCELED = 0x0019;
        public const int DBT_CONFIGCHANGED = 0x0018;
        public const int DBT_CUSTOMEVENT = 0x8006;
        public const int DBT_DEVICEQUERYREMOVE = 0x8001;
        public const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        public const int DBT_DEVICEREMOVEPENDING = 0x8003;
        public const int DBT_DEVICETYPESPECIFIC = 0x8005;
        public const int DBT_DEVNODES_CHANGED = 0x0007;
        public const int DBT_QUERYCHANGECONFIG = 0x0017;
        public const int DBT_USERDEFINED = 0xFFFF;
        public FrmPhotoCopy()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            DriveInfo[] deviceList = DriveInfo.GetDrives();
            checkedListBox1.Items.Clear();
            foreach (DriveInfo drive in deviceList)
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    checkedListBox1.Items.Add(drive.Name.ToString());
                }
            }
        }


        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == WM_DEVICECHANGE)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case WM_DEVICECHANGE://
                            break;
                        case DBT_DEVICEARRIVAL://U盘插入
                            DriveInfo[] s = DriveInfo.GetDrives();
                            checkedListBox1.Items.Clear();
                            foreach (DriveInfo drive in s)
                            {
                                if (drive.DriveType == DriveType.Removable)
                                {
                                    checkedListBox1.Items.Add(drive.Name.ToString());
                                }
                            }
                            break;
                        case DBT_CONFIGCHANGECANCELED:

                            break;
                        case DBT_CONFIGCHANGED:

                            break;
                        case DBT_CUSTOMEVENT:

                            break;
                        case DBT_DEVICEQUERYREMOVE:

                            break;
                        case DBT_DEVICEQUERYREMOVEFAILED:

                            break;
                        case DBT_DEVICEREMOVECOMPLETE: //U盘卸载
                            DriveInfo[] list = DriveInfo.GetDrives();
                            checkedListBox1.Items.Clear();
                            foreach (DriveInfo drive in list)
                            {
                                if (drive.DriveType == DriveType.Removable)
                                {
                                    checkedListBox1.Items.Add(drive.Name.ToString());
                                }
                            }
                            break;
                        case DBT_DEVICEREMOVEPENDING:

                            break;
                        case DBT_DEVICETYPESPECIFIC:

                            break;
                        case DBT_DEVNODES_CHANGED://可用，设备变化时

                            break;
                        case DBT_QUERYCHANGECONFIG:

                            break;
                        case DBT_USERDEFINED:
  
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error(ex.Message);
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 只允许单选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        this.checkedListBox1.SetItemCheckState(i, System.Windows.Forms.CheckState.Unchecked);
                    }
                }
            }
        }

        private void tabControlPanel1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 上传照片到可移动设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            //获取用户勾选的可移动设备
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBoxHelper.Info("请勾选要传输的可移动设备!");
                return;
            }
            string targetPath = checkedListBox1.CheckedItems[0].ToString();
            List<int> empIdList = new List<int>();
            if (dgvPhoto.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPhoto.Rows.Count; i++)
                {
                    if ((bool)dgvPhoto.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        int empid = Convert.ToInt32(dgvPhoto.Rows[i].Cells[1].Value);
                        empIdList.Add(empid);
                    }
                }
            }

            if (empIdList.Count == 0)
            {
                MessageBoxHelper.Info("请勾选需要拷贝照片的员工!");
                return;
            }
            empIdList.Sort();
            int index = 1;
            int count = empIdList.Count;
            buttonX1.Enabled = false;
            List<EmpInfo> empInfoes = EmpInfoService.ToList();
            foreach (int empid in empIdList)
            {
                ShowMsg(string.Format("正在拷贝照片,EmpId ={0}", empid));
                try
                {
                    EmpInfo emp = empInfoes.FirstOrDefault(s => s.EmpId == empid);
                    if (emp == null) continue;
                    byte[] bin = null;
                    if (emp.Photo == null)
                        emp.Photo = new Bitmap(Application.StartupPath + @"/Image/DefaultPhoto.png");
                    string fileName = ArrayHelper.ArrayToHex(ArrayHelper.IntToBytes(empid, 4));
                    ///获取文件名
                    string targetFile = targetPath + fileName + ".bin";
                    ShowMsg(string.Format("图片文件预存放路径为:{0}", targetFile));
                    File.WriteAllBytes(targetFile, bin);
                    ShowMsg(string.Format("照片文件的长度为:{0}", bin.Length));
                    ShowMsg(string.Format("拷贝照片成功,EmpId ={0}", empid));
                }
                catch (Exception ex)
                {
                    ShowMsg(string.Format("拷贝照片失败:{0},EmpId ={1}", ex.Message, empid));
                }
                progressBar1.Value = (index) * 100 / count;
                label1.Text = (index * 100 / count).ToString() + @"%";
                Application.DoEvents();
                index++;
            }
            buttonX1.Enabled = true;
        }

        private void PhotoCopy_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.FillDeptComboBox(comboBoxItem1.ComboBoxEx);
        }

        private void comboBoxItem1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            try
            {
                if (comboBoxItem1.SelectedIndex != -1)
                {
                    int deptId = 0;
                    try
                    {
                        deptId = Convert.ToInt32(comboBoxItem1.ComboBoxEx.SelectedValue);
                    }
                    catch
                    {
                        return;
                    }
                    List<EmpInfo> empList = new List<EmpInfo>();
                    int type = checkBoxItem1.Checked ? 1 : 0;
                    switch (type)
                    {
                        case 0:
                            empList = EmpInfoService.GetByDeptId(deptId);
                            break;
                        case 1:
                            empList = EmpInfoService.GetAllByDept(deptId);
                            break;

                    }
                    dgvEmp.DataSource = null;
                    dgvEmp.Rows.Clear();
                    foreach (EmpInfo emp in empList)
                    {
                        int rowIndex = dgvEmp.Rows.Add();
                        dgvEmp.Rows[rowIndex].Cells[1].Value = emp.EmpId;
                        dgvEmp.Rows[rowIndex].Cells[2].Value = emp.EmpCode;
                        dgvEmp.Rows[rowIndex].Cells[3].Value = emp.EmpName;
                    }
                    checkBox2.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("加载部门人员失败:" + ex.Message);
                return;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEmp.Rows)
            {
                row.Cells[0].Value = checkBox2.Checked;
            }
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dgvEmp.Rows.Count == 0)
            {
                MessageBoxHelper.Info("没有可添加的人员!");
                return;
            }
            for (int i = 0; i < dgvEmp.Rows.Count; i++)
            {
                if ((bool)dgvEmp.Rows[i].Cells[0].EditedFormattedValue)
                {
                    int empId = Convert.ToInt32(dgvEmp.Rows[i].Cells[1].Value);
                    string empCode = dgvEmp.Rows[i].Cells[2].Value.ToString();
                    string empName = dgvEmp.Rows[i].Cells[3].Value.ToString();
                    AddRow(dgvPhoto, empId, empCode, empName);
                    dgvEmp.Rows.Remove(dgvEmp.Rows[i]);
                    i--;
                }
            }
            foreach (DataGridViewRow row in dgvPhoto.Rows)
            {
                row.Cells[0].Value = true;
            }
        }
        /// <summary>
        /// 添加新行
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        private void AddRow(DataGridView dgv, int empId, string empCode, string empName)
        {
            bool flag = true;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                int _empId = Convert.ToInt32(dgv.Rows[i].Cells[1].Value);
                if (_empId == empId)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                try
                {
                    if (dgv.DataSource != null)
                    {
                        string[] strArray = { empId.ToString(), empCode, empName };
                        ((DataTable)dgv.DataSource).Rows.Add(strArray);
                    }
                    else
                    {
                        int index = dgv.Rows.Add();
                        dgv.Rows[index].Cells[1].Value = empId;
                        dgv.Rows[index].Cells[2].Value = empCode;
                        dgv.Rows[index].Cells[3].Value = empName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error("往表格添加新行失败:" + ex.Message);
                    return;
                }
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvEmp.DataSource;
            if (dt != null)
            {
                dt.Rows.Clear();
                dgvEmp.DataSource = dt;
            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            if (dgvPhoto.DataSource != null)
            {
                DataTable dt = (DataTable)dgvPhoto.DataSource;
                dt.Rows.Clear();
                dgvPhoto.DataSource = dt;
            }
            else
            {
                dgvPhoto.Rows.Clear();
            }
        }

        private void checkBoxItem1_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            GetData();
        }

        private void FrmPhotoCopy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!buttonX1.Enabled)
            {
                MessageBoxHelper.Info("正在拷贝人员照片,请稍后!");
                e.Cancel = true;
            }
        }

        #region 消息提示
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
                if (txtLog.Lines.Length != 0)
                    txtLog.AppendText("\r\n");
                txtLog.AppendText(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
            }
        }
        #endregion
    }
}


