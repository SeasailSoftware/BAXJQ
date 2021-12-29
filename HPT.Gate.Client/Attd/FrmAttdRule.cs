using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using hpt.gate.Entity;
using hpt.gate.DbTools.Service;
using hpt.gate.client.Base;

namespace hptGate.Attend
{
    public partial class FrmAttdRule : BaseWindow
    {
        public FrmAttdRule()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 更新考勤制度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                AttdRule attendRule = new AttdRule();
                attendRule.WorkMinute = Convert.ToInt32(numericUpDown1.Value);
                attendRule.Late = Convert.ToInt32(numericUpDown2.Value);
                attendRule.LeaveEarly = Convert.ToInt32(numericUpDown3.Value);
                attendRule.NoSignIn = checkBox2.Checked ? 1 : 0;
                attendRule.NoSignInType = comboBox1.SelectedIndex;
                attendRule.NoSignInMInute = Convert.ToInt32(numericUpDown5.Value);
                attendRule.NoSignOut = checkBox3.Checked ? 1 : 0;
                attendRule.NoSignOutType = comboBox2.SelectedIndex;
                attendRule.NoSignOutMinute = Convert.ToInt32(numericUpDown6.Value);
                attendRule.Late_Absent = checkBox4.Checked ? 1 : 0;
                attendRule.Late_Absent_Minute = Convert.ToInt32(numericUpDown7.Value);
                attendRule.Leave_Absent = checkBox5.Checked ? 1 : 0;
                attendRule.Leave_Absent_Minute = Convert.ToInt32(numericUpDown8.Value);
                attendRule.OT = checkBox1.Checked ? 1 : 0;
                attendRule.OT_Minute = Convert.ToInt32(numericUpDown4.Value);

                AttendService.UpdateAttendRule(attendRule);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存考勤制度失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void FAttendRules_Load(object sender, EventArgs e)
        {
            ///加载考勤制度
            LoadAttendRule();
        }

        /// <summary>
        /// 加载考勤制度信息
        /// </summary>
        private void LoadAttendRule()
        {
            try
            {
                AttdRule attendRule = AttendService.GetAttendRules();
                ///标准工时
                numericUpDown1.Value = attendRule.WorkMinute;
                ///迟到设置
                numericUpDown2.Value = attendRule.Late;
                ///早退设置
                numericUpDown3.Value = attendRule.LeaveEarly;
                ///无签到
                checkBox2.Checked = attendRule.NoSignIn == 1;
                comboBox1.SelectedIndex = attendRule.NoSignInType;
                numericUpDown5.Value = attendRule.NoSignInMInute;
                comboBox1.Enabled = checkBox2.Checked;
                numericUpDown5.Enabled = checkBox2.Checked;
                ///无签退
                checkBox3.Checked = attendRule.NoSignOut == 1;
                comboBox2.SelectedIndex = attendRule.NoSignOutType;
                numericUpDown6.Value = attendRule.NoSignOutMinute;
                comboBox2.Enabled = checkBox3.Checked;
                numericUpDown6.Enabled = checkBox3.Checked;
                ///迟到超过
                checkBox4.Checked = attendRule.Late_Absent == 1;
                numericUpDown7.Value = attendRule.Late_Absent_Minute;
                numericUpDown7.Enabled = checkBox4.Checked;
                ///早退超过
                checkBox5.Checked = attendRule.Leave_Absent == 1;
                numericUpDown8.Value = attendRule.Leave_Absent_Minute;
                numericUpDown8.Enabled = checkBox5.Checked;
                ///加班
                checkBox1.Checked = attendRule.OT == 1;
                numericUpDown4.Value = attendRule.OT_Minute;
                numericUpDown4.Enabled = checkBox1.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载考勤制度信息失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox2.Checked;
            numericUpDown5.Enabled = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = checkBox3.Checked;
            numericUpDown6.Enabled = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown7.Enabled = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown8.Enabled = checkBox5.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown4.Enabled = checkBox1.Checked;
        }
    }
}