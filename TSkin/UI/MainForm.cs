using StackBlur.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TSkin.MainApi;

namespace TSkin
{
    //绘图层
    public partial class MainForm : Form
    {
        //控件层
        private Main Main;
        //带参构造
        public MainForm(Main main)
        {
            //将控制层传值过来
            this.Main = main;
            //减少闪烁
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();

            //初始化
            //置顶窗体
            //TopMost = Main.TopMost;
            //Main.BringToFront();
            //是否在任务栏显示
            ShowInTaskbar = false;
            //无边框模式
            FormBorderStyle = FormBorderStyle.None;

            //设置ICO
            Icon = Main.Icon;
            ShowIcon = Main.ShowIcon;

            //设置标题名
            Text = Main.Text;
            //还原任务栏右键菜单
            //CommonClass.SetTaskMenu(Main);
            //窗口鼠标穿透效果
            //CanPenetrate();

            int bbq = main.ShadowWidth * 2;
            if (main.MinimumSize.Width != 0 && main.MinimumSize.Height != 0)
            {
                this.MinimumSize = new Size(main.MinimumSize.Width + bbq, main.MinimumSize.Height + bbq);
            }
            if (main.MaximumSize.Width != 0 && main.MaximumSize.Height != 0)
            {
                this.MaximumSize = new Size(main.MaximumSize.Width + bbq, main.MaximumSize.Height + bbq);
            }
            Tom(false);
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        #region 初始化
        public void OnSizeChange()
        {
            if (!isEditSize)
            {
                int _ShadowWidth = Main.ShadowWidth * 2;
                rect.Size = new Size(Main.Width, Main.Height);

                shadow_rect.Size = new Size(rect.Width + _ShadowWidth, rect.Height + _ShadowWidth);
                if (Main.WindowState == FormWindowState.Maximized)
                {
                    this.Visible = false;
                }
                else
                {
                    this.Visible = true;
                    this.Size = shadow_rect.Size;
                }
            }
        }
        public void OnLocationChange()
        {
            this.Location = new Point(Main.Left - Main.ShadowWidth, Main.Top - Main.ShadowWidth);
        }

        Rectangle shadow_rect = new Rectangle(0, 0, 0, 0), rect;

        bool isEditSize = true;
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Tom();
            isEditSize = false;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!isEditSize)
            {
                isEditSize = true;
                int _ShadowWidth = Main.ShadowWidth * 2;

                Rectangle Mainrectangle = new Rectangle(this.Left + Main.ShadowWidth, this.Top + Main.ShadowWidth, this.Width - _ShadowWidth, this.Height - _ShadowWidth);
                Main.Location = Mainrectangle.Location;
                Main.Size = Mainrectangle.Size;
                rect.Size = Mainrectangle.Size;
                shadow_rect.Size = new Size(rect.Width + _ShadowWidth, rect.Height + _ShadowWidth);

                isEditSize = false;
                SetBits();
                //Main.Focus();
            }
        }
        #endregion

        #region 还原任务栏右键菜单

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

        #region 控件层相关事件
        //主窗体大小改变时
        public void Tom(bool isP=true)
        {
            int _ShadowWidth = Main.ShadowWidth * 2;
            Location = new Point(Main.Left - Main.ShadowWidth, Main.Top - Main.ShadowWidth);
            rect = new Rectangle(Main.ShadowWidth, Main.ShadowWidth, Main.Width, Main.Height);
            shadow_rect.Size = new Size(rect.Width + _ShadowWidth, rect.Height + _ShadowWidth);
            this.Size = shadow_rect.Size;
            if (isP)
            {
                SetBits();
            }
        }

        #endregion

        #region 不规则无毛边方法

        SolidBrush TransparentsolidBrush = new SolidBrush(Color.Transparent);
        public void SetBits()
        {
            if (shadow_rect.Width > 0 && shadow_rect.Height > 0)
            {
                using (Bitmap bitmap = new Bitmap(shadow_rect.Width, shadow_rect.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

                        using (GraphicsPath path = rect.CreateRoundedRectanglePath(Main.Radius, Main.RoundStyle))
                        {
                            g.FillPath(Main.ShadowColor, path);
                            bitmap.StackBlur(Main.ShadowWidth);

                            g.CompositingMode = CompositingMode.SourceCopy;
                            g.FillPath(TransparentsolidBrush, path);
                        }
                    }


                    //绘制绘图层背景
                    //if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                    //    throw new ApplicationException("图片必须是32位带Alhpa通道的图片。");
                    IntPtr oldBits = IntPtr.Zero;
                    IntPtr screenDC = NativeMethods.GetDC(IntPtr.Zero);
                    IntPtr hBitmap = IntPtr.Zero;
                    IntPtr memDc = NativeMethods.CreateCompatibleDC(screenDC);

                    try
                    {
                        NativeMethods.Point topLoc = new NativeMethods.Point(Left, Top);
                        NativeMethods.Size bitMapSize = new NativeMethods.Size(Width, Height);
                        NativeMethods.BLENDFUNCTION blendFunc = new NativeMethods.BLENDFUNCTION();
                        NativeMethods.Point srcLoc = new NativeMethods.Point(0, 0);

                        hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                        oldBits = NativeMethods.SelectObject(memDc, hBitmap);

                        blendFunc.BlendOp = AC.AC_SRC_OVER;
                        blendFunc.SourceConstantAlpha = Byte.Parse("255");
                        blendFunc.AlphaFormat = AC.AC_SRC_ALPHA;
                        blendFunc.BlendFlags = 0;

                        NativeMethods.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, NativeMethods.ULW_ALPHA);
                    }
                    finally
                    {
                        if (hBitmap != IntPtr.Zero)
                        {
                            NativeMethods.SelectObject(memDc, oldBits);
                            NativeMethods.DeleteObject(hBitmap);
                        }
                        NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
                        NativeMethods.DeleteDC(memDc);
                    }
                }
            }
        }
        #endregion

        //拦截消息
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x21:
                    m.Result = new IntPtr(0x03);
                    break;
                case WM.WM_NCHITTEST:
                    WmNcHitTest(ref m);
                    break;
                case WM.WM_NCPAINT:
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        public void WmNcHitTest(ref Message m)
        {
            Point point = new Point(m.LParam.ToInt32());
            point = base.PointToClient(point);

            if (Main.WindowState == FormWindowState.Normal && Main.CanResize)
            {
                if (point.X < Main.ShadowWidth * 2 && point.Y < Main.ShadowWidth * 2)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTTOPLEFT);
                    return;
                }

                if (point.X > Width - Main.ShadowWidth * 2 && point.Y < Main.ShadowWidth * 2)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTTOPRIGHT);
                    return;
                }

                if (point.X < Main.ShadowWidth * 2 && point.Y > Height - Main.ShadowWidth * 2)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTBOTTOMLEFT);
                    return;
                }

                if (point.X > Width - Main.ShadowWidth * 2 && point.Y > Height - Main.ShadowWidth * 2)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTBOTTOMRIGHT);
                    return;
                }

                if (point.Y < Main.ShadowWidth)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTTOP);
                    return;
                }

                if (point.Y > Height - Main.ShadowWidth)
                {
                    m.Result = new IntPtr(
                        HITTEST.HTBOTTOM);
                    return;
                }

                if (point.X < Main.ShadowWidth)
                {
                    m.Result = new IntPtr(
                       HITTEST.HTLEFT);
                    return;
                }

                if (point.X > Width - Main.ShadowWidth)
                {
                    m.Result = new IntPtr(
                       HITTEST.HTRIGHT);
                    return;
                }
            }
            m.Result = new IntPtr(
                     HITTEST.HTCLIENT);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Main.skin = null;
            base.OnClosing(e);
        }
    }
}
