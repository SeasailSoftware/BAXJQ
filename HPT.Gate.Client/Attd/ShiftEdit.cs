using System;
using System.Windows.Forms;
using hpt.gate.Entity;
using hpt.gate.DbTools.Service;

namespace hptGate.Attend
{
    public partial class ShiftEdit : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// 新增或者修改的标志
        /// </summary>
        public int _Flag;

        /// <summary>
        /// 班次编号
        /// </summary>
        public int _ShiftId;
        /// <summary>
        /// 新增构造函数
        /// </summary>
        public ShiftEdit()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _Flag = 0;
            try
            {
                _ShiftId = AttendService.GetUseAbleShiftId();
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取可用的班次编号失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ///默认为白班
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        /// <summary>
        /// 修改构造函数
        /// </summary>
        /// <param name="shiftId"></param>
        public ShiftEdit(int shiftId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _Flag = 1;
            _ShiftId = shiftId;
            try
            {
                Shift shift = AttendService.GetShiftById(shiftId);
                comboBox1.SelectedIndex = shift.SType;
                textBox1.Text = shift.SName;
                textBox2.Text = shift.SDesc;
                ///时段一
                dateTimePicker1.Text = shift.BeginTime1;
                checkBox1.Checked = shift.Begin_SignIn1 == 1;
                numericUpDown1.Value = shift.Begin_PreEffect1;
                numericUpDown4.Value = shift.Begin_BehindEffect1;

                dateTimePicker4.Text = shift.EndTime1;
                checkBox4.Checked = shift.End_SignOut1 == 1;
                numericUpDown7.Value = shift.End_PreEffect1;
                numericUpDown10.Value = shift.End_BehindEffect1;
                comboBox2.SelectedIndex = shift.AttendType1;
                ///时段二
                dateTimePicker2.Text = shift.BeginTime2;
                checkBox2.Checked = shift.Begin_SignIn2 == 1;
                numericUpDown2.Value = shift.Begin_PreEffect2;
                numericUpDown5.Value = shift.Begin_BehindEffect2;

                dateTimePicker5.Text = shift.EndTime2;
                checkBox5.Checked = shift.End_SignOut2 == 1;
                numericUpDown8.Value = shift.End_PreEffect2;
                numericUpDown11.Value = shift.End_BehindEffect2;
                comboBox3.SelectedIndex = shift.AttendType2;
                ///时段三
                dateTimePicker3.Text = shift.BeginTime3;
                checkBox3.Checked = shift.Begin_SignIn3 == 1;
                numericUpDown3.Value = shift.Begin_PreEffect3;
                numericUpDown6.Value = shift.Begin_BehindEffect3;

                dateTimePicker6.Text = shift.EndTime3;
                checkBox6.Checked = shift.End_SignOut3 == 1;
                numericUpDown9.Value = shift.End_PreEffect3;
                numericUpDown12.Value = shift.End_BehindEffect3;
                comboBox4.SelectedIndex = shift.AttendType3;
                ///标准工时
                numericUpDown13.Value = shift.WorkHour;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载班次信息失败:" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ///判断是否已存在跨天班次的标志
            bool flag = false;
            Shift shift = new Shift();
            shift.SID = _ShiftId;
            int sType = comboBox1.SelectedIndex;
            shift.SType = sType;
            string sName = textBox1.Text.Trim();
            if (sName.Equals(string.Empty))
            {
                MessageBox.Show("班次名称不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            shift.SName = sName;
            string sDesc = textBox2.Text.Trim();
            shift.SDesc = sDesc;

            ///时段一上班时间
            string beginTime1 = dateTimePicker1.Text;
            shift.BeginTime1 = beginTime1;
            int begin_SignIn1 = checkBox1.Checked ? 1 : 0;
            shift.Begin_SignIn1 = begin_SignIn1;
            int begin_PreEffect1 = Convert.ToInt32(numericUpDown1.Value);
            shift.Begin_PreEffect1 = begin_PreEffect1;
            int begin_BehindEffect1 = Convert.ToInt32(numericUpDown4.Value);
            shift.Begin_BehindEffect1 = begin_BehindEffect1;
            ///时段一下班时间
            string endTime1 = dateTimePicker4.Text;
            ///判断时间段一是否合法
            if (shift.SType == 0)
            {
                if (GetTimeSpan(beginTime1, endTime1) < 0)
                {
                    MessageBox.Show("白班上班时间不能大于下班时间!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (GetTimeSpan(beginTime1, endTime1) < 30)
                {
                    MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                if (GetTimeSpan(beginTime1, endTime1) <= 0)
                {
                    if (GetTimeSpan(endTime1, beginTime1) < 30)
                    {
                        MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    flag = true;
                }
                else
                {
                    if (GetTimeSpan(beginTime1, endTime1) < 30)
                    {
                        MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            shift.EndTime1 = endTime1;
            int end_SignOut1 = checkBox4.Checked ? 1 : 0;
            shift.End_SignOut1 = end_SignOut1;
            int end_PreEffect1 = Convert.ToInt32(numericUpDown7.Value);
            shift.End_PreEffect1 = end_PreEffect1;
            int end_BehindEffect1 = Convert.ToInt32(numericUpDown10.Value);
            shift.End_BehindEffect1 = end_BehindEffect1;
            int attendType1 = comboBox2.SelectedIndex;
            shift.AttendType1 = attendType1;
            ///时段二上班时间
            string beginTime2 = dateTimePicker2.Text;
            shift.BeginTime2 = beginTime2;
            int begin_SignIn2 = checkBox2.Checked ? 1 : 0;
            shift.Begin_SignIn2 = begin_SignIn2;
            int begin_PreEffect2 = Convert.ToInt32(numericUpDown2.Value);
            shift.Begin_PreEffect2 = begin_PreEffect2;
            int begin_BehindEffect2 = Convert.ToInt32(numericUpDown5.Value);
            shift.Begin_BehindEffect2 = begin_BehindEffect2;
            ///时段二下班时间
            string endTime2 = dateTimePicker5.Text;
            if (!beginTime2.Equals("00:00") && !endTime2.Equals("00:00"))
            {
                if (shift.SType == 0)
                {
                    if (GetTimeSpan(endTime1, beginTime2) < 0)
                    {
                        MessageBox.Show("时段二上班时间必须大于时段一下班时间!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (GetTimeSpan(beginTime2, endTime2) < 0)
                    {
                        MessageBox.Show("白班上班时间不能大于下班时间!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (GetTimeSpan(beginTime2, endTime2) < 30)
                    {
                        MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    if (GetTimeSpan(endTime1, beginTime2) < 0)
                    {
                        flag = true;
                    }
                    ///存在跨天时间段
                    if (flag)
                    {
                        if (GetTimeSpan(beginTime2, endTime2) < 0)
                        {
                            MessageBox.Show("每个夜班只能有一个跨天的时间段!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        if (GetTimeSpan(beginTime2, endTime2) < 30)
                        {
                            MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        if (GetTimeSpan(beginTime2, endTime2) < 0)
                        {
                            if (GetTimeSpan(beginTime2, endTime2) + 1440 < 30)
                            {
                                MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            flag = true;
                        }
                        else
                        {
                            if (GetTimeSpan(beginTime2, endTime2) < 30)
                            {
                                MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }
            }
            shift.EndTime2 = endTime2;
            int end_SignOut2 = checkBox5.Checked ? 1 : 0;
            shift.End_SignOut2 = end_SignOut2;
            int end_PreEffect2 = Convert.ToInt32(numericUpDown8.Value);
            shift.End_PreEffect2 = end_PreEffect2;
            int end_BehindEffect2 = Convert.ToInt32(numericUpDown11.Value);
            shift.End_BehindEffect2 = end_BehindEffect2;
            int attendType2 = comboBox3.SelectedIndex;
            shift.AttendType2 = attendType2;
            ///时段三上班时间
            string beginTime3 = dateTimePicker3.Text;
            shift.BeginTime3 = beginTime3;
            int begin_SignIn3 = checkBox3.Checked ? 1 : 0;
            shift.Begin_SignIn3 = begin_SignIn3;
            int begin_PreEffect3 = Convert.ToInt32(numericUpDown3.Value);
            shift.Begin_PreEffect3 = begin_PreEffect3;
            int begin_BehindEffect3 = Convert.ToInt32(numericUpDown6.Value);
            shift.Begin_BehindEffect3 = begin_BehindEffect3;
            ///时段三下班时间
            string endTime3 = dateTimePicker6.Text;
            if (!beginTime3.Equals("00:00") && !endTime3.Equals("00:00"))
            {
                if (shift.SType == 0)
                {
                    if (GetTimeSpan(endTime2, beginTime3) < 0)
                    {
                        MessageBox.Show("当前时段上班时间必须大于前一时段的下班时间!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (GetTimeSpan(beginTime3, endTime3) < 0)
                    {
                        MessageBox.Show("白班上班时间不能大于下班时间!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (GetTimeSpan(beginTime3, endTime3) < 30)
                    {
                        MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    ///存在跨天时间段
                    if (flag)
                    {
                        if (GetTimeSpan(endTime2, beginTime3) < 0)
                        {
                            MessageBox.Show("时段三上班时间必须大于时段二下班时间!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (GetTimeSpan(beginTime3, endTime3) < 0)
                        {
                            MessageBox.Show("每个夜班只能有一个跨天的时间段!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        if (GetTimeSpan(beginTime3, endTime3) < 30)
                        {
                            MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        if (GetTimeSpan(beginTime3, endTime3) < 0)
                        {
                            if (GetTimeSpan(beginTime3, endTime3) + 1440 < 30)
                            {
                                MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            flag = true;
                        }
                        else
                        {
                            if (GetTimeSpan(beginTime3, endTime3) < 30)
                            {
                                MessageBox.Show("班次每个时间段不能小于30分钟!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }
            }
            shift.EndTime3 = endTime3;
            int end_SignOut3 = checkBox6.Checked ? 1 : 0;
            shift.End_SignOut3 = end_SignOut3;
            int end_PreEffect3 = Convert.ToInt32(numericUpDown9.Value);
            shift.End_PreEffect3 = end_PreEffect3;
            int end_BehindEffect3 = Convert.ToInt32(numericUpDown12.Value);
            shift.End_BehindEffect3 = end_BehindEffect3;
            int attendType3 = comboBox4.SelectedIndex;
            shift.AttendType3 = attendType3;
            int workHour = Convert.ToInt32(numericUpDown13.Value);
            shift.WorkHour = workHour;

            ///新增
            if (_Flag == 0)
            {
                try
                {
                    AttendService.InsertShift(shift);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("添加班次失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            ///修改
            if (_Flag == 1)
            {
                try
                {
                    AttendService.UpdateShift(shift);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("添加班次失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
        /// <summary>
        /// 返回两个时间相差的分钟数
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int GetTimeSpan(string begin, string end)
        {
            DateTime dtBegin = Convert.ToDateTime(begin);
            DateTime dtEnd = Convert.ToDateTime(end);
            TimeSpan tsBegin = new TimeSpan(dtBegin.Ticks);
            TimeSpan tsEnd = new TimeSpan(dtEnd.Ticks);
            TimeSpan tsBetween = tsEnd.Subtract(tsBegin).Duration();
            int value = tsBetween.Hours * 60 + tsBetween.Minutes;
            return (dtBegin <= dtEnd ? value : -value);
        }

    }
}