using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TPlayer.Web
{
    public class VideoFormatUtil
    {
        private static string[] videoExtensionList = new string[] { "m3u8", "mp4", "flv", "mpeg" };

        private static List<VideoFormat> videoFormatList = new List<VideoFormat>{
            new VideoFormat("m3u8",new List<string>{"application/octet-stream", "application/vnd.apple.mpegurl", "application/mpegurl", "application/x-mpegurl", "audio/mpegurl", "audio/x-mpegurl" }),
            new VideoFormat("mp4",new List<string>{"video/mp4", "application/mp4", "video/h264" }),
            new VideoFormat("flv", new List<string>{"video/x-flv" }),
            new VideoFormat("f4v", new List<string>{"video/x-f4v" }),
            new VideoFormat("mpeg", new List<string>{"video/vnd.mpegurl" })
        };

        public static bool containsVideoExtension(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                foreach (string item in videoExtensionList)
                {
                    if (url.Contains(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool isLikeVideo(string fullUrl)
        {
            try
            {
                Uri urlObject = new Uri(fullUrl);
                string extension = Path.GetExtension(urlObject.AbsolutePath);
                if (string.IsNullOrEmpty(extension))
                {
                    return true;
                }
                if (videoExtensionList.Contains(extension.ToLower()))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static VideoFormat detectVideoFormat(string url, string mime)
        {
            try
            {
                string path = new Uri(url).AbsolutePath;
                string extension = Path.GetExtension(path);
                if (".mp4".Equals(extension))
                {
                    mime = "video/mp4";
                }
            }
            catch
            {
                return null;
            }

            mime = mime.ToLower();
            foreach (VideoFormat videoFormat in videoFormatList)
            {
                if (!string.IsNullOrEmpty(mime))
                {
                    foreach (string mimePattern in videoFormat.MimeList)
                    {
                        if (mime.Contains(mimePattern))
                        {
                            return videoFormat;
                        }
                    }
                }
            }
            return null;
        }
    }

    public class VideoFormat
    {
        private string name;
        private List<string> mimeList;

        public VideoFormat(string name, List<string> mimeList)
        {
            this.name = name;
            this.mimeList = mimeList;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }


        public List<string> MimeList
        {
            get
            {
                return mimeList;
            }
            set
            {
                this.mimeList = value;
            }
        }
    }

    public class VideoInfo
    {
        private string fileName;
        private string url;
        private VideoFormat videoFormat;
        private long size;//单位byte m3u8不显示
        private double duration;//单位s m3u8专用
        private string sourcePageUrl;//原网页url
        private string sourcePageTitle;//原网页标题

        public string getFileName()
        {
            return fileName;
        }

        public void setFileName(string fileName)
        {
            this.fileName = fileName;
        }

        public string getUrl()
        {
            return url;
        }

        public void setUrl(string url)
        {
            this.url = url;
        }

        public VideoFormat getVideoFormat()
        {
            return videoFormat;
        }

        public void setVideoFormat(VideoFormat videoFormat)
        {
            this.videoFormat = videoFormat;
        }

        public long getSize()
        {
            return size;
        }

        public void setSize(long size)
        {
            this.size = size;
        }

        public double getDuration()
        {
            return duration;
        }

        public void setDuration(double duration)
        {
            this.duration = duration;
        }

        public string getSourcePageUrl()
        {
            return sourcePageUrl;
        }

        public void setSourcePageUrl(string sourcePageUrl)
        {
            this.sourcePageUrl = sourcePageUrl;
        }

        public string getSourcePageTitle()
        {
            return sourcePageTitle;
        }

        public void setSourcePageTitle(string sourcePageTitle)
        {
            this.sourcePageTitle = sourcePageTitle;
        }
    }
}
