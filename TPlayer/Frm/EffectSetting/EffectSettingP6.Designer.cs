namespace TPlayer.Frm
{
    partial class EffectSettingP6
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
            this.slideButton1 = new TSkin.SlideButton();
            this.tComboBox2 = new TSkin.TComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tRadioButton2 = new TSkin.TRadioButton();
            this.tRadioButton3 = new TSkin.TRadioButton();
            this.tRadioButton1 = new TSkin.TRadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // slideButton1
            // 
            this.slideButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.slideButton1.IsOn = false;
            this.slideButton1.Location = new System.Drawing.Point(260, 18);
            this.slideButton1.Name = "slideButton1";
            this.slideButton1.Size = new System.Drawing.Size(28, 16);
            this.slideButton1.TabIndex = 0;
            // 
            // tComboBox2
            // 
            this.tComboBox2.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tComboBox2.ArrowColorHover = System.Drawing.Color.White;
            this.tComboBox2.BackColor = System.Drawing.Color.Transparent;
            this.tComboBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tComboBox2.BorderWidth = 1F;
            this.tComboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(63)))));
            this.tComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tComboBox2.DropDownBackColorActive = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.tComboBox2.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.tComboBox2.DropDownForeColorActive = System.Drawing.Color.White;
            this.tComboBox2.DropDownForeColor = System.Drawing.Color.White;
            this.tComboBox2.DropDownForeColorHove = System.Drawing.Color.WhiteSmoke;
            this.tComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tComboBox2.ForeColor = System.Drawing.Color.White;
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
            this.tComboBox2.Location = new System.Drawing.Point(16, 40);
            this.tComboBox2.Name = "tComboBox2";
            this.tComboBox2.Radius = 4;
            this.tComboBox2.Size = new System.Drawing.Size(280, 24);
            this.tComboBox2.TabIndex = 1;
            this.tComboBox2.Text = "";
            this.tComboBox2.TextMargin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(16, 16);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(87, 20);
            this.label23.TabIndex = 0;
            this.label23.Text = "3D";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "分色模式";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tRadioButton2);
            this.panel1.Controls.Add(this.tRadioButton3);
            this.panel1.Controls.Add(this.tRadioButton1);
            this.panel1.Location = new System.Drawing.Point(19, 94);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 30);
            this.panel1.TabIndex = 2;
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
            // EffectSettingP6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(31)))));
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.slideButton1);
            this.Controls.Add(this.tComboBox2);
            this.Controls.Add(this.label23);
            this.Font = new System.Drawing.Font("微软雅黑",9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "EffectSettingP6";
            this.Size = new System.Drawing.Size(312, 290);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TSkin.SlideButton slideButton1;
        private TSkin.TComboBox tComboBox2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private TSkin.TRadioButton tRadioButton2;
        private TSkin.TRadioButton tRadioButton3;
        private TSkin.TRadioButton tRadioButton1;
    }
}
