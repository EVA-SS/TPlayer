using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using TSkin;

namespace TPlayer
{
    public partial class Prompt : Form
    {
        IPrompt prompt = null;

        #region 初始化
        public Prompt(IPrompt prompt, string left, string middle = null, string right = null, string file = null, bool autoCloes = true)
        {
            this.prompt = prompt;
            this.left = left;
            this.middle = middle;
            this.right = right;
            this.file = file;
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            UpdateStyles();
            InitializeComponent();
            this.autoCloes = autoCloes;
            if (string.IsNullOrEmpty(file))
            {
                CanPenetrate();
            }
        }

        #region 无焦点窗体透明窗口渲染
        /// <summary>
        /// 使窗口有鼠标穿透功能
        /// </summary>
        void CanPenetrate()
        {
            int intExTemp = Win32.GetWindowLong(this.Handle, Win32.GWL_EXSTYLE);
            int oldGWLEx = Win32.SetWindowLong(this.Handle, Win32.GWL_EXSTYLE, Win32.WS_EX_TRANSPARENT | Win32.WS_EX_LAYERED);
        }
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

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x21)
            {
                m.Result = new IntPtr(0x03);
                return;
            }
            base.WndProc(ref m);
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        #endregion
        private void Player_LocationSizeChanged(object sender, EventArgs e)
        {
            this.Location = new Point(prompt.iForm.Left + 10, prompt.iForm.Top + (prompt.iForm.Height - this.Height - 130));
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!string.IsNullOrEmpty(file))
            {
                if (File.Exists(file))
                {
                    file.OpenExplorer();
                }
                else
                {
                    System.Diagnostics.Process.Start(file);
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            SetWith();
            Player_LocationSizeChanged(this, e);
            prompt.iForm.LocationChanged += Player_LocationSizeChanged;
            prompt.iForm.SizeChanged += Player_LocationSizeChanged;
            base.OnLoad(e);
            Print();
            //SetSize();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            prompt.iPrompt = null;
            prompt.iForm.LocationChanged -= Player_LocationSizeChanged;
            prompt.iForm.SizeChanged -= Player_LocationSizeChanged;
            base.OnClosing(e);
        }
        #endregion

        #region 属性

        bool _autoCloes = false;
        public bool autoCloes
        {
            get { return _autoCloes; }
            set
            {
                if (_autoCloes != value)
                {
                    _autoCloes = value;
                    timer1.Enabled = _autoCloes;
                }
            }
        }
        string file = null;

        public void Change(string left, string middle = null, string right = null)
        {
            int count = 0;
            if (this.right != right)
            {
                this.right = right;
                count++;
            }
            if (this.middle != middle)
            {
                this.middle = middle;
                count++;
            }
            if (this.left != left)
            {
                this.left = left;
                count++;
            }
            if (count > 0)
            {
                timer1.Enabled = false;
                SetWith();
                this.Invoke(new Action(() =>
                {
                    Print();
                }));
                timer1.Enabled = _autoCloes;
            }
        }

        string right = null;
        string middle = null;
        string left = null;
        Rectangle Text1_rect = new Rectangle(8, 0, 0, 0), Text2_rect = new Rectangle(0, 0, 0, 0), Text3_rect = new Rectangle(0, 0, 0, 0);

        int split = 2;
        void SetWith()
        {
            Size sizeF1 = GetTextSize(left);

            Text1_rect.Width = sizeF1.Width;

            int hasWidth = Text1_rect.X + sizeF1.Width;

            if (!string.IsNullOrEmpty(middle))
            {
                Text2_rect.X = hasWidth + split;
                Size sizeF2 = GetTextSize(middle);
                Text2_rect.Width = sizeF2.Width;
                hasWidth += sizeF2.Width + split;
            }
            if (!string.IsNullOrEmpty(right))
            {
                Text3_rect.X = hasWidth + split;

                Size sizeF3 = GetTextSize(right);
                Text3_rect.Width = sizeF3.Width;

                hasWidth += Text3_rect.Width + split;
            }

            this.Size = new Size(hasWidth + Text1_rect.X, sizeF1.Height + 8);
        }
        Size GetTextSize(string str)
        {
            Size size = TextRenderer.MeasureText(str, font);
            size.Width += 4;
            size.Height += 2;
            return size;
        }

        Rectangle rectmF;
        protected override void OnSizeChanged(EventArgs e)
        {
            Rectangle rect = ClientRectangle;
            rectmF = new Rectangle(0, 0, rect.Width - 1, rect.Height - 1);

            Text1_rect.Height = Text2_rect.Height = Text3_rect.Height = this.Height;
            base.OnSizeChanged(e);
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

        SolidBrush solidBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
        SolidBrush solidBrushs = new SolidBrush(Color.FromArgb(180, 255, 255, 255));
        SolidBrush solidBrushss = new SolidBrush(Color.White);
        Font font = new Font("微软雅黑", 11, FontStyle.Bold);
        StringFormat stringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
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
                using (GraphicsPath path = rectmF.CreateRoundedRectanglePath(this.Height))
                {
                    g.FillPath(solidBrush, path);
                }
                //g.FillRectangle(solidBrushs, Text1_rect);
                g.DrawString(left, font, solidBrushs, Text1_rect, stringFormat);
                if (!string.IsNullOrEmpty(middle))
                {
                    //g.FillRectangle(solidBrushss, Text2_rect);
                    g.DrawString(middle, font, solidBrushss, Text2_rect, stringFormat);
                }

                if (!string.IsNullOrEmpty(right))
                {
                    //g.FillRectangle(solidBrushs, Text3_rect);
                    g.DrawString(right, font, solidBrushs, Text3_rect, stringFormat);
                }
            }

            return original_bmp;
        }


        #region 属性

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #endregion
    }

    public interface IPrompt : IFormCore
    {
        Prompt iPrompt { get; set; }
    }
}
