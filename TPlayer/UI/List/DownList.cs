using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TSkin;

namespace TPlayerList
{
    public partial class DownList : Control
    {
        public DownList()
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
        public List<DownListItem> Items = new List<DownListItem>();
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
            else if (KeyCode == Keys.Enter || KeyCode == Keys.Space)
            {
                DownListItem TitleItem = Items[SelectIndex];
                if (TitleItem != null)
                {
                    SelectItem = TitleItem;
                    if (TitleItem.Visible)
                    {
                        if (DownClick != null)
                        {
                            DownClick(TitleItem);
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        void upha(int seb)
        {
            int bbqq = seb -= 1; try
            {
                DownListItem TitleItem = Items[bbqq];
                while (TitleItem != null)
                {
                    if (TitleItem.Visible)
                    {
                        seb = bbqq;
                        SelectIndex = TitleItem.Index;
                        SetSv(TitleItem);


                        foreach (DownListItem TitleItems in Items)
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
                        break;
                    }
                    bbqq--;
                    TitleItem = Items[bbqq];
                }
            }
            catch { }
        }
        void doha(int seb)
        {
            int bbqq = seb += 1;
            try
            {
                DownListItem TitleItem = Items[bbqq];
                while (TitleItem != null)
                {
                    if (TitleItem.Visible)
                    {
                        seb = bbqq;
                        SelectIndex = TitleItem.Index;

                        SetSv(TitleItem);
                        foreach (DownListItem TitleItems in Items)
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
                        break;
                    }
                    bbqq++;
                    TitleItem = Items[bbqq];
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
        public void SetSv(DownListItem TitleItem)
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
        public void SetSvs(DownListItem TitleItem)
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
        int _PlayIndex = -1;
        public int PlayIndex
        {
            get { return _PlayIndex; }
            set
            {
                if (_PlayIndex != value)
                {
                    _PlayIndex = value;
                    InPaint();
                    Invalidate();
                }
            }
        }
        bool _Playing = false;
        public bool Playing
        {
            get { return _Playing; }
            set
            {
                if (_Playing != value)
                {
                    _Playing = value;
                    if (_PlayIndex > -1)
                    {
                        try
                        {
                            Tom(Items[_PlayIndex]);
                        }
                        catch { }
                    }
                    //InPaint();
                    //Invalidate();
                }
            }
        }
        int Tops = 0;
        public void InPaint()
        {
            if (Width > 0 && Height > 0)
            {
                Tops = 0;
                try
                {
                    foreach (DownListItem it in Items)
                    {
                        if (it.Visible)
                        {
                            if (string.IsNullOrEmpty(it.Desc))
                            {
                                int hehe = 38;
                                it.Bounds = new Rectangle(0, Tops, Width, hehe);
                                it.Bound = new Rectangle(it.Bounds.X + 1, it.Bounds.Y + 1, it.Bounds.Width - 2, it.Bounds.Height - 2);
                                it.CheckBounds = new Rectangle(it.Bounds.X + (hehe - 20) / 2, Tops + (hehe - 20) / 2, 24, 24);

                                int width = Width;
                                if (_PlayIndex == it.Index)
                                {
                                    width -= (hehe * 2) + 10;
                                    it.Btn1Bounds = new Rectangle(it.Bounds.X + it.Bounds.Width - ((hehe * 2) + 10), Tops, hehe, hehe);
                                    it.Btn2Bounds = new Rectangle(it.Bounds.X + it.Bounds.Width - (hehe + 10), Tops, hehe, hehe);
                                }
                                else
                                {
                                    it.Btn2Bounds = it.Btn1Bounds = new Rectangle(0, 0, 0, 0);
                                }
                                if (_IsCheck)
                                {
                                    it.NameBound = new Rectangle(38, Tops, width - 38, hehe);
                                }
                                else
                                {
                                    it.NameBound = new Rectangle(6, Tops, width - 6, hehe);
                                }
                                Tops += hehe;
                            }
                            else
                            {
                                int hehe = 50;
                                it.Bounds = new Rectangle(0, Tops, Width, hehe);
                                it.Bound = new Rectangle(it.Bounds.X + 1, it.Bounds.Y + 1, it.Bounds.Width - 2, it.Bounds.Height - 2);
                                it.CheckBounds = new Rectangle(it.Bounds.X + (38 - 20) / 2, Tops + (38 - 20) / 2, 24, 24);

                                int width = Width;
                                if (_PlayIndex == it.Index)
                                {
                                    width -= (hehe * 2) + 10;
                                    it.Btn1Bounds = new Rectangle(it.Bounds.X + it.Bounds.Width - ((hehe * 2) + 10), Tops, hehe, hehe);
                                    it.Btn2Bounds = new Rectangle(it.Bounds.X + it.Bounds.Width - (hehe + 10), Tops, hehe, hehe);

                                }
                                else
                                {
                                    it.Btn2Bounds = it.Btn1Bounds = new Rectangle(0, 0, 0, 0);
                                }
                                if (_IsCheck)
                                {
                                    it.NameBound = new Rectangle(38, Tops, width - 38, 28);
                                    it.DescBound = new Rectangle(38, Tops + it.NameBound.Height, it.NameBound.Width, 22);
                                }
                                else
                                {
                                    it.NameBound = new Rectangle(6, Tops, width - 6, 28);
                                    it.DescBound = new Rectangle(6, Tops + it.NameBound.Height, width - 6, 22);
                                }
                                Tops += hehe;
                            }
                        }
                    }
                    chatVScroll.VirtualHeight = Tops;//绘制完成计算虚拟高度决定是否绘制滚动条
                                                     //Invalidate(chatVScroll.Bounds);
                }
                catch { }
            }
        }

        public int SelectIndex = -1;
        protected override void OnSizeChanged(EventArgs e)
        {
            this.InPaint();
            base.OnSizeChanged(e);
        }
        public SolidBrush FontColor1 = new SolidBrush(Color.White);
        public SolidBrush FontColor2 = new SolidBrush(Color.Gray);
        public SolidBrush solidBrush = new SolidBrush(Color.FromArgb(20, 255, 255, 255));

        public SolidBrush solidBrushOk = new SolidBrush(Color.FromArgb(31, 208, 165));

        //SolidBrush CheckBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));

        //SolidBrush yllosolidBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 0));

        StringFormat _StringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
        StringFormat _StringFormatC = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
        Pen pen = new Pen(Color.Black, 2);
        Font on = new Font("微软雅黑", 10);
        Font on2 = new Font("微软雅黑", 9);
        Font onA = TPlayer.FontAwesome.GetFont(26);
        Font onAS1 = TPlayer.FontAwesome.GetFont(18);

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(0, -chatVScroll.Value);        //根据滚动条的值设置坐标偏移
            g.SmoothingMode = SmoothingMode.HighQuality;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            int toms = chatVScroll.Bounds.Y + chatVScroll.Value;
            int tom = toms - 30;

            List<DownListItem> _Items = Items.FindAll(db => db.Visible && (db.Bounds.Y > tom && db.Bounds.Y - toms < Height));
            try
            {
                foreach (DownListItem it in _Items)
                {
                    if (_IsCheck)
                    {
                        g.DrawString(TPlayer.FontAwesome.GetUnicode(it.Check ? 20282 : 20283), onAS1, FontColor1, it.CheckBounds, _StringFormatC);
                    }

                    if (it.MouseHover)
                    {
                        g.FillRectangle(solidBrush, it.Bounds);
                    }
                    if (_PlayIndex == it.Index)
                    {
                        if (it.Btn1Bounds.X > 0 && it.Btn2Bounds.X > 0)
                        {
                            g.DrawString(TPlayer.FontAwesome.GetUnicode(_Playing ? "4FC4" : "4FBE"), onA, it.MouseHover1 ? solidBrushOk : FontColor1, it.Btn1Bounds, _StringFormatC);
                            g.DrawString(TPlayer.FontAwesome.GetUnicode("4FFC"), onA, it.MouseHover2 ? solidBrushOk : FontColor1, it.Btn2Bounds, _StringFormatC);
                        }

                        g.DrawString(it.Name, on, solidBrushOk, it.NameBound, _StringFormat);
                        g.DrawString(it.Desc, on2, solidBrushOk, it.DescBound, _StringFormat);
                    }
                    else
                    {
                        if (it.Enabled)
                        {
                            //g.DrawString(TPlayer.FontAwesome.GetUnicode(20230), onA, FontColor1, it.NameBound, _StringFormat);
                            //20414 播放
                            //20420 暂停
                            //20416 菜单
                            g.DrawString(it.Name, on, FontColor1, it.NameBound, _StringFormat);
                            g.DrawString(it.Desc, on2, FontColor2, it.DescBound, _StringFormat);
                        }
                        else
                        {
                            g.DrawString(it.Name, on, FontColor2, it.NameBound, _StringFormat);
                            g.DrawString(it.Desc, on2, FontColor2, it.DescBound, _StringFormat);
                        }
                    }
                    if (it.Select)
                    {
                        g.DrawRectangle(pen, it.Bound);
                        //g.FillRectangle(yllosolidBrush, it.Bounds);
                    }
                }
                g.ResetTransform();             //重置坐标系
                if (chatVScroll.ShouldBeDraw)   //是否绘制滚动条
                    chatVScroll.ReDrawScroll(g);
            }
            catch { }

            base.OnPaint(e);
        }
        public event DownEventHandler PlayClick;
        public event DownEventHandler DownClick;
        public event DownEventHandler MoreClick;
        public delegate void DownEventHandler(DownListItem Item);

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.Focus();
            if (e.Delta > 0) chatVScroll.Value -= 50;
            if (e.Delta < 0) chatVScroll.Value += 50;
            base.OnMouseWheel(e);
        }
        Point old;
        public void Tom(DownListItem its)
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
                    chatVScroll.IsMouseDown = true;
                    chatVScroll.MouseDownY = e.Y;
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
                            DownListItem it = Items[i];

                            if (it.Visible)
                            {
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
                else if (m_ptMousePos.X == old.X && m_ptMousePos.Y == old.Y)
                {
                    if (Items.Count > 0)
                    {
                        SelectItem = null;

                        for (int i = 0; i < Items.Count; i++)
                        {
                            DownListItem it = Items[i];
                            if (it.Visible)
                            {
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

                                    Rectangle rect1 = it.Btn1Bounds;
                                    rect1.Y -= tom;
                                    if (rect1.Contains(m_ptMousePos))
                                    {
                                        if (PlayClick != null)
                                        { PlayClick(it); }
                                    }
                                    else
                                    {
                                        Rectangle rect2 = it.Btn2Bounds;
                                        rect2.Y -= tom;
                                        if (rect2.Contains(m_ptMousePos))
                                        {
                                            if (MoreClick != null)
                                            { MoreClick(it); }
                                        }
                                        else
                                        {
                                            if (DownClick != null)
                                            { DownClick(it); }
                                        }
                                    }

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
                        foreach (DownListItem its in Items)
                        {
                            if (its.Visible)
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

                                    Rectangle rect1 = its.Btn1Bounds;
                                    rect1.Y -= tom;
                                    if (rect1.Contains(m_ptMousePos))
                                    {
                                        if (its.MouseHover2)
                                        {
                                            its.MouseHover2 = false;
                                            count++;
                                        }
                                        if (!its.MouseHover1)
                                        {
                                            its.MouseHover1 = true;
                                            count++;
                                        }
                                    }
                                    else
                                    {
                                        Rectangle rect2 = its.Btn2Bounds;
                                        rect2.Y -= tom;
                                        if (rect2.Contains(m_ptMousePos))
                                        {
                                            if (its.MouseHover1)
                                            {
                                                its.MouseHover1 = false;
                                                count++;
                                            }
                                            if (!its.MouseHover2)
                                            {
                                                its.MouseHover2 = true;
                                                count++;
                                            }
                                        }
                                        else
                                        {
                                            if (its.MouseHover2)
                                            {
                                                its.MouseHover2 = false;
                                                count++;
                                            }
                                            if (its.MouseHover1)
                                            {
                                                its.MouseHover1 = false;
                                                count++;
                                            }
                                        }
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
                                    if (its.MouseHover1)
                                    {
                                        count++;
                                        its.MouseHover1 = false;
                                    }
                                    if (its.MouseHover2)
                                    {
                                        count++;
                                        its.MouseHover2 = false;
                                    }
                                    if (count > 0)
                                    {
                                        this.Invalidate(rect);
                                    }
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
                foreach (DownListItem its in Items)
                {
                    if (its.Visible)
                    {
                        if (its.MouseHover)
                        {
                            Rectangle rect = its.Bounds;
                            int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
                            rect.Y -= tom;
                            its.MouseHover = false;
                            its.MouseHover1 = false;
                            its.MouseHover2 = false;
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
                foreach (DownListItem it in Items)
                {
                    if (it.Visible)
                    {
                        if (it.MouseHover)
                        {
                            Rectangle rect = it.Bounds;
                            int tom = chatVScroll.Bounds.Y + chatVScroll.Value;
                            rect.Y -= tom;
                            it.MouseHover = false;
                            it.MouseHover1 = false;
                            it.MouseHover2 = false;
                            this.Invalidate(rect);
                        }
                    }
                }
            }
            catch { }
            chatVScroll.ClearAllMouseOn();
            base.OnLeave(e);
        }

        public DownListItem SelectItem { get; set; }
    }
    
    public class DownListItem
    {
        public bool MouseHover { set; get; }
        public Rectangle NameBound { set; get; }
        public Rectangle DescBound { set; get; }
        public Rectangle Bounds { set; get; }
        public Rectangle Bound { set; get; }

        public string Name { get; set; }
        public string Desc { get; set; }

        public bool Visible { set; get; }
        public Rectangle CheckBounds { set; get; }

        public bool MouseHover1 { set; get; }
        public bool MouseHover2 { set; get; }
        public Rectangle Btn1Bounds { set; get; }
        public Rectangle Btn2Bounds { set; get; }
        public bool Check { set; get; }
        public bool Enabled { set; get; }
        //public bool Check { set; get; }
        public object Tag { set; get; }
        public int Index { set; get; }
        public bool Select { set; get; }
    }
}
