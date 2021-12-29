using HPT.Gate.Host.Base;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPT.Gate.Host.DevPara
{
    public partial class FrmTimegroupOfWeekAdd : WinBase
    {
        public FrmTimegroupOfWeekAdd()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = string.Empty;
            if (comboBox1.SelectedIndex == 0)
            {
                s = "本页面所有的时间段将会恢复到'00:00:00',确定操作?";
            }
            else
            {
                s = "本页面所有的将会跟星期一保持一致，确定操作?";
            }
            DialogResult dr = MessageBox.Show(s, "退出系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr != DialogResult.OK) return;
            if (comboBox1.SelectedIndex == 0)
            {
                dateTimePicker1.Text = "00:00";
                dateTimePicker2.Text = "00:00";
                dateTimePicker3.Text = "00:00";
                dateTimePicker4.Text = "00:00";
                dateTimePicker5.Text = "00:00";
                dateTimePicker6.Text = "00:00";
                dateTimePicker7.Text = "00:00";
                dateTimePicker8.Text = "00:00";
                dateTimePicker9.Text = "00:00";
                dateTimePicker10.Text = "00:00";
                dateTimePicker11.Text = "00:00";
                dateTimePicker12.Text = "00:00";
                dateTimePicker13.Text = "00:00";
                dateTimePicker14.Text = "00:00";
                dateTimePicker15.Text = "00:00";
                dateTimePicker16.Text = "00:00";
                dateTimePicker17.Text = "00:00";
                dateTimePicker18.Text = "00:00";
                dateTimePicker19.Text = "00:00";
                dateTimePicker20.Text = "00:00";
                dateTimePicker21.Text = "00:00";
                dateTimePicker22.Text = "00:00";
                dateTimePicker23.Text = "00:00";
                dateTimePicker24.Text = "00:00";
                dateTimePicker25.Text = "00:00";
                dateTimePicker26.Text = "00:00";
                dateTimePicker27.Text = "00:00";
                dateTimePicker28.Text = "00:00";
                dateTimePicker29.Text = "00:00";
                dateTimePicker30.Text = "00:00";
                dateTimePicker31.Text = "00:00";
                dateTimePicker32.Text = "00:00";
                dateTimePicker33.Text = "00:00";
                dateTimePicker34.Text = "00:00";
                dateTimePicker35.Text = "00:00";
                dateTimePicker36.Text = "00:00";
                dateTimePicker37.Text = "00:00";
                dateTimePicker38.Text = "00:00";
                dateTimePicker39.Text = "00:00";
                dateTimePicker40.Text = "00:00";
                dateTimePicker41.Text = "00:00";
                dateTimePicker42.Text = "00:00";
                dateTimePicker43.Text = "00:00";
                dateTimePicker44.Text = "00:00";
                dateTimePicker45.Text = "00:00";
                dateTimePicker46.Text = "00:00";
                dateTimePicker47.Text = "00:00";
                dateTimePicker48.Text = "00:00";
                dateTimePicker49.Text = "00:00";
                dateTimePicker50.Text = "00:00";
                dateTimePicker51.Text = "00:00";
                dateTimePicker52.Text = "00:00";
                dateTimePicker53.Text = "00:00";
                dateTimePicker54.Text = "00:00";
                dateTimePicker55.Text = "00:00";
                dateTimePicker56.Text = "00:00";
            }
            else
            {

                string time1 = dateTimePicker16.Text.Trim();
                string time2 = dateTimePicker15.Text.Trim();
                string time3 = dateTimePicker14.Text.Trim();
                string time4 = dateTimePicker13.Text.Trim();
                string time5 = dateTimePicker12.Text.Trim();
                string time6 = dateTimePicker11.Text.Trim();
                string time7 = dateTimePicker10.Text.Trim();
                string time8 = dateTimePicker9.Text.Trim();
                ///星期天
                dateTimePicker1.Text = time1;
                dateTimePicker2.Text = time2;
                dateTimePicker4.Text = time3;
                dateTimePicker3.Text = time4;
                dateTimePicker6.Text = time5;
                dateTimePicker5.Text = time6;
                dateTimePicker8.Text = time7;
                dateTimePicker7.Text = time8;
                ///星期二
                dateTimePicker24.Text = time1;
                dateTimePicker23.Text = time2;
                dateTimePicker22.Text = time3;
                dateTimePicker21.Text = time4;
                dateTimePicker20.Text = time5;
                dateTimePicker19.Text = time6;
                dateTimePicker18.Text = time7;
                dateTimePicker17.Text = time8;
                ///星期三
                dateTimePicker32.Text = time1;
                dateTimePicker31.Text = time2;
                dateTimePicker30.Text = time3;
                dateTimePicker29.Text = time4;
                dateTimePicker28.Text = time5;
                dateTimePicker27.Text = time6;
                dateTimePicker26.Text = time7;
                dateTimePicker25.Text = time8;
                ///星期四
                dateTimePicker40.Text = time1;
                dateTimePicker39.Text = time2;
                dateTimePicker38.Text = time3;
                dateTimePicker37.Text = time4;
                dateTimePicker36.Text = time5;
                dateTimePicker35.Text = time6;
                dateTimePicker34.Text = time7;
                dateTimePicker33.Text = time8;
                ///星期五
                dateTimePicker48.Text = time1;
                dateTimePicker47.Text = time2;
                dateTimePicker46.Text = time3;
                dateTimePicker45.Text = time4;
                dateTimePicker44.Text = time5;
                dateTimePicker43.Text = time6;
                dateTimePicker42.Text = time7;
                dateTimePicker41.Text = time8;
                ///星期六
                dateTimePicker56.Text = time1;
                dateTimePicker55.Text = time2;
                dateTimePicker54.Text = time3;
                dateTimePicker53.Text = time4;
                dateTimePicker52.Text = time5;
                dateTimePicker51.Text = time6;
                dateTimePicker50.Text = time7;
                dateTimePicker49.Text = time8;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveTimegroupOfWeek();
        }

        #region 保存星期时间组
        private void SaveTimegroupOfWeek()
        {
            var GroupName = textBox1.Text.Trim();
            if (GroupName.Equals(string.Empty))
            {
                MessageBox.Show("新增时间组名字不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var GDesc = textBox2.Text.Trim();
            var time0begin0 = dateTimePicker1.Text.Trim();
            var time0end0 = dateTimePicker2.Text.Trim();
            var time0begin1 = dateTimePicker4.Text.Trim();
            var time0end1 = dateTimePicker3.Text.Trim();
            var time0begin2 = dateTimePicker6.Text.Trim();
            var time0end2 = dateTimePicker5.Text.Trim();
            var time0begin3 = dateTimePicker8.Text.Trim();
            var time0end3 = dateTimePicker7.Text.Trim();
            var time1begin0 = dateTimePicker16.Text.Trim();
            var time1end0 = dateTimePicker15.Text.Trim();
            var time1begin1 = dateTimePicker14.Text.Trim();
            var time1end1 = dateTimePicker13.Text.Trim();
            var time1begin2 = dateTimePicker12.Text.Trim();
            var time1end2 = dateTimePicker11.Text.Trim();
            var time1begin3 = dateTimePicker10.Text.Trim();
            var time1end3 = dateTimePicker9.Text.Trim();
            var time2begin0 = dateTimePicker24.Text.Trim();
            var time2end0 = dateTimePicker23.Text.Trim();
            var time2begin1 = dateTimePicker22.Text.Trim();
            var time2end1 = dateTimePicker21.Text.Trim();
            var time2begin2 = dateTimePicker20.Text.Trim();
            var time2end2 = dateTimePicker19.Text.Trim();
            var time2begin3 = dateTimePicker18.Text.Trim();
            var time2end3 = dateTimePicker17.Text.Trim();
            var time3begin0 = dateTimePicker32.Text.Trim();
            var time3end0 = dateTimePicker31.Text.Trim();
            var time3begin1 = dateTimePicker30.Text.Trim();
            var time3end1 = dateTimePicker29.Text.Trim();
            var time3begin2 = dateTimePicker28.Text.Trim();
            var time3end2 = dateTimePicker27.Text.Trim();
            var time3begin3 = dateTimePicker26.Text.Trim();
            var time3end3 = dateTimePicker25.Text.Trim();
            var time4begin0 = dateTimePicker40.Text.Trim();
            var time4end0 = dateTimePicker39.Text.Trim();
            var time4begin1 = dateTimePicker38.Text.Trim();
            var time4end1 = dateTimePicker37.Text.Trim();
            var time4begin2 = dateTimePicker36.Text.Trim();
            var time4end2 = dateTimePicker35.Text.Trim();
            var time4begin3 = dateTimePicker34.Text.Trim();
            var time4end3 = dateTimePicker33.Text.Trim();
            var time5begin0 = dateTimePicker48.Text.Trim();
            var time5end0 = dateTimePicker47.Text.Trim();
            var time5begin1 = dateTimePicker46.Text.Trim();
            var time5end1 = dateTimePicker45.Text.Trim();
            var time5begin2 = dateTimePicker44.Text.Trim();
            var time5end2 = dateTimePicker43.Text.Trim();
            var time5begin3 = dateTimePicker42.Text.Trim();
            var time5end3 = dateTimePicker41.Text.Trim();
            var time6begin0 = dateTimePicker56.Text.Trim();
            var time6end0 = dateTimePicker55.Text.Trim();
            var time6begin1 = dateTimePicker54.Text.Trim();
            var time6end1 = dateTimePicker53.Text.Trim();
            var time6begin2 = dateTimePicker52.Text.Trim();
            var time6end2 = dateTimePicker51.Text.Trim();
            var time6begin3 = dateTimePicker50.Text.Trim();
            var time6end3 = dateTimePicker49.Text.Trim();
            try
            {
                TimegroupOfDoorService.Insert(GroupName, GDesc,
                time0begin0, time0end0, time0begin1, time0end1, time0begin2, time0end2, time0begin3, time0end3,
                time1begin0, time1end0, time1begin1, time1end1, time1begin2, time1end2, time1begin3, time1end3,
                time2begin0, time2end0, time2begin1, time2end1, time2begin2, time2end2, time2begin3, time2end3,
                time3begin0, time3end0, time3begin1, time3end1, time3begin2, time3end2, time3begin3, time3end3,
                time4begin0, time4end0, time4begin1, time4end1, time4begin2, time4end2, time4begin3, time4end3,
                time5begin0, time5end0, time5begin1, time5end1, time5begin2, time5end2, time5begin3, time5end3,
                time6begin0, time6end0, time6begin1, time6end1, time6begin2, time6end2, time6begin3, time6end3);
                MessageBox.Show("增加门禁时间段成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("在增加门禁时间段过程中发生错误，错误信息:" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        #endregion

        private void FrmTimegroupOfWeekAdd_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Text = "00:00";
            dateTimePicker2.Text = "00:00";
            dateTimePicker3.Text = "00:00";
            dateTimePicker4.Text = "00:00";
            dateTimePicker5.Text = "00:00";
            dateTimePicker6.Text = "00:00";
            dateTimePicker7.Text = "00:00";
            dateTimePicker8.Text = "00:00";
            dateTimePicker9.Text = "00:00";
            dateTimePicker10.Text = "00:00";
            dateTimePicker11.Text = "00:00";
            dateTimePicker12.Text = "00:00";
            dateTimePicker13.Text = "00:00";
            dateTimePicker14.Text = "00:00";
            dateTimePicker15.Text = "00:00";
            dateTimePicker16.Text = "00:00";
            dateTimePicker17.Text = "00:00";
            dateTimePicker18.Text = "00:00";
            dateTimePicker19.Text = "00:00";
            dateTimePicker20.Text = "00:00";
            dateTimePicker21.Text = "00:00";
            dateTimePicker22.Text = "00:00";
            dateTimePicker23.Text = "00:00";
            dateTimePicker24.Text = "00:00";
            dateTimePicker25.Text = "00:00";
            dateTimePicker26.Text = "00:00";
            dateTimePicker27.Text = "00:00";
            dateTimePicker28.Text = "00:00";
            dateTimePicker29.Text = "00:00";
            dateTimePicker30.Text = "00:00";
            dateTimePicker31.Text = "00:00";
            dateTimePicker32.Text = "00:00";
            dateTimePicker33.Text = "00:00";
            dateTimePicker34.Text = "00:00";
            dateTimePicker35.Text = "00:00";
            dateTimePicker36.Text = "00:00";
            dateTimePicker37.Text = "00:00";
            dateTimePicker38.Text = "00:00";
            dateTimePicker39.Text = "00:00";
            dateTimePicker40.Text = "00:00";
            dateTimePicker41.Text = "00:00";
            dateTimePicker42.Text = "00:00";
            dateTimePicker43.Text = "00:00";
            dateTimePicker44.Text = "00:00";
            dateTimePicker45.Text = "00:00";
            dateTimePicker46.Text = "00:00";
            dateTimePicker47.Text = "00:00";
            dateTimePicker48.Text = "00:00";
            dateTimePicker49.Text = "00:00";
            dateTimePicker50.Text = "00:00";
            dateTimePicker51.Text = "00:00";
            dateTimePicker52.Text = "00:00";
            dateTimePicker53.Text = "00:00";
            dateTimePicker54.Text = "00:00";
            dateTimePicker55.Text = "00:00";
            dateTimePicker56.Text = "00:00";
        }

        private void groupPanel3_Click(object sender, EventArgs e)
        {

        }
    }
}
