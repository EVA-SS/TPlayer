namespace TPlayer.Frm
{
    partial class WebVideo
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
            this.panel1 = new System.Windows.Forms.PictureBox();
            this.menuXList1 = new TPlayerList.MenuXList();
            this.panel3 = new System.Windows.Forms.Label();
            this.btn_min = new TSkin.TBut();
            this.btn_max = new TSkin.TBut();
            this.btn_close = new TSkin.TBut();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.text_search = new TSkin.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tMenuStrip1 = new TSkin.TMenuStrip();
            this.最大资源网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oK资源网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.速播资源站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.麻花资源ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最新资源网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._123资源网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.超快资源网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.卧龙资源网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高清资源网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webVideoList1 = new TPlayerList.WebVideoList();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.menuXList1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btn_min);
            this.panel1.Controls.Add(this.btn_max);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 50);
            this.panel1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = false;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MaxMove);
            // 
            // menuXList1
            // 
            this.menuXList1.BackColor = System.Drawing.Color.Transparent;
            this.menuXList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuXList1.Font = new System.Drawing.Font("微软雅黑",11F);
            this.menuXList1.Location = new System.Drawing.Point(60, 0);
            this.menuXList1.Name = "menuXList1";
            this.menuXList1.SelectItem = null;
            this.menuXList1.SelectItemIndex = -1;
            this.menuXList1.Size = new System.Drawing.Size(690, 50);
            this.menuXList1.TabIndex = 1;
            this.menuXList1.DownClick += new TPlayerList.MenuXList.DownEventHandler(this.menuXList1_DownClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(750, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(130, 50);
            this.panel3.TabIndex = 2;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MaxMove);
            // 
            // btn_min
            // 
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.BorderWidth = 0F;
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_min.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_min.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_min.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_min.ImageSize = new System.Drawing.Size(26,26);
            this.btn_min.Location = new System.Drawing.Point(880, 0);
            this.btn_min.MaxValue = 0D;
            this.btn_min.Name = "btn_min";
            this.btn_min.Size = new System.Drawing.Size(40, 50);
            this.btn_min.TabIndex = 3;
            this.btn_min.TabStop = false;
            this.btn_min.Text = "";
            this.btn_min.Value = 0D;
            this.btn_min.Click += new System.EventHandler(this.Min_Click);
            // 
            // btn_max
            // 
            this.btn_max.BackColor = System.Drawing.Color.Transparent;
            this.btn_max.BorderWidth = 0F;
            this.btn_max.BackColor = System.Drawing.Color.Transparent;
            this.btn_max.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_max.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_max.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_max.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_max.ImageSize = new System.Drawing.Size(26,26);
            this.btn_max.Location = new System.Drawing.Point(920, 0);
            this.btn_max.MaxValue = 0D;
            this.btn_max.Name = "btn_max";
            this.btn_max.Size = new System.Drawing.Size(40, 50);
            this.btn_max.TabIndex = 4;
            this.btn_max.TabStop = false;
            this.btn_max.Text = "";
            this.btn_max.Value = 0D;
            this.btn_max.Click += new System.EventHandler(this.Max_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BorderWidth = 0F;
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btn_close.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_close.ImageSize = new System.Drawing.Size(26,26);
            this.btn_close.Location = new System.Drawing.Point(960, 0);
            this.btn_close.MaxValue = 0D;
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(40, 50);
            this.btn_close.TabIndex = 5;
            this.btn_close.TabStop = false;
            this.btn_close.Text = "";
            this.btn_close.Value = 0D;
            this.btn_close.Click += new System.EventHandler(this.Close_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(60, 50);
            this.panel2.TabIndex = 0;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MaxMove);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(16, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(28, 28);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuClick);
            // 
            // text_search
            // 
            this.text_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.text_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_search.CueBannerText = "搜索...";
            this.text_search.Font = new System.Drawing.Font("微软雅黑",12F);
            this.text_search.ForeColor = System.Drawing.Color.Black;
            this.text_search.Location = new System.Drawing.Point(766, 14);
            this.text_search.Name = "text_search";
            this.text_search.ShowCueFocused = true;
            this.text_search.Size = new System.Drawing.Size(108, 22);
            this.text_search.TabIndex = 0;
            this.text_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_search_KeyPress);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tMenuStrip1
            // 
            this.tMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最大资源网ToolStripMenuItem,
            this.oK资源网ToolStripMenuItem,
            this.速播资源站ToolStripMenuItem,
            this.麻花资源ToolStripMenuItem,
            this.最新资源网ToolStripMenuItem,
            this._123资源网ToolStripMenuItem,
            this.超快资源网ToolStripMenuItem,
            this.卧龙资源网ToolStripMenuItem,
            this.高清资源网ToolStripMenuItem});
            this.tMenuStrip1.Name = "tMenuStrip1";
            this.tMenuStrip1.Size = new System.Drawing.Size(137, 202);
            // 
            // 最大资源网ToolStripMenuItem
            // 
            this.最大资源网ToolStripMenuItem.Name = "最大资源网ToolStripMenuItem";
            this.最大资源网ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.最大资源网ToolStripMenuItem.Text = "最大资源网";
            this.最大资源网ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // oK资源网ToolStripMenuItem
            // 
            this.oK资源网ToolStripMenuItem.Name = "oK资源网ToolStripMenuItem";
            this.oK资源网ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.oK资源网ToolStripMenuItem.Text = "OK资源网";
            this.oK资源网ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 速播资源站ToolStripMenuItem
            // 
            this.速播资源站ToolStripMenuItem.Name = "速播资源站ToolStripMenuItem";
            this.速播资源站ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.速播资源站ToolStripMenuItem.Text = "速播资源站";
            this.速播资源站ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 麻花资源ToolStripMenuItem
            // 
            this.麻花资源ToolStripMenuItem.Name = "麻花资源ToolStripMenuItem";
            this.麻花资源ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.麻花资源ToolStripMenuItem.Text = "麻花资源";
            this.麻花资源ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 最新资源网ToolStripMenuItem
            // 
            this.最新资源网ToolStripMenuItem.Name = "最新资源网ToolStripMenuItem";
            this.最新资源网ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.最新资源网ToolStripMenuItem.Text = "最新资源网";
            this.最新资源网ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // _123资源网ToolStripMenuItem
            // 
            this._123资源网ToolStripMenuItem.Name = "_123资源网ToolStripMenuItem";
            this._123资源网ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this._123资源网ToolStripMenuItem.Text = "123资源网";
            this._123资源网ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 超快资源网ToolStripMenuItem
            // 
            this.超快资源网ToolStripMenuItem.Name = "超快资源网ToolStripMenuItem";
            this.超快资源网ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.超快资源网ToolStripMenuItem.Text = "超快资源网";
            this.超快资源网ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 卧龙资源网ToolStripMenuItem
            // 
            this.卧龙资源网ToolStripMenuItem.Name = "卧龙资源网ToolStripMenuItem";
            this.卧龙资源网ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.卧龙资源网ToolStripMenuItem.Text = "卧龙资源网";
            this.卧龙资源网ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 高清资源网ToolStripMenuItem
            // 
            this.高清资源网ToolStripMenuItem.Name = "高清资源网ToolStripMenuItem";
            this.高清资源网ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.高清资源网ToolStripMenuItem.Text = "高清资源网";
            this.高清资源网ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // webVideoList1
            // 
            this.webVideoList1.DimHeight = 50;
            this.webVideoList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webVideoList1.Location = new System.Drawing.Point(0, 50);
            this.webVideoList1.Name = "webVideoList1";
            this.webVideoList1.SelectItem = null;
            this.webVideoList1.Size = new System.Drawing.Size(1000, 433);
            this.webVideoList1.TabIndex = 0;
            this.webVideoList1.DownClick += new TPlayerList.WebVideoList.DownEventHandler(this.webVideoList1_DownClick);
            // 
            // WebVideo
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1000, 483);
            this.Borders = new System.Windows.Forms.Padding(0);
            this.Controls.Add(this.text_search);
            this.Controls.Add(this.webVideoList1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "WebVideo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视频在线";
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private TPlayerList.MenuXList menuXList1;
        private System.Windows.Forms.Panel panel2;
        private TSkin.TBut btn_min;
        private TSkin.TBut btn_max;
        private TSkin.TBut btn_close;
        private System.Windows.Forms.Label panel3;
        private TSkin.TextBox text_search;
        private System.Windows.Forms.Timer timer1;
        private TPlayerList.WebVideoList webVideoList1;
        private TSkin.TMenuStrip tMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 最大资源网ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oK资源网ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 速播资源站ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 麻花资源ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最新资源网ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _123资源网ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 超快资源网ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 卧龙资源网ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高清资源网ToolStripMenuItem;
    }
}