using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TSkin
{
    public partial class TProg : Control
    {
        public TProg()
        {
            this.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.Selectable |
               ControlStyles.DoubleBuffer | ControlStyles.Selectable |
               ControlStyles.SupportsTransparentBackColor |
               ControlStyles.ContainerControl |
               ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }
        #region 属性  
        public int GetWith()
        {
            Size sizeF = TextRenderer.MeasureText(_Txt, font);
            return sizeF.Width + 20;
        }
        private string _Txt = "";
        [Description("文字"), Category("外观"), DefaultValue("")]
        public string Txt
        {
            get { return _Txt; }
            set
            {
                if (_Txt != value)
                {
                    _Txt = value;
                    this.Invalidate();
                }
            }
        }

        private double _Value = 0;
        public double Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    if (value > _MaxValue)
                    { _Value = _MaxValue; }
                    else
                    {
                        _Value = value;
                    }
                    this.Invalidate();
                }
            }
        }
        private double _MaxValue = 0;
        public double MaxValue
        {
            get { return _MaxValue; }
            set
            {
                if (_MaxValue != value)
                {
                    _MaxValue = value;
                    this.Invalidate();
                }
            }
        }


        int _radius = 0;
        [Description("圆角"), Category("TSkin样式"), DefaultValue(0)]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value;

                    this.Invalidate();
                }
            }
        }
        #endregion
        public bool isErr = false;
        SolidBrush fontbrush = new SolidBrush(Color.Black);
        SolidBrush Efontbrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        SolidBrush backbrush = new SolidBrush(Color.FromArgb(40, 0, 0, 0));
        SolidBrush fbackbrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
        //SolidBrush bluesolidBrush = new SolidBrush(Color.FromArgb(100, 81, 135, 240));
        SolidBrush Limebrush = new SolidBrush(Color.FromArgb(80, 0, 255, 0));
        SolidBrush Redbrush = new SolidBrush(Color.FromArgb(80, 255, 0, 0));

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 32)
            {
                //回车
                e.Handled = true;
                OnClick(EventArgs.Empty);
            }
            base.OnKeyPress(e);
        }

        bool IsHove = false;
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!IsHove)
            {
                IsHove = true;
                this.Invalidate();
            }
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (IsHove)
            {
                IsHove = false;
                this.Invalidate();
            }
            base.OnMouseLeave(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            if (IsHove)
            {
                IsHove = false;
                this.Invalidate();
            }
            base.OnLeave(e);
        }


        Font font = new Font("微软雅黑", 10);
        StringFormat stringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = ClientRectangle;
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            if (_radius < 1)
            {
                g.FillRectangle(IsHove ? fbackbrush : backbrush, rect);
            }
            else
            {
                FillRoundRectangle(g, IsHove ? fbackbrush : backbrush, rect, _radius);
            }

            if (Value != 0 && MaxValue != 0)
            {
                double TTT = rect.Width * (_Value / _MaxValue);
                if (TTT > 4)
                {
                    RectangleF rects = new RectangleF(rect.X, rect.Y, (float)TTT, rect.Height);
                    if (_radius < 1)
                    {
                        g.FillRectangle(isErr ? Redbrush : Limebrush, rects);
                    }
                    else
                    {
                        FillRoundRectangle(g, isErr ? Redbrush : Limebrush, rects, _radius);
                    }
                }
            }
            if (_Txt != "")
            {
                g.DrawString(Txt, font, base.Enabled ? fontbrush : Efontbrush, rect, stringFormat);
            }

            base.OnPaint(e);
        }
        public void FillRoundRectangle(Graphics g, Brush brush, RectangleF rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }
        internal GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }
        public void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }
        internal GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

    }
}
