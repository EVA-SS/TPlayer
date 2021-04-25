using System;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class HasDownLoad : NetDimension.WinForm.ModernUIForm
    {
        public HasDownLoad(string title)
        {
            InitializeComponent();
            Text = label1.Text = title;
            tCheckBox1.Checked = SystemSettings.DownloadCompleteExit;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void no_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            SystemSettings.DownloadCompleteExit = tCheckBox1.Checked;
            this.DialogResult = tCheckBox1.Checked ? DialogResult.Retry : DialogResult.Ignore;
        }
    }
}
