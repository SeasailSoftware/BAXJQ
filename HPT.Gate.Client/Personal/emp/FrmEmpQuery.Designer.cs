namespace HPT.Gate.Client.emp
{
    partial class FrmEmpQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmpQuery));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbTelephone = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbIdCard = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDuty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cbbEmpStatus = new System.Windows.Forms.ComboBox();
            this.ckbDept = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbbDept = new System.Windows.Forms.ComboBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.tbCardNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbEmpName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbEmpCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.groupBox1);
            this.panel_main.Size = new System.Drawing.Size(366, 267);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 297);
            this.panel_bottom.Size = new System.Drawing.Size(366, 42);
            // 
            // btCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(227, 10);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(64, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbTelephone);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbIdCard);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbDuty);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.cbbEmpStatus);
            this.groupBox1.Controls.Add(this.ckbDept);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cbbDept);
            this.groupBox1.Controls.Add(this.buttonX1);
            this.groupBox1.Controls.Add(this.tbCardNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbEmpName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbEmpCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 267);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // tbTelephone
            // 
            this.tbTelephone.Location = new System.Drawing.Point(117, 176);
            this.tbTelephone.Name = "tbTelephone";
            this.tbTelephone.Size = new System.Drawing.Size(161, 21);
            this.tbTelephone.TabIndex = 74;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 73;
            this.label6.Text = "联系电话";
            // 
            // tbIdCard
            // 
            this.tbIdCard.Location = new System.Drawing.Point(117, 122);
            this.tbIdCard.Name = "tbIdCard";
            this.tbIdCard.Size = new System.Drawing.Size(161, 21);
            this.tbIdCard.TabIndex = 72;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 71;
            this.label5.Text = "身份证号码";
            // 
            // tbDuty
            // 
            this.tbDuty.Location = new System.Drawing.Point(117, 149);
            this.tbDuty.Name = "tbDuty";
            this.tbDuty.Size = new System.Drawing.Size(161, 21);
            this.tbDuty.TabIndex = 70;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 69;
            this.label4.Text = "职务";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(58, 233);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 68;
            this.label19.Text = "人员状态";
            // 
            // cbbEmpStatus
            // 
            this.cbbEmpStatus.FormattingEnabled = true;
            this.cbbEmpStatus.Items.AddRange(new object[] {
            "0-在职",
            "1-离职",
            "2-全部"});
            this.cbbEmpStatus.Location = new System.Drawing.Point(117, 230);
            this.cbbEmpStatus.Name = "cbbEmpStatus";
            this.cbbEmpStatus.Size = new System.Drawing.Size(161, 20);
            this.cbbEmpStatus.TabIndex = 67;
            // 
            // ckbDept
            // 
            this.ckbDept.AutoSize = true;
            this.ckbDept.Location = new System.Drawing.Point(117, 46);
            this.ckbDept.Name = "ckbDept";
            this.ckbDept.Size = new System.Drawing.Size(84, 16);
            this.ckbDept.TabIndex = 62;
            this.ckbDept.Text = "包括子部门";
            this.ckbDept.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(58, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 61;
            this.label14.Text = "所属部门";
            // 
            // cbbDept
            // 
            this.cbbDept.FormattingEnabled = true;
            this.cbbDept.Location = new System.Drawing.Point(117, 20);
            this.cbbDept.Name = "cbbDept";
            this.cbbDept.Size = new System.Drawing.Size(161, 20);
            this.cbbDept.TabIndex = 60;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonX1.Location = new System.Drawing.Point(284, 204);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(55, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 6;
            this.buttonX1.Text = "读卡";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // tbCardNo
            // 
            this.tbCardNo.Enabled = false;
            this.tbCardNo.Location = new System.Drawing.Point(117, 203);
            this.tbCardNo.Name = "tbCardNo";
            this.tbCardNo.Size = new System.Drawing.Size(161, 21);
            this.tbCardNo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "卡号";
            // 
            // tbEmpName
            // 
            this.tbEmpName.Location = new System.Drawing.Point(117, 95);
            this.tbEmpName.Name = "tbEmpName";
            this.tbEmpName.Size = new System.Drawing.Size(161, 21);
            this.tbEmpName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "人员姓名";
            // 
            // tbEmpCode
            // 
            this.tbEmpCode.Location = new System.Drawing.Point(117, 68);
            this.tbEmpCode.Name = "tbEmpCode";
            this.tbEmpCode.Size = new System.Drawing.Size(161, 21);
            this.tbEmpCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "人员编号";
            // 
            // FrmEmpQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 339);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEmpQuery";
            this.Text = "查找员工信息";
            this.Load += new System.EventHandler(this.FrmEmpQuery_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_bottom.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.TextBox tbCardNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbEmpName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbEmpCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbDept;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbbDept;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbbEmpStatus;
        private System.Windows.Forms.TextBox tbDuty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbIdCard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTelephone;
        private System.Windows.Forms.Label label6;
    }
}