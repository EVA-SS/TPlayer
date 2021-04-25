using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace TPlayer.Helper
{
    class Parser
    {
        //METHOD, KEY, IV
        string[] m3u8CurrentKey = new string[] { "NONE", "", "" };
        public string m3u8SavePath = string.Empty;
        public string jsonSavePath = string.Empty;
        private string baseUrl = string.Empty;
        private string m3u8Url = string.Empty;
        private string downDir = string.Empty;
        private string downName = string.Empty;
        private bool liveStream = false;
        private long bestBandwidth = 0;
        private string bestUrl = string.Empty;
        private string bestUrlAudio = string.Empty;
        private string bestUrlSub = string.Empty;
        Dictionary<string, string> MEDIA_AUDIO = new Dictionary<string, string>();
        private string audioUrl = string.Empty; //音轨地址
        Dictionary<string, string> MEDIA_SUB = new Dictionary<string, string>();
        private string subUrl = string.Empty; //字幕地址
        //存放多轨道的信息
        private ArrayList extLists = new ArrayList();
        private static bool isQiQiuYun = false;
        //存放Range信息，允许用户只下载部分视频
        private static int rangeStart = 0;
        private static int rangeEnd = -1;
        //存放Range信息，允许用户只下载部分视频
        private static string durStart = "";
        private static string durEnd = "";
        //是否自动清除优酷广告分片
        private static bool delAd = true;
        //标记是否已清除优酷广告分片
        private static bool hasAd = false;

        public string BaseUrl { get => baseUrl; set => baseUrl = value; }
        public string M3u8Url { get => m3u8Url; set => m3u8Url = value; }
        public string DownDir { get => downDir; set => downDir = value; }
        public string DownName { get => downName; set => downName = value; }
        public static int RangeStart { get => rangeStart; set => rangeStart = value; }
        public static int RangeEnd { get => rangeEnd; set => rangeEnd = value; }
        public static bool DelAd { get => delAd; set => delAd = value; }
        public static string DurStart { get => durStart; set => durStart = value; }
        public static string DurEnd { get => durEnd; set => durEnd = value; }
        public bool LiveStream { get => liveStream; set => liveStream = value; }

        public bool Parse(out string Err)
        {
            m3u8SavePath = Path.Combine(DownDir, "raw.m3u8");
            jsonSavePath = Path.Combine(DownDir, "meta.json");

            if (!Directory.Exists(DownDir))//若文件夹不存在则新建文件夹   
                Directory.CreateDirectory(DownDir); //新建文件夹  

            //存放分部的所有信息(#EXT-X-DISCONTINUITY)
            JArray parts = new JArray();
            //存放分片的所有信息
            JArray segments = new JArray();
            JObject segInfo = new JObject();
            extLists.Clear();
            string m3u8Content = string.Empty;
            string m3u8Method = string.Empty;
            string[] extMAP = { "", "" };
            string[] extList = new string[10];
            long segIndex = 0;
            long startIndex = 0;
            int targetDuration = 0;
            double totalDuration = 0;
            bool expectSegment = false, expectPlaylist = false, isIFramesOnly = false, isIndependentSegments = false, isEndlist = false, isAd = false, isM3u = false;


            //获取m3u8内容

            if (M3u8Url.StartsWith("http"))
            {
                m3u8Content = HttpLib.Http.Get(M3u8Url).redirect(true).request();
                if (string.IsNullOrEmpty(m3u8Content))
                {
                    Err = "内容为空";
                    return false;
                }
            }
            else if (M3u8Url.StartsWith("file:"))
            {
                Uri t = new Uri(M3u8Url);
                m3u8Content = File.ReadAllText(t.LocalPath);
            }
            else if (File.Exists(M3u8Url))
            {
                m3u8Content = File.ReadAllText(M3u8Url);
                if (!M3u8Url.Contains("\\"))
                    M3u8Url = Path.Combine(Environment.CurrentDirectory, M3u8Url);
                Uri t = new Uri(M3u8Url);
                M3u8Url = t.ToString();
            }

            if (string.IsNullOrEmpty(m3u8Content))
            {
                Err = "内容为空";
                return false;
            }

            if (m3u8Content.Contains("qiqiuyun.net/") || m3u8Content.Contains("aliyunedu.net/") || m3u8Content.Contains("qncdn.edusoho.net/")) //气球云
                isQiQiuYun = true;

            if (M3u8Url.Contains("tlivecloud-playback-cdn.ysp.cctv.cn") && M3u8Url.Contains("endtime="))
                isEndlist = true;

            //输出m3u8文件
            File.WriteAllText(m3u8SavePath, m3u8Content);

            //如果BaseUrl为空则截取字符串充当
            if (BaseUrl == "")
            {
                if (new Regex("#YUMING\\|(.*)").IsMatch(m3u8Content))
                    BaseUrl = new Regex("#YUMING\\|(.*)").Match(m3u8Content).Groups[1].Value;
                else
                    BaseUrl = GetBaseUrl(M3u8Url);
            }



            //逐行分析
            using (StringReader sr = new StringReader(m3u8Content))
            {
                string line;
                double segDuration = 0;
                string segUrl = string.Empty;
                //#EXT-X-BYTERANGE:<n>[@<o>]
                long expectByte = -1; //parm n
                long startByte = 0;  //parm o

                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    if (line.StartsWith(HLSTags.ext_m3u))
                        isM3u = true;
                    //只下载部分字节
                    else if (line.StartsWith(HLSTags.ext_x_byterange))
                    {
                        string[] t = line.Replace(HLSTags.ext_x_byterange + ":", "").Split('@');
                        if (t.Length > 0)
                        {
                            if (t.Length == 1)
                            {
                                expectByte = Convert.ToInt64(t[0]);
                                segInfo.Add("expectByte", expectByte);
                            }
                            if (t.Length == 2)
                            {
                                expectByte = Convert.ToInt64(t[0]);
                                startByte = Convert.ToInt64(t[1]);
                                segInfo.Add("expectByte", expectByte);
                                segInfo.Add("startByte", startByte);
                            }
                        }
                        expectSegment = true;
                    }
                    //国家地理去广告
                    else if (line.StartsWith("#UPLYNK-SEGMENT"))
                    {
                        if (line.Contains(",ad"))
                            isAd = true;
                        else if (line.Contains(",segment"))
                            isAd = false;
                    }
                    //国家地理去广告
                    else if (isAd)
                    {
                        continue;
                    }
                    //解析定义的分段长度
                    else if (line.StartsWith(HLSTags.ext_x_targetduration))
                    {
                        targetDuration = Convert.ToInt32(Convert.ToDouble(line.Replace(HLSTags.ext_x_targetduration + ":", "").Trim()));
                    }
                    //解析起始编号
                    else if (line.StartsWith(HLSTags.ext_x_media_sequence))
                    {
                        segIndex = Convert.ToInt64(line.Replace(HLSTags.ext_x_media_sequence + ":", "").Trim());
                        startIndex = segIndex;
                    }
                    else if (line.StartsWith(HLSTags.ext_x_discontinuity_sequence)) ;
                    else if (line.StartsWith(HLSTags.ext_x_program_date_time)) ;
                    //解析不连续标记，需要单独合并（timestamp不同）
                    else if (line.StartsWith(HLSTags.ext_x_discontinuity))
                    {
                        //修复优酷去除广告后的遗留问题
                        if (hasAd && parts.Count > 0)
                        {
                            segments = (JArray)parts[parts.Count - 1];
                            parts.RemoveAt(parts.Count - 1);
                            hasAd = false;
                            continue;
                        }
                        //常规情况的#EXT-X-DISCONTINUITY标记，新建part
                        if (!hasAd && segments.Count > 1)
                        {
                            parts.Add(segments);
                            segments = new JArray();
                        }
                    }
                    else if (line.StartsWith(HLSTags.ext_x_cue_out)) ;
                    else if (line.StartsWith(HLSTags.ext_x_cue_out_start)) ;
                    else if (line.StartsWith(HLSTags.ext_x_cue_span)) ;
                    else if (line.StartsWith(HLSTags.ext_x_version)) ;
                    else if (line.StartsWith(HLSTags.ext_x_allow_cache)) ;
                    //解析KEY
                    else if (line.StartsWith(HLSTags.ext_x_key))
                    {
                        //m3u8CurrentKey = ParseKey(line);
                    }
                    //解析分片时长(暂时不考虑标题属性)
                    else if (line.StartsWith(HLSTags.extinf))
                    {
                        string[] tmp = line.Replace(HLSTags.extinf + ":", "").Split(',');
                        segDuration = Convert.ToDouble(tmp[0]);
                        segInfo.Add("index", segIndex);
                        segInfo.Add("method", m3u8CurrentKey[0]);
                        //是否有加密，有的话写入KEY和IV
                        if (m3u8CurrentKey[0] != "NONE")
                        {
                            segInfo.Add("key", m3u8CurrentKey[1]);
                            //没有读取到IV，自己生成
                            if (m3u8CurrentKey[2] == "")
                                segInfo.Add("iv", "0x" + Convert.ToString(segIndex, 2).PadLeft(32, '0'));
                            else
                                segInfo.Add("iv", m3u8CurrentKey[2]);
                        }
                        totalDuration += segDuration;
                        segInfo.Add("duration", segDuration);
                        expectSegment = true;
                        segIndex++;
                    }
                    //解析STREAM属性
                    else if (line.StartsWith(HLSTags.ext_x_stream_inf))
                    {
                        expectPlaylist = true;
                        string bandwidth = Global.GetTagAttribute(line, "BANDWIDTH");
                        string average_bandwidth = Global.GetTagAttribute(line, "AVERAGE-BANDWIDTH");
                        string codecs = Global.GetTagAttribute(line, "CODECS");
                        string resolution = Global.GetTagAttribute(line, "RESOLUTION");
                        string frame_rate = Global.GetTagAttribute(line, "FRAME-RATE");
                        string hdcp_level = Global.GetTagAttribute(line, "HDCP-LEVEL");
                        string audio = Global.GetTagAttribute(line, "AUDIO");
                        string video = Global.GetTagAttribute(line, "VIDEO");
                        string subtitles = Global.GetTagAttribute(line, "SUBTITLES");
                        string closed_captions = Global.GetTagAttribute(line, "CLOSED-CAPTIONS");
                        extList = new string[] { bandwidth, average_bandwidth, codecs, resolution,
                            frame_rate,hdcp_level,audio,video,subtitles,closed_captions };
                    }
                    else if (line.StartsWith(HLSTags.ext_x_i_frame_stream_inf)) ;
                    else if (line.StartsWith(HLSTags.ext_x_media))
                    {
                        if (Global.GetTagAttribute(line, "TYPE") == "AUDIO")
                            MEDIA_AUDIO.Add(Global.GetTagAttribute(line, "GROUP-ID"), CombineURL(BaseUrl, Global.GetTagAttribute(line, "URI")));
                        if (Global.GetTagAttribute(line, "TYPE") == "SUBTITLES")
                        {
                            if (!MEDIA_SUB.ContainsKey(Global.GetTagAttribute(line, "GROUP-ID")))
                                MEDIA_SUB.Add(Global.GetTagAttribute(line, "GROUP-ID"), CombineURL(BaseUrl, Global.GetTagAttribute(line, "URI")));
                        }
                    }
                    else if (line.StartsWith(HLSTags.ext_x_playlist_type)) ;
                    else if (line.StartsWith(HLSTags.ext_i_frames_only))
                    {
                        isIFramesOnly = true;
                    }
                    else if (line.StartsWith(HLSTags.ext_is_independent_segments))
                    {
                        isIndependentSegments = true;
                    }
                    //m3u8主体结束
                    else if (line.StartsWith(HLSTags.ext_x_endlist))
                    {
                        if (segments.Count > 0)
                            parts.Add(segments);
                        segments = new JArray();
                        isEndlist = true;
                    }
                    //#EXT-X-MAP
                    else if (line.StartsWith(HLSTags.ext_x_map))
                    {
                        if (extMAP[0] == "")
                        {
                            extMAP[0] = Global.GetTagAttribute(line, "URI");
                            if (line.Contains("BYTERANGE"))
                                extMAP[1] = Global.GetTagAttribute(line, "BYTERANGE");
                            if (!extMAP[0].StartsWith("http")) extMAP[0] = CombineURL(BaseUrl, extMAP[0]);
                        }
                        //遇到了其他的map，说明已经不是一个视频了，全部丢弃即可
                        else
                        {
                            if (segments.Count > 0)
                                parts.Add(segments);
                            segments = new JArray();
                            isEndlist = true;
                            break;
                        }
                    }
                    else if (line.StartsWith(HLSTags.ext_x_start)) ;
                    //评论行不解析
                    else if (line.StartsWith("#")) continue;
                    //空白行不解析
                    else if (line.StartsWith("\r\n")) continue;
                    //解析分片的地址
                    else if (expectSegment)
                    {
                        segUrl = CombineURL(BaseUrl, line);
                        if (M3u8Url.Contains("?__gda__"))
                        {
                            segUrl += new Regex("\\?__gda__.*").Match(M3u8Url).Value;
                        }
                        segInfo.Add("segUri", segUrl);
                        segments.Add(segInfo);
                        segInfo = new JObject();
                        //优酷的广告分段则清除此分片
                        //需要注意，遇到广告说明程序对上文的#EXT-X-DISCONTINUITY做出的动作是不必要的，
                        //其实上下文是同一种编码，需要恢复到原先的part上
                        if (DelAd && segUrl.Contains("ccode=") && segUrl.Contains("/ad/") && segUrl.Contains("duration="))
                        {
                            segments.RemoveAt(segments.Count - 1);
                            segIndex--;
                            hasAd = true;
                        }
                        //优酷广告(4K分辨率测试)
                        if (DelAd && segUrl.Contains("ccode=0902") && segUrl.Contains("duration="))
                        {
                            segments.RemoveAt(segments.Count - 1);
                            segIndex--;
                            hasAd = true;
                        }
                        expectSegment = false;
                    }
                    //解析STREAM属性的URI
                    else if (expectPlaylist)
                    {
                        string listUrl;
                        listUrl = CombineURL(BaseUrl, line);
                        if (M3u8Url.Contains("?__gda__"))
                        {
                            listUrl += new Regex("\\?__gda__.*").Match(M3u8Url).Value;
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append("{");
                        sb.Append("\"URL\":\"" + listUrl + "\",");
                        for (int i = 0; i < 10; i++)
                        {
                            if (extList[i] != "")
                            {
                                switch (i)
                                {
                                    case 0:
                                        sb.Append("\"BANDWIDTH\":\"" + extList[i] + "\",");
                                        break;
                                    case 1:
                                        sb.Append("\"AVERAGE-BANDWIDTH\":\"" + extList[i] + "\",");
                                        break;
                                    case 2:
                                        sb.Append("\"CODECS\":\"" + extList[i] + "\",");
                                        break;
                                    case 3:
                                        sb.Append("\"RESOLUTION\":\"" + extList[i] + "\",");
                                        break;
                                    case 4:
                                        sb.Append("\"FRAME-RATE\":\"" + extList[i] + "\",");
                                        break;
                                    case 5:
                                        sb.Append("\"HDCP-LEVEL\":\"" + extList[i] + "\",");
                                        break;
                                    case 6:
                                        sb.Append("\"AUDIO\":\"" + extList[i] + "\",");
                                        break;
                                    case 7:
                                        sb.Append("\"VIDEO\":\"" + extList[i] + "\",");
                                        break;
                                    case 8:
                                        sb.Append("\"SUBTITLES\":\"" + extList[i] + "\",");
                                        break;
                                    case 9:
                                        sb.Append("\"CLOSED-CAPTIONS\":\"" + extList[i] + "\",");
                                        break;
                                }
                            }
                        }
                        sb.Append("}");
                        extLists.Add(sb.ToString().Replace(",}", "}"));
                        if (Convert.ToInt64(extList[0]) > bestBandwidth)
                        {
                            bestBandwidth = Convert.ToInt64(extList[0]);
                            bestUrl = listUrl;
                            bestUrlAudio = extList[6];
                            bestUrlSub = extList[8];
                        }
                        extList = new string[8];
                        expectPlaylist = false;
                    }
                }
            }

            if (isM3u == false)
            {
                Err = "无法读取m3u8";
                return false;
            }

            //直播的情况，无法遇到m3u8结束标记，需要手动将segments加入parts
            if (parts.HasValues == false)
                parts.Add(segments);


            //构造JSON文件
            JObject jsonResult = new JObject();
            jsonResult.Add("m3u8", M3u8Url);
            jsonResult.Add("m3u8BaseUri", BaseUrl);
            jsonResult.Add("updateTime", DateTime.Now.ToString("o"));
            JObject jsonM3u8Info = new JObject();
            jsonM3u8Info.Add("originalCount", segIndex - startIndex);
            jsonM3u8Info.Add("count", segIndex - startIndex);
            jsonM3u8Info.Add("vod", isEndlist);
            jsonM3u8Info.Add("targetDuration", targetDuration);
            jsonM3u8Info.Add("totalDuration", totalDuration);
            if (bestUrlAudio != "" && MEDIA_AUDIO.ContainsKey(bestUrlAudio))
            {
                jsonM3u8Info.Add("audio", MEDIA_AUDIO[bestUrlAudio]);
            }
            if (bestUrlSub != "" && MEDIA_SUB.ContainsKey(bestUrlSub))
            {
                jsonM3u8Info.Add("sub", MEDIA_SUB[bestUrlSub]);
            }
            if (extMAP[0] != "")
            {
                if (extMAP[1] == "")
                    jsonM3u8Info.Add("extMAP", extMAP[0]);
                else
                    jsonM3u8Info.Add("extMAP", extMAP[0] + "|" + extMAP[1]);
            }

            //根据DurRange来生成分片Range
            if (DurStart != "" || DurEnd != "")
            {
                double secStart = 0;
                double secEnd = -1;

                if (DurEnd == "")
                {
                    secEnd = totalDuration;
                }

                //时间码
                Regex reg2 = new Regex(@"(\d+):(\d+):(\d+)");
                if (reg2.IsMatch(DurStart))
                {
                    int HH = Convert.ToInt32(reg2.Match(DurStart).Groups[1].Value);
                    int MM = Convert.ToInt32(reg2.Match(DurStart).Groups[2].Value);
                    int SS = Convert.ToInt32(reg2.Match(DurStart).Groups[3].Value);
                    secStart = SS + MM * 60 + HH * 60 * 60;
                }
                if (reg2.IsMatch(DurEnd))
                {
                    int HH = Convert.ToInt32(reg2.Match(DurEnd).Groups[1].Value);
                    int MM = Convert.ToInt32(reg2.Match(DurEnd).Groups[2].Value);
                    int SS = Convert.ToInt32(reg2.Match(DurEnd).Groups[3].Value);
                    secEnd = SS + MM * 60 + HH * 60 * 60;
                }

                bool flag1 = false;
                bool flag2 = false;
                if (secEnd - secStart > 0)
                {
                    double dur = 0; //当前时间
                    foreach (JArray part in parts)
                    {
                        foreach (var seg in part)
                        {
                            dur += Convert.ToDouble(seg["duration"].ToString());
                            if (flag1 == false && dur > secStart)
                            {
                                RangeStart = seg["index"].Value<int>();
                                flag1 = true;
                            }

                            if (flag2 == false && dur >= secEnd)
                            {
                                RangeEnd = seg["index"].Value<int>();
                                flag2 = true;
                            }
                        }
                    }
                }
            }


            //根据Range来清除部分分片
            if (RangeStart != 0 || RangeEnd != -1)
            {
                if (RangeEnd == -1)
                    RangeEnd = (int)(segIndex - startIndex - 1);
                int newCount = 0;
                double newTotalDuration = 0;
                JArray newParts = new JArray();
                foreach (JArray part in parts)
                {
                    JArray newPart = new JArray();
                    foreach (var seg in part)
                    {
                        if (RangeStart <= seg["index"].Value<int>() && seg["index"].Value<int>() <= RangeEnd)
                        {
                            newPart.Add(seg);
                            newCount++;
                            newTotalDuration += Convert.ToDouble(seg["duration"].ToString());
                        }
                    }
                    if (newPart.Count != 0)
                        newParts.Add(newPart);
                }
                parts = newParts;
                jsonM3u8Info["count"] = newCount;
                jsonM3u8Info["totalDuration"] = newTotalDuration;
            }


            //添加
            jsonM3u8Info.Add("segments", parts);
            jsonResult.Add("m3u8Info", jsonM3u8Info);


            //输出JSON文件
            //if (!liveStream)
            //{
            //    LOGGER.WriteLine("Writing Json: [meta.json]");
            //    LOGGER.PrintLine("写出meta.json");
            //}
            File.WriteAllText(jsonSavePath, jsonResult.ToString());
            //检测是否为master list
            MasterListCheck();
            Err = null;
            return true;
        }


        public void MasterListCheck()
        {
            //若存在多个清晰度条目，输出另一个json文件存放
            if (extLists.Count != 0)
            {
                File.Copy(m3u8SavePath, Path.GetDirectoryName(m3u8SavePath) + "\\master.m3u8", true);
                //LOGGER.WriteLine("Master List Found");
                //LOGGER.PrintLine("识别到大师列表", LOGGER.Warning);
                string t = "{" + "\"masterUri\":\"" + M3u8Url + "\","
                    + "\"updateTime\":\"" + DateTime.Now.ToString("o") + "\","
                    + "\"playLists:\":[" + string.Join(",", extLists.ToArray()) + "]" + "}";
                //输出json文件
                //LOGGER.WriteLine("Writing Master List Json: [playLists.json]");
                //LOGGER.PrintLine("写出playLists.json");
                File.WriteAllText(Path.GetDirectoryName(jsonSavePath) + "\\playLists.json", Global.ConvertJsonString(t));
                //LOGGER.WriteLine("Select Playlist: " + bestUrl);
                //LOGGER.PrintLine("已自动选择最高清晰度");
                //LOGGER.WriteLine("Start Re-Parsing");
                //LOGGER.PrintLine("重新解析m3u8...", LOGGER.Warning);
                //重置Baseurl并重新解析
                M3u8Url = bestUrl;
                BaseUrl = "";
                string Err;
                Parse(out Err);
            }
        }

        //解决低版本.Net框架的一个BUG（XP上百分之百复现）
        //https://stackoverflow.com/questions/781205/getting-a-url-with-an-url-encoded-slash#
        private void ForceCanonicalPathAndQuery(Uri uri)
        {
            string paq = uri.PathAndQuery; // need to access PathAndQuery
            FieldInfo flagsFieldInfo = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
            ulong flags = (ulong)flagsFieldInfo.GetValue(uri);
            flags &= ~((ulong)0x30); // Flags.PathNotCanonical|Flags.QueryNotCanonical
            flagsFieldInfo.SetValue(uri, flags);
        }

        /// <summary>
        /// 拼接Baseurl和RelativeUrl
        /// </summary>
        /// <param name="baseurl">Baseurl</param>
        /// <param name="url">RelativeUrl</param>
        /// <returns></returns>
        public string CombineURL(string baseurl, string url)
        {
            /*
            //本地文件形式
            if (File.Exists(Path.Combine(baseurl, url)))
            {
                return Path.Combine(baseurl, url);
            }*/


            Uri uri1 = new Uri(baseurl);  //这里直接传完整的URL即可
            Uri uri2 = new Uri(uri1, url);
            ForceCanonicalPathAndQuery(uri2);  //兼容XP的低版本.Net
            url = uri2.ToString();


            /*
            if (!url.StartsWith("http")) 
            {
                if (url.StartsWith("/"))
                {
                    if (!url.Contains(":"))  // => /livelvy:livelvy/lvy1/WysAABmKyDctEW8V-13959.ts?n=vdn-gdzh-tel-1-6
                        url = baseurl.Substring(0, baseurl.Length - 1) + url;
                    else
                        url = baseurl.Substring(0, baseurl.Length - 1) + url.Substring(url.IndexOf(':'));
                }
                else
                    url = baseurl + url;
            }*/

            return url;
        }

        /// <summary>
        /// 从url中截取字符串充当baseurl
        /// </summary>
        /// <param name="m3u8url"></param>
        /// <returns></returns>
        public static string GetBaseUrl(string m3u8url)
        {
            var web = HttpLib.Http.Get(m3u8url).redirect(true).requestNone();
            if (web != null)
            {
                string _url = web.AbsoluteUri;
                if (!isQiQiuYun)
                {
                    if (_url != m3u8url)
                    {
                        m3u8url = _url;
                    }
                }
                string url = HttpLib.Http.Get(m3u8url).redirect(true).requestNone().AbsoluteUri;
                if (url.Contains("?"))
                    url = url.Remove(url.LastIndexOf('?'));
                url = url.Substring(0, url.LastIndexOf('/') + 1);
                return url;
            }return m3u8url;
        }
    }
}
