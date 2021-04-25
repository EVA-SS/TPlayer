using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TPlayerSupport;
using TSkin;

namespace TPlayer.Com
{
    public partial class Header : Form
    {
        TPlayer player = null;
        //[DllImport("user32")]
        //public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);

        #region 初始化

        bool isLoad_ = false;
        public Header(TPlayer player)
        {
            this.player = player;
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

        void CmClose()
        {
            player.LocationChanged -= Player_LocationSizeChanged;
            player.SizeChanged -= Player_LocationSizeChanged;
            player.Focus();
            lock (player._header)
            {
                player._header = null;
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
                    controlAnimation.Move(this, this.Top, -this.Height, 300, AnimationType.Ball);
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
        }

        int isCanClose = -1;
        public void NoClose()
        {
            isCanClose = -1;
            controlAnimation.Close();

            if (player.IsFullScreen && SystemSettings.Animation)
            {
                controlAnimation.Move(this, this.Top, player.Top, 100, AnimationType.Ball);
            }
            else
            {
                this.Top = player.Top;
            }
        }
        ControlAnimations controlAnimation = new ControlAnimations();
        protected override void OnLoad(EventArgs e)
        {
            int size1_Height = TextRenderer.MeasureText("Qq", font).Height;
            name_rect.Height = size1_Height;
            name_rect.Y = (40 - size1_Height) / 2;

            //name_rect
            Player_LocationSizeChanged(this, e);
            player.LocationChanged += Player_LocationSizeChanged;
            player.SizeChanged += Player_LocationSizeChanged;

            #region 绑定动画

            controlAnimation.OKClick += (value) =>
            {
                this.Top = (int)value;
                if (isCanClose == 0)
                {
                    isCanClose = 1;
                    this.Close();
                }
            };
            controlAnimation.JClick += (value) =>
            {
                this.Top = (int)value;
            };

            #endregion

            isLoad_ = true;
            base.OnLoad(e);
            SetSize();
            //if (!player.LoadState)
            //{
            //    AnimateWindow(this.Handle, 800, 0x40000 | 0x0004 | 0x20000);
            //}

            #region 动画

            if (player.IsFullScreen && SystemSettings.Animation)
            {
                int _top = this.Top;
                int _tops = this.Top -= this.Height;
                this.Top += _tops;
                controlAnimation.Move(this, _tops, _top, 100, AnimationType.Ball);
            }

            #endregion
        }
        private void Player_LocationSizeChanged(object sender, EventArgs e)
        {
            if (player.isMax || player.IsFullScreen)
            {
                Rectangle WorkingAreaRect = Screen.PrimaryScreen.WorkingArea;
                this.Width = player.ClientRectangle.Width;
                this.Location = WorkingAreaRect.Location;
            }
            else
            {
                this.Width = player.Width;
                this.Location = player.Location;
            }
        }

        #endregion

        #region 属性

        private string _explain = null;
        [Description("说明"), Category("TSkin样式"), DefaultValue(null)]
        public string Explain
        {
            get { return _explain; }
            set
            {
                if (_explain != value)
                {
                    _explain = value;
                    //SetSize();
                    this.Invoke(new Action(() =>
                    {
                        Print();
                    }));
                }
            }
        }

        #endregion

        #region 移动坐标
        Rectangle name_rect = new Rectangle(40, 0, 0, 0);
        Rectangle explain_rect = new Rectangle(60, 30, 0, 20);
        Rectangle rec_rect = new Rectangle(10, 30, 150, 20);
        Rectangle am_logo_rect = new Rectangle(0, 0, 80, 40);
        Rectangle btn_logo_rect = new Rectangle(10, 10, 20, 20);
        Rectangle btn_close_rect = new Rectangle(0, 0, 40, 40);
        Rectangle btn_max_rect = new Rectangle(0, 0, 40, 40);
        Rectangle btn_min_rect = new Rectangle(0, 0, 40, 40);
        Rectangle btn_top_rect = new Rectangle(0, 0, 40, 40);
        Rectangle btn_mini_rect = new Rectangle(0, 0, 40, 40);
        Rectangle btn_top_rects = new Rectangle(0, 0, 20, 20);
        Rectangle btn_mini_rects = new Rectangle(0, 0, 20, 20);
        void SetSize()
        {
            if (isLoad_)
            {
                Rectangle rect = ClientRectangle;
                btn_logo_rect.Location = new Point((am_logo_rect.Height - btn_mini_rects.Width) / 2, (am_logo_rect.Height - btn_logo_rect.Height) / 2);
                int wix = 40;
                btn_close_rect.X = rect.Width - wix;
                wix += 40;
                btn_max_rect.X = rect.Width - wix;
                wix += 40;
                btn_min_rect.X = rect.Width - wix;
                if (!player.IsFullScreen)
                {
                    wix += 40;
                    btn_top_rect.X = rect.Width - wix;
                    btn_top_rects.Location = new Point(btn_top_rect.X + (btn_top_rect.Width - btn_top_rects.Width) / 2, (btn_top_rect.Height - btn_top_rects.Height) / 2);
                    if (!player.isDLNA)
                    {
                        wix += 40;
                        btn_mini_rect.X = rect.Width - wix;
                        btn_mini_rects.Location = new Point(btn_mini_rect.X + (btn_mini_rect.Width - btn_mini_rects.Width) / 2, (btn_mini_rect.Height - btn_mini_rects.Height) / 2);
                    }
                }
                name_rect.Width = rect.Width - 30 - wix;
                explain_rect.Width = rect.Width - 40;
                Print();
            }
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            SetSize();
            base.OnSizeChanged(e);
        }

        #endregion

        #region 样式
        Font font = new Font("微软雅黑", 12);
        Font fontws = new Font("微软雅黑", 10);
        Font fontw = new Font("微软雅黑", 8);
        StringFormat stringFormat = new StringFormat { LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };
        StringFormat stringFormat2 = new StringFormat { LineAlignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };
        StringFormat stringFormats = new StringFormat { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
        SolidBrush solidBrush_White = new SolidBrush(Color.White);
        //SolidBrush solidBrush_White_Ok = new SolidBrush(Color.White);
        SolidBrush solidBrush_WhiteSmoke = new SolidBrush(Color.FromArgb(190, 255, 255, 255));
        SolidBrush solidBrush_Hove = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        SolidBrush solidBrush_Hove2 = new SolidBrush(Color.FromArgb(235, 17, 35));
        SolidBrush solidBrush_Red = new SolidBrush(Color.FromArgb(247, 76, 49));
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
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                using (LinearGradientBrush myBrush = new LinearGradientBrush(original_rect, Color.FromArgb(100, Color.Black), Color.Transparent, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(myBrush, original_rect);
                }

                if (isHove_close)
                {
                    g.FillRectangle(solidBrush_Hove2, btn_close_rect);
                }
                g.DrawImage(Properties.Resources.icon_play, btn_logo_rect);
                g.DrawImage(Properties.Resources.btn_close, btn_close_rect);
                g.DrawImage(player.isMax || player.IsFullScreen ? (isHove_max ? TSkin.Api.GetImgHDispose(Properties.Resources.btn_max, 0.3f) : Properties.Resources.btn_max) : (isHove_max ? TSkin.Api.GetImgHDispose(Properties.Resources.btn_restore, 0.3f) : Properties.Resources.btn_restore), btn_max_rect);
                g.DrawImage(isHove_min ? TSkin.Api.GetImgHDispose(Properties.Resources.btn_min, 0.3f) : Properties.Resources.btn_min, btn_min_rect);
                if (!player.IsFullScreen)
                {
                    g.DrawImage(player.isTop ? (isHove_top ? TSkin.Api.GetImgHDispose(Properties.Resources.btn_top_ok) : Properties.Resources.btn_top_ok) : (isHove_top ? TSkin.Api.GetImgHDispose(Properties.Resources.btn_top) : Properties.Resources.btn_top), btn_top_rects);
                    if (!player.isDLNA)
                    {
                        g.DrawImage(player.isMini ? (isHove_mini ? TSkin.Api.GetImgHDispose(Properties.Resources.btn_mini_ok) : Properties.Resources.btn_mini_ok) : (isHove_mini ? TSkin.Api.GetImgHDispose(Properties.Resources.btn_mini) : Properties.Resources.btn_mini), btn_mini_rects);
                    }
                    //if (isHove_top)
                    //{
                    //    g.FillRectangle(solidBrush_Hove, btn_top_rect);
                    //}
                    //if (isHove_mini)
                    //{
                    //    g.FillRectangle(solidBrush_Hove, btn_mini_rect);
                    //}
                }

                //if (isHove_max)
                //{
                //    g.FillRectangle(solidBrush_Hove, btn_max_rect);
                //}
                //if (isHove_min)
                //{
                //    g.FillRectangle(solidBrush_Hove, btn_min_rect);
                //}
                if (player.IsFullScreen)
                {
                    g.DrawString(DateTime.Now.ToString("HH:mm"), fontws, solidBrush_White, original_rect, stringFormats);
                }


                if (!string.IsNullOrEmpty(_explain))
                {
                    g.DrawString(_explain, fontw, solidBrush_WhiteSmoke, explain_rect, stringFormat);
                }
                if (name_rect.Width > 0 && !string.IsNullOrEmpty(player.Text))
                {
                    g.DrawString(player.Text, font, solidBrush_White, name_rect, stringFormat);
                }

                if (player.isRecord)
                {
                    g.DrawString("● REC  " + TConvert.ToTimeStr(DateTime.Now - player.RecordTime), fontws, solidBrush_Red, rec_rect, stringFormat2);
                }

            }

            return original_bmp;
        }


        #endregion

        public delegate void ClickEventHandler(int value);
        public event ClickEventHandler MClick;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (btn_logo_rect.Contains(e.Location))
                {
                }
                else if (btn_close_rect.Contains(e.Location))
                {
                }
                else if (btn_max_rect.Contains(e.Location))
                {
                }
                else if (btn_min_rect.Contains(e.Location))
                {
                }
                else if (!player.IsFullScreen && btn_top_rect.Contains(e.Location))
                {
                }
                else if (!player.IsFullScreen && btn_mini_rect.Contains(e.Location))
                {
                }
                else
                {
                    base.OnMouseDown(e);
                }
            }
            else
            {
                base.OnMouseDown(e);
            }
        }
        bool isHove_close = false, isHove_max = false, isHove_min = false, isHove_top = false, isHove_mini = false;
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (btn_logo_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(0); }
                }
                else if (btn_close_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(3); }
                }
                else if (btn_max_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(2); }
                }
                else if (btn_min_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(1); }
                }
                else if (!player.IsFullScreen && btn_top_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(4); }
                }
                else if (!player.IsFullScreen && !player.isDLNA && btn_mini_rect.Contains(e.Location))
                {
                    if (MClick != null)
                    { MClick(5); }
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
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int count = 0, counts = 0;
            if (btn_logo_rect.Contains(e.Location))
            {
                counts++;
            }
            //else if (isHove_logo)
            //{
            //    isHove_logo = false;
            //    count++;
            //}

            if (btn_close_rect.Contains(e.Location))
            {
                counts++;
                if (!isHove_close)
                {
                    isHove_close = true;
                    count++;
                }
            }
            else if (isHove_close)
            {
                isHove_close = false;
                count++;
            }

            if (btn_max_rect.Contains(e.Location))
            {
                counts++;
                if (!isHove_max)
                {
                    isHove_max = true;
                    count++;
                }
            }
            else if (isHove_max)
            {
                isHove_max = false;
                count++;
            }

            if (btn_min_rect.Contains(e.Location))
            {
                counts++;
                if (!isHove_min)
                {
                    isHove_min = true;
                    count++;
                }
            }
            else if (isHove_min)
            {
                isHove_min = false;
                count++;
            }

            if (!player.IsFullScreen && btn_top_rect.Contains(e.Location))
            {
                counts++;
                if (!isHove_top)
                {
                    isHove_top = true;
                    count++;
                }
            }
            else if (isHove_top)
            {
                isHove_top = false;
                count++;
            }

            if (!player.IsFullScreen && !player.isDLNA && btn_mini_rect.Contains(e.Location))
            {
                counts++;
                if (!isHove_mini)
                {
                    isHove_mini = true;
                    count++;
                }
            }
            else if (isHove_mini)
            {
                isHove_mini = false;
                count++;
            }
            this.Cursor = counts > 0 ? Cursors.Hand : Cursors.Default;
            if (count > 0)
            {
                Print();
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (player.isMini && !player.isMoveIn)
            {
                if (!player.isInPlayRect(player.PointToClient(MousePosition)))
                {
                    player.Controller(false);
                }
            }
            else
            {
                int count = 0;
                if (isHove_close)
                {
                    isHove_close = false;
                    count++;
                }
                if (isHove_max)
                {
                    isHove_max = false;
                    count++;
                }
                if (isHove_min)
                {
                    isHove_min = false;
                    count++;
                }
                if (isHove_top)
                {
                    isHove_top = false;
                    count++;
                }
                if (isHove_mini)
                {
                    isHove_mini = false;
                    count++;
                }
                if (count > 0)
                {
                    Print();
                }
            }
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
