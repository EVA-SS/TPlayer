using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TSkin
{
    public partial class PanelFrame : Panel
    {
        public PanelFrame()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                //ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
        }

        /// 边框颜色   
        /// </summary>   
        private Pen _borderPen = new Pen(Color.Transparent, 0), _borderFocusPen = new Pen(Color.FromArgb(167, 166, 170), 0), _borderActivePen = new Pen(Color.FromArgb(167, 166, 170), 0);


        #region 属性   

        SolidBrush backbrush = new SolidBrush(Color.Transparent);
        [Description("背景颜色"), Category("自定义"), DefaultValue(typeof(Color), "Transparent")]
        public new Color BackColor
        {
            get => backbrush.Color;
            set
            {
                if (backbrush.Color != value)
                {
                    backbrush.Color = value;
                    p1 = backbrush.Color != Color.Transparent;
                    this.Invalidate();
                }
            }
        }


        /// <summary>
        /// 边框颜色
        /// </summary>
        [Category("自定义"), Description("边框颜色"), DefaultValue(typeof(Color), "Transparent")]
        public Color BorderColor
        {
            get
            {
                return this._borderPen.Color;
            }
            set
            {
                if (_borderPen.Color != value)
                {
                    this._borderPen.Color = value;
                    p2 = _borderPen.Width > 0 && (_borderPen.Color != Color.Transparent || _borderFocusPen.Color != Color.Transparent);
                    this.Invalidate();
                }
            }
        }

        [Category("自定义"), Description("边框激活颜色"), DefaultValue(typeof(Color), "167, 166, 170")]
        public Color BorderColorActive
        {
            get
            {
                return this._borderActivePen.Color;
            }
            set
            {
                if (_borderActivePen.Color != value)
                {
                    this._borderActivePen.Color = value;
                    p3 = _borderActivePen.Width > 0 && _borderActivePen.Color != Color.Transparent;
                    this.Invalidate();
                }
            }
        }


        [Category("自定义"), Description("边框焦点颜色"), DefaultValue(typeof(Color), "167, 166, 170")]
        public Color BorderColorFocus
        {
            get
            {
                return this._borderFocusPen.Color;
            }
            set
            {
                if (_borderFocusPen.Color != value)
                {
                    this._borderFocusPen.Color = value;
                    p2 = _borderPen.Width > 0 && (_borderPen.Color != Color.Transparent || _borderFocusPen.Color != Color.Transparent);
                    if (_Focus)
                    {
                        this.Invalidate();
                    }
                }
            }
        }
        [Description("边框宽度"), Category("自定义"), DefaultValue(0f)]
        public float BorderWidth
        {
            get { return _borderPen.Width; }
            set
            {
                if (_borderPen.Width != value)
                {
                    _borderPen.Width = value;
                    p2 = _borderPen.Width > 0 && (_borderPen.Color != Color.Transparent || _borderFocusPen.Color != Color.Transparent);
                    this.Invalidate();
                }
            }
        }
        [Description("边框激活宽度"), Category("自定义"), DefaultValue(0f)]
        public float BorderWidthActive
        {
            get { return _borderActivePen.Width; }
            set
            {
                if (_borderActivePen.Width != value)
                {
                    _borderActivePen.Width = value;
                    p3 = _borderActivePen.Width > 0 && _borderActivePen.Color != Color.Transparent;
                    this.Invalidate();
                }
            }
        }
        bool p1 = false, p2 = false, p3 = false;
        private bool _isActive = false;
        [Description("激活状态"), Category("自定义"), DefaultValue(false)]
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



        bool _Focus = false;
        [Description("焦点状态"), Category("自定义"), DefaultValue(false)]
        public bool IsFocus
        {
            get => _Focus;
            set
            {
                if (_Focus != value)
                {
                    _Focus = value;

                    this.Invalidate();
                }
            }
        }
        Control focusControl = null;
        [Description("焦点控件"), Category("自定义"), DefaultValue(null)]
        public Control FocusControl
        {
            get => focusControl;
            set
            {
                if (focusControl != value)
                {
                    if (focusControl != null && !focusControl.IsDisposed)
                    {
                        focusControl.GotFocus -= FocusControl_GotFocus;
                        focusControl.LostFocus -= FocusControl_LostFocus;
                    }
                    focusControl = value;

                    focusControl.GotFocus += FocusControl_GotFocus;
                    focusControl.LostFocus += FocusControl_LostFocus;
                    this.Invalidate();
                }
            }
        }

        private void FocusControl_LostFocus(object sender, EventArgs e)
        {
            IsFocus = false;
        }

        private void FocusControl_GotFocus(object sender, EventArgs e)
        {
            IsFocus = true;
        }

        int _radius = 0;
        [Description("圆角"), Category("自定义"), DefaultValue(0)]
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

        #endregion 属性

        protected override void OnPaint(PaintEventArgs e)
        {
            if (p1 || (!_isActive && p2) || (_isActive && p3))
            {
                Graphics g = e.Graphics;
                Rectangle rect = ClientRectangle;
                RectangleF rectm = new RectangleF(0, 0, rect.Width - 1, rect.Height - 1);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                //边框Width为1个像素   
                if (_radius > 0)
                {
                    using (GraphicsPath path = rectm.CreateRoundedRectanglePath(_radius, _radius > 0 ? UICornerRadiusSides.All : UICornerRadiusSides.None))
                    {
                        if (p1)
                        {
                            g.FillPath(backbrush, path);
                        }
                        //绘制边框

                        if (_isActive)
                        {
                            if (p3)
                            {
                                g.DrawPath(_borderActivePen, path);
                            }
                        }
                        else if (!_isActive && p2)
                        {
                            g.DrawPath(_Focus ? _borderFocusPen : _borderPen, path);
                        }
                    }
                }
                else
                {
                    if (backbrush.Color != Color.Transparent)
                    {
                        g.FillRectangle(backbrush, rect);
                    }

                    //绘制边框
                    if (_isActive)
                    {
                        if (p3)
                        {
                            g.DrawRectangles(_borderActivePen, new RectangleF[] { rectm });
                        }
                    }
                    else if (!_isActive && p2)
                    {
                        g.DrawRectangles(_Focus ? _borderFocusPen : _borderPen, new RectangleF[] { rectm });
                    }
                }
            }

            base.OnPaint(e);
        }
    }
}
