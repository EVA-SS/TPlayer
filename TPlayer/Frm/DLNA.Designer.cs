namespace TPlayer
{
    partial class DLNA
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
            this.label1 = new System.Windows.Forms.Label();
            this.slideButton1 = new TSkin.SlideButton();
            this.btn_refresh = new TSkin.TBut();
            this.shadowLine1 = new TSkin.ShadowLine();
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroLoading = new TSkin.LoadingMetroHorizontal();
            this.panel_desk = new TSkin.TButDLNA();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btn_min = new TSkin.TBut();
            this.btn_close = new TSkin.TBut();
            this.shadowLine1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pictureBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "  投屏";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // slideButton1
            // 
            this.slideButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.slideButton1.IsOn = false;
            this.slideButton1.Location = new System.Drawing.Point(53, 7);
            this.slideButton1.Name = "slideButton1";
            this.slideButton1.Size = new System.Drawing.Size(32, 18);
            this.slideButton1.TabIndex = 0;
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_refresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_refresh.Enabled = false;
            this.btn_refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_refresh.ImageSize = new System.Drawing.Size(26, 26);
            this.btn_refresh.Location = new System.Drawing.Point(270, 0);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(40, 32);
            this.btn_refresh.TabIndex = 0;
            this.btn_refresh.Text = "";
            this.btn_refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // shadowLine1
            // 
            this.shadowLine1.Controls.Add(this.panel1);
            this.shadowLine1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadowLine1.Location = new System.Drawing.Point(0, 120);
            this.shadowLine1.Name = "shadowLine1";
            this.shadowLine1.Size = new System.Drawing.Size(390, 276);
            this.shadowLine1.TabIndex = 0;
            this.shadowLine1.Type = TSkin.ShadowLine.ShadowType.TOP;
            this.shadowLine1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 276);
            this.panel1.TabIndex = 0;
            // 
            // metroLoading
            // 
            this.metroLoading.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroLoading.Location = new System.Drawing.Point(0, 32);
            this.metroLoading.Name = "metroLoading";
            this.metroLoading.Size = new System.Drawing.Size(390, 8);
            this.metroLoading.TabIndex = 0;
            this.metroLoading.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // panel_desk
            // 
            this.panel_desk.ActiveColor = System.Drawing.Color.White;
            this.panel_desk.ActiveColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel_desk.DefaultColor = System.Drawing.Color.Transparent;
            this.panel_desk.DefaultColor2 = System.Drawing.Color.Transparent;
            this.panel_desk.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_desk.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.panel_desk.Image = global::TPlayer.Properties.Resources.icon_tv_win;
            this.panel_desk.Location = new System.Drawing.Point(0, 40);
            this.panel_desk.Name = "panel_desk";
            this.panel_desk.Size = new System.Drawing.Size(390, 80);
            this.panel_desk.TabIndex = 0;
            this.panel_desk.Text = "桌面投屏";
            this.panel_desk.Text2 = "将视频嵌入到桌面";
            this.panel_desk.Visible = false;
            this.panel_desk.Click += new System.EventHandler(this.PanelZM_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Controls.Add(this.slideButton1);
            this.pictureBox4.Controls.Add(this.btn_refresh);
            this.pictureBox4.Controls.Add(this.label1);
            this.pictureBox4.Controls.Add(this.btn_min);
            this.pictureBox4.Controls.Add(this.btn_close);
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox4.Location = new System.Drawing.Point(0, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(390, 32);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // btn_min
            // 
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_min.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_min.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_min.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_min.ImageSize = new System.Drawing.Size(26, 26);
            this.btn_min.Location = new System.Drawing.Point(310, 0);
            this.btn_min.Name = "btn_min";
            this.btn_min.Size = new System.Drawing.Size(40, 32);
            this.btn_min.TabIndex = 0;
            this.btn_min.TabStop = false;
            this.btn_min.Text = "";
            this.btn_min.Click += new System.EventHandler(this.btn_min_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_close.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_close.ImageSize = new System.Drawing.Size(26, 26);
            this.btn_close.Location = new System.Drawing.Point(350, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(40, 32);
            this.btn_close.TabIndex = 0;
            this.btn_close.TabStop = false;
            this.btn_close.Text = "";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // DLNA
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Borders = new System.Windows.Forms.Padding(0);
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(390, 396);
            this.Controls.Add(this.shadowLine1);
            this.Controls.Add(this.panel_desk);
            this.Controls.Add(this.metroLoading);
            this.Controls.Add(this.pictureBox4);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximumSize = new System.Drawing.Size(390, 1000);
            this.MinimumSize = new System.Drawing.Size(390, 200);
            this.Name = "DLNA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "投屏";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            this.shadowLine1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pictureBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private TSkin.SlideButton slideButton1;
        private TSkin.LoadingMetroHorizontal metroLoading;
        private TSkin.TBut btn_refresh;
        private TSkin.ShadowLine shadowLine1;
        private TSkin.TButDLNA panel_desk;
        private System.Windows.Forms.PictureBox pictureBox4;
        private TSkin.TBut btn_min;
        private TSkin.TBut btn_close;
        private System.Windows.Forms.Panel panel1;
    }
}