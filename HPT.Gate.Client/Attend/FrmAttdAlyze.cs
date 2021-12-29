using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HPT.Gate.DataAccess.Service;
using System.Linq;
using Joey.UserControls;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Client.Tools;

namespace hpt.gate.attend.Frm
{
    public partial class FrmAttdAlyze : JWindow
    {
        public FrmAttdAlyze()
        {
            InitializeComponent();
        }

        private void btProc_Click(object sender, EventArgs e)
        {
            string beginDate = Convert.ToDateTime(dtpBegin.Text).AddDays(-1).ToString("yyyy-MM-dd");
            string endDate = Convert.ToDateTime(dtpEnd.Text).AddDays(1).ToString("yyyy-MM-dd");
            int deptId = Convert.ToInt32(cbbDept.SelectedValue);
            int flag = checkBox1.Checked ? 1 : 0;
            string empCode = tbEmpCode.Text;
            string empName = tbEmpName.Text;
            btProc.Enabled = false;
            btExit.Enabled = false;
            ProcAttdData(beginDate, endDate, deptId, flag, empCode, empName);
            btProc.Enabled = true;
            btExit.Enabled = true;
        }

        #region 考勤分析处理

        private void ProcAttdData(string beginDate, string endDate, int deptId, int deptType, string empCode, string empName)
        {
            List<EmpInfo> empList = EmpInfoService.FindOnCondition(deptId, deptType, empCode, empName);
            if (empList.Count == 0)
            {
                MessageBoxHelper.Info("没有可分析处理的人员!");
                return;
            }
            txtLog.Clear();
            ShowMsg("开始考勤分析处理...");
            Application.DoEvents();
            int index = 0;
            int count = empList.Count;
            ShowProgress(0);
            DateTime dt = DateTime.Now;
            TimeSpan tsDt = new TimeSpan(dt.Ticks);
            List<AttendData> attendDataList = AttendDataService.ToList(beginDate, endDate);
            List<AttendShiftOfEmp> shiftList = AttendShiftOfEmpService.GetAll(beginDate, endDate);
            foreach (EmpInfo emp in empList)
            {
                ShowMsg($"正在处理[{emp.EmpCode},{emp.EmpName}]的考勤数据..");
                Application.DoEvents();
                AttendRule rule = AttendRuleService.Get();
                DateTime dtBegin = DateTime.Now;
                List<AttendData> dataList = attendDataList.Where(p => p.EmpId == emp.EmpId).ToList();
                List<AttendShiftOfEmp> shifts = shiftList.Where(p => p.EmpId == emp.EmpId).ToList();
                try
                {
                    AttendManager.Proc(emp, dataList, shifts, rule, beginDate, endDate);
                }
                catch (Exception ex)
                {
                    ShowMsg($"考勤分析处理失败:{ex.Message}");
                    continue;
                }
                index++;
                DateTime dtEnd = DateTime.Now;
                TimeSpan tsBegin = new TimeSpan(dtBegin.Ticks);
                TimeSpan tsEnd = new TimeSpan(dtEnd.Ticks);
                TimeSpan tsBetween = tsEnd.Subtract(tsBegin).Duration();
                float value = tsBetween.Hours * 60 * 60 + tsBetween.Minutes * 60 + tsBetween.Seconds + (float)tsBetween.Milliseconds / 1000;
                ShowMsg($"处理完毕 , 用时:{value}秒");
                ShowProgress(index * 100 / count);

                TimeSpan tsUsed = tsEnd.Subtract(tsDt).Duration();
                string text = "用时:" + (tsUsed.Hours.ToString().Length == 1 ? "0" + tsUsed.Hours.ToString() : tsUsed.Hours.ToString());
                text += ":" + (tsUsed.Minutes.ToString().Length == 1 ? "0" + tsUsed.Minutes.ToString() : tsUsed.Minutes.ToString());
                text += ":" + (tsUsed.Seconds.ToString().Length == 1 ? "0" + tsUsed.Seconds.ToString() : tsUsed.Seconds.ToString());
                label6.Text = text;
                Application.DoEvents();
            }
        }

        #endregion

        private void FrmAttdAlyze_Load(object sender, EventArgs e)
        {
            try
            {
                ComboBoxHelper.FillDeptComboBox(cbbDept);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error($"加载部门列表失败:{ex.Message}");
            }
            GetFirstDayOfCurrentMonth();

        }

        #region 获取当月第一天
        private void GetFirstDayOfCurrentMonth()
        {
            string firstDay = string.Empty;
            DateTime dt = DateTime.Now;
            firstDay = dt.Year.ToString() + "-" + dt.Month.ToString() + "-01";
            dtpBegin.Text = firstDay;
        }
        #endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #region 展示消息
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
                if (txtLog.Lines.Length >= 100)
                    txtLog.Clear();
                txtLog.AppendText($"{Environment.NewLine}{DateTime.Now.ToString("yyyy_MM-dd")} {msg}");
            }
        }
        #endregion

        #region 展示进度
        private delegate void dlgShowProgress(int value);
        private void ShowProgress(int value)
        {
            if (statusStrip1.InvokeRequired)
            {
                dlgShowProgress dlg = new dlgShowProgress(ShowProgress);
                statusStrip1.Invoke(dlg, value);
            }
            else
            {
                toolStripProgressBar1.Value = value;
                toolStripStatusLabel2.Text = $"{value}%";
            }
        }

        #endregion

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            List<EmpInfo> empList = EmpInfoService.ToList();
            foreach (EmpInfo emp in empList)
            {
                ShowMsg($"正在生成[{emp.EmpCode}{emp.EmpName}]的测试数据...");
                Application.DoEvents();
                DateTime dtBegin = new DateTime(2018, 01, 01);
                DateTime dtEnd = new DateTime(2018, 01, 31);
                DateTime dt = dtBegin;
                while (dt < dtEnd)
                {
                    ShowMsg($"正在生成[{dt.ToString("yyyy-MM-dd")}]的测试数据...");
                    Application.DoEvents();
                    AttendData data1 = new AttendData();
                    AttendData data2 = new AttendData();
                    AttendData data3 = new AttendData();
                    AttendData data4 = new AttendData();
                    data1.EmpId = emp.EmpId;
                    data2.EmpId = emp.EmpId;
                    data3.EmpId = emp.EmpId;
                    data4.EmpId = emp.EmpId;
                    data1.CardNo = emp.ICCardNo;
                    data2.CardNo = emp.ICCardNo;
                    data3.CardNo = emp.ICCardNo;
                    data4.CardNo = emp.ICCardNo;
                    data1.RecDatetime = new DateTime(dt.Year, dt.Month, dt.Day, 7, 45, 30);
                    data2.RecDatetime = new DateTime(dt.Year, dt.Month, dt.Day, 12, 05, 30);
                    data3.RecDatetime = new DateTime(dt.Year, dt.Month, dt.Day, 13, 15, 30);
                    data4.RecDatetime = new DateTime(dt.Year, dt.Month, dt.Day, 17, 45, 30);
                    data1.IOFlag = "进";
                    data2.IOFlag = "出";
                    data3.IOFlag = "进";
                    data4.IOFlag = "出";
                    data1.Passed = 0;
                    data2.Passed = 0;
                    data3.Passed = 0;
                    data4.Passed = 0;
                    data1.DeviceId = 1;
                    data2.DeviceId = 1;
                    data3.DeviceId = 1;
                    data4.DeviceId = 1;
                    AttendDataService.Insert(data1);
                    AttendDataService.Insert(data2);
                    AttendDataService.Insert(data3);
                    AttendDataService.Insert(data4);
                    dt = dt.AddDays(1);
                    ShowMsg($"[{dt.ToString("yyyy-MM-dd")}]的测试数据生成完毕!");
                    Application.DoEvents();
                }
                Application.DoEvents();
            }
        }
    }
}
