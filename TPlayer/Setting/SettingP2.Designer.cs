namespace TPlayer.Frm
{
    partial class SettingP2
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.backImage = new TSkin.BackImagePanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.check_showlogo = new TSkin.TCheckBox();
            this.backImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "外观设置";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // backImage
            // 
            this.backImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.backImage.BackgroundImage = global::TPlayer.Properties.Resources.backimg;
            this.backImage.Controls.Add(this.label2);
            this.backImage.Location = new System.Drawing.Point(18, 30);
            this.backImage.Name = "backImage";
            this.backImage.Size = new System.Drawing.Size(200, 100);
            this.backImage.TabIndex = 1;
            this.backImage.Click += new System.EventHandler(this.backImage_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "点击修改背景壁纸";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.backImage_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label5.Location = new System.Drawing.Point(44, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "增强沉浸式体验";
            // 
            // check_showlogo
            // 
            this.check_showlogo.AutoSize = true;
            this.check_showlogo.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_showlogo.Location = new System.Drawing.Point(28, 151);
            this.check_showlogo.Margin = new System.Windows.Forms.Padding(0);
            this.check_showlogo.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_showlogo.Name = "check_showlogo";
            this.check_showlogo.Size = new System.Drawing.Size(147, 30);
            this.check_showlogo.TabIndex = 1;
            this.check_showlogo.Text = "隐藏 TPlayer Logo";
            this.check_showlogo.UseVisualStyleBackColor = true;
            // 
            // SettingP2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.check_showlogo);
            this.Controls.Add(this.backImage);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑",10F);
            this.Name = "SettingP2";
            this.Size = new System.Drawing.Size(460, 339);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            this.backImage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private TSkin.BackImagePanel backImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private TSkin.TCheckBox check_showlogo;
    }
}
