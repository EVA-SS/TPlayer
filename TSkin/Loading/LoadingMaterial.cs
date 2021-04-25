using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSkin
{
    public class LoadingMaterial : Control
    {
        #region 构造函数
        public LoadingMaterial()
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

        private SolidBrush colorBrush = new SolidBrush(Color.FromArgb(33, 150, 243));

        [Category("进度"), Description("进度颜色"), DefaultValue(typeof(Color), "33, 150, 243")]
        public Color Color { get { return colorBrush.Color; } set { colorBrush.Color = value; } }

        private int circularWidth = 3;
        [Category("进度"), Description("进度宽度"), DefaultValue(3)]
        public int CircularWidth
        {
            get { return circularWidth; }
            set
            {
                if (circularWidth != value)
                {
                    circularWidth = value;
                }
            }
        }

        private int speed = 1;
        [Category("进度"), Description("进度速度"), DefaultValue(1)]
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
            }
        }

        #endregion

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
                            bool ProgState = false;
                            Action task = () =>
                            {
                                if (!this.IsHandleCreated)
                                {
                                    Thread.Sleep(1000);
                                }

                                while (_state)
                                {
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
                                }
                            };
                            Task.Run(task).ContinueWith((action => { _state = real_state = false; }));

                        }
                    }
                }
            }
        }

        #endregion

        int Prog1 = 2;
        int Prog2 = 0;

        protected override void OnResize(EventArgs e)
        {
            Height = Width;
            base.OnResize(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (State)
            {
                Rectangle rect = ClientRectangle;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                using (Pen p = new Pen(colorBrush, circularWidth))
                {
                    p.LineJoin = LineJoin.Round;
                    e.Graphics.DrawArc(p, new Rectangle(new Point(rect.X + circularWidth / 2, rect.Y + circularWidth / 2), new Size(rect.Width - 1 - circularWidth, rect.Height - 1 - circularWidth)), Prog2, (float)(Prog1 * 3.6));
                }
            }
        }
    }
}
