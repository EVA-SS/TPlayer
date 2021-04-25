using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TPlayerAssist
{
    static class Program
    {
        public static string ExePath = Application.ExecutablePath;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                MessageBox.Show(string.Join(" ", args));
                MessageBox.Show(Encoding.Default.GetString(Convert.FromBase64String(args[0])));
                AppMain appMain = (Encoding.Default.GetString(Convert.FromBase64String(args[0]))).ToJson<AppMain>();
                if (appMain != null)
                {
                    switch (appMain.action)
                    {
                        case "SetLink":
                            SetLink(appMain.appPath, appMain.json.ToJson<AppMainSetLink>());
                            break;
                        case "Update":
                            Update(appMain.appPath, appMain.json.ToJson<AppMainUpdate>());
                            break;
                    }
                }
            }
        }
        static void SetLink(string appPath, AppMainSetLink args)
        {
            if (args != null)
            {
                FileInfo fileInfo = new FileInfo(appPath);
                //DirectoryInfo directoryInfo = new DirectoryInfo(fileInfo.DirectoryName);
                if (args.set != null && args.set.Count > 0)
                {
                    foreach (string name in args.set)
                    {
                        if (!string.IsNullOrEmpty(name.Trim()))
                        {
                            string str = name.TrimStart('.');
                            string iconPath = fileInfo.DirectoryName + "\\icon\\" + str + ".ico";
                            if (!File.Exists(iconPath))
                            {
                                iconPath = appPath;
                            }
                            LinkFile.SetLink(name, "TPlayer" + name, iconPath, appPath);
                        }
                    }
                }
                if (args.del != null && args.del.Count > 0)
                {
                    foreach (string name in args.del)
                    {
                        if (!string.IsNullOrEmpty(name.Trim()))
                        {
                            LinkFile.DelLink("TPlayer" + name);
                        }
                    }
                }
            }
        }

        static void Update(string appPath, AppMainUpdate args)
        {
            if (args != null)
            {
                FileInfo fileInfo = new FileInfo(appPath);
                //MessageBox.Show(string.Join("|", files));
                foreach (string item in args.files)
                {
                    FileInfo fileInfos = new FileInfo(fileInfo.DirectoryName + "\\" + item);
                    if (fileInfos.Exists)
                    {
                        int errCount = 0;
                        bool isRun = true;
                        while (isRun)
                        {
                            try
                            {
                                File.Delete(fileInfos.FullName);
                                isRun = false;
                            }
                            catch
                            {
                                System.Threading.Thread.Sleep(500);
                            }
                            if (errCount > 4)
                            {
                                isRun = false;
                            }
                        }
                    }
                    else if (!Directory.Exists(fileInfos.DirectoryName))
                    {
                        Directory.CreateDirectory(fileInfos.DirectoryName);
                    }
                    File.Move(args.basePath + item, fileInfo.DirectoryName + "\\" + item);
                }
                Directory.Delete(args.basePath, true);

                Process.Start(appPath, "UpdateOk_" + Convert.ToBase64String(Encoding.UTF8.GetBytes(ExePath)));
            }
        }
    }
    public class AppMain
    {
        public string action { get; set; }
        public string appPath { get; set; }
        public string json { get; set; }
    }
    public class AppMainSetLink
    {
        public List<string> set { get; set; }
        public List<string> del { get; set; }
    }
    public class AppMainUpdate
    {
        public string basePath { get; set; }
        public List<string> files { get; set; }
    }
}
