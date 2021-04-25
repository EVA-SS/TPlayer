namespace TPlayer.Frm
{
    partial class EffectSettingP4
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
            this.tong1 = new System.Windows.Forms.Label();
            this.tong2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tBut1 = new TSkin.TBut();
            this.btn_add_track = new TSkin.TBut();
            this.tCom_track = new TSkin.TComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_sound_channel4 = new TSkin.TRadioButton();
            this.btn_sound_channel3 = new TSkin.TRadioButton();
            this.btn_sound_channel2 = new TSkin.TRadioButton();
            this.btn_sound_channel1 = new TSkin.TRadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tong = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tong1
            // 
            this.tong1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tong1.Font = new System.Drawing.Font("微软雅黑",12F);
            this.tong1.Location = new System.Drawing.Point(132, 147);
            this.tong1.Name = "tong1";
            this.tong1.Size = new System.Drawing.Size(20, 24);
            this.tong1.TabIndex = 5;
            this.tong1.Text = "+";
            this.tong1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tong2
            // 
            this.tong2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tong2.Font = new System.Drawing.Font("微软雅黑",12F);
            this.tong2.Location = new System.Drawing.Point(273, 147);
            this.tong2.Name = "tong2";
            this.tong2.Size = new System.Drawing.Size(20, 24);
            this.tong2.TabIndex = 6;
            this.tong2.Text = "-";
            this.tong2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(16, 124);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 20);
            this.label16.TabIndex = 0;
            this.label16.Text = "同步";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tBut1
            // 
            this.tBut1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.tBut1.BorderWidth = 1F;
            this.tBut1.Location = new System.Drawing.Point(16, 147);
            this.tBut1.MaxValue = 0D;
            this.tBut1.Name = "tBut1";
            this.tBut1.Size = new System.Drawing.Size(66, 24);
            this.tBut1.TabIndex = 6;
            this.tBut1.Tag = "0";
            this.tBut1.Text = "默认";
            this.tBut1.Value = 0D;
            // 
            // btn_add_track
            // 
            this.btn_add_track.BorderWidth = 0F;
            this.btn_add_track.Location = new System.Drawing.Point(222, 72);
            this.btn_add_track.MaxValue = 0D;
            this.btn_add_track.Name = "btn_add_track";
            this.btn_add_track.Radius = 18;
            this.btn_add_track.Size = new System.Drawing.Size(74, 20);
            this.btn_add_track.TabIndex = 2;
            this.btn_add_track.Tag = "0";
            this.btn_add_track.Text = "添加音轨";
            this.btn_add_track.Value = 0D;
            this.btn_add_track.Click += new System.EventHandler(this.btn_add_track_Click);
            // 
            // tCom_track
            // 
            this.tCom_track.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tCom_track.ArrowColorHover = System.Drawing.Color.White;
            this.tCom_track.BackColor = System.Drawing.Color.Transparent;
            this.tCom_track.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tCom_track.BorderWidth = 1F;
            this.tCom_track.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(63)))));
            this.tCom_track.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tCom_track.DropDownBackColorActive = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.tCom_track.DropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.tCom_track.DropDownForeColorActive = System.Drawing.Color.White;
            this.tCom_track.DropDownForeColor = System.Drawing.Color.White;
            this.tCom_track.DropDownForeColorHove = System.Drawing.Color.WhiteSmoke;
            this.tCom_track.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tCom_track.ForeColor = System.Drawing.Color.White;
            this.tCom_track.FormattingEnabled = true;
            this.tCom_track.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.tCom_track.Location = new System.Drawing.Point(16, 96);
            this.tCom_track.Name = "tCom_track";
            this.tCom_track.Radius = 4;
            this.tCom_track.Size = new System.Drawing.Size(280, 24);
            this.tCom_track.TabIndex = 1;
            this.tCom_track.Text = "";
            this.tCom_track.TextMargin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_sound_channel4);
            this.panel2.Controls.Add(this.btn_sound_channel3);
            this.panel2.Controls.Add(this.btn_sound_channel2);
            this.panel2.Controls.Add(this.btn_sound_channel1);
            this.panel2.Location = new System.Drawing.Point(12, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(292, 30);
            this.panel2.TabIndex = 0;
            // 
            // btn_sound_channel4
            // 
            this.btn_sound_channel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_sound_channel4.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_sound_channel4.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_sound_channel4.Location = new System.Drawing.Point(210, 0);
            this.btn_sound_channel4.Margin = new System.Windows.Forms.Padding(0);
            this.btn_sound_channel4.Name = "btn_sound_channel4";
            this.btn_sound_channel4.Size = new System.Drawing.Size(82, 30);
            this.btn_sound_channel4.TabIndex = 3;
            this.btn_sound_channel4.TabStop = true;
            this.btn_sound_channel4.Tag = "3";
            this.btn_sound_channel4.Text = "左右混合\r\n";
            this.btn_sound_channel4.UseVisualStyleBackColor = true;
            this.btn_sound_channel4.Click += new System.EventHandler(this.change_sound_channel);
            // 
            // btn_sound_channel3
            // 
            this.btn_sound_channel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_sound_channel3.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_sound_channel3.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_sound_channel3.Location = new System.Drawing.Point(140, 0);
            this.btn_sound_channel3.Margin = new System.Windows.Forms.Padding(0);
            this.btn_sound_channel3.Name = "btn_sound_channel3";
            this.btn_sound_channel3.Size = new System.Drawing.Size(70, 30);
            this.btn_sound_channel3.TabIndex = 2;
            this.btn_sound_channel3.TabStop = true;
            this.btn_sound_channel3.Tag = "2";
            this.btn_sound_channel3.Text = "右音道";
            this.btn_sound_channel3.UseVisualStyleBackColor = true;
            this.btn_sound_channel3.Click += new System.EventHandler(this.change_sound_channel);
            // 
            // btn_sound_channel2
            // 
            this.btn_sound_channel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_sound_channel2.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_sound_channel2.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_sound_channel2.Location = new System.Drawing.Point(70, 0);
            this.btn_sound_channel2.Margin = new System.Windows.Forms.Padding(0);
            this.btn_sound_channel2.Name = "btn_sound_channel2";
            this.btn_sound_channel2.Size = new System.Drawing.Size(70, 30);
            this.btn_sound_channel2.TabIndex = 1;
            this.btn_sound_channel2.TabStop = true;
            this.btn_sound_channel2.Tag = "1";
            this.btn_sound_channel2.Text = "左音道";
            this.btn_sound_channel2.UseVisualStyleBackColor = true;
            this.btn_sound_channel2.Click += new System.EventHandler(this.change_sound_channel);
            // 
            // btn_sound_channel1
            // 
            this.btn_sound_channel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_sound_channel1.DownColor = System.Drawing.Color.DodgerBlue;
            this.btn_sound_channel1.Font = new System.Drawing.Font("微软雅黑",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_sound_channel1.Location = new System.Drawing.Point(0, 0);
            this.btn_sound_channel1.Margin = new System.Windows.Forms.Padding(0);
            this.btn_sound_channel1.Name = "btn_sound_channel1";
            this.btn_sound_channel1.Size = new System.Drawing.Size(70, 30);
            this.btn_sound_channel1.TabIndex = 0;
            this.btn_sound_channel1.TabStop = true;
            this.btn_sound_channel1.Tag = "0";
            this.btn_sound_channel1.Text = "立体声";
            this.btn_sound_channel1.UseVisualStyleBackColor = true;
            this.btn_sound_channel1.Click += new System.EventHandler(this.change_sound_channel);
            // 
            // label24
            // 
            this.label24.AutoEllipsis = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.ForeColor = System.Drawing.Color.Gray;
            this.label24.Location = new System.Drawing.Point(0, 264);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(312, 26);
            this.label24.TabIndex = 0;
            this.label24.Text = "若声音处理功能无法使用 请检查解码器";
            this.label24.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label24.Visible = false;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(16, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 20);
            this.label15.TabIndex = 0;
            this.label15.Text = "音轨";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(16, 16);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(62, 20);
            this.label27.TabIndex = 0;
            this.label27.Text = "音道选择";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel4.Controls.Add(this.tong);
            this.panel4.Location = new System.Drawing.Point(152, 147);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(116, 24);
            this.panel4.TabIndex = 4;
            // 
            // tong
            // 
            this.tong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.tong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tong.Font = new System.Drawing.Font("微软雅黑",10F);
            this.tong.ForeColor = System.Drawing.Color.White;
            this.tong.Location = new System.Drawing.Point(0, 3);
            this.tong.Name = "tong";
            this.tong.Size = new System.Drawing.Size(116, 18);
            this.tong.TabIndex = 0;
            this.tong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // EffectSettingP4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(31)))));
            this.Controls.Add(this.tong1);
            this.Controls.Add(this.tong2);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.tBut1);
            this.Controls.Add(this.btn_add_track);
            this.Controls.Add(this.tCom_track);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("微软雅黑",9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "EffectSettingP4";
            this.Size = new System.Drawing.Size(312, 290);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label tong1;
        private System.Windows.Forms.Label tong2;
        private System.Windows.Forms.Label label16;
        private TSkin.TBut tBut1;
        private TSkin.TBut btn_add_track;
        private TSkin.TComboBox tCom_track;
        private System.Windows.Forms.Panel panel2;
        private TSkin.TRadioButton btn_sound_channel4;
        private TSkin.TRadioButton btn_sound_channel3;
        private TSkin.TRadioButton btn_sound_channel2;
        private TSkin.TRadioButton btn_sound_channel1;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tong;
    }
}
