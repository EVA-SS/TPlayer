using System;
using System.Collections.Generic;
using System.Diagnostics;
using TPlayerSupport;

namespace TPlayer
{
    /// <summary>
    /// 更新核心
    /// </summary>
    public class UpdateCore
    {
        static string UpdateUrl = "https://github.com/Haku-Men/tplayer.io/";
        //static string UpdateUrl = "https://github.com/giant-app/LiveWallpaper/";
        /// <summary>
        /// 判断是否需要更新
        /// </summary>
        /// <param name="verson">系统当前版本号</param>
        /// <param name="info">返回更新信息</param>
        /// <param name="htmlErr">是否请求失败</param>
        /// <param name="pre">是否更新预览版</param>
        /// <returns>是否需要更新</returns>
        public static bool Update(string verson, out UpdateInfo info, out bool htmlErr, bool pre = false)
        {
            return Update(new Version(verson), out info, out htmlErr, pre);
        }
        /// <summary>
        /// 判断是否需要更新
        /// </summary>
        /// <param name="verson">系统当前版本号</param>
        /// <param name="info">返回更新信息</param>
        /// <param name="htmlErr">是否请求失败</param>
        /// <param name="pre">是否更新预览版</param>
        /// <returns>是否需要更新</returns>
        public static bool Update(Version verson, out UpdateInfo info, out bool htmlErr, bool pre = false)
        {
            htmlErr = false;
            if (pre)
            {
                HttpLib.WebResult webResult;
                string html = HttpLib.Http.Get(UpdateUrl + "releases").redirect(true).request(out webResult);
                if (webResult != null)
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    info = GetUpdateInfo(doc.DocumentNode);
                    if (info == null || string.IsNullOrEmpty(info.verson))
                    {
                        return false;
                    }
                    Version newverson = info.verson.GetVersion();
                    int tm = newverson.CompareTo(verson);
                    if (tm > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else { htmlErr = true; }
            }
            else
            {
                HttpLib.WebResult webResult;
                string html = HttpLib.Http.Get(UpdateUrl + "releases/latest").redirect(true).request(out webResult);
                if (webResult != null)
                {
                    string Newversion = webResult.AbsoluteUri.Substring(webResult.AbsoluteUri.LastIndexOf("/") + 1);
                    Version newverson = Newversion.GetVersion();

                    int tm = newverson.CompareTo(verson);
                    if (tm > 0)
                    {
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(html);
                        info = GetUpdateInfo(doc.DocumentNode, Newversion);
                        if (info != null)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            info = null;
            return false;
        }
        static HtmlAgilityPack.HtmlNode GetReleaseMain(HtmlAgilityPack.HtmlNode doc)
        {
            HtmlAgilityPack.HtmlNode releaseMain = doc.SelectSingleNode("//div[contains(@class,'release-entry')]");
            if (releaseMain == null)
            {
                releaseMain = doc.SelectSingleNode("//div[contains(@class,'label-latest')]");
            }
            if (releaseMain == null)
            {
                return doc;
            }
            else
            {
                return releaseMain;
            }
        }
        static UpdateInfo GetUpdateInfo(HtmlAgilityPack.HtmlNode doc, string NewVersion = null)
        {
            UpdateInfo updateInfo = new UpdateInfo
            {
                verson = NewVersion,
                files = new List<UpdateFileInfo>(),
                desc = new List<string>()
            };

            //label-latest
            HtmlAgilityPack.HtmlNode releaseMain = GetReleaseMain(doc);

            HtmlAgilityPack.HtmlNode Pre = releaseMain.SelectSingleNode("//div[contains(@class,'flex-self-start')]");
            HtmlAgilityPack.HtmlNode header = releaseMain.SelectSingleNode("//div[contains(@class,'release-header')]");
            HtmlAgilityPack.HtmlNode body = releaseMain.SelectSingleNode("//div[contains(@class,'markdown-body')]");
            if (Pre != null)
            {
                if (Pre.InnerText.Contains("Pre"))
                {
                    updateInfo.pre = true;
                }
            }
            if (header != null)
            {
                if (NewVersion != null)
                {
                    HtmlAgilityPack.HtmlNode headers = header.SelectSingleNode("div[1]/div[1]");
                    if (headers != null)
                    {
                        updateInfo.title = headers.InnerText.Replace("\n", "").Trim();
                    }
                }
                else
                {
                    HtmlAgilityPack.HtmlNode headers = header.SelectSingleNode("div[1]/div[1]/a[1]");
                    if (headers != null)
                    {
                        string _Newversion = headers.GetAttributeValue("href", null);
                        if (_Newversion != null)
                        {
                            updateInfo.verson = _Newversion.Substring(_Newversion.LastIndexOf("/") + 1).TrimStart('v');
                        }
                        updateInfo.title = headers.InnerText.Replace("\n", "").Trim();
                    }
                }
                HtmlAgilityPack.HtmlNode headersTime = header.SelectSingleNode("p[1]/relative-time[1]");
                if (headersTime != null)
                {
                    string time = headersTime.GetAttributeValue("datetime", null);//2020-07-07T07:58:07Z
                    updateInfo.time = time.ToDateTime(DateTime.Now);
                }
            }
            if (body != null)
            {
                HtmlAgilityPack.HtmlNodeCollection nodes = body.SelectNodes("p");
                if (nodes != null && nodes.Count > 0)
                {
                    foreach (HtmlAgilityPack.HtmlNode item in nodes)
                    {
                        HtmlAgilityPack.HtmlNodeCollection Anodes = item.ChildNodes;
                        if (Anodes != null && Anodes.Count > 0)
                        {
                            foreach (HtmlAgilityPack.HtmlNode Aitem in Anodes)
                            {
                                switch (Aitem.Name)
                                {
                                    case "a":
                                        string url = Aitem.GetAttributeValue("href", null);
                                        if (url != null)
                                        {
                                            updateInfo.files.Add(new UpdateFileInfo
                                            {
                                                name = Aitem.InnerText.Trim(),
                                                url = url
                                            });
                                        }
                                        break;
                                    case "#text":
                                        updateInfo.desc.Add(Aitem.InnerText.Replace("\n", "").Trim());
                                        break;
                                    default:
                                        Debug.WriteLine(Aitem.Name);
                                        break;
                                }

                            }
                        }
                        else
                        {
                            updateInfo.desc.Add(item.InnerText.Trim());
                        }
                    }
                }
                else
                {
                    var reg = new System.Text.RegularExpressions.Regex(@"(?is)<a(?:(?!href=).)*href=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>");
                    var mc = reg.Matches(body.InnerHtml);
                    foreach (System.Text.RegularExpressions.Match m in mc)
                    {
                        string url = m.Groups["url"].Value;
                        if (url.StartsWith("/files/"))
                        {
                            updateInfo.files.Add(new UpdateFileInfo
                            {
                                name = m.Groups["text"].Value,
                                url = url
                            });
                        }
                    }
                }
                //string title = header.InnerText.Trim();
            }

            if (updateInfo.files.Count == 0)
            {
                HtmlAgilityPack.HtmlNode filebody = releaseMain.SelectSingleNode("//div[contains(@class,'Box--condensed')]");
                if (filebody != null)
                {
                    HtmlAgilityPack.HtmlNodeCollection files = filebody.SelectNodes("div[1]/div");
                    if (files != null)
                    {
                        foreach (HtmlAgilityPack.HtmlNode file in files)
                        {
                            HtmlAgilityPack.HtmlNode Afiles = file.SelectSingleNode("a[1]");
                            if (Afiles != null)
                            {
                                string url = Afiles.GetAttributeValue("href", null);
                                if (url != null)
                                {
                                    if (url.StartsWith("/"))
                                    {
                                        url = "https://github.com" + url;
                                    }
                                    //https://github.com/giant-app/LiveWallpaper/archive/v1.4.67.zip
                                    updateInfo.files.Add(new UpdateFileInfo
                                    {
                                        name = Afiles.InnerText.Trim(),
                                        url = url
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return updateInfo;

        }
    }

    /// <summary>
    /// 更新信息
    /// </summary>
    public class UpdateInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 最新版本
        /// </summary>
        public string verson { get; set; }
        /// <summary>
        /// 预览版
        /// </summary>
        public bool pre { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime time { get; set; }
        /// <summary>
        /// 更新文件
        /// </summary>
        public List<UpdateFileInfo> files { get; set; }
        /// <summary>
        /// 描述集合
        /// </summary>
        public List<string> desc { get; set; }
        /// <summary>
        /// 描述格式化
        /// </summary>
        public string descTxt
        {
            get
            {
                if (desc != null && desc.Count > 0)
                {
                    return string.Join(Environment.NewLine, desc);
                }
                return null;
            }
        }
    }

    /// <summary>
    /// 更新文件信息
    /// </summary>
    public class UpdateFileInfo
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string url { get; set; }
    }
}
