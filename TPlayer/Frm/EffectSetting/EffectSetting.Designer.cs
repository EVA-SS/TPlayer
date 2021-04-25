namespace TPlayer
{
    partial class EffectSetting
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
            this.b0 = new TSkin.TBut();
            this.b1 = new TSkin.TBut();
            this.b2 = new TSkin.TBut();
            this.b3 = new TSkin.TBut();
            this.b4 = new TSkin.TBut();
            this.b5 = new TSkin.TBut();
            this.b6 = new TSkin.TBut();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // b0
            // 
            this.b0.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b0.BackColorActive2 = System.Drawing.Color.DodgerBlue;
            this.b0.Dock = System.Windows.Forms.DockStyle.Top;
            this.b0.IsActive = true;
            this.b0.Location = new System.Drawing.Point(0, 12);
            this.b0.Name = "b0";
            this.b0.Size = new System.Drawing.Size(86, 30);
            this.b0.TabIndex = 0;
            this.b0.Tag = "0";
            this.b0.Text = "播放";
            this.b0.Click += new System.EventHandler(this.TypeChange);
            // 
            // b1
            // 
            this.b1.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b1.BackColorActive2 = System.Drawing.Color.DodgerBlue;
            this.b1.Dock = System.Windows.Forms.DockStyle.Top;
            this.b1.Location = new System.Drawing.Point(0, 42);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(86, 30);
            this.b1.TabIndex = 1;
            this.b1.Tag = "1";
            this.b1.Text = "画面";
            this.b1.Click += new System.EventHandler(this.TypeChange);
            // 
            // b2
            // 
            this.b2.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b2.BackColorActive2 = System.Drawing.Color.DodgerBlue;
            this.b2.Dock = System.Windows.Forms.DockStyle.Top;
            this.b2.Location = new System.Drawing.Point(0, 132);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(86, 30);
            this.b2.TabIndex = 4;
            this.b2.Tag = "2";
            this.b2.Text = "色彩";
            this.b2.Click += new System.EventHandler(this.TypeChange);
            // 
            // b3
            // 
            this.b3.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b3.BackColorActive2 = System.Drawing.Color.DodgerBlue;
            this.b3.Dock = System.Windows.Forms.DockStyle.Top;
            this.b3.Location = new System.Drawing.Point(0, 162);
            this.b3.Name = "b3";
            this.b3.Size = new System.Drawing.Size(86, 30);
            this.b3.TabIndex = 5;
            this.b3.Tag = "3";
            this.b3.Text = "声音";
            this.b3.Click += new System.EventHandler(this.TypeChange);
            // 
            // b4
            // 
            this.b4.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b4.BackColorActive2 = System.Drawing.Color.DodgerBlue;
            this.b4.Dock = System.Windows.Forms.DockStyle.Top;
            this.b4.Location = new System.Drawing.Point(0, 192);
            this.b4.Name = "b4";
            this.b4.Size = new System.Drawing.Size(86, 30);
            this.b4.TabIndex = 6;
            this.b4.Tag = "4";
            this.b4.Text = "字幕";
            this.b4.Click += new System.EventHandler(this.TypeChange);
            // 
            // b5
            // 
            this.b5.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b5.BackColorActive2 = System.Drawing.Color.DodgerBlue;
            this.b5.Dock = System.Windows.Forms.DockStyle.Top;
            this.b5.Location = new System.Drawing.Point(0, 72);
            this.b5.Name = "b5";
            this.b5.Size = new System.Drawing.Size(86, 30);
            this.b5.TabIndex = 2;
            this.b5.Tag = "5";
            this.b5.Text = "3  D";
            this.b5.Visible = false;
            this.b5.Click += new System.EventHandler(this.TypeChange);
            // 
            // b6
            // 
            this.b6.BackColorActive = System.Drawing.Color.DodgerBlue;
            this.b6.BackColorActive2 = System.Drawing.Color.DodgerBlue;
            this.b6.Dock = System.Windows.Forms.DockStyle.Top;
            this.b6.Location = new System.Drawing.Point(0, 102);
            this.b6.Name = "b6";
            this.b6.Size = new System.Drawing.Size(86, 30);
            this.b6.TabIndex = 3;
            this.b6.Tag = "6";
            this.b6.Text = "V  R";
            this.b6.Visible = false;
            this.b6.Click += new System.EventHandler(this.TypeChange);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
            this.panel1.Controls.Add(this.b4);
            this.panel1.Controls.Add(this.b3);
            this.panel1.Controls.Add(this.b2);
            this.panel1.Controls.Add(this.b6);
            this.panel1.Controls.Add(this.b5);
            this.panel1.Controls.Add(this.b1);
            this.panel1.Controls.Add(this.b0);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(86, 290);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 12);
            this.label1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(86, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 290);
            this.panel2.TabIndex = 1;
            // 
            // EffectSetting
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(398, 290);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.MinimumSize = new System.Drawing.Size(398, 290);
            this.Name = "EffectSetting";
            this.Radius = 8;
            this.RoundStyle = ((TSkin.UICornerRadiusSides)((((TSkin.UICornerRadiusSides.LeftTop | TSkin.UICornerRadiusSides.RightTop) 
            | TSkin.UICornerRadiusSides.RightBottom) 
            | TSkin.UICornerRadiusSides.LeftBottom)));
            this.ShadowWidth = 8;
            this.ShowInTaskbar = false;
            this.Text = "EffectSetting";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TSkin.TBut b0;
        private TSkin.TBut b1;
        private TSkin.TBut b2;
        private TSkin.TBut b3;
        private TSkin.TBut b4;
        private TSkin.TBut b5;
        private TSkin.TBut b6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
    }
}