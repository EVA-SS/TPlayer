using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TSkin;

namespace TPlayer.Com
{
    public partial class Volume : Form
    {
        TPlayer player = null;
        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);

        #region 初始化

        bool isLoad_ = false;
        public Volume(TPlayer player)
        {
            this.player = player;
            _value = player.player.GetVolume();
            if (_value >= _maxvalue)
            {
                _value = _maxvalue;
            }
            SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.DoubleBuffer, true);
            UpdateStyles();
            InitializeComponent();
        }
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            player.Player_PreviewKeyDown(this, e);
            base.OnPreviewKeyDown(e);
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            player._volume = null;
            player.is_volume = false;
            player.LocationChanged -= Player_LocationSizeChanged;
            player.SizeChanged -= Player_LocationSizeChanged;
            base.OnClosing(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            Player_LocationSizeChanged(this, e);
            player.LocationChanged += Player_LocationSizeChanged;
            player.SizeChanged += Player_LocationSizeChanged;
            isLoad_ = true;
            base.OnLoad(e);
            SetSize();
            AeroGlass.EnableBlur(this.Handle);
        }
        void Player_LocationSizeChanged(object sender, EventArgs e)
        {
            this.Location = new Point(player.Left + player.Width - this.Width - 60 - ((60 - this.Width) / 2), player.Top + (player.Height - this.Height) - 60);
        }

        #endregion

        #region 移动坐标
        Rectangle rect = new Rectangle(0, 0, 0, 0);
        void SetSize()
        {
            if (isLoad_)
            {
                Rectangle rect = ClientRectangle;
                this.rect = new Rectangle((rect.Width - 6) / 2, 20, 6, rect.Height - 40);
                Print();
            }
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            SetSize();
            base.OnSizeChanged(e);
        }

        #endregion

        #region 属性

        int _value = 0;
        public int Value
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
                    else
                    {
                        _value = value;
                    }
                    Print();
                }
            }
        }

        int _maxvalue = 100;
        public int MaxValue
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
        public bool IsMouseDown = false;
        #endregion

        #region 样式
        SolidBrush solidBrush_White = new SolidBrush(Color.White);
        SolidBrush solidBrush = new SolidBrush(Color.FromArgb(160, 0, 0, 0));
        SolidBrush solidBrush_WhiteSmoke = new SolidBrush(Color.FromArgb(40, 255, 255, 255));
        SolidBrush _ValueColor = new SolidBrush(Color.FromArgb(250, Color.DodgerBlue));
        #endregion

        #region 渲染

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
            catch { this.Close(); }
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
                g.FillRectangle(solidBrush, this.ClientRectangle);
                g.FillRectangle(solidBrush_WhiteSmoke, rect);

                #region 绘制进度条

                if (_value > 0 && _maxvalue > 0)
                {
                    float uk;
                    if (IsMouseDown)
                    {
                        double jing = _value / 100.0;
                        uk = (float)(rect.Height * jing);
                    }
                    else
                    {
                        uk = (float)(rect.Height * ((_value * 1.0) / (_maxvalue * 1.0)));
                    }
                    g.FillRectangle(_ValueColor, new RectangleF(rect.X, rect.Y + (rect.Height - uk), rect.Width, uk));


                    RectangleF y_rect = new RectangleF(rect.X - (rect.Width / 2), (rect.Y + (rect.Height - uk)) - rect.Width, rect.Width * 2, rect.Width * 2);
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

                #endregion
            }

            return original_bmp;
        }

        #endregion

        #region 进度条事件
        protected override void OnMouseEnter(EventArgs e)
        {
            player.is_volume = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            player.is_volume = true;
            if (IsMouseDown && e.Button == MouseButtons.Left)
            {
                int un = (int)Math.Round(100 - ((Convert.ToDouble(e.Y - rect.Y) / Convert.ToDouble(rect.Height)) * 100.0));
                if (un >= _maxvalue)
                {
                    Value = _maxvalue;
                    player.ShowCov(100);
                    //下一个
                }
                else if (un < 0)
                {
                    Value = 0;
                    player.ShowCov(0);
                }
                else
                {
                    Value = un;
                    player.ShowCov(un);
                }
                //Print();
            }
            else
            {
                base.OnMouseMove(e);
            }
        }

        //protected override void OnDeactivate(EventArgs e)
        //{
        //    base.OnDeactivate(e);
        //    this.Close();
        //}
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Close();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (IsMouseDown && e.Button == MouseButtons.Left)
            {
                IsMouseDown = false;
                int un = (int)Math.Round(100 - ((Convert.ToDouble(e.Y - rect.Y) / Convert.ToDouble(rect.Height)) * 100.0));
                if (un >= _maxvalue)
                {
                    Value = _maxvalue;
                    player.ShowCov(100);
                    //下一个
                }
                else if (un < 0)
                {
                    Value = 0;
                    player.ShowCov(0);
                }
                else
                {
                    Value = un;
                    player.ShowCov(un);
                }
            }
            else
            {
                base.OnMouseUp(e);
            }
        }
        //public delegate void ValueEventHandler(double value);
        //public event ValueEventHandler ValueChange;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _maxvalue > 0 && rect.Contains(e.Location))
            {
                IsMouseDown = true;
                int un = (int)Math.Round(100 - ((Convert.ToDouble(e.Y - rect.Y) / Convert.ToDouble(rect.Height)) * 100.0));
                if (un >= _maxvalue)
                {
                    Value = _maxvalue;
                    player.ShowCov(100);
                    //下一个
                }
                else if (un < 0)
                {
                    Value = 0;
                    player.ShowCov(0);
                }
                else
                {
                    Value = un;
                    player.ShowCov(un);
                }
            }
            else
            {
                base.OnMouseDown(e);
            }
        }


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
                Value = a;
                player.ShowCov(a);
            }
            catch { }

            base.OnMouseWheel(e);
        }
        #endregion

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
