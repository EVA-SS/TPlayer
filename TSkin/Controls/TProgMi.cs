using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TPlayer;

namespace TSkin
{
    public partial class TProgMi : Control
    {
        public TProgMi()
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


            controlAnimations.OKClick += (value) =>
            {
                prog_rect.Height = (float)value;
                Invalidate();
            };
            controlAnimations.JClick += (value) =>
            {
                prog_rect.Height = (float)value;
                Invalidate();
            };
        }

        #region 移动坐标

        /// <summary>
        /// 进度条区域
        /// </summary>
        public RectangleF prog_rect = new RectangleF(4, 0, 0, 4);
        protected override void OnSizeChanged(EventArgs e)
        {
            Rectangle rect = ClientRectangle;
            prog_rect.Width = rect.Width - 8;
            if (this.Height != 16)
            {
                this.Height = 16;
            }
            else
            {
                base.OnSizeChanged(e);
            }
        }


        #endregion

        #region 渲染

        #region 样式

        SolidBrush _ValueColor = new SolidBrush(Color.FromArgb(250, Color.DodgerBlue));
        SolidBrush solidBrush_White = new SolidBrush(Color.White);
        SolidBrush solidBrush_WhiteSmoke = new SolidBrush(Color.FromArgb(40, 255, 255, 255));

        #endregion
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

            try
            {
                if (_maxvalue > 0)
                {
                    //double p = _value;
                    prog_rect.Y = (rect.Height - prog_rect.Height) / 2;
                    g.FillRectangle(solidBrush_WhiteSmoke, prog_rect);

                    bool _isRenderEllipse = false;
                    float uk;
                    if (IsMouseDown)
                    {
                        //4
                        _isRenderEllipse = true;
                        double jing = _value / 100.0;
                        uk = (float)(prog_rect.Width * jing);
                        if (_isRenderEllipse)
                        {
                            if (uk < prog_rect.Height)
                            {
                                uk = prog_rect.Height;
                            }
                            else if (uk > (prog_rect.Width - prog_rect.Height))
                            {
                                uk = (prog_rect.Width - prog_rect.Height);
                            }
                        }
                    }
                    else
                    {
                        //2
                        uk = (float)(prog_rect.Width * (_value / _maxvalue));
                        if (IsMouseHove)
                        {
                            _isRenderEllipse = true;

                            if (_isRenderEllipse)
                            {
                                if (uk < prog_rect.Height)
                                {
                                    uk = prog_rect.Height;
                                }
                                else if (uk > (prog_rect.Width - prog_rect.Height))
                                {
                                    uk = (prog_rect.Width - prog_rect.Height);
                                }
                            }
                        }
                    }
                    g.FillRectangle(_ValueColor, new RectangleF(prog_rect.X, prog_rect.Y, uk, prog_rect.Height));
                    if (_isRenderEllipse)
                    {
                        g.FillEllipse(solidBrush_White, new RectangleF(prog_rect.X + uk - prog_rect.Height, prog_rect.Y - (prog_rect.Height / 2), prog_rect.Height * 2, prog_rect.Height * 2));
                    }
                }

            }
            catch { }

            base.OnPaint(e);
        }

        ControlAnimations controlAnimations = new ControlAnimations();

        #region 属性

        #region 进度
        double _value;
        [Description("当前值"), Category("TSkin样式"), DefaultValue(0)]

        public double Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    if (value >= _maxvalue)
                    {
                        _value = _maxvalue;
                    }
                    else if (value < 0)
                    {
                        _value = 0;
                    }
                    else
                    {
                        _value = value;
                    }
                    if (!IsMouseDown)
                    {
                        Invalidate();
                    }
                }
            }
        }

        double _maxvalue = 100;
        [Description("最大值"), Category("TSkin样式"), DefaultValue(100)]
        public double MaxValue
        {
            get { return _maxvalue; }
            set
            {
                if (_maxvalue != value)
                {
                    _maxvalue = value;
                    if (!IsMouseDown)
                    {
                        Invalidate();
                    }
                }
            }
        }
        public bool IsMouseHove = false;
        public bool IsMouseDown = false;

        [Description("默认背景颜色"), Category("TSkin样式"), DefaultValue(typeof(Color), "40, 255, 255, 255")]
        public Color DefaultColor
        {
            get => solidBrush_WhiteSmoke.Color;
            set
            {
                if (solidBrush_WhiteSmoke.Color != value)
                {
                    solidBrush_WhiteSmoke = new SolidBrush(value);
                    this.Invalidate();
                }
            }
        }

        #endregion

        #endregion

        #region 进度条事件
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseDown && e.Button == MouseButtons.Left)
            {
                double valueTemp = ((e.X - prog_rect.X) * 1.0) / (prog_rect.Width * 1.0);
                double un = valueTemp * _maxvalue;
                if (valueTemp * _maxvalue < 0)
                {
                    Value = 0;
                    Invalidate();
                    if (ValueChange != null) { ValueChange(0); }
                }
                else
                {
                    Value = valueTemp * 100.0;
                    Invalidate();
                    if (ValueChange != null) { ValueChange(_value); }
                }
            }
            else
            {
                if (_maxvalue > 0 && !IsMouseHove)
                {
                    IsMouseHove = true;
                    controlAnimations.Move(this, 4, 6, 300, AnimationType.Ball);
                }

                base.OnMouseMove(e);
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.Cursor = Cursors.Default;
            if (IsMouseDown || IsMouseHove)
            {
                IsMouseDown = false;
                IsMouseHove = false;
                controlAnimations.Move(this, 6, 4, 300, AnimationType.Ball);
            }
            else
            {
                base.OnMouseLeave(e);
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (IsMouseDown && e.Button == MouseButtons.Left)
            {
                IsMouseDown = false;
                double valueTemp = ((e.X - prog_rect.X) * 1.0) / (prog_rect.Width * 1.0);

                if (valueTemp * _maxvalue < 0)
                {
                    Value = 0;
                    if (ValueChange != null) { ValueChange(0); }
                }
                else
                {
                    Value = valueTemp * 100.0;
                    if (ValueChange != null) { ValueChange(_value); }
                }
            }
            else
            {
                base.OnMouseUp(e);
            }
        }
        public delegate void ValueEventHandler(double value);
        public event ValueEventHandler ValueChange;


        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _maxvalue > 0)
            {
                IsMouseDown = true;
                double jv = (Convert.ToDouble(e.X - prog_rect.X) / Convert.ToDouble(prog_rect.Width)) * 100.0;
                if (jv > 0 && jv < 100)
                {
                    _value = jv;
                    Invalidate();
                }
            }
            else
            {
                base.OnMouseDown(e);
            }
        }
        #endregion

        #endregion


    }
}
