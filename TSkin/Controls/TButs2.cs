using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TSkin
{
    [ToolboxBitmap(typeof(Button))]
    public partial class TButs2 : Control
    {

        public TButs2()
        {
            this.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.Selectable |
               ControlStyles.DoubleBuffer | ControlStyles.Selectable |
               ControlStyles.SupportsTransparentBackColor |
               ControlStyles.ContainerControl |
               ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }


        #region 属性
        bool select = false;
        [Description("是否选中"), Category("TSkin样式"), DefaultValue(false)]
        public bool Sel
        {
            get { return select; }
            set
            {
                if (select != value)
                {
                    select = value;

                    this.Invalidate();
                }
            }
        }

        Image EnImg = null;
        private Image _Img;
        [Description("图片"), Category("TSkin样式"), DefaultValue(null)]
        //[DefaultValue(typeof(Font), "宋体, 9pt")]
        public Image Image
        {
            get { return _Img; }
            set
            {
                if (_Img != value)
                {
                    _Img = value;
                    if (_Img != null)
                    {
                        EnImg = Api.GetImgH(_Img, 0.7f);
                    }
                    else { EnImg = null; }
                    this.Invalidate();
                }
            }
        }

        private Image _ImageSel;
        [Description("鼠标激活图片"), Category("TSkin样式"), DefaultValue(null)]
        //[DefaultValue(typeof(Font), "宋体, 9pt")]
        public Image ImageSel
        {
            get { return _ImageSel; }
            set
            {
                if (_ImageSel != value)
                {
                    _ImageSel = value;
                    this.Invalidate();
                }
            }
        }


        int _ImageWidth = 0;
        [Description("图片宽度"), Category("TSkin样式"), DefaultValue(0)]
        public int ImageWidth
        {
            get { return _ImageWidth; }
            set
            {
                if (_ImageWidth != value)
                {
                    _ImageWidth = value;
                    Rectangle rect = ClientRectangle;
                    img_rect = new Rectangle(rect.Width - _ImageWidth / 2, rect.Height - _ImageWidth / 2, _ImageWidth, _ImageWidth);
                    this.Invalidate();
                }
            }
        }

        #endregion


        protected override void OnSizeChanged(EventArgs e)
        {
            Rectangle rect = ClientRectangle;
            img_rect = new Rectangle((rect.Width - _ImageWidth) / 2, (rect.Height - _ImageWidth) / 2, _ImageWidth, _ImageWidth);
            base.OnSizeChanged(e);
        }

        public void PerformClick()
        {
            this.OnClick(EventArgs.Empty);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 32)
            {
                //回车
                e.Handled = true;
                OnClick(EventArgs.Empty);
                this.Parent.Focus();
                //this.Invalidate();
            }
            base.OnKeyPress(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }
        bool IsHove = false;
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!IsHove)
            {
                IsHove = true;
                this.Invalidate();
            }

            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {

            if (IsHove)
            {
                IsHove = false;
                this.Invalidate();
            }
            base.OnMouseLeave(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            if (IsHove)
            {
                IsHove = false;
                this.Invalidate();
            }
            base.OnLeave(e);
        }

        Rectangle img_rect;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;


            if (_Img != null)
            {
                if (select && _ImageSel != null)
                {
                    g.DrawImage(_ImageSel, img_rect);
                }
                else
                {
                    g.DrawImage(IsHove ? EnImg : _Img, img_rect);
                }
            }


            base.OnPaint(e);
        }




    }
}
