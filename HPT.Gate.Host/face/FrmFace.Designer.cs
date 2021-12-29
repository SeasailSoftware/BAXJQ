namespace HPT.Gate.Host.face
{
    partial class FrmFace
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFace));
            this.ckbAll = new System.Windows.Forms.CheckBox();
            this.dgvFaceDevice = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar4 = new DevComponents.DotNetBar.Bar();
            this.buttonItem25 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem26 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem27 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaceDevice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.ckbAll);
            this.panel_main.Controls.Add(this.txtLog);
            this.panel_main.Controls.Add(this.dgvFaceDevice);
            this.panel_main.Controls.Add(this.bar4);
            this.panel_main.Size = new System.Drawing.Size(743, 497);
            // 
            // ckbAll
            // 
            this.ckbAll.AutoSize = true;
            this.ckbAll.Location = new System.Drawing.Point(62, 81);
            this.ckbAll.Name = "ckbAll";
            this.ckbAll.Size = new System.Drawing.Size(15, 14);
            this.ckbAll.TabIndex = 6;
            this.ckbAll.UseVisualStyleBackColor = true;
            this.ckbAll.CheckedChanged += new System.EventHandler(this.ckbAll_CheckedChanged);
            // 
            // dgvFaceDevice
            // 
            this.dgvFaceDevice.AllowUserToAddRows = false;
            this.dgvFaceDevice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFaceDevice.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvFaceDevice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFaceDevice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFaceDevice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column6,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFaceDevice.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFaceDevice.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvFaceDevice.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvFaceDevice.Location = new System.Drawing.Point(0, 78);
            this.dgvFaceDevice.MultiSelect = false;
            this.dgvFaceDevice.Name = "dgvFaceDevice";
            this.dgvFaceDevice.RowTemplate.Height = 23;
            this.dgvFaceDevice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFaceDevice.Size = new System.Drawing.Size(743, 251);
            this.dgvFaceDevice.TabIndex = 5;
            // 
            // Column7
            // 
            this.Column7.FillWeight = 50F;
            this.Column7.HeaderText = "";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "DevId";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "IP地址";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "端口号";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "序列号";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Mac";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "密码";
            this.Column5.Name = "Column5";
            // 
            // bar4
            // 
            this.bar4.BackColor = System.Drawing.Color.Transparent;
            this.bar4.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar4.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar4.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem25,
            this.buttonItem26,
            this.buttonItem27,
            this.buttonItem3,
            this.buttonItem1,
            this.buttonItem2,
            this.buttonItem4,
            this.buttonItem6});
            this.bar4.ItemSpacing = 10;
            this.bar4.Location = new System.Drawing.Point(0, 0);
            this.bar4.Name = "bar4";
            this.bar4.Size = new System.Drawing.Size(743, 78);
            this.bar4.Stretch = true;
            this.bar4.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.bar4.TabIndex = 4;
            this.bar4.TabStop = false;
            this.bar4.Text = "bar4";
            // 
            // buttonItem25
            // 
            this.buttonItem25.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem25.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem25.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem25.Image")));
            this.buttonItem25.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem25.Name = "buttonItem25";
            this.buttonItem25.Text = "添加设备";
            this.buttonItem25.Click += new System.EventHandler(this.buttonItem25_Click);
            // 
            // buttonItem26
            // 
            this.buttonItem26.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem26.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem26.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem26.Image")));
            this.buttonItem26.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem26.Name = "buttonItem26";
            this.buttonItem26.Text = "修改设备";
            this.buttonItem26.Click += new System.EventHandler(this.buttonItem26_Click);
            // 
            // buttonItem27
            // 
            this.buttonItem27.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem27.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem27.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem27.Image")));
            this.buttonItem27.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem27.Name = "buttonItem27";
            this.buttonItem27.Text = "删除设备";
            this.buttonItem27.Click += new System.EventHandler(this.buttonItem27_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem3.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem3.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem3.Image")));
            this.buttonItem3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "校时";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
            this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "初始化";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem2.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem2.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem2.Image")));
            this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "添加照片";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem4.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem4.Image = global::HPT.Gate.Host.Properties.Resources.bt_query;
            this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "人脸同步详情";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click_1);
            // 
            // buttonItem6
            // 
            this.buttonItem6.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem6.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem6.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem6.Image")));
            this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem6.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Text = "退 出";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 329);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(743, 168);
            this.txtLog.TabIndex = 7;
            this.txtLog.Text = "";
            // 
            // FrmFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 527);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFace";
            this.Text = "人脸识别设备管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFace_FormClosed);
            this.Load += new System.EventHandler(this.FrmFace_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaceDevice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar4;
        private DevComponents.DotNetBar.ButtonItem buttonItem25;
        private DevComponents.DotNetBar.ButtonItem buttonItem26;
        private DevComponents.DotNetBar.ButtonItem buttonItem27;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvFaceDevice;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private System.Windows.Forms.CheckBox ckbAll;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
    }
}