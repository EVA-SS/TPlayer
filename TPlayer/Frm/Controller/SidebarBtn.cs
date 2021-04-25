using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TSkin;

namespace TPlayer.Com
{
    public partial class SidebarBtn : Form
    {
        TPlayer player = null;

        #region 初始化


        public SidebarBtn(TPlayer player)
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
        private void Player_LocationSizeChanged(object sender, EventArgs e)
        {

            if (player.isMax || player.IsFullScreen)
            {
                Rectangle WorkingAreaRect = Screen.PrimaryScreen.WorkingArea;
                if (player._sidebar != null)
                {
                    this.Location = new Point(WorkingAreaRect.Left + player.ClientRectangle.Width - this.Width - player._sidebar.Width, WorkingAreaRect.Top + ((player.ClientRectangle.Height - this.Height) / 2));
                }
                else
                {
                    this.Location = new Point(WorkingAreaRect.Left + player.ClientRectangle.Width - this.Width, WorkingAreaRect.Top + ((player.ClientRectangle.Height - this.Height) / 2));
                }
            }
            else
            {
                this.Location = new Point(player.Left + player.Width - this.Width, player.Top + ((player.Height - this.Height) / 2));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Player_LocationSizeChanged(this, e);
            player.LocationChanged += Player_LocationSizeChanged;
            player.SizeChanged += Player_LocationSizeChanged;
            base.OnLoad(e);
            Print();
            //SetSize();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            player.LocationChanged -= Player_LocationSizeChanged;
            player.SizeChanged -= Player_LocationSizeChanged;
            player._sidebarBtn = null;
            base.OnClosing(e);
        }
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
        SolidBrush solidBrush = new SolidBrush(Color.FromArgb(200, 54, 54, 63));
        SolidBrush solidBrushs = new SolidBrush(Color.FromArgb(200, 255, 255, 255));
        static int size = 8;
        static int sizes = size / 2;
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
                using (GraphicsPath path = CreateRoundedRectanglePath(this.ClientRectangle, 6))
                {
                    g.FillPath(solidBrush, path);
                }
                //28, 88
                int x1 = this.Width / 2;
                int y1 = this.Height / 2;

                Point[] pntArr;
                if (_IsLeft)
                {
                    pntArr = new Point[] { new Point(x1 + sizes, y1), new Point(x1 - sizes, y1 - sizes), new Point(x1 - sizes, y1 + sizes) };
                }
                else
                {
                    pntArr = new Point[] { new Point(x1 - sizes, y1), new Point(x1 + sizes, y1 - sizes), new Point(x1 + sizes, y1 + sizes) };
                }
                g.FillPolygon(solidBrushs, pntArr);
            }

            return original_bmp;
        }
        #region 左圆角背景
        internal GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right, rect.Y);
            //roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height);
            //roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);

            #region 左下

            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);

            #endregion
            roundedRect.CloseFigure();
            return roundedRect;
        }

        #endregion

        #region 属性

        bool _IsLeft = false;
        public bool IsLeft
        {
            get { return _IsLeft; }
            set
            {
                if (_IsLeft != value)
                {
                    _IsLeft = value;
                    Print();
                }
            }
        }

        #endregion
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

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (player.isMini)
            {
                if (player._sidebarBtn != null)
                {
                    player._sidebarBtn.Close();
                }
            }
        }

    }
}
