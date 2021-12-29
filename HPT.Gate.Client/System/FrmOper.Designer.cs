namespace HPT.Gate.Client.oper
{
    partial class FrmOper
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOper));
            this.dgvOper = new System.Windows.Forms.DataGridView();
            this.管理员编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.op_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem41 = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem17 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem42 = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem20 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOper
            // 
            this.dgvOper.AllowUserToAddRows = false;
            this.dgvOper.AllowUserToDeleteRows = false;
            this.dgvOper.AllowUserToResizeColumns = false;
            this.dgvOper.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dgvOper.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOper.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOper.BackgroundColor = System.Drawing.Color.White;
            this.dgvOper.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOper.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOper.ColumnHeadersHeight = 25;
            this.dgvOper.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.管理员编号,
            this.op_Name,
            this.Column2});
            this.dgvOper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOper.Location = new System.Drawing.Point(0, 79);
            this.dgvOper.Name = "dgvOper";
            this.dgvOper.ReadOnly = true;
            this.dgvOper.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvOper.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOper.RowTemplate.Height = 23;
            this.dgvOper.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOper.Size = new System.Drawing.Size(934, 443);
            this.dgvOper.TabIndex = 9;
            // 
            // 管理员编号
            // 
            this.管理员编号.DataPropertyName = "op_id";
            this.管理员编号.HeaderText = "管理员编号";
            this.管理员编号.Name = "管理员编号";
            this.管理员编号.ReadOnly = true;
            // 
            // op_Name
            // 
            this.op_Name.DataPropertyName = "op_Name";
            this.op_Name.FillWeight = 120F;
            this.op_Name.HeaderText = "管理员帐号";
            this.op_Name.Name = "op_Name";
            this.op_Name.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Descr";
            this.Column2.FillWeight = 120F;
            this.Column2.HeaderText = "描述";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.Transparent;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem16,
            this.labelItem41,
            this.buttonItem17,
            this.labelItem42,
            this.buttonItem20,
            this.labelItem1,
            this.buttonItem1});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(934, 78);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.bar1.TabIndex = 8;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // buttonItem16
            // 
            this.buttonItem16.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem16.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonItem16.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem16.Image")));
            this.buttonItem16.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem16.Name = "buttonItem16";
            this.buttonItem16.Text = "添加用户";
            this.buttonItem16.Click += new System.EventHandler(this.buttonItem16_Click);
            // 
            // labelItem41
            // 
            this.labelItem41.Name = "labelItem41";
            this.labelItem41.Text = "    ";
            // 
            // buttonItem17
            // 
            this.buttonItem17.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem17.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonItem17.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem17.Image")));
            this.buttonItem17.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem17.Name = "buttonItem17";
            this.buttonItem17.Text = "修改用户信息";
            this.buttonItem17.Click += new System.EventHandler(this.buttonItem17_Click);
            // 
            // labelItem42
            // 
            this.labelItem42.Name = "labelItem42";
            this.labelItem42.Text = "    ";
            // 
            // buttonItem20
            // 
            this.buttonItem20.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem20.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonItem20.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem20.Image")));
            this.buttonItem20.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem20.Name = "buttonItem20";
            this.buttonItem20.Text = "删除用户";
            this.buttonItem20.Click += new System.EventHandler(this.buttonItem20_Click);
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "   ";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
            this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "用户权限管理";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 1);
            this.panel1.TabIndex = 11;
            // 
            // FrmOper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 522);
            this.Controls.Add(this.dgvOper);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bar1);
            this.DoubleBuffered = true;
            this.Name = "FrmOper";
            this.Text = "FrmOper";
            this.Load += new System.EventHandler(this.FrmOper_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvOper;
        private System.Windows.Forms.DataGridViewTextBoxColumn 管理员编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn op_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem16;
        private DevComponents.DotNetBar.LabelItem labelItem41;
        private DevComponents.DotNetBar.ButtonItem buttonItem17;
        private DevComponents.DotNetBar.LabelItem labelItem42;
        private DevComponents.DotNetBar.ButtonItem buttonItem20;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private System.Windows.Forms.Panel panel1;
    }
}