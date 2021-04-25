using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TSkin;

namespace TPlayerList
{
    public partial class MenuXList : Control
    {

        public MenuXList()
        {
            chatVScroll = new XScroll(this);
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
        }

        #region Properties

        /// <summary>
        /// 获取或者设置滚动条背景色
        /// </summary>
        [DefaultValue(typeof(Color), "Transparent"), Category("ArrowColor")]
        [Description("滚动条的背景颜色")]
        public Color ScrollBackColor
        {
            get { return chatVScroll.BackColor; }
            set { chatVScroll.BackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条滑块默认颜色
        /// </summary>
        [DefaultValue(typeof(Color), "60, 0, 0,0"), Category("ArrowColor")]
        [Description("滚动条滑块默认情况下的颜色")]
        public Color ScrollSliderDefaultColor
        {
            get { return chatVScroll.SliderDefaultColor; }
            set { chatVScroll.SliderDefaultColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条点下的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "136, 0, 0,0"), Category("ArrowColor")]
        [Description("滚动条滑块被点击或者鼠标移动到上面时候的颜色")]
        public Color ScrollSliderDownColor
        {
            get { return chatVScroll.SliderDownColor; }
            set { chatVScroll.SliderDownColor = value; }
        }

        //private bool _VScroll;
        ///// <summary>
        ///// 获取或者设置选中的子项的颜色
        ///// </summary>
        //[DefaultValue(typeof(bool), "true"), Category("VScroll")]
        //[Description("显示滚动条")]
        //public bool VScroll
        //{
        //    get { return _VScroll;  }
        //    set { _VScroll = value; }
        //}


        #endregion

        #region 属性
        public List<TopItem> Items = new List<TopItem>();


        public XScroll chatVScroll;    //滚动条

        //protected TopItem m_mouseOnItem;

        #endregion
        private int _SelectItemIndex = -1;
        public int SelectItemIndex
        {
            get { return _SelectItemIndex; }
            set
            {
                if (_SelectItemIndex != value)
                {
                    _SelectItemIndex = value;
                    this.Invalidate();
                }
            }
        }
        public bool Kdown(Keys KeyCode)
        {
            if (KeyCode == Keys.Right)
            {
                //int seb = SelectIndex;
                //doha(seb);


                int seb = SelectIndex;
                int bbqq = seb + 1;
                if (bbqq < Items.Count)
                {
                    TopItem TopItem = Items[bbqq];
                    SelectIndex = TopItem.Index;
                    SetSv(TopItem);
                    foreach (TopItem TopItems in Items)
                    {
                        if (TopItems.Select)
                        {
                            TopItems.Select = false;
                            Tom(TopItems);
                        }
                    }
                    if (!TopItem.Select)
                    {
                        TopItem.Select = true;
                        Tom(TopItem);
                    }
                }

                return true;
            }
            else if (KeyCode == Keys.Left)
            {

                int seb = SelectIndex;
                int bbqq = seb - 1;
                if (bbqq >= 0)
                {
                    TopItem TopItem = Items[bbqq];

                    SelectIndex = TopItem.Index;
                    SetSv(TopItem);
                    foreach (TopItem TopItems in Items)
                    {
                        if (TopItems.Select)
                        {
                            TopItems.Select = false;
                            Tom(TopItems);
                        }
                    }
                    if (!TopItem.Select)
                    {
                        TopItem.Select = true;
                        Tom(TopItem);
                    }
                }

                return true;
            }
            else if (KeyCode == Keys.Enter || KeyCode == Keys.Space)
            {
                if (SelectIndex != -1)
                {
                    TopItem TopItem = Items[SelectIndex];
                    if (TopItem != null)
                    {
                        SelectItem = TopItem;
                        if (TopItem.Visible)
                        {
                            if (DownClick != null)
                            {
                                DownClick(TopItem);
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
            Kdown(e.KeyCode);
            //base.OnPreviewKeyDown(e);return;
            base.OnPreviewKeyDown(e);
        }
        public void SetSv(TopItem TopItem)
        {
            if (chatVScroll.ShouldBeDraw)
            {
                if (chatVScroll.Value + Width < TopItem.Bounds.X)
                {
                    chatVScroll.Value = TopItem.Bounds.X;
                }
                else if (chatVScroll.Value > TopItem.Bounds.X)
                {
                    chatVScroll.Value = TopItem.Bounds.X - 150;
                }
            }
        }
        public void SetSvs(TopItem TopItem)
        {
            if (chatVScroll.ShouldBeDraw)
            {
                if (chatVScroll.Value + Width < TopItem.Bounds.X + 100)
                {
                    chatVScroll.Value += 5;
                }
                else if (chatVScroll.Value > TopItem.Bounds.X - 100)
                {
                    chatVScroll.Value -= 5;
                }
            }
        }
        int Tops = 0;
        public void InPaint()
        {
            if (Width > 0 && Height > 0)
            {
                int Topsz = 0;
                if (Items.Count != 0)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        TopItem it = Items[i];

                        it.Index = i;
                        if (it.Visible)
                        {
                            Size sizeF = TextRenderer.MeasureText(it.Name, this.Font);
                            it.FontWidth = sizeF.Width;
                            it.FontHeight = sizeF.Height + 10;
                            it.Name_Bounds = new Rectangle(Topsz + 10, (Height - it.FontHeight) / 2, it.FontWidth, it.FontHeight);
                            it.SelectBounds = new Rectangle(Topsz + 10, 5, it.FontWidth, 5);
                            it.Bounds = new Rectangle(Topsz, 0, it.FontWidth + 20, Height);
                            it.ErrBounds = new Rectangle((it.Bounds.X + it.Bounds.Width) - 16, 12, 16, 16);
                            Topsz += it.Bounds.Width;
                        }
                    }
                    Tops = Topsz;
                    if (chatVScroll.VirtualWidth != Tops)
                    {
                        chatVScroll.VirtualHeight = Height;
                        chatVScroll.VirtualWidth = Tops;
                        Invalidate(chatVScroll.Bounds);
                    }

                }
            }
        }

        public int SelectIndex = -1;
        protected override void OnSizeChanged(EventArgs e)
        {
            if (Width > 0 && Height > 0)
            {
                int Topsz = 0;
                if (Items.Count != 0)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        TopItem it = Items[i];

                        it.Index = i;
                        if (it.Visible)
                        {
                            it.Name_Bounds = new Rectangle(Topsz + 10, (Height - it.FontHeight) / 2, it.FontWidth, it.FontHeight);
                            it.SelectBounds = new Rectangle(Topsz + 10, 5, it.FontWidth, 5);
                            it.Bounds = new Rectangle(Topsz, 0, it.FontWidth + 20, Height);
                            it.ErrBounds = new Rectangle((it.Bounds.X + it.Bounds.Width) - 16, 12, 16, 16);
                            Topsz += it.Bounds.Width;
                        }
                    }
                    Tops = Topsz;
                    chatVScroll.VirtualHeight = Height;
                    chatVScroll.VirtualWidth = Tops;
                    Invalidate(chatVScroll.Bounds);
                }
            }

            //InPaint();
            //int height = Height;
            //foreach (TopItem it in Items)
            //{
            //    it.Bounds = new Rectangle(it.Bounds.X, it.Bounds.Y, it.Bounds.Width, height);
            //}
            //if (chatVScroll.VirtualWidth != Tops)
            //{
            //    chatVScroll.VirtualHeight = height;
            //    chatVScroll.VirtualWidth = Tops;
            //    Invalidate(chatVScroll.Bounds);
            //}
            base.OnSizeChanged(e);
        }
        public SolidBrush FontColor_Default = new SolidBrush(Color.FromArgb(220, Color.Black));
        public SolidBrush FontColor_Hove = new SolidBrush(Color.Black);
        public SolidBrush FontColor_False = new SolidBrush(Color.LightGray);
        public SolidBrush FontColor_White = new SolidBrush(Color.White);

        //SolidBrush solidBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
        SolidBrush bsolidBrush = new SolidBrush(Color.FromArgb(0, 120, 216));
        SolidBrush rsolidBrush = new SolidBrush(Color.FromArgb(235, 17, 35));

        SolidBrush yllosolidBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 0));
        StringFormat _StringFormat_C = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };

        StringFormat _StringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                if (base.ForeColor != value)
                {
                    FontColor_Default = new SolidBrush(value);
                    FontColor_False = new SolidBrush(Color.FromArgb(100, value));
                    base.ForeColor = value;
                    this.Invalidate();
                }
            }
        }



        [Description("边框颜色"), Category("TSkin样式"), DefaultValue(typeof(Color), "0, 120, 216")]
        public Color BorderColor
        {
            get => bsolidBrush.Color;
            set
            {
                if (bsolidBrush.Color != value)
                {
                    bsolidBrush.Color = value;
                    this.Invalidate();
                }
            }
        }
        Font font_err = new Font("微软雅黑", 10, FontStyle.Bold);

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Items.Count > 0)
            {
                Graphics g = e.Graphics;
                g.TranslateTransform(-chatVScroll.Value, 0);        //根据滚动条的值设置坐标偏移
                g.SmoothingMode = SmoothingMode.HighQuality;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

                int toms = chatVScroll.Bounds.X + chatVScroll.Value;
                int tom = toms - 200;


                for (int i = 0; i < Items.Count; i++)
                {
                    TopItem it = Items[i];

                    if (it.Visible)
                    {
                        if (it.Bounds.X < tom)
                        {
                            continue;
                        }
                        if (it.Bounds.X - toms > Width)
                        {
                            continue;
                        }
                        if (it.Select)
                        {
                            g.FillRectangle(yllosolidBrush, it.Bounds);
                        }
                        //if (it.Check)
                        //{
                        //    g.FillRectangle(wsolidBrush, it.Bounds);
                        //}

                        if (it.Err)
                        {
                            g.FillEllipse(rsolidBrush, it.ErrBounds);
                            g.DrawString("!", font_err, FontColor_White, it.ErrBounds, _StringFormat_C);
                        }

                        if (_SelectItemIndex == it.Index)
                        {
                            g.FillRectangle(bsolidBrush, it.SelectBounds);
                        }
                        if (it.Enabled)
                        {
                            g.DrawString(it.Name, this.Font, it.MouseHover ? FontColor_Hove : FontColor_Default, it.Name_Bounds, _StringFormat);
                        }
                        else
                        {
                            g.DrawString(it.Name, this.Font, FontColor_False, it.Name_Bounds, _StringFormat);
                        }
                    }
                }
                g.ResetTransform();             //重置坐标系
                                                ////MessageBox.Show(Tops.ToString());
                if (chatVScroll.ShouldBeDraw)   //是否绘制滚动条
                    chatVScroll.ReDrawScroll(g);

            }
            base.OnPaint(e);
        }
        public event DownEventHandler DownClick;
        public void OnDownClick(TopItem topItem)
        {
            if (DownClick != null)
            {
                SelectIndex = topItem.Index;
                chatVScroll.Value = topItem.Bounds.X;
                Invalidate();
                DownClick(topItem);
            }
        }
        public delegate void DownEventHandler(TopItem Item);

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0) chatVScroll.Value -= 50;
            if (e.Delta < 0) chatVScroll.Value += 50;
            base.OnMouseWheel(e);
        }
        Point old;
        public void Tom(TopItem its)
        {
            Rectangle rect = its.Bounds;
            int tom = chatVScroll.Bounds.X + chatVScroll.Value;
            rect.X -= tom;
            this.Invalidate(rect);
        }
        bool IsMouseDown = false;
        int MouseDownX = -1;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            Point m_ptMousePos = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                MouseDownX = e.X;
                IsMouseDown = true;
                old = m_ptMousePos;
                if (chatVScroll.Bounds.Contains(m_ptMousePos))
                {
                    chatVScroll.MouseDownX = MouseDownX;
                    chatVScroll.IsMouseDown = true;
                    //if (chatVScroll.SliderBounds.Contains(m_ptMousePos))
                    //{
                    //}
                }
                else
                {
                    if (Items.Count > 0)
                    {
                        SelectItem = null;
                        for (int i = 0; i < Items.Count; i++)
                        {
                            TopItem it = Items[i];
                            if (it.Visible)
                            {
                                int tom = chatVScroll.Bounds.X + chatVScroll.Value;
                                Rectangle rect = it.Bounds;
                                Rectangle rects = it.Name_Bounds;
                                rect.X -= tom;
                                rects.X -= tom;
                                if (rects.Contains(m_ptMousePos))
                                {
                                    SelectIndex = i;
                                    SelectItem = it;
                                    it.Select = false;
                                    break;
                                }
                            }
                        }
                        if (SelectItem == null)
                        {
                            base.OnMouseDown(e);
                        }
                    }
                    else
                    {
                        base.OnMouseDown(e);
                    }
                }
            }
            else if (e.Button == MouseButtons.XButton1)
            {
                chatVScroll.Value += 50;
                //base.OnMouseDown(e);
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                chatVScroll.Value -= 50;
                //base.OnMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Point m_ptMousePos = e.Location;

            if (e.Button == MouseButtons.Left)
            {
                MouseDownX = -1;
                IsMouseDown = false;
                if (chatVScroll.IsMouseDown)
                {
                    chatVScroll.IsMouseDown = false;
                    //如果有滚动条 判断是否在滚动条类点击
                    if (chatVScroll.Bounds.Contains(m_ptMousePos))
                    {        //判断在滚动条那个位置点击
                        if (!chatVScroll.SliderBounds.Contains(m_ptMousePos))
                            chatVScroll.MoveSliderToLocation(m_ptMousePos.X);
                        return;
                    }
                }
                else if (m_ptMousePos == old)
                {
                    if (Items.Count > 0)
                    {
                        SelectItem = null;
                        for (int i = 0; i < Items.Count; i++)
                        {
                            TopItem it = Items[i];
                            if (it.Visible)
                            {
                                int tom = chatVScroll.Bounds.X + chatVScroll.Value;
                                Rectangle rect = it.Bounds;
                                Rectangle rects = it.Name_Bounds;
                                rect.X -= tom;
                                rects.X -= tom;
                                if (it.Select)
                                {
                                    it.Select = false;
                                    Invalidate(rect);
                                }
                                if (rects.Contains(m_ptMousePos))
                                {
                                    SelectIndex = i;
                                    SelectItem = it;
                                    it.Select = false;
                                    if (DownClick != null)
                                    { DownClick(it); }
                                }
                            }
                        }
                        if (SelectItem == null)
                        {
                            base.OnMouseUp(e);
                        }
                    }
                    else
                    {
                        base.OnMouseUp(e);
                    }

                    //this.Focus();
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
            Point m_ptMousePos = e.Location;
            if (e.Button == MouseButtons.XButton1)
            {
                chatVScroll.Value += 50;
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                chatVScroll.Value -= 50;
            }

            //bool ij = chatVScroll.Bounds.Contains(m_ptMousePos);
            if (chatVScroll.IsMouseDown)
            {
                //如果滚动条的滑块处于被点击 那么移动
                chatVScroll.MoveSliderFromLocation(e.X);
                return;
            }
            else
            {
                if (m_ptMousePos.X != old.X && m_ptMousePos.Y != old.Y && IsMouseDown)
                {
                    chatVScroll.MouseDownX = MouseDownX;
                    chatVScroll.IsMouseDown = true;
                }
                else
                {
                    int tom = chatVScroll.Bounds.X + chatVScroll.Value;
                    foreach (TopItem its in Items)
                    {
                        if (its.Visible)
                        {
                            Rectangle rect = its.Bounds;
                            Rectangle rects = its.Name_Bounds;
                            rect.X -= tom;
                            rects.X -= tom;
                            if (rects.Contains(m_ptMousePos))
                            {
                                if (!its.MouseHover)
                                {
                                    its.MouseHover = true;
                                    this.Invalidate(rect);
                                }
                            }
                            else
                            {
                                if (its.MouseHover)
                                {
                                    its.MouseHover = false;
                                    this.Invalidate(rect);
                                }
                            }
                        }
                    }
                }
            }
            if (chatVScroll.ShouldBeDraw)
            {
                if (chatVScroll.Bounds.Contains(m_ptMousePos))
                {
                    //ClearItemMouseOn();
                    if (chatVScroll.SliderBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnSlider = true;
                    else
                        chatVScroll.IsMouseOnSlider = false;
                    return;
                }
                else
                    chatVScroll.ClearAllMouseOn();
            }
            //m_ptMousePos.Y += chatVScroll.Value;//如果不在滚动条范围类 那么根据滚动条当前值计算虚拟的一个坐标
            //ClearItemMouseOn();
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            SelectIndex = -1;
            try
            {
                foreach (TopItem its in Items)
                {
                    if (its.Visible)
                    {
                        if (its.MouseHover)
                        {
                            Rectangle rect = its.Bounds;
                            int tom = chatVScroll.Bounds.X + chatVScroll.Value;
                            rect.X -= tom;
                            its.MouseHover = false;
                            this.Invalidate(rect);
                        }
                    }
                }
            }
            catch { }
            chatVScroll.ClearAllMouseOn();
            base.OnMouseLeave(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            try
            {
                foreach (TopItem it in Items)
                {
                    if (it.Visible)
                    {
                        if (it.MouseHover || it.Select)
                        {
                            Rectangle rect = it.Bounds;
                            int tom = chatVScroll.Bounds.X + chatVScroll.Value;
                            rect.X -= tom;
                            it.MouseHover = false;
                            it.Select = false;
                            this.Invalidate(rect);
                        }
                    }
                }
            }
            catch { }
            chatVScroll.ClearAllMouseOn();
            base.OnLeave(e);
        }

        public TopItem SelectItem { get; set; }
    }

    public class TopItem
    {
        public object ID { set; get; }
        public bool MouseHover { set; get; }
        public int FontWidth { set; get; }
        public int FontHeight { set; get; }
        //public Rectangle Tou { set; get; }
        public Rectangle Bounds { set; get; }
        public Rectangle SelectBounds { set; get; }
        public Rectangle ErrBounds { set; get; }
        public Rectangle Name_Bounds { set; get; }
        public string Name { get; set; }
        public string Pinyin { get; set; }
        public object Tag { get; set; }
        public bool Visible { set; get; }
        public bool Enabled { set; get; }
        public bool Err { set; get; }

        public int Index { set; get; }
        public bool Select { set; get; }
    }
}
