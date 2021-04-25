namespace TPlayer
{
    partial class DownList
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_play = new TSkin.TBut();
            this.btn_pause = new TSkin.TBut();
            this.btn_min = new TSkin.TBut();
            this.btn_close = new TSkin.TBut();
            this.shadowLine1 = new TSkin.ShadowLine();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.shadowLine1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_play);
            this.panel1.Controls.Add(this.btn_pause);
            this.panel1.Controls.Add(this.btn_min);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("微软雅黑",11F);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 34);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微软雅黑",11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(80, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 34);
            this.label2.TabIndex = 0;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "下载列表";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // btn_play
            // 
            this.btn_play.BackColor = System.Drawing.Color.Transparent;
            this.btn_play.BackColor = System.Drawing.Color.Transparent;
            this.btn_play.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_play.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_play.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_play.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_play.ImageSize = new System.Drawing.Size(26,26);
            this.btn_play.Location = new System.Drawing.Point(262, 0);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(34, 34);
            this.btn_play.TabIndex = 7;
            this.btn_play.TabStop = false;
            this.btn_play.Text = "";
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.BackColor = System.Drawing.Color.Transparent;
            this.btn_pause.BackColor = System.Drawing.Color.Transparent;
            this.btn_pause.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_pause.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_pause.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_pause.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_pause.ImageSize = new System.Drawing.Size(26,26);
            this.btn_pause.Location = new System.Drawing.Point(296, 0);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(34, 34);
            this.btn_pause.TabIndex = 6;
            this.btn_pause.TabStop = false;
            this.btn_pause.Text = "";
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // btn_min
            // 
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_min.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_min.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_min.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_min.ImageSize = new System.Drawing.Size(26,26);
            this.btn_min.Location = new System.Drawing.Point(330, 0);
            this.btn_min.Name = "btn_min";
            this.btn_min.Size = new System.Drawing.Size(34, 34);
            this.btn_min.TabIndex = 3;
            this.btn_min.TabStop = false;
            this.btn_min.Text = "";
            this.btn_min.Click += new System.EventHandler(this.Min_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btn_close.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_close.ImageSize = new System.Drawing.Size(26,26);
            this.btn_close.Location = new System.Drawing.Point(364, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(34, 34);
            this.btn_close.TabIndex = 5;
            this.btn_close.TabStop = false;
            this.btn_close.Text = "";
            this.btn_close.Click += new System.EventHandler(this.Close_Click);
            // 
            // shadowLine1
            // 
            this.shadowLine1.Controls.Add(this.panel2);
            this.shadowLine1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadowLine1.Location = new System.Drawing.Point(0, 34);
            this.shadowLine1.Name = "shadowLine1";
            this.shadowLine1.Size = new System.Drawing.Size(398, 283);
            this.shadowLine1.TabIndex = 2;
            this.shadowLine1.Type = TSkin.ShadowLine.ShadowType.TOP;
            this.shadowLine1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(398, 283);
            this.panel2.TabIndex = 0;
            // 
            // DownList
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Borders = new System.Windows.Forms.Padding(0);
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(398, 317);
            this.Controls.Add(this.shadowLine1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(398, 290);
            this.Name = "DownList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下载列表";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            this.panel1.ResumeLayout(false);
            this.shadowLine1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private TSkin.TBut btn_min;
        private TSkin.TBut btn_close;
        private TSkin.TBut btn_play;
        private TSkin.TBut btn_pause;
        private System.Windows.Forms.Label label2;
        private TSkin.ShadowLine shadowLine1;
        private System.Windows.Forms.Panel panel2;
    }
}