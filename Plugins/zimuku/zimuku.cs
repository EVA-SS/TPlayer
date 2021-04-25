using PluginHelper;
using System;
using System.Collections.Generic;

namespace zimuku
{
    public class zimuku : SubtitleWebPlugin
    {
        //http://zimuku.la/search?q=%E9%97%AA%E7%94%B5%E4%BE%A0
        //http://zimuku.la/detail/132247.html#subinfo
        //http://zmk.pw/dld/132247.html
        public string PluginName => "字幕库";

        public Version PluginVersion => new Version(1, 0, 0, 0);

        string urlbase = "http://zimuku.la";
        public List<SubtitleWebList> Search(string search)
        {
            List<SubtitleWebList> zimu1s = new List<SubtitleWebList>();

            List<HttpLib.Val> _conmand = new List<HttpLib.Val> {
                new HttpLib.Val("q",search)
            };
            List<HttpLib.Val> _header = new List<HttpLib.Val> {
                    new HttpLib.Val("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"),
                    new HttpLib.Val("Accept-Encoding","gzip, deflate"),
                    new HttpLib.Val("Accept-Language","zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"),
                    new HttpLib.Val("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.37"),
                };
            string html = HttpLib.Http.Get(urlbase + "/search").header(_header).data(_conmand).redirect(true).request();

            if (!string.IsNullOrEmpty(html))
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                HtmlAgilityPack.HtmlNode htmlNode = doc.DocumentNode;
                var sd = htmlNode.SelectNodes("//div[@class='box clearfix']/div[@class='item prel clearfix']");
                if (sd != null)
                {
                    foreach (HtmlAgilityPack.HtmlNode item in sd)
                    {
                        try
                        {
                            HtmlAgilityPack.HtmlNode main1 = item.SelectSingleNode("div[1]/a");
                            HtmlAgilityPack.HtmlNode main1_img = main1.SelectSingleNode("img");

                            HtmlAgilityPack.HtmlNode main2 = item.SelectSingleNode("div[2]");

                            var main2_table = main2.SelectNodes("div/table/tbody/tr");

                            SubtitleWebList zimu1 = new SubtitleWebList
                            {
                                title = main2.SelectSingleNode("p[1]/a").InnerText,
                                intro = main2.SelectSingleNode("p[2]/a").InnerText,
                                img = "http:" + main1_img.GetAttributeValue("data-original", null),
                                url = urlbase + main1.GetAttributeValue("href", null),
                                data = new List<SubtitleWebItem>(),
                            };
                            foreach (HtmlAgilityPack.HtmlNode table in main2_table)
                            {
                                try
                                {
                                    HtmlAgilityPack.HtmlNode td1 = table.SelectSingleNode("td[1]");
                                    HtmlAgilityPack.HtmlNode td2 = table.SelectSingleNode("td[2]/div/i");

                                    HtmlAgilityPack.HtmlNode td11 = td1.SelectSingleNode("img");
                                    if (td2 != null && td11 != null)
                                    {
                                        HtmlAgilityPack.HtmlNode td1a = td1.SelectSingleNode("a");
                                        string type = td2.GetAttributeValue("title", null).TrimEnd('分');
                                        SubtitleWebItem zimu2 = new SubtitleWebItem
                                        {
                                            imgalt = td11.GetAttributeValue("alt", null).Replace("&nbsp;", " ").Trim(),
                                            imgsrc = urlbase + td11.GetAttributeValue("src", null),
                                            title = td1a.InnerText,
                                            url = urlbase + td1a.GetAttributeValue("href", null),
                                            down = table.SelectSingleNode("td[3]").InnerText,
                                        };
                                        if (type.StartsWith("字幕质量:"))
                                        {
                                            type = type.Remove(0, 5).Trim();
                                            int _type;
                                            if (int.TryParse(type, out _type))
                                            {
                                                zimu2.type = _type;
                                            }
                                        }
                                        zimu1.data.Add(zimu2);
                                    }

                                }
                                catch { }
                            }
                            if (zimu1.data.Count > 0)
                            {
                                zimu1s.Add(zimu1);
                            }
                        }
                        catch { }
                    }
                }
            }

            return zimu1s;
        }
    }
}
