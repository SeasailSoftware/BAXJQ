namespace hpt.gate.lcd.standard
{
    partial class FrmLcd
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLcd));
            this.recordTimer = new System.Windows.Forms.Timer(this.components);
            this.countTimer = new System.Windows.Forms.Timer(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label_title = new System.Windows.Forms.Label();
            this.label_time = new System.Windows.Forms.Label();
            this.label_date = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.preView2 = new System.Windows.Forms.PictureBox();
            this.preView1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbRecDatetime = new System.Windows.Forms.Label();
            this.lbIOFlag = new System.Windows.Forms.Label();
            this.lbEmpName = new System.Windows.Forms.Label();
            this.lbEmpCode = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbDept = new System.Windows.Forms.Label();
            this.photo1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1_recdatetime = new System.Windows.Forms.Label();
            this.label1_ioflag = new System.Windows.Forms.Label();
            this.label1_empname = new System.Windows.Forms.Label();
            this.label1_empcode = new System.Windows.Forms.Label();
            this.label_dept = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.photo2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ListSummary = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photo1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photo2)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // recordTimer
            // 
            this.recordTimer.Enabled = true;
            this.recordTimer.Interval = 2000;
            this.recordTimer.Tick += new System.EventHandler(this.recordTimer_Tick);
            // 
            // countTimer
            // 
            this.countTimer.Enabled = true;
            this.countTimer.Interval = 5000;
            this.countTimer.Tick += new System.EventHandler(this.countTimer_Tick);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // label_title
            // 
            this.label_title.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("楷体", 63.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label_title.Location = new System.Drawing.Point(136, 66);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(1307, 114);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "XXX项目组门禁实时信息展示";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_time
            // 
            this.label_time.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_time.BackColor = System.Drawing.Color.Transparent;
            this.label_time.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_time.ForeColor = System.Drawing.Color.Red;
            this.label_time.Location = new System.Drawing.Point(1516, 98);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(319, 54);
            this.label_time.TabIndex = 5;
            this.label_time.Text = "12:01:30  星期一";
            this.label_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_date
            // 
            this.label_date.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_date.BackColor = System.Drawing.Color.Transparent;
            this.label_date.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_date.ForeColor = System.Drawing.Color.Maroon;
            this.label_date.Location = new System.Drawing.Point(1515, 43);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(320, 53);
            this.label_date.TabIndex = 4;
            this.label_date.Text = "2019年01月01日";
            this.label_date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 388F));
            this.tableLayoutPanel1.Controls.Add(this.label13, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.preView2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.preView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(136, 232);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.35135F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.64865F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1669, 773);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label13.Location = new System.Drawing.Point(1283, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(383, 66);
            this.label13.TabIndex = 8;
            this.label13.Text = "人数统计";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(643, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(634, 66);
            this.label2.TabIndex = 6;
            this.label2.Text = "②出口";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // preView2
            // 
            this.preView2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.preView2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("preView2.BackgroundImage")));
            this.preView2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.preView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preView2.Location = new System.Drawing.Point(690, 76);
            this.preView2.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.preView2.Name = "preView2";
            this.preView2.Size = new System.Drawing.Size(540, 343);
            this.preView2.TabIndex = 1;
            this.preView2.TabStop = false;
            // 
            // preView1
            // 
            this.preView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.preView1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("preView1.BackgroundImage")));
            this.preView1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.preView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preView1.Location = new System.Drawing.Point(50, 76);
            this.preView1.Margin = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.preView1.Name = "preView1";
            this.preView1.Size = new System.Drawing.Size(540, 343);
            this.preView1.TabIndex = 1;
            this.preView1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.lbRecDatetime);
            this.panel1.Controls.Add(this.lbIOFlag);
            this.panel1.Controls.Add(this.lbEmpName);
            this.panel1.Controls.Add(this.lbEmpCode);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lbDept);
            this.panel1.Controls.Add(this.photo1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(50, 444);
            this.panel1.Margin = new System.Windows.Forms.Padding(50, 15, 50, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 314);
            this.panel1.TabIndex = 3;
            // 
            // lbRecDatetime
            // 
            this.lbRecDatetime.BackColor = System.Drawing.Color.Transparent;
            this.lbRecDatetime.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRecDatetime.ForeColor = System.Drawing.Color.Navy;
            this.lbRecDatetime.Location = new System.Drawing.Point(351, 239);
            this.lbRecDatetime.Name = "lbRecDatetime";
            this.lbRecDatetime.Size = new System.Drawing.Size(148, 37);
            this.lbRecDatetime.TabIndex = 11;
            this.lbRecDatetime.Text = "                       ";
            this.lbRecDatetime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbIOFlag
            // 
            this.lbIOFlag.BackColor = System.Drawing.Color.Transparent;
            this.lbIOFlag.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbIOFlag.ForeColor = System.Drawing.Color.Navy;
            this.lbIOFlag.Location = new System.Drawing.Point(352, 182);
            this.lbIOFlag.Name = "lbIOFlag";
            this.lbIOFlag.Size = new System.Drawing.Size(156, 37);
            this.lbIOFlag.TabIndex = 10;
            this.lbIOFlag.Text = "                    ";
            this.lbIOFlag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbEmpName
            // 
            this.lbEmpName.BackColor = System.Drawing.Color.Transparent;
            this.lbEmpName.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbEmpName.ForeColor = System.Drawing.Color.Navy;
            this.lbEmpName.Location = new System.Drawing.Point(351, 130);
            this.lbEmpName.Name = "lbEmpName";
            this.lbEmpName.Size = new System.Drawing.Size(157, 37);
            this.lbEmpName.TabIndex = 9;
            this.lbEmpName.Text = "                    ";
            this.lbEmpName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbEmpCode
            // 
            this.lbEmpCode.BackColor = System.Drawing.Color.Transparent;
            this.lbEmpCode.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbEmpCode.ForeColor = System.Drawing.Color.Navy;
            this.lbEmpCode.Location = new System.Drawing.Point(351, 76);
            this.lbEmpCode.Name = "lbEmpCode";
            this.lbEmpCode.Size = new System.Drawing.Size(157, 37);
            this.lbEmpCode.TabIndex = 8;
            this.lbEmpCode.Text = "                    ";
            this.lbEmpCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(255, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 28);
            this.label5.TabIndex = 7;
            this.label5.Text = "刷卡时间:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(254, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 28);
            this.label4.TabIndex = 6;
            this.label4.Text = "进       出:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(254, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "姓       名:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(254, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 28);
            this.label6.TabIndex = 4;
            this.label6.Text = "编       号:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(254, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 28);
            this.label7.TabIndex = 3;
            this.label7.Text = "部      门:";
            // 
            // lbDept
            // 
            this.lbDept.BackColor = System.Drawing.Color.Transparent;
            this.lbDept.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDept.ForeColor = System.Drawing.Color.Navy;
            this.lbDept.Location = new System.Drawing.Point(351, 32);
            this.lbDept.Name = "lbDept";
            this.lbDept.Size = new System.Drawing.Size(157, 37);
            this.lbDept.TabIndex = 2;
            this.lbDept.Text = "                    ";
            this.lbDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // photo1
            // 
            this.photo1.BackColor = System.Drawing.Color.White;
            this.photo1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("photo1.BackgroundImage")));
            this.photo1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.photo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.photo1.Location = new System.Drawing.Point(18, 32);
            this.photo1.Name = "photo1";
            this.photo1.Padding = new System.Windows.Forms.Padding(3);
            this.photo1.Size = new System.Drawing.Size(209, 253);
            this.photo1.TabIndex = 1;
            this.photo1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.label1_recdatetime);
            this.panel2.Controls.Add(this.label1_ioflag);
            this.panel2.Controls.Add(this.label1_empname);
            this.panel2.Controls.Add(this.label1_empcode);
            this.panel2.Controls.Add(this.label_dept);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.photo2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(690, 444);
            this.panel2.Margin = new System.Windows.Forms.Padding(50, 15, 50, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(540, 314);
            this.panel2.TabIndex = 4;
            // 
            // label1_recdatetime
            // 
            this.label1_recdatetime.BackColor = System.Drawing.Color.Transparent;
            this.label1_recdatetime.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_recdatetime.ForeColor = System.Drawing.Color.Navy;
            this.label1_recdatetime.Location = new System.Drawing.Point(368, 230);
            this.label1_recdatetime.Name = "label1_recdatetime";
            this.label1_recdatetime.Size = new System.Drawing.Size(145, 37);
            this.label1_recdatetime.TabIndex = 17;
            this.label1_recdatetime.Text = "                    ";
            this.label1_recdatetime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1_ioflag
            // 
            this.label1_ioflag.BackColor = System.Drawing.Color.Transparent;
            this.label1_ioflag.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_ioflag.ForeColor = System.Drawing.Color.Navy;
            this.label1_ioflag.Location = new System.Drawing.Point(368, 176);
            this.label1_ioflag.Name = "label1_ioflag";
            this.label1_ioflag.Size = new System.Drawing.Size(137, 37);
            this.label1_ioflag.TabIndex = 16;
            this.label1_ioflag.Text = "                    ";
            this.label1_ioflag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1_empname
            // 
            this.label1_empname.BackColor = System.Drawing.Color.Transparent;
            this.label1_empname.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_empname.ForeColor = System.Drawing.Color.Navy;
            this.label1_empname.Location = new System.Drawing.Point(368, 121);
            this.label1_empname.Name = "label1_empname";
            this.label1_empname.Size = new System.Drawing.Size(137, 37);
            this.label1_empname.TabIndex = 15;
            this.label1_empname.Text = "                    ";
            this.label1_empname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1_empcode
            // 
            this.label1_empcode.BackColor = System.Drawing.Color.Transparent;
            this.label1_empcode.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_empcode.ForeColor = System.Drawing.Color.Navy;
            this.label1_empcode.Location = new System.Drawing.Point(368, 76);
            this.label1_empcode.Name = "label1_empcode";
            this.label1_empcode.Size = new System.Drawing.Size(137, 37);
            this.label1_empcode.TabIndex = 14;
            this.label1_empcode.Text = "                    ";
            this.label1_empcode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_dept
            // 
            this.label_dept.BackColor = System.Drawing.Color.Transparent;
            this.label_dept.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_dept.ForeColor = System.Drawing.Color.Navy;
            this.label_dept.Location = new System.Drawing.Point(368, 26);
            this.label_dept.Name = "label_dept";
            this.label_dept.Size = new System.Drawing.Size(137, 37);
            this.label_dept.TabIndex = 13;
            this.label_dept.Text = "                    ";
            this.label_dept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Maroon;
            this.label8.Location = new System.Drawing.Point(260, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 28);
            this.label8.TabIndex = 12;
            this.label8.Text = "刷卡时间:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Maroon;
            this.label9.Location = new System.Drawing.Point(260, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 28);
            this.label9.TabIndex = 11;
            this.label9.Text = "进       出:";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Maroon;
            this.label10.Location = new System.Drawing.Point(260, 125);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 28);
            this.label10.TabIndex = 10;
            this.label10.Text = "姓       名:";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Maroon;
            this.label11.Location = new System.Drawing.Point(260, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 28);
            this.label11.TabIndex = 9;
            this.label11.Text = "编       号:";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Maroon;
            this.label12.Location = new System.Drawing.Point(260, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 28);
            this.label12.TabIndex = 8;
            this.label12.Text = "部      门:";
            // 
            // photo2
            // 
            this.photo2.BackColor = System.Drawing.Color.White;
            this.photo2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("photo2.BackgroundImage")));
            this.photo2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.photo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.photo2.Location = new System.Drawing.Point(19, 29);
            this.photo2.Name = "photo2";
            this.photo2.Padding = new System.Windows.Forms.Padding(3);
            this.photo2.Size = new System.Drawing.Size(213, 256);
            this.photo2.TabIndex = 1;
            this.photo2.TabStop = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(634, 66);
            this.label1.TabIndex = 5;
            this.label1.Text = "①入口";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel3.Controls.Add(this.ListSummary);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1280, 76);
            this.panel3.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.panel3.Name = "panel3";
            this.tableLayoutPanel1.SetRowSpan(this.panel3, 2);
            this.panel3.Size = new System.Drawing.Size(389, 687);
            this.panel3.TabIndex = 7;
            // 
            // ListSummary
            // 
            this.ListSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ListSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.ListSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListSummary.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ListSummary.ForeColor = System.Drawing.Color.Navy;
            this.ListSummary.GridLines = true;
            this.ListSummary.Location = new System.Drawing.Point(0, 0);
            this.ListSummary.Name = "ListSummary";
            this.ListSummary.Size = new System.Drawing.Size(389, 687);
            this.ListSummary.TabIndex = 0;
            this.ListSummary.UseCompatibleStateImageBehavior = false;
            this.ListSummary.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 89;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "工种";
            this.columnHeader2.Width = 166;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "场内总数";
            this.columnHeader3.Width = 127;
            // 
            // FrmLcd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HPT.Gate.Host.Properties.Resources.timg_2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label_time);
            this.Controls.Add(this.label_date);
            this.Controls.Add(this.label_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLcd";
            this.Text = "FrmMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLcd_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.preView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photo1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photo2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer recordTimer;
        private System.Windows.Forms.Timer countTimer;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox preView2;
        private System.Windows.Forms.PictureBox preView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbRecDatetime;
        private System.Windows.Forms.Label lbIOFlag;
        private System.Windows.Forms.Label lbEmpName;
        private System.Windows.Forms.Label lbEmpCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbDept;
        private System.Windows.Forms.PictureBox photo1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1_recdatetime;
        private System.Windows.Forms.Label label1_ioflag;
        private System.Windows.Forms.Label label1_empname;
        private System.Windows.Forms.Label label1_empcode;
        private System.Windows.Forms.Label label_dept;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox photo2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListView ListSummary;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}