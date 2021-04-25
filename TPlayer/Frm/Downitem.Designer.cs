
namespace TPlayer.Frm
{
    partial class Downitem
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.size = new System.Windows.Forms.Label();
            this.link_retry = new System.Windows.Forms.LinkLabel();
            this.link_open = new System.Windows.Forms.LinkLabel();
            this.link_del = new System.Windows.Forms.LinkLabel();
            this.state = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.prog = new TSkin.LoadingMaterialHorizonta();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.size);
            this.panel5.Controls.Add(this.link_retry);
            this.panel5.Controls.Add(this.link_open);
            this.panel5.Controls.Add(this.link_del);
            this.panel5.Controls.Add(this.state);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(68, 42);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(330, 26);
            this.panel5.TabIndex = 6;
            // 
            // size
            // 
            this.size.AutoEllipsis = true;
            this.size.BackColor = System.Drawing.Color.Transparent;
            this.size.Dock = System.Windows.Forms.DockStyle.Fill;
            this.size.Location = new System.Drawing.Point(0, 0);
            this.size.Name = "size";
            this.size.Size = new System.Drawing.Size(100, 26);
            this.size.TabIndex = 0;
            this.size.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // link_retry
            // 
            this.link_retry.ActiveLinkColor = System.Drawing.Color.Black;
            this.link_retry.Dock = System.Windows.Forms.DockStyle.Right;
            this.link_retry.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.link_retry.LinkColor = System.Drawing.Color.Black;
            this.link_retry.Location = new System.Drawing.Point(100, 0);
            this.link_retry.Name = "link_retry";
            this.link_retry.Size = new System.Drawing.Size(34, 26);
            this.link_retry.TabIndex = 0;
            this.link_retry.TabStop = true;
            this.link_retry.Text = "重试";
            this.link_retry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.link_retry.Visible = false;
            this.link_retry.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(250)))));
            this.link_retry.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_retry_LinkClicked);
            // 
            // link_open
            // 
            this.link_open.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(250)))));
            this.link_open.Dock = System.Windows.Forms.DockStyle.Right;
            this.link_open.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.link_open.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(250)))));
            this.link_open.Location = new System.Drawing.Point(134, 0);
            this.link_open.Name = "link_open";
            this.link_open.Size = new System.Drawing.Size(80, 26);
            this.link_open.TabIndex = 2;
            this.link_open.TabStop = true;
            this.link_open.Text = "打开文件位置";
            this.link_open.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.link_open.Visible = false;
            this.link_open.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(250)))));
            this.link_open.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_open_LinkClicked);
            // 
            // link_del
            // 
            this.link_del.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.link_del.Dock = System.Windows.Forms.DockStyle.Right;
            this.link_del.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.link_del.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.link_del.Location = new System.Drawing.Point(214, 0);
            this.link_del.Name = "link_del";
            this.link_del.Size = new System.Drawing.Size(60, 26);
            this.link_del.TabIndex = 1;
            this.link_del.TabStop = true;
            this.link_del.Text = "删除任务";
            this.link_del.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.link_del.Visible = false;
            this.link_del.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.link_del.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_del_LinkClicked);
            // 
            // state
            // 
            this.state.AutoEllipsis = true;
            this.state.BackColor = System.Drawing.Color.Transparent;
            this.state.Dock = System.Windows.Forms.DockStyle.Right;
            this.state.ForeColor = System.Drawing.Color.DimGray;
            this.state.Location = new System.Drawing.Point(274, 0);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(56, 26);
            this.state.TabIndex = 0;
            this.state.Text = "准备中";
            this.state.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // time
            // 
            this.time.AutoEllipsis = true;
            this.time.BackColor = System.Drawing.Color.Transparent;
            this.time.Dock = System.Windows.Forms.DockStyle.Right;
            this.time.Font = new System.Drawing.Font("微软雅黑",8F);
            this.time.ForeColor = System.Drawing.Color.DimGray;
            this.time.Location = new System.Drawing.Point(270, 0);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(60, 38);
            this.time.TabIndex = 0;
            this.time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // title
            // 
            this.title.AutoEllipsis = true;
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.title.Font = new System.Drawing.Font("微软雅黑",11F);
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(270, 38);
            this.title.TabIndex = 0;
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.title.Click += new System.EventHandler(this.logo_Click);
            // 
            // logo
            // 
            this.logo.BackColor = System.Drawing.Color.Transparent;
            this.logo.Dock = System.Windows.Forms.DockStyle.Left;
            this.logo.Location = new System.Drawing.Point(0, 0);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(68, 68);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 1;
            this.logo.TabStop = false;
            this.logo.Click += new System.EventHandler(this.logo_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.title);
            this.panel1.Controls.Add(this.time);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(68, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 38);
            this.panel1.TabIndex = 0;
            // 
            // prog
            // 
            this.prog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.prog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prog.Location = new System.Drawing.Point(68, 38);
            this.prog.Name = "prog";
            this.prog.ProgColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(227)))), ((int)(((byte)(194)))));
            this.prog.Size = new System.Drawing.Size(330, 4);
            this.prog.TabIndex = 0;
            // 
            // Downitem
            // 
            this.Controls.Add(this.prog);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.logo);
            this.Name = "Downitem";
            this.Size = new System.Drawing.Size(398, 68);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label size;
        public System.Windows.Forms.Label state;
        public System.Windows.Forms.Label title;
        public System.Windows.Forms.PictureBox logo;
        public TSkin.LoadingMaterialHorizonta prog;
        public System.Windows.Forms.Label time;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.LinkLabel link_del;
        public System.Windows.Forms.LinkLabel link_open;
        public System.Windows.Forms.LinkLabel link_retry;
    }
}
