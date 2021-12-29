namespace HPT.Face.Client.Personal.emp
{
    partial class FrmCaptureFace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCaptureFace));
            this.picCamera = new AForge.Controls.VideoSourcePlayer();
            this.btnCapture = new DevComponents.DotNetBar.ButtonX();
            this.picCapture = new System.Windows.Forms.PictureBox();
            this.buttonX6 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.picCamera);
            this.panel_main.Controls.Add(this.btnCapture);
            this.panel_main.Controls.Add(this.picCapture);
            this.panel_main.Controls.Add(this.buttonX6);
            this.panel_main.Controls.Add(this.buttonX1);
            this.panel_main.Size = new System.Drawing.Size(504, 330);
            // 
            // picCamera
            // 
            this.picCamera.Location = new System.Drawing.Point(3, 6);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(278, 319);
            this.picCamera.TabIndex = 95;
            this.picCamera.Text = "videoSourcePlayer1";
            this.picCamera.VideoSource = null;
            // 
            // btnCapture
            // 
            this.btnCapture.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCapture.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnCapture.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCapture.Image = ((System.Drawing.Image)(resources.GetObject("btnCapture.Image")));
            this.btnCapture.Location = new System.Drawing.Point(341, 248);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(84, 35);
            this.btnCapture.TabIndex = 94;
            this.btnCapture.Text = "抓拍";
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // picCapture
            // 
            this.picCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.picCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picCapture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCapture.Location = new System.Drawing.Point(287, 6);
            this.picCapture.Name = "picCapture";
            this.picCapture.Size = new System.Drawing.Size(200, 236);
            this.picCapture.TabIndex = 91;
            this.picCapture.TabStop = false;
            // 
            // buttonX6
            // 
            this.buttonX6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX6.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX6.Location = new System.Drawing.Point(287, 295);
            this.buttonX6.Name = "buttonX6";
            this.buttonX6.Size = new System.Drawing.Size(68, 23);
            this.buttonX6.TabIndex = 92;
            this.buttonX6.Text = "确定(S)";
            this.buttonX6.Click += new System.EventHandler(this.buttonX6_Click_1);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.buttonX1.Location = new System.Drawing.Point(419, 295);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(68, 23);
            this.buttonX1.TabIndex = 93;
            this.buttonX1.Text = "取消(C)";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click_1);
            // 
            // FrmCaptureFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(504, 360);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCaptureFace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCaptureFace_FormClosing_1);
            this.Load += new System.EventHandler(this.FrmCaptureFace_Load_1);
            this.panel_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCapture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer picCamera;
        private DevComponents.DotNetBar.ButtonX btnCapture;
        private System.Windows.Forms.PictureBox picCapture;
        private DevComponents.DotNetBar.ButtonX buttonX6;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}