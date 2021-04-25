///
/// A module that deal with animation of control
/// Auther: Exoticknight
/// Last edited: 2013/5/28
/// 

/// List of available static functions:
/// ------------------------------------------------------
/// void HorizontalMove
/// (
///     Control control,
///     int endLeft,
///     int lastTime,
///     AnimationType animationType
/// )
/// Horizontally move control until control.Left == [endLeft] in given [lastTime], using [animationType]
/// ------------------------------------------------------


using System;
using System.Windows.Forms;

namespace TPlayer
{
    public class ControlAnimations : IDisposable
    {
        #region 释放
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_timer != null)
            {
                try
                {
                    _timer.Enabled = false;
                    _timer.Dispose();
                    _timer = null;
                }
                catch { }
            }
        }
        public void Close()
        {
            this.Dispose(true);
        }
        #endregion

        public delegate void JEventHandler(double value);
        public event JEventHandler JClick;

        public event JEventHandler OKClick;

        private static double CalculateValue(AnimationType animationType, double x)
        {
            double _y = 1;
            switch (animationType)
            {
                case AnimationType.Liner:
                    _y = x;
                    break;
                case AnimationType.Ease:
                    _y = Math.Sqrt(x);
                    break;
                case AnimationType.Ball:
                    _y = Math.Sqrt(1.0 - Math.Pow(x - 1, 2));
                    break;
                case AnimationType.Resilience:
                    _y = -10.0 / 6.0 * x * (x - 1.6);
                    break;
            }
            return _y;
        }

        /// <summary>
        /// A class that store a set of animation of the control
        /// </summary>
        class AnimationStatus
        {
            AnimationType _animationType;
            double _initValue;
            double _endValue;
            double _totalValue;
            int _totalFrames;
            int _currentFrames;

            /// <summary>
            /// type of the animation, such as liner, Ease...
            /// </summary>
            public AnimationType AnimationType
            {
                get { return _animationType; }
            }
            /// <summary>
            /// current value of the attribute that is ready to change
            /// </summary>
            public double InitValue
            {
                get { return _initValue; }
            }
            /// <summary>
            /// final value of the attribute that is ready to change
            /// </summary>
            public double EndValue
            {
                get { return _endValue; }
            }
            /// <summary>
            /// total value that changed
            /// </summary>
            public double TotalValue
            {
                get { return _totalValue; }
            }
            /// <summary>
            /// total frames the animation should play, READONLY
            /// </summary>
            public int TotalFrames
            {
                get { return _totalFrames; }
            }
            /// <summary>
            /// current frames the animation has played
            /// </summary>
            public int CurrentFrames
            {
                get { return _currentFrames; }
                set { _currentFrames = value; }
            }

            // contructor
            public AnimationStatus(double initValue, double endValue, int totalFrames, AnimationType animationType)
            {
                this._animationType = animationType;
                this._initValue = initValue;
                this._endValue = endValue;
                this._totalValue = Math.Abs(this._endValue - this._initValue);
                this._totalFrames = totalFrames;
                this._currentFrames = 1;
            }
        }


        Timer _timer;
        /// <summary>
        /// common function of moving control
        /// </summary>
        /// <param name="contorl">the control to be moved</param>
        /// <param name="timer">the timer that control the time of animation</param>
        /// <param name="animationStatue">current statue of animation</param>
        private bool Animate(out double value, AnimationStatus animationStatue)
        {
            if (animationStatue.CurrentFrames > animationStatue.TotalFrames)
            {
                value = 0;
                return false;
            }

            double _progress = (double)animationStatue.CurrentFrames / (double)animationStatue.TotalFrames;

            value =
            animationStatue.InitValue < animationStatue.EndValue ?
            animationStatue.InitValue + Math.Round(animationStatue.TotalValue * CalculateValue(animationStatue.AnimationType, _progress)) :
            animationStatue.InitValue - Math.Round(animationStatue.TotalValue * CalculateValue(animationStatue.AnimationType, _progress));


            animationStatue.CurrentFrames++;
            return true;
        }

        public void Move(Control control, double start, double end, int lastTime, AnimationType animationType)
        {
            Dispose();

            int _frames = lastTime % 15 > 0 ? lastTime / 15 + 1 : lastTime / 15;
            AnimationStatus animationStatue = new AnimationStatus(start, end, _frames, animationType);
            _timer = new Timer();
            _timer.Interval = 15;
            _timer.Tick += delegate
            {
                double value;
                bool isTask = Animate(out value, animationStatue);
                if (isTask)
                {
                    if (JClick != null)
                        JClick(value);
                }
                else
                {
                    Dispose();
                    if (OKClick != null)
                        OKClick(end);
                }
            };
            _timer.Enabled = true;
            _timer.Start();
        }

    }
}
