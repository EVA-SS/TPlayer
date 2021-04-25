namespace TPlayer.Frm
{
    partial class SettingP6
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tRadioButton2 = new TSkin.TRadioButton();
            this.tRadioButton3 = new TSkin.TRadioButton();
            this.tRadioButton1 = new TSkin.TRadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tComboBox2 = new TSkin.TComboBox();
            this.check_VREnable = new TSkin.TCheckBox();
            this.check_3D = new TSkin.TCheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "3D 设置";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label5.Location = new System.Drawing.Point(44, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "分色 3D 播放模式，搭配3D眼睛";
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tRadioButton2);
            this.panel1.Controls.Add(this.tRadioButton3);
            this.panel1.Controls.Add(this.tRadioButton1);
            this.panel1.Location = new System.Drawing.Point(44, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 30);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // tRadioButton2
            // 
            this.tRadioButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tRadioButton2.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.tRadioButton2.Location = new System.Drawing.Point(200, 0);
            this.tRadioButton2.Margin = new System.Windows.Forms.Padding(0);
            this.tRadioButton2.Name = "tRadioButton2";
            this.tRadioButton2.Size = new System.Drawing.Size(72, 30);
            this.tRadioButton2.TabIndex = 2;
            this.tRadioButton2.TabStop = true;
            this.tRadioButton2.Text = "上下";
            this.tRadioButton2.UseVisualStyleBackColor = true;
            // 
            // tRadioButton3
            // 
            this.tRadioButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.tRadioButton3.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.tRadioButton3.Location = new System.Drawing.Point(128, 0);
            this.tRadioButton3.Margin = new System.Windows.Forms.Padding(0);
            this.tRadioButton3.Name = "tRadioButton3";
            this.tRadioButton3.Size = new System.Drawing.Size(72, 30);
            this.tRadioButton3.TabIndex = 1;
            this.tRadioButton3.TabStop = true;
            this.tRadioButton3.Text = "左右";
            this.tRadioButton3.UseVisualStyleBackColor = true;
            // 
            // tRadioButton1
            // 
            this.tRadioButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tRadioButton1.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.tRadioButton1.Location = new System.Drawing.Point(0, 0);
            this.tRadioButton1.Margin = new System.Windows.Forms.Padding(0);
            this.tRadioButton1.Name = "tRadioButton1";
            this.tRadioButton1.Size = new System.Drawing.Size(128, 30);
            this.tRadioButton1.TabIndex = 0;
            this.tRadioButton1.TabStop = true;
            this.tRadioButton1.Text = "虚拟(2D转3D)";
            this.tRadioButton1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(28, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "分色模式";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(28, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "分色颜色";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "VR 设置";
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label6.Location = new System.Drawing.Point(44, 306);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(170, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "虚拟现实 / 全景视频";
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // tComboBox2
            // 
            this.tComboBox2.BackColor = System.Drawing.Color.Transparent;
            this.tComboBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tComboBox2.BorderWidth = 1F;
            this.tComboBox2.BackColor = System.Drawing.Color.White;
            this.tComboBox2.BackColor2 = System.Drawing.Color.White;
            this.tComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tComboBox2.DropDownBackColorActive = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.tComboBox2.DropDownForeColorHove = System.Drawing.Color.DimGray;
            this.tComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tComboBox2.ForeColor = System.Drawing.Color.Black;
            this.tComboBox2.FormattingEnabled = true;
            this.tComboBox2.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.tComboBox2.Items.AddRange(new object[] {
            "红青",
            "青红",
            "黄蓝",
            "蓝黄",
            "绿紫",
            "紫绿",
            "红绿",
            "绿红",
            "绿蓝",
            "蓝绿",
            "红蓝,",
            "蓝红"});
            this.tComboBox2.Location = new System.Drawing.Point(44, 190);
            this.tComboBox2.Name = "tComboBox2";
            this.tComboBox2.Radius = 4;
            this.tComboBox2.Size = new System.Drawing.Size(158, 26);
            this.tComboBox2.TabIndex = 2;
            this.tComboBox2.Text = "";
            this.tComboBox2.TextMargin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            // 
            // check_VREnable
            // 
            this.check_VREnable.AutoSize = true;
            this.check_VREnable.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_VREnable.Location = new System.Drawing.Point(28, 276);
            this.check_VREnable.Margin = new System.Windows.Forms.Padding(0);
            this.check_VREnable.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_VREnable.Name = "check_VREnable";
            this.check_VREnable.Size = new System.Drawing.Size(76, 30);
            this.check_VREnable.TabIndex = 3;
            this.check_VREnable.Text = "VR模式";
            this.check_VREnable.UseVisualStyleBackColor = true;
            // 
            // check_3D
            // 
            this.check_3D.AutoSize = true;
            this.check_3D.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.check_3D.Location = new System.Drawing.Point(28, 30);
            this.check_3D.Margin = new System.Windows.Forms.Padding(0);
            this.check_3D.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_3D.Name = "check_3D";
            this.check_3D.Size = new System.Drawing.Size(76, 30);
            this.check_3D.TabIndex = 0;
            this.check_3D.Text = "3D模式";
            this.check_3D.UseVisualStyleBackColor = true;
            // 
            // SettingP6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScroll = true;
            this.Controls.Add(this.tComboBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.check_VREnable);
            this.Controls.Add(this.check_3D);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑",10F);
            this.Name = "SettingP6";
            this.Size = new System.Drawing.Size(460, 339);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private TSkin.TCheckBox check_3D;
        private System.Windows.Forms.Panel panel1;
        private TSkin.TRadioButton tRadioButton2;
        private TSkin.TRadioButton tRadioButton3;
        private TSkin.TRadioButton tRadioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private TSkin.TComboBox tComboBox2;
        private System.Windows.Forms.Label label4;
        private TSkin.TCheckBox check_VREnable;
        private System.Windows.Forms.Label label6;
    }
}
