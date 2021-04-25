using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class Downitem : UserControl
    {
        public delegate void ItemHandler(Downitem core, string e);
        public event ItemHandler LinkClick;
        public Downitem()
        {
            InitializeComponent();
        }

        private void link_retry_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LinkClick != null)
            {
                LinkClick(this, "retry");
            }
        }

        private void link_open_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LinkClick != null)
            {
                LinkClick(this, "open");
            }
        }

        private void link_del_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LinkClick != null)
            {
                LinkClick(this, "del");
            }
        }

        private void logo_Click(object sender, System.EventArgs e)
        {
            if (LinkClick != null)
            {
                LinkClick(this, "open");
            }
        }
    }
}
