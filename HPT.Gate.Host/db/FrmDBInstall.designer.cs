namespace  hpt.gate
{
    partial class FrmDBInstall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBInstall));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnInstall = new DevComponents.DotNetBar.ButtonX();
            this.rbAccountOfWindows = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btChooseDBPath = new System.Windows.Forms.Button();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbDbPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.rbAccountOfSQL = new System.Windows.Forms.RadioButton();
            this.cbbServer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.panel_main.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.groupBox1);
            this.panel_main.Size = new System.Drawing.Size(378, 361);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.btnInstall);
            this.groupBox1.Controls.Add(this.rbAccountOfWindows);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btChooseDBPath);
            this.groupBox1.Controls.Add(this.tbUserName);
            this.groupBox1.Controls.Add(this.tbDbPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.rbAccountOfSQL);
            this.groupBox1.Controls.Add(this.cbbServer);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 361);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackgroundImage = global::HPT.Gate.Host.Properties.Resources._1529745161_1_;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnCancel.Location = new System.Drawing.Point(249, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "取消(C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnOK.Location = new System.Drawing.Point(153, 317);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 33;
            this.btnOK.Text = "确定(S)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInstall.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInstall.BackgroundImage")));
            this.btnInstall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInstall.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnInstall.Location = new System.Drawing.Point(54, 317);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInstall.TabIndex = 32;
            this.btnInstall.Text = "安装(I)";
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // rbAccountOfWindows
            // 
            this.rbAccountOfWindows.AutoSize = true;
            this.rbAccountOfWindows.Location = new System.Drawing.Point(56, 185);
            this.rbAccountOfWindows.Name = "rbAccountOfWindows";
            this.rbAccountOfWindows.Size = new System.Drawing.Size(197, 16);
            this.rbAccountOfWindows.TabIndex = 13;
            this.rbAccountOfWindows.Text = "使用windows NT集成安全设置(W)";
            this.rbAccountOfWindows.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名:";
            // 
            // btChooseDBPath
            // 
            this.btChooseDBPath.Location = new System.Drawing.Point(301, 123);
            this.btChooseDBPath.Name = "btChooseDBPath";
            this.btChooseDBPath.Size = new System.Drawing.Size(33, 23);
            this.btChooseDBPath.TabIndex = 31;
            this.btChooseDBPath.Text = "...";
            this.btChooseDBPath.UseVisualStyleBackColor = true;
            this.btChooseDBPath.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(119, 236);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(159, 21);
            this.tbUserName.TabIndex = 3;
            this.tbUserName.Text = "sa";
            // 
            // tbDbPath
            // 
            this.tbDbPath.Location = new System.Drawing.Point(56, 125);
            this.tbDbPath.Name = "tbDbPath";
            this.tbDbPath.Size = new System.Drawing.Size(252, 21);
            this.tbDbPath.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密  码:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(35, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 14);
            this.label6.TabIndex = 29;
            this.label6.Text = "2.指定数据库文件存放路径:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(119, 269);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(159, 21);
            this.tbPassword.TabIndex = 5;
            // 
            // rbAccountOfSQL
            // 
            this.rbAccountOfSQL.AutoSize = true;
            this.rbAccountOfSQL.Checked = true;
            this.rbAccountOfSQL.Location = new System.Drawing.Point(56, 207);
            this.rbAccountOfSQL.Name = "rbAccountOfSQL";
            this.rbAccountOfSQL.Size = new System.Drawing.Size(173, 16);
            this.rbAccountOfSQL.TabIndex = 14;
            this.rbAccountOfSQL.TabStop = true;
            this.rbAccountOfSQL.Text = "使用指定的用户名和密码(U)";
            this.rbAccountOfSQL.UseVisualStyleBackColor = true;
            this.rbAccountOfSQL.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // cbbServer
            // 
            this.cbbServer.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbServer.FormattingEnabled = true;
            this.cbbServer.Location = new System.Drawing.Point(56, 74);
            this.cbbServer.Name = "cbbServer";
            this.cbbServer.Size = new System.Drawing.Size(288, 22);
            this.cbbServer.TabIndex = 22;
            this.cbbServer.Text = ".";
            this.cbbServer.DropDown += new System.EventHandler(this.cbbServer_DropDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(35, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "3.输入登录服务器的信息:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(35, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 14);
            this.label4.TabIndex = 18;
            this.label4.Text = "1.输入服务器名称:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(35, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "指定下列设置以连接到SQL server 数据:";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX3.Location = new System.Drawing.Point(32, 7);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(75, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 12;
            this.buttonX3.Text = "安装(I)";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // FrmDBInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 391);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDBInstall";
            this.Text = "通道闸智能管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBInstall_FormClosing);
            this.Load += new System.EventHandler(this.DBInstall_Load);
            this.panel_main.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbAccountOfSQL;
        private System.Windows.Forms.RadioButton rbAccountOfWindows;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbServer;
        private System.Windows.Forms.TextBox tbDbPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btChooseDBPath;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnInstall;
    }
}