using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSkin
{
    public class LoadingMaterialHorizonta : Control
    {
        #region 构造函数

        public LoadingMaterialHorizonta()
        {
            SetStyle(
                 ControlStyles.UserPaint |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer, true);
            UpdateStyles();
        }
        protected override void Dispose(bool disposing)
        {
            State = false;
            base.Dispose(disposing);
        }

        #endregion

        #region 属性

        private bool _endStop = true;
        [Category("进度"), Description("是否等待最后一次动画结束"), DefaultValue(true)]
        public bool EndStop
        {
            get
            {
                return _endStop;
            }
            set
            {
                if (_endStop != value)
                {
                    _endStop = value;

                }
            }
        }


        private SolidBrush colorBrush = new SolidBrush(Color.FromArgb(33, 150, 243));

        [Category("进度"), Description("进度颜色"), DefaultValue(typeof(Color), "33, 150, 243")]
        public Color Color { get { return colorBrush.Color; } set { colorBrush.Color = value; } }


        private SolidBrush progcolorBrush = new SolidBrush(Color.FromArgb(30, 140, 230));

        [Category("进度"), Description("进度颜色"), DefaultValue(typeof(Color), "30, 140, 230")]
        public Color ProgColor { get { return progcolorBrush.Color; } set { progcolorBrush.Color = value; } }


        private double _Value = 0.0;
        [Category("进度"), Description("当前值"), DefaultValue(0.0)]
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


        private double _MaxValue = 100.0;
        [Category("进度"), Description("最大值"), DefaultValue(100.0)]
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

        private bool _isLeft = false;
        [Category("进度"), Description("动画方向状态"), DefaultValue(false)]
        public bool isLeft
        {
            get
            {
                return _isLeft;
            }
            set
            {
                if (_isLeft != value)
                {
                    _isLeft = value;
                }
            }
        }


        #endregion

        #region 动画开关

        [Category("进度"), Description("动画真实状态")]
        public bool RealState
        {
            get { return real_state; }
        }

        bool _state = false, real_state = false, change = false, isMo = false;
        [Category("进度"), Description("动画状态"), DefaultValue(false)]
        public bool State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    if (_state)
                    {
                        if (!real_state)
                        {
                            real_state = true;
                            change = false;
                            Action task = () =>
                            {
                                if (!this.IsHandleCreated)
                                {
                                    Thread.Sleep(1000);
                                }

                                int _width = this._width, _width_min = this._width_min;
                                float prog = this.prog, prog_min = this.prog_min;

                                while (_state)
                                {
                                    if (!_state && !EndStop)
                                    {
                                        return;
                                    }
                                    if (rect.X > _width)
                                    {
                                        if (!_state)
                                        {
                                            return;
                                        }
                                        isMo = !isMo;

                                        if (change)
                                        {
                                            _width = this._width;
                                            _width_min = this._width_min;

                                            prog = this.prog;
                                            prog_min = this.prog_min;
                                            change = false;
                                        }

                                        rect.Width = _width;

                                        rect.X = -_width;
                                        rect.Width = isMo ? _width : _width / 2 + _width_min;
                                    }
                                    else
                                    {
                                        if (isMo)
                                        {
                                            rect.Width -= prog_min;
                                        }
                                        else
                                        {
                                            rect.X += prog / 2;
                                        }
                                        rect.X += prog;
                                    }
                                    Thread.Sleep(10);

                                    this.Invoke(new Action(() =>
                                    {
                                        this.Invalidate();
                                    }));
                                }
                            };
                            Task.Run(task).ContinueWith((action => { _state = real_state = false; }));
                        }
                    }
                }
            }
        }

        #endregion

        #region 坐标计算
        RectangleF rect = new RectangleF(0, 0, 0, 0);
        int _width = 0, _width_min = 0;
        float prog = 0, prog_min = 0;
        protected override void OnSizeChanged(EventArgs e)
        {
            //rect = ClientRectangle;
            _width = Width;
            rect.Height = Height;

            prog = ((float)_width) / 40;
            prog_min = (float)Math.Floor(prog / 2.1);
            _width_min = _width / 3;

            change = true;
            base.OnSizeChanged(e);
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            if (real_state || (_Value > 0 && _MaxValue > 0))
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality;//抗锯齿
                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

                if (real_state)
                {
                    if (isLeft)
                    {
                        g.TranslateTransform(_width, rect.Height);
                        g.RotateTransform(180);
                    }
                    g.FillRectangle(colorBrush, rect);
                }
                if (_Value > 0 && _MaxValue > 0)
                {
                    Rectangle _rect = ClientRectangle;
                    double TTT = _rect.Width * (_Value / _MaxValue);
                    if (TTT > 0)
                    {
                        g.FillRectangle(progcolorBrush, new RectangleF(_rect.X, _rect.Y, (float)TTT, _rect.Height));
                    }
                }
            }
            base.OnPaint(e);
        }
    }
}
