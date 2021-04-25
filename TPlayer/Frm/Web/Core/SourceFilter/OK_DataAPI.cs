using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TPlayerSupport;

namespace TPlayer.Frm.Web
{
    public class OK_DataAPI
    {
        public static Ok_API GetData(string htmlUrl)
        {
            if (!htmlUrl.StartsWith("http"))
            {
                htmlUrl = baseStr + htmlUrl;
            }
            List<HttpLib.Val> _header = new List<HttpLib.Val> {
                new HttpLib.Val("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"),
                new HttpLib.Val("Accept-Encoding","gzip, deflate, br"),
                new HttpLib.Val("Accept-Language","zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"),
                new HttpLib.Val("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.44"),
            };
            return HttpLib.Http.Get(htmlUrl).header(_header).redirect(true).request().ToJson<Ok_API>();
        }
        //http://www.okzyw.com/help/#JSONAPI   API
        //https://api.okzy.tv/api.php/provide/vod/at/json/?ac=detail&t=37

        public static string baseStr = "https://api.okzy.tv/api.php/provide/vod/at/json/";

        private static string[] removeTitle = { "电影", "连续剧", "综艺", "动漫", "资讯", "解说", "公告", "头条", "微电影" };
        /// <summary>
        /// 影片类型
        /// </summary>
        /// <returns></returns>
        public static List<TitleName> GetVideoType(Ok_API dataApi)
        {
            List<TitleName> titles = new List<TitleName>();
            foreach (var item in dataApi.@class)
            {
                //if (removeTitle.Contains(item.type_name)) continue;
                //if (GreenMode)
                //{
                //    if (item.type_name.Contains("福利") || item.type_name.Contains("伦理"))
                //    {
                //        continue;
                //    }
                //}
                TitleName titleName = new TitleName();
                titleName.titleId = item.type_id;
                titleName.titleType = item.type_name;
                titles.Add(titleName);
            }
            return titles;
        }

        /// <summary>
        /// 影片列表获取 
        /// </summary>
        /// <param name="dataApi"></param>
        /// <returns></returns>
        public static TitleName GetVideoList(Ok_API dataApi)
        {
            TitleName titleName = new TitleName();
            List<Video> videos = new List<Video>();
            foreach (var item in dataApi.list)
            {
                //if (GreenMode)
                //{
                //    if (item.vod_class.Contains("伦理片") || item.vod_class.Contains("福利片"))
                //    {
                //        continue;
                //    }
                //}
                Video video = new Video();
                video.id = item.vod_id;
                video.videoName = item.vod_name;
                video.videoImgUrl = item.vod_pic;
                video.videoType = item.vod_class;
                video.videoTotalInfoTxt = item.vod_content;
                video.videoSynopsis = item.vod_content;
                List<PlayAddress> playAddresses = new List<PlayAddress>();

                //播放地址
                string[] type = item.vod_play_from.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                string[] typeURL = item.vod_play_url.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < type.Length; i++)
                {
                    if (type[i].Contains("m3u8"))
                    {
                        continue;
                    }
                    PlayAddress playAddress = new PlayAddress();
                    playAddress.playType = type[i];
                    if (type[i].Contains("kuyun"))
                    {
                        playAddress.playType = "播放";
                    }
                    List<VideoUrl> videoUrls = new List<VideoUrl>();
                    string[] urls = typeURL[i].Split('#');
                    foreach (var item2 in urls)
                    {
                        VideoUrl videoUrl = new VideoUrl();
                        string[] ars = item2.Split('$');
                        if (ars.Count() == 2)
                        {
                            videoUrl.playName = ars[0] != null ? ars[0] : " ";
                            videoUrl.playURL = ars[1] != null ? ars[1] : " ";
                        }
                        else
                        {
                            videoUrl.playName = item2;
                            videoUrl.playURL = item2;
                        }
                        videoUrls.Add(videoUrl);
                    }
                    playAddress.videoUrl = videoUrls;
                    playAddresses.Add(playAddress);
                }
                //下载地址
                PlayAddress playDown = new PlayAddress();
                if (!string.IsNullOrWhiteSpace(item.vod_down_from))
                {
                    playDown.playType = item.vod_down_from;
                    if (item.vod_down_from.Contains("xunlei"))
                    {
                        playDown.playType = "下载";
                    }
                    List<VideoUrl> xunurls = new List<VideoUrl>();
                    string[] url = item.vod_down_url.Split('#');
                    foreach (var item2 in url)
                    {
                        VideoUrl videoUrl = new VideoUrl();
                        string[] ars = item2.Split('$');
                        if (ars.Count() == 2)
                        {
                            videoUrl.playName = ars[0] != null ? ars[0] : " ";
                            videoUrl.playURL = ars[1] != null ? ars[1] : " ";
                        }
                        else
                        {
                            videoUrl.playName = item2;
                            videoUrl.playURL = item2;
                        }
                        xunurls.Add(videoUrl);
                    }
                    playDown.videoUrl = xunurls;
                    playAddresses.Add(playDown);
                }

                video.playInformation = playAddresses;
                video.videoUpdateTime = item.vod_time;
                videos.Add(video);
            }
            titleName.titleVideos = videos;
            titleName.pageCount = dataApi.pagecount;
            return titleName;
        }
    }
}
