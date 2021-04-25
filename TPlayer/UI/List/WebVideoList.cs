using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TPlayer;
using TSkin;

namespace TPlayerList
{
    public partial class WebVideoList : Control
    {
        public WebVideoList()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            chatVScroll = new YScroll(this);
        }

        #region Properties

        int dimHeight = 0;
        [DefaultValue(false), Category("高级")]
        [Description("模糊高度")]
        public int DimHeight
        {
            get { return dimHeight; }
            set
            {
                if (value > -1)
                {
                    dimHeight = value;
                }
            }
        }

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
        /// <summary>
        /// 获取或者设置滚动条箭头的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "28, 151, 234"), Category("ArrowColor")]
        [Description("滚动条箭头的按下颜色")]
        public Color ScrollArrowDownColor
        {
            get { return chatVScroll.ArrowDownColor; }
            set { chatVScroll.ArrowDownColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条的箭头颜色
        /// </summary>
        [DefaultValue(typeof(Color), "134, 137, 153"), Category("ArrowColor")]
        [Description("滚动条箭头的颜色")]
        public Color ScrollArrowColor
        {
            get { return chatVScroll.ArrowColor; }
            set { chatVScroll.ArrowColor = value; }
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
        public bool _IsCheck = false;
        [DefaultValue(false), Category("外观")]
        [Description("是否多选")]
        public bool IsCheck
        {
            get { return _IsCheck; }
            set
            {
                if (_IsCheck != value)
                {
                    _IsCheck = value;
                    InPaint();
                    Invalidate();
                }
            }
        }
        #endregion

        #region 属性
        public List<WebVideoListItem> Items = new List<WebVideoListItem>();
        public YScroll chatVScroll;    //滚动条
        #endregion

        public bool Kdown(Keys KeyCode)
        {
            if (KeyCode == Keys.Down)
            {
                int seb = SelectIndex;
                doha(seb); return true;
            }
            else if (KeyCode == Keys.Up)
            {
                int seb = SelectIndex;
                upha(seb); return true;
            }
            else if (KeyCode == Keys.Enter)
            {
                WebVideoListItem TitleItem = Items[SelectIndex];
                if (TitleItem != null)
                {
                    SelectItem = TitleItem;
                    if (DownClick != null)
                    {
                        DownClick(TitleItem);
                    }
                    return true;
                }
            }
            return false;
        }
        void upha(int seb)
        {
            int bbqq = seb -= 1;
            try
            {
                WebVideoListItem TitleItem = Items[bbqq];
                if (TitleItem != null)
                {
                    seb = bbqq;
                    SelectIndex = TitleItem.Index;
                    SetSv(TitleItem);


                    foreach (WebVideoListItem TitleItems in Items)
                    {
                        if (TitleItems.Select)
                        {
                            TitleItems.Select = false;
                            Tom(TitleItems);
                        }
                    }
                    if (!TitleItem.Select)
                    {
                        TitleItem.Select = true;
                        Tom(TitleItem);
                    }
                }
            }
            catch { }
        }
        void doha(int seb)
        {
            int bbqq = seb += 1;
            try
            {
                WebVideoListItem TitleItem = Items[bbqq];
                if (TitleItem != null)
                {

                    seb = bbqq;
                    SelectIndex = TitleItem.Index;

                    SetSv(TitleItem);
                    foreach (WebVideoListItem TitleItems in Items)
                    {
                        if (TitleItems.Select)
                        {
                            TitleItems.Select = false;
                            Tom(TitleItems);
                        }
                    }
                    if (!TitleItem.Select)
                    {
                        TitleItem.Select = true;
                        Tom(TitleItem);
                    }
                }
            }
            catch { }
        }
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
            Kdown(e.KeyCode);
            //base.OnPreviewKeyDown(e);return;
            base.OnPreviewKeyDown(e);
        }
        public void SetSv(WebVideoListItem TitleItem)
        {
            if (chatVScroll.ShouldBeDraw)
            {
                if (chatVScroll.Value + Height - 50 < TitleItem.Bounds.Y)
                {
                    chatVScroll.Value = TitleItem.Bounds.Y;
                }
                else if (chatVScroll.Value > TitleItem.Bounds.Y)
                {
                    chatVScroll.Value = TitleItem.Bounds.Y - 150;
                }
            }
        }
        public void SetSvs(WebVideoListItem TitleItem)
        {
            if (chatVScroll.ShouldBeDraw)
            {
                if (chatVScroll.Value + Height < TitleItem.Bounds.Y + 100)
                {
                    chatVScroll.Value += 5;
                }
                else if (chatVScroll.Value > TitleItem.Bounds.Y - 100)
                {
                    chatVScroll.Value -= 5;
                }
            }
        }

        int Tops = 0;
        public void InPaint()
        {
            Tops = 0;

            if (Items.Count > 0 && Width > 0 && Height > 0)
            {
                int width = Width;
                int MaxHeight = 0;
                int windth_zi = (int)Math.Floor((width * 1.0) / 212);
                int index = 0;
                int _LRWidth = (width - windth_zi * 212) / 2;
                int UseWidth = _LRWidth;
                for (int i = 0; i < Items.Count; i++)
                {
                    WebVideoListItem it = Items[i];
                    it.Index = i;

                    if (index == windth_zi)
                    {
                        index = 0;
                        UseWidth = _LRWidth;
                        Tops += MaxHeight + 6;
                        MaxHeight = 0;
                    }

                    index++;


                    if (it.Img == null)
                    {
                        it.Bounds = new Rectangle(UseWidth + ((212 - 180) / 2), Tops, 180, 220);
                        MaxHeight = Math.Max(MaxHeight, 220);
                    }
                    else
                    {
                        #region 转换图片大小
                        if (it.ImgSize.Height == 0)
                        {
                            Size size = it.Img.Size;
                            double dbl = (size.Width * 1.0) / (size.Height * 1.0);
                            int _width = (int)size.Width, _height = (int)size.Height;
                            int swidth = _width, sheight = _height;
                            if (_width == _height)
                            {
                                //正方形
                                if (_width > 200)
                                {
                                    //图片过大
                                    swidth = sheight = 200;
                                }
                                else
                                {
                                    swidth = sheight = _width;
                                }
                            }
                            else if (_width > _height)
                            {
                                //横向图
                                if (_width > 200)
                                {
                                    //width = destWidth;
                                    //height = (destWidth * sourHeight) / sourWidth;
                                    //图片过大
                                    swidth = 200;
                                    sheight = (int)((double)200 / dbl);
                                }
                            }
                            else
                            {
                                //纵向图
                                if (_height > 200)
                                {
                                    //图片过大
                                    sheight = 200;
                                    swidth = (int)((double)200 * dbl);
                                }
                            }
                            if (swidth > 0 && sheight > 0)
                            {
                                it.ImgSize = new Size(swidth, sheight);
                            }
                        }
                        #endregion

                        it.Bounds = new Rectangle(UseWidth + ((212 - it.ImgSize.Width) / 2), Tops, it.ImgSize.Width, it.ImgSize.Height);
                        MaxHeight = Math.Max(MaxHeight, it.ImgSize.Height);
                    }

                    it.NameBound = new Rectangle(it.Bounds.X, it.Bounds.Y + it.Bounds.Height - 20, it.Bounds.Width, 20);

                    it.IsRender = true;

                    UseWidth += 212;

                }
                if (MaxHeight > 0)
                {
                    Tops += MaxHeight + 6;
                }
                //Invalidate(chatVScroll.Bounds);
            }
            chatVScroll.VirtualHeight = Tops;//绘制完成计算虚拟高度决定是否绘制滚动条
        }

        public int SelectIndex = -1;
        protected override void OnSizeChanged(EventArgs e)
        {
            InPaint();
            base.OnSizeChanged(e);
        }
        public SolidBrush FontColor1 = new SolidBrush(Color.White);
        public SolidBrush FontColor2 = new SolidBrush(Color.Black);
        public SolidBrush solidBrush = new SolidBrush(Color.FromArgb(60, 0, 0, 0));
        public SolidBrush solidBrush3 = new SolidBrush(Color.FromArgb(180, 0, 0, 0));
        public SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(200, 255, 255, 255));

        //SolidBrush CheckBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));

        //SolidBrush yllosolidBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 0));

        StringFormat _StringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
        StringFormat _StringFormatC = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
        Pen pen = new Pen(Color.DodgerBlue, 2);
        Font on = new Font("微软雅黑", 10);
        Font onA = FontAwesome.GetFont(30);

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g_main = e.Graphics;
            g_main.SmoothingMode = SmoothingMode.HighQuality;//使绘图质量最高，即消除锯齿
            g_main.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g_main.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

            Rectangle rect = this.ClientRectangle;
            using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height + dimHeight))
            {
                int last = chatVScroll.Bounds.Y + chatVScroll.Value;
                int first = last - (300 + dimHeight);

                List<WebVideoListItem> _Items = Items.FindAll(db => (db.Bounds.Y > first && db.Bounds.Y - last < rect.Height));

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.TranslateTransform(0, -(chatVScroll.Value - dimHeight));//根据滚动条的值设置坐标偏移
                    g.SmoothingMode = SmoothingMode.HighQuality;//使绘图质量最高，即消除锯齿
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
                    foreach (WebVideoListItem it in _Items)
                    {
                        if (it.IsRender)
                        {
                            if (it.Img != null)
                            {
                                g.DrawImage(it.Img, it.Bounds);
                            }
                            else
                            {
                                g.FillRectangle(FontColor1, it.Bounds);
                                g.DrawString(FontAwesome.GetUnicode("51BC"), onA, FontColor2, it.Bounds, _StringFormatC);
                            }
                            g.FillRectangle(solidBrush3, it.NameBound);
                            g.DrawString(it.Name, on, FontColor1, it.NameBound, _StringFormat);

                            if (it.MouseHover)
                            {
                                g.FillRectangle(solidBrush, it.Bounds);
                                g.DrawString(FontAwesome.GetUnicode("518F"), onA, FontColor1, it.Bounds, _StringFormatC);
                            }

                            if (it.Select)
                            {
                                g.DrawRectangle(pen, it.Bound);
                                //g.FillRectangle(yllosolidBrush, it.Bounds);
                            }
                        }
                    }
                    //g.ResetTransform();//重置坐标系
                }
                //if (bitmapTop != null) {
                //    bitmapTop.Dispose();
                //}


                if (dimHeight > 0 && TopChange != null && _Items.Count > 0)
                {
                    Bitmap bitmapTop = new Bitmap(rect.Width, dimHeight);

                    Rectangle rectTop = new Rectangle(0, 0, rect.Width, dimHeight);
                    using (Graphics gTop = Graphics.FromImage(bitmapTop))
                    {
                        //gTop.SmoothingMode = SmoothingMode.AntiAlias;//使绘图质量最高，即消除锯齿
                        //gTop.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        ////g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        //gTop.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量

                        gTop.DrawImage(bitmap, rectTop, rectTop, GraphicsUnit.Pixel);
                    }
                    //if (Bmp != null)
                    //{
                    //    Bmp.Dispose();
                    //    Marshal.FreeHGlobal(ImageCopyPointer);
                    //}


                    Rectangle Rect = new Rectangle(0, 0, bitmapTop.Width, bitmapTop.Height);
                    bitmapTop.GaussianBlur(ref Rect, 80, false);

                    using (Graphics gTop = Graphics.FromImage(bitmapTop))
                    {
                        gTop.FillRectangle(solidBrush2, rectTop);
                    }
                    //Bmp.UsmSharpen(ref Rect, BarRadius.Value, BarAmount.Value);
                    TopChange(bitmapTop);


                }

                g_main.DrawImage(bitmap, rect, new Rectangle(0, dimHeight, rect.Width, rect.Height), GraphicsUnit.Pixel);
                //g_main.DrawImage(bitmap, rect, new Rectangle(0, 0, rect.Width, rect.Height + 40), GraphicsUnit.Pixel);

                if (chatVScroll.ShouldBeDraw)//是否绘制滚动条
                    chatVScroll.ReDrawScroll(g_main);
            }

            base.OnPaint(e);
        }

        public event DownEventHandler DownClick;
        public delegate void DownEventHandler(WebVideoListItem Item);

        public delegate void TopChangeEventHandler(Bitmap bmp);
        public event TopChangeEventHandler TopChange;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.Focus();
            if (e.Delta > 0) chatVScroll.Value -= 50;
            if (e.Delta < 0) chatVScroll.Value += 50;
            base.OnMouseWheel(e);
        }
        Point old;
        public void Tom(WebVideoListItem its)
        {
            Rectangle rect = its.Bounds;
            int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
            rect.Y -= tom;
            this.Invalidate(rect);
        }


        bool IsMouseDown = false;
        int MouseDownY = -1;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            Point m_ptMousePos = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                MouseDownY = e.Y;
                IsMouseDown = true;
                old = m_ptMousePos;

                if (chatVScroll.Bounds.Contains(m_ptMousePos))
                {
                    chatVScroll.MouseDownY = MouseDownY;
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
                            WebVideoListItem it = Items[i];

                            Rectangle rect = it.Bounds;
                            int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
                            rect.Y -= tom;
                            if (rect.Contains(m_ptMousePos))
                            {
                                SelectIndex = i;
                                SelectItem = it;
                                break;
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
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                chatVScroll.Value -= 50;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Point m_ptMousePos = e.Location;
            //if (e.Button == MouseButtons.Left)
            //    chatVScroll.IsMouseDown = false;
            //base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                MouseDownY = -1;
                IsMouseDown = false;
                if (chatVScroll.IsMouseDown)
                {
                    chatVScroll.IsMouseDown = false;
                    //如果有滚动条 判断是否在滚动条类点击
                    if (chatVScroll.Bounds.Contains(m_ptMousePos))
                    {
                        //判断在滚动条那个位置点击
                        if (!chatVScroll.SliderBounds.Contains(m_ptMousePos))
                            chatVScroll.MoveSliderToLocation(m_ptMousePos.Y);
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
                            WebVideoListItem it = Items[i];

                            Rectangle rect = it.Bounds;
                            int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
                            rect.Y -= tom;
                            if (rect.Contains(m_ptMousePos))
                            {
                                SelectIndex = i;
                                SelectItem = it;
                                if (!it.Select)
                                {
                                    it.Select = true;
                                    Invalidate(rect);
                                }
                                if (DownClick != null)
                                { DownClick(it); }
                                //break;
                            }
                            else
                            {
                                if (it.Select)
                                {
                                    it.Select = false;
                                    Invalidate(rect);
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
                }
                else
                {
                    base.OnMouseUp(e);
                }
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
                chatVScroll.MoveSliderFromLocation(e.Y);
                return;
            }
            else
            {
                if (m_ptMousePos.X != old.X && m_ptMousePos.Y != old.Y && IsMouseDown)
                {
                    chatVScroll.MouseDownY = MouseDownY;
                    chatVScroll.IsMouseDown = true;
                }
                else
                {
                    try
                    {
                        foreach (WebVideoListItem its in Items)
                        {
                            Rectangle rect = its.Bounds;
                            int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
                            rect.Y -= tom;
                            if (rect.Contains(m_ptMousePos))
                            {
                                int count = 0;
                                if (!its.MouseHover)
                                {
                                    count++;
                                    its.MouseHover = true;
                                }

                                if (count > 0)
                                {
                                    this.Invalidate(rect);
                                }
                            }
                            else
                            {
                                int count = 0;
                                if (its.MouseHover)
                                {
                                    count++;
                                    its.MouseHover = false;
                                }
                                if (count > 0)
                                {
                                    this.Invalidate(rect);
                                }
                            }

                        }
                    }
                    catch { }
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
                    if (chatVScroll.UpBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnUp = true;
                    else
                        chatVScroll.IsMouseOnUp = false;
                    if (chatVScroll.DownBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnDown = true;
                    else
                        chatVScroll.IsMouseOnDown = false;
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
            try
            {
                foreach (WebVideoListItem its in Items)
                {

                    if (its.MouseHover)
                    {
                        Rectangle rect = its.Bounds;
                        int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
                        rect.Y -= tom;
                        its.MouseHover = false;
                        this.Invalidate(rect);
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
                foreach (WebVideoListItem it in Items)
                {

                    if (it.MouseHover)
                    {
                        Rectangle rect = it.Bounds;
                        int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
                        rect.Y -= tom;
                        it.MouseHover = false;
                        this.Invalidate(rect);
                    }

                }
            }
            catch { }
            chatVScroll.ClearAllMouseOn();
            base.OnLeave(e);
        }

        public WebVideoListItem SelectItem { get; set; }
    }


    public class WebVideoListItem
    {
        public bool MouseHover { set; get; }
        public Rectangle NameBound { set; get; }

        Rectangle _Bounds;
        public Rectangle Bounds
        {
            set
            {
                _Bounds = value;
                Bound = new Rectangle(value.X + 1, value.Y + 1, value.Width - 2, value.Height - 2);
            }
            get
            {
                return _Bounds;
            }
        }
        public Rectangle Bound { set; get; }

        public Image Img { get; set; }
        public string Name { get; set; }
        //public bool Check { set; get; }
        public object Tag { set; get; }
        public int Index { set; get; }
        public bool Select { set; get; }
        public bool IsRender { set; get; }
        public Size ImgSize { get; set; }
    }
}
