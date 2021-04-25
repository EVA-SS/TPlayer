using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TPlayerSupport;
using TSkin;

namespace TPlayer
{
    public partial class Loading : Form
    {
        TPlayer player;
        public Loading(TPlayer player)
        {
            this.player = player;
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            //s.BringToFront();
            CanPenetrate();
            player.LocationChanged += Main;
            player.SizeChanged += Main;
        }
        void Main(object sender, EventArgs e)
        {
            this.Location = new Point((player.Left + player.Width / 2) - 150, (player.Top + player.Height / 2) - 40);
        }
        private string _Tilie = "加载中";
        public string Titite
        {
            set
            {
                if (value != _Tilie)
                {
                    _Tilie = value;
                }
            }
            get { return _Tilie; }
        }
        #region 样式
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            txtrect.Width = Width;
        }

        Font newFont = new Font("微软雅黑", 13);
        SolidBrush solidBrush = new SolidBrush(Color.White);
        StringFormat stringFormat = new StringFormat { Alignment = StringAlignment.Center };
        Rectangle ju = new Rectangle(125, 5, 50, 50);//80

        Rectangle txtrect = new Rectangle(0, 60, 0, 25);
        #endregion

        #region 动画

        public bool Stats = true;
        int jh = 2;
        int jhs = 0;

        private void GG()
        {
            bool isRun = true;
            while (isRun)
            {
                try
                {
                    Invoke(new Action(() =>
                    {
                        Print();
                    }));
                    jhs += 6;
                    if (jhs >= 360)
                    { jhs = 0; }
                    if (Stats)
                    {
                        if (jh >= 94)
                        { Stats = false; }
                        else
                        {
                            jh += 2;
                        }
                    }
                    else
                    {
                        if (jh <= 6)
                        { Stats = true; }
                        else
                        {
                            jh -= 4; jhs += 12;
                        }
                    }
                    Thread.Sleep(20);
                }
                catch { isRun = false; }
            }
        }
        Thread Tim = null;
        void TimCO(bool s)
        {
            if (s)
            {
                Tim = new Thread(GG);
                //Tim.Priority = ThreadPriority.Highest;
                Tim.IsBackground = true;
                Tim.Start();
            }
            else
            {
                if (Tim != null)
                {
                    try
                    {
                        Tim.Abort(); Tim = null;
                    }
                    catch { }
                }
            }
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

        Bitmap PrintBit(Rectangle original_rect)
        {
            Bitmap original_bmp = new Bitmap(original_rect.Width, original_rect.Height);
            using (Graphics g = Graphics.FromImage(original_bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                if (Tim != null)
                {
                    using (Pen p = new Pen(new SolidBrush(Color.White), 4))
                    {
                        p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                        g.DrawArc(p, ju, jhs, (float)(jh * 3.6));
                    }
                }
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //g.DrawString(kee, newFont, new SolidBrush(Color.White), new Point(0,30));
                g.DrawString(Titite, newFont, solidBrush, txtrect, stringFormat);
            }

            return original_bmp;
        }

        #endregion

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

        protected override void OnLoad(EventArgs e)
        {
            MaximumSize = MinimumSize = Size = new Size(300, 80);
            base.OnLoad(e);
            Main(this, e);
            TimCO(true);
        }

        long UpByteStr = 0;
        protected override void OnClosing(CancelEventArgs e)
        {
            player.LocationChanged -= Main;
            player.SizeChanged -= Main;
            player._loading = null;
            timBk.Enabled = false; timBk.Dispose();
            _Tilie = "即将打开";
            TimCO(false);
            base.OnClosing(e);
        }


        private void timBk_Tick(object sender, EventArgs e)
        {
            try
            {
                //通过参数 GetConfig(29) 获取 APlayer 的读取字节数，根据下式计算出下载速度：
                //下载速度（KB/S） = （ 第二次获取的字节数 -  第一次获取的字节数）/ 1024 /  两次获取的隔秒数
                //如果要换成 Kbps（比特率），则结果应该再乘以 8。
                if (!player.isDownloadCodec )
                {
                    long bytestr = player.player.GetConfig(29).ToLong();
                    if (bytestr > 0)
                    {
                        double kb = bytestr - UpByteStr;
                        _Tilie = kb.CountSize() + " /S";
                        UpByteStr = bytestr;
                    }
                }
            }
            catch
            {
                timBk.Enabled = false; timBk.Dispose();
            }
        }
    }
}
