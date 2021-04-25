namespace TPlayer
{
    partial class ControllerMini
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
            this.progFull = new TSkin.TProgFull();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_next = new TSkin.TBut();
            this.btn_play = new TSkin.TBut();
            this.btn_tv = new TSkin.TBut();
            this.btn_exit = new TSkin.TBut();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progFull
            // 
            this.progFull.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.progFull.Dock = System.Windows.Forms.DockStyle.Top;
            this.progFull.Location = new System.Drawing.Point(0, 0);
            this.progFull.MaxValue = 0D;
            this.progFull.Name = "progFull";
            this.progFull.Size = new System.Drawing.Size(350, 6);
            this.progFull.TabIndex = 0;
            this.progFull.Value = 0D;
            this.progFull.MaxTimeChange += new TSkin.TProgFull.ValueEventHandler(this.progFull_MaxTimeChange);
            this.progFull.TimeChange += new TSkin.TProgFull.ValueEventHandler(this.progFull_TimeChange);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题啊标题";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 23);
            this.label2.TabIndex = 0;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_next
            // 
            this.btn_next.BorderWidth = 0F;
            this.btn_next.DefaultColor = System.Drawing.Color.Transparent;
            this.btn_next.DefaultColor2 = System.Drawing.Color.Transparent;
            this.btn_next.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_next.Image = global::TPlayer.Properties.Resources.material_skip_next_h;
            this.btn_next.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_next.ImageSize = 24;
            this.btn_next.Location = new System.Drawing.Point(42, 0);
            this.btn_next.MaxValue = 0D;
            this.btn_next.Name = "btn_next";
            this.btn_next.Radius = 40;
            this.btn_next.Size = new System.Drawing.Size(42, 42);
            this.btn_next.TabIndex = 1;
            this.btn_next.Text = "";
            this.btn_next.Value = 0D;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_play
            // 
            this.btn_play.BorderWidth = 0F;
            this.btn_play.DefaultColor = System.Drawing.Color.Transparent;
            this.btn_play.DefaultColor2 = System.Drawing.Color.Transparent;
            this.btn_play.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_play.Image = global::TPlayer.Properties.Resources.material_play_h;
            this.btn_play.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_play.ImageSize = 24;
            this.btn_play.Location = new System.Drawing.Point(0, 0);
            this.btn_play.MaxValue = 0D;
            this.btn_play.Name = "btn_play";
            this.btn_play.Radius = 40;
            this.btn_play.Size = new System.Drawing.Size(42, 42);
            this.btn_play.TabIndex = 0;
            this.btn_play.Text = "";
            this.btn_play.Value = 0D;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // btn_tv
            // 
            this.btn_tv.BorderWidth = 0F;
            this.btn_tv.DefaultColor = System.Drawing.Color.Transparent;
            this.btn_tv.DefaultColor2 = System.Drawing.Color.Transparent;
            this.btn_tv.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_tv.Image = global::TPlayer.Properties.Resources.material_tv_h;
            this.btn_tv.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_tv.ImageSize = 24;
            this.btn_tv.Location = new System.Drawing.Point(84, 0);
            this.btn_tv.MaxValue = 0D;
            this.btn_tv.Name = "btn_tv";
            this.btn_tv.Radius = 40;
            this.btn_tv.Size = new System.Drawing.Size(42, 42);
            this.btn_tv.TabIndex = 2;
            this.btn_tv.Text = "";
            this.btn_tv.Value = 0D;
            this.btn_tv.Click += new System.EventHandler(this.btn_tv_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.BorderWidth = 0F;
            this.btn_exit.DefaultColor = System.Drawing.Color.Transparent;
            this.btn_exit.DefaultColor2 = System.Drawing.Color.Transparent;
            this.btn_exit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_exit.Image = global::TPlayer.Properties.Resources.material_exit_h;
            this.btn_exit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_exit.ImageSize = 24;
            this.btn_exit.Location = new System.Drawing.Point(126, 0);
            this.btn_exit.MaxValue = 0D;
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Radius = 40;
            this.btn_exit.Size = new System.Drawing.Size(42, 42);
            this.btn_exit.TabIndex = 3;
            this.btn_exit.Text = "";
            this.btn_exit.Value = 0D;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btn_play);
            this.panel1.Controls.Add(this.btn_next);
            this.panel1.Controls.Add(this.btn_tv);
            this.panel1.Controls.Add(this.btn_exit);
            this.panel1.Location = new System.Drawing.Point(182, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 42);
            this.panel1.TabIndex = 0;
            // 
            // ControllerMini
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 80);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progFull);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.MaximumSize = new System.Drawing.Size(350, 80);
            this.MinimumSize = new System.Drawing.Size(350, 80);
            this.Name = "ControllerMini";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public TSkin.TProgFull progFull;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        private TSkin.TBut btn_play;
        private TSkin.TBut btn_next;
        private TSkin.TBut btn_tv;
        private TSkin.TBut btn_exit;
        private System.Windows.Forms.Panel panel1;
    }
}