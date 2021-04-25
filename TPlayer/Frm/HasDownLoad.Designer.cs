namespace TPlayer.Frm
{
    partial class HasDownLoad
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ok = new TSkin.TBut();
            this.no = new TSkin.TBut();
            this.btn_back = new TSkin.TBut();
            this.tCheckBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TPlayer.Properties.Resources.icon_warn;
            this.pictureBox1.Location = new System.Drawing.Point(32, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑",16F);
            this.label1.Location = new System.Drawing.Point(101, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "还有 1个 任务正在下载";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(103, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(333, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "退出也可以保存下载进度，请勿担心";
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ok.BorderColor = System.Drawing.Color.RoyalBlue;
            this.ok.BorderWidth = 1F;
            this.ok.BackColor = System.Drawing.Color.RoyalBlue;
            this.ok.BackColor2 = System.Drawing.Color.CornflowerBlue;
            this.ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ok.ForeColor = System.Drawing.Color.White;
            this.ok.Location = new System.Drawing.Point(32, 116);
            this.ok.Name = "ok";
            this.ok.Radius = 2;
            this.ok.Size = new System.Drawing.Size(139, 24);
            this.ok.TabIndex = 0;
            this.ok.Text = "我点错了";
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // no
            // 
            this.no.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.no.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.no.BackColor2 = System.Drawing.Color.OrangeRed;
            this.no.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.no.ForeColor = System.Drawing.Color.White;
            this.no.Location = new System.Drawing.Point(211, 116);
            this.no.Name = "no";
            this.no.Radius = 2;
            this.no.Size = new System.Drawing.Size(88, 24);
            this.no.TabIndex = 2;
            this.no.Text = "继续退出";
            this.no.Click += new System.EventHandler(this.no_Click);
            // 
            // btn_back
            // 
            this.btn_back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_back.BorderWidth = 1F;
            this.btn_back.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_back.Location = new System.Drawing.Point(318, 116);
            this.btn_back.Name = "btn_back";
            this.btn_back.Radius = 2;
            this.btn_back.Size = new System.Drawing.Size(104, 24);
            this.btn_back.TabIndex = 0;
            this.btn_back.Text = "后台下载";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // tCheckBox1
            // 
            this.tCheckBox1.Location = new System.Drawing.Point(322, 92);
            this.tCheckBox1.Name = "tCheckBox1";
            this.tCheckBox1.Size = new System.Drawing.Size(100, 20);
            this.tCheckBox1.TabIndex = 3;
            this.tCheckBox1.Text = "下载完成退出";
            this.tCheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HasDownLoad
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Borders = new System.Windows.Forms.Padding(0);
            this.CancelButton = this.ok;
            this.ClientSize = new System.Drawing.Size(448, 152);
            this.Controls.Add(this.tCheckBox1);
            this.Controls.Add(this.no);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(448, 152);
            this.Name = "HasDownLoad";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AduDownLoad 下载器未找到";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private TSkin.TBut ok;
        private TSkin.TBut no;
        private TSkin.TBut btn_back;
        private System.Windows.Forms.CheckBox tCheckBox1;
    }
}