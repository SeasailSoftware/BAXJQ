namespace HPT.Gate.Host.face
{
    partial class FrmFaceEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFaceEdit));
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMac = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numDevId = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDevId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.label9);
            this.panel_main.Controls.Add(this.numPort);
            this.panel_main.Controls.Add(this.label8);
            this.panel_main.Controls.Add(this.numDevId);
            this.panel_main.Controls.Add(this.label2);
            this.panel_main.Controls.Add(this.tbIPAddress);
            this.panel_main.Controls.Add(this.buttonX2);
            this.panel_main.Controls.Add(this.buttonX3);
            this.panel_main.Controls.Add(this.label1);
            this.panel_main.Controls.Add(this.tbMac);
            this.panel_main.Controls.Add(this.label3);
            this.panel_main.Controls.Add(this.tbSN);
            this.panel_main.Controls.Add(this.label4);
            this.panel_main.Controls.Add(this.tbPassword);
            this.panel_main.Size = new System.Drawing.Size(379, 243);

            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(128, 152);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(167, 21);
            this.tbPassword.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "密码";
            // 
            // tbSN
            // 
            this.tbSN.Location = new System.Drawing.Point(128, 98);
            this.tbSN.Name = "tbSN";
            this.tbSN.Size = new System.Drawing.Size(167, 21);
            this.tbSN.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 44;
            this.label3.Text = "序列号";
            // 
            // tbMac
            // 
            this.tbMac.Location = new System.Drawing.Point(128, 125);
            this.tbMac.Name = "tbMac";
            this.tbMac.Size = new System.Drawing.Size(167, 21);
            this.tbMac.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "Mac";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX3.Location = new System.Drawing.Point(222, 202);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(75, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 41;
            this.buttonX3.Text = "取消(C)";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX2.Location = new System.Drawing.Point(85, 202);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 40;
            this.buttonX2.Text = "确定(S)";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(128, 44);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(167, 21);
            this.tbIPAddress.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 34;
            this.label2.Text = "IP地址";
            // 
            // numDevId
            // 
            this.numDevId.Location = new System.Drawing.Point(128, 17);
            this.numDevId.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numDevId.Name = "numDevId";
            this.numDevId.ReadOnly = true;
            this.numDevId.Size = new System.Drawing.Size(167, 21);
            this.numDevId.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(81, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 55;
            this.label8.Text = "机器号";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(81, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 57;
            this.label9.Text = "端口号";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(128, 71);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.ReadOnly = true;
            this.numPort.Size = new System.Drawing.Size(167, 21);
            this.numPort.TabIndex = 56;
            this.numPort.Value = new decimal(new int[] {
            8090,
            0,
            0,
            0});
            // 
            // FrmFaceEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 273);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFaceEdit";
            this.Text = "更新设备";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFaceEdit_FormClosed);
            this.Load += new System.EventHandler(this.FrmFaceEdit_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDevId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMac;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numDevId;
    }
}