namespace TPlayer.Frm
{
    partial class Setting
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.b_5 = new TSkin.TBut();
            this.b_4 = new TSkin.TBut();
            this.b_3 = new TSkin.TBut();
            this.b_2 = new TSkin.TBut();
            this.b_6 = new TSkin.TBut();
            this.btn_resume = new TSkin.TBut();
            this.b_7 = new TSkin.TBut();
            this.b_1 = new TSkin.TBut();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btn_refresh = new TSkin.TBut();
            this.btn_min = new TSkin.TBut();
            this.btn_close = new TSkin.TBut();
            this.panel3 = new System.Windows.Forms.Panel();
            this.shadowLine1 = new TSkin.ShadowLine();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pictureBox4.SuspendLayout();
            this.shadowLine1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel1.Controls.Add(this.b_5);
            this.panel1.Controls.Add(this.b_4);
            this.panel1.Controls.Add(this.b_3);
            this.panel1.Controls.Add(this.b_2);
            this.panel1.Controls.Add(this.b_6);
            this.panel1.Controls.Add(this.btn_resume);
            this.panel1.Controls.Add(this.b_7);
            this.panel1.Controls.Add(this.b_1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 384);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // b_5
            // 
            this.b_5.BackColor = System.Drawing.Color.Transparent;
            this.b_5.BackColor2 = System.Drawing.Color.Transparent;
            this.b_5.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b_5.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(230)))));
            this.b_5.Dock = System.Windows.Forms.DockStyle.Top;
            this.b_5.ForeColorActive = System.Drawing.Color.White;
            this.b_5.ImageActive = global::TPlayer.Properties.Resources.btn_min;
            this.b_5.ImageMargin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.b_5.ImageSize = new System.Drawing.Size(24, 24);
            this.b_5.Location = new System.Drawing.Point(0, 283);
            this.b_5.Name = "b_5";
            this.b_5.Size = new System.Drawing.Size(154, 40);
            this.b_5.TabIndex = 6;
            this.b_5.Tag = "5";
            this.b_5.Text = "关于";
            this.b_5.Click += new System.EventHandler(this.menu_type);
            // 
            // b_4
            // 
            this.b_4.BackColor = System.Drawing.Color.Transparent;
            this.b_4.BackColor2 = System.Drawing.Color.Transparent;
            this.b_4.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b_4.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(230)))));
            this.b_4.Dock = System.Windows.Forms.DockStyle.Top;
            this.b_4.ForeColorActive = System.Drawing.Color.White;
            this.b_4.ImageActive = global::TPlayer.Properties.Resources.btn_min;
            this.b_4.ImageMargin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.b_4.ImageSize = new System.Drawing.Size(24, 24);
            this.b_4.Location = new System.Drawing.Point(0, 243);
            this.b_4.Name = "b_4";
            this.b_4.Size = new System.Drawing.Size(154, 40);
            this.b_4.TabIndex = 5;
            this.b_4.Tag = "4";
            this.b_4.Text = "隐私";
            this.b_4.Click += new System.EventHandler(this.menu_type);
            // 
            // b_3
            // 
            this.b_3.BackColor = System.Drawing.Color.Transparent;
            this.b_3.BackColor2 = System.Drawing.Color.Transparent;
            this.b_3.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b_3.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(230)))));
            this.b_3.Dock = System.Windows.Forms.DockStyle.Top;
            this.b_3.ForeColorActive = System.Drawing.Color.White;
            this.b_3.ImageActive = global::TPlayer.Properties.Resources.btn_min;
            this.b_3.ImageMargin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.b_3.ImageSize = new System.Drawing.Size(24, 24);
            this.b_3.Location = new System.Drawing.Point(0, 203);
            this.b_3.Name = "b_3";
            this.b_3.Size = new System.Drawing.Size(154, 40);
            this.b_3.TabIndex = 4;
            this.b_3.Tag = "3";
            this.b_3.Text = "按键";
            this.b_3.Click += new System.EventHandler(this.menu_type);
            // 
            // b_2
            // 
            this.b_2.BackColor = System.Drawing.Color.Transparent;
            this.b_2.BackColor2 = System.Drawing.Color.Transparent;
            this.b_2.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b_2.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(230)))));
            this.b_2.Dock = System.Windows.Forms.DockStyle.Top;
            this.b_2.ForeColorActive = System.Drawing.Color.White;
            this.b_2.ImageActive = global::TPlayer.Properties.Resources.btn_min;
            this.b_2.ImageMargin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.b_2.ImageSize = new System.Drawing.Size(24, 24);
            this.b_2.Location = new System.Drawing.Point(0, 163);
            this.b_2.Name = "b_2";
            this.b_2.Size = new System.Drawing.Size(154, 40);
            this.b_2.TabIndex = 3;
            this.b_2.Tag = "2";
            this.b_2.Text = "外观";
            this.b_2.Click += new System.EventHandler(this.menu_type);
            // 
            // b_6
            // 
            this.b_6.BackColor = System.Drawing.Color.Transparent;
            this.b_6.BackColor2 = System.Drawing.Color.Transparent;
            this.b_6.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b_6.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(230)))));
            this.b_6.Dock = System.Windows.Forms.DockStyle.Top;
            this.b_6.ForeColorActive = System.Drawing.Color.White;
            this.b_6.ImageActive = global::TPlayer.Properties.Resources.btn_min;
            this.b_6.ImageMargin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.b_6.ImageSize = new System.Drawing.Size(24, 24);
            this.b_6.Location = new System.Drawing.Point(0, 123);
            this.b_6.Name = "b_6";
            this.b_6.Size = new System.Drawing.Size(154, 40);
            this.b_6.TabIndex = 2;
            this.b_6.Tag = "6";
            this.b_6.Text = "高级";
            this.b_6.Click += new System.EventHandler(this.menu_type);
            // 
            // btn_resume
            // 
            this.btn_resume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_resume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(253)))));
            this.btn_resume.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(253)))));
            this.btn_resume.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btn_resume.Location = new System.Drawing.Point(16, 342);
            this.btn_resume.Name = "btn_resume";
            this.btn_resume.Radius = 4;
            this.btn_resume.Size = new System.Drawing.Size(122, 30);
            this.btn_resume.TabIndex = 6;
            this.btn_resume.Text = "恢复默认设置";
            this.btn_resume.Click += new System.EventHandler(this.btn_resume_Click);
            // 
            // b_7
            // 
            this.b_7.BackColor = System.Drawing.Color.Transparent;
            this.b_7.BackColor2 = System.Drawing.Color.Transparent;
            this.b_7.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b_7.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(230)))));
            this.b_7.Dock = System.Windows.Forms.DockStyle.Top;
            this.b_7.ForeColorActive = System.Drawing.Color.White;
            this.b_7.ImageActive = global::TPlayer.Properties.Resources.btn_min;
            this.b_7.ImageMargin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.b_7.ImageSize = new System.Drawing.Size(24, 24);
            this.b_7.Location = new System.Drawing.Point(0, 83);
            this.b_7.Name = "b_7";
            this.b_7.Size = new System.Drawing.Size(154, 40);
            this.b_7.TabIndex = 1;
            this.b_7.Tag = "7";
            this.b_7.Text = "下载";
            this.b_7.Click += new System.EventHandler(this.menu_type);
            // 
            // b_1
            // 
            this.b_1.BackColor = System.Drawing.Color.Transparent;
            this.b_1.BackColor2 = System.Drawing.Color.Transparent;
            this.b_1.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b_1.BackColorActive2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(230)))));
            this.b_1.Dock = System.Windows.Forms.DockStyle.Top;
            this.b_1.ForeColorActive = System.Drawing.Color.White;
            this.b_1.ImageActive = global::TPlayer.Properties.Resources.btn_min;
            this.b_1.ImageMargin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.b_1.ImageSize = new System.Drawing.Size(24, 24);
            this.b_1.IsActive = true;
            this.b_1.Location = new System.Drawing.Point(0, 43);
            this.b_1.Name = "b_1";
            this.b_1.Size = new System.Drawing.Size(154, 40);
            this.b_1.TabIndex = 0;
            this.b_1.Tag = "1";
            this.b_1.Text = "常规";
            this.b_1.Click += new System.EventHandler(this.menu_type);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 43);
            this.panel2.TabIndex = 0;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "设置";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Controls.Add(this.btn_refresh);
            this.pictureBox4.Controls.Add(this.btn_min);
            this.pictureBox4.Controls.Add(this.btn_close);
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox4.Location = new System.Drawing.Point(6, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(480, 40);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_refresh.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_refresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_refresh.Enabled = false;
            this.btn_refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_refresh.ImageSize = new System.Drawing.Size(26, 26);
            this.btn_refresh.Location = new System.Drawing.Point(360, 0);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(40, 40);
            this.btn_refresh.TabIndex = 4;
            this.btn_refresh.Text = "";
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_min
            // 
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_min.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_min.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_min.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_min.ImageSize = new System.Drawing.Size(26, 26);
            this.btn_min.Location = new System.Drawing.Point(400, 0);
            this.btn_min.Name = "btn_min";
            this.btn_min.Size = new System.Drawing.Size(40, 40);
            this.btn_min.TabIndex = 3;
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
            this.btn_close.Location = new System.Drawing.Point(440, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(40, 40);
            this.btn_close.TabIndex = 5;
            this.btn_close.TabStop = false;
            this.btn_close.Text = "";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(6, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(480, 344);
            this.panel3.TabIndex = 3;
            // 
            // shadowLine1
            // 
            this.shadowLine1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.shadowLine1.Controls.Add(this.panel3);
            this.shadowLine1.Controls.Add(this.pictureBox4);
            this.shadowLine1.Controls.Add(this.pictureBox1);
            this.shadowLine1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadowLine1.Location = new System.Drawing.Point(154, 0);
            this.shadowLine1.Name = "shadowLine1";
            this.shadowLine1.Size = new System.Drawing.Size(486, 384);
            this.shadowLine1.TabIndex = 1;
            this.shadowLine1.Type = TSkin.ShadowLine.ShadowType.LEFT;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(6, 384);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // Setting
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Borders = new System.Windows.Forms.Padding(0);
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(640, 384);
            this.Controls.Add(this.shadowLine1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(620, 380);
            this.Name = "Setting";
            this.Opacity = 0.98D;
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pictureBox4.ResumeLayout(false);
            this.shadowLine1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private TSkin.TBut btn_resume;
        private System.Windows.Forms.PictureBox pictureBox4;
        private TSkin.TBut btn_min;
        private TSkin.TBut btn_close;
        private System.Windows.Forms.Panel panel3;
        private TSkin.TBut b_1;
        private TSkin.TBut b_6;
        private TSkin.TBut b_2;
        private TSkin.TBut b_3;
        private TSkin.TBut b_4;
        private TSkin.TBut b_5;
        private TSkin.TBut b_7;
        private TSkin.TBut btn_refresh;
        private TSkin.ShadowLine shadowLine1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}