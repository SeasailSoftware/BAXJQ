using hpt.gate.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using hpt.gate.DbTools.Service;
using hpt.gate.Service;
using hpt.gate.client.config;
using hpt.gate.Helper;
using hpt.gate.client.Tools;

namespace hpt.gate.attend.Frm
{
    public partial class FrmAttdAlyze : DevComponents.DotNetBar.Office2007Form
    {
        public FrmAttdAlyze()
        {
            InitializeComponent();
        }

        private void btProc_Click(object sender, EventArgs e)
        {
            string beginDate = dtpBegin.Text;
            string endDate = dtpEnd.Text;
            int deptId = Convert.ToInt32(cbbDept.SelectedValue);
            int flag = checkBox1.Checked ? 1 : 0;
            string empCode = tbEmpCode.Text;
            string empName = tbEmpName.Text;
            btProc.Enabled = false;
            ProcAttdData(beginDate, endDate, deptId, flag, empCode, empName);
            btProc.Enabled = true;
        }

        #region 考勤分析处理

        private void ProcAttdData(string beginDate, string endDate, int deptId, int deptType, string empCode, string empName)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            try
            {
                empList = AttendService.GetAttendProcEmpList(deptId, deptType, empCode, empName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取人员列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (empList.Count == 0)
            {
                MessageBox.Show("没有可分析处理的人员!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            richTextBox1.Clear();
            richTextBox1.AppendText("\r\n正在生成考勤数据...");
            Application.DoEvents();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                foreach (EmpInfo emp in empList)
                {

                    try
                    {
                        AttendDBService.CreateAttendData(emp, beginDate, endDate, AppSettings.AttendBeginTime, AppSettings.AttendEndDay, AppSettings.AttendEndTime, dbHelper);
                        richTextBox1.AppendText("\r\n" + emp.EmpCode + emp.EmpName + "考勤数据生成完毕!");
                        Application.DoEvents();
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.AppendText(string.Format("生成[{0}][{1}]的考勤数据失败:{2}", empCode, empName, ex.Message));
                        return;
                    }
                    Application.DoEvents();
                }
            }
            richTextBox1.AppendText("\r\n考勤数据生成完毕,开始考勤分析处理....");
            Application.DoEvents();
            int index = 0;
            int count = empList.Count;
            using (SQLiteHelper dbHelper = new SQLiteHelper())
            {
                toolStripProgressBar1.Value = 0;
                DateTime dt = DateTime.Now;
                TimeSpan tsDt = new TimeSpan(dt.Ticks);
                foreach (EmpInfo emp in empList)
                {
                    richTextBox1.AppendText("\r\n 开始处理考勤数据 , 处理对象" + emp.EmpCode + " " + emp.EmpName + "...");
                    Application.DoEvents();
                    DateTime dtBegin = DateTime.Now;

                    try
                    {
                        AttendManager.ProAttendDataOfEmp(emp, beginDate, endDate, AppSettings.AttendBeginTime, AppSettings.AttendEndDay, AppSettings.AttendEndTime);
                        ///AttendDBService.ProAttendDataOfEmp(emp, beginDate, endDate,dbHelper);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("分析处理" + emp.EmpCode + emp.EmpName + "的考勤数据失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    index++;
                    toolStripProgressBar1.Value = index * 100 / count;
                    toolStripStatusLabel2.Text = (index * 100 / count).ToString() + "%";
                    DateTime dtEnd = DateTime.Now;
                    TimeSpan tsBegin = new TimeSpan(dtBegin.Ticks);
                    TimeSpan tsEnd = new TimeSpan(dtEnd.Ticks);
                    TimeSpan tsBetween = tsEnd.Subtract(tsBegin).Duration();
                    float value = tsBetween.Hours * 60 * 60 + tsBetween.Minutes * 60 + tsBetween.Seconds + (float)tsBetween.Milliseconds / 1000;
                    richTextBox1.AppendText("\r\n 处理完毕 , 用时:" + value.ToString() + "秒");

                    TimeSpan tsUsed = tsEnd.Subtract(tsDt).Duration();
                    string text = "用时:" + (tsUsed.Hours.ToString().Length == 1 ? "0" + tsUsed.Hours.ToString() : tsUsed.Hours.ToString());
                    text += ":" + (tsUsed.Minutes.ToString().Length == 1 ? "0" + tsUsed.Minutes.ToString() : tsUsed.Minutes.ToString());
                    text += ":" + (tsUsed.Seconds.ToString().Length == 1 ? "0" + tsUsed.Seconds.ToString() : tsUsed.Seconds.ToString());
                    label6.Text = text;
                    Application.DoEvents();
                }
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
                MessageBox.Show("加载部门列表失败:" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
