namespace TPlayer.Frm
{
    partial class SettingP7
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_no_pie_desc = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_DownloadCacheCount = new System.Windows.Forms.Label();
            this.label_DownloadCount = new System.Windows.Forms.Label();
            this.label_DownloadTaskCount = new System.Windows.Forms.Label();
            this.label_DownloadTimeOut = new System.Windows.Forms.Label();
            this.label_DownloadRetryCount = new System.Windows.Forms.Label();
            this.sliderBar_DownloadCount = new TSkin.SliderBar();
            this.sliderBar_DownloadTaskCount = new TSkin.SliderBar();
            this.sliderBar_DownloadCacheCount = new TSkin.SliderBar();
            this.sliderBar_DownloadTimeOut = new TSkin.SliderBar();
            this.sliderBar_DownloadRetryCount = new TSkin.SliderBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "下载设置";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(28, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "同时下载数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(258, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "下载线程数量";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(28, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "下载缓冲数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(258, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "下载重试次数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(28, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "下载超时";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_no_pie_desc
            // 
            this.label_no_pie_desc.AutoEllipsis = true;
            this.label_no_pie_desc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_no_pie_desc.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label_no_pie_desc.ForeColor = System.Drawing.Color.DimGray;
            this.label_no_pie_desc.Location = new System.Drawing.Point(0, 316);
            this.label_no_pie_desc.Name = "label_no_pie_desc";
            this.label_no_pie_desc.Size = new System.Drawing.Size(460, 23);
            this.label_no_pie_desc.TabIndex = 0;
            this.label_no_pie_desc.Text = "下载器无任何额外限制，按照设置满速下载";
            this.label_no_pie_desc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_no_pie_desc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(28, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "下载数量越多可能会造成卡顿";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑",8F);
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(258, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 30);
            this.label8.TabIndex = 0;
            this.label8.Text = "下载线程数量不是越多越好\r\n这里推荐";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_DownloadCacheCount
            // 
            this.label_DownloadCacheCount.Font = new System.Drawing.Font("微软雅黑",10F, System.Drawing.FontStyle.Bold);
            this.label_DownloadCacheCount.Location = new System.Drawing.Point(106, 132);
            this.label_DownloadCacheCount.Name = "label_DownloadCacheCount";
            this.label_DownloadCacheCount.Size = new System.Drawing.Size(130, 20);
            this.label_DownloadCacheCount.TabIndex = 0;
            this.label_DownloadCacheCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_DownloadCacheCount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_DownloadCount
            // 
            this.label_DownloadCount.Font = new System.Drawing.Font("微软雅黑",10F, System.Drawing.FontStyle.Bold);
            this.label_DownloadCount.Location = new System.Drawing.Point(106, 32);
            this.label_DownloadCount.Name = "label_DownloadCount";
            this.label_DownloadCount.Size = new System.Drawing.Size(96, 20);
            this.label_DownloadCount.TabIndex = 0;
            this.label_DownloadCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_DownloadCount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_DownloadTaskCount
            // 
            this.label_DownloadTaskCount.Font = new System.Drawing.Font("微软雅黑",10F, System.Drawing.FontStyle.Bold);
            this.label_DownloadTaskCount.Location = new System.Drawing.Point(336, 32);
            this.label_DownloadTaskCount.Name = "label_DownloadTaskCount";
            this.label_DownloadTaskCount.Size = new System.Drawing.Size(96, 20);
            this.label_DownloadTaskCount.TabIndex = 0;
            this.label_DownloadTaskCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_DownloadTaskCount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_DownloadTimeOut
            // 
            this.label_DownloadTimeOut.Font = new System.Drawing.Font("微软雅黑",10F, System.Drawing.FontStyle.Bold);
            this.label_DownloadTimeOut.Location = new System.Drawing.Point(106, 232);
            this.label_DownloadTimeOut.Name = "label_DownloadTimeOut";
            this.label_DownloadTimeOut.Size = new System.Drawing.Size(96, 20);
            this.label_DownloadTimeOut.TabIndex = 0;
            this.label_DownloadTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_DownloadTimeOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // label_DownloadRetryCount
            // 
            this.label_DownloadRetryCount.Font = new System.Drawing.Font("微软雅黑",10F, System.Drawing.FontStyle.Bold);
            this.label_DownloadRetryCount.Location = new System.Drawing.Point(336, 232);
            this.label_DownloadRetryCount.Name = "label_DownloadRetryCount";
            this.label_DownloadRetryCount.Size = new System.Drawing.Size(96, 20);
            this.label_DownloadRetryCount.TabIndex = 0;
            this.label_DownloadRetryCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_DownloadRetryCount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            // 
            // sliderBar_DownloadCount
            // 
            this.sliderBar_DownloadCount.BackColorLine = System.Drawing.Color.WhiteSmoke;
            this.sliderBar_DownloadCount.Location = new System.Drawing.Point(28, 56);
            this.sliderBar_DownloadCount.MaxValue = 5;
            this.sliderBar_DownloadCount.MinValue = 1;
            this.sliderBar_DownloadCount.Name = "sliderBar_DownloadCount";
            this.sliderBar_DownloadCount.Points = null;
            this.sliderBar_DownloadCount.Size = new System.Drawing.Size(174, 44);
            this.sliderBar_DownloadCount.TabIndex = 1;
            this.sliderBar_DownloadCount.Value = 2;
            // 
            // sliderBar_DownloadTaskCount
            // 
            this.sliderBar_DownloadTaskCount.BackColorLine = System.Drawing.Color.WhiteSmoke;
            this.sliderBar_DownloadTaskCount.Location = new System.Drawing.Point(258, 56);
            this.sliderBar_DownloadTaskCount.MinValue = 1;
            this.sliderBar_DownloadTaskCount.Name = "sliderBar_DownloadTaskCount";
            this.sliderBar_DownloadTaskCount.Points = null;
            this.sliderBar_DownloadTaskCount.Size = new System.Drawing.Size(174, 44);
            this.sliderBar_DownloadTaskCount.TabIndex = 2;
            this.sliderBar_DownloadTaskCount.Value = 2;
            // 
            // sliderBar_DownloadCacheCount
            // 
            this.sliderBar_DownloadCacheCount.BackColorLine = System.Drawing.Color.WhiteSmoke;
            this.sliderBar_DownloadCacheCount.Location = new System.Drawing.Point(28, 156);
            this.sliderBar_DownloadCacheCount.MaxValue = 20480;
            this.sliderBar_DownloadCacheCount.MinValue = 1024;
            this.sliderBar_DownloadCacheCount.Name = "sliderBar_DownloadCacheCount";
            this.sliderBar_DownloadCacheCount.Points = null;
            this.sliderBar_DownloadCacheCount.Size = new System.Drawing.Size(404, 44);
            this.sliderBar_DownloadCacheCount.TabIndex = 3;
            this.sliderBar_DownloadCacheCount.Value = 1024;
            // 
            // sliderBar_DownloadTimeOut
            // 
            this.sliderBar_DownloadTimeOut.BackColorLine = System.Drawing.Color.WhiteSmoke;
            this.sliderBar_DownloadTimeOut.Location = new System.Drawing.Point(28, 256);
            this.sliderBar_DownloadTimeOut.MaxValue = 60000;
            this.sliderBar_DownloadTimeOut.MinValue = 1;
            this.sliderBar_DownloadTimeOut.Name = "sliderBar_DownloadTimeOut";
            this.sliderBar_DownloadTimeOut.Points = null;
            this.sliderBar_DownloadTimeOut.Size = new System.Drawing.Size(174, 44);
            this.sliderBar_DownloadTimeOut.TabIndex = 4;
            this.sliderBar_DownloadTimeOut.Value = 2;
            // 
            // sliderBar_DownloadRetryCount
            // 
            this.sliderBar_DownloadRetryCount.BackColorLine = System.Drawing.Color.WhiteSmoke;
            this.sliderBar_DownloadRetryCount.Location = new System.Drawing.Point(258, 256);
            this.sliderBar_DownloadRetryCount.MaxValue = 40;
            this.sliderBar_DownloadRetryCount.MinValue = 1;
            this.sliderBar_DownloadRetryCount.Name = "sliderBar_DownloadRetryCount";
            this.sliderBar_DownloadRetryCount.Points = null;
            this.sliderBar_DownloadRetryCount.Size = new System.Drawing.Size(174, 44);
            this.sliderBar_DownloadRetryCount.TabIndex = 5;
            this.sliderBar_DownloadRetryCount.Value = 2;
            // 
            // SettingP7
            // 
            this.Controls.Add(this.sliderBar_DownloadRetryCount);
            this.Controls.Add(this.sliderBar_DownloadTimeOut);
            this.Controls.Add(this.sliderBar_DownloadCacheCount);
            this.Controls.Add(this.sliderBar_DownloadTaskCount);
            this.Controls.Add(this.sliderBar_DownloadCount);
            this.Controls.Add(this.label_DownloadRetryCount);
            this.Controls.Add(this.label_DownloadTaskCount);
            this.Controls.Add(this.label_DownloadCount);
            this.Controls.Add(this.label_DownloadCacheCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_no_pie_desc);
            this.Controls.Add(this.label_DownloadTimeOut);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("微软雅黑",10F);
            this.Name = "SettingP7";
            this.Size = new System.Drawing.Size(460, 339);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_no_pie_desc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_DownloadCacheCount;
        private System.Windows.Forms.Label label_DownloadCount;
        private System.Windows.Forms.Label label_DownloadTaskCount;
        private System.Windows.Forms.Label label_DownloadTimeOut;
        private System.Windows.Forms.Label label_DownloadRetryCount;
        private TSkin.SliderBar sliderBar_DownloadCount;
        private TSkin.SliderBar sliderBar_DownloadTaskCount;
        private TSkin.SliderBar sliderBar_DownloadCacheCount;
        private TSkin.SliderBar sliderBar_DownloadTimeOut;
        private TSkin.SliderBar sliderBar_DownloadRetryCount;
    }
}
