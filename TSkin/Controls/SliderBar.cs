using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;


namespace TSkin
{
    public class SliderBar : Control
    {
        #region 变量

        private RectangleF valueBar = new RectangleF(7, 5, 0, 6);
        private RectangleF valueRect = new RectangleF(0, 1, 14, 14);
        private bool mouseFlat = false;

        #endregion

        #region 属性

        public List<SliderPoints> Points { get; set; }

        private SolidBrush _themeColor = new SolidBrush(Color.FromArgb(64, 158, 255));
        [Description("主题颜色"), Category("外观"), DefaultValue(typeof(Color), "64, 158, 255")]
        public Color ThemeColor
        {
            get { return _themeColor.Color; }
            set
            {
                _themeColor.Color = value;
                Invalidate();
            }
        }
        private SolidBrush _backColor = new SolidBrush(Color.White);
        [Description("线条背景颜色"), Category("外观"), DefaultValue(typeof(Color), "White")]
        public Color BackColorLine
        {
            get { return _backColor.Color; }
            set
            {
                _backColor.Color = value;
                Invalidate();
            }
        }

        private SolidBrush _backColorCir = new SolidBrush(Color.White);
        [Description("圆点颜色颜色"), Category("外观"), DefaultValue(typeof(Color), "White")]
        public Color BackColorCir
        {
            get { return _backColorCir.Color; }
            set
            {
                _backColorCir.Color = value;
                Invalidate();
            }
        }


        private int _minValue = 0;
        [Description("Slider的最小值"), Category("外观"), DefaultValue(0)]
        public int MinValue
        {
            get { return _minValue; }
            set
            {
                if (value > _maxValue) return;
                if (_minValue != value)
                {
                    _minValue = value;
                    Invalidate();
                }
            }
        }

        private int _maxValue = 10;
        [Description("Slider的最大值"), Category("外观"), DefaultValue(10)]
        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (value < _minValue || value < _value) return;
                if (_maxValue != value)
                {
                    _maxValue = value;
                    Invalidate();
                }
            }
        }
        public delegate void ValueEventHandler(int value);
        public event ValueEventHandler ValueChange;
        private int _value = 0;
        [Description("Slider的当前值"), Category("外观"), DefaultValue(0)]
        public int Value
        {
            get { return _value; }
            set
            {
                if (value < _minValue)
                {
                    value = _minValue;
                }
                else if (value > _maxValue)
                {
                    value = _maxValue;
                }
                if (_value != value)
                {
                    _value = value;
                    if (ValueChange != null)
                    {
                        ValueChange(_value);
                    }
                    Invalidate();
                }
            }
        }

        private bool _showValue = true;
        [Description("是否显示数值"), Category("外观"), DefaultValue(true)]
        public bool ShowValue
        {
            get { return _showValue; }
            set
            {
                _showValue = value;
                Invalidate();
            }
        }


        #endregion

        #region 事件
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = _showValue ? 44 : 16;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                mouseFlat = true;
                //double ji = Convert.ToDouble(e.X + 15 - 8) / Convert.ToDouble(Width - 30);
                double ji = (e.X * 1.0) / ((Width - 30) * 1.0);
                int _val = (int)Math.Round(ji * _maxValue);
                if (Points != null && Points.Count > 0)
                {
                    int _valOne = (int)Math.Ceiling(ji * Width) * 2;
                    //if (_valOne < 60)
                    //{
                    //    _valOne = 60;
                    //}
                    SliderPoints val = Points.Find(ab => ab.rect.Contains(e.Location));
                    if (val != null)
                    {
                        Value = val.val;
                    }
                    else
                    {
                        Value = _val;
                    }
                }
                else
                {
                    Value = _val;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseFlat)
            {
                double ji = (e.X * 1.0) / ((Width - 30) * 1.0);
                Value = (int)Math.Round(ji * _maxValue);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseFlat = false;
            Invalidate();
        }

        #endregion

        StringFormat Center { get => new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }; }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            //graphics.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(15, Height - 10, Width - 30, 4);
            int uk;
            if (_value == _maxValue)
            {
                uk = rect.Width;
            }
            else
            {
                uk = (int)Math.Round((rect.Width) * ((_value - _minValue) * 1.0 / (_maxValue * 1.0)));
            }

            Rectangle jrect = new Rectangle(rect.X, rect.Y, uk, rect.Height);


            if (ShowValue && mouseFlat)
            {
                int cirWidth = (int)Math.Ceiling(graphics.MeasureString(_value.ToString(), Font).Width) + 2;
                if (cirWidth < 18)
                {
                    cirWidth = 18;
                }
                int cirWidth2 = (cirWidth / 2);
                RectangleF cirRect = new RectangleF(jrect.X + uk - cirWidth2, 0, cirWidth, 18);
                if (cirRect.X + cirRect.Width > this.Width)
                {
                    cirRect.X = this.Width - cirRect.Width;
                }
                using (GraphicsPath path = cirRect.CreateRoundedRectanglePath(18))
                {
                    graphics.FillPath(_themeColor, path);
                    graphics.FillPolygon(_themeColor, new PointF[]
                    {
                        new PointF((jrect.X +uk)-cirWidth2,10f),
                        new PointF((jrect.X +uk)+cirWidth2,10f),
                        new PointF(jrect.X +uk,24F)
                    });
                    graphics.DrawString(_value.ToString(), Font, _backColorCir, cirRect, Center);
                }
            }

            graphics.FillRectangle(_backColor, rect);
            graphics.FillRectangle(_themeColor, jrect);

            if (Points != null && Points.Count > 0)
            {
                foreach (SliderPoints item in Points)
                {
                    int uks;
                    if (item.val == _maxValue)
                    {
                        uks = rect.Width;
                    }
                    else
                    {
                        uks = (int)Math.Round(rect.Width * ((item.val - _minValue) * 1.0 / (_maxValue * 1.0)));
                    }
                    item.rect = new RectangleF(rect.X + uks - 6, rect.Y - ((12 - 4) / 2), 12, 12);
                    graphics.FillEllipse(_themeColor, item.rect);
                    graphics.FillEllipse(_backColorCir, new RectangleF(rect.X + uks - 3, rect.Y - ((6 - 4) / 2), 6, 6));
                }
            }


            //graphics.FillRectangle(new SolidBrush(DrawHelper.BackColor), new RectangleF(15, Height - 10, Width - 30, 4));

            graphics.FillEllipse(_themeColor, new RectangleF(rect.X + uk - 8, rect.Y - ((16 - 4) / 2), 16, 16));
            graphics.FillEllipse(_backColorCir, new RectangleF(rect.X + uk - 5, rect.Y - ((10 - 4) / 2), 10, 10));
        }



        public SliderBar()
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
            Height = 44;
        }
    }
    public class SliderPoints
    {
        public SliderPoints()
        {
        }
        public SliderPoints(int val)
        {
            this.val = val;
        }
        public RectangleF rect { get; set; }
        public int val { get; set; }
    }
}