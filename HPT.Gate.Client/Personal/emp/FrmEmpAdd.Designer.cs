namespace HPT.Gate.Client
{
    partial class FrmEmpAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmpAdd));
            this.buttonX11 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX8 = new DevComponents.DotNetBar.ButtonX();
            this.label14 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.dtpJoinDate = new System.Windows.Forms.DateTimePicker();
            this.lbLeaveDate = new System.Windows.Forms.Label();
            this.picPhoto = new System.Windows.Forms.PictureBox();
            this.numHireTimes = new System.Windows.Forms.NumericUpDown();
            this.cbbDept = new System.Windows.Forms.ComboBox();
            this.dtpLeaveDate = new System.Windows.Forms.DateTimePicker();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cbbEmpStatus = new System.Windows.Forms.ComboBox();
            this.cbbRehire = new System.Windows.Forms.ComboBox();
            this.cbbSex = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbDuty = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label15 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpBirthday = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbAddress = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label57 = new System.Windows.Forms.Label();
            this.tbIDCardNo = new System.Windows.Forms.TextBox();
            this.buttonX10 = new DevComponents.DotNetBar.ButtonX();
            this.label11 = new System.Windows.Forms.Label();
            this.tbIDSerial = new System.Windows.Forms.TextBox();
            this.buttonX9 = new DevComponents.DotNetBar.ButtonX();
            this.label10 = new System.Windows.Forms.Label();
            this.tbICCardNo = new System.Windows.Forms.TextBox();
            this.buttonX7 = new DevComponents.DotNetBar.ButtonX();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbTicketType = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbIDCard = new DevComponents.DotNetBar.Controls.MaskedTextBoxAdv();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX4 = new System.Windows.Forms.TextBox();
            this.tbNation = new System.Windows.Forms.TextBox();
            this.tbEmpName = new System.Windows.Forms.TextBox();
            this.tbEmpCode = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbFingerPrint = new System.Windows.Forms.GroupBox();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.tbFPData = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHireTimes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbFingerPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.panel2);
            this.panel_main.Controls.Add(this.panel1);
            this.panel_main.Controls.Add(this.groupBox5);
            this.panel_main.Controls.Add(this.groupBox1);
            this.panel_main.Size = new System.Drawing.Size(631, 528);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 558);
            this.panel_bottom.Size = new System.Drawing.Size(631, 42);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(141, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(414, 10);
            this.btnCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // buttonX11
            // 
            this.buttonX11.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX11.Location = new System.Drawing.Point(307, 236);
            this.buttonX11.Name = "buttonX11";
            this.buttonX11.Size = new System.Drawing.Size(120, 23);
            this.buttonX11.TabIndex = 75;
            this.buttonX11.Text = "身份证阅读器录入";
            this.buttonX11.Click += new System.EventHandler(this.buttonX11_Click_1);
            // 
            // buttonX8
            // 
            this.buttonX8.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX8.Location = new System.Drawing.Point(515, 236);
            this.buttonX8.Name = "buttonX8";
            this.buttonX8.Size = new System.Drawing.Size(71, 23);
            this.buttonX8.TabIndex = 74;
            this.buttonX8.Text = "清除人脸";
            this.buttonX8.Click += new System.EventHandler(this.buttonX8_Click_1);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(31, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 59;
            this.label14.Text = "所属部门";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Enabled = false;
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(241, 160);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 12);
            this.label21.TabIndex = 73;
            this.label21.Text = "雇佣次数";
            // 
            // dtpJoinDate
            // 
            this.dtpJoinDate.CustomFormat = "yyyy-MM-dd";
            this.dtpJoinDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpJoinDate.Location = new System.Drawing.Point(102, 130);
            this.dtpJoinDate.Name = "dtpJoinDate";
            this.dtpJoinDate.Size = new System.Drawing.Size(121, 21);
            this.dtpJoinDate.TabIndex = 69;
            // 
            // lbLeaveDate
            // 
            this.lbLeaveDate.AutoSize = true;
            this.lbLeaveDate.BackColor = System.Drawing.Color.Transparent;
            this.lbLeaveDate.Enabled = false;
            this.lbLeaveDate.ForeColor = System.Drawing.Color.Black;
            this.lbLeaveDate.Location = new System.Drawing.Point(240, 189);
            this.lbLeaveDate.Name = "lbLeaveDate";
            this.lbLeaveDate.Size = new System.Drawing.Size(53, 12);
            this.lbLeaveDate.TabIndex = 68;
            this.lbLeaveDate.Text = "离职日期";
            // 
            // picPhoto
            // 
            this.picPhoto.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.picPhoto.BackgroundImage = global::HPT.Gate.Client.Properties.Resources.DefaultPhoto;
            this.picPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPhoto.Location = new System.Drawing.Point(436, 23);
            this.picPhoto.Name = "picPhoto";
            this.picPhoto.Size = new System.Drawing.Size(165, 180);
            this.picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPhoto.TabIndex = 18;
            this.picPhoto.TabStop = false;
            this.picPhoto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.picPhoto.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.picPhoto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // numHireTimes
            // 
            this.numHireTimes.Enabled = false;
            this.numHireTimes.Location = new System.Drawing.Point(300, 155);
            this.numHireTimes.Name = "numHireTimes";
            this.numHireTimes.Size = new System.Drawing.Size(120, 21);
            this.numHireTimes.TabIndex = 72;
            // 
            // cbbDept
            // 
            this.cbbDept.FormattingEnabled = true;
            this.cbbDept.Location = new System.Drawing.Point(102, 23);
            this.cbbDept.Name = "cbbDept";
            this.cbbDept.Size = new System.Drawing.Size(120, 20);
            this.cbbDept.TabIndex = 17;
            this.cbbDept.SelectedIndexChanged += new System.EventHandler(this.cbbDept_SelectedIndexChanged);
            // 
            // dtpLeaveDate
            // 
            this.dtpLeaveDate.CustomFormat = "yyyy-MM-dd";
            this.dtpLeaveDate.Enabled = false;
            this.dtpLeaveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLeaveDate.Location = new System.Drawing.Point(299, 183);
            this.dtpLeaveDate.Name = "dtpLeaveDate";
            this.dtpLeaveDate.Size = new System.Drawing.Size(120, 21);
            this.dtpLeaveDate.TabIndex = 67;
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.Location = new System.Drawing.Point(436, 205);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(71, 23);
            this.buttonX3.TabIndex = 34;
            this.buttonX3.Text = "选择照片";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(31, 190);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 66;
            this.label19.Text = "人员状态";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(31, 161);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 12);
            this.label20.TabIndex = 71;
            this.label20.Text = "是否重雇用";
            // 
            // cbbEmpStatus
            // 
            this.cbbEmpStatus.Enabled = false;
            this.cbbEmpStatus.FormattingEnabled = true;
            this.cbbEmpStatus.Items.AddRange(new object[] {
            "0-在职",
            "1-离职"});
            this.cbbEmpStatus.Location = new System.Drawing.Point(102, 187);
            this.cbbEmpStatus.Name = "cbbEmpStatus";
            this.cbbEmpStatus.Size = new System.Drawing.Size(121, 20);
            this.cbbEmpStatus.TabIndex = 65;
            this.cbbEmpStatus.SelectedIndexChanged += new System.EventHandler(this.cbbEmpStatus_SelectedIndexChanged);
            // 
            // cbbRehire
            // 
            this.cbbRehire.Enabled = false;
            this.cbbRehire.FormattingEnabled = true;
            this.cbbRehire.Items.AddRange(new object[] {
            "0-否",
            "1-是"});
            this.cbbRehire.Location = new System.Drawing.Point(102, 158);
            this.cbbRehire.Name = "cbbRehire";
            this.cbbRehire.Size = new System.Drawing.Size(121, 20);
            this.cbbRehire.TabIndex = 70;
            // 
            // cbbSex
            // 
            this.cbbSex.FormattingEnabled = true;
            this.cbbSex.Items.AddRange(new object[] {
            "男",
            "女"});
            this.cbbSex.Location = new System.Drawing.Point(300, 23);
            this.cbbSex.Name = "cbbSex";
            this.cbbSex.Size = new System.Drawing.Size(120, 20);
            this.cbbSex.TabIndex = 39;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(31, 136);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 64;
            this.label18.Text = "聘用日期";
            // 
            // tbDuty
            // 
            // 
            // 
            // 
            this.tbDuty.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbDuty.ButtonCustom.Tooltip = "";
            this.tbDuty.ButtonCustom2.Tooltip = "";
            this.tbDuty.Location = new System.Drawing.Point(300, 130);
            this.tbDuty.MaxLength = 15;
            this.tbDuty.Name = "tbDuty";
            this.tbDuty.Size = new System.Drawing.Size(120, 15);
            this.tbDuty.TabIndex = 62;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(264, 135);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 61;
            this.label15.Text = "职务";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(264, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "民族";
            // 
            // dtpBirthday
            // 
            this.dtpBirthday.CustomFormat = "yyyy-MM-dd";
            this.dtpBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBirthday.Location = new System.Drawing.Point(300, 103);
            this.dtpBirthday.Name = "dtpBirthday";
            this.dtpBirthday.Size = new System.Drawing.Size(120, 21);
            this.dtpBirthday.TabIndex = 45;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(31, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 58;
            this.label13.Text = "人员编号";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.BackColor = System.Drawing.Color.Transparent;
            this.label55.ForeColor = System.Drawing.Color.Black;
            this.label55.Location = new System.Drawing.Point(264, 108);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(29, 12);
            this.label55.TabIndex = 46;
            this.label55.Text = "出生";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(31, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 57;
            this.label2.Text = "人员姓名";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.Color.Transparent;
            this.label56.ForeColor = System.Drawing.Color.Black;
            this.label56.Location = new System.Drawing.Point(30, 216);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(29, 12);
            this.label56.TabIndex = 47;
            this.label56.Text = "住址";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(31, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 56;
            this.label1.Text = "身份证号码";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(264, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 55;
            this.label12.Text = "电话";
            // 
            // tbAddress
            // 
            // 
            // 
            // 
            this.tbAddress.Border.Class = "TextBoxBorder";
            this.tbAddress.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbAddress.ButtonCustom.Tooltip = "";
            this.tbAddress.ButtonCustom2.Tooltip = "";
            this.tbAddress.Location = new System.Drawing.Point(101, 213);
            this.tbAddress.MaxLength = 100;
            this.tbAddress.Multiline = true;
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(200, 46);
            this.tbAddress.TabIndex = 50;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.ForeColor = System.Drawing.Color.Black;
            this.label57.Location = new System.Drawing.Point(264, 27);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(29, 12);
            this.label57.TabIndex = 51;
            this.label57.Text = "性别";
            // 
            // tbIDCardNo
            // 
            this.tbIDCardNo.Location = new System.Drawing.Point(61, 20);
            this.tbIDCardNo.MaxLength = 20;
            this.tbIDCardNo.Name = "tbIDCardNo";
            this.tbIDCardNo.Size = new System.Drawing.Size(134, 21);
            this.tbIDCardNo.TabIndex = 4;
            // 
            // buttonX10
            // 
            this.buttonX10.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX10.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX10.Image = ((System.Drawing.Image)(resources.GetObject("buttonX10.Image")));
            this.buttonX10.Location = new System.Drawing.Point(130, 47);
            this.buttonX10.Name = "buttonX10";
            this.buttonX10.Size = new System.Drawing.Size(65, 22);
            this.buttonX10.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX10.TabIndex = 5;
            this.buttonX10.Text = "读卡";
            this.buttonX10.Click += new System.EventHandler(this.buttonX10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(26, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "卡号";
            // 
            // tbIDSerial
            // 
            this.tbIDSerial.Enabled = false;
            this.tbIDSerial.Location = new System.Drawing.Point(52, 20);
            this.tbIDSerial.MaxLength = 20;
            this.tbIDSerial.Name = "tbIDSerial";
            this.tbIDSerial.Size = new System.Drawing.Size(144, 21);
            this.tbIDSerial.TabIndex = 4;
            // 
            // buttonX9
            // 
            this.buttonX9.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX9.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX9.Image = ((System.Drawing.Image)(resources.GetObject("buttonX9.Image")));
            this.buttonX9.Location = new System.Drawing.Point(131, 47);
            this.buttonX9.Name = "buttonX9";
            this.buttonX9.Size = new System.Drawing.Size(65, 22);
            this.buttonX9.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX9.TabIndex = 5;
            this.buttonX9.Text = "读卡";
            this.buttonX9.Click += new System.EventHandler(this.buttonX9_Click_2);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(17, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "卡号";
            // 
            // tbICCardNo
            // 
            this.tbICCardNo.Enabled = false;
            this.tbICCardNo.Location = new System.Drawing.Point(57, 20);
            this.tbICCardNo.MaxLength = 20;
            this.tbICCardNo.Name = "tbICCardNo";
            this.tbICCardNo.Size = new System.Drawing.Size(144, 21);
            this.tbICCardNo.TabIndex = 1;
            // 
            // buttonX7
            // 
            this.buttonX7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX7.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX7.Image = ((System.Drawing.Image)(resources.GetObject("buttonX7.Image")));
            this.buttonX7.Location = new System.Drawing.Point(136, 47);
            this.buttonX7.Name = "buttonX7";
            this.buttonX7.Size = new System.Drawing.Size(65, 22);
            this.buttonX7.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX7.TabIndex = 2;
            this.buttonX7.Text = "读卡";
            this.buttonX7.Click += new System.EventHandler(this.buttonX7_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(20, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "卡号";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(291, 46);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(125, 21);
            this.dtpEnd.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(32, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "卡类选择";
            // 
            // cbbTicketType
            // 
            this.cbbTicketType.FormattingEnabled = true;
            this.cbbTicketType.Location = new System.Drawing.Point(91, 20);
            this.cbbTicketType.Name = "cbbTicketType";
            this.cbbTicketType.Size = new System.Drawing.Size(134, 20);
            this.cbbTicketType.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(232, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 17;
            this.label16.Text = "结束日期";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(32, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 15;
            this.label17.Text = "开始日期";
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(91, 46);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(134, 21);
            this.dtpBegin.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbIDCard);
            this.groupBox1.Controls.Add(this.buttonX1);
            this.groupBox1.Controls.Add(this.picPhoto);
            this.groupBox1.Controls.Add(this.textBoxX4);
            this.groupBox1.Controls.Add(this.tbNation);
            this.groupBox1.Controls.Add(this.tbEmpName);
            this.groupBox1.Controls.Add(this.tbEmpCode);
            this.groupBox1.Controls.Add(this.buttonX11);
            this.groupBox1.Controls.Add(this.buttonX8);
            this.groupBox1.Controls.Add(this.label57);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.tbAddress);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dtpJoinDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbLeaveDate);
            this.groupBox1.Controls.Add(this.label56);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numHireTimes);
            this.groupBox1.Controls.Add(this.label55);
            this.groupBox1.Controls.Add(this.cbbDept);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.dtpLeaveDate);
            this.groupBox1.Controls.Add(this.dtpBirthday);
            this.groupBox1.Controls.Add(this.buttonX3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.tbDuty);
            this.groupBox1.Controls.Add(this.cbbEmpStatus);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.cbbSex);
            this.groupBox1.Controls.Add(this.cbbRehire);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(631, 270);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // tbIDCard
            // 
            // 
            // 
            // 
            this.tbIDCard.BackgroundStyle.Class = "TextBoxBorder";
            this.tbIDCard.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbIDCard.ButtonClear.Tooltip = "";
            this.tbIDCard.ButtonClear.Visible = true;
            this.tbIDCard.ButtonCustom.Tooltip = "";
            this.tbIDCard.ButtonCustom2.Tooltip = "";
            this.tbIDCard.ButtonDropDown.Tooltip = "";
            this.tbIDCard.Location = new System.Drawing.Point(101, 103);
            this.tbIDCard.Mask = "00000000000000000A";
            this.tbIDCard.Name = "tbIDCard";
            this.tbIDCard.Size = new System.Drawing.Size(135, 20);
            this.tbIDCard.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tbIDCard.TabIndex = 81;
            this.tbIDCard.Text = "";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Location = new System.Drawing.Point(515, 205);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(71, 23);
            this.buttonX1.TabIndex = 80;
            this.buttonX1.Text = "打开摄像头";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click_1);
            // 
            // textBoxX4
            // 
            this.textBoxX4.Location = new System.Drawing.Point(299, 76);
            this.textBoxX4.Name = "textBoxX4";
            this.textBoxX4.Size = new System.Drawing.Size(121, 21);
            this.textBoxX4.TabIndex = 79;
            // 
            // tbNation
            // 
            this.tbNation.Location = new System.Drawing.Point(300, 49);
            this.tbNation.Name = "tbNation";
            this.tbNation.Size = new System.Drawing.Size(120, 21);
            this.tbNation.TabIndex = 78;
            // 
            // tbEmpName
            // 
            this.tbEmpName.Location = new System.Drawing.Point(102, 76);
            this.tbEmpName.Name = "tbEmpName";
            this.tbEmpName.Size = new System.Drawing.Size(120, 21);
            this.tbEmpName.TabIndex = 77;
            this.tbEmpName.TextChanged += new System.EventHandler(this.tbEmpName_TextChanged);
            // 
            // tbEmpCode
            // 
            this.tbEmpCode.Location = new System.Drawing.Point(102, 49);
            this.tbEmpCode.Name = "tbEmpCode";
            this.tbEmpCode.Size = new System.Drawing.Size(120, 21);
            this.tbEmpCode.TabIndex = 76;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 354);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 86);
            this.panel1.TabIndex = 44;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbIDCardNo);
            this.groupBox4.Controls.Add(this.buttonX10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(420, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(211, 86);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "身份证号码";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbIDSerial);
            this.groupBox3.Controls.Add(this.buttonX9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(210, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 86);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "身份证序列号";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbICCardNo);
            this.groupBox2.Controls.Add(this.buttonX7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 86);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IC/ID卡";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dtpEnd);
            this.groupBox5.Controls.Add(this.cbbTicketType);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.dtpBegin);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 270);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(631, 84);
            this.groupBox5.TabIndex = 44;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "卡类选择";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gbFingerPrint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 440);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(631, 88);
            this.panel2.TabIndex = 45;
            // 
            // gbFingerPrint
            // 
            this.gbFingerPrint.Controls.Add(this.buttonX4);
            this.gbFingerPrint.Controls.Add(this.buttonX2);
            this.gbFingerPrint.Controls.Add(this.tbFPData);
            this.gbFingerPrint.Controls.Add(this.label6);
            this.gbFingerPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFingerPrint.Location = new System.Drawing.Point(0, 0);
            this.gbFingerPrint.Name = "gbFingerPrint";
            this.gbFingerPrint.Size = new System.Drawing.Size(631, 88);
            this.gbFingerPrint.TabIndex = 45;
            this.gbFingerPrint.TabStop = false;
            this.gbFingerPrint.Text = "指纹";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX4.Image = ((System.Drawing.Image)(resources.GetObject("buttonX4.Image")));
            this.buttonX4.Location = new System.Drawing.Point(525, 54);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(90, 22);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 4;
            this.buttonX4.Text = "删除指纹";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click_1);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX2.Image = ((System.Drawing.Image)(resources.GetObject("buttonX2.Image")));
            this.buttonX2.Location = new System.Drawing.Point(525, 26);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(90, 22);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 3;
            this.buttonX2.Text = "录入指纹";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click_1);
            // 
            // tbFPData
            // 
            this.tbFPData.Enabled = false;
            this.tbFPData.Location = new System.Drawing.Point(79, 26);
            this.tbFPData.Multiline = true;
            this.tbFPData.Name = "tbFPData";
            this.tbFPData.Size = new System.Drawing.Size(440, 50);
            this.tbFPData.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(20, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "指纹数据";
            // 
            // FrmEmpAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 600);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEmpAdd";
            this.ShowControllBox = true;
            this.Text = "新增人员信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmEmpAdd_FormClosed);
            this.Load += new System.EventHandler(this.EmpForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrmEmpAdd_MouseClick);
            this.panel_main.ResumeLayout(false);
            this.panel_bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHireTimes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.gbFingerPrint.ResumeLayout(false);
            this.gbFingerPrint.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbbDept;
        private System.Windows.Forms.PictureBox picPhoto;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private System.Windows.Forms.ComboBox cbbSex;
        private System.Windows.Forms.Label label57;
        private DevComponents.DotNetBar.Controls.TextBoxX tbAddress;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.DateTimePicker dtpBirthday;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.ButtonX buttonX7;
        private System.Windows.Forms.TextBox tbICCardNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbTicketType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbLeaveDate;
        private System.Windows.Forms.DateTimePicker dtpLeaveDate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbbEmpStatus;
        private System.Windows.Forms.Label label18;
        private DevComponents.DotNetBar.Controls.TextBoxX tbDuty;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpJoinDate;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown numHireTimes;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbbRehire;
        private DevComponents.DotNetBar.ButtonX buttonX8;
        private System.Windows.Forms.TextBox tbIDCardNo;
        private DevComponents.DotNetBar.ButtonX buttonX10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbIDSerial;
        private DevComponents.DotNetBar.ButtonX buttonX9;
        private System.Windows.Forms.Label label10;
        private DevComponents.DotNetBar.ButtonX buttonX11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbEmpName;
        private System.Windows.Forms.TextBox tbEmpCode;
        private System.Windows.Forms.TextBox textBoxX4;
        private System.Windows.Forms.TextBox tbNation;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.MaskedTextBoxAdv tbIDCard;
        private System.Windows.Forms.GroupBox gbFingerPrint;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.TextBox tbFPData;
        private System.Windows.Forms.Label label6;
    }
}