namespace HPT.Joey.Lib.Controls
{
    partial class LoadingControl
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
            this.lbl_caption = new System.Windows.Forms.Label();
            this.lbl_description = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lbl_caption
            // 
            this.lbl_caption.BackColor = System.Drawing.Color.Transparent;
            this.lbl_caption.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_caption.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_caption.Location = new System.Drawing.Point(107, 9);
            this.lbl_caption.Name = "lbl_caption";
            this.lbl_caption.Size = new System.Drawing.Size(297, 23);
            this.lbl_caption.TabIndex = 0;
            this.lbl_caption.Text = "Please Wait";
            // 
            // lbl_description
            // 
            this.lbl_description.BackColor = System.Drawing.Color.Transparent;
            this.lbl_description.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_description.Location = new System.Drawing.Point(108, 32);
            this.lbl_description.Name = "lbl_description";
            this.lbl_description.Size = new System.Drawing.Size(296, 22);
            this.lbl_description.TabIndex = 1;
            this.lbl_description.Text = "label2";
            this.lbl_description.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(110, 57);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(294, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 3;
            // 
            // LoadingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::HPT.Joey.Lib.Properties.Resources.bg_blue1;
            this.ClientSize = new System.Drawing.Size(416, 85);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lbl_description);
            this.Controls.Add(this.lbl_caption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingControl";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "LoadingControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadingControl_FormClosing_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_caption;
        private System.Windows.Forms.Label lbl_description;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}