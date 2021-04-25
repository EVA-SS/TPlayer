namespace TPlayer.Frm
{
    partial class EffectSettingP2
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
            this.label5 = new System.Windows.Forms.Label();
            this.btn_accelerate = new TSkin.TCheckBox();
            this.btn_quality = new TSkin.TCheckBox();
            this.btn_rotate4 = new TSkin.TBut();
            this.btn_rotate2 = new TSkin.TBut();
            this.btn_rotate3 = new TSkin.TBut();
            this.btn_rotate1 = new TSkin.TBut();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_scale4 = new TSkin.TRadioButton();
            this.btn_scale3 = new TSkin.TRadioButton();
            this.btn_scale2 = new TSkin.TRadioButton();
            this.btn_scale1 = new TSkin.TRadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(0, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(312, 26);
            this.label5.TabIndex = 0;
            this.label5.Text = "若画质设置、旋转无法生效 请检查解码器";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_accelerate
            // 
            this.btn_accelerate.AutoSize = true;
            this.btn_accelerate.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_accelerate.Location = new System.Drawing.Point(22, 200);
            this.btn_accelerate.Margin = new System.Windows.Forms.Padding(0);
            this.btn_accelerate.MouseLocation = new System.Drawing.Point(-1, -1);
            this.btn_accelerate.Name = "btn_accelerate";
            this.btn_accelerate.Size = new System.Drawing.Size(79, 30);
            this.btn_accelerate.TabIndex = 5;
            this.btn_accelerate.Text = "硬件加速";
            this.btn_accelerate.UseVisualStyleBackColor = true;
            // 
            // btn_quality
            // 
            this.btn_quality.AutoSize = true;
            this.btn_quality.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_quality.Location = new System.Drawing.Point(178, 200);
            this.btn_quality.Margin = new System.Windows.Forms.Padding(0);
            this.btn_quality.MouseLocation = new System.Drawing.Point(-1, -1);
            this.btn_quality.Name = "btn_quality";
            this.btn_quality.Size = new System.Drawing.Size(79, 30);
            this.btn_quality.TabIndex = 6;
            this.btn_quality.Text = "画质增强";
            this.btn_quality.UseVisualStyleBackColor = true;
            // 
            // btn_rotate4
            // 
            this.btn_rotate4.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btn_rotate4.Image = global::TPlayer.Properties.Resources.icon_image2;
            this.btn_rotate4.ImageMargin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btn_rotate4.ImageSize = new System.Drawing.Size(20,20);
            this.btn_rotate4.Location = new System.Drawing.Point(178, 136);
            this.btn_rotate4.Name = "btn_rotate4";
            this.btn_rotate4.Radius = 32;
            this.btn_rotate4.Size = new System.Drawing.Size(90, 34);
            this.btn_rotate4.TabIndex = 4;
            this.btn_rotate4.Tag = "1";
            this.btn_rotate4.Text = "垂直旋转";
            this.btn_rotate4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_rotate4.TextMargin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btn_rotate4.Click += new System.EventHandler(this.btn_rotate4_Click);
            // 
            // btn_rotate2
            // 
            this.btn_rotate2.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btn_rotate2.Image = global::TPlayer.Properties.Resources.icon_rotate2;
            this.btn_rotate2.ImageMargin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btn_rotate2.ImageSize = new System.Drawing.Size(20,20);
            this.btn_rotate2.Location = new System.Drawing.Point(178, 98);
            this.btn_rotate2.Name = "btn_rotate2";
            this.btn_rotate2.Radius = 32;
            this.btn_rotate2.Size = new System.Drawing.Size(102, 34);
            this.btn_rotate2.TabIndex = 2;
            this.btn_rotate2.Text = "逆时针旋转";
            this.btn_rotate2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_rotate2.TextMargin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btn_rotate2.Click += new System.EventHandler(this.btn_rotate2_Click);
            // 
            // btn_rotate3
            // 
            this.btn_rotate3.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btn_rotate3.Image = global::TPlayer.Properties.Resources.icon_image1;
            this.btn_rotate3.ImageMargin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btn_rotate3.ImageSize = new System.Drawing.Size(20,20);
            this.btn_rotate3.Location = new System.Drawing.Point(22, 136);
            this.btn_rotate3.Name = "btn_rotate3";
            this.btn_rotate3.Radius = 32;
            this.btn_rotate3.Size = new System.Drawing.Size(90, 34);
            this.btn_rotate3.TabIndex = 3;
            this.btn_rotate3.Tag = "1";
            this.btn_rotate3.Text = "水平旋转";
            this.btn_rotate3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_rotate3.TextMargin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btn_rotate3.Click += new System.EventHandler(this.btn_rotate3_Click);
            // 
            // btn_rotate1
            // 
            this.btn_rotate1.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btn_rotate1.Image = global::TPlayer.Properties.Resources.icon_rotate1;
            this.btn_rotate1.ImageMargin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btn_rotate1.ImageSize = new System.Drawing.Size(20,20);
            this.btn_rotate1.Location = new System.Drawing.Point(22, 98);
            this.btn_rotate1.Name = "btn_rotate1";
            this.btn_rotate1.Radius = 32;
            this.btn_rotate1.Size = new System.Drawing.Size(102, 34);
            this.btn_rotate1.TabIndex = 1;
            this.btn_rotate1.Tag = "0";
            this.btn_rotate1.Text = "顺时针旋转";
            this.btn_rotate1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_rotate1.TextMargin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btn_rotate1.Click += new System.EventHandler(this.btn_rotate1_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_scale4);
            this.panel3.Controls.Add(this.btn_scale3);
            this.panel3.Controls.Add(this.btn_scale2);
            this.panel3.Controls.Add(this.btn_scale1);
            this.panel3.Location = new System.Drawing.Point(12, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(292, 30);
            this.panel3.TabIndex = 0;
            // 
            // btn_scale4
            // 
            this.btn_scale4.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_scale4.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_scale4.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_scale4.Location = new System.Drawing.Point(195, 0);
            this.btn_scale4.Margin = new System.Windows.Forms.Padding(0);
            this.btn_scale4.Name = "btn_scale4";
            this.btn_scale4.Size = new System.Drawing.Size(91, 30);
            this.btn_scale4.TabIndex = 3;
            this.btn_scale4.TabStop = true;
            this.btn_scale4.Text = "铺满屏幕";
            this.btn_scale4.UseVisualStyleBackColor = true;
            this.btn_scale4.Click += new System.EventHandler(this.Change_XS);
            // 
            // btn_scale3
            // 
            this.btn_scale3.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_scale3.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_scale3.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_scale3.Location = new System.Drawing.Point(130, 0);
            this.btn_scale3.Margin = new System.Windows.Forms.Padding(0);
            this.btn_scale3.Name = "btn_scale3";
            this.btn_scale3.Size = new System.Drawing.Size(65, 30);
            this.btn_scale3.TabIndex = 2;
            this.btn_scale3.TabStop = true;
            this.btn_scale3.Text = "16:9";
            this.btn_scale3.UseVisualStyleBackColor = true;
            this.btn_scale3.Click += new System.EventHandler(this.Change_XS);
            // 
            // btn_scale2
            // 
            this.btn_scale2.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_scale2.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_scale2.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_scale2.Location = new System.Drawing.Point(65, 0);
            this.btn_scale2.Margin = new System.Windows.Forms.Padding(0);
            this.btn_scale2.Name = "btn_scale2";
            this.btn_scale2.Size = new System.Drawing.Size(65, 30);
            this.btn_scale2.TabIndex = 1;
            this.btn_scale2.TabStop = true;
            this.btn_scale2.Text = "4:3";
            this.btn_scale2.UseVisualStyleBackColor = true;
            this.btn_scale2.Click += new System.EventHandler(this.Change_XS);
            // 
            // btn_scale1
            // 
            this.btn_scale1.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_scale1.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_scale1.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_scale1.Location = new System.Drawing.Point(0, 0);
            this.btn_scale1.Margin = new System.Windows.Forms.Padding(0);
            this.btn_scale1.Name = "btn_scale1";
            this.btn_scale1.Size = new System.Drawing.Size(65, 30);
            this.btn_scale1.TabIndex = 0;
            this.btn_scale1.TabStop = true;
            this.btn_scale1.Text = "默认";
            this.btn_scale1.UseVisualStyleBackColor = true;
            this.btn_scale1.Click += new System.EventHandler(this.Change_XS);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "旋转";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "画质";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "比例";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EffectSettingP2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(31)))));
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_accelerate);
            this.Controls.Add(this.btn_quality);
            this.Controls.Add(this.btn_rotate4);
            this.Controls.Add(this.btn_rotate2);
            this.Controls.Add(this.btn_rotate3);
            this.Controls.Add(this.btn_rotate1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("微软雅黑",9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "EffectSettingP2";
            this.Size = new System.Drawing.Size(312, 290);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private TSkin.TCheckBox btn_accelerate;
        private TSkin.TCheckBox btn_quality;
        private TSkin.TBut btn_rotate4;
        private TSkin.TBut btn_rotate2;
        private TSkin.TBut btn_rotate3;
        private TSkin.TBut btn_rotate1;
        private System.Windows.Forms.Panel panel3;
        private TSkin.TRadioButton btn_scale4;
        private TSkin.TRadioButton btn_scale3;
        private TSkin.TRadioButton btn_scale2;
        private TSkin.TRadioButton btn_scale1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}
