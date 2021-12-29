namespace Joey.UserControls
{
    partial class JForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JForm));
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel_top = new System.Windows.Forms.Panel();
            this.label_close = new System.Windows.Forms.Label();
            this.image_title = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.panel_bottom.SuspendLayout();
            this.panel_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_bottom
            // 
            this.panel_bottom.BackColor = System.Drawing.Color.Transparent;
            this.panel_bottom.Controls.Add(this.btnOK);
            this.panel_bottom.Controls.Add(this.btnCancel);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 150);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(378, 42);
            this.panel_bottom.TabIndex = 109;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = global::HPT.Joey.Lib.Properties.Resources.bg_blue1;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(82, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定(S)";
            this.btnOK.TextColor = System.Drawing.SystemColors.ButtonHighlight;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::HPT.Joey.Lib.Properties.Resources.bg_blue1;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(243, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消(C)";
            this.btnCancel.TextColor = System.Drawing.SystemColors.ButtonHighlight;
            // 
            // panel_main
            // 
            this.panel_main.BackColor = System.Drawing.Color.Transparent;
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 30);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(378, 120);
            this.panel_main.TabIndex = 110;
            // 
            // panel_top
            // 
            this.panel_top.BackColor = System.Drawing.Color.Transparent;
            this.panel_top.BackgroundImage = global::HPT.Joey.Lib.Properties.Resources.bg_blue1;
            this.panel_top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_top.Controls.Add(this.label_close);
            this.panel_top.Controls.Add(this.image_title);
            this.panel_top.Controls.Add(this.label_title);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(378, 30);
            this.panel_top.TabIndex = 108;
            this.panel_top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_top_MouseDown);
            this.panel_top.Resize += new System.EventHandler(this.panel_top_Resize);
            // 
            // label_close
            // 
            this.label_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_close.Image = global::HPT.Joey.Lib.Properties.Resources.close_normal;
            this.label_close.Location = new System.Drawing.Point(348, 0);
            this.label_close.Name = "label_close";
            this.label_close.Size = new System.Drawing.Size(30, 30);
            this.label_close.TabIndex = 111;
            this.label_close.Click += new System.EventHandler(this.button_close_Click);
            this.label_close.MouseEnter += new System.EventHandler(this.label_windowstool_MouseEnter);
            this.label_close.MouseLeave += new System.EventHandler(this.label_windowstool_MouseLeave);
            // 
            // image_title
            // 
            this.image_title.BackColor = System.Drawing.Color.Transparent;
            this.image_title.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("image_title.BackgroundImage")));
            this.image_title.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.image_title.Location = new System.Drawing.Point(3, 5);
            this.image_title.Name = "image_title";
            this.image_title.Size = new System.Drawing.Size(20, 20);
            this.image_title.TabIndex = 110;
            this.image_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.image_title_MouseDown);
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_title.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_title.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_title.Location = new System.Drawing.Point(29, 7);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(32, 17);
            this.label_title.TabIndex = 102;
            this.label_title.Text = "提示";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_title_MouseDown);
            // 
            // JForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(378, 192);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel_top);
            this.Name = "JForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.BaseWindow_Load);
            this.SizeChanged += new System.EventHandler(this.BaseWindow_SizeChanged);
            this.TextChanged += new System.EventHandler(this.BaseWindow_TextChanged);
            this.panel_bottom.ResumeLayout(false);
            this.panel_top.ResumeLayout(false);
            this.panel_top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Panel panel_top;
        public System.Windows.Forms.Panel panel_main;
        public System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Panel image_title;
        private System.Windows.Forms.Label label_close;
        public DevComponents.DotNetBar.ButtonX btnOK;
        public DevComponents.DotNetBar.ButtonX btnCancel;
    }
}

