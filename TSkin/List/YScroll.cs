using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TSkin
{
    public class YScroll
    {
        //滚动条自身的区域
        private Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
        }
        //上边箭头区域
        private Rectangle upBounds;
        public Rectangle UpBounds
        {
            get { return upBounds; }
        }
        //下边箭头区域
        private Rectangle downBounds;
        public Rectangle DownBounds
        {
            get { return downBounds; }
        }
        //滑块区域
        private Rectangle sliderBounds;
        public Rectangle SliderBounds
        {
            get { return sliderBounds; }
        }

        private Color backColor = Color.Transparent;
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        private Color sliderDefaultColor = Color.FromArgb(60, 0, 0, 0);
        public Color SliderDefaultColor
        {
            get { return sliderDefaultColor; }
            set
            {
                if (sliderDefaultColor == value)
                    return;
                sliderDefaultColor = value;
                ctrl.Invalidate(this.sliderBounds);
            }
        }

        private Color sliderDownColor = Color.FromArgb(136, 0, 0, 0);
        public Color SliderDownColor
        {
            get { return sliderDownColor; }
            set
            {
                if (sliderDownColor == value)
                    return;
                sliderDownColor = value;
                ctrl.Invalidate(this.sliderBounds);
            }
        }

        private Color arrowhColor = Color.FromArgb(28, 151, 234);
        public Color ArrowDownColor
        {
            get { return arrowhColor; }
            set
            {
                if (arrowhColor == value)
                    return;
                arrowhColor = value;
                ctrl.Invalidate(this.bounds);
            }
        }

        private Color arrowColor = Color.FromArgb(134, 137, 153);
        public Color ArrowColor
        {
            get { return arrowColor; }
            set
            {
                if (arrowColor == value)
                    return;
                arrowColor = value;
                ctrl.Invalidate(this.bounds);
            }
        }
        //绑定的控件
        private Control ctrl;
        public Control Ctrl
        {
            get { return ctrl; }
            set { ctrl = value; }
        }
        //虚拟的一个高度(控件中内容的高度)
        private int virtualHeight;
        public int VirtualHeight
        {
            get { return virtualHeight; }
            set
            {
                if (value <= ctrl.Height)
                {
                    if (shouldBeDraw == false)
                        return;
                    shouldBeDraw = false;
                    this.value = 0;
                    ctrl.Invalidate();
                }
                else
                {
                    shouldBeDraw = true;
                    if (value - this.value < ctrl.Height)
                    {
                        this.value -= ctrl.Height - value + this.value;
                        ctrl.Invalidate();
                    }
                }
                virtualHeight = value;
            }
        }
        //当前滚动条位置
        private int value;
        public int Value
        {
            get { return value; }
            set
            {
                if (!shouldBeDraw)
                    return;
                if (value < 0)
                {
                    if (this.value == 0)
                        return;
                    this.value = 0;
                    ctrl.Invalidate();

                    if (ScrollChange != null)
                    { ScrollChange(0); }
                    return;
                }
                if (value > virtualHeight - ctrl.Height)
                {
                    if (this.value == virtualHeight - ctrl.Height)
                        return;
                    this.value = virtualHeight - ctrl.Height;

                    ctrl.Invalidate();
                    if (ScrollChange != null)
                    { ScrollChange(Math.Round((this.value * 1.0) / ((virtualHeight - bounds.Height) * 1.0), 2)); }
                    return;
                }
                this.value = value;

                if (ScrollChange != null)
                { ScrollChange(Math.Round((this.value * 1.0) / ((virtualHeight - bounds.Height) * 1.0), 2)); }
                ctrl.Invalidate();
            }
        }
        //是否有必要在控件上绘制滚动条
        private bool shouldBeDraw;
        public bool ShouldBeDraw
        {
            get { return shouldBeDraw; }
        }

        private bool isMouseDown;
        public bool IsMouseDown
        {
            get { return isMouseDown; }
            set
            {
                if (value)
                {
                    m_nLastSliderY = sliderBounds.Y;
                }
                isMouseDown = value;
            }
        }

        private bool isMouseOnSlider;
        public bool IsMouseOnSlider
        {
            get { return isMouseOnSlider; }
            set
            {
                if (isMouseOnSlider == value)
                    return;
                isMouseOnSlider = value;
                ctrl.Invalidate(this.SliderBounds);
            }
        }

        private bool isMouseOnUp;
        public bool IsMouseOnUp
        {
            get { return isMouseOnUp; }
            set
            {
                if (isMouseOnUp == value)
                    return;
                isMouseOnUp = value;
                ctrl.Invalidate(this.UpBounds);
            }
        }

        private bool isMouseOnDown;
        public bool IsMouseOnDown
        {
            get { return isMouseOnDown; }
            set
            {
                if (isMouseOnDown == value)
                    return;
                isMouseOnDown = value;
                ctrl.Invalidate(this.DownBounds);
            }
        }
        //鼠标在滑块点下时候的y坐标
        private int mouseDownY;
        public int MouseDownY
        {
            get { return mouseDownY; }
            set { mouseDownY = value; }
        }
        //滑块移动前的 滑块的y坐标
        private int m_nLastSliderY;

        public YScroll(Control c)
        {
            this.ctrl = c;
            virtualHeight = 400;
            bounds = new Rectangle(0, 0, 10, 10);
            upBounds = new Rectangle(0, 0, 10, 10);
            downBounds = new Rectangle(0, 0, 10, 10);
            sliderBounds = new Rectangle(0, 0, 10, 10);
        }

        public void ClearAllMouseOn()
        {
            if (!this.isMouseOnDown && !this.isMouseOnSlider && !this.isMouseOnUp)
                return;
            this.isMouseOnDown =
                this.isMouseOnSlider =
                this.isMouseOnUp = false;
            ctrl.Invalidate(this.bounds);
        }
        public void OnScrollChange(double value)
        {
            if (ScrollChange != null)
            { ScrollChange(value); }
        }
        public event ScrollEventHandler ScrollChange;
        public delegate void ScrollEventHandler(double value);

        //将滑块跳动至一个地方
        public void MoveSliderToLocation(int nCurrentMouseY)
        {
            if (nCurrentMouseY - sliderBounds.Height / 2 < 11)
                sliderBounds.Y = 11;
            else if (nCurrentMouseY + sliderBounds.Height / 2 > ctrl.Height - 11)
                sliderBounds.Y = ctrl.Height - sliderBounds.Height - 11;
            else
                sliderBounds.Y = nCurrentMouseY - sliderBounds.Height / 2;
            this.value = (int)((double)(sliderBounds.Y - 11) / (ctrl.Height - 22 - SliderBounds.Height) * (virtualHeight - ctrl.Height));


            ctrl.Invalidate();

            if (ScrollChange != null)
            { ScrollChange(Math.Round((this.value * 1.0) / ((virtualHeight - bounds.Height) * 1.0), 2)); }
        }
        //根据鼠标位置移动滑块
        public void MoveSliderFromLocation(int nCurrentMouseY)
        {
            //if (!this.IsMouseDown) return;
            if (m_nLastSliderY + nCurrentMouseY - mouseDownY < 11)
            {
                if (sliderBounds.Y == 11)
                    return;
                sliderBounds.Y = 11;
            }
            else if (m_nLastSliderY + nCurrentMouseY - mouseDownY > ctrl.Height - 11 - SliderBounds.Height)
            {
                if (sliderBounds.Y == ctrl.Height - 11 - sliderBounds.Height)
                    return;
                sliderBounds.Y = ctrl.Height - 11 - sliderBounds.Height;
            }
            else
            {
                sliderBounds.Y = m_nLastSliderY + nCurrentMouseY - mouseDownY;
            }
            this.value = (int)((double)(sliderBounds.Y - 11) / (ctrl.Height - 22 - SliderBounds.Height) * (virtualHeight - ctrl.Height));

            ctrl.Invalidate();

            if (ScrollChange != null)
            { ScrollChange(Math.Round((this.value * 1.0) / ((virtualHeight - bounds.Height) * 1.0), 2)); }
        }
        //绘制滚动条
        public void ReDrawScroll(Graphics g)
        {
            if (!shouldBeDraw)
                return;
            bounds.X = ctrl.Width - 12;
            bounds.Height = ctrl.Height;
            upBounds.X = downBounds.X = bounds.X;
            downBounds.Y = ctrl.Height - 10;
            //计算滑块位置
            sliderBounds.X = bounds.X;
            sliderBounds.Height = (int)(((double)ctrl.Height / virtualHeight) * (ctrl.Height - 22));
            if (sliderBounds.Height < 10) sliderBounds.Height = 10;
            sliderBounds.Y = 11 + (int)(((double)value / (virtualHeight - ctrl.Height)) * (ctrl.Height - 22 - sliderBounds.Height));
            SolidBrush sb = new SolidBrush(this.backColor);
            SolidBrush sbc = new SolidBrush(this.arrowColor);
            SolidBrush sbcd = new SolidBrush(this.arrowColor);
            try
            {
                g.FillRectangle(sb, bounds);

                //g.FillRectangle(sb, upBounds);
                //g.FillRectangle(sb, downBounds);
                if (this.isMouseDown || this.isMouseOnSlider)
                {
                    sb.Color = this.sliderDownColor;
                }
                else
                {
                    sb.Color = this.sliderDefaultColor;
                }
                if (this.isMouseOnUp)
                {
                    sbc.Color = this.arrowhColor;
                }
                if (this.isMouseOnDown)
                {
                    sbcd.Color = this.arrowhColor;
                }
                using (GraphicsPath path = sliderBounds.CreateRoundedRectanglePath( 9 , UICornerRadiusSides.All))
                {
                    g.FillPath(sb, path);
                }
                //g.FillRectangle(sb, sliderBounds);
                //sb.Color = this.arrowColor;
                //if (this.isMouseOnUp)
                g.FillPolygon(sbc, new Point[]{
                    new Point(ctrl.Width -7,3),
                    new Point(ctrl.Width - 12,9),
                    new Point(ctrl.Width-2 ,9)
                });
                //if (this.isMouseOnDown)
                g.FillPolygon(sbcd, new Point[]{
                    new Point(ctrl.Width -7,ctrl.Height -3),
                    new Point(ctrl.Width - 12,ctrl.Height -9),
                    new Point(ctrl.Width -2,ctrl.Height -9)
                });
            }
            finally
            {
                sb.Dispose();
            }
        }

    }
}
