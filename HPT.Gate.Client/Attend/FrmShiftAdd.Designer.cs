namespace HPT.Gate.Client.Attend
{
    partial class FrmShiftAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShiftAdd));
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            this.tbShiftName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbShiftType = new System.Windows.Forms.ComboBox();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.cbbTimeGroupOfShift = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbGroupNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panel_main.Size = new System.Drawing.Size(613, 427);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 457);
            this.panel_bottom.Size = new System.Drawing.Size(613, 42);
            // 
            // btCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(405, 10);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(132, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            this.dgvShifts.Location = new System.Drawing.Point(25, 43);
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.RowTemplate.Height = 23;
            this.dgvShifts.Size = new System.Drawing.Size(557, 329);
            this.dgvShifts.TabIndex = 11;
            this.dgvShifts.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShifts_CellContentDoubleClick);
            this.dgvShifts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShifts_CellDoubleClick);
            // 
            // tbShiftName
            // 
            this.tbShiftName.Location = new System.Drawing.Point(173, 16);
            this.tbShiftName.Name = "tbShiftName";
            this.tbShiftName.Size = new System.Drawing.Size(115, 21);
            this.tbShiftName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "班次名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "周期单位";
            // 
            // cbbShiftType
            // 
            this.cbbShiftType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbShiftType.FormattingEnabled = true;
            this.cbbShiftType.Items.AddRange(new object[] {
            "0-星期",
            "1-月份"});
            this.cbbShiftType.Location = new System.Drawing.Point(378, 16);
            this.cbbShiftType.Name = "cbbShiftType";
            this.cbbShiftType.Size = new System.Drawing.Size(121, 20);
            this.cbbShiftType.TabIndex = 3;
            this.cbbShiftType.SelectedIndexChanged += new System.EventHandler(this.cbbShiftType_SelectedIndexChanged);
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX4.Location = new System.Drawing.Point(528, 389);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(58, 23);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 5;
            this.buttonX4.Text = "清空所有";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX3.Location = new System.Drawing.Point(457, 389);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(58, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 4;
            this.buttonX3.Text = "确定";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // cbbTimeGroupOfShift
            // 
            this.cbbTimeGroupOfShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTimeGroupOfShift.FormattingEnabled = true;
            this.cbbTimeGroupOfShift.Items.AddRange(new object[] {
            "0-时间段一"});
            this.cbbTimeGroupOfShift.Location = new System.Drawing.Point(349, 391);
            this.cbbTimeGroupOfShift.Name = "cbbTimeGroupOfShift";
            this.cbbTimeGroupOfShift.Size = new System.Drawing.Size(102, 20);
            this.cbbTimeGroupOfShift.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(302, 394);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "设置为";
            // 
            // cbbGroupNo
            // 
            this.cbbGroupNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGroupNo.FormattingEnabled = true;
            this.cbbGroupNo.Items.AddRange(new object[] {
            "0-时间段一",
            "1-时间段二",
            "2-时间段三"});
            this.cbbGroupNo.Location = new System.Drawing.Point(203, 391);
            this.cbbGroupNo.Name = "cbbGroupNo";
            this.cbbGroupNo.Size = new System.Drawing.Size(93, 20);
            this.cbbGroupNo.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "快速设置:";
            // 
            // FrmShiftAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 499);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmShiftAdd";
            this.Text = "添加班次";
            this.Load += new System.EventHandler(this.FrmShiftAdd_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbShiftName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbShiftType;
        private System.Windows.Forms.DataGridView dgvShifts;
        private System.Windows.Forms.ComboBox cbbTimeGroupOfShift;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbGroupNo;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private System.Windows.Forms.Label label3;
    }
}