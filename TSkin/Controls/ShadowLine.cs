using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TSkin
{
    [ToolboxBitmap(typeof(Panel))]
    public class ShadowLine : Control
    {
        public ShadowLine()
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

        #region 阴影

        private bool _cenabled = true;
        [Category("外观"), Description("是否开启阴影"), DefaultValue(true)]
        public bool CEnabled
        {
            get
            {
                return _cenabled;
            }
            set
            {
                if (_cenabled != value)
                {
                    _cenabled = value;
                    if (value)
                    {
                        OnSizeChanged(null);
                    }
                    this.Invalidate();
                }
            }
        }

        private int _width = 6;
        [Category("外观"), Description("宽度"), DefaultValue(6)]
        public int CWidth
        {
            get
            {
                return _width;
            }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    //rects.Location = new Point(_margin, _margin);
                    OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        private Color color = Color.FromArgb(30, 0, 0, 0);

        [Category("外观"), Description("阴影颜色"), DefaultValue(typeof(Color), "30, 0, 0, 0")]
        public Color Color { get { return color; } set { color = value; this.Invalidate(); } }


        private ShadowType type = ShadowType.BUTTON;

        [Category("外观"), Description("阴影样式"), DefaultValue(typeof(ShadowType), "BUTTON")]
        public ShadowType Type { get { return type; } set { type = value; this.Invalidate(); } }
        public enum ShadowType
        {
            TOP,
            BUTTON,
            LEFT,
            RIGHT
        }

        #endregion


        #region 角度

        private bool _aenabled = false;
        [Category("外观"), Description("是否开启角度"), DefaultValue(false)]
        public bool AEnabled
        {
            get
            {
                return _aenabled;
            }
            set
            {
                if (_aenabled != value)
                {
                    _aenabled = value;
                    //rects.Location = new Point(_margin, _margin);

                    //if (value)
                    //{
                    //    OnSizeChanged(null);
                    //}
                    this.Invalidate();
                }
            }
        }

        private int _awidth = 30;
        [Category("外观"), Description("角度宽度"), DefaultValue(30)]
        public int AWidth
        {
            get
            {
                return _awidth;
            }
            set
            {
                if (_awidth != value)
                {
                    _awidth = value;
                    //rects.Location = new Point(_margin, _margin);
                    OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        private SolidBrush acolor = new SolidBrush(Color.White);

        [Category("外观"), Description("角度颜色"), DefaultValue(typeof(Color), "White")]
        public Color AColor { get { return acolor.Color; } set { acolor.Color = value; this.Invalidate(); } }


        private ShadowType atype = ShadowType.BUTTON;

        [Category("外观"), Description("角度样式"), DefaultValue(typeof(ShadowType), "BUTTON")]
        public ShadowType AType { get { return atype; } set { atype = value; this.Invalidate(); } }


        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_cenabled || _aenabled)
            {
                Rectangle _rect = ClientRectangle;
                if (_rect.Width > 0 && _rect.Height > 0)
                {
                    Graphics g = e.Graphics;

                    if (_cenabled && _width > 0)
                    {
                        LinearGradientBrush brush = null;
                        switch (type)
                        {
                            case ShadowType.TOP:
                                brush = new LinearGradientBrush(new Rectangle(0, 0, _rect.Width, _width), color, Color.Transparent, LinearGradientMode.Vertical);
                                break;
                            case ShadowType.BUTTON:
                                brush = new LinearGradientBrush(new Rectangle(0, _rect.Height - _width, _rect.Width, _width), Color.Transparent, color, LinearGradientMode.Vertical);
                                break;
                            case ShadowType.LEFT:
                                brush = new LinearGradientBrush(new Rectangle(0, 0, _width, _rect.Height), color, Color.Transparent, LinearGradientMode.Horizontal);
                                break;
                            case ShadowType.RIGHT:
                                brush = new LinearGradientBrush(new Rectangle(_rect.Width - _width, 0, _width, _rect.Height), Color.Transparent, color, LinearGradientMode.Horizontal);
                                break;
                        }
                        if (brush != null)
                        {
                            using (brush)
                            {
                                g.FillRectangle(brush, brush.Rectangle);
                            }
                        }
                    }

                    if (_aenabled && _awidth > 0)
                    {
                        g.FillPolygon(acolor, GetCenter(_rect, atype, _awidth));
                    }
                }
            }
            base.OnPaint(e);
        }


        Point[] GetCenter(Rectangle FRect, ShadowType triangle, int size)
        {
            int size_ = size / 2;
            int tempX = FRect.X + FRect.Width / 2, tempY = FRect.Y + FRect.Height / 2;//中心点

            Point[] pntArr = new Point[3];
            switch (triangle)
            {
                case ShadowType.TOP:
                    pntArr[0] = new Point(tempX, FRect.Y + FRect.Height - size_);
                    pntArr[1] = new Point(tempX - size_, FRect.Y + FRect.Height);
                    pntArr[2] = new Point(tempX + size_, FRect.Y + FRect.Height);
                    break;
                case ShadowType.BUTTON:
                    pntArr[0] = new Point(tempX, size_);
                    pntArr[1] = new Point(tempX - size_, FRect.Y);
                    pntArr[2] = new Point(tempX + size_, FRect.Y);
                    break;
                case ShadowType.LEFT:
                    pntArr[0] = new Point(FRect.X + FRect.Width - size_, tempY);
                    pntArr[1] = new Point(FRect.X + FRect.Width, tempY + size_);
                    pntArr[2] = new Point(FRect.X + FRect.Width, tempY - size_);
                    break;
                case ShadowType.RIGHT:
                    pntArr[0] = new Point(size_, tempY);
                    pntArr[1] = new Point(FRect.X, tempY + size_);
                    pntArr[2] = new Point(FRect.X, tempY - size_);
                    break;
            }
            return pntArr;
        }
    }
}
