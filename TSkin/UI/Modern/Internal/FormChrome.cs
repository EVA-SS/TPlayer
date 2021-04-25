﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetDimension.WinForm
{


    public abstract class FormChrome : Form
    {


        private const int designTimeDpi = 96;
        private int oldDpi;
        private int startupDpi;
        private int currentDpi;
        //private int lastChangedDpi = 0;

        private bool scaling = false;
        private bool isMoving = false;
        private bool shouldScale = false;

        private bool loaded;

        private Size minSizeState;
        private Size maxSizeState;
        private Stack<Dictionary<Control, AnchorStyles>> anchorsStack;

        private static readonly Point minimizedFormLocation = new Point(-32000, -32000);
        internal static readonly Point InvalidPoint = new Point(-10000, -10000);
        private Rectangle regionRect = Rectangle.Empty;

        private int isInitializing = 0;
        private bool shouldUpdateOnResumeLayout = false;
        private bool forceInitialized = false;
        //private static Padding? SavedBorders = null;

        private Size minimumClientSize;
        private Size maximumClientSize;
        private Size? minimumSize = null;
        private Size? maximumSize = null;

        private bool _activated;
        private bool _windowActive;
        private bool _trackingMouse;
        private bool _captured;
        //private bool _disposing;
        private static bool? isDesingerProcess = null;

        //private IntPtr _screenDC;

        private Color shadowColor = Color.Black;
        private Color borderColor = ColorTranslator.FromHtml("#1883D7");
        private Padding borders = new Padding(1, 1, 1, 1);
        private bool isEnterSizeMoveMode;

        private PROCESS_DPI_AWARENESS processDpiAwareness = PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
        private bool isPerMonitorAwareV2 = false;

        internal ChromeDecorator shadowDecorator;
        public void CloseShadow()
        {
            if (shadowDecorator != null)
                shadowDecorator.Dispose();
        }


        bool _Resizable = true;

        [Description("改变窗口大小"), Category("UI Settings"), DefaultValue(true)]
        public bool Resizable
        {
            get
            {
                return _Resizable;
            }
            set
            {
                _Resizable = value;
                if (shadowDecorator != null)
                    shadowDecorator.Resizable = value;
            }
        }

        #region Reflected

        private FieldInfo clientWidthField;
        private FieldInfo clientHeightField;
        private FieldInfo formStateSetClientSizeField;
        private FieldInfo formStateField;

        #endregion

        #region Win10 High DPI
        private const int PROCESS_QUERY_INFORMATION = 0x0400;
        private const int PROCESS_VM_READ = 0x0010;

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

        [DllImport("Kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr handle);

        private const int S_OK = 0;
        private enum PROCESS_DPI_AWARENESS
        {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_AWARE = 2
        }
        [DllImport("Shcore.dll")]
        private static extern int GetProcessDpiAwareness(IntPtr hprocess, out PROCESS_DPI_AWARENESS value);

        [DllImport("user32.dll")]
        internal static extern int GetAwarenessFromDpiAwarenessContext(IntPtr dpiContext);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowDpiAwarenessContext(IntPtr hWnd);

        private PROCESS_DPI_AWARENESS GetDpiState(uint processId)
        {
            try
            {
                IntPtr handle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, processId);
                if (handle != IntPtr.Zero)
                {
                    PROCESS_DPI_AWARENESS value;
                    int result = GetProcessDpiAwareness(handle, out value);
                    if (result == S_OK)
                    {
                        System.Diagnostics.Debug.Print(value.ToString());
                    }
                    CloseHandle(handle);
                    if (result != S_OK)
                    {
                        throw new Win32Exception(result);
                    }
                    return value;
                }
            }
            catch
            {

            }
            return PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
        }

        [DllImport("User32.dll")]
        private static extern IntPtr MonitorFromPoint([In] System.Drawing.Point pt, [In] uint dwFlags);

        //https://msdn.microsoft.com/en-us/library/windows/desktop/dn280510(v=vs.85).aspx
        [DllImport("Shcore.dll")]
        private static extern IntPtr GetDpiForMonitor([In] IntPtr hmonitor, [In] DpiType dpiType, [Out] out uint dpiX, [Out] out uint dpiY);
        private enum DpiType
        {
            Effective = 0,
            Angular = 1,
            Raw = 2,
        }

        private void GetDpiForScreen(System.Windows.Forms.Screen screen, DpiType dpiType, out uint dpiX, out uint dpiY)
        {
            try
            {
                var pnt = new System.Drawing.Point(screen.Bounds.Left + 1, screen.Bounds.Top + 1);
                var mon = MonitorFromPoint(pnt, 2/*MONITOR_DEFAULTTONEAREST*/);
                GetDpiForMonitor(mon, dpiType, out dpiX, out dpiY);
            }
            catch
            {

                Graphics g = this.CreateGraphics();

                try
                {
                    dpiX = (uint)g.DpiX;
                    dpiY = (uint)g.DpiY;
                }
                catch
                {
                    dpiX = 96;
                    dpiY = 96;
                }
                finally
                {
                    g.Dispose();
                }
            }
        }

        #endregion

        #region Properties
        /// <summary>
        /// Occurs when the active window setting changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler WindowActiveChanged;

        /// <summary>
        /// 设置或获取活动状态窗口投影颜色
        /// </summary>
        [Category("UI Settings")]
        [Description("")]
        public Color ShadowColor
        {
            get => shadowColor;
            set => shadowColor = shadowDecorator.ShadowColor = value;
        }

        [Category("UI Settings")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;

                InvalidateNonClient();
            }
        }

        [Category("UI Settings")]
        public Padding Borders
        {
            get
            {
                return borders;
            }
            set
            {
                borders = value;
                RecalcClientSize();
            }
        }
        #endregion

        #region States
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual Color InactiveBorderColor
        {
            get
            {
                var r = Convert.ToInt32(BorderColor.R * .7f);
                var g = Convert.ToInt32(BorderColor.G * .7f);
                var b = Convert.ToInt32(BorderColor.B * .7f);
                var color = Color.FromArgb(255, r, g, b);

                return color;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        protected static bool IsDesingerProcess
        {
            get
            {
                if (isDesingerProcess == null)
                {
                    isDesingerProcess = System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";
                }

                return isDesingerProcess.Value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        protected bool IsDesignMode => DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || IsDesingerProcess;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        protected bool IsInitializing => !forceInitialized && (this.isInitializing != 0 || IsLayoutSuspendedCore);

        private SizeF dpiScaleFactor = new SizeF(1f, 1f);

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected SizeF DpiScaleFactor
        {
            get { return this.dpiScaleFactor; }
            private set { this.dpiScaleFactor = value; }
        }

        #endregion

        public FormChrome()
        {
            SetStyle(ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            //_screenDC = Win32.CreateCompatibleDC(IntPtr.Zero);


            AutoScaleMode = AutoScaleMode.None;

            InitializeReflectedFields();

            shadowDecorator = new ChromeDecorator(this, false);

            if (!IsDesignMode)
            {
                this.minimumClientSize = Size.Empty;
                this.maximumClientSize = Size.Empty;

            }

            processDpiAwareness = GetDpiState((uint)System.Diagnostics.Process.GetCurrentProcess().Id);

            if (processDpiAwareness == PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE)
            {
                try
                {

                    var ret = (int)GetWindowDpiAwarenessContext(Handle);


                    if (ret == 34)  // DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2
                    {
                        isPerMonitorAwareV2 = true;
                    }
                }
                catch
                {

                }

            }


            if (FormStyleHelper.IsWindows8OrLower)
            {
                float dx, dy;
                Graphics g = this.CreateGraphics();

                try
                {
                    dx = g.DpiX;
                    dy = g.DpiY;
                }
                finally
                {
                    g.Dispose();
                }

                oldDpi = currentDpi;

                startupDpi = currentDpi = (int)dx;

            }
            else
            {
                var currentScreen = Screen.FromHandle(Handle);
                GetDpiForScreen(currentScreen, DpiType.Effective, out var dpiX, out var dpiY);

                oldDpi = currentDpi;

                startupDpi = currentDpi = (int)dpiX;
            }


            WriteConsoleLog($"INITIALIZED DPI = {startupDpi}");


        }

        private void InitializeReflectedFields()
        {
            clientWidthField = typeof(Control).GetField("clientWidth", BindingFlags.NonPublic | BindingFlags.Instance);
            clientHeightField = typeof(Control).GetField("clientHeight", BindingFlags.NonPublic | BindingFlags.Instance);
            formStateSetClientSizeField = typeof(Form).GetField("FormStateSetClientSize", BindingFlags.NonPublic | BindingFlags.Static);
            formStateField = typeof(Form).GetField("formState", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        #region Scaling

        public virtual void DpiScaleChanged(SizeF scaleFactor)
        {
            this.dpiScaleFactor = scaleFactor;
        }


        private void HandleDpiChanged()
        {

            currentDpi = User32.GetOriginalDeviceDpi(Handle);

            float scaleFactor = 1f;

            if (oldDpi != 0)
            {
                scaleFactor = (float)currentDpi / oldDpi;
            }

            //if(lastChangedDpi == currentDpi)
            //{
            //    WriteConsoleLog($"LAST:{lastChangedDpi} equals CURRENT:{currentDpi} Ignore...");
            //    return;
            //}

            //else if (oldDpi == 0) //Form shown for the first time.
            //{
            //    scaleFactor = (float)currentDpi / designTimeDpi;

            //}

            WriteConsoleLog($"DPI_SCALE_FACTOR = {scaleFactor}");


            if (scaleFactor == 1f)
            {
                return;
            }





            this.maxSizeState = this.MaximumSize;
            this.minSizeState = this.MinimumSize;
            this.MinimumSize = Size.Empty;
            this.MaximumSize = Size.Empty;

            this.SaveAnchorStates();
            this.Scale(new SizeF(scaleFactor, scaleFactor));
            //lastChangedDpi = currentDpi;
            this.RestoreAnchorStates();


            //Cursor.Position = PointToScreen(new Point((int)Math.Round(Cursor.Position.X * scaleFactor, MidpointRounding.AwayFromZero), (int)Math.Round(Cursor.Position.Y * scaleFactor, MidpointRounding.AwayFromZero)));

            this.MinimumSize = FormStyleHelper.ScaleSize(minSizeState, new SizeF(scaleFactor, scaleFactor));
            this.MaximumSize = FormStyleHelper.ScaleSize(maxSizeState, new SizeF(scaleFactor, scaleFactor));

        }



        protected virtual bool OnWMDpiChanged(ref Message m)
        {
            if (scaling)
            {
                oldDpi = currentDpi;
                return false;
            }
            else
            {
                oldDpi = currentDpi;
                currentDpi = (short)(int)m.WParam;
            }




            WriteConsoleLog($"[WM_DPICHANGED]: {oldDpi} -> {currentDpi}");



            if (!loaded)
            {
                return false;
            }

            if (oldDpi != currentDpi)
            {


                if (this.isMoving)
                {
                    shouldScale = true;
                }
                else
                {

                    HandleDpiChanged();
                    shouldScale = false;
                }

                return true;
            }

            return false;

        }

        protected override void OnResizeBegin(EventArgs e)
        {
            this.isMoving = true;

            base.OnResizeBegin(e);

        }



        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);

            this.isMoving = false;

            if (shouldScale || ShouldPerformScaling())
            {
                WriteConsoleLog($"[RESIZE END] {shouldScale} {DpiScaleFactor}");


                shouldScale = false;


                HandleDpiChanged();
            }
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            if (this.shouldScale)
            {
                WriteConsoleLog($"[MOVE CHANGE] {shouldScale} {DpiScaleFactor}");


                this.shouldScale = false;

                HandleDpiChanged();
            }
        }


        private bool ShouldPerformScaling()
        {
            var dpi = User32.GetOriginalDeviceDpi(Handle);

            if (dpi != currentDpi)
            {
                return true;
            }

            return false;
        }

        private bool CanPerformScaling()
        {
            Screen screen = Screen.FromHandle(Handle);



            //Screen screen = Screen.FromPoint(Location);

            if (screen.Bounds.Contains(this.Bounds))
            {
                //WriteConsoleLog($"Form in [{screen}] is {screen.Bounds.Contains(this.Bounds)}");

                return true;
            }

            return false;
        }


        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {



            Rectangle rect = this.Bounds;


            if (oldDpi != 0)
            {
                rect = this.GetScaledBounds(rect, factor, specified);
            }

            if (oldDpi == 0 && factor.Width > 1f)
            {

                rect = this.GetScaledBounds(rect, factor, specified);
            }

            DpiScaleChanged(factor);

            this.scaling = true;

            ScaleFontForControl(this, factor.Width);


            base.ScaleControl(factor, specified);


            this.scaling = false;

            this.Size = rect.Size;



        }

        private void ScaleFontForControl(Control control, float factor)
        {

            if (!loaded) return;

            var oldFont = control.Font;
            var newFont = new Font(control.Font.FontFamily,
                   Convert.ToSingle(Math.Round(control.Font.Size * factor, 2, MidpointRounding.ToEven)),
                   control.Font.Style);



            if (control.Parent == null)
            {
                control.Font = newFont;
                //WriteConsoleLog($"[FORM FONT CHANGED]:{oldFont.Size} -> {control.Font.Size} {control.Font.Unit}");
            }
            else if (control.Font != control.TopLevelControl.Font)
            {

                control.Font = newFont;


                //WriteConsoleLog($"[CONTROL FONT CHANGED]:{oldFont.Size} -> {control.Font.Size} {control.Font.Unit}");
            }



            foreach (Control child in control.Controls)
            {
                ScaleFontForControl(child, factor);
            }
        }

        private void SaveAnchorStates()
        {
            if (this.anchorsStack == null)
            {
                this.anchorsStack = new Stack<Dictionary<Control, AnchorStyles>>();
            }

            Dictionary<Control, AnchorStyles> anchors = new Dictionary<Control, AnchorStyles>();
            Queue<Control> queue = new Queue<Control>();

            foreach (Control ctrl in this.Controls)
            {
                queue.Enqueue(ctrl);
            }

            while (queue.Count > 0)
            {
                Control ctrl = queue.Dequeue();

                if (ctrl.Dock == DockStyle.None && ctrl.Anchor != AnchorStyles.None)
                {
                    anchors.Add(ctrl, ctrl.Anchor);
                    ctrl.Anchor = AnchorStyles.None;
                }

                foreach (Control childControl in ctrl.Controls)
                {
                    queue.Enqueue(childControl);
                }
            }

            this.anchorsStack.Push(anchors);
        }

        private void RestoreAnchorStates()
        {
            Dictionary<Control, AnchorStyles> anchors = this.anchorsStack.Pop();
            Queue<Control> queue = new Queue<Control>();

            foreach (Control ctrl in this.Controls)
            {
                queue.Enqueue(ctrl);
            }

            while (queue.Count > 0)
            {
                Control ctrl = queue.Dequeue();

                if (anchors.ContainsKey(ctrl))
                {
                    ctrl.Anchor = anchors[ctrl];
                    anchors.Remove(ctrl);
                }

                foreach (Control childControl in ctrl.Controls)
                {
                    queue.Enqueue(childControl);
                }
            }

            anchors.Clear();
        }




        protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
        {

            Rectangle rect = base.GetScaledBounds(bounds, factor, specified);

            Size sz = SizeFromClientSize(Size.Empty);
            if (!GetStyle(ControlStyles.FixedWidth) && ((specified & BoundsSpecified.Width) != BoundsSpecified.None))
            {
                int clientWidth = bounds.Width - sz.Width;
                rect.Width = ((int)Math.Round((double)(clientWidth * factor.Width))) + sz.Width;
            }
            if (!GetStyle(ControlStyles.FixedHeight) && ((specified & BoundsSpecified.Height) != BoundsSpecified.None))
            {
                int clientHeight = bounds.Height - sz.Height;
                rect.Height = ((int)Math.Round((double)(clientHeight * factor.Height))) + sz.Height;
            }

            //this.Cursor = new Cursor(Cursor.Current.Handle);

            return rect;
        }

        protected override void ScaleCore(float x, float y)
        {
            MaximumClientSize = new Size((int)Math.Round(MaximumClientSize.Width * x), (int)Math.Round(MaximumClientSize.Height * y));
            base.ScaleCore(x, y);
            MinimumClientSize = new Size((int)Math.Round(MinimumClientSize.Width * x), (int)Math.Round(MinimumClientSize.Height * y));
        }


        #endregion


        #region Size the window




        protected virtual Padding GetWindowRealNCMargin()
        {
            RECT boundsRect = new RECT();

            Rectangle screenClient;

            CreateParams cp = this.CreateParams;

            if (this.IsHandleCreated)
            {
                // RectangleToScreen will force handle creation and we don't want this during Form.ScaleControl
                screenClient = this.RectangleToScreen(this.ClientRectangle);
            }
            else
            {
                screenClient = this.ClientRectangle;
                screenClient.Offset(-this.Location.X, -this.Location.Y);
            }

            boundsRect.left = screenClient.Left;
            boundsRect.top = screenClient.Top;
            boundsRect.right = screenClient.Right;
            boundsRect.bottom = screenClient.Bottom;


            User32.AdjustWindowRectEx(ref boundsRect, cp.Style, this.MainMenuStrip != null, cp.ExStyle);




            Padding result = new Padding(
                    screenClient.Left - boundsRect.left,
                    screenClient.Top - boundsRect.top,
                    boundsRect.right - screenClient.Right,
                    boundsRect.bottom - screenClient.Bottom);






            if (currentDpi != 0)

            {
                if (processDpiAwareness == PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE && isPerMonitorAwareV2)
                {
                    result = ScalePadding(result);
                }
                //System.Diagnostics.Debugger.WriteLine($"DPI={currentDpi} {result}");

            }




            return result;


        }

        //static readonly Dictionary<FormBorderStyle, Padding> StandardPaddings = new Dictionary<FormBorderStyle, Padding>
        //{
        //    [FormBorderStyle.None] = new Padding(0, 0, 0, 0),
        //    [FormBorderStyle.FixedSingle] = new Padding(3, 26, 3, 3),
        //    [FormBorderStyle.Fixed3D] = new Padding(5, 28, 5, 5),
        //    [FormBorderStyle.FixedDialog] = new Padding(3, 26, 3, 3),
        //    [FormBorderStyle.Sizable] = new Padding(8, 31, 8, 8),
        //    [FormBorderStyle.FixedToolWindow] = new Padding(3, 26, 3, 3),
        //    [FormBorderStyle.SizableToolWindow] = new Padding(8, 31, 8, 8),

        //};



        Padding ScalePadding(Padding value)
        {

            var scaleFactor = 1f;
            var hMonitor = User32.MonitorFromWindow(Handle, (uint)MonitorFromWindowFlags.MONITOR_DEFAULTTONEAREST);

            try
            {
                User32.GetDpiForMonitor(hMonitor, MonitorDpiType.MDT_DEFAULT, out int dx, out int dy);

                scaleFactor = dx / (float)designTimeDpi;
            }
            catch
            {
            }



            var standardScale = startupDpi / (float)designTimeDpi;

            var standardBorder = (int)Math.Ceiling((value.Bottom - 2) / standardScale) + 2;
            var standardCaption = (int)Math.Round((SystemInformation.CaptionHeight) / standardScale, MidpointRounding.AwayFromZero);
            Padding result;

            var border = (int)Math.Floor((standardBorder - 2) * scaleFactor) + 2;
            var caption = (int)Math.Round((standardCaption) * scaleFactor, MidpointRounding.ToEven) + border;



            result = new Padding(border, caption, border, border);

            return result;

            //var scaleFactor = dpi / (float)designTimeDpi;
            //var paddings = StandardPaddings[FormBorderStyle]; //something incorect.
            //var captionHeight = SystemInformation.CaptionHeight;
            //var border3DSize = SystemInformation.Border3DSize;
            //var borderSize = SystemInformation.FixedFrameBorderSize;

            //var sizingBorderSize = SystemInformation.SizingBorderWidth;


            //var borderSize = SystemInformation.BorderSize;
            //var frameBorder = SystemInformation.FrameBorderSize;
            //var fixedBorder = SystemInformation.FixedFrameBorderSize;
            //var threeD = SystemInformation.Border3DSize;
            //var resizeBorder = SystemInformation.SizingBorderWidth;

            //var caption = 0;
            //var border = 0;

            //if (scaleFactor < 1)
            //{
            //    caption = (int)Math.Floor(SystemInformation.CaptionHeight * scaleFactor);
            //    border = (int)Math.Floor((double)value.Bottom * scaleFactor);
            //}
            //else
            //{
            //    caption = (int)Math.Ceiling(SystemInformation.CaptionHeight * scaleFactor);
            //    border = (int)Math.Ceiling((double)value.Bottom * scaleFactor);
            //}

            //border = (int)(scaleFactor < 0f ? Math.Floor((value.Bottom - 2) * scaleFactor) : Math.Ceiling((value.Bottom - 2) * scaleFactor)) + 2;

            //border = (int)Math.Ceiling((value.Bottom - 2) * scaleFactor) + 2;


            //WriteConsoleLog($"Default:\t[C]{standardCaption} [B]{standardBorder}");
            //WriteConsoleLog($"Scaled:\t\t[C]{caption-border} [B]{border}");





            //switch (FormBorderStyle)
            //{

            //    case FormBorderStyle.FixedSingle:
            //    case FormBorderStyle.Fixed3D:
            //    case FormBorderStyle.FixedDialog:
            //    case FormBorderStyle.FixedToolWindow:
            //        result = new Padding(paddings.Left, (int)Math.Round((paddings.Top - paddings.Bottom) * scaleFactor, MidpointRounding.ToEven) + paddings.Bottom, paddings.Right, paddings.Bottom);
            //        break;
            //    case FormBorderStyle.Sizable:
            //    case FormBorderStyle.SizableToolWindow:
            //        result = new Padding(
            //            (int)((paddings.Left - 2) * scaleFactor + 2f),
            //            (int)((paddings.Top - 2) * scaleFactor + 2f),
            //            (int)((paddings.Right - 2) * scaleFactor + 2f),
            //            (int)((paddings.Bottom - 2) * scaleFactor + 2f));

            //        break;
            //    default:
            //        return value;
            //}




        }

        public override Size MinimumSize
        {
            get
            {
                if (minimumSize.HasValue)
                    return minimumSize.Value;
                return base.MinimumSize;
            }
            set
            {
                minimumSize = value;
                if (IsInitializing)
                {
                    return;
                }
                Size maxSize = MaximumSize;
                base.MinimumSize = value;
                if (maxSize != MaximumSize)
                    MaximumClientSize = ClientSizeFromSize(MaximumSize);
            }
        }
        public override Size MaximumSize
        {
            get
            {
                if (maximumSize.HasValue)
                    return maximumSize.Value;
                return base.MaximumSize;
            }
            set
            {
                maximumSize = value;
                if (IsInitializing)
                {
                    return;
                }
                Size minSize = MinimumSize;
                base.MaximumSize = value;
                if (MinimumSize != minSize)
                    MinimumClientSize = ClientSizeFromSize(MinimumSize);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size MinimumClientSize
        {
            get { return minimumClientSize; }
            set
            {
                value = ConstrainMinimumClientSize(value);
                if (MinimumClientSize == value) return;
                minimumClientSize = value;
                OnMinimumClientSizeChanged();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size MaximumClientSize
        {
            get { return maximumClientSize; }
            set
            {
                if (MaximumClientSize == value) return;
                maximumClientSize = value;
                OnMaximumClientSizeChanged();
            }
        }

        protected Size ConstrainMinimumClientSize(Size value)
        {
            value.Width = Math.Max(0, value.Width);
            value.Height = Math.Max(0, value.Height);
            return value;
        }

        protected virtual void OnMinimumClientSizeChanged()
        {
            if (IsInitializing) return;
            MinimumSize = GetConstrainSize(MinimumClientSize);
        }

        protected virtual void OnMaximumClientSizeChanged()
        {
            if (IsInitializing) return;
            MaximumSize = GetConstrainSize(MaximumClientSize);
        }

        public new void SuspendLayout()
        {

            base.SuspendLayout();

            isInitializing++;
        }

        public new void ResumeLayout()
        {
            ResumeLayout(true);
        }

        public new void ResumeLayout(bool performLayout)
        {

            if (this.isInitializing > 0)
                this.isInitializing--;

            if (this.isInitializing == 0)
            {
                CheckForceUIChangedCore();
            }

            base.ResumeLayout(performLayout);

            if (!IsInitializing)
            {
                CheckMinimumSize();
                CheckMaximumSize();
            }
        }

        private void CheckMinimumSize()
        {
            if (this.minimumSize == null) return;
            Size msize = (Size)minimumSize;
            if (!msize.IsEmpty)
            {
                if (msize.Width > 0) msize.Width = Math.Min(msize.Width, Size.Width);
                if (msize.Height > 0) msize.Height = Math.Min(msize.Height, Size.Height);
                if (this.maximumSize != null && !this.maximumSize.Value.IsEmpty)
                {
                    if (this.maximumSize.Value.Width == this.minimumSize.Value.Width)
                        msize.Width = Size.Width;
                    if (this.maximumSize.Value.Height == this.minimumSize.Value.Height)
                        msize.Height = Size.Height;
                }
            }
            this.minimumSize = null;
            base.MinimumSize = msize;
        }

        private void CheckMaximumSize()
        {
            if (this.maximumSize == null) return;
            Size msize = (Size)maximumSize;
            if (!msize.IsEmpty)
            {
                if (msize.Width > 0) msize.Width = Math.Max(msize.Width, Size.Width);
                if (msize.Height > 0) msize.Height = Math.Max(msize.Height, Size.Height);
                if (this.minimumSize != null && !this.minimumSize.Value.IsEmpty)
                {
                    if (this.maximumSize.Value.Width == this.minimumSize.Value.Width)
                        msize.Width = Size.Width;
                    if (this.maximumSize.Value.Height == this.minimumSize.Value.Height)
                        msize.Height = Size.Height;
                }
            }
            this.maximumSize = null;
            base.MaximumSize = msize;
        }

        private void CheckForceUIChangedCore()
        {
            if (this.isInitializing != 0) return;
            if (LayoutSuspendCountCore == 1 && this.shouldUpdateOnResumeLayout)
            {
                this.forceInitialized = true;
                try
                {
                    OnUIChangedCore();
                }
                finally
                {
                    this.forceInitialized = false;
                }
            }
        }

        protected internal void SetLayoutDeferred()
        {
            const int STATE_LAYOUTDEFERRED = 512;
            MethodInfo mi = typeof(Control).GetMethod("SetState", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(int), typeof(bool) }, null);
            mi.Invoke(this, new object[] { STATE_LAYOUTDEFERRED, true });
        }

        private void OnUIChangedCore()
        {
            if (IsInitializing)
            {
                if (Visible && IsLayoutSuspendedCore)
                {
                    SetLayoutDeferred();
                }
                this.shouldUpdateOnResumeLayout = true;
                return;
            }
            this.shouldUpdateOnResumeLayout = false;
            bool shouldUpdateSize = CheckUpdateUI();
            Size clientSize = ClientSize;
            OnMinimumClientSizeChanged();
            OnMaximumClientSizeChanged();
            FieldInfo fiBounds = typeof(Form).GetField("restoredWindowBounds", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo fiBoundsSpec = typeof(Form).GetField("restoredWindowBoundsSpecified", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo fiBounds2 = typeof(Form).GetField("restoreBounds", BindingFlags.NonPublic | BindingFlags.Instance);
            Rectangle restoredWinBounds = (Rectangle)fiBounds.GetValue(this);
            Rectangle restoreBounds = (Rectangle)fiBounds2.GetValue(this);
            BoundsSpecified restoredWinBoundsSpec = (BoundsSpecified)fiBoundsSpec.GetValue(this);

            GetFormStateExWindowBoundsIsClientSize(out int frmStateExWindowBoundsWidthIsClientSize, out int frmStateExWindowBoundsHeightIsClientSize);

            int windowState = SaveFormStateWindowState();
            bool normalState = SaveControlStateNormalState();
            if (shouldUpdateSize)
                Size = SizeFromClientSize(clientSize);
            if ((restoredWinBoundsSpec & BoundsSpecified.Width) != 0 && (restoredWinBoundsSpec & BoundsSpecified.Height) != 0) restoreBounds.Size = SizeFromClientSize(restoredWinBounds.Size);
            if (WindowState != FormWindowState.Normal && IsHandleCreated)
            {
                fiBounds.SetValue(this, restoredWinBounds);
                fiBounds2.SetValue(this, restoreBounds);
                SetFormStateExWindowBoundsIsClientSize(frmStateExWindowBoundsWidthIsClientSize, frmStateExWindowBoundsHeightIsClientSize);
            }
            if (IsMdiChild)
            {
                RestoreFormStateWindowState(windowState);
                RestoreControlStateNormalState(normalState);
            }


        }

        protected bool CheckUpdateUI()
        {
            Padding? savedMargins = null;

            Size savedClientSize = ClientSize;

            if (DesignMode && IsInitializing)
            {
                //savedMargins = SavedBorders;
                savedMargins = GetWindowRealNCMargin();
            }

            var needReset = false;

            var borders = GetWindowRealNCMargin();

            if (savedMargins != null && !object.Equals(savedMargins.Value, borders /* RealWindowBorders*/))
            {
                ClientSize = savedClientSize;
            }

            if (IsHandleCreated)
            {
                UxTheme.SetWindowTheme(this.Handle, "", "");
                Refresh();
            }
            return needReset;
        }

        private int SaveFormStateWindowState()
        {
            FieldInfo formStateWindowState = typeof(Form).GetField("FormStateWindowState", BindingFlags.NonPublic | BindingFlags.Static);
            BitVector32.Section formStateWindowStateSection = ((BitVector32.Section)formStateWindowState.GetValue(this));
            BitVector32 formStateData = (BitVector32)formStateField.GetValue(this);
            return formStateData[formStateWindowStateSection];
        }

        private void RestoreFormStateWindowState(int state)
        {
            FieldInfo formStateWindowState = typeof(Form).GetField("FormStateWindowState", BindingFlags.NonPublic | BindingFlags.Static);
            BitVector32.Section formStateWindowStateSection = ((BitVector32.Section)formStateWindowState.GetValue(this));
            BitVector32 formStateData = (BitVector32)formStateField.GetValue(this);
            formStateData[formStateWindowStateSection] = state;
            formStateField.SetValue(this, formStateData);
        }

        private bool SaveControlStateNormalState()
        {
            FieldInfo state = typeof(Control).GetField("state", BindingFlags.NonPublic | BindingFlags.Instance);
            return ((int)state.GetValue(this) & 0x10000) != 0;
        }

        private void RestoreControlStateNormalState(bool isNormal)
        {
            FieldInfo state = typeof(Control).GetField("state", BindingFlags.NonPublic | BindingFlags.Instance);
            int value = (int)state.GetValue(this);
            state.SetValue(this, isNormal ? (value | 0x10000) : (value & (~0x10000)));
        }

        private void GetFormStateExWindowBoundsIsClientSize(out int width, out int height)
        {
            FieldInfo formStateExInfo = typeof(Form).GetField("formStateEx", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo formStateExWindowBoundsWidthIsClientSizeInfo = typeof(Form).GetField("FormStateExWindowBoundsWidthIsClientSize", BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo formStateExWindowBoundsHeightIsClientSizeInfo = typeof(Form).GetField("FormStateExWindowBoundsHeightIsClientSize", BindingFlags.NonPublic | BindingFlags.Static);
            BitVector32.Section widthSection = (BitVector32.Section)formStateExWindowBoundsWidthIsClientSizeInfo.GetValue(this);
            BitVector32.Section heightSection = (BitVector32.Section)formStateExWindowBoundsHeightIsClientSizeInfo.GetValue(this);
            BitVector32 formState = (BitVector32)formStateExInfo.GetValue(this);
            width = formState[widthSection];
            height = formState[heightSection];
        }

        private void SetFormStateExWindowBoundsIsClientSize(int width, int height)
        {
            FieldInfo formStateExInfo = typeof(Form).GetField("formStateEx", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo formStateExWindowBoundsWidthIsClientSizeInfo = typeof(Form).GetField("FormStateExWindowBoundsWidthIsClientSize", BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo formStateExWindowBoundsHeightIsClientSizeInfo = typeof(Form).GetField("FormStateExWindowBoundsHeightIsClientSize", BindingFlags.NonPublic | BindingFlags.Static);
            BitVector32.Section widthSection = (BitVector32.Section)formStateExWindowBoundsWidthIsClientSizeInfo.GetValue(this);
            BitVector32.Section heightSection = (BitVector32.Section)formStateExWindowBoundsHeightIsClientSizeInfo.GetValue(this);
            BitVector32 formState = (BitVector32)formStateExInfo.GetValue(this);
            formState[widthSection] = width;
            formState[heightSection] = height;
            formStateExInfo.SetValue(this, formState);
        }

        protected virtual void RecalcClientSize()
        {
            RECT clientRect = new RECT();
            User32.GetClientRect(Handle, ref clientRect);
            Rectangle clientBounds = new Rectangle(clientRect.left, clientRect.top, clientRect.right, clientRect.bottom);
            clientBounds.Offset(-clientBounds.Left, -clientBounds.Top);
            clientBounds.Offset(ClientMargin.Left, ClientMargin.Top);

            SetClientSizeCore(clientBounds.Width, clientBounds.Height);

            InvalidateNonClient();
        }

        /// <summary>
        /// Gets the size of the borders requested by the real window.
        /// </summary>
        /// <returns>Border sizing.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Padding RealWindowBorders
        {
            get
            {
                // Use the form level create params to get the real borders
                return FormStyleHelper.GetWindowBorders(CreateParams);
            }
        }

        /// <summary>
        /// Gets and sets the active state of the window.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool WindowActive
        {
            get { return _windowActive; }

            set
            {
                if (_windowActive != value)
                {
                    _windowActive = value;
                    OnWindowActiveChanged();
                }
            }
        }


        /// <summary>
        /// Request the non-client area be recalculated.
        /// </summary>
        public void RecalcNonClient()
        {
            if (!IsDisposed && !Disposing && IsHandleCreated)
            {
                User32.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0,
                                (SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOMOVE |
                                       SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOSIZE |
                                       SetWindowPosFlags.SWP_NOOWNERZORDER | SetWindowPosFlags.SWP_FRAMECHANGED));
            }
        }

        /// <summary>
        /// Request the non-client area be repainted.
        /// </summary>
        public void RedrawNonClient()
        {
            InvalidateNonClient(Rectangle.Empty, true);
        }

        /// <summary>
        /// Convert a screen location to a window location.
        /// </summary>
        /// <param name="screenPt">Screen point.</param>
        /// <returns>Point in window coordinates.</returns>
        protected Point ScreenToWindow(Point screenPt)
        {
            // First of all convert to client coordinates
            Point clientPt = PointToClient(screenPt);

            // Now adjust to take into account the top and left borders
            //Padding borders = RealWindowBorders;
            Padding borders = GetWindowRealNCMargin();
            clientPt.Offset(borders.Left, borders.Top);

            return clientPt;
        }

        /// <summary>
        /// Request the non-client area be repainted.
        /// </summary>
        public void InvalidateNonClient()
        {
            InvalidateNonClient(Rectangle.Empty, true);
        }

        /// <summary>
        /// Request the non-client area be repainted.
        /// </summary>
        /// <param name="invalidRect">Area to invalidate.</param>
        protected void InvalidateNonClient(Rectangle invalidRect)
        {
            InvalidateNonClient(invalidRect, true);
        }

        /// <summary>
        /// Gets rectangle that is the real window rectangle based on Win32 API call.
        /// </summary>
        protected Rectangle RealWindowRectangle
        {
            get
            {
                // Grab the actual current size of the window, this is more accurate than using
                // the 'this.Size' which is out of date when performing a resize of the window.
                RECT windowRect = new RECT();
                User32.GetWindowRect(Handle, ref windowRect);

                // Create rectangle that encloses the entire window
                return new Rectangle(0, 0, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);
            }
        }

        /// <summary>
        /// Request the non-client area be repainted.
        /// </summary>
        /// <param name="invalidRect">Area to invalidate.</param>
        /// <param name="excludeClientArea">Should client area be excluded.</param>
        protected void InvalidateNonClient(Rectangle invalidRect, bool excludeClientArea)
        {
            if (!IsDisposed && !Disposing && IsHandleCreated)
            {
                if (invalidRect.IsEmpty)
                {
                    Rectangle realWindowRectangle = RealWindowRectangle;

                    invalidRect = new Rectangle(realWindowRectangle.Left,
                                                realWindowRectangle.Top,
                                                realWindowRectangle.Width,
                                                realWindowRectangle.Height);
                }

                using (Region invalidRegion = new Region(invalidRect))
                {
                    if (excludeClientArea)
                    {
                        RECT clientRect = new RECT();
                        User32.GetClientRect(Handle, ref clientRect);
                        Rectangle clientBounds = new Rectangle(clientRect.left, clientRect.top, clientRect.right, clientRect.bottom);
                        clientBounds.Offset(-clientBounds.Left, -clientBounds.Top);
                        clientBounds.Offset(ClientMargin.Left, ClientMargin.Top);

                        invalidRegion.Exclude(clientBounds);

                    }

                    //using (Graphics g = Graphics.FromHwnd(Handle))
                    //{
                    //    IntPtr hRgn = invalidRegion.GetHrgn(g);
                    //    try
                    //    {
                    //        User32.RedrawWindow(Handle, IntPtr.Zero, hRgn,
                    //                        (uint)(RedrawWindowFlags.RDW_FRAME | RedrawWindowFlags.RDW_UPDATENOW | RedrawWindowFlags.RDW_INVALIDATE));
                    //    }
                    //    catch { }
                    //    Gdi32.DeleteObject(hRgn);
                    //}
                }
            }
        }



        /// <summary>
        /// Raises the HandleCreated event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            // Can fail on versions before XP SP1
            try
            {
                // Prevent the OS from drawing the non-client area in classic look
                // if the application stops responding to windows messages
                User32.DisableProcessWindowsGhosting();
                UxTheme.SetWindowTheme(Handle, "", "");
            }
            catch { }



            base.OnHandleCreated(e);


        }

        protected override void OnCreateControl()
        {


            base.OnCreateControl();


            if (startupDpi != designTimeDpi && !IsDesignMode)
            {
                var factor = startupDpi / (float)designTimeDpi;

                WriteConsoleLog($"STARTUP_SCALE_FACTOR = {factor}");

                this.Scale(new SizeF(factor, factor));

            }



            loaded = true;


            HandleDpiChanged();

            UpdateFormShadow();



            if (!IsDesignMode)
            {


                if (StartPosition == FormStartPosition.CenterParent && Owner != null)
                {
                    Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                    Owner.Location.Y + Owner.Height / 2 - Height / 2);

                }
                else if (StartPosition == FormStartPosition.CenterScreen || (StartPosition == FormStartPosition.CenterParent && Owner == null))
                {
                    var currentScreen = Screen.FromPoint(MousePosition);

                    var initScreen = Screen.FromHandle(Handle);

                    if (currentScreen != initScreen)
                    {
                        Location = new Point(Left + currentScreen.WorkingArea.Left, Top + currentScreen.WorkingArea.Top);
                    }

                    Location = new Point(currentScreen.WorkingArea.Left + (currentScreen.WorkingArea.Width / 2 - this.Width / 2), currentScreen.WorkingArea.Top + (currentScreen.WorkingArea.Height / 2 - this.Height / 2));

                }
            }


        }

        protected override void OnLoad(EventArgs e)
        {


            base.OnLoad(e);




            //OnMinimumClientSizeChanged();
            //OnMaximumClientSizeChanged();

            CalcFormBounds();




        }

        protected override void OnShown(EventArgs e)
        {




            base.OnShown(e);





            var action = new Action(() =>
            {
                shadowDecorator.Enable(true);

                if (IsActive)
                {
                    shadowDecorator.SetFocus();
                }
            });

            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(180);

                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(action));
                }
                else
                {
                    action.Invoke();
                }
            });
        }

        private void UpdateFormShadow()
        {
            if (IsDesignMode) return;

            if (!IsMdiChild && FormBorderStyle != FormBorderStyle.None && Parent == null)
            {
                shadowDecorator.InitializeShadows();

                if (Owner != null)
                {
                    shadowDecorator.SetOwner(Owner.Handle);
                }

            }
        }

        protected Rectangle formBounds = Rectangle.Empty;
        protected Rectangle prevFormBounds = Rectangle.Empty;
        protected bool isFormPainted = false;
        protected bool boundsUpdated = false;
        protected bool IsRegionPainted { get; private set; }

        protected internal void CalcFormBounds()
        {
            if (IsMdiChild || !IsHandleCreated) return;
            var correctFormBounds = new RECT();
            User32.GetWindowRect(this.Handle, ref correctFormBounds);
            Rectangle currentBounds = new Rectangle(correctFormBounds.left, correctFormBounds.top, correctFormBounds.right - correctFormBounds.left, correctFormBounds.bottom - correctFormBounds.top);

            if (IsMinimizedState(currentBounds)) currentBounds = Rectangle.Empty;
            if (formBounds == currentBounds && (boundsUpdated || !IsRegionPainted))
                return;
            this.isFormPainted = false;
            prevFormBounds = formBounds;
            formBounds = currentBounds;
            if (prevFormBounds != formBounds) boundsUpdated = true;
        }

        protected internal bool IsMinimizedState(Rectangle bounds)
        {
            return WindowState == FormWindowState.Minimized && bounds.Location == minimizedFormLocation;
        }


        private Size cachedClientSize = new Size(-1, -1);


        protected override void CreateHandle()
        {
            if (!this.IsDisposed)
            {
                base.CreateHandle();
            }

            if (WindowState != FormWindowState.Minimized)
            {
                Size = SizeFromClientSize(ClientSize);
            }

            if (this.cachedClientSize != new Size(-1, -1))
            {
                this.SetClientSizeCore(cachedClientSize.Width, cachedClientSize.Height);
                this.cachedClientSize = new Size(-1, -1);
            }
        }



        /// <summary>
        /// Gets the margin that determines the position and size of the client
        /// area of the Form.
        /// </summary>
        protected virtual Padding ClientMargin
        {
            get
            {
                Padding clientMargin = Borders;
                return clientMargin;
            }
        }




        protected override void SetClientSizeCore(int x, int y)
        {
            if (!this.IsHandleCreated)
            {
                this.cachedClientSize = new Size(x, y);
                base.SetClientSizeCore(x, y);
                return;
            }


            if ((((clientWidthField != null))
                && ((clientHeightField != null)
                && (formStateField != null)))
                && (formStateSetClientSizeField != null))
            {

                //Size sizeToSet = new Size(x + this.ClientMargin.Horizontal, y + this.ClientMargin.Vertical);

                //base.Size = sizeToSet;

                //GetScaledBounds()

                Rectangle formBounds = new Rectangle(new Point(0, 0), SizeFromClientSize(new Size(x, y)));


                if (oldDpi == 0 && currentDpi != 0)
                {
                    var scaleFactor = currentDpi / (float)designTimeDpi;

                    //formBounds = GetScaledBounds(formBounds, new SizeF(scaleFactor, scaleFactor), BoundsSpecified.Size);
                }


                this.Size = formBounds.Size;

                Size clientSize = ClientSizeFromSize(this.Size);


                clientWidthField.SetValue(this, x);
                clientHeightField.SetValue(this, y);
                BitVector32.Section section = (BitVector32.Section)formStateSetClientSizeField.GetValue(this);
                BitVector32 vector = (BitVector32)formStateField.GetValue(this);
                vector[section] = 1;
                formStateField.SetValue(this, vector);
                this.OnClientSizeChanged(EventArgs.Empty);
                vector[section] = 0;
                formStateField.SetValue(this, vector);
            }
            else
            {
                base.SetClientSizeCore(x, y);
            }

        }

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new Left property value of the control.</param>
        /// <param name="y">The new Top property value of the control.</param>
        /// <param name="width">The new Width property value of the control.</param>
        /// <param name="height">The new Height property value of the control.</param>
        /// <param name="specified">A bitwise combination of the BoundsSpecified values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {

            if (IsDesignMode)
            {
                base.SetBoundsCore(x, y, width, height, specified);
                return;
            }

            var size = PatchFormSizeInRestoreWindowBoundsIfNecessary(width, height);
            size = CalcPreferredSizeCore(size);


            if (isMaximizedTest && WindowState != FormWindowState.Minimized)
            {
                if (y != Top)
                {
                    y = Top;

                }

                if (x != Left)
                {
                    x = Left;
                }

                isMaximizedTest = false;
            }


            base.SetBoundsCore(x, y, size.Width, size.Height, specified);

            //if (!this.IsInitializing && this.cachedClientSize != new Size(-1, -1))
            //{
            //    Padding scaledClientMargin = FormStyleHelper.ScalePadding(ClientMargin, new SizeF(1f / DpiScaleFactor.Width, 1f / DpiScaleFactor.Height));
            //    this.cachedClientSize = new Size(width - scaledClientMargin.Horizontal, height - scaledClientMargin.Vertical);
            //}

            //if (!IsDesignMode)
            //{
            //    PerformAutoScale();
            //}
        }

        protected virtual Size PatchFormSizeInRestoreWindowBoundsIfNecessary(int width, int height)
        {
            if (WindowState == FormWindowState.Normal)
            {
                try
                {
                    FieldInfo restoredWindowBoundsSpecified = typeof(Form).GetField("restoredWindowBoundsSpecified", BindingFlags.NonPublic | BindingFlags.Instance);
                    BoundsSpecified restoredSpecified = (BoundsSpecified)restoredWindowBoundsSpecified.GetValue(this);
                    if ((restoredSpecified & BoundsSpecified.Size) != BoundsSpecified.None)
                    {
                        FieldInfo formStateExWindowBoundsFieldInfo = typeof(Form).GetField("FormStateExWindowBoundsWidthIsClientSize", BindingFlags.NonPublic | BindingFlags.Static);
                        FieldInfo formStateExFieldInfo = typeof(Form).GetField("formStateEx", BindingFlags.NonPublic | BindingFlags.Instance);
                        FieldInfo restoredBoundsFieldInfo = typeof(Form).GetField("restoredWindowBounds", BindingFlags.NonPublic | BindingFlags.Instance);

                        if (formStateExWindowBoundsFieldInfo != null && formStateExFieldInfo != null && restoredBoundsFieldInfo != null)
                        {
                            Rectangle restoredWindowBounds = (Rectangle)restoredBoundsFieldInfo.GetValue(this);
                            BitVector32.Section section = (BitVector32.Section)formStateExWindowBoundsFieldInfo.GetValue(this);
                            BitVector32 vector = (BitVector32)formStateExFieldInfo.GetValue(this);
                            if (vector[section] == 1)
                            {
                                width = restoredWindowBounds.Width + ClientMargin.Horizontal;
                                height = restoredWindowBounds.Height + ClientMargin.Vertical;
                            }
                        }
                    }
                }
                catch
                {
                }
            }

            return new Size(width, height);

        }

        protected virtual Size CalcPreferredSizeCore(Size size)
        {
            return size;
        }

        /// <summary>
        /// Raises the Activated event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            WindowActive = true;
            base.OnActivated(e);
        }

        /// <summary>
        /// Raises the Deactivate event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnDeactivate(EventArgs e)
        {
            WindowActive = false;
            base.OnDeactivate(e);
        }

        private PropertyInfo piLayout = null;
        [Browsable(false)]
        protected internal bool IsLayoutSuspendedCore
        {
            get
            {
                if (piLayout == null) piLayout = typeof(Control).GetProperty("IsLayoutSuspended", BindingFlags.Instance | BindingFlags.NonPublic);
                if (piLayout != null) return (bool)piLayout.GetValue(this, null);
                return false;
            }
        }

        private FieldInfo fiLayoutSuspendCount = null;
        [Browsable(false)]
        protected internal byte LayoutSuspendCountCore
        {
            get
            {
                if (fiLayoutSuspendCount == null) fiLayoutSuspendCount = typeof(Control).GetField("layoutSuspendCount", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fiLayoutSuspendCount != null) return (byte)fiLayoutSuspendCount.GetValue(this);
                return 1;
            }
        }

        private FieldInfo formStateCoreField;
        private FieldInfo FormStateCoreField
        {
            get
            {
                if (formStateCoreField == null)
                    formStateCoreField = typeof(Form).GetField("formState", BindingFlags.NonPublic | BindingFlags.Instance);
                return formStateCoreField;
            }
        }

        private FieldInfo formStateWindowActivated;
        private FieldInfo FormStateWindowActivatedField
        {
            get
            {
                if (formStateWindowActivated == null)
                    formStateWindowActivated = typeof(Form).GetField("FormStateIsWindowActivated", BindingFlags.NonPublic | BindingFlags.Static);
                return formStateWindowActivated;
            }
        }

        [Browsable(false)]
        protected bool IsActive
        {
            get
            {
                BitVector32 bv = (BitVector32)FormStateCoreField.GetValue(this);
                BitVector32.Section s = (BitVector32.Section)FormStateWindowActivatedField.GetValue(this);
                return bv[s] == 1;
            }
        }

        protected virtual Region GetDefaultFormRegion(ref Rectangle rect)
        {
            rect = Rectangle.Empty;
            return null;
        }

        private void SetRegion(Region region, Rectangle rect)
        {
            if (this.regionRect == rect)
            {
                if (region != null)
                    region.Dispose();
                return;
            }

            if (Region != null)
            {
                Region.Dispose();
            }

            Region = region;

            if (object.Equals(region, Region))
                this.regionRect = rect;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (FormBorderStyle != FormBorderStyle.None && WindowState != FormWindowState.Minimized && !scaling)
            {
                PatchClientSize();
            }

            //CalcFormBounds();


            base.OnSizeChanged(e);
        }

        protected override Size SizeFromClientSize(Size clientSize)
        {
            clientSize.Width += ClientMargin.Horizontal;
            clientSize.Height += ClientMargin.Vertical;

            return clientSize;
        }

        protected virtual void PatchClientSize()
        {
            var size = ClientSizeFromSize(Size);
            FieldInfo fiWidth = typeof(Control).GetField("clientWidth", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo fiHeight = typeof(Control).GetField("clientHeight", BindingFlags.Instance | BindingFlags.NonPublic);
            fiWidth.SetValue(this, size.Width);
            fiHeight.SetValue(this, size.Height);

        }

        protected virtual void WriteConsoleLog(string message)
        {
#if DEBUG
            //Console.WriteLine(message);
#endif
        }

        protected virtual Size GetConstrainSize(Size clientSize)
        {
            if (clientSize == Size.Empty) return Size.Empty;
            return SizeFromClientSize(clientSize);
        }

        protected virtual Size ClientSizeFromSize(Size formSize)
        {
            if (formSize == Size.Empty)
            {
                return Size.Empty;
            }
            Size sz = SizeFromClientSize(Size.Empty);

            Size res = new Size(formSize.Width - sz.Width, formSize.Height - sz.Height);




            if (WindowState != FormWindowState.Maximized)
                return res;
            var rect = new RECT();

            var ncMargin = GetWindowRealNCMargin();

            rect.left = 0;
            rect.top = 0;
            rect.right = res.Width - ncMargin.Horizontal + sz.Width;
            rect.bottom = res.Height - ncMargin.Bottom * 2 + sz.Height;

            return new Size(rect.right, rect.bottom);
        }


        #endregion

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {

            if (FormBorderStyle == FormBorderStyle.None)
            {
                base.WndProc(ref m);
                return;

            }

            bool processed = false;

            // We do not process the message if on an MDI child, because doing so prevents the 
            // LayoutMdi call on the parent from working and cascading/tiling the children
            if ((m.Msg == (uint)WindowsMessages.WM_NCCALCSIZE) &&
                ((MdiParent == null)))
            {
                NCCalcSize(ref m);
            }

            // Do we need to override message processing?
            if (!IsDisposed && !Disposing)
            {
                switch ((WindowsMessages)m.Msg)
                {
                    case WindowsMessages.WM_DPICHANGED:
                        processed = OnWMDpiChanged(ref m);
                        break;

                    case WindowsMessages.WM_NCPAINT:
                        OnWMNCPaint(ref m);
                        processed = true;

                        break;
                    case WindowsMessages.WM_SIZE:
                        OnWMSize(ref m);
                        break;
                    case WindowsMessages.WM_ACTIVATEAPP:
                        User32.SendFrameChanged(Handle);
                        break;
                    case WindowsMessages.WM_NCHITTEST:
                        processed = OnWMNCHITTEST(ref m);
                        break;
                    case WindowsMessages.WM_NCACTIVATE:
                        processed = OnWMNCACTIVATE(ref m);
                        m.Result = Win32.MESSAGE_HANDLED;
                        User32.SendFrameChanged(Handle);

                        break;
                    case WindowsMessages.WM_NCMOUSEMOVE:
                        processed = OnWMNCMOUSEMOVE(ref m);
                        User32.SendFrameChanged(Handle);
                        break;
                    case WindowsMessages.WM_NCLBUTTONDOWN:
                        processed = OnWMNCLBUTTONDOWN(ref m);
                        User32.SendFrameChanged(Handle);

                        break;
                    case WindowsMessages.WM_NCLBUTTONUP:
                        processed = OnWMNCLBUTTONUP(ref m);
                        User32.SendFrameChanged(Handle);

                        break;
                    case WindowsMessages.WM_MOUSEMOVE:
                        if (_captured)
                            processed = OnWMMOUSEMOVE(ref m);
                        break;
                    case WindowsMessages.WM_LBUTTONUP:
                        if (_captured)
                            processed = OnWMLBUTTONUP(ref m);
                        break;
                    case WindowsMessages.WM_NCMOUSELEAVE:
                        if (!_captured)
                            processed = OnWMNCMOUSELEAVE(ref m);
                        break;
                    case WindowsMessages.WM_MOVE:
                        OnWMMove(ref m);
                        break;
                    case WindowsMessages.WM_ENTERSIZEMOVE:
                        var rect = new RECT();
                        User32.GetWindowRect(Handle, ref rect);
                        isEnterSizeMoveMode = true;
                        break;

                    case WindowsMessages.WM_EXITSIZEMOVE:
                        isEnterSizeMoveMode = false;
                        if (shadowDecorator != null && !shadowDecorator.IsEnabled)
                        {
                            shadowDecorator.Enable(true);
                        }
                        break;

                    case WindowsMessages.WM_SIZING:
                        if (IsHandleCreated && isEnterSizeMoveMode == true && shadowDecorator.IsEnabled)
                        {
                            shadowDecorator.Enable(false);
                        }
                        break;
                    case WindowsMessages.WM_NCLBUTTONDBLCLK:
                        processed = OnWMNCLBUTTONDBLCLK(ref m);
                        break;
                    case WindowsMessages.WM_SYSCOMMAND:
                        // Is this the command for closing the form?
                        var state = (int)m.WParam.ToInt64();

                        if (state == SystemCommandFlags.SC_CLOSE)
                        {
                            PropertyInfo pi = typeof(Form).GetProperty("CloseReason", BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.NonPublic);

                            // Update form with the reason for the close
                            pi.SetValue(this, CloseReason.UserClosing, null);
                        }

                        if ((int)m.WParam.ToInt64() != 61696)
                            processed = OnPaintNonClient(ref m);
                        break;
                    case WindowsMessages.WM_NCUAHDRAWCAPTION:
                    case WindowsMessages.WM_NCUAHDRAWFRAME:
                    case WindowsMessages.WM_UNKNOWN_GHOST:
                        m.Result = (IntPtr)(0);
                        processed = true;
                        User32.SendFrameChanged(Handle);

                        break;
                }
            }

            // If the message has not been handled, let base class process it
            if (!processed)
                base.WndProc(ref m);
        }

        #region Windows Message Handlers
        private void OnWMMove(ref Message m)
        {
            Point screenPoint = new Point((int)m.LParam.ToInt64());
        }

        bool isMaximizedTest = false;

        private void OnWMSize(ref Message m)
        {
            if (!IsHandleCreated)
            {
                return;
            }


            if (WindowState == FormWindowState.Maximized)
            {

                isMaximizedTest = true;

                Screen screen = Screen.FromHandle(Handle);
                if (screen == null) return;
                Rectangle bounds = FormBorderStyle == FormBorderStyle.None ? screen.Bounds : screen.WorkingArea;


                RECT windowRect = new RECT();
                User32.GetWindowRect(Handle, ref windowRect);

                Rectangle formBounds = new Rectangle(windowRect.left, windowRect.top, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);

                if (formBounds.X == -10000 || formBounds.Y == -10000)
                    return;

                Rectangle r = new Rectangle(bounds.X - formBounds.X, bounds.Y - formBounds.Y, formBounds.Width - (formBounds.Width - bounds.Width), formBounds.Height - (formBounds.Height - bounds.Height));

                SetRegion(new Region(r), r);
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                SetRegion(null, Rectangle.Empty);

                return;
            }
            else
            {


                Rectangle rect = new Rectangle();
                Region region = GetDefaultFormRegion(ref rect);
                SetRegion(region, rect);

            }




        }

        private void NCCalcSize(ref Message m)
        {


            if (m.WParam == (IntPtr)1)
            {


                var ncMargin = GetWindowRealNCMargin();





                var ncCalcSizeParams = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));

                //if (SavedBorders == null)
                //{
                //    SavedBorders = RealWindowBorders;
                //}

                Padding calculatedClientMargin = this.ClientMargin;

                //var windowBounds = new Rectangle(ncCalcSizeParams.rectProposed.left, ncCalcSizeParams.rectProposed.top, ncCalcSizeParams.rectProposed.right - ncCalcSizeParams.rectProposed.left, ncCalcSizeParams.rectProposed.bottom - ncCalcSizeParams.rectProposed.top);

                //Win32.GetClientRect(Handle, out var clientRect);
                //Rectangle clientBounds = RectangleToScreen(new Rectangle(clientRect.left, clientRect.top, clientRect.right, clientRect.bottom));


                //var left = clientBounds.Left - windowBounds.Left;

                //var right = windowBounds.Right - clientBounds.Right;

                //var top = clientBounds.Top - windowBounds.Top;

                //var bottom = windowBounds.Bottom - clientBounds.Bottom;

                //var calcMargin = new Padding(left, top, right, bottom);
                //var x = RealWindowBorders - ncMargin;

                //if (calcMargin != RealWindowBorders)
                //{
                //    if(calcMargin != ClientMargin)
                //    {

                //    }
                //}





                if (FormBorderStyle == FormBorderStyle.None)
                {
                    calculatedClientMargin = Padding.Empty;
                    ncMargin = Padding.Empty;
                }





                ncCalcSizeParams.rectProposed.top -= ncMargin.Top;


                ncCalcSizeParams.rectBeforeMove = ncCalcSizeParams.rectProposed;



                if (WindowState != FormWindowState.Maximized)
                {
                    ncCalcSizeParams.rectProposed.right += ncMargin.Right;
                    ncCalcSizeParams.rectProposed.bottom += ncMargin.Bottom;
                    ncCalcSizeParams.rectProposed.left -= ncMargin.Left;

                    ncCalcSizeParams.rectProposed.top += calculatedClientMargin.Top;
                    ncCalcSizeParams.rectProposed.left += calculatedClientMargin.Left;
                    ncCalcSizeParams.rectProposed.right -= calculatedClientMargin.Right;
                    ncCalcSizeParams.rectProposed.bottom -= calculatedClientMargin.Bottom;

                }
                else if (WindowState == FormWindowState.Maximized)
                {
                    ncCalcSizeParams.rectProposed.top += ncMargin.Bottom;
                }




                Marshal.StructureToPtr(ncCalcSizeParams, m.LParam, false);
                m.Result = (IntPtr)0x400;



            }

        }

        private void OnWMNCPaint(ref Message m)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }

            Rectangle bounds = RealWindowRectangle;

            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }

            int getDCEXFlags = (int)(DCX.WINDOW | DCX.CACHE | DCX.CLIPSIBLINGS | DCX.VALIDATE);
            IntPtr hRegion = IntPtr.Zero;

            if (m.WParam != (IntPtr)1)
            {
                getDCEXFlags |= (int)DCX.INTERSECTRGN;
                hRegion = m.WParam;
            }


            IntPtr hDC = User32.GetDCEx(Handle, hRegion, getDCEXFlags);

            try
            {
                if (hDC != IntPtr.Zero)
                {

                    using (Graphics drawingSurface = Graphics.FromHdc(hDC))
                    {

                        WindowChromePaint(drawingSurface, bounds);

                    }
                }
            }
            finally
            {
                User32.ReleaseDC(m.HWnd, hDC);
            }

            m.Result = Win32.MESSAGE_PROCESS;

        }




        /// <summary>
        /// Process the WM_NCHITTEST message when overriding window chrome.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        /// <returns>True if the message was processed; otherwise false.</returns>
        protected virtual bool OnWMNCHITTEST(ref Message m)
        {
            // Extract the point in screen coordinates
            Point screenPoint = new Point((int)m.LParam.ToInt64());

            // Convert to window coordinates
            Point windowPoint = ScreenToWindow(screenPoint);

            // Perform hit testing
            m.Result = WindowChromeHitTest(windowPoint, false);

            // Message processed, do not pass onto base class for processing
            return true;
        }

        /// <summary>
        /// Process the WM_NCACTIVATE message when overriding window chrome.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        /// <returns>True if the message was processed; otherwise false.</returns>
        protected virtual bool OnWMNCACTIVATE(ref Message m)
        {
            // Cache the new active state
            WindowActive = (m.WParam == (IntPtr)(1));

            InvalidateNonClient();

            // The first time an MDI child gets an WM_NCACTIVATE, let it process as normal
            if ((MdiParent != null) && !_activated)
                _activated = true;
            else
            {
                // Allow default processing of activation change
                m.Result = (IntPtr)(1);
                // Message processed, do not pass onto base class for processing
                return true;
            }

            return false;
        }

        /// <summary>
        /// Process a windows message that requires the non client area be repainted.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        /// <returns>True if the message was processed; otherwise false.</returns>
        protected virtual bool OnPaintNonClient(ref Message m)
        {
            // Let window be updated with new text
            DefWndProc(ref m);

            // Need a repaint to show change
            InvalidateNonClient();

            // Message processed, do not pass onto base class for processing
            return true;
        }

        /// <summary>
        /// Process the WM_NCMOUSEMOVE message when overriding window chrome.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        /// <returns>True if the message was processed; otherwise false.</returns>
        protected virtual bool OnWMNCMOUSEMOVE(ref Message m)
        {
            // Extract the point in screen coordinates
            Point screenPoint = new Point((int)m.LParam.ToInt64());

            // Convert to window coordinates
            Point windowPoint = ScreenToWindow(screenPoint);


            // Perform actual mouse movement actions
            WindowChromeNonClientMouseMove(windowPoint);

            // If we are not tracking when the mouse leaves the non-client window
            if (!_trackingMouse)
            {
                TRACKMOUSEEVENTS tme = new TRACKMOUSEEVENTS();

                // This structure needs to know its own size in bytes
                tme.cbSize = (uint)Marshal.SizeOf(typeof(TRACKMOUSEEVENTS));
                tme.dwHoverTime = 100;

                // We need to know then the mouse leaves the non client window area
                tme.dwFlags = (int)(TMEFlags.TME_LEAVE | TMEFlags.TME_NONCLIENT);

                // We want to track our own window
                tme.hWnd = Handle;

                // Call Win32 API to start tracking
                User32.TrackMouseEvent(ref tme);

                // Do not need to track again until mouse reenters the window
                _trackingMouse = true;
            }

            // Indicate that we processed the message
            m.Result = IntPtr.Zero;

            // Message processed, do not pass onto base class for processing
            return true;
        }


        /// <summary>
        /// Process the WM_NCLBUTTONDOWN message when overriding window chrome.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>4
        /// <returns>True if the message was processed; otherwise false.</returns>
        protected virtual bool OnWMNCLBUTTONDOWN(ref Message m)
        {
            // Extract the point in screen coordinates
            Point screenPoint = new Point((int)m.LParam.ToInt64());

            // Convert to window coordinates
            Point windowPoint = ScreenToWindow(screenPoint);

            // Perform actual mouse down processing
            return WindowChromeLeftMouseDown(windowPoint);
        }

        /// <summary>
        /// Process the WM_LBUTTONUP message when overriding window chrome.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        /// <returns>True if the message was processed; otherwise false.</returns>
        protected virtual bool OnWMNCLBUTTONUP(ref Message m)
        {
            // Extract the point in screen coordinates
            Point screenPoint = new Point((int)m.LParam.ToInt64());

            // Convert to window coordinates
            Point windowPoint = ScreenToWindow(screenPoint);


            // Perform actual mouse up processing
            return WindowChromeLeftMouseUp(windowPoint);
        }

        private bool OnWMNCLBUTTONDBLCLK(ref Message m)
        {
            return true;
        }

        /// <summary>
        /// Process the WM_NCMOUSELEAVE message when overriding window chrome.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        /// <returns>True if the message was processed; otherwise false.</returns>
        protected virtual bool OnWMNCMOUSELEAVE(ref Message m)
        {
            // Next time the mouse enters the window we need to track it leaving
            _trackingMouse = false;

            // Perform actual mouse leave actions
            WindowChromeMouseLeave();

            // Indicate that we processed the message
            m.Result = IntPtr.Zero;

            // Need a repaint to show change
            InvalidateNonClient();

            // Message processed, do not pass onto base class for processing
            return true;
        }

        /// <summary>
        /// Process the OnWM_MOUSEMOVE message when overriding window chrome.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        /// <returns>True if the message was processed; otherwise false.</returns>
        protected virtual bool OnWMMOUSEMOVE(ref Message m)
        {
            // Extract the point in client coordinates
            Point clientPoint = new Point((int)m.LParam);

            // Convert to screen coordinates
            Point screenPoint = PointToScreen(clientPoint);

            // Convert to window coordinates
            Point windowPoint = ScreenToWindow(screenPoint);

            // Perform actual mouse movement actions
            WindowChromeNonClientMouseMove(windowPoint);

            return true;
        }

        /// <summary>
        /// Process the WM_LBUTTONUP message when overriding window chrome.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        /// <returns>True if the message was processed; otherwise false.</returns>
        protected virtual bool OnWMLBUTTONUP(ref Message m)
        {
            // Capture has now expired
            _captured = false;
            Capture = false;

            // No longer have a target element for events
            //_capturedElement = null;

            // Next time the mouse enters the window we need to track it leaving
            _trackingMouse = false;

            // Extract the point in client coordinates
            Point clientPoint = new Point((int)m.LParam);

            // Convert to screen coordinates
            Point screenPoint = PointToScreen(clientPoint);

            // Convert to window coordinates
            Point windowPoint = ScreenToWindow(screenPoint);


            // Need a repaint to show change
            InvalidateNonClient();

            return true;
        }

        /// <summary>
        /// Called when the active state of the window changes.
        /// </summary>
        protected virtual void OnWindowActiveChanged()
        {
            WindowActiveChanged?.Invoke(this, EventArgs.Empty);
        }


        /// <summary>
        /// Perform hit testing.
        /// </summary>
        /// <param name="pt">Point in window coordinates.</param>
        /// <param name="composition">Are we performing composition.</param>
        /// <returns></returns>
        protected virtual IntPtr WindowChromeHitTest(Point pt, bool composition)
        {
            return (IntPtr)HitTest.HTCLIENT;
        }


        /// <summary>
        /// Perform painting of the window chrome.
        /// </summary>
        /// <param name="g">Graphics instance to use for drawing.</param>
        /// <param name="bounds">Bounds enclosing the window chrome.</param>
        protected virtual void WindowChromePaint(Graphics g, Rectangle bounds)
        {
            RECT clientRect = new RECT();
            User32.GetClientRect(Handle, ref clientRect);
            Rectangle clientBounds = new Rectangle(clientRect.left, clientRect.top, clientRect.right, clientRect.bottom);
            clientBounds.Offset(-clientBounds.Left, -clientBounds.Top);
            clientBounds.Offset(ClientMargin.Left, ClientMargin.Top);

            var chromeRegion = new Region(bounds);

            if (WindowState == FormWindowState.Maximized)
            {
                chromeRegion.Exclude(bounds);
            }
            else
            {
                chromeRegion.Exclude(clientBounds);
            }

            var borderColor = WindowActive ? BorderColor : InactiveBorderColor;

            g.FillRegion(new SolidBrush(borderColor), chromeRegion);
        }

        /// <summary>
        /// Perform non-client mouse movement processing.
        /// </summary>
        /// <param name="pt">Point in window coordinates.</param>
        protected virtual void WindowChromeNonClientMouseMove(Point pt)
        {

        }

        /// <summary>
        /// Process the left mouse down event.
        /// </summary>
        /// <param name="pt">Window coordinate of the mouse up.</param>
        /// <returns>True if event is processed; otherwise false.</returns>
        protected virtual bool WindowChromeLeftMouseDown(Point pt)
        {


            return false;
        }

        /// <summary>
        /// Process the left mouse up event.
        /// </summary>
        /// <param name="pt">Window coordinate of the mouse up.</param>
        /// <returns>True if event is processed; otherwise false.</returns>
        protected virtual bool WindowChromeLeftMouseUp(Point pt)
        {
            // By default, we have not handled the mouse up event
            return false;
        }

        /// <summary>
        /// Perform mouse leave processing.
        /// </summary>
        protected virtual void WindowChromeMouseLeave()
        {

        }



        #endregion

        ///// <summary>
        ///// Releases all resources used by the Control. 
        ///// </summary>
        ///// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    _disposing = true;

        //    if (disposing)
        //    {
        //        // Must unhook from the palette paint events

        //    }

        //    base.Dispose(disposing);


        //    if (_screenDC != IntPtr.Zero)
        //        Win32.DeleteDC(_screenDC);
        //}
    }
}
