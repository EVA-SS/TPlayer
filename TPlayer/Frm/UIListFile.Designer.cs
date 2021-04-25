namespace TPlayer.Frm
{
    partial class UIListFile
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.titleCheckControl1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Label();
            this.btn_no = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.titleCheckControl1);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.btn_no);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 348);
            this.panel2.TabIndex = 1;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // titleCheckControl1
            // 
            this.titleCheckControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleCheckControl1.Font = new System.Drawing.Font("微软雅黑",11F);
            this.titleCheckControl1.Location = new System.Drawing.Point(0, 30);
            this.titleCheckControl1.Name = "titleCheckControl1";
            this.titleCheckControl1.Size = new System.Drawing.Size(340, 271);
            this.titleCheckControl1.TabIndex = 0;
            this.titleCheckControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Font = new System.Drawing.Font("微软雅黑",11F);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(340, 30);
            this.panel5.TabIndex = 0;
            this.panel5.Text = " 查看文件";
            this.panel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.panel5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // btn_no
            // 
            this.btn_no.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_no.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_no.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btn_no.FlatAppearance.BorderSize = 0;
            this.btn_no.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_no.Font = new System.Drawing.Font("微软雅黑",12F);
            this.btn_no.Location = new System.Drawing.Point(0, 301);
            this.btn_no.Name = "btn_no";
            this.btn_no.Size = new System.Drawing.Size(340, 47);
            this.btn_no.TabIndex = 0;
            this.btn_no.Text = "取消";
            this.btn_no.UseVisualStyleBackColor = true;
            this.btn_no.Click += new System.EventHandler(this.btn_no_Click);
            // 
            // UIListFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btn_no;
            this.CanResize = true;
            this.ClientSize = new System.Drawing.Size(340, 348);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("微软雅黑",10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = global::TPlayer.Properties.Resources.TLogo;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(300, 30);
            this.Name = "UIListFile";
            this.Radius = 30;
            this.RoundStyle = ((TSkin.UICornerRadiusSides)((((TSkin.UICornerRadiusSides.LeftTop | TSkin.UICornerRadiusSides.RightTop) 
            | TSkin.UICornerRadiusSides.RightBottom) 
            | TSkin.UICornerRadiusSides.LeftBottom)));
            this.ShadowWidth = 8;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel titleCheckControl1;
        private System.Windows.Forms.Label panel5;
        private System.Windows.Forms.Button btn_no;
    }
}