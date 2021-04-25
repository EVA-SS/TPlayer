using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace TSkin
{
    [ToolboxBitmap(typeof(Button))]
    public partial class TButDLNA : Control
    {
        #region 构造函数

        Thread Tim = null;
        Pen loading_pen;
        public TButDLNA()
        {
            _borderFocus.DashStyle = DashStyle.DashDot;
            loading_pen = new Pen(Color.White, 2);
            loading_pen.LineJoin = LineJoin.Round;
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

        #endregion

        #region 属性

        #region 普通状态

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

        private string _Text2 = null;
        [Description("文字2"), Category("外观-普通"), DefaultValue(null)]
        public string Text2
        {
            get => _Text2;
            set
            {
                if (_Text2 != value)
                {
                    _Text2 = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
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


        Font font2 = new Font("微软雅黑", 9);

        [Description("字体2"), Category("外观-普通"), DefaultValue(typeof(Font), "微软雅黑,9")]
        public Font Font2
        {
            get => font2;
            set
            {
                if (font2 != value)
                {
                    font2 = value;
                    this.Invalidate();
                }
            }
        }


        SolidBrush foreColor2 = new SolidBrush(Color.DimGray);

        [Description("字体颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "DimGray")]
        public Color ForeColor2
        {
            get => foreColor2.Color;
            set
            {
                if (foreColor2.Color != value)
                {
                    foreColor2.Color = value;
                    this.Invalidate();
                }
            }
        }


        Color backcolor1 = Color.FromArgb(100, 0, 0, 0);
        Color backcolor2 = Color.FromArgb(100, 0, 0, 0);
        [Description("背景颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "100, 0, 0, 0")]
        public Color DefaultColor
        {
            get => backcolor1;
            set
            {
                if (backcolor1 != value)
                {
                    backcolor1 = value;
                    if (backcolor1 != Color.Transparent && backcolor2 != Color.Transparent)
                    {
                        if (rectmF.Width > 0 && rectmF.Height > 0)
                        {
                            backbrush = new LinearGradientBrush(rectmF, backcolor1, backcolor2, LinearGradientMode.Horizontal);
                        }
                    }
                    else
                    {
                        backbrush = new SolidBrush(backcolor1);
                    }
                    this.Invalidate();
                }
            }
        }

        [Description("背景渐变色"), Category("外观-普通"), DefaultValue(typeof(Color), "100, 0, 0, 0")]
        public Color DefaultColor2
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
        [Description("边框宽度"), Category("外观-普通"), DefaultValue(0f)]
        public float BorderWidth
        {
            get { return _BorderWidth; }
            set
            {
                if (_BorderWidth != value)
                {
                    _border.Width = _Activeborder.Width = value;
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

        private Image _Img;
        [Description("图片"), Category("外观-普通"), DefaultValue(null)]
        public Image Image
        {
            get { return _Img; }
            set
            {
                if (_Img != value)
                {
                    _Img = value;
                    //if (value != null)
                    //{
                    //    _ImageSize = value.Width;
                    //}
                    this.Invalidate();
                }
            }
        }


        int _ImageSize = 50;
        [Description("图片宽度"), Category("外观-普通"), DefaultValue(50)]
        public int ImageSize
        {
            get { return _ImageSize; }
            set
            {
                if (_ImageSize != value)
                {
                    _ImageSize = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region 移动

        SolidBrush backhovebrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
        [Description("鼠标移入颜色"), Category("外观-移动"), DefaultValue(typeof(Color), "20, 0, 0, 0")]
        public Color HoverColor
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

        private Image _ImageHove;
        [Description("鼠标移入图片"), Category("外观-移动"), DefaultValue(null)]
        public Image ImageHove
        {
            get { return _ImageHove; }
            set
            {
                if (_ImageHove != value)
                {
                    _ImageHove = value;
                    if (IsHove)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        #endregion

        #region 焦点

        [Description("焦点边框颜色"), Category("外观-焦点"), DefaultValue(typeof(Color), "200, 0, 0, 0")]
        public Color BorderFocusColor
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

        private bool _isActive = false;
        [Description("激活状态"), Category("外观-激活"), DefaultValue(false)]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;

                    this.Invalidate();
                }
            }
        }

        [Description("激活后字体颜色"), Category("外观-激活"), DefaultValue(typeof(Color), "Transparent")]
        public Color ForeColorActive
        {
            get => foreActivebrush.Color;
            set
            {
                if (foreActivebrush.Color != value)
                {
                    foreActivebrush.Color = value;
                    if (IsActive)
                    {
                        this.Invalidate();
                    }
                }
            }
        }


        Color backcolorsel1 = Color.FromArgb(100, 0, 0, 0);
        Color backcolorsel2 = Color.Transparent;
        [Description("激活后背景颜色"), Category("外观-激活"), DefaultValue(typeof(Color), "100, 0, 0, 0")]
        public Color ActiveColor
        {
            get => backcolorsel1;
            set
            {
                if (backcolorsel1 != value)
                {
                    backcolorsel1 = value;


                    SetBrush(ref backselbrush, backcolorsel1, backcolorsel2);

                    if (IsActive)
                    {
                        this.Invalidate();
                    }
                }
            }
        }


        [Description("激活后背景渐变色"), Category("外观-激活"), DefaultValue(typeof(Color), "Transparent")]

        public Color ActiveColor2
        {
            get => backcolorsel2;
            set
            {
                if (backcolorsel2 != value)
                {
                    backcolorsel2 = value;

                    SetBrush(ref backselbrush, backcolorsel1, backcolorsel2);

                    if (IsActive)
                    {
                        this.Invalidate();
                    }
                }
            }
        }


        private Image _ImageActive;
        [Description("激活后图片"), Category("外观-激活"), DefaultValue(null)]
        public Image ImageActive
        {
            get { return _ImageActive; }
            set
            {
                if (_ImageActive != value)
                {
                    _ImageActive = value;
                    if (IsActive)
                    {
                        this.Invalidate();
                    }
                }
            }
        }


        [Description("激活后边框颜色"), Category("外观-激活"), DefaultValue(typeof(Color), "Transparent")]
        public Color ActiveBorderColor
        {
            get => _Activeborder.Color;
            set
            {
                if (_Activeborder.Color != value)
                {
                    _Activeborder.Color = value;
                    if (IsActive)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

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
                    if (brush is SolidBrush && (brush as SolidBrush).Color == backcolor1)
                    {
                        return;
                    }
                    else
                    {
                        brush.Dispose();
                    }
                }
                brush = new SolidBrush(backcolor1);
            }
        }

        #endregion

        #region 坐标布局

        StringFormat stringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };

        Rectangle rectmF;



        Rectangle rectByFont, rectByFont2, rectByImg;
        protected override void OnSizeChanged(EventArgs e)
        {
            Rectangle rect = ClientRectangle;
            if (_ImageSize > 0)
            {
                rectByImg = new Rectangle((rect.Height - _ImageSize) / 2, (rect.Height - _ImageSize) / 2, _ImageSize, _ImageSize);
            }
            else
            {
                rectByImg = new Rectangle(0, 0, 0, 0);
            }
            rectByFont = new Rectangle(76, 15, rect.Width - 80, 30);
            rectByFont2 = new Rectangle(78, 45, rect.Width - 82, 20);


            rectmF = new Rectangle(0, 0, rect.Width - 1, rect.Height - 1);

            SetBrush(ref backbrush, backcolor1, backcolor2);
            SetBrush(ref backselbrush, backcolorsel1, backcolorsel2);
            base.OnSizeChanged(e);
        }

        #endregion

        Pen _borderFocus = new Pen(Color.FromArgb(200, 0, 0, 0), 1);
        Pen _border = new Pen(Color.Transparent, 1), _Activeborder = new Pen(Color.Transparent, 1);
        SolidBrush forebrush = new SolidBrush(Color.Black), foreActivebrush = new SolidBrush(Color.Transparent);
        Brush backbrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)), backselbrush = new SolidBrush(Color.Transparent);

        #region 模拟点击

        public void PerformClick()
        {
            this.OnClick(EventArgs.Empty);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 32)
            {
                //回车
                e.Handled = true;
                OnClick(EventArgs.Empty);
                this.Parent.Focus();
                //this.Invalidate();
            }
            base.OnKeyPress(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }


        #endregion

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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g2 = e.Graphics;

            Rectangle rect = ClientRectangle;
            if (base.Enabled)
            {
                Paint(g2, rect);
            }
            else
            {
                Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    Paint(g, rect);
                }

                g2.DrawImage(Api.GetImgHDispose(bitmap, 0.4f), rect);
            }
            base.OnPaint(e);
        }

        void Paint(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            using (GraphicsPath path = rectmF.CreateRoundedRectanglePath(_radius, _radius > 0 ? UICornerRadiusSides.All : UICornerRadiusSides.None))
            {
                g.FillPath(backbrush, path);
                if (IsActive)
                {
                    g.FillPath(backselbrush, path);
                }
                else if (IsHove)
                {
                    g.FillPath(backhovebrush, path);
                }
                if (_BorderWidth > 0)
                {
                    if (IsActive)
                    {
                        g.DrawPath(_Activeborder, path);
                    }
                    else
                    {
                        g.DrawPath(_border, path);
                    }
                }




                if (IsFocus)
                {
                    g.DrawPath(_borderFocus, path);
                }
            }


            if (_ImageSize > 0)
            {
                if (IsActive && _ImageActive != null)
                {
                    g.DrawImage(_ImageActive, rectByImg);
                }
                else if (_Img != null)
                {
                    if (IsHove && _ImageHove != null)
                    {
                        g.DrawImage(_ImageHove, rectByImg);
                    }
                    else
                    {
                        g.DrawImage(_Img, rectByImg);
                    }
                }
            }

            if (!string.IsNullOrEmpty(Text2))
            {
                g.DrawString(Text2, font2, foreColor2, rectByFont2, stringFormat);
            }
            if (!string.IsNullOrEmpty(Text))
            {
                if (IsActive && foreActivebrush.Color != Color.Transparent)
                {
                    g.DrawString(Text, this.Font, foreActivebrush, rectByFont, stringFormat);

                }
                else
                {
                    g.DrawString(Text, this.Font, forebrush, rectByFont, stringFormat);
                }
            }
        }

    }
}
