namespace TPlayer.Frm
{
    partial class SettingP1
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
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tRadioButton2 = new TSkin.TRadioButton();
            this.tRadioButton3 = new TSkin.TRadioButton();
            this.tRadioButton1 = new TSkin.TRadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.check_link_all = new TSkin.TCheckBox();
            this.check_link_video = new TSkin.TCheckBox();
            this.check_link_music = new TSkin.TCheckBox();
            this.btn_link_reset = new System.Windows.Forms.Button();
            this.btn_link_save = new TSkin.TBut();
            this.label11 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.check_accelerate = new TSkin.TCheckBox();
            this.check_multiple = new TSkin.TCheckBox();
            this.check_cache_greed = new TSkin.TCheckBox();
            this.check_rememberLocation = new TSkin.TCheckBox();
            this.check_cache_video = new TSkin.TCheckBox();
            this.check_minimizeToTray = new TSkin.TCheckBox();
            this.check_animation = new TSkin.TCheckBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "常规设置";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label8
            // 
            this.label8.AutoEllipsis = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label8.Location = new System.Drawing.Point(258, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 34);
            this.label8.TabIndex = 0;
            this.label8.Text = "记忆上次关闭播放时的播放位置和集数";
            this.label8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label7.Location = new System.Drawing.Point(44, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "允许同时运行多个TPlayer";
            this.label7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label6.Location = new System.Drawing.Point(258, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(170, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "不占用任务栏，后台播放 影视";
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label5.Location = new System.Drawing.Point(44, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "取消动画可以减少部分资源占用";
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "视频打开时";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tRadioButton2);
            this.panel1.Controls.Add(this.tRadioButton3);
            this.panel1.Controls.Add(this.tRadioButton1);
            this.panel1.Location = new System.Drawing.Point(22, 268);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 30);
            this.panel1.TabIndex = 5;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // tRadioButton2
            // 
            this.tRadioButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tRadioButton2.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.tRadioButton2.Location = new System.Drawing.Point(240, 0);
            this.tRadioButton2.Margin = new System.Windows.Forms.Padding(0);
            this.tRadioButton2.Name = "tRadioButton2";
            this.tRadioButton2.Size = new System.Drawing.Size(120, 30);
            this.tRadioButton2.TabIndex = 2;
            this.tRadioButton2.TabStop = true;
            this.tRadioButton2.Text = "自动全屏";
            this.tRadioButton2.UseVisualStyleBackColor = true;
            // 
            // tRadioButton3
            // 
            this.tRadioButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.tRadioButton3.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.tRadioButton3.Location = new System.Drawing.Point(120, 0);
            this.tRadioButton3.Margin = new System.Windows.Forms.Padding(0);
            this.tRadioButton3.Name = "tRadioButton3";
            this.tRadioButton3.Size = new System.Drawing.Size(120, 30);
            this.tRadioButton3.TabIndex = 1;
            this.tRadioButton3.TabStop = true;
            this.tRadioButton3.Text = "窗口适应视频";
            this.tRadioButton3.UseVisualStyleBackColor = true;
            // 
            // tRadioButton1
            // 
            this.tRadioButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tRadioButton1.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.tRadioButton1.Location = new System.Drawing.Point(0, 0);
            this.tRadioButton1.Margin = new System.Windows.Forms.Padding(0);
            this.tRadioButton1.Name = "tRadioButton1";
            this.tRadioButton1.Size = new System.Drawing.Size(120, 30);
            this.tRadioButton1.TabIndex = 0;
            this.tRadioButton1.TabStop = true;
            this.tRadioButton1.Text = "视频适应窗口";
            this.tRadioButton1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "网络下载";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label4.Location = new System.Drawing.Point(44, 378);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 52);
            this.label4.TabIndex = 0;
            this.label4.Text = "将视频缓存到本地，缓存结束会自动生成本地文件（下次打开更快）";
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label9
            // 
            this.label9.AutoEllipsis = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label9.Location = new System.Drawing.Point(258, 378);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 34);
            this.label9.TabIndex = 0;
            this.label9.Text = "播放期间持续缓存，直到视频缓存完毕";
            this.label9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 34);
            this.label10.TabIndex = 0;
            this.label10.Text = "文件关联";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.check_link_all);
            this.panel3.Controls.Add(this.check_link_video);
            this.panel3.Controls.Add(this.check_link_music);
            this.panel3.Controls.Add(this.btn_link_reset);
            this.panel3.Controls.Add(this.btn_link_save);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new System.Drawing.Point(14, 455);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(414, 34);
            this.panel3.TabIndex = 6;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // check_link_all
            // 
            this.check_link_all.Dock = System.Windows.Forms.DockStyle.Right;
            this.check_link_all.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_link_all.Location = new System.Drawing.Point(95, 0);
            this.check_link_all.Margin = new System.Windows.Forms.Padding(0);
            this.check_link_all.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_link_all.Name = "check_link_all";
            this.check_link_all.Size = new System.Drawing.Size(66, 34);
            this.check_link_all.TabIndex = 1;
            this.check_link_all.Text = "全选";
            this.check_link_all.UseVisualStyleBackColor = true;
            // 
            // check_link_video
            // 
            this.check_link_video.Dock = System.Windows.Forms.DockStyle.Right;
            this.check_link_video.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_link_video.Location = new System.Drawing.Point(161, 0);
            this.check_link_video.Margin = new System.Windows.Forms.Padding(0);
            this.check_link_video.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_link_video.Name = "check_link_video";
            this.check_link_video.Size = new System.Drawing.Size(66, 34);
            this.check_link_video.TabIndex = 2;
            this.check_link_video.Text = "视频";
            this.check_link_video.UseVisualStyleBackColor = true;
            // 
            // check_link_music
            // 
            this.check_link_music.Dock = System.Windows.Forms.DockStyle.Right;
            this.check_link_music.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_link_music.Location = new System.Drawing.Point(227, 0);
            this.check_link_music.Margin = new System.Windows.Forms.Padding(0);
            this.check_link_music.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_link_music.Name = "check_link_music";
            this.check_link_music.Size = new System.Drawing.Size(66, 34);
            this.check_link_music.TabIndex = 3;
            this.check_link_music.Text = "音频";
            this.check_link_music.UseVisualStyleBackColor = true;
            // 
            // btn_link_reset
            // 
            this.btn_link_reset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_link_reset.FlatAppearance.BorderSize = 0;
            this.btn_link_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_link_reset.Location = new System.Drawing.Point(293, 0);
            this.btn_link_reset.Name = "btn_link_reset";
            this.btn_link_reset.Size = new System.Drawing.Size(49, 34);
            this.btn_link_reset.TabIndex = 4;
            this.btn_link_reset.Text = "重置";
            // 
            // btn_link_save
            // 
            this.btn_link_save.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.btn_link_save.BorderWidth = 8F;
            this.btn_link_save.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_link_save.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btn_link_save.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_link_save.Enabled = false;
            this.btn_link_save.ForeColor = System.Drawing.Color.White;
            this.btn_link_save.Location = new System.Drawing.Point(342, 0);
            this.btn_link_save.Name = "btn_link_save";
            this.btn_link_save.Radius = 16;
            this.btn_link_save.Size = new System.Drawing.Size(46, 34);
            this.btn_link_save.TabIndex = 5;
            this.btn_link_save.Text = "保存";
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(388, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 34);
            this.label11.TabIndex = 0;
            this.label11.Text = "▼";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Font = new System.Drawing.Font("微软雅黑",8F);
            this.panel4.Location = new System.Drawing.Point(14, 489);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(414, 0);
            this.panel4.TabIndex = 7;
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(0, 489);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 12);
            this.label12.TabIndex = 0;
            this.label12.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label13.Location = new System.Drawing.Point(44, 188);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(197, 34);
            this.label13.TabIndex = 0;
            this.label13.Text = "开启后部分功能无法使用 如3D和VR";
            this.label13.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // check_accelerate
            // 
            this.check_accelerate.AutoSize = true;
            this.check_accelerate.DownColor = System.Drawing.Color.DodgerBlue;
            this.check_accelerate.Location = new System.Drawing.Point(28, 158);
            this.check_accelerate.Margin = new System.Windows.Forms.Padding(0);
            this.check_accelerate.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_accelerate.Name = "check_accelerate";
            this.check_accelerate.Size = new System.Drawing.Size(85, 30);
            this.check_accelerate.TabIndex = 4;
            this.check_accelerate.Text = "硬件加速";
            this.check_accelerate.UseVisualStyleBackColor = true;
            // 
            // check_multiple
            // 
            this.check_multiple.AutoSize = true;
            this.check_multiple.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_multiple.Location = new System.Drawing.Point(28, 94);
            this.check_multiple.Margin = new System.Windows.Forms.Padding(0);
            this.check_multiple.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_multiple.Name = "check_multiple";
            this.check_multiple.Size = new System.Drawing.Size(85, 30);
            this.check_multiple.TabIndex = 2;
            this.check_multiple.Text = "多重运行";
            this.check_multiple.UseVisualStyleBackColor = true;
            // 
            // check_cache_greed
            // 
            this.check_cache_greed.AutoSize = true;
            this.check_cache_greed.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_cache_greed.Location = new System.Drawing.Point(243, 348);
            this.check_cache_greed.Margin = new System.Windows.Forms.Padding(0);
            this.check_cache_greed.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_cache_greed.Name = "check_cache_greed";
            this.check_cache_greed.Size = new System.Drawing.Size(85, 30);
            this.check_cache_greed.TabIndex = 7;
            this.check_cache_greed.Text = "贪婪下载";
            this.check_cache_greed.UseVisualStyleBackColor = true;
            // 
            // check_rememberLocation
            // 
            this.check_rememberLocation.AutoSize = true;
            this.check_rememberLocation.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_rememberLocation.Location = new System.Drawing.Point(243, 30);
            this.check_rememberLocation.Margin = new System.Windows.Forms.Padding(0);
            this.check_rememberLocation.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_rememberLocation.Name = "check_rememberLocation";
            this.check_rememberLocation.Size = new System.Drawing.Size(112, 30);
            this.check_rememberLocation.TabIndex = 1;
            this.check_rememberLocation.Text = "记忆播放位置";
            this.check_rememberLocation.UseVisualStyleBackColor = true;
            // 
            // check_cache_video
            // 
            this.check_cache_video.AutoSize = true;
            this.check_cache_video.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_cache_video.Location = new System.Drawing.Point(28, 348);
            this.check_cache_video.Margin = new System.Windows.Forms.Padding(0);
            this.check_cache_video.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_cache_video.Name = "check_cache_video";
            this.check_cache_video.Size = new System.Drawing.Size(85, 30);
            this.check_cache_video.TabIndex = 6;
            this.check_cache_video.Text = "视频缓存";
            this.check_cache_video.UseVisualStyleBackColor = true;
            // 
            // check_minimizeToTray
            // 
            this.check_minimizeToTray.AutoSize = true;
            this.check_minimizeToTray.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_minimizeToTray.Location = new System.Drawing.Point(243, 94);
            this.check_minimizeToTray.Margin = new System.Windows.Forms.Padding(0);
            this.check_minimizeToTray.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_minimizeToTray.Name = "check_minimizeToTray";
            this.check_minimizeToTray.Size = new System.Drawing.Size(112, 30);
            this.check_minimizeToTray.TabIndex = 3;
            this.check_minimizeToTray.Text = "最小化到托盘";
            this.check_minimizeToTray.UseVisualStyleBackColor = true;
            // 
            // check_animation
            // 
            this.check_animation.AutoSize = true;
            this.check_animation.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_animation.Location = new System.Drawing.Point(28, 30);
            this.check_animation.Margin = new System.Windows.Forms.Padding(0);
            this.check_animation.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_animation.Name = "check_animation";
            this.check_animation.Size = new System.Drawing.Size(112, 30);
            this.check_animation.TabIndex = 0;
            this.check_animation.Text = "程序动画效果";
            this.check_animation.UseVisualStyleBackColor = true;
            // 
            // SettingP1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScroll = true;
            this.Controls.Add(this.label13);
            this.Controls.Add(this.check_accelerate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.check_multiple);
            this.Controls.Add(this.check_cache_greed);
            this.Controls.Add(this.check_rememberLocation);
            this.Controls.Add(this.check_cache_video);
            this.Controls.Add(this.check_minimizeToTray);
            this.Controls.Add(this.check_animation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("微软雅黑",10F);
            this.Name = "SettingP1";
            this.Size = new System.Drawing.Size(460, 339);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private TSkin.TCheckBox check_multiple;
        private TSkin.TCheckBox check_rememberLocation;
        private TSkin.TCheckBox check_minimizeToTray;
        private TSkin.TCheckBox check_animation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private TSkin.TRadioButton tRadioButton1;
        private TSkin.TRadioButton tRadioButton2;
        private TSkin.TRadioButton tRadioButton3;
        private System.Windows.Forms.Label label3;
        private TSkin.TCheckBox check_cache_video;
        private TSkin.TCheckBox check_cache_greed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel3;
        private TSkin.TCheckBox check_link_all;
        private TSkin.TCheckBox check_link_video;
        private TSkin.TCheckBox check_link_music;
        private System.Windows.Forms.Button btn_link_reset;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private TSkin.TBut btn_link_save;
        private TSkin.TCheckBox check_accelerate;
        private System.Windows.Forms.Label label13;
    }
}
