namespace Popup
{
    partial class Dialog
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
            this.label_title = new System.Windows.Forms.Label();
            this.label_value = new System.Windows.Forms.Label();
            this.btn_no = new TSkin.TBut();
            this.btn_ok = new TSkin.TBut();
            this.panel_value = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_title.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_title.Location = new System.Drawing.Point(0, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(462, 36);
            this.label_title.TabIndex = 0;
            this.label_title.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_value
            // 
            this.label_value.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_value.ForeColor = System.Drawing.Color.DarkGray;
            this.label_value.Location = new System.Drawing.Point(0, 36);
            this.label_value.Name = "label_value";
            this.label_value.Size = new System.Drawing.Size(462, 24);
            this.label_value.TabIndex = 0;
            this.label_value.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_value.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // btn_no
            // 
            this.btn_no.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_no.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_no.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_no.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_no.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.btn_no.Location = new System.Drawing.Point(81, 142);
            this.btn_no.Name = "btn_no";
            this.btn_no.Radius = 6;
            this.btn_no.Size = new System.Drawing.Size(120, 40);
            this.btn_no.TabIndex = 1;
            this.btn_no.Text = "取消";
            this.btn_no.Click += new System.EventHandler(this.btn_no_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.btn_ok.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.btn_ok.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_ok.ForeColor = System.Drawing.Color.White;
            this.btn_ok.Location = new System.Drawing.Point(261, 142);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Radius = 6;
            this.btn_ok.Size = new System.Drawing.Size(120, 40);
            this.btn_ok.Speed = 2;
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "";
            this.btn_ok.TextMargin = new System.Windows.Forms.Padding(12);
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // panel_value
            // 
            this.panel_value.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_value.AutoEllipsis = true;
            this.panel_value.Location = new System.Drawing.Point(26, 85);
            this.panel_value.Name = "panel_value";
            this.panel_value.Size = new System.Drawing.Size(411, 50);
            this.panel_value.TabIndex = 0;
            this.panel_value.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // Dialog
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btn_no;
            this.ClientSize = new System.Drawing.Size(462, 200);
            this.Controls.Add(this.btn_no);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.label_value);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.panel_value);
            this.Opacity = 0.96D;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(462, 200);
            this.MinimumSize = new System.Drawing.Size(440, 200);
            this.Icon = TPlayer.Properties.Resources.TLogo;
            this.Name = "Dialog";
            this.Radius = 6;
            this.RoundStyle = ((TSkin.UICornerRadiusSides)((((TSkin.UICornerRadiusSides.LeftTop | TSkin.UICornerRadiusSides.RightTop) 
            | TSkin.UICornerRadiusSides.RightBottom) 
            | TSkin.UICornerRadiusSides.LeftBottom)));
            this.ShadowWidth = 4;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_value;
        private TSkin.TBut btn_no;
        private TSkin.TBut btn_ok;
        private System.Windows.Forms.Label panel_value;
    }
}