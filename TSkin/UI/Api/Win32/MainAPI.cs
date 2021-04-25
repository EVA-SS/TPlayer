using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TSkin.MainApi
{
    public static class MainApi
    {
        /// <summary>
        /// 样式绘制圆角
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="bounds">范围</param>
        /// <param name="radius">圆角大小</param>
        /// <param name="roundStyle">圆角样式</param>
        public static void CreateRegion(
           this  Control control,
             Rectangle bounds,
             int radius,
             UICornerRadiusSides roundStyle)
        {
            using (GraphicsPath path = bounds.CreateRoundedRectanglePath(radius, roundStyle))
            {
                Region region = new Region(path);
                path.Widen(Pens.White);
                region.Union(path);
                control.Region = region;
            }
        }
    }
    public static class Result
    {
        public static readonly IntPtr TRUE = new IntPtr(1);
        public static readonly IntPtr FALSE = new IntPtr(0);
    }

    #region NativeMethods

    public class NativeMethods
    {
        private NativeMethods()
        {
        }
        #region 属性变换

        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size(Int32 x, Int32 y)
            {
                cx = x;
                cy = y;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(Int32 x, Int32 y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public const Int32 ULW_ALPHA = 2;
        #endregion

        #region USER32.DLL
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);


        [DllImport("user32.dll")]
        public static extern IntPtr TrackPopupMenu(
            IntPtr hMenu, int uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr par);


        [DllImport("user32", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(
            IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(
            IntPtr hwnd, int nIndex, int dwNewLong);


        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr handle);


        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr handle, IntPtr hdc);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(
            IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public extern static int SendMessage(
            IntPtr hWnd, int msg, int wParam, int lParam);



        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst,
            ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc,
            Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);


        #endregion

        #region GDI32.DLL

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);

        #endregion
    }

    #endregion

    #region Const

    public static class AC
    {
        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;
    }
    public static class GWL
    {
        public const int GWL_EXSTYLE = -20;
    }
    public class HITTEST
    {
        public const int HTNOWHERE = 0;
        public const int HTCLIENT = 1;
        public const int HTCAPTION = 2;
        public const int HTSYSMENU = 3;
        public const int HTGROWBOX = 4;
        public const int HTMENU = 5;
        public const int HTHSCROLL = 6;
        public const int HTVSCROLL = 7;
        public const int HTMINBUTTON = 8;
        public const int HTMAXBUTTON = 9;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTBORDER = 18;
        public const int HTHELP = 21;
    }

    public sealed class TPM
    {
        public const int TPM_LEFTALIGN = 0x0000;
        public const int TPM_TOPALIGN = 0x0000;
        public const int TPM_RETURNCMD = 0x0100;
    }


    public static class WM
    {
        public const int WM_GETMINMAXINFO = 0x0024;
        public const int WM_WINDOWPOSCHANGED = 0x0047;
        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_NCRBUTTONUP = 0x00A5;
        public const int WM_NCUAHDRAWFRAME = 0x00AF;
        public const int WM_NCUAHDRAWCAPTION = 0x00AE;
        public const int WM_SYSCOMMAND = 0x0112;
    }


    public static class WS_EX
    {
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_LAYERED = 0x00080000;

    }
    #endregion

    #region Struct


    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public Point reserved;
        public Size maxSize;
        public Point maxPosition;
        public Size minTrackSize;
        public Size maxTrackSize;
    }

    #endregion
}
