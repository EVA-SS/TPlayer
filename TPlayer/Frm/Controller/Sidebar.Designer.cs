namespace TPlayer.Com
{
    partial class Sidebar
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_del = new TSkin.TButs2();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_more = new TSkin.TButs2();
            this.panel1 = new System.Windows.Forms.Panel();
            this.text_search = new System.Windows.Forms.TextBox();
            this.btn_search = new TSkin.TBut();
            this.playList = new TPlayerList.VideoList();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_del);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.btn_more);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(280, 110);
            this.panel3.TabIndex = 1;
            // 
            // btn_del
            // 
            this.btn_del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_del.BackColor = System.Drawing.Color.Transparent;
            this.btn_del.ImageWidth = 24;
            this.btn_del.Location = new System.Drawing.Point(210, 78);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(26, 26);
            this.btn_del.TabIndex = 3;
            this.btn_del.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))));
            this.label2.Location = new System.Drawing.Point(34, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 24);
            this.label2.TabIndex = 0;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑",16F);
            this.label1.Location = new System.Drawing.Point(34, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "播放列队";
            // 
            // btn_more
            // 
            this.btn_more.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_more.BackColor = System.Drawing.Color.Transparent;
            this.btn_more.ImageWidth = 24;
            this.btn_more.Location = new System.Drawing.Point(242, 78);
            this.btn_more.Name = "btn_more";
            this.btn_more.Size = new System.Drawing.Size(26, 26);
            this.btn_more.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.text_search);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 36);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // text_search
            // 
            this.text_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.text_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(63)))));
            this.text_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_search.Font = new System.Drawing.Font("微软雅黑",12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.text_search.ForeColor = System.Drawing.Color.White;
            this.text_search.Location = new System.Drawing.Point(12, 7);
            this.text_search.Name = "text_search";
            this.text_search.Size = new System.Drawing.Size(224, 22);
            this.text_search.TabIndex = 0;
            this.text_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_search_KeyPress);
            // 
            // btn_search
            // 
            this.btn_search.BorderWidth = 0F;
            this.btn_search.BackColor = System.Drawing.Color.Transparent;
            this.btn_search.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_search.ForeColor = System.Drawing.Color.White;
            this.btn_search.ImageSize = new System.Drawing.Size(22,22);
            this.btn_search.Location = new System.Drawing.Point(244, 0);
            this.btn_search.MaxValue = 0D;
            this.btn_search.Name = "btn_search";
            this.btn_search.Radius = 34;
            this.btn_search.Size = new System.Drawing.Size(36, 36);
            this.btn_search.TabIndex = 1;
            this.btn_search.Value = 0D;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // playList
            // 
            this.playList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playList.Location = new System.Drawing.Point(0, 146);
            this.playList.Name = "playList";
            this.playList.PlayIndex = -1;
            this.playList.Playing = false;
            this.playList.ScrollSliderDefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.playList.ScrollSliderDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.playList.SelectItem = null;
            this.playList.Size = new System.Drawing.Size(280, 203);
            this.playList.TabIndex = 0;
            this.playList.Text = "titleCheckControl1";
            this.playList.PlayClick += new TPlayerList.VideoList.DownEventHandler(this.playList_PlayClick);
            this.playList.DownClick += new TPlayerList.VideoList.DownEventHandler(this.playList_DownClick);
            this.playList.MoreClick += new TPlayerList.VideoList.DownEventHandler(this.playList_MoreClick);
            // 
            // Sidebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(280, 349);
            this.Controls.Add(this.playList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Sidebar";
            this.ShowInTaskbar = false;
            this.Text = "Sidebar";
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        public TPlayerList.VideoList playList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private TSkin.TButs2 btn_del;
        private TSkin.TButs2 btn_more;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox text_search;
        private TSkin.TBut btn_search;
    }
}