namespace HPT.Gate.Client
{
    partial class FrmOperEdit
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOperName = new System.Windows.Forms.TextBox();
            this.tbOperPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRemark = new System.Windows.Forms.TextBox();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.tbOperPass);
            this.panel_main.Controls.Add(this.tbOperName);
            this.panel_main.Controls.Add(this.label2);
            this.panel_main.Controls.Add(this.label1);
            this.panel_main.Controls.Add(this.tbRemark);
            this.panel_main.Controls.Add(this.label4);
            this.panel_main.Size = new System.Drawing.Size(365, 156);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 186);
            this.panel_bottom.Size = new System.Drawing.Size(365, 42);
            // 
            // btCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(217, 10);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(73, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户密码";
            // 
            // tbOperName
            // 
            this.tbOperName.Enabled = false;
            this.tbOperName.Location = new System.Drawing.Point(114, 23);
            this.tbOperName.MaxLength = 15;
            this.tbOperName.Name = "tbOperName";
            this.tbOperName.Size = new System.Drawing.Size(178, 21);
            this.tbOperName.TabIndex = 3;
            // 
            // tbOperPass
            // 
            this.tbOperPass.Location = new System.Drawing.Point(114, 50);
            this.tbOperPass.MaxLength = 10;
            this.tbOperPass.Name = "tbOperPass";
            this.tbOperPass.PasswordChar = '*';
            this.tbOperPass.Size = new System.Drawing.Size(178, 21);
            this.tbOperPass.TabIndex = 4;
            this.tbOperPass.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "备注";
            // 
            // tbRemark
            // 
            this.tbRemark.Location = new System.Drawing.Point(114, 86);
            this.tbRemark.MaxLength = 30;
            this.tbRemark.Multiline = true;
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Size = new System.Drawing.Size(178, 47);
            this.tbRemark.TabIndex = 7;
            this.tbRemark.UseSystemPasswordChar = true;
            // 
            // FrmOperEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(365, 228);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOperEdit";
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.FrmOperEdit_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOperName;
        private System.Windows.Forms.TextBox tbOperPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRemark;
    }
}