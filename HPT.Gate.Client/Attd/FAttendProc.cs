using System;
using System.Collections.Generic;
using System.Windows.Forms;
using hpt.gate.Entity;
using hpt.gate.DbTools;
using hpt.gate.DbTools.Service;
using hpt.gate;
using hpt.gate.client.config;
using hpt.gate.client.Tools;
using hpt.gate.Entity.Service;

namespace hptGate.Attend
{
    public partial class FAttendProc : DevComponents.DotNetBar.Office2007Form
    {
        public FAttendProc()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FAttendProc_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select * from DeptInfo";
                ComboBoxHelper.FillComboBox(sql, comboBox1, "DeptName", "DeptId");
            }
            catch (Exception ex)
            {
                MessageBox.Show("���ز����б�ʧ��:" + ex.Message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string firstDay = string.Empty;
            DateTime dt = DateTime.Now;
            firstDay = dt.Year.ToString() + "-" + dt.Month.ToString() + "-01";
            dateTimePicker1.Text = firstDay;
        }

        /// <summary>
        /// ��ʼ���ڷ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            int deptId = Convert.ToInt32(comboBox1.SelectedValue);
            int deptType = checkBox1.Checked ? 1 : 0;
            string empCode = textBox1.Text.Trim();
            string empName = textBox2.Text.Trim();

            List<EmpInfo> empList = EmpInfoService.FindOnCondition(deptId, deptType, empCode, empName);
            if (empList.Count == 0)
            {
                MessageBox.Show("û�пɷ����������Ա!", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            string beginDate = dateTimePicker1.Text;
            string endDate = dateTimePicker2.Text;

            buttonX1.Enabled = false;
            richTextBox1.AppendText("\r\n��ʼ���ڷ�������...");
            Application.DoEvents();
            int index = 0;
            int count = empList.Count;
            toolStripProgressBar1.Value = 0;
            DateTime dt = DateTime.Now;
            TimeSpan tsDt = new TimeSpan(dt.Ticks);
            foreach (EmpInfo emp in empList)
            {
                DateTime dtBegin = DateTime.Now;
                richTextBox1.AppendText("\r\n ���ڴ���������, �������" + emp.EmpCode + " " + emp.EmpName + "...");
                //ɾ����������
                AttendService.CreateAttendData(emp, beginDate, endDate);
                richTextBox1.AppendText("\r\n" + emp.EmpCode + emp.EmpName + "���������������!");

                Application.DoEvents();
                try
                {
                    AttendManager.ProAttendDataOfEmp(emp, beginDate, endDate, AppSettings.AttendBeginTime, AppSettings.AttendEndDay, AppSettings.AttendEndTime);
                    ///AttendDBService.ProAttendDataOfEmp(emp, beginDate, endDate,dbHelper);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��������" + emp.EmpCode + emp.EmpName + "�Ŀ�������ʧ��:" + ex.Message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                richTextBox1.AppendText("\r\n ������� , ��ʱ:" + value.ToString() + "��");

                TimeSpan tsUsed = tsEnd.Subtract(tsDt).Duration();
                string text = "��ʱ:" + (tsUsed.Hours.ToString().Length == 1 ? "0" + tsUsed.Hours.ToString() : tsUsed.Hours.ToString());
                text += ":" + (tsUsed.Minutes.ToString().Length == 1 ? "0" + tsUsed.Minutes.ToString() : tsUsed.Minutes.ToString());
                text += ":" + (tsUsed.Seconds.ToString().Length == 1 ? "0" + tsUsed.Seconds.ToString() : tsUsed.Seconds.ToString());
                label6.Text = text;
                Application.DoEvents();
            }
            ///-------------------------------------------------------
            buttonX1.Enabled = true;
        }
    }
}