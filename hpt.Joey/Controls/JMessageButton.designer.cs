namespace Joey.UserControls
{
    partial class JMessageButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JMessageButton));
            this.panel_Image = new System.Windows.Forms.Panel();
            this.button_yes = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.label_msg = new System.Windows.Forms.Label();
            this.panel_top = new System.Windows.Forms.Panel();
            this.label_close = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.panel_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Image
            // 
            this.panel_Image.BackColor = System.Drawing.Color.Transparent;
            this.panel_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Image.Location = new System.Drawing.Point(16, 67);
            this.panel_Image.Name = "panel_Image";
            this.panel_Image.Size = new System.Drawing.Size(90, 90);
            this.panel_Image.TabIndex = 115;
            // 
            // button_yes
            // 
            this.button_yes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_yes.BackgroundImage")));
            this.button_yes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_yes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_yes.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_yes.FlatAppearance.BorderSize = 0;
            this.button_yes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(174)))), ((int)(((byte)(185)))));
            this.button_yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_yes.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_yes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_yes.Location = new System.Drawing.Point(136, 169);
            this.button_yes.Name = "button_yes";
            this.button_yes.Size = new System.Drawing.Size(82, 31);
            this.button_yes.TabIndex = 114;
            this.button_yes.Text = "确 定(S)";
            this.button_yes.UseVisualStyleBackColor = false;
            this.button_yes.Visible = false;
            // 
            // button_cancel
            // 
            this.button_cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_cancel.BackgroundImage")));
            this.button_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(174)))), ((int)(((byte)(185)))));
            this.button_cancel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_cancel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_cancel.Location = new System.Drawing.Point(267, 169);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(82, 31);
            this.button_cancel.TabIndex = 113;
            this.button_cancel.Text = "取 消(C)";
            this.button_cancel.UseVisualStyleBackColor = false;
            // 
            // button_ok
            // 
            this.button_ok.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_ok.BackgroundImage")));
            this.button_ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.FlatAppearance.BorderSize = 0;
            this.button_ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(174)))), ((int)(((byte)(185)))));
            this.button_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ok.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ok.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_ok.Location = new System.Drawing.Point(301, 169);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(82, 31);
            this.button_ok.TabIndex = 112;
            this.button_ok.Text = "确 定(S)";
            this.button_ok.UseVisualStyleBackColor = false;
            // 
            // label_msg
            // 
            this.label_msg.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_msg.Location = new System.Drawing.Point(112, 67);
            this.label_msg.Name = "label_msg";
            this.label_msg.Size = new System.Drawing.Size(271, 90);
            this.label_msg.TabIndex = 111;
            this.label_msg.Text = "消息的内容在这里显示";
            this.label_msg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_top
            // 
            this.panel_top.BackgroundImage = global::HPT.Joey.Lib.Properties.Resources.bg_blue1;
            this.panel_top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_top.Controls.Add(this.label_close);
            this.panel_top.Controls.Add(this.label_title);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(395, 33);
            this.panel_top.TabIndex = 109;
            this.panel_top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_top_MouseDown);
            // 
            // label_close
            // 
            this.label_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_close.BackColor = System.Drawing.Color.Transparent;
            this.label_close.Image = global::HPT.Joey.Lib.Properties.Resources.close_normal;
            this.label_close.Location = new System.Drawing.Point(363, 2);
            this.label_close.Name = "label_close";
            this.label_close.Size = new System.Drawing.Size(29, 26);
            this.label_close.TabIndex = 103;
            this.label_close.Click += new System.EventHandler(this.label_close_Click);
            this.label_close.MouseEnter += new System.EventHandler(this.label_windowstool_MouseEnter);
            this.label_close.MouseLeave += new System.EventHandler(this.label_windowstool_MouseLeave);
            // 
            // label_title
            // 
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_title.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_title.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_title.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_title.Location = new System.Drawing.Point(0, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(172, 33);
            this.label_title.TabIndex = 102;
            this.label_title.Text = "提示";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // JMessageButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(395, 212);
            this.Controls.Add(this.panel_Image);
            this.Controls.Add(this.button_yes);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label_msg);
            this.Controls.Add(this.panel_top);
            this.Name = "JMessageButton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JMessageButton";
            this.Load += new System.EventHandler(this.JMessageButton_Load);
            this.panel_top.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Label label_close;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_msg;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_yes;
        private System.Windows.Forms.Panel panel_Image;
    }
}