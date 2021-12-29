namespace HPT.Gate.Client.Authorize
{
    partial class FrmDevAuthorize
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDevAuthorize));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.PlaceTree = new System.Windows.Forms.TreeView();
            this.dgvEmps = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.groupBox2);
            this.panel_main.Controls.Add(this.groupBox1);
            this.panel_main.Size = new System.Drawing.Size(648, 521);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 551);
            this.panel_bottom.Size = new System.Drawing.Size(648, 42);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(126, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(447, 10);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "png-1737.png");
            this.imageList1.Images.SetKeyName(1, "png-0702.png");
            this.imageList1.Images.SetKeyName(2, "png-0521.png");
            this.imageList1.Images.SetKeyName(3, "png-0237.png");
            this.imageList1.Images.SetKeyName(4, "PNG-0937.png");
            this.imageList1.Images.SetKeyName(5, "png-0067.png");
            // 
            // PlaceTree
            // 
            this.PlaceTree.CheckBoxes = true;
            this.PlaceTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaceTree.Location = new System.Drawing.Point(3, 17);
            this.PlaceTree.Name = "PlaceTree";
            this.PlaceTree.Size = new System.Drawing.Size(308, 501);
            this.PlaceTree.TabIndex = 0;
            this.PlaceTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // dgvEmps
            // 
            this.dgvEmps.AllowUserToAddRows = false;
            this.dgvEmps.AllowUserToDeleteRows = false;
            this.dgvEmps.AllowUserToResizeColumns = false;
            this.dgvEmps.AllowUserToResizeRows = false;
            this.dgvEmps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmps.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvEmps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmps.Location = new System.Drawing.Point(3, 44);
            this.dgvEmps.Name = "dgvEmps";
            this.dgvEmps.RowTemplate.Height = 23;
            this.dgvEmps.Size = new System.Drawing.Size(328, 474);
            this.dgvEmps.TabIndex = 1;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "DeptId";
            this.Column4.HeaderText = "DeptId";
            this.Column4.Name = "Column4";
            this.Column4.Visible = false;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "DeptName";
            this.Column5.HeaderText = "部门名称";
            this.Column5.Name = "Column5";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "EmpId";
            this.Column1.HeaderText = "员工识别号";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "人员编号";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "EmpCode";
            this.Column3.HeaderText = "人员姓名";
            this.Column3.Name = "Column3";
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.BackColor = System.Drawing.Color.Transparent;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Left;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.labelItem1,
            this.buttonItem2});
            this.bar1.Location = new System.Drawing.Point(3, 17);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(328, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "挑选人员";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "                               ";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonItem2.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "清除人员";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvEmps);
            this.groupBox1.Controls.Add(this.bar1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 521);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "人员列表";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PlaceTree);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(334, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 521);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备列表";
            // 
            // FrmDevAuthorize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 593);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDevAuthorize";
            this.ShowControllBox = true;
            this.Text = "设备授权";
            this.Load += new System.EventHandler(this.FrmDevAuthorize_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvEmps;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private System.Windows.Forms.TreeView PlaceTree;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}