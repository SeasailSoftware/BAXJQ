namespace HPT.Gate.Host.face
{
    partial class AuthorProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorProgress));
            this.progressEmp = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.progressDev = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbEmp = new System.Windows.Forms.Label();
            this.lbDev = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnDev = new System.Windows.Forms.Panel();
            this.pnEmp = new System.Windows.Forms.Panel();
            this.panel_main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnDev.SuspendLayout();
            this.pnEmp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.tableLayoutPanel1);
            this.panel_main.Size = new System.Drawing.Size(584, 82);

            // 
            // progressEmp
            // 
            // 
            // 
            // 
            this.progressEmp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressEmp.Location = new System.Drawing.Point(66, 7);
            this.progressEmp.Name = "progressEmp";
            this.progressEmp.Size = new System.Drawing.Size(443, 23);
            this.progressEmp.TabIndex = 0;
            this.progressEmp.Text = "progressBarX1";
            // 
            // progressDev
            // 
            // 
            // 
            // 
            this.progressDev.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressDev.Location = new System.Drawing.Point(66, 6);
            this.progressDev.Name = "progressDev";
            this.progressDev.Size = new System.Drawing.Size(443, 23);
            this.progressDev.TabIndex = 1;
            this.progressDev.Text = "progressBarX2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前进度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "总进度";
            // 
            // lbEmp
            // 
            this.lbEmp.AutoSize = true;
            this.lbEmp.Location = new System.Drawing.Point(529, 13);
            this.lbEmp.Name = "lbEmp";
            this.lbEmp.Size = new System.Drawing.Size(23, 12);
            this.lbEmp.TabIndex = 4;
            this.lbEmp.Text = "0/0";
            // 
            // lbDev
            // 
            this.lbDev.AutoSize = true;
            this.lbDev.Location = new System.Drawing.Point(529, 6);
            this.lbDev.Name = "lbDev";
            this.lbDev.Size = new System.Drawing.Size(23, 12);
            this.lbDev.TabIndex = 5;
            this.lbDev.Text = "0/0";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pnDev, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnEmp, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 82);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // pnDev
            // 
            this.pnDev.Controls.Add(this.progressDev);
            this.pnDev.Controls.Add(this.label2);
            this.pnDev.Controls.Add(this.lbDev);
            this.pnDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDev.Location = new System.Drawing.Point(3, 44);
            this.pnDev.Name = "pnDev";
            this.pnDev.Size = new System.Drawing.Size(578, 35);
            this.pnDev.TabIndex = 1;
            // 
            // pnEmp
            // 
            this.pnEmp.Controls.Add(this.label1);
            this.pnEmp.Controls.Add(this.progressEmp);
            this.pnEmp.Controls.Add(this.lbEmp);
            this.pnEmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEmp.Location = new System.Drawing.Point(3, 3);
            this.pnEmp.Name = "pnEmp";
            this.pnEmp.Size = new System.Drawing.Size(578, 35);
            this.pnEmp.TabIndex = 0;
            // 
            // AuthorProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 112);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuthorProgress";
            this.ShowInTaskbar = false;
            this.Text = "授权进度";
            this.Load += new System.EventHandler(this.AuthorProgress_Load);
            this.panel_main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnDev.ResumeLayout(false);
            this.pnDev.PerformLayout();
            this.pnEmp.ResumeLayout(false);
            this.pnEmp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ProgressBarX progressEmp;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressDev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbEmp;
        private System.Windows.Forms.Label lbDev;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnDev;
        private System.Windows.Forms.Panel pnEmp;
    }
}