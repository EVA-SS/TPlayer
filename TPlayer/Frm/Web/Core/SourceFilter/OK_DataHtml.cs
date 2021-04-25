using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TPlayer.Frm.Web
{
    public class OK_DataHtml
    {
        public OK_DataHtml(DataType type)
        {
            baseType = type.ToString().TrimStart('_') + "HTML";

            switch (type)
            {
                case DataType.最大资源网:
                    baseStr = "http://www.zuidazy4.com/";
                    break;
                case DataType.OK资源网:
                    //baseStr = "http://www.okzyw.com/";
                    baseStr = "https://www.okzy.co";
                    break;
                case DataType.速播资源站:
                    baseStr = "https://www.subo988.com/";
                    break;
                case DataType.麻花资源:
                    baseStr = "http://www.mahuazy.com/";
                    break;
                case DataType.最新资源网:
                    baseStr = "http://www.zuixinzy.net/";
                    break;
                case DataType._123资源网:
                    baseStr = "https://www.123ku.com/";
                    break;
                case DataType.超快资源网:
                    baseStr = "http://265zy.cc/";
                    break;
                case DataType.卧龙资源网:
                    baseStr = "https://wolongzy.net/";
                    break;
                case DataType.高清资源网:
                    baseStr = "http://cj.gaoqingzyw.com/";
                    break;
            }

            //var http = HttpLib.Http.Get(baseStr).redirect(true).requestNone();
            //if (http != null)
            //{
            //    if (!string.IsNullOrEmpty(http.AbsoluteUri))
            //    {
            //        baseStr = http.AbsoluteUri;
            //    }
            //}
        }

        public string baseType, baseStr;
        //http://www.zuidazy2.net/
        //http://www.okzy.me/   拦截
        //http://www.okzyw.com/
        public HtmlNode GetHtmlDoc(string htmlUrl)
        {
            HtmlDocument doc = new HtmlDocument();
            List<HttpLib.Val> _header = new List<HttpLib.Val> {
                new HttpLib.Val("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"),
                new HttpLib.Val("Accept-Encoding","gzip, deflate, br"),
                new HttpLib.Val("Accept-Language","zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"),
                new HttpLib.Val("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.44"),
            };
            string strHtml = HttpLib.Http.Get(htmlUrl).header(_header).redirect(true).request();
            int errCount = 0;
            while (string.IsNullOrWhiteSpace(strHtml))
            {
                strHtml = HttpLib.Http.Get(htmlUrl).header(_header).redirect(true).request();
                errCount++;
                if (errCount > 2)
                {
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(strHtml))
            {
                return null;
            }
            doc.LoadHtml(strHtml);
            return doc.DocumentNode;
        }
        /// <summary>
        /// 影片类型
        /// </summary>
        /// <param name="documenthtml"></param>
        /// <returns></returns>
        public List<TitleName> GetVideoType()
        {
            List<TitleName> titleNames = new List<TitleName>();
            HtmlNode htmlNode = GetHtmlDoc(baseStr);
            var sd = htmlNode.SelectNodes("//ul[@id='sddm']/li");
            foreach (HtmlNode item in sd)
            {
                try
                {
                    HtmlNode _a = item.SelectSingleNode("a");
                    HtmlNode _div = item.SelectSingleNode("div");
                    TitleName titleName = GetVideoType(_a);
                    //if (GreenMode)
                    //{
                    //    if (item.InnerText.Contains("福利") || item.InnerText.Contains("伦理"))
                    //    {
                    //        continue;
                    //    }
                    //}
                    if (titleName != null)
                    {
                        titleNames.Add(titleName);
                        if (_div != null)
                        {
                            var _as = _div.SelectNodes("a");
                            if (_as != null && _as.Count > 0)
                            {
                                foreach (HtmlNode items in _as)
                                {
                                    TitleName titleName2 = GetVideoType(items);
                                    if (titleName2 != null)
                                    {
                                        titleNames.Add(titleName2);
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            return titleNames;
        }
        public TitleName GetVideoType(HtmlNode _a)
        {
            try
            {
                if (!_a.InnerText.Contains("帮助中心"))
                    return new TitleName
                    {
                        titleType = _a.InnerText,
                        titleId = Regex.Match(_a.Attributes["href"].Value, "vod-type-id-([0-9]*).html").Groups[1].Value
                    };

            }
            catch { }
            return null;
        }

        /// <summary>
        /// 影片列表获取
        /// </summary>
        /// <param name="urlHome"></param>
        public TitleName GetVideoList(string htmlUrl)
        {
            TitleName titleName = new TitleName();
            if (!htmlUrl.StartsWith("http"))
            {
                htmlUrl = baseStr + htmlUrl;
            }
            HtmlNode htmlNode = GetHtmlDoc(htmlUrl);
            if (htmlNode == null)
            {
                titleName.titleVideos = new List<Video>();
                return titleName;
            }
            //主页影片列表获取
            string xpathTitle = "//span[@class='xing_vb4']/..";
            var htmls = htmlNode.SelectNodes(xpathTitle);
            if (htmls == null) return null;

            List<Video> videos = new List<Video>();
            foreach (var item in htmls)
            {
                Video video = new Video();
                //标识ID
                MatchCollection matchCollection = Regex.Matches(item.SelectSingleNode("./span[@class='xing_vb4']/a").Attributes["href"].Value, "vod-detail-id-([0-9]*).html");
                foreach (Match item1 in matchCollection)
                {
                    video.id = Convert.ToInt32(item1.Groups[1].Value);
                }
                //影片名
                video.videoName = item.SelectSingleNode("./span[@class='xing_vb4']").InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("&nbsp", " ").Trim();
                //封面影片地址
                // video.videoImgUrl = item.SelectSingleNode("./span[@class='xing_vb4']/a").Attributes["href"].Value;
                //影片类型
                video.videoType = item.SelectSingleNode("./span[@class='xing_vb5']").InnerText;
                //if (GreenMode)
                //{
                //    if (video.videoType.Contains("伦理片") || video.videoType.Contains("福利片"))
                //    {
                //        continue;
                //    }
                //}
                //更新时间
                video.videoUpdateTime = item.SelectSingleNode("./span[@class='xing_vb6']|./span[@class='xing_vb7']").InnerText;
                videos.Add(video);
            }
            titleName.titleVideos = videos;
            string szdf = htmlNode.SelectSingleNode("//div[@class='pages']").InnerText;
            titleName.pageCount = " " + Regex.Match(szdf, "当前:(.+?)页").Value;//当前: 1 / 890页
            return titleName;
        }

        /// <summary>
        /// 影片信息获取  
        /// </summary>
        /// <param name="childPageId"></param>
        public Video GetVideoInfo(int videoId)
        {
            try
            {
                string videoUrl = baseStr + "?m=vod-detail-id-" + videoId + ".html";
                Video videoInfo = new Video();
                HtmlNode rootnode1 = GetHtmlDoc(videoUrl);

                //

                HtmlNode vodImg = rootnode1.SelectSingleNode("//div[@class='vodImg']");
                HtmlNode vodInfo = rootnode1.SelectSingleNode("//div[@class='vodInfo']");


                HtmlNode vodImgSrc = vodImg.SelectSingleNode("//img[@class='lazy']/@src");

                videoInfo.videoImgUrl = vodImgSrc.Attributes["src"].Value;//影片图片地址
                videoInfo.videoName = vodImgSrc.Attributes["alt"].Value;//影片名

                HtmlNode vodInfoVodh = vodInfo.SelectSingleNode("//div[@class='vodh']");
                videoInfo.videoType = vodInfoVodh.SelectSingleNode("span").InnerText;
                videoInfo.videoQuality = vodInfoVodh.SelectSingleNode("label").InnerText;


                //影片详情
                try
                {
                    var htmlVodinfobox = vodInfo.SelectNodes("//div[@class='vodinfobox']/ul/li").Where(FuncVodinfobox);
                    List<string> tset = (from a in htmlVodinfobox select a.InnerHtml).ToList();

                    List<VideoInfo> strResult = new List<VideoInfo>();
                    foreach (string item in tset)
                    {
                        if (!string.IsNullOrEmpty(item) && item.Contains("："))
                        {
                            //别名：<span></span>
                            string[] items = item.Split('：');
                            string _value = items[1].Trim();

                            HtmlDocument docs = new HtmlDocument();
                            docs.LoadHtml(_value);

                            strResult.Add(new VideoInfo
                            {
                                key = items[0].Trim(),
                                value = docs.DocumentNode.InnerText.Replace("&nbsp;", " ").Trim()
                            });
                        }
                        else
                        {
                        }
                    }
                    videoInfo.videoTotalInfo = strResult;
                }
                catch { }

                //string vodin = "//div[@class='vodinfobox']/ul/li";
                //var htmle = rootnode1.SelectNodes(vodin);

                //剧情介绍
                videoInfo.videoSynopsis = Compress(rootnode1.SelectSingleNode("//div[@class='vodplayinfo']").InnerText);

                //地址
                videoInfo.playInformation = new List<PlayAddress>();



                HtmlNodeCollection playType_ = rootnode1.SelectNodes("//div[@class='vodplayinfo']/div");
                if (playType_ != null)
                {
                    foreach (HtmlNode playTypeOne in playType_)
                    {
                        var playType = playTypeOne.SelectNodes("//span[@class='suf']");
                        if (playType != null)
                        {
                            foreach (HtmlNode item in playType)
                            {
                                PlayAddress routing = new PlayAddress();
                                routing.playType = item.InnerText;
                                routing.videoUrl = new List<VideoUrl>();
                                var Urls = item.SelectNodes("../../ul/li");
                                foreach (var item1 in Urls)
                                {
                                    VideoUrl videourl = new VideoUrl();
                                    string[] ars = item1.InnerText.Split('$');
                                    videourl.playName = ars[0] != null ? ars[0] : " ";
                                    videourl.playURL = ars[1] != null ? ars[1] : " ";
                                    routing.videoUrl.Add(videourl);
                                }
                                videoInfo.playInformation.Add(routing);
                            }
                            if (videoInfo.playInformation.Count > 0)
                            {
                                return videoInfo;
                            }
                        }
                        else
                        {
                            var playTypes = playTypeOne.SelectNodes("h3");
                            if (playTypes != null)
                            {
                                foreach (var item in playTypes)
                                {
                                    //播放类型：
                                    PlayAddress routing = new PlayAddress();
                                    routing.playType = item.InnerText;
                                    if (routing.playType.Contains("："))
                                    {
                                        routing.playType = routing.playType.Split('：')[1].Trim();
                                    }
                                    routing.videoUrl = new List<VideoUrl>();
                                    var Urls = item.SelectNodes("../ul/li");
                                    if (Urls != null)
                                    {
                                        foreach (var item1 in Urls)
                                        {
                                            VideoUrl videourl = new VideoUrl();
                                            string[] ars = item1.InnerText.Split('$');
                                            videourl.playName = ars[0] != null ? ars[0] : " ";
                                            videourl.playURL = ars[1] != null ? ars[1] : " ";
                                            routing.videoUrl.Add(videourl);
                                        }
                                        videoInfo.playInformation.Add(routing);
                                    }
                                }

                            }
                        }
                    }
                }
                return videoInfo;
            }
            catch { return null; }
        }
        /// <summary>
        /// 去除剧情介绍多余字符
        /// </summary>
        /// <returns></returns>
        public static string Compress(string strHTML)
        {
            strHTML = strHTML.Replace("&nbsp;", " ").Replace("\n     \n", "\n").Replace("\n    \n", "\n").Replace("&quot", "");
            return strHTML;
        }
        private bool FuncVodinfobox(HtmlNode htmlNode)
        {
            if (htmlNode.Attributes.Count > 0)
            {
                if (htmlNode.Attributes["class"].Value == "tags" || htmlNode.Attributes["class"].Value == "cont")
                {
                    return false;
                }
            }
            return true;
        }
    }

    public enum DataType
    {
        最大资源网, OK资源网, 速播资源站, 麻花资源, 最新资源网, _123资源网, 超快资源网, 卧龙资源网, 高清资源网
    }
}
