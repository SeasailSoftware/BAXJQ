namespace HPT.Gate.Client.oper
{
    partial class FrmOperAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperAdd));
            this.label4 = new System.Windows.Forms.Label();
            this.tbRemark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOperName = new System.Windows.Forms.TextBox();
            this.tbOperPass = new System.Windows.Forms.TextBox();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.tbOperName);
            this.panel_main.Controls.Add(this.tbOperPass);
            this.panel_main.Controls.Add(this.tbRemark);
            this.panel_main.Controls.Add(this.label1);
            this.panel_main.Controls.Add(this.label2);
            this.panel_main.Controls.Add(this.label4);
            this.panel_main.Size = new System.Drawing.Size(362, 155);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 185);
            this.panel_bottom.Size = new System.Drawing.Size(362, 42);
            // 
            // btCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(217, 10);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(70, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "备注";
            // 
            // tbRemark
            // 
            this.tbRemark.Location = new System.Drawing.Point(113, 90);
            this.tbRemark.MaxLength = 30;
            this.tbRemark.Multiline = true;
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Size = new System.Drawing.Size(176, 47);
            this.tbRemark.TabIndex = 15;
            this.tbRemark.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "用户名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "用户密码";
            // 
            // tbOperName
            // 
            this.tbOperName.Location = new System.Drawing.Point(113, 20);
            this.tbOperName.MaxLength = 15;
            this.tbOperName.Name = "tbOperName";
            this.tbOperName.Size = new System.Drawing.Size(176, 21);
            this.tbOperName.TabIndex = 11;
            // 
            // tbOperPass
            // 
            this.tbOperPass.Location = new System.Drawing.Point(113, 53);
            this.tbOperPass.MaxLength = 10;
            this.tbOperPass.Name = "tbOperPass";
            this.tbOperPass.Size = new System.Drawing.Size(176, 21);
            this.tbOperPass.TabIndex = 12;
            this.tbOperPass.UseSystemPasswordChar = true;
            // 
            // FrmOperAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(362, 227);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOperAdd";
            this.Text = "添加用户";
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOperName;
        private System.Windows.Forms.TextBox tbOperPass;
    }
}