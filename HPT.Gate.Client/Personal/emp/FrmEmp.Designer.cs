namespace HPT.Gate.Client.emp
{
    partial class FrmEmp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmp));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmsEmp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.EmpTree = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvPempOfEmp = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Col_LeaveDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label_total = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbDeptName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbOff = new System.Windows.Forms.RadioButton();
            this.rbOn = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.ckbChildDept = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bar8 = new DevComponents.DotNetBar.Bar();
            this.buttonItem39 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem40 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem59 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.cmsEmp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPempOfEmp)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar8)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsEmp
            // 
            this.cmsEmp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem5});
            this.cmsEmp.Name = "cmsEmp";
            this.cmsEmp.Size = new System.Drawing.Size(149, 48);
            this.cmsEmp.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsEmp_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1.Text = "添加人员信息";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem5.Text = "修改人员信息";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "png-1737.png");
            this.imageList1.Images.SetKeyName(1, "png-1608.png");
            this.imageList1.Images.SetKeyName(2, "png-0702.png");
            this.imageList1.Images.SetKeyName(3, "png-0521.png");
            this.imageList1.Images.SetKeyName(4, "png-0237.png");
            this.imageList1.Images.SetKeyName(5, "PNG-0937.png");
            this.imageList1.Images.SetKeyName(6, "png-0067.png");
            this.imageList1.Images.SetKeyName(7, "png-0060.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 79);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.EmpTree);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvPempOfEmp);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(1073, 468);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 47;
            // 
            // EmpTree
            // 
            this.EmpTree.BackColor = System.Drawing.Color.White;
            this.EmpTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmpTree.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EmpTree.Location = new System.Drawing.Point(0, 24);
            this.EmpTree.Name = "EmpTree";
            this.EmpTree.ShowNodeToolTips = true;
            this.EmpTree.Size = new System.Drawing.Size(186, 444);
            this.EmpTree.TabIndex = 38;
            this.EmpTree.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.EmpTree_NodeMouseHover);
            this.EmpTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.EmpTree_NodeMouseClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::HPT.Gate.Client.Properties.Resources.bg_blue;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(186, 24);
            this.panel3.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "部门列表";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvPempOfEmp
            // 
            this.dgvPempOfEmp.AllowUserToAddRows = false;
            this.dgvPempOfEmp.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPempOfEmp.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPempOfEmp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvPempOfEmp.BackgroundColor = System.Drawing.Color.White;
            this.dgvPempOfEmp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MediumTurquoise;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPempOfEmp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPempOfEmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPempOfEmp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column5,
            this.Column4,
            this.Empcode,
            this.Empname,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column1,
            this.Column16,
            this.Column17,
            this.Column15,
            this.Column2,
            this.Column8,
            this.Column9,
            this.Col_Status,
            this.Col_LeaveDate,
            this.Column6});
            this.dgvPempOfEmp.ContextMenuStrip = this.cmsEmp;
            this.dgvPempOfEmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPempOfEmp.Location = new System.Drawing.Point(0, 48);
            this.dgvPempOfEmp.MultiSelect = false;
            this.dgvPempOfEmp.Name = "dgvPempOfEmp";
            this.dgvPempOfEmp.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPempOfEmp.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPempOfEmp.RowTemplate.Height = 23;
            this.dgvPempOfEmp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPempOfEmp.Size = new System.Drawing.Size(883, 420);
            this.dgvPempOfEmp.TabIndex = 39;
            this.dgvPempOfEmp.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPempOfEmp_CellClick);
            this.dgvPempOfEmp.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPempOfEmp_CellDoubleClick);
            this.dgvPempOfEmp.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvPempOfEmp_RowPostPaint);
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "DeptId";
            this.Column3.HeaderText = "DeptId";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            this.Column3.Width = 73;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "DeptName";
            this.Column5.FillWeight = 200F;
            this.Column5.HeaderText = "部门名称";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 81;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "EmpId";
            this.Column4.HeaderText = "EmpId";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            this.Column4.Width = 71;
            // 
            // Empcode
            // 
            this.Empcode.DataPropertyName = "Empcode";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Empcode.DefaultCellStyle = dataGridViewCellStyle3;
            this.Empcode.FillWeight = 200F;
            this.Empcode.HeaderText = "员工编号";
            this.Empcode.Name = "Empcode";
            this.Empcode.ReadOnly = true;
            this.Empcode.Width = 81;
            // 
            // Empname
            // 
            this.Empname.DataPropertyName = "EmpName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Empname.DefaultCellStyle = dataGridViewCellStyle4;
            this.Empname.FillWeight = 200F;
            this.Empname.HeaderText = "员工姓名";
            this.Empname.Name = "Empname";
            this.Empname.ReadOnly = true;
            this.Empname.Width = 81;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "Sex";
            this.Column10.HeaderText = "性别";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 57;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "Nation";
            this.Column11.HeaderText = "民族";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Visible = false;
            this.Column11.Width = 57;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "Telephone";
            this.Column12.HeaderText = "电话";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 57;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "IdentityCard";
            this.Column13.FillWeight = 200F;
            this.Column13.HeaderText = "身份证号";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Visible = false;
            this.Column13.Width = 81;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "BirthDay";
            this.Column14.FillWeight = 200F;
            this.Column14.HeaderText = "出生年月";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Visible = false;
            this.Column14.Width = 81;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ICCardNo";
            this.Column1.FillWeight = 210F;
            this.Column1.HeaderText = "IC/ID卡";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 75;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "IDSerial";
            this.Column16.FillWeight = 280F;
            this.Column16.HeaderText = "身份证序列号";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Width = 105;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "IDCardNo";
            this.Column17.FillWeight = 200F;
            this.Column17.HeaderText = "身份证号";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 81;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "HireDate";
            this.Column15.FillWeight = 200F;
            this.Column15.HeaderText = "聘用日期";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 81;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Duty";
            this.Column2.HeaderText = "职务";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 57;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "ReHire";
            this.Column8.FillWeight = 200F;
            this.Column8.HeaderText = "重新雇用";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column8.Width = 81;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "HireTimes";
            this.Column9.FillWeight = 200F;
            this.Column9.HeaderText = "雇用次数";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 81;
            // 
            // Col_Status
            // 
            this.Col_Status.DataPropertyName = "Status";
            this.Col_Status.HeaderText = "在职";
            this.Col_Status.Name = "Col_Status";
            this.Col_Status.ReadOnly = true;
            this.Col_Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Col_Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Col_Status.Width = 57;
            // 
            // Col_LeaveDate
            // 
            this.Col_LeaveDate.DataPropertyName = "LeaveDate";
            this.Col_LeaveDate.FillWeight = 200F;
            this.Col_LeaveDate.HeaderText = "离职日期";
            this.Col_LeaveDate.Name = "Col_LeaveDate";
            this.Col_LeaveDate.ReadOnly = true;
            this.Col_LeaveDate.Visible = false;
            this.Col_LeaveDate.Width = 81;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "FaceStatus";
            this.Column6.HeaderText = "人脸同步状态";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            this.Column6.Width = 105;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::HPT.Gate.Client.Properties.Resources.bg_blue;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label_total);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lbDeptName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.rbOff);
            this.panel2.Controls.Add(this.rbOn);
            this.panel2.Controls.Add(this.rbAll);
            this.panel2.Controls.Add(this.ckbChildDept);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(883, 24);
            this.panel2.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(725, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 24);
            this.label6.TabIndex = 8;
            this.label6.Text = "共查询到";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_total
            // 
            this.label_total.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_total.Location = new System.Drawing.Point(791, 0);
            this.label_total.Name = "label_total";
            this.label_total.Size = new System.Drawing.Size(46, 24);
            this.label_total.TabIndex = 7;
            this.label_total.Text = "000000";
            this.label_total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(837, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "条记录";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbDeptName
            // 
            this.lbDeptName.AutoSize = true;
            this.lbDeptName.Location = new System.Drawing.Point(73, 5);
            this.lbDeptName.Name = "lbDeptName";
            this.lbDeptName.Size = new System.Drawing.Size(0, 12);
            this.lbDeptName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "当前部门:";
            // 
            // rbOff
            // 
            this.rbOff.AutoSize = true;
            this.rbOff.ForeColor = System.Drawing.Color.Gray;
            this.rbOff.Location = new System.Drawing.Point(472, 4);
            this.rbOff.Name = "rbOff";
            this.rbOff.Size = new System.Drawing.Size(71, 16);
            this.rbOff.TabIndex = 3;
            this.rbOff.Text = "离职人员";
            this.rbOff.UseVisualStyleBackColor = true;
            this.rbOff.CheckedChanged += new System.EventHandler(this.rbOff_CheckedChanged);
            // 
            // rbOn
            // 
            this.rbOn.AutoSize = true;
            this.rbOn.ForeColor = System.Drawing.Color.Black;
            this.rbOn.Location = new System.Drawing.Point(395, 4);
            this.rbOn.Name = "rbOn";
            this.rbOn.Size = new System.Drawing.Size(71, 16);
            this.rbOn.TabIndex = 2;
            this.rbOn.Text = "在职人员";
            this.rbOn.UseVisualStyleBackColor = true;
            this.rbOn.CheckedChanged += new System.EventHandler(this.rbOn_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.ForeColor = System.Drawing.Color.Blue;
            this.rbAll.Location = new System.Drawing.Point(318, 4);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(71, 16);
            this.rbAll.TabIndex = 1;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "所有人员";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // ckbChildDept
            // 
            this.ckbChildDept.AutoSize = true;
            this.ckbChildDept.Location = new System.Drawing.Point(184, 5);
            this.ckbChildDept.Name = "ckbChildDept";
            this.ckbChildDept.Size = new System.Drawing.Size(108, 16);
            this.ckbChildDept.TabIndex = 0;
            this.ckbChildDept.Text = "显示子部门人员";
            this.ckbChildDept.UseVisualStyleBackColor = true;
            this.ckbChildDept.CheckedChanged += new System.EventHandler(this.ckbChildDept_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::HPT.Gate.Client.Properties.Resources.bg_blue;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(883, 24);
            this.panel4.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(883, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "人员列表";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumBlue;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1073, 1);
            this.panel1.TabIndex = 46;
            // 
            // bar8
            // 
            this.bar8.BackColor = System.Drawing.Color.Transparent;
            this.bar8.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar8.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem39,
            this.buttonItem40,
            this.buttonItem6,
            this.buttonItem5,
            this.buttonItem1,
            this.buttonItem59,
            this.buttonItem2,
            this.buttonItem4,
            this.buttonItem3,
            this.buttonItem7});
            this.bar8.ItemSpacing = 20;
            this.bar8.Location = new System.Drawing.Point(0, 0);
            this.bar8.Name = "bar8";
            this.bar8.Size = new System.Drawing.Size(1073, 78);
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
            this.buttonItem39.Image = global::HPT.Gate.Client.Properties.Resources.bt_add;
            this.buttonItem39.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem39.Name = "buttonItem39";
            this.buttonItem39.Text = "新  增";
            this.buttonItem39.Click += new System.EventHandler(this.buttonItem39_Click);
            // 
            // buttonItem40
            // 
            this.buttonItem40.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem40.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem40.Image = global::HPT.Gate.Client.Properties.Resources.bt_edit;
            this.buttonItem40.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem40.Name = "buttonItem40";
            this.buttonItem40.Text = "修  改";
            this.buttonItem40.Click += new System.EventHandler(this.buttonItem40_Click);
            // 
            // buttonItem6
            // 
            this.buttonItem6.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem6.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem6.Image = global::HPT.Gate.Client.Properties.Resources.bt_delete;
            this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Text = "删除";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // buttonItem5
            // 
            this.buttonItem5.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem5.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem5.Image = global::HPT.Gate.Client.Properties.Resources.btn_user;
            this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Text = "人脸管理";
            this.buttonItem5.Click += new System.EventHandler(this.buttonItem5_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
            this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "查找人员信息";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem59
            // 
            this.buttonItem59.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem59.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem59.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem59.Image")));
            this.buttonItem59.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem59.Name = "buttonItem59";
            this.buttonItem59.Text = "从Excel导入";
            this.buttonItem59.Click += new System.EventHandler(this.buttonItem59_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem2.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem2.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem2.Image")));
            this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "批量处理";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem4.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem4.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem4.Image")));
            this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "发卡器设置";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem3.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem3.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem3.Image")));
            this.buttonItem3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "导出到Excel";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click_1);
            // 
            // buttonItem7
            // 
            this.buttonItem7.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem7.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonItem7.Image = global::HPT.Gate.Client.Properties.Resources.bt_query;
            this.buttonItem7.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.Text = "出入记录查询";
            this.buttonItem7.Click += new System.EventHandler(this.buttonItem7_Click);
            // 
            // FrmEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 547);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bar8);
            this.Name = "FrmEmp";
            this.Text = "FEmp";
            this.Load += new System.EventHandler(this.FEmp_Load);
            this.cmsEmp.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPempOfEmp)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Bar bar8;
        private DevComponents.DotNetBar.ButtonItem buttonItem39;
        private DevComponents.DotNetBar.ButtonItem buttonItem40;
        private DevComponents.DotNetBar.ButtonItem buttonItem59;
        private System.Windows.Forms.DataGridView dgvPempOfEmp;
        private System.Windows.Forms.TreeView EmpTree;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        public System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip cmsEmp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox ckbChildDept;
        private System.Windows.Forms.RadioButton rbOff;
        private System.Windows.Forms.RadioButton rbOn;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Label lbDeptName;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_total;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Empcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Empname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Col_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_LeaveDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
    }
}