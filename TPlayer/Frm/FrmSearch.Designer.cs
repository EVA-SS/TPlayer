
namespace TPlayerFrm
{
    partial class FrmSearch
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
            this.panel = new System.Windows.Forms.Panel();
            this.btn_search = new TSkin.TBut();
            this.text_search = new TSkin.TextBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.text_search);
            this.panel.Location = new System.Drawing.Point(137, 63);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(183, 44);
            this.panel.TabIndex = 8;
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            // 
            // btn_search
            // 
            this.btn_search.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_search.BackColor = System.Drawing.Color.Transparent;
            this.btn_search.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_search.BackColor2 = System.Drawing.Color.DodgerBlue;
            this.btn_search.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_search.ImageSize = new System.Drawing.Size(26,26);
            this.btn_search.Location = new System.Drawing.Point(320, 63);
            this.btn_search.Name = "btn_search";
            this.btn_search.Radius = 22;
            this.btn_search.Size = new System.Drawing.Size(44, 44);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // text_search
            // 
            this.text_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.text_search.BackColor = System.Drawing.Color.White;
            this.text_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_search.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.text_search.ForeColor = System.Drawing.Color.Black;
            this.text_search.Location = new System.Drawing.Point(9, 11);
            this.text_search.Name = "text_search";
            this.text_search.ShowCueFocused = true;
            this.text_search.Size = new System.Drawing.Size(164, 22);
            this.text_search.TabIndex = 0;
            this.text_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_search_KeyPress);
            // 
            // FrmSearch
            // 
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.panel);
            this.Name = "FrmSearch";
            this.Size = new System.Drawing.Size(500, 170);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private TSkin.TextBox text_search;
        private TSkin.TBut btn_search;
    }
}
