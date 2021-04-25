using Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TPlayerSupport;
using TSkin;

namespace TPlayer.Com
{
    public partial class Controller : Form
    {
        TPlayer player = null;
        //[DllImport("user32")]
        //public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        public bool LoadState = true;
        private readonly AnimationManager rippleAnimationManager;

        #region 初始化

        bool isLoad_ = false;

        public Controller(TPlayer player)
        {
            this.player = player;

            pen = new Pen(Color.DodgerBlue, 2);
            pen.LineJoin = LineJoin.Round;
            pentwo = new Pen(Color.DodgerBlue, 6);
            pentwo.LineJoin = LineJoin.Round;
            SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.DoubleBuffer, true);
            UpdateStyles();

            rippleAnimationManager = new AnimationManager(false)
            {
                AnimationType = Animations.AnimationType.Linear,
                //Increment = 0.10,
                Increment = 0.04,
                SecondaryIncrement = 0
            };
            rippleAnimationManager.OnAnimationProgress += sender => Print();

            InitializeComponent();
            //tomLoading.MClick += TomLoading_MClick;
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            player.Player_PreviewKeyDown(this, e);
            base.OnPreviewKeyDown(e);
        }

        int isCanClose = -1; 
        void CmClose()
        {
            player.LocationChanged -= Player_LocationSizeChanged;
            player.SizeChanged -= Player_LocationSizeChanged;
            player.Focus();
            lock (player._controller)
            {
                player._controller = null;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (isCanClose == 1)
            {
                CmClose();
            }
            else if (isCanClose == -1)
            {
                if (player.IsFullScreen && SystemSettings.Animation)
                {
                    isCanClose = 0;
                    e.Cancel = true;
                    controlAnimation.Move(this, this.Top, this.Top + this.Height, 300, AnimationType.Ball);
                }
                else
                {
                    CmClose();
                }
            }
            else
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }

        public void NoClose()
        {
            isCanClose = -1;
            controlAnimation.Close();

            if (player.IsFullScreen && SystemSettings.Animation)
            {
                controlAnimation.Move(this, this.Top, player.Top + player.Height - this.Height, 300, AnimationType.Ball);
            }
            else
            {
                this.Top = player.Top + player.Height - this.Height;
            }
        }

        ControlAnimations controlAnimations = new ControlAnimations();
        ControlAnimations controlAnimation = new ControlAnimations();
        protected override void OnLoad(EventArgs e)
        {
            Player_LocationSizeChanged(this, e);
            player.LocationChanged += Player_LocationSizeChanged;
            player.SizeChanged += Player_LocationSizeChanged;

            #region 绑定动画

            controlAnimations.OKClick += (value) =>
            {
                prog_rect.Height = (float)value;
                Print();
            };
            controlAnimations.JClick += (value) =>
            {
                prog_rect.Height = (float)value;
                Print();
            };

            controlAnimation.JClick += (value) =>
            {
                this.Top = (int)value;
            };
            controlAnimation.OKClick += (value) =>
            {
                this.Top = (int)value;

                if (isCanClose == 0)
                {
                    //System.Diagnostics.Debug.WriteLine("关闭控制栏了");
                    isCanClose = 1;
                    this.Close();
                }
            };
            #endregion

            isLoad_ = true;
            base.OnLoad(e);
            SetSize();

            if (player.IsFullScreen && SystemSettings.Animation)
            {
                int _top = this.Top;
                int _tops = this.Top + this.Height;
                this.Top += _tops;
                controlAnimation.Move(this, _tops, _top, 300, AnimationType.Ball);
            }
            //else
            //{
            //    AnimateWindow(this.Handle, 200, 0x0008 | 0x20000);
            //}
            //if (!player.LoadState)
            //{
            //    AnimateWindow(this.Handle, 800, 0x0008 | 0x20000);
            //}

        }

        #endregion

        #region 显示判断

        public bool visible_volume = false, visible_download = false, visible_setting = false, visible_photo = false, visible_record = false, visible_tv = false;

        #endregion

        #region 移动坐标

        /// <summary>
        /// 播放暂停圆圈渲染区域
        /// </summary>
        Rectangle btn_play_rect_render;
        /// <summary>
        /// 暂停圆圈默认区域
        /// </summary>
        public Rectangle btn_play_rect_default_render = new Rectangle(10, 10 + 12, 40, 40);
        /// <summary>
        /// 按钮_播放暂停
        /// </summary>
        Rectangle btn_play_rect = new Rectangle(13, 13 + 12, 34, 34);//(60-34)/2
        Point animationSource = new Point(30, 30);
        /// <summary>
        /// 按钮_停止
        /// </summary>
        public Rectangle btn_stop_rect = new Rectangle(0, 0, 14, 14);
        /// <summary>
        /// 按钮_下一首
        /// </summary>
        public Rectangle btn_next_rect = new Rectangle(0, 0, 16, 16);
        /// <summary>
        /// 进度条区域
        /// </summary>
        public RectangleF prog_rect = new RectangleF(0, 6, 0, 4), prog_rects = new RectangleF(0, 0, 0, 14);
        /// <summary>
        /// 进度条时间区域
        /// </summary>
        public Rectangle prog_time_rect;

        public Rectangle btn_fullscreen_rect = new Rectangle(0, 0, 30, 30);
        public Rectangle btn_fullscreen_rects = new Rectangle(0, 0, 24, 24);

        /// <summary>
        /// 按钮_声音
        /// </summary>
        public Rectangle btn_volume_rect = new Rectangle(0, 0, 30, 30);
        public Rectangle btn_volume_rects;
        public Rectangle btn_download_rect = new Rectangle(0, 0, 30, 30);
        public Rectangle btn_download_rects = new Rectangle(0, 0, 20, 20);
        public Rectangle btn_setting_rect = new Rectangle(0, 0, 30, 30);
        public Rectangle btn_photo_rect = new Rectangle(0, 0, 30, 30);
        public Rectangle btn_record_rect = new Rectangle(0, 0, 30, 30);
        public Rectangle btn_record_rects1, btn_record_rects2, btn_record_rects3;
        public Rectangle btn_tv_rect = new Rectangle(0, 0, 30, 30);
        public void SetSize()
        {
            if (isLoad_)
            {
                Rectangle rect = ClientRectangle;
                Rectangle original_rect = new Rectangle(rect.X, rect.Y + 12, rect.Width, rect.Height - 12);

                btn_play_rect_render = new Rectangle(original_rect.X, original_rect.Y, original_rect.Height * 2, original_rect.Height);

                btn_next_rect.Location = new Point(original_rect.Height + (btn_next_rect.Width / 2), original_rect.Y + ((original_rect.Height - btn_next_rect.Height) / 2));



                int _X = _IsNext ? btn_next_rect.X + btn_next_rect.Width + 22 : 70;
                btn_stop_rect.Location = new Point(_X, original_rect.Y + ((original_rect.Height - btn_stop_rect.Height) / 2));
                _X = _X + original_rect.Height / 2;


                int ri_width = original_rect.Height;

                btn_fullscreen_rect.Location = new Point(original_rect.Width - ri_width + (btn_fullscreen_rect.Width / 2), original_rect.Y + (btn_fullscreen_rect.Width / 2));
                btn_fullscreen_rects.Location = new Point(original_rect.Width - ri_width + (btn_fullscreen_rects.Width / 2), original_rect.Y + (btn_fullscreen_rect.Height - btn_fullscreen_rects.Height / 2));

                int readWidth = original_rect.Width - (original_rect.Height * (_IsNext ? 4 : 3));


                if (!player.isMini)
                {
                    ri_width += original_rect.Height;
                    btn_volume_rect.Location = new Point(original_rect.Width - ri_width + (btn_volume_rect.Width / 2), original_rect.Y + (btn_volume_rect.Width / 2));
                    btn_volume_rects = new Rectangle(original_rect.Width - ri_width, 0, original_rect.Height, rect.Height);

                    visible_volume = true;

                    if (!player.isDLNA)
                    {
                        ri_width += original_rect.Height;
                        btn_setting_rect.Location = new Point(original_rect.Width - ri_width + (btn_setting_rect.Width / 2), original_rect.Y + (btn_setting_rect.Width / 2));

                        visible_setting = true;

                        if (readWidth > ri_width + original_rect.Height)
                        {
                            if (player.DownTotalCount > 0)
                            {
                                ri_width += original_rect.Height;
                                btn_download_rect.Location = new Point(original_rect.Width - ri_width + (btn_download_rect.Width / 2), original_rect.Y + (btn_download_rect.Width / 2));
                                btn_download_rects.Location = new Point(original_rect.Width - ri_width + 30, original_rect.Y + 6);
                                visible_download = true;
                            }
                            else { visible_download = false; }
                        }
                        else { visible_download = false; }
                    }
                    else
                    {
                        visible_setting = visible_download = false;
                    }
                }
                else
                {
                    visible_setting = visible_volume = false;
                }

                if (player.IsPlaying && !player.isDLNA && readWidth > ri_width + original_rect.Height)
                {
                    ri_width += original_rect.Height;
                    btn_photo_rect.Location = new Point(original_rect.Width - ri_width + (btn_photo_rect.Width / 2), original_rect.Y + (btn_photo_rect.Width / 2));

                    visible_photo = true;
                    if (readWidth > ri_width + original_rect.Height)
                    {
                        ri_width += original_rect.Height;
                        btn_record_rect.Location = new Point(original_rect.Width - ri_width + (btn_record_rect.Width / 2), original_rect.Y + (btn_record_rect.Width / 2));
                        btn_record_rects1 = MinRect(btn_record_rect, 6);
                        btn_record_rects2 = MinRect(btn_record_rect, 10);
                        btn_record_rects3 = MinRect(btn_record_rect, 14);

                        visible_record = true;
                    }
                    else
                    {
                        visible_record = false;
                    }
                }
                else
                {
                    visible_photo = visible_record = false;
                }

                if (readWidth > ri_width + original_rect.Height)
                {
                    ri_width += original_rect.Height;
                    btn_tv_rect.Location = new Point(original_rect.Width - ri_width + (btn_tv_rect.Width / 2), original_rect.Y + (btn_tv_rect.Width / 2));

                    visible_tv = true;
                }
                else
                {
                    visible_tv = false;
                }


                prog_rects.Width = prog_rect.Width = rect.Width;
                prog_time_rect = new Rectangle(_X + 10, original_rect.Y, rect.Width - _X - ri_width - 10, original_rect.Height);
                Print();
            }
        }
        Rectangle MinRect(Rectangle rect, int size)
        {
            int xy = size / 2;
            return new Rectangle(rect.X + xy, rect.Y + xy, rect.Width - size, rect.Height - size);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            SetSize();
            base.OnSizeChanged(e);
        }

        private void Player_LocationSizeChanged(object sender, EventArgs e)
        {
            if (player.isMax)
            {
                Rectangle maxrect = Screen.PrimaryScreen.WorkingArea;
                //Rectangle maxrect = new Rectangle(ML(), MS());
                this.Width = maxrect.Width;
                this.Location = new Point(maxrect.X, maxrect.Y + maxrect.Height - this.Height);
            }
            else
            {
                this.Width = player.Width;
                this.Location = new Point(player.Location.X, (player.Location.Y + player.Height) - this.Height);
            }

        }
        //private Size MS()
        //{
        //    Size ss = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        //    return ss;
        //}
        //private Point ML()
        //{
        //    Point pp = new Point(Screen.PrimaryScreen.WorkingArea.X, Screen.PrimaryScreen.WorkingArea.Y);
        //    return pp;
        //}

        #endregion

        #region 渲染

        #region 样式

        SolidBrush _ValueColor = new SolidBrush(Color.FromArgb(250, Color.DodgerBlue));
        Font TimeFont = new Font("微软雅黑", 12);
        Font TimeFont_Mini = new Font("微软雅黑", 8);

        StringFormat stringFormat = new StringFormat { LineAlignment = StringAlignment.Center };
        StringFormat stringFormatC = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
        SolidBrush solidBrush_White = new SolidBrush(Color.White);
        SolidBrush solidBrush_WhiteSmoke = new SolidBrush(Color.FromArgb(40, 255, 255, 255));
        SolidBrush solidBrush_WhiteSmokes = new SolidBrush(Color.FromArgb(180, 255, 255, 255));
        SolidBrush solidBrush_Red = new SolidBrush(Color.FromArgb(247, 76, 49));
        Pen pen;
        Pen pentwo;
        Pen pen2 = new Pen(Color.FromArgb(247, 76, 49), 2);
        //StringFormat stringFormatCenter = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };

        #endregion

        public void Print()
        {
            try
            {
                Rectangle original_rect = ClientRectangle;
                if (original_rect.Width > 0 && original_rect.Height > 0)
                {
                    #region 渲染全部
                    using (Bitmap original_bmp = PrintBit(original_rect))
                    {
                        Win32.SetBits(original_bmp, new Rectangle(this.Left, this.Top, original_rect.Width, original_rect.Height), this.Handle);
                    }
                    #endregion
                    GC.Collect();
                }
            }
            catch
            {
                this.Close();
            }
        }
        Bitmap PrintBit(Rectangle original_rect)
        {
            Bitmap original_bmp = new Bitmap(original_rect.Width, original_rect.Height);
            using (Graphics g = Graphics.FromImage(original_bmp))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

                using (LinearGradientBrush myBrush = new LinearGradientBrush(original_rect, Color.Transparent, Color.FromArgb(100, Color.Black), LinearGradientMode.Vertical))
                {
                    g.FillRectangle(myBrush, original_rect);
                }
                try
                {
                    #region 绘制进度条

                    if (_maxvalue > 0)
                    {
                        //double p = _value;
                        g.FillRectangle(solidBrush_WhiteSmoke, prog_rect);


                        if (_buffvalue != null && _buffvalue.Count > 0)
                        {
                            if (_buffvalue.Count == 1 && _buffvalue[0].Index == -10)
                            { g.FillRectangle(solidBrush_WhiteSmokes, prog_rect); }
                            else
                            {
                                float _with = (float)prog_rect.Width / Leng;
                                foreach (_Buffe item in _buffvalue)
                                {
                                    RectangleF rectangleF = new RectangleF(prog_rect.X + (item.Index * _with), prog_rect.Y, item.Leng * _with, prog_rect.Height);
                                    g.FillRectangle(solidBrush_WhiteSmokes, rectangleF);
                                }
                            }
                        }


                        string time_txt;
                        bool _isRenderEllipse = false;
                        float uk;
                        if (IsMouseDown)
                        {
                            _isRenderEllipse = true;
                            //10
                            double jing = _value / 100.0;
                            uk = (float)(original_rect.Width * jing);
                            time_txt = TConvert.ToTimeStr(jing * _maxvalue, _maxvalue);
                        }
                        else
                        {
                            //6
                            uk = (float)(original_rect.Width * (_value / _maxvalue));
                            if (IsMouseHove)
                            {
                                _isRenderEllipse = true;
                            }
                            time_txt = TConvert.ToTimeStr(_value, _maxvalue);
                        }

                        g.FillRectangle(_ValueColor, new RectangleF(prog_rect.X, prog_rect.Y, uk, prog_rect.Height));

                        if (_isRenderEllipse)
                        {
                            RectangleF y_rect = new RectangleF(prog_rect.X + uk - prog_rect.Height, prog_rect.Y - (prog_rect.Height / 2), prog_rect.Height * 2, prog_rect.Height * 2);
                            RectangleF y_rects = new RectangleF(y_rect.X - 2, y_rect.Y - 2, y_rect.Width + 4, y_rect.Height + 4);


                            using (GraphicsPath path = new GraphicsPath())
                            {
                                path.AddEllipse(y_rects);
                                //渐变填充GraphicsPath对象
                                using (PathGradientBrush brush = new PathGradientBrush(path))
                                {
                                    brush.CenterColor = Color.Black;
                                    //渐变中心点位置
                                    //brush.CenterPoint = new PointF(100f, 100f);
                                    brush.SurroundColors = new Color[] { Color.Transparent };
                                    g.FillPath(brush, path);
                                }
                            }

                            g.FillEllipse(solidBrush_White, y_rect);
                        }
                        if (prog_time_rect.Width > 40)
                        {
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            g.DrawString(time_txt, prog_time_rect.Width > 140 ? TimeFont : TimeFont_Mini, solidBrush_White, prog_time_rect, stringFormat);
                        }

                        //if (jing >= 0)
                        //{
                        //    if (_buffvalue != null)
                        //    {
                        //        if (_buffvalue.Count == 1 && _buffvalue[0].Index == -10)
                        //        { g.FillRectangle(solidBrush_White, new RectangleF(prog_rect.X, prog_rect.Height - 1, prog_rect.Width, 1)); }
                        //        else
                        //        {
                        //            float _with = (float)prog_rect.Width / Leng;
                        //            foreach (_Buffe item in _buffvalue)
                        //            {
                        //                RectangleF rectangleF = new RectangleF(prog_rect.X + (item.Index * _with), prog_rect.Height - 3, item.Leng * _with, 3);
                        //                g.FillRectangle(solidBrush_White, rectangleF);
                        //            }
                        //        }
                        //    }
                        //    float uk = (float)(prog_rect.Width * (jing / 100.0));
                        //    g.FillRectangle(_BlockColor, new RectangleF(prog_rect.X, prog_rect.Y, uk, prog_rect.Height));
                        //    g.FillRectangle(new SolidBrush(Color.FromArgb(10, 255, 255, 255)), prog_rect);
                        //}
                    }
                    //else if (player.IsPlaying && prog_time_rect.Width > 20)
                    //{
                    //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    //    g.DrawString("直播", TimeFont_Mini, solidBrush_White, prog_time_rect, stringFormat);
                    //}
                    #endregion

                    //g.DrawArc(pen, lonsrect, 0, 360f);
                    if (rippleAnimationManager.IsAnimating())
                    {
                        for (int i = 0; i < rippleAnimationManager.GetAnimationCount(); i++)
                        {
                            double animationValue = rippleAnimationManager.GetProgress(i);
                            if (animationValue > 0)
                            {
                                float wi = ((float)animationValue * (btn_play_rect_render.Width)) + btn_play_rect_default_render.Height;
                                float gogo = (original_rect.Height - wi) / 2;
                                RectangleF recc = new RectangleF(gogo - 6, gogo + 6, wi, wi);
                                g.DrawEllipse(pentwo, recc);
                            }
                        }
                    }
                    else if (!_IsPlay)
                    {
                        g.DrawEllipse(pen, btn_play_rect_default_render);
                    }


                    if (visible_setting)
                    {
                        g.DrawImage(Properties.Resources.ios_settings, btn_setting_rect);
                    }
                    if (visible_download)
                    {
                        g.DrawImage(Properties.Resources.ios_download, btn_download_rect);
                        g.FillEllipse(solidBrush_Red, btn_download_rects);
                        g.DrawString(player.DownTotalCount.ToString(), TimeFont_Mini, solidBrush_White, btn_download_rects, stringFormatC);


                    }
                    if (visible_volume)
                    {
                        g.DrawImage(GetV(), btn_volume_rect);
                    }
                    if (visible_photo)
                    {
                        g.DrawImage(Properties.Resources.ios_photo_b, btn_photo_rect);

                    }
                    if (visible_record)
                    {
                        g.DrawEllipse(pen2, btn_record_rects1);
                        g.FillEllipse(solidBrush_Red, player.isRecord ? btn_record_rects2 : btn_record_rects3);
                    }
                    if (visible_tv)
                    {
                        g.DrawImage(Properties.Resources.ios_tv, btn_tv_rect);
                    }

                    g.DrawImage(player.IsFullScreen ? Properties.Resources.btn_fullscreen_ok : Properties.Resources.btn_fullscreen, btn_fullscreen_rects);


                    g.DrawImage(Properties.Resources.btn_stop, btn_stop_rect);
                    if (_IsNext)
                    {
                        g.DrawImage(Properties.Resources.btn_next, btn_next_rect);
                    }
                    g.DrawImage(_IsPlay ? Properties.Resources.btn_pause : Properties.Resources.btn_play, btn_play_rect);
                }
                catch { }
            }

            return original_bmp;
        }
        public Image GetV()
        {
            if (player.player.GetConfig(12) == "1")
            {
                return Properties.Resources.ios_volume_mutes;
            }
            else
            {
                int a = player.player.GetVolume();
                if (a > 60)
                {
                    return Properties.Resources.ios_volume_high;
                }
                else if (a > 30)
                { return Properties.Resources.ios_volume_m; }
                else if (a > 0)
                { return Properties.Resources.ios_volume_low; }
                else { return Properties.Resources.ios_volume_mute; }
            }
        }

        #region 属性

        #region 进度
        public double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    if (!IsMouseDown)
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
                        Print();
                    }
                }
            }
        }

        public double _maxvalue;
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
                        Print();
                    }
                }
            }
        }
        public bool IsMouseHove = false;
        public bool IsMouseDown = false;
        List<_Buffe> _buffvalue = null;
        public string _buff = null;
        public int Leng = 0;
        /// <summary>
        /// 当前缓存进度
        /// </summary>
        public void SetBufferValue(List<_Buffe> value)
        {
            _buffvalue = value;
            if (!IsMouseDown)
            {
                Print();
            }
        }
        #endregion

        bool _IsPlay = false;
        public bool IsPlay
        {
            get { return _IsPlay; }
            set
            {
                if (_IsPlay != value)
                {
                    _IsPlay = value;
                    if (LoadState)
                    {
                        if (SystemSettings.Animation)
                        {
                            rippleAnimationManager.Clear();
                            rippleAnimationManager.StartNewAnimation(_IsPlay ? AnimationDirection.In : AnimationDirection.Out, animationSource);
                        }
                        else { Print(); }
                    }
                    //else
                    //{
                    //    Print();
                    //    //Print(btn_play_rect_render);
                    //}
                }
            }
        }

        public bool _IsNext = false;
        public bool IsNext
        {
            get { return _IsNext; }
            set
            {
                if (_IsNext != value)
                {
                    _IsNext = value;
                    SetSize();
                }
            }
        }

        #endregion

        #region 进度条事件
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseDown && e.Button == MouseButtons.Left)
            {
                double jv = (Convert.ToDouble(e.X - prog_rect.X) / Convert.ToDouble(prog_rect.Width)) * 100.0;
                if (jv > 0 && jv < 100)
                {
                    _value = jv;
                    Print();
                }
            }
            else
            {
                if (prog_rects.Contains(e.Location))
                {
                    if (_maxvalue > 0 && !IsMouseHove)
                    {
                        IsMouseHove = true;
                        if (SystemSettings.Animation)
                        {
                            controlAnimations.Move(this, 4, 10, 300, AnimationType.Ball);
                        }
                        else
                        {
                            prog_rect.Height = 10;
                            Print();
                        }
                    }
                }
                else if (IsMouseHove)
                {
                    IsMouseHove = false;
                    if (SystemSettings.Animation)
                    {
                        controlAnimations.Move(this, 10, 4, 300, AnimationType.Ball);
                    }
                    else
                    {
                        prog_rect.Height = 4;
                        Print();
                    }
                }

                int count = 0;
                if (btn_play_rect_default_render.Contains(e.Location))
                {
                    count++;
                }
                else if (btn_stop_rect.Contains(e.Location))
                {
                    count++;
                }
                else if (_IsNext && btn_next_rect.Contains(e.Location))
                {
                    count++;
                }
                else if (btn_fullscreen_rect.Contains(e.Location))
                {
                    count++;
                }
                else if (visible_setting && btn_setting_rect.Contains(e.Location))
                {
                    count++;
                }
                else if (visible_photo && btn_photo_rect.Contains(e.Location))
                {
                    count++;
                }
                else if (visible_record && btn_record_rect.Contains(e.Location))
                {
                    count++;
                }

                else if (visible_tv && btn_tv_rect.Contains(e.Location))
                {
                    count++;
                }
                else if (visible_download && btn_download_rect.Contains(e.Location))
                {
                    count++;
                }


                if (visible_volume && btn_volume_rect.Contains(e.Location))
                {
                    count++;
                    if (player._volume == null)
                    {
                        player._volume = new Volume(player);
                        player._volume.Show(this);
                    }
                }
                else
                {
                    if (player._volume != null)
                    {
                        if (!btn_volume_rects.Contains(e.Location))
                        {
                            player._volume.Close();
                        }
                    }
                }
                this.Cursor = count > 0 ? Cursors.Hand : Cursors.Default;
                base.OnMouseMove(e);
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.Cursor = Cursors.Default;
            if (player.isMini && !player.isMoveIn)
            {
                if (player._volume != null)
                {
                    player._volume.Close();
                }
                if (!player.isInPlayRect(player.PointToClient(MousePosition)))
                {
                    player.Controller(false);
                }
                else
                {
                    if (IsMouseDown || IsMouseHove)
                    {
                        IsMouseDown = false;
                        IsMouseHove = false;
                        if (SystemSettings.Animation)
                        {
                            controlAnimations.Move(this, 10, 4, 300, AnimationType.Ball);
                        }
                        else
                        {
                            prog_rect.Height = 4;
                            Print();
                        }
                    }
                }
            }
            else if (IsMouseDown || IsMouseHove)
            {
                IsMouseDown = false;
                IsMouseHove = false;
                if (SystemSettings.Animation)
                {
                    controlAnimations.Move(this, 10, 4, 300, AnimationType.Ball);
                }
                else
                {
                    prog_rect.Height = 4;
                    Print();
                }
            }
            else
            {
                if (player._volume != null)
                {
                    player._volume.Close();
                }
                base.OnMouseLeave(e);
            }
        }
        public delegate void ValueEventHandler(double value);
        public event ValueEventHandler ValueChange;


        public delegate void ClickEventHandler(int value);
        public event ClickEventHandler MClick;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _maxvalue > 0 && prog_rects.Contains(e.Location))
            {
                IsMouseDown = true;
                double jv = (Convert.ToDouble(e.X - prog_rect.X) / Convert.ToDouble(prog_rect.Width)) * 100.0;
                if (jv > 0 && jv < 100)
                {
                    _value = jv;
                    Print();
                }
            }
            else if (btn_play_rect_default_render.Contains(e.Location))
            {
            }
            else if (btn_stop_rect.Contains(e.Location))
            {
            }
            else if (_IsNext && btn_next_rect.Contains(e.Location))
            {
            }
            else if (btn_fullscreen_rect.Contains(e.Location))
            {
            }
            else if (visible_volume && btn_volume_rect.Contains(e.Location))
            {
            }
            else if (visible_setting && btn_setting_rect.Contains(e.Location))
            {
            }
            else if (visible_photo && btn_photo_rect.Contains(e.Location))
            {
            }
            else if (visible_record && btn_record_rect.Contains(e.Location))
            {
            }
            else if (visible_tv && btn_tv_rect.Contains(e.Location))
            {
            }
            else if (visible_download && btn_download_rect.Contains(e.Location))
            {
            }
            else
            {
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (IsMouseDown)
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
                this.Focus();
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (btn_play_rect_default_render.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(0); }
                }
                else if (btn_stop_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(1); }
                }
                else if (_IsNext && btn_next_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(2); }
                }
                else if (btn_fullscreen_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(5); }
                }
                else if (visible_volume && btn_volume_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(3); }
                }
                else if (visible_setting && btn_setting_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(4); }
                }
                else if (visible_photo && btn_photo_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(6); }
                }
                else if (visible_record && btn_record_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(8); }
                }
                else if (visible_tv && btn_tv_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(7); }
                }
                else if (visible_download && btn_download_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(10); }
                }
                else
                {
                    base.OnMouseUp(e);
                }

            }
            else
            {
                base.OnMouseUp(e);
            }
        }

        #endregion

        #endregion

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                int p = e.Delta;
                int a = player.player.GetVolume();
                if (p > 0)
                {
                    if (a == 99) { a = 100; } else { a += 2; }
                }
                else
                {
                    if (a == 1) { a = 0; } else { a -= 2; }
                }
                player.ShowCov(a);
            }
            catch { }

            base.OnMouseWheel(e);
        }


        #region 透明窗口渲染
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;
                CreateParams cParms = base.CreateParams;
                cParms.Style = cParms.Style | WS_MINIMIZEBOX;   // 允许最小化操作
                if (!DesignMode)//设计模式下不执行代码 && isLayeredWindowForm
                {
                    cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED层样式
                }
                return cParms;
            }
        }
        #endregion
    }

}
