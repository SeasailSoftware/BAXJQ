namespace HPT.Gate.Host
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.numLimitTotal = new System.Windows.Forms.NumericUpDown();
            this.numLocalPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbCamModule = new System.Windows.Forms.CheckBox();
            this.ckbLimitTotalEnabled = new System.Windows.Forms.CheckBox();
            this.ckbLedModule = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbAutoClear = new System.Windows.Forms.CheckBox();
            this.dtpAutoClear = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.ckbAutoBak = new System.Windows.Forms.CheckBox();
            this.ckbFaceEnabled = new System.Windows.Forms.CheckBox();
            this.ckbSynCardData = new System.Windows.Forms.CheckBox();
            this.cbbNetcamType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbJMSFilter = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbJMSAccount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbJMSPassword = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbServerURL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbJMSServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbFPType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbbRightsType = new System.Windows.Forms.ComboBox();
            this.ckbFingerPrint = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbbFaceOutPutType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbFaceMachine = new System.Windows.Forms.ComboBox();
            this.tbJMSClient = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLimitTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLocalPort)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.groupBox2);
            this.panel_main.Controls.Add(this.panel1);
            this.panel_main.Size = new System.Drawing.Size(428, 559);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "本机监听端口";
            // 
            // numLimitTotal
            // 
            this.numLimitTotal.Location = new System.Drawing.Point(136, 289);
            this.numLimitTotal.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numLimitTotal.Name = "numLimitTotal";
            this.numLimitTotal.Size = new System.Drawing.Size(61, 21);
            this.numLimitTotal.TabIndex = 10;
            // 
            // numLocalPort
            // 
            this.numLocalPort.Location = new System.Drawing.Point(115, 20);
            this.numLocalPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numLocalPort.Name = "numLocalPort";
            this.numLocalPort.Size = new System.Drawing.Size(138, 21);
            this.numLocalPort.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(203, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "禁止进入";
            // 
            // ckbCamModule
            // 
            this.ckbCamModule.AutoSize = true;
            this.ckbCamModule.BackColor = System.Drawing.Color.Transparent;
            this.ckbCamModule.Location = new System.Drawing.Point(34, 103);
            this.ckbCamModule.Name = "ckbCamModule";
            this.ckbCamModule.Size = new System.Drawing.Size(132, 16);
            this.ckbCamModule.TabIndex = 1;
            this.ckbCamModule.Text = "启用摄像头抓拍功能";
            this.ckbCamModule.UseVisualStyleBackColor = false;
            // 
            // ckbLimitTotalEnabled
            // 
            this.ckbLimitTotalEnabled.AutoSize = true;
            this.ckbLimitTotalEnabled.BackColor = System.Drawing.Color.Transparent;
            this.ckbLimitTotalEnabled.Location = new System.Drawing.Point(34, 290);
            this.ckbLimitTotalEnabled.Name = "ckbLimitTotalEnabled";
            this.ckbLimitTotalEnabled.Size = new System.Drawing.Size(96, 16);
            this.ckbLimitTotalEnabled.TabIndex = 7;
            this.ckbLimitTotalEnabled.Text = "场内人数达到";
            this.ckbLimitTotalEnabled.UseVisualStyleBackColor = false;
            // 
            // ckbLedModule
            // 
            this.ckbLedModule.AutoSize = true;
            this.ckbLedModule.BackColor = System.Drawing.Color.Transparent;
            this.ckbLedModule.Location = new System.Drawing.Point(34, 81);
            this.ckbLedModule.Name = "ckbLedModule";
            this.ckbLedModule.Size = new System.Drawing.Size(138, 16);
            this.ckbLedModule.TabIndex = 0;
            this.ckbLedModule.Text = "启用Led实时显示功能";
            this.ckbLedModule.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(206, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "自动清零";
            // 
            // ckbAutoClear
            // 
            this.ckbAutoClear.AutoSize = true;
            this.ckbAutoClear.BackColor = System.Drawing.Color.Transparent;
            this.ckbAutoClear.Location = new System.Drawing.Point(37, 258);
            this.ckbAutoClear.Name = "ckbAutoClear";
            this.ckbAutoClear.Size = new System.Drawing.Size(96, 16);
            this.ckbAutoClear.TabIndex = 4;
            this.ckbAutoClear.Text = "场内人数每天";
            this.ckbAutoClear.UseVisualStyleBackColor = false;
            // 
            // dtpAutoClear
            // 
            this.dtpAutoClear.CustomFormat = "HH:mm";
            this.dtpAutoClear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAutoClear.Location = new System.Drawing.Point(139, 255);
            this.dtpAutoClear.Name = "dtpAutoClear";
            this.dtpAutoClear.ShowUpDown = true;
            this.dtpAutoClear.Size = new System.Drawing.Size(61, 21);
            this.dtpAutoClear.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonX2);
            this.panel1.Controls.Add(this.buttonX1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 515);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 44);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX2.Location = new System.Drawing.Point(274, 11);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 1;
            this.buttonX2.Text = "取消(C)";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX1.Location = new System.Drawing.Point(79, 11);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Text = "确定(S)";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // tabItem2
            // 
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "功能配置";
            // 
            // tabItem1
            // 
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "数据库配置";
            // 
            // ckbAutoBak
            // 
            this.ckbAutoBak.AutoSize = true;
            this.ckbAutoBak.BackColor = System.Drawing.Color.Transparent;
            this.ckbAutoBak.Location = new System.Drawing.Point(37, 316);
            this.ckbAutoBak.Name = "ckbAutoBak";
            this.ckbAutoBak.Size = new System.Drawing.Size(156, 16);
            this.ckbAutoBak.TabIndex = 39;
            this.ckbAutoBak.Text = "程序关闭后自动备份数据";
            this.ckbAutoBak.UseVisualStyleBackColor = false;
            // 
            // ckbFaceEnabled
            // 
            this.ckbFaceEnabled.AutoSize = true;
            this.ckbFaceEnabled.BackColor = System.Drawing.Color.Transparent;
            this.ckbFaceEnabled.Location = new System.Drawing.Point(34, 129);
            this.ckbFaceEnabled.Name = "ckbFaceEnabled";
            this.ckbFaceEnabled.Size = new System.Drawing.Size(120, 16);
            this.ckbFaceEnabled.TabIndex = 40;
            this.ckbFaceEnabled.Text = "启用人脸识别功能";
            this.ckbFaceEnabled.UseVisualStyleBackColor = false;
            // 
            // ckbSynCardData
            // 
            this.ckbSynCardData.AutoSize = true;
            this.ckbSynCardData.BackColor = System.Drawing.Color.Transparent;
            this.ckbSynCardData.Location = new System.Drawing.Point(37, 227);
            this.ckbSynCardData.Name = "ckbSynCardData";
            this.ckbSynCardData.Size = new System.Drawing.Size(144, 16);
            this.ckbSynCardData.TabIndex = 41;
            this.ckbSynCardData.Text = "是否同步卡信息到闸机";
            this.ckbSynCardData.UseVisualStyleBackColor = false;
            // 
            // cbbNetcamType
            // 
            this.cbbNetcamType.FormattingEnabled = true;
            this.cbbNetcamType.Items.AddRange(new object[] {
            "0-海康威视网络摄像头",
            "1-大华网络摄像头"});
            this.cbbNetcamType.Location = new System.Drawing.Point(246, 101);
            this.cbbNetcamType.Name = "cbbNetcamType";
            this.cbbNetcamType.Size = new System.Drawing.Size(149, 20);
            this.cbbNetcamType.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(175, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 43;
            this.label6.Text = "摄像头品牌";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbJMSClient);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.tbJMSFilter);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.tbJMSAccount);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tbJMSPassword);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.tbServerURL);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbJMSServer);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbbFPType);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.cbbRightsType);
            this.groupBox2.Controls.Add(this.ckbFingerPrint);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbbFaceOutPutType);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbbFaceMachine);
            this.groupBox2.Controls.Add(this.ckbAutoBak);
            this.groupBox2.Controls.Add(this.numLocalPort);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numLimitTotal);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.ckbLimitTotalEnabled);
            this.groupBox2.Controls.Add(this.ckbLedModule);
            this.groupBox2.Controls.Add(this.cbbNetcamType);
            this.groupBox2.Controls.Add(this.dtpAutoClear);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ckbSynCardData);
            this.groupBox2.Controls.Add(this.ckbAutoClear);
            this.groupBox2.Controls.Add(this.ckbFaceEnabled);
            this.groupBox2.Controls.Add(this.ckbCamModule);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 515);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "功能设置";
            // 
            // tbJMSFilter
            // 
            this.tbJMSFilter.Location = new System.Drawing.Point(100, 446);
            this.tbJMSFilter.Name = "tbJMSFilter";
            this.tbJMSFilter.Size = new System.Drawing.Size(287, 21);
            this.tbJMSFilter.TabIndex = 63;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(31, 449);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 62;
            this.label13.Text = "JMS过滤器";
            // 
            // tbJMSAccount
            // 
            this.tbJMSAccount.Location = new System.Drawing.Point(100, 392);
            this.tbJMSAccount.Name = "tbJMSAccount";
            this.tbJMSAccount.Size = new System.Drawing.Size(287, 21);
            this.tbJMSAccount.TabIndex = 61;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 396);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 60;
            this.label8.Text = "JMS帐号";
            // 
            // tbJMSPassword
            // 
            this.tbJMSPassword.Location = new System.Drawing.Point(100, 419);
            this.tbJMSPassword.Name = "tbJMSPassword";
            this.tbJMSPassword.PasswordChar = '*';
            this.tbJMSPassword.Size = new System.Drawing.Size(287, 21);
            this.tbJMSPassword.TabIndex = 59;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(43, 422);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 58;
            this.label12.Text = "JMS密码";
            // 
            // tbServerURL
            // 
            this.tbServerURL.Location = new System.Drawing.Point(100, 338);
            this.tbServerURL.Name = "tbServerURL";
            this.tbServerURL.Size = new System.Drawing.Size(287, 21);
            this.tbServerURL.TabIndex = 57;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 341);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 56;
            this.label5.Text = "平台服务器";
            // 
            // tbJMSServer
            // 
            this.tbJMSServer.Location = new System.Drawing.Point(100, 365);
            this.tbJMSServer.Name = "tbJMSServer";
            this.tbJMSServer.Size = new System.Drawing.Size(287, 21);
            this.tbJMSServer.TabIndex = 55;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 54;
            this.label4.Text = "JMS服务器";
            // 
            // cbbFPType
            // 
            this.cbbFPType.FormattingEnabled = true;
            this.cbbFPType.Items.AddRange(new object[] {
            "0-指纹后台比对(需要在本机插指纹仪)",
            "1-指纹设备比对(需要将指纹模板下发)"});
            this.cbbFPType.Location = new System.Drawing.Point(249, 57);
            this.cbbFPType.Name = "cbbFPType";
            this.cbbFPType.Size = new System.Drawing.Size(138, 20);
            this.cbbFPType.TabIndex = 53;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(166, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 52;
            this.label11.Text = "指纹比对方式";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 51;
            this.label10.Text = "权限验证方式";
            // 
            // cbbRightsType
            // 
            this.cbbRightsType.FormattingEnabled = true;
            this.cbbRightsType.Items.AddRange(new object[] {
            "0-刷卡,指纹,人脸独立验证",
            "1-人脸+刷卡双重验证,指纹单独验证"});
            this.cbbRightsType.Location = new System.Drawing.Point(115, 180);
            this.cbbRightsType.Name = "cbbRightsType";
            this.cbbRightsType.Size = new System.Drawing.Size(239, 20);
            this.cbbRightsType.TabIndex = 50;
            // 
            // ckbFingerPrint
            // 
            this.ckbFingerPrint.AutoSize = true;
            this.ckbFingerPrint.BackColor = System.Drawing.Color.Transparent;
            this.ckbFingerPrint.Location = new System.Drawing.Point(34, 59);
            this.ckbFingerPrint.Name = "ckbFingerPrint";
            this.ckbFingerPrint.Size = new System.Drawing.Size(96, 16);
            this.ckbFingerPrint.TabIndex = 49;
            this.ckbFingerPrint.Text = "启用指纹功能";
            this.ckbFingerPrint.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(187, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 47;
            this.label9.Text = "人脸输出";
            // 
            // cbbFaceOutPutType
            // 
            this.cbbFaceOutPutType.FormattingEnabled = true;
            this.cbbFaceOutPutType.Items.AddRange(new object[] {
            "0-韦根信号输出(高字节在前)",
            "1-韦根信号输出(低字节在前)",
            "2-串口信号输出"});
            this.cbbFaceOutPutType.Location = new System.Drawing.Point(246, 153);
            this.cbbFaceOutPutType.Name = "cbbFaceOutPutType";
            this.cbbFaceOutPutType.Size = new System.Drawing.Size(149, 20);
            this.cbbFaceOutPutType.TabIndex = 46;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 45;
            this.label7.Text = "人脸机型号";
            // 
            // cbbFaceMachine
            // 
            this.cbbFaceMachine.FormattingEnabled = true;
            this.cbbFaceMachine.Items.AddRange(new object[] {
            "0-HPT动态人脸识别机",
            "1-YF动态人脸识别机",
            "2-SYD动态人脸识别机",
            "3-AXD动态人脸识别机"});
            this.cbbFaceMachine.Location = new System.Drawing.Point(246, 127);
            this.cbbFaceMachine.Name = "cbbFaceMachine";
            this.cbbFaceMachine.Size = new System.Drawing.Size(149, 20);
            this.cbbFaceMachine.TabIndex = 44;
            // 
            // tbJMSClient
            // 
            this.tbJMSClient.Location = new System.Drawing.Point(100, 473);
            this.tbJMSClient.Name = "tbJMSClient";
            this.tbJMSClient.Size = new System.Drawing.Size(287, 21);
            this.tbJMSClient.TabIndex = 65;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(31, 476);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 64;
            this.label14.Text = "JMS客户端";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 589);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.panel_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numLimitTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLocalPort)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ckbCamModule;
        private System.Windows.Forms.CheckBox ckbLedModule;
        private System.Windows.Forms.NumericUpDown numLocalPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpAutoClear;
        private System.Windows.Forms.CheckBox ckbAutoClear;
        private System.Windows.Forms.NumericUpDown numLimitTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckbLimitTotalEnabled;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private System.Windows.Forms.CheckBox ckbAutoBak;
        private System.Windows.Forms.CheckBox ckbFaceEnabled;
        private System.Windows.Forms.CheckBox ckbSynCardData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbbNetcamType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbFaceMachine;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbFaceOutPutType;
        private System.Windows.Forms.CheckBox ckbFingerPrint;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbbRightsType;
        private System.Windows.Forms.ComboBox cbbFPType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbJMSServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbServerURL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbJMSAccount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbJMSPassword;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbJMSFilter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbJMSClient;
        private System.Windows.Forms.Label label14;
    }
}