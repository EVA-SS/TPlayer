namespace TPlayer
{
    partial class BugReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.frm_top = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_close = new TSkin.TBut();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new TSkin.ShadowLine();
            this.shadow = new TSkin.ShadowLine();
            this.loading = new TSkin.LoadingMetroHorizontal();
            ((System.ComponentModel.ISupportInitialize)(this.frm_top)).BeginInit();
            this.frm_top.SuspendLayout();
            this.panel2.SuspendLayout();
            this.shadow.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.label1.Location = new System.Drawing.Point(73, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(661, 28);
            this.label1.TabIndex = 0;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoEllipsis = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑",16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.label5.Location = new System.Drawing.Point(91, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(545, 222);
            this.label5.TabIndex = 0;
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(0, 399);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(727, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "                    这将不会泄露您的任何私人信息";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 370);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(727, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "                    我们将会收集您的系统信息与日志分析";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(163)))));
            this.btn_ok.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.btn_ok.FlatAppearance.BorderSize = 0;
            this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ok.Location = new System.Drawing.Point(265, 30);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(196, 40);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "提交";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // frm_top
            // 
            this.frm_top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.frm_top.Controls.Add(this.label4);
            this.frm_top.Controls.Add(this.btn_close);
            this.frm_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.frm_top.ForeColor = System.Drawing.Color.White;
            this.frm_top.Location = new System.Drawing.Point(0, 0);
            this.frm_top.Name = "frm_top";
            this.frm_top.Size = new System.Drawing.Size(727, 32);
            this.frm_top.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.frm_top.TabIndex = 4;
            this.frm_top.TabStop = false;
            this.frm_top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(40, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(687, 32);
            this.label4.TabIndex = 0;
            this.label4.Text = "  Bug报告";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackColor2 = System.Drawing.Color.Transparent;
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_close.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_close.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_close.ImageSize = new System.Drawing.Size(26,26);
            this.btn_close.Location = new System.Drawing.Point(0, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(40, 32);
            this.btn_close.TabIndex = 0;
            this.btn_close.TabStop = false;
            this.btn_close.Text = "";
            this.btn_close.Click += new System.EventHandler(this.btn_no_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoEllipsis = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑",20F);
            this.label6.Location = new System.Drawing.Point(94, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(480, 85);
            this.label6.TabIndex = 0;
            this.label6.Text = "因为我们的问题给您造成的困扰，我们感到抱歉";
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // panel2
            // 
            this.panel2.AEnabled = true;
            this.panel2.AWidth = 46;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.panel2.CEnabled = false;
            this.panel2.Controls.Add(this.btn_ok);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("微软雅黑",12F);
            this.panel2.Location = new System.Drawing.Point(0, 424);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(727, 80);
            this.panel2.TabIndex = 0;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // shadow
            // 
            this.shadow.Controls.Add(this.label6);
            this.shadow.Controls.Add(this.loading);
            this.shadow.Controls.Add(this.label1);
            this.shadow.Controls.Add(this.label5);
            this.shadow.CWidth = 60;
            this.shadow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadow.Font = new System.Drawing.Font("微软雅黑",12F);
            this.shadow.Location = new System.Drawing.Point(0, 32);
            this.shadow.Name = "shadow";
            this.shadow.Size = new System.Drawing.Size(727, 338);
            this.shadow.TabIndex = 1;
            this.shadow.Type = TSkin.ShadowLine.ShadowType.TOP;
            this.shadow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // loading
            // 
            this.loading.BackColor = System.Drawing.Color.Transparent;
            this.loading.CirularWidth = 6;
            this.loading.Color = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.loading.Dock = System.Windows.Forms.DockStyle.Top;
            this.loading.Location = new System.Drawing.Point(0, 0);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(727, 18);
            this.loading.TabIndex = 0;
            this.loading.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // BugReport
            // 
            this.AcceptButton = this.btn_ok;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(30)))), ((int)(((byte)(6)))));
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(727, 504);
            this.Controls.Add(this.shadow);
            this.Controls.Add(this.frm_top);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("微软雅黑",10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.bug;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "BugReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BugReport";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            ((System.ComponentModel.ISupportInitialize)(this.frm_top)).EndInit();
            this.frm_top.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.shadow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox frm_top;
        private System.Windows.Forms.Label label4;
        private TSkin.TBut btn_close;
        private System.Windows.Forms.Label label6;
        private TSkin.ShadowLine panel2;
        private TSkin.ShadowLine shadow;
        private TSkin.LoadingMetroHorizontal loading;
    }
}