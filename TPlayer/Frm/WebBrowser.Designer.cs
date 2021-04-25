namespace TPlayer.Frm
{
    partial class WebBrowser
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
            this.btn_list = new TSkin.TBut();
            this.btn_next = new TSkin.TBut();
            this.btn_min = new TSkin.TBut();
            this.btn_max = new TSkin.TBut();
            this.btn_close = new TSkin.TBut();
            this.pictureBox2 = new TSkin.LoadingMetro();
            this.webPlay = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_list);
            this.panel1.Controls.Add(this.btn_next);
            this.panel1.Controls.Add(this.btn_min);
            this.panel1.Controls.Add(this.btn_max);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("微软雅黑",11F);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 34);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MaxMove);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(34, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(596, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "浏览器";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MaxMove);
            // 
            // btn_list
            // 
            this.btn_list.BackColor = System.Drawing.Color.Transparent;
            this.btn_list.BorderWidth = 0F;
            this.btn_list.BackColor = System.Drawing.Color.Transparent;
            this.btn_list.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_list.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_list.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_list.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_list.ImageSize = new System.Drawing.Size(26,26);
            this.btn_list.Location = new System.Drawing.Point(630, 0);
            this.btn_list.MaxValue = 0D;
            this.btn_list.Name = "btn_list";
            this.btn_list.Size = new System.Drawing.Size(34, 34);
            this.btn_list.TabIndex = 7;
            this.btn_list.TabStop = false;
            this.btn_list.Text = "";
            this.btn_list.Value = 0D;
            this.btn_list.Visible = false;
            this.btn_list.Click += new System.EventHandler(this.btn_list_Click);
            // 
            // btn_next
            // 
            this.btn_next.BackColor = System.Drawing.Color.Transparent;
            this.btn_next.BorderWidth = 0F;
            this.btn_next.BackColor = System.Drawing.Color.Transparent;
            this.btn_next.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_next.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_next.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_next.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_next.ImageSize = new System.Drawing.Size(14,14);
            this.btn_next.Location = new System.Drawing.Point(664, 0);
            this.btn_next.MaxValue = 0D;
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(34, 34);
            this.btn_next.TabIndex = 6;
            this.btn_next.TabStop = false;
            this.btn_next.Text = "";
            this.btn_next.Value = 0D;
            this.btn_next.Visible = false;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
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
            this.btn_min.Location = new System.Drawing.Point(698, 0);
            this.btn_min.MaxValue = 0D;
            this.btn_min.Name = "btn_min";
            this.btn_min.Size = new System.Drawing.Size(34, 34);
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
            this.btn_max.Location = new System.Drawing.Point(732, 0);
            this.btn_max.MaxValue = 0D;
            this.btn_max.Name = "btn_max";
            this.btn_max.Size = new System.Drawing.Size(34, 34);
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
            this.btn_close.Location = new System.Drawing.Point(766, 0);
            this.btn_close.MaxValue = 0D;
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(34, 34);
            this.btn_close.TabIndex = 5;
            this.btn_close.TabStop = false;
            this.btn_close.Text = "";
            this.btn_close.Value = 0D;
            this.btn_close.Click += new System.EventHandler(this.Close_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Color = System.Drawing.Color.DodgerBlue;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 34);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.Visible = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MaxMove);
            // 
            // webPlay
            // 
            this.webPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webPlay.Location = new System.Drawing.Point(0, 34);
            this.webPlay.Name = "webPlay";
            this.webPlay.Size = new System.Drawing.Size(800, 416);
            this.webPlay.TabIndex = 0;
            this.webPlay.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webPlay_DocumentCompleted);
            this.webPlay.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webPlay_Navigated);
            this.webPlay.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webPlay_Navigating);
            // 
            // WebBrowser
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Borders = new System.Windows.Forms.Padding(0);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webPlay);
            this.Controls.Add(this.panel1);
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "WebBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "浏览器";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private TSkin.TBut btn_min;
        private TSkin.TBut btn_max;
        private TSkin.TBut btn_close;
        private TSkin.LoadingMetro pictureBox2;
        private System.Windows.Forms.WebBrowser webPlay;
        private System.Windows.Forms.Label label1;
        private TSkin.TBut btn_list;
        private TSkin.TBut btn_next;
    }
}