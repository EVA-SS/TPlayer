using Animations;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace TSkin
{
    public partial class RotateImage : Control
    {
        private readonly AnimationManager hoverAnimationManager;
        public int Retrysp = 1;
        public RotateImage()
        {
            //InitializeComponent();
            //实例化创建Image图像
            this.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.Selectable |
               ControlStyles.DoubleBuffer |
               ControlStyles.SupportsTransparentBackColor |
               ControlStyles.ContainerControl |
               ControlStyles.UserPaint, true);
            //base.BackColor = Color.Transparent;
            this.UpdateStyles();
            hoverAnimationManager = new AnimationManager
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };
            hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
        }
        Rectangle rect;
        protected override void OnSizeChanged(EventArgs e)
        {
            if (_Image != null)
            {
                rect = new Rectangle(Width / 2 - _Image.Width / 2, Height / 2 - _Image.Height / 2, _Image.Width, _Image.Height);
            }
            base.OnSizeChanged(e);
        }
        #region 属性

        private Color BackColors;
        [Description("设置鼠标移动上去的背景颜色"),//显示在属性设计视图中的描述
        Category("TSkin样式"),
        DefaultValue(typeof(Color), "WhiteSmoke")]//给予初始值
        public Color HoverColor
        {
            get { return BackColors; }
            set
            {
                BackColors = value;
            }
        }

        private Image _Image = null;
        /// <summary>
        /// 按钮上显示的图片
        /// </summary>
        [Description("按钮上显示的图片")]
        public Image Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                base.Invalidate();
            }
        }
        public bool State = false;
        int txtRotate = 362;
        Thread Tim = null;
        #endregion
        public void Start()
        {
            if (!State)
            {
                if (_Image != null)
                {
                    State = true;
                    Tim = new Thread(Run);
                    Tim.IsBackground = true; Tim.Start();
                }
            }
        }
        public void Stop()
        {
            if (State)
            {
                try
                {
                    State = false;
                    Tim.Abort(); Tim = null;
                }
                catch { }
            }
        }
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(10);
                if (txtRotate <= 0) { txtRotate = 365; }
                txtRotate -= 5;
                this.Invalidate();
            }
        }
        protected override void Dispose(bool disposing)
        {
            Stop();
            base.Dispose(disposing);
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            MouseEnter += (sender, args) =>
            {
                hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                if (_Image != null)
                {
                    if (txtRotate == 360 || txtRotate == 0)
                    {
                        g.DrawImage(_Image, rect);
                    }
                    else
                    {
                        Image p = Rotate(_Image, txtRotate);
                        g.DrawImage(p, rect.Location);
                    }
                }
                using (Brush b = new SolidBrush(Color.FromArgb((int)(hoverAnimationManager.GetProgress() * BackColors.A), BackColors)))
                    g.FillRectangle(b, ClientRectangle);
            }
            catch { }
            base.OnPaint(e);
        }

        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        public Image Rotate(Image ing, int angle)
        {
            try
            {
                angle = angle % 360;
                //弧度转换
                double radian = angle * Math.PI / 180.0;
                double cos = Math.Cos(radian);
                double sin = Math.Sin(radian);
                //原图的宽和高
                int w = ing.Width;
                int h = ing.Height;
                int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
                int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
                //目标位图
                Bitmap dsImage = new Bitmap(W, H);
                Graphics g = Graphics.FromImage(dsImage);
                //g.InterpolationMode = InterpolationMode.Bilinear;
                g.SmoothingMode = SmoothingMode.HighQuality;
                //计算偏移量
                Point Offset = new Point((W - w) / 2, (H - h) / 2);
                //构造图像显示区域：让图像的中心与窗口的中心点一致
                Rectangle rect = new Rectangle(0, 0, w, h);
                Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                g.TranslateTransform(center.X, center.Y);
                g.RotateTransform(360 - angle);
                //恢复图像在水平和垂直方向的平移
                g.TranslateTransform(-center.X, -center.Y);
                g.DrawImage(ing, rect);
                //重至绘图的所有变换
                g.ResetTransform();
                g.Save();
                g.Dispose();
                //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                return dsImage;
            }
            catch { return null; }
        }
    }
}
