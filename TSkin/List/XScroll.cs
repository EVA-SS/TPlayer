using System.Drawing;
using System.Windows.Forms;

namespace TSkin
{

    public class XScroll
    {
        //滚动条自身的区域
        private Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
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

        //绑定的控件
        private Control ctrl;
        public Control Ctrl
        {
            get { return ctrl; }
            set { ctrl = value; }
        }
        //虚拟的一个高度(控件中内容的高度)
        private int virtualWidth;
        public int VirtualWidth
        {
            get { return virtualWidth; }
            set
            {
                if (ctrl.Width == 0 || value <= ctrl.Width)
                {
                    if (shouldBeDraw == false)
                        return;
                    shouldBeDraw = false;
                    ctrl.Invalidate();
                }
                else
                {
                    shouldBeDraw = true;
                    if (value - this.value < ctrl.Width)
                    {
                        this.value -= ctrl.Width - value + this.value;
                        ctrl.Invalidate();
                    }
                }
                virtualWidth = value;
            }
        }
        public int VirtualHeight
        {
            set
            {
                int he = value - 8;
                bounds = new Rectangle(0, he, 8, 8);
                sliderBounds = new Rectangle(0, he, 8, 8);
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
                    return;
                }
                if (value > virtualWidth - ctrl.Width)
                {
                    if (this.value == virtualWidth - ctrl.Width)
                        return;
                    this.value = virtualWidth - ctrl.Width;

                    ctrl.Invalidate();
                    return;
                }
                this.value = value;

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
                    m_nLastSliderX = sliderBounds.X;
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

        //鼠标在滑块点下时候的y坐标
        private int mouseDownX;
        public int MouseDownX
        {
            get { return mouseDownX; }
            set { mouseDownX = value; }
        }
        //滑块移动前的 滑块的y坐标
        private int m_nLastSliderX;

        public XScroll(Control c)
        {
            this.ctrl = c;
            //virtualWidth = 400;
            bounds = new Rectangle(0, 0, 8, 8);
            sliderBounds = new Rectangle(0, 0, 8, 8);
        }

        public void ClearAllMouseOn()
        {
            this.isMouseOnSlider = false;
            ctrl.Invalidate(this.bounds);
        }
        //将滑块跳动至一个地方
        public void MoveSliderToLocation(int nCurrentMouseX)
        {
            if (nCurrentMouseX - sliderBounds.Width / 2 <= 0)
                sliderBounds.X = 0;
            else if (nCurrentMouseX + sliderBounds.Width / 2 > ctrl.Width)
                sliderBounds.X = ctrl.Width - sliderBounds.Width;
            else
                sliderBounds.X = nCurrentMouseX - sliderBounds.Width / 2;
            this.value = (int)((double)(sliderBounds.X) / (ctrl.Width - SliderBounds.Width) * (virtualWidth - ctrl.Width));

            //if (VClick != null)
            //{ VClick(null, null); }

            ctrl.Invalidate();
        }
        //根据鼠标位置移动滑块
        public void MoveSliderFromLocation(int nCurrentMouseX)
        {
            if (m_nLastSliderX + nCurrentMouseX - mouseDownX <= 0)
            {
                if (sliderBounds.X == 0)
                    return;
                sliderBounds.X = 0;
            }
            else if (m_nLastSliderX + nCurrentMouseX - mouseDownX > ctrl.Width - SliderBounds.Width)
            {
                if (sliderBounds.X == ctrl.Width - sliderBounds.Width)
                    return;
                sliderBounds.X = ctrl.Width - sliderBounds.Width;
            }
            else
            {
                sliderBounds.X = m_nLastSliderX + nCurrentMouseX - mouseDownX;
            }
            this.value = (int)((double)(sliderBounds.X) / (ctrl.Width - SliderBounds.Width) * (virtualWidth - ctrl.Width));
            ctrl.Invalidate();
        }
        //绘制滚动条
        public void ReDrawScroll(Graphics g)
        {
            if (!shouldBeDraw)
                return;
            //bounds.Y = ctrl.Height;
            bounds.Width = ctrl.Width;
            //计算滑块位置
            sliderBounds.X = bounds.X;
            sliderBounds.Width = (int)(((double)ctrl.Width / virtualWidth) * (ctrl.Width));
            if (sliderBounds.Width < 0) sliderBounds.Width = 20;
            sliderBounds.X = (int)(((double)value / (virtualWidth - ctrl.Width)) * (ctrl.Width - sliderBounds.Width));
            if (sliderBounds.X > -1)
            {
                SolidBrush sb = new SolidBrush(this.backColor);
                try
                {
                    g.FillRectangle(sb, bounds);

                    if (this.isMouseDown || this.isMouseOnSlider)
                    {
                        sb.Color = this.sliderDownColor;
                    }
                    else
                    {
                        sb.Color = this.sliderDefaultColor;
                    }
                    g.FillRectangle(sb, sliderBounds);
                }
                finally
                {
                    sb.Dispose();
                }
            }
        }

    }
}
