namespace TPlayer
{
    partial class OpenUrl
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
            this.btn_clear = new TSkin.TBut();
            this.btn_ok = new TSkin.TBut();
            this.btn_no = new TSkin.TBut();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_err = new TSkin.TBut();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Controls.Add(this.btn_no);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 193);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 41);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_clear.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btn_clear.BorderWidth = 1F;
            this.btn_clear.BackColor = System.Drawing.Color.White;
            this.btn_clear.BackColor2 = System.Drawing.Color.White;
            this.btn_clear.Location = new System.Drawing.Point(93, 8);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(86, 24);
            this.btn_clear.TabIndex = 2;
            this.btn_clear.Text = "清空记录";
            this.btn_clear.Visible = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_ok.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btn_ok.BorderWidth = 1F;
            this.btn_ok.BackColor = System.Drawing.Color.White;
            this.btn_ok.BackColor2 = System.Drawing.Color.White;
            this.btn_ok.Location = new System.Drawing.Point(246, 8);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(87, 24);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "确认";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_no
            // 
            this.btn_no.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_no.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btn_no.BorderWidth = 1F;
            this.btn_no.BackColor = System.Drawing.Color.White;
            this.btn_no.BackColor2 = System.Drawing.Color.White;
            this.btn_no.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_no.Location = new System.Drawing.Point(339, 8);
            this.btn_no.Name = "btn_no";
            this.btn_no.Size = new System.Drawing.Size(87, 24);
            this.btn_no.TabIndex = 0;
            this.btn_no.Text = "取消";
            this.btn_no.Click += new System.EventHandler(this.btn_no_Click);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("微软雅黑",12F);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 41);
            this.label2.TabIndex = 0;
            this.label2.Text = "打开URL";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(28, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(331, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "协议支持：http,https(支持. rm\\.rmvb\\.wmv\\.mp3格式)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑",12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入要播放媒体文件的网络地址或本地路径";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.ForeColor = System.Drawing.Color.Black;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(98, 92);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(308, 25);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "打开URL：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // btn_err
            // 
            this.btn_err.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_err.BackColor = System.Drawing.Color.Transparent;
            this.btn_err.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_err.Enabled = false;
            this.btn_err.EnabledOpacity = 1F;
            this.btn_err.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_err.ImageSize = new System.Drawing.Size(30,30);
            this.btn_err.LoadingColor = System.Drawing.Color.Black;
            this.btn_err.Location = new System.Drawing.Point(395, 15);
            this.btn_err.Name = "btn_err";
            this.btn_err.Radius = 1;
            this.btn_err.Size = new System.Drawing.Size(30, 30);
            this.btn_err.TabIndex = 0;
            this.btn_err.TabStop = false;
            this.btn_err.Text = "";
            this.btn_err.Click += new System.EventHandler(this.btn_err_Click);
            this.btn_err.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // OpenUrl
            // 
            this.AcceptButton = this.btn_ok;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Borders = new System.Windows.Forms.Padding(0);
            this.CancelButton = this.btn_no;
            this.ClientSize = new System.Drawing.Size(438, 234);
            this.Controls.Add(this.btn_err);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑",9F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(438, 234);
            this.Name = "OpenUrl";
            this.Resizable = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenUrl";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private TSkin.TBut btn_no;
        private TSkin.TBut btn_ok;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private TSkin.TBut btn_clear;
        private TSkin.TBut btn_err;
    }
}