using System;
using System.Windows.Forms;
using hpt.gate.Entity;
using hpt.gate.DbTools.Service;
using hpt.gate.client.Base;

namespace hptGate.Attend
{
    public partial class FrmAttdCycleEdit : BaseWindow
    {
        /// <summary>
        /// 新增还是修改标志
        /// </summary>
        public int _Flag;
        /// <summary>
        /// 周期ID
        /// </summary>
        public int _CID;

        /// <summary>
        /// 新增构造函数
        /// </summary>
        public FrmAttdCycleEdit()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ///加载班次列表
            LoadShift();
            comboBox1.SelectedIndex = 0;
        }
        /// <summary>
        /// 修改构造函数
        /// </summary>
        /// <param name="cid"></param>
        public FrmAttdCycleEdit(int cid)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this._CID = cid;
            _Flag = 1;
            ///加载班次列表
            LoadShift();
            try
            {
                AttendCycle attdCycle = AttendService.GetAttendCycleById(cid);
                textBox1.Text = attdCycle.CName;
                comboBox1.SelectedIndex = attdCycle.CType;
                dateTimePicker1.Text = attdCycle.BeginDate;
                dateTimePicker2.Text = attdCycle.EndDate;
                switch (attdCycle.CType)
                {
                    case 0:
                        pWeek.Visible = true;
                        pMonth.Visible = false;
                        comboBox2.SelectedIndex = attdCycle.Week1;
                        comboBox3.SelectedIndex = attdCycle.Week2;
                        comboBox4.SelectedIndex = attdCycle.Week3;
                        comboBox5.SelectedIndex = attdCycle.Week4;
                        comboBox6.SelectedIndex = attdCycle.Week5;
                        comboBox7.SelectedIndex = attdCycle.Week6;
                        comboBox39.SelectedIndex = attdCycle.Week7;
                        break;
                    case 1:
                        pMonth.Visible = true;
                        pWeek.Visible = false;
                        comboBox8.SelectedIndex = attdCycle.Day1;
                        comboBox9.SelectedIndex = attdCycle.Day2;
                        comboBox10.SelectedIndex = attdCycle.Day3;
                        comboBox11.SelectedIndex = attdCycle.Day4;
                        comboBox12.SelectedIndex = attdCycle.Day5;
                        comboBox13.SelectedIndex = attdCycle.Day6;
                        comboBox14.SelectedIndex = attdCycle.Day7;
                        comboBox15.SelectedIndex = attdCycle.Day8;
                        comboBox16.SelectedIndex = attdCycle.Day9;
                        comboBox17.SelectedIndex = attdCycle.Day10;
                        comboBox18.SelectedIndex = attdCycle.Day11;
                        comboBox19.SelectedIndex = attdCycle.Day12;
                        comboBox20.SelectedIndex = attdCycle.Day13;
                        comboBox21.SelectedIndex = attdCycle.Day14;
                        comboBox22.SelectedIndex = attdCycle.Day15;
                        comboBox23.SelectedIndex = attdCycle.Day16;
                        comboBox24.SelectedIndex = attdCycle.Day17;
                        comboBox25.SelectedIndex = attdCycle.Day18;
                        comboBox26.SelectedIndex = attdCycle.Day19;
                        comboBox27.SelectedIndex = attdCycle.Day20;
                        comboBox28.SelectedIndex = attdCycle.Day21;
                        comboBox29.SelectedIndex = attdCycle.Day22;
                        comboBox30.SelectedIndex = attdCycle.Day23;
                        comboBox31.SelectedIndex = attdCycle.Day24;
                        comboBox32.SelectedIndex = attdCycle.Day25;
                        comboBox33.SelectedIndex = attdCycle.Day26;
                        comboBox34.SelectedIndex = attdCycle.Day27;
                        comboBox35.SelectedIndex = attdCycle.Day28;
                        comboBox36.SelectedIndex = attdCycle.Day29;
                        comboBox37.SelectedIndex = attdCycle.Day30;
                        comboBox38.SelectedIndex = attdCycle.Day31;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载考勤周期信息失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            switch (index)
            {
                case 0:
                    pWeek.Visible = true;
                    pMonth.Visible = false;
                    break;
                case 1:
                    pMonth.Visible = true;
                    pWeek.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// 加载班次列表
        /// </summary>
        public void LoadShift()
        {
            try
            {
                /*
                string sql = "Select SID,SName from Shift";
                AttendService.FillComboBox(sql, comboBox2, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox3, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox4, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox5, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox6, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox7, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox8, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox9, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox10, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox11, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox12, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox13, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox14, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox15, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox16, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox17, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox18, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox19, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox20, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox21, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox22, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox23, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox24, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox25, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox26, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox27, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox28, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox29, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox30, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox31, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox32, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox33, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox34, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox35, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox36, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox37, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox38, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox39, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox40, "SName", "SID");
                AttendService.FillComboBox(sql, comboBox41, "SName", "SID");
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载班次列表失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            AttendCycle adc = new AttendCycle();
            adc.CID = _CID;
            string cName = textBox1.Text.Trim().Trim();
            if (cName.Equals(string.Empty))
            {
                MessageBox.Show("考勤周期名称不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            adc.CName = cName;
            int cType = comboBox1.SelectedIndex;
            adc.CType = cType;
            string beginDate = dateTimePicker1.Text;
            adc.BeginDate = beginDate;
            string endDate = dateTimePicker2.Text;
            adc.EndDate = endDate;
            switch (cType)
            {
                case 0:
                    adc.Week1 = comboBox2.SelectedIndex;
                    adc.Week2 = comboBox3.SelectedIndex;
                    adc.Week3 = comboBox4.SelectedIndex;
                    adc.Week4 = comboBox5.SelectedIndex;
                    adc.Week5 = comboBox6.SelectedIndex;
                    adc.Week6 = comboBox7.SelectedIndex;
                    adc.Week7 = comboBox39.SelectedIndex;
                    adc.Day1 = 0;
                    adc.Day2 = 0;
                    adc.Day3 = 0;
                    adc.Day4 = 0;
                    adc.Day5 = 0;
                    adc.Day6 = 0;
                    adc.Day7 = 0;
                    adc.Day8 = 0;
                    adc.Day9 = 0;
                    adc.Day10 = 0;
                    adc.Day11 = 0;
                    adc.Day12 = 0;
                    adc.Day13 = 0;
                    adc.Day14 = 0;
                    adc.Day15 = 0;
                    adc.Day16 = 0;
                    adc.Day17 = 0;
                    adc.Day18 = 0;
                    adc.Day19 = 0;
                    adc.Day20 = 0;
                    adc.Day21 = 0;
                    adc.Day22 = 0;
                    adc.Day23 = 0;
                    adc.Day24 = 0;
                    adc.Day25 = 0;
                    adc.Day26 = 0;
                    adc.Day27 = 0;
                    adc.Day28 = 0;
                    adc.Day29 = 0;
                    adc.Day30 = 0;
                    adc.Day31 = 0;
                    break;
                case 1:
                    adc.Week1 = 0;
                    adc.Week2 = 0;
                    adc.Week3 = 0;
                    adc.Week4 = 0;
                    adc.Week5 = 0;
                    adc.Week6 = 0;
                    adc.Week7 = 0;
                    adc.Day1 = comboBox8.SelectedIndex;
                    adc.Day2 = comboBox9.SelectedIndex;
                    adc.Day3 = comboBox10.SelectedIndex;
                    adc.Day4 = comboBox11.SelectedIndex;
                    adc.Day5 = comboBox12.SelectedIndex;
                    adc.Day6 = comboBox13.SelectedIndex;
                    adc.Day7 = comboBox14.SelectedIndex;
                    adc.Day8 = comboBox15.SelectedIndex;
                    adc.Day9 = comboBox16.SelectedIndex;
                    adc.Day10 = comboBox17.SelectedIndex;
                    adc.Day11 = comboBox18.SelectedIndex;
                    adc.Day12 = comboBox19.SelectedIndex;
                    adc.Day13 = comboBox20.SelectedIndex;
                    adc.Day14 = comboBox21.SelectedIndex;
                    adc.Day15 = comboBox22.SelectedIndex;
                    adc.Day16 = comboBox23.SelectedIndex;
                    adc.Day17 = comboBox24.SelectedIndex;
                    adc.Day18 = comboBox25.SelectedIndex;
                    adc.Day19 = comboBox26.SelectedIndex;
                    adc.Day20 = comboBox27.SelectedIndex;
                    adc.Day21 = comboBox28.SelectedIndex;
                    adc.Day22 = comboBox29.SelectedIndex;
                    adc.Day23 = comboBox30.SelectedIndex;
                    adc.Day24 = comboBox31.SelectedIndex;
                    adc.Day25 = comboBox32.SelectedIndex;
                    adc.Day26 = comboBox33.SelectedIndex;
                    adc.Day27 = comboBox34.SelectedIndex;
                    adc.Day28 = comboBox35.SelectedIndex;
                    adc.Day29 = comboBox36.SelectedIndex;
                    adc.Day30 = comboBox37.SelectedIndex;
                    adc.Day31 = comboBox38.SelectedIndex;
                    break;
            }


            ///新增
            if (_Flag == 0)
            {
                try
                {
                    AttendService.InsertAttendCycle(adc);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("添加考勤周期失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            ///修改
            if (_Flag == 1)
            {
                try
                {
                    AttendService.UpdateAttendCycle(adc);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改考勤周期失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

        }

        private void comboBox40_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                int index = comboBox40.SelectedIndex;
                comboBox2.SelectedIndex = index;
                comboBox3.SelectedIndex = index;
                comboBox4.SelectedIndex = index;
                comboBox5.SelectedIndex = index;
                comboBox6.SelectedIndex = index;
                comboBox7.SelectedIndex = index;
                comboBox39.SelectedIndex = index;
            }
        }

        private void comboBox41_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                int index = comboBox41.SelectedIndex;
                comboBox9.SelectedIndex = index;
                comboBox10.SelectedIndex = index;
                comboBox11.SelectedIndex = index;
                comboBox12.SelectedIndex = index;
                comboBox13.SelectedIndex = index;
                comboBox14.SelectedIndex = index;
                comboBox15.SelectedIndex = index;
                comboBox16.SelectedIndex = index;
                comboBox17.SelectedIndex = index;
                comboBox18.SelectedIndex = index;
                comboBox19.SelectedIndex = index;
                comboBox20.SelectedIndex = index;
                comboBox21.SelectedIndex = index;
                comboBox22.SelectedIndex = index;
                comboBox23.SelectedIndex = index;
                comboBox24.SelectedIndex = index;
                comboBox25.SelectedIndex = index;
                comboBox26.SelectedIndex = index;
                comboBox27.SelectedIndex = index;
                comboBox28.SelectedIndex = index;
                comboBox29.SelectedIndex = index;
                comboBox30.SelectedIndex = index;
                comboBox31.SelectedIndex = index;
                comboBox32.SelectedIndex = index;
                comboBox33.SelectedIndex = index;
                comboBox34.SelectedIndex = index;
                comboBox35.SelectedIndex = index;
            }
        }
    }
}