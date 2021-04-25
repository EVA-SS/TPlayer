namespace TPlayer.Frm
{
    partial class Update
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.icon_pie = new TSkin.TBut();
            this.btn_install = new TSkin.TBut();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(406, 34);
            this.label2.TabIndex = 0;
            this.label2.Text = "  ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoEllipsis = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑",32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(102)))), ((int)(((byte)(214)))));
            this.label4.Location = new System.Drawing.Point(10, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(322, 41);
            this.label4.TabIndex = 1;
            this.label4.Text = "全新版本";
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoEllipsis = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑",16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.label5.Location = new System.Drawing.Point(12, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(390, 313);
            this.label5.TabIndex = 1;
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // icon_pie
            // 
            this.icon_pie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.icon_pie.BorderWidth = 0F;
            this.icon_pie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(106)))), ((int)(((byte)(10)))));
            this.icon_pie.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
            this.icon_pie.ForeColor = System.Drawing.Color.White;
            this.icon_pie.Location = new System.Drawing.Point(338, 42);
            this.icon_pie.MaxValue = 0D;
            this.icon_pie.Name = "icon_pie";
            this.icon_pie.Radius = 20;
            this.icon_pie.Size = new System.Drawing.Size(65, 26);
            this.icon_pie.TabIndex = 3;
            this.icon_pie.Text = "预览版";
            this.icon_pie.Value = 0D;
            this.icon_pie.Visible = false;
            this.icon_pie.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // btn_install
            // 
            this.btn_install.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_install.BorderWidth = 0F;
            this.btn_install.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(237)))), ((int)(((byte)(240)))));
            this.btn_install.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(237)))), ((int)(((byte)(240)))));
            this.btn_install.Font = new System.Drawing.Font("微软雅黑",14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_install.Location = new System.Drawing.Point(71, 431);
            this.btn_install.MaxValue = 0D;
            this.btn_install.Name = "btn_install";
            this.btn_install.Radius = 38;
            this.btn_install.Size = new System.Drawing.Size(272, 40);
            this.btn_install.TabIndex = 0;
            this.btn_install.Text = "我已知晓";
            this.btn_install.Value = 0D;
            this.btn_install.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // Update
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(414, 490);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.icon_pie);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_install);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Name = "Update";
            this.Radius = 60;
            this.RoundStyle = TSkin.UICornerRadiusSides.All;
            this.ShadowWidth = 10;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新信息";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private TSkin.TBut icon_pie;
        private TSkin.TBut btn_install;
    }
}