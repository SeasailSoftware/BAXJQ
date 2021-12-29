namespace HPT.Gate.Host.face
{
    partial class FrmNetPara
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNetPara));
            this.tbGateWay = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.tbNetMark = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.tbMac = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.panel_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.label27);
            this.panel_main.Controls.Add(this.tbIPAddress);
            this.panel_main.Controls.Add(this.label28);
            this.panel_main.Controls.Add(this.tbMac);
            this.panel_main.Controls.Add(this.label29);
            this.panel_main.Controls.Add(this.tbNetMark);
            this.panel_main.Controls.Add(this.label30);
            this.panel_main.Controls.Add(this.tbGateWay);
            this.panel_main.Controls.Add(this.buttonX2);
            this.panel_main.Controls.Add(this.buttonX3);
            this.panel_main.Size = new System.Drawing.Size(389, 186);
            // 
            // tbGateWay
            // 
            this.tbGateWay.Location = new System.Drawing.Point(125, 93);
            this.tbGateWay.Name = "tbGateWay";
            this.tbGateWay.Size = new System.Drawing.Size(192, 21);
            this.tbGateWay.TabIndex = 15;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(54, 96);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(65, 12);
            this.label30.TabIndex = 14;
            this.label30.Text = "默认网关：";
            // 
            // tbNetMark
            // 
            this.tbNetMark.Location = new System.Drawing.Point(125, 66);
            this.tbNetMark.Name = "tbNetMark";
            this.tbNetMark.Size = new System.Drawing.Size(192, 21);
            this.tbNetMark.TabIndex = 13;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(54, 69);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 12);
            this.label29.TabIndex = 12;
            this.label29.Text = "子网掩码：";
            // 
            // tbMac
            // 
            this.tbMac.Location = new System.Drawing.Point(125, 39);
            this.tbMac.Name = "tbMac";
            this.tbMac.ReadOnly = true;
            this.tbMac.Size = new System.Drawing.Size(192, 21);
            this.tbMac.TabIndex = 11;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(84, 42);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(35, 12);
            this.label28.TabIndex = 10;
            this.label28.Text = "MAC：";
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(125, 12);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(192, 21);
            this.tbIPAddress.TabIndex = 9;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(60, 15);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(59, 12);
            this.label27.TabIndex = 8;
            this.label27.Text = "IP 地址：";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX3.Location = new System.Drawing.Point(242, 144);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(75, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 31;
            this.buttonX3.Text = "取消(C)";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX2.Location = new System.Drawing.Point(86, 144);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 30;
            this.buttonX2.Text = "确定(S)";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // FrmNetPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 216);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNetPara";
            this.Text = "网络参数";
            this.Load += new System.EventHandler(this.FrmNetPara_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbGateWay;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tbNetMark;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tbMac;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Label label27;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX2;
    }
}