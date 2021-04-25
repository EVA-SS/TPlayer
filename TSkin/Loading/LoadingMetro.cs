using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ThreadingTimer = System.Threading.Timer;
using UITimer = System.Windows.Forms.Timer;

namespace TSkin
{
    /// <summary>
    /// 表示一个加载圆圈动画
    /// </summary>
    public partial class LoadingMetro : Control
    {
        #region 构造

        public LoadingMetro()
        {
            //双缓冲，禁擦背景
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor,
                true);

            //初始化绘图timer
            _graphicsTmr = new UITimer { Interval = 1 };
            //Invalidate()强制重绘,绘图操作在OnPaint中实现
            _graphicsTmr.Tick += (sender1, e1) => Invalidate(false);

            _dotSize = Width / 10f;

            //初始化"点"
            _dots = new Dot[5];

            Color = Color.White;
        }

        #endregion 构造

        #region 属性

        /// <summary>
        ///     圆心
        /// </summary>
        [Browsable(false)]
        public PointF CircleCenter
        {
            get { return new PointF(Width / 2f, Height / 2f); }
        }

        /// <summary>
        ///     半径
        /// </summary>
        [Browsable(false)]
        public float CircleRadius
        {
            get { return Width / 2f - _dotSize; }
        }

        /// <summary>
        ///     颜色
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("设置\"点\"的前景色")]
        public Color Color { get; set; }

        #endregion 属性

        #region 字段

        //点数组
        private readonly Dot[] _dots;

        //Timers
        private readonly UITimer _graphicsTmr;
        private ThreadingTimer _actionTmr;

        //点大小
        private float _dotSize;

        //是否活动
        private bool _isActived;

        //是否绘制:用于状态重置时挂起与恢复绘图
        private bool _isDrawing = true;

        //Timer计数:用于延迟启动每个点
        private int _timerCount;

        #endregion 字段

        #region 常量

        //动作间隔(Timer)
        private const int ActionInterval = 30;

        //计数基数：用于计算每个点启动延迟：index * timerCountRadix
        private const int TimerCountRadix = 45;

        #endregion 常量

        #region 方法

        //检查是否重置
        private bool CheckToReset()
        {
            return _dots.Count(d => d.Opacity > 0) == 0;
        }

        //初始化点元素
        private void CreateDots()
        {
            for (int i = 0; i < _dots.Length; ++i)
                _dots[i] = new Dot(CircleCenter, CircleRadius);
        }

        /// <summary>
        ///     开关
        /// </summary>
        public bool Switch()
        {
            if (!_isActived)
                Start();
            else
                Stop();

            return _isActived;
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            try
            {
                CreateDots();

                _timerCount = 0;
                foreach (Dot dot in _dots)
                    dot.Reset();

                _graphicsTmr.Start();

                //初始化动作timer
                _actionTmr = new ThreadingTimer(
                    state =>
                    {
                        //动画动作
                        for (int i = 0; i < _dots.Length; i++)
                            if (_timerCount++ > i * TimerCountRadix)
                                _dots[i].DotAction();

                        //是否重置
                        if (CheckToReset())
                        {
                            //重置前暂停绘图
                            _isDrawing = false;

                            _timerCount = 0;

                            foreach (Dot dot in _dots)
                                dot.Reset();

                            //恢复绘图
                            _isDrawing = true;
                        }

                        try
                        {
                            _actionTmr.Change(ActionInterval, Timeout.Infinite);
                        }
                        catch { }
                    },
                    null, ActionInterval, Timeout.Infinite);

                _isActived = true;
            }
            catch { }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            try
            {
                _graphicsTmr.Stop();
                _actionTmr.Dispose();
                _isActived = false;
                Invalidate();
            }
            catch { }
        }

        #endregion 方法

        #region 重写

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (_isActived && _isDrawing)
                {
                    //抗锯齿
                    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                    using (var bmp = new Bitmap(200, 200))
                    {
                        //缓冲绘制
                        using (Graphics bufferGraphics = Graphics.FromImage(bmp))
                        {
                            //抗锯齿
                            bufferGraphics.SmoothingMode = SmoothingMode.HighQuality;
                            foreach (Dot dot in _dots)
                            {
                                var rect = new RectangleF(
                                    new PointF(dot.Location.X - _dotSize / 2, dot.Location.Y - _dotSize / 2),
                                    new SizeF(_dotSize, _dotSize));

                                bufferGraphics.FillEllipse(new SolidBrush(Color.FromArgb(dot.Opacity, Color)),
                                    rect);
                            }
                        }

                        //贴图
                        e.Graphics.DrawImage(bmp, new PointF(0, 0));
                    } //bmp disposed
                }
            }
            catch { }
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Height = Width;
            _dotSize = Width / 12f;

            base.OnResize(e);
        }

        #endregion 重写
        public static class Common
        {
            /// <summary>
            ///     根据半径、角度求圆上坐标
            /// </summary>
            /// <param name="center">圆心</param>
            /// <param name="radius">半径</param>
            /// <param name="angle">角度</param>
            /// <returns>坐标</returns>
            public static PointF GetDotLocationByAngle(PointF center, float radius, int angle)
            {
                var x = (float)(center.X + radius * Math.Cos(angle * Math.PI / 180));
                var y = (float)(center.Y + radius * Math.Sin(angle * Math.PI / 180));

                return new PointF(x, y);
            }
        }
        /// <summary>
        /// 表示一个"点"
        /// </summary>
        internal sealed class Dot
        {
            #region 字段/属性

            //圆心
            private readonly PointF _circleCenter;
            //半径
            private readonly float _circleRadius;

            /// <summary>
            /// 当前帧绘图坐标，在每次DoAction()时重新计算
            /// </summary>
            public PointF Location;

            //点相对于圆心的角度，用于计算点的绘图坐标
            private int _angle;
            //透明度
            private int _opacity;
            //动画进度
            private int _progress;
            //速度
            private int _speed;

            /// <summary>
            /// 透明度
            /// </summary>
            public int Opacity
            {
                get { return _opacity < MinOpacity ? MinOpacity : (_opacity > MaxOpacity ? MaxOpacity : _opacity); }
            }

            #endregion

            #region 常量

            //最小/最大速度
            private const int MinSpeed = 2;
            private const int MaxSpeed = 11;

            //出现区的相对角度        
            private const int AppearAngle = 90;
            //减速区的相对角度
            private const int SlowAngle = 225;
            //加速区的相对角度
            private const int QuickAngle = 315;

            //最小/最大角度
            private const int MinAngle = 0;
            private const int MaxAngle = 360;

            //淡出速度
            private const int AlphaSub = 25;

            //最小/最大透明度
            private const int MinOpacity = 0;
            private const int MaxOpacity = 255;

            #endregion 常量

            #region 构造器

            public Dot(PointF circleCenter, float circleRadius)
            {
                Reset();
                _circleCenter = circleCenter;
                _circleRadius = circleRadius;
            }

            #endregion 构造器

            #region 方法

            /// <summary>
            /// 重新计算当前帧绘图坐标
            /// </summary>
            private void ReCalcLocation()
            {
                Location = Common.GetDotLocationByAngle(_circleCenter, _circleRadius, _angle);
            }

            /// <summary>
            /// 点动作
            /// </summary>
            public void DotAction()
            {
                switch (_progress)
                {
                    case 0:
                        {
                            _opacity = MaxOpacity;
                            AddSpeed();
                            if (_angle + _speed >= SlowAngle && _angle + _speed < QuickAngle)
                            {
                                _progress = 1;
                                _angle = SlowAngle - _speed;
                            }
                        }
                        break;
                    case 1:
                        {
                            SubSpeed();
                            if (_angle + _speed >= QuickAngle || _angle + _speed < SlowAngle)
                            {
                                _progress = 2;
                                _angle = QuickAngle - _speed;
                            }
                        }
                        break;
                    case 2:
                        {
                            AddSpeed();
                            if (_angle + _speed >= SlowAngle && _angle + _speed < QuickAngle)
                            {
                                _progress = 3;
                                _angle = SlowAngle - _speed;
                            }
                        }
                        break;
                    case 3:
                        {
                            SubSpeed();
                            if (_angle + _speed >= QuickAngle && _angle + _speed < MaxAngle)
                            {
                                _progress = 4;
                                _angle = QuickAngle - _speed;
                            }
                        }
                        break;
                    case 4:
                        {
                            SubSpeed();
                            if (_angle + _speed >= MinAngle && _angle + _speed < AppearAngle)
                            {
                                _progress = 5;
                                _angle = MinAngle;
                            }
                        }
                        break;
                    case 5:
                        {
                            AddSpeed();
                            FadeOut();
                        }
                        break;
                }

                //移动
                _angle = _angle >= (MaxAngle - _speed) ? MinAngle : _angle + _speed;
                //重新计算坐标
                ReCalcLocation();
            }

            //淡出
            private void FadeOut()
            {
                if ((_opacity -= AlphaSub) <= 0)
                    _angle = AppearAngle;
            }

            //重置状态
            public void Reset()
            {
                _angle = AppearAngle;
                _speed = MinSpeed;
                _progress = 0;
                _opacity = 1;
            }

            //加速
            private void AddSpeed()
            {
                if (++_speed >= MaxSpeed) _speed = MaxSpeed;
            }

            //减速
            private void SubSpeed()
            {
                if (--_speed <= MinSpeed) _speed = MinSpeed;
            }

            #endregion 方法
        }
    }
    
}