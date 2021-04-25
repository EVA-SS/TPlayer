using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TSkin
{
    public class BackImagePanel : Panel
    {
        public BackImagePanel()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.DoubleBuffer |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.UpdateStyles();
        }

        Image _backImg = null, _backImgNew = null;
        [Description("设置背景图像"), Category("外观"), DefaultValue(null)]
        public override Image BackgroundImage
        {
            get { return _backImg; }
            set
            {
                if (_backImg != value)
                {
                    if (_backImg != null)
                    {
                        _backImg.Dispose();
                    }
                    _backImg = value;
                    if (_blur > 0)
                    {
                        OnBlur();
                    }
                    else
                    {
                        _backImgNew = null;
                    }
                    OnSizeChanged(null);
                    Invalidate();
                }
            }
        }
        private int _blur = 0;
        [Description("设置背景模糊程度"), Category("外观"), DefaultValue(0)]
        public int Blur
        {
            get
            {
                return _blur;
            }
            set
            {
                if (_blur != value)
                {
                    _blur = value; 
                    if (_backImg != null)
                    {
                        OnBlur();
                        Invalidate();
                    }
                }
            }

        }

        SolidBrush _blurBrush = new SolidBrush(Color.Transparent);
        [Description("设置模糊叠加"), Category("外观"), DefaultValue(typeof(Color), "Transparent")]
        public Color BlurColor
        {
            get { return _blurBrush.Color; }
            set
            {
                if (_blurBrush.Color != value)
                {
                    _blurBrush.Color = value;
                    if (_backImg != null&& _blur>0)
                    {
                        OnBlur();
                        Invalidate();
                    }
                }
            }
        }
        void OnBlur()
        {
            if (_backImgNew != null)
            {
                _backImgNew.Dispose();
            }
            Rectangle Rect = new Rectangle(0, 0, _backImg.Width, _backImg.Height);
            Bitmap bitmap = new Bitmap(_backImg);
            if (_blurBrush.Color != Color.Transparent)
            {
                using (Graphics gTop = Graphics.FromImage(bitmap))
                {
                    gTop.FillRectangle(_blurBrush, Rect);
                }
            }
            bitmap.GaussianBlur(ref Rect, _blur, false);
            _backImgNew = bitmap;
        }
        Image _img = null;
        [Description("设置图像"), Category("外观"), DefaultValue(null)]
        public Image Image
        {
            get { return _img; }
            set
            {
                if (_img != null)
                {
                    _img.Dispose();
                }
                _img = value;
                Invalidate();
            }
        }
        int _imgSize = 0;
        [Description("设置图像大小"), Category("外观"), DefaultValue(0)]
        public int ImageSize
        {
            get { return _imgSize; }
            set
            {
                _imgSize = value;
                if (_imgSize > 0)
                {
                    OnSizeChanged(null);
                }
                Invalidate();
            }
        }

        Rectangle rect_img, rect_qg, rect_imgs;
        protected override void OnSizeChanged(EventArgs e)
        {
            if (_backImg != null)
            {
                Rectangle rect = this.ClientRectangle;
                if (rect.Width > 0 && rect.Height > 0)
                {

                    rect_img = new Rectangle(0, 0, _backImg.Width, _backImg.Height);

                    int destWidth = rect.Width, destHeight = rect.Height;
                    int sH, sW;
                    if ((rect_img.Width * destHeight) > (rect_img.Height * destWidth))
                    {
                        sH = destHeight;
                        sW = (rect_img.Width * destHeight) / rect_img.Height;
                    }
                    else
                    {
                        sW = destWidth;
                        sH = (destWidth * rect_img.Height) / rect_img.Width;
                    }

                    rect_qg = new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH);

                    if (_imgSize > 0)
                    {
                        rect_imgs = new Rectangle((rect.Width - _imgSize) / 2, (rect.Height - _imgSize) / 2, _imgSize, _imgSize);
                    }
                }
            }
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            if (_backImg != null)
            {
                g.DrawImage(_backImgNew != null ? _backImgNew : _backImg, rect_qg, rect_img, GraphicsUnit.Pixel);
            }

            if (_img != null && _imgSize > 0)
            {
                g.DrawImage(_img, rect_imgs);
            }
        }
    }
}
