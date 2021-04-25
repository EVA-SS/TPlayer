using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace TSkin
{
    public class TMenuStrip : ContextMenuStrip
    {
        //internal AnimationManager animationManager;
        //internal Point animationSource;

        //public delegate void ItemClickStart(object sender, ToolStripItemClickedEventArgs e);
        //public event ItemClickStart OnItemClickStart;

        public TMenuStrip()
        {
            Renderer = new TlToolStripRender();
            //animationManager = new AnimationManager(false)
            //{
            //    Increment = 0.07,
            //    AnimationType = AnimationType.Linear
            //};
            //animationManager.OnAnimationProgress += sender => Invalidate();
            //animationManager.OnAnimationFinished += sender => OnItemClicked(delayesArgs);

            //BackColor = SkinManager.GetApplicationBackgroundColor();
        }

        //protected override void OnMouseUp(MouseEventArgs mea)
        //{
        //    base.OnMouseUp(mea);

        //    animationSource = mea.Location;
        //}

        //private ToolStripItemClickedEventArgs delayesArgs;
        //protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        //{
        //    if (e.ClickedItem != null && !(e.ClickedItem is ToolStripSeparator))
        //    {
        //        if (e == delayesArgs)
        //        {
        //            //The event has been fired manualy because the args are the ones we saved for delay
        //            base.OnItemClicked(e);
        //        }
        //        else
        //        {
        //            //Interrupt the default on click, saving the args for the delay which is needed to display the animaton
        //            delayesArgs = e;

        //            //Fire custom event to trigger actions directly but keep cms open
        //            if (OnItemClickStart != null) OnItemClickStart(this, e);

        //            //Start animation
        //            animationManager.StartNewAnimation(AnimationDirection.In);
        //        }
        //    }
        //}
    }

    public class TToolStripMenuItem : ToolStripMenuItem
    {
        //public TToolStripMenuItem()
        //{
        //    AutoSize = false;
        //    Size = new Size(120, 30);
        //}

        protected override ToolStripDropDown CreateDefaultDropDown()
        {
            var baseDropDown = base.CreateDefaultDropDown();
            if (DesignMode) return baseDropDown;

            var defaultDropDown = new TMenuStrip();
            defaultDropDown.Items.AddRange(baseDropDown.Items);

            return defaultDropDown;
        }
    }

    internal class TlToolStripRender : ToolStripProfessionalRenderer
    {
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            Rectangle t = new Rectangle(new Point((int)e.Graphics.ClipBounds.X, 1), new Size((int)e.Graphics.ClipBounds.Width, 1));
            //var itemRect = e.Graphics.ClipBounds;
            g.FillRectangle(new SolidBrush(Color.FromArgb(250, 250, 250)), t);
            //base.OnRenderSeparator(e);
        }
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            //var g = e.Graphics;
            //g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //var itemRect = GetItemRect(e.Item);
            //var textRect = new Rectangle(24, itemRect.Y, itemRect.Width - (24 + 16), itemRect.Height);
            //g.DrawString(
            //    e.Text, 
            //    e.TextFont, 
            //    e.Item.Enabled ? SkinManager.GetPrimaryTextBrush() : SkinManager.GetDisabledOrHintBrush(),
            //    textRect, 
            //    new StringFormat { LineAlignment = StringAlignment.Center });

            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var itemRect = GetItemRect(e.Item);
            var textRect = new Rectangle(30, itemRect.Y, itemRect.Width - (24 + 16), itemRect.Height);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

            g.DrawString(e.Text, e.TextFont, e.Item.Enabled ? (e.Item.Selected ? FontColor2 : FontColor1) : FontColor3, textRect, new StringFormat { LineAlignment = StringAlignment.Center });
        }
        public SolidBrush FontColor1 = new SolidBrush(Color.FromArgb(50, 50, 50));
        public SolidBrush FontColor2 = new SolidBrush(Color.White);
        public SolidBrush FontColor3 = new SolidBrush(Color.FromArgb(60, 170, 255));
        Color coo = Color.FromArgb(60, 170, 255);
        //自定义鼠标放上去选项卡颜色
        Color _HoverBackColor = Color.FromArgb(51, 168, 255);
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            //Brush H = new SolidBrush(_HoverBackColor);

            //Draw background
            var itemRect = GetItemRect(e.Item);
            Brush myBrush = new LinearGradientBrush(itemRect, _HoverBackColor, coo, LinearGradientMode.Horizontal);
            //g.FillRectangle(myBrush, j);
            g.FillRectangle(e.Item.Selected && e.Item.Enabled ? myBrush : FontColor2, itemRect);

            //Ripple animation
            //var toolStrip = e.ToolStrip as TContextMenuStrip;
            //if (toolStrip != null)
            //{
            //    var animationManager = toolStrip.animationManager;
            //    var animationSource = toolStrip.animationSource;
            //    if (toolStrip.animationManager.IsAnimating() && e.Item.Bounds.Contains(animationSource))
            //    {
            //        for (int i = 0; i < animationManager.GetAnimationCount(); i++)
            //        {
            //            var animationValue = animationManager.GetProgress(i);
            //            var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationValue * 50)), Color.Black));
            //            var rippleSize = (int)(animationValue * itemRect.Width * 2.5);
            //            g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, itemRect.Y - itemRect.Height, rippleSize, itemRect.Height * 3));
            //        }
            //    }
            //}
        }


        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            var g = e.Graphics;
            g.DrawRectangle(
                new Pen(Color.FromArgb(60, 0, 0, 0)),//自定义边框颜色
                new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
        }

        private Rectangle GetItemRect(ToolStripItem item)
        {
            return new Rectangle(0, item.ContentRectangle.Y, item.ContentRectangle.Width + 4, item.ContentRectangle.Height);
        }
    }
}
