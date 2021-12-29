namespace HPT.Gate.Host.face
{
    partial class FrmFaceDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFaceDetail));
            this.bar4 = new DevComponents.DotNetBar.Bar();
            this.btnQuery = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.btnClear = new DevComponents.DotNetBar.ButtonItem();
            this.dgvTaskResult = new System.Windows.Forms.DataGridView();
            this.RecId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskResult)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.dgvTaskResult);
            this.panel_main.Controls.Add(this.bar4);
            this.panel_main.Size = new System.Drawing.Size(800, 420);
            // 
            // bar4
            // 
            this.bar4.BackColor = System.Drawing.Color.Transparent;
            this.bar4.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar4.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar4.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnQuery,
            this.btnClear,
            this.btnClose});
            this.bar4.ItemSpacing = 15;
            this.bar4.Location = new System.Drawing.Point(0, 0);
            this.bar4.Name = "bar4";
            this.bar4.Size = new System.Drawing.Size(800, 78);
            this.bar4.Stretch = true;
            this.bar4.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.bar4.TabIndex = 5;
            this.bar4.TabStop = false;
            this.bar4.Text = "bar4";
            // 
            // btnQuery
            // 
            this.btnQuery.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.btnQuery.Image = global::HPT.Gate.Host.Properties.Resources.bt_query;
            this.btnQuery.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Text = "同步查询";
            this.btnQuery.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnClose.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "退 出";
            this.btnClose.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // btnClear
            // 
            this.btnClear.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.btnClear.Image = global::HPT.Gate.Host.Properties.Resources.bt_delete;
            this.btnClear.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnClear.Name = "btnClear";
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // dgvTaskResult
            // 
            this.dgvTaskResult.AllowUserToAddRows = false;
            this.dgvTaskResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaskResult.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvTaskResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecId,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dgvTaskResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaskResult.Location = new System.Drawing.Point(0, 78);
            this.dgvTaskResult.Name = "dgvTaskResult";
            this.dgvTaskResult.RowTemplate.Height = 23;
            this.dgvTaskResult.Size = new System.Drawing.Size(800, 342);
            this.dgvTaskResult.TabIndex = 6;
            // 
            // RecId
            // 
            this.RecId.DataPropertyName = "RecId";
            this.RecId.HeaderText = "编号";
            this.RecId.Name = "RecId";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "TaskId";
            this.Column2.HeaderText = "任务编号";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "EmpId";
            this.Column3.HeaderText = "人员编号";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "DeviceId";
            this.Column4.HeaderText = "设备编号";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Result";
            this.Column5.HeaderText = "同步状态";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Msg";
            this.Column6.HeaderText = "提示信息";
            this.Column6.Name = "Column6";
            // 
            // FrmFaceDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFaceDetail";
            this.Text = "人脸同步详情";
            this.panel_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTaskResult;
        private DevComponents.DotNetBar.Bar bar4;
        private DevComponents.DotNetBar.ButtonItem btnQuery;
        private DevComponents.DotNetBar.ButtonItem btnClear;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}