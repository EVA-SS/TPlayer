using StackBlur.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSkin
{
    [ToolboxBitmap(typeof(Button))]
    public partial class TBut : Control, IButtonControl
    {
        #region 构造函数

        Pen loading_pen;
        public TBut()
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
            base.BackColor = Color.Transparent;
        }
        protected override void Dispose(bool disposing)
        {
            Stop();
            base.Dispose(disposing);
        }

        #endregion

        #region 属性
        public void SetSize()
        {
            Size sizeF = TextRenderer.MeasureText(this.Text, this.Font);
            int width = sizeF.Width + _TextMargin.Left + _TextMargin.Right;
            int height = sizeF.Height;
            if ((_Img != null || _ImageActive != null) && _ImageSize.Width > 0)
            {
                width += _ImageSize.Width + _ImageMargin.Left + _ImageMargin.Right;

                if (_TextMargin.Top > _ImageMargin.Top)
                {
                    height += _TextMargin.Top;
                }
                else
                {
                    height += _ImageMargin.Top;
                }

                if (_TextMargin.Bottom > _ImageMargin.Bottom)
                {
                    height += _TextMargin.Bottom;
                }
                else
                {
                    height += _ImageMargin.Bottom;
                }
            }
            else
            {
                height += _TextMargin.Top + _TextMargin.Bottom;
            }
            Size size = new Size(width, height);
            if (!IsDisposed && !Disposing && IsHandleCreated)
            {
                this.Invoke(new Action(() =>
                {
                    this.Size = size;
                    //this.OnSizeChanged(null);
                    this.Invalidate();
                }));
            }

        }

        bool _AutoSize = false;
        [Description("自动调整大小"), Category("外观-普通"), DefaultValue(false), Browsable(true)]
        public bool ASize
        {
            get => _AutoSize;
            set
            {
                if (_AutoSize != value)
                {
                    _AutoSize = value;
                    if (value)
                    {
                        SetSize();
                    }
                    else
                    {
                        this.OnSizeChanged(null);
                    }
                    this.Invalidate();
                }
            }
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (_AutoSize)
            {
                SetSize();
            }
        }

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
                    if (_AutoSize)
                    {
                        SetSize();
                    }
                    else
                    {
                        this.OnSizeChanged(null);
                    }
                    this.Invalidate();
                }
            }
        }

        bool _IsShadow = false;
        [Description("是否显示阴影"), Category("外观-普通"), DefaultValue(false)]
        public bool IsShadow
        {
            get { return _IsShadow; }
            set
            {
                if (_IsShadow != value)
                {
                    _IsShadow = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }


        int _ShadowWidth = 8;
        [Description("阴影宽度"), Category("外观-普通"), DefaultValue(8)]
        public int ShadowWidth
        {
            get { return _ShadowWidth; }
            set
            {
                if (_ShadowWidth != value)
                {
                    _ShadowWidth = value;
                    if (_IsShadow)
                    {
                        this.OnSizeChanged(null);
                        this.Invalidate();
                    }
                }
            }
        }

        SolidBrush shadowbrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        [Description("阴影颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "100, 0, 0, 0")]
        public Color ShadowColor
        {
            get => shadowbrush.Color;
            set
            {
                if (shadowbrush.Color != value)
                {
                    shadowbrush.Color = value;
                    if (_IsShadow)
                    {
                        this.OnSizeChanged(null);
                        this.Invalidate();
                    }
                }
            }
        }


        bool _MultiLine = false;
        [Description("是否多行"), Category("外观-普通"), DefaultValue(false)]
        public bool MultiLine
        {
            get { return _MultiLine; }
            set
            {
                if (_MultiLine != value)
                {
                    _MultiLine = value;
                    stringFormat.FormatFlags = value ? 0 : StringFormatFlags.NoWrap;
                    this.Invalidate();
                }
            }
        }

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



        private ContentAlignment _TextAlign = ContentAlignment.MiddleCenter;
        [Description("字体位置"), Category("外观-普通"), DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
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

        bool badge = false;
        [Description("是否显示角标"), Category("外观-普通"), DefaultValue(false)]
        public bool Badge
        {
            get { return badge; }
            set
            {
                if (badge != value)
                {
                    badge = value;
                    this.Invalidate();
                }
            }
        }

        SolidBrush badgeColor = new SolidBrush(Color.FromArgb(112, 237, 58));
        [Description("角标颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "112, 237, 58")]
        public Color BadgeColor
        {
            get { return badgeColor.Color; }
            set
            {
                if (badgeColor.Color != value)
                {
                    badgeColor.Color = value;
                    if (badge)
                    {
                        this.Invalidate();
                    }
                }
            }
        }
        int badgeWidth = 10;
        [Description("角标大小"), Category("外观-普通"), DefaultValue(10)]
        public int BadgeSize
        {
            get { return badgeWidth; }
            set
            {
                if (badgeWidth != value)
                {
                    badgeWidth = value;
                    if (badge)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

        #region 进度条

        bool _LoadShow = false;
        [Description("是否加载显示其他内容"), Category("外观-普通"), DefaultValue(false)]
        public bool LoadShow
        {
            get { return _LoadShow; }
            set
            {
                if (_LoadShow != value)
                {
                    _LoadShow = value;
                }
            }
        }

        private int speed = 1;
        [Category("进度"), Description("外观-普通"), DefaultValue(1)]
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
            }
        }

        [Description("加载边框颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "White")]
        public Color LoadingColor
        {
            get => loading_pen.Color;
            set
            {
                if (loading_pen.Color != value)
                {
                    loading_pen.Color = value;
                    this.Invalidate();
                }
            }
        }

        double value = 0, valueMiddle = 0, maxvalue = 0;
        [Description("当前值"), Category("外观-普通"), DefaultValue(0.0)]
        public double Value
        {
            get { return value; }
            set
            {
                this.value = value;
                this.Invalidate();
            }
        }

        [Description("中间值"), Category("外观-普通"), DefaultValue(0.0)]
        public double ValueMiddle
        {
            get { return valueMiddle; }
            set
            {
                this.valueMiddle = value;
                this.Invalidate();
            }
        }

        [Description("最大值"), Category("外观-普通"), DefaultValue(0.0)]
        public double MaxValue
        {
            get { return maxvalue; }
            set
            {
                maxvalue = value;
                this.Invalidate();
            }
        }

        SolidBrush valuebrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0));
        [Description("进度条颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "10, 0, 0, 0")]
        public Color ValueColor
        {
            get => valuebrush.Color;
            set
            {
                if (valuebrush.Color != value)
                {
                    valuebrush.Color = value;
                    this.Invalidate();
                }
            }
        }

        SolidBrush valueMiddlebrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0));
        [Description("进度条中间颜色"), Category("外观-普通"), DefaultValue(typeof(Color), "10, 0, 0, 0")]
        public Color ValueColorMiddle
        {
            get => valueMiddlebrush.Color;
            set
            {
                if (valueMiddlebrush.Color != value)
                {
                    valueMiddlebrush.Color = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

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
                    this.Invalidate();
                }
            }
        }


        private ContentAlignment _ImageAlign = ContentAlignment.MiddleLeft;
        [Description("图片位置"), Category("外观-普通"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment ImageAlign
        {
            get => _ImageAlign;
            set
            {
                if (_ImageAlign != value)
                {
                    _ImageAlign = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }


        Size _ImageSize = new Size(0, 0);
        [Description("图片宽度"), Category("外观-普通"), DefaultValue(typeof(Size), "0, 0")]
        public Size ImageSize
        {
            get { return _ImageSize; }
            set
            {
                if (_ImageSize.Width != value.Width || _ImageSize.Height != value.Height)
                {
                    if (value.Width > 0 && value.Height > 0)
                    {
                        _ImageSize.Width = value.Width;
                        _ImageSize.Height = value.Height;
                        this.OnSizeChanged(null);
                        this.Invalidate();
                    }
                    else if (value.Width > 0 || value.Height > 0)
                    {
                        _ImageSize.Width = value.Width;
                        _ImageSize.Height = value.Height;
                    }
                    else
                    {
                        _ImageSize.Width = 0;
                        _ImageSize.Height = 0;
                        this.OnSizeChanged(null);
                        this.Invalidate();
                    }
                }
            }
        }


        Padding _ImageMargin = new Padding(0);
        [Description("图片边距"), Category("外观-普通"), DefaultValue(typeof(Padding), "0, 0, 0, 0")]
        public Padding ImageMargin
        {
            get { return _ImageMargin; }
            set
            {
                if (_ImageMargin != value)
                {
                    _ImageMargin = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }


        float _enabledOpacity = 0.4f;
        [Description("禁用透明度"), Category("外观-普通"), DefaultValue(0.4f)]
        public float EnabledOpacity
        {
            get { return _enabledOpacity; }
            set
            {
                if (_enabledOpacity != value)
                {
                    _enabledOpacity = value;
                    if (!base.Enabled)
                    {
                        this.Invalidate();
                    }
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
            get => foreActivebrush;
            set
            {
                if (foreActivebrush != value)
                {
                    foreActivebrush = value;
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
        public Color BackColorActive
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

        public Color BackColorActive2
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
        public Color BorderColorActive
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

        #endregion

        #region 动画开关

        ManualResetEvent resetEvent = new ManualResetEvent(false);
        bool Tasking = false;


        bool _state = false;
        [Category("进度"), Description("动画"), DefaultValue(false)]
        public bool State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    Stop();
                    if (_state)
                    {
                        resetEvent.Set();
                        if (!Tasking)
                        {
                            Tasking = true;
                            Action _action = () =>
                            {
                                bool ProgState = false;
                                while (true)
                                {
                                    resetEvent.WaitOne();
                                    Prog2 += speed;
                                    if (Prog2 >= 360)
                                    { Prog2 = 0; }
                                    if (ProgState)
                                    {
                                        if (Prog1 >= 98)
                                        { ProgState = false; }
                                        else
                                        {
                                            Prog1++;
                                        }
                                    }
                                    else
                                    {
                                        if (Prog1 <= 2)
                                        { ProgState = true; }
                                        else
                                        {
                                            Prog1--;
                                            Prog2 += speed;
                                            if (speed < 2)
                                            { Prog2 += 3; }
                                            else
                                            {
                                                Prog2 += speed;
                                            }
                                        }
                                    }
                                    Thread.Sleep(10);
                                    this.Invalidate();
                                    resetEvent.WaitOne();
                                }
                            };
                            Task.Run(_action).ContinueWith((action =>
                            {
                                Tasking = false;
                            }));
                        }
                    }
                }
            }
        }

        int Prog1 = 2;
        int Prog2 = 0;

        void Stop()
        {
            resetEvent.Reset();
            this.Invalidate();
        }
        #endregion

        #region 坐标布局

        StringFormat stringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };

        Rectangle rectmF;



        Rectangle rectByFont, rectByImg, rectByLoading;
        protected override void OnSizeChanged(EventArgs e)
        {
            Rectangle rect = ClientRectangle;
            if (_IsShadow)
            {
                int _showW = _ShadowWidth + (int)(_ShadowWidth * 1.5);
                Rectangle _rect = new Rectangle(_showW / 2, _showW / 2, rect.Width - _showW, rect.Height - _showW);
                SizeChange(_rect);
            }
            else { SizeChange(rect); }

            base.OnSizeChanged(e);
        }

        void SizeChange(Rectangle rect)
        {
            //int HasX = _ImageMargin.Left, HasY = _ImageMargin.Top, HasX2 = _ImageMargin.Bottom, HasY2 = _ImageMargin.Right;
            SizeF fontSizeF = TextRenderer.MeasureText(string.IsNullOrEmpty(this.Text) ? "Qq森甩" : this.Text, this.Font);
            Size fontSize = new Size((int)Math.Ceiling(fontSizeF.Width), (int)Math.Ceiling(fontSizeF.Height));
            rectByFont = new Rectangle(rect.X + _TextMargin.Left, rect.Y + _TextMargin.Top, rect.Width - (_TextMargin.Left + _TextMargin.Right), rect.Height - (_TextMargin.Top + _TextMargin.Bottom));

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

            if (_ImageSize.Width > 0 && _ImageSize.Height > 0)
            {
                switch (_ImageAlign)
                {
                    case ContentAlignment.TopLeft:
                        //内容在垂直方向上顶部对齐，在水平方向上左边对齐
                        rectByImg = new Rectangle(rect.X + _ImageMargin.Left, rect.Y + _ImageMargin.Top, _ImageSize.Width, _ImageSize.Height);
                        break;
                    case ContentAlignment.TopCenter:
                        //内容在垂直方向上顶部对齐，在水平方向上居中对齐
                        rectByImg = new Rectangle(rect.X + (rect.Width - _ImageSize.Width) / 2, rect.Y + _ImageMargin.Top, _ImageSize.Width, _ImageSize.Height);

                        break;
                    case ContentAlignment.TopRight:
                        //内容在垂直方向上顶部对齐，在水平方向上右边对齐
                        rectByImg = new Rectangle(rect.X + (rect.Width - _ImageSize.Width) - _ImageMargin.Right, rect.Y + _ImageMargin.Top, _ImageSize.Width, _ImageSize.Height);
                        break;
                    case ContentAlignment.MiddleLeft:
                        //内容在垂直方向上中间对齐，在水平方向上左边对齐
                        rectByImg = new Rectangle(rect.X + _ImageMargin.Left, rect.Y + (rect.Height - _ImageSize.Height) / 2, _ImageSize.Width, _ImageSize.Height);

                        break;
                    case ContentAlignment.MiddleCenter:
                        //内容在垂直方向上中间对齐，在水平方向上居中对齐
                        rectByImg = new Rectangle(rect.X + (rect.Width - _ImageSize.Width) / 2, rect.Y + (rect.Height - _ImageSize.Height) / 2, _ImageSize.Width, _ImageSize.Height);
                        break;
                    case ContentAlignment.MiddleRight:
                        //内容在垂直方向上中间对齐，在水平方向上右边对齐
                        rectByImg = new Rectangle(rect.X + (rect.Width - _ImageSize.Width) - _ImageMargin.Right, rect.Y + (rect.Height - _ImageSize.Height) / 2, _ImageSize.Width, _ImageSize.Height);

                        break;
                    case ContentAlignment.BottomLeft:
                        //内容在垂直方向上底边对齐，在水平方向上左边对齐
                        rectByImg = new Rectangle(rect.X + _ImageMargin.Left, rect.Y + (rect.Height - _ImageSize.Height) - _ImageMargin.Bottom, _ImageSize.Width, _ImageSize.Height);
                        break;
                    case ContentAlignment.BottomCenter:
                        //内容在垂直方向上底边对齐，在水平方向上居中对齐
                        rectByImg = new Rectangle(rect.X + (rect.Width - _ImageSize.Width) / 2, rect.Y + (rect.Height - _ImageSize.Height) - _ImageMargin.Bottom, _ImageSize.Width, _ImageSize.Height);

                        break;
                    case ContentAlignment.BottomRight:
                        //内容在垂直方向上底边对齐，在水平方向上右边对齐
                        rectByImg = new Rectangle(rect.X + (rect.Width - _ImageSize.Width) - _ImageMargin.Right, rect.Y + (rect.Height - _ImageSize.Height) - _ImageMargin.Bottom, _ImageSize.Width, _ImageSize.Height);
                        break;
                }


                rectByLoading = new Rectangle(rect.X + rectByImg.X + 2, rect.Y + rectByImg.Y + 2, rectByImg.Width - 4, rectByImg.Height - 4);
                //rectByLoading = rectByImg;
            }
            else
            {
                if (string.IsNullOrEmpty(this.Text))
                {
                    rectByLoading = new Rectangle(rect.X + (rect.Width - fontSize.Height) / 2, rect.Y + (rect.Height - fontSize.Height) / 2, fontSize.Height, fontSize.Height);
                }
                else
                {
                    switch (_TextAlign)
                    {
                        case ContentAlignment.TopLeft:
                            //内容在垂直方向上顶部对齐，在水平方向上左边对齐
                            rectByLoading = new Rectangle(rect.X + (rect.Width - fontSize.Height) - _TextMargin.Right, rect.Y + _TextMargin.Top, fontSize.Height, fontSize.Height);
                            break;
                        case ContentAlignment.TopCenter:
                            //内容在垂直方向上顶部对齐，在水平方向上居中对齐
                            rectByLoading = new Rectangle(rect.X + _TextMargin.Left, rect.Y + _TextMargin.Top, fontSize.Height, fontSize.Height);
                            break;
                        case ContentAlignment.TopRight:
                            //内容在垂直方向上顶部对齐，在水平方向上右边对齐
                            rectByLoading = new Rectangle(rect.X + _TextMargin.Left, rect.Y + _TextMargin.Top, fontSize.Height, fontSize.Height);
                            break;
                        case ContentAlignment.MiddleLeft:
                            //内容在垂直方向上中间对齐，在水平方向上左边对齐
                            rectByLoading = new Rectangle(rect.X + (rect.Width - fontSize.Height) - _TextMargin.Right, rect.Y + (rect.Height - fontSize.Height) / 2, fontSize.Height, fontSize.Height);
                            break;
                        case ContentAlignment.MiddleCenter:
                            //内容在垂直方向上中间对齐，在水平方向上居中对齐
                            rectByLoading = new Rectangle(rect.X + _TextMargin.Left, rect.Y + (rect.Height - fontSize.Height) / 2, fontSize.Height, fontSize.Height);

                            break;
                        case ContentAlignment.BottomLeft:
                            //内容在垂直方向上底边对齐，在水平方向上左边对齐

                            rectByLoading = new Rectangle(rect.X + (rect.Width - fontSize.Height) - _TextMargin.Right, rect.Y + (rect.Height - fontSize.Height) - _TextMargin.Bottom, fontSize.Height, fontSize.Height);

                            break;
                        case ContentAlignment.BottomCenter:
                            //内容在垂直方向上底边对齐，在水平方向上居中对齐

                            rectByLoading = new Rectangle(rect.X + _TextMargin.Left, rect.Y + (rect.Height - fontSize.Height) - _TextMargin.Bottom, fontSize.Height, fontSize.Height);

                            break;
                        case ContentAlignment.BottomRight:
                            //内容在垂直方向上底边对齐，在水平方向上右边对齐
                            rectByLoading = new Rectangle(rect.X + _TextMargin.Left, rect.Y + (rect.Height - fontSize.Height) - _TextMargin.Bottom, fontSize.Height, fontSize.Height);

                            break;

                        default:
                            rectByLoading = new Rectangle(rect.X + fontSize.Height / 2, rect.Y + (rect.Height - fontSize.Height) / 2, fontSize.Height, fontSize.Height);
                            break;
                    }
                }
                //rectByLoading = new Rectangle(loading_size / 2, (rect.Height - loading_size) / 2, loading_size, loading_size);
            }

            rectmF = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            //rectmF = new RectangleF(2,2, rect.Width - 5, rect.Height - 5);

            SetBrush(ref backbrush, backcolor1, backcolor2);
            SetBrush(ref backselbrush, backcolorsel1, backcolorsel2);

        }

        #endregion

        Pen _borderFocus = new Pen(Color.FromArgb(200, 0, 0, 0), 1);
        Pen _border = new Pen(Color.Transparent, 1), _Activeborder = new Pen(Color.Transparent, 1);
        Color foreActivebrush = Color.Transparent;
        Brush backbrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)), backselbrush = new SolidBrush(Color.Transparent);

        #region 模拟点击

        DialogResult dialogResult = DialogResult.None;

        [DefaultValue(typeof(DialogResult), "None")]
        public DialogResult DialogResult
        {
            get { return dialogResult; }
            set
            {
                if (dialogResult != value)
                {
                    dialogResult = value;
                }
            }
        }
        public void PerformClick()
        {
            this.OnClick(EventArgs.Empty);
        }
        public void NotifyDefault(bool value)
        {

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
            if (maxvalue == 0)
            {
                if (!IsHove)
                {
                    IsHove = true;
                    this.Invalidate();
                }
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

        bool _Visible = true;
        [Description("是否显示控件及其所有子控件"), Category("行为"), DefaultValue(true)]
        public new bool Visible
        {
            get => _Visible;
            set
            {
                base.Visible = _Visible = value;
            }
        }

        SolidBrush TransparentsolidBrush = new SolidBrush(Color.Transparent);
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gMain = e.Graphics;
            gMain.SmoothingMode = SmoothingMode.AntiAlias;
            gMain.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gMain.CompositingQuality = CompositingQuality.HighQuality;
            //gMain.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rect = ClientRectangle;
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //g2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;

                    if (_IsShadow)
                    {
                        int _showW = _ShadowWidth + (int)(_ShadowWidth * 1.5);
                        Rectangle _rect = new Rectangle(_showW / 2, _showW / 2, rect.Width - _showW, rect.Height - _showW);
                        //Rectangle _rect = new Rectangle((_showW / 2) - 2, (_showW / 2) - 2, rect.Width - _showW + 4, rect.Height - _showW + 4);

                        //g3.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        using (var path = _rect.CreateRoundedRectanglePath(Radius, UICornerRadiusSides.All))
                        {
                            g.FillPath(shadowbrush, path);
                            bmp.StackBlur(_ShadowWidth);
                        }
                        g.DrawImage(bmp, rect);
                    }


                    using (GraphicsPath path = rectmF.CreateRoundedRectanglePath(_radius, UICornerRadiusSides.All))
                    {
                        if (_IsShadow)
                        {
                            g.CompositingMode = CompositingMode.SourceCopy;
                            g.FillPath(TransparentsolidBrush, path);
                            g.CompositingMode = CompositingMode.SourceOver;
                        }

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

                        if (maxvalue > 0 && value > 0)
                        {
                            if (maxvalue == value)
                            {
                                g.FillPath(valuebrush, path);
                            }
                            else
                            {
                                if (valueMiddle > 0)
                                {
                                    double wit2 = valueMiddle / maxvalue;
                                    float Wit2 = rectmF.Width * (float)wit2;
                                    RectangleF rect_value2 = new RectangleF(rectmF.Y, rectmF.X, Wit2, rectmF.Height);

                                    using (GraphicsPath paths2 = rect_value2.CreateRoundedRectanglePath(_radius, _radius > 0 ? UICornerRadiusSides.All : UICornerRadiusSides.None))
                                    {
                                        g.FillPath(valueMiddlebrush, paths2);
                                    }
                                }
                                double wit = value / maxvalue;
                                //if (wit > 20)
                                //{
                                float Wit = rectmF.Width * (float)wit;
                                RectangleF rect_value = new RectangleF(rectmF.Y, rectmF.X, Wit, rectmF.Height);

                                using (GraphicsPath paths = rect_value.CreateRoundedRectanglePath(_radius, _radius > 0 ? UICornerRadiusSides.All : UICornerRadiusSides.None))
                                {
                                    g.FillPath(valuebrush, paths);
                                }
                            }
                        }

                        if (IsFocus)
                        {
                            g.DrawPath(_borderFocus, path);
                        }

                        if ((!State || _LoadShow) && _ImageSize.Width > 0 && _ImageSize.Height > 0)
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

                            if (badge)
                            {
                                g.FillEllipse(badgeColor, new Rectangle(rectByImg.X + rectByImg.Width - badgeWidth, rectByImg.Y, badgeWidth, badgeWidth));
                            }
                        }
                        else if (badge)
                        {
                            g.FillEllipse(badgeColor, new Rectangle(rectByFont.X + rectByFont.Width - badgeWidth, rectByFont.Y, badgeWidth, badgeWidth));
                        }

                        if (State)
                        {
                            g.DrawArc(loading_pen, rectByLoading, Prog2, (float)(Prog1 * 3.6));
                        }
                    }
                }

                if (base.Enabled || _enabledOpacity >= 1)
                {
                    gMain.DrawImage(bmp, rect);
                }
                else
                {
                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        ColorMatrix matrix = new ColorMatrix();
                        matrix.Matrix33 = _enabledOpacity;
                        attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                        gMain.DrawImage(bmp, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attributes);
                    }
                }
                if (!string.IsNullOrEmpty(Text))
                {
                    int c2 = (int)(255 * ((base.Enabled || _enabledOpacity >= 1) ? 1 : _enabledOpacity));
                    if (IsActive && foreActivebrush != Color.Transparent)
                    {
                        gMain.DrawString(Text, this.Font, new SolidBrush(Color.FromArgb(c2, foreActivebrush)), rectByFont, stringFormat);
                    }
                    else
                    {
                        gMain.DrawString(Text, this.Font, new SolidBrush(Color.FromArgb(c2, ForeColor)), rectByFont, stringFormat);
                    }
                }

            }
            base.OnPaint(e);
        }

    }
}
