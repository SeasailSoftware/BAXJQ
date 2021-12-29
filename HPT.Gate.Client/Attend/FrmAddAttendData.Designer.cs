namespace HPT.Gate.Client.Attend
{
    partial class FrmAddAttendData
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpRecDate = new System.Windows.Forms.DateTimePicker();
            this.dtpRecTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "记录日期";
            // 
            // dtpRecDate
            // 
            this.dtpRecDate.CustomFormat = "yyyy-MM-dd";
            this.dtpRecDate.Enabled = false;
            this.dtpRecDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRecDate.Location = new System.Drawing.Point(127, 15);
            this.dtpRecDate.Name = "dtpRecDate";
            this.dtpRecDate.Size = new System.Drawing.Size(135, 21);
            this.dtpRecDate.TabIndex = 1;
            // 
            // dtpRecTime
            // 
            this.dtpRecTime.CustomFormat = "HH:mm";
            this.dtpRecTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRecTime.Location = new System.Drawing.Point(127, 42);
            this.dtpRecTime.Name = "dtpRecTime";
            this.dtpRecTime.ShowUpDown = true;
            this.dtpRecTime.Size = new System.Drawing.Size(64, 21);
            this.dtpRecTime.TabIndex = 3;
            this.dtpRecTime.Value = new System.DateTime(2018, 2, 4, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "记录时间";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX1.Location = new System.Drawing.Point(70, 87);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 4;
            this.buttonX1.Text = "保存(S)";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX2.Location = new System.Drawing.Point(187, 87);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 5;
            this.buttonX2.Text = "取消(C)";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // FrmAddAttendData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 122);
            this.ControlBox = false;
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.dtpRecTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpRecDate);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "FrmAddAttendData";
            this.Text = "添加考勤记录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpRecDate;
        private System.Windows.Forms.DateTimePicker dtpRecTime;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
    }
}