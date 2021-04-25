using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System;
using Animations;
using System.Collections.Generic;

namespace TSkin
{
    public partial class TomLoadingBut : Control
    {
        public bool LoadState = true;
        private readonly AnimationManager rippleAnimationManager;
        public TomLoadingBut()
        {
            pen = new Pen(Color.DodgerBlue, 2);
            pen.LineJoin = LineJoin.Round;
            pentwo = new Pen(Color.DodgerBlue, 6);
            pentwo.LineJoin = LineJoin.Round;
            //双缓冲，禁擦背景
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            rippleAnimationManager = new AnimationManager(false)
            {
                AnimationType = AnimationType.Linear,
                //Increment = 0.10,
                Increment = 0.05,
                SecondaryIncrement = 0
            };
            rippleAnimationManager.OnAnimationProgress += sender => Invalidate(btn_play_rect_render);
        }

        #region 样式
        SolidBrush _BlockColor = new SolidBrush(Color.FromArgb(40, Color.DodgerBlue));
        Font font = new Font("微软雅黑", 10);
        Font fontw = new Font("微软雅黑", 8);
        StringFormat stringFormat = new StringFormat { LineAlignment = StringAlignment.Center };
        SolidBrush solidBrush_White = new SolidBrush(Color.White);
        //SolidBrush solidBrush_White_Ok = new SolidBrush(Color.White);
        SolidBrush solidBrush_WhiteSmoke = new SolidBrush(Color.FromArgb(190, 255, 255, 255));
        Pen pen;
        Pen pentwo;
        Point animationSource;
        #endregion

        #region 进度
        double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    if (value >= _maxvalue)
                    {
                        _value = 0;
                        //s.s.Platlistz();
                    }
                    else
                    {
                        _value = value;
                    }
                    if (!IsMouseDown)
                    {
                        Invalidate(prog_rect);
                    }
                }
            }
        }

        double _maxvalue;
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
                        Invalidate(prog_rect);
                    }
                }
            }
        }
        public bool IsMouseDown = false;
        public int Leng = 0;
        List<TPlayer._Buffe> _buffvalue = null;
        /// <summary>
        /// 当前缓存进度
        /// </summary>
        public List<TPlayer._Buffe> BufferValue
        {
            get { return _buffvalue; }
            set
            {
                if (_buffvalue != value)
                {
                    _buffvalue = value;
                    if (!IsMouseDown)
                    {
                        Invalidate(prog_rect);
                    }
                }
            }
        }
        #endregion
        #region 进度条事件
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseDown && e.Button == MouseButtons.Left)
            {
                double jv = (Convert.ToDouble(e.X- prog_rect.X) / Convert.ToDouble(prog_rect.Width)) * 100.0;
                if (jv > 0 && jv < 100)
                {
                    _value = jv;
                    Invalidate(prog_rect);
                }
            }
            else
            {
                base.OnMouseMove(e);
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (IsMouseDown)
            {
                IsMouseDown = false;
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
                Value = (Convert.ToDouble(e.X - prog_rect.X) / Convert.ToDouble(prog_rect.Width)) * 100.0;
                double un = Value / 100.0 * _maxvalue;

                if (un >= _maxvalue)
                {
                    Value = 0;
                    //下一个
                }
                else if (un < 0)
                {
                    if (ValueChange != null) { ValueChange(0); }
                }
                else
                {
                    if (ValueChange != null) { ValueChange(un); }
                }
            }
            else
            {
                base.OnMouseUp(e);
            }
        }
        public delegate void ValueEventHandler(double value);
        public event ValueEventHandler ValueChange;


        public delegate void ClickEventHandler(int value);
        public event ClickEventHandler MClick;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _maxvalue > 0 && prog_rect.Contains(e.Location))
            {
                IsMouseDown = true;
            }
            else if (lonrect.Contains(e.Location))
            {
                if (MClick != null)
                { MClick(0); }
            }
            else if (btn_stop_rect.Contains(e.Location))
            {
                if (MClick != null)
                { MClick(1); }
            }
            else if (btn_next_rect.Contains(e.Location))
            {
                if (MClick != null)
                { MClick(2); }
            }
            else
            {
                base.OnMouseDown(e);
            }
        }
        #endregion

        protected override void OnSizeChanged(EventArgs e)
        {
            SetSize();
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                var g = e.Graphics;
                //抗锯齿
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
                //g.DrawArc(pen, lonsrect, 0, 360f);
                if (rippleAnimationManager.IsAnimating())
                {
                    for (int i = 0; i < rippleAnimationManager.GetAnimationCount(); i++)
                    {
                        double animationValue = rippleAnimationManager.GetProgress(i);
                        if (animationValue > 0)
                        {
                            float wi = ((float)animationValue * (btn_play_rect_render.Width)) + lonrect.Height;
                            float gogo = (Height - wi) / 2;
                            RectangleF recc = new RectangleF(gogo, gogo, wi, wi);
                            g.DrawEllipse(pentwo, recc);
                        }
                    }
                }
                else if (!_IsPlay)
                {
                    g.DrawEllipse(pen, lonrect);
                }
                #region 绘制进度条
                double p = _value;
                double jing;
                if (!IsMouseDown)
                {
                    jing = Convert.ToDouble(p) / Convert.ToDouble(_maxvalue) * 100.0;
                }
                else
                {
                    jing = p;
                }
                //SolidBrush colorBrush = new SolidBrush(Color.White);
                if (jing >= 0)
                {
                    if (_buffvalue != null)
                    {
                        if (_buffvalue.Count == 1 && _buffvalue[0].Index == -10)
                        { g.FillRectangle(solidBrush_White, new RectangleF(prog_rect.X, prog_rect.Height - 1, prog_rect.Width, 1)); }
                        else
                        {
                            float _with = (float)prog_rect.Width / Leng;
                            foreach (TPlayer._Buffe item in _buffvalue)
                            {
                                RectangleF rectangleF = new RectangleF(prog_rect.X + (item.Index * _with), prog_rect.Height - 3, item.Leng * _with, 3);
                                g.FillRectangle(solidBrush_White, rectangleF);
                            }
                        }
                    }
                    float uk = (float)(prog_rect.Width * (jing / 100.0));
                    RectangleF j = new RectangleF(prog_rect.X, prog_rect.Y, uk, prog_rect.Height);
                    g.FillRectangle(_BlockColor, j);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(10, 255, 255, 255)), prog_rect);
                }
                #endregion


                g.DrawImage(TPlayer.Properties.Resources.btn_volume, btn_volume_rect);

                g.DrawImage(TPlayer.Properties.Resources.btn_stop, btn_stop_rect);
                if (_IsNext)
                {
                    g.DrawImage(TPlayer.Properties.Resources.btn_next, btn_next_rect);
                }
                g.DrawImage(_IsPlay ? TPlayer.Properties.Resources.btn_pause : TPlayer.Properties.Resources.btn_play, btn_play_rect);

            }
            catch { }
            base.OnPaint(e);
        }

        bool _IsPlay = false;
        public bool IsPlay
        {
            get { return _IsPlay; }
            set
            {
                if (_IsPlay != value)
                {
                    _IsPlay = value;
                    Invalidate(btn_play_rect_render);
                    if (LoadState)
                    {
                        rippleAnimationManager.Clear();
                        rippleAnimationManager.StartNewAnimation(_IsPlay ? AnimationDirection.In : AnimationDirection.Out, animationSource);
                    }
                }
            }
        }

        bool _IsNext = true;
        public bool IsNext
        {
            get { return _IsNext; }
            set
            {
                if (_IsNext != value)
                {
                    _IsNext = value;
                    SetSize();
                    Invalidate();
                }
            }
        }
        Rectangle btn_play_rect_render;
        Rectangle btn_play_rect;
        public Rectangle lonrect;
        public Rectangle btn_stop_rect;
        public Rectangle btn_next_rect;
        public Rectangle prog_rect;
        public Rectangle btn_volume_rect;
        void SetSize()
        {
            Rectangle rect = ClientRectangle;
            //int _lon = rect.Height / 2;
            int _lon1 = rect.Height - 20;
            int _lon2 = rect.Height - 26;
            animationSource = new Point((btn_play_rect.X + btn_play_rect.Width) / 2, (btn_play_rect.Y + btn_play_rect.Height) / 2);
            btn_play_rect_render = new Rectangle(0, 0, rect.Height * 2, rect.Height);
            btn_play_rect = new Rectangle(13, 13, _lon2, _lon2);
            lonrect = new Rectangle(10, 10, _lon1, _lon1);

            int _lon3 = rect.Height - 44;
            btn_next_rect = new Rectangle(rect.Height+11, 22, _lon3, _lon3);

            int _lon4 = rect.Height - 46;
            int _X = _IsNext ? btn_next_rect.X + btn_next_rect.Width + _lon4 : rect.Height + _lon3;
            btn_stop_rect = new Rectangle(_X, 23, _lon4, _lon4);
            _X = _X + rect.Height / 2;

            int _lon5 = rect.Height - 30;
            btn_volume_rect = new Rectangle(rect.Width - rect.Height+15, 15, _lon5, _lon5);

            prog_rect = new Rectangle(_X, 0, rect.Width- _X-rect.Height, rect.Height);
            //if (_IsNext)
            //{
            //}
            //else { }
            //btn_next_rect = new Rectangle(rect.Height + 11, 22, _lon3, _lon3);
        }
    }
}
