namespace HPT.Gate.Client.BarCode
{
    partial class FrmBarcodeSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBarcodeSettings));
            this.label2 = new System.Windows.Forms.Label();
            this.numBarcodeEffecttime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numTimesOfIn = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numTimesOfOut = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.DevTree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel_main.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBarcodeEffecttime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimesOfIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimesOfOut)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.label2);
            this.panel_main.Controls.Add(this.numBarcodeEffecttime);
            this.panel_main.Controls.Add(this.label3);
            this.panel_main.Controls.Add(this.label4);
            this.panel_main.Controls.Add(this.numTimesOfIn);
            this.panel_main.Controls.Add(this.label5);
            this.panel_main.Controls.Add(this.label7);
            this.panel_main.Controls.Add(this.numTimesOfOut);
            this.panel_main.Controls.Add(this.label6);
            this.panel_main.Controls.Add(this.DevTree);
            this.panel_main.Controls.Add(this.label1);
            this.panel_main.Size = new System.Drawing.Size(376, 298);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Location = new System.Drawing.Point(0, 328);
            this.panel_bottom.Size = new System.Drawing.Size(376, 42);
            // 
            // btCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(237, 10);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(64, 10);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "有效时间:";
            // 
            // numBarcodeEffecttime
            // 
            this.numBarcodeEffecttime.Location = new System.Drawing.Point(118, 16);
            this.numBarcodeEffecttime.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numBarcodeEffecttime.Name = "numBarcodeEffecttime";
            this.numBarcodeEffecttime.Size = new System.Drawing.Size(114, 21);
            this.numBarcodeEffecttime.TabIndex = 3;
            this.numBarcodeEffecttime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "分钟";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "限制入口次数:";
            // 
            // numTimesOfIn
            // 
            this.numTimesOfIn.Location = new System.Drawing.Point(118, 43);
            this.numTimesOfIn.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numTimesOfIn.Name = "numTimesOfIn";
            this.numTimesOfIn.Size = new System.Drawing.Size(114, 21);
            this.numTimesOfIn.TabIndex = 8;
            this.numTimesOfIn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "次(0代表不限制次数)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(238, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "次(0代表不限制次数)";
            // 
            // numTimesOfOut
            // 
            this.numTimesOfOut.Location = new System.Drawing.Point(118, 70);
            this.numTimesOfOut.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numTimesOfOut.Name = "numTimesOfOut";
            this.numTimesOfOut.Size = new System.Drawing.Size(114, 21);
            this.numTimesOfOut.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "限制出口次数:";
            // 
            // DevTree
            // 
            this.DevTree.CheckBoxes = true;
            this.DevTree.Location = new System.Drawing.Point(31, 126);
            this.DevTree.Name = "DevTree";
            this.DevTree.Size = new System.Drawing.Size(308, 154);
            this.DevTree.TabIndex = 13;
            this.DevTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.DevTree_AfterCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "指定闸机权限:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "png-0789.png");
            this.imageList1.Images.SetKeyName(1, "png-1705.png");
            this.imageList1.Images.SetKeyName(2, "png-1484.png");
            this.imageList1.Images.SetKeyName(3, "png-1481.png");
            this.imageList1.Images.SetKeyName(4, "png-0638.png");
            // 
            // FrmBarcodeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 370);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBarcodeSettings";
            this.Text = "条码设置";
            this.Load += new System.EventHandler(this.FrmBarcodeSettings_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numBarcodeEffecttime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimesOfIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimesOfOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numBarcodeEffecttime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numTimesOfIn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numTimesOfOut;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TreeView DevTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
    }
}