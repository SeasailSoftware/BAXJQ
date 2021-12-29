namespace HPT.Gate.Client.Personal.emp
{
    partial class FrmRecords
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecords));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_count = new System.Windows.Forms.Panel();
            this.label_count = new System.Windows.Forms.Label();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.btExPort = new DevComponents.DotNetBar.ButtonX();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btFind = new DevComponents.DotNetBar.ButtonX();
            this.tbEmpName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbEmpCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbDept = new System.Windows.Forms.CheckBox();
            this.cbbDept = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_main.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_count.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.dgvRecords);
            this.panel_main.Controls.Add(this.panel_count);
            this.panel_main.Controls.Add(this.panel1);
            this.panel_main.Size = new System.Drawing.Size(871, 508);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btExPort);
            this.panel1.Controls.Add(this.dtpEnd);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpBegin);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btFind);
            this.panel1.Controls.Add(this.tbEmpName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbEmpCode);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.ckbDept);
            this.panel1.Controls.Add(this.cbbDept);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 78);
            this.panel1.TabIndex = 0;
            // 
            // panel_count
            // 
            this.panel_count.Controls.Add(this.label_count);
            this.panel_count.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_count.Location = new System.Drawing.Point(0, 480);
            this.panel_count.Name = "panel_count";
            this.panel_count.Size = new System.Drawing.Size(871, 28);
            this.panel_count.TabIndex = 1;
            // 
            // label_count
            // 
            this.label_count.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_count.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_count.Location = new System.Drawing.Point(0, 0);
            this.label_count.Name = "label_count";
            this.label_count.Size = new System.Drawing.Size(871, 28);
            this.label_count.TabIndex = 0;
            this.label_count.Text = "共查询到0条记录!";
            this.label_count.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecords.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvRecords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.dgvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecords.Location = new System.Drawing.Point(0, 78);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.RowTemplate.Height = 23;
            this.dgvRecords.Size = new System.Drawing.Size(871, 402);
            this.dgvRecords.TabIndex = 2;
            // 
            // btExPort
            // 
            this.btExPort.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btExPort.BackColor = System.Drawing.Color.Transparent;
            this.btExPort.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.btExPort.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btExPort.Image = ((System.Drawing.Image)(resources.GetObject("btExPort.Image")));
            this.btExPort.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExPort.Location = new System.Drawing.Point(655, 6);
            this.btExPort.Name = "btExPort";
            this.btExPort.Size = new System.Drawing.Size(77, 61);
            this.btExPort.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btExPort.TabIndex = 44;
            this.btExPort.Text = "导出到Excel";
            this.btExPort.Click += new System.EventHandler(this.btExPort_Click);
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(435, 44);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(105, 21);
            this.dtpEnd.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(376, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "结束日期";
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(435, 15);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(105, 21);
            this.dtpBegin.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(376, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "开始日期";
            // 
            // btFind
            // 
            this.btFind.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btFind.BackColor = System.Drawing.Color.Transparent;
            this.btFind.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.btFind.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btFind.Image = ((System.Drawing.Image)(resources.GetObject("btFind.Image")));
            this.btFind.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btFind.Location = new System.Drawing.Point(571, 6);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(63, 61);
            this.btFind.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btFind.TabIndex = 39;
            this.btFind.Text = "查找";
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // tbEmpName
            // 
            this.tbEmpName.Location = new System.Drawing.Point(259, 45);
            this.tbEmpName.Name = "tbEmpName";
            this.tbEmpName.Size = new System.Drawing.Size(97, 21);
            this.tbEmpName.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(200, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 37;
            this.label5.Text = "人员姓名";
            // 
            // tbEmpCode
            // 
            this.tbEmpCode.Location = new System.Drawing.Point(259, 18);
            this.tbEmpCode.Name = "tbEmpCode";
            this.tbEmpCode.Size = new System.Drawing.Size(97, 21);
            this.tbEmpCode.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(200, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "人员编号";
            // 
            // ckbDept
            // 
            this.ckbDept.AutoSize = true;
            this.ckbDept.BackColor = System.Drawing.Color.Transparent;
            this.ckbDept.Location = new System.Drawing.Point(80, 44);
            this.ckbDept.Name = "ckbDept";
            this.ckbDept.Size = new System.Drawing.Size(84, 16);
            this.ckbDept.TabIndex = 34;
            this.ckbDept.Text = "包括子部门";
            this.ckbDept.UseVisualStyleBackColor = false;
            // 
            // cbbDept
            // 
            this.cbbDept.FormattingEnabled = true;
            this.cbbDept.Location = new System.Drawing.Point(80, 18);
            this.cbbDept.Name = "cbbDept";
            this.cbbDept.Size = new System.Drawing.Size(97, 20);
            this.cbbDept.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(21, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "所属部门";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "部门名称";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "人员编号";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "人员姓名";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "卡号";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "刷卡类型";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "设备名称";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "刷卡时间";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "出入口";
            this.Column8.Name = "Column8";
            // 
            // FrmRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 538);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRecords";
            this.Text = "出入记录查询";
            this.Load += new System.EventHandler(this.FrmRecords_Load);
            this.panel_main.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_count.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_count;
        private System.Windows.Forms.Label label_count;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvRecords;
        private DevComponents.DotNetBar.ButtonX btExPort;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX btFind;
        private System.Windows.Forms.TextBox tbEmpName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbEmpCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckbDept;
        private System.Windows.Forms.ComboBox cbbDept;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}