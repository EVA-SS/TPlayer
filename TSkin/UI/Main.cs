using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TSkin.MainApi;

namespace TSkin
{
    public partial class Main : Form
    {
        #region 变量

        //绘制层
        public MainForm skin = null;
        private UICornerRadiusSides _roundStyle = UICornerRadiusSides.None;
        private Rectangle _deltaRect;
        private int _radius = 1;
        private bool _active = false;
        private Padding _padding;
        private bool _clientSizeSet;
        private int _inWmWindowPosChanged;

        #endregion

        #region 无参构造函数
        public Main() : base()
        {
            base.SetStyle(
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();

            base.BackgroundImageLayout = ImageLayout.None;
            base.Padding = DefaultPadding;
        }
        #endregion

        #region 属性
        private bool canresize = false;
        //private bool canmove = true;

        [Description("可以用鼠标拖动改变窗体大小")]
        [DefaultValue(false)]
        public bool CanResize
        {
            get { return canresize; }
            set { canresize = value; }
        }

        //[Description("可以用鼠标拖动窗体位置")]
        //[DefaultValue(true)]
        //public bool CanMove
        //{
        //    get { return canmove; }
        //    set { canmove = value; }
        //}

        private Padding _borderPadding = new Padding(0);
        /// <summary>
        /// 获取或设置窗体的边框大小。
        /// </summary>
        internal protected Padding BorderPadding
        {
            get { return _borderPadding; }
            set { _borderPadding = value; }
        }

        private bool shadow = true;
        /// <summary>
        /// 是否启用窗体阴影
        /// </summary>
        [Category("Shadow")]
        [DefaultValue(true)]
        [Description("是否启用窗体阴影")]
        public bool Shadow
        {
            get { return shadow; }
            set
            {
                if (shadow != value)
                {
                    shadow = value;
                }
            }
        }

        private Color shadowColor = Color.FromArgb(100, 0, 0, 0);
        /// <summary>
        /// 窗体阴影颜色
        /// </summary>
        [Category("Shadow")]
        [DefaultValue(typeof(Color), "100, 0, 0, 0")]
        [Description("窗体阴影颜色")]
        public Color ShadowColor
        {
            get { return shadowColor; }
            set
            {
                if (shadowColor != value)
                {
                    shadowColor = value;
                    //更新阴影
                    if (skin != null)
                    {
                        skin.Tom();
                    }
                }
            }
        }

        private int shadowWidth = 2;
        /// <summary>
        /// 窗体阴影宽度
        /// </summary>
        [Category("Shadow")]
        [DefaultValue(typeof(int), "2")]
        [Description("窗体阴影宽度")]
        public int ShadowWidth
        {
            get { return shadowWidth; }
            set
            {
                if (shadowWidth != value && value > 1 && value < 151)
                {
                    shadowWidth = value;
                    //更新阴影
                    if (skin != null)
                    {
                        skin.Tom();
                    }
                }
            }
        }

        [DefaultValue(typeof(UICornerRadiusSides), "0")]
        [Category("Skin")]
        [Description("设置或获取窗体的圆角样式")]
        public UICornerRadiusSides RoundStyle
        {
            get { return _roundStyle; }
            set
            {
                if (_roundStyle != value)
                {
                    _roundStyle = value;
                    SetReion();
                    base.Invalidate();
                    if (skin != null)
                    {
                        skin.Tom();
                    }
                }
            }
        }

        [DefaultValue(1)]
        [Category("Skin")]
        [Description("设置或获取窗体的圆角的大小")]
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value < 1 ? 1 : value;
                    SetReion();
                    base.Invalidate();
                    if (skin != null)
                    {
                        skin.Tom();
                    }
                }
            }
        }

        [DefaultValue(typeof(Padding), "0")]
        public new Padding Padding
        {
            get { return _padding; }
            set
            {
                _padding = value;
                base.Padding = new Padding(
                    BorderPadding.Left + _padding.Left,
                    BorderPadding.Left + _padding.Top,
                    BorderPadding.Left + _padding.Right,
                    BorderPadding.Left + _padding.Bottom);
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = RealClientRect;
                rect.X += (_borderPadding.Left + Padding.Left);
                rect.Y += (_borderPadding.Top + Padding.Top);
                rect.Width -= (_borderPadding.Horizontal + Padding.Horizontal);
                rect.Height -= (_borderPadding.Vertical + Padding.Vertical);
                return rect;
            }
        }

        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(
                    BorderPadding.Left,
                    BorderPadding.Left,
                    BorderPadding.Left,
                    BorderPadding.Left);
            }
        }
        /// <summary>
        /// 获取窗体的真实客户区大小。
        /// </summary>
        public Rectangle RealClientRect
        {
            get
            {
                if (base.WindowState == FormWindowState.Maximized)
                {
                    return new Rectangle(
                        _deltaRect.X, _deltaRect.Y,
                        base.Width - _deltaRect.Width, base.Height - _deltaRect.Height);
                }
                else
                {
                    return new Rectangle(Point.Empty, base.Size);
                }
            }
        }
        #endregion

        #region 重载事件

        //控件首次创建时被调用
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!DesignMode && Shadow)
            {
                ShowSkin();
            }
            isEditMove = false;
            isEditSize = false;
        }
        //窗体关闭时

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!e.Cancel)
            {
                //先关闭阴影窗体
                CloseSkin();
            }
        }

        //Show或Hide被调用时
        //private bool OneVisibles = true;
        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible && Shadow && !DesignMode)
            {
                ShowSkin();
                //OneVisibles = false;
            }
            else
            {
                if (skin != null)
                {
                    skin.Visible = false;
                }
            }
            base.OnVisibleChanged(e);
        }

        //窗口加载时
        protected override void OnLoad(EventArgs e)
        {
            ResizeCore();
            SetReion();
            base.OnLoad(e);
        }
        void CloseSkin()
        {
            if (skin != null)
            {
                skin.Close();
            }
        }
        void ShowSkin()
        {
            if (skin != null)
            {
                skin.Visible = true;
            }
            else
            {
                skin = new MainForm(this);
                skin.Show(this);
            }
        }

        protected override void OnMinimumSizeChanged(EventArgs e)
        {
            if (skin != null)
            {
                if (MinimumSize.Width == 0 && MinimumSize.Height == 0)
                {
                    skin.MinimumSize = new Size(0, 0);
                }
                else
                {
                    int bbq = ShadowWidth * 2;
                    skin.MinimumSize = new Size(MinimumSize.Width + bbq, MinimumSize.Height + bbq);
                }
            }
            base.OnMinimumSizeChanged(e);
        }
        protected override void OnMaximumSizeChanged(EventArgs e)
        {
            if (skin != null)
            {
                if (MaximumSize.Width == 0 && MaximumSize.Height == 0)
                {
                    skin.MaximumSize = new Size(0, 0);
                }
                else
                {
                    int bbq = ShadowWidth * 2;
                    skin.MaximumSize = new Size(MaximumSize.Width + bbq, MaximumSize.Height + bbq);
                }
            }
            base.OnMaximumSizeChanged(e);
        }

        public bool isEditMove = true;
        public bool isEditSize = true;
        //窗体移动时
        protected override void OnLocationChanged(EventArgs e)
        {
            if (skin != null && !isEditMove)
            {
                isEditMove = true;
                skin.OnLocationChange();
                isEditMove = false;
            }
            base.OnLocationChanged(e);
            mStopAnthor();
        }
        protected override void OnResize(EventArgs e)
        {
            if (skin != null && !isEditSize)
            {
                isEditSize = true;
                skin.OnSizeChange();
                isEditSize = false;
            }
            ResizeCore();
            base.OnResize(e);
        }

        /// <summary>
        /// 窗体改变大小。
        /// </summary>
        protected virtual void ResizeCore()
        {
            CalcDeltaRect();
            SetReion();
        }


        public AnchorStyles Aanhor = AnchorStyles.None;

        //更新状态
        private void mStopAnthor()
        {
            if (this.Left <= 0)
            {
                Aanhor = AnchorStyles.Left;
            }
            else if (this.Left >= Screen.PrimaryScreen.Bounds.Width - this.Width)
            {
                Aanhor = AnchorStyles.Right;
            }
            else if (this.Top <= 0)
            {
                Aanhor = AnchorStyles.Top;
            }
            else
            {
                Aanhor = AnchorStyles.None;
            }
        }

        //重绘
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    Graphics g = e.Graphics;
        //    g.SmoothingMode = SmoothingMode.HighQuality; //高质量
        //    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //    Rectangle rect = ClientRectangle;

        //    if (showborder)
        //    {
        //        //画边框
        //        renderer.DrawSkinFormBorder(
        //          new SkinFormBorderRenderEventArgs(
        //          this, g, rect, _active));
        //    }
        //}
        //拦截消息
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM.WM_NCHITTEST:
                    WmNcHitTest(ref m);
                    break;
                case WM.WM_NCPAINT:
                    break;
                case WM.WM_NCCALCSIZE:
                    //WmNcCalcSize(ref m);
                    break;
                case WM.WM_WINDOWPOSCHANGED:
                    WmWindowPosChanged(ref m);
                    break;
                case WM.WM_GETMINMAXINFO:
                    WmGetMinMaxInfo(ref m);
                    break;
                case WM.WM_NCACTIVATE:
                    WmNcActive(ref m);
                    break;
                case WM.WM_NCRBUTTONUP:
                    WmNcRButtonUp(ref m);
                    break;
                case WM.WM_NCUAHDRAWCAPTION:
                case WM.WM_NCUAHDRAWFRAME:

                    m.Result = Result.TRUE;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }


        protected override void OnStyleChanged(EventArgs e)
        {
            if (_clientSizeSet)
            {
                ClientSize = ClientSize;
                _clientSizeSet = false;
            }
            base.OnStyleChanged(e);
        }

        protected override void SetClientSizeCore(int x, int y)
        {
            _clientSizeSet = false;
            Type typeControl = typeof(Control);
            Type typeForm = typeof(Form);
            FieldInfo fiWidth = typeControl.GetField("clientWidth",
                BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo fiHeight = typeControl.GetField("clientHeight",
                BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo fi1 = typeForm.GetField("FormStateSetClientSize",
                BindingFlags.NonPublic | BindingFlags.Static),
            fiFormState = typeForm.GetField("formState",
            BindingFlags.NonPublic | BindingFlags.Instance);

            if (fiWidth != null && fiHeight != null &&
                fiFormState != null && fi1 != null)
            {
                _clientSizeSet = true;
                Size = new Size(x, y);
                fiWidth.SetValue(this, x);
                fiHeight.SetValue(this, y);
                BitVector32.Section bi1 = (BitVector32.Section)fi1.GetValue(this);
                BitVector32 state = (BitVector32)fiFormState.GetValue(this);
                state[bi1] = 1;
                fiFormState.SetValue(this, state);
                OnClientSizeChanged(EventArgs.Empty);
                state[bi1] = 0;
                fiFormState.SetValue(this, state);
            }
            else
            {
                base.SetClientSizeCore(x, y);
            }
        }

        protected override Size SizeFromClientSize(Size clientSize)
        {
            return clientSize;
        }

        protected override Rectangle GetScaledBounds(
            Rectangle bounds, SizeF factor, BoundsSpecified specified)
        {
            Rectangle rect = base.GetScaledBounds(bounds, factor, specified);

            Size sz = SizeFromClientSize(Size.Empty);
            if (!GetStyle(ControlStyles.FixedWidth) &&
                ((specified & BoundsSpecified.Width) != BoundsSpecified.None))
            {
                int clientWidth = bounds.Width - sz.Width;
                rect.Width = ((int)Math.Round(
                    (double)(clientWidth * factor.Width))) + sz.Width;
            }
            if (!GetStyle(ControlStyles.FixedHeight) &&
                ((specified & BoundsSpecified.Height) != BoundsSpecified.None))
            {
                int clientHeight = bounds.Height - sz.Height;
                rect.Height = ((int)Math.Round(
                    (double)(clientHeight * factor.Height))) + sz.Height;
            }
            return rect;
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            Size minSize = MinimumSize;
            Size maxSize = MaximumSize;
            Size sz = SizeFromClientSize(Size.Empty);
            base.ScaleControl(factor, specified);
            if (minSize != Size.Empty)
            {
                minSize -= sz;
                minSize = new Size((int)Math.Round(
                    minSize.Width * factor.Width),
                    (int)Math.Round(minSize.Height * factor.Height)) + sz;
            }
            if (maxSize != Size.Empty)
            {
                maxSize -= sz;
                maxSize = new Size((int)Math.Round(
                    maxSize.Width * factor.Width),
                    (int)Math.Round(maxSize.Height * factor.Height)) + sz;
            }
            MinimumSize = minSize;
            MaximumSize = maxSize;
        }

        protected override void SetBoundsCore(
            int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (_inWmWindowPosChanged != 0)
            {
                try
                {
                    Type type = typeof(Form);
                    FieldInfo fi1 = type.GetField("FormStateExWindowBoundsWidthIsClientSize",
                        BindingFlags.NonPublic | BindingFlags.Static),
                        fiFormState = type.GetField("formStateEx",
                        BindingFlags.NonPublic | BindingFlags.Instance),
                        fiBounds = type.GetField("restoredWindowBounds",
                        BindingFlags.NonPublic | BindingFlags.Instance);

                    if (fi1 != null && fiFormState != null && fiBounds != null)
                    {
                        Rectangle restoredWindowBounds = (Rectangle)fiBounds.GetValue(this);
                        BitVector32.Section bi1 = (BitVector32.Section)fi1.GetValue(this);
                        BitVector32 state = (BitVector32)fiFormState.GetValue(this);
                        if (state[bi1] == 1)
                        {
                            width = restoredWindowBounds.Width;
                            height = restoredWindowBounds.Height;
                        }
                    }
                }
                catch
                {
                }
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }


        protected void CalcDeltaRect()
        {
            if (base.WindowState == FormWindowState.Maximized)
            {
                Rectangle bounds = base.Bounds;

                Rectangle realRect = Screen.GetWorkingArea(this);
                realRect.X -= _borderPadding.Left;
                realRect.Y -= _borderPadding.Top;
                realRect.Width += _borderPadding.Horizontal;
                realRect.Height += _borderPadding.Vertical;

                int x = 0;
                int y = 0;
                int width = 0;
                int height = 0;

                if (bounds.Left < realRect.Left)
                {
                    x = realRect.Left - bounds.Left;
                }

                if (bounds.Top < realRect.Top)
                {
                    y = realRect.Top - bounds.Top;
                }

                if (bounds.Width > realRect.Width)
                {
                    width = bounds.Width - realRect.Width;
                }

                if (bounds.Height > realRect.Height)
                {
                    height = bounds.Height - realRect.Height;
                }

                _deltaRect = new Rectangle(x, y, width, height);
            }
            else
            {
                _deltaRect = Rectangle.Empty;
            }
        }
        #endregion

        #region 处理Windows消息的方法
        /// <summary>
        /// 响应 WM_WINDOWPOSCHANGED 消息。
        /// </summary>
        /// <param name="m"></param>
        protected virtual void WmWindowPosChanged(ref Message m)
        {
            _inWmWindowPosChanged++;
            base.WndProc(ref m);
            _inWmWindowPosChanged--;
        }

        /// <summary>
        /// 响应 WM_NCRBUTTONUP 消息。
        /// </summary>
        /// <param name="m"></param>
        protected virtual void WmNcRButtonUp(ref Message m)
        {
            TrackPopupSysMenu(ref m);
            base.WndProc(ref m);
        }

        protected void TrackPopupSysMenu(ref Message m)
        {
            if (m.WParam.ToInt32() == HITTEST.HTCAPTION)
            {
                TrackPopupSysMenu(m.HWnd, new Point(m.LParam.ToInt32()));
            }
        }

        protected void TrackPopupSysMenu(IntPtr hWnd, Point point)
        {
            if (point.Y <= Top + _borderPadding.Top + _deltaRect.Y + 24)
            {
                IntPtr hMenu = NativeMethods.GetSystemMenu(hWnd, false);
                IntPtr command = NativeMethods.TrackPopupMenu(hMenu,
                   TPM.TPM_RETURNCMD | TPM.TPM_TOPALIGN | TPM.TPM_LEFTALIGN,
                   point.X, point.Y, 0, hWnd, IntPtr.Zero);
                NativeMethods.PostMessage(hWnd, WM.WM_SYSCOMMAND, command, IntPtr.Zero);
            }
        }

        ///// <summary>
        ///// 响应 WM_NCCALCSIZE 消息。
        ///// </summary>
        ///// <param name="m"></param>
        //protected virtual void WmNcCalcSize(ref Message m)
        //{
        //    if (base.Opacity != 1.0d)
        //    {
        //        //Invalidate();
        //    }
        //}

        private void WmNcHitTest(ref Message m)
        {
            Point point = new Point(m.LParam.ToInt32());
            point = base.PointToClient(point);

            if (WindowState == FormWindowState.Normal && CanResize)
            {
                if (point.X < 10 && point.Y < 10)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTTOPLEFT);
                    return;
                }

                if (point.X > Width - 10 && point.Y < 10)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTTOPRIGHT);
                    return;
                }

                if (point.X < 10 && point.Y > Height - 10)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTBOTTOMLEFT);
                    return;
                }

                if (point.X > Width - 10 && point.Y > Height - 10)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTBOTTOMRIGHT);
                    return;
                }

                if (point.Y < 5)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTTOP);
                    return;
                }

                if (point.Y > Height - 5)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTBOTTOM);
                    return;
                }

                if (point.X < 5)
                {
                    m.Result = new IntPtr(
                       HITTEST.HTLEFT);
                    return;
                }

                if (point.X > Width - 5)
                {
                    m.Result = new IntPtr(
                       HITTEST.HTRIGHT);
                    return;
                }
            }
            m.Result = new IntPtr(
                     HITTEST.HTCLIENT);
        }

        private void WmGetMinMaxInfo(ref Message m)
        {
            MINMAXINFO minmax =
                (MINMAXINFO)Marshal.PtrToStructure(
                m.LParam, typeof(MINMAXINFO));

            if (MaximumSize != Size.Empty)
            {
                minmax.maxTrackSize = MaximumSize;
            }
            else
            {
                Rectangle rect = Screen.GetWorkingArea(this);
                int h = this.FormBorderStyle == FormBorderStyle.None ? 0 : -1;
                minmax.maxPosition = new Point(
                    rect.X,
                    rect.Y);
                minmax.maxTrackSize = new Size(
                    rect.Width,
                    rect.Height + h);
            }

            if (MinimumSize != Size.Empty)
            {
                minmax.minTrackSize = MinimumSize;
            }
            Marshal.StructureToPtr(minmax, m.LParam, false);
        }

        private void WmNcActive(ref Message m)
        {
            if (m.WParam.ToInt32() == 1)
            {
                _active = true;
            }
            else
            {
                _active = false;
            }
            m.Result = Result.TRUE;
            base.Invalidate();
        }
        #endregion

        #region 私有方法

        //窗体圆角
        private void SetReion()
        {
            if (base.Region != null)
            {
                base.Region.Dispose();
            }
            this.CreateRegion(RealClientRect, _radius, RoundStyle);
        }
        #endregion
    }
}
