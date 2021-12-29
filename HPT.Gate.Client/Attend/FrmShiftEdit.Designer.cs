namespace HPT.Gate.Client.Attend
{
    partial class FrmShiftEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShiftEdit));
            this.label3 = new System.Windows.Forms.Label();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.tbShiftName = new System.Windows.Forms.TextBox();
            this.cbbTimeGroupOfShift = new System.Windows.Forms.ComboBox();
            this.cbbShiftType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            this.cbbGroupNo = new System.Windows.Forms.ComboBox();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.label3);
            this.panel_main.Controls.Add(this.buttonX4);
            this.panel_main.Controls.Add(this.label1);
            this.panel_main.Controls.Add(this.label2);
            this.panel_main.Controls.Add(this.buttonX3);
            this.panel_main.Controls.Add(this.tbShiftName);
            this.panel_main.Controls.Add(this.cbbTimeGroupOfShift);
            this.panel_main.Controls.Add(this.cbbShiftType);
            this.panel_main.Controls.Add(this.label4);
            this.panel_main.Controls.Add(this.dgvShifts);
            this.panel_main.Controls.Add(this.cbbGroupNo);
            this.panel_main.Size = new System.Drawing.Size(639, 420);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 450);
            this.panel_bottom.Size = new System.Drawing.Size(639, 42);
            // 
            // btCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(426, 10);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(138, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 397);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "快速设置:";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX4.Location = new System.Drawing.Point(534, 391);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(58, 23);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 21;
            this.buttonX4.Text = "清空所有";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "班次名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "周期单位";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX3.Location = new System.Drawing.Point(463, 391);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(58, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 20;
            this.buttonX3.Text = "确定";
            // 
            // tbShiftName
            // 
            this.tbShiftName.Location = new System.Drawing.Point(179, 18);
            this.tbShiftName.Name = "tbShiftName";
            this.tbShiftName.Size = new System.Drawing.Size(115, 21);
            this.tbShiftName.TabIndex = 14;
            // 
            // cbbTimeGroupOfShift
            // 
            this.cbbTimeGroupOfShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTimeGroupOfShift.FormattingEnabled = true;
            this.cbbTimeGroupOfShift.Items.AddRange(new object[] {
            "0-时间段一"});
            this.cbbTimeGroupOfShift.Location = new System.Drawing.Point(355, 393);
            this.cbbTimeGroupOfShift.Name = "cbbTimeGroupOfShift";
            this.cbbTimeGroupOfShift.Size = new System.Drawing.Size(102, 20);
            this.cbbTimeGroupOfShift.TabIndex = 18;
            // 
            // cbbShiftType
            // 
            this.cbbShiftType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbShiftType.FormattingEnabled = true;
            this.cbbShiftType.Items.AddRange(new object[] {
            "0-星期",
            "1-月份"});
            this.cbbShiftType.Location = new System.Drawing.Point(384, 18);
            this.cbbShiftType.Name = "cbbShiftType";
            this.cbbShiftType.Size = new System.Drawing.Size(121, 20);
            this.cbbShiftType.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(308, 396);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "设置为";
            // 
            // dgvShifts
            // 
            this.dgvShifts.AllowUserToAddRows = false;
            this.dgvShifts.AllowUserToDeleteRows = false;
            this.dgvShifts.AllowUserToResizeColumns = false;
            this.dgvShifts.AllowUserToResizeRows = false;
            this.dgvShifts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShifts.BackgroundColor = System.Drawing.Color.White;
            this.dgvShifts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShifts.Location = new System.Drawing.Point(31, 45);
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.RowTemplate.Height = 23;
            this.dgvShifts.Size = new System.Drawing.Size(557, 329);
            this.dgvShifts.TabIndex = 22;
            // 
            // cbbGroupNo
            // 
            this.cbbGroupNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGroupNo.FormattingEnabled = true;
            this.cbbGroupNo.Items.AddRange(new object[] {
            "0-时间段一",
            "1-时间段二",
            "2-时间段三"});
            this.cbbGroupNo.Location = new System.Drawing.Point(209, 393);
            this.cbbGroupNo.Name = "cbbGroupNo";
            this.cbbGroupNo.Size = new System.Drawing.Size(93, 20);
            this.cbbGroupNo.TabIndex = 15;
            // 
            // FrmShiftEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 492);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmShiftEdit";
            this.Text = "修改班次信息";
            this.Load += new System.EventHandler(this.FrmShiftEdit_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private System.Windows.Forms.TextBox tbShiftName;
        private System.Windows.Forms.ComboBox cbbTimeGroupOfShift;
        private System.Windows.Forms.ComboBox cbbShiftType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvShifts;
        private System.Windows.Forms.ComboBox cbbGroupNo;
    }
}