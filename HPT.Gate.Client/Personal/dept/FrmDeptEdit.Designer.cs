namespace HPT.Gate.Client.dept
{
    partial class FrmDeptEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDeptEdit));
            this.cbbParDept = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDeptName = new System.Windows.Forms.TextBox();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.tbDeptName);
            this.panel_main.Controls.Add(this.label1);
            this.panel_main.Controls.Add(this.label2);
            this.panel_main.Controls.Add(this.cbbParDept);
            this.panel_main.Size = new System.Drawing.Size(357, 99);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 132);
            this.panel_bottom.Size = new System.Drawing.Size(357, 42);
            // 
            // btCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(211, 10);
            this.btnCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(71, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbbParDept
            // 
            this.cbbParDept.FormattingEnabled = true;
            this.cbbParDept.Location = new System.Drawing.Point(114, 26);
            this.cbbParDept.Name = "cbbParDept";
            this.cbbParDept.Size = new System.Drawing.Size(188, 20);
            this.cbbParDept.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "所属部门";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "部门名称";
            // 
            // tbDeptName
            // 
            this.tbDeptName.Location = new System.Drawing.Point(114, 52);
            this.tbDeptName.MaxLength = 15;
            this.tbDeptName.Name = "tbDeptName";
            this.tbDeptName.Size = new System.Drawing.Size(188, 21);
            this.tbDeptName.TabIndex = 19;
            // 
            // FrmDeptEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(357, 174);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDeptEdit";
            this.Text = "部门信息编辑";
            this.Load += new System.EventHandler(this.FrmDeptEdit_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbbParDept;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDeptName;
    }
}