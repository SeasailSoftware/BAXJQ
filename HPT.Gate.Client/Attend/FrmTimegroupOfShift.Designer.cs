namespace HPT.Gate.Client.Attend
{
    partial class FrmTimegroupOfShift
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTimegroupOfShift));
            this.bar8 = new DevComponents.DotNetBar.Bar();
            this.buttonItem39 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem8 = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvTimeGroup = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.dgvTimeGroup);
            this.panel_main.Controls.Add(this.panel1);
            this.panel_main.Controls.Add(this.bar8);
            this.panel_main.Size = new System.Drawing.Size(852, 420);
            // 
            // bar8
            // 
            this.bar8.BackColor = System.Drawing.Color.Transparent;
            this.bar8.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar8.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar8.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem39,
            this.labelItem8,
            this.buttonItem5,
            this.labelItem1,
            this.buttonItem1,
            this.buttonItem2});
            this.bar8.Location = new System.Drawing.Point(0, 0);
            this.bar8.Name = "bar8";
            this.bar8.Size = new System.Drawing.Size(852, 78);
            this.bar8.Stretch = true;
            this.bar8.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.bar8.TabIndex = 44;
            this.bar8.TabStop = false;
            this.bar8.Text = "bar8";
            // 
            // buttonItem39
            // 
            this.buttonItem39.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem39.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem39.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem39.Image")));
            this.buttonItem39.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem39.Name = "buttonItem39";
            this.buttonItem39.Text = "添  加";
            this.buttonItem39.Click += new System.EventHandler(this.buttonItem39_Click);
            // 
            // labelItem8
            // 
            this.labelItem8.Name = "labelItem8";
            this.labelItem8.Text = "  ";
            // 
            // buttonItem5
            // 
            this.buttonItem5.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem5.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem5.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem5.Image")));
            this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Text = "修  改";
            this.buttonItem5.Click += new System.EventHandler(this.buttonItem5_Click);
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "  ";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
            this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "删  除";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem2.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem2.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem2.Image")));
            this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem2.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "退   出";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumBlue;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 2);
            this.panel1.TabIndex = 46;
            // 
            // dgvTimeGroup
            // 
            this.dgvTimeGroup.AllowUserToAddRows = false;
            this.dgvTimeGroup.AllowUserToDeleteRows = false;
            this.dgvTimeGroup.AllowUserToResizeColumns = false;
            this.dgvTimeGroup.AllowUserToResizeRows = false;
            this.dgvTimeGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTimeGroup.BackgroundColor = System.Drawing.Color.White;
            this.dgvTimeGroup.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTimeGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimeGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dgvTimeGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTimeGroup.Location = new System.Drawing.Point(0, 80);
            this.dgvTimeGroup.Name = "dgvTimeGroup";
            this.dgvTimeGroup.RowTemplate.Height = 23;
            this.dgvTimeGroup.Size = new System.Drawing.Size(852, 340);
            this.dgvTimeGroup.TabIndex = 47;
            this.dgvTimeGroup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTimeGroup_CellClick);
            this.dgvTimeGroup.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTimeGroup_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "GroupId";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "时间段名称";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "上班时间";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "下班时间";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "开始签到时间";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "结束签到时间";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "开始签退时间";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "结束签退时间";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "工作日";
            this.Column9.Name = "Column9";
            // 
            // FrmTimegroupOfShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTimegroupOfShift";
            this.Text = "时间段维护";
            this.Load += new System.EventHandler(this.FrmTimegroupOfShift_Load);
            this.panel_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Bar bar8;
        private DevComponents.DotNetBar.ButtonItem buttonItem39;
        private DevComponents.DotNetBar.LabelItem labelItem8;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvTimeGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
    }
}