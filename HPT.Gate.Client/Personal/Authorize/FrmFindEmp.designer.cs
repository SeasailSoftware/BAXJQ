namespace HPT.Gate.Client.Authorize
{
    partial class FrmFindEmp
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
            this.cbbDept = new System.Windows.Forms.ComboBox();
            this.ckbDept = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbEmpCode = new System.Windows.Forms.TextBox();
            this.tbEmpName = new System.Windows.Forms.TextBox();
            this.btSubmit = new DevComponents.DotNetBar.ButtonX();
            this.btCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "部门名称:";
            // 
            // cbbDept
            // 
            this.cbbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDept.FormattingEnabled = true;
            this.cbbDept.Location = new System.Drawing.Point(141, 19);
            this.cbbDept.Name = "cbbDept";
            this.cbbDept.Size = new System.Drawing.Size(181, 20);
            this.cbbDept.TabIndex = 1;
            // 
            // ckbDept
            // 
            this.ckbDept.AutoSize = true;
            this.ckbDept.Checked = true;
            this.ckbDept.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbDept.Location = new System.Drawing.Point(141, 45);
            this.ckbDept.Name = "ckbDept";
            this.ckbDept.Size = new System.Drawing.Size(84, 16);
            this.ckbDept.TabIndex = 2;
            this.ckbDept.Text = "包括子部门";
            this.ckbDept.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "人员编号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "人员姓名:";
            // 
            // tbEmpCode
            // 
            this.tbEmpCode.Location = new System.Drawing.Point(141, 67);
            this.tbEmpCode.Name = "tbEmpCode";
            this.tbEmpCode.Size = new System.Drawing.Size(181, 21);
            this.tbEmpCode.TabIndex = 7;
            // 
            // tbEmpName
            // 
            this.tbEmpName.Location = new System.Drawing.Point(141, 94);
            this.tbEmpName.Name = "tbEmpName";
            this.tbEmpName.Size = new System.Drawing.Size(181, 21);
            this.tbEmpName.TabIndex = 8;
            // 
            // btSubmit
            // 
            this.btSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.btSubmit.Location = new System.Drawing.Point(106, 137);
            this.btSubmit.Name = "btSubmit";
            this.btSubmit.Size = new System.Drawing.Size(75, 23);
            this.btSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btSubmit.TabIndex = 9;
            this.btSubmit.Text = "确定";
            this.btSubmit.Click += new System.EventHandler(this.btSubmit_Click);
            // 
            // btCancel
            // 
            this.btCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.btCancel.Location = new System.Drawing.Point(255, 137);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btCancel.TabIndex = 10;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FrmFindEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 176);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSubmit);
            this.Controls.Add(this.tbEmpName);
            this.Controls.Add(this.tbEmpCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ckbDept);
            this.Controls.Add(this.cbbDept);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFindEmp";
            this.Text = "查找人员";
            this.Load += new System.EventHandler(this.FrmFindEmp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbDept;
        private System.Windows.Forms.CheckBox ckbDept;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbEmpCode;
        private System.Windows.Forms.TextBox tbEmpName;
        private DevComponents.DotNetBar.ButtonX btSubmit;
        private DevComponents.DotNetBar.ButtonX btCancel;
    }
}