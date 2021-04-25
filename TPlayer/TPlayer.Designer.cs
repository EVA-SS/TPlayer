namespace TPlayer
{
    partial class TPlayer
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
            this.tProgress = new TSkin.TProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_open_file = new TSkin.TBut();
            this.btn_open_web = new TSkin.TBut();
            this.panel1 = new System.Windows.Forms.PictureBox();
            this.backImage = new TSkin.BackImagePanel();
            this.Menu = new TSkin.TMenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开本地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开网络ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.播放时最前ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.总是最前ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消最前ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画面模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视频适应窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.窗口适应视频ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.原始比例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.铺满窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.音频模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.音轨列表无ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AudioChannel_0 = new System.Windows.Forms.ToolStripMenuItem();
            this.AudioChannel_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.AudioChannel_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.AudioChannel_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.输出设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字幕模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择字幕无ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示字幕ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在线字幕ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.投射设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存截图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gif图截取ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视频转码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视频属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip = new TSkin.TMenuStrip();
            this.ToolItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyDown = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuDown = new TSkin.TMenuStrip();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.backImage.SuspendLayout();
            this.Menu.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.menuDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // tProgress
            // 
            this.tProgress.ContainerControl = this;
            this.tProgress.Location = new System.Drawing.Point(-100, -100);
            this.tProgress.Name = "tProgress";
            this.tProgress.Size = new System.Drawing.Size(0, 0);
            this.tProgress.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::TPlayer.Properties.Resources.icon_play;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmFullMove);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label2.Location = new System.Drawing.Point(70, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 34);
            this.label2.TabIndex = 0;
            this.label2.Text = "万能播放器";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmFullMove);
            // 
            // btn_open_file
            // 
            this.btn_open_file.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(94)))), ((int)(((byte)(184)))));
            this.btn_open_file.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(75)))), ((int)(((byte)(164)))));
            this.btn_open_file.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(47)))), ((int)(((byte)(137)))));
            this.btn_open_file.BorderWidth = 1F;
            this.btn_open_file.IsShadow = true;
            this.btn_open_file.Location = new System.Drawing.Point(66, 49);
            this.btn_open_file.Name = "btn_open_file";
            this.btn_open_file.Radius = 26;
            this.btn_open_file.Size = new System.Drawing.Size(126, 46);
            this.btn_open_file.TabIndex = 0;
            this.btn_open_file.Text = "打开文件";
            this.btn_open_file.Click += new System.EventHandler(this.btn_open_file_Click);
            // 
            // btn_open_web
            // 
            this.btn_open_web.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btn_open_web.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.btn_open_web.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(77)))), ((int)(((byte)(141)))));
            this.btn_open_web.BorderWidth = 1F;
            this.btn_open_web.IsShadow = true;
            this.btn_open_web.Location = new System.Drawing.Point(192, 49);
            this.btn_open_web.Name = "btn_open_web";
            this.btn_open_web.Radius = 26;
            this.btn_open_web.Size = new System.Drawing.Size(126, 46);
            this.btn_open_web.TabIndex = 1;
            this.btn_open_web.Text = "打开URL";
            this.btn_open_web.Click += new System.EventHandler(this.btn_open_web_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btn_open_web);
            this.panel1.Controls.Add(this.btn_open_file);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(341, 275);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 90);
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = false;
            this.panel1.Visible = false;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmFullMove);
            // 
            // backImage
            // 
            this.backImage.BlurColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.backImage.ContextMenuStrip = this.Menu;
            this.backImage.Controls.Add(this.panel1);
            this.backImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backImage.ImageSize = 200;
            this.backImage.Location = new System.Drawing.Point(0, 0);
            this.backImage.Name = "backImage";
            this.backImage.Size = new System.Drawing.Size(1000, 640);
            this.backImage.TabIndex = 0;
            this.backImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmFullMove);
            // 
            // Menu
            // 
            this.Menu.BackColor = System.Drawing.Color.White;
            this.Menu.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.画面模式ToolStripMenuItem,
            this.音频模式ToolStripMenuItem,
            this.字幕模式ToolStripMenuItem,
            this.投射设备ToolStripMenuItem,
            this.保存截图ToolStripMenuItem,
            this.gif图截取ToolStripMenuItem,
            this.视频转码ToolStripMenuItem,
            this.视频属性ToolStripMenuItem});
            this.Menu.Name = "menuStrip";
            this.Menu.ShowImageMargin = false;
            this.Menu.Size = new System.Drawing.Size(119, 356);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开本地ToolStripMenuItem,
            this.打开网络ToolStripMenuItem});
            this.toolStripMenuItem1.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(118, 24);
            this.toolStripMenuItem1.Text = "打开";
            // 
            // 打开本地ToolStripMenuItem
            // 
            this.打开本地ToolStripMenuItem.Name = "打开本地ToolStripMenuItem";
            this.打开本地ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.打开本地ToolStripMenuItem.Text = "打开本地";
            this.打开本地ToolStripMenuItem.Click += new System.EventHandler(this.btn_open_file_Click);
            // 
            // 打开网络ToolStripMenuItem
            // 
            this.打开网络ToolStripMenuItem.Name = "打开网络ToolStripMenuItem";
            this.打开网络ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.打开网络ToolStripMenuItem.Text = "打开网络";
            this.打开网络ToolStripMenuItem.Click += new System.EventHandler(this.btn_open_web_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(118, 24);
            this.toolStripMenuItem2.Text = "全屏";
            this.toolStripMenuItem2.Visible = false;
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.播放时最前ToolStripMenuItem,
            this.总是最前ToolStripMenuItem,
            this.取消最前ToolStripMenuItem});
            this.toolStripMenuItem3.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(118, 24);
            this.toolStripMenuItem3.Text = "窗口最前";
            // 
            // 播放时最前ToolStripMenuItem
            // 
            this.播放时最前ToolStripMenuItem.Name = "播放时最前ToolStripMenuItem";
            this.播放时最前ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.播放时最前ToolStripMenuItem.Text = "播放时最前";
            // 
            // 总是最前ToolStripMenuItem
            // 
            this.总是最前ToolStripMenuItem.Name = "总是最前ToolStripMenuItem";
            this.总是最前ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.总是最前ToolStripMenuItem.Text = "总是最前";
            // 
            // 取消最前ToolStripMenuItem
            // 
            this.取消最前ToolStripMenuItem.Name = "取消最前ToolStripMenuItem";
            this.取消最前ToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.取消最前ToolStripMenuItem.Text = "取消最前";
            // 
            // 画面模式ToolStripMenuItem
            // 
            this.画面模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.视频适应窗口ToolStripMenuItem,
            this.窗口适应视频ToolStripMenuItem,
            this.toolStripSeparator1,
            this.原始比例ToolStripMenuItem,
            this.铺满窗口ToolStripMenuItem});
            this.画面模式ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.画面模式ToolStripMenuItem.Name = "画面模式ToolStripMenuItem";
            this.画面模式ToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.画面模式ToolStripMenuItem.Text = "画面模式";
            this.画面模式ToolStripMenuItem.Visible = false;
            // 
            // 视频适应窗口ToolStripMenuItem
            // 
            this.视频适应窗口ToolStripMenuItem.Name = "视频适应窗口ToolStripMenuItem";
            this.视频适应窗口ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.视频适应窗口ToolStripMenuItem.Text = "窗口适应视频";
            this.视频适应窗口ToolStripMenuItem.Click += new System.EventHandler(this.窗口适应视频ToolStripMenuItem_Click);
            // 
            // 窗口适应视频ToolStripMenuItem
            // 
            this.窗口适应视频ToolStripMenuItem.Name = "窗口适应视频ToolStripMenuItem";
            this.窗口适应视频ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.窗口适应视频ToolStripMenuItem.Text = "使用视频大小";
            this.窗口适应视频ToolStripMenuItem.Click += new System.EventHandler(this.使用视频大小ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // 原始比例ToolStripMenuItem
            // 
            this.原始比例ToolStripMenuItem.Name = "原始比例ToolStripMenuItem";
            this.原始比例ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.原始比例ToolStripMenuItem.Text = "原始比例";
            // 
            // 铺满窗口ToolStripMenuItem
            // 
            this.铺满窗口ToolStripMenuItem.Name = "铺满窗口ToolStripMenuItem";
            this.铺满窗口ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.铺满窗口ToolStripMenuItem.Text = "铺满窗口";
            // 
            // 音频模式ToolStripMenuItem
            // 
            this.音频模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.音轨列表无ToolStripMenuItem,
            this.AudioChannel_0,
            this.AudioChannel_1,
            this.AudioChannel_2,
            this.AudioChannel_3,
            this.输出设备ToolStripMenuItem});
            this.音频模式ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.音频模式ToolStripMenuItem.Name = "音频模式ToolStripMenuItem";
            this.音频模式ToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.音频模式ToolStripMenuItem.Text = "音频模式";
            // 
            // 音轨列表无ToolStripMenuItem
            // 
            this.音轨列表无ToolStripMenuItem.Name = "音轨列表无ToolStripMenuItem";
            this.音轨列表无ToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.音轨列表无ToolStripMenuItem.Text = "音轨列表(无)";
            this.音轨列表无ToolStripMenuItem.Visible = false;
            // 
            // AudioChannel_0
            // 
            this.AudioChannel_0.Name = "AudioChannel_0";
            this.AudioChannel_0.Size = new System.Drawing.Size(163, 24);
            this.AudioChannel_0.Tag = "0";
            this.AudioChannel_0.Text = "立体声";
            this.AudioChannel_0.Click += new System.EventHandler(this.AudioChannelChange_Click);
            // 
            // AudioChannel_1
            // 
            this.AudioChannel_1.Name = "AudioChannel_1";
            this.AudioChannel_1.Size = new System.Drawing.Size(163, 24);
            this.AudioChannel_1.Tag = "1";
            this.AudioChannel_1.Text = "左声道";
            this.AudioChannel_1.Click += new System.EventHandler(this.AudioChannelChange_Click);
            // 
            // AudioChannel_2
            // 
            this.AudioChannel_2.Name = "AudioChannel_2";
            this.AudioChannel_2.Size = new System.Drawing.Size(163, 24);
            this.AudioChannel_2.Tag = "2";
            this.AudioChannel_2.Text = "右声道";
            this.AudioChannel_2.Click += new System.EventHandler(this.AudioChannelChange_Click);
            // 
            // AudioChannel_3
            // 
            this.AudioChannel_3.Name = "AudioChannel_3";
            this.AudioChannel_3.Size = new System.Drawing.Size(163, 24);
            this.AudioChannel_3.Tag = "3";
            this.AudioChannel_3.Text = "左右混合";
            this.AudioChannel_3.Click += new System.EventHandler(this.AudioChannelChange_Click);
            // 
            // 输出设备ToolStripMenuItem
            // 
            this.输出设备ToolStripMenuItem.Name = "输出设备ToolStripMenuItem";
            this.输出设备ToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.输出设备ToolStripMenuItem.Text = "输出设备";
            // 
            // 字幕模式ToolStripMenuItem
            // 
            this.字幕模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择字幕无ToolStripMenuItem,
            this.显示字幕ToolStripMenuItem,
            this.在线字幕ToolStripMenuItem});
            this.字幕模式ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.字幕模式ToolStripMenuItem.Name = "字幕模式ToolStripMenuItem";
            this.字幕模式ToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.字幕模式ToolStripMenuItem.Text = "字幕模式";
            // 
            // 选择字幕无ToolStripMenuItem
            // 
            this.选择字幕无ToolStripMenuItem.Name = "选择字幕无ToolStripMenuItem";
            this.选择字幕无ToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.选择字幕无ToolStripMenuItem.Text = "选择字幕(无)";
            // 
            // 显示字幕ToolStripMenuItem
            // 
            this.显示字幕ToolStripMenuItem.Name = "显示字幕ToolStripMenuItem";
            this.显示字幕ToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.显示字幕ToolStripMenuItem.Text = "显示字幕";
            this.显示字幕ToolStripMenuItem.Click += new System.EventHandler(this.显示字幕ToolStripMenuItem_Click);
            // 
            // 在线字幕ToolStripMenuItem
            // 
            this.在线字幕ToolStripMenuItem.Name = "在线字幕ToolStripMenuItem";
            this.在线字幕ToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.在线字幕ToolStripMenuItem.Text = "在线字幕";
            this.在线字幕ToolStripMenuItem.Click += new System.EventHandler(this.在线匹配ToolStripMenuItem_Click);
            // 
            // 投射设备ToolStripMenuItem
            // 
            this.投射设备ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.投射设备ToolStripMenuItem.Name = "投射设备ToolStripMenuItem";
            this.投射设备ToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.投射设备ToolStripMenuItem.Text = "投射设备";
            // 
            // 保存截图ToolStripMenuItem
            // 
            this.保存截图ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.保存截图ToolStripMenuItem.Name = "保存截图ToolStripMenuItem";
            this.保存截图ToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.保存截图ToolStripMenuItem.Text = "保存截图";
            this.保存截图ToolStripMenuItem.Visible = false;
            this.保存截图ToolStripMenuItem.Click += new System.EventHandler(this.保存截图ToolStripMenuItem_Click);
            // 
            // gif图截取ToolStripMenuItem
            // 
            this.gif图截取ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.gif图截取ToolStripMenuItem.Name = "gif图截取ToolStripMenuItem";
            this.gif图截取ToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.gif图截取ToolStripMenuItem.Text = "Gif图截取";
            this.gif图截取ToolStripMenuItem.Visible = false;
            this.gif图截取ToolStripMenuItem.Click += new System.EventHandler(this.gif图截取ToolStripMenuItem_Click);
            // 
            // 视频转码ToolStripMenuItem
            // 
            this.视频转码ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.视频转码ToolStripMenuItem.Name = "视频转码ToolStripMenuItem";
            this.视频转码ToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.视频转码ToolStripMenuItem.Text = "视频转码";
            // 
            // 视频属性ToolStripMenuItem
            // 
            this.视频属性ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.视频属性ToolStripMenuItem.Name = "视频属性ToolStripMenuItem";
            this.视频属性ToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.视频属性ToolStripMenuItem.Text = "视频属性";
            this.视频属性ToolStripMenuItem.Click += new System.EventHandler(this.视频属性ToolStripMenuItem_Click);
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.menuStrip;
            this.notify.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.notify.Text = "TPlayer";
            this.notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseDoubleClick);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolItem1,
            this.ToolItem2,
            this.ToolItem3});
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(101, 70);
            // 
            // ToolItem1
            // 
            this.ToolItem1.Name = "ToolItem1";
            this.ToolItem1.Size = new System.Drawing.Size(100, 22);
            this.ToolItem1.Text = "播放";
            this.ToolItem1.Click += new System.EventHandler(this.ToolItem1_Click);
            // 
            // ToolItem2
            // 
            this.ToolItem2.Name = "ToolItem2";
            this.ToolItem2.Size = new System.Drawing.Size(100, 22);
            this.ToolItem2.Text = "暂停";
            this.ToolItem2.Visible = false;
            this.ToolItem2.Click += new System.EventHandler(this.ToolItem2_Click);
            // 
            // ToolItem3
            // 
            this.ToolItem3.Name = "ToolItem3";
            this.ToolItem3.Size = new System.Drawing.Size(100, 22);
            this.ToolItem3.Text = "退出";
            this.ToolItem3.Click += new System.EventHandler(this.ToolItem3_Click);
            // 
            // notifyDown
            // 
            this.notifyDown.ContextMenuStrip = this.menuDown;
            this.notifyDown.Icon = global::TPlayer.Properties.Resources.TLogoDown;
            this.notifyDown.Text = "TPlayer 正在下载";
            this.notifyDown.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyDown_MouseDoubleClick);
            // 
            // menuDown
            // 
            this.menuDown.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuDown.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem6});
            this.menuDown.Name = "menuStrip";
            this.menuDown.Size = new System.Drawing.Size(137, 48);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem4.Text = "打开播放器";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem6.Text = "退出";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.ToolItem3_Click);
            // 
            // TPlayer
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Borders = new System.Windows.Forms.Padding(0);
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.backImage);
            this.Controls.Add(this.tProgress);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "TPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TPlayer";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmFullMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.backImage.ResumeLayout(false);
            this.Menu.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TSkin.TProgressBar tProgress;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label label2;
        private TSkin.TBut btn_open_web;
        private TSkin.TBut btn_open_file;
        private System.Windows.Forms.PictureBox panel1;
        public TSkin.BackImagePanel backImage;
        private System.Windows.Forms.NotifyIcon notify;
        private TSkin.TMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolItem2;
        private System.Windows.Forms.ToolStripMenuItem ToolItem3;
        private TSkin.TMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 打开本地ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开网络ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 播放时最前ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 总是最前ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消最前ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画面模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视频适应窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 窗口适应视频ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 原始比例ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 铺满窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 音频模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 音轨列表无ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AudioChannel_0;
        private System.Windows.Forms.ToolStripMenuItem AudioChannel_1;
        private System.Windows.Forms.ToolStripMenuItem AudioChannel_2;
        private System.Windows.Forms.ToolStripMenuItem AudioChannel_3;
        private System.Windows.Forms.ToolStripMenuItem 输出设备ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 字幕模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择字幕无ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示字幕ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在线字幕ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 投射设备ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存截图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gif图截取ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视频转码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视频属性ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyDown;
        private TSkin.TMenuStrip menuDown;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
    }
}