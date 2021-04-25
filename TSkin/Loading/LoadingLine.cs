using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSkin
{
    public partial class LoadingLine : Control
    {
        public LoadingLine()
        {
            //双缓冲，禁擦背景
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor,
                true);
        }
        protected override void Dispose(bool disposing)
        {
            _state = false;
            base.Dispose(disposing);
        }
        private Color _color1;
        public Color Color1 { get { return _color1; } set { _color1 = value; } }
        private Color _color2;
        public Color Color2 { get { return _color2; } set { _color2 = value; } }


        #region 动画开关

        [Category("进度"), Description("动画真实状态")]
        public bool RealState
        {
            get { return real_state; }
        }

        bool _state = false, real_state = false;
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
                            Action task = () =>
                            {
                                if (!this.IsHandleCreated)
                                {
                                    Thread.Sleep(1000);
                                }

                                while (_state)
                                {
                                    rects.Y = bbbq;
                                    bbbq += 5;
                                    if (bbbq > Height * 2)
                                    {
                                        bbbq = 0;
                                    }
                                    Thread.Sleep(100);
                                    Invalidate(rect);
                                }
                            };
                            Task.Run(task).ContinueWith((action => { _state = real_state = false; }));
                        }
                    }
                }
            }
        }

        #endregion
        int bbbq = 0;

        Rectangle rects = new Rectangle(0, 0, 0, 5);
        Rectangle rect = new Rectangle(0, 0, 0, 5);
        protected override void OnSizeChanged(EventArgs e)
        {
            rect.Size = this.Size;
            rects.Size = this.Size;
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (State)
            {
                var g = e.Graphics;
                //抗锯齿
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

                LinearGradientBrush brush = new LinearGradientBrush(rects, _color2, _color1, LinearGradientMode.ForwardDiagonal);
                brush.SetSigmaBellShape(0.5f);
                g.FillRectangle(brush, rect);
            }
            base.OnPaint(e);
        }
    }
}
