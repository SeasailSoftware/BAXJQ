namespace HPT.Gate.Client.Attend
{
    partial class FrmTimegroupOfShiftAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTimegroupOfShiftAdd));
            this.cbbMustSignOut = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbbMustSignIn = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.numMinute = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpEndTime2 = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpBeginTime2 = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpEndTime1 = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpBeginTime1 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numEarly = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numLate = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpTime2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTime1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tbDay = new System.Windows.Forms.TextBox();
            this.cbbOTSignOut = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cbbOTSignIn = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEarly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.label1);
            this.panel_main.Controls.Add(this.tbName);
            this.panel_main.Controls.Add(this.label2);
            this.panel_main.Controls.Add(this.dtpTime1);
            this.panel_main.Controls.Add(this.label3);
            this.panel_main.Controls.Add(this.dtpTime2);
            this.panel_main.Controls.Add(this.label4);
            this.panel_main.Controls.Add(this.numLate);
            this.panel_main.Controls.Add(this.label5);
            this.panel_main.Controls.Add(this.label7);
            this.panel_main.Controls.Add(this.numEarly);
            this.panel_main.Controls.Add(this.label6);
            this.panel_main.Controls.Add(this.dtpBeginTime1);
            this.panel_main.Controls.Add(this.label8);
            this.panel_main.Controls.Add(this.label9);
            this.panel_main.Controls.Add(this.dtpEndTime1);
            this.panel_main.Controls.Add(this.label10);
            this.panel_main.Controls.Add(this.dtpBeginTime2);
            this.panel_main.Controls.Add(this.label11);
            this.panel_main.Controls.Add(this.dtpEndTime2);
            this.panel_main.Controls.Add(this.label12);
            this.panel_main.Controls.Add(this.label13);
            this.panel_main.Controls.Add(this.numMinute);
            this.panel_main.Controls.Add(this.label14);
            this.panel_main.Controls.Add(this.cbbMustSignIn);
            this.panel_main.Controls.Add(this.label15);
            this.panel_main.Controls.Add(this.cbbMustSignOut);
            this.panel_main.Controls.Add(this.label16);
            this.panel_main.Controls.Add(this.label17);
            this.panel_main.Controls.Add(this.tbDay);
            this.panel_main.Controls.Add(this.label19);
            this.panel_main.Controls.Add(this.cbbOTSignIn);
            this.panel_main.Controls.Add(this.label18);
            this.panel_main.Controls.Add(this.cbbOTSignOut);
            this.panel_main.Size = new System.Drawing.Size(376, 433);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 463);
            this.panel_bottom.Size = new System.Drawing.Size(376, 42);
            // 
            // btCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(237, 10);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(65, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbbMustSignOut
            // 
            this.cbbMustSignOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMustSignOut.FormattingEnabled = true;
            this.cbbMustSignOut.Items.AddRange(new object[] {
            "0-否",
            "1-是"});
            this.cbbMustSignOut.Location = new System.Drawing.Point(167, 326);
            this.cbbMustSignOut.Name = "cbbMustSignOut";
            this.cbbMustSignOut.Size = new System.Drawing.Size(135, 20);
            this.cbbMustSignOut.TabIndex = 27;
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Location = new System.Drawing.Point(61, 326);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 23);
            this.label15.TabIndex = 26;
            this.label15.Text = "是否必须签退";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbbMustSignIn
            // 
            this.cbbMustSignIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMustSignIn.FormattingEnabled = true;
            this.cbbMustSignIn.Items.AddRange(new object[] {
            "0-否",
            "1-是"});
            this.cbbMustSignIn.Location = new System.Drawing.Point(167, 300);
            this.cbbMustSignIn.Name = "cbbMustSignIn";
            this.cbbMustSignIn.Size = new System.Drawing.Size(135, 20);
            this.cbbMustSignIn.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Location = new System.Drawing.Point(61, 300);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 24;
            this.label14.Text = "是否必须签到";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numMinute
            // 
            this.numMinute.Location = new System.Drawing.Point(167, 273);
            this.numMinute.Name = "numMinute";
            this.numMinute.Size = new System.Drawing.Size(79, 21);
            this.numMinute.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Location = new System.Drawing.Point(61, 275);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 22;
            this.label13.Text = "记为";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(61, 248);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 23);
            this.label12.TabIndex = 20;
            this.label12.Text = "记为";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpEndTime2
            // 
            this.dtpEndTime2.CustomFormat = "HH:mm";
            this.dtpEndTime2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime2.Location = new System.Drawing.Point(167, 219);
            this.dtpEndTime2.Name = "dtpEndTime2";
            this.dtpEndTime2.ShowUpDown = true;
            this.dtpEndTime2.Size = new System.Drawing.Size(138, 21);
            this.dtpEndTime2.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(61, 222);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 18;
            this.label11.Text = "结束签退时间";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpBeginTime2
            // 
            this.dtpBeginTime2.CustomFormat = "HH:mm";
            this.dtpBeginTime2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginTime2.Location = new System.Drawing.Point(167, 192);
            this.dtpBeginTime2.Name = "dtpBeginTime2";
            this.dtpBeginTime2.ShowUpDown = true;
            this.dtpBeginTime2.Size = new System.Drawing.Size(138, 21);
            this.dtpBeginTime2.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(61, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 16;
            this.label10.Text = "开始签退时间";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpEndTime1
            // 
            this.dtpEndTime1.CustomFormat = "HH:mm";
            this.dtpEndTime1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime1.Location = new System.Drawing.Point(167, 165);
            this.dtpEndTime1.Name = "dtpEndTime1";
            this.dtpEndTime1.ShowUpDown = true;
            this.dtpEndTime1.Size = new System.Drawing.Size(138, 21);
            this.dtpEndTime1.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(61, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 23);
            this.label9.TabIndex = 14;
            this.label9.Text = "结束签到时间";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpBeginTime1
            // 
            this.dtpBeginTime1.CustomFormat = "HH:mm";
            this.dtpBeginTime1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginTime1.Location = new System.Drawing.Point(167, 138);
            this.dtpBeginTime1.Name = "dtpBeginTime1";
            this.dtpBeginTime1.ShowUpDown = true;
            this.dtpBeginTime1.Size = new System.Drawing.Size(138, 21);
            this.dtpBeginTime1.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(61, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 12;
            this.label8.Text = "开始签到时间";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(231, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "分钟记为早退";
            // 
            // numEarly
            // 
            this.numEarly.Location = new System.Drawing.Point(167, 111);
            this.numEarly.Name = "numEarly";
            this.numEarly.Size = new System.Drawing.Size(58, 21);
            this.numEarly.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Location = new System.Drawing.Point(61, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 9;
            this.label7.Text = "提前下班时间";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(231, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "分钟记为迟到";
            // 
            // numLate
            // 
            this.numLate.Location = new System.Drawing.Point(167, 84);
            this.numLate.Name = "numLate";
            this.numLate.Size = new System.Drawing.Size(58, 21);
            this.numLate.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(61, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "超过上班时间";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTime2
            // 
            this.dtpTime2.CustomFormat = "HH:mm";
            this.dtpTime2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime2.Location = new System.Drawing.Point(167, 57);
            this.dtpTime2.Name = "dtpTime2";
            this.dtpTime2.ShowUpDown = true;
            this.dtpTime2.Size = new System.Drawing.Size(138, 21);
            this.dtpTime2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(61, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "下班时间";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTime1
            // 
            this.dtpTime1.CustomFormat = "HH:mm";
            this.dtpTime1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime1.Location = new System.Drawing.Point(167, 30);
            this.dtpTime1.Name = "dtpTime1";
            this.dtpTime1.ShowUpDown = true;
            this.dtpTime1.Size = new System.Drawing.Size(138, 21);
            this.dtpTime1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(61, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "上班时间";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(167, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(138, 21);
            this.tbName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(61, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "时间段名称";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(252, 248);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 30;
            this.label16.Text = "个工作日";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(273, 275);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 31;
            this.label17.Text = "分钟";
            // 
            // tbDay
            // 
            this.tbDay.Location = new System.Drawing.Point(167, 245);
            this.tbDay.Name = "tbDay";
            this.tbDay.Size = new System.Drawing.Size(79, 21);
            this.tbDay.TabIndex = 32;
            this.tbDay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // cbbOTSignOut
            // 
            this.cbbOTSignOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOTSignOut.FormattingEnabled = true;
            this.cbbOTSignOut.Items.AddRange(new object[] {
            "0-否",
            "1-是"});
            this.cbbOTSignOut.Location = new System.Drawing.Point(167, 378);
            this.cbbOTSignOut.Name = "cbbOTSignOut";
            this.cbbOTSignOut.Size = new System.Drawing.Size(135, 20);
            this.cbbOTSignOut.TabIndex = 36;
            // 
            // label18
            // 
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Location = new System.Drawing.Point(61, 378);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 23);
            this.label18.TabIndex = 35;
            this.label18.Text = "是否计算下班后加班";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbbOTSignIn
            // 
            this.cbbOTSignIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOTSignIn.FormattingEnabled = true;
            this.cbbOTSignIn.Items.AddRange(new object[] {
            "0-否",
            "1-是"});
            this.cbbOTSignIn.Location = new System.Drawing.Point(167, 352);
            this.cbbOTSignIn.Name = "cbbOTSignIn";
            this.cbbOTSignIn.Size = new System.Drawing.Size(135, 20);
            this.cbbOTSignIn.TabIndex = 34;
            // 
            // label19
            // 
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.Location = new System.Drawing.Point(61, 352);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 23);
            this.label19.TabIndex = 33;
            this.label19.Text = "是否计算上班前加班";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmTimegroupOfShiftAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(376, 505);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTimegroupOfShiftAdd";
            this.Text = "添加时间段";
            this.Load += new System.EventHandler(this.FrmTimegroupOfShiftAdd_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEarly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTime1;
        private System.Windows.Forms.DateTimePicker dtpTime2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numLate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numEarly;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpBeginTime1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpEndTime1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpBeginTime2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpEndTime2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numMinute;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbbMustSignIn;
        private System.Windows.Forms.ComboBox cbbMustSignOut;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbDay;
        private System.Windows.Forms.ComboBox cbbOTSignOut;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbbOTSignIn;
        private System.Windows.Forms.Label label19;
    }
}