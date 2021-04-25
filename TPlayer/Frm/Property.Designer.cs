
namespace TPlayer.Frm
{
    partial class Property
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Exit = new TSkin.TBut();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.curve = new TSkin.Curve();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timBk = new System.Windows.Forms.Timer(this.components);
            this.loadingMaterial = new TSkin.LoadingMaterialHorizonta();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.curve.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Exit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 206);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 50);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 50);
            this.label2.TabIndex = 0;
            this.label2.Text = "媒体信息";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // Exit
            // 
            this.Exit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Exit.BorderWidth = 1F;
            this.Exit.BackColor = System.Drawing.Color.White;
            this.Exit.BackColor2 = System.Drawing.Color.White;
            this.Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Exit.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Exit.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Exit.Location = new System.Drawing.Point(396, 14);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(87, 23);
            this.Exit.TabIndex = 0;
            this.Exit.Text = "确定";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(252, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(230, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "码率信息：未知";
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(12, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(230, 20);
            this.label9.TabIndex = 19;
            this.label9.Text = "文件时长：未知";
            this.label9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(252, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "视频帧率：未知";
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(12, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "编码格式：未知";
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "分 辨 率 ：未知";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(470, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Video Title";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(252, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(230, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "文件大小：未知";
            this.label8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // curve
            // 
            this.curve.Controls.Add(this.label10);
            this.curve.Controls.Add(this.label7);
            this.curve.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.curve.Location = new System.Drawing.Point(0, 132);
            this.curve.Name = "curve";
            this.curve.Size = new System.Drawing.Size(494, 74);
            this.curve.TabIndex = 0;
            this.curve.ValColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(250)))));
            this.curve.ValColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(250)))));
            this.curve.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(0, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(494, 20);
            this.label10.TabIndex = 0;
            this.label10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(494, 20);
            this.label7.TabIndex = 0;
            this.label7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // timBk
            // 
            this.timBk.Interval = 1000;
            this.timBk.Tick += new System.EventHandler(this.timBk_Tick);
            // 
            // loadingMaterial
            // 
            this.loadingMaterial.Color = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(227)))), ((int)(((byte)(194)))));
            this.loadingMaterial.Dock = System.Windows.Forms.DockStyle.Top;
            this.loadingMaterial.Location = new System.Drawing.Point(0, 0);
            this.loadingMaterial.Name = "loadingMaterial";
            this.loadingMaterial.ProgColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(250)))));
            this.loadingMaterial.Size = new System.Drawing.Size(494, 4);
            this.loadingMaterial.TabIndex = 0;
            this.loadingMaterial.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(227)))), ((int)(((byte)(194)))));
            this.label11.Location = new System.Drawing.Point(396, 108);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 20);
            this.label11.TabIndex = 0;
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label11.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            // 
            // Property
            // 
            this.AcceptButton = this.Exit;
            this.BackColor = System.Drawing.Color.White;
            this.Borders = new System.Windows.Forms.Padding(0);
            this.CancelButton = this.Exit;
            this.ClientSize = new System.Drawing.Size(494, 256);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.loadingMaterial);
            this.Controls.Add(this.curve);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(494, 492);
            this.MinimumSize = new System.Drawing.Size(494, 256);
            this.Name = "Property";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "媒体信息";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMove);
            this.panel1.ResumeLayout(false);
            this.curve.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private TSkin.TBut Exit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private TSkin.Curve curve;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timBk;
        private System.Windows.Forms.Label label10;
        private TSkin.LoadingMaterialHorizonta loadingMaterial;
        private System.Windows.Forms.Label label11;
    }
}