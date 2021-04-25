using System;
using System.Drawing;
using System.Runtime.InteropServices;

/// <summary>
/// Wind32API����
/// </summary>
namespace TSkin
{
    public class Win32
    {
        public static void SetBits(Bitmap bitmap, Rectangle top_rect, IntPtr intPtr)
        {
            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);
            try
            {
                Win32.Point topLoc = new Win32.Point(top_rect.X, top_rect.Y);
                Win32.Size topSize = new Win32.Size(top_rect.Width, top_rect.Height);
                //Win32.Point srcLoc = new Win32.Point(src_rect.X, src_rect.Y);
                Win32.Point srcLoc = new Win32.Point(0, 0);

                //Win32.Size bitMapSize = new Win32.Size(src_rect.Width, src_rect.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;
                Win32.UpdateLayeredWindow(intPtr, screenDC, ref topLoc, ref topSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        }
        public const int WM_CONTEXTMENU = 0x007B;
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_LAYERED = 0x00080000;

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

        public const byte AC_SRC_OVER = 0;
        public const Int32 ULW_ALPHA = 2;
        public const byte AC_SRC_ALPHA = 1;

        /// <summary>
        /// ��������ʾ
        /// </summary>
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        /// <summary>
        /// ���ҵ�����ʾ
        /// </summary>
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        /// <summary>
        /// ���ϵ�����ʾ
        /// </summary>
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        /// <summary>
        /// ���µ�����ʾ
        /// </summary>
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        /// <summary>
        /// ��ʹ����AW_HIDE��־����ʹ���������ص������������ڣ�����ʹ����������չ����չ������
        /// </summary>
        public const Int32 AW_CENTER = 0x00000010;
        /// <summary>
        /// ���ش��ڣ�ȱʡ����ʾ����
        /// </summary>
        public const Int32 AW_HIDE = 0x00010000;
        /// <summary>
        /// ����ڡ���ʹ����AW_HIDE��־����ʹ�������־
        /// </summary>
        public const Int32 AW_ACTIVATE = 0x00020000;
        /// <summary>
        /// ʹ�û������͡�ȱʡ��Ϊ�����������͡���ʹ��AW_CENTER��־ʱ�������־�ͱ�����
        /// </summary>
        public const Int32 AW_SLIDE = 0x00040000;
        /// <summary>
        /// ͸���ȴӸߵ���
        /// </summary>
        public const Int32 AW_BLEND = 0x00080000;

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);
        /// <summary>
        /// ִ�ж���
        /// </summary>
        /// <param name="whnd">�ؼ����</param>
        /// <param name="dwtime">����ʱ��</param>
        /// <param name="dwflag">�����������</param>
        /// <returns>boolֵ�������Ƿ�ɹ�</returns>
        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);

        /// <summary>
        /// <para>�ú�����ָ������Ϣ���͵�һ���������ڡ�</para>
        /// <para>�˺���Ϊָ���Ĵ��ڵ��ô��ڳ���ֱ�����ڳ���������Ϣ�ٷ��ء�</para>
        /// <para>������PostMessage��ͬ����һ����Ϣ���͵�һ���̵߳���Ϣ���к��������ء�</para>
        /// return ����ֵ : ָ����Ϣ����Ľ���������������͵���Ϣ��
        /// </summary>
        /// <param name="hWnd">Ҫ������Ϣ���Ǹ����ڵľ��</param>
        /// <param name="Msg">��Ϣ�ı�ʶ��</param>
        /// <param name="wParam">����ȡ������Ϣ</param>
        /// <param name="lParam">����ȡ������Ϣ</param>
        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessageA")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(
            IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(
            IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteObject(IntPtr hObj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd">һ���ֲ㴰�ڵľ�����ֲ㴰������CreateWindowEx������������ʱӦָ��WS_EX_LAYERED��չ��ʽ�� Windows 8�� WS_EX_LAYERED��չ��ʽ֧�ֶ������ں��Ӵ��ڡ�֮ǰ��Windows�汾��WS_EX_LAYERED��չ��ʽ��֧�ֶ�������</param>
        /// <param name="hdcDst">��Ļ���豸������(DC)��������ָ��ΪNULL����ô�����ڵ��ú���ʱ�Լ���á��������ڴ������ݸ���ʱ���ɫ����ɫƥ�䡣���hdcDstΪNULL������ʹ��Ĭ�ϵ�ɫ�塣���hdcSrcָ��ΪNULL����ôhdcDst����ָ��ΪNULL��</param>
        /// <param name="pptDst">ָ��ֲ㴰���������Ļ��λ�õ�POINT�ṹ��ָ�롣������ֵ�ǰλ�ò��䣬pptDst����ָ��ΪNULL��</param>
        /// <param name="psize">ָ��ֲ㴰�ڵĴ�С��SIZE�ṹ��ָ�롣������ڵĴ�С���ֲ��䣬psize����ָ��ΪNULL�����hdcSrcָ��ΪNULL��psize����ָ��ΪNULL��</param>
        /// <param name="hdcSrc">�ֲ㴰�ڻ�ͼ������豸�����ľ��������������ͨ�����ú���CreateCompatibleDC��á�������ڵ���״�Ϳ��ӷ�Χ���ֲ��䣬hdcSrc����ָ��ΪNULL��</param>
        /// <param name="pptSrc">ָ��ֲ㴰�ڻ�ͼ�������豸������λ�õ�POINT�ṹ��ָ�롣���hdcSrcָ��ΪNULL��pptSrc��Ӧ��ָ��ΪNULL��</param>
        /// <param name="crKey">ָ���ϳɷֲ㴰��ʱʹ�õ���ɫֵ��Ҫ����һ������ΪCOLORREF��ֵ��ʹ��RGB�ꡣ</param>
        /// <param name="pblend">ָ��ָ���ϳɷֲ㴰��ʱʹ�õ�͸���Ƚṹ��ָ�롣</param>
        /// <param name="dwFlags">����������ֵ֮һ�����hdcSrcָ��ΪNULL��dwFlagsӦ��ָ��Ϊ0��</param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);

        /// <summary>
        /// ����API����,��������:ָ�����ڵ�ָ��λ�ò�������.
        /// </summary>
        /// <param name="hwnd">���ھ��</param>
        /// <param name="hBitmap">���������ʽ,����ΪNULL</param>
        /// <param name="nWidth">������Ŀ��</param>
        /// <param name="nHeight">������ĸ߶�</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "CreateCaret")]
        public static extern int CreateCaret(IntPtr hwnd, int hBitmap, int nWidth, int nHeight);

        /// <summary>
        /// ��ʾ�����
        /// </summary>
        /// <param name="hWnd">���ھ��</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ShowCaret")]
        public static extern bool ShowCaret(IntPtr hWnd);

        [DllImport("User32.dll", EntryPoint = "SetCaretPos")]
        public static extern bool SetCaretPos(int x, int y);



        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_LBUTTONDBLCLK = 0x0203;

        public const int WM_MOUSELEAVE = 0x02A3;



        public const int WM_PAINT = 0x000F;
        public const int WM_ERASEBKGND = 0x0014;

        public const int WM_PRINT = 0x0317;

        //const int EN_HSCROLL       =   0x0601;
        //const int EN_VSCROLL       =   0x0602;

        public const int WM_HSCROLL = 0x0114;
        public const int WM_VSCROLL = 0x0115;


        public const int EM_GETSEL = 0x00B0;
        public const int EM_LINEINDEX = 0x00BB;
        public const int EM_LINEFROMCHAR = 0x00C9;

        public const int EM_POSFROMCHAR = 0x00D6;



        [DllImport("USER32.DLL", EntryPoint = "PostMessage")]
        public static extern bool PostMessage(IntPtr hwnd, uint msg,
            IntPtr wParam, IntPtr lParam);

        /*
            BOOL PostMessage(          HWND hWnd,
                UINT Msg,
                WPARAM wParam,
                LPARAM lParam
                );
        */

        // Put this declaration in your class   //IntPtr
        [DllImport("USER32.DLL", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam,
            IntPtr lParam);




        [DllImport("USER32.DLL", EntryPoint = "GetCaretBlinkTime")]
        public static extern uint GetCaretBlinkTime();




        const int WM_PRINTCLIENT = 0x0318;

        const long PRF_CHECKVISIBLE = 0x00000001L;
        const long PRF_NONCLIENT = 0x00000002L;
        const long PRF_CLIENT = 0x00000004L;
        const long PRF_ERASEBKGND = 0x00000008L;
        const long PRF_CHILDREN = 0x00000010L;
        const long PRF_OWNED = 0x00000020L;

        /*  Will clean this up later doing something like this
        enum  CaptureOptions : long
        {
            PRF_CHECKVISIBLE= 0x00000001L,
            PRF_NONCLIENT	= 0x00000002L,
            PRF_CLIENT		= 0x00000004L,
            PRF_ERASEBKGND	= 0x00000008L,
            PRF_CHILDREN	= 0x00000010L,
            PRF_OWNED		= 0x00000020L
        }
        */


        public static bool CaptureWindow(System.Windows.Forms.Control control,
                                ref System.Drawing.Bitmap bitmap)
        {
            //This function captures the contents of a window or control

            Graphics g2 = Graphics.FromImage(bitmap);

            //PRF_CHILDREN // PRF_NONCLIENT
            int meint = (int)(PRF_CLIENT | PRF_ERASEBKGND); //| PRF_OWNED ); //  );
            System.IntPtr meptr = new System.IntPtr(meint);

            System.IntPtr hdc = g2.GetHdc();
            Win32.SendMessage(control.Handle, Win32.WM_PRINT, hdc, meptr);

            g2.ReleaseHdc(hdc);
            g2.Dispose();

            return true;

        }

    }
}