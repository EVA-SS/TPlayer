using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSkin
{
    public partial class LoadingMetroHorizontal : Control
    {
        #region 构造函数

        List<float> Cirular = new List<float>();

        public LoadingMetroHorizontal()
        {
            //双缓冲，禁擦背景
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
            _state = false;
            base.Dispose(disposing);
        }

        #endregion

        #region 属性

        private int _DotCount = 5;
        [Category("进度"), Description("动画点数"), DefaultValue(5)]
        public int DotCount
        {
            get { return _DotCount; }
            set
            {
                if (_DotCount != value)
                {
                    _DotCount = value;
                    OnSizeChanged(null)
                        ;
                }
            }
        }

        private int _cirularwidth = 4;
        [Category("进度"), Description("圆圈宽度"), DefaultValue(4)]
        public int CirularWidth
        {
            get
            {
                return _cirularwidth;
            }
            set
            {
                if (_cirularwidth != value)
                {
                    _cirularwidth = value; OnSizeChanged(null);
                }
            }
        }

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

        #region 进度颜色

        private SolidBrush solidBrush = new SolidBrush(Color.FromArgb(33, 150, 243));

        [Category("进度"), Description("进度颜色"), DefaultValue(typeof(Color), "33, 150, 243")]
        public Color Color { get { return solidBrush.Color; } set { solidBrush.Color = value; } }

        #endregion

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

        #endregion

        #region 动画开关

        [Category("进度"), Description("动画真实状态")]
        public bool RealState
        {
            get { return real_state; }
        }

        bool _state = false, real_state = false, change = false;
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
                        change = false;
                        real_state = true;
                        Action task = () =>
                        {
                            _Animation();
                        };
                        Task.Run(task).ContinueWith((action =>
                        {
                            _state = real_state = false;
                            this.Invoke(new Action(() =>
                            {
                                this.Invalidate();
                            }));
                        }));
                    }
                }
            }
        }

        void _Animation()
        {
            if (!this.IsHandleCreated)
            {
                Thread.Sleep(1000);
            }

            int DotCount = this.DotCount;
            Rectangle rect = this.rect;
            RectangleF rect_down = this.rect_down;
            double _temp_down = this._temp_down;
            double f_down = this.f_down;
            List<int> Cirular_OK = new List<int>();
            Clear(10);

            while (true)
            {
                if (!_state && !EndStop)
                {
                    return;
                }
                if (Cirular_OK.Count == Cirular.Count)
                {
                    if (!_state)
                    {
                        return;
                    }

                    Thread.Sleep(500);

                    if (change)
                    {
                        DotCount = this.DotCount;
                        rect = this.rect;
                        rect_down = this.rect_down;
                        _temp_down = this._temp_down;
                        f_down = this.f_down;
                        change = false;
                    }

                    Clear(80);
                    Cirular_OK.Clear();
                }
                else
                {
                    for (int i = 0; i < DotCount; i++)
                    {
                        Getint(Cirular_OK, i, rect, rect_down, _temp_down, f_down);
                    }
                    Thread.Sleep(10);
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.Invalidate();
                        }));
                    }
                }
            }
        }

        #endregion

        #region 坐标计算

        Rectangle rect;
        RectangleF rect_down;
        int _cirulary = 0;
        int width_down = 0;
        double _temp_down = 0;
        double f_down = 0;
        protected override void OnSizeChanged(EventArgs e)
        {
            rect = ClientRectangle;
            _cirulary = (rect.Height - _cirularwidth) / 2;
            width_down = rect.Width / 4;
            f_down = width_down / 3.0;
            rect_down = new RectangleF((rect.Width - width_down) / 2, 0, width_down, rect.Height);
            _temp_down = rect_down.X + rect_down.Width;

            change = true;
            base.OnSizeChanged(e);
        }

        #endregion

        #region 辅助方法

        private void Clear(float val, float speed = 70)
        {
            List<float> Cirular = new List<float>();
            for (int i = 0; i < DotCount; i++)
            {
                Cirular.Add(-(val + (speed * i)));
            }
            this.Cirular = Cirular;
        }
        private void Getint(List<int> Cirular_OK, int i, Rectangle rect, RectangleF rect_down, double _temp_down, double f_down)
        {
            if (!Cirular_OK.Contains(i))
            {
                double value = Cirular[i];
                if (value > rect_down.X && value < _temp_down)
                {
                    if (Cirular[i] > 1)
                    {
                        double bb = (rect.Width - value) / (100.0 + f_down);
                        Cirular[i] += (float)bb;
                    }
                }
                else
                {
                    if (rect.Width > value)
                    {
                        if (value > _temp_down)
                        {
                            double bb = (rect.Width - (rect.Width - value)) / 100.0;
                            Cirular[i] += (float)bb;
                        }
                        else
                        {
                            double bb = (rect.Width - value) / 100.0;
                            Cirular[i] += (float)bb;
                        }
                    }
                    else
                    {
                        Cirular[i] = -10;
                        Cirular_OK.Add(i);
                    }
                }
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            //g.FillRectangle(new SolidBrush(Color.Blue), rect);
            //g.FillRectangle(new SolidBrush(Color.Red), rect_down);
            if ((real_state && Cirular.Count > 0) || (_Value > 0 && _MaxValue > 0))
            {
                var g = e.Graphics;
                //抗锯齿
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

                if (real_state && Cirular.Count > 0)
                {

                    for (int i = 0; i < Cirular.Count; i++)
                    {
                        var rect = new RectangleF(
                            new PointF((Cirular[i] - (_cirularwidth / 2)), _cirulary),
                            new SizeF(_cirularwidth, _cirularwidth));
                        g.FillEllipse(solidBrush, rect);

                    }
                }
                if (_Value > 0 && _MaxValue > 0)
                {
                    Rectangle _rect = ClientRectangle;
                    double TTT = _rect.Width * (_Value / _MaxValue);
                    if (TTT > 0)
                    {
                        g.FillRectangle(solidBrush, new RectangleF(_rect.X, _rect.Y, (float)TTT, _rect.Height));
                    }
                }
            }
            base.OnPaint(e);
        }
    }
}
