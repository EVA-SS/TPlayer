using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TSkin
{
    public partial class TComboBox : ComboBox
    {
        public TComboBox()
        {
            _borderFocus.DashStyle = DashStyle.Dash;
            this.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.Selectable |
               ControlStyles.DoubleBuffer |
               ControlStyles.SupportsTransparentBackColor |
               ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);

            base.BackColor = Color.Transparent;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            base.DrawMode = DrawMode.OwnerDrawFixed;
            this.UpdateStyles();
        }

        #region 属性

        #region 不可修改属性

        [Browsable(false)]
        public new ComboBoxStyle DropDownStyle
        {
            get
            {
                return ComboBoxStyle.DropDownList;
            }
            set { }
        }
        [Browsable(false)]
        public new DrawMode DrawMode
        {
            get
            {
                return DrawMode.OwnerDrawFixed;
            }
            set { }
        }


        #endregion

        #region 普通状态

        #region 文字

        [Description("文字"), Category("外观-普通"), DefaultValue(null)]
        public override string Text
        {
            get => base.Text;
            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        protected override void OnCreateControl()
        {
            forebrush.Color = base.ForeColor;
            base.OnCreateControl();
        }
        protected override void OnForeColorChanged(EventArgs e)
        {
            if (base.IsHandleCreated)
            {
                if (base.ForeColor != forebrush.Color)
                {
                    forebrush.Color = base.ForeColor;
                    this.Invalidate();
                }
            }
            base.OnForeColorChanged(e);
        }


        [Description("字体颜色"), Category("外观-普通")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                if (base.ForeColor != value)
                {
                    base.ForeColor = forebrush.Color = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        Padding _TextMargin = new Padding(0);
        [Description("文字边距"), Category("外观-普通"), DefaultValue(typeof(Padding), "0, 0, 0, 0")]
        public Padding TextMargin
        {
            get { return _TextMargin; }
            set
            {
                if (_TextMargin != value)
                {
                    _TextMargin = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        private ContentAlignment _TextAlign = ContentAlignment.MiddleLeft;
        [Description("字体位置"), Category("外观-普通"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment TextAlign
        {
            get => _TextAlign;
            set
            {
                if (_TextAlign != value)
                {
                    _TextAlign = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        Color backcolor1 = Color.FromArgb(100, 0, 0, 0);
        Color backcolor2 = Color.FromArgb(100, 0, 0, 0);
        [Description("背景颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "100, 0, 0, 0")]
        public new Color BackColor
        {
            get => backcolor1;
            set
            {
                if (backcolor1 != value)
                {
                    backcolor1 = value;
                    SetBrush(ref backbrush, backcolor1, backcolor2);
                    this.Invalidate();
                }
            }
        }

        [Description("背景渐变色"), Category("外观-普通"), DefaultValue(typeof(Color), "100, 0, 0, 0")]
        public Color BackColor2
        {
            get => backcolor2;
            set
            {
                if (backcolor2 != value)
                {
                    backcolor2 = value;
                    SetBrush(ref backbrush, backcolor1, backcolor2);
                    this.Invalidate();
                }
            }
        }


        [Description("边框颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "Transparent")]
        public Color BorderColor
        {
            get => _border.Color;
            set
            {
                if (_border.Color != value)
                {
                    _border.Color = value;
                    this.Invalidate();
                }
            }
        }

        float _BorderWidth = 0;
        [Description("边框宽度"), Category("外观-普通"), DefaultValue(0)]
        public float BorderWidth
        {
            get { return _BorderWidth; }
            set
            {
                if (_BorderWidth != value)
                {
                    _border.Width = value;
                    _BorderWidth = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        int _radius = 0;
        [Description("圆角"), Category("外观-普通"), DefaultValue(0)]
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


        SolidBrush _arrowColor = new SolidBrush(Color.Black);
        [Description("箭头颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "Black")]
        public Color ArrowColor
        {
            get => _arrowColor.Color;
            set
            {
                if (_arrowColor.Color != value)
                {
                    _arrowColor.Color = value;
                    this.Invalidate();
                }
            }
        }


        SolidBrush _dropDownForeColor = new SolidBrush(Color.Black);
        [Description("默认下拉文字颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "Black")]
        public Color DropDownForeColor
        {
            get => _dropDownForeColor.Color;
            set
            {
                if (_dropDownForeColor.Color != value)
                {
                    _dropDownForeColor.Color = value;
                    this.Invalidate();
                }
            }
        }


        SolidBrush _dropDownBackColor = new SolidBrush(Color.White);
        [Description("默认下拉背景颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "White")]
        public Color DropDownBackColor
        {
            get => _dropDownBackColor.Color;
            set
            {
                if (_dropDownBackColor.Color != value)
                {
                    _dropDownBackColor.Color = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region 移动

        SolidBrush backhovebrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
        [Description("鼠标移入颜色"), Category("外观-移动"), DefaultValue(typeof(Color), "20, 0, 0, 0")]
        public Color BackColorHover
        {
            get => backhovebrush.Color;
            set
            {
                if (backhovebrush.Color != value)
                {
                    backhovebrush.Color = value;
                    if (IsHove)
                    {
                        this.Invalidate();
                    }
                }
            }
        }


        SolidBrush _arrowHoverColor = new SolidBrush(Color.Black);
        [Description("箭头颜色"), Category("外观-移动"), DefaultValue(typeof(Color), "Black")]
        public Color ArrowColorHover
        {
            get => _arrowHoverColor.Color;
            set
            {
                if (_arrowHoverColor.Color != value)
                {
                    _arrowHoverColor.Color = value;
                    this.Invalidate();
                }
            }
        }


        SolidBrush _dropDownForeHoverColor = new SolidBrush(Color.Black);
        [Description("下拉选中文字颜色"), Category("外观-移动"), DefaultValue(typeof(Color), "Black")]
        public Color DropDownForeColorHove
        {
            get => _dropDownForeHoverColor.Color;
            set
            {
                if (_dropDownForeHoverColor.Color != value)
                {
                    _dropDownForeHoverColor.Color = value;
                    this.Invalidate();
                }
            }
        }

        SolidBrush _dropDownHoverBackColor = new SolidBrush(Color.WhiteSmoke);
        [Description("下拉选中背景颜色"), Category("外观-移动"), DefaultValue(typeof(Color), "WhiteSmoke")]
        public Color DropDownBackColorHover
        {
            get => _dropDownHoverBackColor.Color;
            set
            {
                if (_dropDownHoverBackColor.Color != value)
                {
                    _dropDownHoverBackColor.Color = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region 焦点

        [Description("焦点边框颜色"), Category("外观-焦点"), DefaultValue(typeof(Color), "200, 0, 0, 0")]
        public Color BorderColorFocus
        {
            get => _borderFocus.Color;
            set
            {
                if (_borderFocus.Color != value)
                {
                    _borderFocus.Color = value;
                    if (IsFocus)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        #endregion

        #region 激活

        SolidBrush _dropDownForeActiveColor = new SolidBrush(Color.Black);
        [Description("下拉选中文字颜色"), Category("外观-激活"), DefaultValue(typeof(Color), "Black")]
        public Color DropDownForeColorActive
        {
            get => _dropDownForeActiveColor.Color;
            set
            {
                if (_dropDownForeActiveColor.Color != value)
                {
                    _dropDownForeActiveColor.Color = value;
                    this.Invalidate();
                }
            }
        }

        SolidBrush _dropDownActiveBackColor = new SolidBrush(Color.WhiteSmoke);
        [Description("下拉激活背景颜色"), Category("外观-激活"), DefaultValue(typeof(Color), "WhiteSmoke")]
        public Color DropDownBackColorActive
        {
            get => _dropDownActiveBackColor.Color;
            set
            {
                if (_dropDownActiveBackColor.Color != value)
                {
                    _dropDownActiveBackColor.Color = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #endregion

        void SetBrush(ref Brush brush, Color color1, Color color2)
        {
            bool isOk = false;
            if (color1 == color2)
            {
                if (brush != null)
                {
                    brush.Dispose();
                }
                brush = new SolidBrush(color1);
            }
            else if (color1 != Color.Transparent || color2 != Color.Transparent)
            {
                if (rectmF.Width > 0 && rectmF.Height > 0)
                {
                    isOk = true;
                    if (brush != null)
                    {
                        brush.Dispose();
                    }
                    brush = new LinearGradientBrush(rectmF, color1, color2, LinearGradientMode.Horizontal);
                }
            }
            if (!isOk)
            {
                if (brush != null)
                {
                    if (brush is SolidBrush && (brush as SolidBrush).Color == color1)
                    {
                        return;
                    }
                    else
                    {
                        brush.Dispose();
                    }
                }
                brush = new SolidBrush(color1);
            }
        }

        bool IsHove = false, IsFocus = false;

        #region 移动

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

        #endregion

        #region 焦点

        protected override void OnGotFocus(EventArgs e)
        {
            IsFocus = true;
            this.Invalidate();
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            IsFocus = false;
            this.Invalidate();
            base.OnLostFocus(e);
        }

        #endregion

        #region 坐标布局

        StringFormat stringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };

        Rectangle rectmF;

        Rectangle rectByFont;
        protected override void OnSizeChanged(EventArgs e)
        {
            Rectangle rect = ClientRectangle;


            //Size fontSize = TextRenderer.MeasureText(string.IsNullOrEmpty(this.Text) ? "Qq森甩" : this.Text, this.Font);
            rectByFont = new Rectangle(_TextMargin.Left, _TextMargin.Top, rect.Width - (_TextMargin.Left + _TextMargin.Right), rect.Height - (_TextMargin.Top + _TextMargin.Bottom));

            switch (_TextAlign)
            {
                case ContentAlignment.TopLeft:
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    //内容在垂直方向上顶部对齐，在水平方向上左边对齐
                    break;
                case ContentAlignment.TopCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    //内容在垂直方向上顶部对齐，在水平方向上居中对齐

                    break;
                case ContentAlignment.TopRight:
                    //内容在垂直方向上顶部对齐，在水平方向上右边对齐
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                    //内容在垂直方向上中间对齐，在水平方向上左边对齐
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    break;
                case ContentAlignment.MiddleCenter:
                    //内容在垂直方向上中间对齐，在水平方向上居中对齐
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    //内容在垂直方向上中间对齐，在水平方向上右边对齐
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    break;
                case ContentAlignment.BottomLeft:
                    //内容在垂直方向上底边对齐，在水平方向上左边对齐
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    //内容在垂直方向上底边对齐，在水平方向上居中对齐
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Far;

                    break;
                case ContentAlignment.BottomRight:
                    //内容在垂直方向上底边对齐，在水平方向上右边对齐
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
            }

            rectmF = new Rectangle(0, 0, rect.Width - 1, rect.Height - 1);

            SetBrush(ref backbrush, backcolor1, backcolor2);

            base.OnSizeChanged(e);
        }

        #endregion

        Pen _border = new Pen(Color.Transparent, 1);
        Pen _borderFocus = new Pen(Color.FromArgb(200, 0, 0, 0), 1);
        SolidBrush forebrush = new SolidBrush(Color.Black);
        Brush backbrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));

        #region 绘制控件 

        int triangleSize = 6;
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                //g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                if (e.State.HasFlag(DrawItemState.Selected))
                {
                    g.FillRectangle(_dropDownActiveBackColor, e.Bounds);
                    g.DrawString(GetItemText(Items[e.Index]), e.Font, _dropDownForeActiveColor, e.Bounds, stringFormat);
                }
                else if (e.State.HasFlag(DrawItemState.Focus))
                {
                    g.FillRectangle(_dropDownHoverBackColor, e.Bounds);
                    g.DrawString(GetItemText(Items[e.Index]), e.Font, _dropDownForeHoverColor, e.Bounds, stringFormat);
                }
                else
                {
                    g.FillRectangle(_dropDownBackColor, e.Bounds);
                    g.DrawString(GetItemText(Items[e.Index]), e.Font, _dropDownForeColor, e.Bounds, stringFormat);
                    //System.Diagnostics.Debug.WriteLine(e.State);
                }
            }

            base.OnDrawItem(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g2 = e.Graphics;
            Rectangle rect = ClientRectangle;
            if (base.Enabled)
            {
                _Paint(g2, rect);
            }
            else
            {
                Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    _Paint(g, rect);
                }

                g2.DrawImage(Api.GetImgHDispose(bitmap, 0.4f), rect);
            }
            base.OnPaint(e);
        }
        void _Paint(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            if (_radius > 0)
            {
                using (GraphicsPath path = DrawRoundRect(rectmF, _radius))
                {
                    g.FillPath(backbrush, path);
                    if (IsHove)
                    {
                        g.FillPath(backhovebrush, path);
                    }

                    if (!IsHove && IsFocus)
                    {
                        g.DrawPath(_borderFocus, path);
                    }
                    else if (_BorderWidth > 0)
                    {
                        g.DrawPath(_border, path);
                    }
                }
            }
            else
            {
                g.FillRectangle(backbrush, rect);
                if (IsHove)
                {
                    g.FillRectangle(backhovebrush, rect);
                }

                if (!IsHove && IsFocus)
                {
                    g.DrawRectangles(_borderFocus, new RectangleF[] { rectmF });
                }
                else if (_BorderWidth > 0)
                {
                    g.DrawRectangles(_border, new RectangleF[] { rectmF });
                }
            }

            int xxi = rect.Y + (rect.Height / 2);
            Point[] ps = new Point[] {
                new Point((rect.X+rect.Width - ((22-triangleSize) / 2))-10,xxi+(triangleSize/2)),
                new Point((rect.X+rect.Width-(triangleSize/2))-10,xxi-(triangleSize/2)),
                new Point((rect.X+rect.Width-(22-((22-triangleSize) / 2)))-10,xxi-(triangleSize/2))
            };
            g.FillPolygon(IsHove ? _arrowHoverColor : _arrowColor, ps);

            if (!string.IsNullOrEmpty(Text))
            {
                g.DrawString(Text, this.Font, forebrush, rectByFont, stringFormat);
            }
        }

        internal GraphicsPath DrawRoundRect(RectangleF rect, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            gp.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            gp.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            gp.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }

        #endregion
    }
}
