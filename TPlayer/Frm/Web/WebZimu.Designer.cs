namespace TPlayer.Frm
{
    partial class WebZimu
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.text_search = new TSkin.TextBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.pic_logo = new System.Windows.Forms.PictureBox();
            this.btn_close = new TSkin.TBut();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.frmSearch1 = new TPlayerFrm.FrmSearch();
            this.metroToolTip1 = new TSkin.MetroToolTip();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel13);
            this.panel1.Controls.Add(this.panel14);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 44);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.label1.Location = new System.Drawing.Point(94, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "在线字幕（zimuku.la）";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.text_search);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(471, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(124, 44);
            this.panel3.TabIndex = 7;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // text_search
            // 
            this.text_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.text_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.text_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_search.CueBannerText = "搜索字幕";
            this.text_search.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.text_search.ForeColor = System.Drawing.Color.Black;
            this.text_search.Location = new System.Drawing.Point(3, 11);
            this.text_search.Name = "text_search";
            this.text_search.ShowCueFocused = true;
            this.text_search.Size = new System.Drawing.Size(118, 22);
            this.text_search.TabIndex = 0;
            this.metroToolTip1.SetToolTip(this.text_search, "搜索字幕");
            this.text_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_search_KeyPress);
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.Transparent;
            this.panel13.Controls.Add(this.pictureBox2);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel13.Location = new System.Drawing.Point(86, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(8, 44);
            this.panel13.TabIndex = 0;
            this.panel13.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pictureBox2.Location = new System.Drawing.Point(0, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1, 24);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Transparent;
            this.panel14.Controls.Add(this.pic_logo);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(86, 44);
            this.panel14.TabIndex = 0;
            this.panel14.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // pic_logo
            // 
            this.pic_logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_logo.Image = global::TPlayer.Properties.Resources.zimuku_la;
            this.pic_logo.Location = new System.Drawing.Point(10, 6);
            this.pic_logo.Name = "pic_logo";
            this.pic_logo.Size = new System.Drawing.Size(64, 32);
            this.pic_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_logo.TabIndex = 0;
            this.pic_logo.TabStop = false;
            this.pic_logo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btn_close.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_close.ImageSize = new System.Drawing.Size(26,26);
            this.btn_close.Location = new System.Drawing.Point(595, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(44, 44);
            this.btn_close.TabIndex = 0;
            this.btn_close.TabStop = false;
            this.btn_close.Text = "";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 44);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(639, 285);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // frmSearch1
            // 
            this.frmSearch1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.frmSearch1.CueBannerText = "";
            this.frmSearch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmSearch1.Location = new System.Drawing.Point(0, 44);
            this.frmSearch1.Name = "frmSearch1";
            this.frmSearch1.Size = new System.Drawing.Size(639, 285);
            this.frmSearch1.TabIndex = 0;
            this.frmSearch1.OnSearch += new TPlayerFrm.FrmSearch.StrEventHandler(this.frmSearch1_OnSearch);
            this.frmSearch1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.BackColor = System.Drawing.Color.White;
            this.metroToolTip1.BorderColor = System.Drawing.Color.DodgerBlue;
            this.metroToolTip1.ForeColor = System.Drawing.Color.Black;
            this.metroToolTip1.OwnerDraw = true;
            this.metroToolTip1.ShowAlways = true;
            // 
            // WebZimu
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Borders = new System.Windows.Forms.Padding(0);
            this.ClientSize = new System.Drawing.Size(639, 329);
            this.Controls.Add(this.frmSearch1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "WebZimu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字幕在线";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label pictureBox2;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.PictureBox pic_logo;
        private TSkin.TBut btn_close;
        private System.Windows.Forms.Panel panel3;
        private TSkin.TextBox text_search;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private TSkin.MetroToolTip metroToolTip1;
        private TPlayerFrm.FrmSearch frmSearch1;
    }
}