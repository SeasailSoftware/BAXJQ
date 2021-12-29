namespace HPT.Gate.Host.DevPara
{
    partial class FrmSoftKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSoftKey));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ckbFingerPrint = new System.Windows.Forms.CheckBox();
            this.ckbCamera = new System.Windows.Forms.CheckBox();
            this.ckbLed = new System.Windows.Forms.CheckBox();
            this.ckbLcd = new System.Windows.Forms.CheckBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCustName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.panel_main.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.label3);
            this.panel_main.Controls.Add(this.tbCustName);
            this.panel_main.Controls.Add(this.label4);
            this.panel_main.Controls.Add(this.label20);
            this.panel_main.Controls.Add(this.dtpBegin);
            this.panel_main.Controls.Add(this.dtpEnd);
            this.panel_main.Controls.Add(this.groupBox2);
            this.panel_main.Controls.Add(this.buttonX1);
            this.panel_main.Size = new System.Drawing.Size(361, 264);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckbFingerPrint);
            this.groupBox2.Controls.Add(this.ckbCamera);
            this.groupBox2.Controls.Add(this.ckbLed);
            this.groupBox2.Controls.Add(this.ckbLcd);
            this.groupBox2.Location = new System.Drawing.Point(58, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 112);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "软件功能选择";
            // 
            // ckbFingerPrint
            // 
            this.ckbFingerPrint.AutoSize = true;
            this.ckbFingerPrint.Enabled = false;
            this.ckbFingerPrint.Location = new System.Drawing.Point(31, 20);
            this.ckbFingerPrint.Name = "ckbFingerPrint";
            this.ckbFingerPrint.Size = new System.Drawing.Size(126, 16);
            this.ckbFingerPrint.TabIndex = 29;
            this.ckbFingerPrint.Text = "启动指纹+人脸功能";
            this.ckbFingerPrint.UseVisualStyleBackColor = true;
            // 
            // ckbCamera
            // 
            this.ckbCamera.AutoSize = true;
            this.ckbCamera.Enabled = false;
            this.ckbCamera.Location = new System.Drawing.Point(31, 86);
            this.ckbCamera.Name = "ckbCamera";
            this.ckbCamera.Size = new System.Drawing.Size(132, 16);
            this.ckbCamera.TabIndex = 32;
            this.ckbCamera.Text = "启用摄像头抓拍功能";
            this.ckbCamera.UseVisualStyleBackColor = true;
            // 
            // ckbLed
            // 
            this.ckbLed.AutoSize = true;
            this.ckbLed.Enabled = false;
            this.ckbLed.Location = new System.Drawing.Point(31, 42);
            this.ckbLed.Name = "ckbLed";
            this.ckbLed.Size = new System.Drawing.Size(90, 16);
            this.ckbLed.TabIndex = 30;
            this.ckbLed.Text = "启动Led功能";
            this.ckbLed.UseVisualStyleBackColor = true;
            // 
            // ckbLcd
            // 
            this.ckbLcd.AutoSize = true;
            this.ckbLcd.Enabled = false;
            this.ckbLcd.Location = new System.Drawing.Point(31, 64);
            this.ckbLcd.Name = "ckbLcd";
            this.ckbLcd.Size = new System.Drawing.Size(138, 16);
            this.ckbLcd.TabIndex = 31;
            this.ckbLcd.Text = "启用Lcd液晶显示功能";
            this.ckbLcd.UseVisualStyleBackColor = true;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd";
            this.dtpEnd.Enabled = false;
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(115, 66);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(108, 21);
            this.dtpEnd.TabIndex = 40;
            this.dtpEnd.Value = new System.DateTime(2099, 1, 1, 0, 0, 0, 0);
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd";
            this.dtpBegin.Enabled = false;
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(115, 39);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(108, 21);
            this.dtpBegin.TabIndex = 39;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(56, 72);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 38;
            this.label20.Text = "结束日期";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 37;
            this.label4.Text = "开始日期";
            // 
            // tbCustName
            // 
            this.tbCustName.Enabled = false;
            this.tbCustName.Location = new System.Drawing.Point(115, 12);
            this.tbCustName.Name = "tbCustName";
            this.tbCustName.Size = new System.Drawing.Size(194, 21);
            this.tbCustName.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "客户名称";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX1.Location = new System.Drawing.Point(274, 229);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 42;
            this.buttonX1.Text = "关闭(S)";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // FrmSoftKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(361, 294);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSoftKey";
            this.Text = "加密锁信息";
            this.Load += new System.EventHandler(this.FrmSoftKey_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckbFingerPrint;
        private System.Windows.Forms.CheckBox ckbCamera;
        private System.Windows.Forms.CheckBox ckbLed;
        private System.Windows.Forms.CheckBox ckbLcd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCustName;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}