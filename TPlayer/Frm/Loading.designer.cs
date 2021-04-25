namespace TPlayer
{
    partial class Loading
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timBk = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timBk
            // 
            this.timBk.Enabled = true;
            this.timBk.Interval = 1000;
            this.timBk.Tick += new System.EventHandler(this.timBk_Tick);
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = Properties.Resources.TLogo;
            this.Name = "Loading";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timBk;
        private System.ComponentModel.IContainer components;
    }
}