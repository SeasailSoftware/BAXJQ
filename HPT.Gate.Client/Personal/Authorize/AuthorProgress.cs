using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Client.Base;
using System.Threading.Tasks;
using HPT.Gate.DataAccess.Entity.Service;
using Joey.UserControls;

namespace HPT.Gate.Client.device
{
    public partial class AuthorProgress : FrmBase
    {
        private List<EmpInfo> _EmpList;
        private List<int> _DevList;

        public AuthorProgress(List<EmpInfo> empList, List<int> deviceList)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this._EmpList = empList;
            this._DevList = deviceList;
            lbEmp.Text = "0" + @"/" + _EmpList.Count.ToString();
            lbDev.Text = "0" + @"/" + _DevList.Count.ToString();
        }
        /// <summary>
        /// 显示进度
        /// </summary>
        /// <param name="empList"></param>
        /// <param name="deviceList"></param>
        private void ShowProgress(List<EmpInfo> empList, List<int> deviceList)
        {
            ///授权写入数据库
            int devCount = deviceList.Count;
            int empCount = empList.Count;
            for (int i = 0; i < devCount; i++)
            {
                int deviceId = deviceList[i];
                for (int k = 0; k < empCount; k++)
                {
                    EmpInfo emp = empList[k];
                    int empId = emp.EmpId;
                    try
                    {
                        DevRightOfEmpService.Insert(empId, deviceId);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.Error($"设备授权发生错误:{ex.Message},EmpId ={empId}");
                    }
                    ShowEmpProgress(k, empCount);
                }
                ShowDevProgress(i, devCount);
                Application.DoEvents();
                Thread.Sleep(1000);
            }
            this.Invoke(new MethodInvoker(() => { Close(); }));
        }

        private void AuthorProgress_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => { ShowProgress(_EmpList, _DevList); });
        }

        #region 显示人员进度
        private delegate void dlgShowEmpProgress(int current, int total);
        private void ShowEmpProgress(int current, int total)
        {
            if (pnEmp.InvokeRequired)
            {
                dlgShowEmpProgress dlg = new dlgShowEmpProgress(ShowEmpProgress);
                pnEmp.Invoke(dlg, current, total);
            }
            else
            {
                progressEmp.Value = (current + 1) * 100 / total;
                lbEmp.Text = $"{current + 1}/{total}";
            }
        }
        #endregion

        #region 显示设备进度
        private delegate void dlgShowDevProgress(int current, int total);
        private void ShowDevProgress(int current, int total)
        {
            if (pnDev.InvokeRequired)
            {
                dlgShowDevProgress dlg = new dlgShowDevProgress(ShowDevProgress);
                pnDev.Invoke(dlg, current, total);
            }
            else
            {
                progressDev.Value = (current + 1) * 100 / total;
                lbDev.Text = $"{current + 1}/{total}";
            }
        }
        #endregion

    }
}
