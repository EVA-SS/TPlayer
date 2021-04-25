using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TSkin
{
    [ToolboxBitmap(typeof(ToolTip))]
    public class MetroToolTip : ToolTip
    {
        #region Fields
        private Color borderColor = Color.DodgerBlue;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }


        [Browsable(false)]
        public new string ToolTipTitle
        {
            get { return base.ToolTipTitle; }
            set { base.ToolTipTitle = ""; }
        }

        [Browsable(false)]
        public new ToolTipIcon ToolTipIcon
        {
            get { return base.ToolTipIcon; }
            set { base.ToolTipIcon = ToolTipIcon.None; }
        }

        #endregion

        #region Constructor

        public MetroToolTip()
        {
            OwnerDraw = true;
            ShowAlways = true;

            Draw += new DrawToolTipEventHandler(MetroToolTip_Draw);
            Popup += new PopupEventHandler(MetroToolTip_Popup);
        }

        #endregion

        #region Management Methods

        public new void SetToolTip(Control control, string caption)
        {
            base.SetToolTip(control, caption);
        }

        private void MetroToolTip_Popup(object sender, PopupEventArgs e)
        {
            //if (e.AssociatedWindow is IMetroForm)
            //{
            //    Style = ((IMetroForm)e.AssociatedWindow).Style;
            //    Theme = ((IMetroForm)e.AssociatedWindow).Theme;
            //    StyleManager = ((IMetroForm)e.AssociatedWindow).StyleManager;
            //}
            //else if (e.AssociatedControl is IMetroControl)
            //{
            //    Style = ((IMetroControl)e.AssociatedControl).Style;
            //    Theme = ((IMetroControl)e.AssociatedControl).Theme;
            //    StyleManager = ((IMetroControl)e.AssociatedControl).StyleManager;
            //}

            e.ToolTipSize = new Size(e.ToolTipSize.Width + 24, e.ToolTipSize.Height + 9);
        }

        private void MetroToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
            using (Pen p = new Pen(borderColor))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
            }

            Font f = new Font("微软雅黑", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
            TextRenderer.DrawText(e.Graphics, e.ToolTipText, f, e.Bounds, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        #endregion
    }
}
