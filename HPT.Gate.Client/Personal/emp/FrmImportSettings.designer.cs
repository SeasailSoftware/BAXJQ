namespace hpt.gate.dataImport
{
    partial class FrmImportSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportSettings));
            this.label5 = new System.Windows.Forms.Label();
            this.cbbTicketType = new System.Windows.Forms.ComboBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.cbbDept = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbbEmp = new System.Windows.Forms.ComboBox();
            this.cbbCardType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbbCardNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.label5);
            this.panel_main.Controls.Add(this.cbbTicketType);
            this.panel_main.Controls.Add(this.cbbDept);
            this.panel_main.Controls.Add(this.dtpEnd);
            this.panel_main.Controls.Add(this.dtpBegin);
            this.panel_main.Controls.Add(this.label3);
            this.panel_main.Controls.Add(this.label4);
            this.panel_main.Controls.Add(this.cbbCardNo);
            this.panel_main.Controls.Add(this.label1);
            this.panel_main.Controls.Add(this.label13);
            this.panel_main.Controls.Add(this.label14);
            this.panel_main.Controls.Add(this.label2);
            this.panel_main.Controls.Add(this.cbbEmp);
            this.panel_main.Controls.Add(this.cbbCardType);
            this.panel_main.Size = new System.Drawing.Size(430, 193);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 223);
            this.panel_bottom.Size = new System.Drawing.Size(430, 42);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(88, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(268, 10);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(71, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 62;
            this.label5.Text = "票类选择";
            // 
            // cbbTicketType
            // 
            this.cbbTicketType.FormattingEnabled = true;
            this.cbbTicketType.Items.AddRange(new object[] {
            "0-10进制数ID/IC卡号",
            "1-16进制4字节ID/IC卡号",
            "2-16位身份证序列号",
            "3-18位身份证号码"});
            this.cbbTicketType.Location = new System.Drawing.Point(130, 124);
            this.cbbTicketType.Name = "cbbTicketType";
            this.cbbTicketType.Size = new System.Drawing.Size(225, 20);
            this.cbbTicketType.TabIndex = 61;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(266, 150);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(89, 21);
            this.dtpEnd.TabIndex = 60;
            this.dtpEnd.Value = new System.DateTime(2099, 1, 1, 0, 0, 0, 0);
            // 
            // cbbDept
            // 
            this.cbbDept.FormattingEnabled = true;
            this.cbbDept.Items.AddRange(new object[] {
            "0-部门名称不存在不做导入处理",
            "1-部门名称不存在导入到最上级部门"});
            this.cbbDept.Location = new System.Drawing.Point(130, 20);
            this.cbbDept.Name = "cbbDept";
            this.cbbDept.Size = new System.Drawing.Size(225, 20);
            this.cbbDept.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(41, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "IC/ID卡号类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(71, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "部门导入";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(237, 154);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 59;
            this.label14.Text = "至";
            // 
            // cbbEmp
            // 
            this.cbbEmp.FormattingEnabled = true;
            this.cbbEmp.Items.AddRange(new object[] {
            "0-人员编号重复时不作导入",
            "1-人员编号重复时覆盖导入"});
            this.cbbEmp.Location = new System.Drawing.Point(130, 46);
            this.cbbEmp.Name = "cbbEmp";
            this.cbbEmp.Size = new System.Drawing.Size(225, 20);
            this.cbbEmp.TabIndex = 4;
            // 
            // cbbCardType
            // 
            this.cbbCardType.FormattingEnabled = true;
            this.cbbCardType.Items.AddRange(new object[] {
            "0-10进制数ID/IC卡号(长度8位数)",
            "1-10进制数ID/IC卡号(长度10位数)",
            "2-16进制4字节ID/IC卡号(高位在前)",
            "3-16进制4字节ID/IC卡号(低位在前)"});
            this.cbbCardType.Location = new System.Drawing.Point(130, 98);
            this.cbbCardType.Name = "cbbCardType";
            this.cbbCardType.Size = new System.Drawing.Size(225, 20);
            this.cbbCardType.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(71, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "人员导入";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(83, 154);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 58;
            this.label13.Text = "有效期";
            // 
            // cbbCardNo
            // 
            this.cbbCardNo.FormattingEnabled = true;
            this.cbbCardNo.Items.AddRange(new object[] {
            "0-卡号重复时不作导入",
            "1-卡号重复时覆盖导入"});
            this.cbbCardNo.Location = new System.Drawing.Point(130, 72);
            this.cbbCardNo.Name = "cbbCardNo";
            this.cbbCardNo.Size = new System.Drawing.Size(225, 20);
            this.cbbCardNo.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(71, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "卡号导入";
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(130, 150);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(98, 21);
            this.dtpBegin.TabIndex = 57;
            // 
            // FrmImportSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 265);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImportSettings";
            this.ShowControllBox = true;
            this.Text = "导入设置";
            this.Load += new System.EventHandler(this.FrmImportSettings_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbEmp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbDept;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbCardType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbCardNo;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbTicketType;
    }
}