using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TPlayerAssist
{
    public class LinkFile
    {
        public static string[] FileNameWhoInvokeEXE = null;
        public const int WM_COPYDATA = 0x4a;

        public static bool DeleteRightMenuLink(string MenuName)
        {
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"*\shell", true))
                {
                    key.DeleteSubKeyTree(MenuName);
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        public static bool DelLink(string TypeName)
        {
            try
            {
                using (RegistryKey classesRoot = Registry.ClassesRoot)
                {
                    classesRoot.DeleteSubKeyTree(TypeName);
                }
                SHChangeNotify(0x8000000, 0, IntPtr.Zero, IntPtr.Zero);
                return true;
            }
            catch
            {
            }
            return false;
        }

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);
        public static void SendStr(IntPtr hWnd, string str)
        {
            try
            {
                COPYDATASTRUCT copydatastruct;
                int length = Encoding.Default.GetBytes(str).Length;
                copydatastruct.dwData = (IntPtr)100;
                copydatastruct.lpData = str;
                copydatastruct.cbData = length + 1;
                SendMessage(hWnd, 0x4a, 0, ref copydatastruct);
            }
            catch
            {
            }
        }

        public static bool SetLink(string ExName, string TypeName, string IconPath, string AppPath)
        {
            try
            {
                using (RegistryKey classesRoot = Registry.ClassesRoot)
                {
                    classesRoot.CreateSubKey(ExName);
                    classesRoot.DeleteSubKeyTree(ExName);
                    classesRoot.CreateSubKey(ExName);
                    using (RegistryKey key2 = classesRoot.OpenSubKey(ExName, true))
                    {
                        key2.SetValue("", TypeName);
                        key2.SetValue("Content Type", TypeName);
                        classesRoot.CreateSubKey(TypeName);
                        classesRoot.DeleteSubKeyTree(TypeName);
                        classesRoot.CreateSubKey(TypeName);
                        using (RegistryKey key3 = classesRoot.OpenSubKey(TypeName, true))
                        {
                            key3.SetValue("", TypeName);
                            if (File.Exists(IconPath))
                            {
                                key3.CreateSubKey("DefaultIcon").SetValue("", "\"" + IconPath + "\",0");
                            }
                            else
                            {
                                key3.CreateSubKey("DefaultIcon").SetValue("", "\"" + AppPath + "\",0");
                            }
                            key3.CreateSubKey(@"shell\Open\Command").SetValue("", "\"" + AppPath + "\" \"%1\"");
                        }
                    }
                }
                SHChangeNotify(0x8000000, 0, IntPtr.Zero, IntPtr.Zero);
                return true;
            }
            catch
            {
            }
            return false;
        }

        public static bool SetRightMenuLink(string MenuName, string AppPath)
        {
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"*\shell", true))
                {
                    key.CreateSubKey(MenuName).CreateSubKey("command").SetValue("", "\"" + AppPath + "\"\" %1\"");
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
    }
}
