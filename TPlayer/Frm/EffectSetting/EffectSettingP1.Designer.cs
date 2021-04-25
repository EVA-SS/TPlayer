namespace TPlayer.Frm
{
    partial class EffectSettingP1
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
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_rende5 = new TSkin.TRadioButton();
            this.btn_rende4 = new TSkin.TRadioButton();
            this.btn_rende3 = new TSkin.TRadioButton();
            this.btn_rende2 = new TSkin.TRadioButton();
            this.btn_rende1 = new TSkin.TRadioButton();
            this.label20 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btn_rende5);
            this.panel6.Controls.Add(this.btn_rende4);
            this.panel6.Controls.Add(this.btn_rende3);
            this.panel6.Controls.Add(this.btn_rende2);
            this.panel6.Controls.Add(this.btn_rende1);
            this.panel6.Location = new System.Drawing.Point(12, 38);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(292, 149);
            this.panel6.TabIndex = 0;
            // 
            // btn_rende5
            // 
            this.btn_rende5.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_rende5.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_rende5.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_rende5.Location = new System.Drawing.Point(0, 120);
            this.btn_rende5.Margin = new System.Windows.Forms.Padding(0);
            this.btn_rende5.Name = "btn_rende5";
            this.btn_rende5.Size = new System.Drawing.Size(292, 30);
            this.btn_rende5.TabIndex = 4;
            this.btn_rende5.TabStop = true;
            this.btn_rende5.Tag = "5";
            this.btn_rende5.Text = "AVR";
            this.btn_rende5.UseVisualStyleBackColor = true;
            this.btn_rende5.Click += new System.EventHandler(this.chang_rende);
            // 
            // btn_rende4
            // 
            this.btn_rende4.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_rende4.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_rende4.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_rende4.Location = new System.Drawing.Point(0, 90);
            this.btn_rende4.Margin = new System.Windows.Forms.Padding(0);
            this.btn_rende4.Name = "btn_rende4";
            this.btn_rende4.Size = new System.Drawing.Size(292, 30);
            this.btn_rende4.TabIndex = 3;
            this.btn_rende4.TabStop = true;
            this.btn_rende4.Tag = "4";
            this.btn_rende4.Text = "增强自渲染 | EVRCP";
            this.btn_rende4.UseVisualStyleBackColor = true;
            this.btn_rende4.Click += new System.EventHandler(this.chang_rende);
            // 
            // btn_rende3
            // 
            this.btn_rende3.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_rende3.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_rende3.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_rende3.Location = new System.Drawing.Point(0, 60);
            this.btn_rende3.Margin = new System.Windows.Forms.Padding(0);
            this.btn_rende3.Name = "btn_rende3";
            this.btn_rende3.Size = new System.Drawing.Size(292, 30);
            this.btn_rende3.TabIndex = 2;
            this.btn_rende3.TabStop = true;
            this.btn_rende3.Tag = "3";
            this.btn_rende3.Text = "增强型 | EVR";
            this.btn_rende3.UseVisualStyleBackColor = true;
            this.btn_rende3.Click += new System.EventHandler(this.chang_rende);
            // 
            // btn_rende2
            // 
            this.btn_rende2.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_rende2.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_rende2.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_rende2.Location = new System.Drawing.Point(0, 30);
            this.btn_rende2.Margin = new System.Windows.Forms.Padding(0);
            this.btn_rende2.Name = "btn_rende2";
            this.btn_rende2.Size = new System.Drawing.Size(292, 30);
            this.btn_rende2.TabIndex = 1;
            this.btn_rende2.TabStop = true;
            this.btn_rende2.Tag = "2";
            this.btn_rende2.Text = "未渲染 | Renderless";
            this.btn_rende2.UseVisualStyleBackColor = true;
            this.btn_rende2.Click += new System.EventHandler(this.chang_rende);
            // 
            // btn_rende1
            // 
            this.btn_rende1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_rende1.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_rende1.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_rende1.Location = new System.Drawing.Point(0, 0);
            this.btn_rende1.Margin = new System.Windows.Forms.Padding(0);
            this.btn_rende1.Name = "btn_rende1";
            this.btn_rende1.Size = new System.Drawing.Size(292, 30);
            this.btn_rende1.TabIndex = 0;
            this.btn_rende1.TabStop = true;
            this.btn_rende1.Tag = "1";
            this.btn_rende1.Text = "覆盖模式 | Overlay";
            this.btn_rende1.UseVisualStyleBackColor = true;
            this.btn_rende1.Click += new System.EventHandler(this.chang_rende);
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(16, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(62, 20);
            this.label20.TabIndex = 0;
            this.label20.Text = "渲染";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(0, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(312, 26);
            this.label5.TabIndex = 0;
            this.label5.Text = "播放器黑屏或功能异常，请重新播放或  修复解码器";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Gainsboro;
            this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Silver;
            this.linkLabel1.Location = new System.Drawing.Point(226, 264);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(73, 26);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "修复解码器";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Silver;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // EffectSettingP1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(31)))));
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label20);
            this.Font = new System.Drawing.Font("微软雅黑",9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "EffectSettingP1";
            this.Size = new System.Drawing.Size(312, 290);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private TSkin.TRadioButton btn_rende5;
        private TSkin.TRadioButton btn_rende4;
        private TSkin.TRadioButton btn_rende3;
        private TSkin.TRadioButton btn_rende2;
        private TSkin.TRadioButton btn_rende1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
