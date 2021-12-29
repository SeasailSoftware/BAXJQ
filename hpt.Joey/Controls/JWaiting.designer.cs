namespace HPT.Joey.Lib.Controls
{
    partial class JWaiting
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
            this.SuspendLayout();
            // 
            // lbl_caption
            // 
            this.lbl_caption.BackColor = System.Drawing.Color.Transparent;
            this.lbl_caption.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_caption.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_caption.Location = new System.Drawing.Point(107, 31);
            this.lbl_caption.Name = "lbl_caption";
            this.lbl_caption.Size = new System.Drawing.Size(195, 23);
            this.lbl_caption.TabIndex = 7;
            this.lbl_caption.Text = "正在加载中,请稍后...";
            // 
            // JWaiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HPT.Joey.Lib.Properties.Resources.bg_blue1;
            this.ClientSize = new System.Drawing.Size(311, 86);
            this.Controls.Add(this.lbl_caption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "JWaiting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProcessForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.JProgressForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_caption;
    }
}