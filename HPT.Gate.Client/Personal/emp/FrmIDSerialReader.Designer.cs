namespace HPT.Gate.Client.emp
{
    partial class FrmIDSerialReader
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
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.gbIDSerial = new System.Windows.Forms.GroupBox();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbIDSerialPort = new System.Windows.Forms.ComboBox();
            this.gbIDSerial.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX3.Location = new System.Drawing.Point(88, 109);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(75, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 18;
            this.buttonX3.Text = "确定";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX4.Location = new System.Drawing.Point(238, 109);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(75, 23);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 19;
            this.buttonX4.Text = "取消";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // gbIDSerial
            // 
            this.gbIDSerial.BackColor = System.Drawing.Color.Transparent;
            this.gbIDSerial.Controls.Add(this.buttonX2);
            this.gbIDSerial.Controls.Add(this.label2);
            this.gbIDSerial.Controls.Add(this.cbbIDSerialPort);
            this.gbIDSerial.Location = new System.Drawing.Point(29, 21);
            this.gbIDSerial.Name = "gbIDSerial";
            this.gbIDSerial.Size = new System.Drawing.Size(345, 65);
            this.gbIDSerial.TabIndex = 16;
            this.gbIDSerial.TabStop = false;
            this.gbIDSerial.Text = "身份证序列号发卡器";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX2.Location = new System.Drawing.Point(235, 24);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(48, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 10;
            this.buttonX2.Text = "刷新";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "串口";
            // 
            // cbbIDSerialPort
            // 
            this.cbbIDSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbIDSerialPort.FormattingEnabled = true;
            this.cbbIDSerialPort.Location = new System.Drawing.Point(54, 27);
            this.cbbIDSerialPort.Name = "cbbIDSerialPort";
            this.cbbIDSerialPort.Size = new System.Drawing.Size(147, 20);
            this.cbbIDSerialPort.TabIndex = 6;
            // 
            // FrmIDSerialReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 149);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.buttonX4);
            this.Controls.Add(this.gbIDSerial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmIDSerialReader";
            this.Text = "身份证序列号发卡器设置";
            this.Load += new System.EventHandler(this.FrmIDSerialReader_Load);
            this.gbIDSerial.ResumeLayout(false);
            this.gbIDSerial.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbIDSerial;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbIDSerialPort;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX4;
    }
}