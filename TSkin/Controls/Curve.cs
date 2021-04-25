using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace TSkin
{
    public partial class Curve : Control
    {
        public Curve()
        {
            //双缓冲，禁擦背景
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        #region 属性
        bool topToBottom = false;
        [Description("曲线图由上向下绘制"), Category("外观"), DefaultValue(false)]
        public bool TopToBottom
        {
            get => topToBottom;
            set
            {
                if (topToBottom != value)
                {
                    topToBottom = value;
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        int maxCount = 60;
        [Description("曲线图最大长度"), Category("外观"), DefaultValue(60)]
        public int MaxCount
        {
            get => maxCount;
            set
            {
                if (maxCount != value)
                {
                    maxCount = value;
                }
            }
        }

        Color valueBack = Color.FromArgb(130, 215, 146);
        [Description("进度颜色"), Category("外观"), DefaultValue(typeof(Color), "130, 215, 146")]
        public Color ValColor
        {
            get => valueBack;
            set
            {
                if (valueBack != value)
                {
                    valueBack = value;
                    this.Invalidate();
                }
            }
        }

        Color value2Back = Color.Transparent;
        [Description("进度颜色2"), Category("外观"), DefaultValue(typeof(Color), "Transparent")]
        public Color ValColor2
        {
            get => value2Back;
            set
            {
                if (value2Back != value)
                {
                    value2Back = value;
                    this.Invalidate();
                }
            }
        }
        List<float> SpeedList = new List<float>();
        public void Add(float val)
        {
            lock (SpeedList)
            {
                while (SpeedList.Count > maxCount)
                {
                    SpeedList.RemoveAt(0);
                }

                SpeedList.Add(val);
            }
            this.OnSizeChanged(null);
            this.Invalidate();
        }
        public void Clear()
        {
            lock (SpeedList)
            {
                if (SpeedList.Count > 0)
                {
                    SpeedList.Clear();
                    this.OnSizeChanged(null);
                    this.Invalidate();
                }
            }
        }

        #endregion


        PointF GetPointByProgressAndSpeed(float width, int progress, float speed, float max_speed)
        {
            float p1 = (float)Math.Round(width * progress, 3);
            float p = speed * Height / max_speed;
            float p2 = topToBottom ? p : Height - p;
            if (p1 > 0)
            {
                return new PointF(p1, p2);
            }
            else { return new PointF(0, p2); }
        }


        List<PointF> _points = new List<PointF>();

        protected override void OnSizeChanged(EventArgs e)
        {
            lock (SpeedList)
            {
                Rectangle rect = ClientRectangle;
                if (SpeedList.Count > 1)
                {
                    float max_speed = SpeedList.Max();
                    max_speed += max_speed / 3;
                    lock (_points)
                    {
                        _points.Clear();

                        for (int i = 0; i < SpeedList.Count; i++)
                        {
                            _points.Add(GetPointByProgressAndSpeed(rect.Width / (SpeedList.Count - 1), i, SpeedList[i], max_speed));
                        }
                        if (topToBottom)
                        {
                            _points.Insert(0, new PointF(0, 0));
                            _points.Add(new PointF(rect.Width, 0));
                        }
                        else
                        {
                            _points.Insert(0, new PointF(0, rect.Height));
                            _points.Add(new PointF(rect.Width, rect.Height));
                        }
                    }
                }
                else
                {
                    lock (_points)
                    {
                        _points.Clear();
                    }
                }

            }
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            //抗锯齿
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

            #region 进度条

            lock (_points)
            {
                if (_points.Count > 0)
                {
                    Rectangle rect = ClientRectangle;

                    //float max_speed = SpeedList.Max();
                    //max_speed += max_speed / 2;
                    //进度
                    using (LinearGradientBrush brush = new LinearGradientBrush(rect, valueBack, value2Back, LinearGradientMode.Vertical))
                    {
                        g.FillClosedCurve(brush, _points.ToArray(), FillMode.Winding);
                    }

                }
            }

            #endregion

            base.OnPaint(e);
        }
    }
}
