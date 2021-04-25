namespace TPlayer.Frm
{
    partial class SettingP5
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
            this.label_no_title = new System.Windows.Forms.Label();
            this.label_ProductVersion = new System.Windows.Forms.Label();
            this.label_no_copyright = new System.Windows.Forms.Label();
            this.pic_logo = new System.Windows.Forms.PictureBox();
            this.pic_err = new System.Windows.Forms.PictureBox();
            this.label_NewVersion = new System.Windows.Forms.Label();
            this.label_updateInfo = new System.Windows.Forms.Label();
            this.label_no_pie_desc = new System.Windows.Forms.Label();
            this.check_pre = new TSkin.TCheckBox();
            this.btn_info = new TSkin.TBut();
            this.btn_feedback = new TSkin.TBut();
            this.btn_update = new TSkin.TBut();
            this.metroLoading = new TSkin.LoadingMetroHorizontal();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).BeginInit();
            this.pic_logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_err)).BeginInit();
            this.SuspendLayout();
            // 
            // label_no_title
            // 
            this.label_no_title.AutoSize = true;
            this.label_no_title.Location = new System.Drawing.Point(14, 0);
            this.label_no_title.Name = "label_no_title";
            this.label_no_title.Size = new System.Drawing.Size(65, 20);
            this.label_no_title.TabIndex = 0;
            this.label_no_title.Text = "关于我们";
            this.label_no_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_ProductVersion
            // 
            this.label_ProductVersion.AutoEllipsis = true;
            this.label_ProductVersion.BackColor = System.Drawing.Color.Transparent;
            this.label_ProductVersion.Font = new System.Drawing.Font("微软雅黑",14F);
            this.label_ProductVersion.Location = new System.Drawing.Point(14, 30);
            this.label_ProductVersion.Name = "label_ProductVersion";
            this.label_ProductVersion.Size = new System.Drawing.Size(380, 35);
            this.label_ProductVersion.TabIndex = 0;
            this.label_ProductVersion.Text = "TPlayer ";
            this.label_ProductVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_ProductVersion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_no_copyright
            // 
            this.label_no_copyright.AutoEllipsis = true;
            this.label_no_copyright.BackColor = System.Drawing.Color.Transparent;
            this.label_no_copyright.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_no_copyright.ForeColor = System.Drawing.Color.DimGray;
            this.label_no_copyright.Location = new System.Drawing.Point(0, 304);
            this.label_no_copyright.Name = "label_no_copyright";
            this.label_no_copyright.Size = new System.Drawing.Size(460, 35);
            this.label_no_copyright.TabIndex = 0;
            this.label_no_copyright.Text = "©2020 Tom";
            this.label_no_copyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_no_copyright.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // pic_logo
            // 
            this.pic_logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_logo.Controls.Add(this.pic_err);
            this.pic_logo.Image = global::TPlayer.Properties.Resources.Logo;
            this.pic_logo.Location = new System.Drawing.Point(19, 68);
            this.pic_logo.Name = "pic_logo";
            this.pic_logo.Size = new System.Drawing.Size(60, 60);
            this.pic_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_logo.TabIndex = 1;
            this.pic_logo.TabStop = false;
            this.pic_logo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // pic_err
            // 
            this.pic_err.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_err.Image = global::TPlayer.Properties.Resources.icon_warn;
            this.pic_err.Location = new System.Drawing.Point(30, 30);
            this.pic_err.Name = "pic_err";
            this.pic_err.Size = new System.Drawing.Size(30, 30);
            this.pic_err.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_err.TabIndex = 2;
            this.pic_err.TabStop = false;
            this.pic_err.Visible = false;
            // 
            // label_NewVersion
            // 
            this.label_NewVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_NewVersion.BackColor = System.Drawing.Color.Transparent;
            this.label_NewVersion.Font = new System.Drawing.Font("微软雅黑",14F);
            this.label_NewVersion.Location = new System.Drawing.Point(85, 74);
            this.label_NewVersion.Name = "label_NewVersion";
            this.label_NewVersion.Size = new System.Drawing.Size(225, 25);
            this.label_NewVersion.TabIndex = 0;
            this.label_NewVersion.Text = "您使用的是最新版本";
            this.label_NewVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_NewVersion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_updateInfo
            // 
            this.label_updateInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_updateInfo.BackColor = System.Drawing.Color.Transparent;
            this.label_updateInfo.Font = new System.Drawing.Font("微软雅黑",9F);
            this.label_updateInfo.ForeColor = System.Drawing.Color.DimGray;
            this.label_updateInfo.Location = new System.Drawing.Point(87, 102);
            this.label_updateInfo.Name = "label_updateInfo";
            this.label_updateInfo.Size = new System.Drawing.Size(330, 20);
            this.label_updateInfo.TabIndex = 0;
            this.label_updateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_updateInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_no_pie_desc
            // 
            this.label_no_pie_desc.AutoEllipsis = true;
            this.label_no_pie_desc.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label_no_pie_desc.Location = new System.Drawing.Point(34, 281);
            this.label_no_pie_desc.Name = "label_no_pie_desc";
            this.label_no_pie_desc.Size = new System.Drawing.Size(395, 23);
            this.label_no_pie_desc.TabIndex = 0;
            this.label_no_pie_desc.Text = "体验即将推出的TPlayer，它具有更高质量的更新和某些关键功能";
            this.label_no_pie_desc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // check_pre
            // 
            this.check_pre.AutoSize = true;
            this.check_pre.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_pre.Location = new System.Drawing.Point(19, 251);
            this.check_pre.Margin = new System.Windows.Forms.Padding(0);
            this.check_pre.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_pre.Name = "check_pre";
            this.check_pre.Size = new System.Drawing.Size(154, 30);
            this.check_pre.TabIndex = 1;
            this.check_pre.Text = "加入预览版体验计划";
            this.check_pre.UseVisualStyleBackColor = true;
            // 
            // btn_info
            // 
            this.btn_info.BackColorActive = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(106)))), ((int)(((byte)(10)))));
            this.btn_info.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(106)))), ((int)(((byte)(10)))));
            this.btn_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(134)))), ((int)(((byte)(58)))));
            this.btn_info.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(140)))), ((int)(((byte)(62)))));
            this.btn_info.ForeColor = System.Drawing.Color.White;
            this.btn_info.ForeColorActive = System.Drawing.Color.White;
            this.btn_info.Location = new System.Drawing.Point(316, 68);
            this.btn_info.Name = "btn_info";
            this.btn_info.Radius = 10;
            this.btn_info.Size = new System.Drawing.Size(65, 26);
            this.btn_info.TabIndex = 0;
            this.btn_info.Text = "";
            this.btn_info.Visible = false;
            this.btn_info.Click += new System.EventHandler(this.btn_info_Click);
            // 
            // btn_feedback
            // 
            this.btn_feedback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_feedback.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_feedback.Location = new System.Drawing.Point(19, 203);
            this.btn_feedback.Name = "btn_feedback";
            this.btn_feedback.Size = new System.Drawing.Size(114, 30);
            this.btn_feedback.TabIndex = 0;
            this.btn_feedback.Text = "意见反馈";
            this.btn_feedback.Click += new System.EventHandler(this.btn_feedback_Click);
            // 
            // btn_update
            // 
            this.btn_update.BackColorActive = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(208)))), ((int)(((byte)(88)))));
            this.btn_update.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btn_update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_update.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_update.ForeColorActive = System.Drawing.Color.White;
            this.btn_update.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_update.IsActive = true;
            this.btn_update.Location = new System.Drawing.Point(32, 144);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(114, 30);
            this.btn_update.TabIndex = 0;
            this.btn_update.Text = "检查更新";
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // metroLoading
            // 
            this.metroLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLoading.BackColor = System.Drawing.Color.Transparent;
            this.metroLoading.EndStop = false;
            this.metroLoading.Location = new System.Drawing.Point(87, 105);
            this.metroLoading.Name = "metroLoading";
            this.metroLoading.Size = new System.Drawing.Size(360, 6);
            this.metroLoading.TabIndex = 0;
            this.metroLoading.Visible = false;
            this.metroLoading.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // SettingP5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.Controls.Add(this.label_no_pie_desc);
            this.Controls.Add(this.check_pre);
            this.Controls.Add(this.btn_info);
            this.Controls.Add(this.btn_feedback);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.metroLoading);
            this.Controls.Add(this.label_updateInfo);
            this.Controls.Add(this.label_NewVersion);
            this.Controls.Add(this.pic_logo);
            this.Controls.Add(this.label_no_copyright);
            this.Controls.Add(this.label_ProductVersion);
            this.Controls.Add(this.label_no_title);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑",10F);
            this.Name = "SettingP5";
            this.Size = new System.Drawing.Size(460, 339);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).EndInit();
            this.pic_logo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_no_title;
        private System.Windows.Forms.Label label_ProductVersion;
        private System.Windows.Forms.Label label_no_copyright;
        private TSkin.TBut btn_update;
        private TSkin.LoadingMetroHorizontal metroLoading;
        private System.Windows.Forms.PictureBox pic_logo;
        private System.Windows.Forms.Label label_NewVersion;
        private System.Windows.Forms.Label label_updateInfo;
        private TSkin.TBut btn_info;
        private System.Windows.Forms.Label label_no_pie_desc;
        private TSkin.TCheckBox check_pre;
        private TSkin.TBut btn_feedback;
        private System.Windows.Forms.PictureBox pic_err;
    }
}
