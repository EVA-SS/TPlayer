using System;
using System.Diagnostics;

namespace TPlayer.Web
{
    public class M3U8Util
    {
        public static double figureM3U8Duration(ref string url)
        {
            string m3U8Content = HttpLib.Http.Get(url).redirect(true).request();
            bool isSubFileFound = false;
            double totalDuration = 0d;
            foreach (string lineString in m3U8Content.Split('\n'))
            {
                string _lineString = lineString.Trim();
                if (isSubFileFound)
                {
                    if (_lineString.StartsWith("#"))
                    {
                        //格式错误 直接返回时长0
                        Debug.WriteLine("M3U8Util", "格式错误1");
                        return 0d;
                    }
                    else
                    {
                        string subFileUrl = new Uri(new Uri(url), _lineString).ToString();
                        url = subFileUrl;
                        return figureM3U8Duration(ref subFileUrl);
                    }
                }
                if (_lineString.StartsWith("#"))
                {
                    if (_lineString.StartsWith("#EXT-X-STREAM-INF"))
                    {
                        isSubFileFound = true;
                        continue;
                    }
                    if (_lineString.StartsWith("#EXTINF:"))
                    {
                        int sepPosition = _lineString.IndexOf(",");
                        if (sepPosition <= "#EXTINF:".Length)
                        {
                            sepPosition = _lineString.Length;
                        }
                        double duration = 0d;
                        try
                        {
                            duration = double.Parse(_lineString.Substring("#EXTINF:".Length, sepPosition - "#EXTINF:".Length).Trim());
                        }
                        catch
                        {
                            //格式错误 直接返回时长0
                            Debug.WriteLine("M3U8Util", "格式错误3");
                            return 0d;
                        }
                        totalDuration += duration;
                    }
                }

            }
            return totalDuration;
        }


    }
}
