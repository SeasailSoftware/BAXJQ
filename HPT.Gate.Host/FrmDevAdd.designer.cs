namespace HPT.Gate.Host
{
    partial class FrmDevAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDevAdd));
            this.tbDeviceName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbPlace = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbbDevType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbGateway = new DevComponents.Editors.IpAddressInput();
            this.tbSubnet = new DevComponents.Editors.IpAddressInput();
            this.tbIPAddress = new DevComponents.Editors.IpAddressInput();
            this.tbServerIPAddress = new DevComponents.Editors.IpAddressInput();
            this.numServerPort = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.lbServerIPAddress = new System.Windows.Forms.Label();
            this.numDeviceId = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSoftVersion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbHardVersion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.tbMac = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.dgvOnline = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_main.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGateway)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSubnet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIPAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbServerIPAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDeviceId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOnline)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.groupBox2);
            this.panel_main.Controls.Add(this.groupBox1);
            this.panel_main.Controls.Add(this.panel1);
            this.panel_main.Size = new System.Drawing.Size(532, 447);
            // 
            // tbDeviceName
            // 
            this.tbDeviceName.Location = new System.Drawing.Point(89, 305);
            this.tbDeviceName.Name = "tbDeviceName";
            this.tbDeviceName.Size = new System.Drawing.Size(130, 21);
            this.tbDeviceName.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(30, 308);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 22;
            this.label12.Text = "设备名称";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(54, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "网关";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(30, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "物理地址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(54, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "端口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(42, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP地址";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "所属区域";
            // 
            // cbbPlace
            // 
            this.cbbPlace.FormattingEnabled = true;
            this.cbbPlace.Items.AddRange(new object[] {
            "0-学生宿舍",
            "1-学校大门"});
            this.cbbPlace.Location = new System.Drawing.Point(89, 20);
            this.cbbPlace.Name = "cbbPlace";
            this.cbbPlace.Size = new System.Drawing.Size(130, 20);
            this.cbbPlace.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cbbDevType);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbGateway);
            this.groupBox1.Controls.Add(this.tbSubnet);
            this.groupBox1.Controls.Add(this.tbIPAddress);
            this.groupBox1.Controls.Add(this.tbServerIPAddress);
            this.groupBox1.Controls.Add(this.numServerPort);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lbServerIPAddress);
            this.groupBox1.Controls.Add(this.numDeviceId);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbSoftVersion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbHardVersion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numPort);
            this.groupBox1.Controls.Add(this.tbMac);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cbbPlace);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbDeviceName);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(278, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 400);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备信息";
            // 
            // cbbDevType
            // 
            this.cbbDevType.FormattingEnabled = true;
            this.cbbDevType.Items.AddRange(new object[] {
            "0-门禁设备",
            "1-考勤设备",
            "2-校车考勤"});
            this.cbbDevType.Location = new System.Drawing.Point(89, 46);
            this.cbbDevType.Name = "cbbDevType";
            this.cbbDevType.Size = new System.Drawing.Size(130, 20);
            this.cbbDevType.TabIndex = 66;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(30, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 65;
            this.label9.Text = "闸机类型";
            // 
            // tbGateway
            // 
            this.tbGateway.AutoOverwrite = true;
            // 
            // 
            // 
            this.tbGateway.BackgroundStyle.Class = "DateTimeInputBackground";
            this.tbGateway.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGateway.ButtonClear.Tooltip = "";
            this.tbGateway.ButtonCustom.Tooltip = "";
            this.tbGateway.ButtonCustom2.Tooltip = "";
            this.tbGateway.ButtonDropDown.Tooltip = "";
            this.tbGateway.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.tbGateway.ButtonFreeText.Tooltip = "";
            this.tbGateway.ButtonFreeText.Visible = true;
            this.tbGateway.Location = new System.Drawing.Point(89, 251);
            this.tbGateway.Name = "tbGateway";
            this.tbGateway.Size = new System.Drawing.Size(130, 21);
            this.tbGateway.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tbGateway.TabIndex = 64;
            // 
            // tbSubnet
            // 
            this.tbSubnet.AutoOverwrite = true;
            // 
            // 
            // 
            this.tbSubnet.BackgroundStyle.Class = "DateTimeInputBackground";
            this.tbSubnet.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbSubnet.ButtonClear.Tooltip = "";
            this.tbSubnet.ButtonCustom.Tooltip = "";
            this.tbSubnet.ButtonCustom2.Tooltip = "";
            this.tbSubnet.ButtonDropDown.Tooltip = "";
            this.tbSubnet.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.tbSubnet.ButtonFreeText.Tooltip = "";
            this.tbSubnet.ButtonFreeText.Visible = true;
            this.tbSubnet.Location = new System.Drawing.Point(89, 224);
            this.tbSubnet.Name = "tbSubnet";
            this.tbSubnet.Size = new System.Drawing.Size(130, 21);
            this.tbSubnet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tbSubnet.TabIndex = 63;
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.AutoOverwrite = true;
            // 
            // 
            // 
            this.tbIPAddress.BackgroundStyle.Class = "DateTimeInputBackground";
            this.tbIPAddress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbIPAddress.ButtonClear.Tooltip = "";
            this.tbIPAddress.ButtonCustom.Tooltip = "";
            this.tbIPAddress.ButtonCustom2.Tooltip = "";
            this.tbIPAddress.ButtonDropDown.Tooltip = "";
            this.tbIPAddress.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.tbIPAddress.ButtonFreeText.Tooltip = "";
            this.tbIPAddress.ButtonFreeText.Visible = true;
            this.tbIPAddress.Location = new System.Drawing.Point(89, 197);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(130, 21);
            this.tbIPAddress.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tbIPAddress.TabIndex = 62;
            // 
            // tbServerIPAddress
            // 
            this.tbServerIPAddress.AutoOverwrite = true;
            // 
            // 
            // 
            this.tbServerIPAddress.BackgroundStyle.Class = "DateTimeInputBackground";
            this.tbServerIPAddress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbServerIPAddress.ButtonClear.Tooltip = "";
            this.tbServerIPAddress.ButtonCustom.Tooltip = "";
            this.tbServerIPAddress.ButtonCustom2.Tooltip = "";
            this.tbServerIPAddress.ButtonDropDown.Tooltip = "";
            this.tbServerIPAddress.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.tbServerIPAddress.ButtonFreeText.Tooltip = "";
            this.tbServerIPAddress.ButtonFreeText.Visible = true;
            this.tbServerIPAddress.Location = new System.Drawing.Point(89, 143);
            this.tbServerIPAddress.Name = "tbServerIPAddress";
            this.tbServerIPAddress.Size = new System.Drawing.Size(130, 21);
            this.tbServerIPAddress.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tbServerIPAddress.TabIndex = 61;
            // 
            // numServerPort
            // 
            this.numServerPort.Location = new System.Drawing.Point(89, 170);
            this.numServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numServerPort.Name = "numServerPort";
            this.numServerPort.Size = new System.Drawing.Size(130, 21);
            this.numServerPort.TabIndex = 60;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(18, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 59;
            this.label10.Text = "服务器端口";
            // 
            // lbServerIPAddress
            // 
            this.lbServerIPAddress.AutoSize = true;
            this.lbServerIPAddress.BackColor = System.Drawing.Color.Transparent;
            this.lbServerIPAddress.Location = new System.Drawing.Point(6, 146);
            this.lbServerIPAddress.Name = "lbServerIPAddress";
            this.lbServerIPAddress.Size = new System.Drawing.Size(77, 12);
            this.lbServerIPAddress.TabIndex = 57;
            this.lbServerIPAddress.Text = "服务器IP地址";
            // 
            // numDeviceId
            // 
            this.numDeviceId.Location = new System.Drawing.Point(89, 116);
            this.numDeviceId.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numDeviceId.Name = "numDeviceId";
            this.numDeviceId.ReadOnly = true;
            this.numDeviceId.Size = new System.Drawing.Size(130, 21);
            this.numDeviceId.TabIndex = 56;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(42, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 55;
            this.label7.Text = "机器号";
            // 
            // tbSoftVersion
            // 
            this.tbSoftVersion.Location = new System.Drawing.Point(89, 359);
            this.tbSoftVersion.Name = "tbSoftVersion";
            this.tbSoftVersion.ReadOnly = true;
            this.tbSoftVersion.Size = new System.Drawing.Size(130, 21);
            this.tbSoftVersion.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(30, 362);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 53;
            this.label5.Text = "软件版本";
            // 
            // tbHardVersion
            // 
            this.tbHardVersion.Location = new System.Drawing.Point(89, 332);
            this.tbHardVersion.Name = "tbHardVersion";
            this.tbHardVersion.ReadOnly = true;
            this.tbHardVersion.Size = new System.Drawing.Size(130, 21);
            this.tbHardVersion.TabIndex = 52;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(30, 335);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 51;
            this.label6.Text = "硬件版本";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(89, 278);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(130, 21);
            this.numPort.TabIndex = 48;
            // 
            // tbMac
            // 
            this.tbMac.Location = new System.Drawing.Point(89, 87);
            this.tbMac.Name = "tbMac";
            this.tbMac.Size = new System.Drawing.Size(130, 21);
            this.tbMac.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(30, 227);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 28;
            this.label11.Text = "子网掩码";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonX3);
            this.panel1.Controls.Add(this.buttonX2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 47);
            this.panel1.TabIndex = 26;
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonX3.BackgroundImage")));
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX3.Location = new System.Drawing.Point(349, 12);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(75, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 27;
            this.buttonX3.Text = "取消(C)";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonX2.BackgroundImage")));
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX2.Location = new System.Drawing.Point(109, 12);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 26;
            this.buttonX2.Text = "确定(S)";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonX4);
            this.groupBox2.Controls.Add(this.dgvOnline);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 400);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "在线设备";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX4.Location = new System.Drawing.Point(216, 12);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(54, 23);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 28;
            this.buttonX4.Text = "刷新";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // dgvOnline
            // 
            this.dgvOnline.AllowUserToAddRows = false;
            this.dgvOnline.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOnline.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvOnline.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOnline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOnline.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvOnline.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvOnline.Location = new System.Drawing.Point(3, 41);
            this.dgvOnline.MultiSelect = false;
            this.dgvOnline.Name = "dgvOnline";
            this.dgvOnline.RowHeadersVisible = false;
            this.dgvOnline.RowTemplate.Height = 23;
            this.dgvOnline.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOnline.Size = new System.Drawing.Size(272, 356);
            this.dgvOnline.TabIndex = 27;
            this.dgvOnline.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOnline_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "IP地址";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "端口";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Mac";
            this.Column3.Name = "Column3";
            // 
            // FrmDevAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(532, 477);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDevAdd";
            this.Text = "设备登记";
            this.Load += new System.EventHandler(this.DeviceForm_Load);
            this.panel_main.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGateway)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSubnet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIPAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbServerIPAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDeviceId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOnline)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbDeviceName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbbPlace;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbMac;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.TextBox tbSoftVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbHardVersion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numDeviceId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numServerPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbServerIPAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private System.Windows.Forms.DataGridView dgvOnline;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private DevComponents.Editors.IpAddressInput tbServerIPAddress;
        private DevComponents.Editors.IpAddressInput tbGateway;
        private DevComponents.Editors.IpAddressInput tbSubnet;
        private DevComponents.Editors.IpAddressInput tbIPAddress;
        private System.Windows.Forms.ComboBox cbbDevType;
        private System.Windows.Forms.Label label9;
    }
}