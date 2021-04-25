using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TPlayer
{
    static class Program
    {
        public static string ExePath =  Application.ExecutablePath;
        public static string BasePath = Directory.GetCurrentDirectory() + "\\";
        public static string PluginPath = BasePath + "Plugins\\";
        public static string TempPath = BasePath + "temp\\";
        public static string DownLoadPath = BasePath + "download\\";
        public static string CachePath = BasePath + "cache\\";
        public static string UpdatePath = BasePath + "update\\";
        public static string CodecsPath = BasePath + "codecs\\";
        public static string ScreenshotPath = BasePath + "截图\\";
        public static string IconPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\流水导入工具.lnk";
        public static TPlayer _Main = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] arge)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new TEST1());
            //return;

            List<string> files = new List<string>();
            if (arge.Length > 0)
            {
                string filetemp = "";
                foreach (string item in arge)
                {
                    filetemp += item + " ";
                    if (File.Exists(filetemp.Trim()))
                    {
                        files.Add(filetemp);
                        filetemp = "";
                    }
                }
                filetemp = filetemp.Trim();
                if (files.Count == 0)
                {
                    if (filetemp.StartsWith("UpdateOk_"))
                    {
                        //MessageBox.Show("删除：" + System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(filetemp.Substring(9))));
                        File.Delete(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(filetemp.Substring(9))));
                        //return;
                    }
                    else if (filetemp == "-r codecs")
                    {
                        Application.Run(new DownCodecs());
                        return;
                    }
                }
            }

            int ind = 841000;
            IntPtr mainind = GetMemory(ind);
            if (mainind == IntPtr.Zero)
            {
                StartApp(ind, files);
            }
            else
            {
                if (SystemSettings.Multiple)
                {
                    while (mainind != IntPtr.Zero)
                    {
                        ind += 1;
                        mainind = GetMemory(ind);
                    }
                    StartApp(ind, files);
                }
                else
                {
                    if (files.Count != 0)
                    {
                        string strURL = string.Join("|", files);
                        CopyDataStruct cds;
                        cds.dwData = (IntPtr)1; //这里可以传入一些自定义的数据，但只能是4字节整数      
                        cds.lpData = strURL;//消息字符串
                        cds.cbData = System.Text.Encoding.Default.GetBytes(strURL).Length + 1;  //注意，这里的长度是按字节来算的
                        SendMessage(mainind, 0x004A, 0, ref cds);// 这里要修改成接收窗口的标题“接收端”
                    }
                    HandleRunningInstance(mainind);
                }
            }
            //Application.Run(new Frm.Setting());
        }
        static void StartApp(int ind, List<string> files)
        {
#if DEBUG
#else
            Application.ThreadException += Application_ThreadException; //UI线程异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; //多线程异常
#endif
            _Main = new TPlayer(ind, files);
            Application.Run(_Main);
        }
        //UI线程异常
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Log.LogHelper.LogError(e.Exception);
            BugReport sn = new BugReport(e.Exception);
            sn.ShowDialog();
        }

        //多线程异常
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.LogHelper.LogError((e.ExceptionObject as Exception));
            //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);

            //BugReport sn = new BugReport((Exception)e.ExceptionObject);
            //sn.ShowDialog();
        }

        #region 防止多开

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        private static void HandleRunningInstance(IntPtr hWnd)
        {
            // 确保窗口没有被最小化或最大化   
            //ShowWindowAsync(hWnd, 4);
            // 设置真实例程为foreground  window    
            SetForegroundWindow(hWnd);// 放到最前端   
        }
        static IntPtr GetMemory(int key)
        { return GetMemory(key.ToString()); }
        static IntPtr GetMemory(string key)
        {
            try
            {
                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(key))
                {
                    using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                    {
                        IntPtr handler = IntPtr.Zero;
                        accessor.Read(0, out handler);
                        return handler;//<span style="font-family: Arial, Helvetica, sans-serif;">这里的handler就是我们窗口句柄</span>  
                    }
                }
            }
            catch
            {
                return IntPtr.Zero;
            }
        }
        #endregion

        #region 发送
        public struct CopyDataStruct
        {
            public IntPtr dwData;
            public int cbData;

            [MarshalAs(UnmanagedType.LPStr)]

            public string lpData;
        }
        //在DLL库中的发送消息函数
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
            IntPtr hWnd,                         // 目标窗口的句柄  
            int Msg,                          // 在这里是WM_COPYDATA
            int wParam,                       // 第一个消息参数
            ref CopyDataStruct lParam        // 第二个消息参数
           );

        #endregion
    }
}
