namespace TPlayer.Frm
{
    partial class EffectSettingP3
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
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_color3 = new System.Windows.Forms.PictureBox();
            this.btn_color2 = new System.Windows.Forms.PictureBox();
            this.btn_color1 = new System.Windows.Forms.PictureBox();
            this.prog_hue = new TSkin.TProgMi();
            this.prog_contrast = new TSkin.TProgMi();
            this.prog_saturation = new TSkin.TProgMi();
            this.prog_brightness = new TSkin.TProgMi();
            this.label_hue = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label_saturation = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label_contrast = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_brightness = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btn_color3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_color2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_color1)).BeginInit();
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label14.Location = new System.Drawing.Point(211, 214);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 20);
            this.label14.TabIndex = 0;
            this.label14.Text = "柔和";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label13.Location = new System.Drawing.Point(118, 214);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "明亮";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label8.Location = new System.Drawing.Point(25, 214);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "默认";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_color3
            // 
            this.btn_color3.Image = global::TPlayer.Properties.Resources.icon_color3;
            this.btn_color3.Location = new System.Drawing.Point(211, 176);
            this.btn_color3.Name = "btn_color3";
            this.btn_color3.Size = new System.Drawing.Size(70, 38);
            this.btn_color3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_color3.TabIndex = 4;
            this.btn_color3.TabStop = false;
            this.btn_color3.Click += new System.EventHandler(this.btn_color3_Click);
            // 
            // btn_color2
            // 
            this.btn_color2.Image = global::TPlayer.Properties.Resources.icon_color2;
            this.btn_color2.Location = new System.Drawing.Point(118, 176);
            this.btn_color2.Name = "btn_color2";
            this.btn_color2.Size = new System.Drawing.Size(70, 38);
            this.btn_color2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_color2.TabIndex = 4;
            this.btn_color2.TabStop = false;
            this.btn_color2.Click += new System.EventHandler(this.btn_color2_Click);
            // 
            // btn_color1
            // 
            this.btn_color1.Image = global::TPlayer.Properties.Resources.icon_color1;
            this.btn_color1.Location = new System.Drawing.Point(25, 176);
            this.btn_color1.Name = "btn_color1";
            this.btn_color1.Size = new System.Drawing.Size(70, 38);
            this.btn_color1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_color1.TabIndex = 4;
            this.btn_color1.TabStop = false;
            this.btn_color1.Click += new System.EventHandler(this.btn_color1_Click);
            // 
            // prog_hue
            // 
            this.prog_hue.Location = new System.Drawing.Point(70, 135);
            this.prog_hue.MaxValue = 100D;
            this.prog_hue.Name = "prog_hue";
            this.prog_hue.Size = new System.Drawing.Size(198, 16);
            this.prog_hue.TabIndex = 3;
            this.prog_hue.Value = 0D;
            // 
            // prog_contrast
            // 
            this.prog_contrast.Location = new System.Drawing.Point(70, 105);
            this.prog_contrast.MaxValue = 100D;
            this.prog_contrast.Name = "prog_contrast";
            this.prog_contrast.Size = new System.Drawing.Size(198, 16);
            this.prog_contrast.TabIndex = 2;
            this.prog_contrast.Value = 0D;
            // 
            // prog_saturation
            // 
            this.prog_saturation.Location = new System.Drawing.Point(70, 75);
            this.prog_saturation.MaxValue = 100D;
            this.prog_saturation.Name = "prog_saturation";
            this.prog_saturation.Size = new System.Drawing.Size(198, 16);
            this.prog_saturation.TabIndex = 1;
            this.prog_saturation.Value = 0D;
            // 
            // prog_brightness
            // 
            this.prog_brightness.Location = new System.Drawing.Point(70, 45);
            this.prog_brightness.MaxValue = 100D;
            this.prog_brightness.Name = "prog_brightness";
            this.prog_brightness.Size = new System.Drawing.Size(198, 16);
            this.prog_brightness.TabIndex = 0;
            this.prog_brightness.Value = 0D;
            // 
            // label_hue
            // 
            this.label_hue.Location = new System.Drawing.Point(274, 128);
            this.label_hue.Name = "label_hue";
            this.label_hue.Size = new System.Drawing.Size(34, 30);
            this.label_hue.TabIndex = 0;
            this.label_hue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(12, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 30);
            this.label12.TabIndex = 0;
            this.label12.Text = "色   相";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_saturation
            // 
            this.label_saturation.Location = new System.Drawing.Point(274, 68);
            this.label_saturation.Name = "label_saturation";
            this.label_saturation.Size = new System.Drawing.Size(34, 30);
            this.label_saturation.TabIndex = 0;
            this.label_saturation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(12, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 30);
            this.label11.TabIndex = 0;
            this.label11.Text = "饱和度";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_contrast
            // 
            this.label_contrast.Location = new System.Drawing.Point(274, 98);
            this.label_contrast.Name = "label_contrast";
            this.label_contrast.Size = new System.Drawing.Size(34, 30);
            this.label_contrast.TabIndex = 0;
            this.label_contrast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(12, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 30);
            this.label10.TabIndex = 0;
            this.label10.Text = "对比度";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(0, 264);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(312, 26);
            this.label6.TabIndex = 0;
            this.label6.Text = "若色彩调节功能无法使用 请检查解码器";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label6.Visible = false;
            // 
            // label_brightness
            // 
            this.label_brightness.Location = new System.Drawing.Point(274, 38);
            this.label_brightness.Name = "label_brightness";
            this.label_brightness.Size = new System.Drawing.Size(34, 30);
            this.label_brightness.TabIndex = 0;
            this.label_brightness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "亮   度";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "色彩";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EffectSettingP3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(31)))));
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_color3);
            this.Controls.Add(this.btn_color2);
            this.Controls.Add(this.btn_color1);
            this.Controls.Add(this.prog_hue);
            this.Controls.Add(this.prog_contrast);
            this.Controls.Add(this.prog_saturation);
            this.Controls.Add(this.prog_brightness);
            this.Controls.Add(this.label_hue);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label_saturation);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label_contrast);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_brightness);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("微软雅黑",9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "EffectSettingP3";
            this.Size = new System.Drawing.Size(312, 290);
            ((System.ComponentModel.ISupportInitialize)(this.btn_color3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_color2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_color1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox btn_color3;
        private System.Windows.Forms.PictureBox btn_color2;
        private System.Windows.Forms.PictureBox btn_color1;
        private TSkin.TProgMi prog_hue;
        private TSkin.TProgMi prog_contrast;
        private TSkin.TProgMi prog_saturation;
        private TSkin.TProgMi prog_brightness;
        private System.Windows.Forms.Label label_hue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_saturation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label_contrast;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_brightness;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
    }
}
