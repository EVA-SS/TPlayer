using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class Mask : Form
    {
        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        Form form;
        double opacity;
        public Mask(Form form, Color back, double opacity = 0.8)
        {
            this.form = form;
            this.opacity = opacity;
            this.ClientSize = new Size(800, 450);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowIcon = this.ShowInTaskbar = false;
            this.BackColor = back;
        }

        protected override void OnLoad(EventArgs e)
        {
            Form_LSChanged(null, null);
            form.LocationChanged += Form_LSChanged;
            form.SizeChanged += Form_LSChanged;
            AnimateWindow(this.Handle, 100, 0x00080000);
            base.OnLoad(e);
            this.Opacity = opacity;
        }


        private void Form_LSChanged(object sender, EventArgs e)
        {
            this.Location = form.Location;
            this.Size = form.Size;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            form.LocationChanged -= Form_LSChanged;
            form.SizeChanged -= Form_LSChanged;
            this.Opacity = 1;
            base.OnClosing(e);
            AnimateWindow(this.Handle, 300, 0x00010000 | 0x00080000 | 0x00040000);
        }
    }
}
