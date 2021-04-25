//using System;
//using System.Collections.Generic;
//using System.IO.MemoryMappedFiles;
//using System.Runtime.InteropServices;
//using System.Text;

//namespace TPlayerDownLib
//{
//    public class DownLib
//    {
//        public static string Key = "840100";

//        public static bool SendTPlayer(List<PlayVideo> data, int index = 0)
//        {
//            int ind = 841000;
//            IntPtr mainind = GetMemory(ind);
//            int errCount = 0;
//            while (mainind == IntPtr.Zero && errCount < 10)
//            {
//                errCount++;
//                ind += 1;
//                mainind = GetMemory(ind);
//            }

//            if (mainind == IntPtr.Zero)
//            {
//                return false;
//            }
//            else
//            {
//                string strURL = "play1_" + index + "|" + Newtonsoft.Json.JsonConvert.SerializeObject(data);
//                CopyDataStruct cds;
//                cds.dwData = (IntPtr)1; //这里可以传入一些自定义的数据，但只能是4字节整数      
//                cds.lpData = strURL;//消息字符串
//                cds.cbData = Encoding.Default.GetBytes(strURL).Length + 1;  //注意，这里的长度是按字节来算的
//                SendMessage(mainind, 0x004A, 0, ref cds);// 这里要修改成接收窗口的标题“接收端”
//                return true;
//            }
//        }
//        public static bool RunDownLoad
//        {
//            get
//            {
//                IntPtr mainind = GetMemory(Key);
//                return mainind != IntPtr.Zero;
//            }
//        }
//        public static bool SendDownLoad(VideoTask data)
//        {
//            IntPtr mainind = GetMemory(Key);
//            if (mainind == IntPtr.Zero)
//            {
//                return false;
//            }
//            else
//            {
//                string strURL = "task1_" + Newtonsoft.Json.JsonConvert.SerializeObject(data);
//                CopyDataStruct cds;
//                cds.dwData = (IntPtr)1; //这里可以传入一些自定义的数据，但只能是4字节整数      
//                cds.lpData = strURL;//消息字符串
//                cds.cbData = Encoding.Default.GetBytes(strURL).Length + 1;  //注意，这里的长度是按字节来算的
//                SendMessage(mainind, 0x004A, 0, ref cds);// 这里要修改成接收窗口的标题“接收端”
//                return true;
//            }

//        }
//        //在DLL库中的发送消息函数
//        [DllImport("User32.dll", EntryPoint = "SendMessage")]
//        private static extern int SendMessage(
//            IntPtr hWnd,                         // 目标窗口的句柄  
//            int Msg,                          // 在这里是WM_COPYDATA
//            int wParam,                       // 第一个消息参数
//            ref CopyDataStruct lParam        // 第二个消息参数
//           );


//        static IntPtr GetMemory(int key)
//        { return GetMemory(key.ToString()); }
//        static IntPtr GetMemory(string key)
//        {
//            try
//            {
//                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(key))
//                {
//                    using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
//                    {
//                        IntPtr handler = IntPtr.Zero;
//                        accessor.Read(0, out handler);
//                        return handler;
//                    }
//                }
//            }
//            catch
//            {
//                return IntPtr.Zero;
//            }
//        }


//    }
//    public class PlayVideo
//    {
//        /// <summary>
//        /// 影视名称
//        /// </summary>
//        public string name { get; set; }
//        /// <summary>
//        /// 影视地址
//        /// </summary>
//        public string url { get; set; }
//    }
//    public class VideoTask
//    {
//        /// <summary>
//        /// 影视名称
//        /// </summary>
//        public string name { get; set; }
//        /// <summary>
//        /// 影视封面
//        /// </summary>
//        public string cover { get; set; }
//        /// <summary>
//        /// 简介
//        /// </summary>
//        public string intro { get; set; }
//        /// <summary>
//        /// 下载任务
//        /// </summary>
//        public List<VideoTaskList> list { get; set; }
//    }
//    public class VideoTaskList
//    {
//        /// <summary>
//        /// 集名称
//        /// </summary>
//        public string name { get; set; }
//        /// <summary>
//        /// 集下载地址
//        /// </summary>
//        public string url { get; set; }
//        public string fileName { get; set; }
//        /// <summary>
//        /// 缓存地址
//        /// </summary>
//        public string tempPath { get; set; }

//        /// <summary>
//        /// 保存路径
//        /// </summary>
//        public string savePath { get; set; }
//    }
//    #region 发送
//    struct CopyDataStruct
//    {
//        public IntPtr dwData;
//        public int cbData;

//        [MarshalAs(UnmanagedType.LPStr)]

//        public string lpData;
//    }

//    #endregion
//}
