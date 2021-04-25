using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TPlayer
{
    public class PlayerInfoJson
    {
        /// <summary>
        /// 视频名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 播放类型
        /// </summary>
        public string PlayType { get; set; }
        /// <summary>
        /// 视频播放地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 视频路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 视频缓存路径
        /// </summary>
        public string FileCache { get; set; }

        /// <summary>
        /// 视频哈希
        /// </summary>
        public string Hash { get; set; }
    }
    public class PlayerItem
    {
        public PlayerItem(string type, string url, string name = null, string colltag = null)
        {
            this.type = type;
            this.url = url;
            this.colltag = colltag;
            if (name == null)
            {
                this.name = Path.GetFileNameWithoutExtension(url.ToVideoName());
            }
            else
            {
                this.name = name;
            }
        }
        public string type { get; set; }
        public string name { get; set; }
        public string time { get; set; }
        public string url { get; set; }
        public string colltag { get; set; }

        public Size videoSize { get; set; }

        #region 本地属性

        /// <summary>
        /// 文件名
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// 文件哈希
        /// </summary>
        public string fileHash { get; set; }
        /// <summary>
        /// 没后缀文件名
        /// </summary>
        public string fileNameNo { get; set; }

        /// <summary>
        /// 视频缓存路径
        /// </summary>
        public string cachePath { get; set; }
        /// <summary>
        /// 视频下载路径
        /// </summary>
        public string videoPath { get; set; }

        /// <summary>
        /// 录制视频路径
        /// </summary>
        public string recordPath { get; set; }

        #endregion
    }
    public class DownItem
    {
        public string ID { get; set; }
        public double FileSize { get; set; }
        public bool isRun { get; set; }
        public bool isComplete { get; set; }
        public string ErrMsg { get; set; }
        public string name { get; set; }
        public string fileName { get; set; }
        public string url { get; set; }
        public string imgUrl { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Image img { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public bool imgOk { get; set; }
        public string basepath { get; set; }
        public string savepath { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Helper.DownCore core { get; set; }


    }

    public class _Buffe
    {
        public _Buffe(int Index)
        {
            this.Index = Index;
        }
        public _Buffe(int Index, int Leng)
        {
            this.Index = Index;
            this.Leng = Leng;
        }
        public int Index { get; set; }
        public int Leng { get; set; }
    }


    public enum PlayFrameStyle
    {
        /// <summary>
        /// 正常
        /// </summary>
        None,
        /// <summary>
        /// 屏幕铺满
        /// </summary>
        Paved,
        _4_3,
        _16_9,
    }

    #region 关联
    public static class AppLink
    {
        public static List<string> Recommend = new List<string> {
            ".asf",".avi",".ram", ".rm", ".rmvb",".mov",".flv", ".f4v", ".dat",".m2t",".mpe", ".mpeg", ".mpg", ".ts",".m4v", ".mp4",".3g2", ".3gp", ".divx", ".mkv", ".mts", ".webm", ".aac", ".ac3", ".amr", ".m4a", ".mid", ".midi", ".mp3", ".ogg", ".wav", ".wma", ".wv"
        };

        public static List<string> Videos = new List<string> {
            "Windows媒体",  "Real媒体","Apple媒体","Flash媒体","MPEG1/2媒体","MPEG4媒体","3GPP媒体","其它视频文件",
        };
        public static List<string> GetAllLinkList
        {
            get
            {
                List<string> sLinkLists = new List<string>();
                foreach (AppLinkList item in AppLink.LinkList)
                {
                    foreach (string items in item.value)
                    {
                        sLinkLists.Add(items);
                    }
                }
                return sLinkLists;
            }
        }

        public static List<AppLinkList> LinkList = new List<AppLinkList> {
            new AppLinkList("Windows媒体",new List<string>{".asf",".avi", ".wm", ".wmp", ".wmv" }),
            new AppLinkList("Real媒体",new List<string>{ ".ram", ".rm", ".rmvb", ".rp", ".rpm", ".rt", ".smi", ".smil"}),

            new AppLinkList("Apple媒体",new List<string>{ ".mov", ".qt"}),
            new AppLinkList("Flash媒体",new List<string>{ ".flv", ".f4v", ".hlv", ".swf" }),
            new AppLinkList("MPEG1/2媒体",new List<string>{ ".dat", ".m1v", ".m2p", ".m2t", ".m2ts", ".m2v", ".mpv2", ".mpe", ".mpeg", ".mpg", ".mp2v", ".pss", ".pva", ".tp", ".tpr", ".ts" }),

            new AppLinkList("MPEG4媒体",new List<string>{ ".m4b", ".m4p", ".m4v", ".mp4", ".mpeg4" }),

            new AppLinkList("3GPP媒体",new List<string>{ ".3g2", ".3gp", ".3gp2", ".3gpp" }),

            new AppLinkList("其它视频文件",new List<string>{ ".amv", ".bik", ".csf", ".divx", ".evo", ".ivm", ".mkv", ".mod", ".mts", ".ogm", ".pmp", ".tod", ".vp6", ".webm", ".xlmv", ".scm" }),
             new AppLinkList("其它音频文件",new List<string>{ ".aac", ".ac3", ".amr", ".cda", ".dts", ".flac", ".m1a", ".m2a", ".m4a", ".mid", ".midi", ".mka", ".mp2", ".mp3", ".mpa", ".ogg", ".ra", ".tak", ".tta", ".wav", ".wma", ".wv", ".acc" }),

            new AppLinkList("CD/DVD媒体",new List<string>{ ".ifo", ".vob",".dvd"}),
            new AppLinkList("字幕文件",new List<string>{ ".srt", ".ass", ".ssa", ".idx", ".sub", ".sup", ".psb", ".usf", ".ssf" })
        };
    }
    public class AppLinkList
    {
        public AppLinkList(string key, List<string> value)
        {
            this.key = key;
            this.value = value;
        }
        public string key { get; set; }
        public List<string> value { get; set; }
        public List<TSkin.TCheckBox> checks { get; set; }
        public int selcount { get; set; }
    }


    #endregion

    #region 管理员运行
    public class AdminAppMain
    {
        public string action { get; set; }
        public string appPath { get; set; }
        public string json { get; set; }
    }
    public class AdminAppMainSetLink
    {
        public AdminAppMainSetLink(List<string> set, List<string> del)
        {
            this.set = set;
            this.del = del;
        }
        public List<string> set { get; set; }
        public List<string> del { get; set; }
    }

    public class AdminAppMainUpdate
    {
        public AdminAppMainUpdate(string basePath, List<string> files)
        {
            this.basePath = basePath;
            this.files = files;
        }
        public string basePath { get; set; }
        public List<string> files { get; set; }
    }
    #endregion
}
